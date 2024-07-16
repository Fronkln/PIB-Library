using System;
using Yarhl.IO;

namespace PIBLib
{
    public class PibBaseMetaball
    {
        public float Rate;
        public RGBA Color;
        public float NmlRange;
        public float OEUnknown1 = -1f;
        public float OEUnknown2 = 566;
        public float OEUnknown3 = 1f;
        public float LtShininess;
        public float LtIoe;

        internal virtual void Read(DataReader reader)
        {
            Rate = reader.ReadSingle();
            Color = reader.ReadRGBA();
            NmlRange = reader.ReadSingle();
            OEUnknown1 = reader.ReadSingle();
            OEUnknown2 = reader.ReadSingle();
            OEUnknown3 = reader.ReadSingle();
            LtShininess = reader.ReadSingle();
            LtIoe = reader.ReadSingle();
        }

        internal virtual void Write(DataWriter writer)
        {
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
