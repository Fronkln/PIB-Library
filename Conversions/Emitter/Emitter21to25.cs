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

            int flag2 = emitter.Flags3;
            int flag3 = emitter.Flags2;

            //Y5 and Y0 YAc0024
            if (flag3.HasFlag(1 << 5))
            {
                flag3 &= ~(1 << 5);
                flag3 |= 1 << 3;
            }

            //Y5 and Y0 YAc0024
            if (flag3.HasFlag(16))
                flag3 &= ~(16);

            emitter.Flags2 = flag2;
            emitter.Flags3 = flag3;

            return emitter;
        }
    }
}
