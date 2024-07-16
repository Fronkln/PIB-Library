using System;
using Yarhl.IO;

namespace PIBLib
{
    public class DEPibCommonUnkStructure2 : PibBaseCommonUnkStructure2
    {
        public float Unk5;

        internal override void Read(DataReader reader)
        {
            Unk1 = reader.ReadSingle();
            Unk2 = reader.ReadSingle();
            Unk3 = reader.ReadSingle();
            Unk4 = reader.ReadSingle();
            Unk5 = reader.ReadSingle();
        }

        internal override void Write(DataWriter writer)
        {
            writer.Write(Unk1);
            writer.Write(Unk2);
            writer.Write(Unk3);
            writer.Write(Unk4);
            writer.Write(Unk5);
        }
    }
}
