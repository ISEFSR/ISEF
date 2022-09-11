namespace cvti.isef.winformapp.Controls.Import
{
    partial class ImportChooseExcelFiles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportChooseExcelFiles));
            this.tableLayoutPanelFileRO = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabelFileRO = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabelFiilePO = new System.Windows.Forms.LinkLabel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.openFileDialogFile = new System.Windows.Forms.OpenFileDialog();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.label11 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.tableLayoutPanelYear = new System.Windows.Forms.TableLayoutPanel();
            this.numericUpDownYear = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.tableLayoutPanelFilePO = new System.Windows.Forms.TableLayoutPanel();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.panelControlButtons = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tableLayoutPanelFileRO.SuspendLayout();
            this.tableLayoutPanelYear.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownYear)).BeginInit();
            this.tableLayoutPanelFilePO.SuspendLayout();
            this.panelControlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelFileRO
            // 
            this.tableLayoutPanelFileRO.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanelFileRO.ColumnCount = 1;
            this.tableLayoutPanelFileRO.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelFileRO.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanelFileRO.Controls.Add(this.linkLabelFileRO, 0, 2);
            this.tableLayoutPanelFileRO.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanelFileRO.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanelFileRO.Location = new System.Drawing.Point(0, 229);
            this.tableLayoutPanelFileRO.Margin = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanelFileRO.Name = "tableLayoutPanelFileRO";
            this.tableLayoutPanelFileRO.RowCount = 3;
            this.tableLayoutPanelFileRO.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelFileRO.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelFileRO.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelFileRO.Size = new System.Drawing.Size(649, 86);
            this.tableLayoutPanelFileRO.TabIndex = 4;
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
            this.label1.Size = new System.Drawing.Size(120, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Vstupný RO súbor";
            this.toolTipInfo.SetToolTip(this.label1, "Vstupný XLSX súbor obsahujúci dáta pre RO a kalendárny rok");
            // 
            // linkLabelFileRO
            // 
            this.linkLabelFileRO.AutoSize = true;
            this.linkLabelFileRO.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabelFileRO.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(72)))), ((int)(((byte)(144)))));
            this.linkLabelFileRO.Location = new System.Drawing.Point(15, 45);
            this.linkLabelFileRO.Margin = new System.Windows.Forms.Padding(15, 0, 3, 0);
            this.linkLabelFileRO.Name = "linkLabelFileRO";
            this.linkLabelFileRO.Size = new System.Drawing.Size(78, 13);
            this.linkLabelFileRO.TabIndex = 2;
            this.linkLabelFileRO.TabStop = true;
            this.linkLabelFileRO.Tag = "ro";
            this.linkLabelFileRO.Text = "Vyber súbor...";
            this.linkLabelFileRO.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelFileRO_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.Help;
            this.label2.Location = new System.Drawing.Point(15, 27);
            this.label2.Margin = new System.Windows.Forms.Padding(15, 0, 3, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(342, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Výber vstupného XLSX súboru s údajmi za rozpočtové organizácie";
            // 
            // linkLabelFiilePO
            // 
            this.linkLabelFiilePO.AutoSize = true;
            this.linkLabelFiilePO.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabelFiilePO.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(72)))), ((int)(((byte)(144)))));
            this.linkLabelFiilePO.Location = new System.Drawing.Point(15, 45);
            this.linkLabelFiilePO.Margin = new System.Windows.Forms.Padding(15, 0, 3, 0);
            this.linkLabelFiilePO.Name = "linkLabelFiilePO";
            this.linkLabelFiilePO.Size = new System.Drawing.Size(78, 13);
            this.linkLabelFiilePO.TabIndex = 8;
            this.linkLabelFiilePO.TabStop = true;
            this.linkLabelFiilePO.Tag = "po";
            this.linkLabelFiilePO.Text = "Vyber súbor...";
            this.linkLabelFiilePO.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelFileRO_LinkClicked);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Cursor = System.Windows.Forms.Cursors.Help;
            this.label6.Location = new System.Drawing.Point(15, 27);
            this.label6.Margin = new System.Windows.Forms.Padding(15, 0, 3, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(300, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Výber vstupného XLSX súboru za príspevkové organizácie";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Cursor = System.Windows.Forms.Cursors.Help;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.label5.Location = new System.Drawing.Point(15, 10);
            this.label5.Margin = new System.Windows.Forms.Padding(15, 10, 3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 17);
            this.label5.TabIndex = 5;
            this.label5.Text = "Vstupný PO súbor";
            this.toolTipInfo.SetToolTip(this.label5, "Vstupný XLSX súbor obsahujúci dáta pre PO a kalendárny rok");
            // 
            // openFileDialogFile
            // 
            this.openFileDialogFile.Filter = "Excel file (xlsx)|*.xlsx";
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
            this.toolTipInfo.SetToolTip(this.button3, "Automaticky na zákalde názvu vyberie súbory z vybraného adresáru");
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
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
            this.tableLayoutPanelYear.Size = new System.Drawing.Size(649, 99);
            this.tableLayoutPanelYear.TabIndex = 15;
            // 
            // numericUpDownYear
            // 
            this.numericUpDownYear.Location = new System.Drawing.Point(15, 63);
            this.numericUpDownYear.Margin = new System.Windows.Forms.Padding(15, 3, 3, 25);
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
            this.label12.Location = new System.Drawing.Point(15, 47);
            this.label12.Margin = new System.Windows.Forms.Padding(15, 0, 3, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(207, 13);
            this.label12.TabIndex = 14;
            this.label12.Text = "Výber vstupných DBF súborov pre kraje";
            // 
            // tableLayoutPanelFilePO
            // 
            this.tableLayoutPanelFilePO.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanelFilePO.ColumnCount = 1;
            this.tableLayoutPanelFilePO.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelFilePO.Controls.Add(this.linkLabelFiilePO, 0, 2);
            this.tableLayoutPanelFilePO.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanelFilePO.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanelFilePO.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanelFilePO.Location = new System.Drawing.Point(0, 316);
            this.tableLayoutPanelFilePO.Margin = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanelFilePO.Name = "tableLayoutPanelFilePO";
            this.tableLayoutPanelFilePO.RowCount = 3;
            this.tableLayoutPanelFilePO.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelFilePO.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelFilePO.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelFilePO.Size = new System.Drawing.Size(649, 98);
            this.tableLayoutPanelFilePO.TabIndex = 16;
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
            this.label14.Size = new System.Drawing.Size(649, 83);
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
            this.label13.Size = new System.Drawing.Size(649, 45);
            this.label13.TabIndex = 20;
            this.label13.Text = "Import z dvoch vstupných XLSX súborov";
            // 
            // panelControlButtons
            // 
            this.panelControlButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelControlButtons.Controls.Add(this.panel1);
            this.panelControlButtons.Controls.Add(this.button1);
            this.panelControlButtons.Controls.Add(this.button2);
            this.panelControlButtons.Controls.Add(this.button3);
            this.panelControlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControlButtons.Location = new System.Drawing.Point(0, 511);
            this.panelControlButtons.Name = "panelControlButtons";
            this.panelControlButtons.Size = new System.Drawing.Size(649, 35);
            this.panelControlButtons.TabIndex = 21;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(649, 1);
            this.panel1.TabIndex = 14;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Location = new System.Drawing.Point(536, 7);
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
            this.button2.Location = new System.Drawing.Point(420, 7);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(110, 25);
            this.button2.TabIndex = 12;
            this.button2.Text = "&Naspäť";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 128);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(649, 1);
            this.panel3.TabIndex = 22;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 228);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(649, 1);
            this.panel2.TabIndex = 23;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 315);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(649, 1);
            this.panel4.TabIndex = 24;
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "Adresar so vstupnym XLSX suborom";
            // 
            // ImportChooseExcelFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanelFilePO);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.tableLayoutPanelFileRO);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.tableLayoutPanelYear);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panelControlButtons);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "ImportChooseExcelFiles";
            this.Size = new System.Drawing.Size(649, 546);
            this.tableLayoutPanelFileRO.ResumeLayout(false);
            this.tableLayoutPanelFileRO.PerformLayout();
            this.tableLayoutPanelYear.ResumeLayout(false);
            this.tableLayoutPanelYear.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownYear)).EndInit();
            this.tableLayoutPanelFilePO.ResumeLayout(false);
            this.tableLayoutPanelFilePO.PerformLayout();
            this.panelControlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelFileRO;
        private System.Windows.Forms.LinkLabel linkLabelFiilePO;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabelFileRO;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog openFileDialogFile;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelYear;
        private System.Windows.Forms.NumericUpDown numericUpDownYear;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelFilePO;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel panelControlButtons;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}
