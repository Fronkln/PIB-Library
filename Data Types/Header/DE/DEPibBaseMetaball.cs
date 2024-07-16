using System;
using Yarhl.IO;

namespace PIBLib
{
    public class DEPibBaseMetaball : PibBaseMetaball
    {
        public float NmlZ;
        public float LtLambertOffset;
        public float LtRatio;
        public float HltIntensity;

        internal override void Read(DataReader reader)
        {
            Rate = reader.ReadSingle();
            Color = reader.ReadRGBA();
            NmlRange = reader.ReadSingle();
            NmlZ = reader.ReadSingle();
            LtShininess = reader.ReadSingle();
            LtIoe = reader.ReadSingle();
            LtLambertOffset = reader.ReadSingle();
            LtRatio = reader.ReadSingle();
            HltIntensity = reader.ReadSingle();
        }

        internal override void Write(DataWriter writer)
        {
            writer.Write(Rate);
            writer.Write(Color);
            writer.Write(NmlRange);
            writer.Write(NmlZ);
            writer.Write(LtShininess);
            writer.Write(LtIoe);
            writer.Write(LtLambertOffset);
            writer.Write(LtRatio);
            writer.Write(HltIntensity);
        }
    }
}
