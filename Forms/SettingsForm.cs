using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp3.Services;

namespace WinFormsApp3
{
    public partial class SettingsForm : Form
    {
        private ComboBox comboBoxTheme;
        private Button buttonApply;
        // private Button buttonUpdate;
        private TableLayoutPanel mainLayoutPanel;
        private TrackBar trackBarValue;
        private Label label1;
        private Label labelTrackBar;

        public SettingsForm()
        {
            InitializeComponent();
            
            // Apply current theme on startup
            string currentTheme = Properties.Settings.Default.ThemeName;
            if (!string.IsNullOrEmpty(currentTheme))
            {
                ThemeManager.ApplyTheme(this, currentTheme);
            }

            comboBoxTheme.Items.AddRange(ThemeManager.Themes.Keys.ToArray());
            comboBoxTheme.SelectedItem = Properties.Settings.Default.ThemeName;
            trackBarValue.Value = Properties.Settings.Default.SomeValue; // Встановлюємо значення
            labelTrackBar.Text = $"Поріг чутливості: {trackBarValue.Value}";
        }

        private void ButtonApply_Click(object sender, EventArgs e)
        {
            ApplySelectedThemeAndClose();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            // Налаштуйте форму при завантаженні форми, якщо потрібно
        }

        private void ApplySelectedThemeAndClose()
        {
            if (comboBoxTheme.SelectedItem != null)
            {
                string selectedTheme = comboBoxTheme.SelectedItem.ToString();
                Properties.Settings.Default.ThemeName = selectedTheme;
                Properties.Settings.Default.SomeValue = trackBarValue.Value; // Зберігаємо значення TrackBar
                Properties.Settings.Default.Save();

                // Apply theme to all open forms
                foreach (Form form in Application.OpenForms)
                {
                    ThemeManager.ApplyTheme(form, selectedTheme);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть тему.", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void trackBarValue_ValueChanged(object sender, EventArgs e)
        {
            labelTrackBar.Text = $"Поріг чутливості: {trackBarValue.Value}";
        }

        private void InitializeComponent()
        {
            mainLayoutPanel = new TableLayoutPanel();
            comboBoxTheme = new ComboBox();
            buttonApply = new Button();
            // buttonUpdate = new Button();
            label1 = new Label();
            trackBarValue = new TrackBar();
            labelTrackBar = new Label();
            mainLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarValue).BeginInit();
            SuspendLayout();
            // 
            // mainLayoutPanel
            // 
            mainLayoutPanel.ColumnCount = 1;
            mainLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            
            // Row 0: Label "Theme"
            mainLayoutPanel.Controls.Add(label1, 0, 0);
            // Row 1: ComboBox Theme
            mainLayoutPanel.Controls.Add(comboBoxTheme, 0, 1);
            // Row 2: Label Value (Moved UP)
            mainLayoutPanel.Controls.Add(labelTrackBar, 0, 2);
            // Row 3: TrackBar (Moved DOWN)
            mainLayoutPanel.Controls.Add(trackBarValue, 0, 3);
            // Row 4: Button Apply
            mainLayoutPanel.Controls.Add(buttonApply, 0, 4);
            
            mainLayoutPanel.Dock = DockStyle.Fill;
            mainLayoutPanel.Location = new Point(0, 0);
            mainLayoutPanel.Margin = new Padding(10);
            mainLayoutPanel.Name = "mainLayoutPanel";
            mainLayoutPanel.RowCount = 6;
            
            // Adjust row styles
            mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F)); // Label Theme
            mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F)); // ComboBox
            mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F)); // Label Value (Swapped height)
            mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F)); // TrackBar (Swapped height)
            mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Spacer/Fill
            mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F)); // Button Apply

            mainLayoutPanel.Size = new Size(500, 350);
            mainLayoutPanel.TabIndex = 0;
            
            // 
            // label1 (Theme Label)
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(100, 20);
            label1.TabIndex = 4;
            label1.Text = "Виберіть тему:";
            label1.TextAlign = ContentAlignment.BottomLeft;
            
            // 
            // comboBoxTheme
            // 
            comboBoxTheme.Dock = DockStyle.Fill;
            comboBoxTheme.FormattingEnabled = true;
            comboBoxTheme.Location = new Point(3, 33);
            comboBoxTheme.Name = "comboBoxTheme";
            comboBoxTheme.Size = new Size(494, 28);
            comboBoxTheme.TabIndex = 0;
            
            // 
            // trackBarValue
            // 
            trackBarValue.Dock = DockStyle.Fill;
            trackBarValue.Location = new Point(3, 83);
            trackBarValue.Maximum = 30;
            trackBarValue.Name = "trackBarValue";
            trackBarValue.Size = new Size(494, 54);
            trackBarValue.TabIndex = 1;
            trackBarValue.Scroll += trackBarValue_Scroll;
            trackBarValue.ValueChanged += trackBarValue_ValueChanged;
            
            // 
            // labelTrackBar
            // 
            labelTrackBar.AutoSize = true;
            labelTrackBar.Location = new Point(3, 140);
            labelTrackBar.Name = "labelTrackBar";
            labelTrackBar.Size = new Size(100, 20);
            labelTrackBar.TabIndex = 2;
            labelTrackBar.Text = $"Поріг чутливості: {trackBarValue.Value}";
            
            // 
            // buttonApply
            // 
            buttonApply.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonApply.Location = new Point(350, 300);
            buttonApply.Name = "buttonApply";
            buttonApply.Size = new Size(140, 40);
            buttonApply.TabIndex = 3;
            buttonApply.Text = "Застосувати";
            buttonApply.UseVisualStyleBackColor = true;
            buttonApply.Click += ButtonApply_Click;

            // 
            // SettingsForm
            // 
            ClientSize = new Size(500, 350);
            Controls.Add(mainLayoutPanel);
            Name = "SettingsForm";
            Text = "Налаштування";
            Load += SettingsForm_Load;
            mainLayoutPanel.ResumeLayout(false);
            mainLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarValue).EndInit();
            ResumeLayout(false);
        }

        /*
        private async void buttonUpdate_Click(object sender, EventArgs e)
        {
            var updater = new GitHubUpdateService();
            // TODO: замініть значення на ваші реальні owner/repo
            const string owner = "YOUR_OWNER";
            const string repo = "YOUR_REPO";
            await updater.CheckForUpdateAsync(this, owner, repo);
        }
        */

        private Form1 form1;
        private void trackBarValue_Scroll(object sender, EventArgs e)
        {
            //form1.threshold = (float)this.trackBar.Value;
        }
    }
}