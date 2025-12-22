using System;
using Yarhl.IO;

namespace PIBLib
{
    public class DEPibTrackv43 : DEPibBaseTrack
    {
        public uint PosNum = 11;
        public float FollowRate = 0;
        public int UpdateLife;
        public float NoiseScale = 0.5f;
        public float NoiseVecScale = 1f;
        public float NoiseUpSpeed = 0.2f;

        internal override void Read(DataReader reader)
        {
            FadeLength = reader.ReadSingle();
            DivNum = reader.ReadUInt32();
            PosNum = reader.ReadUInt32();
            FollowRate = reader.ReadSingle();
            UpdateLife = reader.ReadInt32();
            NoiseScale = reader.ReadSingle();
            NoiseVecScale = reader.ReadSingle();
            NoiseUpSpeed = reader.ReadSingle();
        }

        internal override void Write(DataWriter writer)
        {
            writer.Write(FadeLength);
            writer.Write(DivNum);
            writer.Write(PosNum);
            writer.Write(FollowRate);
            writer.Write(UpdateLife);
            writer.Write(NoiseScale);
            writer.Write(NoiseVecScale);
            writer.Write(NoiseUpSpeed);
        }
    }
}
