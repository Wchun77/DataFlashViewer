using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace DataFlashViewer
{
    public class FieldDef
    {
        public string Name { get; set; }
        public int Offset { get; set; }
        public int Size { get; set; }   // bytes
        public bool Signed { get; set; }
        public string Group { get; set; }   // top-level union/struct member name
    }

    public static class HFileParser
    {
        // Extract SAVE_DATA_LENGH value from #define in .h file
        public static int ParseDataSize(string filePath)
        {
            string src = File.ReadAllText(filePath);
            src = StripComments(src);

            var m = System.Text.RegularExpressions.Regex.Match(
                src, @"#define\s+SAVE_DATA_LEN[GH]+\s+(\d+)");
            if (m.Success && int.TryParse(m.Groups[1].Value, out int val) && val > 0)
                return val;

            return -1;
        }

        // Entry point: parse .h file, return flat field list for save_setup_data_type.reg
        public static List<FieldDef> Parse(string filePath)
        {
            string src = File.ReadAllText(filePath);
            src = StripComments(src);

            string regBody = ExtractRegStructBody(src);
            if (regBody == null)
                throw new Exception("Cannot find save_setup_data_type reg struct body.");

            var fields = new List<FieldDef>();
            ParseStructBody(regBody, fields, offsetBase: 0, groupName: null);
            return fields;
        }

        // Remove // and /* */ comments
        private static string StripComments(string src)
        {
            // Block comments
            src = Regex.Replace(src, @"/\*.*?\*/", " ", RegexOptions.Singleline);
            // Line comments
            src = Regex.Replace(src, @"//[^\n]*", " ");
            return src;
        }

        // Find the outermost struct body inside save_setup_data_type that is named "reg"
        private static string ExtractRegStructBody(string src)
        {
            // Locate "save_setup_data_type"
            int typePos = src.IndexOf("save_setup_data_type");
            if (typePos < 0) return null;

            // Find the outer union/struct opening brace
            int outerOpen = src.IndexOf('{', typePos);
            if (outerOpen < 0) return null;

            string outerBody = ExtractBraceBlock(src, outerOpen);

            // Find "} reg;" inside outerBody
            // The reg member is a struct { ... } reg;
            // Scan for struct blocks followed by "} reg;"
            int searchFrom = 0;
            while (true)
            {
                int structKw = outerBody.IndexOf("struct", searchFrom);
                if (structKw < 0) break;

                int openBrace = outerBody.IndexOf('{', structKw);
                if (openBrace < 0) break;

                string candidate = ExtractBraceBlock(outerBody, openBrace);
                int closePos = openBrace + candidate.Length + 2; // position after '}'

                // Check what follows the closing brace
                string tail = outerBody.Substring(closePos).TrimStart();
                if (Regex.IsMatch(tail, @"^reg\s*;"))
                    return candidate;

                searchFrom = openBrace + 1;
            }

            return null;
        }

        // Parse a struct body, appending FieldDefs; returns bytes consumed
        private static int ParseStructBody(string body, List<FieldDef> fields,
                                           int offsetBase, string groupName)
        {
            int offset = offsetBase;
            int pos = 0;

            while (pos < body.Length)
            {
                SkipWhitespace(body, ref pos);
                if (pos >= body.Length) break;

                // Nested union or struct
                if (MatchKeyword(body, pos, "union") || MatchKeyword(body, pos, "struct"))
                {
                    bool isUnion = MatchKeyword(body, pos, "union");
                    pos += isUnion ? 5 : 6;
                    SkipWhitespace(body, ref pos);

                    // Optional tag name before '{'
                    string tag = null;
                    if (pos < body.Length && body[pos] != '{')
                        tag = ReadIdentifier(body, ref pos);

                    SkipWhitespace(body, ref pos);
                    if (pos >= body.Length || body[pos] != '{') break;

                    string block = ExtractBraceBlock(body, pos);
                    pos += block.Length + 2; // skip '{' content '}'

                    SkipWhitespace(body, ref pos);

                    // Member name after closing brace
                    string memberName = ReadIdentifier(body, ref pos);
                    SkipToSemicolon(body, ref pos);

                    string childGroup = groupName ?? memberName;

                    if (isUnion)
                    {
                        // Pick the first struct { } bits member; skip alias fields
                        string bitsBody = FindBitsStructBody(block);
                        if (bitsBody != null)
                        {
                            int consumed = ParseStructBody(bitsBody, fields, offset, childGroup);
                            offset += consumed;
                        }
                        else
                        {
                            // No bits struct — parse the block directly
                            int consumed = ParseStructBody(block, fields, offset, childGroup);
                            offset += consumed;
                        }
                    }
                    else
                    {
                        int consumed = ParseStructBody(block, fields, offset, childGroup);
                        offset += consumed;
                    }

                    continue;
                }

                // Plain field declaration: [type] [name][array]? ;
                // Supported types: uint8_t uint16_t uint32_t int8_t int16_t int32_t
                TypeInfo ti = TryReadType(body, ref pos);
                if (ti == null)
                {
                    // Skip unknown token
                    SkipToSemicolon(body, ref pos);
                    continue;
                }

                SkipWhitespace(body, ref pos);
                string fieldName = ReadIdentifier(body, ref pos);
                if (string.IsNullOrEmpty(fieldName))
                {
                    SkipToSemicolon(body, ref pos);
                    continue;
                }

                // Check for bitfield  : N
                SkipWhitespace(body, ref pos);
                bool isBitfield = pos < body.Length && body[pos] == ':';
                if (isBitfield)
                    SkipToSemicolon(body, ref pos);

                // Check for array [N]
                int arrayLen = 1;
                if (!isBitfield)
                {
                    SkipWhitespace(body, ref pos);
                    if (pos < body.Length && body[pos] == '[')
                    {
                        pos++; // skip '['
                        string numStr = "";
                        while (pos < body.Length && body[pos] != ']')
                            numStr += body[pos++];
                        if (pos < body.Length) pos++; // skip ']'
                        int.TryParse(numStr.Trim(), out arrayLen);
                    }
                    SkipToSemicolon(body, ref pos);
                }

                if (isBitfield)
                {
                    // Bitfield: emit one byte field for the whole byte,
                    // but only on the first bitfield in this byte group.
                    // Simple approach: just emit each bitfield as a named sub-byte
                    // with the parent byte offset; size = 1 for display.
                    fields.Add(new FieldDef
                    {
                        Name = fieldName,
                        Offset = offset,
                        Size = 1,
                        Signed = ti.Signed,
                        Group = groupName
                    });
                    // Do NOT advance offset for bitfields — caller handles byte boundary
                }
                else
                {
                    for (int i = 0; i < arrayLen; i++)
                    {
                        string name = arrayLen > 1
                            ? string.Format("{0}[{1}]", fieldName, i)
                            : fieldName;

                        fields.Add(new FieldDef
                        {
                            Name = name,
                            Offset = offset,
                            Size = ti.Size,
                            Signed = ti.Signed,
                            Group = groupName
                        });
                        offset += ti.Size;
                    }
                }
            }

            return offset - offsetBase;
        }

        // Inside a union block, find the first "struct { ... } bits;" and return its body
        private static string FindBitsStructBody(string block)
        {
            int pos = 0;
            while (pos < block.Length)
            {
                SkipWhitespace(block, ref pos);
                if (!MatchKeyword(block, pos, "struct")) { pos++; continue; }

                pos += 6;
                SkipWhitespace(block, ref pos);

                // Optional tag
                if (pos < block.Length && block[pos] != '{')
                    ReadIdentifier(block, ref pos);

                SkipWhitespace(block, ref pos);
                if (pos >= block.Length || block[pos] != '{') continue;

                string body = ExtractBraceBlock(block, pos);
                int closePos = pos + body.Length + 2;

                SkipWhitespace(block, ref closePos);
                string memberName = ReadIdentifier(block, ref closePos);

                // Accept any struct member name — it is the canonical field view
                if (!string.IsNullOrEmpty(memberName))
                    return body;

                pos++;
            }
            return null;
        }

        // Extract content between matching braces starting at openPos (which must be '{')
        private static string ExtractBraceBlock(string src, int openPos)
        {
            int depth = 0;
            int start = openPos + 1;
            for (int i = openPos; i < src.Length; i++)
            {
                if (src[i] == '{') depth++;
                else if (src[i] == '}')
                {
                    depth--;
                    if (depth == 0)
                        return src.Substring(start, i - start);
                }
            }
            return src.Substring(start);
        }

        private static TypeInfo TryReadType(string src, ref int pos)
        {
            SkipWhitespace(src, ref pos);
            int saved = pos;

            string[] types = { "uint32_t", "uint16_t", "uint8_t",
                                "int32_t",  "int16_t",  "int8_t" };

            foreach (string t in types)
            {
                if (pos + t.Length <= src.Length &&
                    src.Substring(pos, t.Length) == t &&
                    (pos + t.Length >= src.Length || !char.IsLetterOrDigit(src[pos + t.Length]) && src[pos + t.Length] != '_'))
                {
                    pos += t.Length;
                    bool signed = t.StartsWith("int");
                    int size = t.Contains("32") ? 4 : t.Contains("16") ? 2 : 1;
                    return new TypeInfo { Size = size, Signed = signed };
                }
            }

            pos = saved;
            return null;
        }

        private static bool MatchKeyword(string src, int pos, string kw)
        {
            if (pos + kw.Length > src.Length) return false;
            if (src.Substring(pos, kw.Length) != kw) return false;
            int after = pos + kw.Length;
            if (after < src.Length && (char.IsLetterOrDigit(src[after]) || src[after] == '_'))
                return false;
            return true;
        }

        private static string ReadIdentifier(string src, ref int pos)
        {
            SkipWhitespace(src, ref pos);
            int start = pos;
            while (pos < src.Length && (char.IsLetterOrDigit(src[pos]) || src[pos] == '_'))
                pos++;
            return src.Substring(start, pos - start);
        }

        private static void SkipWhitespace(string src, ref int pos)
        {
            while (pos < src.Length && char.IsWhiteSpace(src[pos]))
                pos++;
        }

        private static void SkipToSemicolon(string src, ref int pos)
        {
            while (pos < src.Length && src[pos] != ';')
                pos++;
            if (pos < src.Length) pos++; // skip ';'
        }

        private class TypeInfo
        {
            public int Size { get; set; }
            public bool Signed { get; set; }
        }
    }
}