
namespace Rolling_wheel
{
    partial class Form2
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
            this.Rad1TrackBar = new System.Windows.Forms.TrackBar();
            this.OkButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Rad1TrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // Rad1TrackBar
            // 
            this.Rad1TrackBar.Location = new System.Drawing.Point(233, 168);
            this.Rad1TrackBar.Maximum = 60;
            this.Rad1TrackBar.Minimum = 1;
            this.Rad1TrackBar.Name = "Rad1TrackBar";
            this.Rad1TrackBar.Size = new System.Drawing.Size(211, 48);
            this.Rad1TrackBar.TabIndex = 0;
            this.Rad1TrackBar.Value = 1;
            this.Rad1TrackBar.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(590, 130);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "button1";
            this.OkButton.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.Rad1TrackBar);
            this.Name = "Form2";
            this.ShowInTaskbar = false;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Rad1TrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar Rad1TrackBar;
        private System.Windows.Forms.Button OkButton;
    }
}