using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DataFlashViewer
{
    internal partial class DataSizeInputForm : Form
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

        public int DataSize { get; private set; }

        public DataSizeInputForm()
        {
            InitializeComponent();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtValue.Text.Trim(), out int val) || val <= 0)
            {
                MessageBox.Show("Enter a valid positive integer.",
                                "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                return;
            }
            DataSize = val;
        }
    }
}