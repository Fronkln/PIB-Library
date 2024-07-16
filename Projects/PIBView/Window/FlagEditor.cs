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
    public partial class FlagEditor : Form
    {
        private List<Enum> m_checkedValues = new List<Enum>();

        private Action<long> m_doneDeleg;

        public FlagEditor()
        {
            InitializeComponent();
        }

        public void Init(string[] list, long startVal, Action<long> finishDeleg)
        {
            foreach (string str in list)
                checkedListBox1.Items.Add(str.SplitOnCapitals());


            for(int i = 0; i < list.Length; i++)
            {
                if((startVal & ((long)1 << i)) != 0)
                    checkedListBox1.SetItemChecked(i, true);
            }

            m_doneDeleg = finishDeleg;
        }

        private void AttackFlagEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FlagEditor_Load(object sender, EventArgs e)
        {

        }

        private void FlagEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1.Instance.Enabled = true;

            long finalVal = 0;

            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                    finalVal += (long)1 << i;
            }


            m_doneDeleg?.Invoke(finalVal);
        }
    }
}
