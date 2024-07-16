using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIBLib
{
    public class TextureImportInfo
    {
        public byte[] Data = new byte[32];
        public List<TextureImportResource> Resources = new List<TextureImportResource>();
    }
}
