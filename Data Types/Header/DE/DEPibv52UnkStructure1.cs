using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{
    public class DEPibv52UnkStructure1 : DEPibv45UnkStructure1
    {
        public float Unk9;
        public float Unk10;
        public float Unk11;
        public float Unk12;

        public override void Read(DataReader reader)
        {
            base.Read(reader);

            Unk9 = reader.ReadSingle();
            Unk10 = reader.ReadSingle();
            Unk11 = reader.ReadSingle();
            Unk12 = reader.ReadSingle();
        }

        public override void Write(DataWriter writer)
        {
            base.Write(writer);

            writer.Write(Unk9);
            writer.Write(Unk10);
            writer.Write(Unk11);
            writer.Write(Unk12);
        }
    }
}
