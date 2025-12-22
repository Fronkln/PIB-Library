namespace PIBView.Window
{
    partial class ColorChangePrompt
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            animationCurveRadio = new RadioButton();
            particleRadio = new RadioButton();
            metaballRadio = new RadioButton();
            button1 = new Button();
            SuspendLayout();
            // 
            // animationCurveRadio
            // 
            animationCurveRadio.AutoSize = true;
            animationCurveRadio.Location = new Point(59, 23);
            animationCurveRadio.Name = "animationCurveRadio";
            animationCurveRadio.Size = new Size(115, 19);
            animationCurveRadio.TabIndex = 0;
            animationCurveRadio.TabStop = true;
            animationCurveRadio.Text = "Animation Curve";
            animationCurveRadio.UseVisualStyleBackColor = true;
            animationCurveRadio.CheckedChanged += animationCurveRadio_CheckedChanged;
            // 
            // particleRadio
            // 
            particleRadio.AutoSize = true;
            particleRadio.Location = new Point(59, 48);
            particleRadio.Name = "particleRadio";
            particleRadio.Size = new Size(64, 19);
            particleRadio.TabIndex = 1;
            particleRadio.TabStop = true;
            particleRadio.Text = "Particle";
            particleRadio.UseVisualStyleBackColor = true;
            particleRadio.CheckedChanged += particleRadio_CheckedChanged;
            // 
            // metaballRadio
            // 
            metaballRadio.AutoSize = true;
            metaballRadio.Location = new Point(59, 73);
            metaballRadio.Name = "metaballRadio";
            metaballRadio.Size = new Size(71, 19);
            metaballRadio.TabIndex = 2;
            metaballRadio.TabStop = true;
            metaballRadio.Text = "Metaball";
            metaballRadio.UseVisualStyleBackColor = true;
            metaballRadio.CheckedChanged += metaballRadio_CheckedChanged;
            // 
            // button1
            // 
            button1.Location = new Point(49, 112);
            button1.Name = "button1";
            button1.Size = new Size(125, 36);
            button1.TabIndex = 3;
            button1.Text = "Select";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // ColorChangePrompt
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(242, 160);
            Controls.Add(button1);
            Controls.Add(metaballRadio);
            Controls.Add(particleRadio);
            Controls.Add(animationCurveRadio);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "ColorChangePrompt";
            Text = "Select Color To Edit";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RadioButton animationCurveRadio;
        private RadioButton particleRadio;
        private RadioButton metaballRadio;
        private Button button1;
    }
}