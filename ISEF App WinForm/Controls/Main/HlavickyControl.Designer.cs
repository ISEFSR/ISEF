namespace cvti.isef.winformapp.Controls.Main
{
    partial class HlavickyControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HlavickyControl));
            this.toolStripClassifiersSettings = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAddNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonRemove = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonShow = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonCreateDefault = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonReload = new System.Windows.Forms.ToolStripButton();
            this.listViewHlavicky = new System.Windows.Forms.ListView();
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageListHlavicky = new System.Windows.Forms.ImageList(this.components);
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.buttonRoll = new System.Windows.Forms.Button();
            this.openFileDialogHlavicka = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelSeparatorHorizontal = new System.Windows.Forms.Panel();
            this.panelHlavickaFilter = new System.Windows.Forms.Panel();
            this.comboBoxTypHlavicky = new System.Windows.Forms.ComboBox();
            this.checkBoxilltruj = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.panelVerticalSeparator = new System.Windows.Forms.Panel();
            this.hlavickaControl = new cvti.isef.winformapp.Controls.Main.Hlavicky.HlavickaControl();
            this.panelHorizontalSeparator = new System.Windows.Forms.Panel();
            this.toolStripClassifiersSettings.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelHlavickaFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripClassifiersSettings
            // 
            this.toolStripClassifiersSettings.AutoSize = false;
            this.toolStripClassifiersSettings.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripClassifiersSettings.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAddNew,
            this.toolStripSeparator1,
            this.toolStripButtonRemove,
            this.toolStripSeparator5,
            this.toolStripButtonShow,
            this.toolStripSeparator2,
            this.toolStripButtonCreateDefault,
            this.toolStripSeparator3,
            this.toolStripButtonReload});
            this.toolStripClassifiersSettings.Location = new System.Drawing.Point(0, 0);
            this.toolStripClassifiersSettings.Name = "toolStripClassifiersSettings";
            this.toolStripClassifiersSettings.Size = new System.Drawing.Size(1278, 43);
            this.toolStripClassifiersSettings.TabIndex = 10;
            this.toolStripClassifiersSettings.Text = "toolStripClassifiers";
            // 
            // toolStripButtonAddNew
            // 
            this.toolStripButtonAddNew.Image = global::cvti.isef.winformapp.Properties.Resources.add;
            this.toolStripButtonAddNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddNew.Name = "toolStripButtonAddNew";
            this.toolStripButtonAddNew.Size = new System.Drawing.Size(108, 40);
            this.toolStripButtonAddNew.Text = "Pridaj hlavičku";
            this.toolStripButtonAddNew.ToolTipText = "Exportuje vybraný číselník pre vybraný kalendárny rok do XLSX súboru.";
            this.toolStripButtonAddNew.Click += new System.EventHandler(this.toolStripButtonAddNew_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 43);
            // 
            // toolStripButtonRemove
            // 
            this.toolStripButtonRemove.Image = global::cvti.isef.winformapp.Properties.Resources.icons8_delete_file_48;
            this.toolStripButtonRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRemove.Name = "toolStripButtonRemove";
            this.toolStripButtonRemove.Size = new System.Drawing.Size(119, 40);
            this.toolStripButtonRemove.Text = "Odstráň vybranú";
            this.toolStripButtonRemove.ToolTipText = "Načíta odznova údaje pre vybraný číselník a kalendárny rok";
            this.toolStripButtonRemove.Click += new System.EventHandler(this.toolStripButtonRemove_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 43);
            // 
            // toolStripButtonShow
            // 
            this.toolStripButtonShow.Image = global::cvti.isef.winformapp.Properties.Resources.icons8_analyze_48;
            this.toolStripButtonShow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonShow.Name = "toolStripButtonShow";
            this.toolStripButtonShow.Size = new System.Drawing.Size(108, 40);
            this.toolStripButtonShow.Text = "Otvor hlavičku";
            this.toolStripButtonShow.ToolTipText = "Zobrazí prehľad pre vybraný číselník";
            this.toolStripButtonShow.Click += new System.EventHandler(this.toolStripButtonShow_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 43);
            // 
            // toolStripButtonCreateDefault
            // 
            this.toolStripButtonCreateDefault.Image = global::cvti.isef.winformapp.Properties.Resources.icons8_analyze_48;
            this.toolStripButtonCreateDefault.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCreateDefault.Name = "toolStripButtonCreateDefault";
            this.toolStripButtonCreateDefault.Size = new System.Drawing.Size(118, 40);
            this.toolStripButtonCreateDefault.Text = "Vytvor defaultne";
            this.toolStripButtonCreateDefault.ToolTipText = "Odznova vytvorí všetky defaultné hlavičky";
            this.toolStripButtonCreateDefault.Click += new System.EventHandler(this.toolStripButtonCreateDefault_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 43);
            // 
            // toolStripButtonReload
            // 
            this.toolStripButtonReload.Image = global::cvti.isef.winformapp.Properties.Resources.update48;
            this.toolStripButtonReload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonReload.Name = "toolStripButtonReload";
            this.toolStripButtonReload.Size = new System.Drawing.Size(116, 40);
            this.toolStripButtonReload.Text = "Načítaj odznova";
            this.toolStripButtonReload.ToolTipText = "Odznova načíta všetky hlavičky";
            this.toolStripButtonReload.Click += new System.EventHandler(this.toolStripButtonReload_Click);
            // 
            // listViewHlavicky
            // 
            this.listViewHlavicky.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listViewHlavicky.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewHlavicky.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName});
            this.listViewHlavicky.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewHlavicky.FullRowSelect = true;
            this.listViewHlavicky.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewHlavicky.HideSelection = false;
            this.listViewHlavicky.LargeImageList = this.imageListHlavicky;
            this.listViewHlavicky.Location = new System.Drawing.Point(0, 50);
            this.listViewHlavicky.MultiSelect = false;
            this.listViewHlavicky.Name = "listViewHlavicky";
            this.listViewHlavicky.ShowItemToolTips = true;
            this.listViewHlavicky.Size = new System.Drawing.Size(315, 639);
            this.listViewHlavicky.SmallImageList = this.imageListHlavicky;
            this.listViewHlavicky.TabIndex = 11;
            this.listViewHlavicky.TileSize = new System.Drawing.Size(40, 40);
            this.listViewHlavicky.UseCompatibleStateImageBehavior = false;
            this.listViewHlavicky.View = System.Windows.Forms.View.Details;
            this.listViewHlavicky.SelectedIndexChanged += new System.EventHandler(this.listViewHlavicky_SelectedIndexChanged);
            this.listViewHlavicky.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewHlavicky_KeyDown);
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Názov";
            this.columnHeaderName.Width = 315;
            // 
            // imageListHlavicky
            // 
            this.imageListHlavicky.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListHlavicky.ImageStream")));
            this.imageListHlavicky.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListHlavicky.Images.SetKeyName(0, "excel48.png");
            // 
            // buttonRoll
            // 
            this.buttonRoll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRoll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonRoll.FlatAppearance.BorderSize = 0;
            this.buttonRoll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRoll.Location = new System.Drawing.Point(286, 3);
            this.buttonRoll.Name = "buttonRoll";
            this.buttonRoll.Size = new System.Drawing.Size(23, 23);
            this.buttonRoll.TabIndex = 4;
            this.buttonRoll.Text = "v";
            this.toolTipInfo.SetToolTip(this.buttonRoll, "Zobraz filtre pre hlavičky");
            this.buttonRoll.UseVisualStyleBackColor = true;
            this.buttonRoll.Click += new System.EventHandler(this.buttonRoll_Click);
            // 
            // openFileDialogHlavicka
            // 
            this.openFileDialogHlavicka.Filter = "Excel file|*.xlsx";
            this.openFileDialogHlavicka.Multiselect = true;
            this.openFileDialogHlavicka.Title = "Vyber hlavičku";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panelSeparatorHorizontal);
            this.panel1.Controls.Add(this.listViewHlavicky);
            this.panel1.Controls.Add(this.panelHlavickaFilter);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(315, 689);
            this.panel1.TabIndex = 23;
            // 
            // panelSeparatorHorizontal
            // 
            this.panelSeparatorHorizontal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.panelSeparatorHorizontal.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSeparatorHorizontal.Location = new System.Drawing.Point(0, 50);
            this.panelSeparatorHorizontal.Name = "panelSeparatorHorizontal";
            this.panelSeparatorHorizontal.Size = new System.Drawing.Size(315, 1);
            this.panelSeparatorHorizontal.TabIndex = 12;
            // 
            // panelHlavickaFilter
            // 
            this.panelHlavickaFilter.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelHlavickaFilter.Controls.Add(this.comboBoxTypHlavicky);
            this.panelHlavickaFilter.Controls.Add(this.checkBoxilltruj);
            this.panelHlavickaFilter.Controls.Add(this.buttonRoll);
            this.panelHlavickaFilter.Controls.Add(this.label2);
            this.panelHlavickaFilter.Controls.Add(this.label1);
            this.panelHlavickaFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHlavickaFilter.Location = new System.Drawing.Point(0, 0);
            this.panelHlavickaFilter.Name = "panelHlavickaFilter";
            this.panelHlavickaFilter.Size = new System.Drawing.Size(315, 50);
            this.panelHlavickaFilter.TabIndex = 5;
            // 
            // comboBoxTypHlavicky
            // 
            this.comboBoxTypHlavicky.Enabled = false;
            this.comboBoxTypHlavicky.FormattingEnabled = true;
            this.comboBoxTypHlavicky.Location = new System.Drawing.Point(3, 92);
            this.comboBoxTypHlavicky.Name = "comboBoxTypHlavicky";
            this.comboBoxTypHlavicky.Size = new System.Drawing.Size(305, 21);
            this.comboBoxTypHlavicky.TabIndex = 6;
            this.comboBoxTypHlavicky.SelectedIndexChanged += new System.EventHandler(this.comboBoxTypHlavicky_SelectedIndexChanged);
            // 
            // checkBoxilltruj
            // 
            this.checkBoxilltruj.AutoSize = true;
            this.checkBoxilltruj.Location = new System.Drawing.Point(4, 69);
            this.checkBoxilltruj.Name = "checkBoxilltruj";
            this.checkBoxilltruj.Size = new System.Drawing.Size(115, 17);
            this.checkBoxilltruj.TabIndex = 5;
            this.checkBoxilltruj.Text = "Filtruj podľa typu";
            this.checkBoxilltruj.UseVisualStyleBackColor = true;
            this.checkBoxilltruj.CheckedChanged += new System.EventHandler(this.checkBoxilltruj_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(229, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Výber dostupného hlavičkového xls súboru";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(5);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label1.Size = new System.Drawing.Size(140, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "VÝBER HLAVIČKY";
            // 
            // panelVerticalSeparator
            // 
            this.panelVerticalSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.panelVerticalSeparator.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelVerticalSeparator.Location = new System.Drawing.Point(315, 44);
            this.panelVerticalSeparator.Name = "panelVerticalSeparator";
            this.panelVerticalSeparator.Size = new System.Drawing.Size(1, 689);
            this.panelVerticalSeparator.TabIndex = 25;
            // 
            // hlavickaControl
            // 
            this.hlavickaControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(250)))));
            this.hlavickaControl.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.hlavickaControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hlavickaControl.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hlavickaControl.Location = new System.Drawing.Point(315, 44);
            this.hlavickaControl.Margin = new System.Windows.Forms.Padding(4);
            this.hlavickaControl.MessageBoardBackground = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.hlavickaControl.MessageBoardForeground = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.hlavickaControl.Name = "hlavickaControl";
            this.hlavickaControl.Size = new System.Drawing.Size(963, 689);
            this.hlavickaControl.TabIndex = 24;
            // 
            // panelHorizontalSeparator
            // 
            this.panelHorizontalSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.panelHorizontalSeparator.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHorizontalSeparator.Location = new System.Drawing.Point(0, 43);
            this.panelHorizontalSeparator.Name = "panelHorizontalSeparator";
            this.panelHorizontalSeparator.Size = new System.Drawing.Size(1278, 1);
            this.panelHorizontalSeparator.TabIndex = 26;
            // 
            // HlavickyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelVerticalSeparator);
            this.Controls.Add(this.hlavickaControl);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelHorizontalSeparator);
            this.Controls.Add(this.toolStripClassifiersSettings);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "HlavickyControl";
            this.Size = new System.Drawing.Size(1278, 733);
            this.toolStripClassifiersSettings.ResumeLayout(false);
            this.toolStripClassifiersSettings.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panelHlavickaFilter.ResumeLayout(false);
            this.panelHlavickaFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripClassifiersSettings;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddNew;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonRemove;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolStripButtonShow;
        private System.Windows.Forms.ListView listViewHlavicky;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private System.Windows.Forms.ImageList imageListHlavicky;
        private System.Windows.Forms.OpenFileDialog openFileDialogHlavicka;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelHlavickaFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Hlavicky.HlavickaControl hlavickaControl;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Panel panelSeparatorHorizontal;
        private System.Windows.Forms.Panel panelVerticalSeparator;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Button buttonRoll;
        private System.Windows.Forms.ComboBox comboBoxTypHlavicky;
        private System.Windows.Forms.CheckBox checkBoxilltruj;
        private System.Windows.Forms.ToolStripButton toolStripButtonCreateDefault;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButtonReload;
        private System.Windows.Forms.Panel panelHorizontalSeparator;
    }
}
