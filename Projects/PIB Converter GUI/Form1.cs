using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using PIBLib;

namespace PIB_Converter_GUI
{
    public partial class Form1 : Form
    {
        private string m_pibPath;
        private string m_outputPath;
        private RegistryKey m_prefs;

        public Form1()
        {
            InitializeComponent();
            convertButton.Enabled = !string.IsNullOrEmpty(m_pibPath) && File.Exists(m_pibPath);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            inputBox.Items.AddRange(Enum.GetNames<PibVersion>());
            outputBox.Items.AddRange(Enum.GetNames<PibVersion>());

            m_prefs = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\PIB Converter GUI", true);

            if (m_prefs == null)
                m_prefs = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\PIB Converter GUI");

            if (m_prefs.GetValue("input_game") == null)
                m_prefs.SetValue("input_game", 0);

            if (m_prefs.GetValue("output_game") == null)
                m_prefs.SetValue("output_game", 0);

            if (m_prefs.GetValue("last_path") == null)
                m_prefs.SetValue("last_path", "");

            if (m_prefs.GetValue("last_output_path") == null)
                m_prefs.SetValue("last_output_path", "");

            if (m_prefs.GetValue("copy_texture") == null)
                m_prefs.SetValue("copy_texture", 0);

            if (m_prefs.GetValue("change_id") == null)
                m_prefs.SetValue("change_id", 0);

            m_prefs.Flush();

            inputBox.SelectedIndex = (int)m_prefs.GetValue("input_game");
            outputBox.SelectedIndex = (int)m_prefs.GetValue("output_game");
            copyTexturesCheck.Checked = (int)m_prefs.GetValue("copy_texture") == 1;
            idChangeCheck.Checked = (int)m_prefs.GetValue("change_id") == 1;
            
            m_pibPath = (string)m_prefs.GetValue("last_path");
            pathBox.Text = m_pibPath;
            convertButton.Enabled = !string.IsNullOrEmpty(m_pibPath) && File.Exists(m_pibPath);
        }

        private void button2_Paint(object sender, PaintEventArgs e)
        {
            convertButton.Enabled = !string.IsNullOrEmpty(m_pibPath);
        }

        private void fileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Yakuza Particle (*.pib)|*.pib";

            if(dialog.ShowDialog() == DialogResult.OK)
            {
                m_pibPath = dialog.FileName;
                pathBox.Text = dialog.FileName;

                convertButton.Enabled = !string.IsNullOrEmpty(m_pibPath);
            }
        }

        private void convertButton_Layout(object sender, LayoutEventArgs e)
        {
        }

        private void convertButton_Click(object sender, EventArgs e)
        {
            if (!File.Exists(m_pibPath))
                return;

            string startDir = (string)m_prefs.GetValue("last_output_path");

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Yakuza Particle (*.pib)|*.pib";

            if(!string.IsNullOrEmpty(startDir) && Directory.Exists(startDir))
                dialog.InitialDirectory = m_outputPath;

            dialog.FileName += Path.GetFileName(m_pibPath);

            if (dialog.ShowDialog() == DialogResult.OK)
                m_outputPath = dialog.FileName;
            else
                return;

            if (!dialog.FileName.EndsWith(".pib"))
                dialog.FileName += ".pib";

            PibVersion inputVersion = (PibVersion)Enum.Parse(typeof(PibVersion), inputBox.Items[inputBox.SelectedIndex].ToString());
            PibVersion outputVersion = (PibVersion)Enum.Parse(typeof(PibVersion), outputBox.Items[outputBox.SelectedIndex].ToString());

            try
            {

                BasePib pib = PIB.Read(m_pibPath);

                
                if (changeIdTextbox.Enabled)
                    pib.ParticleID = uint.Parse(changeIdTextbox.Text);

                if(pib == null)
                {
                    throw new Exception("Error reading pib. Invalid or missing file");
                }

                switch (inputVersion)
                {
                    case PibVersion.Kenzan:
                        ConvertFromKenzan(pib, outputVersion);
                        break;
                    case PibVersion.Y3:
                        ConvertFromY3(pib, outputVersion);
                        break;
                    case PibVersion.Y5:
                        ConvertFromY5(pib, outputVersion);
                        break;
                    case PibVersion.Ishin:
                        ConvertFromIshin(pib, outputVersion);
                        break;
                    case PibVersion.JE:
                        ConvertFromJE(pib, outputVersion);
                        break;
                }

                if(copyTexturesCheck.Checked)
                {
                    string inpDir = new FileInfo(m_pibPath).Directory.FullName;
                    string outputDir = new FileInfo(m_outputPath).Directory.FullName;

                    foreach(BasePibEmitter emitter in pib.Emitters)
                        foreach(string texture in emitter.Textures)
                        {
                            string ddsPath = Path.Combine(inpDir, texture);

                            if (File.Exists(ddsPath))
                                File.Copy(ddsPath, Path.Combine(outputDir, texture), true);
                        }
                }

                m_prefs.SetValue("input_game", inputBox.SelectedIndex);
                m_prefs.SetValue("output_game", outputBox.SelectedIndex);
                m_prefs.SetValue("last_path", m_pibPath);
                m_prefs.SetValue("last_output_path", new FileInfo(m_outputPath).Directory.FullName);
                m_prefs.Flush();

                MessageBox.Show("Conversion complete");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error converting\n\n" + ex.Message + "\n\n" + ex.InnerException);
            }
        }

        private void ConvertFromKenzan(BasePib basePib, PibVersion outputVersion)
        {
            Pib8 pib = (Pib8)basePib;

            switch (outputVersion)
            {
                case PibVersion.Y3:
                    PIB.Write(pib.ToV19(), m_outputPath);
                    break;
                case PibVersion.Y5:
                    PIB.Write(pib.ToV19().ToV21(), m_outputPath);
                    break;
                case PibVersion.Ishin:
                    PIB.Write(pib.ToV19().ToV21().ToV25(), m_outputPath);
                    break;
                case PibVersion.Y0:
                    PIB.Write(pib.ToV19().ToV21().ToV25().ToV27(), m_outputPath);
                    break;
            }
        }

        private void ConvertFromY3(BasePib basePib, PibVersion outputVersion)
        {
            Pib19 pib = (Pib19)basePib;

            switch(outputVersion)
            {
                case PibVersion.Y5:
                    PIB.Write(pib.ToV21(), m_outputPath);
                    break;
                case PibVersion.Ishin:
                    PIB.Write(pib.ToV21().ToV25(), m_outputPath);
                    break;
                case PibVersion.Y0:
                    PIB.Write(pib.ToV21().ToV25().ToV27(), m_outputPath);
                    break;
            }
        }

        private void ConvertFromY5(BasePib basePib, PibVersion outputVersion)
        {
            Pib21 pib = (Pib21)basePib;

            switch (outputVersion)
            {
                case PibVersion.Ishin:
                    PIB.Write(pib.ToV25(), m_outputPath);
                    break;
                case PibVersion.Y0:
                    PIB.Write(pib.ToV25().ToV27(), m_outputPath);
                    break;
            }
        }

        private void ConvertFromIshin(BasePib basePib, PibVersion outputVersion)
        {
            Pib25 pib = (Pib25)basePib;

            switch (outputVersion)
            {
                case PibVersion.Y0:
                    PIB.Write(pib.ToV27(), m_outputPath);
                    break;
            }
        }

        private void ConvertFromJE(BasePib basePib, PibVersion outputVersion)
        {
            Pib45 pib = (Pib45)basePib;

            switch (outputVersion)
            {
                case PibVersion.YK2:
                    PIB.Write(pib.ToV43(), m_outputPath);
                    break;
            }
        }

        private void idChangeBox_CheckedChanged(object sender, EventArgs e)
        {
            changeIdTextbox.Enabled = idChangeCheck.Checked;
        }

        private void pathBox_TextChanged(object sender, EventArgs e)
        {
            m_pibPath = pathBox.Text;

            if (File.Exists(m_pibPath) && m_pibPath.EndsWith(".pib"))
                convertButton.Enabled = true;
            else
                convertButton.Enabled = false;
        }
    }
}
