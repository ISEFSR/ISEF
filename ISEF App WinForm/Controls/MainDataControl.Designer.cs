namespace cvti.isef.winformapp.Controls
{
    partial class MainDataControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainDataControl));
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageDashboard = new System.Windows.Forms.TabPage();
            this.dashboardControl = new cvti.isef.winformapp.Controls.Main.DashboardControl();
            this.tabPageCiselniky = new System.Windows.Forms.TabPage();
            this.ciselnikyControl = new cvti.isef.winformapp.Controls.Main.CiselnikyControl();
            this.tabPageHlavicky = new System.Windows.Forms.TabPage();
            this.hlavickyControl = new cvti.isef.winformapp.Controls.Main.HlavickyControl();
            this.tabPageZostavy = new System.Windows.Forms.TabPage();
            this.zostavyControl = new cvti.isef.winformapp.Controls.Main.ZostavyControl();
            this.tabPageData = new System.Windows.Forms.TabPage();
            this.dataPreviewControl = new cvti.isef.winformapp.Controls.Main.DataPreviewControl();
            this.tabPageGenerator = new System.Windows.Forms.TabPage();
            this.generatorControl = new cvti.isef.winformapp.Controls.Main.GeneratorControl();
            this.imageListTabControl = new System.Windows.Forms.ImageList(this.components);
            this.panelTitle = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelInfo = new System.Windows.Forms.Label();
            this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
            this.menuStripMainMenu = new System.Windows.Forms.MenuStrip();
            this.iSEFApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.nastaveniaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.odstránenieDátToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nápovedaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.oAplikáciiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlMain.SuspendLayout();
            this.tabPageDashboard.SuspendLayout();
            this.tabPageCiselniky.SuspendLayout();
            this.tabPageHlavicky.SuspendLayout();
            this.tabPageZostavy.SuspendLayout();
            this.tabPageData.SuspendLayout();
            this.tabPageGenerator.SuspendLayout();
            this.panelTitle.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
            this.menuStripMainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageDashboard);
            this.tabControlMain.Controls.Add(this.tabPageCiselniky);
            this.tabControlMain.Controls.Add(this.tabPageHlavicky);
            this.tabControlMain.Controls.Add(this.tabPageZostavy);
            this.tabControlMain.Controls.Add(this.tabPageData);
            this.tabControlMain.Controls.Add(this.tabPageGenerator);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.ImageList = this.imageListTabControl;
            this.tabControlMain.ItemSize = new System.Drawing.Size(150, 50);
            this.tabControlMain.Location = new System.Drawing.Point(0, 174);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(1237, 632);
            this.tabControlMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControlMain.TabIndex = 0;
            this.tabControlMain.SelectedIndexChanged += new System.EventHandler(this.tabControlMain_SelectedIndexChanged);
            // 
            // tabPageDashboard
            // 
            this.tabPageDashboard.Controls.Add(this.dashboardControl);
            this.tabPageDashboard.ImageKey = "dashboard_dark_100.png";
            this.tabPageDashboard.Location = new System.Drawing.Point(4, 54);
            this.tabPageDashboard.Name = "tabPageDashboard";
            this.tabPageDashboard.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDashboard.Size = new System.Drawing.Size(1229, 574);
            this.tabPageDashboard.TabIndex = 0;
            this.tabPageDashboard.Text = "DASHBOARD";
            this.tabPageDashboard.ToolTipText = "Dashboard aplikácie, zobrazujúci";
            this.tabPageDashboard.UseVisualStyleBackColor = true;
            // 
            // dashboardControl
            // 
            this.dashboardControl.BackColor = System.Drawing.Color.White;
            this.dashboardControl.BackImage = null;
            this.dashboardControl.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.dashboardControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dashboardControl.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dashboardControl.InfoText = global::cvti.isef.winformapp.Properties.Resources.DashboardInfo;
            this.dashboardControl.Location = new System.Drawing.Point(3, 3);
            this.dashboardControl.Name = "dashboardControl";
            this.dashboardControl.Size = new System.Drawing.Size(1223, 568);
            this.dashboardControl.TabIndex = 0;
            this.dashboardControl.TitleImage = global::cvti.isef.winformapp.Properties.Resources.report_white_100;
            this.dashboardControl.TitleText = "DASHBOARD";
            this.dashboardControl.TransferCreationClicked += new System.EventHandler(this.dashboardControl_TransferCreationClicked);
            this.dashboardControl.ShowDataForStupen += new System.EventHandler<System.Tuple<cvti.data.Enums.InputType, int>>(this.dashboardControl_ShowDataForStupen);
            // 
            // tabPageCiselniky
            // 
            this.tabPageCiselniky.Controls.Add(this.ciselnikyControl);
            this.tabPageCiselniky.ImageKey = "lookup_dark_100.png";
            this.tabPageCiselniky.Location = new System.Drawing.Point(4, 54);
            this.tabPageCiselniky.Name = "tabPageCiselniky";
            this.tabPageCiselniky.Size = new System.Drawing.Size(1229, 574);
            this.tabPageCiselniky.TabIndex = 2;
            this.tabPageCiselniky.Text = "ČÍSELNÍKY";
            this.tabPageCiselniky.ToolTipText = "Číselniky aplikácie s možnosťou...";
            this.tabPageCiselniky.UseVisualStyleBackColor = true;
            // 
            // ciselnikyControl
            // 
            this.ciselnikyControl.BackImage = null;
            this.ciselnikyControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ciselnikyControl.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ciselnikyControl.InfoText = global::cvti.isef.winformapp.Properties.Resources.CiselnokyInfo;
            this.ciselnikyControl.Location = new System.Drawing.Point(0, 0);
            this.ciselnikyControl.Name = "ciselnikyControl";
            this.ciselnikyControl.Size = new System.Drawing.Size(1229, 574);
            this.ciselnikyControl.TabIndex = 0;
            this.ciselnikyControl.TitleImage = global::cvti.isef.winformapp.Properties.Resources.lookup_white_100;
            this.ciselnikyControl.TitleText = "ČÍSELNÍKY";
            // 
            // tabPageHlavicky
            // 
            this.tabPageHlavicky.Controls.Add(this.hlavickyControl);
            this.tabPageHlavicky.ImageKey = "excel_dark_100.png";
            this.tabPageHlavicky.Location = new System.Drawing.Point(4, 54);
            this.tabPageHlavicky.Name = "tabPageHlavicky";
            this.tabPageHlavicky.Size = new System.Drawing.Size(1229, 574);
            this.tabPageHlavicky.TabIndex = 5;
            this.tabPageHlavicky.Text = "HLAVIČKY";
            this.tabPageHlavicky.UseVisualStyleBackColor = true;
            // 
            // hlavickyControl
            // 
            this.hlavickyControl.BackColor = System.Drawing.Color.White;
            this.hlavickyControl.BackImage = null;
            this.hlavickyControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hlavickyControl.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hlavickyControl.InfoText = global::cvti.isef.winformapp.Properties.Resources.HlavickyInfp;
            this.hlavickyControl.Location = new System.Drawing.Point(0, 0);
            this.hlavickyControl.Name = "hlavickyControl";
            this.hlavickyControl.Size = new System.Drawing.Size(1229, 574);
            this.hlavickyControl.TabIndex = 0;
            this.hlavickyControl.TitleImage = global::cvti.isef.winformapp.Properties.Resources.excel_white_100;
            this.hlavickyControl.TitleText = "HLAVIČKY";
            // 
            // tabPageZostavy
            // 
            this.tabPageZostavy.Controls.Add(this.zostavyControl);
            this.tabPageZostavy.ImageKey = "report_dark_100.png";
            this.tabPageZostavy.Location = new System.Drawing.Point(4, 54);
            this.tabPageZostavy.Name = "tabPageZostavy";
            this.tabPageZostavy.Size = new System.Drawing.Size(1229, 574);
            this.tabPageZostavy.TabIndex = 1;
            this.tabPageZostavy.Text = "ZOSTAVY";
            this.tabPageZostavy.ToolTipText = "Preddefinované zostavy aplikácie...";
            this.tabPageZostavy.UseVisualStyleBackColor = true;
            // 
            // zostavyControl
            // 
            this.zostavyControl.BackColor = System.Drawing.SystemColors.Control;
            this.zostavyControl.BackImage = null;
            this.zostavyControl.Cursor = System.Windows.Forms.Cursors.Default;
            this.zostavyControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zostavyControl.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.zostavyControl.InfoText = global::cvti.isef.winformapp.Properties.Resources.ZostavyInfo;
            this.zostavyControl.Location = new System.Drawing.Point(0, 0);
            this.zostavyControl.MessageBoardBackground = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.zostavyControl.MessageBoardForeground = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.zostavyControl.Name = "zostavyControl";
            this.zostavyControl.Size = new System.Drawing.Size(1229, 574);
            this.zostavyControl.TabIndex = 0;
            this.zostavyControl.TitleImage = global::cvti.isef.winformapp.Properties.Resources.report_white_100;
            this.zostavyControl.TitleText = "ZOSTAVY";
            this.zostavyControl.Load += new System.EventHandler(this.zostavyControl_Load);
            // 
            // tabPageData
            // 
            this.tabPageData.Controls.Add(this.dataPreviewControl);
            this.tabPageData.ImageKey = "data_dark_100.png";
            this.tabPageData.Location = new System.Drawing.Point(4, 54);
            this.tabPageData.Name = "tabPageData";
            this.tabPageData.Size = new System.Drawing.Size(1229, 574);
            this.tabPageData.TabIndex = 3;
            this.tabPageData.Text = "PREHĽAD";
            this.tabPageData.UseVisualStyleBackColor = true;
            // 
            // dataPreviewControl
            // 
            this.dataPreviewControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(250)))));
            this.dataPreviewControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.dataPreviewControl.BackImage = null;
            this.dataPreviewControl.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.dataPreviewControl.DataManager = null;
            this.dataPreviewControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataPreviewControl.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dataPreviewControl.InfoText = global::cvti.isef.winformapp.Properties.Resources.DataInfo;
            this.dataPreviewControl.Location = new System.Drawing.Point(0, 0);
            this.dataPreviewControl.Margin = new System.Windows.Forms.Padding(4);
            this.dataPreviewControl.MessageBoardBackground = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dataPreviewControl.MessageBoardForeground = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.dataPreviewControl.Name = "dataPreviewControl";
            this.dataPreviewControl.Size = new System.Drawing.Size(1229, 574);
            this.dataPreviewControl.TabIndex = 0;
            this.dataPreviewControl.TitleImage = global::cvti.isef.winformapp.Properties.Resources.data_white_100;
            this.dataPreviewControl.TitleText = "DATA";
            // 
            // tabPageGenerator
            // 
            this.tabPageGenerator.Controls.Add(this.generatorControl);
            this.tabPageGenerator.ImageKey = "sql_dark_100.png";
            this.tabPageGenerator.Location = new System.Drawing.Point(4, 54);
            this.tabPageGenerator.Name = "tabPageGenerator";
            this.tabPageGenerator.Size = new System.Drawing.Size(1229, 574);
            this.tabPageGenerator.TabIndex = 4;
            this.tabPageGenerator.Text = "GENERATOR";
            this.tabPageGenerator.UseVisualStyleBackColor = true;
            // 
            // generatorControl
            // 
            this.generatorControl.BackImage = null;
            this.generatorControl.DataManager = null;
            this.generatorControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.generatorControl.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.generatorControl.InfoText = global::cvti.isef.winformapp.Properties.Resources.GeneratorInfo;
            this.generatorControl.Location = new System.Drawing.Point(0, 0);
            this.generatorControl.Name = "generatorControl";
            this.generatorControl.Size = new System.Drawing.Size(1229, 574);
            this.generatorControl.TabIndex = 0;
            this.generatorControl.TitleImage = global::cvti.isef.winformapp.Properties.Resources.sql_white_100;
            this.generatorControl.TitleText = "GENERATOR";
            // 
            // imageListTabControl
            // 
            this.imageListTabControl.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTabControl.ImageStream")));
            this.imageListTabControl.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTabControl.Images.SetKeyName(0, "placeholder80.png");
            this.imageListTabControl.Images.SetKeyName(1, "excel_dark_100.png");
            this.imageListTabControl.Images.SetKeyName(2, "report_dark_100.png");
            this.imageListTabControl.Images.SetKeyName(3, "sql_dark_100.png");
            this.imageListTabControl.Images.SetKeyName(4, "dashboard_dark_100.png");
            this.imageListTabControl.Images.SetKeyName(5, "data_dark_100.png");
            this.imageListTabControl.Images.SetKeyName(6, "lookup_dark_100.png");
            // 
            // panelTitle
            // 
            this.panelTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(89)))), ((int)(((byte)(127)))));
            this.panelTitle.Controls.Add(this.tableLayoutPanel1);
            this.panelTitle.Controls.Add(this.pictureBoxIcon);
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitle.Location = new System.Drawing.Point(0, 24);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Size = new System.Drawing.Size(1237, 150);
            this.panelTitle.TabIndex = 1;
            this.panelTitle.Paint += new System.Windows.Forms.PaintEventHandler(this.panelTitle_Paint);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.labelTitle, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelInfo, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(100, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(538, 150);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.BackColor = System.Drawing.Color.Transparent;
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.labelTitle.Location = new System.Drawing.Point(10, 10);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(528, 45);
            this.labelTitle.TabIndex = 5;
            this.labelTitle.Text = "DASHBOARD";
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.BackColor = System.Drawing.Color.Transparent;
            this.labelInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelInfo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.labelInfo.Location = new System.Drawing.Point(10, 55);
            this.labelInfo.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.labelInfo.Size = new System.Drawing.Size(528, 95);
            this.labelInfo.TabIndex = 6;
            this.labelInfo.Text = "Dashboard aplikácie ISEF, obsahujúci sumárne informácie o nahatých údajoch, a pre" +
    "hľad aktivity používateľov.";
            // 
            // pictureBoxIcon
            // 
            this.pictureBoxIcon.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxIcon.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBoxIcon.Image = global::cvti.isef.winformapp.Properties.Resources.dashboard_white_100;
            this.pictureBoxIcon.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxIcon.Name = "pictureBoxIcon";
            this.pictureBoxIcon.Size = new System.Drawing.Size(100, 150);
            this.pictureBoxIcon.TabIndex = 7;
            this.pictureBoxIcon.TabStop = false;
            // 
            // menuStripMainMenu
            // 
            this.menuStripMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iSEFApplicationToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStripMainMenu.Location = new System.Drawing.Point(0, 0);
            this.menuStripMainMenu.Name = "menuStripMainMenu";
            this.menuStripMainMenu.Size = new System.Drawing.Size(1237, 24);
            this.menuStripMainMenu.TabIndex = 2;
            this.menuStripMainMenu.Text = "menuStrip1";
            // 
            // iSEFApplicationToolStripMenuItem
            // 
            this.iSEFApplicationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.toolStripSeparator3,
            this.nastaveniaToolStripMenuItem,
            this.refreshToolStripMenuItem,
            this.odstránenieDátToolStripMenuItem,
            this.toolStripSeparator1,
            this.quitToolStripMenuItem});
            this.iSEFApplicationToolStripMenuItem.Name = "iSEFApplicationToolStripMenuItem";
            this.iSEFApplicationToolStripMenuItem.Size = new System.Drawing.Size(103, 20);
            this.iSEFApplicationToolStripMenuItem.Text = "&Spracovanie dát";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Enabled = false;
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.importToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.importToolStripMenuItem.Text = "&Import...";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Enabled = false;
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.exportToolStripMenuItem.Text = "&Export...";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(178, 6);
            // 
            // nastaveniaToolStripMenuItem
            // 
            this.nastaveniaToolStripMenuItem.Enabled = false;
            this.nastaveniaToolStripMenuItem.Name = "nastaveniaToolStripMenuItem";
            this.nastaveniaToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.nastaveniaToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.nastaveniaToolStripMenuItem.Text = "&Nastavenia...";
            this.nastaveniaToolStripMenuItem.Click += new System.EventHandler(this.nastaveniaToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.refreshToolStripMenuItem.Text = "&Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // odstránenieDátToolStripMenuItem
            // 
            this.odstránenieDátToolStripMenuItem.Name = "odstránenieDátToolStripMenuItem";
            this.odstránenieDátToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.odstránenieDátToolStripMenuItem.Text = "Odstránenie dát...";
            this.odstránenieDátToolStripMenuItem.Click += new System.EventHandler(this.odstránenieDátToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(178, 6);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nápovedaToolStripMenuItem,
            this.toolStripSeparator2,
            this.restartToolStripMenuItem,
            this.toolStripSeparator4,
            this.oAplikáciiToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.helpToolStripMenuItem.Text = "&Pomoc";
            // 
            // nápovedaToolStripMenuItem
            // 
            this.nápovedaToolStripMenuItem.Name = "nápovedaToolStripMenuItem";
            this.nápovedaToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.nápovedaToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.nápovedaToolStripMenuItem.Text = "&Nápoveda";
            this.nápovedaToolStripMenuItem.Click += new System.EventHandler(this.nápovedaToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(168, 6);
            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.restartToolStripMenuItem.Text = "&Restart";
            this.restartToolStripMenuItem.Click += new System.EventHandler(this.restartToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(168, 6);
            // 
            // oAplikáciiToolStripMenuItem
            // 
            this.oAplikáciiToolStripMenuItem.Name = "oAplikáciiToolStripMenuItem";
            this.oAplikáciiToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.oAplikáciiToolStripMenuItem.Text = "&O Aplikácii";
            this.oAplikáciiToolStripMenuItem.Click += new System.EventHandler(this.oAplikáciiToolStripMenuItem_Click);
            // 
            // MainDataControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.panelTitle);
            this.Controls.Add(this.menuStripMainMenu);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "MainDataControl";
            this.Size = new System.Drawing.Size(1237, 806);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageDashboard.ResumeLayout(false);
            this.tabPageCiselniky.ResumeLayout(false);
            this.tabPageHlavicky.ResumeLayout(false);
            this.tabPageZostavy.ResumeLayout(false);
            this.tabPageData.ResumeLayout(false);
            this.tabPageGenerator.ResumeLayout(false);
            this.panelTitle.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
            this.menuStripMainMenu.ResumeLayout(false);
            this.menuStripMainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageDashboard;
        private Controls.Main.DashboardControl dashboardControl;
        private System.Windows.Forms.TabPage tabPageZostavy;
        private Controls.Main.ZostavyControl zostavyControl;
        private System.Windows.Forms.TabPage tabPageCiselniky;
        private System.Windows.Forms.ImageList imageListTabControl;
        private System.Windows.Forms.Panel panelTitle;
        private Controls.Main.CiselnikyControl ciselnikyControl;
        private System.Windows.Forms.PictureBox pictureBoxIcon;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.TabPage tabPageData;
        private Controls.Main.DataPreviewControl dataPreviewControl;
        private System.Windows.Forms.TabPage tabPageGenerator;
        private Main.GeneratorControl generatorControl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabPage tabPageHlavicky;
        private Main.HlavickyControl hlavickyControl;
        private System.Windows.Forms.MenuStrip menuStripMainMenu;
        private System.Windows.Forms.ToolStripMenuItem iSEFApplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem nastaveniaToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nápovedaToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem oAplikáciiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem odstránenieDátToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    }
}
