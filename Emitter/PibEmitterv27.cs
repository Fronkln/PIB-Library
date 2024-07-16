﻿using System;
using Yarhl.IO;

namespace PIBLib
{
    public class PibEmitterv27 : PibEmitterv25
    {
        public int UnknownV27 = 0;

        public OEPibUnkStructure1v27 OEUnkStructure1 = new OEPibUnkStructure1v27();

        internal override void Read(DataReader reader, PibVersion version)
        {
            Flags = reader.ReadInt32();
            Flags2 = reader.ReadInt32();
            Flags3 = reader.ReadInt32();

            UnknownV27 = reader.ReadInt32();
            Blend = reader.ReadByte();
            Type = reader.ReadByte();
            reader.Stream.Position += 1;
            MetaballBlend = reader.ReadByte();

            UnkReg1 = new float[] { reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle() };

            AABoxCenter = reader.ReadVector3();
            AABoxExtent = reader.ReadVector3();

            int geoVtxCount = reader.ReadInt32();

            OOEUnkStructure6 = new OOEPibBaseUnkStructure6();
            OOEUnkStructure6.Read(reader);

            Metaball = new PibBaseMetaball();
            Metaball.Read(reader);

            DirectivityH = reader.ReadSingle();
            DirectivityV = reader.ReadSingle();
            DirectivityPower = reader.ReadSingle();

            Culling = reader.ReadUInt32();

            OEUnkStructure1 = new OEPibUnkStructure1v27();
            OEUnkStructure1.Read(reader);

            AnimationData = new EmitterBaseAnimationData();
            AnimationData.Read(reader);

            OOEUnkStructure2 = new OOEPibBaseUnkStructure2();
            OOEUnkStructure2.Read(reader);

            MinSpread = reader.ReadVector3();
            MaxSpread = reader.ReadVector3();

            OEUnknown7 = reader.ReadSingle();
            OEUnknown8 = reader.ReadSingle();
            OEUnknown9 = reader.ReadSingle();
            OEUnknown10 = reader.ReadSingle();
            OEUnknown11 = reader.ReadSingle();
            OEUnknown12 = reader.ReadSingle();

            OOEUnkStructure3 = new OOEPibBaseUnkStructure3();
            OOEUnkStructure3.Read(reader);

            CommonUnkStructure2 = new PibBaseCommonUnkStructure2();
            CommonUnkStructure2.Read(reader);

            OEUnknown16 = reader.ReadSingle();

            OOEUnkStructure4 = new OOEPibBaseUnkStructure4();
            OOEUnkStructure4.Read(reader);

            OOEUnkStructure5 = new OOEPibBaseUnkStructure5();
            OOEUnkStructure5.Read(reader);

            //End of main data
            int data1Size = reader.ReadInt32(); //Includes DDS header

            //Endian swapped section
            reader.Endianness = EndiannessMode.LittleEndian;
            DDSHeader.Read(reader);

            int floatCount = (data1Size - 128) / 4;
            int chunkCount = (data1Size - 128) / 256;

            ReadUnknownSection1(reader, data1Size - 128);

            reader.Endianness = EndiannessMode.BigEndian;
            //End of endian swapped section

            EmitterType emitterType = GetEmitterType();
            Source = emitterType == EmitterType.Model ? new BaseParticleModel() : new BaseParticleBillboard();

            int textureCount = reader.ReadInt32();

            for (int i = 0; i < textureCount; i++)
            {
                Textures.Add(reader.ReadString(32).Split(new[] { '\0' }, 2)[0]);
            }

            int extraTexCount = reader.ReadInt32();

            for (int i = 0; i < extraTexCount; i++)
                UnknownExtraTextures.Add(reader.ReadString(32));

            reader.Stream.Position += 4;

            int unknownCount2 = reader.ReadInt32();

            ReadUnknownData1(reader, Type, geoVtxCount, version);
            Source.Read(reader, this, (int)Flags, unknownCount2, (uint)version);
        }

        internal override void Write(DataWriter writer, PibVersion version)
        {
            writer.Write(Flags);
            writer.Write(Flags2);
            writer.Write(Flags3);

            writer.Write(UnknownV27);
            writer.Write(Blend);
            writer.Write((byte)Type);
            writer.WriteTimes(0, 1);
            writer.Write(MetaballBlend);

            for (int i = 0; i < 3; i++)
                writer.Write(UnkReg1[i]);

            writer.Write(AABoxCenter);
            writer.Write(AABoxExtent);

            writer.Write(GetUnknownDataCount());

            OOEUnkStructure6.Write(writer);

            Metaball.Write(writer);

            writer.Write(DirectivityH);
            writer.Write(DirectivityV);
            writer.Write(DirectivityPower);

            writer.Write(Culling);

            OEUnkStructure1.Write(writer);
            AnimationData.Write(writer);
            OOEUnkStructure2.Write(writer);

            writer.Write(MinSpread);
            writer.Write(MaxSpread);

            writer.Write(OEUnknown7);
            writer.Write(OEUnknown8);
            writer.Write(OEUnknown9);
            writer.Write(OEUnknown10);
            writer.Write(OEUnknown11);
            writer.Write(OEUnknown12);

            OOEUnkStructure3.Write(writer);
            CommonUnkStructure2.Write(writer);

            writer.Write(OEUnknown16);

            OOEUnkStructure4.Write(writer);
            OOEUnkStructure5.Write(writer);

            //End of main data
            WriteUnknownSection1(writer);

            writer.Write(Textures.Count);

            for (int i = 0; i < Textures.Count; i++)
                writer.Write(Textures[i].ToLength(32), false);

            writer.Write(UnknownExtraTextures.Count);

            foreach (string str in UnknownExtraTextures)
                writer.Write(str.ToLength(32), false);

            writer.WriteTimes(0, 4);
            writer.Write(Source.GetDataCount());

            foreach (var chunk in UnknownData1)
                chunk.Write(writer);

            Source.Write(writer, version);
        }
    }
}
