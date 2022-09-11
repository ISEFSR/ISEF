namespace cvti.isef.winformapp.Controls.Main.Ciselniky
{
    partial class TransferControl
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
            this.buttonRemoveSelected = new System.Windows.Forms.Button();
            this.buttonAddNew = new System.Windows.Forms.Button();
            this.buttonShowPreview = new System.Windows.Forms.Button();
            this.buttonDiscard = new System.Windows.Forms.Button();
            this.buttonSaveChanges = new System.Windows.Forms.Button();
            this.listBoxTransfers = new System.Windows.Forms.ListBox();
            this.contextMenuStripCondition = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.andToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.orToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanelWrapper = new System.Windows.Forms.TableLayoutPanel();
            this.comboBoxTo = new System.Windows.Forms.ComboBox();
            this.labelRok = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxRok = new System.Windows.Forms.TextBox();
            this.comboBoxFrom = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxPolozka = new System.Windows.Forms.ComboBox();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.buttonGenerateDefault = new System.Windows.Forms.Button();
            this.panelControlButtons.SuspendLayout();
            this.contextMenuStripCondition.SuspendLayout();
            this.tableLayoutPanelWrapper.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControlButtons
            // 
            this.panelControlButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelControlButtons.Controls.Add(this.buttonGenerateDefault);
            this.panelControlButtons.Controls.Add(this.buttonRemoveSelected);
            this.panelControlButtons.Controls.Add(this.buttonAddNew);
            this.panelControlButtons.Controls.Add(this.buttonShowPreview);
            this.panelControlButtons.Controls.Add(this.buttonDiscard);
            this.panelControlButtons.Controls.Add(this.buttonSaveChanges);
            this.panelControlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControlButtons.Location = new System.Drawing.Point(0, 447);
            this.panelControlButtons.Name = "panelControlButtons";
            this.panelControlButtons.Size = new System.Drawing.Size(784, 30);
            this.panelControlButtons.TabIndex = 10;
            this.panelControlButtons.Paint += new System.Windows.Forms.PaintEventHandler(this.panelControlButtons_Paint);
            // 
            // buttonRemoveSelected
            // 
            this.buttonRemoveSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRemoveSelected.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonRemoveSelected.Location = new System.Drawing.Point(3, 4);
            this.buttonRemoveSelected.Name = "buttonRemoveSelected";
            this.buttonRemoveSelected.Size = new System.Drawing.Size(120, 23);
            this.buttonRemoveSelected.TabIndex = 11;
            this.buttonRemoveSelected.Text = "Odstráň vybraný";
            this.toolTipInfo.SetToolTip(this.buttonRemoveSelected, "Odstráni vybaný transfer");
            this.buttonRemoveSelected.UseVisualStyleBackColor = true;
            this.buttonRemoveSelected.Click += new System.EventHandler(this.buttonRemoveSelected_Click);
            // 
            // buttonAddNew
            // 
            this.buttonAddNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddNew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonAddNew.Location = new System.Drawing.Point(129, 4);
            this.buttonAddNew.Name = "buttonAddNew";
            this.buttonAddNew.Size = new System.Drawing.Size(120, 23);
            this.buttonAddNew.TabIndex = 10;
            this.buttonAddNew.Text = "Nový transfer";
            this.toolTipInfo.SetToolTip(this.buttonAddNew, "Pridá nový transfer");
            this.buttonAddNew.UseVisualStyleBackColor = true;
            this.buttonAddNew.Click += new System.EventHandler(this.buttonAddNew_Click);
            // 
            // buttonShowPreview
            // 
            this.buttonShowPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonShowPreview.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonShowPreview.Location = new System.Drawing.Point(255, 4);
            this.buttonShowPreview.Name = "buttonShowPreview";
            this.buttonShowPreview.Size = new System.Drawing.Size(120, 23);
            this.buttonShowPreview.TabIndex = 9;
            this.buttonShowPreview.Text = "Zobraz prehľad";
            this.toolTipInfo.SetToolTip(this.buttonShowPreview, "Zobrazí prehľad pre vybranú položku");
            this.buttonShowPreview.UseVisualStyleBackColor = true;
            this.buttonShowPreview.Click += new System.EventHandler(this.buttonShowPreview_Click);
            // 
            // buttonDiscard
            // 
            this.buttonDiscard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDiscard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonDiscard.Location = new System.Drawing.Point(381, 4);
            this.buttonDiscard.Name = "buttonDiscard";
            this.buttonDiscard.Size = new System.Drawing.Size(120, 23);
            this.buttonDiscard.TabIndex = 8;
            this.buttonDiscard.Text = "Zruš zmeny";
            this.toolTipInfo.SetToolTip(this.buttonDiscard, "Zruší neuložené zmeny");
            this.buttonDiscard.UseVisualStyleBackColor = true;
            this.buttonDiscard.Click += new System.EventHandler(this.buttonDiscard_Click);
            // 
            // buttonSaveChanges
            // 
            this.buttonSaveChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSaveChanges.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonSaveChanges.Location = new System.Drawing.Point(507, 4);
            this.buttonSaveChanges.Name = "buttonSaveChanges";
            this.buttonSaveChanges.Size = new System.Drawing.Size(120, 23);
            this.buttonSaveChanges.TabIndex = 7;
            this.buttonSaveChanges.Text = "Ulož zmeny";
            this.toolTipInfo.SetToolTip(this.buttonSaveChanges, "Uloži vykonané zmeny");
            this.buttonSaveChanges.UseVisualStyleBackColor = true;
            this.buttonSaveChanges.Click += new System.EventHandler(this.buttonSaveChanges_Click);
            // 
            // listBoxTransfers
            // 
            this.listBoxTransfers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxTransfers.ContextMenuStrip = this.contextMenuStripCondition;
            this.listBoxTransfers.FormattingEnabled = true;
            this.listBoxTransfers.Location = new System.Drawing.Point(3, 3);
            this.listBoxTransfers.Name = "listBoxTransfers";
            this.listBoxTransfers.Size = new System.Drawing.Size(277, 433);
            this.listBoxTransfers.TabIndex = 11;
            this.listBoxTransfers.SelectedIndexChanged += new System.EventHandler(this.listBoxTransfers_SelectedIndexChanged);
            // 
            // contextMenuStripCondition
            // 
            this.contextMenuStripCondition.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.andToolStripMenuItem,
            this.orToolStripMenuItem,
            this.toolStripMenuItem2});
            this.contextMenuStripCondition.Name = "contextMenuStripCondition";
            this.contextMenuStripCondition.Size = new System.Drawing.Size(86, 70);
            // 
            // andToolStripMenuItem
            // 
            this.andToolStripMenuItem.Name = "andToolStripMenuItem";
            this.andToolStripMenuItem.Size = new System.Drawing.Size(85, 22);
            this.andToolStripMenuItem.Tag = "0";
            this.andToolStripMenuItem.Text = "=";
            this.andToolStripMenuItem.Click += new System.EventHandler(this.andToolStripMenuItem_Click);
            // 
            // orToolStripMenuItem
            // 
            this.orToolStripMenuItem.Name = "orToolStripMenuItem";
            this.orToolStripMenuItem.Size = new System.Drawing.Size(85, 22);
            this.orToolStripMenuItem.Tag = "1";
            this.orToolStripMenuItem.Text = "> ";
            this.orToolStripMenuItem.Click += new System.EventHandler(this.andToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(85, 22);
            this.toolStripMenuItem2.Tag = "2";
            this.toolStripMenuItem2.Text = "<";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.andToolStripMenuItem_Click);
            // 
            // tableLayoutPanelWrapper
            // 
            this.tableLayoutPanelWrapper.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanelWrapper.ColumnCount = 1;
            this.tableLayoutPanelWrapper.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelWrapper.Controls.Add(this.comboBoxTo, 0, 7);
            this.tableLayoutPanelWrapper.Controls.Add(this.labelRok, 0, 0);
            this.tableLayoutPanelWrapper.Controls.Add(this.label2, 0, 6);
            this.tableLayoutPanelWrapper.Controls.Add(this.label1, 0, 4);
            this.tableLayoutPanelWrapper.Controls.Add(this.textBoxRok, 0, 1);
            this.tableLayoutPanelWrapper.Controls.Add(this.comboBoxFrom, 0, 5);
            this.tableLayoutPanelWrapper.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanelWrapper.Controls.Add(this.comboBoxPolozka, 0, 3);
            this.tableLayoutPanelWrapper.Location = new System.Drawing.Point(286, 3);
            this.tableLayoutPanelWrapper.Name = "tableLayoutPanelWrapper";
            this.tableLayoutPanelWrapper.RowCount = 11;
            this.tableLayoutPanelWrapper.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelWrapper.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelWrapper.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelWrapper.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelWrapper.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelWrapper.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelWrapper.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelWrapper.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelWrapper.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelWrapper.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelWrapper.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelWrapper.Size = new System.Drawing.Size(495, 433);
            this.tableLayoutPanelWrapper.TabIndex = 12;
            // 
            // comboBoxTo
            // 
            this.comboBoxTo.FormattingEnabled = true;
            this.comboBoxTo.Location = new System.Drawing.Point(3, 147);
            this.comboBoxTo.Name = "comboBoxTo";
            this.comboBoxTo.Size = new System.Drawing.Size(240, 21);
            this.comboBoxTo.TabIndex = 16;
            // 
            // labelRok
            // 
            this.labelRok.AutoSize = true;
            this.labelRok.Location = new System.Drawing.Point(3, 10);
            this.labelRok.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.labelRok.Name = "labelRok";
            this.labelRok.Size = new System.Drawing.Size(27, 13);
            this.labelRok.TabIndex = 13;
            this.labelRok.Text = "Rok";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Kam";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Odkiaľ";
            // 
            // textBoxRok
            // 
            this.textBoxRok.Location = new System.Drawing.Point(3, 26);
            this.textBoxRok.Name = "textBoxRok";
            this.textBoxRok.ReadOnly = true;
            this.textBoxRok.Size = new System.Drawing.Size(240, 22);
            this.textBoxRok.TabIndex = 14;
            // 
            // comboBoxFrom
            // 
            this.comboBoxFrom.FormattingEnabled = true;
            this.comboBoxFrom.Location = new System.Drawing.Point(3, 107);
            this.comboBoxFrom.Name = "comboBoxFrom";
            this.comboBoxFrom.Size = new System.Drawing.Size(240, 21);
            this.comboBoxFrom.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(3, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Položka";
            // 
            // comboBoxPolozka
            // 
            this.comboBoxPolozka.Enabled = false;
            this.comboBoxPolozka.FormattingEnabled = true;
            this.comboBoxPolozka.Location = new System.Drawing.Point(3, 67);
            this.comboBoxPolozka.Name = "comboBoxPolozka";
            this.comboBoxPolozka.Size = new System.Drawing.Size(240, 21);
            this.comboBoxPolozka.TabIndex = 17;
            // 
            // buttonGenerateDefault
            // 
            this.buttonGenerateDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonGenerateDefault.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonGenerateDefault.Location = new System.Drawing.Point(633, 4);
            this.buttonGenerateDefault.Name = "buttonGenerateDefault";
            this.buttonGenerateDefault.Size = new System.Drawing.Size(120, 23);
            this.buttonGenerateDefault.TabIndex = 12;
            this.buttonGenerateDefault.Text = "Generuj defaultne";
            this.toolTipInfo.SetToolTip(this.buttonGenerateDefault, "Vygeneruje defaultne transfery, ak ich nájde medz ekonomickými položkami");
            this.buttonGenerateDefault.UseVisualStyleBackColor = true;
            this.buttonGenerateDefault.Click += new System.EventHandler(this.buttonGenerateDefault_Click);
            // 
            // TransferControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanelWrapper);
            this.Controls.Add(this.listBoxTransfers);
            this.Controls.Add(this.panelControlButtons);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "TransferControl";
            this.Size = new System.Drawing.Size(784, 477);
            this.panelControlButtons.ResumeLayout(false);
            this.contextMenuStripCondition.ResumeLayout(false);
            this.tableLayoutPanelWrapper.ResumeLayout(false);
            this.tableLayoutPanelWrapper.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelControlButtons;
        private System.Windows.Forms.ListBox listBoxTransfers;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelWrapper;
        private System.Windows.Forms.ComboBox comboBoxTo;
        private System.Windows.Forms.Label labelRok;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxRok;
        private System.Windows.Forms.ComboBox comboBoxFrom;
        private System.Windows.Forms.ComboBox comboBoxPolozka;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripCondition;
        private System.Windows.Forms.ToolStripMenuItem andToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem orToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private System.Windows.Forms.Button buttonAddNew;
        private System.Windows.Forms.Button buttonShowPreview;
        private System.Windows.Forms.Button buttonDiscard;
        private System.Windows.Forms.Button buttonSaveChanges;
        private System.Windows.Forms.Button buttonRemoveSelected;
        private System.Windows.Forms.Button buttonGenerateDefault;
    }
}
