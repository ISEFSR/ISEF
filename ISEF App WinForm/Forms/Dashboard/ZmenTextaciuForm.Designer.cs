
namespace cvti.isef.winformapp.Forms.Dashboard
{
    partial class ZmenTextaciuForm
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
            this.panelContent = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.cueTextBoxRok = new cvti.isef.winformapp.Components.CueTextBox(this.components);
            this.cueTextBoxLong = new cvti.isef.winformapp.Components.CueTextBox(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.cueTextBoxKod = new cvti.isef.winformapp.Components.CueTextBox(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.cueTextBoxShort = new cvti.isef.winformapp.Components.CueTextBox(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.panelSeparator = new System.Windows.Forms.Panel();
            this.labelSubTitle = new System.Windows.Forms.Label();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.panelContent.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControlButtons
            // 
            this.panelControlButtons.Location = new System.Drawing.Point(0, 351);
            this.panelControlButtons.Size = new System.Drawing.Size(504, 50);
            // 
            // labelTitle
            // 
            this.labelTitle.Text = "Zmeň textáciu pre hodnotu číselníka";
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.tableLayoutPanel1);
            this.panelContent.Controls.Add(this.panelSeparator);
            this.panelContent.Controls.Add(this.labelSubTitle);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 85);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(504, 266);
            this.panelContent.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cueTextBoxRok, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cueTextBoxLong, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.cueTextBoxKod, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.cueTextBoxShort, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 81);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(480, 179);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Cursor = System.Windows.Forms.Cursors.Help;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Rok:";
            this.toolTipInfo.SetToolTip(this.label5, "Rok položky číselníka");
            // 
            // cueTextBoxRok
            // 
            this.cueTextBoxRok.Cue = null;
            this.cueTextBoxRok.Location = new System.Drawing.Point(3, 16);
            this.cueTextBoxRok.Name = "cueTextBoxRok";
            this.cueTextBoxRok.ReadOnly = true;
            this.cueTextBoxRok.Size = new System.Drawing.Size(141, 22);
            this.cueTextBoxRok.TabIndex = 7;
            // 
            // cueTextBoxLong
            // 
            this.cueTextBoxLong.Cue = null;
            this.cueTextBoxLong.Location = new System.Drawing.Point(3, 139);
            this.cueTextBoxLong.Name = "cueTextBoxLong";
            this.cueTextBoxLong.Size = new System.Drawing.Size(234, 22);
            this.cueTextBoxLong.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Help;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.label3.Location = new System.Drawing.Point(3, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Dlhý text";
            this.toolTipInfo.SetToolTip(this.label3, "Dlhý text pre položku číselníka");
            // 
            // cueTextBoxKod
            // 
            this.cueTextBoxKod.Cue = null;
            this.cueTextBoxKod.Location = new System.Drawing.Point(3, 57);
            this.cueTextBoxKod.Name = "cueTextBoxKod";
            this.cueTextBoxKod.ReadOnly = true;
            this.cueTextBoxKod.Size = new System.Drawing.Size(141, 22);
            this.cueTextBoxKod.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Cursor = System.Windows.Forms.Cursors.Help;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.label4.Location = new System.Drawing.Point(3, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Kód:";
            this.toolTipInfo.SetToolTip(this.label4, "Kód položky číselníka");
            // 
            // cueTextBoxShort
            // 
            this.cueTextBoxShort.Cue = null;
            this.cueTextBoxShort.Location = new System.Drawing.Point(3, 98);
            this.cueTextBoxShort.Name = "cueTextBoxShort";
            this.cueTextBoxShort.Size = new System.Drawing.Size(234, 22);
            this.cueTextBoxShort.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.Help;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.label2.Location = new System.Drawing.Point(3, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Krátky text:";
            this.toolTipInfo.SetToolTip(this.label2, "Krátky text položky číselníka");
            // 
            // panelSeparator
            // 
            this.panelSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.panelSeparator.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSeparator.Location = new System.Drawing.Point(0, 61);
            this.panelSeparator.Name = "panelSeparator";
            this.panelSeparator.Size = new System.Drawing.Size(504, 1);
            this.panelSeparator.TabIndex = 9;
            // 
            // labelSubTitle
            // 
            this.labelSubTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelSubTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelSubTitle.Location = new System.Drawing.Point(0, 0);
            this.labelSubTitle.Name = "labelSubTitle";
            this.labelSubTitle.Padding = new System.Windows.Forms.Padding(5);
            this.labelSubTitle.Size = new System.Drawing.Size(504, 61);
            this.labelSubTitle.TabIndex = 0;
            this.labelSubTitle.Text = "Zmena textácie položky s kódom {kod} pre rok {rok} číselníka {cis}.  Položka vzni" +
    "kla po nahraní dát a nenachádzala sa v predchádajúcich rokoch pre to nemá pridel" +
    "enú textáciu.";
            // 
            // ZmenTextaciuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 401);
            this.Controls.Add(this.panelContent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ZmenTextaciuForm";
            this.ShowButtonsPanel = true;
            this.ShowCancelButton = true;
            this.ShowImageIcon = true;
            this.ShowOkButton = true;
            this.Text = "Zmeň textáciu";
            this.TitleText = "Zmeň textáciu pre hodnotu číselníka";
            this.Controls.SetChildIndex(this.panelControlButtons, 0);
            this.Controls.SetChildIndex(this.panelContent, 0);
            this.panelContent.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label5;
        private Components.CueTextBox cueTextBoxRok;
        private Components.CueTextBox cueTextBoxLong;
        private System.Windows.Forms.Label label3;
        private Components.CueTextBox cueTextBoxKod;
        private System.Windows.Forms.Label label4;
        private Components.CueTextBox cueTextBoxShort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelSeparator;
        private System.Windows.Forms.Label labelSubTitle;
        private System.Windows.Forms.ToolTip toolTipInfo;
    }
}