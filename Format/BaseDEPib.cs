using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{
    public class BaseDEPib : BasePib
    {
        public float EVCorrect;

        public float[] ColorAnimationR = new float[32];
        public float[] ColorAnimationG = new float[32];
        public float[] ColorAnimationB = new float[32];
        public float[] ColorAnimationI = new float[32];

        public float ColorTime;
        public float IntensityTime;

        public uint SoundCuesheet;

        public PibFadeModule Fade = new PibFadeModule();

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

            EVCorrect = reader.ReadSingle();

            for(int i = 0; i < ColorAnimationR.Length; i++)
                ColorAnimationR[i] = reader.ReadSingle();

            for (int i = 0; i < ColorAnimationG.Length; i++)
                ColorAnimationG[i] = reader.ReadSingle();

            for (int i = 0; i < ColorAnimationB.Length; i++)
                ColorAnimationB[i] = reader.ReadSingle();

            for (int i = 0; i < ColorAnimationI.Length; i++)
                ColorAnimationI[i] = reader.ReadSingle();

            ColorTime = reader.ReadSingle();
            IntensityTime = reader.ReadSingle();
            Flags = reader.ReadUInt32();
            SoundCuesheet = reader.ReadUInt32();

            reader.Stream.Position += 8;

            BaseMatrix = reader.ReadMatrix4x4();
            Scale = reader.ReadVector3();

            reader.Stream.Position += 8;

            Fade = reader.Read<PibFadeModule>();

            reader.Stream.Position += 4;
        }

        protected override void WriteHeader(DataWriter writer)
        {
            writer.Write(ParticleID);
            writer.Write(EmitterCount);
            writer.Write(Duration);
            writer.Write(DurationOffset);

            writer.Write(Speed);
            writer.Write(ForwardOffset);
            writer.Write(MaxIntensity);

            writer.Write(Radius);
            writer.Write(Range);

            writer.Write(EVCorrect);

            foreach (float f in ColorAnimationR)
                writer.Write(f);
            
            foreach (float f in ColorAnimationG)
                writer.Write(f);
            
            foreach (float f in ColorAnimationB)
                writer.Write(f);
            
            foreach (float f in ColorAnimationI)
                writer.Write(f);

            writer.Write(ColorTime);
            writer.Write(IntensityTime);
            writer.Write(Flags);
            writer.Write(SoundCuesheet);

            writer.WriteTimes(0, 8);

            writer.Write(BaseMatrix);
            writer.Write(Scale);

            writer.WriteTimes(0, 8);

            writer.WriteOfType(Fade);

            writer.WriteTimes(0, 4);
        }
    }
}
