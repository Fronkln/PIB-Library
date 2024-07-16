using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PIBLib.Conversions
{
    internal static class Pib43To45
    {
        public static Pib45 Convert(Pib43 pib43)
        {
            Pib45 pib = new Pib58();
            pib43.CopyFields(pib);
            pib.Version = PibVersion.JE;

            pib.Emitters = new List<BasePibEmitter>();

            foreach (PibEmitterv43 emitter in pib43.Emitters)
                pib.Emitters.Add(Emitter43To45.Convert(emitter));

            return pib;
        }
    }
}
