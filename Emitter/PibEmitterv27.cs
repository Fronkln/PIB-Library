using System;
using Yarhl.IO;

namespace PIBLib
{
    //32 bytes more than v25, 28 bytes at emitter data, a unknown int nearby the beginning
    public class PibEmitterv27 : PibEmitterv25
    {
        internal override void Read(DataReader reader, PibVersion version)
        {
            Flags = reader.ReadUInt32();

            Unknown_0x4 = reader.ReadBytes(8);
            reader.ReadBytes(4); //new
            UnknownCount_0xC = reader.ReadByte();
            Type = reader.ReadByte();
            reader.ReadBytes(2);

            Unknown0x10 = reader.ReadBytes(36);

            int unknownCount1 = reader.ReadInt32();

            UnknownMainData =  reader.ReadBytes(372); //modified

            int data1Size = reader.ReadInt32(); //Includes DDS header

            //Endian swapped section
            reader.Endianness = EndiannessMode.LittleEndian;
            DDSHeader = reader.ReadBytes(128);


            int floatCount = (data1Size - 128) / 4;

            UnknownSection1 = new float[floatCount];

            for (int i = 0; i < floatCount; i++)
                UnknownSection1[i] = reader.ReadSingle();

            reader.Endianness = EndiannessMode.BigEndian;
            //End of endian swapped section

            EmitterType emitterType = GetEmitterType();
            Source = emitterType == EmitterType.Model ? new BaseParticleModel() : new BaseParticleBillboard();

            int textureCount = reader.ReadInt32();

            for (int i = 0; i < textureCount; i++)
                Textures.Add(reader.ReadString(32).Split(new[] { '\0' }, 2)[0]);

            reader.Stream.Position += 8;

            int unknownCount2 = reader.ReadInt32();

            ReadUnknownData1(reader, Type, unknownCount1);
            Source.Read(reader, this, (int)Flags, unknownCount2, (uint)version);
        }

        internal override void Write(DataWriter writer)
        {
            writer.Write(Flags);

            writer.Write(Unknown_0x4);
            writer.Write(0);
            writer.Write(UnknownCount_0xC);
            writer.Write(Type);
            writer.WriteTimes(0, 2);

            writer.Write(Unknown0x10);

            writer.Write(GetUnknownDataCount());
            writer.Write(UnknownMainData);
            writer.Write(896);

            writer.Endianness = EndiannessMode.LittleEndian;
            writer.Write(DDSHeader);

            foreach (float f in UnknownSection1)
                writer.Write(f);

            writer.Endianness = EndiannessMode.BigEndian;

            writer.Write(Textures.Count);

            foreach (string str in Textures)
                writer.Write(str.ToLength(32));

            writer.WriteTimes(0, 8);
            writer.Write(Source.GetDataCount());

            writer.Write(UnknownData1);
            Source.Write(writer);
        }
    }
}
