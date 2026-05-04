using PIBLib.Conversions;
using Yarhl.IO;

namespace PIBLib
{
    public class Pib21 : Pib19
    {
        public Pib19 ToV19()
        {
            return Pib21To19.Convert(this);
        }

        public Pib25 ToV25()
        {
            return Pib21To25.Convert(this);
        }

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
