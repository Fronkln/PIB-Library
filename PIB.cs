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

            return Read(File.ReadAllBytes(path), Path.GetFileNameWithoutExtension(path));
        }

        public static void Write(BasePib pib, string path)
        {
            DataWriter writer = new DataWriter(new DataStream()) { Endianness = (pib.Version >= PibVersion.Y6 ? EndiannessMode.LittleEndian : EndiannessMode.BigEndian) };

            writer.Write("PIBX", false);
            writer.Write((pib.Version < PibVersion.Y6 || pib.Version == PibVersion.Kenzan) ? 0x2010000 : 0x2000000);
            writer.Write((uint)pib.Version);
            writer.WriteTimes(0, 4);

            pib.Write(writer);
            writer.Stream.WriteTo(path);
        }


        public static BasePib Read(byte[] buffer, string name = null)
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
            pibFile.Name = name;

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

                //case 8:
                //return new Pib8();
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
                case 52:
                    return new Pib52();
                case 58:
                    return new Pib58();
            }
        }

        public static bool CanConvert(PibVersion input, PibVersion target)
        {
            switch(input)
            {
                case PibVersion.Y3:
                    return target == PibVersion.Y5;
                case PibVersion.Y5:
                    return target == PibVersion.Y3;
                case PibVersion.Y6:
                    return target == PibVersion.Y0 || target == PibVersion.Ishin || target == PibVersion.Y5 || target == PibVersion.Y3;
                case PibVersion.YK2:
                    return target == PibVersion.Y6 || target == PibVersion.Y0 || target == PibVersion.Ishin || target == PibVersion.Y5 || target == PibVersion.Y3;
            }

            return false;
        }

        public static BasePib Convert(BasePib source, PibVersion target)
        {
            BasePib result = null;

            if (source.Version == target)
                return source;

            switch (source.Version)
            {
                default:
                    return source;
                case PibVersion.Kenzan:
                    //   result = ConvertFromKenzan(source, target);
                    break;
                case PibVersion.Y3:
                    result = ConvertFromY3(source, target);
                    break;
                case PibVersion.Y5:
                    result = ConvertFromY5(source, target);
                    break;
                case PibVersion.Ishin:
                    result = ConvertFromIshin(source, target);
                    break;
                case PibVersion.Y0:
                    result = ConvertFromY0(source, target);
                    break;
                case PibVersion.Y6:
                    result = ConvertFromY6(source, target);
                    break;
                case PibVersion.YK2:
                    result = ConvertFromYK2(source, target);
                    break;
                case PibVersion.JE:
                    result = ConvertFromJE(source, target);
                    break;
                case PibVersion.YLAD:
                    result = ConvertFromYLAD(source, target);
                    break;
                case PibVersion.LJ:
                    result = ConvertFromLJ(source, target);
                    break;
            }


            if (result != null)
                result.Version = target;

            if (result == null)
                result = source;

            result.Name = source.Name;
            return result;
        }


        /*
        private static BasePib ConvertFromKenzan(BasePib basePib, PibVersion outputVersion)
        {
            Pib8 pib = (Pib8)basePib;

            switch (outputVersion)
            {
                default:
                    return null;
                case PibVersion.Y3:
                    return pib.ToV19();
                case PibVersion.Y5:
                    return pib.ToV19().ToV21();
                case PibVersion.Ishin:
                    return pib.ToV19().ToV21().ToV25();
                case PibVersion.Y0:
                    return pib.ToV19().ToV21().ToV25().ToV27();
                case PibVersion.Y6:
                    return pib.ToV19().ToV21().ToV25().ToV27().ToV29();
            }
        }
        */

        private static BasePib ConvertFromY3(BasePib basePib, PibVersion outputVersion)
        {
            Pib19 pib = (Pib19)basePib;

            switch (outputVersion)
            {
                default:
                    return null;
                case PibVersion.Y5:
                    return pib.ToV21();
                case PibVersion.Ishin:
                    return pib.ToV21().ToV25();
                case PibVersion.Y0:
                    return pib.ToV21().ToV25().ToV27();
                    /*
               case PibVersion.Y6:
                   return pib.ToV21().ToV25().ToV27().ToV29();
                   */
            }
        }

        private static BasePib ConvertFromY5(BasePib basePib, PibVersion outputVersion)
        {
            Pib21 pib = (Pib21)basePib;

            switch (outputVersion)
            {
                default:
                    return null;

                case PibVersion.Y3:
                    return pib.ToV19();
                case PibVersion.Ishin:
                    return pib.ToV25();
                case PibVersion.Y0:
                    return pib.ToV25().ToV27();
                    // case PibVersion.Y6:
                    //return pib.ToV25().ToV27().ToV29();
            }
        }


        private static BasePib ConvertFromIshin(BasePib basePib, PibVersion outputVersion)
        {
            Pib25 pib = (Pib25)basePib;

            switch (outputVersion)
            {
                default:
                    return null;
                case PibVersion.Y3:
                    return pib.ToV21().ToV19();
                case PibVersion.Y5:
                    return pib.ToV21();
                    // case PibVersion.Y0:
                    //return pib.ToV27();
                    //case PibVersion.Y6:
                    //return pib.ToV27().ToV29();
            }
        }
        private static BasePib ConvertFromY0(BasePib basePib, PibVersion outputVersion)
        {
            Pib27 pib = (Pib27)basePib;

            switch (outputVersion)
            {
                default:
                    return null;
                case PibVersion.Y3:
                    return pib.ToV25().ToV21().ToV19();
                case PibVersion.Y5:
                    return pib.ToV25().ToV21();
                case PibVersion.Ishin:
                    return pib.ToV25();
                    // case PibVersion.Y6:
                    //  return pib.ToV29();
            }
        }

        private static BasePib ConvertFromY6(BasePib basePib, PibVersion outputVersion)
        {
            Pib29 pib = (Pib29)basePib;

            switch (outputVersion)
            {
                default:
                    return null;
                //case PibVersion.Y3:
                //  return pib.ToV27().ToV25().ToV21().ToV19();
                // case PibVersion.Y5:
                //   return pib.ToV27().ToV25().ToV21();
                //  case PibVersion.Ishin:
                //  return pib.ToV27().ToV25();
                case PibVersion.Y0:
                    return pib.ToV27();

            }
        }

        private static BasePib ConvertFromYK2(BasePib basePib, PibVersion outputVersion)
        {
            Pib43 pib = (Pib43)basePib;

            switch (outputVersion)
            {
                default:
                    return null;

                case PibVersion.Y0:
                    return pib.ToV29().ToV27();
                case PibVersion.Y6:
                    return pib.ToV29();
                case PibVersion.JE:
                    return pib.ToV45();
                case PibVersion.YLAD:
                    return pib.ToV45().ToV52();
            }
        }

        private static BasePib ConvertFromJE(BasePib basePib, PibVersion outputVersion)
        {
            Pib45 pib = (Pib45)basePib;

            switch (outputVersion)
            {
                default:
                    return null;

                case PibVersion.Y0:
                    return pib.ToV43().ToV29().ToV27();
                case PibVersion.Y6:
                    return pib.ToV43().ToV29();
                case PibVersion.YK2:
                    return pib.ToV43();
                case PibVersion.YLAD:
                    return pib.ToV52();
            }
        }

        private static BasePib ConvertFromYLAD(BasePib basePib, PibVersion outputVersion)
        {
            Pib52 pib = (Pib52)basePib;

            switch (outputVersion)
            {
                default:
                    return null;

                case PibVersion.Y0:
                    return pib.ToV45().ToV43().ToV29().ToV27();
                case PibVersion.Y6:
                    return pib.ToV45().ToV43().ToV29();
                case PibVersion.YK2:
                    return pib.ToV45().ToV43();
                case PibVersion.JE:
                    return pib.ToV45();
                case PibVersion.LJ:
                    return pib.ToV58();
            }
        }

        private static BasePib ConvertFromLJ(BasePib basePib, PibVersion outputVersion)
        {
            Pib58 pib = (Pib58)basePib;

            switch (outputVersion)
            {
                default:
                    return null;

                case PibVersion.Y0:
                    return pib.ToV52().ToV45().ToV43().ToV29().ToV27();
                case PibVersion.Y6:
                    return pib.ToV52().ToV45().ToV43().ToV29();
                case PibVersion.YK2:
                    return pib.ToV52().ToV45().ToV43();
                case PibVersion.JE:
                    return pib.ToV52().ToV45();
                case PibVersion.YLAD:
                    return pib.ToV52();
            }
        }
    }
}
