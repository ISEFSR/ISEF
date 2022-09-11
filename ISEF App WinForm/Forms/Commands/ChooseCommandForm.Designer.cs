
namespace cvti.isef.winformapp.Forms.Commands
{
    partial class ChooseCommandForm
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
            this.panelCommands = new System.Windows.Forms.Panel();
            this.listBoxCommands = new System.Windows.Forms.ListBox();
            this.panelCommands.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControlButtons
            // 
            this.panelControlButtons.Location = new System.Drawing.Point(0, 393);
            this.panelControlButtons.Size = new System.Drawing.Size(550, 50);
            // 
            // labelTitle
            // 
            this.labelTitle.Text = "Vyber SQL dotaz";
            // 
            // panelCommands
            // 
            this.panelCommands.Controls.Add(this.listBoxCommands);
            this.panelCommands.Location = new System.Drawing.Point(12, 91);
            this.panelCommands.Name = "panelCommands";
            this.panelCommands.Size = new System.Drawing.Size(528, 296);
            this.panelCommands.TabIndex = 3;
            this.panelCommands.Visible = false;
            // 
            // listBoxCommands
            // 
            this.listBoxCommands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxCommands.FormattingEnabled = true;
            this.listBoxCommands.Location = new System.Drawing.Point(0, 0);
            this.listBoxCommands.Name = "listBoxCommands";
            this.listBoxCommands.Size = new System.Drawing.Size(528, 296);
            this.listBoxCommands.TabIndex = 0;
            // 
            // ChooseCommandForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 443);
            this.Controls.Add(this.panelCommands);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChooseCommandForm";
            this.ShowButtonsPanel = true;
            this.ShowCancelButton = true;
            this.ShowImageIcon = true;
            this.ShowOkButton = true;
            this.Text = "Výber SQL dotazu";
            this.TitleText = "Vyber SQL dotaz";
            this.Controls.SetChildIndex(this.panelControlButtons, 0);
            this.Controls.SetChildIndex(this.panelCommands, 0);
            this.panelCommands.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelCommands;
        private System.Windows.Forms.ListBox listBoxCommands;
    }
}