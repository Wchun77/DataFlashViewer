using System;
using System.Drawing;
using System.Windows.Forms;

namespace DataFlashViewer
{
    internal class DataSizeInputForm : Form
    {
        private Label lblPrompt;
        private TextBox txtValue;
        private Button btnOk;
        private Button btnCancel;

        public int DataSize { get; private set; }

        public DataSizeInputForm()
        {
            lblPrompt = new Label();
            txtValue = new TextBox();
            btnOk = new Button();
            btnCancel = new Button();

            this.SuspendLayout();

            // lblPrompt
            lblPrompt.AutoSize = false;
            lblPrompt.Location = new Point(12, 14);
            lblPrompt.Size = new Size(360, 36);
            lblPrompt.Text = "Cannot auto-detect DATA_SIZE.\n" +
                                  "Enter SAVE_DATA_LENGH value from your .h file:";

            // txtValue
            txtValue.Location = new Point(12, 58);
            txtValue.Size = new Size(120, 22);
            txtValue.Text = "68";

            // btnOk
            btnOk.Location = new Point(12, 92);
            btnOk.Size = new Size(80, 26);
            btnOk.Text = "OK";
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Click += BtnOk_Click;

            // btnCancel
            btnCancel.Location = new Point(100, 92);
            btnCancel.Size = new Size(80, 26);
            btnCancel.Text = "Cancel";
            btnCancel.DialogResult = DialogResult.Cancel;

            // Form
            this.AcceptButton = btnOk;
            this.CancelButton = btnCancel;
            this.ClientSize = new Size(386, 132);
            this.Controls.AddRange(new Control[] { lblPrompt, txtValue, btnOk, btnCancel });
            this.Font = new Font("Consolas", 9F);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "DATA_SIZE";

            this.ResumeLayout(false);
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