using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIBLib.Conversions
{
    internal static class Emitter21to25
    {
        public static PibEmitterv25 Convert(PibEmitterv21 emitterv21)
        {
            PibEmitterv25 emitter = new PibEmitterv25();

            emitter.Flags = emitterv21.Flags;
            emitter.Unknown_0x4 = emitterv21.Unknown_0x4;
            emitter.UnknownCount_0xC = emitterv21.UnknownCount_0xC;
            emitter.Type = emitterv21.Type;

            emitter.Unknown0x10 = emitterv21.Unknown0x10;

            emitter.UnknownMainData = emitterv21.UnknownMainData;
            
            emitter.DDSHeader = emitterv21.DDSHeader;
            emitter.UnknownSection1 = emitterv21.UnknownSection1;

            emitter.Source = emitterv21.Source;

            emitter.Textures = emitterv21.Textures;
            emitter.UnknownData1 = emitterv21.UnknownData1;

            return emitter;
        }
    }
}
