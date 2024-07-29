using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PIBLib.Conversions
{
    internal static class Pib21To25
    {
        public static Pib25 Convert(Pib21 pib21)
        {
            Pib25 pib = new Pib25();
            pib.Version = PibVersion.Ishin;

            pib21.CopyFields(pib);


            for(int i = 0; i < pib.ColorAnimationR.Length; i++)
            {
                pib.ColorAnimationR[i] = 1f;
                pib.ColorAnimationG[i] = 1f;
                pib.ColorAnimationB[i] = 1f;
                pib.ColorAnimationI[i] = 1f;
            }

            int flags = pib21.UnknownFlags_0x3E;

            if (flags.HasFlag(1 << 9))
            {
                flags = flags.SetFlag(1 << 10);
                flags = flags.RemoveFlag(1 << 9);
            }

            pib.Emitters = new List<BasePibEmitter>();

            foreach (PibEmitterv21 emitter in pib21.Emitters)
                pib.Emitters.Add(Emitter21to25.Convert(emitter));

            return pib;
        }
    }
}
