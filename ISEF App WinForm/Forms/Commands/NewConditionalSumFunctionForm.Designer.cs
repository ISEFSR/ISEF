
namespace cvti.isef.winformapp.Forms.Commands
{
    partial class NewConditionalSumFunctionForm
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
            this.panelConditions = new System.Windows.Forms.Panel();
            this.listBoxConditions = new System.Windows.Forms.ListBox();
            this.panelConditions.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControlButtons
            // 
            this.panelControlButtons.Location = new System.Drawing.Point(0, 377);
            this.panelControlButtons.Size = new System.Drawing.Size(630, 50);
            // 
            // labelTitle
            // 
            this.labelTitle.Text = "Nová SUMIF funkcia - výber podmienky";
            // 
            // panelConditions
            // 
            this.panelConditions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelConditions.Controls.Add(this.listBoxConditions);
            this.panelConditions.Location = new System.Drawing.Point(12, 91);
            this.panelConditions.Name = "panelConditions";
            this.panelConditions.Size = new System.Drawing.Size(606, 280);
            this.panelConditions.TabIndex = 3;
            // 
            // listBoxConditions
            // 
            this.listBoxConditions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxConditions.FormattingEnabled = true;
            this.listBoxConditions.Location = new System.Drawing.Point(0, 0);
            this.listBoxConditions.Name = "listBoxConditions";
            this.listBoxConditions.Size = new System.Drawing.Size(606, 280);
            this.listBoxConditions.TabIndex = 0;
            // 
            // NewConditionalSumFunctionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 427);
            this.Controls.Add(this.panelConditions);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewConditionalSumFunctionForm";
            this.ShowButtonsPanel = true;
            this.ShowCancelButton = true;
            this.ShowImageIcon = true;
            this.ShowOkButton = true;
            this.Text = "Nová SUMIF funkcia";
            this.TitleText = "Nová SUMIF funkcia - výber podmienky";
            this.Controls.SetChildIndex(this.panelControlButtons, 0);
            this.Controls.SetChildIndex(this.panelConditions, 0);
            this.panelConditions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelConditions;
        private System.Windows.Forms.ListBox listBoxConditions;
    }
}