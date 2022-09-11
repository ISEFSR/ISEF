namespace cvti.isef.winformapp.Controls.Main.Generator
{
    partial class CommandsControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommandsControl));
            this.panelAvailableCommands = new System.Windows.Forms.Panel();
            this.listViewCommands = new System.Windows.Forms.ListView();
            this.columnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageListListView = new System.Windows.Forms.ImageList(this.components);
            this.panelHorizontalSeparator = new System.Windows.Forms.Panel();
            this.panelTitle = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonRoll = new System.Windows.Forms.Button();
            this.labelView = new System.Windows.Forms.Label();
            this.trackBarListView = new System.Windows.Forms.TrackBar();
            this.toolStripConditionList = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAddCommand = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonClone = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonRemoveCommand = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonExport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonImport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonGenerateDefault = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.openFileDialogImportFile = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialogExport = new System.Windows.Forms.FolderBrowserDialog();
            this.panelVerticalSeparator = new System.Windows.Forms.Panel();
            this.selectedCommandControl = new cvti.isef.winformapp.Controls.Main.Generator.Commands.SelectedCommandControl();
            this.panelSeparator = new System.Windows.Forms.Panel();
            this.panelAvailableCommands.SuspendLayout();
            this.panelTitle.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarListView)).BeginInit();
            this.toolStripConditionList.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelAvailableCommands
            // 
            this.panelAvailableCommands.Controls.Add(this.listViewCommands);
            this.panelAvailableCommands.Controls.Add(this.panelHorizontalSeparator);
            this.panelAvailableCommands.Controls.Add(this.panelTitle);
            this.panelAvailableCommands.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelAvailableCommands.Location = new System.Drawing.Point(0, 44);
            this.panelAvailableCommands.Name = "panelAvailableCommands";
            this.panelAvailableCommands.Size = new System.Drawing.Size(306, 670);
            this.panelAvailableCommands.TabIndex = 1;
            // 
            // listViewCommands
            // 
            this.listViewCommands.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewCommands.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnName});
            this.listViewCommands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewCommands.HideSelection = false;
            this.listViewCommands.LargeImageList = this.imageListListView;
            this.listViewCommands.Location = new System.Drawing.Point(0, 52);
            this.listViewCommands.Name = "listViewCommands";
            this.listViewCommands.Size = new System.Drawing.Size(306, 618);
            this.listViewCommands.SmallImageList = this.imageListListView;
            this.listViewCommands.TabIndex = 5;
            this.listViewCommands.UseCompatibleStateImageBehavior = false;
            this.listViewCommands.SelectedIndexChanged += new System.EventHandler(this.listBoxCommands_SelectedIndexChanged);
            this.listViewCommands.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBoxCommands_KeyDown);
            // 
            // columnName
            // 
            this.columnName.Text = "Názov";
            this.columnName.Width = 260;
            // 
            // imageListListView
            // 
            this.imageListListView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListListView.ImageStream")));
            this.imageListListView.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListListView.Images.SetKeyName(0, "sql50.png");
            // 
            // panelHorizontalSeparator
            // 
            this.panelHorizontalSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.panelHorizontalSeparator.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHorizontalSeparator.Location = new System.Drawing.Point(0, 51);
            this.panelHorizontalSeparator.Name = "panelHorizontalSeparator";
            this.panelHorizontalSeparator.Size = new System.Drawing.Size(306, 1);
            this.panelHorizontalSeparator.TabIndex = 6;
            // 
            // panelTitle
            // 
            this.panelTitle.Controls.Add(this.tableLayoutPanel1);
            this.panelTitle.Controls.Add(this.buttonRoll);
            this.panelTitle.Controls.Add(this.labelView);
            this.panelTitle.Controls.Add(this.trackBarListView);
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitle.Location = new System.Drawing.Point(0, 0);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Size = new System.Drawing.Size(306, 51);
            this.panelTitle.TabIndex = 4;
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(265, 51);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(89)))), ((int)(((byte)(127)))));
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "Výber príkazu";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(89)))), ((int)(((byte)(127)))));
            this.label2.Location = new System.Drawing.Point(3, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(208, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Výber dostupného SQL SELECT príkazu ";
            // 
            // buttonRoll
            // 
            this.buttonRoll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRoll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonRoll.FlatAppearance.BorderSize = 0;
            this.buttonRoll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRoll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(89)))), ((int)(((byte)(127)))));
            this.buttonRoll.Location = new System.Drawing.Point(281, 3);
            this.buttonRoll.Name = "buttonRoll";
            this.buttonRoll.Size = new System.Drawing.Size(23, 23);
            this.buttonRoll.TabIndex = 6;
            this.buttonRoll.Text = "v";
            this.toolTipInfo.SetToolTip(this.buttonRoll, "Zobraz filtre pre príkazy");
            this.buttonRoll.UseVisualStyleBackColor = true;
            this.buttonRoll.Click += new System.EventHandler(this.buttonRoll_Click);
            // 
            // labelView
            // 
            this.labelView.AutoSize = true;
            this.labelView.Location = new System.Drawing.Point(4, 69);
            this.labelView.Name = "labelView";
            this.labelView.Size = new System.Drawing.Size(57, 13);
            this.labelView.TabIndex = 5;
            this.labelView.Text = "LargeIcon";
            // 
            // trackBarListView
            // 
            this.trackBarListView.BackColor = System.Drawing.Color.White;
            this.trackBarListView.LargeChange = 1;
            this.trackBarListView.Location = new System.Drawing.Point(2, 85);
            this.trackBarListView.Maximum = 4;
            this.trackBarListView.Name = "trackBarListView";
            this.trackBarListView.Size = new System.Drawing.Size(298, 45);
            this.trackBarListView.TabIndex = 4;
            this.trackBarListView.ValueChanged += new System.EventHandler(this.trackBarListView_ValueChanged);
            // 
            // toolStripConditionList
            // 
            this.toolStripConditionList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAddCommand,
            this.toolStripSeparator1,
            this.toolStripButtonClone,
            this.toolStripSeparator4,
            this.toolStripButtonRemoveCommand,
            this.toolStripSeparator3,
            this.toolStripButtonExport,
            this.toolStripSeparator2,
            this.toolStripButtonImport,
            this.toolStripSeparator5,
            this.toolStripButtonGenerateDefault,
            this.toolStripSeparator6});
            this.toolStripConditionList.Location = new System.Drawing.Point(0, 0);
            this.toolStripConditionList.Name = "toolStripConditionList";
            this.toolStripConditionList.Size = new System.Drawing.Size(1166, 43);
            this.toolStripConditionList.TabIndex = 2;
            this.toolStripConditionList.Text = "toolStrip1";
            // 
            // toolStripButtonAddCommand
            // 
            this.toolStripButtonAddCommand.AutoSize = false;
            this.toolStripButtonAddCommand.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButtonAddCommand.Image = global::cvti.isef.winformapp.Properties.Resources.add;
            this.toolStripButtonAddCommand.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddCommand.Name = "toolStripButtonAddCommand";
            this.toolStripButtonAddCommand.Size = new System.Drawing.Size(140, 40);
            this.toolStripButtonAddCommand.Text = "Pridaj nový";
            this.toolStripButtonAddCommand.ToolTipText = "Umožňuje vytvoriť nový SELECT príkaz";
            this.toolStripButtonAddCommand.Click += new System.EventHandler(this.toolStripButtonAddCommand_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 43);
            // 
            // toolStripButtonClone
            // 
            this.toolStripButtonClone.AutoSize = false;
            this.toolStripButtonClone.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButtonClone.Image = global::cvti.isef.winformapp.Properties.Resources.clone32;
            this.toolStripButtonClone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonClone.Name = "toolStripButtonClone";
            this.toolStripButtonClone.Size = new System.Drawing.Size(140, 40);
            this.toolStripButtonClone.Text = "Naklonuj vybraný";
            this.toolStripButtonClone.ToolTipText = "Naklonuje vybraný SELECT príkaz";
            this.toolStripButtonClone.Click += new System.EventHandler(this.toolStripButtonClone_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 43);
            // 
            // toolStripButtonRemoveCommand
            // 
            this.toolStripButtonRemoveCommand.AutoSize = false;
            this.toolStripButtonRemoveCommand.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButtonRemoveCommand.Image = global::cvti.isef.winformapp.Properties.Resources.remove;
            this.toolStripButtonRemoveCommand.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRemoveCommand.Name = "toolStripButtonRemoveCommand";
            this.toolStripButtonRemoveCommand.Size = new System.Drawing.Size(140, 40);
            this.toolStripButtonRemoveCommand.Text = "Odstráň vybraný";
            this.toolStripButtonRemoveCommand.ToolTipText = "Odstráni vybraný SELECT príkaz";
            this.toolStripButtonRemoveCommand.Click += new System.EventHandler(this.toolStripButtonRemoveCommand_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 43);
            // 
            // toolStripButtonExport
            // 
            this.toolStripButtonExport.AutoSize = false;
            this.toolStripButtonExport.Image = global::cvti.isef.winformapp.Properties.Resources.download;
            this.toolStripButtonExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExport.Name = "toolStripButtonExport";
            this.toolStripButtonExport.Size = new System.Drawing.Size(140, 40);
            this.toolStripButtonExport.Text = "Exportuje príkazy";
            this.toolStripButtonExport.ToolTipText = "Exportuje príkazy do JSON súboru";
            this.toolStripButtonExport.Click += new System.EventHandler(this.toolStripButtonExport_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 43);
            // 
            // toolStripButtonImport
            // 
            this.toolStripButtonImport.AutoSize = false;
            this.toolStripButtonImport.Image = global::cvti.isef.winformapp.Properties.Resources.import;
            this.toolStripButtonImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonImport.Name = "toolStripButtonImport";
            this.toolStripButtonImport.Size = new System.Drawing.Size(140, 40);
            this.toolStripButtonImport.Text = "Importuj príkazy";
            this.toolStripButtonImport.ToolTipText = "Importuj príkazy z JSON súboru";
            this.toolStripButtonImport.Click += new System.EventHandler(this.toolStripButtonImport_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 43);
            // 
            // toolStripButtonGenerateDefault
            // 
            this.toolStripButtonGenerateDefault.AutoSize = false;
            this.toolStripButtonGenerateDefault.Image = global::cvti.isef.winformapp.Properties.Resources.import;
            this.toolStripButtonGenerateDefault.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonGenerateDefault.Name = "toolStripButtonGenerateDefault";
            this.toolStripButtonGenerateDefault.Size = new System.Drawing.Size(140, 40);
            this.toolStripButtonGenerateDefault.Text = "Generuj defaultné";
            this.toolStripButtonGenerateDefault.ToolTipText = "Vygeneruje defaultné SQL SELECT prikazy";
            this.toolStripButtonGenerateDefault.Click += new System.EventHandler(this.toolStripButtonGenerateDefault_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 43);
            // 
            // openFileDialogImportFile
            // 
            this.openFileDialogImportFile.Filter = "*.json|*.json";
            this.openFileDialogImportFile.Title = "Import príkazov do aplikácie";
            // 
            // folderBrowserDialogExport
            // 
            this.folderBrowserDialogExport.Description = "Vyber exportný adresár";
            this.folderBrowserDialogExport.SelectedPath = "export.json";
            // 
            // panelVerticalSeparator
            // 
            this.panelVerticalSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.panelVerticalSeparator.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelVerticalSeparator.Location = new System.Drawing.Point(306, 44);
            this.panelVerticalSeparator.Name = "panelVerticalSeparator";
            this.panelVerticalSeparator.Size = new System.Drawing.Size(1, 670);
            this.panelVerticalSeparator.TabIndex = 3;
            // 
            // selectedCommandControl
            // 
            this.selectedCommandControl.BackColor = System.Drawing.Color.White;
            this.selectedCommandControl.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.selectedCommandControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectedCommandControl.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.selectedCommandControl.Location = new System.Drawing.Point(307, 44);
            this.selectedCommandControl.Margin = new System.Windows.Forms.Padding(4);
            this.selectedCommandControl.MessageBoardBackground = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.selectedCommandControl.MessageBoardForeground = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.selectedCommandControl.Name = "selectedCommandControl";
            this.selectedCommandControl.Size = new System.Drawing.Size(859, 670);
            this.selectedCommandControl.TabIndex = 2;
            this.selectedCommandControl.CommandSaved += new System.EventHandler(this.selectedCommandControl_CommandSaved);
            this.selectedCommandControl.CommandCreated += new System.EventHandler(this.selectedCommandControl_CommandCreated);
            // 
            // panelSeparator
            // 
            this.panelSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.panelSeparator.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSeparator.Location = new System.Drawing.Point(0, 43);
            this.panelSeparator.Name = "panelSeparator";
            this.panelSeparator.Size = new System.Drawing.Size(1166, 1);
            this.panelSeparator.TabIndex = 29;
            // 
            // CommandsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.selectedCommandControl);
            this.Controls.Add(this.panelVerticalSeparator);
            this.Controls.Add(this.panelAvailableCommands);
            this.Controls.Add(this.panelSeparator);
            this.Controls.Add(this.toolStripConditionList);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "CommandsControl";
            this.Size = new System.Drawing.Size(1166, 714);
            this.Controls.SetChildIndex(this.toolStripConditionList, 0);
            this.Controls.SetChildIndex(this.panelSeparator, 0);
            this.Controls.SetChildIndex(this.panelAvailableCommands, 0);
            this.Controls.SetChildIndex(this.panelVerticalSeparator, 0);
            this.Controls.SetChildIndex(this.selectedCommandControl, 0);
            this.panelAvailableCommands.ResumeLayout(false);
            this.panelTitle.ResumeLayout(false);
            this.panelTitle.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarListView)).EndInit();
            this.toolStripConditionList.ResumeLayout(false);
            this.toolStripConditionList.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelAvailableCommands;
        private System.Windows.Forms.ToolStrip toolStripConditionList;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddCommand;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonRemoveCommand;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private Commands.SelectedCommandControl selectedCommandControl;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonImport;
        private System.Windows.Forms.ToolStripButton toolStripButtonExport;
        private System.Windows.Forms.OpenFileDialog openFileDialogImportFile;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogExport;
        private System.Windows.Forms.ToolStripButton toolStripButtonClone;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelTitle;
        private System.Windows.Forms.Panel panelHorizontalSeparator;
        private System.Windows.Forms.ListView listViewCommands;
        private System.Windows.Forms.Panel panelVerticalSeparator;
        private System.Windows.Forms.ImageList imageListListView;
        private System.Windows.Forms.Label labelView;
        private System.Windows.Forms.TrackBar trackBarListView;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.Button buttonRoll;
        private System.Windows.Forms.Panel panelSeparator;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolStripButtonGenerateDefault;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        //private Ciselniky.CiselnikDocPreviewControl ciselnikDocPreviewControl1;
    }
}
