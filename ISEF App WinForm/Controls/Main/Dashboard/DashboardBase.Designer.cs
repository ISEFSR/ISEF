namespace cvti.isef.winformapp.Controls.Main.Dashboard
{
    partial class DashboardBase
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
            this.panelControlsWrapper = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panelControlsWrapper
            // 
            this.panelControlsWrapper.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControlsWrapper.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelControlsWrapper.Location = new System.Drawing.Point(10, 10);
            this.panelControlsWrapper.Margin = new System.Windows.Forms.Padding(10);
            this.panelControlsWrapper.Name = "panelControlsWrapper";
            this.panelControlsWrapper.Size = new System.Drawing.Size(346, 223);
            this.panelControlsWrapper.TabIndex = 0;
            // 
            // DashboardBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControlsWrapper);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "DashboardBase";
            this.Size = new System.Drawing.Size(366, 243);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelControlsWrapper;
    }
}
