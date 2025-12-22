using System;
using System.Collections.Generic;
using Yarhl.IO;

namespace PIBLib
{
    public class BasePib
    {
        public byte Unk;
        public bool IsBigEndian;
        public PibVersion Version;
        public string Name; //Just to keep it friendlier, isnt actually in Pib file

        public uint ParticleID;
        public uint EmitterCount;
        public uint Duration; //In game ticks
        public int DurationOffset;
        public float Speed;
        public float ForwardOffset;
        public float MaxIntensity;
        public float Radius;
        public float Range;

        public uint Flags;

        //Variables that are scattered in various places in pib versions
        //Will be read by their respective class
        public Matrix4x4 BaseMatrix;
        public Vector3 Scale;

        public PibFadeModule Fade = new PibFadeModule();

        public Vector4 UnknownVector;

        public List<BasePibEmitter> Emitters = new List<BasePibEmitter>();

        /// <summary>
        /// Return all textures used by pib emitters.
        /// </summary>
        public string[] AllTextures
        {
            get
            {
                List<string> textures = new List<string>();

                foreach (var emitter in Emitters)
                    foreach (string str in emitter.Textures)
                    {
                        string texFile = str;
                        if (!texFile.EndsWith(".dds"))
                            texFile += ".dds";

                        if (!textures.Contains(texFile))
                            textures.Add(texFile);
                    }

                return textures.ToArray();
            }
        }

        internal virtual void Read(DataReader reader)
        {
            ReadCorePibData(reader);
        }

        protected virtual void ReadCorePibData(DataReader reader)
        {

        }

        protected virtual void ReadEmitters(DataReader reader, int count)
        {

        }


        protected virtual void WriteHeader(DataWriter writer)
        {
        }

        internal virtual void Write(DataWriter writer)
        {
            WriteHeader(writer);
        }
    }
}
