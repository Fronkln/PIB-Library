using System;
using System.Collections.Generic;
using System.Reflection;

namespace PIBLib.Conversions
{
    internal static class Pib43To29
    {
        public static Pib29 Convert(Pib43 pib43)
        {
            Pib29 pib = new Pib29();

            pib43.CopyFields(pib);
            pib.Version = PibVersion.Y6;
            pib.Emitters = new List<BasePibEmitter>();

            foreach(PibEmitterv43 emitter in pib43.Emitters)
            {
                pib.Emitters.Add(Emitter43to29.Convert(emitter));
            }

            return pib;
        }
    }
}
