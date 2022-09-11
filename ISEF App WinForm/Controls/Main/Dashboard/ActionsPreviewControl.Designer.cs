namespace cvti.isef.winformapp.Controls.Main.Dashboard
{
    partial class ActionsPreviewControl
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonFilters = new System.Windows.Forms.Button();
            this.flowLayoutPanelActions = new System.Windows.Forms.FlowLayoutPanel();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStripMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.načítajOdznovaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionControl1 = new cvti.isef.winformapp.Controls.Main.Dashboard.ActionControl();
            this.actionControl2 = new cvti.isef.winformapp.Controls.Main.Dashboard.ActionControl();
            this.actionControl3 = new cvti.isef.winformapp.Controls.Main.Dashboard.ActionControl();
            this.actionControl4 = new cvti.isef.winformapp.Controls.Main.Dashboard.ActionControl();
            this.actionControl5 = new cvti.isef.winformapp.Controls.Main.Dashboard.ActionControl();
            this.actionControl6 = new cvti.isef.winformapp.Controls.Main.Dashboard.ActionControl();
            this.actionControl7 = new cvti.isef.winformapp.Controls.Main.Dashboard.ActionControl();
            this.actionControl8 = new cvti.isef.winformapp.Controls.Main.Dashboard.ActionControl();
            this.actionControl9 = new cvti.isef.winformapp.Controls.Main.Dashboard.ActionControl();
            this.actionControl10 = new cvti.isef.winformapp.Controls.Main.Dashboard.ActionControl();
            this.panelTitle.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanelActions.SuspendLayout();
            this.contextMenuStripMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitle
            // 
            this.panelTitle.BackColor = System.Drawing.Color.White;
            this.panelTitle.Controls.Add(this.tableLayoutPanel1);
            this.panelTitle.Controls.Add(this.buttonFilters);
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitle.Location = new System.Drawing.Point(0, 0);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Size = new System.Drawing.Size(279, 85);
            this.panelTitle.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.labelTitle, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.tableLayoutPanel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(89)))), ((int)(((byte)(127)))));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(249, 85);
            this.tableLayoutPanel1.TabIndex = 3;
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
            this.labelTitle.Size = new System.Drawing.Size(162, 25);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Posledná aktivita";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(89)))), ((int)(((byte)(127)))));
            this.label1.Location = new System.Drawing.Point(5, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(241, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "Posledne zaznamenaná aktivita používateľlov aplikácie";
            // 
            // buttonFilters
            // 
            this.buttonFilters.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonFilters.FlatAppearance.BorderSize = 0;
            this.buttonFilters.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFilters.Image = global::cvti.isef.winformapp.Properties.Resources.hamburger;
            this.buttonFilters.Location = new System.Drawing.Point(255, 3);
            this.buttonFilters.Name = "buttonFilters";
            this.buttonFilters.Size = new System.Drawing.Size(21, 30);
            this.buttonFilters.TabIndex = 1;
            this.toolTipInfo.SetToolTip(this.buttonFilters, "Možnosti");
            this.buttonFilters.UseVisualStyleBackColor = true;
            this.buttonFilters.Click += new System.EventHandler(this.buttonFilters_Click);
            // 
            // flowLayoutPanelActions
            // 
            this.flowLayoutPanelActions.AutoScroll = true;
            this.flowLayoutPanelActions.Controls.Add(this.actionControl1);
            this.flowLayoutPanelActions.Controls.Add(this.actionControl2);
            this.flowLayoutPanelActions.Controls.Add(this.actionControl3);
            this.flowLayoutPanelActions.Controls.Add(this.actionControl4);
            this.flowLayoutPanelActions.Controls.Add(this.actionControl5);
            this.flowLayoutPanelActions.Controls.Add(this.actionControl6);
            this.flowLayoutPanelActions.Controls.Add(this.actionControl7);
            this.flowLayoutPanelActions.Controls.Add(this.actionControl8);
            this.flowLayoutPanelActions.Controls.Add(this.actionControl9);
            this.flowLayoutPanelActions.Controls.Add(this.actionControl10);
            this.flowLayoutPanelActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelActions.Location = new System.Drawing.Point(0, 85);
            this.flowLayoutPanelActions.Name = "flowLayoutPanelActions";
            this.flowLayoutPanelActions.Size = new System.Drawing.Size(279, 504);
            this.flowLayoutPanelActions.TabIndex = 1;
            // 
            // contextMenuStripMenu
            // 
            this.contextMenuStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.načítajOdznovaToolStripMenuItem});
            this.contextMenuStripMenu.Name = "contextMenuStripMenu";
            this.contextMenuStripMenu.Size = new System.Drawing.Size(160, 26);
            // 
            // načítajOdznovaToolStripMenuItem
            // 
            this.načítajOdznovaToolStripMenuItem.Name = "načítajOdznovaToolStripMenuItem";
            this.načítajOdznovaToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.načítajOdznovaToolStripMenuItem.Text = "Načítaj odznova";
            this.načítajOdznovaToolStripMenuItem.Click += new System.EventHandler(this.načítajOdznovaToolStripMenuItem_Click);
            // 
            // actionControl1
            // 
            this.actionControl1.BackColor = System.Drawing.Color.White;
            this.actionControl1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.actionControl1.Location = new System.Drawing.Point(3, 3);
            this.actionControl1.Name = "actionControl1";
            this.actionControl1.Size = new System.Drawing.Size(246, 152);
            this.actionControl1.TabIndex = 0;
            // 
            // actionControl2
            // 
            this.actionControl2.BackColor = System.Drawing.Color.White;
            this.actionControl2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.actionControl2.Location = new System.Drawing.Point(3, 161);
            this.actionControl2.Name = "actionControl2";
            this.actionControl2.Size = new System.Drawing.Size(246, 152);
            this.actionControl2.TabIndex = 1;
            // 
            // actionControl3
            // 
            this.actionControl3.BackColor = System.Drawing.Color.White;
            this.actionControl3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.actionControl3.Location = new System.Drawing.Point(3, 319);
            this.actionControl3.Name = "actionControl3";
            this.actionControl3.Size = new System.Drawing.Size(246, 152);
            this.actionControl3.TabIndex = 2;
            // 
            // actionControl4
            // 
            this.actionControl4.BackColor = System.Drawing.Color.White;
            this.actionControl4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.actionControl4.Location = new System.Drawing.Point(3, 477);
            this.actionControl4.Name = "actionControl4";
            this.actionControl4.Size = new System.Drawing.Size(246, 152);
            this.actionControl4.TabIndex = 3;
            // 
            // actionControl5
            // 
            this.actionControl5.BackColor = System.Drawing.Color.White;
            this.actionControl5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.actionControl5.Location = new System.Drawing.Point(3, 635);
            this.actionControl5.Name = "actionControl5";
            this.actionControl5.Size = new System.Drawing.Size(246, 152);
            this.actionControl5.TabIndex = 4;
            // 
            // actionControl6
            // 
            this.actionControl6.BackColor = System.Drawing.Color.White;
            this.actionControl6.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.actionControl6.Location = new System.Drawing.Point(3, 793);
            this.actionControl6.Name = "actionControl6";
            this.actionControl6.Size = new System.Drawing.Size(246, 152);
            this.actionControl6.TabIndex = 5;
            // 
            // actionControl7
            // 
            this.actionControl7.BackColor = System.Drawing.Color.White;
            this.actionControl7.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.actionControl7.Location = new System.Drawing.Point(3, 951);
            this.actionControl7.Name = "actionControl7";
            this.actionControl7.Size = new System.Drawing.Size(246, 152);
            this.actionControl7.TabIndex = 6;
            // 
            // actionControl8
            // 
            this.actionControl8.BackColor = System.Drawing.Color.White;
            this.actionControl8.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.actionControl8.Location = new System.Drawing.Point(3, 1109);
            this.actionControl8.Name = "actionControl8";
            this.actionControl8.Size = new System.Drawing.Size(246, 152);
            this.actionControl8.TabIndex = 7;
            // 
            // actionControl9
            // 
            this.actionControl9.BackColor = System.Drawing.Color.White;
            this.actionControl9.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.actionControl9.Location = new System.Drawing.Point(3, 1267);
            this.actionControl9.Name = "actionControl9";
            this.actionControl9.Size = new System.Drawing.Size(246, 152);
            this.actionControl9.TabIndex = 8;
            // 
            // actionControl10
            // 
            this.actionControl10.BackColor = System.Drawing.Color.White;
            this.actionControl10.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.actionControl10.Location = new System.Drawing.Point(3, 1425);
            this.actionControl10.Name = "actionControl10";
            this.actionControl10.Size = new System.Drawing.Size(246, 152);
            this.actionControl10.TabIndex = 9;
            // 
            // ActionsPreviewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanelActions);
            this.Controls.Add(this.panelTitle);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "ActionsPreviewControl";
            this.Size = new System.Drawing.Size(279, 589);
            this.panelTitle.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanelActions.ResumeLayout(false);
            this.contextMenuStripMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTitle;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelActions;
        private ActionControl actionControl1;
        private ActionControl actionControl2;
        private System.Windows.Forms.Label labelTitle;
        private ActionControl actionControl3;
        private ActionControl actionControl4;
        private ActionControl actionControl5;
        private ActionControl actionControl6;
        private ActionControl actionControl7;
        private ActionControl actionControl8;
        private ActionControl actionControl9;
        private ActionControl actionControl10;
        private System.Windows.Forms.Button buttonFilters;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMenu;
        private System.Windows.Forms.ToolStripMenuItem načítajOdznovaToolStripMenuItem;
    }
}
