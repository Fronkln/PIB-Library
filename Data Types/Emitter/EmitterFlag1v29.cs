using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIBLib
{
    public enum EmitterFlag1v29 : int
    {
        eFLG_GLOBAL_AXIS = 1 << 0,
        eFLG_EMITTER_ANIM = 1 << 0x1,
        eFLG_PTC_SRC_ANIM = 1 << 0x2,
        eFLG_COLOR_ANIM = 1 << 0x3,
        eFLG_UV_ANIM = 1 << 0x4,
        eFLG_AXIS_ANIM = 1 << 0x5,
        eFLG_BILLBOARD = 1 << 0x6,
        eFLG_LOOP = 1 << 0x7,
        eFLG_UNK_V21_FLAG = 1 << 0x8,
        eFLG_USE_NORMAL = 1 << 0x9,
        Flag11 = 1 << 0xA,
        Flag12 = 1 << 0xB,
        eFLG_GLARE = 1 << 0xC,
        eFLG_SOFT_PTC = 1 << 0xD,
        Flag15 = 1 << 0xE,
        Flag16 = 1 << 0xF,
        eFLG_WRITE_Z = 1 << 0x10,
        eFLG_TRIANGLE_LIST = 1 << 0x11,
        eFLG_UNK_V27_FLAG2 = 1 << 0x12,
        eFLG_METABALL = 1 << 0x13,
        eFLG_METABALL_B = 1 << 0x14,
        eFLG_METABALL_R = 1 << 0x15,
        eFLG_METABALL_E = 1 << 0x16,
        eFLG_PTC_BB = 1 << 0x17,
        eFLG_PTC_BB_Y = 1 << 0x18,
        Flag26 = 1 << 0x19,
        eFLG_SMALL_BUF = 1 << 0x1A,
        eFLG_ALWAYS = 1 << 0x1B,
        Flag29 = 1 << 0x1C,
        eFLG_COLOR_ANIM_MULTIPLE = 1 << 0x1D,
        Flag31 = 1 << 0x1E,
        Flag32 = 1 << 0x1F,
    }
}
