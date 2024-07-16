using System;
using System.Reflection;

namespace PIBLib.Conversions
{
    public static class Emitter43To45
    {
        public static PibEmitterv45 Convert(PibEmitterv43 emitter43)
        {
            PibEmitterv45 emitter = new PibEmitterv45();
            emitter43.CopyFields(emitter);

            emitter.VAT = new DEPibVATv45();
            emitter43.VAT.CopyFields(emitter.VAT);

            EmitterFlag1v43 flags = (EmitterFlag1v43)emitter.Flags;
            uint de2Flags = 0;

            //Doesnt exist in JE at a first glance? yyj0056 in Y6, YK2 and JE
            flags &= ~EmitterFlag1v43.eFLG_V27_FLAG;

            //Convert old flags to v45 flags
            foreach (Enum flag in flags.GetFlags())
            {
                string flagStr = flag.ToString();
                uint de2Value = System.Convert.ToUInt32(Enum.Parse(typeof(EmitterFlag1v45), flagStr));

                de2Flags |= de2Value;
            }

            emitter.Flags = (int)de2Flags;

            return emitter;
        }
    }
}
