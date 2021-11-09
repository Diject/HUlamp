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

        private RxBuffer Buffer;
        private RxAltDataArgs DeviceSettings;
        private bool ShowDeviceSettings = false;
        private VisualizationData Data;
        private bool VisualizationEnable = false;
        private static readonly string subKey = @"Software\HUlamp Terminal";

        private ColorPicker colorPicker = new ColorPicker();

        private RadioButton[] ModeRadioButtonGroup;

        private void RxDataActions(object sender, RxMainDataArgs e)
        {
            String str = "[" + DateTime.Now.ToString("HH:mm:ss.ffff") + "] Current = " + e.CurrentData.ToString("F3") + " Temperature = " +
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
                ModeRadioButtonGroup[DeviceSettings.BacklightMode].Checked = true;
                float colMax = Math.Max(DeviceSettings.BacklightRed, Math.Max(DeviceSettings.BacklightGreen, DeviceSettings.BacklightBlue));
                byte r = (byte)(DeviceSettings.BacklightRed / colMax * 255f + 0.5f);
                byte g = (byte)(DeviceSettings.BacklightGreen / colMax * 255f + 0.5f);
                byte b = (byte)(DeviceSettings.BacklightBlue / colMax * 255f + 0.5f);
                Color cl = Color.FromArgb(r, g, b);
                nsColorPanel.BackColor = cl;
                nsBacklightStateCB.Checked = Convert.ToBoolean(DeviceSettings.LEDState);
                nsTransmitterStateCB.Checked = Convert.ToBoolean(DeviceSettings.ETXMode);
                nsBacklightTB.Value = (int)(DeviceSettings.BrightnessMultiplier * 100f + 0.5f);
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
                    if (serialPort.IsOpen)
                    {
                        ShowDeviceSettings = false;
                        byte[] array = SendCommandList.RequestAltData();
                        serialPort.Write(array, 0, (int)array.Length);
                    }
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
                            binWriter.Write(textboxParam1.Text.ToFloat());
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
            if (!timer.Enabled) //определение выключено ли
            {
                enumerator = new MMDeviceEnumerator();
                if (serialPort.IsOpen)
                {
                    try
                    {
                        //ShowDeviceSettings = false;
                        //byte[] array = SendCommandList.RequestAltData();
                        //serialPort.Write(array, 0, (int)array.Length);
                        //Thread.Sleep(200);
                        byte[] array = SendCommandList.SetBrightnessCommand(1);
                        serialPort.Write(array, 0, (int)array.Length);
                        Thread.Sleep(1);
                        array = SendCommandList.ChangeBacklightModeCommand(0);
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
                VisualizationEnable = true;
                nsVisualizationOnCB.CheckedChanged -= nsVisualizationOnCB_CheckedChanged;
                nsVisualizationOnCB.Checked = true;
                nsVisualizationOnCB.CheckedChanged += nsVisualizationOnCB_CheckedChanged;
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
                VisualizationEnable = false;
                nsVisualizationOnCB.CheckedChanged -= nsVisualizationOnCB_CheckedChanged;
                nsVisualizationOnCB.Checked = false;
                nsVisualizationOnCB.CheckedChanged += nsVisualizationOnCB_CheckedChanged;
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
            Data.BrightnessMul = v;
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
            textBoxVolPow.Text = data.VolumePowerValue.ToString();

            nsVisualizationVolumeTB.ValueChanged -= nsVisualizationVolumeTB_ValueChanged;
            nsVisualizationOnCB.CheckedChanged -= nsVisualizationOnCB_CheckedChanged;
            nsVisualizationAutoVol.CheckedChanged -= nsVisualizationAutoVol_CheckedChanged;
            nsVisualizationStartOn.CheckedChanged -= nsVisualizationStartOn_CheckedChanged;
            nsVisualizationVolumeTB.Value = Convert.ToInt32(data.AutoVolumeTarget * 100d + 0.5);
            nsVisualizationOnCB.Checked = VisualizationEnable;
            nsVisualizationAutoVol.Checked = data.AutoVolumeEnable;
            nsVisualizationStartOn.Checked = data.VisualizationEnable;
            nsVisualizationVolumeTB.ValueChanged += nsVisualizationVolumeTB_ValueChanged;
            nsVisualizationOnCB.CheckedChanged += nsVisualizationOnCB_CheckedChanged;
            nsVisualizationAutoVol.CheckedChanged += nsVisualizationAutoVol_CheckedChanged;
            nsVisualizationStartOn.CheckedChanged += nsVisualizationStartOn_CheckedChanged;
            nsVisualizationVolumeTB.Enabled = nsVisualizationAutoVol.Checked;
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
            data.VolumePowerValue = textBoxVolPow.Text.ToDouble();

            data.VolumeTrack = brightnessTrackBar.Value;
            data.VisualizationEnable = autoEnableVisCB.Checked;

            nsVisualizationVolumeTB.ValueChanged -= nsVisualizationVolumeTB_ValueChanged;
            nsVisualizationOnCB.CheckedChanged -= nsVisualizationOnCB_CheckedChanged;
            nsVisualizationAutoVol.CheckedChanged -= nsVisualizationAutoVol_CheckedChanged;
            nsVisualizationStartOn.CheckedChanged -= nsVisualizationStartOn_CheckedChanged;
            nsVisualizationVolumeTB.Value = Convert.ToInt32(data.AutoVolumeTarget * 100d + 0.5);
            nsVisualizationOnCB.Checked = VisualizationEnable;
            nsVisualizationAutoVol.Checked = data.AutoVolumeEnable;
            nsVisualizationStartOn.Checked = data.VisualizationEnable;
            nsVisualizationVolumeTB.ValueChanged += nsVisualizationVolumeTB_ValueChanged;
            nsVisualizationOnCB.CheckedChanged += nsVisualizationOnCB_CheckedChanged;
            nsVisualizationAutoVol.CheckedChanged += nsVisualizationAutoVol_CheckedChanged;
            nsVisualizationStartOn.CheckedChanged += nsVisualizationStartOn_CheckedChanged;
            nsVisualizationVolumeTB.Enabled = nsVisualizationAutoVol.Checked;
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (tabControl.SelectedTab == tabVisualization)
            {
                LoadVisualizationPreset(Data);
            }
            if (tabControl.SelectedTab == tabFastSettings)
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
            ModeRadioButtonGroup = new RadioButton[5] { nsModeRB1, nsModeRB2, nsModeRB3, nsModeRB4, nsModeRB5 };
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
                        ShowDeviceSettings = false;
                        byte[] array = SendCommandList.RequestAltData();
                        serialPort.Write(array, 0, (int)array.Length);
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

            LoadVisualizationPreset(Data);
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
                    label1.Text = "Ячейка (uint8)";
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
            if (statisticCheckBox.Checked)
            {
                Data.Value.ResetMaxValues();
                statLab1.Visible = true;
                statLab2.Visible = true;
                statLab3.Visible = true;
                statLab4.Visible = true;
                statLab5.Visible = true;
                statLab6.Visible = true;
                statLab7.Visible = true;
                statLab8.Visible = true;
                statLab9.Visible = true;
                statLab10.Visible = true;
                statLab11.Visible = true;
                statLab12.Visible = true;
                statLabD1.Visible = true;
                statLabD2.Visible = true;
                statLabD3.Visible = true;
            }
            else
            {
                statLab1.Visible = false;
                statLab2.Visible = false;
                statLab3.Visible = false;
                statLab4.Visible = false;
                statLab5.Visible = false;
                statLab6.Visible = false;
                statLab7.Visible = false;
                statLab8.Visible = false;
                statLab9.Visible = false;
                statLab10.Visible = false;
                statLab11.Visible = false;
                statLab12.Visible = false;
                statLabD1.Visible = false;
                statLabD2.Visible = false;
                statLabD3.Visible = false;
            }
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

        private void cmAddB_Click(object sender, EventArgs e)
        {
            if (cmTreeView.Nodes.Count >= 61) return;
            //DialogResult dres = colorDialog.ShowDialog();
            //if (dres == DialogResult.Cancel) return;
            colorPicker.ShowDialog();
            if (colorPicker.DialogResult == DialogResult.Cancel) return;
            int sn = 1;
            if (cmTreeView.SelectedNode != null) sn = cmTreeView.SelectedNode.Index;
            Color cl = colorPicker.Color;
            TreeNode node = new TreeNode(Convert.ToString(cl));
            node.BackColor = cl;
            node.Tag = cl;
            node.ContextMenuStrip = cmListContextMenu;
            cmTreeView.Nodes.Add(node);
        }

        private void cmMenuItemChange_Click(object sender, EventArgs e)
        {
            if (cmTreeView.SelectedNode == null) return;
            colorPicker.ShowDialog();
            if (colorPicker.DialogResult == DialogResult.Cancel) return;
            TreeNode node = (cmTreeView.SelectedNode == cmTreeView.Nodes[0]) ? cmTreeView.Nodes[cmTreeView.Nodes.Count - 1] : cmTreeView.SelectedNode;
            node.Tag = colorPicker.Color;
            node.Text = Convert.ToString(colorPicker.Color);
            node.BackColor = colorPicker.Color;
        }

        private void cmMenuItemDelete_Click(object sender, EventArgs e)
        {
            if (cmTreeView.SelectedNode == null) return;
            if (cmTreeView.SelectedNode == cmTreeView.Nodes[0]) cmTreeView.Nodes[cmTreeView.Nodes.Count - 1].Remove();
            else cmTreeView.SelectedNode.Remove();
        }

        private void cmMenuItemInsert_Click(object sender, EventArgs e)
        {
            if (cmTreeView.Nodes.Count >= 61) return;
            //DialogResult dres = colorDialog.ShowDialog();
            colorPicker.ShowDialog();
            if (colorPicker.DialogResult == DialogResult.Cancel) return;
            TreeNode node = new TreeNode(Convert.ToString(colorPicker.Color));
            node.BackColor = colorPicker.Color;
            node.Tag = colorPicker.Color;
            node.ContextMenuStrip = cmListContextMenu;
            cmTreeView.Nodes.Insert(cmTreeView.SelectedNode.Index + 1, node);
        }

        private void cmMenuItemDuplicate_Click(object sender, EventArgs e)
        {
            if (cmTreeView.Nodes.Count >= 61) return;
            if (cmTreeView.SelectedNode == null) return;
            Color cl = (cmTreeView.SelectedNode == cmTreeView.Nodes[0]) ? cmTreeView.Nodes[cmTreeView.Nodes.Count - 1].BackColor :
                cmTreeView.SelectedNode.BackColor;
            TreeNode node = new TreeNode(Convert.ToString(cl));
            node.BackColor = cl;
            node.Tag = cl;
            node.ContextMenuStrip = cmListContextMenu;
            cmTreeView.Nodes.Insert(cmTreeView.SelectedNode.Index + 1, node);
        }

        private void cmChangeB_Click(object sender, EventArgs e)
        {
            if (cmTreeView.SelectedNode == null) return;
            //DialogResult dres = colorDialog.ShowDialog();
            colorPicker.ShowDialog();
            if (colorPicker.DialogResult == DialogResult.Cancel) return;
            cmTreeView.SelectedNode.Tag = colorPicker.Color;
            cmTreeView.SelectedNode.Text = Convert.ToString(colorPicker.Color);
            cmTreeView.SelectedNode.BackColor = colorPicker.Color;
        }


        private void cmTreeView_DoubleClick(object sender, EventArgs e)
        {
            if (cmTreeView.SelectedNode == null) return;
            colorPicker.ShowDialog();
            if (colorPicker.DialogResult == DialogResult.Cancel) return;
            TreeNode node = (cmTreeView.SelectedNode == cmTreeView.Nodes[0]) ? cmTreeView.Nodes[cmTreeView.Nodes.Count - 1] : cmTreeView.SelectedNode;
            node.Tag = colorPicker.Color;
            node.Text = Convert.ToString(colorPicker.Color);
            node.BackColor = colorPicker.Color;
        }

        private void cmDeleteSelectedB_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < cmTreeView.Nodes.Count; i++)
            {
                if (cmTreeView.Nodes[i].Checked) cmTreeView.Nodes[i--].Remove();
            }
        }

        private void cmTreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node == cmTreeView.Nodes[0])
            {
                for (int i = 1; i < cmTreeView.Nodes.Count; i++)
                {
                    cmTreeView.Nodes[i].Checked = cmTreeView.Nodes[0].Checked;
                }
            }
        }

        private float _alphaConvert(byte value)
        {
            float brtMul = cmBrtMultextBox.Text.ToFloat();
            if ((brtMul > 1) || (brtMul < 0)) brtMul = 1;
            return ((float)value) / 255f * 10f * brtMul;
        }

        private float _colorConvert(byte value)
        {
            return ((float)value) / 255f * 200f;
        }

        private float _stepCalculate(float val1, float val2)
        {
            return Math.Abs((val2 - val1) / 1000f);
        }

        private void cmLoadToDeviceB_Click(object sender, EventArgs e)
        {
            if (cmTreeView.Nodes.Count <= 1) return;
            float brt = _alphaConvert(((Color)cmTreeView.Nodes[1].Tag).A);
            float red = _colorConvert(((Color)cmTreeView.Nodes[1].Tag).R);
            float green = _colorConvert(((Color)cmTreeView.Nodes[1].Tag).G);
            float blue = _colorConvert(((Color)cmTreeView.Nodes[1].Tag).B);
            byte[] array = SendCommandList.CustomModeDataCommand(0, brt, 10f, red, 200f, green, 200f, blue, 200f);
            serialPort.Write(array, 0, (int)array.Length);

            Thread.Sleep(10);
            brt = _alphaConvert(((Color)cmTreeView.Nodes[1].Tag).A);
            red = _colorConvert(((Color)cmTreeView.Nodes[1].Tag).R);
            green = _colorConvert(((Color)cmTreeView.Nodes[1].Tag).G);
            blue = _colorConvert(((Color)cmTreeView.Nodes[1].Tag).B);
            float brtL = _alphaConvert(((Color)cmTreeView.Nodes[cmTreeView.Nodes.Count - 1].Tag).A);
            float redL = _colorConvert(((Color)cmTreeView.Nodes[cmTreeView.Nodes.Count - 1].Tag).R);
            float greenL = _colorConvert(((Color)cmTreeView.Nodes[cmTreeView.Nodes.Count - 1].Tag).G);
            float blueL = _colorConvert(((Color)cmTreeView.Nodes[cmTreeView.Nodes.Count - 1].Tag).B);
            array = SendCommandList.CustomModeDataCommand((byte)1, brt, _stepCalculate(brtL, brt), red, _stepCalculate(redL, red),
                green, _stepCalculate(greenL, green), blue, _stepCalculate(blueL, blue));
            serialPort.Write(array, 0, (int)array.Length);
            Thread.Sleep(10);

            for (int i = 2; i < cmTreeView.Nodes.Count; i++)
            {
                brt = _alphaConvert(((Color)cmTreeView.Nodes[i].Tag).A);
                red = _colorConvert(((Color)cmTreeView.Nodes[i].Tag).R);
                green = _colorConvert(((Color)cmTreeView.Nodes[i].Tag).G);
                blue = _colorConvert(((Color)cmTreeView.Nodes[i].Tag).B);
                brtL = _alphaConvert(((Color)cmTreeView.Nodes[i - 1].Tag).A);
                redL = _colorConvert(((Color)cmTreeView.Nodes[i - 1].Tag).R);
                greenL = _colorConvert(((Color)cmTreeView.Nodes[i - 1].Tag).G);
                blueL = _colorConvert(((Color)cmTreeView.Nodes[i - 1].Tag).B);
                array = SendCommandList.CustomModeDataCommand((byte)(i), brt, _stepCalculate(brtL, brt), red, _stepCalculate(redL, red),
                    green, _stepCalculate(greenL, green), blue, _stepCalculate(blueL, blue));
                serialPort.Write(array, 0, (int)array.Length);
                Thread.Sleep(10);
            }

            array = SendCommandList.CustomModeSetupCommand((byte)(cmTreeView.Nodes.Count - 1));
            serialPort.Write(array, 0, (int)array.Length);
            Thread.Sleep(10);

            if (cmSaveCB.Checked)
            {
                array = SendCommandList.SaveCommand("10");
                serialPort.Write(array, 0, (int)array.Length);
            }
        }

        private void cmCustomModeOnOffB_Click(object sender, EventArgs e)
        {
            if (timer.Enabled)
            {
                Button1_Click(this, new EventArgs());
                Thread.Sleep(5);
            }
            byte[] array = SendCommandList.ChangeBacklightModeCommand("2");
            serialPort.Write(array, 0, (int)array.Length);
            Thread.Sleep(5);
            if (cmSaveCB.Checked)
            {
                array = SendCommandList.SaveCommand("4");
                serialPort.Write(array, 0, (int)array.Length);
            }
            DeviceSettings.BacklightMode = 2;
        }

        private void cmTreeView_MouseMove(object sender, MouseEventArgs e)
        {
            cmTreeView.SelectedNode = cmTreeView.GetNodeAt(e.Location);
        }

        private void retryTimer_Tick(object sender, EventArgs e)
        {
            if (Recorder != null)
                if (!Recorder.IsRecording && timer.Enabled)
                {
                    AudioFormat = new WaveFormat();
                    SampleRate = AudioFormat.SampleRate;
                    Recorder.Dispose();
                    Recorder = new LoopbackRecorder(Data.BufferSize, true);
                    Recorder.StartRecording();
                }
        }

        private void autoVolumeResB_Click(object sender, EventArgs e)
        {
            belowMaxVolumeCounter = -1000;
        }

        private void nsVisualizationOnCB_CheckedChanged(object sender, EventArgs e)
        {
            Button1_Click(this, new EventArgs());
        }

        private void nsVisualizationStartOn_CheckedChanged(object sender, EventArgs e)
        {
            Data.VisualizationEnable = !Data.VisualizationEnable;
            autoEnableVisCB.Checked = Data.VisualizationEnable;
            Data.SaveToRegistry();
        }

        private void nsVisualizationAutoVol_CheckedChanged(object sender, EventArgs e)
        {
            Data.AutoVolumeEnable = !Data.AutoVolumeEnable;
            autoVolumeCheckBox.Checked = Data.AutoVolumeEnable;
            Data.SaveToRegistry();
            nsVisualizationVolumeTB.Enabled = nsVisualizationAutoVol.Checked;
        }

        private void nsVisualizationVolumeTB_ValueChanged(object sender, EventArgs e)
        {
            Data.AutoVolumeTarget = (double)nsVisualizationVolumeTB.Value / 100d;
            textBoxAutoVolTarget.Text = Data.AutoVolumeTarget.ToString();
            Data.SaveToRegistry();
        }

        private void nsSetModeB_Click(object sender, EventArgs e)
        {
            if (VisualizationEnable)
            {
                Button1_Click(this, new EventArgs());
            }
            for (int i = 0; i < ModeRadioButtonGroup.Length; i++)
            {
                if (ModeRadioButtonGroup[i].Checked)
                {
                    if (serialPort.IsOpen)
                    {
                        byte[] array = SendCommandList.ChangeBacklightModeCommand((byte)i);
                        serialPort.Write(array, 0, (int)array.Length);
                        Thread.Sleep(2);
                        array = SendCommandList.SaveCommand("4");
                        serialPort.Write(array, 0, (int)array.Length);

                        DeviceSettings.BacklightMode = (byte)i;
                        break;
                    }
                }
            }

        }

        private void nsChangeColorB_Click(object sender, EventArgs e)
        {
            if (VisualizationEnable)
            {
                Button1_Click(this, new EventArgs());
            }
            colorPicker.Color = nsColorPanel.BackColor;
            if (colorPicker.ShowDialog() == DialogResult.OK)
            {
                nsColorPanel.BackColor = colorPicker.Color;
                float r = 0, g = 0, b = 0, a = 0;
                r = _colorConvert(nsColorPanel.BackColor.R);
                g = _colorConvert(nsColorPanel.BackColor.G);
                b = _colorConvert(nsColorPanel.BackColor.B);
                a = ((float)nsColorPanel.BackColor.A) / 255f * 10f;
                if (serialPort.IsOpen)
                {
                    byte[] array = SendCommandList.SetColourCommand(r, g, b);
                    serialPort.Write(array, 0, (int)array.Length);
                    Thread.Sleep(2);
                    array = SendCommandList.SetBrightnessCommand(a);
                    serialPort.Write(array, 0, (int)array.Length);
                    Thread.Sleep(2);
                    array = SendCommandList.SaveCommand("5");
                    serialPort.Write(array, 0, (int)array.Length);
                    Thread.Sleep(2);
                    array = SendCommandList.SaveCommand("6");
                    serialPort.Write(array, 0, (int)array.Length);
                    Thread.Sleep(2);
                    array = SendCommandList.SaveCommand("7");
                    serialPort.Write(array, 0, (int)array.Length);
                    Thread.Sleep(2);
                    array = SendCommandList.SaveCommand("8");
                    serialPort.Write(array, 0, (int)array.Length);

                    DeviceSettings.BacklightRed = r;
                    DeviceSettings.BacklightGreen = g;
                    DeviceSettings.BacklightBlue = b;
                    DeviceSettings.BacklightBrightness = a;
                }
            }
        }

        private void nsTransmitterStateCB_CheckedChanged(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                byte[] array = SendCommandList.SetTXControlCommand((nsTransmitterStateCB.Checked) ? "1" : "0");
                serialPort.Write(array, 0, (int)array.Length);
                Thread.Sleep(2);
                array = SendCommandList.SaveCommand("1");
                serialPort.Write(array, 0, (int)array.Length);

                DeviceSettings.ETXMode = (byte)((nsTransmitterStateCB.Checked) ? 1 : 0);
            }
        }

        private void nsBacklightSetB_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                float val = (float)nsBacklightTB.Value / 100f;
                byte[] array = SendCommandList.SetGlobalBrightnessCommand(val);
                serialPort.Write(array, 0, (int)array.Length);
                Thread.Sleep(2);
                array = SendCommandList.SaveCommand("9");
                serialPort.Write(array, 0, (int)array.Length);

                DeviceSettings.BrightnessMultiplier = val;
            }
        }

        private void nsDataUpdate_Click(object sender, EventArgs e)
        {
            bool visualizationState = false;
            if (VisualizationEnable)
            {
                visualizationState = true;
                Button1_Click(this, new EventArgs());
            }
            Thread.Sleep(200);
            if (serialPort.IsOpen)
            {
                ShowDeviceSettings = false;
                byte[] array = SendCommandList.RequestAltData();
                serialPort.Write(array, 0, (int)array.Length);
            }
            Thread.Sleep(200);
            if (visualizationState)
            {
                Button1_Click(this, new EventArgs());
            }
        }

        private void nsDeviceRebootB_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                byte[] array = SendCommandList.ResetCommand();
                serialPort.Write(array, 0, (int)array.Length);
            }
        }
    }
}
