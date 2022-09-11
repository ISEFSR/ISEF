namespace cvti.isef.winformapp.Controls.Main.Ciselniky
{
    partial class ZdrojovaKlasifikaciaControl
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
            this.buttonDiscardChanges = new System.Windows.Forms.Button();
            this.buttonSaveChanges = new System.Windows.Forms.Button();
            this.treeViewZdroje = new System.Windows.Forms.TreeView();
            this.contextMenuStripCondition = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.andToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.orToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxPK1 = new System.Windows.Forms.TextBox();
            this.labelPK1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxRok = new System.Windows.Forms.TextBox();
            this.textBoxPopis = new System.Windows.Forms.TextBox();
            this.textBoxNazov = new System.Windows.Forms.TextBox();
            this.textBoxKod = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelPK2 = new System.Windows.Forms.Label();
            this.textBoxPK2 = new System.Windows.Forms.TextBox();
            this.labelPK3 = new System.Windows.Forms.Label();
            this.textBoxPK3 = new System.Windows.Forms.TextBox();
            this.labelPK4 = new System.Windows.Forms.Label();
            this.textBoxPK4 = new System.Windows.Forms.TextBox();
            this.labelPK5 = new System.Windows.Forms.Label();
            this.textBoxPK5 = new System.Windows.Forms.TextBox();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxKodKde = new System.Windows.Forms.TextBox();
            this.panelControlButtons.SuspendLayout();
            this.contextMenuStripCondition.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControlButtons
            // 
            this.panelControlButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelControlButtons.Controls.Add(this.buttonShowPreview);
            this.panelControlButtons.Controls.Add(this.buttonDiscardChanges);
            this.panelControlButtons.Controls.Add(this.buttonSaveChanges);
            this.panelControlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControlButtons.Location = new System.Drawing.Point(0, 502);
            this.panelControlButtons.Name = "panelControlButtons";
            this.panelControlButtons.Size = new System.Drawing.Size(845, 30);
            this.panelControlButtons.TabIndex = 9;
            this.panelControlButtons.Paint += new System.Windows.Forms.PaintEventHandler(this.panelControlButtons_Paint);
            // 
            // buttonShowPreview
            // 
            this.buttonShowPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonShowPreview.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonShowPreview.Location = new System.Drawing.Point(3, 4);
            this.buttonShowPreview.Name = "buttonShowPreview";
            this.buttonShowPreview.Size = new System.Drawing.Size(120, 23);
            this.buttonShowPreview.TabIndex = 12;
            this.buttonShowPreview.Text = "Zobraz prehľad";
            this.toolTipInfo.SetToolTip(this.buttonShowPreview, "Zobrazí prehľad pre vybranú položku a rok");
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
            this.buttonDiscardChanges.TabIndex = 13;
            this.buttonDiscardChanges.Text = "Zruš zmeny";
            this.toolTipInfo.SetToolTip(this.buttonDiscardChanges, "Zruší neuložené zmeny");
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
            this.buttonSaveChanges.TabIndex = 14;
            this.buttonSaveChanges.Text = "Ulož zmeny";
            this.toolTipInfo.SetToolTip(this.buttonSaveChanges, "Uloźí vykonané zmeny");
            this.buttonSaveChanges.UseVisualStyleBackColor = true;
            this.buttonSaveChanges.Click += new System.EventHandler(this.buttonSaveChanges_Click);
            // 
            // treeViewZdroje
            // 
            this.treeViewZdroje.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeViewZdroje.ContextMenuStrip = this.contextMenuStripCondition;
            this.treeViewZdroje.HideSelection = false;
            this.treeViewZdroje.HotTracking = true;
            this.treeViewZdroje.Location = new System.Drawing.Point(3, 3);
            this.treeViewZdroje.Name = "treeViewZdroje";
            this.treeViewZdroje.Size = new System.Drawing.Size(326, 493);
            this.treeViewZdroje.TabIndex = 10;
            this.treeViewZdroje.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeViewZdroje.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewZdroje_NodeMouseClick);
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
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.textBoxPK1, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.labelPK1, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 18);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBoxRok, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBoxPopis, 0, 19);
            this.tableLayoutPanel1.Controls.Add(this.textBoxNazov, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.textBoxKod, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelPK2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBoxPK2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelPK3, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBoxPK3, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelPK4, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.textBoxPK4, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.labelPK5, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.textBoxPK5, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 16);
            this.tableLayoutPanel1.Controls.Add(this.textBoxKodKde, 0, 17);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(335, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 20;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(507, 493);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // textBoxPK1
            // 
            this.textBoxPK1.ContextMenuStrip = this.contextMenuStripCondition;
            this.textBoxPK1.Location = new System.Drawing.Point(3, 139);
            this.textBoxPK1.Name = "textBoxPK1";
            this.textBoxPK1.Size = new System.Drawing.Size(239, 22);
            this.textBoxPK1.TabIndex = 12;
            this.textBoxPK1.Tag = "56";
            // 
            // labelPK1
            // 
            this.labelPK1.AutoSize = true;
            this.labelPK1.Cursor = System.Windows.Forms.Cursors.Help;
            this.labelPK1.Location = new System.Drawing.Point(3, 123);
            this.labelPK1.Name = "labelPK1";
            this.labelPK1.Size = new System.Drawing.Size(93, 13);
            this.labelPK1.TabIndex = 12;
            this.labelPK1.Text = "Pomocný kód 1 *";
            this.toolTipInfo.SetToolTip(this.labelPK1, "Pomocný kód 1");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Cursor = System.Windows.Forms.Cursors.Help;
            this.label4.Location = new System.Drawing.Point(3, 205);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 9;
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
            this.label3.TabIndex = 8;
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
            this.label2.TabIndex = 7;
            this.label2.Text = "Kod *";
            this.toolTipInfo.SetToolTip(this.label2, "Kód vybranej položky");
            // 
            // textBoxRok
            // 
            this.textBoxRok.ContextMenuStrip = this.contextMenuStripCondition;
            this.textBoxRok.Location = new System.Drawing.Point(3, 16);
            this.textBoxRok.Name = "textBoxRok";
            this.textBoxRok.ReadOnly = true;
            this.textBoxRok.Size = new System.Drawing.Size(239, 22);
            this.textBoxRok.TabIndex = 5;
            this.textBoxRok.Tag = "0";
            // 
            // textBoxPopis
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.textBoxPopis, 2);
            this.textBoxPopis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxPopis.Location = new System.Drawing.Point(3, 221);
            this.textBoxPopis.Multiline = true;
            this.textBoxPopis.Name = "textBoxPopis";
            this.textBoxPopis.Size = new System.Drawing.Size(501, 269);
            this.textBoxPopis.TabIndex = 2;
            // 
            // textBoxNazov
            // 
            this.textBoxNazov.ContextMenuStrip = this.contextMenuStripCondition;
            this.textBoxNazov.Location = new System.Drawing.Point(3, 98);
            this.textBoxNazov.Name = "textBoxNazov";
            this.textBoxNazov.Size = new System.Drawing.Size(239, 22);
            this.textBoxNazov.TabIndex = 3;
            // 
            // textBoxKod
            // 
            this.textBoxKod.ContextMenuStrip = this.contextMenuStripCondition;
            this.textBoxKod.Location = new System.Drawing.Point(3, 57);
            this.textBoxKod.Name = "textBoxKod";
            this.textBoxKod.ReadOnly = true;
            this.textBoxKod.Size = new System.Drawing.Size(239, 22);
            this.textBoxKod.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Help;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Rok *";
            this.toolTipInfo.SetToolTip(this.label1, "Kalendárny rok pre ktorý je položka platná");
            // 
            // labelPK2
            // 
            this.labelPK2.AutoSize = true;
            this.labelPK2.Cursor = System.Windows.Forms.Cursors.Help;
            this.labelPK2.Location = new System.Drawing.Point(256, 0);
            this.labelPK2.Name = "labelPK2";
            this.labelPK2.Size = new System.Drawing.Size(93, 13);
            this.labelPK2.TabIndex = 12;
            this.labelPK2.Text = "Pomocný kód 2 *";
            this.toolTipInfo.SetToolTip(this.labelPK2, "Pomocný kód 2");
            // 
            // textBoxPK2
            // 
            this.textBoxPK2.ContextMenuStrip = this.contextMenuStripCondition;
            this.textBoxPK2.Location = new System.Drawing.Point(256, 16);
            this.textBoxPK2.Name = "textBoxPK2";
            this.textBoxPK2.Size = new System.Drawing.Size(239, 22);
            this.textBoxPK2.TabIndex = 12;
            this.textBoxPK2.Tag = "57";
            // 
            // labelPK3
            // 
            this.labelPK3.AutoSize = true;
            this.labelPK3.Cursor = System.Windows.Forms.Cursors.Help;
            this.labelPK3.Location = new System.Drawing.Point(256, 41);
            this.labelPK3.Name = "labelPK3";
            this.labelPK3.Size = new System.Drawing.Size(93, 13);
            this.labelPK3.TabIndex = 12;
            this.labelPK3.Text = "Pomocný kód 3 *";
            this.toolTipInfo.SetToolTip(this.labelPK3, "Pomocný kód 3");
            // 
            // textBoxPK3
            // 
            this.textBoxPK3.ContextMenuStrip = this.contextMenuStripCondition;
            this.textBoxPK3.Location = new System.Drawing.Point(256, 57);
            this.textBoxPK3.Name = "textBoxPK3";
            this.textBoxPK3.Size = new System.Drawing.Size(239, 22);
            this.textBoxPK3.TabIndex = 12;
            this.textBoxPK3.Tag = "58";
            // 
            // labelPK4
            // 
            this.labelPK4.AutoSize = true;
            this.labelPK4.Cursor = System.Windows.Forms.Cursors.Help;
            this.labelPK4.Location = new System.Drawing.Point(256, 82);
            this.labelPK4.Name = "labelPK4";
            this.labelPK4.Size = new System.Drawing.Size(93, 13);
            this.labelPK4.TabIndex = 13;
            this.labelPK4.Text = "Pomocný kód 4 *";
            this.toolTipInfo.SetToolTip(this.labelPK4, "Pomocný kód 4");
            // 
            // textBoxPK4
            // 
            this.textBoxPK4.ContextMenuStrip = this.contextMenuStripCondition;
            this.textBoxPK4.Location = new System.Drawing.Point(256, 98);
            this.textBoxPK4.Name = "textBoxPK4";
            this.textBoxPK4.Size = new System.Drawing.Size(239, 22);
            this.textBoxPK4.TabIndex = 13;
            this.textBoxPK4.Tag = "59";
            // 
            // labelPK5
            // 
            this.labelPK5.AutoSize = true;
            this.labelPK5.Cursor = System.Windows.Forms.Cursors.Help;
            this.labelPK5.Location = new System.Drawing.Point(256, 123);
            this.labelPK5.Name = "labelPK5";
            this.labelPK5.Size = new System.Drawing.Size(93, 13);
            this.labelPK5.TabIndex = 14;
            this.labelPK5.Text = "Pomocný kód 5 *";
            this.toolTipInfo.SetToolTip(this.labelPK5, "Pomocný kód 5");
            // 
            // textBoxPK5
            // 
            this.textBoxPK5.ContextMenuStrip = this.contextMenuStripCondition;
            this.textBoxPK5.Location = new System.Drawing.Point(256, 139);
            this.textBoxPK5.Name = "textBoxPK5";
            this.textBoxPK5.Size = new System.Drawing.Size(239, 22);
            this.textBoxPK5.TabIndex = 15;
            this.textBoxPK5.Tag = "60";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 164);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Typ zdoja:";
            // 
            // textBoxKodKde
            // 
            this.textBoxKodKde.Location = new System.Drawing.Point(3, 180);
            this.textBoxKodKde.Name = "textBoxKodKde";
            this.textBoxKodKde.Size = new System.Drawing.Size(239, 22);
            this.textBoxKodKde.TabIndex = 17;
            this.textBoxKodKde.TextChanged += new System.EventHandler(this.textBoxKodKde_TextChanged);
            // 
            // ZdrojovaKlasifikaciaControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.treeViewZdroje);
            this.Controls.Add(this.panelControlButtons);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "ZdrojovaKlasifikaciaControl";
            this.Size = new System.Drawing.Size(845, 532);
            this.panelControlButtons.ResumeLayout(false);
            this.contextMenuStripCondition.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelControlButtons;
        private System.Windows.Forms.TreeView treeViewZdroje;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxRok;
        private System.Windows.Forms.TextBox textBoxPopis;
        private System.Windows.Forms.TextBox textBoxNazov;
        private System.Windows.Forms.TextBox textBoxKod;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelPK3;
        private System.Windows.Forms.Label labelPK2;
        private System.Windows.Forms.Label labelPK1;
        private System.Windows.Forms.Button buttonShowPreview;
        private System.Windows.Forms.Button buttonDiscardChanges;
        private System.Windows.Forms.Button buttonSaveChanges;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private System.Windows.Forms.TextBox textBoxPK5;
        private System.Windows.Forms.TextBox textBoxPK4;
        private System.Windows.Forms.Label labelPK5;
        private System.Windows.Forms.Label labelPK4;
        private System.Windows.Forms.TextBox textBoxPK3;
        private System.Windows.Forms.TextBox textBoxPK2;
        private System.Windows.Forms.TextBox textBoxPK1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripCondition;
        private System.Windows.Forms.ToolStripMenuItem andToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem orToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxKodKde;
    }
}
