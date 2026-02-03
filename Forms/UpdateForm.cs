using System;
using System.Diagnostics;
using System.Windows.Forms;
using WinFormsApp3.Services;

namespace WinFormsApp3.Forms
{
    public partial class UpdateForm : Form
    {
        private Label lblCurrentVersion;
        private Label lblLatestVersion;
        private Button btnCheck;
        private Button btnDownload;
        private Label lblStatus;
        private GitHubUpdateService _updater;
        private string _downloadUrl;

        public UpdateForm()
        {
            InitializeComponent();
            _updater = new GitHubUpdateService();
            LoadVersionInfo();

            // Apply current theme
            string currentTheme = Properties.Settings.Default.ThemeName;
            if (!string.IsNullOrEmpty(currentTheme))
            {
                ThemeManager.ApplyTheme(this, currentTheme);
            }
        }

        private void LoadVersionInfo()
        {
            lblCurrentVersion.Text = $"Поточна версія: {Application.ProductVersion}";
            lblLatestVersion.Text = "Остання версія: не перевірено";
            btnDownload.Enabled = false;
        }

        private async void btnCheck_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "Перевірка...";
            lblStatus.ForeColor = System.Drawing.Color.Black;
            btnCheck.Enabled = false;

            try
            {
                // Правильні дані репозиторію
                string owner = "DuradazhiVlad"; 
                string repo = "SoundCatcher";

                // Attempt to get update
                 var release = await _updater.GetLatestReleaseAsync(owner, repo);
                 if (release != null)
                 {
                     lblLatestVersion.Text = $"Остання версія: {release.tag_name}";
                     _downloadUrl = release.html_url;
                     
                     var current = Application.ProductVersion?.TrimStart('v', 'V');
                     var latest = release.tag_name?.TrimStart('v', 'V');
                     
                     if (!string.Equals(current, latest, StringComparison.OrdinalIgnoreCase))
                     {
                         lblStatus.Text = "Доступна нова версія!";
                         lblStatus.ForeColor = System.Drawing.Color.Green;
                         btnDownload.Visible = true;
                         btnDownload.Enabled = true;
                     }
                     else
                     {
                         lblStatus.Text = "У вас остання версія.";
                         lblStatus.ForeColor = System.Drawing.Color.Black;
                         btnDownload.Enabled = false;
                     }
                 }
                 else
                 {
                     lblStatus.Text = "Не вдалося отримати дані.";
                     lblStatus.ForeColor = System.Drawing.Color.Red;
                 }
            }
            catch (Exception ex)
            {
                 lblStatus.Text = "Помилка перевірки.";
                 lblStatus.ForeColor = System.Drawing.Color.Red;
                 MessageBox.Show($"Не вдалося отримати дані оновлення:\n{ex.Message}", "Помилка оновлення", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnCheck.Enabled = true;
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_downloadUrl))
            {
                try
                {
                    Process.Start(new ProcessStartInfo { FileName = _downloadUrl, UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не вдалося відкрити посилання: {ex.Message}");
                }
            }
        }

        private void InitializeComponent()
        {
            this.lblCurrentVersion = new System.Windows.Forms.Label();
            this.lblLatestVersion = new System.Windows.Forms.Label();
            this.btnCheck = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            
            // Container layout
            var mainLayout = new System.Windows.Forms.TableLayoutPanel();
            var buttonsLayout = new System.Windows.Forms.FlowLayoutPanel();

            this.SuspendLayout();
            mainLayout.SuspendLayout();
            buttonsLayout.SuspendLayout();

            // 
            // mainLayout
            // 
            mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            mainLayout.ColumnCount = 1;
            mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            
            mainLayout.RowCount = 5;
            mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F)); // Current Version
            mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F)); // Latest Version
            mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F)); // Buttons
            mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));      // Status (wrap text)
            mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F)); // Filler

            mainLayout.Padding = new System.Windows.Forms.Padding(10);
            
            // 
            // lblCurrentVersion
            // 
            this.lblCurrentVersion.AutoSize = true;
            this.lblCurrentVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentVersion.Name = "lblCurrentVersion";
            this.lblCurrentVersion.Text = "Поточна версія:";
            this.lblCurrentVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            
            // 
            // lblLatestVersion
            // 
            this.lblLatestVersion.AutoSize = true;
            this.lblLatestVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLatestVersion.Name = "lblLatestVersion";
            this.lblLatestVersion.Text = "Остання версія: не перевірено";
            this.lblLatestVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // buttonsLayout
            // 
            buttonsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            buttonsLayout.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            buttonsLayout.Controls.Add(this.btnCheck);
            buttonsLayout.Controls.Add(this.btnDownload);
            buttonsLayout.Margin = new System.Windows.Forms.Padding(0);

            // 
            // btnCheck
            // 
            this.btnCheck.AutoSize = true;
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(120, 35);
            this.btnCheck.TabIndex = 2;
            this.btnCheck.Text = "Перевірити";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            
            // 
            // btnDownload
            // 
            this.btnDownload.AutoSize = true;
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(120, 35);
            this.btnDownload.TabIndex = 3;
            this.btnDownload.Text = "Завантажити";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Enabled = false;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Text = "Натисніть перевірити, щоб дізнатись про оновлення.";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.TopLeft;

            // Add controls to mainLayout
            mainLayout.Controls.Add(this.lblCurrentVersion, 0, 0);
            mainLayout.Controls.Add(this.lblLatestVersion, 0, 1);
            mainLayout.Controls.Add(buttonsLayout, 0, 2);
            mainLayout.Controls.Add(this.lblStatus, 0, 3);

            // 
            // UpdateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 250);
            this.Controls.Add(mainLayout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Оновлення";
            
            mainLayout.ResumeLayout(false);
            mainLayout.PerformLayout();
            buttonsLayout.ResumeLayout(false);
            buttonsLayout.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
