using System.Runtime.InteropServices;

namespace LnEoAnalyzer
{
    /// <summary>테두리 없는 창의 이동 / 최대화 / 최소화 (SpadApp 과 동일한 방식)</summary>
    public partial class MainForm
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelTitleBar_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            if (e.Clicks == 2) { ToggleMaximize(); return; }
            try
            {
                ReleaseCapture();
                SendMessage(Handle, 0x112, 0xf012, 0);
            }
            catch { }
        }

        private void panelTitleBar_MouseDoubleClick(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2) ToggleMaximize();
        }

        private void btnMaximize_Click(object? sender, EventArgs e) => ToggleMaximize();

        private void btnMinimize_Click(object? sender, EventArgs e)
        {
            formSize = ClientSize;
            WindowState = FormWindowState.Minimized;
        }

        private void MainForm_SizeChanged(object? sender, EventArgs e)
        {
            switch (WindowState)
            {
                case FormWindowState.Maximized:
                    Padding = new Padding(0, 8, 8, 0);
                    break;
                case FormWindowState.Normal:
                    if (Padding.Top != borderSize) Padding = new Padding(borderSize);
                    break;
            }
        }

        private void ToggleMaximize()
        {
            if (WindowState == FormWindowState.Normal)
            {
                formSize = ClientSize;
                MaximizedBounds = Screen.FromHandle(Handle).WorkingArea;
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
                if (formSize.Width > 0) Size = formSize;
            }
        }

        /// <summary>테두리 없는 창도 가장자리를 끌어 크기 조절 가능하게</summary>
        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x84;
            const int HTLEFT = 10, HTRIGHT = 11, HTTOP = 12, HTTOPLEFT = 13, HTTOPRIGHT = 14,
                      HTBOTTOM = 15, HTBOTTOMLEFT = 16, HTBOTTOMRIGHT = 17;
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST && WindowState == FormWindowState.Normal)
            {
                long lp = m.LParam.ToInt64();
                var p = PointToClient(new Point((short)(lp & 0xffff), (short)((lp >> 16) & 0xffff)));
                const int g = 6;
                bool l = p.X <= g, r = p.X >= ClientSize.Width - g, t = p.Y <= g, b = p.Y >= ClientSize.Height - g;
                if (t && l) m.Result = (IntPtr)HTTOPLEFT;
                else if (t && r) m.Result = (IntPtr)HTTOPRIGHT;
                else if (b && l) m.Result = (IntPtr)HTBOTTOMLEFT;
                else if (b && r) m.Result = (IntPtr)HTBOTTOMRIGHT;
                else if (l) m.Result = (IntPtr)HTLEFT;
                else if (r) m.Result = (IntPtr)HTRIGHT;
                else if (t) m.Result = (IntPtr)HTTOP;
                else if (b) m.Result = (IntPtr)HTBOTTOM;
            }
        }
    }
}
