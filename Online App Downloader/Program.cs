using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Online_App_Downloader
{
    internal static class Program {
        [DllImport("User32.dll")]
        private static extern bool SetProcessDpiAware();

        [STAThread]
        static void Main() {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                SetProcessDpiAware();
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
