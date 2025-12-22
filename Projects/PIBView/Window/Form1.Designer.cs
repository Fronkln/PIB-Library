namespace PIBView
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            toolStrip1 = new ToolStrip();
            toolStripDropDownButton1 = new ToolStripDropDownButton();
            openToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            toolStripSplitButton1 = new ToolStripSplitButton();
            pIBConvertToolStripMenuItem = new ToolStripMenuItem();
            findPIBByIDToolStripMenuItem = new ToolStripMenuItem();
            changeColorToolStripMenuItem = new ToolStripMenuItem();
            pibTree = new TreeView();
            varPanel = new TableLayoutPanel();
            panel1 = new Panel();
            toolStrip1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripDropDownButton1, toolStripSplitButton1 });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(918, 25);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            toolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, saveToolStripMenuItem, saveAsToolStripMenuItem });
            toolStripDropDownButton1.Image = (Image)resources.GetObject("toolStripDropDownButton1.Image");
            toolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            toolStripDropDownButton1.Size = new Size(38, 22);
            toolStripDropDownButton1.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(114, 22);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(114, 22);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new Size(114, 22);
            saveAsToolStripMenuItem.Text = "Save As";
            saveAsToolStripMenuItem.Click += saveAsToolStripMenuItem_Click;
            // 
            // toolStripSplitButton1
            // 
            toolStripSplitButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripSplitButton1.DropDownItems.AddRange(new ToolStripItem[] { pIBConvertToolStripMenuItem, findPIBByIDToolStripMenuItem, changeColorToolStripMenuItem });
            toolStripSplitButton1.Image = (Image)resources.GetObject("toolStripSplitButton1.Image");
            toolStripSplitButton1.ImageTransparentColor = Color.Magenta;
            toolStripSplitButton1.Name = "toolStripSplitButton1";
            toolStripSplitButton1.Size = new Size(45, 22);
            toolStripSplitButton1.Text = "Tool";
            toolStripSplitButton1.ToolTipText = "Tool";
            // 
            // pIBConvertToolStripMenuItem
            // 
            pIBConvertToolStripMenuItem.Name = "pIBConvertToolStripMenuItem";
            pIBConvertToolStripMenuItem.Size = new Size(180, 22);
            pIBConvertToolStripMenuItem.Text = "PIB Convert";
            pIBConvertToolStripMenuItem.Click += pIBConvertToolStripMenuItem_Click;
            // 
            // findPIBByIDToolStripMenuItem
            // 
            findPIBByIDToolStripMenuItem.Name = "findPIBByIDToolStripMenuItem";
            findPIBByIDToolStripMenuItem.Size = new Size(180, 22);
            findPIBByIDToolStripMenuItem.Text = "Find PIB by ID";
            findPIBByIDToolStripMenuItem.Click += findPIBByIDToolStripMenuItem_Click;
            // 
            // changeColorToolStripMenuItem
            // 
            changeColorToolStripMenuItem.Name = "changeColorToolStripMenuItem";
            changeColorToolStripMenuItem.Size = new Size(180, 22);
            changeColorToolStripMenuItem.Text = "Change Color";
            changeColorToolStripMenuItem.Click += changeColorToolStripMenuItem_Click;
            // 
            // pibTree
            // 
            pibTree.Location = new Point(12, 28);
            pibTree.Name = "pibTree";
            pibTree.Size = new Size(218, 337);
            pibTree.TabIndex = 1;
            pibTree.AfterSelect += pibTree_AfterSelect;
            // 
            // varPanel
            // 
            varPanel.AutoScroll = true;
            varPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            varPanel.ColumnCount = 2;
            varPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 240F));
            varPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 317F));
            varPanel.Location = new Point(3, 3);
            varPanel.Name = "varPanel";
            varPanel.RowCount = 1;
            varPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            varPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            varPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            varPanel.Size = new Size(664, 331);
            varPanel.TabIndex = 2;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(varPanel);
            panel1.Location = new Point(236, 28);
            panel1.Name = "panel1";
            panel1.Size = new Size(670, 337);
            panel1.TabIndex = 3;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(918, 383);
            Controls.Add(panel1);
            Controls.Add(pibTree);
            Controls.Add(toolStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "PIBView by Jhrino";
            Load += Form1_Load;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private TableLayoutPanel varPanel;
        private Panel panel1;
        private ToolStripSplitButton toolStripSplitButton1;
        private ToolStripMenuItem pIBConvertToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripMenuItem findPIBByIDToolStripMenuItem;
        private ToolStripMenuItem changeColorToolStripMenuItem;
        public TreeView pibTree;
    }
}