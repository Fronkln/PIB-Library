using PIBLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIBView
{
    internal class TreeNodePib : TreeNode
    {
        public BasePib Pib;

        public TreeNodePib(BasePib pib)
        {
            Pib = pib;
            Text = $"{pib.Name} ({pib.Version})";

            foreach (BasePibEmitter emitter in pib.Emitters)
               Nodes.Add(new TreeNodePibEmitter(emitter));
        }
    }
}
