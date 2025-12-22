using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PIBLib.Conversions
{
    internal static class Emitter25To21
    {
        public static PibEmitterv21 Convert(PibEmitterv25 emitter25)
        {
            PibEmitterv21 emitter = new PibEmitterv21();
            emitter25.CopyFields(emitter);

            emitter.Metaball = new PibBaseMetaball();
            emitter25.Metaball.CopyFields(emitter.Metaball);

            int flag2 = emitter.Flags3;
            int flag3 = emitter.Flags2;

            emitter.Flags2 = flag2;
            emitter.Flags3 = flag3;


            return emitter;
        }
    }
}
