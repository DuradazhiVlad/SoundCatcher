using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsApp3
{
    public partial class SettingsForm : Form
    {
        private ComboBox comboBoxTheme;
        private Button buttonApply;
        private TableLayoutPanel mainLayoutPanel;
        private TrackBar trackBarValue;
        private Label label1;
        private Label labelTrackBar;

        public SettingsForm()
        {
            InitializeComponent();
            comboBoxTheme.Items.AddRange(ThemeManager.Themes.Keys.ToArray());
            comboBoxTheme.SelectedItem = Properties.Settings.Default.ThemeName;
            trackBarValue.Value = Properties.Settings.Default.SomeValue; // Припустимо, у вас є така настройка
            labelTrackBar.Text = $"Значення: {trackBarValue.Value}";
        }

        private void ButtonApply_Click(object sender, EventArgs e)
        {
            ApplySelectedThemeAndClose();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            // Додаткова логіка при завантаженні форми, якщо потрібно
        }

        private void ApplySelectedThemeAndClose()
        {
            if (comboBoxTheme.SelectedItem != null)
            {
                string selectedTheme = comboBoxTheme.SelectedItem.ToString();
                Properties.Settings.Default.ThemeName = selectedTheme;
                Properties.Settings.Default.SomeValue = trackBarValue.Value; // Збереження значення TrackBar
                Properties.Settings.Default.Save();

                var mainForm = Application.OpenForms.OfType<Form1>().FirstOrDefault();
                if (mainForm != null)
                {
                    ThemeManager.ApplyTheme(mainForm, selectedTheme);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть тему.", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void trackBarValue_ValueChanged(object sender, EventArgs e)
        {
            labelTrackBar.Text = $"Значення: {trackBarValue.Value}";
        }

        private void InitializeComponent()
        {
            mainLayoutPanel = new TableLayoutPanel();
            comboBoxTheme = new ComboBox();
            buttonApply = new Button();
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
            mainLayoutPanel.Controls.Add(comboBoxTheme, 0, 0);
            mainLayoutPanel.Controls.Add(buttonApply, 0, 4);
            mainLayoutPanel.Controls.Add(label1, 0, 1);
            mainLayoutPanel.Controls.Add(trackBarValue, 0, 2);
            mainLayoutPanel.Controls.Add(labelTrackBar, 0, 3);
            mainLayoutPanel.Dock = DockStyle.Fill;
            mainLayoutPanel.Location = new Point(0, 0);
            mainLayoutPanel.Margin = new Padding(4, 5, 4, 5);
            mainLayoutPanel.Name = "mainLayoutPanel";
            mainLayoutPanel.RowCount = 5;
            mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 96F));
            mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            mainLayoutPanel.RowStyles.Add(new RowStyle());
            mainLayoutPanel.RowStyles.Add(new RowStyle());
            mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 71F));
            mainLayoutPanel.Size = new Size(1021, 468);
            mainLayoutPanel.TabIndex = 0;
            // 
            // comboBoxTheme
            // 
            comboBoxTheme.Dock = DockStyle.Top;
            comboBoxTheme.FormattingEnabled = true;
            comboBoxTheme.Location = new Point(22, 16);
            comboBoxTheme.Margin = new Padding(22, 16, 22, 0);
            comboBoxTheme.Name = "comboBoxTheme";
            comboBoxTheme.Size = new Size(977, 40);
            comboBoxTheme.TabIndex = 0;
            // 
            // buttonApply
            // 
            buttonApply.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonApply.Location = new Point(782, 311);
            buttonApply.Margin = new Padding(4, 16, 22, 24);
            buttonApply.Name = "buttonApply";
            buttonApply.Size = new Size(217, 64);
            buttonApply.TabIndex = 3;
            buttonApply.Text = "Застосувати";
            buttonApply.UseVisualStyleBackColor = true;
            buttonApply.Click += ButtonApply_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 96);
            label1.Name = "label1";
            label1.Size = new Size(257, 32);
            label1.TabIndex = 4;
            label1.Text = "Чутливість мікрофону";
            // 
            // trackBarValue
            // 
            trackBarValue.Dock = DockStyle.Top;
            trackBarValue.Location = new Point(22, 157);
            trackBarValue.Margin = new Padding(22, 16, 22, 0);
            trackBarValue.Maximum = 30;
            trackBarValue.Name = "trackBarValue";
            trackBarValue.Size = new Size(977, 90);
            trackBarValue.TabIndex = 1;
            trackBarValue.Scroll += trackBarValue_Scroll;
            trackBarValue.ValueChanged += trackBarValue_ValueChanged;
            // 
            // labelTrackBar
            // 
            labelTrackBar.AutoSize = true;
            labelTrackBar.Location = new Point(22, 247);
            labelTrackBar.Margin = new Padding(22, 0, 22, 16);
            labelTrackBar.Name = "labelTrackBar";
            labelTrackBar.Size = new Size(145, 32);
            labelTrackBar.TabIndex = 2;
            labelTrackBar.Text = "Значення: 0";
            // 
            // SettingsForm
            // 
            ClientSize = new Size(1021, 468);
            Controls.Add(mainLayoutPanel);
            Margin = new Padding(4, 5, 4, 5);
            MinimumSize = new Size(494, 357);
            Name = "SettingsForm";
            Text = "Налаштування";
            Load += SettingsForm_Load;
            mainLayoutPanel.ResumeLayout(false);
            mainLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarValue).EndInit();
            ResumeLayout(false);
        }

        private Form1 form1 ;
        private void trackBarValue_Scroll(object sender, EventArgs e)
        {
            //form1.threshold = (float)this.trackBar.Value;
        }
    }
}