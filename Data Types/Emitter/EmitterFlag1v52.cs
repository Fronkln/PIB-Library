using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIBLib
{
    /// <summary>
    /// Unchanged in LJ too
    /// </summary>
    public enum EmitterFlag1v52 : long
    {
        eFLG_GLOBAL_AXIS = (long)1 << 0,
        eFLG_EMITTER_ANIM = (long)1 << 0x1,
        eFLG_EMITTER_COLOR_ANIM = (long)1 << 0x2,
        eFLG_PTC_SRC_ANIM = (long)1 << 0x3,
        eFLG_COLOR_ANIM = (long)1 << 0x4,
        eFLG_UV_ANIM = (long)1 << 0x5,
        eFLG_AXIS_ANIM = (long)1 << 0x6,
        eFLG_BILLBOARD = (long)1 << 0x7,
        eFLG_MESH_VAT = (long)1 << 0x8,
        eFLG_LOOP = (long)1 << 0x9,
        eFLG_USE_NORMAL = (long)1 << 0xA,
        eFLG_X_AXIS_NBB = (long)1 << 0xB,
        eFLG_GLARE = (long)1 << 0xC,
        eFLG_SOFT_PTC = (long)1 << 0xD,
        eFLG_SHELTER = (long)1 << 0xE,
        eFLG_POINT_LIGHT = (long)1 << 0xF,
        eFLG_WRITE_Z = (long)1 << 0x10,
        eFLG_TRIANGLE_LIST = (long)1 << 0x11,
        eFLG_METABALL = (long)1 << 0x12,
        eFLG_METABALL_B = (long)1 << 0x13,
        eFLG_METABALL_R = (long)1 << 0x14,
        eFLG_METABALL_E = (long)1 << 0x15,
        eFLG_METABALL_A_THRESHOLD_01 = (long)1 << 0x16,
        eFLG_METABALL_WORK_X4 = (long)1 << 0x17,
        eFLG_METABALL_HEIGHT_FLAT_USE = (long)1 << 0x18,
        eFLG_METABALL_TEX_FORMAT_L8 = (long)1 << 0x19,
        eFLG_PTC_BB = (long)1 << 0x1A,
        eFLG_PTC_BB_Y = (long)1 << 0x1B,
        eFLG_NO_EXTRA_COLOR = (long)1 << 0x1C,
        eFLG_SMALL_BUF = (long)1 << 0x1D,
        eFLG_ALWAYS = (long)1 << 0x1E,
        eFLG_COLOR_ANIM_MULTIPLE = (long)1 << 0x1F,
        eFLG_PROJECITON = (long)1 << 0x20,
        eFLG_MESH = (long)1 << 0x21,
    }
}
