using PIBLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIBView
{
    internal class TreeNodePibIns : TreeNode
    {
        public ParticleIns Ins;

        public TreeNodePibIns(ParticleIns Ins)
        {
            this.Ins = Ins;
            Text = "Particle";
        }
    }
}
