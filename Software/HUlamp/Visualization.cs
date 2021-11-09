using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Threading;
using System.Timers;
using System.Globalization;
using Microsoft.Win32;
using NAudio;
using NAudio.Wave;
using NAudio.CoreAudioApi;
using FFTW.NET;
using System.Numerics;

namespace HUlamp
{
    public partial class MainForm : Form
    {
        private System.Timers.Timer timerLEDUpdate;
        private System.Timers.Timer timer;
        private System.Timers.Timer timerVolumeUpdate;

        private int SampleRate = 44100; // sample rate of the sound card
        private MMDeviceEnumerator enumerator;
        private int errorCount = 0;
        private float maxVolume = 0;
        private int belowMaxVolumeCounter = 0;
        private float belowMaxVolumeValue = 0;
        private float systemVolume = 1;

        private LoopbackRecorder Recorder;
        private WaveFormat AudioFormat;

        private void TimerVolumeUpdate_Tick(object sender, ElapsedEventArgs e)
        {
            double aVolMul = 1;
            Data.LastValue[3] = Data.Value[3];
            double value = Data.ColourDefault[3];
            MMDevice adev = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
            float volume = adev.AudioMeterInformation.MasterPeakValue;

            if (Data.AutoVolumeEnable)
            {
                float mv = adev.AudioEndpointVolume.MasterVolumeLevel;
                if (mv != systemVolume)
                {
                    systemVolume = adev.AudioEndpointVolume.MasterVolumeLevel;
                    maxVolume = 0;
                    belowMaxVolumeCounter = Data.AutoVolumeCounterMax;
                }
                if ((volume > 0.000001) && (belowMaxVolumeCounter > 0)) belowMaxVolumeCounter--;
                else if (volume < 0.000001 && belowMaxVolumeCounter > Data.SilenceResetCounter) belowMaxVolumeCounter = Data.SilenceResetCounter;
                if (volume >= belowMaxVolumeValue) belowMaxVolumeCounter = Data.AutoVolumeCounterMax;

                if (belowMaxVolumeCounter == 0)
                {
                    maxVolume = belowMaxVolumeValue;
                    belowMaxVolumeValue = maxVolume * (float)(1d - Data.AutoVolumeMulHysteresis);
                    belowMaxVolumeCounter = -1000;
                }
                else if (belowMaxVolumeCounter < -1)
                {
                    maxVolume *= 0.99f;
                    belowMaxVolumeValue = maxVolume;
                    belowMaxVolumeCounter++;
                }
                else if (belowMaxVolumeCounter == -1)
                {
                    belowMaxVolumeCounter = Data.AutoVolumeCounterMax;
                }

                if (maxVolume < volume)
                {
                    maxVolume = volume;
                    belowMaxVolumeValue = volume * (float)(1d - Data.AutoVolumeMulHysteresis);
                    belowMaxVolumeCounter = Data.AutoVolumeCounterMax;
                }
                if (maxVolume != 0)
                {
                    aVolMul = Data.AutoVolumeTarget / maxVolume * Math.Pow(volume / maxVolume, Data.VolumePowerValue);
                    Data.Value[Data.ColourLink[3]] = (value + volume * Data.Coefficient[3]) * aVolMul;
                }
            }
            else Data.Value[Data.ColourLink[3]] = value + volume * Data.Coefficient[3];

            if (Data.Value[3] > 10) Data.Value[3] = 10;

            double div = ((double)(timer.Interval) / (double)(timerVolumeUpdate.Interval) * (double)Data.IntervalMultiplier);
            double st1 = (Data.Value[3] - Data.CurrentValue[3]) / div;
            double st2 = (Data.Value[3] - Data.LastValue[3]) / div;
            //Data.Step[3] = (Math.Abs(st1) > Math.Abs(st2)) ? st1 : st2;
            Data.Step[3] = st1;
            Data.StepValueLimit[3] = Data.Value[3] + Data.Step[3] * div;
            double valRise = Data.RiseMaxStep[3] * Data.AutoVolumeTarget * Data.Coefficient[3];
            if (Data.Step[3] > valRise) Data.Step[3] = valRise;
            else
            {
                double valFall = -Data.FallMaxStep[3] * Data.AutoVolumeTarget * Data.Coefficient[3];
                if (Data.Step[3] < valFall) Data.Step[3] = valFall;
            }
            //if (Data.Step[3] > Data.RiseMaxStep[3]) Data.Step[3] = Data.RiseMaxStep[3];
            //else if (Data.Step[3] < -Data.FallMaxStep[3]) Data.Step[3] = -Data.FallMaxStep[3];

        }

        private void FFTTimerCallback()
        {
            Array.Copy(Data.Value.Value, Data.LastValue, Data.LastValue.Length - 1);
            Array.Copy(Data.ColourDefault, Data.Value.Value, Data.ColourDefault.Length - 1);
            if (!PlotLatestData1())
            {
                Array.Copy(Data.LastValue, Data.Value.Value, 3);
            }
            int posMax = 0;
            for (int i = 0; i < 3; i++)
            {
                if (Data.Value[posMax] < Data.Value[i]) posMax = i;
            }
            for (int i = 0; i < 3; i++)
            {
                Data.Value.Value[i] *= (i == posMax) ? Data.LeaderMul[i] : Data.FollowerMul[i];
            }

            for (int i = 0; i < 3; i++) if (Data.Value[i] < 0) Data.Value[i] = 0;

            if (statisticCheckBox.Checked)
            {
                statLab1.Invoke(new Action(() => statLab1.Text = Data.Value[0].ToString("F1")));
                statLab2.Invoke(new Action(() => statLab2.Text = Data.Value[1].ToString("F1")));
                statLab3.Invoke(new Action(() => statLab3.Text = Data.Value[2].ToString("F1")));
                statLab4.Invoke(new Action(() => statLab4.Text = Data.Value[3].ToString("F2")));
                statLab5.Invoke(new Action(() => statLab5.Text = Data.Value.Max[0].ToString("F1")));
                statLab6.Invoke(new Action(() => statLab6.Text = Data.Value.Max[1].ToString("F1")));
                statLab7.Invoke(new Action(() => statLab7.Text = Data.Value.Max[2].ToString("F1")));
                statLab8.Invoke(new Action(() => statLab8.Text = Data.Value.Max[3].ToString("F2")));
                statLab9.Invoke(new Action(() => statLab9.Text = Data.CurrentValue[0].ToString("F1")));
                statLab10.Invoke(new Action(() => statLab10.Text = Data.CurrentValue[1].ToString("F1")));
                statLab11.Invoke(new Action(() => statLab11.Text = Data.CurrentValue[2].ToString("F1")));
                statLab2.Invoke(new Action(() => statLab12.Text = Data.CurrentValue[3].ToString("F2")));
            }

            double sum = Data.Value[0] + Data.Value[1] + Data.Value[2];
            double mul = sum / Data.Value[posMax];
            for (int i = 0; i < 3; i++)
            {
                Data.Value[i] = Data.Value[i] / sum * 200 * mul;
            }

            double st1 = 0, st2 = 0;
            for (int i = 0; i < 3; i++)
            {
                double div = ((double)(timer.Interval) / (double)(timerLEDUpdate.Interval) * (double)Data.IntervalMultiplier);
                st1 = (Data.Value[i] - Data.CurrentValue[i]) / div;
                st2 = (Data.Value[i] - Data.LastValue[i]) / div;
                //Data.Step[i] = (Math.Abs(st1) > Math.Abs(st2)) ? st1 : st2;
                Data.Step[i] = st1;
                Data.StepValueLimit[i] = Data.Value[i] + Data.Step[i] * div;
                if (Data.Step[i] > Data.RiseMaxStep[i]) Data.Step[i] = Data.RiseMaxStep[i];
                else if (Data.Step[i] < -Data.FallMaxStep[i]) Data.Step[i] = -Data.FallMaxStep[i];
            }
        }

        private void Timer_Tick(object sender, ElapsedEventArgs e)
        {
            FFTTimerCallback();
        }


        public double[] FFT(double[] data)
        {
            System.Numerics.Complex[] fftComplex = new System.Numerics.Complex[data.Length];
            System.Numerics.Complex[] output = new Complex[data.Length];
            for (int i = 0; i < data.Length; i++) fftComplex[i] = data[i];
            var pinIn = new PinnedArray<Complex>(fftComplex);
            var pinOut = new PinnedArray<Complex>(output);
            DFT.FFT(pinIn, pinOut);
            for (int i = 0; i < data.Length; i++)
                data[i] = pinOut[i].Magnitude;
            pinIn.Dispose();
            pinOut.Dispose();
            return data;
        }

        private bool PlotLatestData1()
        {
            double[] fvmaxl = new double[3];
            double[] fvmaxm = new double[6];
            double[] fvmaxh = new double[6];

            byte[] audioBytes = Recorder.StreamData;

            int graphPointCount = audioBytes.Length / 2;

            double[] fft = new double[graphPointCount];

            for (int i = 0; i < graphPointCount; i++)
            {
                Int16 val = BitConverter.ToInt16(audioBytes, i * 2);
                fft[i] = (double)(val) / 65536.0;
            }
            fft = FFT(fft);

            Array.Clear(fvmaxl, 0, fvmaxl.Length);
            Array.Clear(fvmaxm, 0, fvmaxm.Length);
            Array.Clear(fvmaxh, 0, fvmaxh.Length);

            int mil = (int)(Data.Frequency[0] / ((float)SampleRate / (float)((int)graphPointCount / (int)4)));
            for (int i = 0; i <= mil; i++)
            {
                for (int j = 0; j < Math.Min(mil, fvmaxl.Length); j++)
                    if (fvmaxl[j] < fft[i])
                    {
                        Array.Sort(fvmaxl, (x, y) => -x.CompareTo(y));
                        Array.Copy(fvmaxl, j, fvmaxl, j + 1, Math.Min(mil, fvmaxl.Length) - j - 1);
                        fvmaxl[j] = fft[i];
                        break;
                    }
            }
            double v = 0;
            for (int i = 0; i < Math.Min(mil, fvmaxl.Length); i++) v += fvmaxl[i];
            Data.Value.Value[Data.ColourLink[0]] += Data.Coefficient[0] * (v) / Math.Min(mil, fvmaxl.Length) / 20.0;

            int mim = (int)(Data.Frequency[1] / ((float)SampleRate / (float)(graphPointCount / 4)));
            for (int i = mil + 1; i <= mim; i++)
            {
                for (int j = 0; j < Math.Min(mim - mil, fvmaxm.Length); j++)
                    if (fvmaxm[j] < fft[i])
                    {
                        Array.Sort(fvmaxm, (x, y) => -x.CompareTo(y));
                        Array.Copy(fvmaxm, j, fvmaxm, j + 1, Math.Min(mim - mil, fvmaxm.Length) - j - 1);
                        fvmaxm[j] = fft[i];
                        break;
                    }
            }
            v = 0;
            for (int i = 0; i < Math.Min(mim, fvmaxm.Length); i++) v += fvmaxm[i];
            Data.Value.Value[Data.ColourLink[1]] += Data.Coefficient[1] * (v) / Math.Min(mim, fvmaxm.Length) / 20.0;

            int mih = (int)(Data.Frequency[2] / ((float)SampleRate / (float)(graphPointCount / 4)));
            for (int i = mim + 1; i <= mih; i++)
            {
                for (int j = 0; j < Math.Min(mih - mim, fvmaxh.Length); j++)
                    if (fvmaxh[j] < fft[i])
                    {
                        Array.Sort(fvmaxh, (x, y) => -x.CompareTo(y));
                        Array.Copy(fvmaxh, j, fvmaxh, j + 1, Math.Min(mih - mim, fvmaxh.Length) - j - 1);
                        fvmaxh[j] = fft[i];
                        break;
                    }
            }
            v = 0;
            for (int i = 0; i < Math.Min(mih, fvmaxh.Length); i++) v += fvmaxh[i];
            Data.Value.Value[Data.ColourLink[2]] += Data.Coefficient[2] * (v) / Math.Min(mih, fvmaxh.Length) / 20.0;
            return true;
        }

        private void TimerLEDUpdate_Tick(object sender, ElapsedEventArgs e)
        {
            float[] rgb = new float[3];
            int k = Thread.CurrentThread.ManagedThreadId;
            double vol = Data.CurrentValue[3] + Data.Step[3];

            if (vol < 0) vol = 0;
            if (!Data.AutoVolumeEnable) vol *= Data.BrightnessMul;
            if ((Data.Step[3] >= 0 && vol > Data.StepValueLimit[3]) || (Data.Step[3] < 0 && vol < Data.StepValueLimit[3])) vol = Data.StepValueLimit[3];
            if (vol > 10) vol = 10;
            Data.CurrentValue[3] = vol;

            double v;
            for (int i = 0; i < 3; i++)
            {
                v = Data.CurrentValue[i];
                v += Data.Step[i];
                if (v < 0) v = 0;
                if ((Data.Step[i] >= 0 && v > Data.StepValueLimit[i]) || (Data.Step[i] < 0 && v < Data.StepValueLimit[i])) v = Data.StepValueLimit[i];
                if (v > 200) v = 200;
                if (v < 0) v = 0;
                Data.CurrentValue[i] = v;
                //v *= Data.RGBMul;
                rgb[i] = (float)v;
            }
            if (serialPort.IsOpen)
            {
                byte[] array = SendCommandList.SetColourCommand(rgb[0] * (float)vol, rgb[1] * (float)vol, rgb[2] * (float)vol);
                try
                {
                    serialPort.Write(array, 0, array.Length);
                }
                catch { errorCount++; };
            }

        }
    }
}
