using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIBLib
{
    public enum EmitterFlag1v21 : int
    {
        eFLG_GLOBAL_AXIS = 1 << 0,
        eFLG_EMITTER_ANIM = 1 << 0x1,
        eFLG_PTC_SRC_ANIM = 1 << 0x2,
        eFLG_COLOR_ANIM  = 1 << 0x3,
        eFLG_UV_ANIM = 1 << 0x4,
        Flag6 = 1 << 0x5,
        eFLG_BILLBOARD = 1 << 0x7,
        eFLG_LOOP = 1 << 0x7,
        eFLG_UNK_V21_FLAG = 1 << 0x8,
        Flag10  = 1 << 0x9,
        eFLG_USE_NORMAL = 1 << 0xA,
        Flag12 = 1 << 0xB,
        Flag13 = 1 << 0xC,
        Flag14 = 1 << 0xD,
        Flag15 = 1 << 0xE,
        Flag16 = 1 << 0xF,
        Flag17 = 1 << 0x10,
        Flag18 = 1 << 0x11,
        Flag19 = 1 << 0x12,
        Flag20 = 1 << 0x13,
        eFLG_TRIANGLE_LIST = 1 << 0x14,
        Flag22 = 1 << 0x15,
        Flag23 = 1 << 0x16,
        eFLG_METABALL = 1 << 0x17,
        eFLG_METABALL_R = 1 << 0x18,
        Flag26 = 1 << 0x19,
        Flag27 = 1 << 0x1A,
        Flag28 = 1 << 0x1B,
        Flag29 = 1 << 0x1C,
        Flag30 = 1 << 0x1D,
        Flag31 = 1 << 0x1E,
        Flag32 = 1 << 0x1F,
    }
}
