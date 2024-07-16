using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PIBLib.Conversions
{
    internal static class Pib29To27
    {
        public static Pib27 Convert(Pib29 pib29)
        {
            Pib27 pib27 = new Pib27();
            pib29.CopyFields(pib27);
            pib27.Version = PibVersion.Y0;

            pib27.Fade = pib29.Fade;
            pib27.Fade.NearFadeDistance = 0;
            pib27.ColorAnimationR = pib29.ColorAnimationR;
            pib27.ColorAnimationG = pib29.ColorAnimationG;
            pib27.ColorAnimationB = pib29.ColorAnimationB;
            pib27.ColorAnimationI = pib29.ColorAnimationI;

            pib27.Emitters = new List<BasePibEmitter>();

            foreach (PibEmitterv29 emitter in pib29.Emitters)
            {
                pib27.Emitters.Add(Emitter29To27.Convert(emitter));
            }

            return pib27;
        }
    }
}
