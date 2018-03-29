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
            this.pbLevel = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.reset_Button = new System.Windows.Forms.Button();
            this.pause_Button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbLevel)).BeginInit();
            this.SuspendLayout();
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
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(858, 50);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 22);
            this.textBox1.TabIndex = 3;
            // 
            // reset_Button
            // 
            this.reset_Button.Location = new System.Drawing.Point(858, 90);
            this.reset_Button.Name = "reset_Button";
            this.reset_Button.Size = new System.Drawing.Size(100, 23);
            this.reset_Button.TabIndex = 4;
            this.reset_Button.Text = "Reset";
            this.reset_Button.UseVisualStyleBackColor = true;
            this.reset_Button.Click += new System.EventHandler(this.reset_Button_Click);
            // 
            // pause_Button
            // 
            this.pause_Button.Location = new System.Drawing.Point(858, 131);
            this.pause_Button.Name = "pause_Button";
            this.pause_Button.Size = new System.Drawing.Size(100, 23);
            this.pause_Button.TabIndex = 5;
            this.pause_Button.Text = "Pause(esc)";
            this.pause_Button.UseVisualStyleBackColor = true;
            this.pause_Button.Click += new System.EventHandler(this.pause_Button_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(980, 578);
            this.Controls.Add(this.pause_Button);
            this.Controls.Add(this.reset_Button);
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
        private System.Windows.Forms.Button reset_Button;
        private System.Windows.Forms.Button pause_Button;
    }
}

