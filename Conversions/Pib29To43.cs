using System.Collections.Generic;
using System.Reflection;

namespace PIBLib.Conversions
{
    internal class Pib29To43
    {
        public static Pib43 Convert(Pib29 pib29)
        {
            Pib43 pib = new Pib43();
            pib29.CopyFields(pib);
            pib.Version = PibVersion.Y6;
            pib.Emitters = new List<BasePibEmitter>();

            foreach (PibEmitterv29 emitter in pib29.Emitters)
            {
                pib.Emitters.Add(Emitter29To43.Convert(emitter));
            }

            return pib;
        }
    }
}
