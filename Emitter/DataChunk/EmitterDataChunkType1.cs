using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{
    public class EmitterDataChunkType1 : EmitterBaseDataChunk
    {
        public Vector3 Position;
        public Vector4 UV01;
        public Vector4 UV23;

        public EmitterDataChunkType1()
        {
           // Data = new float[44 / 4];
        }
        internal override void Read(DataReader reader, PibVersion version)
        {
            Position = reader.ReadVector3();
            UV01 = reader.ReadVector4();
            UV23 = reader.ReadVector4();

            
           // Data = new float[44 / 4];

           // for(int i = 0; i < Data.Length; i++)
                //Data[i] = reader.ReadSingle();
        }

        internal override void Write(DataWriter writer)
        {
#warning TEMPORARY UNTIL U FINISH CHUNK CONVERSIONS
            if (Data != null && Data.Length > 0)
                base.Write(writer);
            else
            {
                writer.Write(Position);
                writer.Write(UV01);
                writer.Write(UV23);
            }
        }
    }
}
