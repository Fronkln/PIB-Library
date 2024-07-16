using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PIBLib.Conversions
{
    internal static class Emitter21to25
    {
        public static PibEmitterv25 Convert(PibEmitterv21 emitterv21)
        {
            PibEmitterv25 emitter = new PibEmitterv25();
            emitterv21.CopyFields(emitter);

            emitter.Metaball = new PibBaseMetaball();
            emitterv21.Metaball.CopyFields(emitter.Metaball);

            return emitter;
        }
    }
}
