using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yarhl.IO;

namespace PIBLib
{
    internal static class Extensions
    {
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

        public static RGB32 ReadRGB32(this DataReader reader)
        {
            RGB32 rgb = new RGB32();
            rgb.R = reader.ReadByte();
            rgb.G = reader.ReadByte();
            rgb.B = reader.ReadByte();

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

        public static void Write(this DataWriter writer, RGB32 rgb)
        {
            writer.Write(rgb.R);
            writer.Write(rgb.G);
            writer.Write(rgb.B);
        }

        //Slow, shit, but its good enough for my needs
        public static void Insert(this DataWriter writer, int amount)
        {
            long pos = writer.Stream.Position;

            List<byte> dat = new List<byte>(writer.Stream.ToArray());
            dat.InsertRange((int)writer.Stream.Position, new byte[amount]);

            writer.Stream.Position = 0;
            writer.Write(dat.ToArray());
            writer.Stream.Position = pos;
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
    }
}


namespace System.Reflection
{
    internal static class ReflectionExtensions
    {
        public static void CopyFields(this object source, Object destination)
        {
            // copy base class properties.

            foreach (FieldInfo prop in source.GetType().GetFields())
            {
                FieldInfo prop2 = source.GetType().GetField(prop.Name);
                prop2.SetValue(destination, prop.GetValue(source));
            }
        }
    }
}
