
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
        private WaveInEvent waveSource = new WaveInEvent();
        private WaveFileWriter writer;
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

        }



        private void налаштуванняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var settings = new SettingsForm();
            settings.ShowDialog();
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
                this.lblStatus.Text = $"Шлях за замовчуванням обрано: {this.SelectedPath}";
            }
        }
        
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (this.SelectedPath == "")
            {
                this.timer.Interval = 5000;
                //this.lblStatus.Text = "Оберіть шлях до теки";
            }
            if (DateTime.Now.ToString("HH:mm") == "08:01" && this.recordAudit)
            {
                this.labelStatus.Text = "Запис триває";
                this.button1.Text = "Зупинити запис";
                this.waveIn = new WaveIn();
                this.waveIn.WaveFormat = new WaveFormat(44100, 1);
                this.fileName = DateTime.Now.ToString("dd.MM.yyyy") + ".mp3";
                this.outputFilePath = Path.Combine(this.SelectedPath, this.fileName);
                this.writer = new WaveFileWriter(this.outputFilePath, this.waveIn.WaveFormat);
                this.waveIn.DataAvailable += new EventHandler<WaveInEventArgs>(this.OnDataAvailable);
                this.waveIn.StartRecording();
                this.recordAudit = false;
            }
            if (!(DateTime.Now.ToString("HH:mm") == "08:00") || this.recordAudit)
                return;
            this.labelStatus.Text = "Запис зупинено";
            this.button1.Text = "Розпочати запис";
            this.waveIn.StopRecording();
            this.writer?.Dispose();
            this.writer = (WaveFileWriter)null;
            this.waveIn = (WaveIn)null;
            this.recordAudit = true;
        }
        private void зберегтиЯкToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
                return;
            this.fileName = DateTime.Now.ToString("dd.MM.yyyy") + ".mp3";
            this.SelectedPath = folderBrowserDialog.SelectedPath;
            this.outputFilePath = Path.Combine(this.SelectedPath, this.fileName);
            this.lblStatus.Text = "Ви вибрали шлях : " + this.outputFilePath.ToString();

        }
        private void StartRecording()
        {
            try
            {
                this.labelStatus.Text = "Запис триває";
                this.button1.Text = "Зупинити запис";
                this.waveIn = new WaveIn();
                this.waveIn.WaveFormat = new WaveFormat(44100, 1);
                this.fileName = DateTime.Now.ToString("dd.MM.yyyy") + ".mp3";
                this.outputFilePath = Path.Combine(this.SelectedPath, this.fileName);
                this.writer = new WaveFileWriter(this.outputFilePath, this.waveIn.WaveFormat);
                this.waveIn.DataAvailable += new EventHandler<WaveInEventArgs>(this.OnDataAvailable);
                this.waveIn.StartRecording();
                this.isRecording = true;
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
            this.labelStatus.Text = "Запис зупинено";
            this.button1.Text = "Розпочати запис";
            if (this.isRecording)
            {
                this.waveIn.StopRecording();
                this.isRecording = false;
                this.writer?.Dispose();
                this.writer = null;
                this.waveIn?.Dispose();
                this.waveIn = null;
            }
        }
        //private bool autoStopExecuted = false;
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.isRecording)
            {
                StopRecording();
                //this.isRecording = false;
                //this.autoStopExecuted = true; // Встановлюємо флаг, щоб запобігти автоматичній зупинці одразу після ручної
            }
            else
            {
                StartRecording();
                //this.isRecording = true ;
                //this.autoStopExecuted = false; // Скидаємо флаг при ручному початку
            }
        }
           
        
        private void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            byte[] buffer = e.Buffer;
            int bytesRecorded = e.BytesRecorded;
            int num = 0;
            float maxSample = 0.0f;
            for (int index = 0; index < e.BytesRecorded; index += 2)
            {
                maxSample = (float)(short)((int)buffer[index + 1] << 8 | (int)buffer[index]) / 327f;
                if ((double)maxSample < 0.0)
                    maxSample = -maxSample;
                if ((double)maxSample > (double)num)
                    num = (int)maxSample;
            }
            if ((double)num <= (double)this.threshold)
                return;
            this.waveformPainter1.AddMax(maxSample);
            this.writer.Write(buffer, 0, bytesRecorded);
        }
    }
}
