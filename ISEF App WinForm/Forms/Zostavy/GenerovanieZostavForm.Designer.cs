namespace cvti.isef.winformapp.Forms
{
    partial class GenerovanieZostavForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GenerovanieZostavForm));
            this.flowLayoutPanelZostavy = new System.Windows.Forms.FlowLayoutPanel();
            this.labelSubtitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.comboBoxYear = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.buttonExpandAll = new System.Windows.Forms.Button();
            this.buttonCollapseAll = new System.Windows.Forms.Button();
            this.buttonOpenDirectory = new System.Windows.Forms.Button();
            this.buttonExport = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.panelSeparator = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxPodmienky = new System.Windows.Forms.ComboBox();
            this.checkBoxHeader = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panelInfo.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            //this.labelTitle.Size = new System.Drawing.Size(587, 75);
            //this.labelTitle.Text = "Generovanie vybraných zostáv";
            //// 
            //// panelControlButtons
            //// 
            //this.panelControlButtons.Location = new System.Drawing.Point(277, 772);
            //this.panelControlButtons.Size = new System.Drawing.Size(935, 50);
            // 
            // flowLayoutPanelZostavy
            // 
            this.flowLayoutPanelZostavy.AutoScroll = true;
            this.flowLayoutPanelZostavy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelZostavy.Location = new System.Drawing.Point(277, 266);
            this.flowLayoutPanelZostavy.Name = "flowLayoutPanelZostavy";
            this.flowLayoutPanelZostavy.Size = new System.Drawing.Size(935, 506);
            this.flowLayoutPanelZostavy.TabIndex = 3;
            // 
            // labelSubtitle
            // 
            this.labelSubtitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelSubtitle.Location = new System.Drawing.Point(0, 85);
            this.labelSubtitle.Name = "labelSubtitle";
            this.labelSubtitle.Padding = new System.Windows.Forms.Padding(10);
            this.labelSubtitle.Size = new System.Drawing.Size(1212, 85);
            this.labelSubtitle.TabIndex = 4;
            this.labelSubtitle.Text = resources.GetString("labelSubtitle.Text");
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 170);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1212, 1);
            this.panel1.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 265);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1212, 1);
            this.panel2.TabIndex = 6;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tableLayoutPanel1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 171);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1212, 94);
            this.panel3.TabIndex = 7;
            // 
            // comboBoxYear
            // 
            this.comboBoxYear.FormattingEnabled = true;
            this.comboBoxYear.Location = new System.Drawing.Point(10, 55);
            this.comboBoxYear.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.comboBoxYear.Name = "comboBoxYear";
            this.comboBoxYear.Size = new System.Drawing.Size(238, 21);
            this.comboBoxYear.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 37);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(10, 2, 0, 0);
            this.label2.Size = new System.Drawing.Size(250, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Kalendárny rok pre ktorý sa exportujú zostavy";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.label1.Size = new System.Drawing.Size(111, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "Kalendárny rok";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.Control;
            this.panel4.Controls.Add(this.buttonExpandAll);
            this.panel4.Controls.Add(this.buttonCollapseAll);
            this.panel4.Controls.Add(this.buttonOpenDirectory);
            this.panel4.Controls.Add(this.buttonExport);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 822);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1212, 50);
            this.panel4.TabIndex = 8;
            // 
            // buttonExpandAll
            // 
            this.buttonExpandAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExpandAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonExpandAll.Location = new System.Drawing.Point(702, 12);
            this.buttonExpandAll.Name = "buttonExpandAll";
            this.buttonExpandAll.Size = new System.Drawing.Size(120, 25);
            this.buttonExpandAll.TabIndex = 4;
            this.buttonExpandAll.Text = "Rozbaľ všetky";
            this.buttonExpandAll.UseVisualStyleBackColor = true;
            this.buttonExpandAll.Click += new System.EventHandler(this.buttonExpandAll_Click);
            // 
            // buttonCollapseAll
            // 
            this.buttonCollapseAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCollapseAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonCollapseAll.Location = new System.Drawing.Point(828, 12);
            this.buttonCollapseAll.Name = "buttonCollapseAll";
            this.buttonCollapseAll.Size = new System.Drawing.Size(120, 25);
            this.buttonCollapseAll.TabIndex = 3;
            this.buttonCollapseAll.Text = "Zabaľ všetky";
            this.buttonCollapseAll.UseVisualStyleBackColor = true;
            this.buttonCollapseAll.Click += new System.EventHandler(this.buttonCollapseAll_Click);
            // 
            // buttonOpenDirectory
            // 
            this.buttonOpenDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOpenDirectory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonOpenDirectory.Location = new System.Drawing.Point(954, 12);
            this.buttonOpenDirectory.Name = "buttonOpenDirectory";
            this.buttonOpenDirectory.Size = new System.Drawing.Size(120, 25);
            this.buttonOpenDirectory.TabIndex = 2;
            this.buttonOpenDirectory.Text = "Otvor adresár";
            this.buttonOpenDirectory.UseVisualStyleBackColor = true;
            this.buttonOpenDirectory.Click += new System.EventHandler(this.buttonOpenDirectory_Click);
            // 
            // buttonExport
            // 
            this.buttonExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonExport.Location = new System.Drawing.Point(1080, 12);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(120, 25);
            this.buttonExport.TabIndex = 1;
            this.buttonExport.Text = "Spusti export";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1212, 1);
            this.panel5.TabIndex = 0;
            // 
            // panelInfo
            // 
            this.panelInfo.AutoScroll = true;
            this.panelInfo.BackColor = System.Drawing.SystemColors.Control;
            this.panelInfo.Controls.Add(this.panelSeparator);
            this.panelInfo.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelInfo.Location = new System.Drawing.Point(0, 266);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(277, 556);
            this.panelInfo.TabIndex = 9;
            this.panelInfo.Visible = false;
            // 
            // panelSeparator
            // 
            this.panelSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.panelSeparator.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelSeparator.Location = new System.Drawing.Point(276, 0);
            this.panelSeparator.Name = "panelSeparator";
            this.panelSeparator.Size = new System.Drawing.Size(1, 556);
            this.panelSeparator.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label6, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxPodmienky, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxHeader, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxYear, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(780, 94);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(259, 10);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.label3.Size = new System.Drawing.Size(158, 27);
            this.label3.TabIndex = 3;
            this.label3.Text = "Dodatočná podmienka";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(259, 37);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(10, 2, 0, 0);
            this.label4.Size = new System.Drawing.Size(240, 15);
            this.label4.TabIndex = 4;
            this.label4.Text = "Dodatočná podmienka aplikovaná na výber";
            // 
            // comboBoxPodmienky
            // 
            this.comboBoxPodmienky.FormattingEnabled = true;
            this.comboBoxPodmienky.Location = new System.Drawing.Point(266, 55);
            this.comboBoxPodmienky.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.comboBoxPodmienky.Name = "comboBoxPodmienky";
            this.comboBoxPodmienky.Size = new System.Drawing.Size(238, 21);
            this.comboBoxPodmienky.TabIndex = 5;
            // 
            // checkBoxHeader
            // 
            this.checkBoxHeader.AutoSize = true;
            this.checkBoxHeader.Checked = true;
            this.checkBoxHeader.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxHeader.Location = new System.Drawing.Point(517, 55);
            this.checkBoxHeader.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.checkBoxHeader.Name = "checkBoxHeader";
            this.checkBoxHeader.Size = new System.Drawing.Size(149, 17);
            this.checkBoxHeader.TabIndex = 6;
            this.checkBoxHeader.Text = "Hlavička na novú stranu";
            this.checkBoxHeader.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(510, 10);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.label5.Size = new System.Drawing.Size(70, 27);
            this.label5.TabIndex = 7;
            this.label5.Text = "Hlavička";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(510, 37);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(10, 2, 0, 0);
            this.label6.Size = new System.Drawing.Size(310, 15);
            this.label6.TabIndex = 8;
            this.label6.Text = "Určuje či sa zostava exportuje s hlavičkou na novej strane";
            // 
            // GenerovanieZostavForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1212, 872);
            this.Controls.Add(this.flowLayoutPanelZostavy);
            this.Controls.Add(this.panelInfo);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelSubtitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GenerovanieZostavForm";
            this.Text = "Generovanie zostáv";
            this.Controls.SetChildIndex(this.labelSubtitle, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.panel3, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.panel4, 0);
            this.Controls.SetChildIndex(this.panelInfo, 0);
            this.Controls.SetChildIndex(this.panelControlButtons, 0);
            this.Controls.SetChildIndex(this.flowLayoutPanelZostavy, 0);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panelInfo.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelZostavy;
        private System.Windows.Forms.Label labelSubtitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox comboBoxYear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button buttonOpenDirectory;
        private System.Windows.Forms.Button buttonExpandAll;
        private System.Windows.Forms.Button buttonCollapseAll;
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.Panel panelSeparator;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxPodmienky;
        private System.Windows.Forms.CheckBox checkBoxHeader;
    }
}