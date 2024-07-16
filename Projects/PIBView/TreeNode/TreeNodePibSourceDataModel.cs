using PIBLib;

namespace PIBView
{
    internal class TreeNodePibSourceDataModel : TreeNodePibSourceData
    {
        public BaseParticleModelData ModelData;

        public TreeNodePibSourceDataModel(BaseParticleModelData data)
        {
            ModelData = data;
            Text = "Model Particle";
        }
    }
}
