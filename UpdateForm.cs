using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Card_Manager
{
    public partial class UpdateForm : Form
    {
        private const string GitHubApiUrl = "https://api.github.com/repos/Sheekovic/GiftCardManager/releases/latest";
        private const string CurrentVersion = "v3.0.0"; // Your app version
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

            string tempFilePath = Path.Combine(Path.GetTempPath(), "update.zip");

            using HttpClient client = new();
            var data = await client.GetByteArrayAsync(downloadUrl);
            await File.WriteAllBytesAsync(tempFilePath, data);

            lblStatus.Text = "Extracting update...";
            await Task.Delay(1000);

            string extractPath = Path.Combine(Directory.GetCurrentDirectory(), "Update");
            ZipFile.ExtractToDirectory(tempFilePath, extractPath, true);

            lblStatus.Text = "Update installed. Restarting...";
            await Task.Delay(2000);

            Process.Start(Path.Combine(extractPath, "GiftCardManager.exe")); // Change to your EXE name
            Environment.Exit(0);
        }

        private void LaunchMainApp()
        {
            this.Hide();
            Application.Run(new GiftCardManager());
        }
    }
}
