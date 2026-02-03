using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp3.Services
{
    public class GitHubUpdateService
    {
        private readonly HttpClient _http = new HttpClient();

        public GitHubUpdateService()
        {
            _http.DefaultRequestHeaders.UserAgent.ParseAdd("SoundCatcher-Updater/1.0");
        }

        public record ReleaseInfo(string tag_name, string html_url);

        public async Task<ReleaseInfo?> GetLatestReleaseAsync(string owner, string repo)
        {
            var url = $"https://api.github.com/repos/{owner}/{repo}/releases/latest";
            var response = await _http.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;
            var tag = root.GetProperty("tag_name").GetString() ?? "";
            var html = root.GetProperty("html_url").GetString() ?? "";
            return new ReleaseInfo(tag, html);
        }

        public async Task CheckForUpdateAsync(Form parent, string owner, string repo)
        {
            try
            {
                var latest = await GetLatestReleaseAsync(owner, repo);
                if (latest == null)
                {
                    MessageBox.Show(parent, "Не вдалося перевірити оновлення.", "Оновлення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var currentVersion = Application.ProductVersion;
                // Compare tag to product version; adjust if your tag naming differs (e.g., v1.2.3)
                var latestTag = latest.tag_name?.TrimStart('v', 'V');
                var current = currentVersion?.TrimStart('v', 'V');
                if (!string.Equals(latestTag, current, StringComparison.OrdinalIgnoreCase))
                {
                    var result = MessageBox.Show(parent, $"Доступна нова версія: {latest.tag_name}. Відкрити сторінку оновлення?", "Оновлення", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {
                        try { System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo { FileName = latest.html_url, UseShellExecute = true }); }
                        catch { /* ignore */ }
                    }
                }
                else
                {
                    MessageBox.Show(parent, "У вас встановлена остання версія.", "Оновлення", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(parent, $"Помилка перевірки оновлення: {ex.Message}", "Оновлення", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}