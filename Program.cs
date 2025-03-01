using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Card_Manager
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // Attach the Assembly Resolver to load DLLs from "Libs" folder
            AppDomain.CurrentDomain.AssemblyResolve += LoadFromLibsFolder;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Ensure the application exits fully when closed
            Application.ApplicationExit += OnApplicationExit;

            // Check for an update and run the updater if available
            string exePath = Path.Combine(Application.StartupPath, "Update", "GCM.exe");
            if (File.Exists(exePath))
            {
                Process.Start(new ProcessStartInfo(exePath) { UseShellExecute = true });
                return;
            }

            var updateForm = new UpdateForm();
            Application.Run(updateForm); // Run the update form first

            // Ensure the app continues running if updateForm closes normally
            if (!updateForm.IsDisposed)
            {
                Application.Run(new GiftCardManager());
            }
        }

        private static void OnApplicationExit(object sender, EventArgs e)
        {
            // Ensures all processes, background tasks, and threads are terminated
            Process.GetCurrentProcess().Kill();
        }


        private static Assembly LoadFromLibsFolder(object sender, ResolveEventArgs args)
        {
            // Define the path to the "Libs" folder
            string libsFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Libs");

            // Extract the DLL file name from the requested assembly
            string assemblyFileName = new AssemblyName(args.Name).Name + ".dll";
            string assemblyPath = Path.Combine(libsFolderPath, assemblyFileName);

            // If the DLL exists in the "Libs" folder, load it
            if (File.Exists(assemblyPath))
            {
                return Assembly.LoadFrom(assemblyPath);
            }

            return null; // Return null if the DLL is not found
        }
    }
}
