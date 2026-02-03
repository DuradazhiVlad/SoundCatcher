using System;
using System.IO;
using NAudio.Wave;

namespace WinFormsApp3.Services
{
    public class AudioRecorder : IDisposable
    {
        private WaveIn? _waveIn;
        private WaveFileWriter? _writer;
        private bool _isRecording;

        public float Threshold { get; set; } = 0.0f;
        public bool IsRecording => _isRecording;

        public event Action? RecordingStarted;
        public event Action? RecordingStopped;
        public event Action<float>? MaxSampleAvailable;

        public void Start(string outputFilePath, int sampleRate = 44100, int channels = 1)
        {
            if (_isRecording) return;

            Directory.CreateDirectory(Path.GetDirectoryName(outputFilePath)!);

            _waveIn = new WaveIn();
            _waveIn.WaveFormat = new WaveFormat(sampleRate, channels);
            _writer = new WaveFileWriter(outputFilePath, _waveIn.WaveFormat);
            _waveIn.DataAvailable += OnDataAvailable;
            _waveIn.StartRecording();
            _isRecording = true;
            RecordingStarted?.Invoke();
        }

        public void Stop()
        {
            if (!_isRecording) return;
            try
            {
                _waveIn?.StopRecording();
            }
            finally
            {
                _isRecording = false;
                _writer?.Dispose();
                _writer = null;
                _waveIn?.Dispose();
                _waveIn = null;
                RecordingStopped?.Invoke();
            }
        }

        private void OnDataAvailable(object? sender, WaveInEventArgs e)
        {
            if (_writer == null) return;

            var buffer = e.Buffer;
            var bytesRecorded = e.BytesRecorded;
            int num = 0;
            float maxSample = 0.0f;

            for (int i = 0; i < bytesRecorded; i += 2)
            {
                maxSample = (float)(short)((int)buffer[i + 1] << 8 | (int)buffer[i]) / 327f;
                if (maxSample < 0f) maxSample = -maxSample;
                if (maxSample > num) num = (int)maxSample;
            }

            if (num <= Threshold) return;

            MaxSampleAvailable?.Invoke(maxSample);
            _writer.Write(buffer, 0, bytesRecorded);
        }

        public void Dispose()
        {
            Stop();
        }
    }
}