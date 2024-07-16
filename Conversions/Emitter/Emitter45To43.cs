using System;
using System.Reflection;
using System.Collections.Generic;
using Yarhl.IO;

namespace PIBLib.Conversions
{
    internal static class Emitter45To43
    {
        public static PibEmitterv43 Convert(PibEmitterv45 emitter45)
        {
            PibEmitterv43 emitter = new PibEmitterv43();
            emitter45.CopyFields(emitter);

            emitter.VAT = new DEPibBaseVAT();
            emitter45.VAT.CopyFields(emitter.VAT);

            EmitterFlag1v45 flags = (EmitterFlag1v45)emitter45.Flags;
            uint v43Flags = 0;

            //Convert old flags to v45 flags
            foreach (Enum flag in flags.GetFlags())
            {
                string flagStr = flag.ToString();
                uint de2Value = System.Convert.ToUInt32(Enum.Parse(typeof(EmitterFlag1v43), flagStr));

                v43Flags |= de2Value;
            }

            emitter.Flags = (int)v43Flags;

            return emitter;
        }
    }
}
