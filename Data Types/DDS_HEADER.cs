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
        public int dwHeight;
        public int dwWidth;
        public int dwPitchOrLinearSize;
        public int dwDepth;
        public int dwMipMapCount;
        public int[] dwReserved1;
        public byte[] ddspf; //DDS PIXELFORMAT 
        public int dwCaps;
        public int dwCaps2;
        public int dwCaps3;
        public int dwCaps4;
        public int dwReserved2;

        internal void Read(DataReader reader)
        {
            Magic = reader.ReadChars(4);
            dwSize = reader.ReadInt32();
            dwFlags = reader.ReadInt32();
            dwHeight = reader.ReadInt32();
            dwWidth = reader.ReadInt32();
            dwPitchOrLinearSize = reader.ReadInt32();
            dwDepth = reader.ReadInt32();
            dwMipMapCount = reader.ReadInt32();

            dwReserved1 = new int[11];
            for (int i = 0; i < 11; i++)
                dwReserved1[i] = reader.ReadInt32();

            ddspf = reader.ReadBytes(36);

            dwCaps = reader.ReadInt32();
            dwCaps2 = reader.ReadInt32();
            dwCaps3 = reader.ReadInt32();
            dwCaps4 = reader.ReadInt32();
        }

        internal void Write(DataWriter writer)
        {
            writer.Write(Magic);
            writer.Write(dwSize);
            writer.Write(dwFlags);
            writer.Write(dwHeight);
            writer.Write(dwWidth);
            writer.Write(dwPitchOrLinearSize);
            writer.Write(dwDepth);
            writer.Write(dwMipMapCount);

            for (int i = 0; i < dwReserved1.Length; i++)
                writer.Write(dwReserved1[i]);

            writer.Write(ddspf);

            writer.Write(dwCaps);
            writer.Write(dwCaps2);
            writer.Write(dwCaps3);
            writer.Write(dwCaps4);
        }
    }
}
