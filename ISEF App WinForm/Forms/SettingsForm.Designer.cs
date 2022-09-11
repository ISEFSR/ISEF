namespace cvti.isef.winformapp.Forms
{
    partial class SettingsForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelOutputDirectory = new System.Windows.Forms.Label();
            this.linkLabelOutputDirectory = new System.Windows.Forms.LinkLabel();
            this.labelHeadersDirectory = new System.Windows.Forms.Label();
            this.linkLabelHeadersDirectory = new System.Windows.Forms.LinkLabel();
            this.tableLayoutPanelDirectories = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.linkLabelDataDirectory = new System.Windows.Forms.LinkLabel();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBoxFiles = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelfiles = new System.Windows.Forms.TableLayoutPanel();
            this.linkLabelReports = new System.Windows.Forms.LinkLabel();
            this.linkLabelHeaders = new System.Windows.Forms.LinkLabel();
            this.linkLabelCommands = new System.Windows.Forms.LinkLabel();
            this.linkLabelConditions = new System.Windows.Forms.LinkLabel();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBoxDirectories = new System.Windows.Forms.GroupBox();
            this.tabPageServer = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxServerName = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonShowName = new System.Windows.Forms.Button();
            this.buttonShowPass = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBoxAuth = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanelDirectories.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBoxFiles.SuspendLayout();
            this.tableLayoutPanelfiles.SuspendLayout();
            this.groupBoxDirectories.SuspendLayout();
            this.tabPageServer.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControlButtons
            // 
            this.panelControlButtons.Location = new System.Drawing.Point(0, 611);
            this.panelControlButtons.Size = new System.Drawing.Size(634, 50);
            // 
            // labelTitle
            // 
            this.labelTitle.Text = "Nastavenia aplikácie";
            // 
            // labelOutputDirectory
            // 
            this.labelOutputDirectory.AutoSize = true;
            this.labelOutputDirectory.Location = new System.Drawing.Point(3, 5);
            this.labelOutputDirectory.Margin = new System.Windows.Forms.Padding(3, 5, 3, 2);
            this.labelOutputDirectory.Name = "labelOutputDirectory";
            this.labelOutputDirectory.Size = new System.Drawing.Size(156, 13);
            this.labelOutputDirectory.TabIndex = 2;
            this.labelOutputDirectory.Text = "Adresár pre výstupné súbory:";
            // 
            // linkLabelOutputDirectory
            // 
            this.linkLabelOutputDirectory.AutoSize = true;
            this.linkLabelOutputDirectory.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelOutputDirectory.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabelOutputDirectory.LinkColor = System.Drawing.Color.SteelBlue;
            this.linkLabelOutputDirectory.Location = new System.Drawing.Point(3, 25);
            this.linkLabelOutputDirectory.Margin = new System.Windows.Forms.Padding(3, 5, 3, 2);
            this.linkLabelOutputDirectory.Name = "linkLabelOutputDirectory";
            this.linkLabelOutputDirectory.Size = new System.Drawing.Size(57, 13);
            this.linkLabelOutputDirectory.TabIndex = 3;
            this.linkLabelOutputDirectory.TabStop = true;
            this.linkLabelOutputDirectory.Text = "./Output/";
            this.linkLabelOutputDirectory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ll_StartProcess);
            // 
            // labelHeadersDirectory
            // 
            this.labelHeadersDirectory.AutoSize = true;
            this.labelHeadersDirectory.Location = new System.Drawing.Point(3, 45);
            this.labelHeadersDirectory.Margin = new System.Windows.Forms.Padding(3, 5, 3, 2);
            this.labelHeadersDirectory.Name = "labelHeadersDirectory";
            this.labelHeadersDirectory.Size = new System.Drawing.Size(163, 13);
            this.labelHeadersDirectory.TabIndex = 4;
            this.labelHeadersDirectory.Text = "Adresár pre hlavičkové súbory:";
            // 
            // linkLabelHeadersDirectory
            // 
            this.linkLabelHeadersDirectory.AutoSize = true;
            this.linkLabelHeadersDirectory.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelHeadersDirectory.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabelHeadersDirectory.LinkColor = System.Drawing.Color.SteelBlue;
            this.linkLabelHeadersDirectory.Location = new System.Drawing.Point(3, 65);
            this.linkLabelHeadersDirectory.Margin = new System.Windows.Forms.Padding(3, 5, 3, 2);
            this.linkLabelHeadersDirectory.Name = "linkLabelHeadersDirectory";
            this.linkLabelHeadersDirectory.Size = new System.Drawing.Size(63, 13);
            this.linkLabelHeadersDirectory.TabIndex = 5;
            this.linkLabelHeadersDirectory.TabStop = true;
            this.linkLabelHeadersDirectory.Text = "./Hlavicky/";
            this.linkLabelHeadersDirectory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ll_StartProcess);
            // 
            // tableLayoutPanelDirectories
            // 
            this.tableLayoutPanelDirectories.ColumnCount = 1;
            this.tableLayoutPanelDirectories.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelDirectories.Controls.Add(this.labelOutputDirectory, 0, 0);
            this.tableLayoutPanelDirectories.Controls.Add(this.linkLabelOutputDirectory, 0, 1);
            this.tableLayoutPanelDirectories.Controls.Add(this.labelHeadersDirectory, 0, 2);
            this.tableLayoutPanelDirectories.Controls.Add(this.linkLabelHeadersDirectory, 0, 3);
            this.tableLayoutPanelDirectories.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanelDirectories.Controls.Add(this.linkLabelDataDirectory, 0, 5);
            this.tableLayoutPanelDirectories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelDirectories.Location = new System.Drawing.Point(3, 18);
            this.tableLayoutPanelDirectories.Name = "tableLayoutPanelDirectories";
            this.tableLayoutPanelDirectories.RowCount = 10;
            this.tableLayoutPanelDirectories.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelDirectories.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelDirectories.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelDirectories.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelDirectories.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelDirectories.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelDirectories.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelDirectories.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelDirectories.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelDirectories.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelDirectories.Size = new System.Drawing.Size(590, 189);
            this.tableLayoutPanelDirectories.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 85);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 5, 3, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(145, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Adresár pre dátové súbory:";
            // 
            // linkLabelDataDirectory
            // 
            this.linkLabelDataDirectory.AutoSize = true;
            this.linkLabelDataDirectory.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelDataDirectory.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabelDataDirectory.LinkColor = System.Drawing.Color.SteelBlue;
            this.linkLabelDataDirectory.Location = new System.Drawing.Point(3, 105);
            this.linkLabelDataDirectory.Margin = new System.Windows.Forms.Padding(3, 5, 3, 2);
            this.linkLabelDataDirectory.Name = "linkLabelDataDirectory";
            this.linkLabelDataDirectory.Size = new System.Drawing.Size(44, 13);
            this.linkLabelDataDirectory.TabIndex = 7;
            this.linkLabelDataDirectory.TabStop = true;
            this.linkLabelDataDirectory.Text = "./Data/";
            this.linkLabelDataDirectory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ll_StartProcess);
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPage1);
            this.tabControlMain.Controls.Add(this.tabPageServer);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.ItemSize = new System.Drawing.Size(150, 40);
            this.tabControlMain.Location = new System.Drawing.Point(0, 85);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(634, 526);
            this.tabControlMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControlMain.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Controls.Add(this.groupBoxFiles);
            this.tabPage1.Controls.Add(this.groupBoxDirectories);
            this.tabPage1.Location = new System.Drawing.Point(4, 44);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(626, 478);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Všeobecné nastavenia";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBoxFiles
            // 
            this.groupBoxFiles.Controls.Add(this.tableLayoutPanelfiles);
            this.groupBoxFiles.Location = new System.Drawing.Point(15, 243);
            this.groupBoxFiles.Margin = new System.Windows.Forms.Padding(10);
            this.groupBoxFiles.Name = "groupBoxFiles";
            this.groupBoxFiles.Size = new System.Drawing.Size(596, 229);
            this.groupBoxFiles.TabIndex = 8;
            this.groupBoxFiles.TabStop = false;
            this.groupBoxFiles.Text = "Súbory aplikácie";
            // 
            // tableLayoutPanelfiles
            // 
            this.tableLayoutPanelfiles.ColumnCount = 1;
            this.tableLayoutPanelfiles.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelfiles.Controls.Add(this.linkLabelReports, 0, 7);
            this.tableLayoutPanelfiles.Controls.Add(this.linkLabelHeaders, 0, 5);
            this.tableLayoutPanelfiles.Controls.Add(this.linkLabelCommands, 0, 3);
            this.tableLayoutPanelfiles.Controls.Add(this.linkLabelConditions, 0, 1);
            this.tableLayoutPanelfiles.Controls.Add(this.label9, 0, 6);
            this.tableLayoutPanelfiles.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanelfiles.Controls.Add(this.label7, 0, 2);
            this.tableLayoutPanelfiles.Controls.Add(this.label8, 0, 4);
            this.tableLayoutPanelfiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelfiles.Location = new System.Drawing.Point(3, 18);
            this.tableLayoutPanelfiles.Name = "tableLayoutPanelfiles";
            this.tableLayoutPanelfiles.RowCount = 8;
            this.tableLayoutPanelfiles.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelfiles.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelfiles.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelfiles.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelfiles.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelfiles.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelfiles.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelfiles.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelfiles.Size = new System.Drawing.Size(590, 208);
            this.tableLayoutPanelfiles.TabIndex = 0;
            // 
            // linkLabelReports
            // 
            this.linkLabelReports.AutoSize = true;
            this.linkLabelReports.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelReports.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabelReports.LinkColor = System.Drawing.Color.SteelBlue;
            this.linkLabelReports.Location = new System.Drawing.Point(3, 145);
            this.linkLabelReports.Margin = new System.Windows.Forms.Padding(3, 5, 3, 2);
            this.linkLabelReports.Name = "linkLabelReports";
            this.linkLabelReports.Size = new System.Drawing.Size(44, 13);
            this.linkLabelReports.TabIndex = 15;
            this.linkLabelReports.TabStop = true;
            this.linkLabelReports.Text = "./Data/";
            this.linkLabelReports.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ll_StartProcess);
            // 
            // linkLabelHeaders
            // 
            this.linkLabelHeaders.AutoSize = true;
            this.linkLabelHeaders.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelHeaders.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabelHeaders.LinkColor = System.Drawing.Color.SteelBlue;
            this.linkLabelHeaders.Location = new System.Drawing.Point(3, 105);
            this.linkLabelHeaders.Margin = new System.Windows.Forms.Padding(3, 5, 3, 2);
            this.linkLabelHeaders.Name = "linkLabelHeaders";
            this.linkLabelHeaders.Size = new System.Drawing.Size(44, 13);
            this.linkLabelHeaders.TabIndex = 14;
            this.linkLabelHeaders.TabStop = true;
            this.linkLabelHeaders.Text = "./Data/";
            this.linkLabelHeaders.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ll_StartProcess);
            // 
            // linkLabelCommands
            // 
            this.linkLabelCommands.AutoSize = true;
            this.linkLabelCommands.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelCommands.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabelCommands.LinkColor = System.Drawing.Color.SteelBlue;
            this.linkLabelCommands.Location = new System.Drawing.Point(3, 65);
            this.linkLabelCommands.Margin = new System.Windows.Forms.Padding(3, 5, 3, 2);
            this.linkLabelCommands.Name = "linkLabelCommands";
            this.linkLabelCommands.Size = new System.Drawing.Size(44, 13);
            this.linkLabelCommands.TabIndex = 13;
            this.linkLabelCommands.TabStop = true;
            this.linkLabelCommands.Text = "./Data/";
            this.linkLabelCommands.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ll_StartProcess);
            // 
            // linkLabelConditions
            // 
            this.linkLabelConditions.AutoSize = true;
            this.linkLabelConditions.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelConditions.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabelConditions.LinkColor = System.Drawing.Color.SteelBlue;
            this.linkLabelConditions.Location = new System.Drawing.Point(3, 25);
            this.linkLabelConditions.Margin = new System.Windows.Forms.Padding(3, 5, 3, 2);
            this.linkLabelConditions.Name = "linkLabelConditions";
            this.linkLabelConditions.Size = new System.Drawing.Size(44, 13);
            this.linkLabelConditions.TabIndex = 9;
            this.linkLabelConditions.TabStop = true;
            this.linkLabelConditions.Text = "./Data/";
            this.linkLabelConditions.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ll_StartProcess);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 125);
            this.label9.Margin = new System.Windows.Forms.Padding(3, 5, 3, 2);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(109, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "Súbor so zostavami:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 5);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 5, 3, 2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(122, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Súbor s podmienkami:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 45);
            this.label7.Margin = new System.Windows.Forms.Padding(3, 5, 3, 2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Súbo s príkazmi:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 85);
            this.label8.Margin = new System.Windows.Forms.Padding(3, 5, 3, 2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Súbor s hlavičkami:";
            // 
            // groupBoxDirectories
            // 
            this.groupBoxDirectories.Controls.Add(this.tableLayoutPanelDirectories);
            this.groupBoxDirectories.Location = new System.Drawing.Point(15, 13);
            this.groupBoxDirectories.Margin = new System.Windows.Forms.Padding(10);
            this.groupBoxDirectories.Name = "groupBoxDirectories";
            this.groupBoxDirectories.Size = new System.Drawing.Size(596, 210);
            this.groupBoxDirectories.TabIndex = 7;
            this.groupBoxDirectories.TabStop = false;
            this.groupBoxDirectories.Text = "Adresáre aplikácie";
            // 
            // tabPageServer
            // 
            this.tabPageServer.Controls.Add(this.groupBox1);
            this.tabPageServer.Location = new System.Drawing.Point(4, 44);
            this.tabPageServer.Name = "tabPageServer";
            this.tabPageServer.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageServer.Size = new System.Drawing.Size(626, 478);
            this.tabPageServer.TabIndex = 1;
            this.tabPageServer.Text = "MSSQL Server";
            this.tabPageServer.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Location = new System.Drawing.Point(15, 13);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(596, 194);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pripojenie na MSSQL server";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.textBox2, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.comboBox2, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.comboBoxServerName, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.textBox1, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonShowName, 2, 3);
            this.tableLayoutPanel2.Controls.Add(this.buttonShowPass, 2, 4);
            this.tableLayoutPanel2.Controls.Add(this.label10, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.comboBoxAuth, 1, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 18);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(590, 173);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(181, 112);
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '*';
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(285, 22);
            this.textBox2.TabIndex = 7;
            // 
            // comboBox2
            // 
            this.comboBox2.Enabled = false;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(181, 30);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(285, 21);
            this.comboBox2.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Meno databázy:";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Prihlasovacie meno:";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Heslo:";
            // 
            // comboBoxServerName
            // 
            this.comboBoxServerName.Enabled = false;
            this.comboBoxServerName.FormattingEnabled = true;
            this.comboBoxServerName.Location = new System.Drawing.Point(181, 3);
            this.comboBoxServerName.Name = "comboBoxServerName";
            this.comboBoxServerName.Size = new System.Drawing.Size(285, 21);
            this.comboBoxServerName.TabIndex = 4;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(181, 84);
            this.textBox1.Name = "textBox1";
            this.textBox1.PasswordChar = '*';
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(285, 22);
            this.textBox1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 0, 50, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Názov / adresa serveru:";
            // 
            // buttonShowName
            // 
            this.buttonShowName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonShowName.Location = new System.Drawing.Point(472, 84);
            this.buttonShowName.Name = "buttonShowName";
            this.buttonShowName.Size = new System.Drawing.Size(34, 22);
            this.buttonShowName.TabIndex = 8;
            this.buttonShowName.UseVisualStyleBackColor = true;
            this.buttonShowName.Click += new System.EventHandler(this.buttonShowName_Click);
            // 
            // buttonShowPass
            // 
            this.buttonShowPass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonShowPass.Location = new System.Drawing.Point(472, 112);
            this.buttonShowPass.Name = "buttonShowPass";
            this.buttonShowPass.Size = new System.Drawing.Size(34, 22);
            this.buttonShowPass.TabIndex = 9;
            this.buttonShowPass.UseVisualStyleBackColor = true;
            this.buttonShowPass.Click += new System.EventHandler(this.buttonShowPass_Click);
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 61);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(122, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "Spôsob autentifikácie:";
            // 
            // comboBoxAuth
            // 
            this.comboBoxAuth.Enabled = false;
            this.comboBoxAuth.FormattingEnabled = true;
            this.comboBoxAuth.Items.AddRange(new object[] {
            "Windows authentification",
            "Prihlásenie pomocou mena a hesla"});
            this.comboBoxAuth.Location = new System.Drawing.Point(181, 57);
            this.comboBoxAuth.Name = "comboBoxAuth";
            this.comboBoxAuth.Size = new System.Drawing.Size(285, 21);
            this.comboBoxAuth.TabIndex = 11;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(634, 661);
            this.Controls.Add(this.tabControlMain);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.ImageIcon = global::cvti.isef.winformapp.Properties.Resources.settings75;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(650, 700);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(650, 700);
            this.Name = "SettingsForm";
            this.ShowButtonsPanel = true;
            this.ShowCancelButton = true;
            this.ShowImageIcon = true;
            this.ShowOkButton = true;
            this.Text = "ISEF Nastavenia";
            this.TitleText = "Nastavenia aplikácie";
            this.Controls.SetChildIndex(this.panelControlButtons, 0);
            this.Controls.SetChildIndex(this.tabControlMain, 0);
            this.tableLayoutPanelDirectories.ResumeLayout(false);
            this.tableLayoutPanelDirectories.PerformLayout();
            this.tabControlMain.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBoxFiles.ResumeLayout(false);
            this.tableLayoutPanelfiles.ResumeLayout(false);
            this.tableLayoutPanelfiles.PerformLayout();
            this.groupBoxDirectories.ResumeLayout(false);
            this.tabPageServer.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelOutputDirectory;
        private System.Windows.Forms.LinkLabel linkLabelOutputDirectory;
        private System.Windows.Forms.Label labelHeadersDirectory;
        private System.Windows.Forms.LinkLabel linkLabelHeadersDirectory;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelDirectories;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPageServer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxServerName;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel linkLabelDataDirectory;
        private System.Windows.Forms.GroupBox groupBoxFiles;
        private System.Windows.Forms.GroupBox groupBoxDirectories;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelfiles;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel linkLabelReports;
        private System.Windows.Forms.LinkLabel linkLabelHeaders;
        private System.Windows.Forms.LinkLabel linkLabelCommands;
        private System.Windows.Forms.LinkLabel linkLabelConditions;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonShowName;
        private System.Windows.Forms.Button buttonShowPass;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBoxAuth;
    }
}