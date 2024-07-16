using PIBLib;

namespace PIBView
{
    internal class TreeNodePibSourceDataBillboard : TreeNodePibSourceData
    {
        public BaseParticleBillboardData BillboardData;

        public TreeNodePibSourceDataBillboard(BaseParticleBillboardData data)
        {
            BillboardData = data;
            Text = "Billboard Particle";
        }
    }
}
