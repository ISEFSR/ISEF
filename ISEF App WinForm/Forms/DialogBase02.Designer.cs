
namespace cvti.isef.winformapp.Forms
{
    partial class DialogBase02
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
            this.flowLayoutPanelButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanelTitle = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.isefLabelTitleBigTitle = new cvti.isef.winformapp.Components.ISEFLabelTitleBig(this.components);
            this.isefLabelInfo = new cvti.isef.winformapp.Components.ISEFLabel(this.components);
            this.isefButtonYes = new cvti.isef.winformapp.Components.ISEFButton(this.components);
            this.isefButtonNo = new cvti.isef.winformapp.Components.ISEFButton(this.components);
            this.isefButtonCancel = new cvti.isef.winformapp.Components.ISEFButton(this.components);
            this.flowLayoutPanelButtons.SuspendLayout();
            this.tableLayoutPanelTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanelButtons
            // 
            this.flowLayoutPanelButtons.BackColor = System.Drawing.SystemColors.Control;
            this.flowLayoutPanelButtons.Controls.Add(this.isefButtonYes);
            this.flowLayoutPanelButtons.Controls.Add(this.isefButtonNo);
            this.flowLayoutPanelButtons.Controls.Add(this.isefButtonCancel);
            this.flowLayoutPanelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanelButtons.Location = new System.Drawing.Point(0, 515);
            this.flowLayoutPanelButtons.Name = "flowLayoutPanelButtons";
            this.flowLayoutPanelButtons.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanelButtons.Size = new System.Drawing.Size(689, 50);
            this.flowLayoutPanelButtons.TabIndex = 0;
            // 
            // tableLayoutPanelTitle
            // 
            this.tableLayoutPanelTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(89)))), ((int)(((byte)(127)))));
            this.tableLayoutPanelTitle.ColumnCount = 2;
            this.tableLayoutPanelTitle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelTitle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelTitle.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanelTitle.Controls.Add(this.isefLabelTitleBigTitle, 1, 0);
            this.tableLayoutPanelTitle.Controls.Add(this.isefLabelInfo, 1, 1);
            this.tableLayoutPanelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanelTitle.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelTitle.Name = "tableLayoutPanelTitle";
            this.tableLayoutPanelTitle.RowCount = 2;
            this.tableLayoutPanelTitle.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelTitle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelTitle.Size = new System.Drawing.Size(689, 100);
            this.tableLayoutPanelTitle.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.tableLayoutPanelTitle.SetRowSpan(this.pictureBox1, 2);
            this.pictureBox1.Size = new System.Drawing.Size(94, 94);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // isefLabelTitleBigTitle
            // 
            this.isefLabelTitleBigTitle.AutoSize = true;
            this.isefLabelTitleBigTitle.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.isefLabelTitleBigTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.isefLabelTitleBigTitle.Location = new System.Drawing.Point(105, 10);
            this.isefLabelTitleBigTitle.Margin = new System.Windows.Forms.Padding(5, 10, 10, 0);
            this.isefLabelTitleBigTitle.Name = "isefLabelTitleBigTitle";
            this.isefLabelTitleBigTitle.Size = new System.Drawing.Size(167, 25);
            this.isefLabelTitleBigTitle.TabIndex = 1;
            this.isefLabelTitleBigTitle.Text = "isefLabelTitleBig1";
            // 
            // isefLabelInfo
            // 
            this.isefLabelInfo.AutoSize = true;
            this.isefLabelInfo.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.isefLabelInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.isefLabelInfo.Location = new System.Drawing.Point(105, 37);
            this.isefLabelInfo.Margin = new System.Windows.Forms.Padding(5, 2, 10, 10);
            this.isefLabelInfo.Name = "isefLabelInfo";
            this.isefLabelInfo.Size = new System.Drawing.Size(58, 13);
            this.isefLabelInfo.TabIndex = 2;
            this.isefLabelInfo.Text = "isefLabel1";
            // 
            // isefButtonYes
            // 
            this.isefButtonYes.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.isefButtonYes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(89)))), ((int)(((byte)(127)))));
            this.isefButtonYes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.isefButtonYes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.isefButtonYes.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.isefButtonYes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.isefButtonYes.Location = new System.Drawing.Point(564, 10);
            this.isefButtonYes.Margin = new System.Windows.Forms.Padding(5, 10, 0, 10);
            this.isefButtonYes.Name = "isefButtonYes";
            this.isefButtonYes.Size = new System.Drawing.Size(120, 30);
            this.isefButtonYes.TabIndex = 0;
            this.isefButtonYes.Text = "&Yes";
            this.isefButtonYes.UseVisualStyleBackColor = false;
            // 
            // isefButtonNo
            // 
            this.isefButtonNo.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.isefButtonNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(89)))), ((int)(((byte)(127)))));
            this.isefButtonNo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.isefButtonNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.isefButtonNo.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.isefButtonNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.isefButtonNo.Location = new System.Drawing.Point(439, 10);
            this.isefButtonNo.Margin = new System.Windows.Forms.Padding(5, 10, 0, 10);
            this.isefButtonNo.Name = "isefButtonNo";
            this.isefButtonNo.Size = new System.Drawing.Size(120, 30);
            this.isefButtonNo.TabIndex = 1;
            this.isefButtonNo.Text = "&No";
            this.isefButtonNo.UseVisualStyleBackColor = false;
            // 
            // isefButtonCancel
            // 
            this.isefButtonCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.isefButtonCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(89)))), ((int)(((byte)(127)))));
            this.isefButtonCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.isefButtonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.isefButtonCancel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.isefButtonCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.isefButtonCancel.Location = new System.Drawing.Point(314, 10);
            this.isefButtonCancel.Margin = new System.Windows.Forms.Padding(5, 10, 0, 10);
            this.isefButtonCancel.Name = "isefButtonCancel";
            this.isefButtonCancel.Size = new System.Drawing.Size(120, 30);
            this.isefButtonCancel.TabIndex = 2;
            this.isefButtonCancel.Text = "&Cancel";
            this.isefButtonCancel.UseVisualStyleBackColor = false;
            // 
            // DialogBase02
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(689, 565);
            this.Controls.Add(this.tableLayoutPanelTitle);
            this.Controls.Add(this.flowLayoutPanelButtons);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "DialogBase02";
            this.Text = "DialogBase02";
            this.flowLayoutPanelButtons.ResumeLayout(false);
            this.tableLayoutPanelTitle.ResumeLayout(false);
            this.tableLayoutPanelTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelButtons;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelTitle;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Components.ISEFLabelTitleBig isefLabelTitleBigTitle;
        private Components.ISEFLabel isefLabelInfo;
        private Components.ISEFButton isefButtonYes;
        private Components.ISEFButton isefButtonNo;
        private Components.ISEFButton isefButtonCancel;
    }
}