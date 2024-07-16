using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PIBLib.Conversions
{
    internal static class Pib58To52
    {
        public static Pib52 Convert(Pib58 pib58)
        {
            Pib52 pib = new Pib52();
            pib58.CopyFields(pib);
            pib.Version = PibVersion.YLAD;

            pib.Emitters = new List<BasePibEmitter>();

            foreach (PibEmitterv58 emitter in pib58.Emitters)
                pib.Emitters.Add(Emitter58To52.Convert(emitter));

            return pib;
        }
    }
}
