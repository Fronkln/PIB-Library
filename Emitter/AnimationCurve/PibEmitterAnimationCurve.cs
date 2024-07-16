using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{
    public class PibEmitterAnimationCurve
    {
        internal virtual void Read(DataReader reader, int numfloats)
        {
            throw new NotImplementedException();
        }

        internal virtual void Write(DataWriter writer)
        {
            throw new NotImplementedException();
        }

        internal virtual int GetDataSize()
        {
            throw new NotImplementedException();
        }
    }
}
