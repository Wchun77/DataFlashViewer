using DataFlashViewer.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DataFlashViewer
{
    public partial class FormM : Form
    {
        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            int value = 1;
            DwmSetWindowAttribute(this.Handle, DWMWA_USE_IMMERSIVE_DARK_MODE, ref value, sizeof(int));
        }
        private List<FieldDef> _fields = new List<FieldDef>();
        private DataFlashInfo _flash = null;

        public FormM()
        {
            InitializeComponent();
            Icon = Resources.sd_card;
            lvRecords.SelectedIndexChanged += LvRecords_SelectedIndexChanged;
            btnBrowseH.Click += BtnBrowseH_Click;
            btnBrowseBin.Click += BtnBrowseBin_Click;

            txtHFile.AllowDrop = true;
            txtBinFile.AllowDrop = true;
            txtHFile.DragEnter += TxtHFile_DragEnter;
            txtHFile.DragDrop += TxtHFile_DragDrop;
            txtBinFile.DragEnter += TxtBinFile_DragEnter;
            txtBinFile.DragDrop += TxtBinFile_DragDrop;
        }

        // ---------------------------------------------------------------
        // Drag and drop handlers
        // ---------------------------------------------------------------

        private void TxtHFile_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length == 1 && Path.GetExtension(files[0]).ToLower() == ".h")
                e.Effect = DragDropEffects.Copy;
        }

        private void TxtHFile_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length != 1) return;
            try
            {
                _fields = HFileParser.Parse(files[0]);
                txtHFile.Text = files[0];
                TryRefresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to parse .h file:\n" + ex.Message,
                                "Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtBinFile_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length == 1 && Path.GetExtension(files[0]).ToLower() == ".bin")
                e.Effect = DragDropEffects.Copy;
        }

        private void TxtBinFile_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length != 1) return;
            try
            {
                int dataSize = DetectDataSize(files[0]);
                _flash = DataFlashParser.Parse(files[0], dataSize);
                txtBinFile.Text = files[0];
                TryRefresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to parse .bin file:\n" + ex.Message,
                                "Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------------------------------------------------------------
        // Browse handlers
        // ---------------------------------------------------------------

        private void BtnBrowseH_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title = "Select header file";
                dlg.Filter = "C Header files (*.h)|*.h|All files (*.*)|*.*";
                if (dlg.ShowDialog() != DialogResult.OK) return;

                try
                {
                    _fields = HFileParser.Parse(dlg.FileName);
                    txtHFile.Text = dlg.FileName;
                    TryRefresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to parse .h file:\n" + ex.Message,
                                    "Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnBrowseBin_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title = "Select DataFlash bin file";
                dlg.Filter = "Binary files (*.bin)|*.bin|All files (*.*)|*.*";
                if (dlg.ShowDialog() != DialogResult.OK) return;

                try
                {
                    int dataSize = DetectDataSize(dlg.FileName);
                    _flash = DataFlashParser.Parse(dlg.FileName, dataSize);
                    txtBinFile.Text = dlg.FileName;
                    TryRefresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to parse .bin file:\n" + ex.Message,
                                    "Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ---------------------------------------------------------------
        // Attempt to refresh UI when both files are loaded
        // ---------------------------------------------------------------

        private void TryRefresh()
        {
            if (_flash == null) return;

            UpdateStats();
            PopulateRecordList();

            // Auto-select first item (newest record)
            if (lvRecords.Items.Count > 0)
            {
                lvRecords.Items[0].Selected = true;
                lvRecords.Items[0].Focused = true;
            }
        }

        // ---------------------------------------------------------------
        // Stats bar
        // ---------------------------------------------------------------

        private void UpdateStats()
        {
            lblStatSlots.Text = string.Format("Slots in block\n{0}", _flash.DataLoopSize);
            lblStatRecords.Text = string.Format("Valid records\n{0}", _flash.Records.Count);
            lblStatNewest.Text = string.Format("Newest slot\n{0}",
                                      _flash.NewestSlot >= 0 ? _flash.NewestSlot.ToString() : "—");
            lblStatNext.Text = string.Format("Active block\n{0}  (seq={1})",
                                      _flash.ActiveBlock >= 0 ? _flash.ActiveBlock.ToString() : "—",
                                      _flash.BlockSeq >= 0 ? _flash.BlockSeq.ToString() : "—");
        }

        // ---------------------------------------------------------------
        // Left panel: record list (newest first)
        // ---------------------------------------------------------------

        private void PopulateRecordList()
        {
            lvRecords.BeginUpdate();
            lvRecords.Items.Clear();

            var records = _flash.Records;

            // Display newest first: iterate records in reverse
            for (int i = records.Count - 1; i >= 0; i--)
            {
                var r = records[i];
                int displayNum = records.Count - i; // 1 = newest

                var item = new ListViewItem(displayNum.ToString());
                item.SubItems.Add(r.SlotIndex.ToString());
                item.SubItems.Add(string.Format("0x{0:X4}", r.ByteOffset));
                item.Tag = r;

                lvRecords.Items.Add(item);
            }

            lvRecords.EndUpdate();
        }

        // ---------------------------------------------------------------
        // Right panel: field tree for selected record
        // ---------------------------------------------------------------

        private void LvRecords_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvRecords.SelectedItems.Count == 0) return;

            var record = lvRecords.SelectedItems[0].Tag as FlashRecord;
            if (record == null) return;

            PopulateFieldTree(record);
        }

        private void PopulateFieldTree(FlashRecord record)
        {
            tvFields.BeginUpdate();
            tvFields.Nodes.Clear();

            if (_fields.Count == 0)
            {
                // No .h loaded: show raw bytes grouped by offset
                PopulateRawTree(record);
                tvFields.EndUpdate();
                return;
            }

            // Group fields by Group name
            var groups = new Dictionary<string, TreeNode>();

            foreach (var fd in _fields)
            {
                string groupKey = fd.Group ?? "(root)";

                if (!groups.ContainsKey(groupKey))
                {
                    var gNode = new TreeNode(groupKey);
                    gNode.ForeColor = Color.FromArgb(130, 130, 130);
                    groups[groupKey] = gNode;
                    tvFields.Nodes.Add(gNode);
                }

                string valueStr = ReadFieldValue(record.Payload, fd);
                string nodeText = string.Format("{0,-30} {1}", fd.Name, valueStr);

                var fNode = new TreeNode(nodeText);
                fNode.ForeColor = Color.FromArgb(190, 190, 190);
                fNode.Tag = fd;
                groups[groupKey].Nodes.Add(fNode);
            }

            tvFields.ExpandAll();
            tvFields.EndUpdate();
        }

        private void PopulateRawTree(FlashRecord record)
        {
            var root = new TreeNode("raw bytes");
            root.ForeColor = Color.FromArgb(130, 130, 130);

            for (int i = 0; i < record.Payload.Length; i++)
            {
                string text = string.Format("[{0:D3}]  0x{1:X2}  ({2})",
                                            i, record.Payload[i], record.Payload[i]);
                var node = new TreeNode(text);
                node.ForeColor = Color.FromArgb(190, 190, 190);
                root.Nodes.Add(node);
            }

            tvFields.Nodes.Add(root);
            root.Expand();
        }

        // ---------------------------------------------------------------
        // Read a field value from payload bytes
        // ---------------------------------------------------------------

        private string ReadFieldValue(byte[] payload, FieldDef fd)
        {
            int off = fd.Offset;

            if (off < 0 || off + fd.Size > payload.Length)
                return "—";

            if (fd.Size == 1)
            {
                byte v = payload[off];
                return fd.Signed
                    ? string.Format("0x{0:X2}  ({1})", v, (sbyte)v)
                    : string.Format("0x{0:X2}  ({1})", v, v);
            }
            else if (fd.Size == 2)
            {
                ushort v = (ushort)(payload[off] | (payload[off + 1] << 8));
                return fd.Signed
                    ? string.Format("0x{0:X4}  ({1})", v, (short)v)
                    : string.Format("0x{0:X4}  ({1})", v, v);
            }
            else if (fd.Size == 4)
            {
                uint v = (uint)(payload[off]
                              | (payload[off + 1] << 8)
                              | (payload[off + 2] << 16)
                              | (payload[off + 3] << 24));
                return fd.Signed
                    ? string.Format("0x{0:X8}  ({1})", v, (int)v)
                    : string.Format("0x{0:X8}  ({1})", v, v);
            }

            return "—";
        }

        // ---------------------------------------------------------------
        // Detect DATA_SIZE from bin file
        // Try to match SAVE_DATA_LENGH by scanning for a slot with valid CRC
        // across common sizes. Falls back to asking the user.
        // ---------------------------------------------------------------

        private int DetectDataSize(string binPath)
        {
            // Primary: read SAVE_DATA_LENGH from .h if already loaded
            if (!string.IsNullOrEmpty(txtHFile.Text) && File.Exists(txtHFile.Text))
            {
                int fromH = HFileParser.ParseDataSize(txtHFile.Text);
                if (fromH > 0) return fromH;
            }

            // Fallback: ask user
            using (var dlg = new DataSizeInputForm())
            {
                if (dlg.ShowDialog() != DialogResult.OK)
                    throw new Exception("DATA_SIZE not provided.");
                return dlg.DataSize;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine($"Form Size: {Size.Width}, {Size.Height}");
            Console.WriteLine($"Panel1 Size: {splitMain.Panel1.Width}, {splitMain.Panel1.Height}");
            Console.WriteLine($"Panel2 Size: {splitMain.Panel2.Width}, {splitMain.Panel2.Height}");
        }
    }
}