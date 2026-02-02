using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{
    public class BaseOOEPibEmitter : BasePibEmitter
    {
        /// <summary>
        /// Determines type of data contained
        /// </summary>
        public ushort Type;
        public float OEUnknown6 = 1;
        public float OEUnknown7 = 1;
        public float[] TextureAnimationSpeed = new float[2];
        public float[] TextureUnknown = new float[2];

        public float OEUnknown16 = 0f;

        public OOEPibBaseUnkStructure6 OOEUnkStructure6 = new OOEPibBaseUnkStructure6();
        public OOEPibBaseUnkStructure2 OOEUnkStructure2 = new OOEPibBaseUnkStructure2();
        public OOEPibBaseUnkStructure3 OOEUnkStructure3 = new OOEPibBaseUnkStructure3();
        public OOEPibBaseUnkStructure4 OOEUnkStructure4 = new OOEPibBaseUnkStructure4();
        public OOEPibBaseUnkStructure5 OOEUnkStructure5 = new OOEPibBaseUnkStructure5();

        public Vector3 UnkRegion1;

        public float UnkMaxSpreadRegVal2 = 0;

        protected override void WriteAnimationCurves(DataWriter writer)
        {
            writer.Write(128 + (PropertyAnimationCurve[0].GetDataSize() * PropertyAnimationCurve.Count));
            writer.Endianness = EndiannessMode.LittleEndian;

            DDSHeader.Write(writer);

            foreach (PibEmitterAnimationCurve animCurve in PropertyAnimationCurve)
                animCurve.Write(writer);

            writer.Endianness = EndiannessMode.BigEndian;
        }

        protected override void ReadUnknownData1(DataReader reader, int type, int count, PibVersion version)
        {
            for (int i = 0; i < count; i++)
            {
                switch (Type)
                {
                    default:
                        throw new System.Exception("Unknown data type: " + Type);
                    case 0:
                        EmitterDataChunkType0 t0 = new EmitterDataChunkType0();
                        t0.Read(reader, version);
                        UnknownData1.Add(t0);
                        break;
                    case 1:
                        EmitterDataChunkType1 t1 = new EmitterDataChunkType1();
                        t1.Read(reader, version);
                        UnknownData1.Add(t1);
                        break;
                    case 2:
                        EmitterDataChunkType2 t2 = new EmitterDataChunkType2();
                        t2.Read(reader, version);
                        UnknownData1.Add(t2);
                        break;
                    case 3:
                        EmitterDataChunkType3 t3 = new EmitterDataChunkType3();
                        t3.Read(reader, version);
                        UnknownData1.Add(t3);
                        break;
                    case 4:
                        EmitterDataChunkType4 t4 = new EmitterDataChunkType4();
                        t4.Read(reader, version);
                        UnknownData1.Add(t4);
                        break;
                    case 5:
                        EmitterDataChunkType5 t5 = new EmitterDataChunkType5();
                        t5.Read(reader, version);
                        UnknownData1.Add(t5);
                        break;
                    case 6:
                        EmitterDataChunkType6 t6 = new EmitterDataChunkType6();
                        t6.Read(reader, version);
                        UnknownData1.Add(t6);
                        break;
                }
            }
        }
    }
}
