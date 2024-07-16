using System;
using Yarhl.IO;

namespace PIBLib
{
    public class DEPibVFModule
    {
        public uint DivNum = 0;
        public float VelocityScale = 0;
        public Vector3 RangeSize = Vector3.zero;
        public Vector3 RangeOffset = Vector3.zero;
        public float Life = 0;

        public void Read(DataReader reader)
        {
            DivNum = reader.ReadUInt32();
            VelocityScale = reader.ReadSingle();
            RangeSize = reader.ReadVector3();
            RangeOffset = reader.ReadVector3();
            Life = reader.ReadSingle();
        }

        public void Write(DataWriter writer)
        {
            writer.Write(DivNum);
            writer.Write(VelocityScale);
            writer.Write(RangeSize);
            writer.Write(RangeOffset);
            writer.Write(Life);
        }
    }
}
