using PIBLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIBView
{
    internal class TreeNodePibMetaball : TreeNode
    {
        public PibBaseMetaball Metaball;

        public TreeNodePibMetaball(PibBaseMetaball metaball)
        {
            Metaball = metaball;
            Text = "Metaball";
        }
    }
}
