using System;
using System.Reflection;
using System.Collections.Generic;
using Yarhl.IO;

namespace PIBLib.Conversions
{
    internal static class Emitter45To43
    {
        public static PibEmitterv43 Convert(PibEmitterv45 emitterv45)
        {
            PibEmitterv43 emitter = new PibEmitterv43();
            emitterv45.CopyFields(emitter);


            //PERFORMING SURGERY ON A PIB (NO WAY!) (realigning data to not crash YK2)
            List<byte> trimmedDatList = new List<byte>(emitterv45.UnknownMainData);
            trimmedDatList.RemoveRange(208, 4);
            trimmedDatList.RemoveRange(216, 4);
            trimmedDatList.RemoveRange(220, 16);
            trimmedDatList.RemoveRange(244, 8);

            using (DataStream stream = DataStreamFactory.FromMemory())
            {
                DataWriter writer = new DataWriter(stream) { Endianness = EndiannessMode.LittleEndian };
                writer.Write(trimmedDatList.ToArray());
                writer.Stream.Position = 132;
                writer.Write(1f);

                emitter.UnknownMainData = writer.Stream.ToArray();
            }

            return emitter;
        }
    }
}
