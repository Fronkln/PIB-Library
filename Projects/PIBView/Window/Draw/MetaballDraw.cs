using PIBLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIBView
{
    internal static class MetaballDraw
    {
        public static void Draw(Form1 form, TreeNodePibMetaball metaballNode)
        {
            TreeNodePibEmitter emitterParent = metaballNode.Parent as TreeNodePibEmitter;
            TreeNodePib pibParent = emitterParent.Parent as TreeNodePib;
            PibBaseMetaball metaball = metaballNode.Metaball;

            if(emitterParent.Emitter.IsMetaball())
                form.CreateHeader("Metaball");
            else
                form.CreateHeader("Metaball (Inactive)");

            form.CreateInput("Rate", metaball.Rate.ToString(), delegate (string val) { metaball.Rate = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);
            form.CreateInput("Normal Range", metaball.NmlRange.ToString(), delegate (string val) { metaball.NmlRange = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);
            form.CreateInput("Light Shininess", metaball.LtShininess.ToString(), delegate (string val) { metaball.LtShininess = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);
            form.CreateInput("Light IOE", metaball.LtIoe.ToString(), delegate (string val) { metaball.LtIoe = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);

            if(Form1.pibVersion < PibVersion.Y6)
            {
                form.CreateInput("OE Unknown 1", metaball.OEUnknown1.ToString(), delegate (string val) { metaball.OEUnknown1 = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);
                form.CreateInput("OE Unknown 2", metaball.OEUnknown2.ToString(), delegate (string val) { metaball.OEUnknown1 = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);
                form.CreateInput("OE Unknown 3", metaball.OEUnknown3.ToString(), delegate (string val) { metaball.OEUnknown1 = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);
            }
            else
            {
                DEPibBaseMetaball deMetaball = metaball as DEPibBaseMetaball;

                form.CreateInput("Normal Z", deMetaball.NmlZ.ToString(), delegate (string val) { deMetaball.NmlZ = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);
                form.CreateInput("Light Lambert Offset", deMetaball.LtLambertOffset.ToString(), delegate (string val) { deMetaball.LtLambertOffset = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);
                form.CreateInput("Light Ratio", deMetaball.LtRatio.ToString(), delegate (string val) { deMetaball.LtRatio = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);
                form.CreateInput("H Light Intensity", deMetaball.HltIntensity.ToString(), delegate (string val) { deMetaball.HltIntensity = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);
            }

            Panel colPanel = null;
            colPanel = form.CreatePanel("Metaball Color", metaball.Color,
                delegate (Color col)
                {
                    metaball.Color = col;
                    colPanel.BackColor = col;
                });

        }
    }
}
