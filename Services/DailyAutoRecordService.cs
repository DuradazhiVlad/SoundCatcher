using System;
using System.IO;

namespace WinFormsApp3.Services
{
    public class DailyAutoRecordService
    {
        private readonly AudioRecorder _recorder;
        private string _selectedPath = string.Empty;
        private bool _recordAudit = false; // mirrors original logic

        public DailyAutoRecordService(AudioRecorder recorder)
        {
            _recorder = recorder;
        }

        public void SetSelectedPath(string path)
        {
            _selectedPath = path ?? string.Empty;
        }

        public string SelectedPath => _selectedPath;

        public event Action<string>? StatusInfo; // optional status updates

        public void Tick(DateTime now)
        {
            if (string.IsNullOrWhiteSpace(_selectedPath))
            {
                StatusInfo?.Invoke("Оберіть шлях до теки");
                return;
            }

            var time = now.ToString("HH:mm");
            if (time == "08:01" && _recordAudit)
            {
                var fileName = now.ToString("dd.MM.yyyy") + ".mp3";
                var outputFilePath = Path.Combine(_selectedPath, fileName);
                _recorder.Start(outputFilePath);
                _recordAudit = false;
                StatusInfo?.Invoke("Запис триває");
            }
            else if (time == "08:00" && !_recordAudit)
            {
                _recorder.Stop();
                _recordAudit = true;
                StatusInfo?.Invoke("Запис зупинено");
            }
        }

        public void ManualStart()
        {
            var now = DateTime.Now;
            var fileName = now.ToString("dd.MM.yyyy") + ".mp3";
            var outputFilePath = Path.Combine(_selectedPath, fileName);
            _recorder.Start(outputFilePath);
        }

        public void ManualStop()
        {
            _recorder.Stop();
        }

        public void SetThreshold(float threshold)
        {
            _recorder.Threshold = threshold;
        }
    }
}