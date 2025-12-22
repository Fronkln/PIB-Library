using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIBLib
{
    //First 7 flags match with DE1. unsure about remaining, AAa0000
    public enum EmitterFlag2v45 : int
    {
        eFLG_FLOW = 1 << 0x0,
        eFLG_DRAW_SHADOW = 1 << 0x1,
        eFLG_METABALL_HIGH_LIGHT = 1 << 0x2,
        eFLG_FIX_AXIS = 1 << 0x3,
        eFLG_ALPHA_THRESHOLD = 1 << 0x4,
        eFLG_SHADOW = 1 << 0x5,
        eFLG_NEAR_FADE = 1 << 0x6,
        eFLG_VTX_QUAD = 1 << 0x7,
        eFLG_COLOR_BLEND_MULTIPLE = 1 << 0x8,
        eFLG_SORT = 1 << 0x9,
        Flag11 = 1 << 0xA,
        eFLG_BLUR = 1 << 0xB,
        eFLG_ATTENUATION_SPEED_MINUS = 1 << 0xC,
        eFLG_COLLISION = 1 << 0xD,
        eFLG_SCALE_ANIM_MULTIPLE = 1 << 0xE,
        eFLG_CHECK_DRAW_MASK = 1 << 0xF,
        eFLG_BLUR_MULTI = 1 << 0x10,
        eFLG_VECTOR_FIELD = 1 << 0x11,
        eFLG_VECTOR_FIELD_ANIM = 1 << 0x12,
        eFLG_VECTOR_FIELD_LIFE = 1 << 0x13,
        eFLG_RIBON = 1 << 0x14,
        eFLG_TRACK = 1 << 0x15,
        eFLG_TRACK_CROSS = 1 << 0x16,
        eFLG_TRACK_OVERWRITE = 1 << 0x17,
        eFLG_TRACK_NOISE = 1 << 0x18,
        eFLG_CURL_NOISE = 1 << 0x19,
        eFLG_TIME_FADE = 1 << 0x1A,
        eFLG_GATHER = 1 <<  0x1B,
        Flag28 = 1 << 28,
        Flag29 = 1 << 29,
        Flag30 = 1 << 30,
        Flag31 = 1 << 31,
    }
}
