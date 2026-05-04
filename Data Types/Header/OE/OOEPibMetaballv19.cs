using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{
    public class OOEPibMetaballv19 : PibBaseMetaball
    {
        public int Flags;
        public float OEUnknown1 = -1f;
        public float OEUnknown2 = 566;
        public float OEUnknown3 = 1f;

        internal override  void Read(DataReader reader)
        {
            Flags = reader.ReadInt32();
            Rate = reader.ReadSingle();
            Color = reader.ReadRGBA();
            NmlRange = reader.ReadSingle();
            OEUnknown1 = reader.ReadSingle();
            OEUnknown2 = reader.ReadSingle();
            OEUnknown3 = reader.ReadSingle();
            LtShininess = reader.ReadSingle();
            LtIoe = reader.ReadSingle();
        }

        internal override void Write(DataWriter writer)
        {
            writer.Write(Flags);
            writer.Write(Rate);
            writer.Write(Color);
            writer.Write(NmlRange);
            writer.Write(OEUnknown1);
            writer.Write(OEUnknown2);
            writer.Write(OEUnknown3);
            writer.Write(LtShininess);
            writer.Write(LtIoe);
        }
    }
}
