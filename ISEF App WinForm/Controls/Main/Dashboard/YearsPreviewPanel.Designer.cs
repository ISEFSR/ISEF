namespace cvti.isef.winformapp.Controls.Main.Dashboard
{
    partial class YearsPreviewPanel
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
            this.panelTitle = new System.Windows.Forms.Panel();
            this.buttonMenu = new System.Windows.Forms.Button();
            this.tableLayoutPanelTitle = new System.Windows.Forms.TableLayoutPanel();
            this.labelSubtitle = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.buttonFilters = new System.Windows.Forms.Button();
            this.pictureBoxInfo = new System.Windows.Forms.PictureBox();
            this.labelInfo = new System.Windows.Forms.Label();
            this.flowLayoutPanelYearsInfo = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanelInfo = new System.Windows.Forms.TableLayoutPanel();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStripSettings = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.načítajOdznovaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.získajDátaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelTitle.SuspendLayout();
            this.tableLayoutPanelTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfo)).BeginInit();
            this.tableLayoutPanelInfo.SuspendLayout();
            this.contextMenuStripSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitle
            // 
            this.panelTitle.BackColor = System.Drawing.Color.White;
            this.panelTitle.Controls.Add(this.buttonMenu);
            this.panelTitle.Controls.Add(this.tableLayoutPanelTitle);
            this.panelTitle.Controls.Add(this.buttonFilters);
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitle.Location = new System.Drawing.Point(0, 0);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Size = new System.Drawing.Size(220, 85);
            this.panelTitle.TabIndex = 1;
            // 
            // buttonMenu
            // 
            this.buttonMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonMenu.FlatAppearance.BorderSize = 0;
            this.buttonMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMenu.Image = global::cvti.isef.winformapp.Properties.Resources.hamburger;
            this.buttonMenu.Location = new System.Drawing.Point(196, 3);
            this.buttonMenu.Name = "buttonMenu";
            this.buttonMenu.Size = new System.Drawing.Size(21, 30);
            this.buttonMenu.TabIndex = 4;
            this.toolTipInfo.SetToolTip(this.buttonMenu, "Možnosti");
            this.buttonMenu.UseVisualStyleBackColor = true;
            this.buttonMenu.Click += new System.EventHandler(this.buttonMenu_Click);
            // 
            // tableLayoutPanelTitle
            // 
            this.tableLayoutPanelTitle.ColumnCount = 1;
            this.tableLayoutPanelTitle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelTitle.Controls.Add(this.labelSubtitle, 0, 1);
            this.tableLayoutPanelTitle.Controls.Add(this.labelTitle, 0, 0);
            this.tableLayoutPanelTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.tableLayoutPanelTitle.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelTitle.Name = "tableLayoutPanelTitle";
            this.tableLayoutPanelTitle.RowCount = 2;
            this.tableLayoutPanelTitle.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelTitle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelTitle.Size = new System.Drawing.Size(190, 85);
            this.tableLayoutPanelTitle.TabIndex = 3;
            // 
            // labelSubtitle
            // 
            this.labelSubtitle.AutoSize = true;
            this.labelSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(89)))), ((int)(((byte)(127)))));
            this.labelSubtitle.Location = new System.Drawing.Point(5, 26);
            this.labelSubtitle.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.labelSubtitle.Name = "labelSubtitle";
            this.labelSubtitle.Size = new System.Drawing.Size(149, 26);
            this.labelSubtitle.TabIndex = 2;
            this.labelSubtitle.Text = "Prehľad nahratých údajov v databáze aplikácie ISEF.";
            // 
            // labelTitle
            // 
            this.labelTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(89)))), ((int)(((byte)(127)))));
            this.labelTitle.Location = new System.Drawing.Point(5, 5);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(118, 21);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Prehľad rokov";
            // 
            // buttonFilters
            // 
            this.buttonFilters.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonFilters.Location = new System.Drawing.Point(255, 3);
            this.buttonFilters.Name = "buttonFilters";
            this.buttonFilters.Size = new System.Drawing.Size(21, 30);
            this.buttonFilters.TabIndex = 1;
            this.buttonFilters.Text = ":";
            this.buttonFilters.UseVisualStyleBackColor = true;
            // 
            // pictureBoxInfo
            // 
            this.pictureBoxInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxInfo.Image = global::cvti.isef.winformapp.Properties.Resources.info96;
            this.pictureBoxInfo.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxInfo.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxInfo.Name = "pictureBoxInfo";
            this.pictureBoxInfo.Size = new System.Drawing.Size(214, 112);
            this.pictureBoxInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxInfo.TabIndex = 2;
            this.pictureBoxInfo.TabStop = false;
            // 
            // labelInfo
            // 
            this.labelInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelInfo.ForeColor = System.Drawing.Color.Gray;
            this.labelInfo.Location = new System.Drawing.Point(3, 112);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(208, 113);
            this.labelInfo.TabIndex = 3;
            this.labelInfo.Text = "Vyzerá to tak, že v databáze nemáte nahrané žiadne dáta.";
            this.labelInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // flowLayoutPanelYearsInfo
            // 
            this.flowLayoutPanelYearsInfo.AutoScroll = true;
            this.flowLayoutPanelYearsInfo.Location = new System.Drawing.Point(3, 406);
            this.flowLayoutPanelYearsInfo.Name = "flowLayoutPanelYearsInfo";
            this.flowLayoutPanelYearsInfo.Size = new System.Drawing.Size(214, 100);
            this.flowLayoutPanelYearsInfo.TabIndex = 4;
            this.flowLayoutPanelYearsInfo.Visible = false;
            // 
            // tableLayoutPanelInfo
            // 
            this.tableLayoutPanelInfo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanelInfo.ColumnCount = 1;
            this.tableLayoutPanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelInfo.Controls.Add(this.pictureBoxInfo, 0, 0);
            this.tableLayoutPanelInfo.Controls.Add(this.labelInfo, 0, 1);
            this.tableLayoutPanelInfo.Location = new System.Drawing.Point(3, 143);
            this.tableLayoutPanelInfo.Name = "tableLayoutPanelInfo";
            this.tableLayoutPanelInfo.RowCount = 2;
            this.tableLayoutPanelInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelInfo.Size = new System.Drawing.Size(214, 225);
            this.tableLayoutPanelInfo.TabIndex = 5;
            // 
            // contextMenuStripSettings
            // 
            this.contextMenuStripSettings.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.načítajOdznovaToolStripMenuItem,
            this.získajDátaToolStripMenuItem});
            this.contextMenuStripSettings.Name = "contextMenuStripSettings";
            this.contextMenuStripSettings.Size = new System.Drawing.Size(160, 48);
            // 
            // načítajOdznovaToolStripMenuItem
            // 
            this.načítajOdznovaToolStripMenuItem.Name = "načítajOdznovaToolStripMenuItem";
            this.načítajOdznovaToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.načítajOdznovaToolStripMenuItem.Text = "Načítaj odznova";
            this.načítajOdznovaToolStripMenuItem.ToolTipText = "Odznova načíta údaje z databázy";
            this.načítajOdznovaToolStripMenuItem.Click += new System.EventHandler(this.načítajOdznovaToolStripMenuItem_Click);
            // 
            // získajDátaToolStripMenuItem
            // 
            this.získajDátaToolStripMenuItem.Name = "získajDátaToolStripMenuItem";
            this.získajDátaToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.získajDátaToolStripMenuItem.Text = "Získaj dáta";
            this.získajDátaToolStripMenuItem.ToolTipText = "Odznova získa stats a zobrazí data";
            this.získajDátaToolStripMenuItem.Click += new System.EventHandler(this.získajDátaToolStripMenuItem_Click);
            // 
            // YearsPreviewPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanelYearsInfo);
            this.Controls.Add(this.tableLayoutPanelInfo);
            this.Controls.Add(this.panelTitle);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "YearsPreviewPanel";
            this.Size = new System.Drawing.Size(220, 509);
            this.panelTitle.ResumeLayout(false);
            this.tableLayoutPanelTitle.ResumeLayout(false);
            this.tableLayoutPanelTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfo)).EndInit();
            this.tableLayoutPanelInfo.ResumeLayout(false);
            this.contextMenuStripSettings.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTitle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelTitle;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelSubtitle;
        private System.Windows.Forms.Button buttonFilters;
        private System.Windows.Forms.PictureBox pictureBoxInfo;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelYearsInfo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelInfo;
        private System.Windows.Forms.Button buttonMenu;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripSettings;
        private System.Windows.Forms.ToolStripMenuItem načítajOdznovaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem získajDátaToolStripMenuItem;
    }
}
