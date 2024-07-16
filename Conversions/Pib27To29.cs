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

            pib29.Unknown_0x23C = 1f;
            pib29.Unknown_0x240 = new byte[16];
            pib29.Unknown_0x29C = new byte[24];
            pib29.Unknown0x2B4 = pib27.UnknownVector_0xB4;
            pib29.UnkFloats = pib27.UnkFloats;

            pib29.Emitters = new List<BasePibEmitter>();

            foreach(PibEmitterv27 emitter in pib27.Emitters)
            {
                pib29.Emitters.Add(Emitter27To29.Convert(emitter));
            }

            return pib29;
        }
    }
}
