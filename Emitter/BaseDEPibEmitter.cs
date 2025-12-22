using System;
using Yarhl.IO;

namespace PIBLib
{
    public class BaseDEPibEmitter : BasePibEmitter
    {
        public byte GeoVertex;
        public byte InsVertex;

        public DEPibEmitterBlurModule Blur = new DEPibEmitterBlurModule();

        public float Gravity = 0;

        public float ShadowRate = -1;

        public float DirectivityH = -1;
        public float DirectivityV = -1;
        public float DirectivityPower = -1;

        public uint Culling = 2;
        public float LightScale = 1;
        public float LightRatio = 1;
        public float Glare = 1;

        public float CollisionRestitution = 0.5f;
        public float CollisionFriction = 0.5f;
        public float VecScale = 1;
        public float NormalScale = 1;

        public DEPibBaseTrack Track = new DEPibBaseTrack();

        //DE 1.0: Size stored in Geo VTX 3, up to UV3
        //DE 2.0: Removed from Geo VTX, its own structure, also added flip
        public DEPibUV UV = new DEPibUV();

        public OOEPibBaseUnkStructure2 UnkStructure2 = new OOEPibBaseUnkStructure2();
        public DEPibBaseUnkStructure3 UnkStructure3 = new DEPibBaseUnkStructure3();

        public float UnkVal1 = 0;
        public float UnkVal2 = 0;
        public float UnkVal3 = 0;
        public float UnkVal4 = 0;
        public float UnkVal5 = 0;


        public DEPibUnkStructure6 UnkStructure6 = new DEPibUnkStructure6();

        public BaseDEPibEmitter() : base()
        {
            Metaball = new DEPibBaseMetaball();
        }

        protected override void ReadUnknownData1(DataReader reader, int type, int count, PibVersion version)
        {

            for (int i = 0; i < count; i++)
            {
                EmitterBaseDataChunk chunk = new EmitterBaseDataChunk();

                switch(type)
                {
                    default:
                        chunk = new EmitterDataChunkType1();
                        break;
                    case 0:
                        if (version < PibVersion.YLAD)
                            chunk = new EmitterDataChunkType0DE();
                        else
                            chunk = new EmitterDataChunkType1();
                        break;
                }

                chunk.Read(reader, version);
                UnknownData1.Add(chunk);
            }
        }
    }
}
