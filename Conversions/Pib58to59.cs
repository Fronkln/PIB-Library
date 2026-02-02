using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PIBLib
{ 
    internal class Pib58to59
    {
        public static Pib59 Convert(Pib58 pib58)
        {
            Pib59 pib = new Pib59();
            pib58.CopyFields(pib);
            pib.Version = PibVersion.YK3;

            pib.Emitters = new List<BasePibEmitter>();

            foreach (PibEmitterv58 emitter in pib58.Emitters)
                pib.Emitters.Add(emitter);

            return pib;
        }
    }
}
