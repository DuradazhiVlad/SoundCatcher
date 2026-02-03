namespace WinFormsApp3
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        //private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            //if (disposing && (components != null))
            //{
            //    components.Dispose();
            //}
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            файлToolStripMenuItem = new ToolStripMenuItem();
            зберегтиЯкToolStripMenuItem = new ToolStripMenuItem();
            налаштуванняToolStripMenuItem = new ToolStripMenuItem();
            оновленняToolStripMenuItem = new ToolStripMenuItem();
            button1 = new Button();
            labelStatus = new Label();
            lblStatus = new Label();
            waveformPainter1 = new NAudio.Gui.WaveformPainter();
            label1 = new Label();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(32, 32);
            menuStrip1.Items.AddRange(new ToolStripItem[] { файлToolStripMenuItem, налаштуванняToolStripMenuItem, оновленняToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1032, 40);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            файлToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { зберегтиЯкToolStripMenuItem });
            файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            файлToolStripMenuItem.Size = new Size(90, 36);
            файлToolStripMenuItem.Text = "Файл";
            // 
            // зберегтиЯкToolStripMenuItem
            // 
            зберегтиЯкToolStripMenuItem.Name = "зберегтиЯкToolStripMenuItem";
            зберегтиЯкToolStripMenuItem.Size = new Size(293, 44);
            зберегтиЯкToolStripMenuItem.Text = "Зберегти як...";
            зберегтиЯкToolStripMenuItem.Click += зберегтиЯкToolStripMenuItem_Click;
            // 
            // налаштуванняToolStripMenuItem
            // 
            налаштуванняToolStripMenuItem.Name = "налаштуванняToolStripMenuItem";
            налаштуванняToolStripMenuItem.Size = new Size(194, 36);
            налаштуванняToolStripMenuItem.Text = "Налаштування";
            налаштуванняToolStripMenuItem.Click += налаштуванняToolStripMenuItem_Click;
            // 
            // оновленняToolStripMenuItem
            // 
            оновленняToolStripMenuItem.Name = "оновленняToolStripMenuItem";
            оновленняToolStripMenuItem.Size = new Size(194, 36);
            оновленняToolStripMenuItem.Text = "Оновлення";
            оновленняToolStripMenuItem.Click += оновленняToolStripMenuItem_Click;
            // 
            // button1
            //
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button1.Location = new Point(758, 464);
            button1.Name = "button1";
            button1.Size = new Size(262, 46);
            button1.TabIndex = 1;
            button1.Text = "Розпочати запис";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            //
            // labelStatus
            //
            labelStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            labelStatus.AutoSize = true;
            labelStatus.Location = new Point(12, 471);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(199, 32);
            labelStatus.TabIndex = 2;
            labelStatus.Text = "Розпочати запис";
            //
            // lblStatus
            //
            lblStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(12, 423);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(78, 32);
            lblStatus.TabIndex = 3;
            lblStatus.Text = "label1";
            //
            // waveformPainter1
            //
            waveformPainter1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            waveformPainter1.BackColor = SystemColors.ActiveCaption;
            waveformPainter1.ForeColor = SystemColors.AppWorkspace;
            waveformPainter1.ImeMode = ImeMode.Off;
            waveformPainter1.Location = new Point(141, 120);
            waveformPainter1.Name = "waveformPainter1";
            waveformPainter1.RightToLeft = RightToLeft.No;
            waveformPainter1.Size = new Size(730, 279);
            waveformPainter1.TabIndex = 4;
            waveformPainter1.Text = "waveformPainter1";
            //
            // label1
            //
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(12, 69);
            label1.Name = "label1";
            label1.Size = new Size(801, 32);
            label1.TabIndex = 5;
            label1.Text = "Це програма звукозапису. Цю програму створив Дурадажи Владислав";
            //
            // Form1
            //
            //AutoScaleDimensions = new SizeF(13F, 32F);
            //AutoScaleMode = AutoScaleMode.Font;
            //AutoSize = true;
            //AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(1032, 522); // Цей рядок може бути видалено, якщо AutoSize = true
            Controls.Add(label1);
            Controls.Add(waveformPainter1);
            Controls.Add(lblStatus);
            Controls.Add(labelStatus);
            Controls.Add(button1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Звукозапис-Влад Дурадажи";
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem файлToolStripMenuItem;
        private ToolStripMenuItem налаштуванняToolStripMenuItem;
        private ToolStripMenuItem оновленняToolStripMenuItem;
        private ToolStripMenuItem зберегтиЯкToolStripMenuItem;
        private Button button1;
        private Label labelStatus;
        private Label lblStatus;
        private NAudio.Gui.WaveformPainter waveformPainter1;
    }
}
