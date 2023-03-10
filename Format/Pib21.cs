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
        public Vector2 UnknownVector_0xB4 = new Vector2(-1, -1);
        public uint UnknownFlags_0x3E = 512;

        internal override void Read(DataReader reader)
        {
            ReadCorePibData(reader);

            Unknown_0x34 = reader.ReadInt32();

            reader.Endianness = EndiannessMode.LittleEndian;
            UnknownFlag_0x38 = reader.ReadUInt32();
            reader.Endianness = EndiannessMode.BigEndian;

            UnknownFlags_0x3E = reader.ReadUInt32();

            BaseMatrix = reader.ReadMatrix4x4();
            Scale = reader.ReadVector3();

            reader.ReadBytes(40);
            UnknownVector_0xB4 = reader.ReadVector2();
            reader.ReadBytes(4);

            ReadEmitters(reader, (int)EmitterCount);
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
            writer.Write(Color);

            writer.Write((byte)Unknown4);
            writer.Write(Unknown5);

            //End of core
            writer.Write(Unknown_0x34);

            writer.Endianness = EndiannessMode.LittleEndian;
            writer.Write(UnknownFlag_0x38);
            writer.Endianness = EndiannessMode.BigEndian;

            writer.Write(UnknownFlags_0x3E);

            writer.Write(BaseMatrix);
            writer.Write(Scale);

            writer.WriteTimes(0, 40);
            writer.Write(UnknownVector_0xB4);
            writer.WriteTimes(0, 4);

            foreach (var emitter in Emitters)
                emitter.Write(writer);
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

        public Pib25 ToV25()
        {
            return Pib21To25.Convert(this);
        }
    }
}
