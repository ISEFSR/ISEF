using cvti.isef.winformapp.Controls.Main.Data;

namespace cvti.isef.winformapp.Controls.Main
{
    partial class DataPreviewControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataPreviewControl));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonNewColumn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonReload = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonDataExport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonChooseCommand = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSaveCommand = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonPickCond = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSaveCond = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.dataGridViewData = new System.Windows.Forms.DataGridView();
            this.contextMenuStripGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exportujDátaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.načítajZnovaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.selectCommandControl = new cvti.isef.winformapp.Controls.Main.Data.SelectCommandControl();
            this.panelSeparator = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.scintillaCommand = new ScintillaNET.Scintilla();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.scintillaCondition = new ScintillaNET.Scintilla();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewData)).BeginInit();
            this.contextMenuStripGrid.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.AutoSize = false;
            this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonNewColumn,
            this.toolStripSeparator1,
            this.toolStripButtonReload,
            this.toolStripSeparator2,
            this.toolStripButtonDataExport,
            this.toolStripSeparator4,
            this.toolStripButtonChooseCommand,
            this.toolStripButtonSaveCommand,
            this.toolStripSeparator6,
            this.toolStripButtonPickCond,
            this.toolStripButtonSaveCond,
            this.toolStripButton1,
            this.toolStripSeparator7,
            this.toolStripButton3});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Size = new System.Drawing.Size(1274, 40);
            this.toolStripMenu.TabIndex = 18;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // toolStripButtonNewColumn
            // 
            this.toolStripButtonNewColumn.AutoSize = false;
            this.toolStripButtonNewColumn.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonNewColumn.Image")));
            this.toolStripButtonNewColumn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNewColumn.Name = "toolStripButtonNewColumn";
            this.toolStripButtonNewColumn.Size = new System.Drawing.Size(100, 37);
            this.toolStripButtonNewColumn.Text = "Nový stĺpec";
            this.toolStripButtonNewColumn.ToolTipText = "Pridá nový stĺpec do výberu (na koniec)";
            this.toolStripButtonNewColumn.Click += new System.EventHandler(this.toolStripButtonNewColumn_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 40);
            // 
            // toolStripButtonReload
            // 
            this.toolStripButtonReload.AutoSize = false;
            this.toolStripButtonReload.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonReload.Image")));
            this.toolStripButtonReload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonReload.Name = "toolStripButtonReload";
            this.toolStripButtonReload.Size = new System.Drawing.Size(110, 37);
            this.toolStripButtonReload.Text = "Načítaj znova";
            this.toolStripButtonReload.ToolTipText = "Načítaj znova údaje na základe vybraných stĺpcov a podmienky";
            this.toolStripButtonReload.Click += new System.EventHandler(this.toolStripButtonReload_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 40);
            // 
            // toolStripButtonDataExport
            // 
            this.toolStripButtonDataExport.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDataExport.Image")));
            this.toolStripButtonDataExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDataExport.Name = "toolStripButtonDataExport";
            this.toolStripButtonDataExport.Size = new System.Drawing.Size(101, 37);
            this.toolStripButtonDataExport.Text = "Exportuj data";
            this.toolStripButtonDataExport.Click += new System.EventHandler(this.toolStripButtonDataExport_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 40);
            // 
            // toolStripButtonChooseCommand
            // 
            this.toolStripButtonChooseCommand.Image = global::cvti.isef.winformapp.Properties.Resources.sql501;
            this.toolStripButtonChooseCommand.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonChooseCommand.Name = "toolStripButtonChooseCommand";
            this.toolStripButtonChooseCommand.Size = new System.Drawing.Size(95, 37);
            this.toolStripButtonChooseCommand.Text = "Vyber príkaz";
            this.toolStripButtonChooseCommand.Click += new System.EventHandler(this.toolStripButtonChooseCommand_Click);
            // 
            // toolStripButtonSaveCommand
            // 
            this.toolStripButtonSaveCommand.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSaveCommand.Image")));
            this.toolStripButtonSaveCommand.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSaveCommand.Name = "toolStripButtonSaveCommand";
            this.toolStripButtonSaveCommand.Size = new System.Drawing.Size(88, 37);
            this.toolStripButtonSaveCommand.Text = "Ulož príkaz";
            this.toolStripButtonSaveCommand.Click += new System.EventHandler(this.toolStripButtonSaveCommand_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 40);
            // 
            // toolStripButtonPickCond
            // 
            this.toolStripButtonPickCond.Image = global::cvti.isef.winformapp.Properties.Resources.condition___;
            this.toolStripButtonPickCond.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPickCond.Name = "toolStripButtonPickCond";
            this.toolStripButtonPickCond.Size = new System.Drawing.Size(125, 37);
            this.toolStripButtonPickCond.Text = "Vyber podmienku";
            this.toolStripButtonPickCond.Click += new System.EventHandler(this.toolStripButtonPickCond_Click);
            // 
            // toolStripButtonSaveCond
            // 
            this.toolStripButtonSaveCond.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSaveCond.Image")));
            this.toolStripButtonSaveCond.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSaveCond.Name = "toolStripButtonSaveCond";
            this.toolStripButtonSaveCond.Size = new System.Drawing.Size(118, 37);
            this.toolStripButtonSaveCond.Text = "Ulož podmienku";
            this.toolStripButtonSaveCond.Click += new System.EventHandler(this.toolStripButtonSaveCond_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::cvti.isef.winformapp.Properties.Resources.icons8_delete_file_48;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(137, 37);
            this.toolStripButton1.Text = "Odstráň podmienku";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 40);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = global::cvti.isef.winformapp.Properties.Resources.remove;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(105, 37);
            this.toolStripButton3.Text = "Zruš načítanie";
            this.toolStripButton3.Visible = false;
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // dataGridViewData
            // 
            this.dataGridViewData.AllowUserToAddRows = false;
            this.dataGridViewData.AllowUserToDeleteRows = false;
            this.dataGridViewData.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.dataGridViewData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(250)))));
            this.dataGridViewData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewData.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridViewData.ColumnHeadersHeight = 35;
            this.dataGridViewData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewData.ContextMenuStrip = this.contextMenuStripGrid;
            this.dataGridViewData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewData.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridViewData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.dataGridViewData.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewData.Name = "dataGridViewData";
            this.dataGridViewData.RowHeadersVisible = false;
            this.dataGridViewData.RowHeadersWidth = 51;
            this.dataGridViewData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewData.Size = new System.Drawing.Size(1260, 430);
            this.dataGridViewData.TabIndex = 2;
            this.dataGridViewData.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewData_CellDoubleClick);
            this.dataGridViewData.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridViewObceData_RowPostPaint);
            this.dataGridViewData.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dataGridViewData_MouseMove);
            // 
            // contextMenuStripGrid
            // 
            this.contextMenuStripGrid.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStripGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportujDátaToolStripMenuItem,
            this.načítajZnovaToolStripMenuItem});
            this.contextMenuStripGrid.Name = "contextMenuStripGrid";
            this.contextMenuStripGrid.Size = new System.Drawing.Size(146, 48);
            // 
            // exportujDátaToolStripMenuItem
            // 
            this.exportujDátaToolStripMenuItem.Name = "exportujDátaToolStripMenuItem";
            this.exportujDátaToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.exportujDátaToolStripMenuItem.Text = "Exportuj dáta";
            // 
            // načítajZnovaToolStripMenuItem
            // 
            this.načítajZnovaToolStripMenuItem.Name = "načítajZnovaToolStripMenuItem";
            this.načítajZnovaToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.načítajZnovaToolStripMenuItem.Text = "Načítaj znova";
            // 
            // selectCommandControl
            // 
            this.selectCommandControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.selectCommandControl.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectCommandControl.Location = new System.Drawing.Point(0, 40);
            this.selectCommandControl.Name = "selectCommandControl";
            this.selectCommandControl.SelectedColumn = null;
            this.selectCommandControl.SelectionMode = false;
            this.selectCommandControl.Size = new System.Drawing.Size(1274, 248);
            this.selectCommandControl.TabIndex = 19;
            this.selectCommandControl.EditCondition += new System.EventHandler(this.selectCommandControl_CommandChanged);
            this.selectCommandControl.CommandChanged += new System.EventHandler(this.selectCommandControl_CommandChanged);
            this.selectCommandControl.CommandOrderChanged += new System.EventHandler(this.selectCommandControl_CommandChanged);
            this.selectCommandControl.AliasChanged += new System.EventHandler<cvti.isef.winformapp.Controls.Main.Data.AliasChangedEventArgs>(this.selectCommandControl_AliasChanged);
            // 
            // panelSeparator
            // 
            this.panelSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.panelSeparator.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSeparator.Location = new System.Drawing.Point(0, 288);
            this.panelSeparator.Name = "panelSeparator";
            this.panelSeparator.Size = new System.Drawing.Size(1274, 1);
            this.panelSeparator.TabIndex = 31;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(120, 30);
            this.tabControl1.Location = new System.Drawing.Point(0, 289);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1274, 474);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 33;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridViewData);
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1266, 436);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Prehľad dát";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.scintillaCommand);
            this.tabPage2.Location = new System.Drawing.Point(4, 34);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1266, 436);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "SQL SELECT dotaz";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // scintillaCommand
            // 
            this.scintillaCommand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaCommand.Location = new System.Drawing.Point(3, 3);
            this.scintillaCommand.Name = "scintillaCommand";
            this.scintillaCommand.Size = new System.Drawing.Size(1260, 430);
            this.scintillaCommand.TabIndex = 0;
            this.scintillaCommand.Text = "scintilla1";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.scintillaCondition);
            this.tabPage3.Controls.Add(this.toolStrip1);
            this.tabPage3.Location = new System.Drawing.Point(4, 34);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1266, 436);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "SQL podmienka";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // scintillaCondition
            // 
            this.scintillaCondition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaCondition.Location = new System.Drawing.Point(3, 33);
            this.scintillaCondition.Name = "scintillaCondition";
            this.scintillaCondition.Size = new System.Drawing.Size(1260, 400);
            this.scintillaCondition.TabIndex = 0;
            this.scintillaCondition.Text = "scintilla2";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1260, 30);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(178, 27);
            this.toolStripButton2.Text = "< Odober poslednú podmienku";
            this.toolStripButton2.Click += new System.EventHandler(this.backButton_Click);
            // 
            // DataPreviewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panelSeparator);
            this.Controls.Add(this.selectCommandControl);
            this.Controls.Add(this.toolStripMenu);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DataPreviewControl";
            this.Size = new System.Drawing.Size(1274, 763);
            this.Controls.SetChildIndex(this.toolStripMenu, 0);
            this.Controls.SetChildIndex(this.selectCommandControl, 0);
            this.Controls.SetChildIndex(this.panelSeparator, 0);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewData)).EndInit();
            this.contextMenuStripGrid.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridViewData;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton toolStripButtonNewColumn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonReload;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonDataExport;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripGrid;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripButtonChooseCommand;
        private System.Windows.Forms.ToolStripButton toolStripButtonSaveCommand;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private Data.SelectCommandControl selectCommandControl;
        private System.Windows.Forms.Panel panelSeparator;
        private System.Windows.Forms.ToolStripButton toolStripButtonSaveCond;
        private System.Windows.Forms.ToolStripButton toolStripButtonPickCond;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ToolStripMenuItem exportujDátaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem načítajZnovaToolStripMenuItem;
        private ScintillaNET.Scintilla scintillaCommand;
        private ScintillaNET.Scintilla scintillaCondition;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        //private Data.VyssieUzemneCelkyPreview vyssieUzemneCelkyPreview1;
    }
}
