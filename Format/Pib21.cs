using PIBLib.Conversions;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Runtime.CompilerServices;
using Yarhl.IO;

namespace PIBLib
{
    public class Pib21 : Pib19
    {
        public PibFadeModule Fade = new PibFadeModule();
        public int UnknownFlags_0x3E = 512;

        protected override void ReadCorePibData(DataReader reader)
        {
            ParticleID = reader.ReadUInt32();
            EmitterCount = reader.ReadUInt32();
            Duration = reader.ReadUInt32();
            DurationOffset = reader.ReadInt32();

            Speed = reader.ReadSingle();
            ForwardOffset = reader.ReadSingle();
            MaxIntensity = reader.ReadSingle();
            Color = reader.ReadRGB();

            reader.ReadByte();

            Radius = reader.ReadInt32();
            Range = reader.ReadInt32();

            reader.Endianness = EndiannessMode.LittleEndian;
            UnknownFlag_0x38 = reader.ReadUInt32();
            reader.Endianness = EndiannessMode.BigEndian;

            Unknown_0x3C = reader.ReadInt32();

            BaseMatrix = reader.ReadMatrix4x4();
            Scale = reader.ReadVector4();

            reader.ReadBytes(24);
            Fade = reader.Read<PibFadeModule>();
            //reader.ReadBytes(4);
        }

        internal override void Read(DataReader reader)
        {
            ReadCorePibData(reader);
            ReadEmitters(reader, (int)EmitterCount);
        }

        internal override void Write(DataWriter writer)
        {
            WriteHeader(writer);

            foreach (var emitter in Emitters)
                emitter.Write(writer, Version);
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
            writer.Write(Color);

            writer.WriteTimes(0, 1);

            writer.Write(Radius);
            writer.Write(Range);

            writer.Endianness = EndiannessMode.LittleEndian;
            writer.Write(UnknownFlag_0x38);
            writer.Endianness = EndiannessMode.BigEndian;

            writer.Write(Unknown_0x3C);

            writer.Write(BaseMatrix);
            writer.Write(Scale);

            writer.WriteTimes(0, 24);
            writer.WriteOfType(Fade);
            writer.WriteTimes(0, 4);
        }

        protected override void ReadEmitters(DataReader reader, int count)
        {
            for (int i = 0; i < count; i++)
            {
                PibEmitterv21 emitter = new PibEmitterv21();
                emitter.Read(reader, Version);
                Emitters.Add(emitter);
            }
        }

        
        public Pib19 ToV19()
        {
            return Pib21To19.Convert(this);
        }

        public Pib25 ToV25()
        {
            return Pib21To25.Convert(this);
        }
    }
}
