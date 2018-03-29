namespace Vang_de_volger
{
    partial class MainForm
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pbLevel = new System.Windows.Forms.PictureBox();
            this.pause_Label = new System.Windows.Forms.Label();
            this.restart_Button = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(858, 50);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 22);
            this.textBox1.TabIndex = 3;
            // 
            // pbLevel
            // 
            this.pbLevel.BackColor = System.Drawing.Color.Transparent;
            this.pbLevel.Location = new System.Drawing.Point(3, 2);
            this.pbLevel.Name = "pbLevel";
            this.pbLevel.Size = new System.Drawing.Size(649, 507);
            this.pbLevel.TabIndex = 1;
            this.pbLevel.TabStop = false;
            // 
            // pause_Label
            // 
            this.pause_Label.AutoSize = true;
            this.pause_Label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pause_Label.Location = new System.Drawing.Point(834, 111);
            this.pause_Label.Name = "pause_Label";
            this.pause_Label.Size = new System.Drawing.Size(87, 19);
            this.pause_Label.TabIndex = 4;
            this.pause_Label.Text = "Pause(ESC)";
            this.pause_Label.Click += new System.EventHandler(this.pause_Label_Click);
            // 
            // restart_Button
            // 
            this.restart_Button.AutoSize = true;
            this.restart_Button.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.restart_Button.Location = new System.Drawing.Point(834, 148);
            this.restart_Button.Name = "restart_Button";
            this.restart_Button.Size = new System.Drawing.Size(56, 19);
            this.restart_Button.TabIndex = 5;
            this.restart_Button.Text = "Restart";
            this.restart_Button.Click += new System.EventHandler(this.restart_Button_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(980, 578);
            this.Controls.Add(this.restart_Button);
            this.Controls.Add(this.pause_Label);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.pbLevel);
            this.Name = "MainForm";
            this.Text = "Vang de volger";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pbLevel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pbLevel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label pause_Label;
        private System.Windows.Forms.Label restart_Button;
    }
}

