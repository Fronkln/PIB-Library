using PIBLib.Conversions;
using Yarhl.IO;

namespace PIBLib
{
    public class Pib25 : Pib21
    {
        public Vector2 UnknownVector_0xB4 = new Vector2(-1, -1);

        public float[] ColorAnimationR = new float[32];
        public float[] ColorAnimationG = new float[32];
        public float[] ColorAnimationB = new float[32];
        public float[] ColorAnimationI = new float[32];

        public float Unkv25_1;
        public float IntensityTime;

        protected override void ReadCorePibData(DataReader reader)
        {
            ParticleID = reader.ReadUInt32();
            EmitterCount = reader.ReadUInt32();
            Duration = reader.ReadUInt32();
            DurationOffset = reader.ReadInt32();

            Speed = reader.ReadSingle();
            ForwardOffset = reader.ReadSingle();
            MaxIntensity = reader.ReadSingle();

            Radius = reader.ReadSingle();
            Range = reader.ReadSingle();

            for (int i = 0; i < ColorAnimationR.Length; i++)
                ColorAnimationR[i] = reader.ReadSingle();

            for (int i = 0; i < ColorAnimationG.Length; i++)
                ColorAnimationG[i] = reader.ReadSingle();

            for (int i = 0; i < ColorAnimationB.Length; i++)
                ColorAnimationB[i] = reader.ReadSingle();

            for (int i = 0; i < ColorAnimationI.Length; i++)
                ColorAnimationI[i] = reader.ReadSingle();

            Unkv25_1 = reader.ReadSingle();
            IntensityTime = reader.ReadSingle();
            UnknownFlags_0x3E = reader.ReadInt32();

            BaseMatrix = reader.ReadMatrix4x4();
            Scale = reader.ReadVector3();

            reader.ReadBytes(24);
            Fade = reader.Read<PibFadeModule>();
            reader.ReadBytes(4);
        }
        internal override void Read(DataReader reader)
        {
            ReadCorePibData(reader);
            ReadEmitters(reader, (int)EmitterCount);
        }

        protected override void ReadEmitters(DataReader reader, int count)
        {         
            for (int i = 0; i < EmitterCount; i++)
            {
                PibEmitterv25 emitter = new PibEmitterv25();
                emitter.Read(reader, Version);
                Emitters.Add(emitter);
            }
        }

        protected override void WriteHeader(DataWriter writer)
        {
            writer.Write(ParticleID);
            writer.Write(Emitters.Count);
            writer.Write(Duration);
            writer.Write(DurationOffset);

            writer.Write(Speed);
            writer.Write(ForwardOffset);
            writer.Write(MaxIntensity);

            writer.Write(Radius);
            writer.Write(Range);

            foreach (float f in ColorAnimationR)
                writer.Write(f);

            foreach (float f in ColorAnimationG)
                writer.Write(f);

            foreach (float f in ColorAnimationB)
                writer.Write(f);

            foreach (float f in ColorAnimationI)
                writer.Write(f);

            writer.Write(Unkv25_1);
            writer.Write(IntensityTime);
            writer.Write(UnknownFlags_0x3E);

            writer.Write(BaseMatrix);
            writer.Write(Scale);

            writer.WriteTimes(0, 24);
            writer.WriteOfType(Fade);
            writer.WriteTimes(0, 4);
        }

        internal override void Write(DataWriter writer)
        {
            WriteHeader(writer);

            foreach (var emitter in Emitters)
                emitter.Write(writer, Version);
        }


        public new Pib21 ToV21()
        {
            return Pib25To21.Convert(this);
        }
        public Pib27 ToV27()
        {
            return Pib25To27.Convert(this);
        }
    }
}
