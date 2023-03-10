using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Runtime.CompilerServices;
using Yarhl.IO;

namespace PIBLib
{
    public class Pib21 : Pib19
    {
        protected override void ReadEmitters(DataReader reader, int count)
        {
            for (int i = 0; i < count; i++)
            {
                PibEmitterv21 emitter = new PibEmitterv21();
                emitter.Read(reader, Version);
                Emitters.Add(emitter);
            }
        }
    }
}
