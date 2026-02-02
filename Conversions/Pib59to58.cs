using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PIBLib
{
    internal class Pib59To58
    {
        public static Pib58 Convert(Pib59 pib59)
        {
            Pib58 pib = new Pib58();
            pib59.CopyFields(pib);
            pib.Version = PibVersion.Gaiden;

            pib.Emitters = new List<BasePibEmitter>();

            foreach (PibEmitterv58 emitter in pib59.Emitters)
                pib.Emitters.Add(emitter);

            return pib;
        }
    }
}
