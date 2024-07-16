using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIBLib;

namespace PIBView
{
    internal class TreeNodePibEmitter : TreeNode
    {
        public BasePibEmitter Emitter;

        public TreeNodePibEmitter(BasePibEmitter emitter)
        {
            Emitter = emitter;
            Text = $"Emitter ({(emitter.GetEmitterType())})";

            Nodes.Add(new TreeNodePibSource(emitter.Source));

            TreeNode animCurveRoot = new TreeNode("Property Animation Curves");

            foreach (PibEmitterAnimationCurve curve in emitter.PropertyAnimationCurve)
              animCurveRoot.Nodes.Add(new TreeNode(curve.GetType().Name.Replace("PibEmitter", "")));

            TreeNode metaballRoot = new TreeNodePibMetaball(emitter.Metaball);
            TreeNode animDatRoot = new TreeNodePibAnimationData(emitter.AnimationData);

            Nodes.Add(animCurveRoot);
            Nodes.Add(animDatRoot);
            Nodes.Add(metaballRoot);
        }
    }
}
