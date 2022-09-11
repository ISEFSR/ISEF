namespace cvti.isef.winformapp.Forms
{
    partial class NewColumnControl
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
            this.comboBoxColumns = new System.Windows.Forms.ComboBox();
            this.checkBoxSum = new System.Windows.Forms.CheckBox();
            this.checkBoxConditionalSum = new System.Windows.Forms.CheckBox();
            this.comboBoxConditions = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxAlias = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImageIcon)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxImageIcon
            // 
            this.pictureBoxImageIcon.Image = global::cvti.isef.winformapp.Properties.Resources.add_clmn64;
            // 
            // labelTitle
            // 
            this.labelTitle.Text = "Nový dátový stĺpec";
            // 
            // panelControlButtons
            // 
            this.panelControlButtons.Location = new System.Drawing.Point(0, 364);
            this.panelControlButtons.Size = new System.Drawing.Size(504, 50);
            // 
            // comboBoxColumns
            // 
            this.comboBoxColumns.FormattingEnabled = true;
            this.comboBoxColumns.Location = new System.Drawing.Point(10, 16);
            this.comboBoxColumns.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.comboBoxColumns.Name = "comboBoxColumns";
            this.comboBoxColumns.Size = new System.Drawing.Size(186, 21);
            this.comboBoxColumns.TabIndex = 2;
            this.comboBoxColumns.SelectedIndexChanged += new System.EventHandler(this.comboBoxColumns_SelectedIndexChanged);
            // 
            // checkBoxSum
            // 
            this.checkBoxSum.AutoSize = true;
            this.checkBoxSum.Location = new System.Drawing.Point(10, 84);
            this.checkBoxSum.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.checkBoxSum.Name = "checkBoxSum";
            this.checkBoxSum.Size = new System.Drawing.Size(54, 17);
            this.checkBoxSum.TabIndex = 3;
            this.checkBoxSum.Text = "Suma";
            this.toolTipInfo.SetToolTip(this.checkBoxSum, "Aplikuj sumačnú funkciu na stĺpec");
            this.checkBoxSum.UseVisualStyleBackColor = true;
            this.checkBoxSum.CheckedChanged += new System.EventHandler(this.checkBoxSum_CheckedChanged);
            // 
            // checkBoxConditionalSum
            // 
            this.checkBoxConditionalSum.AutoSize = true;
            this.checkBoxConditionalSum.Enabled = false;
            this.checkBoxConditionalSum.Location = new System.Drawing.Point(10, 107);
            this.checkBoxConditionalSum.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.checkBoxConditionalSum.Name = "checkBoxConditionalSum";
            this.checkBoxConditionalSum.Size = new System.Drawing.Size(120, 17);
            this.checkBoxConditionalSum.TabIndex = 4;
            this.checkBoxConditionalSum.Text = "Podmienená suma";
            this.toolTipInfo.SetToolTip(this.checkBoxConditionalSum, "Aplikuj výberovú podmienku na sumu");
            this.checkBoxConditionalSum.UseVisualStyleBackColor = true;
            this.checkBoxConditionalSum.CheckedChanged += new System.EventHandler(this.checkBoxConditionalSum_CheckedChanged);
            // 
            // comboBoxConditions
            // 
            this.comboBoxConditions.Enabled = false;
            this.comboBoxConditions.FormattingEnabled = true;
            this.comboBoxConditions.Location = new System.Drawing.Point(10, 130);
            this.comboBoxConditions.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.comboBoxConditions.Name = "comboBoxConditions";
            this.comboBoxConditions.Size = new System.Drawing.Size(186, 21);
            this.comboBoxConditions.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 85);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(10);
            this.label2.Size = new System.Drawing.Size(504, 70);
            this.label2.TabIndex = 7;
            this.label2.Text = "Umožnuje zadefinovať a pridať nový stĺpec do výberu. Stĺpec je možné vybrať z poh" +
    "ľadu ASSU. Na každý stĺpec je možné aplikovať ľubovolné funkcie.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Help;
            this.label3.Location = new System.Drawing.Point(10, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Dátový stĺpec:";
            this.toolTipInfo.SetToolTip(this.label3, "Stĺpec zo vstupných dát");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Cursor = System.Windows.Forms.Cursors.Help;
            this.label4.Location = new System.Drawing.Point(10, 40);
            this.label4.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Alias stĺpca:";
            this.toolTipInfo.SetToolTip(this.label4, "Alias stĺpca zo vstupných dát");
            // 
            // textBoxAlias
            // 
            this.textBoxAlias.Location = new System.Drawing.Point(10, 56);
            this.textBoxAlias.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.textBoxAlias.Name = "textBoxAlias";
            this.textBoxAlias.Size = new System.Drawing.Size(186, 22);
            this.textBoxAlias.TabIndex = 10;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBoxAlias, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxConditions, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxColumns, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxConditionalSum, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxSum, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 155);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(504, 209);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // NewColumnControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 414);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ImageIcon = global::cvti.isef.winformapp.Properties.Resources.add_clmn64;
            this.Name = "NewColumnControl";
            this.ShowButtonsPanel = true;
            this.ShowCancelButton = true;
            this.ShowImageIcon = true;
            this.ShowOkButton = true;
            this.Text = "Nový stĺpec";
            this.TitleText = "Nový dátový stĺpec";
            this.Controls.SetChildIndex(this.panelControlButtons, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImageIcon)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBoxColumns;
        private System.Windows.Forms.CheckBox checkBoxSum;
        private System.Windows.Forms.CheckBox checkBoxConditionalSum;
        private System.Windows.Forms.ComboBox comboBoxConditions;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxAlias;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}