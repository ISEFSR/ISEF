namespace cvti.isef.winformapp.Controls.Main.Generator
{
    partial class ConditionsControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConditionsControl));
            this.panelConditions = new System.Windows.Forms.Panel();
            this.listViewConditions = new System.Windows.Forms.ListView();
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageListListView = new System.Windows.Forms.ImageList(this.components);
            this.panelTopTitle = new System.Windows.Forms.Panel();
            this.labelView = new System.Windows.Forms.Label();
            this.trackBarListView = new System.Windows.Forms.TrackBar();
            this.buttonRoll = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStripConditionList = new System.Windows.Forms.ToolStrip();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.equalsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.greaterThanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lessThanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inlistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.likeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.betweenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonRemoveSelected = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonExportAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonImport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.openFileDialogImportExport = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialogFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.selectedConditionControl = new cvti.isef.winformapp.Controls.Main.Generator.Conditios.SelectedConditionControl();
            this.panelSeparator = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelConditions.SuspendLayout();
            this.panelTopTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarListView)).BeginInit();
            this.toolStripConditionList.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelConditions
            // 
            this.panelConditions.Controls.Add(this.listViewConditions);
            this.panelConditions.Controls.Add(this.panelTopTitle);
            this.panelConditions.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelConditions.Location = new System.Drawing.Point(0, 44);
            this.panelConditions.Name = "panelConditions";
            this.panelConditions.Size = new System.Drawing.Size(306, 695);
            this.panelConditions.TabIndex = 2;
            // 
            // listViewConditions
            // 
            this.listViewConditions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName});
            this.listViewConditions.Cursor = System.Windows.Forms.Cursors.Default;
            this.listViewConditions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewConditions.HideSelection = false;
            this.listViewConditions.LargeImageList = this.imageListListView;
            this.listViewConditions.Location = new System.Drawing.Point(0, 51);
            this.listViewConditions.Name = "listViewConditions";
            this.listViewConditions.Size = new System.Drawing.Size(306, 644);
            this.listViewConditions.SmallImageList = this.imageListListView;
            this.listViewConditions.TabIndex = 4;
            this.listViewConditions.UseCompatibleStateImageBehavior = false;
            this.listViewConditions.SelectedIndexChanged += new System.EventHandler(this.listViewConditions_SelectedIndexChanged);
            this.listViewConditions.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewConditions_KeyDown);
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Meno";
            this.columnHeaderName.Width = 260;
            // 
            // imageListListView
            // 
            this.imageListListView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListListView.ImageStream")));
            this.imageListListView.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListListView.Images.SetKeyName(0, "condition___.png");
            // 
            // panelTopTitle
            // 
            this.panelTopTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(250)))));
            this.panelTopTitle.Controls.Add(this.tableLayoutPanel1);
            this.panelTopTitle.Controls.Add(this.labelView);
            this.panelTopTitle.Controls.Add(this.trackBarListView);
            this.panelTopTitle.Controls.Add(this.buttonRoll);
            this.panelTopTitle.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelTopTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopTitle.Location = new System.Drawing.Point(0, 0);
            this.panelTopTitle.Name = "panelTopTitle";
            this.panelTopTitle.Size = new System.Drawing.Size(306, 51);
            this.panelTopTitle.TabIndex = 3;
            // 
            // labelView
            // 
            this.labelView.AutoSize = true;
            this.labelView.Location = new System.Drawing.Point(7, 67);
            this.labelView.Name = "labelView";
            this.labelView.Size = new System.Drawing.Size(57, 13);
            this.labelView.TabIndex = 9;
            this.labelView.Text = "LargeIcon";
            // 
            // trackBarListView
            // 
            this.trackBarListView.BackColor = System.Drawing.Color.White;
            this.trackBarListView.LargeChange = 1;
            this.trackBarListView.Location = new System.Drawing.Point(5, 83);
            this.trackBarListView.Maximum = 4;
            this.trackBarListView.Name = "trackBarListView";
            this.trackBarListView.Size = new System.Drawing.Size(298, 45);
            this.trackBarListView.TabIndex = 8;
            this.trackBarListView.ValueChanged += new System.EventHandler(this.trackBarListView_ValueChanged);
            // 
            // buttonRoll
            // 
            this.buttonRoll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRoll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonRoll.FlatAppearance.BorderSize = 0;
            this.buttonRoll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRoll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(89)))), ((int)(((byte)(127)))));
            this.buttonRoll.Location = new System.Drawing.Point(280, 3);
            this.buttonRoll.Name = "buttonRoll";
            this.buttonRoll.Size = new System.Drawing.Size(23, 23);
            this.buttonRoll.TabIndex = 7;
            this.buttonRoll.Text = "v";
            this.toolTipInfo.SetToolTip(this.buttonRoll, "Zobraz filtre pre príkazy");
            this.buttonRoll.UseVisualStyleBackColor = true;
            this.buttonRoll.Click += new System.EventHandler(this.buttonRoll_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(89)))), ((int)(((byte)(127)))));
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 21);
            this.label1.TabIndex = 4;
            this.label1.Text = "Výber podmienky";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(89)))), ((int)(((byte)(127)))));
            this.label2.Location = new System.Drawing.Point(3, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(185, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Výber dostupného SQL podmienky";
            // 
            // toolStripConditionList
            // 
            this.toolStripConditionList.AutoSize = false;
            this.toolStripConditionList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButton1,
            this.toolStripSeparator1,
            this.toolStripButton1,
            this.toolStripSeparator4,
            this.toolStripButtonRemoveSelected,
            this.toolStripSeparator2,
            this.toolStripButtonExportAll,
            this.toolStripSeparator3,
            this.toolStripButtonImport,
            this.toolStripSeparator5,
            this.toolStripButton2,
            this.toolStripSeparator6});
            this.toolStripConditionList.Location = new System.Drawing.Point(0, 0);
            this.toolStripConditionList.Name = "toolStripConditionList";
            this.toolStripConditionList.Size = new System.Drawing.Size(1416, 43);
            this.toolStripConditionList.TabIndex = 0;
            this.toolStripConditionList.Text = "toolStrip1";
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.AutoSize = false;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.equalsToolStripMenuItem,
            this.greaterThanToolStripMenuItem,
            this.lessThanToolStripMenuItem,
            this.inlistToolStripMenuItem,
            this.likeToolStripMenuItem,
            this.betweenToolStripMenuItem});
            this.toolStripSplitButton1.Image = global::cvti.isef.winformapp.Properties.Resources.add;
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(150, 40);
            this.toolStripSplitButton1.Text = "Pridaj novú";
            this.toolStripSplitButton1.ToolTipText = "Add new condition";
            this.toolStripSplitButton1.ButtonClick += new System.EventHandler(this.toolStripSplitButton1_ButtonClick);
            // 
            // equalsToolStripMenuItem
            // 
            this.equalsToolStripMenuItem.Name = "equalsToolStripMenuItem";
            this.equalsToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.equalsToolStripMenuItem.Tag = "0";
            this.equalsToolStripMenuItem.Text = "Equals...";
            this.equalsToolStripMenuItem.Click += new System.EventHandler(this.conditionToolStripMenuItem_Click);
            // 
            // greaterThanToolStripMenuItem
            // 
            this.greaterThanToolStripMenuItem.Name = "greaterThanToolStripMenuItem";
            this.greaterThanToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.greaterThanToolStripMenuItem.Tag = "1";
            this.greaterThanToolStripMenuItem.Text = "Greater than...";
            this.greaterThanToolStripMenuItem.Click += new System.EventHandler(this.conditionToolStripMenuItem_Click);
            // 
            // lessThanToolStripMenuItem
            // 
            this.lessThanToolStripMenuItem.Name = "lessThanToolStripMenuItem";
            this.lessThanToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.lessThanToolStripMenuItem.Tag = "2";
            this.lessThanToolStripMenuItem.Text = "Less than...";
            this.lessThanToolStripMenuItem.Click += new System.EventHandler(this.conditionToolStripMenuItem_Click);
            // 
            // inlistToolStripMenuItem
            // 
            this.inlistToolStripMenuItem.Name = "inlistToolStripMenuItem";
            this.inlistToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.inlistToolStripMenuItem.Tag = "3";
            this.inlistToolStripMenuItem.Text = "Inlist...";
            this.inlistToolStripMenuItem.Click += new System.EventHandler(this.conditionToolStripMenuItem_Click);
            // 
            // likeToolStripMenuItem
            // 
            this.likeToolStripMenuItem.Enabled = false;
            this.likeToolStripMenuItem.Name = "likeToolStripMenuItem";
            this.likeToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.likeToolStripMenuItem.Text = "Like...";
            // 
            // betweenToolStripMenuItem
            // 
            this.betweenToolStripMenuItem.Enabled = false;
            this.betweenToolStripMenuItem.Name = "betweenToolStripMenuItem";
            this.betweenToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.betweenToolStripMenuItem.Text = "Between...";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 43);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.AutoSize = false;
            this.toolStripButton1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton1.Image = global::cvti.isef.winformapp.Properties.Resources.clone32;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(140, 40);
            this.toolStripButton1.Text = "Naklonuj vybranú";
            this.toolStripButton1.ToolTipText = "ň";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 43);
            // 
            // toolStripButtonRemoveSelected
            // 
            this.toolStripButtonRemoveSelected.AutoSize = false;
            this.toolStripButtonRemoveSelected.Image = global::cvti.isef.winformapp.Properties.Resources.remove;
            this.toolStripButtonRemoveSelected.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRemoveSelected.Name = "toolStripButtonRemoveSelected";
            this.toolStripButtonRemoveSelected.Size = new System.Drawing.Size(150, 40);
            this.toolStripButtonRemoveSelected.Text = "Odstráň vybranú";
            this.toolStripButtonRemoveSelected.ToolTipText = "Remove selected condition";
            this.toolStripButtonRemoveSelected.Click += new System.EventHandler(this.toolStripButtonRemoveSelected_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 43);
            // 
            // toolStripButtonExportAll
            // 
            this.toolStripButtonExportAll.AutoSize = false;
            this.toolStripButtonExportAll.Image = global::cvti.isef.winformapp.Properties.Resources.download;
            this.toolStripButtonExportAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExportAll.Name = "toolStripButtonExportAll";
            this.toolStripButtonExportAll.Size = new System.Drawing.Size(150, 40);
            this.toolStripButtonExportAll.Text = "Exportuj podmienky";
            this.toolStripButtonExportAll.Click += new System.EventHandler(this.toolStripButtonExportAll_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 43);
            // 
            // toolStripButtonImport
            // 
            this.toolStripButtonImport.AutoSize = false;
            this.toolStripButtonImport.Image = global::cvti.isef.winformapp.Properties.Resources.import;
            this.toolStripButtonImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButtonImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonImport.Name = "toolStripButtonImport";
            this.toolStripButtonImport.Size = new System.Drawing.Size(150, 40);
            this.toolStripButtonImport.Text = "Importuj podmienky";
            this.toolStripButtonImport.Click += new System.EventHandler(this.toolStripButtonImport_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 43);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.AutoSize = false;
            this.toolStripButton2.Image = global::cvti.isef.winformapp.Properties.Resources.import;
            this.toolStripButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(150, 40);
            this.toolStripButton2.Text = "Generuj defaultne";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 43);
            // 
            // openFileDialogImportExport
            // 
            this.openFileDialogImportExport.Filter = "*.json|*.json";
            this.openFileDialogImportExport.Title = "Import podmienok";
            // 
            // folderBrowserDialogFolder
            // 
            this.folderBrowserDialogFolder.Description = "Vyber adresár pre exportný súbor";
            // 
            // selectedConditionControl
            // 
            this.selectedConditionControl.BackColor = System.Drawing.Color.White;
            this.selectedConditionControl.Cursor = System.Windows.Forms.Cursors.Default;
            this.selectedConditionControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectedConditionControl.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.selectedConditionControl.Location = new System.Drawing.Point(306, 44);
            this.selectedConditionControl.MessageBoardBackground = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.selectedConditionControl.MessageBoardForeground = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.selectedConditionControl.Name = "selectedConditionControl";
            this.selectedConditionControl.Size = new System.Drawing.Size(1110, 695);
            this.selectedConditionControl.TabIndex = 4;
            this.selectedConditionControl.ConditionSaved += new System.EventHandler(this.selectedConditionControl_ConditionSaved);
            this.selectedConditionControl.NewConditionCreated += new System.EventHandler<cvti.data.Conditions.Condition>(this.selectedConditionControl_NewConditionCreated);
            // 
            // panelSeparator
            // 
            this.panelSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.panelSeparator.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSeparator.Location = new System.Drawing.Point(0, 43);
            this.panelSeparator.Name = "panelSeparator";
            this.panelSeparator.Size = new System.Drawing.Size(1416, 1);
            this.panelSeparator.TabIndex = 29;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(274, 48);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // ConditionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.selectedConditionControl);
            this.Controls.Add(this.panelConditions);
            this.Controls.Add(this.panelSeparator);
            this.Controls.Add(this.toolStripConditionList);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "ConditionsControl";
            this.Size = new System.Drawing.Size(1416, 739);
            this.Controls.SetChildIndex(this.toolStripConditionList, 0);
            this.Controls.SetChildIndex(this.panelSeparator, 0);
            this.Controls.SetChildIndex(this.panelConditions, 0);
            this.Controls.SetChildIndex(this.selectedConditionControl, 0);
            this.panelConditions.ResumeLayout(false);
            this.panelTopTitle.ResumeLayout(false);
            this.panelTopTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarListView)).EndInit();
            this.toolStripConditionList.ResumeLayout(false);
            this.toolStripConditionList.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelConditions;
        private System.Windows.Forms.ToolStrip toolStripConditionList;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem equalsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem greaterThanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lessThanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inlistToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonRemoveSelected;
        private System.Windows.Forms.Panel panelTopTitle;
        private Conditios.SelectedConditionControl selectedConditionControl;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonExportAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButtonImport;
        private System.Windows.Forms.ToolStripMenuItem likeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem betweenToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialogImportExport;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogFolder;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.Button buttonRoll;
        private System.Windows.Forms.ListView listViewConditions;
        private System.Windows.Forms.Label labelView;
        private System.Windows.Forms.TrackBar trackBarListView;
        private System.Windows.Forms.ImageList imageListListView;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.Panel panelSeparator;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
