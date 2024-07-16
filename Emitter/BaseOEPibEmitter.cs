using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{
    //Design failure: Data copy pasted from BaseOOEPibEmitter because OOE has a structure OE doesnt
    public class BaseOEPibEmitter : BasePibEmitter
    {
        /// <summary>
        /// Determines type of data contained
        /// </summary>
        public ushort Type;

        public float OEUnknown1 = 0f;
        public float OEUnknown2 = 0f;
        public float OEUnknown3 = 0f;
        public float OEUnknown4 = 0f;
        public float OEUnknown5 = -1f;
        public float OEUnknownFlag4 = 0f;
        public float OEUnknown7 = 1;
        public float OEUnknown8 = 0f;
        public float OEUnknown9 = 0f;
        public float OEUnknown10 = 0f;
        public float OEUnknown11 = 0f;
        public float OEUnknown12 = 0f;
        public float OEUnknown13 = 0f;
        public float OEUnknown14 = 1f;
        public float OEUnknown15 = 0f;

        protected override void WriteUnknownSection1(DataWriter writer)
        {
            writer.Write(128 + (PropertyAnimationCurve[0].GetDataSize() * PropertyAnimationCurve.Count));
            DDSHeader.Write(writer);

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
