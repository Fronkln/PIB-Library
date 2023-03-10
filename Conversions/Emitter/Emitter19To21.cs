using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Yarhl.IO;

namespace PIBLib.Conversions
{
    internal static class Emitter19To21
    {
        public static PibEmitterv21 Convert(PibEmitterv19 emitter19)
        {
            PibEmitterv21 pibEmitter = new PibEmitterv21();

            pibEmitter.Flags = emitter19.Flags;
            
            pibEmitter.Unknown_0x4 = emitter19.Unknown_0x4;

            pibEmitter.UnknownCount_0xC = emitter19.UnknownCount_0xC;
            pibEmitter.Type = emitter19.Type;
            pibEmitter.Unknown0x10 = emitter19.Unknown0x10;
            pibEmitter.UnknownMainData = emitter19.UnknownMainData;
            
            pibEmitter.DDSHeader = emitter19.DDSHeader;
            pibEmitter.UnknownSection1 = emitter19.UnknownSection1;
            pibEmitter.Source = emitter19.Source;
            pibEmitter.Textures = emitter19.Textures;
            pibEmitter.UnknownData1 = emitter19.UnknownData1;



            using (DataStream stream = DataStreamFactory.FromMemory())
            {
                DataWriter writer = new DataWriter(stream) { Endianness = EndiannessMode.BigEndian};
                writer.Write(pibEmitter.UnknownMainData);
                writer.Stream.Position = 56;
                writer.Insert(16);
                writer.Write(new Vector4(-1, -1, -1, 0));

                pibEmitter.UnknownMainData = writer.Stream.ToArray();
            }

         
            return pibEmitter;
        }
    }
}
