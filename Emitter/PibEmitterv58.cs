using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{
    public class PibEmitterv58 : PibEmitterv52
    {
        public float UnkFlt_58_1 = 1;
        public float UnkFlt_58_2 = 1;
        public float UnkFlt_58_3 = 1;

        public float UnkFlt_58_4 = 0;

        public float UnkFlt_58_5 = 0;
        public float UnkFlt_58_6 = 0;

        public Vector2 UnkVec_58_1 = Vector2.zero;

        internal override void Read(DataReader reader, PibVersion version)
        {
            Flags = reader.ReadUInt64();
            Flags2 = reader.ReadUInt64();
            Flags3 = reader.ReadUInt64();

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

            UnkFlt_58_1 = reader.ReadSingle();
            UnkFlt_58_2 = reader.ReadSingle();
            UnkFlt_58_3 = reader.ReadSingle();
            CollisionRestitution = reader.ReadSingle();
            CollisionFriction = reader.ReadSingle();

            VecScale = reader.ReadSingle();
            NormalScale = reader.ReadSingle();

            Track = new DEPibTrackv43();
            Track.Read(reader);

            AlphaGradation = reader.ReadSingle();

            VF = new DEPibVFModule();
            VF.Read(reader);

            reader.ReadBytes(4);
            UnkFlt_58_4 = reader.ReadSingle();

            VAT = new DEPibVATv45();
            VAT.Read(reader);

            TickWait = reader.ReadUInt32();
            LightUi1 = new float[4]
            {
                reader.ReadSingle(),
                reader.ReadSingle(),
                reader.ReadSingle(),
                reader.ReadSingle()
            };
            LightUi2 = new float[4]
            {
                reader.ReadSingle(),
                reader.ReadSingle(),
                reader.ReadSingle(),
                reader.ReadSingle()
            };

            UnkFlt_58_5 = reader.ReadSingle();
            UnkFlt_58_6 = reader.ReadSingle();

            TexA2Ratio = reader.ReadSingle();
            TexB1Ratio = reader.ReadSingle();

            LtEmissivePower = reader.ReadSingle();
            LtEmissivePowerOfs = reader.ReadSingle();
            GatherPower = reader.ReadSingle();
            ModelOffset = reader.ReadVector3();

            UV = new DEPibUV();
            UV.Read(reader);

            RimBlendCurve = reader.ReadSingle();
            reader.Stream.Position += 20;

            UnkVec_58_1 = reader.ReadVector2();

            AnimationData = new EmitterAnimationDataDE();
            AnimationData.Read(reader);

            UnkStructure2 = new OOEPibBaseUnkStructure2();
            UnkStructure2.Read(reader);

            MinSpread = reader.ReadSingle();
            UnkMinSpreadRegVal1 = reader.ReadSingle();
            UnkMinSpreadRegVal2 = reader.ReadSingle();

            MaxSpread = reader.ReadSingle();
            UnkMaxSpreadRegVal1 = reader.ReadSingle();
            Gravity = reader.ReadSingle();

            UnkVal1 = reader.ReadSingle();

            v45Unk1 = new DEPibv52UnkStructure1();
            v45Unk1.Read(reader);

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
            ReadTextureImports(reader, textureCount);

            int unknownCount2 = reader.ReadInt32();

            ReadUnknownData1(reader, GeoVertex, geoVertexCount, version);
            Source.Read(reader, this, (int)Flags, unknownCount2, (uint)version);
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

            writer.Write(UnkFlt_58_1);
            writer.Write(UnkFlt_58_2);
            writer.Write(UnkFlt_58_3);
            writer.Write(CollisionRestitution);
            writer.Write(CollisionFriction);

            writer.Write(VecScale);
            writer.Write(NormalScale);

            Track.Write(writer);

            writer.Write(AlphaGradation);

            VF.Write(writer);

            writer.WriteTimes(0, 4);
            writer.Write(UnkFlt_58_4);

            VAT.Write(writer);

            writer.Write(TickWait);

            foreach (float f in LightUi1)
                writer.Write(f);

            foreach (float f in LightUi2)
                writer.Write(f);

            writer.Write(UnkFlt_58_5);
            writer.Write(UnkFlt_58_6);

            writer.Write(TexA2Ratio);
            writer.Write(TexB1Ratio);

            writer.Write(LtEmissivePower);
            writer.Write(LtEmissivePowerOfs);
            writer.Write(GatherPower);
            writer.Write(ModelOffset);

            UV.Write(writer);

            writer.Write(RimBlendCurve);

            writer.WriteTimes(0, 20);
            
            writer.Write(UnkVec_58_1);

            AnimationData.Write(writer);
            UnkStructure2.Write(writer);

            writer.Write(MinSpread);
            writer.Write(UnkMinSpreadRegVal1);
            writer.Write(UnkMinSpreadRegVal2);
            writer.Write(MaxSpread);
            writer.Write(UnkMaxSpreadRegVal1);
            writer.Write(Gravity);

            writer.Write(UnkVal1);

            v45Unk1.Write(writer);
            CommonUnkStructure2.Write(writer);

            writer.Write(UnkVal2);
            writer.Write(UnkVal3);
            writer.Write(UnkVal4);
            writer.Write(UnkVal5);

            UnkStructure5.Write(writer);

            WriteAnimationCurves(writer);

            WriteTextureImports(writer, version);

            writer.Write(Source.GetDataCount());

            foreach (var chunk in UnknownData1)
                chunk.Write(writer);

            Source.Write(writer, version);
        }

        internal protected override void ReadTextureImports(DataReader reader, int textureCount)
        {
            UnkNumbers_TextureTable_V42 = new int[textureCount];

            for (int i = 0; i < textureCount; i++)
                UnkNumbers_TextureTable_V42[i] = reader.ReadInt32();

            for (int i = 0; i < textureCount; i++)
                Textures.Add(reader.ReadString(32).Split(new[] { '\0' }, 2)[0]);

            reader.Stream.Position += 4;

            for (int i = 0; i < textureCount; i++)
            {
                TextureImportInfo inf = new TextureImportInfo();
                inf.Data = reader.ReadBytes(32);
                int resourceCount = reader.ReadInt32();


                for (int j = 0; j < resourceCount; j++)
                {
                    TextureImportResource res = new TextureImportResource();
                    res.ID = reader.ReadInt32();
                    res.Name = reader.ReadString(36);

                    inf.Resources.Add(res);
                }

                TextureImports.Add(inf);
            }

            reader.Stream.Position += 4;
        }

        internal protected override void WriteTextureImports(DataWriter writer, PibVersion version)
        {
            writer.Write(Textures.Count);

            for (int i = 0; i < Textures.Count; i++)
                writer.Write(UnkNumbers_TextureTable_V42[i]);

            foreach (string str in Textures)
                writer.Write(str.ToLength(32));

            writer.Write(0);

            foreach (TextureImportInfo inf in TextureImports)
            {
                writer.Write(inf.Data);
                writer.Write(inf.Resources.Count);

                foreach (TextureImportResource res in inf.Resources)
                {
                    writer.Write(res.ID);
                    writer.Write(res.Name.ToLength(36), false);
                }
            }   

            //bruh moment?
            writer.Write(0);
        }

        internal override int GetPropertyAnimationCurveCount()
        {
            return 12;
        }

        protected override void ReadAnimationCurves(DataReader reader, int dataSize)
        {
            PropertyAnimationCurve = new List<PibEmitterAnimationCurve>();

            int numFloats = (dataSize / 4) / GetPropertyAnimationCurveCount();

            PibEmitterAnimationCurveGeneric curve1 = new PibEmitterAnimationCurveGeneric();
            PibEmitterAnimationCurveGeneric curve2 = new PibEmitterAnimationCurveGeneric();
            PibEmitterAnimationCurveGeneric curve3 = new PibEmitterAnimationCurveGeneric();
            PibEmitterAnimationCurveGeneric curve4 = new PibEmitterAnimationCurveGeneric();
            PibEmitterAnimationCurveGeneric curve5 = new PibEmitterAnimationCurveGeneric();
            PibEmitterAnimationCurveGeneric curve6 = new PibEmitterAnimationCurveGeneric();
            PibEmitterAnimationCurveColor curve7 = new PibEmitterAnimationCurveColor();
            PibEmitterAnimationCurveColor curve8 = new PibEmitterAnimationCurveColor();
            PibEmitterAnimationCurveGeneric curve9 = new PibEmitterAnimationCurveGeneric();
            PibEmitterAnimationCurveGeneric curve10 = new PibEmitterAnimationCurveGeneric();
            PibEmitterAnimationCurveGeneric curve11 = new PibEmitterAnimationCurveGeneric();
            PibEmitterAnimationCurveGeneric curve12 = new PibEmitterAnimationCurveGeneric();

            curve1.Read(reader, numFloats);
            curve2.Read(reader, numFloats);
            curve3.Read(reader, numFloats);
            curve4.Read(reader, numFloats);
            curve5.Read(reader, numFloats);
            curve6.Read(reader, numFloats);
            curve7.Read(reader, numFloats);
            curve8.Read(reader, numFloats);
            curve9.Read(reader, numFloats);
            curve10.Read(reader, numFloats);
            curve11.Read(reader, numFloats);
            curve12.Read(reader, numFloats);

            PropertyAnimationCurve.Add(curve1);
            PropertyAnimationCurve.Add(curve2);
            PropertyAnimationCurve.Add(curve3);
            PropertyAnimationCurve.Add(curve4);
            PropertyAnimationCurve.Add(curve5);
            PropertyAnimationCurve.Add(curve6);
            PropertyAnimationCurve.Add(curve7);
            PropertyAnimationCurve.Add(curve8);
            PropertyAnimationCurve.Add(curve9);
            PropertyAnimationCurve.Add(curve10);
            PropertyAnimationCurve.Add(curve11);
            PropertyAnimationCurve.Add(curve12);
        }
    }
}
