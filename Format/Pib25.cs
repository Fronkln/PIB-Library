using PIBLib.Conversions;
using System.Reflection.PortableExecutable;
using Yarhl.IO;

namespace PIBLib
{
    public class Pib25 : BasePib
    {
        public float[,] UnkFloats = new float[5, 26];
        public uint UnknownFlags_0x3E = 1024;
        public Vector2 UnknownVector_0xB4 = new Vector2(-1, -1);

        protected override void ReadCorePibData(DataReader reader)
        {
            ParticleID = reader.ReadUInt32();
            EmitterCount = reader.ReadUInt32();
            Duration = reader.ReadUInt32();
            Unknown1 = reader.ReadInt32();

            Speed = reader.ReadSingle();
            Unknown2 = reader.ReadSingle();
            Unknown3 = reader.ReadSingle();

            Unknown4 = reader.ReadInt32();
            Unknown5 = reader.ReadInt32();
        }

        internal override void Read(DataReader reader)
        {
            ReadCorePibData(reader);

            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 26; j++)
                    UnkFloats[i, j] = reader.ReadSingle();

            UnknownFlags_0x3E = reader.ReadUInt32();

            BaseMatrix = reader.ReadMatrix4x4();
            Scale = reader.ReadVector3();

            reader.ReadBytes(40);
            UnknownVector_0xB4 = reader.ReadVector2();
            reader.ReadBytes(4);

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

            writer.Write(Unknown4);
            writer.Write(Unknown5);

            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 26; j++)
                    writer.Write(UnkFloats[i, j]);

            writer.Write(UnknownFlags_0x3E);

            writer.Write(BaseMatrix);
            writer.Write(Scale);

            writer.WriteTimes(0, 40);
            writer.Write(UnknownVector_0xB4);
            writer.WriteTimes(0, 4);

            foreach (var emitter in Emitters)
                emitter.Write(writer);
        }

        public Pib27 ToV27()
        {
            return Pib25To27.Convert(this);
        }
    }
}
