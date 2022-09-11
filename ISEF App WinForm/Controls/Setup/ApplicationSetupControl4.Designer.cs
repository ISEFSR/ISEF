namespace cvti.isef.winformapp.Controls.Setup
{
    partial class ApplicationSetupControl4
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
            this.labelInfo = new System.Windows.Forms.Label();
            this.tableLayoutPanelData = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.labelServer = new System.Windows.Forms.Label();
            this.labelDatabase = new System.Windows.Forms.Label();
            this.labelPrihlasenie = new System.Windows.Forms.Label();
            this.labelHeaderDir = new System.Windows.Forms.Label();
            this.labelOutputDir = new System.Windows.Forms.Label();
            this.labelDataDir = new System.Windows.Forms.Label();
            this.tableLayoutPanelData.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelInfo
            // 
            this.labelInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelInfo.Location = new System.Drawing.Point(0, 0);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Padding = new System.Windows.Forms.Padding(10);
            this.labelInfo.Size = new System.Drawing.Size(767, 87);
            this.labelInfo.TabIndex = 7;
            this.labelInfo.Text = "Aplikácia teraz bude pokračovať v nastavovaní podľa Vami zadaných údajov. Tento p" +
    "roces môže trvať pár minút počkajte prosím kým aplikácie nastaví všetko potrebné" +
    ". Nižšie vidíte sumarizáciu nastavnení.";
            // 
            // tableLayoutPanelData
            // 
            this.tableLayoutPanelData.ColumnCount = 2;
            this.tableLayoutPanelData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelData.Controls.Add(this.labelDataDir, 1, 5);
            this.tableLayoutPanelData.Controls.Add(this.labelOutputDir, 1, 4);
            this.tableLayoutPanelData.Controls.Add(this.labelHeaderDir, 1, 3);
            this.tableLayoutPanelData.Controls.Add(this.labelPrihlasenie, 1, 2);
            this.tableLayoutPanelData.Controls.Add(this.labelDatabase, 1, 1);
            this.tableLayoutPanelData.Controls.Add(this.labelServer, 1, 0);
            this.tableLayoutPanelData.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanelData.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanelData.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanelData.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanelData.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanelData.Controls.Add(this.label6, 0, 5);
            this.tableLayoutPanelData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelData.Location = new System.Drawing.Point(0, 87);
            this.tableLayoutPanelData.Name = "tableLayoutPanelData";
            this.tableLayoutPanelData.RowCount = 7;
            this.tableLayoutPanelData.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelData.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelData.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelData.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelData.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelData.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelData.Size = new System.Drawing.Size(767, 344);
            this.tableLayoutPanelData.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(10, 2, 15, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "MSSQL Server:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 19);
            this.label2.Margin = new System.Windows.Forms.Padding(10, 2, 15, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Databáza";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 36);
            this.label3.Margin = new System.Windows.Forms.Padding(10, 2, 15, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Spôsob prihlásenia";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 53);
            this.label4.Margin = new System.Windows.Forms.Padding(10, 2, 15, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Adresár pre hlavičky";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 70);
            this.label5.Margin = new System.Windows.Forms.Padding(10, 2, 15, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Adresár pre výstupy";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 87);
            this.label6.Margin = new System.Windows.Forms.Padding(10, 2, 15, 2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Adresár pre dáta";
            // 
            // labelServer
            // 
            this.labelServer.AutoSize = true;
            this.labelServer.Location = new System.Drawing.Point(144, 2);
            this.labelServer.Margin = new System.Windows.Forms.Padding(10, 2, 15, 2);
            this.labelServer.Name = "labelServer";
            this.labelServer.Size = new System.Drawing.Size(79, 13);
            this.labelServer.TabIndex = 6;
            this.labelServer.Text = "MSSQL Server:";
            // 
            // labelDatabase
            // 
            this.labelDatabase.AutoSize = true;
            this.labelDatabase.Location = new System.Drawing.Point(144, 19);
            this.labelDatabase.Margin = new System.Windows.Forms.Padding(10, 2, 15, 2);
            this.labelDatabase.Name = "labelDatabase";
            this.labelDatabase.Size = new System.Drawing.Size(79, 13);
            this.labelDatabase.TabIndex = 7;
            this.labelDatabase.Text = "MSSQL Server:";
            // 
            // labelPrihlasenie
            // 
            this.labelPrihlasenie.AutoSize = true;
            this.labelPrihlasenie.Location = new System.Drawing.Point(144, 36);
            this.labelPrihlasenie.Margin = new System.Windows.Forms.Padding(10, 2, 15, 2);
            this.labelPrihlasenie.Name = "labelPrihlasenie";
            this.labelPrihlasenie.Size = new System.Drawing.Size(79, 13);
            this.labelPrihlasenie.TabIndex = 8;
            this.labelPrihlasenie.Text = "MSSQL Server:";
            // 
            // labelHeaderDir
            // 
            this.labelHeaderDir.AutoSize = true;
            this.labelHeaderDir.Location = new System.Drawing.Point(144, 53);
            this.labelHeaderDir.Margin = new System.Windows.Forms.Padding(10, 2, 15, 2);
            this.labelHeaderDir.Name = "labelHeaderDir";
            this.labelHeaderDir.Size = new System.Drawing.Size(79, 13);
            this.labelHeaderDir.TabIndex = 9;
            this.labelHeaderDir.Text = "MSSQL Server:";
            // 
            // labelOutputDir
            // 
            this.labelOutputDir.AutoSize = true;
            this.labelOutputDir.Location = new System.Drawing.Point(144, 70);
            this.labelOutputDir.Margin = new System.Windows.Forms.Padding(10, 2, 15, 2);
            this.labelOutputDir.Name = "labelOutputDir";
            this.labelOutputDir.Size = new System.Drawing.Size(79, 13);
            this.labelOutputDir.TabIndex = 10;
            this.labelOutputDir.Text = "MSSQL Server:";
            // 
            // labelDataDir
            // 
            this.labelDataDir.AutoSize = true;
            this.labelDataDir.Location = new System.Drawing.Point(144, 87);
            this.labelDataDir.Margin = new System.Windows.Forms.Padding(10, 2, 15, 2);
            this.labelDataDir.Name = "labelDataDir";
            this.labelDataDir.Size = new System.Drawing.Size(79, 13);
            this.labelDataDir.TabIndex = 11;
            this.labelDataDir.Text = "MSSQL Server:";
            // 
            // ApplicationSetupControl4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanelData);
            this.Controls.Add(this.labelInfo);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ApplicationSetupControl4";
            this.Size = new System.Drawing.Size(767, 431);
            this.tableLayoutPanelData.ResumeLayout(false);
            this.tableLayoutPanelData.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelData;
        private System.Windows.Forms.Label labelDataDir;
        private System.Windows.Forms.Label labelOutputDir;
        private System.Windows.Forms.Label labelHeaderDir;
        private System.Windows.Forms.Label labelPrihlasenie;
        private System.Windows.Forms.Label labelDatabase;
        private System.Windows.Forms.Label labelServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}
