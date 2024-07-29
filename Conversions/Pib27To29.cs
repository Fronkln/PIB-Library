using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PIBLib.Conversions
{
    internal static class Pib27To29
    {
        public static Pib29 Convert(Pib27 pib27)
        {
            Pib29 pib29 = new Pib29();
            pib27.CopyFields(pib29);
            pib29.Version = PibVersion.Y6;

            pib29.Fade = pib27.Fade;
            pib29.Fade.NearFadeDistance = 0;
            pib29.ColorAnimationR = pib27.ColorAnimationR;
            pib29.ColorAnimationG = pib27.ColorAnimationG;
            pib29.ColorAnimationB = pib27.ColorAnimationB;
            pib29.ColorAnimationI = pib27.ColorAnimationI;
            pib29.ColorTime = 0.0003333333f;
            pib29.IntensityTime = 0.0003333333f;

            pib29.Emitters = new List<BasePibEmitter>();

            foreach(PibEmitterv27 emitter in pib27.Emitters)
            {
                pib29.Emitters.Add(Emitter27To29.Convert(emitter));
            }

            return pib29;
        }
    }
}
