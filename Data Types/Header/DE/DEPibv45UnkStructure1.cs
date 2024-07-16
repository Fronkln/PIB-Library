using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{
    public class DEPibv45UnkStructure1
    {
        public float Unk1;
        public float Unk2;
        public float Unk3;
        public float Unk4;
        public float Unk5;
        public float Unk6;
        public float Unk7;
        public float Unk8;

        public virtual void Read(DataReader reader)
        {
            Unk1 = reader.ReadSingle();
            Unk2 = reader.ReadSingle();
            Unk3 = reader.ReadSingle();
            Unk4 = reader.ReadSingle();
            Unk5 = reader.ReadSingle();
            Unk6 = reader.ReadSingle();
            Unk7 = reader.ReadSingle();
            Unk8 = reader.ReadSingle();
        }

        public virtual void Write(DataWriter writer)
        {
            writer.Write(Unk1);
            writer.Write(Unk2);
            writer.Write(Unk3);
            writer.Write(Unk4);
            writer.Write(Unk5);
            writer.Write(Unk6);
            writer.Write(Unk7);
            writer.Write(Unk8);
        }
    }
}
