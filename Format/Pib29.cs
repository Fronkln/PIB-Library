using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

            reader.ReadBytes(24);
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
    }
}
