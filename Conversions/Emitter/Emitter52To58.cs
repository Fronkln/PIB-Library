using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PIBLib.Conversions
{
    internal static class Emitter52To58
    {
        public static PibEmitterv52 Convert(PibEmitterv52 emitter52)
        {
            PibEmitterv58 emitter = new PibEmitterv58();
            emitter52.CopyFields(emitter);

            foreach (string texture in emitter52.Textures)
            {
                emitter.TextureImports.Add(new TextureImportInfo());
            }


            int resID = 1;
            foreach (string texture in emitter52.ExtraTextures)
            {
                emitter.TextureImports[emitter.TextureImports.Count - 1].Resources.Add(new TextureImportResource() { ID = resID, Name = texture });
                resID++;
            }

            return emitter;
        }
    }
}
