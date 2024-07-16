using PIBLib.Conversions;
using System;
using Yarhl.IO;

namespace PIBLib
{
    //Inherited from v45: Same header
    public class Pib52 : Pib45
    {
        protected override void ReadEmitters(DataReader reader, int count)
        {
            for (int i = 0; i < count; i++)
            {
                PibEmitterv52 emitter = new PibEmitterv52();
                emitter.Read(reader, Version);
                Emitters.Add(emitter);
            }
        }

        public new Pib45 ToV45()
        {
            return Pib52To45.Convert(this);
        }

        public Pib58 ToV58()
        {
            return Pib52To58.Convert(this);
        }
    }
}
