using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIBLib
{
    public enum EmitterFlag3v43 : int
    {
        eFLG_LIGHTING_ONESHOT = 1 << 0x0,
        eFLG_LIGHTING_G_BUFFER = 1 << 0x1,
        eFLG_LIGHTING_COLOR = 1 << 0x2,
        eFLG_LIGHTING_COLOR_ALL = 1 << 0x3,
        eFLG_LIGHTING_COLOR6 = 1 << 0x4,
        eFLG_LIGHTING_COLOR6_ALL = 1 << 0x5,
        //above part is assumed to be same as v52. copy pasted, flag6 was on same position on both JE and YLAD. yjb0021
        eFLG_LIGHTING_PIXEL = 1 << 0x6,
        eFLG_LIGHTING_VOLUME = 1 << 0x7,
        eFLG_TEX_B_MODULATE = 1 << 0x8,
        eFLG_TEX_B_ADD = 1 << 0x9,
        eFLG_TEX_A2_NORMAL_LIGHT = 1 << 0xA,
        eFLG_TEX_REFLECTION = 1 << 0xB,
        eFLG_TEX_A2_MODULATE = 1 << 0xC,
        eFLG_TEX_A2_ADD = 1 << 0xD,
        eFLG_LIGHTING_EMISSIVE_TEX_A = 1 << 0xE,
        eFLG_TEX_B1_WRAP_CLAMP = 1 << 0xF,
        eFLG_TEX_A2_WRAP_CLAMP = 1 << 0x10,
        eFLG_TEX_A_PATTERN_BLEND = 1 << 0x11,
        eFLG_TEX_B_PATTERN_BLEND = 1 << 0x12,
        eFLG_TEX_A2_PATTERN_BLEND = 1 << 0x13,
        eFLG_VAT_TYPE_FLUID = 1 << 0x14,
        eFLG_VAT_TYPE_SOFT = 1 << 0x15,
        eFLG_VAT_FRAME_LOOP = 1 << 0x16,
        Flag24 = 1 << 0x17,
        Flag25 = 1 << 0x18,
        Flag26 = 1 << 0x19,
        Flag27 = 1 << 0x1A,
        Flag28 = 1 << 0x1B,
        Flag29 = 1 << 0x1C,
        Flag30 = 1 << 0x1D,
        Flag31 = 1 << 0x1E,
        Flag32 = 1 << 0x1F
    }
}
