using System;
using Yarhl.IO;

namespace PIBLib
{
    public class DEPibTrackv43 : DEPibBaseTrack
    {
        public uint PosNum;
        public float FollowRate;
        public int UpdateLife;
        public float NoiseScale;
        public float NoiseVecScale;
        public float NoiseUpSpeed;

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
