using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{
    public class PibEmitterv19 : BaseOOEPibEmitter
    {

        internal override void Read(DataReader reader, PibVersion version)
        {
            Flags = reader.ReadInt32();
            Flags2 = reader.ReadInt32();
            Flags3 = reader.ReadInt32();

            Blend = reader.ReadByte();
            Type = reader.ReadByte();
            //reader.Stream.Position += 1;
            //MetaballBlend = reader.ReadByte();

            reader.Stream.Position += 2;

            UnkRegion1 = reader.ReadVector3();
            AABoxCenter = reader.ReadVector3();
            AABoxExtent = reader.ReadVector3();

            int geoVtxCount = reader.ReadInt32();

            OOEUnkStructure6 = new OOEPibBaseUnkStructure6();
            OOEUnkStructure6.Read(reader);

            Metaball = new PibBaseMetaball();
            Metaball.Read(reader);

            AnimationData = new EmitterBaseAnimationData();
            AnimationData.Read(reader);

            OOEUnkStructure2 = new OOEPibBaseUnkStructure2();
            OOEUnkStructure2.Read(reader);

            PositionOffset = reader.ReadVector3();
            reader.Stream.Position += 4;

            MinSpread = reader.ReadSingle();
            UnkMinSpreadRegVal1 = reader.ReadSingle();
            UnkMinSpreadRegVal2 = reader.ReadSingle();

            MaxSpread = reader.ReadSingle();
            UnkMaxSpreadRegVal1 = reader.ReadSingle();
            UnkMaxSpreadRegVal2 = reader.ReadSingle();

            OEUnknown6 = reader.ReadSingle();
            OEUnknown7 = reader.ReadSingle();
            OEUnknown8 = reader.ReadSingle();
            OEUnknown9 = reader.ReadSingle();
            TextureAnimationSpeed[0] = reader.ReadSingle();
            TextureAnimationSpeed[1] = reader.ReadSingle();
            TextureUnknown[0] = reader.ReadSingle();
            TextureUnknown[1] = reader.ReadSingle();

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

            ReadAnimationCurves(reader, data1Size - 128);

            reader.Endianness = EndiannessMode.BigEndian;
            //End of endian swapped section

            EmitterType emitterType = GetEmitterType();

            if (emitterType == EmitterType.Billboard)
                Source = new BaseParticleBillboard();
            else
                Source = new BaseParticleModel();

            byte textureCount = reader.ReadByte();
            reader.Stream.Position -= 1;

            for (int i = 0; i < textureCount; i++)
            {
                reader.ReadByte();
                Textures.Add(reader.ReadString(31).Split(new[] { '\0' }, 2)[0]);
            }

            reader.Stream.Position += 3;

            int unknownCount2 = reader.ReadInt32();

            ReadUnknownData1(reader, Type, geoVtxCount, version);
            Source.Read(reader, this, (int)Flags, unknownCount2, (uint)version);
        }

        internal override void Write(DataWriter writer, PibVersion version)
        {
            writer.Write(Flags);
            writer.Write(Flags2);
            writer.Write(Flags3);

            writer.Write(Blend);
            writer.Write((byte)Type);

            writer.WriteTimes(0, 2);

            writer.Write(UnkRegion1);
            writer.Write(AABoxCenter);
            writer.Write(AABoxExtent);

            writer.Write(GetUnknownDataCount());

            OOEUnkStructure6.Write(writer);

            Metaball.Write(writer);

            AnimationData.Write(writer);
            OOEUnkStructure2.Write(writer);

            writer.Write(PositionOffset);
            writer.Write(0);

            writer.Write(MinSpread);
            writer.Write(UnkMinSpreadRegVal1);
            writer.Write(UnkMinSpreadRegVal2);
            writer.Write(MaxSpread);
            writer.Write(UnkMaxSpreadRegVal1);
            writer.Write(UnkMaxSpreadRegVal2);

            writer.Write(OEUnknown6);
            writer.Write(OEUnknown7);
            writer.Write(OEUnknown8);
            writer.Write(OEUnknown9);

            foreach (float f in TextureAnimationSpeed)
                writer.Write(f);

            foreach(float f in TextureUnknown)
                writer.Write(f);

            OOEUnkStructure3.Write(writer);
            CommonUnkStructure2.Write(writer);

            writer.Write(OEUnknown16);

            OOEUnkStructure4.Write(writer);
            OOEUnkStructure5.Write(writer);

            //End of main data
            WriteAnimationCurves(writer);

            writer.Write((byte)Textures.Count);

            for(int i = 0; i < Textures.Count; i++)
            {
                writer.Write(Textures[i].ToLength(32), false);
            }
            writer.WriteTimes(0, 2);
            writer.Write(Source.GetDataCount());

            foreach (var chunk in UnknownData1)
                chunk.Write(writer);

            Source.Write(writer, version);
        }

        public override bool IsUseColorCurve()
        {
            return ((EmitterFlag1v19)Flags).HasFlag(EmitterFlag1v19.eFLG_COLOR_ANIM);
        }

        public override bool IsMetaball()
        {
            return ((EmitterFlag1v19)Flags).HasFlag(EmitterFlag1v19.eFLG_METABALL);
        }
    }
}
