namespace cvti.isef.winformapp.Controls.Main.Zostavy
{
    partial class NovaZostavaFirstStep
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NovaZostavaFirstStep));
            this.labelTitle = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listViewHlavicky = new System.Windows.Forms.ListView();
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderRPP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderRPH = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderCPH = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageListListView = new System.Windows.Forms.ImageList(this.components);
            this.panelSeparator = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.labelTitle.Location = new System.Drawing.Point(0, 0);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(15, 15, 3, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Padding = new System.Windows.Forms.Padding(15, 15, 0, 0);
            this.labelTitle.Size = new System.Drawing.Size(138, 36);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Výber hlavičky";
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(0, 36);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(15, 10, 15, 0);
            this.label2.Size = new System.Drawing.Size(729, 66);
            this.label2.TabIndex = 1;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // listViewHlavicky
            // 
            this.listViewHlavicky.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewHlavicky.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderType,
            this.columnHeaderRPP,
            this.columnHeaderRPH,
            this.columnHeaderCPH,
            this.columnHeaderText});
            this.listViewHlavicky.HideSelection = false;
            this.listViewHlavicky.LargeImageList = this.imageListListView;
            this.listViewHlavicky.Location = new System.Drawing.Point(10, 116);
            this.listViewHlavicky.Margin = new System.Windows.Forms.Padding(10);
            this.listViewHlavicky.MultiSelect = false;
            this.listViewHlavicky.Name = "listViewHlavicky";
            this.listViewHlavicky.Size = new System.Drawing.Size(709, 375);
            this.listViewHlavicky.SmallImageList = this.imageListListView;
            this.listViewHlavicky.StateImageList = this.imageListListView;
            this.listViewHlavicky.TabIndex = 2;
            this.listViewHlavicky.UseCompatibleStateImageBehavior = false;
            this.listViewHlavicky.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Názov hlavičky";
            this.columnHeaderName.Width = 160;
            // 
            // columnHeaderType
            // 
            this.columnHeaderType.Text = "Typ hlavičky";
            this.columnHeaderType.Width = 100;
            // 
            // columnHeaderRPP
            // 
            this.columnHeaderRPP.Text = "Riadky / strana";
            this.columnHeaderRPP.Width = 100;
            // 
            // columnHeaderRPH
            // 
            this.columnHeaderRPH.Text = "Riadky / Hlavička";
            this.columnHeaderRPH.Width = 100;
            // 
            // columnHeaderCPH
            // 
            this.columnHeaderCPH.Text = "Stĺpce / Hlavička";
            this.columnHeaderCPH.Width = 100;
            // 
            // columnHeaderText
            // 
            this.columnHeaderText.Text = "Textové stĺpce";
            this.columnHeaderText.Width = 120;
            // 
            // imageListListView
            // 
            this.imageListListView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListListView.ImageStream")));
            this.imageListListView.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListListView.Images.SetKeyName(0, "excel1_35.png");
            // 
            // panelSeparator
            // 
            this.panelSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.panelSeparator.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSeparator.Location = new System.Drawing.Point(0, 102);
            this.panelSeparator.Name = "panelSeparator";
            this.panelSeparator.Size = new System.Drawing.Size(729, 1);
            this.panelSeparator.TabIndex = 3;
            // 
            // NovaZostavaFirstStep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelSeparator);
            this.Controls.Add(this.listViewHlavicky);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "NovaZostavaFirstStep";
            this.Size = new System.Drawing.Size(729, 501);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView listViewHlavicky;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderType;
        private System.Windows.Forms.ColumnHeader columnHeaderRPP;
        private System.Windows.Forms.ColumnHeader columnHeaderRPH;
        private System.Windows.Forms.ColumnHeader columnHeaderCPH;
        private System.Windows.Forms.ColumnHeader columnHeaderText;
        private System.Windows.Forms.ImageList imageListListView;
        private System.Windows.Forms.Panel panelSeparator;
    }
}
