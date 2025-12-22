using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PIBView
{
    public partial class ColorView : Form
    {
        private Action<Color> m_finish;
        bool simple = false;

        public ColorView()
        {
            InitializeComponent();
            Form1.Instance.Enabled = false;

            applyButton.Click += delegate
            {
                m_finish?.Invoke(GetColor());

                Form1.Instance.Enabled = true;
                Close();
            };

            rBox.TextChanged += delegate { try { panel1.BackColor = Color.FromArgb(int.Parse(aBox.Text), int.Parse(rBox.Text), panel1.BackColor.G, panel1.BackColor.B); } catch { } };
            gBox.TextChanged += delegate { try { panel1.BackColor = Color.FromArgb(int.Parse(aBox.Text),panel1.BackColor.R, int.Parse(gBox.Text), panel1.BackColor.B); } catch { } };
            bBox.TextChanged += delegate { try { panel1.BackColor = Color.FromArgb(int.Parse(aBox.Text), panel1.BackColor.R, panel1.BackColor.G, int.Parse(bBox.Text)); } catch { } };
            aBox.TextChanged += delegate { try { panel1.BackColor = Color.FromArgb(int.Parse(aBox.Text), panel1.BackColor.R, panel1.BackColor.G, panel1.BackColor.B); } catch { } };

            minR.TextChanged += delegate { try { panel1.BackColor = Color.FromArgb(int.Parse(minA.Text), int.Parse(minR.Text), panel1.BackColor.G, panel1.BackColor.B); } catch { } };
            minG.TextChanged += delegate { try { panel1.BackColor = Color.FromArgb(int.Parse(minA.Text), panel1.BackColor.R, int.Parse(minG.Text), panel1.BackColor.B); } catch { } };
            minB.TextChanged += delegate { try { panel1.BackColor = Color.FromArgb(int.Parse(minA.Text), panel1.BackColor.R, panel1.BackColor.G, int.Parse(minB.Text)); } catch { } };
            minA.TextChanged += delegate { try { panel1.BackColor = Color.FromArgb(int.Parse(minA.Text), panel1.BackColor.R, panel1.BackColor.G, panel1.BackColor.B); } catch { } };

            FormClosed += delegate { Form1.Instance.Enabled = true; };
        }

        public void Init(Color defaultCol, Action<Color> finished)
        {
            panel1.BackColor = defaultCol;
            rBox.Text = defaultCol.R.ToString();
            gBox.Text = defaultCol.G.ToString();
            bBox.Text = defaultCol.B.ToString();
            aBox.Text = defaultCol.A.ToString();

            minR.Text = rBox.Text;
            maxR.Text = rBox.Text;

            minG.Text = gBox.Text;
            maxG.Text = gBox.Text;

            minB.Text = bBox.Text;
            maxB.Text = bBox.Text;

            minA.Text = aBox.Text;
            maxA.Text = aBox.Text;

            EnableSimple();

            m_finish = finished;
        }

        public Color GetColor()
        {
            if (simple)
                return panel1.BackColor;
            else
            {
                Random rnd = new Random();
                byte r = (byte)rnd.Next(byte.Parse(minR.Text), byte.Parse(maxR.Text));
                byte g = (byte)rnd.Next(byte.Parse(minG.Text), byte.Parse(maxG.Text));
                byte b = (byte)rnd.Next(byte.Parse(minB.Text), byte.Parse(maxB.Text));
                byte a = (byte)rnd.Next(byte.Parse(minA.Text), byte.Parse(maxA.Text));

                return Color.FromArgb(a, r, g, b);
            }
        }

        private void ColorView_Load(object sender, EventArgs e)
        {

        }

        private void simpleRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (simpleRadio.Checked)
                EnableSimple();
        }

        private void EnableSimple()
        {
            minR.Enabled = false;
            minG.Enabled = false;
            minB.Enabled = false;
            minA.Enabled = false;

            maxR.Enabled = false;
            maxG.Enabled = false;
            maxB.Enabled = false;
            maxA.Enabled = false;

            rBox.Enabled = true;
            gBox.Enabled = true;
            bBox.Enabled = true;
            aBox.Enabled = true;

            simple = true;
        }

        private void DisableSimple()
        {
            minR.Enabled = true;
            minG.Enabled = true;
            minB.Enabled = true;
            minA.Enabled = true;

            maxR.Enabled = true;
            maxG.Enabled = true;
            maxB.Enabled = true;
            maxA.Enabled = true;

            rBox.Enabled = false;
            gBox.Enabled = false;
            bBox.Enabled = false;
            aBox.Enabled = false;

            simple = false;
        }

        private void randomBetweenRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (randomBetweenRadio.Checked)
                DisableSimple();
        }
    }
}
