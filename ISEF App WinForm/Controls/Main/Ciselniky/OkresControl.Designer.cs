namespace cvti.isef.winformapp.Controls.Main.Ciselniky
{
    partial class OkresControl
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
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonNew = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonReload = new System.Windows.Forms.Button();
            this.buttonSaveChanges = new System.Windows.Forms.Button();
            this.listBoxOkresy = new System.Windows.Forms.ListBox();
            this.contextMenuStripCondition = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.andToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.orToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBoxKraje = new System.Windows.Forms.ComboBox();
            this.checkBoxKrajFilter = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPopis = new System.Windows.Forms.TextBox();
            this.textBoxNazov = new System.Windows.Forms.TextBox();
            this.textBoxKod = new System.Windows.Forms.TextBox();
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
            this.panelControlButtons.Controls.Add(this.buttonRemove);
            this.panelControlButtons.Controls.Add(this.buttonNew);
            this.panelControlButtons.Controls.Add(this.button1);
            this.panelControlButtons.Controls.Add(this.buttonReload);
            this.panelControlButtons.Controls.Add(this.buttonSaveChanges);
            this.panelControlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControlButtons.Location = new System.Drawing.Point(0, 504);
            this.panelControlButtons.Name = "panelControlButtons";
            this.panelControlButtons.Size = new System.Drawing.Size(824, 30);
            this.panelControlButtons.TabIndex = 3;
            this.panelControlButtons.Paint += new System.Windows.Forms.PaintEventHandler(this.panelControlButtons_Paint);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(507, 4);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(120, 23);
            this.buttonRemove.TabIndex = 13;
            this.buttonRemove.Text = "Odstráň okres";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonNew
            // 
            this.buttonNew.Location = new System.Drawing.Point(381, 4);
            this.buttonNew.Name = "buttonNew";
            this.buttonNew.Size = new System.Drawing.Size(120, 23);
            this.buttonNew.TabIndex = 12;
            this.buttonNew.Text = "Nový okres";
            this.buttonNew.UseVisualStyleBackColor = true;
            this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Zobraz prehľad";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonReload
            // 
            this.buttonReload.Location = new System.Drawing.Point(129, 4);
            this.buttonReload.Name = "buttonReload";
            this.buttonReload.Size = new System.Drawing.Size(120, 23);
            this.buttonReload.TabIndex = 1;
            this.buttonReload.Text = "Zruš zmeny";
            this.buttonReload.UseVisualStyleBackColor = true;
            this.buttonReload.Click += new System.EventHandler(this.buttonReload_Click);
            // 
            // buttonSaveChanges
            // 
            this.buttonSaveChanges.Location = new System.Drawing.Point(255, 4);
            this.buttonSaveChanges.Name = "buttonSaveChanges";
            this.buttonSaveChanges.Size = new System.Drawing.Size(120, 23);
            this.buttonSaveChanges.TabIndex = 0;
            this.buttonSaveChanges.Text = "Ulož zmeny";
            this.buttonSaveChanges.UseVisualStyleBackColor = true;
            this.buttonSaveChanges.Click += new System.EventHandler(this.buttonSaveChanges_Click);
            // 
            // listBoxOkresy
            // 
            this.listBoxOkresy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxOkresy.ContextMenuStrip = this.contextMenuStripCondition;
            this.listBoxOkresy.FormattingEnabled = true;
            this.listBoxOkresy.Location = new System.Drawing.Point(3, 79);
            this.listBoxOkresy.Name = "listBoxOkresy";
            this.listBoxOkresy.Size = new System.Drawing.Size(354, 420);
            this.listBoxOkresy.TabIndex = 4;
            this.listBoxOkresy.SelectedIndexChanged += new System.EventHandler(this.listBoxOkresy_SelectedIndexChanged);
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
            this.orToolStripMenuItem.Text = "> (Je väčšia ako)";
            this.orToolStripMenuItem.Click += new System.EventHandler(this.andToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(166, 22);
            this.toolStripMenuItem2.Tag = "2";
            this.toolStripMenuItem2.Text = "< (Je menšia ako)";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.andToolStripMenuItem_Click);
            // 
            // comboBoxKraje
            // 
            this.comboBoxKraje.Enabled = false;
            this.comboBoxKraje.FormattingEnabled = true;
            this.comboBoxKraje.Location = new System.Drawing.Point(3, 26);
            this.comboBoxKraje.Name = "comboBoxKraje";
            this.comboBoxKraje.Size = new System.Drawing.Size(354, 21);
            this.comboBoxKraje.TabIndex = 6;
            this.comboBoxKraje.SelectedIndexChanged += new System.EventHandler(this.comboBoxKraje_SelectedIndexChanged);
            // 
            // checkBoxKrajFilter
            // 
            this.checkBoxKrajFilter.AutoSize = true;
            this.checkBoxKrajFilter.Location = new System.Drawing.Point(3, 3);
            this.checkBoxKrajFilter.Name = "checkBoxKrajFilter";
            this.checkBoxKrajFilter.Size = new System.Drawing.Size(78, 17);
            this.checkBoxKrajFilter.TabIndex = 7;
            this.checkBoxKrajFilter.Text = "Filtruj kraj";
            this.checkBoxKrajFilter.UseVisualStyleBackColor = true;
            this.checkBoxKrajFilter.CheckedChanged += new System.EventHandler(this.checkBoxKrajFilter_CheckedChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBoxPopis, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.textBoxNazov, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.textBoxKod, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(363, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(392, 496);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Dlhý názov *";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Skratený názov *";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Kod *";
            // 
            // textBoxPopis
            // 
            this.textBoxPopis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxPopis.Location = new System.Drawing.Point(3, 98);
            this.textBoxPopis.Multiline = true;
            this.textBoxPopis.Name = "textBoxPopis";
            this.textBoxPopis.Size = new System.Drawing.Size(386, 395);
            this.textBoxPopis.TabIndex = 2;
            this.textBoxPopis.Tag = "15";
            // 
            // textBoxNazov
            // 
            this.textBoxNazov.Location = new System.Drawing.Point(3, 57);
            this.textBoxNazov.Name = "textBoxNazov";
            this.textBoxNazov.Size = new System.Drawing.Size(239, 22);
            this.textBoxNazov.TabIndex = 3;
            this.textBoxNazov.Tag = "14";
            // 
            // textBoxKod
            // 
            this.textBoxKod.Location = new System.Drawing.Point(3, 16);
            this.textBoxKod.Name = "textBoxKod";
            this.textBoxKod.ReadOnly = true;
            this.textBoxKod.Size = new System.Drawing.Size(239, 22);
            this.textBoxKod.TabIndex = 4;
            this.textBoxKod.Tag = "13";
            // 
            // cueTextBoxFilter
            // 
            this.cueTextBoxFilter.Cue = "Filtruj okres...";
            this.cueTextBoxFilter.Location = new System.Drawing.Point(3, 51);
            this.cueTextBoxFilter.Name = "cueTextBoxFilter";
            this.cueTextBoxFilter.Size = new System.Drawing.Size(354, 22);
            this.cueTextBoxFilter.TabIndex = 18;
            this.cueTextBoxFilter.TextChanged += new System.EventHandler(this.cueTextBoxFilter_TextChanged);
            // 
            // timerFilterDelay
            // 
            this.timerFilterDelay.Interval = 500;
            this.timerFilterDelay.Tick += new System.EventHandler(this.timerFilterDelay_Tick);
            // 
            // OkresControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cueTextBoxFilter);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.checkBoxKrajFilter);
            this.Controls.Add(this.comboBoxKraje);
            this.Controls.Add(this.listBoxOkresy);
            this.Controls.Add(this.panelControlButtons);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "OkresControl";
            this.Size = new System.Drawing.Size(824, 534);
            this.panelControlButtons.ResumeLayout(false);
            this.contextMenuStripCondition.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelControlButtons;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonReload;
        private System.Windows.Forms.Button buttonSaveChanges;
        private System.Windows.Forms.ListBox listBoxOkresy;
        private System.Windows.Forms.ComboBox comboBoxKraje;
        private System.Windows.Forms.CheckBox checkBoxKrajFilter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPopis;
        private System.Windows.Forms.TextBox textBoxNazov;
        private System.Windows.Forms.TextBox textBoxKod;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripCondition;
        private System.Windows.Forms.ToolStripMenuItem andToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem orToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonNew;
        private Components.CueTextBox cueTextBoxFilter;
        private System.Windows.Forms.Timer timerFilterDelay;
    }
}
