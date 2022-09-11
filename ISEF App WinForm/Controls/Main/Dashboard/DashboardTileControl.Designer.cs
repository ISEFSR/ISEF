namespace cvti.isef.winformapp.Controls.Main.Dashboard
{
    partial class DashboardTileControl
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
            this.components = new System.ComponentModel.Container();
            this.panelContent = new System.Windows.Forms.Panel();
            this.tableLayoutPanelPreview = new System.Windows.Forms.TableLayoutPanel();
            this.linkLabelImport = new System.Windows.Forms.LinkLabel();
            this.panelColorLine = new System.Windows.Forms.Panel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.labelInfo = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.panelTitle = new System.Windows.Forms.Panel();
            this.dashbardTilePreviewControl = new cvti.isef.winformapp.Controls.Main.Dashboard.DashbardTilePreviewControl();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.panelContent.SuspendLayout();
            this.tableLayoutPanelPreview.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContent
            // 
            this.panelContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelContent.Controls.Add(this.tableLayoutPanelPreview);
            this.panelContent.Controls.Add(this.panelTitle);
            this.panelContent.Controls.Add(this.dashbardTilePreviewControl);
            this.panelContent.Location = new System.Drawing.Point(10, 10);
            this.panelContent.Margin = new System.Windows.Forms.Padding(10);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(200, 280);
            this.panelContent.TabIndex = 0;
            // 
            // tableLayoutPanelPreview
            // 
            this.tableLayoutPanelPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelPreview.ColumnCount = 1;
            this.tableLayoutPanelPreview.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelPreview.Controls.Add(this.linkLabelImport, 0, 5);
            this.tableLayoutPanelPreview.Controls.Add(this.panelColorLine, 0, 0);
            this.tableLayoutPanelPreview.Controls.Add(this.linkLabel2, 0, 4);
            this.tableLayoutPanelPreview.Controls.Add(this.linkLabel1, 0, 3);
            this.tableLayoutPanelPreview.Controls.Add(this.labelInfo, 0, 2);
            this.tableLayoutPanelPreview.Controls.Add(this.labelTitle, 0, 1);
            this.tableLayoutPanelPreview.Location = new System.Drawing.Point(0, 115);
            this.tableLayoutPanelPreview.Name = "tableLayoutPanelPreview";
            this.tableLayoutPanelPreview.RowCount = 6;
            this.tableLayoutPanelPreview.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanelPreview.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelPreview.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelPreview.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelPreview.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelPreview.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelPreview.Size = new System.Drawing.Size(199, 164);
            this.tableLayoutPanelPreview.TabIndex = 3;
            // 
            // linkLabelImport
            // 
            this.linkLabelImport.AutoSize = true;
            this.linkLabelImport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLabelImport.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabelImport.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(72)))), ((int)(((byte)(144)))));
            this.linkLabelImport.Location = new System.Drawing.Point(3, 141);
            this.linkLabelImport.Margin = new System.Windows.Forms.Padding(3, 0, 0, 10);
            this.linkLabelImport.Name = "linkLabelImport";
            this.linkLabelImport.Size = new System.Drawing.Size(86, 13);
            this.linkLabelImport.TabIndex = 5;
            this.linkLabelImport.TabStop = true;
            this.linkLabelImport.Text = "Importuj data...";
            this.toolTipInfo.SetToolTip(this.linkLabelImport, "Otvorí dialógové okno umožňujúce import údajov");
            this.linkLabelImport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelImport_LinkClicked);
            // 
            // panelColorLine
            // 
            this.panelColorLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(89)))), ((int)(((byte)(127)))));
            this.panelColorLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelColorLine.Location = new System.Drawing.Point(0, 0);
            this.panelColorLine.Margin = new System.Windows.Forms.Padding(0);
            this.panelColorLine.Name = "panelColorLine";
            this.panelColorLine.Size = new System.Drawing.Size(199, 5);
            this.panelColorLine.TabIndex = 1;
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLabel2.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabel2.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(72)))), ((int)(((byte)(144)))));
            this.linkLabel2.Location = new System.Drawing.Point(3, 125);
            this.linkLabel2.Margin = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(81, 13);
            this.linkLabel2.TabIndex = 4;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Zobraz náhľad";
            this.toolTipInfo.SetToolTip(this.linkLabel2, "Zobrazí náhľad pre vybraný stupeň");
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabel1.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(72)))), ((int)(((byte)(144)))));
            this.linkLabel1.Location = new System.Drawing.Point(3, 109);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(3, 5, 0, 3);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(68, 13);
            this.linkLabel1.TabIndex = 3;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Zobraz data";
            this.toolTipInfo.SetToolTip(this.linkLabel1, "Prepne sa na záložku dáta a zobrazí údaje pre stupeň");
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // labelInfo
            // 
            this.labelInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelInfo.Location = new System.Drawing.Point(7, 43);
            this.labelInfo.Margin = new System.Windows.Forms.Padding(7, 3, 3, 3);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(189, 58);
            this.labelInfo.TabIndex = 1;
            this.labelInfo.Text = "Groom forever, stretch tongue and leave it slightly out, blep cat ass trophy drin" +
    "k water out of the faucet at four in the morning.";
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(89)))), ((int)(((byte)(127)))));
            this.labelTitle.Location = new System.Drawing.Point(7, 12);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(7);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(88, 21);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Some title";
            // 
            // panelTitle
            // 
            this.panelTitle.BackgroundImage = global::cvti.isef.winformapp.Properties.Resources.notfound200x115;
            this.panelTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitle.Location = new System.Drawing.Point(0, 0);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Size = new System.Drawing.Size(198, 115);
            this.panelTitle.TabIndex = 0;
            // 
            // dashbardTilePreviewControl
            // 
            this.dashbardTilePreviewControl.Enabled = false;
            this.dashbardTilePreviewControl.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dashbardTilePreviewControl.Location = new System.Drawing.Point(198, 118);
            this.dashbardTilePreviewControl.Name = "dashbardTilePreviewControl";
            this.dashbardTilePreviewControl.SelectedYear = 0;
            this.dashbardTilePreviewControl.Size = new System.Drawing.Size(198, 188);
            this.dashbardTilePreviewControl.TabIndex = 4;
            this.dashbardTilePreviewControl.BackClicked += new System.EventHandler(this.dashbardTilePreviewControl_BackClicked);
            // 
            // DashboardTileControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelContent);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.MaximumSize = new System.Drawing.Size(220, 300);
            this.MinimumSize = new System.Drawing.Size(220, 300);
            this.Name = "DashboardTileControl";
            this.Size = new System.Drawing.Size(220, 300);
            this.panelContent.ResumeLayout(false);
            this.tableLayoutPanelPreview.ResumeLayout(false);
            this.tableLayoutPanelPreview.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Panel panelTitle;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelPreview;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private DashbardTilePreviewControl dashbardTilePreviewControl;
        private System.Windows.Forms.Panel panelColorLine;
        private System.Windows.Forms.LinkLabel linkLabelImport;
        private System.Windows.Forms.ToolTip toolTipInfo;
    }
}
