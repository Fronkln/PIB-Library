using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{
    //Atleast 40 bytes
    public class DEPibBaseUnkStructure2
    {
        public float Unk1 = 0;
        public float Unk2 = 0;
        public float Unk3 = 0;
        public float Unk4 = 0;
        public float Unk5 = 0;
        public float Unk6 = 0;
        public float Unk7 = 0;
        public float Unk8 = 0;
        public float Unk9 = 0;
        public float Unk10 = 0;

        internal virtual void Read(DataReader reader)
        {
            Unk1 = reader.ReadSingle();
            Unk2 = reader.ReadSingle();
            Unk3 = reader.ReadSingle();
            Unk4 = reader.ReadSingle();
            Unk5 = reader.ReadSingle();
            Unk6 = reader.ReadSingle();
            Unk7 = reader.ReadSingle();
            Unk8 = reader.ReadSingle();
            Unk9 = reader.ReadSingle();
            Unk10 = reader.ReadSingle();
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
        }
    }
}
