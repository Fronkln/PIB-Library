using System;
using System.Collections.Generic;
using System.Reflection;

namespace PIBLib.Conversions
{
    internal static class Emitter27To25
    {
        public static PibEmitterv25 Convert(PibEmitterv27 emitter27)
        {
            PibEmitterv25 emitter = new PibEmitterv25();
            emitter27.CopyFields(emitter);

            return emitter;
        }
    }
}
