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

            if(Source.GetDataType() == EmitterType.Billboard)
            {
                foreach(BaseParticleBillboardData datas in source.GetData())
                {
                    TreeNodePibSourceDataBillboard billDataNode = new TreeNodePibSourceDataBillboard(datas);
                    Nodes.Add(billDataNode);
                }
            }
            else
            {
                foreach (BaseParticleModelData datas in source.GetData())
                {
                    TreeNodePibSourceDataModel modelDataNode = new TreeNodePibSourceDataModel(datas);
                    Nodes.Add(modelDataNode);
                }
            }
        }
    }
}
