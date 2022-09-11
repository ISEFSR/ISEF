
namespace cvti.isef.winformapp.Forms
{
    partial class ZobrazCommandForm
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
            this.scintilla1 = new ScintillaNET.Scintilla();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImageIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxImageIcon
            // 
            this.pictureBoxImageIcon.Image = global::cvti.isef.winformapp.Properties.Resources.sql75;
            // 
            // labelTitle
            // 
            this.labelTitle.Text = "Prehľad SQL SELECT dotazu";
            // 
            // panelControlButtons
            // 
            this.panelControlButtons.Location = new System.Drawing.Point(0, 511);
            this.panelControlButtons.Size = new System.Drawing.Size(784, 50);
            this.panelControlButtons.Visible = false;
            // 
            // scintilla1
            // 
            this.scintilla1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scintilla1.Location = new System.Drawing.Point(12, 91);
            this.scintilla1.Name = "scintilla1";
            this.scintilla1.Size = new System.Drawing.Size(760, 414);
            this.scintilla1.TabIndex = 3;
            this.scintilla1.Text = "scintilla1";
            // 
            // ZobrazCommandForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.scintilla1);
            this.ImageIcon = global::cvti.isef.winformapp.Properties.Resources.sql75;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "ZobrazCommandForm";
            this.ShowButtonsPanel = true;
            this.ShowCancelButton = true;
            this.ShowImageIcon = true;
            this.ShowOkButton = true;
            this.Text = "SQL SELECT dotaz";
            this.TitleText = "Prehľad SQL SELECT dotazu";
            this.Controls.SetChildIndex(this.panelControlButtons, 0);
            this.Controls.SetChildIndex(this.scintilla1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImageIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ScintillaNET.Scintilla scintilla1;
    }
}