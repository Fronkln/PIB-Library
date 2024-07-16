using System;
using Yarhl.IO;

namespace PIBLib
{
    public class DEPibMetaballv43 : DEPibBaseMetaball
    {
        public float LtEmissive;
        public float LtReflection;
        public float LtRefraction;
        public float[] HltBefore = new float[2];
        public float[] HltAfter = new float[2];
        public float HltPower;

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
            LtEmissive = reader.ReadSingle();
            LtReflection = reader.ReadSingle();
            LtRefraction = reader.ReadSingle();
            
            HltBefore = new float[] { reader.ReadSingle(), reader.ReadSingle() };
            HltAfter = new float[] { reader.ReadSingle(), reader.ReadSingle() };
            
            HltPower = reader.ReadSingle();
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
            writer.Write(LtEmissive);
            writer.Write(LtReflection);
            writer.Write(LtRefraction);

            foreach (float f in HltBefore) writer.Write(f);
            foreach (float f in HltAfter) writer.Write(f);

            writer.Write(HltPower);
            writer.Write(HltIntensity);
        }
    }
}
