using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;

namespace Card_Manager
{
    public partial class UpdateForm : Form
    {
        private const string GitHubApiUrl = "https://api.github.com/repos/Sheekovic/GiftCardManager/releases/latest";
        private const string CurrentVersion = "v3.1.0"; // Your app version
        private string downloadUrl = "";

        public UpdateForm()
        {
            InitializeComponent();
        }

        private async void UpdateForm_Load(object sender, EventArgs e)
        {
            lblStatus.Text = "Checking for updates...";
            var latestVersion = await GetLatestReleaseVersion();

            if (string.IsNullOrEmpty(latestVersion))
            {
                lblStatus.Text = "Failed to check for updates.";
                await Task.Delay(2000);
                LaunchMainApp();
                return;
            }

            if (latestVersion != CurrentVersion)
            {
                lblStatus.Text = $"New version available: {latestVersion}";
                downloadUrl = await GetLatestReleaseDownloadUrl();
                if (!string.IsNullOrEmpty(downloadUrl))
                {
                    btnUpdate.Visible = true;
                }
                else
                {
                    lblStatus.Text = "No downloadable update found.";
                    await Task.Delay(2000);
                    LaunchMainApp();
                }
            }
            else
            {
                lblStatus.Text = "You're using the latest version.";
                await Task.Delay(2000);
                LaunchMainApp();
            }
        }

        private async Task<string> GetLatestReleaseVersion()
        {
            try
            {
                using HttpClient client = new();
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0");
                var response = await client.GetStringAsync(GitHubApiUrl);
                using JsonDocument doc = JsonDocument.Parse(response);
                return doc.RootElement.GetProperty("tag_name").GetString();
            }
            catch
            {
                return null;
            }
        }

        private async Task<string> GetLatestReleaseDownloadUrl()
        {
            try
            {
                using HttpClient client = new();
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0");
                var response = await client.GetStringAsync(GitHubApiUrl);
                using JsonDocument doc = JsonDocument.Parse(response);
                foreach (var asset in doc.RootElement.GetProperty("assets").EnumerateArray())
                {
                    if (asset.GetProperty("name").GetString().EndsWith(".zip"))
                    {
                        return asset.GetProperty("browser_download_url").GetString();
                    }
                }
            }
            catch { }
            return null;
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(downloadUrl)) return;

            btnUpdate.Enabled = false;
            progressBar.Visible = true;
            lblStatus.Text = "Downloading update...";

            string appFolder = Directory.GetCurrentDirectory();
            string zipFilePath = Path.Combine(appFolder, "update_" + DateTime.Now.Ticks + ".zip");
            string batchFilePath = Path.Combine(appFolder, "updater.bat");

            try
            {
                // Download the update zip into the app folder
                using HttpClient client = new();
                var data = await client.GetByteArrayAsync(downloadUrl);
                await File.WriteAllBytesAsync(zipFilePath, data);

                // Create updater batch file
                File.WriteAllText(batchFilePath, $@"
@echo off
setlocal
cd /d ""{appFolder}""

:: Wait for the main app to close
timeout /t 2 /nobreak >nul

:: Find the latest zip file in the folder
for /f ""delims="" %%F in ('dir /b /o-d *.zip') do set ""updateZip=%%F"" & goto :found
:found

:: Ensure an update zip was found
if not defined updateZip exit

:: Extract and overwrite files
powershell -Command ""Expand-Archive -Path '%updateZip%' -DestinationPath '{appFolder}' -Force""

:: Delete update files
del ""%updateZip%""
del ""{batchFilePath}""

:: Restart the application
start """" ""{Path.Combine(appFolder, "GCM.exe")}""
exit
                ");

                // Run the batch file and exit the app
                Process.Start(new ProcessStartInfo
                {
                    FileName = batchFilePath,
                    UseShellExecute = true,
                    CreateNoWindow = true
                });

                Application.Exit();
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Update failed: " + ex.Message;
            }
        }

        private void LaunchMainApp()
        {
            this.Hide();
            var mainForm = new GiftCardManager();
            mainForm.Show();
        }
    }
}
