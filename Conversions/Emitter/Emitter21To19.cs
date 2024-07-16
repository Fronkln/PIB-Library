using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib.Conversions
{
    internal static class Emitter21To19
    {
        public static PibEmitterv19 Convert(PibEmitterv21 emitter21)
        {
            PibEmitterv19 pibEmitter = new PibEmitterv19();
            emitter21.CopyFields(pibEmitter);

            pibEmitter.Metaball = new PibBaseMetaball();
            emitter21.Metaball.CopyFields(pibEmitter.Metaball);

            if (pibEmitter.OOEUnkStructure6.Unknown1 == 5)
                pibEmitter.OOEUnkStructure6.Unknown1 = 6;

            if (pibEmitter.OOEUnkStructure6.Flag4.HasFlag(64))
            {
                pibEmitter.OOEUnkStructure6.Flag4 &= ~64;
                pibEmitter.OOEUnkStructure6.Flag4 |= 16;
            }

            pibEmitter.OOEUnkStructure6.Unknown3 = -0.01666667f;

            return pibEmitter;
        }
    }
}
