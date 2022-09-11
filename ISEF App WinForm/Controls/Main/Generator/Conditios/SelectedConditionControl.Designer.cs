namespace cvti.isef.winformapp.Controls.Main.Generator.Conditios
{
    partial class SelectedConditionControl
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
            this.panelTopTitle = new System.Windows.Forms.Panel();
            this.labelName = new System.Windows.Forms.Label();
            this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
            this.panelSeparator1 = new System.Windows.Forms.Panel();
            this.treeViewCondition = new System.Windows.Forms.TreeView();
            this.panelConditionTree = new System.Windows.Forms.Panel();
            this.toolStripTree = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAddSameLevel = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAddUnder = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSaveAs = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSaveSelected = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonRestore = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonRemove = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRemoveSubTree = new System.Windows.Forms.ToolStripButton();
            this.textBoxSQL = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxValue = new System.Windows.Forms.TextBox();
            this.label = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxConditionType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxConditionOperator = new System.Windows.Forms.ComboBox();
            this.checkBoxWrap = new System.Windows.Forms.CheckBox();
            this.comboBoxColumn = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelConditionNmae = new System.Windows.Forms.Label();
            this.labelConditionType = new System.Windows.Forms.Label();
            this.checkBoxNegate = new System.Windows.Forms.CheckBox();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.panelTopTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
            this.panelConditionTree.SuspendLayout();
            this.toolStripTree.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTopTitle
            // 
            this.panelTopTitle.BackColor = System.Drawing.SystemColors.Control;
            this.panelTopTitle.Controls.Add(this.labelName);
            this.panelTopTitle.Controls.Add(this.pictureBoxIcon);
            this.panelTopTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelTopTitle.Location = new System.Drawing.Point(0, 0);
            this.panelTopTitle.Name = "panelTopTitle";
            this.panelTopTitle.Size = new System.Drawing.Size(1276, 52);
            this.panelTopTitle.TabIndex = 30;
            // 
            // labelName
            // 
            this.labelName.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(89)))), ((int)(((byte)(127)))));
            this.labelName.Location = new System.Drawing.Point(52, 0);
            this.labelName.Margin = new System.Windows.Forms.Padding(5);
            this.labelName.Name = "labelName";
            this.labelName.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.labelName.Size = new System.Drawing.Size(277, 52);
            this.labelName.TabIndex = 4;
            this.labelName.Tag = "EDITÁCIA PODMIENKY";
            this.labelName.Text = "Podmienka: meno";
            this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBoxIcon
            // 
            this.pictureBoxIcon.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBoxIcon.Image = global::cvti.isef.winformapp.Properties.Resources.condition___;
            this.pictureBoxIcon.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxIcon.Name = "pictureBoxIcon";
            this.pictureBoxIcon.Size = new System.Drawing.Size(52, 52);
            this.pictureBoxIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxIcon.TabIndex = 6;
            this.pictureBoxIcon.TabStop = false;
            // 
            // panelSeparator1
            // 
            this.panelSeparator1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.panelSeparator1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSeparator1.Location = new System.Drawing.Point(0, 52);
            this.panelSeparator1.Name = "panelSeparator1";
            this.panelSeparator1.Size = new System.Drawing.Size(1276, 1);
            this.panelSeparator1.TabIndex = 31;
            // 
            // treeViewCondition
            // 
            this.treeViewCondition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeViewCondition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewCondition.HideSelection = false;
            this.treeViewCondition.Location = new System.Drawing.Point(0, 87);
            this.treeViewCondition.Name = "treeViewCondition";
            this.treeViewCondition.Size = new System.Drawing.Size(504, 619);
            this.treeViewCondition.TabIndex = 32;
            this.treeViewCondition.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewCondition_AfterSelect);
            // 
            // panelConditionTree
            // 
            this.panelConditionTree.Controls.Add(this.treeViewCondition);
            this.panelConditionTree.Controls.Add(this.toolStripTree);
            this.panelConditionTree.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelConditionTree.Location = new System.Drawing.Point(0, 53);
            this.panelConditionTree.Name = "panelConditionTree";
            this.panelConditionTree.Size = new System.Drawing.Size(504, 706);
            this.panelConditionTree.TabIndex = 33;
            // 
            // toolStripTree
            // 
            this.toolStripTree.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAddSameLevel,
            this.toolStripButtonAddUnder,
            this.toolStripSeparator1,
            this.toolStripButtonSave,
            this.toolStripButtonSaveAs,
            this.toolStripButtonSaveSelected,
            this.toolStripSeparator3,
            this.toolStripButtonRestore,
            this.toolStripSeparator2,
            this.toolStripButtonRemove,
            this.toolStripButtonRemoveSubTree});
            this.toolStripTree.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStripTree.Location = new System.Drawing.Point(0, 0);
            this.toolStripTree.Name = "toolStripTree";
            this.toolStripTree.Size = new System.Drawing.Size(504, 87);
            this.toolStripTree.TabIndex = 0;
            this.toolStripTree.Text = "toolStrip1";
            // 
            // toolStripButtonAddSameLevel
            // 
            this.toolStripButtonAddSameLevel.Image = global::cvti.isef.winformapp.Properties.Resources.plus48;
            this.toolStripButtonAddSameLevel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddSameLevel.Name = "toolStripButtonAddSameLevel";
            this.toolStripButtonAddSameLevel.Size = new System.Drawing.Size(134, 24);
            this.toolStripButtonAddSameLevel.Text = "Pridaj podmienku...";
            this.toolStripButtonAddSameLevel.ToolTipText = "Pridá podmienku na rovnakú úroveň";
            this.toolStripButtonAddSameLevel.Click += new System.EventHandler(this.toolStripButtonAddSameLevel_Click);
            // 
            // toolStripButtonAddUnder
            // 
            this.toolStripButtonAddUnder.Image = global::cvti.isef.winformapp.Properties.Resources.subtreeadd;
            this.toolStripButtonAddUnder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddUnder.Name = "toolStripButtonAddUnder";
            this.toolStripButtonAddUnder.Size = new System.Drawing.Size(158, 24);
            this.toolStripButtonAddUnder.Text = "Pridaj podmienku pod...";
            this.toolStripButtonAddUnder.ToolTipText = "Pridá podmienku pod vybranú podmienku";
            this.toolStripButtonAddUnder.Click += new System.EventHandler(this.toolStripButtonAddUnder_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(15, 30);
            // 
            // toolStripButtonSave
            // 
            this.toolStripButtonSave.Image = global::cvti.isef.winformapp.Properties.Resources.save48;
            this.toolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSave.Name = "toolStripButtonSave";
            this.toolStripButtonSave.Size = new System.Drawing.Size(118, 24);
            this.toolStripButtonSave.Text = "Ulož podmienku";
            this.toolStripButtonSave.ToolTipText = "Uloží zmeny";
            this.toolStripButtonSave.Click += new System.EventHandler(this.toolStripButtonSave_Click);
            // 
            // toolStripButtonSaveAs
            // 
            this.toolStripButtonSaveAs.Image = global::cvti.isef.winformapp.Properties.Resources.saveas48;
            this.toolStripButtonSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSaveAs.Name = "toolStripButtonSaveAs";
            this.toolStripButtonSaveAs.Size = new System.Drawing.Size(149, 24);
            this.toolStripButtonSaveAs.Text = "Ulož podmienku ako...";
            this.toolStripButtonSaveAs.ToolTipText = "Uloží podmienku ako...";
            this.toolStripButtonSaveAs.Click += new System.EventHandler(this.toolStripButtonSaveAs_Click);
            // 
            // toolStripButtonSaveSelected
            // 
            this.toolStripButtonSaveSelected.Image = global::cvti.isef.winformapp.Properties.Resources.ssaveas48;
            this.toolStripButtonSaveSelected.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSaveSelected.Name = "toolStripButtonSaveSelected";
            this.toolStripButtonSaveSelected.Size = new System.Drawing.Size(131, 24);
            this.toolStripButtonSaveSelected.Text = "Ulož vybranú ako...";
            this.toolStripButtonSaveSelected.ToolTipText = "Uloží vybranú podmienku ako...";
            this.toolStripButtonSaveSelected.Click += new System.EventHandler(this.toolStripButtonSaveSelected_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AutoSize = false;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(15, 30);
            // 
            // toolStripButtonRestore
            // 
            this.toolStripButtonRestore.Image = global::cvti.isef.winformapp.Properties.Resources.discard48;
            this.toolStripButtonRestore.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRestore.Name = "toolStripButtonRestore";
            this.toolStripButtonRestore.Size = new System.Drawing.Size(92, 24);
            this.toolStripButtonRestore.Text = "Zruš zmeny";
            this.toolStripButtonRestore.ToolTipText = "Zruší všetky vykonané zmeny";
            this.toolStripButtonRestore.Click += new System.EventHandler(this.toolStripButtonRestore_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(15, 30);
            // 
            // toolStripButtonRemove
            // 
            this.toolStripButtonRemove.Image = global::cvti.isef.winformapp.Properties.Resources.minus48;
            this.toolStripButtonRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRemove.Name = "toolStripButtonRemove";
            this.toolStripButtonRemove.Size = new System.Drawing.Size(119, 24);
            this.toolStripButtonRemove.Text = "Odstráň vybranú";
            this.toolStripButtonRemove.ToolTipText = "Odstráňi vybranú podmienku aj s podstromom";
            this.toolStripButtonRemove.Click += new System.EventHandler(this.toolStripButtonRemove_Click);
            // 
            // toolStripButtonRemoveSubTree
            // 
            this.toolStripButtonRemoveSubTree.Image = global::cvti.isef.winformapp.Properties.Resources.subtreremove;
            this.toolStripButtonRemoveSubTree.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRemoveSubTree.Name = "toolStripButtonRemoveSubTree";
            this.toolStripButtonRemoveSubTree.Size = new System.Drawing.Size(128, 24);
            this.toolStripButtonRemoveSubTree.Text = "Odstráň podstrom";
            this.toolStripButtonRemoveSubTree.ToolTipText = "Odstráňi podstrom pre vybranú podmienku";
            this.toolStripButtonRemoveSubTree.Click += new System.EventHandler(this.toolStripButtonRemoveSubTree_Click);
            // 
            // textBoxSQL
            // 
            this.textBoxSQL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxSQL.Location = new System.Drawing.Point(5, 330);
            this.textBoxSQL.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.textBoxSQL.Multiline = true;
            this.textBoxSQL.Name = "textBoxSQL";
            this.textBoxSQL.ReadOnly = true;
            this.textBoxSQL.Size = new System.Drawing.Size(762, 374);
            this.textBoxSQL.TabIndex = 34;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(5, 242);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.ReadOnly = true;
            this.textBoxName.Size = new System.Drawing.Size(325, 22);
            this.textBoxName.TabIndex = 36;
            this.textBoxName.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Help;
            this.label1.Location = new System.Drawing.Point(5, 227);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 37;
            this.label1.Text = "Meno podmienky:";
            this.toolTipInfo.SetToolTip(this.label1, "Meno podmienky");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Help;
            this.label3.Location = new System.Drawing.Point(5, 271);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 13);
            this.label3.TabIndex = 39;
            this.label3.Text = "Hodnota podmienky:";
            this.toolTipInfo.SetToolTip(this.label3, "Hodnota, alebo pravá strana podmeinky");
            // 
            // textBoxValue
            // 
            this.textBoxValue.Location = new System.Drawing.Point(5, 286);
            this.textBoxValue.Margin = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.ReadOnly = true;
            this.textBoxValue.Size = new System.Drawing.Size(325, 22);
            this.textBoxValue.TabIndex = 40;
            this.textBoxValue.TextChanged += new System.EventHandler(this.textBoxValue_TextChanged);
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Cursor = System.Windows.Forms.Cursors.Help;
            this.label.Location = new System.Drawing.Point(5, 315);
            this.label.Margin = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(67, 13);
            this.label.TabIndex = 41;
            this.label.Text = "Podmienka:";
            this.toolTipInfo.SetToolTip(this.label, "SQL podmienka pre vybranú podmienku aj s podstromom");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.Help;
            this.label2.Location = new System.Drawing.Point(5, 98);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 42;
            this.label2.Text = "Typ podmienky:";
            this.toolTipInfo.SetToolTip(this.label2, "Typ podmienky");
            // 
            // comboBoxConditionType
            // 
            this.comboBoxConditionType.Enabled = false;
            this.comboBoxConditionType.FormattingEnabled = true;
            this.comboBoxConditionType.Location = new System.Drawing.Point(5, 113);
            this.comboBoxConditionType.Margin = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.comboBoxConditionType.Name = "comboBoxConditionType";
            this.comboBoxConditionType.Size = new System.Drawing.Size(325, 21);
            this.comboBoxConditionType.TabIndex = 43;
            this.comboBoxConditionType.SelectedIndexChanged += new System.EventHandler(this.comboBoxConditionType_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Cursor = System.Windows.Forms.Cursors.Help;
            this.label4.Location = new System.Drawing.Point(5, 141);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 13);
            this.label4.TabIndex = 44;
            this.label4.Text = "Operátor podmienky:";
            this.toolTipInfo.SetToolTip(this.label4, "Operátor podmienky");
            // 
            // comboBoxConditionOperator
            // 
            this.comboBoxConditionOperator.Enabled = false;
            this.comboBoxConditionOperator.FormattingEnabled = true;
            this.comboBoxConditionOperator.Location = new System.Drawing.Point(5, 156);
            this.comboBoxConditionOperator.Margin = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.comboBoxConditionOperator.Name = "comboBoxConditionOperator";
            this.comboBoxConditionOperator.Size = new System.Drawing.Size(325, 21);
            this.comboBoxConditionOperator.TabIndex = 45;
            this.comboBoxConditionOperator.SelectedIndexChanged += new System.EventHandler(this.comboBoxConditionOperator_SelectedIndexChanged);
            // 
            // checkBoxWrap
            // 
            this.checkBoxWrap.AutoSize = true;
            this.checkBoxWrap.Location = new System.Drawing.Point(5, 76);
            this.checkBoxWrap.Margin = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.checkBoxWrap.Name = "checkBoxWrap";
            this.checkBoxWrap.Size = new System.Drawing.Size(201, 17);
            this.checkBoxWrap.TabIndex = 54;
            this.checkBoxWrap.Text = "Uzavri všetky vnútorné podmienky";
            this.toolTipInfo.SetToolTip(this.checkBoxWrap, "Uzavrie vnútorné podmienky do zátvoriek");
            this.checkBoxWrap.UseVisualStyleBackColor = true;
            this.checkBoxWrap.CheckedChanged += new System.EventHandler(this.checkBoxWrap_CheckedChanged);
            // 
            // comboBoxColumn
            // 
            this.comboBoxColumn.Enabled = false;
            this.comboBoxColumn.FormattingEnabled = true;
            this.comboBoxColumn.Location = new System.Drawing.Point(5, 199);
            this.comboBoxColumn.Margin = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.comboBoxColumn.Name = "comboBoxColumn";
            this.comboBoxColumn.Size = new System.Drawing.Size(325, 21);
            this.comboBoxColumn.TabIndex = 53;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Cursor = System.Windows.Forms.Cursors.Help;
            this.label5.Location = new System.Drawing.Point(5, 184);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 13);
            this.label5.TabIndex = 52;
            this.label5.Text = "Stĺpec podmienky:";
            this.toolTipInfo.SetToolTip(this.label5, "Ľavá strana podmienky");
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.comboBoxColumn, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxWrap, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.textBoxSQL, 0, 15);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.label, 0, 14);
            this.tableLayoutPanel1.Controls.Add(this.labelConditionNmae, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBoxValue, 0, 13);
            this.tableLayoutPanel1.Controls.Add(this.labelConditionType, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 12);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxNegate, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBoxName, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxConditionType, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxConditionOperator, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 6);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(504, 53);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 16;
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(772, 706);
            this.tableLayoutPanel1.TabIndex = 51;
            // 
            // labelConditionNmae
            // 
            this.labelConditionNmae.AutoSize = true;
            this.labelConditionNmae.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelConditionNmae.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelConditionNmae.Location = new System.Drawing.Point(5, 26);
            this.labelConditionNmae.Margin = new System.Windows.Forms.Padding(5, 0, 3, 10);
            this.labelConditionNmae.Name = "labelConditionNmae";
            this.labelConditionNmae.Size = new System.Drawing.Size(764, 13);
            this.labelConditionNmae.TabIndex = 50;
            this.labelConditionNmae.Text = "AssuView.Rok = 2020";
            // 
            // labelConditionType
            // 
            this.labelConditionType.AutoSize = true;
            this.labelConditionType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelConditionType.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelConditionType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelConditionType.Location = new System.Drawing.Point(5, 5);
            this.labelConditionType.Margin = new System.Windows.Forms.Padding(5, 5, 3, 0);
            this.labelConditionType.Name = "labelConditionType";
            this.labelConditionType.Size = new System.Drawing.Size(764, 21);
            this.labelConditionType.TabIndex = 49;
            this.labelConditionType.Text = "cvti.data.core.Conditions.Equal ( = )";
            this.labelConditionType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkBoxNegate
            // 
            this.checkBoxNegate.AutoSize = true;
            this.checkBoxNegate.Location = new System.Drawing.Point(5, 54);
            this.checkBoxNegate.Margin = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.checkBoxNegate.Name = "checkBoxNegate";
            this.checkBoxNegate.Size = new System.Drawing.Size(78, 17);
            this.checkBoxNegate.TabIndex = 46;
            this.checkBoxNegate.Text = "Negovaná";
            this.toolTipInfo.SetToolTip(this.checkBoxNegate, "Určuje či je podmienka negovaná");
            this.checkBoxNegate.UseVisualStyleBackColor = true;
            this.checkBoxNegate.CheckedChanged += new System.EventHandler(this.checkBoxNegate_CheckedChanged);
            // 
            // SelectedConditionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panelConditionTree);
            this.Controls.Add(this.panelSeparator1);
            this.Controls.Add(this.panelTopTitle);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "SelectedConditionControl";
            this.Size = new System.Drawing.Size(1276, 759);
            this.Controls.SetChildIndex(this.panelTopTitle, 0);
            this.Controls.SetChildIndex(this.panelSeparator1, 0);
            this.Controls.SetChildIndex(this.panelConditionTree, 0);
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.panelTopTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
            this.panelConditionTree.ResumeLayout(false);
            this.panelConditionTree.PerformLayout();
            this.toolStripTree.ResumeLayout(false);
            this.toolStripTree.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelTopTitle;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.PictureBox pictureBoxIcon;
        private System.Windows.Forms.Panel panelSeparator1;
        private System.Windows.Forms.TreeView treeViewCondition;
        private System.Windows.Forms.Panel panelConditionTree;
        private System.Windows.Forms.ToolStrip toolStripTree;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddSameLevel;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddUnder;
        private System.Windows.Forms.ToolStripButton toolStripButtonSave;
        private System.Windows.Forms.ToolStripButton toolStripButtonSaveAs;
        private System.Windows.Forms.ToolStripButton toolStripButtonSaveSelected;
        private System.Windows.Forms.ToolStripButton toolStripButtonRestore;
        private System.Windows.Forms.ToolStripButton toolStripButtonRemove;
        private System.Windows.Forms.TextBox textBoxSQL;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxValue;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxConditionType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxConditionOperator;
        private System.Windows.Forms.CheckBox checkBoxNegate;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private System.Windows.Forms.ToolStripButton toolStripButtonRemoveSubTree;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Label labelConditionType;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelConditionNmae;
        private System.Windows.Forms.ComboBox comboBoxColumn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBoxWrap;
    }
}
