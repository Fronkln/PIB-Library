using PIBLib.Conversions;
using System;
using System.Collections.Generic;
using System.Linq;
using Yarhl.IO;

namespace PIBLib
{
    //Changed:
    //Emitter main data (564 > 596 bytes)
    public class Pib45 : Pib43
    {
        protected override void ReadCorePibData(DataReader reader)
        {
            ParticleID = reader.ReadUInt32();
            EmitterCount = reader.ReadUInt32();
            Duration = reader.ReadUInt32();
            DurationOffset = reader.ReadInt32();

            Speed = reader.ReadSingle();
            ForwardOffset = reader.ReadSingle();
            MaxIntensity = reader.ReadSingle();

            Radius = reader.ReadInt32();
            Range = reader.ReadInt32();

            EVCorrect = reader.ReadSingle();

            for (int i = 0; i < ColorAnimationR.Length; i++)
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
            Fade = reader.Read<PibFadeModule>();

            reader.Stream.Position += 12;
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
            writer.WriteOfType(Fade);

            writer.WriteTimes(0, 12);
        }

        internal override void Read(DataReader reader)
        {
            ReadCorePibData(reader);
            ReadEmitters(reader, (int)EmitterCount);
        }

        protected override void ReadEmitters(DataReader reader, int count)
        {
            for (int i = 0; i < count; i++)
            {
                PibEmitterv45 emitter = new PibEmitterv45();
                emitter.Read(reader, Version);
                Emitters.Add(emitter);
            }
        }

        internal override void Write(DataWriter writer)
        {
            WriteHeader(writer);

            foreach (BasePibEmitter emitter in Emitters)
                emitter.Write(writer, Version);
        }
        
        public Pib43 ToV43()
        {
            return Pib45to43.Convert(this);
        }
        public Pib52 ToV52()
        {
            return Pib45To52.Convert(this);
        }
    }
}
