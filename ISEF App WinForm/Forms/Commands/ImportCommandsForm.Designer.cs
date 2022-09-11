namespace cvti.isef.winformapp.Forms.Commands
{
    partial class ImportCommandsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportCommandsForm));
            this.label2 = new System.Windows.Forms.Label();
            this.checkedListBoxCommands = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // panelControlButtons
            // 
            this.panelControlButtons.Location = new System.Drawing.Point(0, 391);
            this.panelControlButtons.Size = new System.Drawing.Size(624, 50);
            // 
            // labelTitle
            // 
            this.labelTitle.Text = "Importuj príkazy z JSON súboru";
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.label2.Location = new System.Drawing.Point(0, 85);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(10, 5, 10, 0);
            this.label2.Size = new System.Drawing.Size(624, 60);
            this.label2.TabIndex = 40;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // checkedListBoxCommands
            // 
            this.checkedListBoxCommands.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxCommands.FormattingEnabled = true;
            this.checkedListBoxCommands.Location = new System.Drawing.Point(19, 155);
            this.checkedListBoxCommands.Margin = new System.Windows.Forms.Padding(10);
            this.checkedListBoxCommands.Name = "checkedListBoxCommands";
            this.checkedListBoxCommands.Size = new System.Drawing.Size(586, 225);
            this.checkedListBoxCommands.TabIndex = 41;
            // 
            // ImportCommandsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.checkedListBoxCommands);
            this.Controls.Add(this.label2);
            this.ImageIcon = global::cvti.isef.winformapp.Properties.Resources.importjson50;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(640, 480);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "ImportCommandsForm";
            this.ShowButtonsPanel = true;
            this.ShowCancelButton = true;
            this.ShowImageIcon = true;
            this.ShowOkButton = true;
            this.Text = "Import príkazov";
            this.TitleText = "Importuj príkazy z JSON súboru";
            this.Controls.SetChildIndex(this.panelControlButtons, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.checkedListBoxCommands, 0);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox checkedListBoxCommands;
    }
}