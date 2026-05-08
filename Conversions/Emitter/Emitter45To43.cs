using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Yarhl.IO;

namespace PIBLib.Conversions
{
    internal static class Emitter45To43
    {
        public static PibEmitterv43 Convert(PibEmitterv45 emitter45)
        {
            PibEmitterv43 emitter = new PibEmitterv43();
            emitter45.CopyFields(emitter);

            emitter.VAT = new DEPibBaseVAT();
            emitter45.VAT.CopyFields(emitter.VAT);

            EmitterFlag1v45 flags = (EmitterFlag1v45)emitter45.Flags;
            uint v43Flags = 0;

            //Convert old flags to v45 flags
            foreach (Enum flag in flags.GetFlags())
            {
                string flagStr = flag.ToString();
                uint de2Value = System.Convert.ToUInt32(Enum.Parse(typeof(EmitterFlag1v43), flagStr));

                v43Flags |= de2Value;
            }

            emitter.Flags = (int)v43Flags;


            //Convert metaballs to geo vtx 0
            //90% of the metaballs in Y6 use geo vtx type 0
            //geo vtx 1 metaballs in YK2 also look really bad.
            if (emitter.IsMetaball() && emitter.GeoVertex == 1)
            {

                EmitterBaseDataChunk[] chunksPre = emitter.UnknownData1.Cast<EmitterBaseDataChunk>().ToArray();
                EmitterDataChunkType0DE[] newChunks = new EmitterDataChunkType0DE[chunksPre.Length];

                for (int i = 0; i < chunksPre.Length; i++)
                {
                    EmitterDataChunkType1 oldChunk = chunksPre[i] as EmitterDataChunkType1;
                    EmitterDataChunkType0DE chunk = new EmitterDataChunkType0DE();
                    chunk.Position = oldChunk.Position;
                    chunk.UV01 = oldChunk.UV01;
                    chunk.UV23 = oldChunk.UV23;

                    newChunks[i] = chunk;
                }

                //Create data for the expanded data of geo vtx 0
                newChunks[0].Unknown1 = new Vector2();
                newChunks[0].Unknown2 = new Vector2();
                newChunks[0].Unknown3 = new Vector2();

                newChunks[1].Unknown1 = new Vector2(0, 1);
                newChunks[1].Unknown2 = new Vector2(0, 1);
                newChunks[1].Unknown3 = new Vector2(0, 1);

                newChunks[2].Unknown1 = new Vector2(1, 1);
                newChunks[2].Unknown2 = new Vector2(1, 1);
                newChunks[2].Unknown3 = new Vector2(1, 1);

                newChunks[3].Unknown1 = new Vector2(1, 0);
                newChunks[3].Unknown2 = new Vector2(1, 0);
                newChunks[3].Unknown3 = new Vector2(1, 0);

                emitter.GeoVertex = 0;
                emitter.UnknownData1 = newChunks.Cast<EmitterBaseDataChunk>().ToList();
            }

            return emitter;
        }
    }
}
