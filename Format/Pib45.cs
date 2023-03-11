using PIBLib.Conversions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{
    //Changed: Emitter main data (564 > 596 bytes)
    public class Pib45 : Pib43
    {
        protected override void ReadEmitters(DataReader reader, int count)
        {
            for (int i = 0; i < count; i++)
            {
                PibEmitterv45 emitter = new PibEmitterv45();
                emitter.Read(reader, Version);
                Emitters.Add(emitter);
            }
        }

        public Pib43 ToV43()
        {
            return Pib45to43.Convert(this);
        }
    }
}
