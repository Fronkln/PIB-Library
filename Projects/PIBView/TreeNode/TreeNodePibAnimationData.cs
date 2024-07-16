using PIBLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIBView
{
    internal class TreeNodePibAnimationData : TreeNode
    {
        public EmitterBaseAnimationData AnimationData;

        public TreeNodePibAnimationData(EmitterBaseAnimationData animationData)
        {
            AnimationData = animationData;
            Text = "Animation";
        }
    }
}
