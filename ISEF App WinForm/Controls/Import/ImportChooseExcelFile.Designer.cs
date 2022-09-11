namespace cvti.isef.winformapp.Controls.Main.Import
{
    partial class ImportChooseExcelFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportChooseExcelFile));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabelFile = new System.Windows.Forms.LinkLabel();
            this.openFileDialogFile = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanelFile = new System.Windows.Forms.TableLayoutPanel();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanelYear = new System.Windows.Forms.TableLayoutPanel();
            this.numericUpDownYear = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.panelControlButtons = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanelFile.SuspendLayout();
            this.tableLayoutPanelYear.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownYear)).BeginInit();
            this.panelControlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Help;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.label1.Location = new System.Drawing.Point(15, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(15, 10, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Vstupný súbor";
            this.toolTipInfo.SetToolTip(this.label1, "Vstupný XLSX súbor obsahujúci dáta pre VVS a kalendárny rok");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.Help;
            this.label2.Location = new System.Drawing.Point(15, 30);
            this.label2.Margin = new System.Windows.Forms.Padding(15, 3, 3, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Výber vstupného XLSX súboru";
            // 
            // linkLabelFile
            // 
            this.linkLabelFile.AutoSize = true;
            this.linkLabelFile.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabelFile.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(72)))), ((int)(((byte)(144)))));
            this.linkLabelFile.Location = new System.Drawing.Point(15, 51);
            this.linkLabelFile.Margin = new System.Windows.Forms.Padding(15, 3, 3, 0);
            this.linkLabelFile.Name = "linkLabelFile";
            this.linkLabelFile.Size = new System.Drawing.Size(78, 13);
            this.linkLabelFile.TabIndex = 2;
            this.linkLabelFile.TabStop = true;
            this.linkLabelFile.Text = "Vyber súbor...";
            this.linkLabelFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelFile_LinkClicked);
            // 
            // openFileDialogFile
            // 
            this.openFileDialogFile.Filter = "Excel file (xlsx)|*.xlsx";
            // 
            // tableLayoutPanelFile
            // 
            this.tableLayoutPanelFile.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanelFile.ColumnCount = 1;
            this.tableLayoutPanelFile.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelFile.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanelFile.Controls.Add(this.linkLabelFile, 0, 2);
            this.tableLayoutPanelFile.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanelFile.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanelFile.Location = new System.Drawing.Point(0, 233);
            this.tableLayoutPanelFile.Margin = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanelFile.Name = "tableLayoutPanelFile";
            this.tableLayoutPanelFile.RowCount = 3;
            this.tableLayoutPanelFile.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelFile.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelFile.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelFile.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelFile.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelFile.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelFile.Size = new System.Drawing.Size(587, 111);
            this.tableLayoutPanelFile.TabIndex = 3;
            // 
            // tableLayoutPanelYear
            // 
            this.tableLayoutPanelYear.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanelYear.ColumnCount = 1;
            this.tableLayoutPanelYear.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelYear.Controls.Add(this.numericUpDownYear, 0, 2);
            this.tableLayoutPanelYear.Controls.Add(this.label12, 0, 1);
            this.tableLayoutPanelYear.Controls.Add(this.label11, 0, 0);
            this.tableLayoutPanelYear.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanelYear.Location = new System.Drawing.Point(0, 129);
            this.tableLayoutPanelYear.Margin = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanelYear.Name = "tableLayoutPanelYear";
            this.tableLayoutPanelYear.RowCount = 3;
            this.tableLayoutPanelYear.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelYear.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelYear.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelYear.Size = new System.Drawing.Size(587, 103);
            this.tableLayoutPanelYear.TabIndex = 16;
            // 
            // numericUpDownYear
            // 
            this.numericUpDownYear.Location = new System.Drawing.Point(15, 66);
            this.numericUpDownYear.Margin = new System.Windows.Forms.Padding(15, 3, 3, 15);
            this.numericUpDownYear.Maximum = new decimal(new int[] {
            2050,
            0,
            0,
            0});
            this.numericUpDownYear.Minimum = new decimal(new int[] {
            2010,
            0,
            0,
            0});
            this.numericUpDownYear.Name = "numericUpDownYear";
            this.numericUpDownYear.Size = new System.Drawing.Size(207, 22);
            this.numericUpDownYear.TabIndex = 14;
            this.numericUpDownYear.Value = new decimal(new int[] {
            2010,
            0,
            0,
            0});
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Cursor = System.Windows.Forms.Cursors.Help;
            this.label12.Location = new System.Drawing.Point(15, 50);
            this.label12.Margin = new System.Windows.Forms.Padding(15, 3, 3, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(207, 13);
            this.label12.TabIndex = 14;
            this.label12.Text = "Výber vstupných DBF súborov pre kraje";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Cursor = System.Windows.Forms.Cursors.Help;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.label11.Location = new System.Drawing.Point(15, 30);
            this.label11.Margin = new System.Windows.Forms.Padding(15, 30, 15, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(101, 17);
            this.label11.TabIndex = 17;
            this.label11.Text = "Kalendárny rok";
            this.toolTipInfo.SetToolTip(this.label11, "Kalendárny rok, pre ktorý budú dáta nahrané. ");
            // 
            // panelControlButtons
            // 
            this.panelControlButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelControlButtons.Controls.Add(this.panel1);
            this.panelControlButtons.Controls.Add(this.button1);
            this.panelControlButtons.Controls.Add(this.button2);
            this.panelControlButtons.Controls.Add(this.button3);
            this.panelControlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControlButtons.Location = new System.Drawing.Point(0, 464);
            this.panelControlButtons.Name = "panelControlButtons";
            this.panelControlButtons.Size = new System.Drawing.Size(587, 35);
            this.panelControlButtons.TabIndex = 17;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(587, 1);
            this.panel1.TabIndex = 14;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Location = new System.Drawing.Point(474, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 25);
            this.button1.TabIndex = 11;
            this.button1.Text = "Ďalej";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.Location = new System.Drawing.Point(358, 7);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(110, 25);
            this.button2.TabIndex = 12;
            this.button2.Text = "&Naspäť";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(3, 7);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(110, 25);
            this.button3.TabIndex = 13;
            this.button3.Text = "&Z Adresáru";
            this.toolTipInfo.SetToolTip(this.button3, "Automaticky na základe názvu priradí súbor");
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label14.Cursor = System.Windows.Forms.Cursors.Help;
            this.label14.Dock = System.Windows.Forms.DockStyle.Top;
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label14.Location = new System.Drawing.Point(0, 45);
            this.label14.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label14.Name = "label14";
            this.label14.Padding = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.label14.Size = new System.Drawing.Size(587, 83);
            this.label14.TabIndex = 19;
            this.label14.Text = resources.GetString("label14.Text");
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label13.Cursor = System.Windows.Forms.Cursors.Help;
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.label13.Location = new System.Drawing.Point(0, 0);
            this.label13.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label13.Name = "label13";
            this.label13.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.label13.Size = new System.Drawing.Size(587, 45);
            this.label13.TabIndex = 20;
            this.label13.Text = "Import zo vstupného XLSX súboru";
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "Adresar so vstupnymi XLSX subormi";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 128);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(587, 1);
            this.panel2.TabIndex = 21;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 232);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(587, 1);
            this.panel3.TabIndex = 22;
            // 
            // ImportChooseExcelFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanelFile);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.tableLayoutPanelYear);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.panelControlButtons);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(15, 3, 3, 25);
            this.Name = "ImportChooseExcelFile";
            this.Size = new System.Drawing.Size(587, 499);
            this.tableLayoutPanelFile.ResumeLayout(false);
            this.tableLayoutPanelFile.PerformLayout();
            this.tableLayoutPanelYear.ResumeLayout(false);
            this.tableLayoutPanelYear.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownYear)).EndInit();
            this.panelControlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabelFile;
        private System.Windows.Forms.OpenFileDialog openFileDialogFile;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelFile;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelYear;
        private System.Windows.Forms.NumericUpDown numericUpDownYear;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panelControlButtons;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
    }
}
