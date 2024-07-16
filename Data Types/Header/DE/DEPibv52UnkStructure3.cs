using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{
    public class DEPibv45UnkStructure3 : DEPibBaseUnkStructure3
    {
        public float Unk9 = 1;
        public float Unk10 = 1;
        public float Unk11 = 1;
        public float Unk12 = 1;

        internal override void Read(DataReader reader)
        {
            Unk1 = reader.ReadSingle();
            Unk2 = reader.ReadSingle();
            Unk3 = reader.ReadSingle();
            Unk4 = reader.ReadSingle();

            Unk5 = reader.ReadSingle();
            Unk9 = reader.ReadSingle();
            Unk10 = reader.ReadSingle();
            Unk11 = reader.ReadSingle();
            Unk12 = reader.ReadSingle();
        }

        internal override void Write(DataWriter writer)
        {
            writer.Write(Unk1);
            writer.Write(Unk2);
            writer.Write(Unk3);
            writer.Write(Unk4);



            writer.Write(Unk5);
            writer.Write(Unk9);
            writer.Write(Unk10);
            writer.Write(Unk11);
            writer.Write(Unk12);
        }
    }
}
