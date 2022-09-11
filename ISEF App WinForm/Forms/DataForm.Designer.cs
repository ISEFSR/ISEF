namespace cvti.isef.winformapp.Forms
{
    partial class DataForm
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
            this.dataGridViewData = new System.Windows.Forms.DataGridView();
            this.textBoxSelectCommand = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStripActions = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonExportSelected = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonShowSQL = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.panelSelectCommand = new System.Windows.Forms.Panel();
            this.linkLabelClipboard = new System.Windows.Forms.LinkLabel();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewData)).BeginInit();
            this.toolStripActions.SuspendLayout();
            this.panelSelectCommand.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControlButtons
            // 
            this.panelControlButtons.Location = new System.Drawing.Point(0, 547);
            this.panelControlButtons.Size = new System.Drawing.Size(813, 50);
            // 
            // labelTitle
            // 
            this.labelTitle.Text = "Prehľad vybraných údajov";
            // 
            // dataGridViewData
            // 
            this.dataGridViewData.AllowUserToAddRows = false;
            this.dataGridViewData.AllowUserToDeleteRows = false;
            this.dataGridViewData.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewData.ColumnHeadersHeight = 35;
            this.dataGridViewData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dataGridViewData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewData.GridColor = System.Drawing.Color.LightGray;
            this.dataGridViewData.Location = new System.Drawing.Point(0, 128);
            this.dataGridViewData.Name = "dataGridViewData";
            this.dataGridViewData.RowHeadersVisible = false;
            this.dataGridViewData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewData.Size = new System.Drawing.Size(813, 419);
            this.dataGridViewData.TabIndex = 6;
            this.dataGridViewData.Visible = false;
            this.dataGridViewData.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridViewData_RowPostPaint);
            this.dataGridViewData.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dataGridViewData_MouseMove);
            // 
            // textBoxSelectCommand
            // 
            this.textBoxSelectCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSelectCommand.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxSelectCommand.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxSelectCommand.Location = new System.Drawing.Point(15, 53);
            this.textBoxSelectCommand.Multiline = true;
            this.textBoxSelectCommand.Name = "textBoxSelectCommand";
            this.textBoxSelectCommand.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxSelectCommand.Size = new System.Drawing.Size(468, 550);
            this.textBoxSelectCommand.TabIndex = 18;
            this.textBoxSelectCommand.Visible = false;
            this.textBoxSelectCommand.Leave += new System.EventHandler(this.textBoxSelectCommand_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(5, 10, 0, 0);
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 19;
            this.label1.Text = "SQL select príkaz:";
            // 
            // toolStripActions
            // 
            this.toolStripActions.AutoSize = false;
            this.toolStripActions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonExportSelected,
            this.toolStripSeparator1,
            this.toolStripButtonShowSQL,
            this.toolStripSeparator2,
            this.toolStripButton1});
            this.toolStripActions.Location = new System.Drawing.Point(0, 85);
            this.toolStripActions.Name = "toolStripActions";
            this.toolStripActions.Size = new System.Drawing.Size(813, 43);
            this.toolStripActions.TabIndex = 21;
            this.toolStripActions.Text = "toolStrip1";
            // 
            // toolStripButtonExportSelected
            // 
            this.toolStripButtonExportSelected.Image = global::cvti.isef.winformapp.Properties.Resources.excel48;
            this.toolStripButtonExportSelected.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExportSelected.Name = "toolStripButtonExportSelected";
            this.toolStripButtonExportSelected.Size = new System.Drawing.Size(90, 40);
            this.toolStripButtonExportSelected.Text = "Export XLSX";
            this.toolStripButtonExportSelected.ToolTipText = "Exportuje vybraný číselník pre vybraný kalendárny rok do XLSX súboru.";
            this.toolStripButtonExportSelected.Click += new System.EventHandler(this.toolStripButtonExportSelected_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 43);
            // 
            // toolStripButtonShowSQL
            // 
            this.toolStripButtonShowSQL.Image = global::cvti.isef.winformapp.Properties.Resources.icons8_analyze_48;
            this.toolStripButtonShowSQL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonShowSQL.Name = "toolStripButtonShowSQL";
            this.toolStripButtonShowSQL.Size = new System.Drawing.Size(87, 40);
            this.toolStripButtonShowSQL.Text = "Zobraz SQL";
            this.toolStripButtonShowSQL.ToolTipText = "Exportuje vybraný číselník pre vybraný kalendárny rok do XLSX súboru.";
            this.toolStripButtonShowSQL.Click += new System.EventHandler(this.toolStripButtonShowSQL_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 43);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::cvti.isef.winformapp.Properties.Resources.save501;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(84, 40);
            this.toolStripButton1.Text = "Ulož príkaz";
            this.toolStripButton1.ToolTipText = "Uloží ako nový SQL príkaz";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // panelSelectCommand
            // 
            this.panelSelectCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSelectCommand.Controls.Add(this.linkLabelClipboard);
            this.panelSelectCommand.Controls.Add(this.label1);
            this.panelSelectCommand.Controls.Add(this.textBoxSelectCommand);
            this.panelSelectCommand.Location = new System.Drawing.Point(819, 0);
            this.panelSelectCommand.Name = "panelSelectCommand";
            this.panelSelectCommand.Size = new System.Drawing.Size(479, 597);
            this.panelSelectCommand.TabIndex = 22;
            this.panelSelectCommand.Visible = false;
            this.panelSelectCommand.Paint += new System.Windows.Forms.PaintEventHandler(this.panelSelectCommand_Paint);
            // 
            // linkLabelClipboard
            // 
            this.linkLabelClipboard.AutoSize = true;
            this.linkLabelClipboard.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabelClipboard.Location = new System.Drawing.Point(12, 32);
            this.linkLabelClipboard.Name = "linkLabelClipboard";
            this.linkLabelClipboard.Padding = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.linkLabelClipboard.Size = new System.Drawing.Size(131, 18);
            this.linkLabelClipboard.TabIndex = 20;
            this.linkLabelClipboard.TabStop = true;
            this.linkLabelClipboard.Text = "Skopíruj do clipboardu";
            this.linkLabelClipboard.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelClipboard_LinkClicked);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.FileName = "output";
            this.saveFileDialog.Filter = "XLSX súbor|*.xlsx";
            // 
            // DataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 597);
            this.Controls.Add(this.dataGridViewData);
            this.Controls.Add(this.panelSelectCommand);
            this.Controls.Add(this.toolStripActions);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ImageIcon = global::cvti.isef.winformapp.Properties.Resources.sql75;
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "DataForm";
            this.ShowButtonsPanel = true;
            this.ShowCancelButton = true;
            this.ShowImageIcon = true;
            this.ShowOkButton = true;
            this.Text = "ISEF Prehľad";
            this.TitleText = "Prehľad vybraných údajov";
            this.Controls.SetChildIndex(this.panelControlButtons, 0);
            this.Controls.SetChildIndex(this.toolStripActions, 0);
            this.Controls.SetChildIndex(this.panelSelectCommand, 0);
            this.Controls.SetChildIndex(this.dataGridViewData, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewData)).EndInit();
            this.toolStripActions.ResumeLayout(false);
            this.toolStripActions.PerformLayout();
            this.panelSelectCommand.ResumeLayout(false);
            this.panelSelectCommand.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridViewData;
        private System.Windows.Forms.TextBox textBoxSelectCommand;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStripActions;
        private System.Windows.Forms.Panel panelSelectCommand;
        private System.Windows.Forms.LinkLabel linkLabelClipboard;
        private System.Windows.Forms.ToolStripButton toolStripButtonExportSelected;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonShowSQL;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}