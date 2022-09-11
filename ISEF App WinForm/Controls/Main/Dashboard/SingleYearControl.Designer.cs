namespace cvti.isef.winformapp.Controls.Main.Dashboard
{
    partial class SingleYearControl
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
            this.labelYear = new System.Windows.Forms.Label();
            this.buttonExpand = new System.Windows.Forms.Button();
            this.labelCreated = new System.Windows.Forms.Label();
            this.pictureBoxInfo = new System.Windows.Forms.PictureBox();
            this.labelLastActualization = new System.Windows.Forms.Label();
            this.labelShort = new System.Windows.Forms.Label();
            this.pictureBoxImageIcon = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.labelTitleVUC = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labelTitleVVS = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.labelSumMao = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelTitleMaO = new System.Windows.Forms.Label();
            this.panelObceColorPanel = new System.Windows.Forms.Panel();
            this.labelCountMao = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.labelTitleMV = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.labelTitleOPRO = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.labelSumVVS = new System.Windows.Forms.Label();
            this.labelCountVVS = new System.Windows.Forms.Label();
            this.labelSumVUC = new System.Windows.Forms.Label();
            this.labelCountVUC = new System.Windows.Forms.Label();
            this.labelSumMV = new System.Windows.Forms.Label();
            this.labeCountMV = new System.Windows.Forms.Label();
            this.labelSumOPRO = new System.Windows.Forms.Label();
            this.labelCountOPRO = new System.Windows.Forms.Label();
            this.panelSeparator = new System.Windows.Forms.Panel();
            this.tableLayoutPanelTitle = new System.Windows.Forms.TableLayoutPanel();
            this.buttonFullScreen = new System.Windows.Forms.Button();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImageIcon)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel9.SuspendLayout();
            this.tableLayoutPanelTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelYear
            // 
            this.labelYear.AutoSize = true;
            this.labelYear.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelYear.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelYear.Location = new System.Drawing.Point(39, 0);
            this.labelYear.Name = "labelYear";
            this.labelYear.Size = new System.Drawing.Size(71, 32);
            this.labelYear.TabIndex = 0;
            this.labelYear.Text = "2020";
            // 
            // buttonExpand
            // 
            this.buttonExpand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExpand.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonExpand.FlatAppearance.BorderSize = 0;
            this.buttonExpand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExpand.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.buttonExpand.Location = new System.Drawing.Point(179, 3);
            this.buttonExpand.Name = "buttonExpand";
            this.buttonExpand.Size = new System.Drawing.Size(20, 20);
            this.buttonExpand.TabIndex = 1;
            this.buttonExpand.Text = "v";
            this.toolTipInfo.SetToolTip(this.buttonExpand, "Rozbalí a zobrazí podrobné informácie k roku");
            this.buttonExpand.UseVisualStyleBackColor = true;
            this.buttonExpand.Click += new System.EventHandler(this.buttonExpand_Click);
            // 
            // labelCreated
            // 
            this.labelCreated.AutoSize = true;
            this.tableLayoutPanelTitle.SetColumnSpan(this.labelCreated, 4);
            this.labelCreated.Cursor = System.Windows.Forms.Cursors.Help;
            this.labelCreated.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.labelCreated.Location = new System.Drawing.Point(3, 48);
            this.labelCreated.Name = "labelCreated";
            this.labelCreated.Size = new System.Drawing.Size(120, 13);
            this.labelCreated.TabIndex = 3;
            this.labelCreated.Text = "dd.MM.YYYY HH:mm:ss";
            // 
            // pictureBoxInfo
            // 
            this.pictureBoxInfo.BackColor = System.Drawing.Color.White;
            this.pictureBoxInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBoxInfo.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxInfo.Name = "pictureBoxInfo";
            this.pictureBoxInfo.Size = new System.Drawing.Size(202, 10);
            this.pictureBoxInfo.TabIndex = 4;
            this.pictureBoxInfo.TabStop = false;
            this.pictureBoxInfo.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxInfo_Paint);
            // 
            // labelLastActualization
            // 
            this.labelLastActualization.AutoSize = true;
            this.tableLayoutPanelTitle.SetColumnSpan(this.labelLastActualization, 4);
            this.labelLastActualization.Location = new System.Drawing.Point(3, 35);
            this.labelLastActualization.Name = "labelLastActualization";
            this.labelLastActualization.Size = new System.Drawing.Size(117, 13);
            this.labelLastActualization.TabIndex = 5;
            this.labelLastActualization.Text = "Posledná aktualizácia";
            // 
            // labelShort
            // 
            this.labelShort.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelShort.Location = new System.Drawing.Point(0, 95);
            this.labelShort.Name = "labelShort";
            this.labelShort.Size = new System.Drawing.Size(202, 100);
            this.labelShort.TabIndex = 6;
            this.labelShort.Text = "Prehľad nahraných údajov pre kalendárny rok {year}.  Zobrazuje počet údajov a cel" +
    "kovú sumu za každý stupeň / nahrané údaje ";
            // 
            // pictureBoxImageIcon
            // 
            this.pictureBoxImageIcon.Image = global::cvti.isef.winformapp.Properties.Resources.calendar40;
            this.pictureBoxImageIcon.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxImageIcon.Name = "pictureBoxImageIcon";
            this.pictureBoxImageIcon.Size = new System.Drawing.Size(30, 29);
            this.pictureBoxImageIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxImageIcon.TabIndex = 6;
            this.pictureBoxImageIcon.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel5, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelSumMao, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelCountMao, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel7, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.panel9, 0, 12);
            this.tableLayoutPanel1.Controls.Add(this.labelSumVVS, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.labelCountVVS, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.labelSumVUC, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.labelCountVUC, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.labelSumMV, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.labeCountMV, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.labelSumOPRO, 0, 14);
            this.tableLayoutPanel1.Controls.Add(this.labelCountOPRO, 0, 13);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 195);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(202, 0);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.labelTitleVUC);
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 126);
            this.panel5.Margin = new System.Windows.Forms.Padding(0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(202, 25);
            this.panel5.TabIndex = 6;
            // 
            // labelTitleVUC
            // 
            this.labelTitleVUC.AutoSize = true;
            this.labelTitleVUC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTitleVUC.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitleVUC.ForeColor = System.Drawing.Color.Salmon;
            this.labelTitleVUC.Location = new System.Drawing.Point(5, 0);
            this.labelTitleVUC.Name = "labelTitleVUC";
            this.labelTitleVUC.Size = new System.Drawing.Size(167, 21);
            this.labelTitleVUC.TabIndex = 1;
            this.labelTitleVUC.Text = "Vyššie územné celky";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Salmon;
            this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(5, 25);
            this.panel6.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.labelTitleVVS);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 63);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(202, 25);
            this.panel3.TabIndex = 3;
            // 
            // labelTitleVVS
            // 
            this.labelTitleVVS.AutoSize = true;
            this.labelTitleVVS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTitleVVS.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitleVVS.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelTitleVVS.Location = new System.Drawing.Point(5, 0);
            this.labelTitleVVS.Name = "labelTitleVVS";
            this.labelTitleVVS.Size = new System.Drawing.Size(169, 21);
            this.labelTitleVVS.TabIndex = 1;
            this.labelTitleVVS.Text = "Verejné vysoké školy";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.RoyalBlue;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(5, 25);
            this.panel4.TabIndex = 0;
            // 
            // labelSumMao
            // 
            this.labelSumMao.AutoSize = true;
            this.labelSumMao.ForeColor = System.Drawing.Color.Gray;
            this.labelSumMao.Location = new System.Drawing.Point(30, 45);
            this.labelSumMao.Margin = new System.Windows.Forms.Padding(30, 5, 3, 5);
            this.labelSumMao.Name = "labelSumMao";
            this.labelSumMao.Size = new System.Drawing.Size(64, 13);
            this.labelSumMao.TabIndex = 2;
            this.labelSumMao.Text = "1.345.654 $";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.labelTitleMaO);
            this.panel2.Controls.Add(this.panelObceColorPanel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(202, 25);
            this.panel2.TabIndex = 0;
            // 
            // labelTitleMaO
            // 
            this.labelTitleMaO.AutoSize = true;
            this.labelTitleMaO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTitleMaO.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitleMaO.ForeColor = System.Drawing.Color.RosyBrown;
            this.labelTitleMaO.Location = new System.Drawing.Point(5, 0);
            this.labelTitleMaO.Name = "labelTitleMaO";
            this.labelTitleMaO.Size = new System.Drawing.Size(110, 21);
            this.labelTitleMaO.TabIndex = 1;
            this.labelTitleMaO.Text = "Mestá a obce";
            // 
            // panelObceColorPanel
            // 
            this.panelObceColorPanel.BackColor = System.Drawing.Color.RosyBrown;
            this.panelObceColorPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelObceColorPanel.Location = new System.Drawing.Point(0, 0);
            this.panelObceColorPanel.Name = "panelObceColorPanel";
            this.panelObceColorPanel.Size = new System.Drawing.Size(5, 25);
            this.panelObceColorPanel.TabIndex = 0;
            // 
            // labelCountMao
            // 
            this.labelCountMao.AutoSize = true;
            this.labelCountMao.ForeColor = System.Drawing.Color.Gray;
            this.labelCountMao.Location = new System.Drawing.Point(30, 27);
            this.labelCountMao.Margin = new System.Windows.Forms.Padding(30, 2, 3, 0);
            this.labelCountMao.Name = "labelCountMao";
            this.labelCountMao.Size = new System.Drawing.Size(93, 13);
            this.labelCountMao.TabIndex = 1;
            this.labelCountMao.Text = "25 325 záznamov";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.labelTitleMV);
            this.panel7.Controls.Add(this.panel8);
            this.panel7.Location = new System.Drawing.Point(0, 189);
            this.panel7.Margin = new System.Windows.Forms.Padding(0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(196, 25);
            this.panel7.TabIndex = 9;
            // 
            // labelTitleMV
            // 
            this.labelTitleMV.AutoSize = true;
            this.labelTitleMV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTitleMV.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitleMV.ForeColor = System.Drawing.Color.SeaGreen;
            this.labelTitleMV.Location = new System.Drawing.Point(5, 0);
            this.labelTitleMV.Name = "labelTitleMV";
            this.labelTitleMV.Size = new System.Drawing.Size(159, 21);
            this.labelTitleMV.TabIndex = 1;
            this.labelTitleMV.Text = "Ministerstvo vnútra";
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.SeaGreen;
            this.panel8.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(5, 25);
            this.panel8.TabIndex = 0;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.labelTitleOPRO);
            this.panel9.Controls.Add(this.panel10);
            this.panel9.Location = new System.Drawing.Point(0, 252);
            this.panel9.Margin = new System.Windows.Forms.Padding(0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(196, 25);
            this.panel9.TabIndex = 10;
            // 
            // labelTitleOPRO
            // 
            this.labelTitleOPRO.AutoSize = true;
            this.labelTitleOPRO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTitleOPRO.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitleOPRO.ForeColor = System.Drawing.Color.SlateBlue;
            this.labelTitleOPRO.Location = new System.Drawing.Point(5, 0);
            this.labelTitleOPRO.Name = "labelTitleOPRO";
            this.labelTitleOPRO.Size = new System.Drawing.Size(161, 21);
            this.labelTitleOPRO.TabIndex = 1;
            this.labelTitleOPRO.Text = "Priamo riadené org.";
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.SlateBlue;
            this.panel10.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel10.Location = new System.Drawing.Point(0, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(5, 25);
            this.panel10.TabIndex = 0;
            // 
            // labelSumVVS
            // 
            this.labelSumVVS.AutoSize = true;
            this.labelSumVVS.ForeColor = System.Drawing.Color.Gray;
            this.labelSumVVS.Location = new System.Drawing.Point(30, 113);
            this.labelSumVVS.Margin = new System.Windows.Forms.Padding(30, 2, 3, 0);
            this.labelSumVVS.Name = "labelSumVVS";
            this.labelSumVVS.Size = new System.Drawing.Size(64, 13);
            this.labelSumVVS.TabIndex = 5;
            this.labelSumVVS.Text = "1.345.654 $";
            // 
            // labelCountVVS
            // 
            this.labelCountVVS.AutoSize = true;
            this.labelCountVVS.ForeColor = System.Drawing.Color.Gray;
            this.labelCountVVS.Location = new System.Drawing.Point(30, 93);
            this.labelCountVVS.Margin = new System.Windows.Forms.Padding(30, 5, 3, 5);
            this.labelCountVVS.Name = "labelCountVVS";
            this.labelCountVVS.Size = new System.Drawing.Size(93, 13);
            this.labelCountVVS.TabIndex = 4;
            this.labelCountVVS.Text = "25 325 záznamov";
            // 
            // labelSumVUC
            // 
            this.labelSumVUC.AutoSize = true;
            this.labelSumVUC.ForeColor = System.Drawing.Color.Gray;
            this.labelSumVUC.Location = new System.Drawing.Point(30, 176);
            this.labelSumVUC.Margin = new System.Windows.Forms.Padding(30, 2, 3, 0);
            this.labelSumVUC.Name = "labelSumVUC";
            this.labelSumVUC.Size = new System.Drawing.Size(64, 13);
            this.labelSumVUC.TabIndex = 8;
            this.labelSumVUC.Text = "1.345.654 $";
            // 
            // labelCountVUC
            // 
            this.labelCountVUC.AutoSize = true;
            this.labelCountVUC.ForeColor = System.Drawing.Color.Gray;
            this.labelCountVUC.Location = new System.Drawing.Point(30, 156);
            this.labelCountVUC.Margin = new System.Windows.Forms.Padding(30, 5, 3, 5);
            this.labelCountVUC.Name = "labelCountVUC";
            this.labelCountVUC.Size = new System.Drawing.Size(93, 13);
            this.labelCountVUC.TabIndex = 7;
            this.labelCountVUC.Text = "25 325 záznamov";
            // 
            // labelSumMV
            // 
            this.labelSumMV.AutoSize = true;
            this.labelSumMV.ForeColor = System.Drawing.Color.Gray;
            this.labelSumMV.Location = new System.Drawing.Point(30, 239);
            this.labelSumMV.Margin = new System.Windows.Forms.Padding(30, 2, 3, 0);
            this.labelSumMV.Name = "labelSumMV";
            this.labelSumMV.Size = new System.Drawing.Size(64, 13);
            this.labelSumMV.TabIndex = 13;
            this.labelSumMV.Text = "1.345.654 $";
            // 
            // labeCountMV
            // 
            this.labeCountMV.AutoSize = true;
            this.labeCountMV.ForeColor = System.Drawing.Color.Gray;
            this.labeCountMV.Location = new System.Drawing.Point(30, 219);
            this.labeCountMV.Margin = new System.Windows.Forms.Padding(30, 5, 3, 5);
            this.labeCountMV.Name = "labeCountMV";
            this.labeCountMV.Size = new System.Drawing.Size(93, 13);
            this.labeCountMV.TabIndex = 11;
            this.labeCountMV.Text = "25 325 záznamov";
            // 
            // labelSumOPRO
            // 
            this.labelSumOPRO.AutoSize = true;
            this.labelSumOPRO.ForeColor = System.Drawing.Color.Gray;
            this.labelSumOPRO.Location = new System.Drawing.Point(30, 302);
            this.labelSumOPRO.Margin = new System.Windows.Forms.Padding(30, 2, 3, 0);
            this.labelSumOPRO.Name = "labelSumOPRO";
            this.labelSumOPRO.Size = new System.Drawing.Size(64, 13);
            this.labelSumOPRO.TabIndex = 14;
            this.labelSumOPRO.Text = "1.345.654 $";
            // 
            // labelCountOPRO
            // 
            this.labelCountOPRO.AutoSize = true;
            this.labelCountOPRO.ForeColor = System.Drawing.Color.Gray;
            this.labelCountOPRO.Location = new System.Drawing.Point(30, 282);
            this.labelCountOPRO.Margin = new System.Windows.Forms.Padding(30, 5, 3, 5);
            this.labelCountOPRO.Name = "labelCountOPRO";
            this.labelCountOPRO.Size = new System.Drawing.Size(93, 13);
            this.labelCountOPRO.TabIndex = 12;
            this.labelCountOPRO.Text = "25 325 záznamov";
            // 
            // panelSeparator
            // 
            this.panelSeparator.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panelSeparator.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelSeparator.Location = new System.Drawing.Point(0, 193);
            this.panelSeparator.Name = "panelSeparator";
            this.panelSeparator.Size = new System.Drawing.Size(202, 2);
            this.panelSeparator.TabIndex = 9;
            // 
            // tableLayoutPanelTitle
            // 
            this.tableLayoutPanelTitle.ColumnCount = 4;
            this.tableLayoutPanelTitle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelTitle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelTitle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelTitle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelTitle.Controls.Add(this.labelYear, 1, 0);
            this.tableLayoutPanelTitle.Controls.Add(this.buttonExpand, 3, 0);
            this.tableLayoutPanelTitle.Controls.Add(this.pictureBoxImageIcon, 0, 0);
            this.tableLayoutPanelTitle.Controls.Add(this.labelCreated, 0, 2);
            this.tableLayoutPanelTitle.Controls.Add(this.labelLastActualization, 0, 1);
            this.tableLayoutPanelTitle.Controls.Add(this.buttonFullScreen, 2, 0);
            this.tableLayoutPanelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanelTitle.Location = new System.Drawing.Point(0, 10);
            this.tableLayoutPanelTitle.Name = "tableLayoutPanelTitle";
            this.tableLayoutPanelTitle.RowCount = 3;
            this.tableLayoutPanelTitle.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelTitle.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelTitle.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelTitle.Size = new System.Drawing.Size(202, 85);
            this.tableLayoutPanelTitle.TabIndex = 7;
            this.tableLayoutPanelTitle.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanelTitle_Paint);
            // 
            // buttonFullScreen
            // 
            this.buttonFullScreen.FlatAppearance.BorderSize = 0;
            this.buttonFullScreen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFullScreen.Image = global::cvti.isef.winformapp.Properties.Resources.full20;
            this.buttonFullScreen.Location = new System.Drawing.Point(153, 3);
            this.buttonFullScreen.Name = "buttonFullScreen";
            this.buttonFullScreen.Size = new System.Drawing.Size(20, 20);
            this.buttonFullScreen.TabIndex = 7;
            this.buttonFullScreen.UseVisualStyleBackColor = true;
            this.buttonFullScreen.Click += new System.EventHandler(this.buttonFullScreen_Click);
            // 
            // SingleYearControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.labelShort);
            this.Controls.Add(this.tableLayoutPanelTitle);
            this.Controls.Add(this.panelSeparator);
            this.Controls.Add(this.pictureBoxInfo);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SingleYearControl";
            this.Size = new System.Drawing.Size(202, 195);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImageIcon)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.tableLayoutPanelTitle.ResumeLayout(false);
            this.tableLayoutPanelTitle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelYear;
        private System.Windows.Forms.Button buttonExpand;
        private System.Windows.Forms.Label labelCreated;
        private System.Windows.Forms.PictureBox pictureBoxInfo;
        private System.Windows.Forms.Label labelLastActualization;
        private System.Windows.Forms.Label labelShort;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label labelTitleVVS;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label labelSumMao;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelTitleMaO;
        private System.Windows.Forms.Panel panelObceColorPanel;
        private System.Windows.Forms.Label labelCountMao;
        private System.Windows.Forms.Label labelSumVUC;
        private System.Windows.Forms.Label labelCountVUC;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label labelTitleVUC;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label labelSumVVS;
        private System.Windows.Forms.Label labelCountVVS;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label labelTitleMV;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label labelTitleOPRO;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label labeCountMV;
        private System.Windows.Forms.Label labelCountOPRO;
        private System.Windows.Forms.Label labelSumMV;
        private System.Windows.Forms.Label labelSumOPRO;
        private System.Windows.Forms.Panel panelSeparator;
        private System.Windows.Forms.PictureBox pictureBoxImageIcon;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelTitle;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private System.Windows.Forms.Button buttonFullScreen;
    }
}
