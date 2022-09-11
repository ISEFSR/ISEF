namespace cvti.isef.winformapp.Forms.Conditions
{
    partial class ImportConditionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportConditionsForm));
            this.checkedListBoxConditions = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panelControlButtons
            // 
            this.panelControlButtons.Location = new System.Drawing.Point(0, 386);
            this.panelControlButtons.Size = new System.Drawing.Size(626, 50);
            // 
            // labelTitle
            // 
            this.labelTitle.Text = "Importuj podmienky z JSON súboru";
            // 
            // checkedListBoxConditions
            // 
            this.checkedListBoxConditions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxConditions.FormattingEnabled = true;
            this.checkedListBoxConditions.Location = new System.Drawing.Point(19, 182);
            this.checkedListBoxConditions.Margin = new System.Windows.Forms.Padding(10);
            this.checkedListBoxConditions.Name = "checkedListBoxConditions";
            this.checkedListBoxConditions.Size = new System.Drawing.Size(588, 191);
            this.checkedListBoxConditions.TabIndex = 46;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.label2.Location = new System.Drawing.Point(0, 85);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.label2.Size = new System.Drawing.Size(626, 87);
            this.label2.TabIndex = 45;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // ImportConditionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 436);
            this.Controls.Add(this.checkedListBoxConditions);
            this.Controls.Add(this.label2);
            this.ImageIcon = global::cvti.isef.winformapp.Properties.Resources.importjson50;
            this.Name = "ImportConditionsForm";
            this.ShowButtonsPanel = true;
            this.ShowCancelButton = true;
            this.ShowImageIcon = true;
            this.ShowOkButton = true;
            this.Text = "Importuj podmineky";
            this.TitleText = "Importuj podmienky z JSON súboru";
            this.Controls.SetChildIndex(this.panelControlButtons, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.checkedListBoxConditions, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBoxConditions;
        private System.Windows.Forms.Label label2;
    }
}