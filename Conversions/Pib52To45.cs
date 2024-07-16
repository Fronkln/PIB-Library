using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PIBLib.Conversions
{
    public static class Pib52To45
    {
        public static Pib45 Convert(Pib52 pib52)
        {
            Pib45 pib = new Pib45();
            pib52.CopyFields(pib);
            pib.Version = PibVersion.JE;

            pib.Emitters = new List<BasePibEmitter>();

            foreach (PibEmitterv52 emitter in pib52.Emitters)
            {
                pib.Emitters.Add(Emitter52To45.Convert(emitter));
            }

            return pib;
        }
    }
}
