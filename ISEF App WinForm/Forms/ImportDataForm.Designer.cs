namespace cvti.isef.winformapp.Forms
{
    partial class ImportDataForm
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
            this.importDataMain = new cvti.isef.winformapp.Controls.Main.Import.ImportDataMain();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImageIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxImageIcon
            // 
            this.pictureBoxImageIcon.Image = global::cvti.isef.winformapp.Properties.Resources.sql75;
            // 
            // labelTitle
            // 
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTitle.Size = new System.Drawing.Size(1050, 75);
            this.labelTitle.Text = "ISEF Import údajov výber stupňa";
            // 
            // panelControlButtons
            // 
            this.panelControlButtons.Location = new System.Drawing.Point(0, 697);
            this.panelControlButtons.Size = new System.Drawing.Size(1125, 50);
            // 
            // importDataMain
            // 
            this.importDataMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.importDataMain.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.importDataMain.Location = new System.Drawing.Point(0, 85);
            this.importDataMain.Manager = null;
            this.importDataMain.Name = "importDataMain";
            this.importDataMain.Size = new System.Drawing.Size(1125, 612);
            this.importDataMain.TabIndex = 0;
            this.importDataMain.CloseMe += new System.EventHandler(this.importDataMain_CloseMe);
            // 
            // ImportDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1125, 747);
            this.Controls.Add(this.importDataMain);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.ImageIcon = global::cvti.isef.winformapp.Properties.Resources.sql75;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1141, 786);
            this.Name = "ImportDataForm";
            this.ShowButtonsPanel = true;
            this.ShowCancelButton = true;
            this.ShowImageIcon = true;
            this.ShowOkButton = true;
            this.Text = "ISEF Import údajov";
            this.TitleText = "ISEF Import údajov výber stupňa";
            this.Controls.SetChildIndex(this.panelControlButtons, 0);
            this.Controls.SetChildIndex(this.importDataMain, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImageIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.Main.Import.ImportDataMain importDataMain;
    }
}