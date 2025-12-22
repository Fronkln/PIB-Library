using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PIBLib.Conversions
{
    internal static class Emitter58To52
    {
        public static PibEmitterv52 Convert(PibEmitterv58 emitter58)
        {
            PibEmitterv52 emitter = new PibEmitterv52();
            emitter58.CopyFields(emitter);

            //TODO:
           // if (emitter.DDSHeader.TextureFormatFlag >= 12)
                //emitter.DDSHeader.TextureFormatFlag = 8;


            //11.07.2024
            //if LJ to YLAD pib is no longer working. this was the culprit

            //shrink anim curves
            emitter.PropertyAnimationCurve.RemoveRange(7, 4);

            /*
            emitter.TextureImports = new List<TextureImportInfo>();

            foreach (TextureImportInfo inf in emitter58.TextureImports)
                foreach (TextureImportResource res in inf.Resources)
                    emitter.ExtraTextures.Add(res.Name);
            */
  

            return emitter;
        }
    }
}
