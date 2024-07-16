using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIBLib
{
    //New flag at the beginning, even shifted the flags of dummy pib.
    [Flags]
    public enum EmitterFlagsDE2 : int
    {
        Unknown0 = 1 << 0,
        Unknown1 = 1 << 1,
        Unknown2 = 1 << 2,
        Unknown3 = 1 << 3,
        Unknown4 = 1 << 4,
        Unknown5 = 1 << 5,
        Unknown6 = 1 << 6,
        Billboard = 1 << 7,
        Unknown7 = 1 << 8,
        Unknown8 = 1 << 9,
        Unknown9 = 1 << 10,
        Unknown10 = 1 << 11,
        Unknown11 = 1 << 12,
        Unknown12 = 1 << 13,
        Unknown13 = 1 << 14,
        Unknown14 = 1 << 15,
        Unknown15 = 1 << 16,
        Unknown16 = 1 << 17,
        Unknown17 = 1 << 18,
        Unknown18 = 1 << 19,
        Unknown19 = 1 << 20,
        Unknown20 = 1 << 21,
        Unknown21 = 1 << 22,
        Unknown22 = 1 << 23,
        Unknown23 = 1 << 24,
        Unknown24 = 1 << 25,
        Unknown25 = 1 << 26,
        Unknown26 = 1 << 27,
        Unknown27 = 1 << 28,
        Unknown28 = 1 << 29,
        Unknown29 = 1 << 30,
    }
}
