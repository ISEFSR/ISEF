namespace cvti.isef.winformapp.Controls.Main.Dashboard
{
    partial class DataPreviewTile
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
            this.panelContent = new System.Windows.Forms.Panel();
            this.cartesianChartPreview = new LiveCharts.WinForms.CartesianChart();
            this.panelTitle = new System.Windows.Forms.Panel();
            this.buttonNewForm = new System.Windows.Forms.Button();
            this.labelTitle = new System.Windows.Forms.Label();
            this.buttonBack = new System.Windows.Forms.Button();
            this.panelContent.SuspendLayout();
            this.panelTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContent
            // 
            this.panelContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContent.BackgroundImage = global::cvti.isef.winformapp.Properties.Resources.nodata;
            this.panelContent.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelContent.Controls.Add(this.cartesianChartPreview);
            this.panelContent.Controls.Add(this.panelTitle);
            this.panelContent.Location = new System.Drawing.Point(10, 10);
            this.panelContent.Margin = new System.Windows.Forms.Padding(10);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(640, 280);
            this.panelContent.TabIndex = 0;
            // 
            // cartesianChartPreview
            // 
            this.cartesianChartPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cartesianChartPreview.Location = new System.Drawing.Point(0, 30);
            this.cartesianChartPreview.Name = "cartesianChartPreview";
            this.cartesianChartPreview.Size = new System.Drawing.Size(638, 248);
            this.cartesianChartPreview.TabIndex = 0;
            this.cartesianChartPreview.Text = "cartesianChart1";
            this.cartesianChartPreview.DataClick += new LiveCharts.Events.DataClickHandler(this.cartesianChartPreview_DataClick);
            // 
            // panelTitle
            // 
            this.panelTitle.Controls.Add(this.buttonNewForm);
            this.panelTitle.Controls.Add(this.labelTitle);
            this.panelTitle.Controls.Add(this.buttonBack);
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitle.Location = new System.Drawing.Point(0, 0);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Size = new System.Drawing.Size(638, 30);
            this.panelTitle.TabIndex = 1;
            // 
            // buttonNewForm
            // 
            this.buttonNewForm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonNewForm.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonNewForm.FlatAppearance.BorderSize = 0;
            this.buttonNewForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNewForm.Image = global::cvti.isef.winformapp.Properties.Resources.full20;
            this.buttonNewForm.Location = new System.Drawing.Point(608, 0);
            this.buttonNewForm.Name = "buttonNewForm";
            this.buttonNewForm.Size = new System.Drawing.Size(30, 30);
            this.buttonNewForm.TabIndex = 4;
            this.buttonNewForm.UseVisualStyleBackColor = true;
            this.buttonNewForm.Click += new System.EventHandler(this.buttonNewForm_Click);
            // 
            // labelTitle
            // 
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(89)))), ((int)(((byte)(127)))));
            this.labelTitle.Location = new System.Drawing.Point(30, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(608, 30);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Prehľad nahratých údajov";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelTitle.Click += new System.EventHandler(this.labelTitle_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonBack.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonBack.FlatAppearance.BorderSize = 0;
            this.buttonBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBack.Location = new System.Drawing.Point(0, 0);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(30, 30);
            this.buttonBack.TabIndex = 1;
            this.buttonBack.Text = "<";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Visible = false;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // DataPreviewTile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelContent);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "DataPreviewTile";
            this.Size = new System.Drawing.Size(660, 300);
            this.panelContent.ResumeLayout(false);
            this.panelTitle.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContent;
        private LiveCharts.WinForms.CartesianChart cartesianChartPreview;
        private System.Windows.Forms.Panel panelTitle;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonNewForm;
    }
}
