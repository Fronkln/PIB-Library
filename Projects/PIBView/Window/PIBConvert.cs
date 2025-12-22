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

namespace PIBView
{
    public partial class PIBConvert : Form
    {
        private string m_pibPath;
        private string m_outputPath;
        private RegistryKey m_prefs;

        public PIBConvert()
        {
            InitializeComponent();
            convertButton.Enabled = !string.IsNullOrEmpty(m_pibPath) && File.Exists(m_pibPath);

            Form1.Instance.Enabled = false;
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

            if (dialog.ShowDialog() == DialogResult.OK)
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

            if (!string.IsNullOrEmpty(startDir) && Directory.Exists(startDir))
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


            bool gaidenToLj = inputVersion == PibVersion.Gaiden && inputVersion <= PibVersion.LJ;
            bool ljToGaiden = inputVersion <= PibVersion.LJ && outputVersion >= PibVersion.Gaiden;

#if !DEBUG
            try
            {
#endif

            BasePib pib = PIB.Read(m_pibPath);


            if (changeIdTextbox.Enabled)
                pib.ParticleID = uint.Parse(changeIdTextbox.Text);

            if (pib == null)
            {
                throw new Exception("Error reading pib. Invalid or missing file");
            }


            BasePib converted = PIB.Convert(pib, outputVersion);

            if (ljToGaiden || gaidenToLj)
            {
                foreach (PibEmitterv43 emitter in converted.Emitters)
                {
                    if (ljToGaiden)
                        emitter.ToGaidenRevision();
                    else
                        emitter.ToLJRevision();
                }
            }

            PIB.Write(converted, m_outputPath);

            if (copyTexturesCheck.Checked)
            {
                string inpDir = new FileInfo(m_pibPath).Directory.FullName;
                string outputDir = new FileInfo(m_outputPath).Directory.FullName;

                foreach (BasePibEmitter emitter in pib.Emitters)
                {
                    foreach (string texture in emitter.Textures)
                    {
                        string ddsPath = TryFetchTexture(inpDir, texture); //Path.Combine(inpDir, texture);

                        if (!string.IsNullOrEmpty(ddsPath))
                        {
                            string ddsName = new FileInfo(ddsPath).Name;
                            try
                            {
                                File.Copy(ddsPath, Path.Combine(outputDir, ddsName), true);
                            }
                            catch
                            {

                            }
                        }
                    }

                    if (inputVersion >= PibVersion.YLAD)
                    {
                        if (emitter.GetEmitterType() == EmitterType.Model)
                        {
                            PibEmitterv52 emitterv52 = emitter as PibEmitterv52;


                            foreach (TextureImportInfo inf in emitterv52.TextureImports)
                            {
                                foreach (TextureImportResource resource in inf.Resources)
                                {
                                    string gmdPath = TryFetchModel(inpDir, resource.Name); //Path.Combine(inpDir, texture);

                                    if (!string.IsNullOrEmpty(gmdPath))
                                    {
                                        string gmdName = new FileInfo(gmdPath).Name;
                                        File.Copy(gmdPath, Path.Combine(outputDir, gmdName), true);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            m_prefs.SetValue("input_game", inputBox.SelectedIndex);
            m_prefs.SetValue("output_game", outputBox.SelectedIndex);
            m_prefs.SetValue("last_path", m_pibPath);
            m_prefs.SetValue("last_output_path", new FileInfo(m_outputPath).Directory.FullName);
            m_prefs.Flush();
        }
#if !DEBUG
            catch (Exception ex)
            {
                MessageBox.Show("Error converting\n\n" + ex.Message + "\n\n" + ex.InnerException);
            }
        }
#endif


        public static string TryFetchTexture(string startDir, string textureName)
        {
            DirectoryInfo dirInf = new DirectoryInfo(startDir);
            string path = "";

            if (!textureName.EndsWith(".dds"))
                textureName += ".dds";

            path = Path.Combine(startDir, textureName);

            if (File.Exists(path))
                return path;

            if (dirInf.Name == "pib")
            {
                path = Path.Combine(dirInf.Parent.FullName, "tex", textureName);

                if (File.Exists(path))
                    return path;
            }

            return "";
        }

        public static string TryFetchModel(string startDir, string modelName)
        {
            DirectoryInfo dirInf = new DirectoryInfo(startDir);
            string path = "";

            if (!modelName.EndsWith(".gmd"))
                modelName += ".gmd";

            path = Path.Combine(startDir, modelName);

            if (File.Exists(path))
                return path;

            if (dirInf.Name == "pib")
            {
                path = Path.Combine(dirInf.Parent.FullName, "mesh", modelName);

                if (File.Exists(path))
                    return path;
            }

            return "";
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

        private void justOpenPIBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Yakuza Particle (*.pib)|*.pib";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                BasePib pibbers = PIB.Read(dialog.FileName);
            }
        }

        private void PIBConvert_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1.Instance.Enabled = true;
        }
    }
}
