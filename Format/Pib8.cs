using PIBLib.Conversions;
using System.Drawing;
using Yarhl.IO;

namespace PIBLib
{
    public class Pib8 : BasePib
    {
        protected override void ReadCorePibData(DataReader reader)
        {
            ParticleID = reader.ReadUInt32();
            EmitterCount = reader.ReadUInt32();
            Duration = reader.ReadUInt32();
            Unknown1 = reader.ReadInt32();
            
            Speed = reader.ReadSingle();
            Unknown2 = reader.ReadSingle();
            Unknown3 = reader.ReadSingle();

            reader.ReadBytes(20);

            ReadEmitters(reader, (int)EmitterCount);
        }

        protected override void ReadEmitters(DataReader reader, int count)
        {
            for (int i = 0; i < count; i++)
            {
                PibEmitterv8 emitter = new PibEmitterv8();
                emitter.Read(reader, Version);
                Emitters.Add(emitter);
            }
        }

        protected override void WriteHeader(DataWriter writer)
        {
            writer.Write("PIBX", false);
            writer.Write(0x0010000);
            writer.Write((uint)Version);
            writer.WriteTimes(0, 4);
        }

        internal override void Write(DataWriter writer)
        {
            WriteHeader(writer);

            writer.Write(ParticleID);
            writer.Write(Emitters.Count);
            writer.Write(Duration);
            writer.Write(Unknown1);

            writer.Write(Speed);
            writer.Write(Unknown2);
            writer.Write(Unknown3);

            writer.WriteTimes(0, 20);

            foreach (var emitter in Emitters)
                emitter.Write(writer, Version);
        }

        public Pib19 ToV19()
        {
            return Pib8To19.Convert(this);
        }
    }
}
