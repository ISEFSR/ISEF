namespace cvti.isef.winformapp.Controls.Main.Data
{
    partial class ColumnTileControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColumnTileControl));
            this.panelContent = new System.Windows.Forms.Panel();
            this.labelFunctions = new System.Windows.Forms.Label();
            this.textBoxAlias = new System.Windows.Forms.TextBox();
            this.labelTableName = new System.Windows.Forms.Label();
            this.panelSeparator = new System.Windows.Forms.Panel();
            this.panelControlButtons = new System.Windows.Forms.Panel();
            this.buttonVisible = new System.Windows.Forms.Button();
            this.imageListIcons = new System.Windows.Forms.ImageList(this.components);
            this.buttonFlip = new System.Windows.Forms.Button();
            this.buttonMoveRight = new System.Windows.Forms.Button();
            this.buttonMoveLeft = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.panelTop = new System.Windows.Forms.Panel();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.panelContent.SuspendLayout();
            this.panelControlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContent
            // 
            this.panelContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContent.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panelContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelContent.Controls.Add(this.labelFunctions);
            this.panelContent.Controls.Add(this.textBoxAlias);
            this.panelContent.Controls.Add(this.labelTableName);
            this.panelContent.Controls.Add(this.panelSeparator);
            this.panelContent.Controls.Add(this.panelControlButtons);
            this.panelContent.Controls.Add(this.panelTop);
            this.panelContent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelContent.Location = new System.Drawing.Point(10, 10);
            this.panelContent.Margin = new System.Windows.Forms.Padding(10);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(208, 184);
            this.panelContent.TabIndex = 0;
            this.panelContent.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelContent_MouseDown);
            this.panelContent.MouseEnter += new System.EventHandler(this.panelContent_MouseEnter);
            this.panelContent.MouseLeave += new System.EventHandler(this.panelContent_MouseLeave);
            this.panelContent.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelContent_MouseUp);
            // 
            // labelFunctions
            // 
            this.labelFunctions.AutoSize = true;
            this.labelFunctions.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelFunctions.ForeColor = System.Drawing.Color.Cornsilk;
            this.labelFunctions.Location = new System.Drawing.Point(0, 99);
            this.labelFunctions.Name = "labelFunctions";
            this.labelFunctions.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.labelFunctions.Size = new System.Drawing.Size(40, 23);
            this.labelFunctions.TabIndex = 14;
            this.labelFunctions.Text = "+ Sum";
            this.toolTipInfo.SetToolTip(this.labelFunctions, "Použité funkcie");
            // 
            // textBoxAlias
            // 
            this.textBoxAlias.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.textBoxAlias.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxAlias.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxAlias.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAlias.ForeColor = System.Drawing.Color.Cornsilk;
            this.textBoxAlias.Location = new System.Drawing.Point(0, 71);
            this.textBoxAlias.Name = "textBoxAlias";
            this.textBoxAlias.ReadOnly = true;
            this.textBoxAlias.Size = new System.Drawing.Size(206, 28);
            this.textBoxAlias.TabIndex = 8;
            this.textBoxAlias.Text = "ColumnAlias";
            this.toolTipInfo.SetToolTip(this.textBoxAlias, "Alias stĺpca");
            this.textBoxAlias.TextChanged += new System.EventHandler(this.textBoxAlias_TextChanged);
            this.textBoxAlias.Leave += new System.EventHandler(this.textBoxAlias_Leave);
            this.textBoxAlias.MouseDown += new System.Windows.Forms.MouseEventHandler(this.textBoxAlias_MouseDown);
            this.textBoxAlias.MouseLeave += new System.EventHandler(this.textBoxAlias_MouseLeave);
            // 
            // labelTableName
            // 
            this.labelTableName.AutoSize = true;
            this.labelTableName.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelTableName.ForeColor = System.Drawing.Color.Cornsilk;
            this.labelTableName.Location = new System.Drawing.Point(0, 38);
            this.labelTableName.Name = "labelTableName";
            this.labelTableName.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.labelTableName.Size = new System.Drawing.Size(104, 33);
            this.labelTableName.TabIndex = 6;
            this.labelTableName.Text = "ISEF.dbo.AssuView";
            this.toolTipInfo.SetToolTip(this.labelTableName, "Meno tabuľky / pohľadu z ktorej stĺpec pochádza");
            // 
            // panelSeparator
            // 
            this.panelSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.panelSeparator.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSeparator.Location = new System.Drawing.Point(0, 37);
            this.panelSeparator.Name = "panelSeparator";
            this.panelSeparator.Size = new System.Drawing.Size(206, 1);
            this.panelSeparator.TabIndex = 13;
            // 
            // panelControlButtons
            // 
            this.panelControlButtons.Controls.Add(this.buttonVisible);
            this.panelControlButtons.Controls.Add(this.buttonFlip);
            this.panelControlButtons.Controls.Add(this.buttonMoveRight);
            this.panelControlButtons.Controls.Add(this.buttonMoveLeft);
            this.panelControlButtons.Controls.Add(this.buttonRemove);
            this.panelControlButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControlButtons.Location = new System.Drawing.Point(0, 10);
            this.panelControlButtons.Name = "panelControlButtons";
            this.panelControlButtons.Size = new System.Drawing.Size(206, 27);
            this.panelControlButtons.TabIndex = 12;
            // 
            // buttonVisible
            // 
            this.buttonVisible.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonVisible.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonVisible.FlatAppearance.BorderSize = 0;
            this.buttonVisible.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonVisible.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.buttonVisible.ImageIndex = 5;
            this.buttonVisible.ImageList = this.imageListIcons;
            this.buttonVisible.Location = new System.Drawing.Point(125, 0);
            this.buttonVisible.Name = "buttonVisible";
            this.buttonVisible.Size = new System.Drawing.Size(27, 27);
            this.buttonVisible.TabIndex = 12;
            this.toolTipInfo.SetToolTip(this.buttonVisible, "Edituj vybraný stĺpec");
            this.buttonVisible.UseVisualStyleBackColor = true;
            this.buttonVisible.Click += new System.EventHandler(this.buttonVisible_Click);
            // 
            // imageListIcons
            // 
            this.imageListIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListIcons.ImageStream")));
            this.imageListIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListIcons.Images.SetKeyName(0, "delete52.png");
            this.imageListIcons.Images.SetKeyName(1, "edit48.png");
            this.imageListIcons.Images.SetKeyName(2, "left52.png");
            this.imageListIcons.Images.SetKeyName(3, "right52.png");
            this.imageListIcons.Images.SetKeyName(4, "eye.png");
            this.imageListIcons.Images.SetKeyName(5, "eye20.png");
            // 
            // buttonFlip
            // 
            this.buttonFlip.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonFlip.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonFlip.FlatAppearance.BorderSize = 0;
            this.buttonFlip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFlip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.buttonFlip.ImageIndex = 1;
            this.buttonFlip.ImageList = this.imageListIcons;
            this.buttonFlip.Location = new System.Drawing.Point(152, 0);
            this.buttonFlip.Name = "buttonFlip";
            this.buttonFlip.Size = new System.Drawing.Size(27, 27);
            this.buttonFlip.TabIndex = 5;
            this.toolTipInfo.SetToolTip(this.buttonFlip, "Edituj vybraný stĺpec");
            this.buttonFlip.UseVisualStyleBackColor = true;
            this.buttonFlip.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonMoveRight
            // 
            this.buttonMoveRight.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonMoveRight.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonMoveRight.FlatAppearance.BorderSize = 0;
            this.buttonMoveRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMoveRight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.buttonMoveRight.ImageIndex = 3;
            this.buttonMoveRight.ImageList = this.imageListIcons;
            this.buttonMoveRight.Location = new System.Drawing.Point(27, 0);
            this.buttonMoveRight.Name = "buttonMoveRight";
            this.buttonMoveRight.Size = new System.Drawing.Size(27, 27);
            this.buttonMoveRight.TabIndex = 11;
            this.toolTipInfo.SetToolTip(this.buttonMoveRight, "Posuň o jeden stĺpec doprava");
            this.buttonMoveRight.UseVisualStyleBackColor = true;
            this.buttonMoveRight.Click += new System.EventHandler(this.buttonMoveRight_Click);
            // 
            // buttonMoveLeft
            // 
            this.buttonMoveLeft.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonMoveLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonMoveLeft.FlatAppearance.BorderSize = 0;
            this.buttonMoveLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMoveLeft.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.buttonMoveLeft.ImageIndex = 2;
            this.buttonMoveLeft.ImageList = this.imageListIcons;
            this.buttonMoveLeft.Location = new System.Drawing.Point(0, 0);
            this.buttonMoveLeft.Name = "buttonMoveLeft";
            this.buttonMoveLeft.Size = new System.Drawing.Size(27, 27);
            this.buttonMoveLeft.TabIndex = 10;
            this.toolTipInfo.SetToolTip(this.buttonMoveLeft, "Posuň o jeden stĺpec doľava");
            this.buttonMoveLeft.UseVisualStyleBackColor = true;
            this.buttonMoveLeft.Click += new System.EventHandler(this.buttonMoveLeft_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonRemove.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonRemove.FlatAppearance.BorderSize = 0;
            this.buttonRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRemove.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.buttonRemove.ImageIndex = 0;
            this.buttonRemove.ImageList = this.imageListIcons;
            this.buttonRemove.Location = new System.Drawing.Point(179, 0);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(27, 27);
            this.buttonRemove.TabIndex = 3;
            this.toolTipInfo.SetToolTip(this.buttonRemove, "Odstráň vybraný stĺpec");
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.Coral;
            this.panelTop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(206, 10);
            this.panelTop.TabIndex = 9;
            // 
            // ColumnTileControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelContent);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ColumnTileControl";
            this.Size = new System.Drawing.Size(228, 204);
            this.panelContent.ResumeLayout(false);
            this.panelContent.PerformLayout();
            this.panelControlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Label labelTableName;
        private System.Windows.Forms.Button buttonFlip;
        private System.Windows.Forms.TextBox textBoxAlias;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private System.Windows.Forms.Button buttonMoveRight;
        private System.Windows.Forms.Button buttonMoveLeft;
        private System.Windows.Forms.ImageList imageListIcons;
        private System.Windows.Forms.Panel panelSeparator;
        private System.Windows.Forms.Panel panelControlButtons;
        private System.Windows.Forms.Label labelFunctions;
        private System.Windows.Forms.Button buttonVisible;
    }
}
