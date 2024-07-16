using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using Yarhl.IO;

namespace PIBLib
{
    internal static class Extensions
    {
        internal static bool HasFlag(this int val, int flag)
        {
            return (val & flag) != 0;
        }

        internal static int SetFlag(this int val, int flag)
        {
           return val |= flag;
        }

        internal static int RemoveFlag(this int val, int flag)
        {
           return val &= ~flag;
        }

        internal static IEnumerable<Enum> GetFlags(this Enum e)
        {
            return Enum.GetValues(e.GetType()).Cast<Enum>().Where(e.HasFlag);
        }

        public static void Overwrite<T>(this List<T> @this, int startIndex, T[] dat)
        {
            int curIdx = 0;

            for (int i = startIndex; i < startIndex + dat.Length; i++)
            {
                @this[i] = dat[curIdx];
                curIdx++;
            }
        }

        public static Vector2 ReadVector2(this DataReader reader)
        {
            return new Vector2(reader.ReadSingle(), reader.ReadSingle());
        }

        public static Vector3 ReadVector3(this DataReader reader)
        {
            return new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
        }

        public static Vector4 ReadVector4(this DataReader reader)
        {
            return new Vector4(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
        }

        public static Matrix4x4 ReadMatrix4x4(this DataReader reader)
        {
            Matrix4x4 mtx = new Matrix4x4();
            mtx.VM0 = reader.ReadVector4();
            mtx.VM1 = reader.ReadVector4();
            mtx.VM2 = reader.ReadVector4();
            mtx.VM3 = reader.ReadVector4();

            return mtx;
        }

        public static RGB ReadRGB(this DataReader reader)
        {
            RGB rgb = new RGB();
            rgb.R = reader.ReadByte();
            rgb.G = reader.ReadByte();
            rgb.B = reader.ReadByte();

            return rgb;
        }

        public static RGBA ReadRGBA(this DataReader reader)
        {
            RGBA rgb = new RGBA();

            rgb.R = reader.ReadByte();
            rgb.G = reader.ReadByte();
            rgb.B = reader.ReadByte();
            rgb.A = reader.ReadByte();

            return rgb;
        }

        public static void Write(this DataWriter writer, Vector2 vec)
        {
            writer.Write(vec.x);
            writer.Write(vec.y);
        }

        public static void Write(this DataWriter writer, Vector3 vec)
        {
            writer.Write(vec.x);
            writer.Write(vec.y);
            writer.Write(vec.z);
        }

        public static void Write(this DataWriter writer, Vector4 vec)
        {
            writer.Write(vec.x);
            writer.Write(vec.y);
            writer.Write(vec.z);
            writer.Write(vec.w);
        }

        public static void Write(this DataWriter writer, Matrix4x4 mtx)
        {
            writer.Write(mtx.VM0);
            writer.Write(mtx.VM1);
            writer.Write(mtx.VM2);
            writer.Write(mtx.VM3);
        }

        public static void Write(this DataWriter writer, RGB rgb)
        {
            writer.Write(rgb.R);
            writer.Write(rgb.G);
            writer.Write(rgb.B);
        }

        public static void Write(this DataWriter writer, RGBA rgb)
        {
            writer.Write(rgb.R);
            writer.Write(rgb.G);
            writer.Write(rgb.B);
            writer.Write(rgb.A);
        }

        public static void FlipEndian(this DataReader reader)
        {
            if (reader.Endianness == EndiannessMode.LittleEndian)
                reader.Endianness = EndiannessMode.BigEndian;
            else
                reader.Endianness = EndiannessMode.LittleEndian;
        }

        /// <summary>
        /// Has to be divisible by 4
        /// </summary>
        /// <param name="writer"></param>
        public static void EndianSwap(this DataWriter writer)
        {
            EndiannessMode endian = writer.Endianness == EndiannessMode.LittleEndian ? EndiannessMode.BigEndian : EndiannessMode.BigEndian;

            DataReader reader = new DataReader(writer.Stream);
            reader.Endianness = writer.Endianness;
            reader.Stream.Position = 0;

            writer.Endianness = endian;
            
            for(int i = 0; i < reader.Stream.Length / 4; i++)
            {
                int val = reader.ReadInt32();
                reader.Stream.Position -= 4;
                writer.Write(val);
            }
        }

        public static string ToLength(this string self, int length)
        {
            if (self == null)
                return null;

            if (self.Length == length)
                return self;

            if (self.Length > length)
                return self.Substring(0, length);


            StringBuilder str = new StringBuilder();
            str.Append(self);

            while (str.Length != length)
                str.Append('\0');

            return str.ToString();
        }

        public static byte[] ToArray(this DataStream stream)
        {
            long pos = stream.Position;
            stream.Position = 0;

            byte[] buf = new byte[stream.Length];
            stream.Read(buf, 0, buf.Length);

            stream.Position = pos;
            
            return buf;
        }


        public static byte[] ReadPart(this DataStream stream, int count)
        {
            long pos = stream.Position;
            byte[] buf = new byte[stream.Length];
            stream.Read(buf, 0, count);

            stream.Position = pos;

            return buf;
        }

        public static byte[] ReadPart(this DataStream stream, int start, int count)
        {
            long pos = stream.Position;
            stream.Position = start;

            byte[] buf = new byte[stream.Length];
            stream.Read(buf, 0, count);

            stream.Position = pos;

            return buf;
        }
    }
}


namespace System.Reflection
{
    internal static class ReflectionExtensions
    {
        //make this better, too much argumentexception
        public static void CopyFields(this object source, Object destination)
        {
            // copy base class properties.

            foreach (FieldInfo prop in source.GetType().GetFields())
            {
                try
                {
                    FieldInfo prop2 = source.GetType().GetField(prop.Name);
                    prop2.SetValue(destination, prop.GetValue(source));
                }
                catch { continue; }
            }
        }
    }
}
