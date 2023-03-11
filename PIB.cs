using System;
using System.IO;
using Yarhl.IO;

namespace PIBLib
{
    public class PIB
    {
        public static BasePib Read(string path)
        {
            if (!File.Exists(path))
                return null;

            return Read(File.ReadAllBytes(path));
        }

        public static void Write(BasePib pib, string path)
        {
            DataWriter writer = new DataWriter(new DataStream()) { Endianness = (pib.Version >= PibVersion.Y6 ? EndiannessMode.LittleEndian : EndiannessMode.BigEndian) };
            pib.Write(writer);
            writer.Stream.WriteTo(path);
        }


        public static BasePib Read(byte[] buffer)
        {
            if (buffer == null || buffer.Length < 0)
                return null;

            DataStream readStream = DataStreamFactory.FromArray(buffer, 0, buffer.Length);
            DataReader reader = new DataReader(readStream) { Endianness = EndiannessMode.LittleEndian, DefaultEncoding = System.Text.Encoding.GetEncoding(932) };

            if (reader.ReadString(4) != "PIBX")
                throw new Exception("Not a PIB file.");

            byte unkByte = reader.ReadByte();
            bool isBigEndian = reader.ReadByte() == 1;
            reader.Stream.Position += 2;

            reader.Endianness = (isBigEndian ? EndiannessMode.BigEndian : EndiannessMode.LittleEndian);

            uint version = reader.ReadUInt32();

            BasePib pibFile = GetPibForVersion(version);
            pibFile.Unk = unkByte;
            pibFile.IsBigEndian = isBigEndian;
            pibFile.Version = (PibVersion)version;

            reader.ReadBytes(4);

            pibFile.Read(reader);

            return pibFile;
        }


        private static BasePib GetPibForVersion(uint version)
        {
            switch (version)
            {
                default:
                    throw new Exception("Unknown PIB Version: " + version);
                case 8:
                    return new Pib8();
                case 19:
                    return new Pib19();
                case 21:
                    return new Pib21();
                case 25:
                    return new Pib25();
                case 27:
                    return new Pib27();
                case 29:
                    return new Pib29();
                case 43:
                    return new Pib43();
                case 45:
                    return new Pib45();
            }
        }
    }
}
