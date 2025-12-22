using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
        public int Flags;
        public int Flags2;
        public int Flags3;

        public byte Blend;
        public byte MetaballBlend;

        public Vector3 AABoxCenter;
        public Vector3 AABoxExtent;

        public PibBaseMetaball Metaball = new PibBaseMetaball();
        public EmitterBaseAnimationData AnimationData = new EmitterBaseAnimationData();
        public PibBaseUnkStructure1 UnkStructure1 = new PibBaseUnkStructure1();
        public PibBaseCommonUnkStructure2 CommonUnkStructure2 = new PibBaseCommonUnkStructure2();

        /// <summary>
        /// The DDS header for the textures in the emitter.
        /// </summary>
        public DDS_HEADER DDSHeader;

        public List<PibEmitterAnimationCurve> PropertyAnimationCurve = new List<PibEmitterAnimationCurve>();

        /// <summary>
        /// Textures used by the emitter.
        /// </summary>
        public List<string> Textures = new List<string>();

        public float MinSpread = 0;
        public float UnkMinSpreadRegVal1 = 0;
        public float UnkMinSpreadRegVal2 = 0;

        public float MaxSpread = 0;
        public float UnkMaxSpreadRegVal1 = 0;

        public Vector3 PositionOffset;

        //Size varies depending on Type
        public List<EmitterBaseDataChunk> UnknownData1 = new List<EmitterBaseDataChunk>();

        public byte[] UnknownMainData;
        public ParticleSource Source;

        public int UnknownFlags_0x8 = 64;

        /// <summary>
        /// On each pib that this isnt 1, reading goes wrong at texture table. FIX FIX FIX! (Count for texture tables?)
        /// <br></br>Responsible for the 7 failed pibs out of 656 on Yakuza 3
        /// </summary>
        public byte UnknownCount_0xC = 1;
        public byte[] Unknown0x10 = new byte[40];


        internal virtual void Read(DataReader reader, PibVersion version)
        {
            throw new System.Exception("Unimplemented read");
        }


        protected virtual void ReadAnimationCurves(DataReader reader, int dataSize)
        {
            PropertyAnimationCurve = new List<PibEmitterAnimationCurve>();

            int numFloats = (dataSize / 4) / GetPropertyAnimationCurveCount();

            PibEmitterAnimationCurveGeneric curve1 = new PibEmitterAnimationCurveGeneric();
            PibEmitterAnimationCurveGeneric curve2 = new PibEmitterAnimationCurveGeneric();
            PibEmitterAnimationCurveGeneric curve3 = new PibEmitterAnimationCurveGeneric();
            PibEmitterAnimationCurveGeneric curve4 = new PibEmitterAnimationCurveGeneric();
            PibEmitterAnimationCurveColor colorCurve = new PibEmitterAnimationCurveColor();
            PibEmitterAnimationCurveGeneric curve6 = new PibEmitterAnimationCurveGeneric();

            curve1.Read(reader, numFloats);
            curve2.Read(reader, numFloats);
            curve3.Read(reader, numFloats);
            curve4.Read(reader, numFloats);
            colorCurve.Read(reader, numFloats);
            curve6.Read(reader, numFloats);

            PropertyAnimationCurve.Add(curve1);
            PropertyAnimationCurve.Add(curve2);
            PropertyAnimationCurve.Add(curve3);
            PropertyAnimationCurve.Add(curve4);
            PropertyAnimationCurve.Add(colorCurve);
            PropertyAnimationCurve.Add(curve6);
        }

        internal virtual int GetPropertyAnimationCurveCount()
        {
            return 6;
        }

        protected virtual void WriteAnimationCurves(DataWriter writer)
        {
            int sizePerCurve = PropertyAnimationCurve[0].GetDataSize();
            int expectedCurveCount = GetPropertyAnimationCurveCount();

            if (PropertyAnimationCurve.Count != GetPropertyAnimationCurveCount())
                throw new System.Exception("Animation curve count mismatch, expected: " + expectedCurveCount + " got: " + PropertyAnimationCurve.Count);

            writer.Write(128 + (sizePerCurve * PropertyAnimationCurve.Count));
            DDSHeader.Write(writer);

            foreach (PibEmitterAnimationCurve animCurve in PropertyAnimationCurve)
                animCurve.Write(writer);
        }

        protected virtual void ReadUnknownData1(DataReader reader, int type, int count, PibVersion version)
        {

        }

        public virtual EmitterType GetEmitterType()
        {
            byte bit = (byte)(Flags >> 6);

            if ((bit & 1) == 1 || (bit & 0x40) == 1)
                return EmitterType.Billboard;

            return EmitterType.Model;
        }

        internal virtual void Write(DataWriter writer, PibVersion version)
        {

        }

        internal virtual int GetUnknownDataCount()
        {
            return UnknownData1.Count;
        }

        public virtual bool IsMetaball()
        {
            return false;
        }

        public virtual bool IsUseColorCurve()
        {
            return false;
        }
        
        public PibEmitterAnimationCurveColor GetColorCurve()
        {
            return (PibEmitterAnimationCurveColor)PropertyAnimationCurve.FirstOrDefault(x => x is PibEmitterAnimationCurveColor);
        }
    }
}
