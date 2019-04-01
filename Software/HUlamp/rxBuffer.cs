using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HUlamp
{
    class RxMainDataArgs
    {
        public RxMainDataArgs(float temp, float current, float voltageData, byte buttonData)
        {
            TemperatureData = temp;
            CurrentData = current;
            ButtonData = buttonData;
            VoltageData = voltageData;
        }

        public float TemperatureData { get; }
        public float CurrentData { get; }
        public float VoltageData { get; }
        public byte ButtonData { get; }

    }

    class RxAltDataArgs
    {
        public byte ETXMode;
        public float NoLoadCurrent;
        public byte LEDState;
        public byte BacklightMode;
        public float BacklightRed;
        public float BacklightGreen;
        public float BacklightBlue;
        public float BacklightBrightness;
        public float BrightnessMultiplier;
    }

    class RxBuffer
    {
        private byte[] buffer;
        private int endIndex;

        

        public RxBuffer(int capacity, DataReadyHandler onMainDataReady = null, AltDataReadyHandler onAltDataReady = null)
        {
            buffer = new byte[capacity];
            endIndex = 0;
            OnMainDataReady += onMainDataReady;
            OnAltDataReady += onAltDataReady;
        }

        public int Count
        {
            get
            {
                return endIndex;
            }
        }

        private bool FindData()
        {
            bool found = false;
            bool altData = false;
            for(; ; ){
                int start = 0, end = 0;
                float currentData, temperatureData, voltageData;
                byte buttonData;
                for (int i = 0; i < buffer.Length - 1; i++)
                {
                    if ((buffer[i] == 0xFA) && (buffer[i + 1] == 0xFE)) start = i + 2;
                    else if ((buffer[i] == 0xFB) && (buffer[i + 1] == 0xFE)) { start = i + 2; altData = true; }
                    if ((buffer[i] == 13) && (buffer[i + 1] == 10)) { end = i - 1; break; }
                }
                if (start < end)
                {
                    using (MemoryStream stream = new MemoryStream(buffer, start, end))
                    {
                        using (BinaryReader binReader = new BinaryReader(stream))
                        {
                            if (!altData && (end - start) >= 12)
                            {
                                currentData = binReader.ReadSingle();
                                temperatureData = binReader.ReadSingle();
                                buttonData = binReader.ReadByte();
                                voltageData = binReader.ReadSingle();
                                OnMainDataReady?.Invoke(this, new RxMainDataArgs(temperatureData, currentData, voltageData, buttonData));
                            }
                            else if (altData && (end - start) >= 26)
                            {
                                RxAltDataArgs args = new RxAltDataArgs();
                                args.ETXMode = binReader.ReadByte();
                                args.NoLoadCurrent = binReader.ReadSingle();
                                args.LEDState = binReader.ReadByte();
                                args.BacklightMode = binReader.ReadByte();
                                args.BacklightRed = binReader.ReadSingle();
                                args.BacklightGreen = binReader.ReadSingle();
                                args.BacklightBlue = binReader.ReadSingle();
                                args.BacklightBrightness = binReader.ReadSingle();
                                args.BrightnessMultiplier = binReader.ReadSingle();
                                OnAltDataReady?.Invoke(this, args);
                            }
                        }
                    }
                    found = true;
                    Array.Copy(buffer, end + 3, buffer, 0, buffer.Length - (end + 3));
                    endIndex -= end + 3;
                }
                else break;
            }
            return found;
        }

        public void Add(byte[] data)
        {
            if (Count + data.Length > buffer.Length)
            {
                endIndex = 0;
            }
            Array.Copy(data, 0, buffer, endIndex, data.Length);
            endIndex += data.Length;
            FindData();
        }

        public byte this[int index]
        {
            get
            {
                if (index >= Count)
                    throw new ArgumentOutOfRangeException();
                return buffer[index % buffer.Length];
            }
        }

        public delegate void DataReadyHandler(object sender, RxMainDataArgs e);
        public delegate void AltDataReadyHandler(object sender, RxAltDataArgs e);
        public event DataReadyHandler OnMainDataReady;
        public event AltDataReadyHandler OnAltDataReady;
    }
}
