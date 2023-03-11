using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{
    public class Pib29 : BasePib
    {
        public float[,] UnkFloats = new float[5, 26];
        public byte[] Unknown_0x23C = new byte[20];
        public Vector2 Unknown0x2B4;

        public byte[] Unknown_0x29C;

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
            base.Read(reader);

            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 26; j++)
                    UnkFloats[i, j] = reader.ReadSingle();

            Unknown_0x23C = reader.ReadBytes(20);

            BaseMatrix = reader.ReadMatrix4x4();
            Scale = reader.ReadVector3();

            Unknown_0x29C = reader.ReadBytes(24);
            Unknown0x2B4 = reader.ReadVector2();
            reader.ReadBytes(4);

            ReadEmitters(reader, (int)EmitterCount);
        }

        protected override void ReadEmitters(DataReader reader, int count)
        {
            for (int i = 0; i < count; i++)
            {
                PibEmitterv29 emitter = new PibEmitterv29();
                emitter.Read(reader, Version);
                Emitters.Add(emitter);
            }
        }

        protected override void WriteHeader(DataWriter writer)
        {
            writer.Write("PIBX", false);
            writer.Write(2);
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

            writer.Write(Unknown4);
            writer.Write(Unknown5);

            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 26; j++)
                    writer.Write(UnkFloats[i, j]);

            writer.Write(Unknown_0x23C);

            writer.Write(BaseMatrix);
            writer.Write(Scale);

            writer.Write(Unknown_0x29C);
            writer.Write(Unknown0x2B4);
            writer.WriteTimes(0, 4);

            foreach (BasePibEmitter emitter in Emitters)
                emitter.Write(writer);
        }
    }
}
