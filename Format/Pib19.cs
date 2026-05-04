using PIBLib.Conversions;
using Yarhl.IO;

namespace PIBLib
{
    public class Pib19 : BasePib
    {
        public RGB LightColor;

        public int Unknown_0x34;
        public float UnknownV19;

        protected override void ReadCorePibData(DataReader reader)
        {
            ParticleID = reader.ReadUInt32();
            EmitterCount = reader.ReadUInt32();
            Duration = reader.ReadUInt32();
            DurationOffset = reader.ReadInt32();

            Speed = reader.ReadSingle();
            ForwardOffset = reader.ReadSingle();
            MaxIntensity = reader.ReadSingle();
            LightColor = reader.ReadRGB();

            reader.ReadByte();

            Radius = reader.ReadInt32();
            Range = reader.ReadInt32();

            UnknownV19 = reader.ReadSingle();
            Flags = reader.ReadUInt32();

            BaseMatrix = reader.ReadMatrix4x4();
            Scale = reader.ReadVector4();

            reader.ReadBytes(20);
            Fade = reader.Read<PibFadeModule>();
            reader.ReadBytes(4);
        }

        internal override void Read(DataReader reader)
        {
            base.Read(reader);
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

            foreach(var emitter in Emitters)
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
            writer.Write(LightColor);

            writer.WriteTimes(0, 1);

            writer.Write(Radius);
            writer.Write(Range);

            writer.Endianness = EndiannessMode.LittleEndian;
            writer.Write(UnknownV19);
            writer.Endianness = EndiannessMode.BigEndian;

            writer.Write(Flags);

            writer.Write(BaseMatrix);
            writer.Write((Vector4)Scale);

            writer.WriteTimes(0, 20);
            writer.WriteOfType(Fade);
            writer.WriteTimes(0, 4);
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
