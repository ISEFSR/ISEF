namespace cvti.isef.winformapp.Controls.Main.Ciselniky
{
    partial class UcetControl
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
            this.panelControlButtons = new System.Windows.Forms.Panel();
            this.buttonShowPreview = new System.Windows.Forms.Button();
            this.buttonDiscard = new System.Windows.Forms.Button();
            this.buttonSaveChanges = new System.Windows.Forms.Button();
            this.listBoxUcty = new System.Windows.Forms.ListBox();
            this.contextMenuStripCondition = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.andToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.orToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanelWrapper = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxLong = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxKod = new System.Windows.Forms.TextBox();
            this.textBoxShort = new System.Windows.Forms.TextBox();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.panelControlButtons.SuspendLayout();
            this.contextMenuStripCondition.SuspendLayout();
            this.tableLayoutPanelWrapper.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControlButtons
            // 
            this.panelControlButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelControlButtons.Controls.Add(this.buttonShowPreview);
            this.panelControlButtons.Controls.Add(this.buttonDiscard);
            this.panelControlButtons.Controls.Add(this.buttonSaveChanges);
            this.panelControlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControlButtons.Location = new System.Drawing.Point(0, 485);
            this.panelControlButtons.Name = "panelControlButtons";
            this.panelControlButtons.Size = new System.Drawing.Size(775, 30);
            this.panelControlButtons.TabIndex = 10;
            this.panelControlButtons.Paint += new System.Windows.Forms.PaintEventHandler(this.panelControlButtons_Paint);
            // 
            // buttonShowPreview
            // 
            this.buttonShowPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonShowPreview.Location = new System.Drawing.Point(3, 3);
            this.buttonShowPreview.Name = "buttonShowPreview";
            this.buttonShowPreview.Size = new System.Drawing.Size(120, 23);
            this.buttonShowPreview.TabIndex = 6;
            this.buttonShowPreview.Text = "Zobraz prehľad";
            this.buttonShowPreview.UseVisualStyleBackColor = true;
            this.buttonShowPreview.Click += new System.EventHandler(this.buttonShowPreview_Click);
            // 
            // buttonDiscard
            // 
            this.buttonDiscard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDiscard.Location = new System.Drawing.Point(129, 3);
            this.buttonDiscard.Name = "buttonDiscard";
            this.buttonDiscard.Size = new System.Drawing.Size(120, 23);
            this.buttonDiscard.TabIndex = 1;
            this.buttonDiscard.Text = "Zruš zmeny";
            this.buttonDiscard.UseVisualStyleBackColor = true;
            this.buttonDiscard.Click += new System.EventHandler(this.buttonDiscard_Click);
            // 
            // buttonSaveChanges
            // 
            this.buttonSaveChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSaveChanges.Location = new System.Drawing.Point(255, 3);
            this.buttonSaveChanges.Name = "buttonSaveChanges";
            this.buttonSaveChanges.Size = new System.Drawing.Size(120, 23);
            this.buttonSaveChanges.TabIndex = 0;
            this.buttonSaveChanges.Text = "Ulož zmeny";
            this.buttonSaveChanges.UseVisualStyleBackColor = true;
            this.buttonSaveChanges.Click += new System.EventHandler(this.buttonSaveChanges_Click);
            // 
            // listBoxUcty
            // 
            this.listBoxUcty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxUcty.ContextMenuStrip = this.contextMenuStripCondition;
            this.listBoxUcty.FormattingEnabled = true;
            this.listBoxUcty.Location = new System.Drawing.Point(3, 3);
            this.listBoxUcty.Name = "listBoxUcty";
            this.listBoxUcty.Size = new System.Drawing.Size(320, 472);
            this.listBoxUcty.TabIndex = 11;
            this.listBoxUcty.SelectedIndexChanged += new System.EventHandler(this.listBoxUcty_SelectedIndexChanged);
            // 
            // contextMenuStripCondition
            // 
            this.contextMenuStripCondition.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.andToolStripMenuItem,
            this.orToolStripMenuItem,
            this.toolStripMenuItem2});
            this.contextMenuStripCondition.Name = "contextMenuStripCondition";
            this.contextMenuStripCondition.Size = new System.Drawing.Size(167, 70);
            // 
            // andToolStripMenuItem
            // 
            this.andToolStripMenuItem.Name = "andToolStripMenuItem";
            this.andToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.andToolStripMenuItem.Tag = "0";
            this.andToolStripMenuItem.Text = "= (Rovná sa)";
            this.andToolStripMenuItem.Click += new System.EventHandler(this.andToolStripMenuItem_Click);
            // 
            // orToolStripMenuItem
            // 
            this.orToolStripMenuItem.Name = "orToolStripMenuItem";
            this.orToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.orToolStripMenuItem.Tag = "1";
            this.orToolStripMenuItem.Text = "> (Je väčšie ako)";
            this.orToolStripMenuItem.Click += new System.EventHandler(this.andToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(166, 22);
            this.toolStripMenuItem2.Tag = "2";
            this.toolStripMenuItem2.Text = "< (Je menšie ako)";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.andToolStripMenuItem_Click);
            // 
            // tableLayoutPanelWrapper
            // 
            this.tableLayoutPanelWrapper.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanelWrapper.ColumnCount = 1;
            this.tableLayoutPanelWrapper.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelWrapper.Controls.Add(this.textBoxLong, 0, 5);
            this.tableLayoutPanelWrapper.Controls.Add(this.label3, 0, 4);
            this.tableLayoutPanelWrapper.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanelWrapper.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanelWrapper.Controls.Add(this.textBoxKod, 0, 1);
            this.tableLayoutPanelWrapper.Controls.Add(this.textBoxShort, 0, 3);
            this.tableLayoutPanelWrapper.Location = new System.Drawing.Point(329, 3);
            this.tableLayoutPanelWrapper.Name = "tableLayoutPanelWrapper";
            this.tableLayoutPanelWrapper.RowCount = 6;
            this.tableLayoutPanelWrapper.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelWrapper.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelWrapper.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelWrapper.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelWrapper.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelWrapper.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelWrapper.Size = new System.Drawing.Size(386, 472);
            this.tableLayoutPanelWrapper.TabIndex = 12;
            // 
            // textBoxLong
            // 
            this.textBoxLong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxLong.Location = new System.Drawing.Point(3, 98);
            this.textBoxLong.Multiline = true;
            this.textBoxLong.Name = "textBoxLong";
            this.textBoxLong.Size = new System.Drawing.Size(380, 371);
            this.textBoxLong.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Help;
            this.label3.Location = new System.Drawing.Point(3, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Dlhý text";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.Help;
            this.label2.Location = new System.Drawing.Point(3, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Skrátený text";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Help;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Kód *";
            // 
            // textBoxKod
            // 
            this.textBoxKod.ContextMenuStrip = this.contextMenuStripCondition;
            this.textBoxKod.Enabled = false;
            this.textBoxKod.Location = new System.Drawing.Point(3, 16);
            this.textBoxKod.Name = "textBoxKod";
            this.textBoxKod.ReadOnly = true;
            this.textBoxKod.Size = new System.Drawing.Size(314, 22);
            this.textBoxKod.TabIndex = 15;
            this.textBoxKod.Tag = "51";
            // 
            // textBoxShort
            // 
            this.textBoxShort.Location = new System.Drawing.Point(3, 57);
            this.textBoxShort.Name = "textBoxShort";
            this.textBoxShort.Size = new System.Drawing.Size(314, 22);
            this.textBoxShort.TabIndex = 16;
            // 
            // UcetControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanelWrapper);
            this.Controls.Add(this.listBoxUcty);
            this.Controls.Add(this.panelControlButtons);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UcetControl";
            this.Size = new System.Drawing.Size(775, 515);
            this.panelControlButtons.ResumeLayout(false);
            this.contextMenuStripCondition.ResumeLayout(false);
            this.tableLayoutPanelWrapper.ResumeLayout(false);
            this.tableLayoutPanelWrapper.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelControlButtons;
        private System.Windows.Forms.Button buttonShowPreview;
        private System.Windows.Forms.Button buttonDiscard;
        private System.Windows.Forms.Button buttonSaveChanges;
        private System.Windows.Forms.ListBox listBoxUcty;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelWrapper;
        private System.Windows.Forms.TextBox textBoxLong;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxKod;
        private System.Windows.Forms.TextBox textBoxShort;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripCondition;
        private System.Windows.Forms.ToolStripMenuItem andToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem orToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
    }
}
