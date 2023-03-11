using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{
    //Changed:
    //Area that used to be padding after Type seems to be used
    //Massive emitter main data increase (356 > 564)
    //New unknown count (after texture count)
    //Unknown_0x10 (40 > 48)
    public class PibEmitterv43 : PibEmitterv29
    {
        public short TypeUnk;

        public int[] UnkNumbers_TextureTable_V42;

        internal override void Read(DataReader reader, PibVersion version)
        {
            Flags = reader.ReadUInt32();

            Unknown_0x4 = reader.ReadBytes(4);

            reader.ReadBytes(4);

            UnknownCount_0xC = reader.ReadByte();
            Type = reader.ReadByte();
            TypeUnk = reader.ReadInt16();

            Unknown0x10 = reader.ReadBytes(48);

            int unknownCount1 = reader.ReadInt32();

            UnknownMainData = reader.ReadBytes(564);

            int data1Size = reader.ReadInt32(); //Includes DDS header

            DDSHeader = reader.ReadBytes(128);

            int floatCount = (data1Size - 128) / 4;

            UnknownSection1 = new float[floatCount];

            for (int i = 0; i < floatCount; i++)
                UnknownSection1[i] = reader.ReadSingle();


            EmitterType emitterType = GetEmitterType();
            Source = emitterType == EmitterType.Model ? new ParticleModelv29() : new ParticleBillboardv29();

            int textureCount = reader.ReadInt32();
            UnkNumbers_TextureTable_V42 = new int[textureCount];

            for (int i = 0; i < textureCount; i++)
                UnkNumbers_TextureTable_V42[i] = reader.ReadInt32();

            for (int i = 0; i < textureCount; i++)
                Textures.Add(reader.ReadString(32).Split(new[] { '\0' }, 2)[0]);

            reader.Stream.Position += 4;
            int extraTexturesCount = reader.ReadInt32();

            for (int i = 0; i < extraTexturesCount; i++)
                ExtraTextures.Add(reader.ReadString(32));

            int unknownCount2 = reader.ReadInt32();

            ReadUnknownData1(reader, Type, unknownCount1);
            Source.Read(reader, this, (int)Flags, unknownCount2, (uint)version);
        }

        internal override void Write(DataWriter writer)
        {
            writer.Write(Flags);
            writer.Write(Unknown_0x4);
            writer.WriteTimes(0, 4);

            writer.Write(UnknownCount_0xC);
            writer.Write(Type);
            writer.Write(TypeUnk);

            writer.Write(Unknown0x10);

            writer.Write(GetUnknownDataCount());
            writer.Write(UnknownMainData);
            writer.Write(128 + UnknownSection1.Length * 4);
            writer.Write(DDSHeader);

            foreach (float f in UnknownSection1)
                writer.Write(f);

            writer.Write(Textures.Count);

            for (int i = 0; i < Textures.Count; i++)
                writer.Write(UnkNumbers_TextureTable_V42[i]);

            foreach (string str in Textures)
                writer.Write(str.ToLength(32));

            writer.WriteTimes(0, 4);

            writer.Write(ExtraTextures.Count);

            foreach (string str in ExtraTextures)
                writer.Write(str.ToLength(32));

            writer.Write(Source.GetDataCount());

            writer.Write(UnknownData1);
            Source.Write(writer);
        }

        public override EmitterType GetEmitterType()
        {
            byte bit = (byte)(Flags >> 6);

            if ((bit & 1) == 1 || (bit & 0x40) == 1)
                return EmitterType.Billboard;

            return EmitterType.Model;
        }
    }
}
