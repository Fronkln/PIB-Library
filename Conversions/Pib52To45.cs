using System;
using System.Collections.Generic;
using System.Reflection;

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

            //1 << 17 was automatically added to every pib in YLAD, including AAa0000, while in JE this wasn't the case.
            pib.Flags = pib.Flags.RemoveFlag(1 << 17);

            PibGlobalFlagsv52 de2Flags = (PibGlobalFlagsv52)pib.Flags;
            int deFlags = 0;

            foreach (Enum flag in de2Flags.GetFlags())
            {
                string flagStr = flag.ToString();

                if(Enum.IsDefined(typeof(PibGlobalFlagsv45), flagStr))
                    deFlags |= System.Convert.ToInt32(Enum.Parse(typeof(PibGlobalFlagsv45), flagStr));
            }

            pib.Flags = (uint)deFlags;

            return pib;
        }
    }
}
