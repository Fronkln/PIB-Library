using Yarhl.IO;

namespace PIBLib
{
    public class DEPibUV
    {
        public Vector2[] UVSize = new Vector2[4] { new Vector2(), new Vector2(), new Vector2(), new Vector2() };
        public Vector2[] UVFlip = new Vector2[4] { new Vector2(), new Vector2(), new Vector2(), new Vector2() };

        internal void Read(DataReader reader)
        {
            for (int i = 0; i < 4; i++)
                UVSize[i] = reader.ReadVector2();

            for (int i = 0; i < 4; i++)
                UVFlip[i] = reader.ReadVector2();
        }

        internal void Write(DataWriter writer)
        {
            foreach (Vector2 vec in UVSize)
                writer.Write(vec);

            foreach (Vector2 vec in UVFlip)
                writer.Write(vec);
        }
    }
}
