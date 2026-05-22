using System;
using System.Collections.Generic;
using System.IO;

namespace DataFlashViewer
{
    public class FlashRecord
    {
        public int RecordIndex { get; set; }  // 0 = oldest
        public int SlotIndex { get; set; }  // 0..DATA_LOOP_SIZE-1
        public int ByteOffset { get; set; }  // SlotIndex * DATA_SIZE
        public byte[] Payload { get; set; }  // DATA_SIZE-1 bytes (checksum excluded)
        public bool CrcValid { get; set; }
    }

    public class DataFlashInfo
    {
        public int DataSize { get; set; }
        public int DataLoopSize { get; set; }
        public int NewestSlot { get; set; }
        public int NextDataAddr { get; set; }
        public List<FlashRecord> Records { get; set; }
    }

    public static class DataFlashParser
    {
        private static readonly byte[] CrcTable = new byte[]
        {
            0x00, 0x5e, 0xbc, 0xe2, 0x61, 0x3f, 0xdd, 0x83,
            0xc2, 0x9c, 0x7e, 0x20, 0xa3, 0xfd, 0x1f, 0x41,
            0x9d, 0xc3, 0x21, 0x7f, 0xfc, 0xa2, 0x40, 0x1e,
            0x5f, 0x01, 0xe3, 0xbd, 0x3e, 0x60, 0x82, 0xdc,
            0x23, 0x7d, 0x9f, 0xc1, 0x42, 0x1c, 0xfe, 0xa0,
            0xe1, 0xbf, 0x5d, 0x03, 0x80, 0xde, 0x3c, 0x62,
            0xbe, 0xe0, 0x02, 0x5c, 0xdf, 0x81, 0x63, 0x3d,
            0x7c, 0x22, 0xc0, 0x9e, 0x1d, 0x43, 0xa1, 0xff,
            0x46, 0x18, 0xfa, 0xa4, 0x27, 0x79, 0x9b, 0xc5,
            0x84, 0xda, 0x38, 0x66, 0xe5, 0xbb, 0x59, 0x07,
            0xdb, 0x85, 0x67, 0x39, 0xba, 0xe4, 0x06, 0x58,
            0x19, 0x47, 0xa5, 0xfb, 0x78, 0x26, 0xc4, 0x9a,
            0x65, 0x3b, 0xd9, 0x87, 0x04, 0x5a, 0xb8, 0xe6,
            0xa7, 0xf9, 0x1b, 0x45, 0xc6, 0x98, 0x7a, 0x24,
            0xf8, 0xa6, 0x44, 0x1a, 0x99, 0xc7, 0x25, 0x7b,
            0x3a, 0x64, 0x86, 0xd8, 0x5b, 0x05, 0xe7, 0xb9,
            0x8c, 0xd2, 0x30, 0x6e, 0xed, 0xb3, 0x51, 0x0f,
            0x4e, 0x10, 0xf2, 0xac, 0x2f, 0x71, 0x93, 0xcd,
            0x11, 0x4f, 0xad, 0xf3, 0x70, 0x2e, 0xcc, 0x92,
            0xd3, 0x8d, 0x6f, 0x31, 0xb2, 0xec, 0x0e, 0x50,
            0xaf, 0xf1, 0x13, 0x4d, 0xce, 0x90, 0x72, 0x2c,
            0x6d, 0x33, 0xd1, 0x8f, 0x0c, 0x52, 0xb0, 0xee,
            0x32, 0x6c, 0x8e, 0xd0, 0x53, 0x0d, 0xef, 0xb1,
            0xf0, 0xae, 0x4c, 0x12, 0x91, 0xcf, 0x2d, 0x73,
            0xca, 0x94, 0x76, 0x28, 0xab, 0xf5, 0x17, 0x49,
            0x08, 0x56, 0xb4, 0xea, 0x69, 0x37, 0xd5, 0x8b,
            0x57, 0x09, 0xeb, 0xb5, 0x36, 0x68, 0x8a, 0xd4,
            0x95, 0xcb, 0x29, 0x77, 0xf4, 0xaa, 0x48, 0x16,
            0xe9, 0xb7, 0x55, 0x0b, 0x88, 0xd6, 0x34, 0x6a,
            0x2b, 0x75, 0x97, 0xc9, 0x4a, 0x14, 0xf6, 0xa8,
            0x74, 0x2a, 0xc8, 0x96, 0x15, 0x4b, 0xa9, 0xf7,
            0xb6, 0xe8, 0x0a, 0x54, 0xd7, 0x89, 0x6b, 0x35,
        };

        public static DataFlashInfo Parse(string filePath, int dataSize)
        {
            byte[] raw = File.ReadAllBytes(filePath);

            int dataLoopSize = raw.Length / dataSize;

            // Scan all slots
            bool[] valid = new bool[dataLoopSize];
            for (int s = 0; s < dataLoopSize; s++)
                valid[s] = IsSlotValid(raw, s, dataSize);

            // Find data_addr = first EMPTY slot scanning forward from 0
            // This is where the firmware will write next.
            int dataAddr = -1;
            for (int s = 0; s < dataLoopSize; s++)
            {
                if (IsSlotEmpty(raw, s, dataSize)) { dataAddr = s; break; }
            }
            // If no empty slot found, all slots written — dataAddr wraps to 0
            if (dataAddr < 0) dataAddr = 0;

            // Newest slot = last written = one before dataAddr
            int newestSlot = (dataAddr - 1 + dataLoopSize) % dataLoopSize;

            // Step back past any INVALID slots to last VALID
            int guard = 0;
            while (!valid[newestSlot] && guard < dataLoopSize)
            {
                newestSlot = (newestSlot - 1 + dataLoopSize) % dataLoopSize;
                guard++;
            }

            if (!valid[newestSlot])
                return new DataFlashInfo
                {
                    DataSize = dataSize,
                    DataLoopSize = dataLoopSize,
                    NewestSlot = -1,
                    NextDataAddr = dataAddr,
                    Records = new List<FlashRecord>()
                };

            int nextDataAddr = (newestSlot + 1) % dataLoopSize;

            // Reconstruct chronological order (oldest first):
            // Start from newestSlot+1, walk forward one full loop,
            // collect only VALID slots.
            var ordered = new List<int>();
            for (int i = 0; i < dataLoopSize; i++)
            {
                int s = (newestSlot + 1 + i) % dataLoopSize;
                if (valid[s]) ordered.Add(s);
            }

            // Build record list (index 0 = oldest)
            var records = new List<FlashRecord>();
            for (int i = 0; i < ordered.Count; i++)
            {
                int slot = ordered[i];
                int off = slot * dataSize;
                var payload = new byte[dataSize];
                Array.Copy(raw, off, payload, 0, dataSize);

                records.Add(new FlashRecord
                {
                    RecordIndex = i,
                    SlotIndex = slot,
                    ByteOffset = off,
                    Payload = payload,
                    CrcValid = valid[slot]
                });
            }

            return new DataFlashInfo
            {
                DataSize = dataSize,
                DataLoopSize = dataLoopSize,
                NewestSlot = newestSlot,
                NextDataAddr = nextDataAddr,
                Records = records
            };
        }

        public static byte Crc8(byte[] data, int length)
        {
            byte crc = 0;
            for (int i = 0; i < length; i++)
                crc = CrcTable[crc ^ data[i]];
            return crc;
        }

        private static bool IsSlotEmpty(byte[] raw, int slot, int dataSize)
        {
            int off = slot * dataSize;
            for (int i = 0; i < dataSize; i++)
                if (raw[off + i] != 0xFF) return false;
            return true;
        }

        private static bool IsSlotValid(byte[] raw, int slot, int dataSize)
        {
            int off = slot * dataSize;

            // Empty slot check
            bool allFF = true;
            for (int i = 0; i < dataSize; i++)
                if (raw[off + i] != 0xFF) { allFF = false; break; }
            if (allFF) return false;

            byte stored = raw[off + dataSize - 1];
            byte calc = Crc8(raw, off, dataSize - 1);
            return stored == calc;
        }

        public static byte Crc8(byte[] raw, int offset, int length)
        {
            byte crc = 0;
            for (int i = 0; i < length; i++)
                crc = CrcTable[crc ^ raw[offset + i]];
            return crc;
        }
    }
}