namespace cvti.isef.winformapp.Controls.Main.Dashboard
{
    partial class ActionControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActionControl));
            this.panelSeparator = new System.Windows.Forms.Panel();
            this.panelTitle = new System.Windows.Forms.Panel();
            this.lblActionType = new System.Windows.Forms.Label();
            this.lblWhen = new System.Windows.Forms.Label();
            this.pictureBoxIcon1 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelBorder = new System.Windows.Forms.Panel();
            this.pictureBoxIcon2 = new System.Windows.Forms.PictureBox();
            this.LinkLabelTitle = new System.Windows.Forms.LinkLabel();
            this.LabelInfo = new System.Windows.Forms.Label();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.panelTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelBorder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon2)).BeginInit();
            this.SuspendLayout();
            // 
            // panelSeparator
            // 
            this.panelSeparator.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelSeparator.Location = new System.Drawing.Point(0, 132);
            this.panelSeparator.Name = "panelSeparator";
            this.panelSeparator.Size = new System.Drawing.Size(226, 5);
            this.panelSeparator.TabIndex = 0;
            this.panelSeparator.Paint += new System.Windows.Forms.PaintEventHandler(this.panelSeparator_Paint);
            // 
            // panelTitle
            // 
            this.panelTitle.Controls.Add(this.lblActionType);
            this.panelTitle.Controls.Add(this.lblWhen);
            this.panelTitle.Controls.Add(this.pictureBoxIcon1);
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitle.Location = new System.Drawing.Point(0, 0);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Size = new System.Drawing.Size(226, 20);
            this.panelTitle.TabIndex = 1;
            // 
            // lblActionType
            // 
            this.lblActionType.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblActionType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblActionType.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblActionType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.lblActionType.Location = new System.Drawing.Point(20, 0);
            this.lblActionType.Name = "lblActionType";
            this.lblActionType.Size = new System.Drawing.Size(102, 20);
            this.lblActionType.TabIndex = 4;
            this.lblActionType.Text = "Tiket #9999";
            this.lblActionType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblActionType.Click += new System.EventHandler(this.component_Clicked);
            // 
            // lblWhen
            // 
            this.lblWhen.Cursor = System.Windows.Forms.Cursors.Help;
            this.lblWhen.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblWhen.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblWhen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.lblWhen.Location = new System.Drawing.Point(122, 0);
            this.lblWhen.Name = "lblWhen";
            this.lblWhen.Size = new System.Drawing.Size(104, 20);
            this.lblWhen.TabIndex = 3;
            this.lblWhen.Text = "55h.";
            this.lblWhen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblWhen.Click += new System.EventHandler(this.component_Clicked);
            // 
            // pictureBoxIcon1
            // 
            this.pictureBoxIcon1.Cursor = System.Windows.Forms.Cursors.Help;
            this.pictureBoxIcon1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBoxIcon1.Image = global::cvti.isef.winformapp.Properties.Resources.info20;
            this.pictureBoxIcon1.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxIcon1.Name = "pictureBoxIcon1";
            this.pictureBoxIcon1.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxIcon1.TabIndex = 0;
            this.pictureBoxIcon1.TabStop = false;
            this.pictureBoxIcon1.Click += new System.EventHandler(this.component_Clicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.pictureBox1.Image = global::cvti.isef.winformapp.Properties.Resources.action_blured;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(226, 137);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // panelBorder
            // 
            this.panelBorder.Controls.Add(this.pictureBoxIcon2);
            this.panelBorder.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelBorder.Location = new System.Drawing.Point(0, 20);
            this.panelBorder.Name = "panelBorder";
            this.panelBorder.Size = new System.Drawing.Size(20, 112);
            this.panelBorder.TabIndex = 2;
            // 
            // pictureBoxIcon2
            // 
            this.pictureBoxIcon2.Cursor = System.Windows.Forms.Cursors.Help;
            this.pictureBoxIcon2.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBoxIcon2.Image = global::cvti.isef.winformapp.Properties.Resources.display20;
            this.pictureBoxIcon2.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxIcon2.Name = "pictureBoxIcon2";
            this.pictureBoxIcon2.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxIcon2.TabIndex = 1;
            this.pictureBoxIcon2.TabStop = false;
            this.pictureBoxIcon2.Click += new System.EventHandler(this.component_Clicked);
            // 
            // LinkLabelTitle
            // 
            this.LinkLabelTitle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LinkLabelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.LinkLabelTitle.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.LinkLabelTitle.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(72)))), ((int)(((byte)(144)))));
            this.LinkLabelTitle.Location = new System.Drawing.Point(20, 20);
            this.LinkLabelTitle.Name = "LinkLabelTitle";
            this.LinkLabelTitle.Size = new System.Drawing.Size(206, 20);
            this.LinkLabelTitle.TabIndex = 10;
            this.LinkLabelTitle.TabStop = true;
            this.LinkLabelTitle.Text = "Some random title";
            this.LinkLabelTitle.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelTitle_LinkClicked);
            this.LinkLabelTitle.Click += new System.EventHandler(this.component_Clicked);
            // 
            // LabelInfo
            // 
            this.LabelInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LabelInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelInfo.Location = new System.Drawing.Point(20, 40);
            this.LabelInfo.Margin = new System.Windows.Forms.Padding(0);
            this.LabelInfo.Name = "LabelInfo";
            this.LabelInfo.Size = new System.Drawing.Size(206, 92);
            this.LabelInfo.TabIndex = 11;
            this.LabelInfo.Text = resources.GetString("LabelInfo.Text");
            this.LabelInfo.Click += new System.EventHandler(this.component_Clicked);
            // 
            // ActionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.LabelInfo);
            this.Controls.Add(this.LinkLabelTitle);
            this.Controls.Add(this.panelBorder);
            this.Controls.Add(this.panelTitle);
            this.Controls.Add(this.panelSeparator);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "ActionControl";
            this.Size = new System.Drawing.Size(226, 137);
            this.panelTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelBorder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSeparator;
        private System.Windows.Forms.Panel panelTitle;
        internal System.Windows.Forms.Label lblActionType;
        internal System.Windows.Forms.Label lblWhen;
        private System.Windows.Forms.PictureBox pictureBoxIcon1;
        private System.Windows.Forms.Panel panelBorder;
        internal System.Windows.Forms.LinkLabel LinkLabelTitle;
        internal System.Windows.Forms.Label LabelInfo;
        private System.Windows.Forms.PictureBox pictureBoxIcon2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolTip toolTipInfo;
    }
}
