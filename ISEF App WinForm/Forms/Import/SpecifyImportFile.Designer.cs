namespace cvti.isef.winformapp.Forms.Import
{
    partial class SpecifyImportFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpecifyImportFile));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownRow = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownColumn = new System.Windows.Forms.NumericUpDown();
            this.checkedListBoxWorksheets = new System.Windows.Forms.CheckedListBox();
            this.labelInfo = new System.Windows.Forms.Label();
            this.panelHorizontalSeparator = new System.Windows.Forms.Panel();
            this.numericUpDownHeader = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownColumn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeader)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlButtons
            // 
            this.panelControlButtons.Location = new System.Drawing.Point(0, 501);
            this.panelControlButtons.Size = new System.Drawing.Size(526, 50);
            // 
            // labelTitle
            // 
            this.labelTitle.Text = "Upresni parametre vstupného súboru";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDownRow, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDownColumn, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.checkedListBoxWorksheets, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDownHeader, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 157);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 10;
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(526, 344);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Cursor = System.Windows.Forms.Cursors.Help;
            this.label5.Location = new System.Drawing.Point(10, 185);
            this.label5.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(316, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Výber hárkov obsahujúcich údaje, ktoré sa budú importovať";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Cursor = System.Windows.Forms.Cursors.Help;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.label4.Location = new System.Drawing.Point(10, 168);
            this.label4.Margin = new System.Windows.Forms.Padding(10, 10, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Výber hárkov";
            // 
            // numericUpDownRow
            // 
            this.numericUpDownRow.Location = new System.Drawing.Point(10, 89);
            this.numericUpDownRow.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.numericUpDownRow.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownRow.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownRow.Name = "numericUpDownRow";
            this.numericUpDownRow.Size = new System.Drawing.Size(120, 22);
            this.numericUpDownRow.TabIndex = 6;
            this.numericUpDownRow.Value = new decimal(new int[] {
            21,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Help;
            this.label3.Location = new System.Drawing.Point(10, 73);
            this.label3.Margin = new System.Windows.Forms.Padding(10, 5, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(324, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Index riadku na ktorom začínajú údaje pre import (one based)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.Help;
            this.label2.Location = new System.Drawing.Point(10, 27);
            this.label2.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(328, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Index stĺpca na ktorom začúinajú údaje pre import (one based)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Help;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(10, 10, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Začiatok údajov";
            // 
            // numericUpDownColumn
            // 
            this.numericUpDownColumn.Location = new System.Drawing.Point(10, 43);
            this.numericUpDownColumn.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.numericUpDownColumn.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownColumn.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownColumn.Name = "numericUpDownColumn";
            this.numericUpDownColumn.Size = new System.Drawing.Size(120, 22);
            this.numericUpDownColumn.TabIndex = 5;
            this.numericUpDownColumn.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // checkedListBoxWorksheets
            // 
            this.checkedListBoxWorksheets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBoxWorksheets.FormattingEnabled = true;
            this.checkedListBoxWorksheets.Location = new System.Drawing.Point(10, 201);
            this.checkedListBoxWorksheets.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.checkedListBoxWorksheets.Name = "checkedListBoxWorksheets";
            this.checkedListBoxWorksheets.Size = new System.Drawing.Size(506, 140);
            this.checkedListBoxWorksheets.TabIndex = 7;
            // 
            // labelInfo
            // 
            this.labelInfo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.labelInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelInfo.Location = new System.Drawing.Point(0, 85);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Padding = new System.Windows.Forms.Padding(10, 5, 10, 0);
            this.labelInfo.Size = new System.Drawing.Size(526, 71);
            this.labelInfo.TabIndex = 2;
            this.labelInfo.Text = resources.GetString("labelInfo.Text");
            // 
            // panelHorizontalSeparator
            // 
            this.panelHorizontalSeparator.BackColor = System.Drawing.Color.Gainsboro;
            this.panelHorizontalSeparator.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHorizontalSeparator.Location = new System.Drawing.Point(0, 156);
            this.panelHorizontalSeparator.Name = "panelHorizontalSeparator";
            this.panelHorizontalSeparator.Size = new System.Drawing.Size(526, 1);
            this.panelHorizontalSeparator.TabIndex = 3;
            // 
            // numericUpDownHeader
            // 
            this.numericUpDownHeader.Location = new System.Drawing.Point(10, 135);
            this.numericUpDownHeader.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.numericUpDownHeader.Name = "numericUpDownHeader";
            this.numericUpDownHeader.Size = new System.Drawing.Size(120, 22);
            this.numericUpDownHeader.TabIndex = 8;
            this.numericUpDownHeader.Value = new decimal(new int[] {
            19,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 119);
            this.label6.Margin = new System.Windows.Forms.Padding(10, 5, 3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(353, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Index riadku na ktorom je hlavička v importnom súbore (one based)";
            // 
            // SpecifyImportFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 551);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panelHorizontalSeparator);
            this.Controls.Add(this.labelInfo);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ImageIcon = global::cvti.isef.winformapp.Properties.Resources.excel75_white;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(542, 590);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(542, 590);
            this.Name = "SpecifyImportFile";
            this.ShowButtonsPanel = true;
            this.ShowCancelButton = true;
            this.ShowImageIcon = true;
            this.ShowOkButton = true;
            this.Text = "Špecifikácia importného śuboru";
            this.TitleText = "Upresni parametre vstupného súboru";
            this.Controls.SetChildIndex(this.panelControlButtons, 0);
            this.Controls.SetChildIndex(this.labelInfo, 0);
            this.Controls.SetChildIndex(this.panelHorizontalSeparator, 0);
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownColumn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeader)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownRow;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownColumn;
        private System.Windows.Forms.CheckedListBox checkedListBoxWorksheets;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Panel panelHorizontalSeparator;
        private System.Windows.Forms.NumericUpDown numericUpDownHeader;
        private System.Windows.Forms.Label label6;
    }
}