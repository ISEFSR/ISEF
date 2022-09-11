namespace cvti.isef.winformapp.Controls.Main.Dashboard
{
    partial class SingleWarning
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
            this.pictureBoxErrorIcon = new System.Windows.Forms.PictureBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelText = new System.Windows.Forms.Label();
            this.linkLabelTakeAction = new System.Windows.Forms.LinkLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxErrorIcon)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxErrorIcon
            // 
            this.pictureBoxErrorIcon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxErrorIcon.Image = global::cvti.isef.winformapp.Properties.Resources.warning_white_30;
            this.pictureBoxErrorIcon.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxErrorIcon.Name = "pictureBoxErrorIcon";
            this.pictureBoxErrorIcon.Size = new System.Drawing.Size(25, 25);
            this.pictureBoxErrorIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxErrorIcon.TabIndex = 0;
            this.pictureBoxErrorIcon.TabStop = false;
            this.pictureBoxErrorIcon.Click += new System.EventHandler(this.labelText_Click);
            this.pictureBoxErrorIcon.MouseEnter += new System.EventHandler(this.ctrl_MouseEnter);
            this.pictureBoxErrorIcon.MouseLeave += new System.EventHandler(this.ctrl_MouseLeave);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.labelTitle.Location = new System.Drawing.Point(36, 2);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(5, 2, 3, 2);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(166, 27);
            this.labelTitle.TabIndex = 1;
            this.labelTitle.Text = "Some title";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelTitle.Click += new System.EventHandler(this.labelText_Click);
            this.labelTitle.MouseEnter += new System.EventHandler(this.ctrl_MouseEnter);
            this.labelTitle.MouseLeave += new System.EventHandler(this.ctrl_MouseLeave);
            // 
            // labelText
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.labelText, 2);
            this.labelText.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.labelText.Location = new System.Drawing.Point(5, 36);
            this.labelText.Margin = new System.Windows.Forms.Padding(5);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(195, 66);
            this.labelText.TabIndex = 2;
            this.labelText.Text = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium dolore" +
    "mque laudantium.";
            this.labelText.Click += new System.EventHandler(this.labelText_Click);
            this.labelText.MouseEnter += new System.EventHandler(this.ctrl_MouseEnter);
            this.labelText.MouseLeave += new System.EventHandler(this.ctrl_MouseLeave);
            // 
            // linkLabelTakeAction
            // 
            this.linkLabelTakeAction.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.linkLabelTakeAction, 2);
            this.linkLabelTakeAction.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLabelTakeAction.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabelTakeAction.LinkColor = System.Drawing.Color.Yellow;
            this.linkLabelTakeAction.Location = new System.Drawing.Point(3, 107);
            this.linkLabelTakeAction.Name = "linkLabelTakeAction";
            this.linkLabelTakeAction.Size = new System.Drawing.Size(78, 13);
            this.linkLabelTakeAction.TabIndex = 3;
            this.linkLabelTakeAction.TabStop = true;
            this.linkLabelTakeAction.Text = "Zmeň textáciu";
            this.linkLabelTakeAction.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelTakeAction_LinkClicked);
            this.linkLabelTakeAction.MouseEnter += new System.EventHandler(this.ctrl_MouseEnter);
            this.linkLabelTakeAction.MouseLeave += new System.EventHandler(this.ctrl_MouseLeave);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(89)))), ((int)(((byte)(127)))));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBoxErrorIcon, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.linkLabelTakeAction, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelTitle, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelText, 0, 1);
            this.tableLayoutPanel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(205, 127);
            this.tableLayoutPanel1.TabIndex = 4;
            this.tableLayoutPanel1.Click += new System.EventHandler(this.labelText_Click);
            this.tableLayoutPanel1.MouseEnter += new System.EventHandler(this.ctrl_MouseEnter);
            this.tableLayoutPanel1.MouseLeave += new System.EventHandler(this.ctrl_MouseLeave);
            // 
            // SingleWarning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SingleWarning";
            this.Size = new System.Drawing.Size(225, 147);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxErrorIcon)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxErrorIcon;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.LinkLabel linkLabelTakeAction;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
