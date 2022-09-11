namespace cvti.isef.winformapp.Forms
{
    partial class ChooseConditionOperator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseConditionOperator));
            this.comboBoxOperator = new System.Windows.Forms.ComboBox();
            this.labelRightHandSide = new System.Windows.Forms.Label();
            this.labelInfo = new System.Windows.Forms.Label();
            this.labelLeftHandSide = new System.Windows.Forms.Label();
            this.linkLabelOperatorInfo = new System.Windows.Forms.LinkLabel();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // panelControlButtons
            // 
            this.panelControlButtons.Size = new System.Drawing.Size(528, 50);
            // 
            // labelTitle
            // 
            this.labelTitle.Text = "Vyber operátor podmienky";
            // 
            // comboBoxOperator
            // 
            this.comboBoxOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOperator.FormattingEnabled = true;
            this.comboBoxOperator.Items.AddRange(new object[] {
            "And",
            "Or",
            "And Not",
            "Or Not"});
            this.comboBoxOperator.Location = new System.Drawing.Point(15, 208);
            this.comboBoxOperator.Name = "comboBoxOperator";
            this.comboBoxOperator.Size = new System.Drawing.Size(160, 21);
            this.comboBoxOperator.TabIndex = 2;
            // 
            // labelRightHandSide
            // 
            this.labelRightHandSide.AutoEllipsis = true;
            this.labelRightHandSide.AutoSize = true;
            this.labelRightHandSide.Location = new System.Drawing.Point(12, 232);
            this.labelRightHandSide.Name = "labelRightHandSide";
            this.labelRightHandSide.Size = new System.Drawing.Size(163, 13);
            this.labelRightHandSide.TabIndex = 3;
            this.labelRightHandSide.Text = "ISEF_TEST.dbo.FK3.Kod = \'096\'";
            // 
            // labelInfo
            // 
            this.labelInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelInfo.Location = new System.Drawing.Point(0, 85);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Padding = new System.Windows.Forms.Padding(10);
            this.labelInfo.Size = new System.Drawing.Size(528, 75);
            this.labelInfo.TabIndex = 4;
            this.labelInfo.Text = resources.GetString("labelInfo.Text");
            // 
            // labelLeftHandSide
            // 
            this.labelLeftHandSide.AutoEllipsis = true;
            this.labelLeftHandSide.AutoSize = true;
            this.labelLeftHandSide.Cursor = System.Windows.Forms.Cursors.Help;
            this.labelLeftHandSide.Location = new System.Drawing.Point(12, 192);
            this.labelLeftHandSide.Name = "labelLeftHandSide";
            this.labelLeftHandSide.Size = new System.Drawing.Size(163, 13);
            this.labelLeftHandSide.TabIndex = 5;
            this.labelLeftHandSide.Text = "ISEF_TEST.dbo.FK3.Kod = \'094\'";
            // 
            // linkLabelOperatorInfo
            // 
            this.linkLabelOperatorInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.linkLabelOperatorInfo.Location = new System.Drawing.Point(0, 160);
            this.linkLabelOperatorInfo.Name = "linkLabelOperatorInfo";
            this.linkLabelOperatorInfo.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.linkLabelOperatorInfo.Size = new System.Drawing.Size(528, 20);
            this.linkLabelOperatorInfo.TabIndex = 6;
            this.linkLabelOperatorInfo.TabStop = true;
            this.linkLabelOperatorInfo.Text = "https://www.w3schools.com/sql/sql_operators.asp";
            this.linkLabelOperatorInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelOperatorInfo_LinkClicked);
            // 
            // ChooseConditionOperator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 321);
            this.Controls.Add(this.linkLabelOperatorInfo);
            this.Controls.Add(this.labelLeftHandSide);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.labelRightHandSide);
            this.Controls.Add(this.comboBoxOperator);
            this.ImageIcon = global::cvti.isef.winformapp.Properties.Resources.sql751;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChooseConditionOperator";
            this.ShowButtonsPanel = true;
            this.ShowImageIcon = true;
            this.Text = "Operátor";
            this.TitleText = "Vyber operátor podmienky";
            this.Controls.SetChildIndex(this.panelControlButtons, 0);
            this.Controls.SetChildIndex(this.comboBoxOperator, 0);
            this.Controls.SetChildIndex(this.labelRightHandSide, 0);
            this.Controls.SetChildIndex(this.labelInfo, 0);
            this.Controls.SetChildIndex(this.labelLeftHandSide, 0);
            this.Controls.SetChildIndex(this.linkLabelOperatorInfo, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxOperator;
        private System.Windows.Forms.Label labelRightHandSide;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Label labelLeftHandSide;
        private System.Windows.Forms.LinkLabel linkLabelOperatorInfo;
        private System.Windows.Forms.ToolTip toolTipInfo;
    }
}