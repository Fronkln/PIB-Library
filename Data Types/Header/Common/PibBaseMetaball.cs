using Yarhl.IO;

namespace PIBLib
{
    public class PibBaseMetaball
    {
        public float Rate;
        public RGBA Color;
        public float NmlRange;
        public float LtShininess;
        public float LtIoe;

        internal virtual void Read(DataReader reader)
        {
            throw new System.NotImplementedException();
        }

        internal virtual void Write(DataWriter writer)
        {
            throw new System.NotImplementedException();
        }
    }
}
