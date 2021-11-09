using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace HUlamp
{
    public class VlueEx
    {
        public double[] Value = new double[] { 0, 0, 0, 0, 0 };
        public double[] Max = new double[] { 0, 0, 0, 0, 0 };

        public double this[int index]
        {
            get
            {
                return Value[index];
            }
            set
            {
                if (!double.IsNaN(value))
                {
                    Max[index] = Math.Max(Max[index], Value[index]);
                    Value[index] = value;
                }
                else Value[index] = 0;
            }
        }

        public void ResetMaxValues()
        {
            Array.Clear(Max, 0, Max.Length);
        }
    }

    public class VisualizationData
    {
        public bool VisualizationEnable = false;
        public bool CustomBuffer = true;
        public double DataProcessingTimerInterval = 185;
        public double DataSendingTimerInterval = 10;
        public double DataVolumeProcessingTimerInterval = 20;
        public int VolumeTrack = 50;
        public double RGBMul = 1;
        public double BrightnessMul = 1;
        public double IntervalMultiplier = 1;
        public int BufferSize = 8192;
        public double[] ColourDefault = new double[4] { -50, 0, 0, 0 };
        public double[] CurrentValue = new double[4] { 0, 0, 0, 0 };
        public double[] LastValue = new double[4] { 0, 0, 0, 0 };
        public double[] Step = new double[4] { 0, 0, 0, 0 };
        public double[] StepValueLimit = new double[4] { 0, 0, 0, 0 };
        public VlueEx Value = new VlueEx();
        public double[] ValueMax = new double[5] { 0, 0, 0, 0, 0 };
        public double[] Coefficient = new double[] { 16, 36, 60, 10 };
        public int[] ColourLink = new int[4] { 2, 1, 0, 3 };
        public double[] Frequency = new double[3] { 90, 400, 8000 };
        public double[] LeaderMul = new double[3] { 1.3, 1.3, 1.3 };
        public double[] FollowerMul = new double[3] { 1, 1, 1 };
        public double[] RiseMaxStep = new double[4] { 6, 6, 8, 0.04 };
        public double[] FallMaxStep = new double[4] { 4, 4, 6, 0.04 };
        public int AutoVolumeCounterMax = 3000;
        public bool AutoVolumeEnable = true;
        public double AutoVolumeMulHysteresis = 0.1;
        public double AutoVolumeTarget = 0.3;
        public double VolumePowerValue = 1;
        public int SilenceResetCounter = 200;

        private static readonly string subKey = @"Software\HUlamp Terminal\Visualization";

        public void SaveToRegistry()
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(subKey);
            key.SetValue("DataProcessingTimerInterval", DataProcessingTimerInterval);
            key.SetValue("DataSendingTimerInterval", DataSendingTimerInterval);
            key.SetValue("DataVolumeProcessingTimerInterval", DataVolumeProcessingTimerInterval);
            key.SetValue("VisualizationEnable", VisualizationEnable);
            key.SetValue("VolumeTrack", VolumeTrack);
            key.SetValue("BufferSize", BufferSize);
            key.SetValue("RGBMul", RGBMul);
            key.SetValue("BrightnessMul", BrightnessMul);
            key.SetValue("IntervalMultiplier", IntervalMultiplier);
            key.SetValue("AutoVolumeEnable", AutoVolumeEnable);
            key.SetValue("AutoVolumeCounterMax", AutoVolumeCounterMax);
            key.SetValue("AutoVolumeMulHysteresis", AutoVolumeMulHysteresis);
            key.SetValue("AutoVolumeTarget", AutoVolumeTarget);
            key.SetValue("VolumePowerValue", VolumePowerValue);
            key.SetValue("SilenceResetCounter", SilenceResetCounter);
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter binWriter = new BinaryWriter(stream))
                {
                    foreach (double val in ColourDefault) binWriter.Write(val);
                    key.SetValue("ColourDefault", stream.ToArray());
                }
            }
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter binWriter = new BinaryWriter(stream))
                {
                    foreach (double val in Coefficient) binWriter.Write(val);
                    key.SetValue("Coefficient", stream.ToArray());
                }
            }
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter binWriter = new BinaryWriter(stream))
                {
                    foreach (int val in ColourLink) binWriter.Write(val);
                    key.SetValue("ColourLink", stream.ToArray());
                }
            }
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter binWriter = new BinaryWriter(stream))
                {
                    foreach (double val in Frequency) binWriter.Write(val);
                    key.SetValue("Frequency", stream.ToArray());
                }
            }
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter binWriter = new BinaryWriter(stream))
                {
                    foreach (double val in LeaderMul) binWriter.Write(val);
                    key.SetValue("LeaderMul", stream.ToArray());
                }
            }
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter binWriter = new BinaryWriter(stream))
                {
                    foreach (double val in FollowerMul) binWriter.Write(val);
                    key.SetValue("FollowerMul", stream.ToArray());
                }
            }
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter binWriter = new BinaryWriter(stream))
                {
                    foreach (double val in RiseMaxStep) binWriter.Write(val);
                    key.SetValue("RiseMaxStep", stream.ToArray());
                }
            }
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter binWriter = new BinaryWriter(stream))
                {
                    foreach (double val in FallMaxStep) binWriter.Write(val);
                    key.SetValue("FallMaxStep", stream.ToArray());
                }
            }
            key.Close();
        }

        public void LoadFromRegistry()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(subKey, false);
            if (key != null)
            {
                try
                {
                    DataProcessingTimerInterval = Convert.ToDouble(key.GetValue("DataProcessingTimerInterval") ?? DataProcessingTimerInterval);
                    DataSendingTimerInterval = Convert.ToDouble(key?.GetValue("DataSendingTimerInterval") ?? DataSendingTimerInterval);
                    DataVolumeProcessingTimerInterval = Convert.ToDouble(key?.GetValue("DataVolumeProcessingTimerInterval") ?? DataVolumeProcessingTimerInterval);
                    VisualizationEnable = Convert.ToBoolean(key.GetValue("VisualizationEnable"));
                    VolumeTrack = (int)key.GetValue("VolumeTrack");
                    BufferSize = (int)key.GetValue("BufferSize");
                    RGBMul = Convert.ToDouble(key.GetValue("RGBMul"));
                    BrightnessMul = Convert.ToDouble(key.GetValue("BrightnessMul"));
                    IntervalMultiplier = Convert.ToDouble(key.GetValue("IntervalMultiplier"));
                    AutoVolumeEnable = Convert.ToBoolean(key.GetValue("AutoVolumeEnable"));
                    AutoVolumeCounterMax = (int)key.GetValue("AutoVolumeCounterMax");
                    AutoVolumeMulHysteresis = Convert.ToDouble(key.GetValue("AutoVolumeMulHysteresis") ?? AutoVolumeMulHysteresis);
                    AutoVolumeTarget = Convert.ToDouble(key.GetValue("AutoVolumeTarget") ?? AutoVolumeTarget);
                    VolumePowerValue = Convert.ToDouble(key.GetValue("VolumePowerValue") ?? VolumePowerValue);
                    SilenceResetCounter = (int)(key.GetValue("SilenceResetCounter") ?? SilenceResetCounter);

                    using (MemoryStream stream = new MemoryStream((byte[])key.GetValue("ColourDefault")))
                    {
                        using (BinaryReader binReader = new BinaryReader(stream))
                        {
                            for (int i = 0; i < ColourDefault.Length; i++)
                                ColourDefault[i] = binReader.ReadDouble();
                        }
                    }
                    using (MemoryStream stream = new MemoryStream((byte[])key.GetValue("Coefficient")))
                    {
                        using (BinaryReader binReader = new BinaryReader(stream))
                        {
                            for (int i = 0; i < Coefficient.Length; i++)
                                Coefficient[i] = binReader.ReadDouble();
                        }
                    }
                    using (MemoryStream stream = new MemoryStream((byte[])key.GetValue("Frequency")))
                    {
                        using (BinaryReader binReader = new BinaryReader(stream))
                        {
                            for (int i = 0; i < Frequency.Length; i++)
                                Frequency[i] = binReader.ReadDouble();
                        }
                    }
                    using (MemoryStream stream = new MemoryStream((byte[])key.GetValue("LeaderMul")))
                    {
                        using (BinaryReader binReader = new BinaryReader(stream))
                        {
                            for (int i = 0; i < LeaderMul.Length; i++)
                                LeaderMul[i] = binReader.ReadDouble();
                        }
                    }
                    using (MemoryStream stream = new MemoryStream((byte[])key.GetValue("FollowerMul")))
                    {
                        using (BinaryReader binReader = new BinaryReader(stream))
                        {
                            for (int i = 0; i < FollowerMul.Length; i++)
                                FollowerMul[i] = binReader.ReadDouble();
                        }
                    }
                    using (MemoryStream stream = new MemoryStream((byte[])key.GetValue("RiseMaxStep")))
                    {
                        using (BinaryReader binReader = new BinaryReader(stream))
                        {
                            for (int i = 0; i < RiseMaxStep.Length; i++)
                                RiseMaxStep[i] = binReader.ReadDouble();
                        }
                    }
                    using (MemoryStream stream = new MemoryStream((byte[])key.GetValue("FallMaxStep")))
                    {
                        using (BinaryReader binReader = new BinaryReader(stream))
                        {
                            for (int i = 0; i < FallMaxStep.Length; i++)
                                FallMaxStep[i] = binReader.ReadDouble();
                        }
                    }
                    using (MemoryStream stream = new MemoryStream((byte[])key.GetValue("ColourLink")))
                    {
                        using (BinaryReader binReader = new BinaryReader(stream))
                        {
                            for (int i = 0; i < ColourLink.Length; i++)
                                ColourLink[i] = binReader.ReadInt32();
                        }
                    }
                }
                catch { key?.Close(); return; }
            }
            key?.Close();
        }
    }

}
