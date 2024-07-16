﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIBLib
{
    public enum EmitterFlag3v52 : long
    {
        eFLG_LIGHTING_ONESHOT = (long)1 << 0x0,
        eFLG_LIGHTING_G_BUFFER = (long)1 << 0x1,
        eFLG_LIGHTING_COLOR = (long)1 << 0x2,
        eFLG_LIGHTING_COLOR_ALL = (long)1 << 0x3,
        eFLG_LIGHTING_COLOR6 = (long)1 << 0x4,
        eFLG_LIGHTING_COLOR6_ALL = (long)1 << 0x5,
        eFLG_LIGHTING_PIXEL = (long)1 << 0x6,
        eFLG_LIGHTING_VOLUME = (long)1 << 0x7,
        eFLG_LIGHTING_UI = (long)1 << 0x8,
        eFLG_LIGHTING_GLASS = (long)1 << 0x9,
        eFLG_LIGHTING_EMISSIVE_CURVE_RGB = (long)1 << 0xA,
        eFLG_LIGHTING_EMISSIVE_CURVE_A = (long)1 << 0xB,
        eFLG_LIGHTING_EMISSIVE_TEX_A = (long)1 << 0xC,
        eFLG_TEX_B_MODULATE = (long)1 << 0xD,
        eFLG_TEX_B_ADD = (long)1 << 0xE,
        eFLG_TEX_B_NORMAL_LIGHT = (long)1 << 0xF,
        eFLG_TEX_B_NORMAL_REFRACTION = (long)1 << 0x10,
        eFLG_TEX_A2_MODULATE = (long)1 << 0x11,
        eFLG_TEX_A2_ADD = (long)1 << 0x12,
        eFLG_TEX_A2_NORMAL_LIGHT = (long)1 << 0x13,
        eFLG_TEX_A2_MULTI_STAGE = (long)1 << 0x14,
        eFLG_TEX_A2_MULTI_CHARA = (long)1 << 0x15,
        eFLG_TEX_A2_FLOW = (long)1 << 0x16,
        eFLG_TEX_REFLECTION = (long)1 << 0x17,
        eFLG_STAGE_REFLECTION = (long)1 << 0x18,
        eFLG_TEX_A1_WRAP_CLAMP = (long)1 << 0x19,
        eFLG_TEX_B1_WRAP_CLAMP = (long)1 << 0x1A,
        eFLG_TEX_A2_WRAP_CLAMP = (long)1 << 0x1B,
        eFLG_TEX_A_PATTERN_BLEND = (long)1 << 0x1C,
        eFLG_TEX_B_PATTERN_BLEND = (long)1 << 0x1D,
        eFLG_TEX_A2_PATTERN_BLEND = (long)1 << 0x1E,
        eFLG_VAT_TYPE_FLUID = (long)1 << 0x1F,
        eFLG_VAT_TYPE_SOFT = (long)1 << 0x20,
        eFLG_VAT_FRAME_LOOP = (long)1 << 0x21,
        eFLG_BILLBOARD_AXIS_X = (long)1 << 0x22,
        eFLG_BILLBOARD_AXIS_Y = (long)1 << 0x23,
        eFLG_BILLBOARD_AXIS_Z = (long)1 << 0x24,
        eFLG_TEX_A_PATTERN_ONESHOT = (long)1 << 0x25,
        eFLG_TEX_B_PATTERN_ONESHOT = (long)1 << 0x26,
        eFLG_TEX_A2_PATTERN_ONESHOT = (long)1 << 0x27,
        eFLG_RIM_BLEND = (long)1 << 0x28,
    }
}
