using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PIBView.Window
{
    public partial class ColorChangePrompt : Form
    {
        public Action<int> Finished;

        public ColorChangePrompt()
        {
            InitializeComponent();

            TreeNodePibEmitter emitter = Form1.Instance.pibTree.SelectedNode as TreeNodePibEmitter;
            animationCurveRadio.Enabled = emitter.Emitter.IsUseColorCurve();
            metaballRadio.Enabled = emitter.Emitter.IsMetaball();
        }

        public void Init(Action<int> finished)
        {
            Finished = finished;
        }

        private void animationCurveRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (animationCurveRadio.Checked)
            {
                particleRadio.Checked = false;
                metaballRadio.Checked = false;
            }
        }

        private void particleRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (particleRadio.Checked)
            {
                animationCurveRadio.Checked = false;
                metaballRadio.Checked = false;
            }
        }

        private void metaballRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (metaballRadio.Checked)
            {
                animationCurveRadio.Checked = false;
                particleRadio.Checked = false;
            }
        }

        public int GetMode()
        {
            if (animationCurveRadio.Checked)
                return 0;
            if (particleRadio.Checked)
                return 1;
            if (metaballRadio.Checked)
                return 2;

            return 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Finished?.Invoke(GetMode());
        }
    }
}
