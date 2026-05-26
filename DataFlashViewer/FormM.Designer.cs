namespace DataFlashViewer
{
    partial class FormM
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.lvRecords = new DataFlashViewer.DarkListView();
            this.colRecIdx = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSlot = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOffset = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelLeftTop = new System.Windows.Forms.Panel();
            this.lblRecordsTitle = new System.Windows.Forms.Label();
            this.panelRight = new System.Windows.Forms.Panel();
            this.tvFields = new DataFlashViewer.DarkTreeView();
            this.panelRightTop = new System.Windows.Forms.Panel();
            this.lblDetailTitle = new System.Windows.Forms.Label();
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnBrowseBin = new System.Windows.Forms.Button();
            this.txtBinFile = new System.Windows.Forms.TextBox();
            this.lblBinFile = new System.Windows.Forms.Label();
            this.btnBrowseH = new System.Windows.Forms.Button();
            this.txtHFile = new System.Windows.Forms.TextBox();
            this.lblHFile = new System.Windows.Forms.Label();
            this.lblAppTitle = new System.Windows.Forms.Label();
            this.panelStats = new System.Windows.Forms.Panel();
            this.lblStatNext = new System.Windows.Forms.Label();
            this.lblStatNewest = new System.Windows.Forms.Label();
            this.lblStatRecords = new System.Windows.Forms.Label();
            this.lblStatSlots = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelLeftTop.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.panelRightTop.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.panelStats.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitMain
            // 
            this.splitMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 130);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.panelLeft);
            this.splitMain.Panel1MinSize = 150;
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.panelRight);
            this.splitMain.Panel2MinSize = 536;
            this.splitMain.Size = new System.Drawing.Size(1200, 550);
            this.splitMain.SplitterDistance = 170;
            this.splitMain.TabIndex = 0;
            // 
            // panelLeft
            // 
            this.panelLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.panelLeft.Controls.Add(this.lvRecords);
            this.panelLeft.Controls.Add(this.panelLeftTop);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(170, 550);
            this.panelLeft.TabIndex = 0;
            // 
            // lvRecords
            // 
            this.lvRecords.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.lvRecords.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvRecords.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colRecIdx,
            this.colSlot,
            this.colOffset});
            this.lvRecords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvRecords.Font = new System.Drawing.Font("Consolas", 8.5F);
            this.lvRecords.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.lvRecords.FullRowSelect = true;
            this.lvRecords.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.lvRecords.HeaderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.lvRecords.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvRecords.HideSelection = false;
            this.lvRecords.Location = new System.Drawing.Point(0, 30);
            this.lvRecords.MultiSelect = false;
            this.lvRecords.Name = "lvRecords";
            this.lvRecords.OwnerDraw = true;
            this.lvRecords.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.lvRecords.SelectionForeColor = System.Drawing.Color.White;
            this.lvRecords.ShowItemToolTips = true;
            this.lvRecords.Size = new System.Drawing.Size(170, 520);
            this.lvRecords.TabIndex = 0;
            this.lvRecords.UseCompatibleStateImageBehavior = false;
            this.lvRecords.View = System.Windows.Forms.View.Details;
            // 
            // colRecIdx
            // 
            this.colRecIdx.Text = "#";
            this.colRecIdx.Width = 23;
            // 
            // colSlot
            // 
            this.colSlot.Text = "slot";
            this.colSlot.Width = 40;
            // 
            // colOffset
            // 
            this.colOffset.Text = "offset";
            this.colOffset.Width = 107;
            // 
            // panelLeftTop
            // 
            this.panelLeftTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelLeftTop.Controls.Add(this.lblRecordsTitle);
            this.panelLeftTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLeftTop.Location = new System.Drawing.Point(0, 0);
            this.panelLeftTop.Name = "panelLeftTop";
            this.panelLeftTop.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.panelLeftTop.Size = new System.Drawing.Size(170, 30);
            this.panelLeftTop.TabIndex = 1;
            // 
            // lblRecordsTitle
            // 
            this.lblRecordsTitle.AutoSize = true;
            this.lblRecordsTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblRecordsTitle.Font = new System.Drawing.Font("Consolas", 8.5F);
            this.lblRecordsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.lblRecordsTitle.Location = new System.Drawing.Point(10, 0);
            this.lblRecordsTitle.Name = "lblRecordsTitle";
            this.lblRecordsTitle.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.lblRecordsTitle.Size = new System.Drawing.Size(56, 22);
            this.lblRecordsTitle.TabIndex = 0;
            this.lblRecordsTitle.Text = "Records";
            this.lblRecordsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelRight
            // 
            this.panelRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.panelRight.Controls.Add(this.tvFields);
            this.panelRight.Controls.Add(this.panelRightTop);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(0, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(1026, 550);
            this.panelRight.TabIndex = 0;
            // 
            // tvFields
            // 
            this.tvFields.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.tvFields.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvFields.Font = new System.Drawing.Font("Consolas", 9F);
            this.tvFields.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.tvFields.FullRowSelect = true;
            this.tvFields.HideSelection = false;
            this.tvFields.Indent = 16;
            this.tvFields.ItemHeight = 22;
            this.tvFields.Location = new System.Drawing.Point(0, 30);
            this.tvFields.Name = "tvFields";
            this.tvFields.ShowLines = false;
            this.tvFields.ShowRootLines = false;
            this.tvFields.Size = new System.Drawing.Size(1026, 520);
            this.tvFields.TabIndex = 0;
            // 
            // panelRightTop
            // 
            this.panelRightTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelRightTop.Controls.Add(this.lblDetailTitle);
            this.panelRightTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelRightTop.Location = new System.Drawing.Point(0, 0);
            this.panelRightTop.Name = "panelRightTop";
            this.panelRightTop.Padding = new System.Windows.Forms.Padding(10, 0, 8, 0);
            this.panelRightTop.Size = new System.Drawing.Size(1026, 30);
            this.panelRightTop.TabIndex = 1;
            // 
            // lblDetailTitle
            // 
            this.lblDetailTitle.AutoSize = true;
            this.lblDetailTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblDetailTitle.Font = new System.Drawing.Font("Consolas", 8.5F);
            this.lblDetailTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.lblDetailTitle.Location = new System.Drawing.Point(10, 0);
            this.lblDetailTitle.Name = "lblDetailTitle";
            this.lblDetailTitle.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.lblDetailTitle.Size = new System.Drawing.Size(49, 22);
            this.lblDetailTitle.TabIndex = 0;
            this.lblDetailTitle.Text = "Fields";
            this.lblDetailTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.panelTop.Controls.Add(this.btnBrowseBin);
            this.panelTop.Controls.Add(this.txtBinFile);
            this.panelTop.Controls.Add(this.lblBinFile);
            this.panelTop.Controls.Add(this.btnBrowseH);
            this.panelTop.Controls.Add(this.txtHFile);
            this.panelTop.Controls.Add(this.lblHFile);
            this.panelTop.Controls.Add(this.lblAppTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.panelTop.Size = new System.Drawing.Size(1200, 78);
            this.panelTop.TabIndex = 2;
            // 
            // btnBrowseBin
            // 
            this.btnBrowseBin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseBin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnBrowseBin.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnBrowseBin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseBin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnBrowseBin.Location = new System.Drawing.Point(1092, 42);
            this.btnBrowseBin.Name = "btnBrowseBin";
            this.btnBrowseBin.Size = new System.Drawing.Size(90, 22);
            this.btnBrowseBin.TabIndex = 6;
            this.btnBrowseBin.Text = "Browse";
            this.btnBrowseBin.UseVisualStyleBackColor = false;
            // 
            // txtBinFile
            // 
            this.txtBinFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBinFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.txtBinFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBinFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.txtBinFile.Location = new System.Drawing.Point(225, 42);
            this.txtBinFile.Name = "txtBinFile";
            this.txtBinFile.ReadOnly = true;
            this.txtBinFile.Size = new System.Drawing.Size(853, 22);
            this.txtBinFile.TabIndex = 5;
            // 
            // lblBinFile
            // 
            this.lblBinFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.lblBinFile.Location = new System.Drawing.Point(170, 42);
            this.lblBinFile.Name = "lblBinFile";
            this.lblBinFile.Size = new System.Drawing.Size(49, 22);
            this.lblBinFile.TabIndex = 4;
            this.lblBinFile.Text = ".bin";
            this.lblBinFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnBrowseH
            // 
            this.btnBrowseH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseH.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnBrowseH.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnBrowseH.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseH.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnBrowseH.Location = new System.Drawing.Point(1092, 10);
            this.btnBrowseH.Name = "btnBrowseH";
            this.btnBrowseH.Size = new System.Drawing.Size(90, 22);
            this.btnBrowseH.TabIndex = 3;
            this.btnBrowseH.Text = "Browse";
            this.btnBrowseH.UseVisualStyleBackColor = false;
            // 
            // txtHFile
            // 
            this.txtHFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.txtHFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.txtHFile.Location = new System.Drawing.Point(225, 10);
            this.txtHFile.Name = "txtHFile";
            this.txtHFile.ReadOnly = true;
            this.txtHFile.Size = new System.Drawing.Size(853, 22);
            this.txtHFile.TabIndex = 2;
            // 
            // lblHFile
            // 
            this.lblHFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.lblHFile.Location = new System.Drawing.Point(170, 10);
            this.lblHFile.Name = "lblHFile";
            this.lblHFile.Size = new System.Drawing.Size(49, 22);
            this.lblHFile.TabIndex = 1;
            this.lblHFile.Text = ".h";
            this.lblHFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAppTitle
            // 
            this.lblAppTitle.AutoSize = true;
            this.lblAppTitle.Font = new System.Drawing.Font("Consolas", 11F, System.Drawing.FontStyle.Bold);
            this.lblAppTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.lblAppTitle.Location = new System.Drawing.Point(10, 12);
            this.lblAppTitle.Name = "lblAppTitle";
            this.lblAppTitle.Size = new System.Drawing.Size(136, 18);
            this.lblAppTitle.TabIndex = 0;
            this.lblAppTitle.Text = "DataFlash Viewer";
            // 
            // panelStats
            // 
            this.panelStats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            this.panelStats.Controls.Add(this.lblStatNext);
            this.panelStats.Controls.Add(this.lblStatNewest);
            this.panelStats.Controls.Add(this.lblStatRecords);
            this.panelStats.Controls.Add(this.lblStatSlots);
            this.panelStats.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelStats.Location = new System.Drawing.Point(0, 78);
            this.panelStats.Name = "panelStats";
            this.panelStats.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.panelStats.Size = new System.Drawing.Size(1200, 52);
            this.panelStats.TabIndex = 1;
            // 
            // lblStatNext
            // 
            this.lblStatNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblStatNext.Font = new System.Drawing.Font("Consolas", 8.5F);
            this.lblStatNext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.lblStatNext.Location = new System.Drawing.Point(512, 6);
            this.lblStatNext.Name = "lblStatNext";
            this.lblStatNext.Size = new System.Drawing.Size(160, 40);
            this.lblStatNext.TabIndex = 0;
            this.lblStatNext.Text = "Active block\n—";
            this.lblStatNext.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStatNewest
            // 
            this.lblStatNewest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblStatNewest.Font = new System.Drawing.Font("Consolas", 8.5F);
            this.lblStatNewest.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.lblStatNewest.Location = new System.Drawing.Point(344, 6);
            this.lblStatNewest.Name = "lblStatNewest";
            this.lblStatNewest.Size = new System.Drawing.Size(160, 40);
            this.lblStatNewest.TabIndex = 1;
            this.lblStatNewest.Text = "Newest slot\n—";
            this.lblStatNewest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStatRecords
            // 
            this.lblStatRecords.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblStatRecords.Font = new System.Drawing.Font("Consolas", 8.5F);
            this.lblStatRecords.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.lblStatRecords.Location = new System.Drawing.Point(176, 6);
            this.lblStatRecords.Name = "lblStatRecords";
            this.lblStatRecords.Size = new System.Drawing.Size(160, 40);
            this.lblStatRecords.TabIndex = 2;
            this.lblStatRecords.Text = "Valid records\n—";
            this.lblStatRecords.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStatSlots
            // 
            this.lblStatSlots.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblStatSlots.Font = new System.Drawing.Font("Consolas", 8.5F);
            this.lblStatSlots.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.lblStatSlots.Location = new System.Drawing.Point(8, 6);
            this.lblStatSlots.Name = "lblStatSlots";
            this.lblStatSlots.Size = new System.Drawing.Size(160, 40);
            this.lblStatSlots.TabIndex = 3;
            this.lblStatSlots.Text = "Slots in block\n—";
            this.lblStatSlots.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(1200, 680);
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.panelStats);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Consolas", 9F);
            this.MinimumSize = new System.Drawing.Size(706, 494);
            this.Name = "FormM";
            this.Text = "DataFlash Viewer";
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.panelLeftTop.ResumeLayout(false);
            this.panelLeftTop.PerformLayout();
            this.panelRight.ResumeLayout(false);
            this.panelRightTop.ResumeLayout(false);
            this.panelRightTop.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelStats.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelLeftTop;
        private System.Windows.Forms.Label lblRecordsTitle;
        private DataFlashViewer.DarkListView lvRecords;
        private System.Windows.Forms.ColumnHeader colRecIdx;
        private System.Windows.Forms.ColumnHeader colSlot;
        private System.Windows.Forms.ColumnHeader colOffset;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Panel panelRightTop;
        private System.Windows.Forms.Label lblDetailTitle;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblAppTitle;
        private System.Windows.Forms.Label lblHFile;
        private System.Windows.Forms.TextBox txtHFile;
        private System.Windows.Forms.Button btnBrowseH;
        private System.Windows.Forms.Label lblBinFile;
        private System.Windows.Forms.TextBox txtBinFile;
        private System.Windows.Forms.Button btnBrowseBin;
        private System.Windows.Forms.Panel panelStats;
        private System.Windows.Forms.Label lblStatSlots;
        private System.Windows.Forms.Label lblStatRecords;
        private System.Windows.Forms.Label lblStatNewest;
        private System.Windows.Forms.Label lblStatNext;
        private DarkTreeView tvFields;
    }
}