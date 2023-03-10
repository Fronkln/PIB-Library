using System;
using Yarhl.IO;

namespace PIBLib
{
    public class PibEmitterv25 : BasePibEmitter
    {
        internal override void Read(DataReader reader, PibVersion version)
        {
            Flags = reader.ReadUInt32();

            Unknown_0x4 = reader.ReadBytes(8);
            UnknownCount_0xC = reader.ReadByte();
            Type = reader.ReadByte();
            reader.ReadBytes(2);

            Unknown0x10 = reader.ReadBytes(36);

            int unknownCount1 = reader.ReadInt32();

            byte[] unkDat2 = reader.ReadBytes(344);

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


        public override EmitterType GetEmitterType()
        {
            byte bit = (byte)(Flags >> 6);

            if ((bit & 0x41) == 1)
                return EmitterType.Billboard;

            return EmitterType.Model;
        }
    }
}
