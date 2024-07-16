namespace PIBView
{
    partial class PIBConvert
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
            Label label1;
            Label label2;
            Label label3;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PIBConvert));
            pathBox = new TextBox();
            fileButton = new Button();
            inputBox = new ComboBox();
            outputBox = new ComboBox();
            convertButton = new Button();
            copyTexturesCheck = new CheckBox();
            idChangeCheck = new CheckBox();
            changeIdTextbox = new TextBox();
            toolStrip1 = new ToolStrip();
            debugTab = new ToolStripDropDownButton();
            justOpenPIBToolStripMenuItem = new ToolStripMenuItem();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 25);
            label1.Name = "label1";
            label1.Size = new Size(70, 15);
            label1.TabIndex = 1;
            label1.Text = "Pib file path";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(40, 81);
            label2.Name = "label2";
            label2.Size = new Size(35, 15);
            label2.TabIndex = 3;
            label2.Text = "Input";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(172, 81);
            label3.Name = "label3";
            label3.Size = new Size(45, 15);
            label3.TabIndex = 5;
            label3.Text = "Output";
            // 
            // pathBox
            // 
            pathBox.Location = new Point(12, 44);
            pathBox.Name = "pathBox";
            pathBox.Size = new Size(521, 23);
            pathBox.TabIndex = 0;
            pathBox.TextChanged += pathBox_TextChanged;
            // 
            // fileButton
            // 
            fileButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            fileButton.Location = new Point(535, 44);
            fileButton.Name = "fileButton";
            fileButton.Size = new Size(29, 23);
            fileButton.TabIndex = 2;
            fileButton.Text = "...";
            fileButton.UseVisualStyleBackColor = true;
            fileButton.Click += fileButton_Click;
            // 
            // inputBox
            // 
            inputBox.FormattingEnabled = true;
            inputBox.Location = new Point(14, 99);
            inputBox.Name = "inputBox";
            inputBox.Size = new Size(96, 23);
            inputBox.TabIndex = 4;
            // 
            // outputBox
            // 
            outputBox.FormattingEnabled = true;
            outputBox.Location = new Point(146, 99);
            outputBox.Name = "outputBox";
            outputBox.Size = new Size(96, 23);
            outputBox.TabIndex = 6;
            // 
            // convertButton
            // 
            convertButton.Font = new Font("Segoe UI", 26F, FontStyle.Regular, GraphicsUnit.Point);
            convertButton.Location = new Point(277, 81);
            convertButton.Name = "convertButton";
            convertButton.Size = new Size(287, 59);
            convertButton.TabIndex = 7;
            convertButton.Text = "Convert";
            convertButton.UseVisualStyleBackColor = true;
            convertButton.Click += convertButton_Click;
            convertButton.Paint += button2_Paint;
            convertButton.Layout += convertButton_Layout;
            // 
            // copyTexturesCheck
            // 
            copyTexturesCheck.AutoSize = true;
            copyTexturesCheck.Location = new Point(12, 159);
            copyTexturesCheck.Name = "copyTexturesCheck";
            copyTexturesCheck.Size = new Size(100, 19);
            copyTexturesCheck.TabIndex = 8;
            copyTexturesCheck.Text = "Copy Textures";
            copyTexturesCheck.UseVisualStyleBackColor = true;
            // 
            // idChangeCheck
            // 
            idChangeCheck.AutoSize = true;
            idChangeCheck.Location = new Point(12, 184);
            idChangeCheck.Name = "idChangeCheck";
            idChangeCheck.Size = new Size(81, 19);
            idChangeCheck.TabIndex = 9;
            idChangeCheck.Text = "Change ID";
            idChangeCheck.UseVisualStyleBackColor = true;
            idChangeCheck.CheckedChanged += idChangeBox_CheckedChanged;
            // 
            // changeIdTextbox
            // 
            changeIdTextbox.Enabled = false;
            changeIdTextbox.Location = new Point(27, 205);
            changeIdTextbox.Name = "changeIdTextbox";
            changeIdTextbox.Size = new Size(100, 23);
            changeIdTextbox.TabIndex = 10;
            changeIdTextbox.Text = "0";
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { debugTab });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(581, 25);
            toolStrip1.TabIndex = 11;
            toolStrip1.Text = "toolStrip1";
            // 
            // debugTab
            // 
            debugTab.DisplayStyle = ToolStripItemDisplayStyle.Text;
            debugTab.DropDownItems.AddRange(new ToolStripItem[] { justOpenPIBToolStripMenuItem });
            debugTab.Image = (Image)resources.GetObject("debugTab.Image");
            debugTab.ImageTransparentColor = Color.Magenta;
            debugTab.Name = "debugTab";
            debugTab.Size = new Size(55, 22);
            debugTab.Text = "Debug";
            // 
            // justOpenPIBToolStripMenuItem
            // 
            justOpenPIBToolStripMenuItem.Name = "justOpenPIBToolStripMenuItem";
            justOpenPIBToolStripMenuItem.Size = new Size(146, 22);
            justOpenPIBToolStripMenuItem.Text = "Just Open PIB";
            justOpenPIBToolStripMenuItem.Click += justOpenPIBToolStripMenuItem_Click;
            // 
            // PIBConvert
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(581, 240);
            Controls.Add(toolStrip1);
            Controls.Add(changeIdTextbox);
            Controls.Add(idChangeCheck);
            Controls.Add(copyTexturesCheck);
            Controls.Add(convertButton);
            Controls.Add(outputBox);
            Controls.Add(label3);
            Controls.Add(inputBox);
            Controls.Add(label2);
            Controls.Add(fileButton);
            Controls.Add(label1);
            Controls.Add(pathBox);
            Name = "PIBConvert";
            Text = "Pib Converter GUI";
            FormClosing += PIBConvert_FormClosing;
            Load += Form1_Load;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox pathBox;
        private System.Windows.Forms.Button fileButton;
        private System.Windows.Forms.ComboBox inputBox;
        private System.Windows.Forms.ComboBox outputBox;
        private System.Windows.Forms.Button convertButton;
        private System.Windows.Forms.CheckBox copyTexturesCheck;
        private System.Windows.Forms.CheckBox idChangeCheck;
        private System.Windows.Forms.TextBox changeIdTextbox;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton debugTab;
        private System.Windows.Forms.ToolStripMenuItem justOpenPIBToolStripMenuItem;
    }
}
