using System;
using Yarhl.IO;

namespace PIBLib
{
    public class DEPibCommonUnkStructure2v52 : DEPibCommonUnkStructure2
    {
        public float Unk6;
        public float Unk7;
        public float Unk8;
        public float Unk9;
        public float Unk10;
        public float Unk11;
        public float Unk12;
        public float Unk13;
        public float Unk14;
        public float Unk15;
        public float Unk16;
        public float Unk17;

        internal override void Read(DataReader reader)
        {
            Unk6 = reader.ReadSingle();
            Unk7 = reader.ReadSingle();
            Unk8 = reader.ReadSingle();
            Unk9 = reader.ReadSingle();
            Unk10 = reader.ReadSingle();
            Unk11 = reader.ReadSingle();
            Unk12 = reader.ReadSingle();
            Unk13 = reader.ReadSingle();
            Unk14 = reader.ReadSingle();
            Unk15 = reader.ReadSingle();
            Unk16 = reader.ReadSingle();
            Unk17 = reader.ReadSingle();
            Unk1 = reader.ReadSingle();
            Unk2 = reader.ReadSingle();
            Unk3 = reader.ReadSingle();
            Unk4 = reader.ReadSingle();
            Unk5 = reader.ReadSingle();
        }

        internal override void Write(DataWriter writer)
        {
            writer.Write(Unk6);
            writer.Write(Unk7);
            writer.Write(Unk8);
            writer.Write(Unk9);
            writer.Write(Unk10);
            writer.Write(Unk11);
            writer.Write(Unk12);
            writer.Write(Unk13);
            writer.Write(Unk14);
            writer.Write(Unk15);
            writer.Write(Unk16);
            writer.Write(Unk17);
            writer.Write(Unk1);
            writer.Write(Unk2);
            writer.Write(Unk3);
            writer.Write(Unk4);
            writer.Write(Unk5);
        }
    }
}
