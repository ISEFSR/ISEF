namespace cvti.isef.winformapp.Controls.Main.Ciselniky
{
    partial class FunkcnaKlasifikaciaControl
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
            this.treeViewFunkcna = new System.Windows.Forms.TreeView();
            this.contextMenuStripCondition = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.andToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.orToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.panelControlButtons = new System.Windows.Forms.Panel();
            this.buttonShowPreview = new System.Windows.Forms.Button();
            this.buttonDiscardChanges = new System.Windows.Forms.Button();
            this.buttonSaveChanges = new System.Windows.Forms.Button();
            this.textBoxPopis = new System.Windows.Forms.TextBox();
            this.textBoxNazov = new System.Windows.Forms.TextBox();
            this.textBoxKod = new System.Windows.Forms.TextBox();
            this.textBoxRok = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStripCondition.SuspendLayout();
            this.panelControlButtons.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeViewFunkcna
            // 
            this.treeViewFunkcna.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeViewFunkcna.ContextMenuStrip = this.contextMenuStripCondition;
            this.treeViewFunkcna.HideSelection = false;
            this.treeViewFunkcna.HotTracking = true;
            this.treeViewFunkcna.Location = new System.Drawing.Point(3, 3);
            this.treeViewFunkcna.Name = "treeViewFunkcna";
            this.treeViewFunkcna.Size = new System.Drawing.Size(326, 515);
            this.treeViewFunkcna.TabIndex = 0;
            this.treeViewFunkcna.Tag = "35";
            this.treeViewFunkcna.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewFunkcna_AfterSelect);
            this.treeViewFunkcna.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewFunkcna_NodeMouseClick);
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
            // panelControlButtons
            // 
            this.panelControlButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelControlButtons.Controls.Add(this.buttonShowPreview);
            this.panelControlButtons.Controls.Add(this.buttonDiscardChanges);
            this.panelControlButtons.Controls.Add(this.buttonSaveChanges);
            this.panelControlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControlButtons.Location = new System.Drawing.Point(0, 529);
            this.panelControlButtons.Name = "panelControlButtons";
            this.panelControlButtons.Size = new System.Drawing.Size(838, 30);
            this.panelControlButtons.TabIndex = 1;
            this.panelControlButtons.Paint += new System.Windows.Forms.PaintEventHandler(this.panelControlButtons_Paint);
            // 
            // buttonShowPreview
            // 
            this.buttonShowPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonShowPreview.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonShowPreview.Location = new System.Drawing.Point(3, 4);
            this.buttonShowPreview.Name = "buttonShowPreview";
            this.buttonShowPreview.Size = new System.Drawing.Size(120, 23);
            this.buttonShowPreview.TabIndex = 8;
            this.buttonShowPreview.Text = "Zobraz prehľad";
            this.toolTipInfo.SetToolTip(this.buttonShowPreview, "Zobrazí prehľad pre vybranú položku");
            this.buttonShowPreview.UseVisualStyleBackColor = true;
            this.buttonShowPreview.Click += new System.EventHandler(this.buttonShowPreview_Click);
            // 
            // buttonDiscardChanges
            // 
            this.buttonDiscardChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDiscardChanges.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonDiscardChanges.Location = new System.Drawing.Point(129, 4);
            this.buttonDiscardChanges.Name = "buttonDiscardChanges";
            this.buttonDiscardChanges.Size = new System.Drawing.Size(120, 23);
            this.buttonDiscardChanges.TabIndex = 9;
            this.buttonDiscardChanges.Text = "Zruš zmeny";
            this.toolTipInfo.SetToolTip(this.buttonDiscardChanges, "Zruší zmeny pre vybranú položku");
            this.buttonDiscardChanges.UseVisualStyleBackColor = true;
            this.buttonDiscardChanges.Click += new System.EventHandler(this.buttonDiscardChanges_Click);
            // 
            // buttonSaveChanges
            // 
            this.buttonSaveChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSaveChanges.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonSaveChanges.Location = new System.Drawing.Point(255, 4);
            this.buttonSaveChanges.Name = "buttonSaveChanges";
            this.buttonSaveChanges.Size = new System.Drawing.Size(120, 23);
            this.buttonSaveChanges.TabIndex = 10;
            this.buttonSaveChanges.Text = "Ulož zmeny";
            this.toolTipInfo.SetToolTip(this.buttonSaveChanges, "Uloží vykonané zmeny pre vybranú položku");
            this.buttonSaveChanges.UseVisualStyleBackColor = true;
            this.buttonSaveChanges.Click += new System.EventHandler(this.buttonSaveChanges_Click);
            // 
            // textBoxPopis
            // 
            this.textBoxPopis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxPopis.Location = new System.Drawing.Point(3, 139);
            this.textBoxPopis.Multiline = true;
            this.textBoxPopis.Name = "textBoxPopis";
            this.textBoxPopis.Size = new System.Drawing.Size(368, 373);
            this.textBoxPopis.TabIndex = 7;
            // 
            // textBoxNazov
            // 
            this.textBoxNazov.ContextMenuStrip = this.contextMenuStripCondition;
            this.textBoxNazov.Location = new System.Drawing.Point(3, 98);
            this.textBoxNazov.Name = "textBoxNazov";
            this.textBoxNazov.Size = new System.Drawing.Size(239, 22);
            this.textBoxNazov.TabIndex = 5;
            //this.textBoxNazov.TextChanged += new System.EventHandler(this.textBoxNazov_TextChanged);
            // 
            // textBoxKod
            // 
            this.textBoxKod.ContextMenuStrip = this.contextMenuStripCondition;
            this.textBoxKod.Location = new System.Drawing.Point(3, 57);
            this.textBoxKod.Name = "textBoxKod";
            this.textBoxKod.ReadOnly = true;
            this.textBoxKod.Size = new System.Drawing.Size(239, 22);
            this.textBoxKod.TabIndex = 3;
            // 
            // textBoxRok
            // 
            this.textBoxRok.ContextMenuStrip = this.contextMenuStripCondition;
            this.textBoxRok.Location = new System.Drawing.Point(3, 16);
            this.textBoxRok.Name = "textBoxRok";
            this.textBoxRok.ReadOnly = true;
            this.textBoxRok.Size = new System.Drawing.Size(239, 22);
            this.textBoxRok.TabIndex = 1;
            this.textBoxRok.Tag = "0";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBoxRok, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBoxPopis, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.textBoxNazov, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.textBoxKod, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(335, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(374, 515);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Cursor = System.Windows.Forms.Cursors.Help;
            this.label4.Location = new System.Drawing.Point(3, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Dlhý názov";
            this.toolTipInfo.SetToolTip(this.label4, "Celý názov položky");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Help;
            this.label3.Location = new System.Drawing.Point(3, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Nazov *";
            this.toolTipInfo.SetToolTip(this.label3, "Skrátený názov položky");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.Help;
            this.label2.Location = new System.Drawing.Point(3, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Kod *";
            this.toolTipInfo.SetToolTip(this.label2, "Unikátny identifkátor položky");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Help;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Rok *";
            this.toolTipInfo.SetToolTip(this.label1, "Kalendárny rok pre ktorý je položka platná");
            // 
            // FunkcnaKlasifikaciaControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panelControlButtons);
            this.Controls.Add(this.treeViewFunkcna);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "FunkcnaKlasifikaciaControl";
            this.Size = new System.Drawing.Size(838, 559);
            this.contextMenuStripCondition.ResumeLayout(false);
            this.panelControlButtons.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewFunkcna;
        private System.Windows.Forms.Panel panelControlButtons;
        private System.Windows.Forms.TextBox textBoxPopis;
        private System.Windows.Forms.TextBox textBoxNazov;
        private System.Windows.Forms.TextBox textBoxKod;
        private System.Windows.Forms.TextBox textBoxRok;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonShowPreview;
        private System.Windows.Forms.Button buttonDiscardChanges;
        private System.Windows.Forms.Button buttonSaveChanges;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripCondition;
        private System.Windows.Forms.ToolStripMenuItem andToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem orToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
    }
}
