using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{
    public class DEPibUnkStructure6
    {
        public float Unk1;
        public float Unk2;
        public float Unk3;
        public float Unk4;
        public float Unk5;
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
        }
    }
}
