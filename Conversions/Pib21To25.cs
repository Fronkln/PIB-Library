using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace PIBLib.Conversions
{
    internal static class Pib21To25
    {
        public static Pib25 Convert(Pib21 pib21)
        {
            Pib25 pib = new Pib25();
            pib.Version = PibVersion.Ishin;


            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 26; j++)
                    pib.UnkFloats[i, j] = 1;

            pib.ParticleID = pib21.ParticleID;
            pib.Duration = pib21.Duration;
            pib.Unknown1 = pib21.Unknown1;

            pib.Speed = pib21.Speed;
            pib.Unknown2 = pib21.Unknown2;
            pib.Unknown3 = pib21.Unknown3;

            pib.Unknown4 = pib21.Unknown4;
            pib.Unknown5 = pib21.Unknown5;

            pib.UnknownFlags_0x3E = pib21.UnknownFlags_0x3E;

            pib.BaseMatrix = pib21.BaseMatrix;
            pib.Scale = pib21.Scale;

            foreach (PibEmitterv21 emitter in pib21.Emitters)
                pib.Emitters.Add(Emitter21to25.Convert(emitter));

            return pib;
        }
    }
}
