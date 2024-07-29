using System;
using System.Collections.Generic;
using System.Linq;
using Yarhl.IO;

namespace PIBLib
{
    public class PibEmitterv52 : BaseDE2PibEmitter
    {
        public new ulong Flags;
        public new ulong Flags2;
        public new ulong Flags3;

        public int Unknownv52_1;
        public int UnknownFlagv52_1;
        public int Unknownv52_2;

        public float[] LightUi2 = new float[4];
        public float TexA2Ratio = 1;
        public float TexB1Ratio = 1;

        public float LtEmissivePower = 1;
        public float LtEmissivePowerOfs = 0;
        public float GatherPower = 0;
        public Vector3 ModelOffset = Vector3.zero;

        public float RimBlendCurve;

        public List<TextureImportInfo> TextureImports = new List<TextureImportInfo>();

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

            AnimationData = new EmitterAnimationDataDE();
            AnimationData.Read(reader);

            UnkStructure2 = new OOEPibBaseUnkStructure2();
            UnkStructure2.Read(reader);

            MinSpread = reader.ReadVector3();
            MaxSpread = reader.ReadVector3();

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

            ReadUnknownSection1(reader, data1Size - 128);

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


        internal protected virtual void ReadTextureImports(DataReader reader, int textureCount)
        {
            UnkNumbers_TextureTable_V42 = new int[textureCount];

            for (int i = 0; i < textureCount; i++)
                UnkNumbers_TextureTable_V42[i] = reader.ReadInt32();

            for (int i = 0; i < textureCount; i++)
                Textures.Add(reader.ReadString(32).Split(new[] { '\0' }, 2)[0]);

            reader.Stream.Position += 36 * textureCount;

            int resourcesCount = reader.ReadInt32();

            for (int i = 0; i < resourcesCount; i++)
                ExtraTextures.Add(reader.ReadString(36));

            reader.Stream.Position += 4;
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

            writer.WriteTimes(0, 4);

            VAT.Write(writer);

            writer.Write(TickWait);

            foreach (float f in LightUi1)
                writer.Write(f);

            foreach (float f in LightUi2)
                writer.Write(f);

            writer.Write(TexA2Ratio);
            writer.Write(TexB1Ratio);

            writer.Write(LtEmissivePower);
            writer.Write(LtEmissivePowerOfs);
            writer.Write(GatherPower);
            writer.Write(ModelOffset);

            UV.Write(writer);

            writer.Write(RimBlendCurve);

            writer.WriteTimes(0, 20);

            AnimationData.Write(writer);
            UnkStructure2.Write(writer);

            writer.Write(MinSpread);
            writer.Write(MaxSpread);

            writer.Write(UnkVal1);

            v45Unk1.Write(writer);
            CommonUnkStructure2.Write(writer);

            writer.Write(UnkVal2);
            writer.Write(UnkVal3);
            writer.Write(UnkVal4);
            writer.Write(UnkVal5);

            UnkStructure5.Write(writer);

            WriteUnknownSection1(writer);

            WriteTextureImports(writer, version);

            writer.Write(Source.GetDataCount());

            foreach (var chunk in UnknownData1)
                chunk.Write(writer);

            Source.Write(writer, version);
        }

        internal protected virtual void WriteTextureImports(DataWriter writer, PibVersion version)
        {
            int GetResourceCount()
            {
                int count = 0;

                foreach (var inf in TextureImports)
                    count += inf.Resources.Count;

                return count;
            }

            List<string> GetAllResources()
            {
                List<string> resources = new List<string>();

                foreach (var inf in TextureImports)
                    resources.AddRange(inf.Resources.Select(x => x.Name));

                return resources;
            }

            writer.Write(Textures.Count);

            for (int i = 0; i < Textures.Count; i++)
                writer.Write(UnkNumbers_TextureTable_V42[i]);

            foreach (string str in Textures)
                writer.Write(str.ToLength(32));

            int resourcesCount = GetResourceCount();

            writer.WriteTimes(0, (36 * Textures.Count) - 4);
            writer.Write(GetResourceCount());

            writer.Write(ExtraTextures.Count + resourcesCount);

            foreach(string str in GetAllResources())
                writer.Write(str.ToLength(36), false);

            foreach (string str in ExtraTextures)
                writer.Write(str.ToLength(36), false);

            writer.WriteTimes(0, 4);
        }

        public override EmitterType GetEmitterType()
        {
            if ((Flags & 0x880) != 0)
                return EmitterType.Billboard;
            else
                return EmitterType.Model;
        }

        protected override void ReadUnknownSection1(DataReader reader, int dataSize)
        {
            PropertyAnimationCurve = new List<PibEmitterAnimationCurve>();

            int numFloats = (dataSize / 4) / GetPropertyAnimationCurveCount();

            //v52: Shift happened at the 3rd curve, Color Curve is now the 7th one instead of 6th
            //8th curve seems to be new. used to be empty data. Compare: JE and YLAD AAa0000.pib AAa0001.pib
            PibEmitterAnimationCurveGeneric curve1 = new PibEmitterAnimationCurveGeneric();
            PibEmitterAnimationCurveGeneric curve2 = new PibEmitterAnimationCurveGeneric();
            PibEmitterAnimationCurveGeneric curve3 = new PibEmitterAnimationCurveGeneric();
            PibEmitterAnimationCurveGeneric curve4 = new PibEmitterAnimationCurveGeneric();
            PibEmitterAnimationCurveGeneric curve5 = new PibEmitterAnimationCurveGeneric();
            PibEmitterAnimationCurveGeneric curve6 = new PibEmitterAnimationCurveGeneric();
            PibEmitterAnimationCurveColor curve7 = new PibEmitterAnimationCurveColor();
            PibEmitterAnimationCurveColor curve8 = new PibEmitterAnimationCurveColor();

            curve1.Read(reader, numFloats);
            curve2.Read(reader, numFloats);
            curve3.Read(reader, numFloats);
            curve4.Read(reader, numFloats);
            curve5.Read(reader, numFloats);
            curve6.Read(reader, numFloats);
            curve7.Read(reader, numFloats);
            curve8.Read(reader, numFloats);

            PropertyAnimationCurve.Add(curve1);
            PropertyAnimationCurve.Add(curve2);
            PropertyAnimationCurve.Add(curve3);
            PropertyAnimationCurve.Add(curve4);
            PropertyAnimationCurve.Add(curve5);
            PropertyAnimationCurve.Add(curve6);
            PropertyAnimationCurve.Add(curve7);
            PropertyAnimationCurve.Add(curve8);
        }

        internal override int GetPropertyAnimationCurveCount()
        {
            return 8;
        }

        public override bool IsUseColorCurve()
        {
            return ((EmitterFlag1v52)Flags).HasFlag(EmitterFlag1v52.eFLG_COLOR_ANIM);
        }

        public override bool IsMetaball()
        {
            return ((EmitterFlag1v52)Flags).HasFlag(EmitterFlag1v52.eFLG_METABALL);
        }
    }
}
