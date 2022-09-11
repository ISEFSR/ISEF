namespace cvti.isef.winformapp.Forms
{
    partial class NovaHlavickaForm
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
            this.verticalProgress = new cvti.isef.winformapp.Controls.Main.Import.VerticalProgress();
            this.SuspendLayout();
            // 
            // panelControlButtons
            // 
            this.panelControlButtons.Location = new System.Drawing.Point(0, 550);
            this.panelControlButtons.Size = new System.Drawing.Size(816, 50);
            // 
            // verticalProgress
            // 
            this.verticalProgress.Dock = System.Windows.Forms.DockStyle.Left;
            this.verticalProgress.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.verticalProgress.Location = new System.Drawing.Point(0, 85);
            this.verticalProgress.Name = "verticalProgress";
            this.verticalProgress.ShowNumber = true;
            this.verticalProgress.Size = new System.Drawing.Size(222, 464);
            this.verticalProgress.TabIndex = 3;
            // 
            // NovaHlavickaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 600);
            this.Controls.Add(this.verticalProgress);
            this.Name = "NovaHlavickaForm";
            this.ShowButtonsPanel = true;
            this.ShowCancelButton = true;
            this.ShowImageIcon = true;
            this.ShowOkButton = true;
            this.Text = "NovaHlavickaForm";
            this.Controls.SetChildIndex(this.panelControlButtons, 0);
            this.Controls.SetChildIndex(this.verticalProgress, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.Main.Import.VerticalProgress verticalProgress;
    }
}