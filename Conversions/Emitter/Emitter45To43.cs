using System;
using System.Reflection;
using System.Collections.Generic;

namespace PIBLib.Conversions
{
    internal static class Emitter45To43
    {
        public static PibEmitterv43 Convert(PibEmitterv45 emitterv45)
        {
            PibEmitterv43 emitter = new PibEmitterv43();
            emitterv45.CopyFields(emitter);

            byte[] trimmedDat = new byte[564];
            Array.Copy(emitterv45.UnknownMainData, trimmedDat, 564);
            emitterv45.UnknownMainData = trimmedDat;

            return emitter;
        }
    }
}
