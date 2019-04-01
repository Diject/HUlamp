using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace HUlamp
{
    static class CommandListString
    {

        public static float ToFloat(this string str)
        {
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            float res;
            try
            {
                res = float.Parse(str.Replace(',', '.'), nfi);
            }
            catch
            {
                res = 0;
            }
            return res;
        }

        public static byte ToByte(this string str)
        {
            byte res;
            try
            {
                res = Convert.ToByte(str);
            }
            catch { res = 0; }
            return res;
        }

        public static double ToDouble(this string str)
        {
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            double res;
            try
            {
                res = double.Parse(str.Replace(',', '.'), nfi);
            }
            catch
            {
                res = 0;
            }
            return res;
        }

        public static int ToInt(this string str)
        {
            int res;
            try
            {
                res = Convert.ToInt32(str);
            }
            catch { res = 0; }
            return res;
        }

    }

    static class SendCommandList
    {
        public static byte[] ResetCommand()
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter binWriter = new BinaryWriter(stream);
            binWriter.Write((byte)0x2D);
            binWriter.Write((byte)0x72);
            binWriter.Write((byte)0x73);
            binWriter.Write((byte)0x74);
            return stream.ToArray();

        }

        public static byte[] SetColourCommand(params string[] values)
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter binWriter = new BinaryWriter(stream);
            binWriter.Write((byte)0x2D);
            binWriter.Write((byte)0x2D);
            binWriter.Write((byte)0x63);
            binWriter.Write((byte)0x6C);
            binWriter.Write(values[0].ToFloat());
            binWriter.Write(values[1].ToFloat());
            binWriter.Write(values[2].ToFloat());
            return stream.ToArray();
        }

        public static byte[] SetColourCommand(float r, float g, float b)
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter binWriter = new BinaryWriter(stream);
            binWriter.Write((byte)0x2D);
            binWriter.Write((byte)0x2D);
            binWriter.Write((byte)0x63);
            binWriter.Write((byte)0x6C);
            binWriter.Write(r);
            binWriter.Write(g);
            binWriter.Write(b);
            return stream.ToArray();
        }

        public static byte[] SetBrightnessCommand(params string[] values)
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter binWriter = new BinaryWriter(stream);
            binWriter.Write((byte)0x2D);
            binWriter.Write((byte)0x2D);
            binWriter.Write((byte)0x62);
            binWriter.Write((byte)0x74);
            binWriter.Write(values[0].ToFloat());
            return stream.ToArray();
        }

        public static byte[] SetBrightnessCommand(float brt)
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter binWriter = new BinaryWriter(stream);
            binWriter.Write((byte)0x2D);
            binWriter.Write((byte)0x2D);
            binWriter.Write((byte)0x62);
            binWriter.Write((byte)0x74);
            binWriter.Write(brt);
            return stream.ToArray();
        }

        public static byte[] ChangeBacklightModeCommand(params string[] values)
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter binWriter = new BinaryWriter(stream);
            binWriter.Write((byte)0x2D);
            binWriter.Write((byte)0x2D);
            binWriter.Write((byte)0x6D);
            binWriter.Write((byte)0x64);
            binWriter.Write(values[0].ToByte());
            return stream.ToArray();
        }

        public static byte[] ChangeBacklightModeCommand(byte mode)
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter binWriter = new BinaryWriter(stream);
            binWriter.Write((byte)0x2D);
            binWriter.Write((byte)0x2D);
            binWriter.Write((byte)0x6D);
            binWriter.Write((byte)0x64);
            binWriter.Write(mode);
            return stream.ToArray();
        }

        public static byte[] CustomModeSetupCommand(params string[] values)
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter binWriter = new BinaryWriter(stream);
            binWriter.Write((byte)0x2D);
            binWriter.Write((byte)0x2D);
            binWriter.Write((byte)0x63);
            binWriter.Write((byte)0x73);
            binWriter.Write(values[0].ToByte());
            return stream.ToArray();
        }

        public static byte[] CustomModeDataCommand(params string[] values)
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter binWriter = new BinaryWriter(stream);
            binWriter.Write((byte)0x2D);
            binWriter.Write((byte)0x2D);
            binWriter.Write((byte)0x63);
            binWriter.Write((byte)0x64);
            binWriter.Write(values[0].ToByte());
            binWriter.Write(values[0].ToByte());
            binWriter.Write(values[0].ToByte());
            binWriter.Write(values[0].ToByte());
            binWriter.Write(values[1].ToFloat());
            binWriter.Write(values[2].ToFloat());
            binWriter.Write(values[3].ToFloat());
            binWriter.Write(values[4].ToFloat());
            binWriter.Write(values[5].ToFloat());
            binWriter.Write(values[6].ToFloat());
            binWriter.Write(values[7].ToFloat());
            binWriter.Write(values[8].ToFloat());
            return stream.ToArray();
        }

        public static byte[] CustomModeCopyCommand(params string[] values)
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter binWriter = new BinaryWriter(stream);
            binWriter.Write((byte)0x2D);
            binWriter.Write((byte)0x2D);
            binWriter.Write((byte)0x63);
            binWriter.Write((byte)0x63);
            binWriter.Write(values[0].ToByte());
            binWriter.Write(values[1].ToByte());
            return stream.ToArray();
        }

        public static byte[] SaveCommand(params string[] values)
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter binWriter = new BinaryWriter(stream);
            binWriter.Write((byte)0x2D);
            binWriter.Write((byte)0x2D);
            binWriter.Write((byte)0x73);
            binWriter.Write((byte)0x76);
            if (values[0] != null && values[0] != "" && values[0].ToInt() != 0) binWriter.Write((byte)values[0].ToInt());
            return stream.ToArray();
        }

        public static byte[] CalibrationRunCommand(params string[] values)
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter binWriter = new BinaryWriter(stream);
            binWriter.Write((byte)0x2D);
            binWriter.Write((byte)0x63);
            binWriter.Write((byte)0x61);
            binWriter.Write((byte)0x6C);
            if (values[0] != null && values[0] != "" && values[0].ToInt() != 0) binWriter.Write(values[0].ToFloat());
            return stream.ToArray();
        }

        public static byte[] SetTXControlCommand(params string[] values)
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter binWriter = new BinaryWriter(stream);
            binWriter.Write((byte)0x2D);
            binWriter.Write((byte)0x2D);
            binWriter.Write((byte)0x74);
            binWriter.Write((byte)0x78);
            binWriter.Write(values[0].ToByte());
            return stream.ToArray();
        }

        public static byte[] RequestAltData()
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter binWriter = new BinaryWriter(stream);
            binWriter.Write((byte)0x2D);
            binWriter.Write((byte)0x2D);
            binWriter.Write((byte)0x64);
            binWriter.Write((byte)0x72);
            return stream.ToArray();
        }
    }
}
