namespace cvti.isef.winformapp.Controls.Main.Dashboard
{
    partial class WarningPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanelContent = new System.Windows.Forms.FlowLayoutPanel();
            this.singleWarning1 = new cvti.isef.winformapp.Controls.Main.Dashboard.SingleWarning();
            this.singleWarning2 = new cvti.isef.winformapp.Controls.Main.Dashboard.SingleWarning();
            this.singleWarning3 = new cvti.isef.winformapp.Controls.Main.Dashboard.SingleWarning();
            this.singleWarning4 = new cvti.isef.winformapp.Controls.Main.Dashboard.SingleWarning();
            this.labelInfo = new System.Windows.Forms.Label();
            this.flowLayoutPanelContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanelContent
            // 
            this.flowLayoutPanelContent.AutoScroll = true;
            this.flowLayoutPanelContent.Controls.Add(this.singleWarning1);
            this.flowLayoutPanelContent.Controls.Add(this.singleWarning2);
            this.flowLayoutPanelContent.Controls.Add(this.singleWarning3);
            this.flowLayoutPanelContent.Controls.Add(this.singleWarning4);
            this.flowLayoutPanelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelContent.Location = new System.Drawing.Point(0, 41);
            this.flowLayoutPanelContent.Name = "flowLayoutPanelContent";
            this.flowLayoutPanelContent.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.flowLayoutPanelContent.Size = new System.Drawing.Size(900, 174);
            this.flowLayoutPanelContent.TabIndex = 0;
            this.flowLayoutPanelContent.WrapContents = false;
            // 
            // singleWarning1
            // 
            this.singleWarning1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.singleWarning1.Location = new System.Drawing.Point(5, 5);
            this.singleWarning1.Margin = new System.Windows.Forms.Padding(5);
            this.singleWarning1.Name = "singleWarning1";
            this.singleWarning1.Size = new System.Drawing.Size(225, 141);
            this.singleWarning1.TabIndex = 0;
            // 
            // singleWarning2
            // 
            this.singleWarning2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.singleWarning2.Location = new System.Drawing.Point(240, 5);
            this.singleWarning2.Margin = new System.Windows.Forms.Padding(5);
            this.singleWarning2.Name = "singleWarning2";
            this.singleWarning2.Size = new System.Drawing.Size(225, 141);
            this.singleWarning2.TabIndex = 1;
            // 
            // singleWarning3
            // 
            this.singleWarning3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.singleWarning3.Location = new System.Drawing.Point(475, 5);
            this.singleWarning3.Margin = new System.Windows.Forms.Padding(5);
            this.singleWarning3.Name = "singleWarning3";
            this.singleWarning3.Size = new System.Drawing.Size(225, 141);
            this.singleWarning3.TabIndex = 2;
            // 
            // singleWarning4
            // 
            this.singleWarning4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.singleWarning4.Location = new System.Drawing.Point(710, 5);
            this.singleWarning4.Margin = new System.Windows.Forms.Padding(5);
            this.singleWarning4.Name = "singleWarning4";
            this.singleWarning4.Size = new System.Drawing.Size(225, 141);
            this.singleWarning4.TabIndex = 3;
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelInfo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(89)))), ((int)(((byte)(127)))));
            this.labelInfo.Location = new System.Drawing.Point(0, 0);
            this.labelInfo.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Padding = new System.Windows.Forms.Padding(10, 10, 0, 10);
            this.labelInfo.Size = new System.Drawing.Size(503, 41);
            this.labelInfo.TabIndex = 1;
            this.labelInfo.Tag = "Našiel som {x} položiek v číselníkoch s nepriradenou textáciou";
            this.labelInfo.Text = "Našiel som 999 položiek v číselníkoch s nepriradenou textáciou";
            // 
            // WarningPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanelContent);
            this.Controls.Add(this.labelInfo);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "WarningPanel";
            this.Size = new System.Drawing.Size(900, 215);
            this.flowLayoutPanelContent.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelContent;
        private SingleWarning singleWarning1;
        private SingleWarning singleWarning2;
        private SingleWarning singleWarning3;
        private SingleWarning singleWarning4;
        private System.Windows.Forms.Label labelInfo;
    }
}
