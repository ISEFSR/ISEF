namespace cvti.isef.winformapp.Controls.Main.Ciselniky
{
    partial class OrganizacieControl
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
            this.panelControlButtons = new System.Windows.Forms.Panel();
            this.buttonWeb = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonReload = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxStupenFilter = new System.Windows.Forms.ComboBox();
            this.comboBoxPodr = new System.Windows.Forms.ComboBox();
            this.comboBoxStupen = new System.Windows.Forms.ComboBox();
            this.comboBoxObec = new System.Windows.Forms.ComboBox();
            this.comboBoxSegm = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxICO = new System.Windows.Forms.TextBox();
            this.contextMenuStripCondition = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.andToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.orToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxShort = new System.Windows.Forms.TextBox();
            this.textBoxLong = new System.Windows.Forms.TextBox();
            this.listBoxOrganizacie = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.cueTextBoxFilter = new cvti.isef.winformapp.Components.CueTextBox(this.components);
            this.timerFilterDelay = new System.Windows.Forms.Timer(this.components);
            this.panelControlButtons.SuspendLayout();
            this.contextMenuStripCondition.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControlButtons
            // 
            this.panelControlButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelControlButtons.Controls.Add(this.buttonWeb);
            this.panelControlButtons.Controls.Add(this.button1);
            this.panelControlButtons.Controls.Add(this.buttonReload);
            this.panelControlButtons.Controls.Add(this.buttonSave);
            this.panelControlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControlButtons.Location = new System.Drawing.Point(0, 529);
            this.panelControlButtons.Name = "panelControlButtons";
            this.panelControlButtons.Size = new System.Drawing.Size(1027, 30);
            this.panelControlButtons.TabIndex = 3;
            this.panelControlButtons.Paint += new System.Windows.Forms.PaintEventHandler(this.panelControlButtons_Paint);
            // 
            // buttonWeb
            // 
            this.buttonWeb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonWeb.Location = new System.Drawing.Point(784, 4);
            this.buttonWeb.Name = "buttonWeb";
            this.buttonWeb.Size = new System.Drawing.Size(240, 23);
            this.buttonWeb.TabIndex = 30;
            this.buttonWeb.Text = "Skontroluj organizáciu na webe";
            this.buttonWeb.UseVisualStyleBackColor = true;
            this.buttonWeb.Click += new System.EventHandler(this.buttonWeb_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Zobraz prehlad";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonReload
            // 
            this.buttonReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonReload.Location = new System.Drawing.Point(129, 4);
            this.buttonReload.Name = "buttonReload";
            this.buttonReload.Size = new System.Drawing.Size(120, 23);
            this.buttonReload.TabIndex = 1;
            this.buttonReload.Text = "Zruš zmeny";
            this.buttonReload.UseVisualStyleBackColor = true;
            this.buttonReload.Click += new System.EventHandler(this.buttonReload_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSave.Location = new System.Drawing.Point(255, 4);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(120, 23);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "Ulož zmeny";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Výber stupňa:";
            // 
            // comboBoxStupenFilter
            // 
            this.comboBoxStupenFilter.FormattingEnabled = true;
            this.comboBoxStupenFilter.Location = new System.Drawing.Point(3, 20);
            this.comboBoxStupenFilter.Name = "comboBoxStupenFilter";
            this.comboBoxStupenFilter.Size = new System.Drawing.Size(326, 21);
            this.comboBoxStupenFilter.TabIndex = 12;
            this.comboBoxStupenFilter.SelectedIndexChanged += new System.EventHandler(this.comboBoxKraj_SelectedIndexChanged);
            // 
            // comboBoxPodr
            // 
            this.comboBoxPodr.FormattingEnabled = true;
            this.comboBoxPodr.Location = new System.Drawing.Point(3, 177);
            this.comboBoxPodr.Name = "comboBoxPodr";
            this.comboBoxPodr.Size = new System.Drawing.Size(226, 21);
            this.comboBoxPodr.TabIndex = 13;
            // 
            // comboBoxStupen
            // 
            this.comboBoxStupen.FormattingEnabled = true;
            this.comboBoxStupen.Location = new System.Drawing.Point(3, 97);
            this.comboBoxStupen.Name = "comboBoxStupen";
            this.comboBoxStupen.Size = new System.Drawing.Size(226, 21);
            this.comboBoxStupen.TabIndex = 14;
            // 
            // comboBoxObec
            // 
            this.comboBoxObec.DisplayMember = "Nazov";
            this.comboBoxObec.FormattingEnabled = true;
            this.comboBoxObec.Location = new System.Drawing.Point(3, 137);
            this.comboBoxObec.Name = "comboBoxObec";
            this.comboBoxObec.Size = new System.Drawing.Size(226, 21);
            this.comboBoxObec.TabIndex = 15;
            // 
            // comboBoxSegm
            // 
            this.comboBoxSegm.FormattingEnabled = true;
            this.comboBoxSegm.Location = new System.Drawing.Point(3, 57);
            this.comboBoxSegm.Name = "comboBoxSegm";
            this.comboBoxSegm.Size = new System.Drawing.Size(226, 21);
            this.comboBoxSegm.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 17;
            this.label1.Tag = "18";
            this.label1.Text = "ICO *";
            // 
            // textBoxICO
            // 
            this.textBoxICO.ContextMenuStrip = this.contextMenuStripCondition;
            this.textBoxICO.Location = new System.Drawing.Point(3, 16);
            this.textBoxICO.Name = "textBoxICO";
            this.textBoxICO.ReadOnly = true;
            this.textBoxICO.Size = new System.Drawing.Size(226, 22);
            this.textBoxICO.TabIndex = 18;
            // 
            // contextMenuStripCondition
            // 
            this.contextMenuStripCondition.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.andToolStripMenuItem,
            this.orToolStripMenuItem,
            this.toolStripMenuItem2});
            this.contextMenuStripCondition.Name = "contextMenuStripCondition";
            this.contextMenuStripCondition.Size = new System.Drawing.Size(167, 70);
            // 
            // andToolStripMenuItem
            // 
            this.andToolStripMenuItem.Name = "andToolStripMenuItem";
            this.andToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.andToolStripMenuItem.Tag = "0";
            this.andToolStripMenuItem.Text = "= (Rovná sa)";
            this.andToolStripMenuItem.Click += new System.EventHandler(this.andToolStripMenuItem_Click);
            // 
            // orToolStripMenuItem
            // 
            this.orToolStripMenuItem.Name = "orToolStripMenuItem";
            this.orToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.orToolStripMenuItem.Tag = "1";
            this.orToolStripMenuItem.Text = "> (Je väčšie ako)";
            this.orToolStripMenuItem.Click += new System.EventHandler(this.andToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(166, 22);
            this.toolStripMenuItem2.Tag = "2";
            this.toolStripMenuItem2.Text = "< (Je menšie ako)";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.andToolStripMenuItem_Click);
            // 
            // textBoxShort
            // 
            this.textBoxShort.ContextMenuStrip = this.contextMenuStripCondition;
            this.textBoxShort.Dock = System.Windows.Forms.DockStyle.Left;
            this.textBoxShort.Location = new System.Drawing.Point(3, 217);
            this.textBoxShort.Name = "textBoxShort";
            this.textBoxShort.Size = new System.Drawing.Size(389, 22);
            this.textBoxShort.TabIndex = 19;
            this.textBoxShort.Tag = "19";
            // 
            // textBoxLong
            // 
            this.textBoxLong.ContextMenuStrip = this.contextMenuStripCondition;
            this.textBoxLong.Location = new System.Drawing.Point(3, 258);
            this.textBoxLong.Multiline = true;
            this.textBoxLong.Name = "textBoxLong";
            this.textBoxLong.Size = new System.Drawing.Size(389, 250);
            this.textBoxLong.TabIndex = 20;
            this.textBoxLong.Tag = "20";
            // 
            // listBoxOrganizacie
            // 
            this.listBoxOrganizacie.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxOrganizacie.ContextMenuStrip = this.contextMenuStripCondition;
            this.listBoxOrganizacie.FormattingEnabled = true;
            this.listBoxOrganizacie.Location = new System.Drawing.Point(3, 73);
            this.listBoxOrganizacie.Name = "listBoxOrganizacie";
            this.listBoxOrganizacie.Size = new System.Drawing.Size(326, 446);
            this.listBoxOrganizacie.TabIndex = 21;
            this.listBoxOrganizacie.SelectedIndexChanged += new System.EventHandler(this.listBoxOrganizacie_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 201);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Skrátený názov *";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 242);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Názov *";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Podriadenosť";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "Stupeň";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 121);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 26;
            this.label7.Text = "Obec:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 27;
            this.label8.Text = "Segment:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 12);
            this.tableLayoutPanel1.Controls.Add(this.textBoxLong, 0, 13);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.textBoxICO, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxPodr, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxSegm, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxStupen, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxObec, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.textBoxShort, 0, 11);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(335, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 14;
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(689, 515);
            this.tableLayoutPanel1.TabIndex = 28;
            // 
            // cueTextBoxFilter
            // 
            this.cueTextBoxFilter.Cue = "Filtruj organizáciu...";
            this.cueTextBoxFilter.Location = new System.Drawing.Point(3, 45);
            this.cueTextBoxFilter.Name = "cueTextBoxFilter";
            this.cueTextBoxFilter.Size = new System.Drawing.Size(326, 22);
            this.cueTextBoxFilter.TabIndex = 29;
            this.cueTextBoxFilter.TextChanged += new System.EventHandler(this.cueTextBoxFilter_TextChanged);
            // 
            // timerFilterDelay
            // 
            this.timerFilterDelay.Interval = 500;
            this.timerFilterDelay.Tick += new System.EventHandler(this.timerFilterDelay_Tick);
            // 
            // OrganizacieControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cueTextBoxFilter);
            this.Controls.Add(this.panelControlButtons);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.listBoxOrganizacie);
            this.Controls.Add(this.comboBoxStupenFilter);
            this.Controls.Add(this.label5);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "OrganizacieControl";
            this.Size = new System.Drawing.Size(1027, 559);
            this.panelControlButtons.ResumeLayout(false);
            this.contextMenuStripCondition.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panelControlButtons;
        private System.Windows.Forms.Button buttonReload;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxStupenFilter;
        private System.Windows.Forms.ComboBox comboBoxPodr;
        private System.Windows.Forms.ComboBox comboBoxStupen;
        private System.Windows.Forms.ComboBox comboBoxObec;
        private System.Windows.Forms.ComboBox comboBoxSegm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxICO;
        private System.Windows.Forms.TextBox textBoxShort;
        private System.Windows.Forms.TextBox textBoxLong;
        private System.Windows.Forms.ListBox listBoxOrganizacie;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripCondition;
        private System.Windows.Forms.ToolStripMenuItem andToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem orToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.Button buttonWeb;
        private Components.CueTextBox cueTextBoxFilter;
        private System.Windows.Forms.Timer timerFilterDelay;
    }
}
