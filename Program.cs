using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Card_Manager
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string exePath = Path.Combine(Application.StartupPath, "Update", "GiftCardManager.exe");
            if (File.Exists(exePath))
            {
                Process.Start(new ProcessStartInfo(exePath) { UseShellExecute = true });
                return;
            }

            Application.Run(new UpdateForm());
        }
    }
}
