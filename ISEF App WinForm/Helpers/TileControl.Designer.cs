namespace cvti.isef.winformapp.Controls.Main.Import
{
    partial class TileControl
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
            this.panelContent = new System.Windows.Forms.Panel();
            this.pictureBoxTypeImage = new System.Windows.Forms.PictureBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTypeImage)).BeginInit();
            this.SuspendLayout();
            // 
            // panelContent
            // 
            this.panelContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContent.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panelContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelContent.Controls.Add(this.pictureBoxTypeImage);
            this.panelContent.Controls.Add(this.labelTitle);
            this.panelContent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelContent.Location = new System.Drawing.Point(15, 15);
            this.panelContent.Margin = new System.Windows.Forms.Padding(15);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(188, 185);
            this.panelContent.TabIndex = 2;
            this.panelContent.Click += new System.EventHandler(this.tileContent_Click);
            this.panelContent.MouseEnter += new System.EventHandler(this.panelContent_MouseEnter);
            this.panelContent.MouseLeave += new System.EventHandler(this.panelContent_MouseLeave);
            // 
            // pictureBoxTypeImage
            // 
            this.pictureBoxTypeImage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBoxTypeImage.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxTypeImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxTypeImage.Image = global::cvti.isef.winformapp.Properties.Resources.notfound140;
            this.pictureBoxTypeImage.Location = new System.Drawing.Point(23, 36);
            this.pictureBoxTypeImage.Margin = new System.Windows.Forms.Padding(20, 40, 20, 20);
            this.pictureBoxTypeImage.Name = "pictureBoxTypeImage";
            this.pictureBoxTypeImage.Size = new System.Drawing.Size(140, 140);
            this.pictureBoxTypeImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxTypeImage.TabIndex = 1;
            this.pictureBoxTypeImage.TabStop = false;
            this.pictureBoxTypeImage.Click += new System.EventHandler(this.tileContent_Click);
            this.pictureBoxTypeImage.MouseEnter += new System.EventHandler(this.panelContent_MouseEnter);
            this.pictureBoxTypeImage.MouseLeave += new System.EventHandler(this.panelContent_MouseLeave);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(6, 6);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(110, 21);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Mestá a obce";
            this.labelTitle.Click += new System.EventHandler(this.tileContent_Click);
            this.labelTitle.MouseEnter += new System.EventHandler(this.panelContent_MouseEnter);
            this.labelTitle.MouseLeave += new System.EventHandler(this.panelContent_MouseLeave);
            // 
            // TileControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelContent);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "TileControl";
            this.Size = new System.Drawing.Size(218, 215);
            this.panelContent.ResumeLayout(false);
            this.panelContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTypeImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.PictureBox pictureBoxTypeImage;
        private System.Windows.Forms.Label labelTitle;
    }
}
