namespace cvti.isef.winformapp.Controls.Setup
{
    partial class ApplicationSetupControl3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationSetupControl3));
            this.labelInfo = new System.Windows.Forms.Label();
            this.tableLayoutPanelFiles = new System.Windows.Forms.TableLayoutPanel();
            this.comboBoxVystup = new System.Windows.Forms.ComboBox();
            this.comboBoxHlavicky = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxData = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tableLayoutPanelFiles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelInfo
            // 
            this.labelInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelInfo.Location = new System.Drawing.Point(0, 0);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Padding = new System.Windows.Forms.Padding(10);
            this.labelInfo.Size = new System.Drawing.Size(754, 87);
            this.labelInfo.TabIndex = 6;
            this.labelInfo.Text = resources.GetString("labelInfo.Text");
            // 
            // tableLayoutPanelFiles
            // 
            this.tableLayoutPanelFiles.ColumnCount = 2;
            this.tableLayoutPanelFiles.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelFiles.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelFiles.Controls.Add(this.comboBoxVystup, 1, 1);
            this.tableLayoutPanelFiles.Controls.Add(this.comboBoxHlavicky, 1, 0);
            this.tableLayoutPanelFiles.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanelFiles.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanelFiles.Controls.Add(this.comboBoxData, 1, 2);
            this.tableLayoutPanelFiles.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanelFiles.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanelFiles.Location = new System.Drawing.Point(0, 87);
            this.tableLayoutPanelFiles.Name = "tableLayoutPanelFiles";
            this.tableLayoutPanelFiles.RowCount = 7;
            this.tableLayoutPanelFiles.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelFiles.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelFiles.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelFiles.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelFiles.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelFiles.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelFiles.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelFiles.Size = new System.Drawing.Size(754, 118);
            this.tableLayoutPanelFiles.TabIndex = 7;
            // 
            // comboBoxVystup
            // 
            this.comboBoxVystup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxVystup.FormattingEnabled = true;
            this.comboBoxVystup.Location = new System.Drawing.Point(178, 30);
            this.comboBoxVystup.Name = "comboBoxVystup";
            this.comboBoxVystup.Size = new System.Drawing.Size(333, 21);
            this.comboBoxVystup.TabIndex = 10;
            // 
            // comboBoxHlavicky
            // 
            this.comboBoxHlavicky.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxHlavicky.FormattingEnabled = true;
            this.comboBoxHlavicky.Location = new System.Drawing.Point(178, 3);
            this.comboBoxHlavicky.Name = "comboBoxHlavicky";
            this.comboBoxHlavicky.Size = new System.Drawing.Size(333, 21);
            this.comboBoxHlavicky.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Help;
            this.label3.Location = new System.Drawing.Point(3, 54);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(10, 5, 50, 0);
            this.label3.Size = new System.Drawing.Size(152, 18);
            this.label3.TabIndex = 12;
            this.label3.Text = "Adresár pre dáta";
            this.toolTip1.SetToolTip(this.label3, "Adresár pre dátové súbory");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.Help;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(10, 5, 50, 0);
            this.label2.Size = new System.Drawing.Size(169, 18);
            this.label2.TabIndex = 11;
            this.label2.Text = "Adresár pre hlavičky";
            this.toolTip1.SetToolTip(this.label2, "Adresár pre hlavičkové súbory. V tomto adresáre budú uložené hlaivčky pre aplikác" +
        "iu");
            // 
            // comboBoxData
            // 
            this.comboBoxData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxData.FormattingEnabled = true;
            this.comboBoxData.Location = new System.Drawing.Point(178, 57);
            this.comboBoxData.Name = "comboBoxData";
            this.comboBoxData.Size = new System.Drawing.Size(333, 21);
            this.comboBoxData.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Help;
            this.label1.Location = new System.Drawing.Point(3, 27);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(10, 5, 50, 0);
            this.label1.Size = new System.Drawing.Size(167, 18);
            this.label1.TabIndex = 8;
            this.label1.Text = "Adresár pre výstupy";
            this.toolTip1.SetToolTip(this.label1, "Adresár pre výstupné súbory. V tomto adresáry budú ukladané ročenkové výstupy");
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ApplicationSetupControl3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanelFiles);
            this.Controls.Add(this.labelInfo);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ApplicationSetupControl3";
            this.Size = new System.Drawing.Size(754, 489);
            this.tableLayoutPanelFiles.ResumeLayout(false);
            this.tableLayoutPanelFiles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelFiles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxData;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxHlavicky;
        private System.Windows.Forms.ComboBox comboBoxVystup;
    }
}
