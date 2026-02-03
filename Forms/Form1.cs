
using Microsoft.VisualBasic;
using NAudio.Gui;
using NAudio.Wave;
using System;
using System.ComponentModel;
using System.IO.Pipelines;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;
using Timer = System.Windows.Forms.Timer;
using TrackBar = System.Windows.Forms.TrackBar;
using WinFormsApp3.Services;

using WinFormsApp3.Forms;

namespace WinFormsApp3
{
    public partial class Form1 : Form
    {
        private string fileName;
        private string outputFilePath;
        private WaveIn waveIn;
        private string SelectedPath = "";
        private bool recordAudit = false;
        private float threshold = 0.0f;
        private bool isRecording = false;
        // Remove raw NAudio fields and use services instead
        private AudioRecorder _recorder = new AudioRecorder();
        private DailyAutoRecordService _autoService;
        private Timer timer;
        private IContainer components = (IContainer)null;
        private Panel panel1;
        private Panel panel2;
        private Label label1;
        private ToolStripMenuItem файлиToolStripMenuItem;
        private ToolStripMenuItem зберегтиФайлToolStripMenuItem;
        private BackgroundWorker backgroundWorker1;
        private Panel panel3;
        private Label label2;
        private Label label3;
        public Form1()
        {
            InitializeComponent();
            string selectedTheme = Properties.Settings.Default.ThemeName;
            ThemeManager.ApplyTheme(this, selectedTheme);
            this.timer = new Timer();
            this.timer.Interval = 1000;
            this.timer.Tick += new EventHandler(this.Timer_Tick);
            this.timer.Start();
            waveformPainter1.TabIndex = 1;
            this.threshold = Properties.Settings.Default.SomeValue;
            _recorder.Threshold = this.threshold;
            _recorder.MaxSampleAvailable += (max) => waveformPainter1.AddMax(max);
            _recorder.RecordingStarted += () =>
            {
                this.labelStatus.Text = "Запис триває";
                this.button1.Text = "Зупинити запис";
                this.isRecording = true;
            };
            _recorder.RecordingStopped += () =>
            {
                this.labelStatus.Text = "Запис зупинено";
                this.button1.Text = "Розпочати запис";
                this.isRecording = false;
            };
            _autoService = new DailyAutoRecordService(_recorder);
        }



        private void налаштуванняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var settings = new SettingsForm();
            settings.ShowDialog();
        }

        private void оновленняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var updateForm = new UpdateForm();
            updateForm.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Формування шляху за замовчуванням: D:\Звукозапис\рік-місяць-день
            string defaultFolderName = DateTime.Now.Year.ToString();
            string defaultPath = Path.Combine("D:\\Звукозапис", defaultFolderName);

            // Перевірка та створення папки, якщо її немає
            if (!Directory.Exists(defaultPath))
            {
                try
                {
                    Directory.CreateDirectory(defaultPath);
                    this.SelectedPath = defaultPath;
                    this.outputFilePath = Path.Combine(this.SelectedPath, DateTime.Now.ToString("dd.MM.yyyy") + ".mp3");
                    _autoService.SetSelectedPath(this.SelectedPath);
                    this.lblStatus.Text = $"Шлях за замовчуванням створено та обрано: {this.SelectedPath}";
                }
                catch (Exception ex)
                {
                    this.lblStatus.Text = $"Помилка при створенні шляху за замовчуванням: {ex.Message}";
                    // Можна також вивести повідомлення користувачеві про помилку
                }
            }
            else
            {
                this.SelectedPath = defaultPath;
                this.outputFilePath = Path.Combine(this.SelectedPath, DateTime.Now.ToString("dd.MM.yyyy") + ".mp3");
                _autoService.SetSelectedPath(this.SelectedPath);
                this.lblStatus.Text = $"Шлях за замовчуванням обрано: {this.SelectedPath}";
            }
        }
        
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (this.SelectedPath == "")
            {
                this.timer.Interval = 5000;
            }
            _autoService.Tick(DateTime.Now);
        }

        private void зберегтиЯкToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
                return;
            this.fileName = DateTime.Now.ToString("dd.MM.yyyy") + ".mp3";
            this.SelectedPath = folderBrowserDialog.SelectedPath;
            _autoService.SetSelectedPath(this.SelectedPath);
            this.outputFilePath = Path.Combine(this.SelectedPath, this.fileName);
            this.lblStatus.Text = "Ви вибрали шлях : " + this.outputFilePath.ToString();
        }
        private void StartRecording()
        {
            try
            {
                _autoService.ManualStart();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка початку запису: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.button1.Text = "Розпочати запис";
                this.isRecording = false;
            }
        }

        private void StopRecording()
        {
            _autoService.ManualStop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.isRecording)
            {
                StopRecording();
            }
            else
            {
                StartRecording();
            }
        }
           
        
        // Remove direct OnDataAvailable; handled by AudioRecorder service
        // private void OnDataAvailable(object sender, WaveInEventArgs e) { }
    }
}
