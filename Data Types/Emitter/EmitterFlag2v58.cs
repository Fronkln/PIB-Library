using System;

namespace PIBLib
{
    //Shifted by 1 starting from **ATLEAST** 1 << 21 of V52, 1 << 0xE seems to match
    public enum EmitterFlag2v58 : ulong
    {
        eFLG_FLOW = (ulong)1 << 0x0,
        eFLG_DRAW_SHADOW = (ulong)1 << 0x1,
        eFLG_METABALL_HIGH_LIGHT = (ulong)1 << 0x2,
        eFLG_FIX_AXIS = (ulong)1 << 0x3,
        eFLG_ALPHA_THRESHOLD = (ulong)1 << 0x4,
        eFLG_SHADOW = (ulong)1 << 0x5,
        eFLG_NEAR_FADE = (ulong)1 << 0x6,
        eFLG_VTX_QUAD = (ulong)1 << 0x7,
        eFLG_COLOR_BLEND_MULTIPLE = (ulong)1 << 0x8,
        eFLG_SORT = (ulong)1 << 0x9,
        eFLG_BLUR = (ulong)1 << 0xA,
        eFLG_ATTENUATION_SPEED = (ulong)1 << 0xB,
        eFLG_ATTENUATION_SPEED_MINUS = (ulong)1 << 0xC,
        eFLG_COLLISION = (ulong)1 << 0xD,
        eFLG_SCALE_ANIM_MULTIPLE = (ulong)1 << 0xE,
        eFLG_CHECK_DRAW_MASK = (ulong)1 << 0xF,
        eFLG_BLUR_MULTI = (ulong)1 << 0x10,
        eFLG_VECTOR_FIELD = (ulong)1 << 0x11,
        eFLG_VECTOR_FIELD_ANIM = (ulong)1 << 0x12,
        eFLG_VECTOR_FIELD_LIFE = (ulong)1 << 0x13,
        eFLG_RIBON = (ulong)1 << 0x14,
        eFLG_TRACK = (ulong)1 << 22,
        eFLG_TRACK_CROSS = (ulong)1 << 23,
        eFLG_TRACK_OVERWRITE = (ulong)1 << 24,
        eFLG_TRACK_NOISE = (ulong)1 << 25,
        eFLG_CURL_NOISE = (ulong)1 << 26,
        eFLG_TIME_FADE = (ulong)1 << 27,
        eFLG_GATHER = (ulong)1 <<  28,
        Flag28 = (ulong)1 << 29,
        Flag29 = (ulong)1 << 30,
        Flag30 = (ulong)1 << 31,
        Flag31 = (ulong)1 << 32,
    }
}
