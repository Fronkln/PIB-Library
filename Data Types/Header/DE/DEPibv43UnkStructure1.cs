using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{
    //I don't think its exactly as big as 112 bytes
    //But the 112 bytes remained unchanged from YK2 to LJ
    //So it makes sense to treat it that way.
    public class DEPibv43UnkStructure1
    {
        public float Unk1 = 0;
        public float Unk2 = 0.5f;
        public float Unk3 = 0.5f;
        public float Unk4 = 0;
        public float Unk5 = 3;
        public float Unk6 = 1;
        public int Unk7 = 6000;
        public float Unk8 = 0;
        public float Unk9 = 1.5f;
        public float Unk10 = 0.5f;
        public float Unk11 = 3000;
        public float Unk12 = 0;
        public float Unk13 = 0.2f;
        public float Unk14 = 0.2f;
        public float Unk15 = 0.2f;
        public float Unk16 = 0.2f;
        public float Unk17 = 0;
        public float Unk18 = 0;
        public float Unk19 = 0;
        public float Unk20 = 1;
        public float Unk21 = 0;
        public float Unk22 = 0;
        public float Unk23 = 0;
        public float Unk24 = 1;

        internal virtual void Read(DataReader reader)
        {
            Unk1 = reader.ReadSingle();
            Unk2 = reader.ReadSingle();
            Unk3 = reader.ReadSingle();
            Unk4 = reader.ReadSingle();
            Unk5 = reader.ReadSingle();
            Unk6 = reader.ReadSingle();
            Unk7 = reader.ReadInt32();
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
            Unk18 = reader.ReadSingle();
            Unk19 = reader.ReadSingle();
            Unk20 = reader.ReadSingle();
            Unk21 = reader.ReadSingle();
            Unk22 = reader.ReadSingle();
            Unk23 = reader.ReadSingle();
            Unk24 = reader.ReadSingle();
        }

        internal virtual void Write(DataWriter writer)
        {
            writer.Write(Unk1);
            writer.Write(Unk2);
            writer.Write(Unk3);
            writer.Write(Unk4);
            writer.Write(Unk5);
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
            writer.Write(Unk18);
            writer.Write(Unk19);
            writer.Write(Unk20);
            writer.Write(Unk21);
            writer.Write(Unk22);
            writer.Write(Unk23);
            writer.Write(Unk24);
        }
    }
}
