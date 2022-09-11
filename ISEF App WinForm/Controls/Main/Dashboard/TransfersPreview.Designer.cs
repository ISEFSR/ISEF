namespace cvti.isef.winformapp.Controls.Main.Dashboard
{
    partial class TransfersPreview
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
            this.panelHorizontalSeparator = new System.Windows.Forms.Panel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanelTransfers = new System.Windows.Forms.FlowLayoutPanel();
            this.labelSubtitle = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelContent
            // 
            this.panelContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelContent.Controls.Add(this.panelHorizontalSeparator);
            this.panelContent.Controls.Add(this.linkLabel1);
            this.panelContent.Controls.Add(this.pictureBox1);
            this.panelContent.Controls.Add(this.flowLayoutPanelTransfers);
            this.panelContent.Controls.Add(this.labelSubtitle);
            this.panelContent.Controls.Add(this.labelTitle);
            this.panelContent.Location = new System.Drawing.Point(10, 10);
            this.panelContent.Margin = new System.Windows.Forms.Padding(10);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(436, 281);
            this.panelContent.TabIndex = 0;
            this.panelContent.Paint += new System.Windows.Forms.PaintEventHandler(this.panelContent_Paint);
            // 
            // panelHorizontalSeparator
            // 
            this.panelHorizontalSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.panelHorizontalSeparator.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHorizontalSeparator.Location = new System.Drawing.Point(0, 59);
            this.panelHorizontalSeparator.Margin = new System.Windows.Forms.Padding(5, 10, 5, 5);
            this.panelHorizontalSeparator.Name = "panelHorizontalSeparator";
            this.panelHorizontalSeparator.Size = new System.Drawing.Size(434, 1);
            this.panelHorizontalSeparator.TabIndex = 1;
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Enabled = false;
            this.linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabel1.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(72)))), ((int)(((byte)(144)))));
            this.linkLabel1.Location = new System.Drawing.Point(107, 260);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(220, 13);
            this.linkLabel1.TabIndex = 4;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Pre nahranie transferov pre rok klik sem...";
            this.linkLabel1.Visible = false;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.pictureBox1.Enabled = false;
            this.pictureBox1.Image = global::cvti.isef.winformapp.Properties.Resources.nenahranepolozky;
            this.pictureBox1.Location = new System.Drawing.Point(109, 67);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(217, 159);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // flowLayoutPanelTransfers
            // 
            this.flowLayoutPanelTransfers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanelTransfers.AutoScroll = true;
            this.flowLayoutPanelTransfers.Location = new System.Drawing.Point(3, 70);
            this.flowLayoutPanelTransfers.Name = "flowLayoutPanelTransfers";
            this.flowLayoutPanelTransfers.Size = new System.Drawing.Size(428, 206);
            this.flowLayoutPanelTransfers.TabIndex = 3;
            // 
            // labelSubtitle
            // 
            this.labelSubtitle.AutoEllipsis = true;
            this.labelSubtitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(89)))), ((int)(((byte)(127)))));
            this.labelSubtitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelSubtitle.ForeColor = System.Drawing.Color.Gainsboro;
            this.labelSubtitle.Location = new System.Drawing.Point(0, 35);
            this.labelSubtitle.Margin = new System.Windows.Forms.Padding(5, 5, 3, 5);
            this.labelSubtitle.Name = "labelSubtitle";
            this.labelSubtitle.Padding = new System.Windows.Forms.Padding(10, 2, 0, 0);
            this.labelSubtitle.Size = new System.Drawing.Size(434, 24);
            this.labelSubtitle.TabIndex = 2;
            this.labelSubtitle.Tag = "Prehľad transferových ( prenesených kompetencií )  položiek za rok {rok}.";
            this.labelSubtitle.Text = "Prehľad transferových ( prenesených kompetencií )  položiek za rok {rok}.";
            // 
            // labelTitle
            // 
            this.labelTitle.AutoEllipsis = true;
            this.labelTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(89)))), ((int)(((byte)(127)))));
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.labelTitle.Location = new System.Drawing.Point(0, 0);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Padding = new System.Windows.Forms.Padding(10, 5, 0, 0);
            this.labelTitle.Size = new System.Drawing.Size(434, 35);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Tag = "Transferové položky za rok {rok}";
            this.labelTitle.Text = "Transferové položky za rok {rok}";
            // 
            // TransfersPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelContent);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "TransfersPreview";
            this.Size = new System.Drawing.Size(456, 301);
            this.panelContent.ResumeLayout(false);
            this.panelContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Panel panelHorizontalSeparator;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelSubtitle;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelTransfers;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}
