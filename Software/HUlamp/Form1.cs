using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Threading;
using System.Timers;
using System.Diagnostics;
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

        private RxBuffer Buffer;
        private RxAltDataArgs DeviceSettings;
        private bool ShowDeviceSettings = false;
        private VisualizationData Data;
        private static readonly string subKey = @"Software\HUlamp Terminal";


        private void RxDataActions(object sender, RxMainDataArgs e)
        {
            String str = "[" + DateTime.Now.ToString("HH:mm:ss.ffff")  + "] Current = " + e.CurrentData.ToString("F3") + " Temperature = " +
                e.TemperatureData.ToString("F1") + " " + Convert.ToString(e.ButtonData) + " " + e.VoltageData.ToString("F1") + "\r\n";
            if (receiveTextBox.Lines.Length < 100)
            {
                receiveTextBox.Text = receiveTextBox.Text.Insert(0, str);
                List<string> list = receiveTextBox.Lines.ToList();
                if (list.Count > 0)
                {
                    receiveTextBox.Lines = list.ToArray();
                    receiveTextBox.Refresh();
                }
            }
            else
            {
                receiveTextBox.Text = receiveTextBox.Text.Insert(0, str);
                List<string> list = receiveTextBox.Lines.ToList();
                if (list.Count > 0)
                {
                    list.RemoveAt(list.Count - 1);
                    receiveTextBox.Lines = list.ToArray();
                    receiveTextBox.Refresh();
                }
            }
        }

        private void RxAltDataActions(object sender, RxAltDataArgs e)
        {
            if (ShowDeviceSettings)
            {
                string str = "ETX Mode = " + e.ETXMode.ToString() +
                    "\r\nNo Load Current Limit = " + e.NoLoadCurrent.ToString() +
                    "\r\nLED Enable = " + e.LEDState.ToString() +
                    "\r\nBacklight Mode = " + e.BacklightMode.ToString() +
                    "\r\nBacklight Red = " + e.BacklightRed.ToString() +
                    "\r\nBacklight Green = " + e.BacklightGreen.ToString() +
                    "\r\nBacklight Blue = " + e.BacklightBlue.ToString() +
                    "\r\nBacklight Brightness = " + e.BacklightBrightness.ToString() +
                    "\r\nBrightness Multiplier = " + e.BrightnessMultiplier;
                Thread t = new Thread(() => AltDataMessageBox(str));
                t.Start();
            }
            else
            {
                DeviceSettings = e;
            }
        }

        private void AltDataMessageBox(string text)
        {
            MessageBox.Show(text, "Данные", MessageBoxButtons.OK);
        }

        private void ComportDataUpdate(object s, EventArgs e)
        {
            int dataLen = serialPort.BytesToRead;
            byte[] data = new byte[dataLen];
            if (serialPort.Read(data, 0, dataLen) == 0) return;
            Buffer?.Add(data);
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void ComportReloadButton_Click(object sender, EventArgs e)
        {
            comportBox.Items.Clear();
            foreach (string portName in SerialPort.GetPortNames())
            {
                comportBox.Items.Add(portName);
            }
            comportBox.SelectedIndex = 0;
        }

        private void ComportConnectB_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
            else
            {
                try
                {
                    serialPort.PortName = (string)comportBox.SelectedItem;
                    serialPort.Open();
                    //Buffer = new RxBuffer(2048, RxDataActions, RxAltDataActions);
                    RegistryKey key = Registry.CurrentUser.CreateSubKey(subKey);
                    key?.SetValue("SerialPort", (string)comportBox.SelectedItem);
                    key?.Close();
                }
                catch (Exception ex) { MessageBox.Show("Не удалось подключиться.", "Error"); }
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            this.BeginInvoke(new EventHandler(ComportDataUpdate));
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort.IsOpen)
            {
                try
                {
                    if (DeviceSettings != null)
                    {
                        byte[] array = SendCommandList.SetBrightnessCommand(DeviceSettings.BacklightBrightness);
                        serialPort.Write(array, 0, (int)array.Length);
                        array = SendCommandList.SetColourCommand(DeviceSettings.BacklightRed, DeviceSettings.BacklightGreen, DeviceSettings.BacklightBlue);
                        serialPort.Write(array, 0, (int)array.Length);
                        array = SendCommandList.ChangeBacklightModeCommand(DeviceSettings.BacklightMode);
                        serialPort.Write(array, 0, (int)array.Length);
                    }
                }
                catch { errorCount++; }
            }
            timer?.Dispose();
            timerLEDUpdate?.Dispose();
            timerVolumeUpdate?.Dispose();
            Recorder?.StopRecording();
            if (serialPort.IsOpen) { serialPort.Close(); }
            Thread.Sleep(10);
            Application.Exit();
        }

        private void ComportDisconnectB_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen) serialPort.Close();
        }

        private void ReceiveTextBox_TextChanged(object sender, EventArgs e)
        {
            
            
        }

        private void CommanSentB_Click(object sender, EventArgs e)
        {
            switch (commandList.SelectedIndex)
            {
                case 0:
                    if (serialPort.IsOpen)
                    {
                        byte[] array = SendCommandList.SetColourCommand(textboxParam1.Text, textboxParam2.Text, textboxParam3.Text);
                        serialPort.Write(array, 0, (int)array.Length);
                    }

                    break;

                case 1:
                    if (serialPort.IsOpen)
                    {
                        byte[] array = SendCommandList.SetBrightnessCommand(textboxParam1.Text);
                        serialPort.Write(array, 0, (int)array.Length);
                    }

                    break;

                case 2:
                    if (serialPort.IsOpen)
                    {
                        byte[] array = SendCommandList.ChangeBacklightModeCommand(textboxParam1.Text);
                        serialPort.Write(array, 0, (int)array.Length);
                    }

                    break;

                case 3:
                    if (serialPort.IsOpen)
                    {
                        byte[] array = SendCommandList.CustomModeSetupCommand(textboxParam1.Text);
                        serialPort.Write(array, 0, (int)array.Length);
                    }

                    break;

                case 4:
                    if (serialPort.IsOpen)
                    {
                        byte[] array = SendCommandList.CustomModeDataCommand(textboxParam1.Text, textboxParam2.Text,
                            textboxParam3.Text, textboxParam4.Text, textboxParam5.Text, textboxParam6.Text,
                            textboxParam7.Text, textboxParam8.Text, textboxParam9.Text);
                        serialPort.Write(array, 0, (int)array.Length);
                    }

                    break;

                case 5:
                    if (serialPort.IsOpen)
                    {
                        byte[] array = SendCommandList.CustomModeCopyCommand(textboxParam1.Text, textboxParam2.Text);
                        serialPort.Write(array, 0, (int)array.Length);
                    }

                    break;

                case 6:
                    if (serialPort.IsOpen)
                    {
                        byte[] array = SendCommandList.SaveCommand(textboxParam1.Text);
                        serialPort.Write(array, 0, (int)array.Length);

                    }

                    break;

                case 7:
                    using (MemoryStream stream = new MemoryStream())
                    {
                        using (BinaryWriter binWriter = new BinaryWriter(stream))
                        {
                            binWriter.Write((byte)0x2D);
                            binWriter.Write((byte)0x2D);
                            binWriter.Write((byte)0x62);
                            binWriter.Write((byte)0x6D);
                            binWriter.Write(Convert.ToSingle(textboxParam1.Text));
                            serialPort.Write(stream.ToArray(), 0, (int)stream.Length);
                        }
                    }

                    break;

                case 8:
                    if (serialPort.IsOpen)
                    {
                        byte[] array = SendCommandList.ResetCommand();
                        serialPort.Write(array, 0, (int)array.Length);

                    }

                    break;

                case 9:
                    if (serialPort.IsOpen)
                    {
                        byte[] array = SendCommandList.CalibrationRunCommand(textboxParam1.Text);
                        serialPort.Write(array, 0, (int)array.Length);

                    }

                    break;

                case 10:
                    if (serialPort.IsOpen)
                    {
                        byte[] array = SendCommandList.SetTXControlCommand(textboxParam1.Text);
                        serialPort.Write(array, 0, (int)array.Length);

                    }

                    break;

                case 11:
                    if (serialPort.IsOpen)
                    {
                        ShowDeviceSettings = true;
                        byte[] array = SendCommandList.RequestAltData();
                        serialPort.Write(array, 0, (int)array.Length);

                    }

                    break;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (timer == null)
            {
                timer = new System.Timers.Timer();
                timer.Interval = Data.DataProcessingTimerInterval;
                timer.AutoReset = true;
                timer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Tick);
            }
            if (timerLEDUpdate == null)
            {
                timerLEDUpdate = new System.Timers.Timer();
                timerLEDUpdate.Interval = Data.DataSendingTimerInterval;
                timerLEDUpdate.AutoReset = true;
                timerLEDUpdate.Elapsed += new System.Timers.ElapsedEventHandler(TimerLEDUpdate_Tick);
            }
            if (timerVolumeUpdate == null)
            {
                timerVolumeUpdate = new System.Timers.Timer();
                timerVolumeUpdate.Interval = Data.DataVolumeProcessingTimerInterval;
                timerVolumeUpdate.AutoReset = true;
                timerVolumeUpdate.Elapsed += new System.Timers.ElapsedEventHandler(TimerVolumeUpdate_Tick);
            }
            if (!timer.Enabled)
            {
                enumerator = new MMDeviceEnumerator();
                if (serialPort.IsOpen)
                {
                    try
                    {
                        ShowDeviceSettings = false;
                        byte[] array = SendCommandList.RequestAltData();
                        serialPort.Write(array, 0, (int)array.Length);
                        Thread.Sleep(200);
                        array = SendCommandList.ChangeBacklightModeCommand(0);
                        serialPort.Write(array, 0, (int)array.Length);
                        array = SendCommandList.SetBrightnessCommand(1);
                        serialPort.Write(array, 0, (int)array.Length);
                    }
                    catch { errorCount++; }
                }
                AudioFormat = new WaveFormat();
                SampleRate = AudioFormat.SampleRate;
                if (Recorder != null)
                {
                    Recorder.Dispose();
                }
                Recorder = new LoopbackRecorder(Data.BufferSize, true);
                Recorder.StartRecording();
                timer.Enabled = true;
                timerLEDUpdate.Enabled = true;
                timerVolumeUpdate.Enabled = true;
                VisualizationOnButton.Text = "Выключить";
                bufferTrackBar.Enabled = false;
            }
            else
            {
                if (serialPort.IsOpen)
                {
                    try
                    {
                        if (DeviceSettings != null)
                        {
                            byte[] array = SendCommandList.SetBrightnessCommand(DeviceSettings.BacklightBrightness);
                            serialPort.Write(array, 0, (int)array.Length);
                            array = SendCommandList.SetColourCommand(DeviceSettings.BacklightRed, DeviceSettings.BacklightGreen, DeviceSettings.BacklightBlue);
                            serialPort.Write(array, 0, (int)array.Length);
                            array = SendCommandList.ChangeBacklightModeCommand(DeviceSettings.BacklightMode);
                            serialPort.Write(array, 0, (int)array.Length);
                        }
                    }
                    catch { errorCount++; }
                }
                timer.Enabled = false;
                timerLEDUpdate.Enabled = false;
                timerVolumeUpdate.Enabled = false;
                enumerator.Dispose();
                Recorder.StopRecording();
                VisualizationOnButton.Text = "Включить";
                bufferTrackBar.Enabled = true;
            }
        }

        private void TimerVolumeUpdate_Tick(object sender, ElapsedEventArgs e)
        {
            Data.LastValue[3] = Data.Value[3];
            Data.Value[3] = Data.ColourDefault[3];
            MMDevice adev = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
            float volume = adev.AudioMeterInformation.MasterPeakValue;
            Data.Value[Data.ColourLink[3]] += volume * Data.Coefficient[3];

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
                if (maxVolume != 0) Data.Value[Data.ColourLink[3]] *= Data.AutoVolumeTarget / maxVolume;
            }

            if (Data.Value[3] > 10) Data.Value[3] = 10;

            double div = ((double)(timer.Interval) / (double)(timerVolumeUpdate.Interval) * (double)Data.IntervalMultiplier);
            double st1 = (Data.Value[3] - Data.CurrentValue[3]) / div;
            double st2 = (Data.Value[3] - Data.LastValue[3]) / div;
            Data.Step[3] = (Math.Abs(st1) > Math.Abs(st2)) ? st1 : st2;
            Data.StepValueLimit[3] = Data.Value[3] + Data.Step[3] * div;
            if (Data.Step[3] > Data.RiseMaxStep[3]) Data.Step[3] = Data.RiseMaxStep[3];
            else if (Data.Step[3] < -Data.FallMaxStep[3]) Data.Step[3] = -Data.FallMaxStep[3];

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
                Data.Value[i] *= (i == posMax) ? Data.LeaderMul[i] : Data.FollowerMul[i];
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
                Data.Step[i] = (Math.Abs(st1) > Math.Abs(st2)) ? st1 : st2;
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
            Data.Value[Data.ColourLink[0]] += Data.Coefficient[0] * (v) / Math.Min(mil, fvmaxl.Length) / 20.0;

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
            Data.Value[Data.ColourLink[1]] += Data.Coefficient[1] * (v) / Math.Min(mim, fvmaxm.Length) / 20.0;

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
            Data.Value[Data.ColourLink[2]] += Data.Coefficient[2] * (v) / Math.Min(mih, fvmaxh.Length) / 20.0;
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

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon.Visible = true;
            }
        }

        private void BrightnessTrackBar_ValueChanged(object sender, EventArgs e)
        {
            double v = (double)brightnessTrackBar.Value;
            v = v * v / 2500;
            brightnessTrackBarLabel.Text = v.ToString("F2");
            Data.BrightnessMul =  v;
        }

        private void LoadVisualizationPreset(VisualizationData data)
        {
            textBoxDataSendTim.Text = data.DataSendingTimerInterval.ToString();
            textBoxDataProcessTim.Text = data.DataProcessingTimerInterval.ToString();
            textBoxVolumeUpdateTim.Text = data.DataVolumeProcessingTimerInterval.ToString();

            lowFreqComboBox.SelectedIndex = data.ColourLink[0];
            midFreqComboBox.SelectedIndex = data.ColourLink[1];
            hightFreqComboBox.SelectedIndex = data.ColourLink[2];
            volumeComboBox.SelectedIndex = data.ColourLink[3];
            textBoxLFC.Text = data.Coefficient[0].ToString();
            textBoxMFC.Text = data.Coefficient[1].ToString();
            textBoxHFC.Text = data.Coefficient[2].ToString();
            textBoxVolC.Text = data.Coefficient[3].ToString();
            textBoxFreqL.Text = data.Frequency[0].ToString();
            textBoxFreqM.Text = data.Frequency[1].ToString();
            textBoxFreqH.Text = data.Frequency[2].ToString();
            textBoxRiseR.Text = data.RiseMaxStep[0].ToString();
            textBoxRiseG.Text = data.RiseMaxStep[1].ToString();
            textBoxRiseB.Text = data.RiseMaxStep[2].ToString();
            textBoxRiseV.Text = data.RiseMaxStep[3].ToString();
            textBoxFallR.Text = data.FallMaxStep[0].ToString();
            textBoxFallG.Text = data.FallMaxStep[1].ToString();
            textBoxFallB.Text = data.FallMaxStep[2].ToString();
            textBoxFallV.Text = data.FallMaxStep[3].ToString();
            int k = 0;
            for (int buf = data.BufferSize; buf > 0; buf >>= 1) k++;
            bufferTrackBar.Value = k - 1;
            textBoxColR.Text = data.ColourDefault[0].ToString();
            textBoxColG.Text = data.ColourDefault[1].ToString();
            textBoxColB.Text = data.ColourDefault[2].ToString();
            textBoxColV.Text = data.ColourDefault[3].ToString();
            textBoxLeaderR.Text = data.LeaderMul[0].ToString();
            textBoxLeaderG.Text = data.LeaderMul[1].ToString();
            textBoxLeaderB.Text = data.LeaderMul[2].ToString();
            textBoxFollowerR.Text = data.FollowerMul[0].ToString();
            textBoxFollowerG.Text = data.FollowerMul[1].ToString();
            textBoxFollowerB.Text = data.FollowerMul[2].ToString();
            autoVolumeCheckBox.Checked = data.AutoVolumeEnable;
            textBoxAutoVolTarget.Text = data.AutoVolumeTarget.ToString();
            textBoxAutoVolCounter.Text = data.AutoVolumeCounterMax.ToString();
            textBoxAutoVolStep.Text = data.AutoVolumeMulHysteresis.ToString();
            brightnessTrackBar.Value = data.VolumeTrack;
            autoEnableVisCB.Checked = data.VisualizationEnable;
        }

        private void ChangeVisualizationPresetData(VisualizationData data)
        {
            if (timerLEDUpdate != null) timerLEDUpdate.Interval = textBoxDataSendTim.Text.ToDouble();
            if (timer != null) timer.Interval = textBoxDataProcessTim.Text.ToDouble();
            if (timerVolumeUpdate != null) timerVolumeUpdate.Interval = textBoxVolumeUpdateTim.Text.ToDouble();
            data.DataProcessingTimerInterval = textBoxDataProcessTim.Text.ToDouble();
            data.DataSendingTimerInterval = textBoxDataSendTim.Text.ToDouble();
            data.DataVolumeProcessingTimerInterval = textBoxVolumeUpdateTim.Text.ToDouble();

            data.ColourLink[0] = lowFreqComboBox.SelectedIndex;
            data.ColourLink[1] = midFreqComboBox.SelectedIndex;
            data.ColourLink[2] = hightFreqComboBox.SelectedIndex;
            data.ColourLink[3] = volumeComboBox.SelectedIndex;
            data.Coefficient[0] = textBoxLFC.Text.ToDouble();
            data.Coefficient[1] = textBoxMFC.Text.ToDouble();
            data.Coefficient[2] = textBoxHFC.Text.ToDouble();
            data.Coefficient[3] = textBoxVolC.Text.ToDouble();
            data.Frequency[0] = textBoxFreqL.Text.ToDouble();
            data.Frequency[1] = textBoxFreqM.Text.ToDouble();
            data.Frequency[2] = textBoxFreqH.Text.ToDouble();
            data.BufferSize = 1 << bufferTrackBar.Value;
            data.RiseMaxStep[0] = textBoxRiseR.Text.ToDouble();
            data.RiseMaxStep[1] = textBoxRiseG.Text.ToDouble();
            data.RiseMaxStep[2] = textBoxRiseB.Text.ToDouble();
            data.RiseMaxStep[3] = textBoxRiseV.Text.ToDouble();
            data.FallMaxStep[0] = textBoxFallR.Text.ToDouble();
            data.FallMaxStep[1] = textBoxFallG.Text.ToDouble();
            data.FallMaxStep[2] = textBoxFallB.Text.ToDouble();
            data.FallMaxStep[3] = textBoxFallV.Text.ToDouble();
            data.ColourDefault[0] = textBoxColR.Text.ToDouble();
            data.ColourDefault[1] = textBoxColG.Text.ToDouble();
            data.ColourDefault[2] = textBoxColB.Text.ToDouble();
            data.ColourDefault[3] = textBoxColV.Text.ToDouble();
            data.LeaderMul[0] = textBoxLeaderR.Text.ToDouble();
            data.LeaderMul[1] = textBoxLeaderG.Text.ToDouble();
            data.LeaderMul[2] = textBoxLeaderB.Text.ToDouble();
            data.FollowerMul[0] = textBoxFollowerR.Text.ToDouble();
            data.FollowerMul[1] = textBoxFollowerG.Text.ToDouble();
            data.FollowerMul[2] = textBoxFollowerB.Text.ToDouble();
            data.AutoVolumeEnable = autoVolumeCheckBox.Checked;
            data.AutoVolumeTarget = textBoxAutoVolTarget.Text.ToDouble();
            data.AutoVolumeCounterMax = textBoxAutoVolCounter.Text.ToInt();
            data.AutoVolumeMulHysteresis = textBoxAutoVolStep.Text.ToDouble();

            data.VolumeTrack = brightnessTrackBar.Value;
            data.VisualizationEnable = autoEnableVisCB.Checked;

        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (tabControl1.SelectedTab == tabVisualization)
            {
                LoadVisualizationPreset(Data);
            }
        }

        private void VisualizationButtonApply_Click(object sender, EventArgs e)
        {
            ChangeVisualizationPresetData(Data);
            Data.SaveToRegistry();
            VisualizationOnButton.Enabled = true;
        }

        private void buttonDefaultReset_Click(object sender, EventArgs e)
        {
            Data = new VisualizationData();
            textBoxDataProcessTim.Text = "100";
            textBoxDataSendTim.Text = "20";
            LoadVisualizationPreset(Data);
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        private void autoVolumeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            brightnessTrackBar.Enabled = (autoVolumeCheckBox.Checked) ? false : true;
            Data.AutoVolumeEnable = autoVolumeCheckBox.Checked;
        }

        private void DataThreadAction()
        {
            Data.LoadFromRegistry();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            Data = new VisualizationData();
            Data.LoadFromRegistry();
            ComportReloadButton_Click(this, new EventArgs());
            RegistryKey key = Registry.CurrentUser.OpenSubKey(subKey, false);
            if (key != null)
            {
                autoConnectCB.Checked = Convert.ToBoolean(key?.GetValue("AutoConnect"));
                formMinimizeCB.Checked = Convert.ToBoolean(key?.GetValue("Minimize"));
                if (autoConnectCB.Checked)
                {
                    try
                    {
                        serialPort.PortName = Convert.ToString(key?.GetValue("SerialPort"));
                        comportBox.Items.Clear();
                        comportBox.Items.Add(serialPort.PortName);
                        comportBox.SelectedIndex = 0;
                        serialPort.Open();
                        Buffer = new RxBuffer(2048, RxDataActions, RxAltDataActions);
                    }
                    catch { MessageBox.Show("Не удалось подключиться.", "Error"); }
                }
                if (formMinimizeCB.Checked)
                {
                    this.WindowState = FormWindowState.Minimized;
                }
            }
            key?.Close();
            if (Data.VisualizationEnable)
            {
                autoEnableVisCB.Checked = Data.VisualizationEnable;
                Thread.Sleep(100);
                Button1_Click(this, new EventArgs());
            }
        }

        private void autoConnectCB_CheckedChanged(object sender, EventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(subKey);
            key?.SetValue("AutoConnect", autoConnectCB.Checked);
            key.Close();
        }

        private void commandList_SelectedIndexChanged(object sender, EventArgs e)
        {
            label1.Text = "";
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";
            label5.Text = "";
            label6.Text = "";
            label7.Text = "";
            label8.Text = "";
            label9.Text = "";
            switch (commandList.SelectedIndex)
            {
                case 0:
                    label1.Text = "Red (float)";
                    label2.Text = "Green (float)";
                    label3.Text = "Blue (float)";
                    commandInfoTextBox.Clear();
                    commandInfoTextBox.AppendText("Устанавливает значение цвета. (float)\r\n");
                    commandInfoTextBox.AppendText("Полноценно работает только в Direct Mode, Flash Mode 1, Flash Mode 2 и при выключенной визуализации.\r\n");
                    commandInfoTextBox.AppendText("Значение цвет*яркость должно быть меньше 2000.\r\n");
                    break;

                case 1:
                    label1.Text = "Яркость (float)";
                    commandInfoTextBox.Clear();
                    commandInfoTextBox.AppendText("Устанавливает значение яркости. (float)\r\n");
                    commandInfoTextBox.AppendText("Полноценно работает только в Direct Mode и при выключенной визуализации.\r\n");
                    commandInfoTextBox.AppendText("Значение цвет*яркость должно быть меньше 2000.\r\n");
                    break;

                case 2:
                    label1.Text = "Значение (uint8)";
                    commandInfoTextBox.Clear();
                    commandInfoTextBox.AppendText("Изменение режима подсветки. (uint8)\r\n");
                    commandInfoTextBox.AppendText("0 - Direct Mode. В этом режиме цвет устанавливается только командами с ПК.\r\n");
                    commandInfoTextBox.AppendText("1 - Default Preset Mode. Анимация переливающихся цветов.\r\n");
                    commandInfoTextBox.AppendText("2 - User Preset Mode. Настраиваемая пользователем анимация.\r\n");
                    commandInfoTextBox.AppendText("3 - Flash Mode 1. Анимация изменения яркости от 0 до 10. Цвет неизменен и устанавливается пользователем.\r\n");
                    commandInfoTextBox.AppendText("4 - Flash Mode 2. Анимация изменения яркости от 5 до 10. Цвет неизменен и устанавливается пользователем.\r\n");
                    break;

                case 3:
                    label1.Text = "Значение (uint8)";
                    commandInfoTextBox.Clear();
                    commandInfoTextBox.AppendText("Настройка User Preset Mode - количество шагов подсветки. (uint8)\r\n");
                    commandInfoTextBox.AppendText("Переход с шага на шаг происходит за 1с.\r\n");
                    commandInfoTextBox.AppendText("Начальный шаг - 0. Значение должно быть равно номеру маскимального шага.\r\n");
                    break;

                case 4:
                    label1.Text = "Шаг режима (uint8)";
                    label2.Text = "Цель ярк. (float)";
                    label3.Text = "Шаг ярк. (float)";
                    label4.Text = "Цель red (float)";
                    label5.Text = "Шаг red (float)";
                    label6.Text = "Цель green (float)";
                    label7.Text = "Шаг green (float)";
                    label8.Text = "Цель blue (float)";
                    label9.Text = "Шаг blue (float)";
                    commandInfoTextBox.Clear();
                    commandInfoTextBox.AppendText("Настройка шагов режима User Preset Mode. Первый параметр - (uint8), остальные (float)\r\n");
                    commandInfoTextBox.AppendText("Переход с шага на шаг режима происходит за 1с.\r\n");
                    commandInfoTextBox.AppendText("В ходе работы режима каждые 1ms происодит изменение шага параметра на указанное значение, пока оно не достигнет цели.\r\n");
                    commandInfoTextBox.AppendText("Если цель не достигнута до следующего шага, то отсчет продолжается с последнего значения\r\n");
                    commandInfoTextBox.AppendText("Пример визуализации красный-оранжевый-желтый-зеленый-синий-розовый:\r\n");
                    commandInfoTextBox.AppendText("{5, 10, 200, 20, 0, 20, 0, 20},\r\n{ 5, 10, 200, 0.1, 0, 0.2, 0, 0.1}, " +
                        "{ 5, 10, 200, 0.1, 0, 0.2, 0, 0.1},\r\n{5, 10, 200, 20, 109, 0.109, 0, 20}, " +
                        "{5, 10, 200, 20, 109, 0.109, 0, 20},\r\n{5, 10, 200, 20, 200, 0.091, 0, 20}, " +
                        "{5, 10, 200, 20, 200, 0.091, 0, 20},,\r\n{ 5, 10, 0, 0.2, 200, 0.0706, 0, 20}, " +
                        "{ 5, 10, 0, 0.2, 200, 0.0706, 0, 20},\r\n{ 5, 10, 0, 0.2, 0, 0.2, 200, 0.2}, " +
                        "{ 5, 10, 0, 0.2, 0, 0.2, 200, 0.2},\r\n{ 5, 10, 100, 0.1, 0, 0.2, 100, 0.1}, " +
	                    "{ 5, 10, 100, 0.1, 0, 0.2, 100, 0.1},\r\n");
                    break;

                case 5:
                    label1.Text = "Скопировать с (uint8)";
                    label2.Text = "Скопировать в (uint8)";
                    commandInfoTextBox.Clear();
                    commandInfoTextBox.AppendText("Копирование значений одного шага User Preset Mode в другой (uint8)\r\n");
                    break;

                case 6:
                    label1.Text = "Значение (uint8)";
                    commandInfoTextBox.Clear();
                    commandInfoTextBox.AppendText("Сохранение настроек в память микроконтроллера (uint8)\r\n");
                    commandInfoTextBox.AppendText("Пустое значение - сохранить все\r\n");
                    commandInfoTextBox.AppendText("1 - сохранить режим беспроводной передачи энергии\r\n");
                    commandInfoTextBox.AppendText("2 - сохранить значение калибровки беспроводной передачи энергии\r\n");
                    commandInfoTextBox.AppendText("3 - сохранить состояние LED подсветки(ключена или выключена)\r\n");
                    commandInfoTextBox.AppendText("4 - сохранить режим подсветки\r\n");
                    commandInfoTextBox.AppendText("5 - сохранить значение красного цвета подсветки (для успешной загрузки должны быть сохранены все цвета)\r\n");
                    commandInfoTextBox.AppendText("6 - сохранить значение зеленого цвета подсветки (для успешной загрузки должны быть сохранены все цвета)\r\n");
                    commandInfoTextBox.AppendText("7 - сохранить значение синего цвета подсветки (для успешной загрузки должны быть сохранены все цвета)\r\n");
                    commandInfoTextBox.AppendText("8 - сохранить локальное значение яркости подсветки\r\n");
                    commandInfoTextBox.AppendText("9 - сохранить глобальное( работает во всех режимах) значение яркости подсветки\r\n");
                    commandInfoTextBox.AppendText("10 - сохранить все параметры User Preset Mode подсветки\r\n");
                    break;

                case 7:
                    label1.Text = "Значение (float)";
                    commandInfoTextBox.Clear();
                    commandInfoTextBox.AppendText("Установить глобальное( работает во всех режимах) значение яркости подсветки (float)\r\n");
                    commandInfoTextBox.AppendText("Должно быть в интервале от 0 до 1\r\n");
                    break;

                case 8:
                    commandInfoTextBox.Clear();
                    commandInfoTextBox.AppendText("Перезагрузка\r\n");
                    break;

                case 9:
                    label1.Text = "Значение (float)";
                    commandInfoTextBox.Clear();
                    commandInfoTextBox.AppendText("Калибровка авторежима передатчика энергии (float)\r\n");
                    commandInfoTextBox.AppendText("Значение - минимальный уровень тока, после которого передатчик выключается в целях экономии энергии\r\n");
                    commandInfoTextBox.AppendText("Пустое значение - автокалибровка. Для корректного выполнения необходимо поставить на <передатчик> <приемник> " +
                        "для беспроводной передачи энергии. И оставить их в подключенном состоянии 180 секунд.\r\n");
                    break;

                case 10:
                    label1.Text = "Значение (uint8)";
                    commandInfoTextBox.Clear();
                    commandInfoTextBox.AppendText("Изменение режима передатчика энергии. (uint8)\r\n");
                    commandInfoTextBox.AppendText("0 - выключен\r\n");
                    commandInfoTextBox.AppendText("1 - авто режим");
                    break;

                case 11:
                    commandInfoTextBox.Clear();
                    commandInfoTextBox.AppendText("Отправка значений настроек\r\n");
                    break;

            }
        }

        private void statisticCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (statisticCheckBox.Checked) Data.Value.ResetMaxValues();
        }

        private void bufferTrackBar_ValueChanged(object sender, EventArgs e)
        {
            bufferTrackBarLabel.Text = bufferTrackBar.Value.ToString();
        }

        private void bufferTrackBar_Scroll_1(object sender, EventArgs e)
        {
            VisualizationOnButton.Enabled = false;
        }

        private void timerStatusUpdate_Tick(object sender, EventArgs e)
        {
            this.Text = "HUlamp Terminal (Connected = " + Convert.ToString(serialPort?.IsOpen) +
                ", Visualization = " + Convert.ToString(timer?.Enabled) + ")";
        }


        private void formMinimizeCB_CheckedChanged(object sender, EventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(subKey);
            key?.SetValue("Minimize", formMinimizeCB.Checked);
            key?.Close();
        }
    }
}
