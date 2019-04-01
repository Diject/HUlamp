using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio;
using NAudio.Wave;
using NAudio.CoreAudioApi;

namespace HUlamp
{
    public class LoopbackRecorder
    {
        private IWaveIn _waveIn;
        private WaveFileWriter _writer;
        private bool _isRecording = false;
        private readonly int _bufferSize; // must be a multiple of 2
        private int _bufferLength = 0;
        private BufferedWaveProvider _bwp;
        private bool AlternativeBuffer;
        public byte[] Buffer;

        public LoopbackRecorder(int bufferSize, bool arrurateBuffer = false)
        {
            _bufferSize = bufferSize;
            AlternativeBuffer = arrurateBuffer;
            if (AlternativeBuffer) Buffer = new byte[bufferSize * 2];
        }

        public void StartRecording()
        {
            // If we are currently record then go ahead and exit out.
            if (_isRecording == true)
            {
                return;
            }
            _waveIn = new WasapiLoopbackCapture();
            if (!AlternativeBuffer) _bwp = new BufferedWaveProvider(_waveIn.WaveFormat)
                {
                    BufferLength = _bufferSize,
                    DiscardOnBufferOverflow = true
                };
            _waveIn.DataAvailable += OnDataAvailable;
            _waveIn.RecordingStopped += OnRecordingStopped;
            _waveIn.StartRecording();
            _isRecording = true;
        }


        public void StopRecording()
        {
            if (_waveIn == null)
            {
                return;
            }
            _waveIn.StopRecording();
        }

        void OnRecordingStopped(object sender, StoppedEventArgs e)
        {
            // Writer Close() needs to come first otherwise NAudio will lock up.
            if (_writer != null)
            {
                _writer.Close();
                _writer = null;
            }
            if (_waveIn != null)
            {
                _waveIn.Dispose();
                _waveIn = null;
            }
            _isRecording = false;
            if (e.Exception != null)
            {
                throw e.Exception;
            }
        } // end void OnRecordingStopped

        void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            if (!AlternativeBuffer)
            {
                _bwp.AddSamples(e.Buffer, 0, e.BytesRecorded);
            }
            else
            {
                if (e.BytesRecorded >= _bufferSize)
                {
                    Array.Copy(e.Buffer, e.BytesRecorded - _bufferSize, Buffer, 0, _bufferSize);
                    _bufferLength = _bufferSize;
                }
                else
                {
                    if (e.BytesRecorded + _bufferLength > _bufferSize)
                    {
                        Array.Copy(Buffer, e.BytesRecorded, Buffer, 0, _bufferSize - e.BytesRecorded);
                        Array.Copy(e.Buffer, 0, Buffer, _bufferSize - e.BytesRecorded, e.BytesRecorded);
                        _bufferLength = _bufferSize;
                    }
                    else
                    {
                        Array.Copy(e.Buffer, 0, Buffer, _bufferLength, e.BytesRecorded);
                        _bufferLength = _bufferLength + e.BytesRecorded;
                    }
                }
            }
            OnStreamDataReady?.Invoke(this);
        }

        public byte[] StreamData
        {
            get
            {
                byte[] res = new byte[_bufferSize];
                if (!AlternativeBuffer)
                {
                    _bwp.Read(res, 0, _bufferSize);
                }
                else
                {
                    Array.Copy(Buffer, res, _bufferSize);
                }
                return res;
            }
        }

        public bool IsRecording
        {
            get
            {
                return _isRecording;
            }
        }

        public void Dispose()
        {
            if (_writer != null)
            {
                _writer.Close();
                _writer.Dispose();
                _writer = null;
            }
            if (_waveIn != null)
            {
                _waveIn.Dispose();
                _waveIn = null;
            }   
        }

        public delegate void DataReadyHandler(object sender);
        public event DataReadyHandler OnStreamDataReady;
    }
}
