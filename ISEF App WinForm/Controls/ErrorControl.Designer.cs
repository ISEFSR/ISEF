namespace cvti.isef.winformapp.Controls.Main
{
    partial class ErrorControl
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
            this.buttonTryAgain = new System.Windows.Forms.Button();
            this.tableLayoutPanelContentWrapper = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.panelErrorMessage = new System.Windows.Forms.Panel();
            this.textBoxStackTrace = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonReset = new System.Windows.Forms.Button();
            this.panelSeparator = new System.Windows.Forms.Panel();
            this.buttonClose = new System.Windows.Forms.Button();
            this.tableLayoutPanelContentWrapper.SuspendLayout();
            this.panelErrorMessage.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonTryAgain
            // 
            this.buttonTryAgain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTryAgain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonTryAgain.Location = new System.Drawing.Point(558, 13);
            this.buttonTryAgain.Name = "buttonTryAgain";
            this.buttonTryAgain.Size = new System.Drawing.Size(125, 25);
            this.buttonTryAgain.TabIndex = 0;
            this.buttonTryAgain.Text = "&Skús znova";
            this.buttonTryAgain.UseVisualStyleBackColor = true;
            this.buttonTryAgain.Click += new System.EventHandler(this.buttonTryAgain_Click);
            // 
            // tableLayoutPanelContentWrapper
            // 
            this.tableLayoutPanelContentWrapper.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanelContentWrapper.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tableLayoutPanelContentWrapper.ColumnCount = 1;
            this.tableLayoutPanelContentWrapper.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelContentWrapper.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanelContentWrapper.Controls.Add(this.labelTitle, 0, 1);
            this.tableLayoutPanelContentWrapper.Controls.Add(this.panelErrorMessage, 0, 2);
            this.tableLayoutPanelContentWrapper.Controls.Add(this.panel1, 0, 3);
            this.tableLayoutPanelContentWrapper.Location = new System.Drawing.Point(144, 79);
            this.tableLayoutPanelContentWrapper.Name = "tableLayoutPanelContentWrapper";
            this.tableLayoutPanelContentWrapper.RowCount = 4;
            this.tableLayoutPanelContentWrapper.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelContentWrapper.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelContentWrapper.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelContentWrapper.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelContentWrapper.Size = new System.Drawing.Size(700, 377);
            this.tableLayoutPanelContentWrapper.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 86);
            this.label1.TabIndex = 0;
            this.label1.Text = ":(";
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelTitle.ForeColor = System.Drawing.Color.Red;
            this.labelTitle.Location = new System.Drawing.Point(3, 86);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(335, 30);
            this.labelTitle.TabIndex = 1;
            this.labelTitle.Text = "Mrzí ma to, ale niečo sa pokazilo";
            // 
            // panelErrorMessage
            // 
            this.panelErrorMessage.AutoScroll = true;
            this.panelErrorMessage.Controls.Add(this.textBoxStackTrace);
            this.panelErrorMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelErrorMessage.Location = new System.Drawing.Point(3, 119);
            this.panelErrorMessage.Name = "panelErrorMessage";
            this.panelErrorMessage.Size = new System.Drawing.Size(694, 205);
            this.panelErrorMessage.TabIndex = 2;
            // 
            // textBoxStackTrace
            // 
            this.textBoxStackTrace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxStackTrace.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxStackTrace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxStackTrace.ForeColor = System.Drawing.Color.Red;
            this.textBoxStackTrace.Location = new System.Drawing.Point(0, 0);
            this.textBoxStackTrace.Multiline = true;
            this.textBoxStackTrace.Name = "textBoxStackTrace";
            this.textBoxStackTrace.ReadOnly = true;
            this.textBoxStackTrace.Size = new System.Drawing.Size(694, 205);
            this.textBoxStackTrace.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.panel1.Controls.Add(this.buttonReset);
            this.panel1.Controls.Add(this.panelSeparator);
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Controls.Add(this.buttonTryAgain);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 327);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(700, 50);
            this.panel1.TabIndex = 3;
            // 
            // buttonReset
            // 
            this.buttonReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonReset.Location = new System.Drawing.Point(427, 13);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(125, 25);
            this.buttonReset.TabIndex = 4;
            this.buttonReset.Text = "&Resetuj nastavenia";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // panelSeparator
            // 
            this.panelSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelSeparator.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSeparator.Location = new System.Drawing.Point(0, 0);
            this.panelSeparator.Name = "panelSeparator";
            this.panelSeparator.Size = new System.Drawing.Size(700, 1);
            this.panelSeparator.TabIndex = 3;
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonClose.Location = new System.Drawing.Point(296, 13);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(125, 25);
            this.buttonClose.TabIndex = 2;
            this.buttonClose.Text = "&Zavri aplikáciu";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // ErrorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Controls.Add(this.tableLayoutPanelContentWrapper);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "ErrorControl";
            this.Size = new System.Drawing.Size(988, 535);
            this.tableLayoutPanelContentWrapper.ResumeLayout(false);
            this.tableLayoutPanelContentWrapper.PerformLayout();
            this.panelErrorMessage.ResumeLayout(false);
            this.panelErrorMessage.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonTryAgain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelContentWrapper;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Panel panelErrorMessage;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelSeparator;
        private System.Windows.Forms.TextBox textBoxStackTrace;
        private System.Windows.Forms.Button buttonReset;
    }
}
