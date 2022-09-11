
namespace cvti.isef.winformapp.Forms.Conditions
{
    partial class ChooseConditionForm
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
            this.panelPodmienka = new System.Windows.Forms.Panel();
            this.listBoxConditions = new System.Windows.Forms.ListBox();
            this.panelPodmienka.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControlButtons
            // 
            this.panelControlButtons.Location = new System.Drawing.Point(0, 393);
            this.panelControlButtons.Size = new System.Drawing.Size(550, 50);
            // 
            // labelTitle
            // 
            this.labelTitle.Text = "Vyber podmienku ";
            // 
            // panelPodmienka
            // 
            this.panelPodmienka.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPodmienka.Controls.Add(this.listBoxConditions);
            this.panelPodmienka.Location = new System.Drawing.Point(12, 91);
            this.panelPodmienka.Name = "panelPodmienka";
            this.panelPodmienka.Size = new System.Drawing.Size(528, 296);
            this.panelPodmienka.TabIndex = 3;
            this.panelPodmienka.Visible = false;
            // 
            // listBoxConditions
            // 
            this.listBoxConditions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxConditions.FormattingEnabled = true;
            this.listBoxConditions.Location = new System.Drawing.Point(0, 0);
            this.listBoxConditions.Name = "listBoxConditions";
            this.listBoxConditions.Size = new System.Drawing.Size(528, 296);
            this.listBoxConditions.TabIndex = 0;
            // 
            // ChooseConditionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 443);
            this.Controls.Add(this.panelPodmienka);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChooseConditionForm";
            this.ShowButtonsPanel = true;
            this.ShowCancelButton = true;
            this.ShowImageIcon = true;
            this.ShowOkButton = true;
            this.Text = "Výber podmienky";
            this.TitleText = "Vyber podmienku ";
            this.Controls.SetChildIndex(this.panelControlButtons, 0);
            this.Controls.SetChildIndex(this.panelPodmienka, 0);
            this.panelPodmienka.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelPodmienka;
        private System.Windows.Forms.ListBox listBoxConditions;
    }
}