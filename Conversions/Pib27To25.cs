using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PIBLib.Conversions
{
    internal static class Pib27To25
    {
        public static Pib25 Convert(Pib27 pib27)
        {
            Pib25 pib = new Pib25();
            pib.Version = PibVersion.Ishin;

            pib27.CopyFields(pib);

            pib.Emitters = new List<BasePibEmitter>();

            foreach (PibEmitterv27 emitter in pib27.Emitters)
                pib.Emitters.Add(Emitter27To25.Convert(emitter));

            return pib;
        }
    }
}
