using System;
using Yarhl.IO;

namespace PIBLib
{
    public class DEPibBaseTrack
    {
        public float FadeLength;
        public uint DivNum;
        internal virtual void Read(DataReader reader)
        {
            FadeLength = reader.ReadSingle();
            DivNum = reader.ReadUInt32();
        }

        internal virtual void Write(DataWriter writer)
        {
            writer.Write(FadeLength);
            writer.Write(DivNum);
        }
    }
}
