using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PIBLib
{ 
    internal class Pib58to59
    {
        /// <summary>
        /// Assumes LJ format, Gaiden pibs are already v59, they just didnt update the version number.
        /// </summary>
        /// <param name="pib58"></param>
        /// <returns></returns>
        public static Pib59 Convert(Pib58 pib58)
        {
            Pib59 pib = new Pib59();
            pib58.CopyFields(pib);
            pib.Version = PibVersion.YK3;

            pib.Emitters = new List<BasePibEmitter>();

            foreach (PibEmitterv58 emitter in pib58.Emitters)
            {
                PibEmitterv58 v59Emitter = new PibEmitterv58();
                emitter.CopyFields(v59Emitter);

                for (int i = 0; i < v59Emitter.TextureShaderIndices.Length; i++)
                    v59Emitter.TextureShaderIndices[i] += 2;

                pib.Emitters.Add(v59Emitter);
            }

            return pib;
        }
    }
}
