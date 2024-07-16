using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace PIBLib.Conversions
{
    internal static class Pib19To21
    {
        public static Pib21 Convert(Pib19 pib19)
        {
            Pib21 pib = new Pib21();
            pib.Version = PibVersion.Y5;

            pib19.CopyFields(pib);

            pib.UnknownFlags_0x3E = 512;

            pib.Fade.NearFadeDistanceAll = -1;
            pib.Fade.NearFadeOffsetAll = -1;

            pib.Emitters = new List<BasePibEmitter>();

            foreach (PibEmitterv19 emitter in pib19.Emitters)
                pib.Emitters.Add(Emitter19To21.Convert(emitter));

            return pib;
        }
    }
}
