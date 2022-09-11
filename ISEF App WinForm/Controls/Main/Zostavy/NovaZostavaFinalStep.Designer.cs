namespace cvti.isef.winformapp.Controls.Main.Zostavy
{
    partial class NovaZostavaFinalStep
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
            this.pictureBoxOk = new System.Windows.Forms.PictureBox();
            this.labelInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOk)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxOk
            // 
            this.pictureBoxOk.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBoxOk.Image = global::cvti.isef.winformapp.Properties.Resources.ok500;
            this.pictureBoxOk.Location = new System.Drawing.Point(203, 87);
            this.pictureBoxOk.Name = "pictureBoxOk";
            this.pictureBoxOk.Size = new System.Drawing.Size(364, 347);
            this.pictureBoxOk.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxOk.TabIndex = 0;
            this.pictureBoxOk.TabStop = false;
            // 
            // labelInfo
            // 
            this.labelInfo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelInfo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(171)))), ((int)(((byte)(101)))));
            this.labelInfo.Location = new System.Drawing.Point(158, 437);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(454, 48);
            this.labelInfo.TabIndex = 1;
            this.labelInfo.Text = "Nová zostava úspeśne vytvorená";
            this.labelInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // NovaZostavaFinalStep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.pictureBoxOk);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "NovaZostavaFinalStep";
            this.Size = new System.Drawing.Size(771, 521);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOk)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxOk;
        private System.Windows.Forms.Label labelInfo;
    }
}
