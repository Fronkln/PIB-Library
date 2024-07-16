using PIBLib;

namespace PIBView
{
    public partial class Form1 : Form
    {
        public static PibVersion pibVersion;
        string filePath = null;

        public static Form1 Instance;


        public Form1()
        {
            InitializeComponent();
            Instance = this;

            if (!string.IsNullOrEmpty(Program.ToLoadPib))
            {
                LoadPib(Program.ToLoadPib);
                Program.ToLoadPib = null;
            }
        }

        public bool IsDEPib()
        {
            return pibVersion > PibVersion.Y0;
        }

        public bool IsDE1Pib()
        {
            return pibVersion > PibVersion.Y0 && pibVersion < PibVersion.YLAD;
        }

        public int GetFlagLength()
        {
            if (IsDEPib())
                return 64; //long
            else
                return 32; //int
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            filePath = dialog.FileName;

            BasePib pib = PIB.Read(dialog.FileName);
            pibVersion = pib.Version;

            pibTree.SuspendLayout();
            pibTree.Nodes.Clear();

            TreeNodePib pibRoot = new TreeNodePib(pib);

            pibTree.Nodes.Add(pibRoot);

            pibTree.ResumeLayout();
        }

        public void LoadPib(string path)
        {
            BasePib pib = PIB.Read(path);
            pibVersion = pib.Version;

            pibTree.SuspendLayout();
            pibTree.Nodes.Clear();

            filePath = path;

            TreeNodePib pibRoot = new TreeNodePib(pib);

            pibTree.Nodes.Add(pibRoot);
            pibRoot.Expand();

            pibTree.ResumeLayout();
        }

        private void pibTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            varPanel.SuspendLayout();
            varPanel.Controls.Clear();
            varPanel.RowStyles.Clear();
            varPanel.RowCount = 0;

            if (e.Node != null)
            {
                if (e.Node is TreeNodePibEmitter)
                    DrawPibEmitter(e.Node as TreeNodePibEmitter);
                else if (e.Node is TreeNodePib)
                    DrawPib(e.Node as TreeNodePib);
                else if (e.Node is TreeNodePibMetaball)
                    MetaballDraw.Draw(this, (e.Node as TreeNodePibMetaball));
                else if (e.Node is TreeNodePibAnimationData)
                    AnimationDataDraw.Draw(this, e.Node as TreeNodePibAnimationData);
            }

            varPanel.RowCount++;
            varPanel.ResumeLayout();
        }

        private void DrawPib(TreeNodePib treePib)
        {
            CreateHeader("Global PIB Setting");
            CreateInput("Particle ID", treePib.Pib.ParticleID.ToString(), delegate (string val) { treePib.Pib.ParticleID = uint.Parse(val); }, NumberBox.NumberMode.UInt);
            CreateInput("Duration", treePib.Pib.Duration.ToString(), delegate (string val) { treePib.Pib.Duration = uint.Parse(val); }, NumberBox.NumberMode.UInt);
            CreateInput("Speed", treePib.Pib.Speed.ToString(), delegate (string val) { treePib.Pib.Speed = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);
            CreateInput("Forward Offset", treePib.Pib.ForwardOffset.ToString(), delegate (string val) { treePib.Pib.ForwardOffset = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);

            CreateInput("Scale X", treePib.Pib.Scale.x.ToString(), delegate (string val) { treePib.Pib.Scale.x = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);
            CreateInput("Scale Y", treePib.Pib.Scale.y.ToString(), delegate (string val) { treePib.Pib.Scale.y = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);
            CreateInput("Scale Z", treePib.Pib.Scale.z.ToString(), delegate (string val) { treePib.Pib.Scale.z = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);
        }
        private void DrawPibEmitter(TreeNodePibEmitter treeEmitter)
        {
            BaseDEPibEmitter deEmitter = treeEmitter.Emitter as BaseDEPibEmitter;
            PibEmitterv43 v43Pib = treeEmitter.Emitter as PibEmitterv43;

            CreateHeader(treeEmitter.Text);
            DrawSpecialFlag1(treeEmitter.Emitter);
            DrawSpecialFlag2(treeEmitter);
            DrawSpecialFlag3(treeEmitter);

            if (!IsDEPib() && pibVersion >= PibVersion.Y3)
                DrawSpecialOEFlag4(treeEmitter);

            CreateInput("Blend", treeEmitter.Emitter.Blend.ToString(), delegate (string val) { treeEmitter.Emitter.Blend = byte.Parse(val); }, NumberBox.NumberMode.Byte);

            if (pibVersion >= PibVersion.YK2)
                CreateInput("Metaball Blend", v43Pib.MetaballBlend.ToString(), delegate (string val) { v43Pib.MetaballBlend = byte.Parse(val); }, NumberBox.NumberMode.Byte);

            CreateHeader("AA Box");
            CreateInput("AA Box Center X", treeEmitter.Emitter.AABoxCenter.x.ToString(), delegate (string val) { treeEmitter.Emitter.AABoxCenter.x = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);
            CreateInput("AA Box Center Y", treeEmitter.Emitter.AABoxCenter.y.ToString(), delegate (string val) { treeEmitter.Emitter.AABoxCenter.y = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);
            CreateInput("AA Box Center Z", treeEmitter.Emitter.AABoxCenter.z.ToString(), delegate (string val) { treeEmitter.Emitter.AABoxCenter.z = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);
            CreateInput("AA Box Extent X", treeEmitter.Emitter.AABoxExtent.x.ToString(), delegate (string val) { treeEmitter.Emitter.AABoxExtent.x = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);
            CreateInput("AA Box Extent Y", treeEmitter.Emitter.AABoxExtent.y.ToString(), delegate (string val) { treeEmitter.Emitter.AABoxExtent.y = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);
            CreateInput("AA Box Extent Z", treeEmitter.Emitter.AABoxExtent.z.ToString(), delegate (string val) { treeEmitter.Emitter.AABoxExtent.z = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);

            CreateHeader("Textures");

            for (int i = 0; i < treeEmitter.Emitter.Textures.Count; i++)
            {
                int k = i;
                CreateInput("Texture", treeEmitter.Emitter.Textures[i], delegate (string val) { treeEmitter.Emitter.Textures[k] = val; });
            }

            if (pibVersion >= PibVersion.YLAD)
            {
                CreateHeader("UV");

                CreateInput("UV1 X Scale", deEmitter.UV.UVSize[0].x.ToString(), delegate (string val) { deEmitter.UV.UVSize[0].x = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);
                CreateInput("UV1 Y Scale", deEmitter.UV.UVSize[0].y.ToString(), delegate (string val) { deEmitter.UV.UVSize[0].y = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);

                CreateInput("UV2 X Scale", deEmitter.UV.UVSize[1].x.ToString(), delegate (string val) { deEmitter.UV.UVSize[1].x = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);
                CreateInput("UV2 Y Scale", deEmitter.UV.UVSize[1].y.ToString(), delegate (string val) { deEmitter.UV.UVSize[1].y = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);

                CreateInput("UV3 X Scale", deEmitter.UV.UVSize[2].x.ToString(), delegate (string val) { deEmitter.UV.UVSize[2].x = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);
                CreateInput("UV3 Y Scale", deEmitter.UV.UVSize[2].y.ToString(), delegate (string val) { deEmitter.UV.UVSize[2].y = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);

                if (!IsDE1Pib())
                {
                    CreateInput("UV4 X Scale", deEmitter.UV.UVSize[3].x.ToString(), delegate (string val) { deEmitter.UV.UVSize[3].x = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);
                    CreateInput("UV4 Y Scale", deEmitter.UV.UVSize[3].y.ToString(), delegate (string val) { deEmitter.UV.UVSize[3].y = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);
                }
            }
        }

        private bool DrawSpecialFlag1(BasePibEmitter emitter)
        {
            string[] flagsList = GetFlag1List().Select(x => x.Replace("eFLG_", "")).ToArray();

            if (flagsList == null || flagsList.Length <= 0)
                return false;

            PibEmitterv52 v52Emitter = emitter as PibEmitterv52;

            CreateButton("Flag 1",
                delegate
                {
                    long flag1 = 0;

                    if (v52Emitter != null)
                        flag1 = (long)v52Emitter.Flags;
                    else
                        flag1 = emitter.Flags;

                    FlagEditor form = new FlagEditor();
                    form.Init(flagsList, flag1, delegate (long val)
                    {
                        if (v52Emitter != null)
                            v52Emitter.Flags = (ulong)val;
                        else
                            emitter.Flags = (int)val;
                    });
                    form.Show();
                }, 40, 14);

            return true;
        }
        private bool DrawSpecialFlag2(TreeNodePibEmitter treeEmitter)
        {
            string[] flagsList = GetFlag2List().Select(x => x.Replace("eFLG_", "")).ToArray();

            if (flagsList == null || flagsList.Length <= 0)
                return false;

            PibEmitterv52 de2Emitter = treeEmitter.Emitter as PibEmitterv52;


            CreateButton("Flag 2",
                delegate
                {
                    long flag2 = 0;

                    if (de2Emitter != null)
                        flag2 = (long)de2Emitter.Flags2;
                    else
                        flag2 = treeEmitter.Emitter.Flags2;

                    FlagEditor form = new FlagEditor();
                    form.Init(flagsList, flag2, delegate (long val)
                    {
                        if (de2Emitter != null)
                            de2Emitter.Flags2 = (ulong)val;
                        else
                            treeEmitter.Emitter.Flags2 = (int)val;
                    });
                    form.Show();
                }, 40, 14);

            return true;
        }
        private bool DrawSpecialFlag3(TreeNodePibEmitter treeEmitter)
        {
            string[] flagsList = GetFlag3List().Select(x => x.Replace("eFLG_", "")).ToArray();

            if (flagsList == null || flagsList.Length <= 0)
                return false;

            PibEmitterv52 de2Emitter = treeEmitter.Emitter as PibEmitterv52;

            CreateButton("Flag 3",
                delegate
                {
                    long flag3 = 0;

                    if (de2Emitter != null)
                        flag3 = (long)de2Emitter.Flags3;
                    else
                        flag3 = treeEmitter.Emitter.Flags3;

                    FlagEditor form = new FlagEditor();
                    form.Init(flagsList, flag3, delegate (long val)
                    {
                        if (de2Emitter != null)
                            de2Emitter.Flags3 = (ulong)val;
                        else
                            treeEmitter.Emitter.Flags3 = (int)val;
                    });
                    form.Show();
                }, 40, 14);

            return true;
        }

        private bool DrawSpecialOEFlag4(TreeNodePibEmitter treeEmitter)
        {
            string[] flagsList = GetOEFlag4List().Select(x => x.Replace("eFLG_", "")).ToArray();

            if (flagsList == null || flagsList.Length <= 0)
                return false;

            CreateButton("Flag 4",
                delegate
                {
                    PibEmitterv19 emitter = treeEmitter.Emitter as PibEmitterv19;

                    int flag4 = 0;
                    flag4 = emitter.OOEUnkStructure6.Flag4;

                    FlagEditor form = new FlagEditor();
                    form.Init(flagsList, flag4, delegate (long val)
                    {
                        emitter.OOEUnkStructure6.Flag4 = (int)val;
                    });
                    form.Show();
                }, 40, 14);

            return true;
        }

        private string[] GetFlag1List()
        {
            string[] values = null;

            if (pibVersion >= PibVersion.YLAD)
                values = Enum.GetNames<EmitterFlag1v52>();
            if (pibVersion == PibVersion.Y0 || pibVersion == PibVersion.Ishin)
                values = Enum.GetNames<EmitterFlag1v27>();
            if (pibVersion == PibVersion.Y3)
                values = Enum.GetNames<EmitterFlag1v19>();
            if (pibVersion == PibVersion.Y5)
                values = Enum.GetNames<EmitterFlag1v21>();
            if (pibVersion == PibVersion.Y6)
                values = Enum.GetNames<EmitterFlag1v29>();
            if (pibVersion == PibVersion.YK2)
                values = Enum.GetNames<EmitterFlag1v43>();
            if (pibVersion == PibVersion.JE)
                values = Enum.GetNames<EmitterFlag1v45>();

            if (values == null)
            {
                List<string> strings = new List<string>();

                for (int i = 0; i < GetFlagLength(); i++)
                    strings.Add("Flag " + (i + 1));

                values = strings.ToArray();
            }

            return values;
        }

        private string[] GetFlag2List()
        {
            string[] values = null;

            if (pibVersion >= PibVersion.Y6)
                values = Enum.GetNames<EmitterFlag2v52>();

            if (pibVersion <= PibVersion.Y5)
                values = Enum.GetNames<EmitterFlag2v21>();

            if (values == null)
            {
                List<string> strings = new List<string>();

                for (int i = 0; i < GetFlagLength(); i++)
                    strings.Add("Flag " + (i + 1));

                values = strings.ToArray();
            }

            return values;
        }

        private string[] GetFlag3List()
        {
            string[] values = null;

            if (pibVersion == PibVersion.Y0 || pibVersion == PibVersion.Ishin)
                values = Enum.GetNames<EmitterFlag3v27>();

            if (pibVersion >= PibVersion.YLAD)
                values = Enum.GetNames<EmitterFlag3v52>();
            else if (pibVersion > PibVersion.Y6)
                values = Enum.GetNames<EmitterFlag3v43>();
            else if (pibVersion == PibVersion.Y6)
                values = Enum.GetNames<EmitterFlag3v29>();

            if (values == null)
            {
                List<string> strings = new List<string>();

                for (int i = 0; i < GetFlagLength(); i++)
                    strings.Add("Flag " + (i + 1));

                values = strings.ToArray();
            }

            return values;
        }

        private string[] GetOEFlag4List()
        {
            string[] values = null;

            if (pibVersion == PibVersion.Y5)
                values = Enum.GetNames<EmitterFlag4v21>();
            else if (pibVersion >= PibVersion.Ishin)
                values = Enum.GetNames<EmitterFlag4v27>();

            if (values == null)
            {
                List<string> strings = new List<string>();

                for (int i = 0; i < GetFlagLength(); i++)
                    strings.Add("Flag " + (i + 1));

                values = strings.ToArray();
            }

            return values;
        }

        public Control CreateText(string label, bool left = false)
        {
            Label text = new Label();

            if (!left)
                text.Anchor = AnchorStyles.Right;
            else
                text.Anchor = AnchorStyles.Left;

            text.AutoSize = true;
            text.Size = new Size(58, 15);
            text.TabIndex = 1;
            text.Text = label;

            return text;
        }



        public void CreateHeader(string label)
        {
            Label label2 = new Label();
            label2.Anchor = AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Arial", 16F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(42, 5);
            label2.Size = new Size(195, 10);
            label2.TabIndex = 0;
            label2.Text = label;

            varPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            varPanel.RowCount++;

            varPanel.Controls.Add(label2, 0, varPanel.RowCount - 1);
            varPanel.Controls.Add(CreateText(""), 1, varPanel.RowCount - 1);
        }

        public TextBox CreateInput(string label, string defaultValue, Action<string> editedCallback, NumberBox.NumberMode mode = NumberBox.NumberMode.Text, bool readOnly = false)
        {
            varPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));
            varPanel.RowCount++;

            NumberBox input = new NumberBox(mode, editedCallback);
            input.Text = defaultValue;
            input.Size = new Size(200, 15);
            input.ReadOnly = readOnly;
            input.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            Control text = CreateText(label, false);

            varPanel.Controls.Add(text, 0, varPanel.RowCount - 1);
            varPanel.Controls.Add(input, 1, varPanel.RowCount - 1);

            return input;
        }

        public Button CreateButton(string text, Action clicked, float size = 25, float textSize = 8)
        {
            Button input = new Button();
            input.Text = text;
            input.Size = new Size(200, (int)size);

            input.Font = new Font(input.Font.Name, textSize);

            varPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, size));
            varPanel.RowCount++;
            varPanel.Controls.Add(CreateText(""), 0, varPanel.RowCount - 1);
            varPanel.Controls.Add(input, 1, varPanel.RowCount - 1);


            input.Click += delegate { clicked?.Invoke(); };

            return input;
        }

        public Panel CreatePanel(string label, Color color, Action<Color> finished, bool isCsvTree = false)
        {
            varPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));
            varPanel.RowCount++;

            Panel input = new Panel();
            input.BorderStyle = BorderStyle.Fixed3D;
            input.Size = new Size(200, 50);
            input.Click += delegate
            {
                ColorView myNewForm = new ColorView();
                myNewForm.Visible = true;
                myNewForm.Init(input.BackColor, finished);
            };
            input.BackColor = color;

            varPanel.Controls.Add(CreateText(label), 0, varPanel.RowCount - 1);
            varPanel.Controls.Add(input, 1, varPanel.RowCount - 1);

            return input;
        }

        public void CreateSpace(float space, bool isCsvTree = false)
        {
            varPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, space));
            varPanel.RowCount++;

            varPanel.Controls.Add(CreateText("", isCsvTree), 0, varPanel.RowCount - 1);
            varPanel.Controls.Add(CreateText("", isCsvTree), 1, varPanel.RowCount - 1);
        }

        private void SavePib()
        {
            PIB.Write((pibTree.Nodes[0] as TreeNodePib).Pib, filePath);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SavePib();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pIBConvertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PIBConvert window = new PIBConvert();
            window.Visible = true;
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            filePath = dialog.FileName;
            SavePib();
        }

        private void findPIBByIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            string input = Microsoft.VisualBasic.Interaction.InputBox("Pib ID",
           "Pib ID (decimal)",
           "",
           0,
           0);

            if (string.IsNullOrEmpty(input))
                return;

            uint val = 0;

            if (!uint.TryParse(input, out val))
                return;

            bool found = false;

            foreach (string pibFile in Directory.GetFiles(dialog.SelectedPath, "*.pib", SearchOption.AllDirectories))
            {
                BasePib pib = null;

                try
                {
                    pib = PIB.Read(pibFile);
                }
                catch
                {
                    continue;
                }

                if (pib.ParticleID == val)
                {
                    MessageBox.Show(pibFile);
                    found = true;
                    break;
                }
            }

            if (!found)
                MessageBox.Show("Couldn't find PIB with ID " + val);
        }

        private void changeColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(filePath))
                return;

            if (pibTree.SelectedNode == null || pibTree.SelectedNode as TreeNodePibEmitter == null)
                return;

            TreeNodePibEmitter emitterNode = pibTree.SelectedNode as TreeNodePibEmitter;
            BasePibEmitter emitter = emitterNode.Emitter;

            Color baseColor = new Color();

            if(emitter.IsUseColorCurve())
            {
                PibEmitterAnimationCurveColor colorCurve = emitter.GetColorCurve();
                baseColor = colorCurve.Values[0];
            }
            else if(emitter.IsMetaball())
            {
                baseColor = emitter.Metaball.Color;
            }
            else
            {
                if (emitter.Source.Particles.Count > 0)
                    baseColor = emitter.Source.Particles[0].Color;
            }


            ColorView colView = new ColorView();
            colView.Init(baseColor, delegate(Color newCol)
            {
                //dont touch alpha

                if (emitter.IsUseColorCurve())
                {
                    PibEmitterAnimationCurveColor colorCurve = emitter.GetColorCurve();
                    RGBA32F newColor = newCol;
                    
                    for(int i = 0; i < colorCurve.Values.Length; i++)
                    {
                        RGBA32F col = colorCurve.Values[i];
                        col.R = newCol.R;
                        col.G = newCol.G;
                        col.B = newCol.B;

                        colorCurve.Values[i] = col;
                    }
                }
                else if (emitter.IsMetaball())
                {
                    emitter.Metaball.Color = newCol;
                }
                else
                {
                    for (int i = 0; i < emitter.Source.Particles.Count; i++)
                    {
                        RGBA col = emitter.Source.Particles[i].Color;
                        col.R = newCol.R;
                        col.G = newCol.G;
                        col.B = newCol.B;

                        emitter.Source.Particles[i].Color = col;
                    }
                }
            });
            colView.Visible = true;
        }
    }
}