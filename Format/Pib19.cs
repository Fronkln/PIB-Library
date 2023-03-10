using PIBLib.Conversions;
using Yarhl.IO;

namespace PIBLib
{
    public class Pib19 : BasePib
    {
        public RGB32 Color;

        public int Unknown_0x34;
        public uint UnknownFlag_0x38;
        public int Unknown_0x3C;

        protected override void ReadCorePibData(DataReader reader)
        {
            ParticleID = reader.ReadUInt32();
            EmitterCount = reader.ReadUInt32();
            Duration = reader.ReadUInt32();
            Unknown1 = reader.ReadInt32();

            Speed = reader.ReadSingle();
            Unknown2 = reader.ReadSingle();
            Unknown3 = reader.ReadSingle();
            Color = reader.ReadRGB32();

            Unknown4 = reader.ReadByte();
            Unknown5 = reader.ReadInt32();
        }

        internal override void Read(DataReader reader)
        {
            base.Read(reader);

            Unknown_0x34 = reader.ReadInt32();

            reader.Endianness = EndiannessMode.LittleEndian;
            UnknownFlag_0x38 = reader.ReadUInt32();
            reader.Endianness = EndiannessMode.BigEndian;

            Unknown_0x3C = reader.ReadInt32();

            BaseMatrix = reader.ReadMatrix4x4();
            Scale = reader.ReadVector4();

            reader.ReadBytes(48);

            ReadEmitters(reader, (int)EmitterCount);
        }

        protected override void ReadEmitters(DataReader reader, int count)
        {
            for (int i = 0; i < count; i++)
            {
                PibEmitterv19 emitter = new PibEmitterv19();
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
            writer.Write(Color);

            writer.Write((byte)Unknown4);
            writer.Write(Unknown5);
            
            //End of core
            writer.Write(Unknown_0x34);

            writer.Endianness = EndiannessMode.LittleEndian;
            writer.Write(UnknownFlag_0x38);
            writer.Endianness = EndiannessMode.BigEndian;

            writer.Write(Unknown_0x3C);

            writer.Write(BaseMatrix);
            writer.Write(Scale);

            writer.WriteTimes(0, 52);

            foreach(var emitter in Emitters)
                emitter.Write(writer);
        }

        /// <summary>
        /// Convert to PIB version 21 (Yakuza 5)
        /// </summary>
        public Pib21 ToV21()
        {
            return Pib19To21.Convert(this);
        }
    }
}
