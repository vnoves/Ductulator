namespace Ductulator
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.DuctTypeBox = new System.Windows.Forms.TextBox();
            this.DuctSizeBox = new System.Windows.Forms.TextBox();
            this.ProposedDuctHeight = new System.Windows.Forms.TextBox();
            this.RoundDuctEquivalentLabel = new System.Windows.Forms.Label();
            this.RoundDuctText = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.WidthProposed = new System.Windows.Forms.ComboBox();
            this.button4 = new System.Windows.Forms.Button();
            this.TypeOfDuctChoise = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Duct Size :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Duct Type :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Propose new duct width :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 193);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "New duct height :";
            // 
            // DuctTypeBox
            // 
            this.DuctTypeBox.Location = new System.Drawing.Point(214, 26);
            this.DuctTypeBox.Name = "DuctTypeBox";
            this.DuctTypeBox.ReadOnly = true;
            this.DuctTypeBox.Size = new System.Drawing.Size(120, 20);
            this.DuctTypeBox.TabIndex = 5;
            // 
            // DuctSizeBox
            // 
            this.DuctSizeBox.Location = new System.Drawing.Point(214, 71);
            this.DuctSizeBox.Name = "DuctSizeBox";
            this.DuctSizeBox.ReadOnly = true;
            this.DuctSizeBox.Size = new System.Drawing.Size(120, 20);
            this.DuctSizeBox.TabIndex = 6;
            // 
            // ProposedDuctHeight
            // 
            this.ProposedDuctHeight.Location = new System.Drawing.Point(214, 190);
            this.ProposedDuctHeight.Name = "ProposedDuctHeight";
            this.ProposedDuctHeight.ReadOnly = true;
            this.ProposedDuctHeight.Size = new System.Drawing.Size(120, 20);
            this.ProposedDuctHeight.TabIndex = 7;
            // 
            // RoundDuctEquivalentLabel
            // 
            this.RoundDuctEquivalentLabel.AutoSize = true;
            this.RoundDuctEquivalentLabel.Location = new System.Drawing.Point(31, 240);
            this.RoundDuctEquivalentLabel.Name = "RoundDuctEquivalentLabel";
            this.RoundDuctEquivalentLabel.Size = new System.Drawing.Size(118, 13);
            this.RoundDuctEquivalentLabel.TabIndex = 8;
            this.RoundDuctEquivalentLabel.Text = "Round duct equivalent:";
            this.RoundDuctEquivalentLabel.Click += new System.EventHandler(this.label5_Click);
            // 
            // RoundDuctText
            // 
            this.RoundDuctText.Location = new System.Drawing.Point(214, 237);
            this.RoundDuctText.Name = "RoundDuctText";
            this.RoundDuctText.ReadOnly = true;
            this.RoundDuctText.Size = new System.Drawing.Size(120, 20);
            this.RoundDuctText.TabIndex = 9;
            this.RoundDuctText.TextChanged += new System.EventHandler(this.RoundDuctText_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(340, 193);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "in";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(340, 244);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(16, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "⌀";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(340, 154);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(15, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "in";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(361, 101);
            this.panel1.TabIndex = 14;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.WidthProposed);
            this.panel2.Controls.Add(this.button4);
            this.panel2.Location = new System.Drawing.Point(12, 130);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(361, 218);
            this.panel2.TabIndex = 15;
            // 
            // WidthProposed
            // 
            this.WidthProposed.FormattingEnabled = true;
            this.WidthProposed.Location = new System.Drawing.Point(201, 15);
            this.WidthProposed.Name = "WidthProposed";
            this.WidthProposed.Size = new System.Drawing.Size(121, 21);
            this.WidthProposed.TabIndex = 20;
            this.WidthProposed.SelectedIndexChanged += new System.EventHandler(this.WidthProposed_SelectedIndexChanged);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(20, 151);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(322, 37);
            this.button4.TabIndex = 19;
            this.button4.Text = "Calculate";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // TypeOfDuctChoise
            // 
            this.TypeOfDuctChoise.FormattingEnabled = true;
            this.TypeOfDuctChoise.Location = new System.Drawing.Point(150, 369);
            this.TypeOfDuctChoise.Name = "TypeOfDuctChoise";
            this.TypeOfDuctChoise.Size = new System.Drawing.Size(205, 21);
            this.TypeOfDuctChoise.TabIndex = 18;
            this.TypeOfDuctChoise.SelectedIndexChanged += new System.EventHandler(this.TypeOfDuctChoise_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 372);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Choose a type of duct:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(33, 406);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(323, 54);
            this.button2.TabIndex = 20;
            this.button2.Text = "Transform";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 476);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TypeOfDuctChoise);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.RoundDuctText);
            this.Controls.Add(this.RoundDuctEquivalentLabel);
            this.Controls.Add(this.ProposedDuctHeight);
            this.Controls.Add(this.DuctSizeBox);
            this.Controls.Add(this.DuctTypeBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ductulator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox DuctTypeBox;
        private System.Windows.Forms.TextBox DuctSizeBox;
        private System.Windows.Forms.TextBox ProposedDuctHeight;
        private System.Windows.Forms.Label RoundDuctEquivalentLabel;
        private System.Windows.Forms.TextBox RoundDuctText;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ComboBox WidthProposed;
        private System.Windows.Forms.ComboBox TypeOfDuctChoise;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button2;
    }
}