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
    internal static class Program
    {
        private const string GitHubApiUrl = "https://api.github.com/repos/Sheekovic/GiftCardManager/releases/latest";
        private const string CurrentVersion = "v3.0.0"; // Replace with your app version

        [STAThread]
        static async Task Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Console.WriteLine("Checking for updates...");
            var latestVersion = await GetLatestReleaseVersion();

            if (string.IsNullOrEmpty(latestVersion))
            {
                Console.WriteLine("Failed to fetch update information.");
                Application.Run(new GiftCardManager());
                return;
            }

            if (latestVersion != CurrentVersion)
            {
                Console.WriteLine($"New version available: {latestVersion}. Downloading update...");
                string downloadUrl = await GetLatestReleaseDownloadUrl();
                if (!string.IsNullOrEmpty(downloadUrl))
                {
                    await DownloadAndInstallUpdate(downloadUrl);
                }
            }
            else
            {
                Console.WriteLine("You are using the latest version.");
            }

            Application.Run(new UpdateForm());
        }

        private static async Task<string> GetLatestReleaseVersion()
        {
            using HttpClient client = new();
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0"); // GitHub API requires a user-agent
            var response = await client.GetStringAsync(GitHubApiUrl);
            using JsonDocument doc = JsonDocument.Parse(response);
            return doc.RootElement.GetProperty("tag_name").GetString();
        }

        private static async Task<string> GetLatestReleaseDownloadUrl()
        {
            using HttpClient client = new();
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0");
            var response = await client.GetStringAsync(GitHubApiUrl);
            using JsonDocument doc = JsonDocument.Parse(response);
            foreach (var asset in doc.RootElement.GetProperty("assets").EnumerateArray())
            {
                if (asset.GetProperty("name").GetString().EndsWith(".zip")) // Change this based on your release format
                {
                    return asset.GetProperty("browser_download_url").GetString();
                }
            }
            return null;
        }

        private static async Task DownloadAndInstallUpdate(string url)
        {
            string tempFilePath = Path.Combine(Path.GetTempPath(), "update.zip");

            using HttpClient client = new();
            var data = await client.GetByteArrayAsync(url);
            await File.WriteAllBytesAsync(tempFilePath, data);

            Console.WriteLine("Update downloaded. Extracting...");

            string extractPath = Path.Combine(Directory.GetCurrentDirectory(), "Update");
            ZipFile.ExtractToDirectory(tempFilePath, extractPath, true);

            Console.WriteLine("Update installed. Restarting...");
            Process.Start(Path.Combine(extractPath, "GCM.exe")); // Change to your executable
            Environment.Exit(0);
        }
    }
}
