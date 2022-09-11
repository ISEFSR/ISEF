namespace cvti.isef.winformapp.Forms
{
    partial class DialogBase
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
            this.panelControlButtons = new System.Windows.Forms.Panel();
            this.flowLayoutPanelButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.panelTitleWrapper = new System.Windows.Forms.Panel();
            this.panelTitle = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.pictureBoxImageIcon = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanelWorking = new System.Windows.Forms.TableLayoutPanel();
            this.isefLabel1 = new cvti.isef.winformapp.Components.ISEFLabel(this.components);
            this.button1 = new cvti.isef.winformapp.Components.ISEFButton(this.components);
            this.button2 = new cvti.isef.winformapp.Components.ISEFButton(this.components);
            this.panelControlButtons.SuspendLayout();
            this.flowLayoutPanelButtons.SuspendLayout();
            this.panelTitleWrapper.SuspendLayout();
            this.panelTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImageIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanelWorking.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControlButtons
            // 
            this.panelControlButtons.BackColor = System.Drawing.SystemColors.Control;
            this.panelControlButtons.Controls.Add(this.flowLayoutPanelButtons);
            this.panelControlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControlButtons.Location = new System.Drawing.Point(0, 493);
            this.panelControlButtons.Name = "panelControlButtons";
            this.panelControlButtons.Size = new System.Drawing.Size(720, 50);
            this.panelControlButtons.TabIndex = 0;
            this.panelControlButtons.Paint += new System.Windows.Forms.PaintEventHandler(this.panelControlButtons_Paint);
            // 
            // flowLayoutPanelButtons
            // 
            this.flowLayoutPanelButtons.Controls.Add(this.button1);
            this.flowLayoutPanelButtons.Controls.Add(this.button2);
            this.flowLayoutPanelButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanelButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanelButtons.Location = new System.Drawing.Point(351, 0);
            this.flowLayoutPanelButtons.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanelButtons.Name = "flowLayoutPanelButtons";
            this.flowLayoutPanelButtons.Size = new System.Drawing.Size(369, 50);
            this.flowLayoutPanelButtons.TabIndex = 2;
            this.flowLayoutPanelButtons.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // panelTitleWrapper
            // 
            this.panelTitleWrapper.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(89)))), ((int)(((byte)(127)))));
            this.panelTitleWrapper.Controls.Add(this.panelTitle);
            this.panelTitleWrapper.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleWrapper.Location = new System.Drawing.Point(0, 0);
            this.panelTitleWrapper.Name = "panelTitleWrapper";
            this.panelTitleWrapper.Size = new System.Drawing.Size(720, 85);
            this.panelTitleWrapper.TabIndex = 1;
            // 
            // panelTitle
            // 
            this.panelTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(89)))), ((int)(((byte)(127)))));
            this.panelTitle.Controls.Add(this.labelTitle);
            this.panelTitle.Controls.Add(this.pictureBoxImageIcon);
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitle.Location = new System.Drawing.Point(0, 0);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Size = new System.Drawing.Size(720, 75);
            this.panelTitle.TabIndex = 0;
            // 
            // labelTitle
            // 
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.labelTitle.Location = new System.Drawing.Point(75, 0);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(15, 0, 3, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.labelTitle.Size = new System.Drawing.Size(444, 75);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBoxImageIcon
            // 
            this.pictureBoxImageIcon.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBoxImageIcon.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxImageIcon.Name = "pictureBoxImageIcon";
            this.pictureBoxImageIcon.Size = new System.Drawing.Size(75, 75);
            this.pictureBoxImageIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxImageIcon.TabIndex = 1;
            this.pictureBoxImageIcon.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::cvti.isef.winformapp.Properties.Resources.loading3;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(263, 219);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // tableLayoutPanelWorking
            // 
            this.tableLayoutPanelWorking.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanelWorking.ColumnCount = 1;
            this.tableLayoutPanelWorking.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelWorking.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanelWorking.Controls.Add(this.isefLabel1, 0, 1);
            this.tableLayoutPanelWorking.Location = new System.Drawing.Point(226, 152);
            this.tableLayoutPanelWorking.Name = "tableLayoutPanelWorking";
            this.tableLayoutPanelWorking.RowCount = 2;
            this.tableLayoutPanelWorking.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelWorking.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelWorking.Size = new System.Drawing.Size(269, 238);
            this.tableLayoutPanelWorking.TabIndex = 3;
            this.tableLayoutPanelWorking.Visible = false;
            // 
            // isefLabel1
            // 
            this.isefLabel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.isefLabel1.AutoSize = true;
            this.isefLabel1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.isefLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.isefLabel1.Location = new System.Drawing.Point(46, 225);
            this.isefLabel1.Name = "isefLabel1";
            this.isefLabel1.Size = new System.Drawing.Size(177, 13);
            this.isefLabel1.TabIndex = 3;
            this.isefLabel1.Text = "Počkajte prosím načítavam dáta...";
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(89)))), ((int)(((byte)(127)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.button1.Location = new System.Drawing.Point(239, 10);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 10, 10, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 30);
            this.button1.TabIndex = 2;
            this.button1.Text = "&OK";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(89)))), ((int)(((byte)(127)))));
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.button2.Location = new System.Drawing.Point(111, 10);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 10, 5, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(120, 30);
            this.button2.TabIndex = 3;
            this.button2.Text = "&Cancel";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // DialogBase
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(720, 543);
            this.Controls.Add(this.tableLayoutPanelWorking);
            this.Controls.Add(this.panelTitleWrapper);
            this.Controls.Add(this.panelControlButtons);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(520, 360);
            this.Name = "DialogBase";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dialog Base";
            this.panelControlButtons.ResumeLayout(false);
            this.flowLayoutPanelButtons.ResumeLayout(false);
            this.panelTitleWrapper.ResumeLayout(false);
            this.panelTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImageIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanelWorking.ResumeLayout(false);
            this.tableLayoutPanelWorking.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelTitleWrapper;
        private System.Windows.Forms.Panel panelTitle;
        protected System.Windows.Forms.FlowLayoutPanel flowLayoutPanelButtons;
        protected System.Windows.Forms.PictureBox pictureBoxImageIcon;
        protected System.Windows.Forms.Label labelTitle;
        protected System.Windows.Forms.Panel panelControlButtons;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelWorking;
        private Components.ISEFLabel isefLabel1;
        internal Components.ISEFButton button1;
        internal Components.ISEFButton button2;
    }
}