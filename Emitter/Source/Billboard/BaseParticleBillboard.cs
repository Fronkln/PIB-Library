using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using Yarhl.IO;

namespace PIBLib
{
    public class BaseParticleBillboardData : ParticleIns
    {
        public Vector3 Offset = new Vector3();
        public float Start = 0;
        public Vector3 MoveDirection = new Vector3();
        public Vector3 Unknown1 = new Vector3();
        public byte[] Unknown2 = new byte[12];
        public float UnknownTimeScaleThing;
        public byte[] Unknown6 = new byte[12];
        public float TimeScale = 0.0003333333f;
        public Vector4 Unknown3;
        public Vector4 Scale = new Vector4(1, 1, 0, 0);
        public Vector4 Unknown4 = new Vector4(1, 1, 0, 0);

        internal virtual void Read(DataReader reader)
        {
            Offset = reader.ReadVector3();
            Start = reader.ReadSingle();
            MoveDirection = reader.ReadVector3();
            Color = reader.ReadRGBA();
            Unknown1 = reader.ReadVector3();
            Lifetime = reader.ReadSingle();
            Unknown2 = reader.ReadBytes(12);
            UnknownTimeScaleThing = reader.ReadSingle();
            Unknown6 = reader.ReadBytes(12);
            TimeScale = reader.ReadSingle();
            Unknown3 = reader.ReadVector4();
            Scale = reader.ReadVector4();
            Unknown4 = reader.ReadVector4();
        }

        internal virtual void Write(DataWriter writer)
        {
            writer.Write(Offset);
            writer.Write(Start);
            writer.Write(MoveDirection);
            writer.Write(Color);
            writer.Write(Unknown1);
            writer.Write(Lifetime);
            writer.Write(Unknown2);
            writer.Write(UnknownTimeScaleThing);
            writer.Write(Unknown6);
            writer.Write(TimeScale);
            writer.Write(Unknown3);
            writer.Write(Scale);
            writer.Write(Unknown4);
        }

        public ParticleBillboardDatav29 ToV29()
        {
            ParticleBillboardDatav29 convertedDat = new ParticleBillboardDatav29();
            convertedDat.Offset = Offset;
            convertedDat.Start = Start;
            convertedDat.MoveDirection = MoveDirection;
            convertedDat.Color = Color;
            convertedDat.Unknown1 = Unknown1;
            convertedDat.Lifetime = Lifetime;
            convertedDat.Unknown2 = Unknown2;
            convertedDat.UnknownTimeScaleThing = UnknownTimeScaleThing;
            convertedDat.Unknown6 = Unknown6;
            convertedDat.TimeScale = TimeScale;
            convertedDat.Unknown3 = Unknown3;
            convertedDat.Scale = Scale;
            convertedDat.Unknown4 = Unknown4;

            return convertedDat;
        }
    }

    public class BaseParticleBillboard : ParticleSource
    {
        internal override void Read(DataReader reader, BasePibEmitter emitter, int flags, int count, uint version)
        {
            //Aaa0001 - Aaa0010 pib weird var moment offset 28

            for (int i = 0; i < count; i++)
            {
                BaseParticleBillboardData billData = new BaseParticleBillboardData();
                billData.Read(reader);

                Particles.Add(billData);
            }

            reader.Endianness = EndiannessMode.BigEndian;
        }

        internal override void Write(DataWriter writer, PibVersion version)
        {
            for (int i = 0; i < Particles.Count; i++)
            {
                BaseParticleBillboardData billData = (BaseParticleBillboardData)Particles[i];
                billData.Write(writer);
            }
        }


        public override int GetDataCount()
        {
            return Particles.Count;
        }

        public override EmitterType GetDataType()
        {
            return EmitterType.Billboard;
        }

        public override List<object> GetData()
        {
            return Particles.Cast<object>().ToList();
        }
    }
}
