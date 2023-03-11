namespace PIB_Converter_GUI
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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            this.pathBox = new System.Windows.Forms.TextBox();
            this.fileButton = new System.Windows.Forms.Button();
            this.inputBox = new System.Windows.Forms.ComboBox();
            this.outputBox = new System.Windows.Forms.ComboBox();
            this.convertButton = new System.Windows.Forms.Button();
            this.copyTexturesCheck = new System.Windows.Forms.CheckBox();
            this.idChangeCheck = new System.Windows.Forms.CheckBox();
            this.changeIdTextbox = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(15, 25);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(70, 15);
            label1.TabIndex = 1;
            label1.Text = "Pib file path";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(40, 81);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(35, 15);
            label2.TabIndex = 3;
            label2.Text = "Input";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(172, 81);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(45, 15);
            label3.TabIndex = 5;
            label3.Text = "Output";
            // 
            // pathBox
            // 
            this.pathBox.Location = new System.Drawing.Point(12, 44);
            this.pathBox.Name = "pathBox";
            this.pathBox.Size = new System.Drawing.Size(521, 23);
            this.pathBox.TabIndex = 0;
            this.pathBox.TextChanged += new System.EventHandler(this.pathBox_TextChanged);
            // 
            // fileButton
            // 
            this.fileButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.fileButton.Location = new System.Drawing.Point(535, 44);
            this.fileButton.Name = "fileButton";
            this.fileButton.Size = new System.Drawing.Size(29, 23);
            this.fileButton.TabIndex = 2;
            this.fileButton.Text = "...";
            this.fileButton.UseVisualStyleBackColor = true;
            this.fileButton.Click += new System.EventHandler(this.fileButton_Click);
            // 
            // inputBox
            // 
            this.inputBox.FormattingEnabled = true;
            this.inputBox.Location = new System.Drawing.Point(14, 99);
            this.inputBox.Name = "inputBox";
            this.inputBox.Size = new System.Drawing.Size(96, 23);
            this.inputBox.TabIndex = 4;
            // 
            // outputBox
            // 
            this.outputBox.FormattingEnabled = true;
            this.outputBox.Location = new System.Drawing.Point(146, 99);
            this.outputBox.Name = "outputBox";
            this.outputBox.Size = new System.Drawing.Size(96, 23);
            this.outputBox.TabIndex = 6;
            // 
            // convertButton
            // 
            this.convertButton.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.convertButton.Location = new System.Drawing.Point(277, 81);
            this.convertButton.Name = "convertButton";
            this.convertButton.Size = new System.Drawing.Size(287, 59);
            this.convertButton.TabIndex = 7;
            this.convertButton.Text = "Convert";
            this.convertButton.UseVisualStyleBackColor = true;
            this.convertButton.Click += new System.EventHandler(this.convertButton_Click);
            this.convertButton.Paint += new System.Windows.Forms.PaintEventHandler(this.button2_Paint);
            this.convertButton.Layout += new System.Windows.Forms.LayoutEventHandler(this.convertButton_Layout);
            // 
            // copyTexturesCheck
            // 
            this.copyTexturesCheck.AutoSize = true;
            this.copyTexturesCheck.Location = new System.Drawing.Point(12, 159);
            this.copyTexturesCheck.Name = "copyTexturesCheck";
            this.copyTexturesCheck.Size = new System.Drawing.Size(100, 19);
            this.copyTexturesCheck.TabIndex = 8;
            this.copyTexturesCheck.Text = "Copy Textures";
            this.copyTexturesCheck.UseVisualStyleBackColor = true;
            // 
            // idChangeCheck
            // 
            this.idChangeCheck.AutoSize = true;
            this.idChangeCheck.Location = new System.Drawing.Point(12, 184);
            this.idChangeCheck.Name = "idChangeCheck";
            this.idChangeCheck.Size = new System.Drawing.Size(81, 19);
            this.idChangeCheck.TabIndex = 9;
            this.idChangeCheck.Text = "Change ID";
            this.idChangeCheck.UseVisualStyleBackColor = true;
            this.idChangeCheck.CheckedChanged += new System.EventHandler(this.idChangeBox_CheckedChanged);
            // 
            // changeIdTextbox
            // 
            this.changeIdTextbox.Enabled = false;
            this.changeIdTextbox.Location = new System.Drawing.Point(27, 205);
            this.changeIdTextbox.Name = "changeIdTextbox";
            this.changeIdTextbox.Size = new System.Drawing.Size(100, 23);
            this.changeIdTextbox.TabIndex = 10;
            this.changeIdTextbox.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 240);
            this.Controls.Add(this.changeIdTextbox);
            this.Controls.Add(this.idChangeCheck);
            this.Controls.Add(this.copyTexturesCheck);
            this.Controls.Add(this.convertButton);
            this.Controls.Add(this.outputBox);
            this.Controls.Add(label3);
            this.Controls.Add(this.inputBox);
            this.Controls.Add(label2);
            this.Controls.Add(this.fileButton);
            this.Controls.Add(label1);
            this.Controls.Add(this.pathBox);
            this.Name = "Form1";
            this.Text = "Pib Converter GUI";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}
