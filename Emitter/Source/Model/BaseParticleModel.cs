using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Yarhl.IO;

namespace PIBLib
{
    public class BaseParticleModelData : ParticleIns
    {
        public Vector4 UnknownVector1;
        public Vector3 UnknownField1;

        public byte[] UnknownField2;
        public float[] UnknownField4;

        public Vector3 Scale;
        public float[] UnknownFloats;
        public Vector4 UnknownVector4;

        public ParticleModelDatav29 ToV29()
        {
            ParticleModelDatav29 model = new ParticleModelDatav29();
            model.UnknownVector1 = UnknownVector1;
            model.UnknownField1 = UnknownField1;
            model.Color = Color;

            model.UnknownField2 = UnknownField2;
            model.Lifetime = Lifetime;
            model.Unknown2 = Unknown2;
            model.UnknownTimeScaleThing = UnknownTimeScaleThing;
            model.Unknown6 = Unknown6;
            model.TimeScale = TimeScale;

            model.UnknownField4 = new float[8];

            for(int i= 0; i < 4; i++)
                model.UnknownField4[i] = UnknownField4[i];

            model.Scale = Scale;
            model.UnknownFloats = UnknownFloats;
            model.UnknownVector4 = UnknownVector4;

            return model;
        }
    }

    public class BaseParticleModel : ParticleSource
    {
        internal override void Read(DataReader reader, BasePibEmitter emitter, int flags, int count, uint version)
        {
            for (int i = 0; i < count; i++)
            {
                BaseParticleModelData modelData = new BaseParticleModelData();
                modelData.UnknownVector1 = reader.ReadVector4();
                modelData.UnknownField1 = reader.ReadVector3();
                modelData.Color = reader.ReadRGBA();

                modelData.UnknownField2 = reader.ReadBytes(12);
                modelData.Lifetime = reader.ReadSingle();

                modelData.Unknown2 = reader.ReadBytes(12);
                modelData.UnknownTimeScaleThing = reader.ReadSingle();
                modelData.Unknown6 = reader.ReadBytes(12);
                modelData.TimeScale = reader.ReadSingle();

                modelData.UnknownField4 = new float[4];

                for (int k = 0; k < 4; k++)
                    modelData.UnknownField4[k] = reader.ReadSingle();

                modelData.Scale = reader.ReadVector3();

                modelData.UnknownFloats = new float[5];

                for (int k = 0; k < 5; k++)
                    modelData.UnknownFloats[k] = reader.ReadSingle();

                modelData.UnknownVector4 = reader.ReadVector4();

                Particles.Add(modelData);
            }
        }

        internal override void Write(DataWriter writer, PibVersion version)
        {
            for (int i = 0; i < Particles.Count; i++)
            {
                BaseParticleModelData modelData = (BaseParticleModelData)Particles[i];

                writer.Write(modelData.UnknownVector1);
                writer.Write(modelData.UnknownField1);
                writer.Write(modelData.Color);

                writer.Write(modelData.UnknownField2);
                writer.Write(modelData.Lifetime);
                writer.Write(modelData.Unknown2);
                writer.Write(modelData.UnknownTimeScaleThing);
                writer.Write(modelData.Unknown6);
                writer.Write(modelData.TimeScale);

                foreach (float f in modelData.UnknownField4)
                    writer.Write(f);

                writer.Write(modelData.Scale);

                foreach (float f in modelData.UnknownFloats)
                    writer.Write(f);

                writer.Write(modelData.UnknownVector4);
            }
        }

        public override int GetDataCount()
        {
            return Particles.Count;
        }

        public override EmitterType GetDataType()
        {
            return EmitterType.Model;
        }

        public override List<object> GetData()
        {
            return Particles.Cast<object>().ToList();
        }
    }
}
