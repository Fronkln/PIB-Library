using PIBLib.Conversions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{
    //Everything same as Pib52, emitter grew by 32 bytes, flags changed
    public class Pib58 : Pib52
    {
        protected override void ReadEmitters(DataReader reader, int count)
        {
            for(int i = 0; i < count; i++)
            {
                PibEmitterv58 emitter = new PibEmitterv58();
                emitter.Read(reader, Version);
                Emitters.Add(emitter);
            }
        }
        
        public Pib52 ToV52()
        {
            return Pib58To52.Convert(this);
        }
    }
}
