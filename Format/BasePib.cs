using System;
using System.Collections.Generic;
using Yarhl.IO;

namespace PIBLib
{
    public class BasePib
    {
        public byte Unk;
        public bool IsBigEndian;
        public PibVersion Version;

        public uint ParticleID;
        public uint EmitterCount;
        public uint Duration; //In game ticks
        public int Unknown1;
        public float Speed;
        public float Unknown2;
        public float Unknown3;

        public int Unknown4;
        public int Unknown5;

        //Variables that are scattered in various places in pib versions
        //Will be read by their respective class
        public Matrix4x4 BaseMatrix;
        public Vector3 Scale;

        public Vector4 UnknownVector;

        public List<BasePibEmitter> Emitters = new List<BasePibEmitter>();

        internal virtual void Read(DataReader reader)
        {
            ReadCorePibData(reader);
        }

        protected virtual void ReadCorePibData(DataReader reader)
        {
            ParticleID = reader.ReadUInt32();
            EmitterCount = reader.ReadUInt32();
            Duration = reader.ReadUInt32();
            Unknown1 = reader.ReadInt32();

            Speed = reader.ReadSingle();
            Unknown2 = reader.ReadSingle();
            Unknown3 = reader.ReadSingle();
            reader.ReadBytes(4);

            Unknown4 = reader.ReadByte();
            Unknown5 = reader.ReadInt32();
        }

        protected virtual void ReadEmitters(DataReader reader, int count)
        {

        }


        protected virtual void WriteHeader(DataWriter writer)
        {
            writer.Write("PIBX", false);
            writer.Write((Version < PibVersion.Y6 || Version == PibVersion.Kenzan) ? 0x2010000 : 0x2000000);
            writer.Write((uint)Version);
            writer.WriteTimes(0, 4);
        }

        internal virtual void Write(DataWriter writer)
        {
            WriteHeader(writer);
        }
    }
}
