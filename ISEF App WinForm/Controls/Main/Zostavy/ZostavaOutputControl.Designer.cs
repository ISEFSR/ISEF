namespace cvti.isef.winformapp.Controls.Output
{
    partial class ZostavaOutputControl
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
            this.pictureBoxStatus = new System.Windows.Forms.PictureBox();
            this.labelName = new System.Windows.Forms.Label();
            this.labelType = new System.Windows.Forms.Label();
            this.buttonRoll = new System.Windows.Forms.Button();
            this.panelSeparator = new System.Windows.Forms.Panel();
            this.tableLayoutPanelInfo = new System.Windows.Forms.TableLayoutPanel();
            this.labelExportedTime = new System.Windows.Forms.Label();
            this.labelStyeTime = new System.Windows.Forms.Label();
            this.labelSkonvertovaneTime = new System.Windows.Forms.Label();
            this.labelZiskane = new System.Windows.Forms.Label();
            this.labelSkonvertovane = new System.Windows.Forms.Label();
            this.labelStyle = new System.Windows.Forms.Label();
            this.labelExported = new System.Windows.Forms.Label();
            this.linkLabelPath = new System.Windows.Forms.LinkLabel();
            this.labelZiskaneTime = new System.Windows.Forms.Label();
            this.tableLayoutPanelError = new System.Windows.Forms.TableLayoutPanel();
            this.labelErrorMessage = new System.Windows.Forms.Label();
            this.labelStackTrace = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStatus)).BeginInit();
            this.tableLayoutPanelInfo.SuspendLayout();
            this.tableLayoutPanelError.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxStatus
            // 
            this.pictureBoxStatus.Image = global::cvti.isef.winformapp.Properties.Resources.app_logo;
            this.pictureBoxStatus.Location = new System.Drawing.Point(5, 5);
            this.pictureBoxStatus.Margin = new System.Windows.Forms.Padding(5);
            this.pictureBoxStatus.Name = "pictureBoxStatus";
            this.tableLayoutPanel1.SetRowSpan(this.pictureBoxStatus, 2);
            this.pictureBoxStatus.Size = new System.Drawing.Size(50, 31);
            this.pictureBoxStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxStatus.TabIndex = 0;
            this.pictureBoxStatus.TabStop = false;
            this.pictureBoxStatus.Click += new System.EventHandler(this.pictureBoxStatus_Click);
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelName.Location = new System.Drawing.Point(65, 5);
            this.labelName.Margin = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(318, 13);
            this.labelName.TabIndex = 1;
            this.labelName.Text = "Výdavky MSVVaS podľa okruhov a rozpočtových organizácií";
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.labelType.Location = new System.Drawing.Point(65, 26);
            this.labelType.Margin = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(48, 13);
            this.labelType.TabIndex = 2;
            this.labelType.Text = "Výdavky";
            // 
            // buttonRoll
            // 
            this.buttonRoll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRoll.FlatAppearance.BorderSize = 0;
            this.buttonRoll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRoll.Location = new System.Drawing.Point(539, 5);
            this.buttonRoll.Margin = new System.Windows.Forms.Padding(0, 5, 5, 0);
            this.buttonRoll.Name = "buttonRoll";
            this.tableLayoutPanel1.SetRowSpan(this.buttonRoll, 2);
            this.buttonRoll.Size = new System.Drawing.Size(28, 31);
            this.buttonRoll.TabIndex = 3;
            this.buttonRoll.Text = "v";
            this.buttonRoll.UseVisualStyleBackColor = true;
            this.buttonRoll.Click += new System.EventHandler(this.buttonRoll_Click);
            // 
            // panelSeparator
            // 
            this.panelSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.panelSeparator.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSeparator.Location = new System.Drawing.Point(0, 43);
            this.panelSeparator.Name = "panelSeparator";
            this.panelSeparator.Size = new System.Drawing.Size(572, 1);
            this.panelSeparator.TabIndex = 4;
            // 
            // tableLayoutPanelInfo
            // 
            this.tableLayoutPanelInfo.ColumnCount = 2;
            this.tableLayoutPanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelInfo.Controls.Add(this.labelExportedTime, 1, 3);
            this.tableLayoutPanelInfo.Controls.Add(this.labelStyeTime, 1, 2);
            this.tableLayoutPanelInfo.Controls.Add(this.labelSkonvertovaneTime, 1, 1);
            this.tableLayoutPanelInfo.Controls.Add(this.labelZiskane, 0, 0);
            this.tableLayoutPanelInfo.Controls.Add(this.labelSkonvertovane, 0, 1);
            this.tableLayoutPanelInfo.Controls.Add(this.labelStyle, 0, 2);
            this.tableLayoutPanelInfo.Controls.Add(this.labelExported, 0, 3);
            this.tableLayoutPanelInfo.Controls.Add(this.linkLabelPath, 0, 5);
            this.tableLayoutPanelInfo.Controls.Add(this.labelZiskaneTime, 1, 0);
            this.tableLayoutPanelInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelInfo.Location = new System.Drawing.Point(0, 44);
            this.tableLayoutPanelInfo.Name = "tableLayoutPanelInfo";
            this.tableLayoutPanelInfo.RowCount = 6;
            this.tableLayoutPanelInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelInfo.Size = new System.Drawing.Size(572, 0);
            this.tableLayoutPanelInfo.TabIndex = 5;
            this.tableLayoutPanelInfo.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanelInfo_Paint);
            // 
            // labelExportedTime
            // 
            this.labelExportedTime.AutoSize = true;
            this.labelExportedTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.labelExportedTime.Location = new System.Drawing.Point(133, 59);
            this.labelExportedTime.Margin = new System.Windows.Forms.Padding(5, 5, 10, 0);
            this.labelExportedTime.Name = "labelExportedTime";
            this.labelExportedTime.Size = new System.Drawing.Size(11, 13);
            this.labelExportedTime.TabIndex = 8;
            this.labelExportedTime.Text = "-";
            // 
            // labelStyeTime
            // 
            this.labelStyeTime.AutoSize = true;
            this.labelStyeTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.labelStyeTime.Location = new System.Drawing.Point(133, 41);
            this.labelStyeTime.Margin = new System.Windows.Forms.Padding(5, 5, 10, 0);
            this.labelStyeTime.Name = "labelStyeTime";
            this.labelStyeTime.Size = new System.Drawing.Size(11, 13);
            this.labelStyeTime.TabIndex = 7;
            this.labelStyeTime.Text = "-";
            // 
            // labelSkonvertovaneTime
            // 
            this.labelSkonvertovaneTime.AutoSize = true;
            this.labelSkonvertovaneTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.labelSkonvertovaneTime.Location = new System.Drawing.Point(133, 23);
            this.labelSkonvertovaneTime.Margin = new System.Windows.Forms.Padding(5, 5, 10, 0);
            this.labelSkonvertovaneTime.Name = "labelSkonvertovaneTime";
            this.labelSkonvertovaneTime.Size = new System.Drawing.Size(11, 13);
            this.labelSkonvertovaneTime.TabIndex = 6;
            this.labelSkonvertovaneTime.Text = "-";
            // 
            // labelZiskane
            // 
            this.labelZiskane.AutoSize = true;
            this.labelZiskane.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.labelZiskane.Location = new System.Drawing.Point(5, 5);
            this.labelZiskane.Margin = new System.Windows.Forms.Padding(5, 5, 10, 0);
            this.labelZiskane.Name = "labelZiskane";
            this.labelZiskane.Size = new System.Drawing.Size(72, 13);
            this.labelZiskane.TabIndex = 0;
            this.labelZiskane.Text = "Dáta získané";
            // 
            // labelSkonvertovane
            // 
            this.labelSkonvertovane.AutoSize = true;
            this.labelSkonvertovane.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.labelSkonvertovane.Location = new System.Drawing.Point(5, 23);
            this.labelSkonvertovane.Margin = new System.Windows.Forms.Padding(5, 5, 10, 0);
            this.labelSkonvertovane.Name = "labelSkonvertovane";
            this.labelSkonvertovane.Size = new System.Drawing.Size(109, 13);
            this.labelSkonvertovane.TabIndex = 1;
            this.labelSkonvertovane.Text = "Dáta skonvertované";
            // 
            // labelStyle
            // 
            this.labelStyle.AutoSize = true;
            this.labelStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.labelStyle.Location = new System.Drawing.Point(5, 41);
            this.labelStyle.Margin = new System.Windows.Forms.Padding(5, 5, 10, 0);
            this.labelStyle.Name = "labelStyle";
            this.labelStyle.Size = new System.Drawing.Size(98, 13);
            this.labelStyle.TabIndex = 2;
            this.labelStyle.Text = "Štýlovanie hotové";
            // 
            // labelExported
            // 
            this.labelExported.AutoSize = true;
            this.labelExported.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.labelExported.Location = new System.Drawing.Point(5, 59);
            this.labelExported.Margin = new System.Windows.Forms.Padding(5, 5, 10, 0);
            this.labelExported.Name = "labelExported";
            this.labelExported.Size = new System.Drawing.Size(113, 13);
            this.labelExported.TabIndex = 3;
            this.labelExported.Text = "Zostava exportovaná";
            // 
            // linkLabelPath
            // 
            this.linkLabelPath.AutoSize = true;
            this.tableLayoutPanelInfo.SetColumnSpan(this.linkLabelPath, 2);
            this.linkLabelPath.Location = new System.Drawing.Point(5, -12);
            this.linkLabelPath.Margin = new System.Windows.Forms.Padding(5, 0, 3, 0);
            this.linkLabelPath.Name = "linkLabelPath";
            this.linkLabelPath.Size = new System.Drawing.Size(47, 13);
            this.linkLabelPath.TabIndex = 4;
            this.linkLabelPath.TabStop = true;
            this.linkLabelPath.Text = "<path>";
            this.linkLabelPath.Visible = false;
            this.linkLabelPath.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelPath_LinkClicked);
            // 
            // labelZiskaneTime
            // 
            this.labelZiskaneTime.AutoSize = true;
            this.labelZiskaneTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.labelZiskaneTime.Location = new System.Drawing.Point(133, 5);
            this.labelZiskaneTime.Margin = new System.Windows.Forms.Padding(5, 5, 10, 0);
            this.labelZiskaneTime.Name = "labelZiskaneTime";
            this.labelZiskaneTime.Size = new System.Drawing.Size(11, 13);
            this.labelZiskaneTime.TabIndex = 5;
            this.labelZiskaneTime.Text = "-";
            // 
            // tableLayoutPanelError
            // 
            this.tableLayoutPanelError.ColumnCount = 1;
            this.tableLayoutPanelError.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelError.Controls.Add(this.labelErrorMessage, 0, 0);
            this.tableLayoutPanelError.Controls.Add(this.labelStackTrace, 0, 1);
            this.tableLayoutPanelError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelError.Location = new System.Drawing.Point(0, 43);
            this.tableLayoutPanelError.Name = "tableLayoutPanelError";
            this.tableLayoutPanelError.RowCount = 2;
            this.tableLayoutPanelError.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelError.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelError.Size = new System.Drawing.Size(572, 0);
            this.tableLayoutPanelError.TabIndex = 6;
            this.tableLayoutPanelError.Visible = false;
            // 
            // labelErrorMessage
            // 
            this.labelErrorMessage.AutoSize = true;
            this.labelErrorMessage.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelErrorMessage.ForeColor = System.Drawing.Color.Red;
            this.labelErrorMessage.Location = new System.Drawing.Point(5, 5);
            this.labelErrorMessage.Margin = new System.Windows.Forms.Padding(5, 5, 3, 0);
            this.labelErrorMessage.Name = "labelErrorMessage";
            this.labelErrorMessage.Size = new System.Drawing.Size(57, 21);
            this.labelErrorMessage.TabIndex = 0;
            this.labelErrorMessage.Text = "label5";
            // 
            // labelStackTrace
            // 
            this.labelStackTrace.AutoSize = true;
            this.labelStackTrace.ForeColor = System.Drawing.Color.Red;
            this.labelStackTrace.Location = new System.Drawing.Point(5, 31);
            this.labelStackTrace.Margin = new System.Windows.Forms.Padding(5, 5, 3, 0);
            this.labelStackTrace.Name = "labelStackTrace";
            this.labelStackTrace.Size = new System.Drawing.Size(38, 1);
            this.labelStackTrace.TabIndex = 1;
            this.labelStackTrace.Text = "label6";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.pictureBoxStatus, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelType, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonRoll, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(572, 43);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // ZostavaOutputControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanelInfo);
            this.Controls.Add(this.panelSeparator);
            this.Controls.Add(this.tableLayoutPanelError);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ZostavaOutputControl";
            this.Size = new System.Drawing.Size(572, 43);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStatus)).EndInit();
            this.tableLayoutPanelInfo.ResumeLayout(false);
            this.tableLayoutPanelInfo.PerformLayout();
            this.tableLayoutPanelError.ResumeLayout(false);
            this.tableLayoutPanelError.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxStatus;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.Button buttonRoll;
        private System.Windows.Forms.Panel panelSeparator;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelInfo;
        private System.Windows.Forms.Label labelZiskane;
        private System.Windows.Forms.Label labelSkonvertovane;
        private System.Windows.Forms.Label labelStyle;
        private System.Windows.Forms.Label labelExported;
        private System.Windows.Forms.LinkLabel linkLabelPath;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelError;
        private System.Windows.Forms.Label labelErrorMessage;
        private System.Windows.Forms.Label labelStackTrace;
        private System.Windows.Forms.Label labelExportedTime;
        private System.Windows.Forms.Label labelStyeTime;
        private System.Windows.Forms.Label labelSkonvertovaneTime;
        private System.Windows.Forms.Label labelZiskaneTime;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
