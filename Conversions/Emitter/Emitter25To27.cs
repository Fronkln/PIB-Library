using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Yarhl.IO;

namespace PIBLib.Conversions
{
    internal static class Emitter25To27
    {
        public static PibEmitterv27 Convert(PibEmitterv25 emitterv25)
        {
            PibEmitterv27 emitter = new PibEmitterv27();
            emitterv25.CopyFields(emitter);

            emitter.OEUnkStructure1 = new OEPibUnkStructure1v27();

            return emitter;
        }
    }
}
