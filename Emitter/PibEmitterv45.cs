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
        public PibEmitterv45()
        {
            VAT = new DEPibVATv45();
        }

        internal override void Read(DataReader reader, PibVersion version)
        {
            Flags = reader.ReadInt32();
            Flags2 = reader.ReadInt32();
            Flags3 = reader.ReadInt32();

            Blend = reader.ReadByte();
            GeoVertex = reader.ReadByte();
            InsVertex = reader.ReadByte();
            MetaballBlend = reader.ReadByte();

            Light = reader.Read<DEPibLightModule>();

            AABoxCenter = reader.ReadVector3();
            AABoxExtent = reader.ReadVector3();

            int geoVertexCount = reader.ReadInt32();

            Blur = reader.Read<DEPibEmitterBlurModule>();
            ShadowRate = reader.ReadSingle();

            Metaball = new DEPibMetaballv43();
            Metaball.Read(reader);

            DirectivityH = reader.ReadSingle();
            DirectivityV = reader.ReadSingle();
            DirectivityPower = reader.ReadSingle();

            Culling = reader.ReadUInt32();
            LightScale = reader.ReadSingle();
            LightRatio = reader.ReadSingle();
            Glare = reader.ReadSingle();

            CollisionRestitution = reader.ReadSingle();
            CollisionFriction = reader.ReadSingle();

            VecScale = reader.ReadSingle();
            NormalScale = reader.ReadSingle();

            Track = new DEPibTrackv43();
            Track.Read(reader);

            AlphaGradation = reader.ReadSingle();

            VF = new DEPibVFModule();
            VF.Read(reader);

            VAT = new DEPibVATv45();
            VAT.Read(reader);

            TickWait = reader.ReadUInt32();

            LightUi1 = new float[]
            {
                reader.ReadSingle(),
                reader.ReadSingle(),
                reader.ReadSingle(),
                reader.ReadSingle()
            };

            reader.Stream.Position += 8;

            AnimationData = new EmitterAnimationDataDE();
            AnimationData.Read(reader);

            UnkStructure2 = new OOEPibBaseUnkStructure2();
            UnkStructure2.Read(reader);

            v45Unk1 = new DEPibv45UnkStructure1();
            v45Unk1.Read(reader);

            PositionOffset = reader.ReadVector3();
            ParticleCount2 = reader.ReadInt32();

            MinSpread = reader.ReadSingle();
            UnkMinSpreadRegVal1 = reader.ReadSingle();
            UnkMinSpreadRegVal2 = reader.ReadSingle();

            MaxSpread = reader.ReadSingle();
            UnkMaxSpreadRegVal1 = reader.ReadSingle();
            Gravity = reader.ReadSingle();

            UnkVal1 = reader.ReadSingle();

            CommonUnkStructure2 = new DEPibCommonUnkStructure2();
            CommonUnkStructure2.Read(reader);

            UnkVal2 = reader.ReadSingle();
            UnkVal3 = reader.ReadSingle();
            UnkVal4 = reader.ReadSingle();
            UnkVal5 = reader.ReadSingle();

            UnkStructure5 = new DEPibv43UnkStructure1();
            UnkStructure5.Read(reader);

            int data1Size = reader.ReadInt32(); //Includes DDS header
            DDSHeader.Read(reader);

            int floatCount = (data1Size - 128) / 4;
            int chunkCount = (data1Size - 128) / 256;

            ReadAnimationCurves(reader, data1Size - 128);

            EmitterType emitterType = GetEmitterType();

            if (emitterType == EmitterType.Billboard)
                Source = new ParticleBillboardv29();
            else
                Source = new ParticleModelv29();

            int textureCount = reader.ReadInt32();
            TextureShaderIndices = new int[textureCount];

            for (int i = 0; i < textureCount; i++)
                TextureShaderIndices[i] = reader.ReadInt32();

            for (int i = 0; i < textureCount; i++)
                Textures.Add(reader.ReadString(32).Split(new[] { '\0' }, 2)[0]);

            int extraTexturesCount = reader.ReadInt32();

            for (int i = 0; i < extraTexturesCount; i++)
                ExtraTextures.Add(reader.ReadString(36));

            reader.Stream.Position += 4;

            int unknownCount2 = reader.ReadInt32();

            ReadUnknownData1(reader, GeoVertex, geoVertexCount, version);
            Source.Read(reader, this, (int)Flags, unknownCount2, (uint)version);

            //DE 1.0: UV size on Geo VTX Chunk
            UV.UVSize[0].x = UnknownData1[2].Data[3];
            UV.UVSize[0].y = UnknownData1[2].Data[4];
            UV.UVSize[1].x = UnknownData1[2].Data[5];
            UV.UVSize[1].y = UnknownData1[2].Data[6];
            UV.UVSize[2].x = UnknownData1[2].Data[7];
            UV.UVSize[2].y = UnknownData1[2].Data[8];
            UV.UVSize[3].x = 1;
            UV.UVSize[3].y = 1;
        }

        internal override void Write(DataWriter writer, PibVersion version)
        {
            writer.Write(Flags);
            writer.Write(Flags2);
            writer.Write(Flags3);

            writer.Write(Blend);
            writer.Write(GeoVertex);
            writer.Write(InsVertex);
            writer.Write(MetaballBlend);

            writer.WriteOfType(Light);

            writer.Write(AABoxCenter);
            writer.Write(AABoxExtent);

            writer.Write(GetUnknownDataCount());

            writer.WriteOfType(Blur);

            writer.Write(ShadowRate);

            Metaball.Write(writer);

            writer.Write(DirectivityH);
            writer.Write(DirectivityV);
            writer.Write(DirectivityPower);

            writer.Write(Culling);

            writer.Write(LightScale);
            writer.Write(LightRatio);

            writer.Write(Glare);

            writer.Write(CollisionRestitution);
            writer.Write(CollisionFriction);

            writer.Write(VecScale);
            writer.Write(NormalScale);

            Track.Write(writer);

            writer.Write(AlphaGradation);

            VF.Write(writer);
            VAT.Write(writer);

            writer.Write(TickWait);

            for (int i = 0; i < 4; i++)
                writer.Write(LightUi1[i]);;

            writer.WriteTimes(0, 8);

            AnimationData.Write(writer);
            UnkStructure2.Write(writer);
            v45Unk1.Write(writer);

            writer.Write(PositionOffset);
            writer.Write(ParticleCount2);

            writer.Write(MinSpread);
            writer.Write(UnkMinSpreadRegVal1);
            writer.Write(UnkMinSpreadRegVal2);
            writer.Write(MaxSpread);
            writer.Write(UnkMaxSpreadRegVal1);
            writer.Write(Gravity);

            writer.Write(UnkVal1);

            CommonUnkStructure2.Write(writer);

            writer.Write(UnkVal2);
            writer.Write(UnkVal3);
            writer.Write(UnkVal4);
            writer.Write(UnkVal5);

            UnkStructure5.Write(writer);

            WriteAnimationCurves(writer);

            writer.Write(Textures.Count);

            for (int i = 0; i < Textures.Count; i++)
                writer.Write(TextureShaderIndices[i]);

            foreach (string str in Textures)
                writer.Write(str.ToLength(32));

            writer.Write(ExtraTextures.Count);

            foreach (string str in ExtraTextures)
                writer.Write(str.ToLength(36));

            writer.WriteTimes(0, 4);

            writer.Write(Source.GetDataCount());

            foreach (var chunk in UnknownData1)
                chunk.Write(writer);

            Source.Write(writer, version);
        }

        public override bool IsUseColorCurve()
        {
            return ((EmitterFlag1v45)Flags).HasFlag(EmitterFlag1v45.eFLG_COLOR_ANIM);
        }

        public override bool IsMetaball()
        {
            return ((EmitterFlag1v45)Flags).HasFlag(EmitterFlag1v45.eFLG_METABALL);
        }
    }
}
