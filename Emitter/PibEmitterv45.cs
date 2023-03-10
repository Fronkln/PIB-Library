using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{
    //Changed:
    //Main data increase (564 > 596)
    public class PibEmitterv45 : PibEmitterv43
    {
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

            UnknownMainData = reader.ReadBytes(596);

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
    }
}
