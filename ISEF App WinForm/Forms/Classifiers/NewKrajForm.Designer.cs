namespace cvti.isef.winformapp.Forms.Classifiers
{
    partial class NewKrajForm
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
            this.components = new System.ComponentModel.Container();
            this.labelInfoText = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.cueTextBox1 = new cvti.isef.winformapp.Components.CueTextBox(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cueTextBox2 = new cvti.isef.winformapp.Components.CueTextBox(this.components);
            this.cueTextBox3 = new cvti.isef.winformapp.Components.CueTextBox(this.components);
            this.toolTipWarning = new System.Windows.Forms.ToolTip(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImageIcon)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTitle.Size = new System.Drawing.Size(429, 75);
            this.labelTitle.Text = "Zaevidovanie nového kraja";
            // 
            // panelControlButtons
            // 
            this.panelControlButtons.Location = new System.Drawing.Point(0, 346);
            this.panelControlButtons.Size = new System.Drawing.Size(504, 50);
            // 
            // labelInfoText
            // 
            this.labelInfoText.Location = new System.Drawing.Point(3, 0);
            this.labelInfoText.Name = "labelInfoText";
            this.labelInfoText.Padding = new System.Windows.Forms.Padding(10);
            this.labelInfoText.Size = new System.Drawing.Size(498, 70);
            this.labelInfoText.TabIndex = 2;
            this.labelInfoText.Text = "Evidencia nového kraja pre aplikáciu ISEF. Kraje pre aplikáciu niesú závislé na k" +
    "alendárnom roku. V aplikácii sa nachádza by default 9 krajov.";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelInfoText, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cueTextBox1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.cueTextBox2, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.cueTextBox3, 0, 6);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 85);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 9;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(504, 261);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Help;
            this.label1.Location = new System.Drawing.Point(10, 70);
            this.label1.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Kód kraja:";
            this.toolTipInfo.SetToolTip(this.label1, "Unikátny identifikátor pre kraj. Musí byť celé číslo");
            // 
            // cueTextBox1
            // 
            this.cueTextBox1.Cue = null;
            this.cueTextBox1.Location = new System.Drawing.Point(10, 86);
            this.cueTextBox1.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.cueTextBox1.Name = "cueTextBox1";
            this.cueTextBox1.Size = new System.Drawing.Size(192, 22);
            this.cueTextBox1.TabIndex = 1;
            this.cueTextBox1.TextChanged += new System.EventHandler(this.cueTextBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.Help;
            this.label2.Location = new System.Drawing.Point(10, 111);
            this.label2.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Názov kraja";
            this.toolTipInfo.SetToolTip(this.label2, "Skrátený názov pre kraj. Maximálny počet znakov je 2.");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Help;
            this.label3.Location = new System.Drawing.Point(10, 152);
            this.label3.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Popisné info kraja:";
            this.toolTipInfo.SetToolTip(this.label3, "Popisné info pre kraj. Maximálny počet znakov je 150.");
            // 
            // cueTextBox2
            // 
            this.cueTextBox2.Cue = null;
            this.cueTextBox2.Location = new System.Drawing.Point(10, 127);
            this.cueTextBox2.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.cueTextBox2.Name = "cueTextBox2";
            this.cueTextBox2.Size = new System.Drawing.Size(304, 22);
            this.cueTextBox2.TabIndex = 4;
            this.cueTextBox2.TextChanged += new System.EventHandler(this.cueTextBox2_TextChanged);
            // 
            // cueTextBox3
            // 
            this.cueTextBox3.Cue = null;
            this.cueTextBox3.Location = new System.Drawing.Point(10, 168);
            this.cueTextBox3.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.cueTextBox3.Multiline = true;
            this.cueTextBox3.Name = "cueTextBox3";
            this.cueTextBox3.Size = new System.Drawing.Size(304, 80);
            this.cueTextBox3.TabIndex = 5;
            this.cueTextBox3.TextChanged += new System.EventHandler(this.cueTextBox3_TextChanged);
            // 
            // toolTipWarning
            // 
            this.toolTipWarning.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            this.toolTipWarning.ToolTipTitle = "Pozor";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // toolTipInfo
            // 
            this.toolTipInfo.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // NewKrajForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 396);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewKrajForm";
            this.ShowButtonsPanel = true;
            this.ShowCancelButton = true;
            this.ShowImageIcon = true;
            this.ShowOkButton = true;
            this.Text = "Nový kraj";
            this.TitleText = "Zaevidovanie nového kraja";
            this.Controls.SetChildIndex(this.panelControlButtons, 0);
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImageIcon)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelInfoText;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private Components.CueTextBox cueTextBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Components.CueTextBox cueTextBox2;
        private Components.CueTextBox cueTextBox3;
        private System.Windows.Forms.ToolTip toolTipWarning;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ToolTip toolTipInfo;
    }
}