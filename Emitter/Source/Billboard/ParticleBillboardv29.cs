using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Yarhl.IO;

namespace PIBLib
{
    public class ParticleBillboardDatav29 : BaseParticleBillboardData
    {
        public Vector4 Unknown5;

        public ParticleBillboardDatav29()
        {
            Unknown2 = new byte[12];
            Unknown6 = new byte[12];
        }

        internal override void Read(DataReader reader)
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
            Unknown5 = reader.ReadVector4();
            Scale = reader.ReadVector4();
            Unknown4 = reader.ReadVector4();
        }

        internal override void Write(DataWriter writer)
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
            writer.Write(Unknown5);
            writer.Write(Scale);
            writer.Write(Unknown4);
        }
    }

    internal class ParticleBillboardv29 : ParticleSourcev29
    {
        internal override void ReadData(DataReader reader, BasePibEmitter emitter, int flags, int count, uint version)
        {

            for (int i = 0; i < count; i++)
            {
                ParticleBillboardDatav29 billData = new ParticleBillboardDatav29();
                billData.Read(reader);

                Particles.Add(billData);
            }
        }

        protected override void WriteData(DataWriter writer, PibVersion version)
        {
            for (int i = 0; i < Particles.Count; i++)
            {
                ParticleBillboardDatav29 billData = (ParticleBillboardDatav29)Particles[i];
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
