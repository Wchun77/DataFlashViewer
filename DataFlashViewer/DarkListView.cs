using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DataFlashViewer
{
    public partial class DarkListView : ListView
    {
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);

        public Color HeaderBackColor { get; set; } = Color.FromArgb(45, 45, 45);
        public Color HeaderForeColor { get; set; } = Color.FromArgb(160, 160, 160);
        public Color SelectionColor { get; set; } = Color.FromArgb(0, 120, 215);
        public Color SelectionForeColor { get; set; } = Color.White;

        public DarkListView()
        {
            OwnerDraw = true;
            DoubleBuffered = true;

            DrawColumnHeader += OnDrawColumnHeader;
            DrawItem += OnDrawItem;
            DrawSubItem += OnDrawSubItem;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (!this.DesignMode)
                SetWindowTheme(this.Handle, "DarkMode_Explorer", null);
        }

        private bool _fillingColumn = false;

        protected override void OnColumnWidthChanged(ColumnWidthChangedEventArgs e)
        {
            base.OnColumnWidthChanged(e);
            if (!_fillingColumn) FillLastColumn();
        }

        protected override void OnClientSizeChanged(EventArgs e)
        {
            base.OnClientSizeChanged(e);
            if (!_fillingColumn) FillLastColumn();
        }

        private void FillLastColumn()
        {
            if (Columns.Count == 0) return;
            _fillingColumn = true;
            BeginUpdate();
            int used = 0;
            for (int i = 0; i < Columns.Count - 1; i++)
                used += Columns[i].Width;
            int available = ClientSize.Width - used;
            Columns[Columns.Count - 1].Width = Math.Max(40, available);
            EndUpdate();
            _fillingColumn = false;
        }

        // ---------------------------------------------------------------
        // Header
        // ---------------------------------------------------------------
        private void OnDrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            using (var bg = new SolidBrush(HeaderBackColor))
                e.Graphics.FillRectangle(bg, e.Bounds);

            using (var border = new SolidBrush(Color.FromArgb(60, 60, 60)))
                e.Graphics.FillRectangle(border,
                    new Rectangle(e.Bounds.Right - 1, e.Bounds.Top, 1, e.Bounds.Height));

            var fmt = new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.EllipsisCharacter
            };

            using (var fg = new SolidBrush(HeaderForeColor))
                e.Graphics.DrawString(e.Header.Text, this.Font, fg,
                    new Rectangle(e.Bounds.X + 6, e.Bounds.Y, e.Bounds.Width - 8, e.Bounds.Height), fmt);
        }

        // ---------------------------------------------------------------
        // Items
        // ---------------------------------------------------------------
        private void OnDrawItem(object sender, DrawListViewItemEventArgs e)
        {
            // Handled per sub-item in OnDrawSubItem
        }

        private void OnDrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            bool selected = e.Item.Selected;
            bool focused = this.Focused;

            Color bg = selected
                ? (focused ? SelectionColor : Color.FromArgb(70, 70, 70))
                : (e.ItemIndex % 2 == 0 ? this.BackColor : Color.FromArgb(32, 32, 32));

            using (var bgBrush = new SolidBrush(bg))
                e.Graphics.FillRectangle(bgBrush, e.Bounds);

            Color fg = (selected && focused) ? SelectionForeColor : this.ForeColor;

            var fmt = new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.EllipsisCharacter
            };

            using (var fgBrush = new SolidBrush(fg))
                e.Graphics.DrawString(e.SubItem.Text, this.Font, fgBrush,
                    new Rectangle(e.Bounds.X + 4, e.Bounds.Y, e.Bounds.Width - 6, e.Bounds.Height), fmt);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            this.Invalidate();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            this.Invalidate();
        }
    }
}