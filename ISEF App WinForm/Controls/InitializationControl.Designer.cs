namespace cvti.isef.winformapp.Controls
{
    partial class InitializationControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.progressBarLoadingIndicator = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // progressBarLoadingIndicator
            // 
            this.progressBarLoadingIndicator.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBarLoadingIndicator.ForeColor = System.Drawing.SystemColors.ControlText;
            this.progressBarLoadingIndicator.Location = new System.Drawing.Point(0, 540);
            this.progressBarLoadingIndicator.MarqueeAnimationSpeed = 10;
            this.progressBarLoadingIndicator.Name = "progressBarLoadingIndicator";
            this.progressBarLoadingIndicator.Size = new System.Drawing.Size(859, 25);
            this.progressBarLoadingIndicator.Step = 25;
            this.progressBarLoadingIndicator.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarLoadingIndicator.TabIndex = 0;
            // 
            // InitializationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.BackgroundImage = global::cvti.isef.winformapp.Properties.Resources.isefinit5;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Controls.Add(this.progressBarLoadingIndicator);
            this.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "InitializationControl";
            this.Size = new System.Drawing.Size(859, 565);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBarLoadingIndicator;
    }
}
