namespace cvti.isef.winformapp.Forms
{
    partial class SaveGeneratedCondition
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveGeneratedCondition));
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxConditonName = new System.Windows.Forms.TextBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxConditionText = new System.Windows.Forms.TextBox();
            this.labelInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlButtons
            // 
            this.panelControlButtons.Location = new System.Drawing.Point(0, 392);
            this.panelControlButtons.Size = new System.Drawing.Size(523, 50);
            // 
            // labelTitle
            // 
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTitle.Size = new System.Drawing.Size(448, 75);
            this.labelTitle.Text = "Uloženie novej podmienky";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Help;
            this.label1.Location = new System.Drawing.Point(12, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Meno podmienky:";
            this.toolTipInfo.SetToolTip(this.label1, "Unikátne meno podmienky");
            // 
            // textBoxConditonName
            // 
            this.textBoxConditonName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxConditonName.Location = new System.Drawing.Point(15, 171);
            this.textBoxConditonName.Name = "textBoxConditonName";
            this.textBoxConditonName.Size = new System.Drawing.Size(256, 22);
            this.textBoxConditonName.TabIndex = 3;
            this.textBoxConditonName.TextChanged += new System.EventHandler(this.textBoxConditonName_TextChanged);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Help;
            this.label3.Location = new System.Drawing.Point(12, 202);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Generovaná podmienka:";
            this.toolTipInfo.SetToolTip(this.label3, "Generovaná SQL podmienka");
            // 
            // textBoxConditionText
            // 
            this.textBoxConditionText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxConditionText.BackColor = System.Drawing.Color.White;
            this.textBoxConditionText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxConditionText.Location = new System.Drawing.Point(12, 218);
            this.textBoxConditionText.Multiline = true;
            this.textBoxConditionText.Name = "textBoxConditionText";
            this.textBoxConditionText.ReadOnly = true;
            this.textBoxConditionText.Size = new System.Drawing.Size(496, 168);
            this.textBoxConditionText.TabIndex = 5;
            // 
            // labelInfo
            // 
            this.labelInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelInfo.Location = new System.Drawing.Point(0, 85);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Padding = new System.Windows.Forms.Padding(10);
            this.labelInfo.Size = new System.Drawing.Size(523, 64);
            this.labelInfo.TabIndex = 7;
            this.labelInfo.Text = resources.GetString("labelInfo.Text");
            // 
            // SaveGeneratedCondition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 442);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxConditionText);
            this.Controls.Add(this.textBoxConditonName);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.ImageIcon = global::cvti.isef.winformapp.Properties.Resources.sql75;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SaveGeneratedCondition";
            this.ShowButtonsPanel = true;
            this.ShowCancelButton = true;
            this.ShowImageIcon = true;
            this.ShowOkButton = true;
            this.Text = "Nová podmienka";
            this.TitleText = "Uloženie novej podmienky";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SaveGeneratedCondition_Paint);
            this.Controls.SetChildIndex(this.panelControlButtons, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.textBoxConditonName, 0);
            this.Controls.SetChildIndex(this.textBoxConditionText, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.labelInfo, 0);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxConditonName;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxConditionText;
        private System.Windows.Forms.Label labelInfo;
    }
}