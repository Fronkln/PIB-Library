using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{
    //Not a real DDS header pib puts their own shit in there.
    public struct DDS_HEADER
    {
        public char[] Magic;
        public int dwSize;
        public int dwFlags;
        public int TextureFormat;
        public int TextureFormatFlag;
        public byte[] irrelevantDat;

        internal void Read(DataReader reader)
        {
            Magic = reader.ReadChars(4);
            dwSize = reader.ReadInt32();
            dwFlags = reader.ReadInt32();
            TextureFormat = reader.ReadInt32();
            TextureFormatFlag = reader.ReadInt32();
            irrelevantDat = reader.ReadBytes(108);
        }

        internal void Write(DataWriter writer)
        {
            writer.Write(Magic);
            writer.Write(dwSize);
            writer.Write(dwFlags);
            writer.Write(TextureFormat);
            writer.Write(TextureFormatFlag);
            writer.Write(irrelevantDat);
        }
    }
}
