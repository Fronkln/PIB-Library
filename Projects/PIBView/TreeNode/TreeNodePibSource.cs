using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIBLib;

namespace PIBView
{
    internal class TreeNodePibSource : TreeNode
    {
        public ParticleSource Source;

        public TreeNodePibSource(ParticleSource source)
        {
            Source = source;
            Text = "Source";

            foreach(ParticleIns ins in source.GetData())
            {
                TreeNodePibIns insNode = new TreeNodePibIns(ins);
                Nodes.Add(insNode);
            }
        }
    }
}
