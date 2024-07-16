using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Yarhl.IO;
using PIBLib;
using System.Reflection;

namespace PIBLib.Conversions
{
    internal static class Emitter19To21
    {
        public static PibEmitterv21 Convert(PibEmitterv19 emitter19)
        {
            PibEmitterv21 pibEmitter = new PibEmitterv21();

            emitter19.CopyFields(pibEmitter);

            //This will freeze with blood/liquid pibs
            //Removing it causes it to appear white, but we dont have much choice for now. Its better than a freeze.
            //Related causes are: Flags, billboard data
            //Yakuza 3: YYc0002
            //Yakuza 5 converted not-freezing counterpart: QQz0007
            // if (pibEmitter.Flags.HasFlag(1 << 23))
            // pibEmitter.Flags = pibEmitter.Flags.RemoveFlag(1 << 23);

            if (pibEmitter.Flags.HasFlag(1 << 23))
            {
                pibEmitter.Flags |= 1 << 8;
                pibEmitter.Flags = pibEmitter.Flags.RemoveFlag(1 << 2);
                pibEmitter.Flags |= 1 << 3;
                pibEmitter.Flags |= 1 << 4;

                pibEmitter.OOEUnkStructure6.Flag4 = 0;

            }

            return pibEmitter;
        }
    }
}
