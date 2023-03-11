using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{
    //Inherited from 29: Identical header
    public class Pib43 : Pib29
    {

        protected override void ReadEmitters(DataReader reader, int count)
        {
            for (int i = 0; i < count; i++)
            {
                PibEmitterv43 emitter = new PibEmitterv43();
                emitter.Read(reader, Version);
                Emitters.Add(emitter);
            }
        }
    }
}
