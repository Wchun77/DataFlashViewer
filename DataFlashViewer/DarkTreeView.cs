using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DataFlashViewer
{
    public partial class DarkTreeView : TreeView
    {
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);

        public DarkTreeView()
        {
            this.DoubleBuffered = true;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (!this.DesignMode)
                SetWindowTheme(this.Handle, "DarkMode_Explorer", null);
        }
    }
}