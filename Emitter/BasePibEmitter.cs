using System.Collections.Generic;
using System.Drawing;
using System.Reflection.PortableExecutable;
using Yarhl.IO;

namespace PIBLib
{
    public enum EmitterType
    {
        Model = 0,
        Billboard = 1
    }

    public class BasePibEmitter
    {
        /// <summary>
        /// Determines emitter type
        /// </summary>
        public uint Flags;
        /// <summary>
        /// Determines type of data contained
        /// </summary>
        public byte Type;

        /// <summary>
        /// The DDS header for the texturess in the emitter.
        /// </summary>
        public byte[] DDSHeader;

        /// <summary>
        /// Constant size, never changes, checked on the smallest and biggest Yakuza 3 pib
        /// </summary>
        public float[] UnknownSection1;

        /// <summary>
        /// Textures used by the emitter.
        /// </summary>
        public List<string> Textures = new List<string>();

        //Size varies depending on Type
        public byte[] UnknownData1;

        public byte[] UnknownMainData;
        public ParticleSource Source;

        public byte[] Unknown_0x4 = new byte[8];

        /// <summary>
        /// On each pib that this isnt 1, reading goes wrong at texture table. FIX FIX FIX! (Count for texture tables?)
        /// <br></br>Responsible for the 7 failed pibs out of 656 on Yakuza 3
        /// </summary>
        public byte UnknownCount_0xC = 1;
        public byte[] Unknown0x10 = new byte[36];


        internal virtual void Read(DataReader reader, PibVersion version)
        {
            throw new System.Exception("Unimplemented read");
        }

        protected virtual void ReadUnknownData1(DataReader reader, int type, int count)
        {
            switch (Type)
            {
                default:
                    throw new System.Exception("Unknown data type: " + Type);
                case 0:
                    UnknownData1 = reader.ReadBytes(32 * count);
                    break;
                case 1:
                    UnknownData1 = reader.ReadBytes(44 * count);
                    break;
                case 2:
                    UnknownData1 = reader.ReadBytes(20 * count);
                    break;
                case 3:
                    UnknownData1 = reader.ReadBytes(40 * count);
                    break;
                case 4:
                    UnknownData1 = reader.ReadBytes(52 * count);
                    break;
                case 5:
                    UnknownData1 = reader.ReadBytes(28 * count);
                    break;
            }
        }

        public virtual EmitterType GetEmitterType()
        {
            byte bit = (byte)(Flags >> 6);

            if ((bit & 1) == 1 || (bit & 0x40) == 1)
                return EmitterType.Billboard;

            return EmitterType.Model;
        }

        internal virtual void Write(DataWriter writer)
        {

        }

        internal virtual int GetUnknownDataCount()
        {

            switch (Type)
            {
                default:
                    throw new System.Exception("Unknown data type: " + Type);
                case 0:
                    return UnknownData1.Length / 32;
                case 1:
                    return UnknownData1.Length / 44;
                case 2:
                    return UnknownData1.Length / 20;
                case 3:
                    return UnknownData1.Length / 40;
                case 4:
                    return UnknownData1.Length / 52;
                case 5:
                    return UnknownData1.Length / 28;
            }
        }
    }
}
