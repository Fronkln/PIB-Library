using System.Collections.Generic;
using Yarhl.IO;

namespace PIBLib
{
    public class PibEmitterv29 : BaseDEPibEmitter
    {
        private byte[] UnkReg1 = new byte[16];
        private int UnkVar1 = 0;

        public List<string> ExtraTextures = new List<string>();

        internal override void Read(DataReader reader, PibVersion version)
        {
            Flags = reader.ReadInt32();
            Flags2 = reader.ReadInt32();
            Flags3 = reader.ReadInt32();

            Blend = reader.ReadByte();
            GeoVertex = reader.ReadByte();
            InsVertex = reader.ReadByte();
            MetaballBlend = 2;

            reader.Stream.Position++;
            UnkReg1 = reader.ReadBytes(16);

            AABoxCenter = reader.ReadVector3();
            AABoxExtent = reader.ReadVector3();

            int geoVertexCount = reader.ReadInt32();

            Blur = reader.Read<DEPibEmitterBlurModule>();
            ShadowRate = reader.ReadSingle();
            UnkVar1 = reader.ReadInt32();

            Metaball = new DEPibBaseMetaball();
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

            Track = new DEPibBaseTrack();
            Track.Read(reader);

            reader.Stream.Position += 12;

            UnkStructure1 = new PibBaseUnkStructure1();
            UnkStructure1.Read(reader);

            AnimationData = new EmitterAnimationDataDE();
            AnimationData.Read(reader);

            UnkStructure2 = new OOEPibBaseUnkStructure2();
            UnkStructure2.Read(reader);

            MinSpread = reader.ReadVector3();
            MaxSpread = reader.ReadVector3();

            UnkVal1 = reader.ReadSingle();

            CommonUnkStructure2 = new DEPibCommonUnkStructure2();
            CommonUnkStructure2.Read(reader);

            UnkVal2 = reader.ReadSingle();
            UnkVal3 = reader.ReadSingle();
            UnkVal4 = reader.ReadSingle();
            UnkVal5 = reader.ReadSingle();

            UnknownMainData = reader.ReadBytes(0);

            int data1Size = reader.ReadInt32(); //Includes DDS header

            DDSHeader.Read(reader);

            int floatCount = (data1Size - 128) / 4;
            int chunkCount = (data1Size - 128) / 256;

            ReadUnknownSection1(reader, data1Size - 128);

            EmitterType emitterType = GetEmitterType();
            Source = emitterType == EmitterType.Model ? new ParticleModelv29() : new ParticleBillboardv29();

            int textureCount = reader.ReadInt32();

            for (int i = 0; i < textureCount; i++)
                Textures.Add(reader.ReadString(32).Split(new[] { '\0' }, 2)[0]);

            reader.Stream.Position += 4;
            int extraTexturesCount = reader.ReadInt32();

            for (int i = 0; i < extraTexturesCount; i++)
                ExtraTextures.Add(reader.ReadString(32));

            int unknownCount2 = reader.ReadInt32();

            ReadUnknownData1(reader, GeoVertex, geoVertexCount, version);
            Source.Read(reader,this, (int)Flags, unknownCount2, (uint)version);
        }

        public override EmitterType GetEmitterType()
        {
            byte bit = (byte)(Flags >> 6);

            if ((bit & 1) == 1 || (bit & 0x40) == 1)
                return EmitterType.Billboard;

            return EmitterType.Model;
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

            writer.Write(UnkReg1);

            writer.Write(AABoxCenter);
            writer.Write(AABoxExtent);

            writer.Write(GetUnknownDataCount());

            writer.WriteOfType(Blur);
            writer.Write(ShadowRate);
            writer.Write(UnkVar1);

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

            writer.WriteTimes(0, 12);

            UnkStructure1.Write(writer);
            AnimationData.Write(writer);
            UnkStructure2.Write(writer);

            writer.Write(MinSpread);
            writer.Write(MaxSpread);

            writer.Write(UnkVal1);

            CommonUnkStructure2.Write(writer);

            writer.Write(UnkVal2);
            writer.Write(UnkVal3);
            writer.Write(UnkVal4);
            writer.Write(UnkVal5);

            WriteUnknownSection1(writer);

            writer.Write(Textures.Count);

            foreach (string str in Textures)
                writer.Write(str.ToLength(32));

            writer.WriteTimes(0, 4);

            writer.Write(ExtraTextures.Count);

            foreach (string str in ExtraTextures)
                writer.Write(str.ToLength(32));

            writer.Write(Source.GetDataCount());

            foreach (var chunk in UnknownData1)
                chunk.Write(writer);

            Source.Write(writer, version);
        }

        protected override void ReadUnknownSection1(DataReader reader, int dataSize)
        {
            PropertyAnimationCurve = new List<PibEmitterAnimationCurve>();

            int numFloats = (dataSize / 4) / GetPropertyAnimationCurveCount();

            PibEmitterAnimationCurveGeneric curve1 = new PibEmitterAnimationCurveGeneric();
            PibEmitterAnimationCurveGeneric curve2 = new PibEmitterAnimationCurveGeneric();
            PibEmitterAnimationCurveGeneric curve3 = new PibEmitterAnimationCurveGeneric();
            PibEmitterAnimationCurveGeneric curve4 = new PibEmitterAnimationCurveGeneric();
            PibEmitterAnimationCurveColor curve5 = new PibEmitterAnimationCurveColor();
            PibEmitterAnimationCurveGeneric curve6 = new PibEmitterAnimationCurveGeneric();

            curve1.Read(reader, numFloats);
            curve2.Read(reader, numFloats);
            curve3.Read(reader, numFloats);
            curve4.Read(reader, numFloats);
            curve5.Read(reader, numFloats);
            curve6.Read(reader, numFloats);

            PropertyAnimationCurve.Add(curve1);
            PropertyAnimationCurve.Add(curve2);
            PropertyAnimationCurve.Add(curve3);
            PropertyAnimationCurve.Add(curve4);
            PropertyAnimationCurve.Add(curve5);
            PropertyAnimationCurve.Add(curve6);
        }

        internal override int GetPropertyAnimationCurveCount()
        {
            return 6;
        }

        public override bool IsUseColorCurve()
        {
            return ((EmitterFlag1v29)Flags).HasFlag(EmitterFlag1v29.eFLG_COLOR_ANIM);
        }

        public override bool IsMetaball()
        {
            return ((EmitterFlag1v29)Flags).HasFlag(EmitterFlag1v29.eFLG_METABALL);
        }
    }
}
