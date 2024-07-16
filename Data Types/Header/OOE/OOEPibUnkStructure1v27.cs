using System;
using Yarhl.IO;

namespace PIBLib
{
    public class OEPibUnkStructure1v27
    {
        public short Unk1_v27 = -1;
        public short Unk2_v27 = -7372;
        public short Unk3_v27 = -1;
        public short Unk4_v27 = -4915;
        public short Unk5_v27 = -1;
        public short Unk6_v27 = -7372;
        public short Unk7_v27 = -1;
        public short Unk8_v27 = -4915;

        public float Unk9_v27 = 0f;
        public float Unk10_v27 = 0f;
        public float Unk11_v27 = 0f;


        internal virtual void Read(DataReader reader)
        {
            Unk1_v27 = reader.ReadInt16();
            Unk2_v27 = reader.ReadInt16();
            Unk3_v27 = reader.ReadInt16();
            Unk4_v27 = reader.ReadInt16();
            Unk5_v27 = reader.ReadInt16();
            Unk6_v27 = reader.ReadInt16();
            Unk7_v27 = reader.ReadInt16();
            Unk8_v27 = reader.ReadInt16();

            Unk9_v27 = reader.ReadSingle();
            Unk10_v27 = reader.ReadSingle();
            Unk11_v27 = reader.ReadSingle();
        }

        internal virtual void Write(DataWriter writer)
        {
            writer.Write(Unk1_v27);
            writer.Write(Unk2_v27);
            writer.Write(Unk3_v27);
            writer.Write(Unk4_v27);
            writer.Write(Unk5_v27);
            writer.Write(Unk6_v27);
            writer.Write(Unk7_v27);
            writer.Write(Unk8_v27);

            writer.Write(Unk9_v27);
            writer.Write(Unk10_v27);
            writer.Write(Unk11_v27);
        }

    }
}
