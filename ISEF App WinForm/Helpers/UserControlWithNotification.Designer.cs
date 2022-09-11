namespace cvti.isef.winformapp.Helpers
{
    partial class UserControlWithNotification
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
            this.panelWarningNotification = new System.Windows.Forms.Panel();
            this.buttonOkNotification = new System.Windows.Forms.Button();
            this.pictureBoxWarningNotification = new System.Windows.Forms.PictureBox();
            this.labelMessageNotificatio = new System.Windows.Forms.Label();
            this.labelTitleNotification = new System.Windows.Forms.Label();
            this.timerHide = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanelLoaderNotification = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBoxLoaderNotification = new System.Windows.Forms.PictureBox();
            this.labelLoaderInfoNotification = new System.Windows.Forms.Label();
            this.panelWarningNotification.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWarningNotification)).BeginInit();
            this.tableLayoutPanelLoaderNotification.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoaderNotification)).BeginInit();
            this.SuspendLayout();
            // 
            // panelWarningNotification
            // 
            this.panelWarningNotification.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelWarningNotification.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelWarningNotification.Controls.Add(this.buttonOkNotification);
            this.panelWarningNotification.Controls.Add(this.pictureBoxWarningNotification);
            this.panelWarningNotification.Controls.Add(this.labelMessageNotificatio);
            this.panelWarningNotification.Controls.Add(this.labelTitleNotification);
            this.panelWarningNotification.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelWarningNotification.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelWarningNotification.Location = new System.Drawing.Point(0, 0);
            this.panelWarningNotification.Name = "panelWarningNotification";
            this.panelWarningNotification.Size = new System.Drawing.Size(791, 100);
            this.panelWarningNotification.TabIndex = 27;
            this.panelWarningNotification.Paint += new System.Windows.Forms.PaintEventHandler(this.panelWarning_Paint);
            // 
            // buttonOkNotification
            // 
            this.buttonOkNotification.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOkNotification.BackColor = System.Drawing.Color.LightBlue;
            this.buttonOkNotification.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOkNotification.Location = new System.Drawing.Point(700, 66);
            this.buttonOkNotification.Name = "buttonOkNotification";
            this.buttonOkNotification.Size = new System.Drawing.Size(75, 23);
            this.buttonOkNotification.TabIndex = 3;
            this.buttonOkNotification.Text = "OK";
            this.buttonOkNotification.UseVisualStyleBackColor = false;
            this.buttonOkNotification.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // pictureBoxWarningNotification
            // 
            this.pictureBoxWarningNotification.Image = global::cvti.isef.winformapp.Properties.Resources.info50;
            this.pictureBoxWarningNotification.Location = new System.Drawing.Point(15, 15);
            this.pictureBoxWarningNotification.Margin = new System.Windows.Forms.Padding(15, 15, 0, 0);
            this.pictureBoxWarningNotification.Name = "pictureBoxWarningNotification";
            this.pictureBoxWarningNotification.Size = new System.Drawing.Size(50, 50);
            this.pictureBoxWarningNotification.TabIndex = 2;
            this.pictureBoxWarningNotification.TabStop = false;
            // 
            // labelMessageNotificatio
            // 
            this.labelMessageNotificatio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.labelMessageNotificatio.Location = new System.Drawing.Point(69, 36);
            this.labelMessageNotificatio.Name = "labelMessageNotificatio";
            this.labelMessageNotificatio.Size = new System.Drawing.Size(637, 29);
            this.labelMessageNotificatio.TabIndex = 1;
            this.labelMessageNotificatio.Text = "Nepodarilo sa mi nájsť hlavičkový XLSX súbor. Skontrolujte prosím adresár kde sa " +
    "nachádzajú hlavičkové súbory. Uistite sa, či ste náhodou hlavičku nepresunuli al" +
    "ebo omylom nezmazali";
            // 
            // labelTitleNotification
            // 
            this.labelTitleNotification.AutoSize = true;
            this.labelTitleNotification.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitleNotification.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(89)))), ((int)(((byte)(127)))));
            this.labelTitleNotification.Location = new System.Drawing.Point(68, 15);
            this.labelTitleNotification.Name = "labelTitleNotification";
            this.labelTitleNotification.Size = new System.Drawing.Size(160, 21);
            this.labelTitleNotification.TabIndex = 0;
            this.labelTitleNotification.Text = "Nenašiel som súbor";
            // 
            // timerHide
            // 
            this.timerHide.Interval = 7500;
            this.timerHide.Tick += new System.EventHandler(this.timerHide_Tick);
            // 
            // tableLayoutPanelLoaderNotification
            // 
            this.tableLayoutPanelLoaderNotification.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanelLoaderNotification.ColumnCount = 1;
            this.tableLayoutPanelLoaderNotification.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelLoaderNotification.Controls.Add(this.pictureBoxLoaderNotification, 0, 0);
            this.tableLayoutPanelLoaderNotification.Controls.Add(this.labelLoaderInfoNotification, 0, 1);
            this.tableLayoutPanelLoaderNotification.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.tableLayoutPanelLoaderNotification.Enabled = false;
            this.tableLayoutPanelLoaderNotification.Location = new System.Drawing.Point(264, 219);
            this.tableLayoutPanelLoaderNotification.Name = "tableLayoutPanelLoaderNotification";
            this.tableLayoutPanelLoaderNotification.RowCount = 2;
            this.tableLayoutPanelLoaderNotification.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelLoaderNotification.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelLoaderNotification.Size = new System.Drawing.Size(263, 345);
            this.tableLayoutPanelLoaderNotification.TabIndex = 28;
            this.tableLayoutPanelLoaderNotification.Visible = false;
            // 
            // pictureBoxLoaderNotification
            // 
            this.pictureBoxLoaderNotification.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxLoaderNotification.Image = global::cvti.isef.winformapp.Properties.Resources.loading3;
            this.pictureBoxLoaderNotification.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxLoaderNotification.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxLoaderNotification.Name = "pictureBoxLoaderNotification";
            this.pictureBoxLoaderNotification.Size = new System.Drawing.Size(263, 172);
            this.pictureBoxLoaderNotification.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxLoaderNotification.TabIndex = 2;
            this.pictureBoxLoaderNotification.TabStop = false;
            // 
            // labelLoaderInfoNotification
            // 
            this.labelLoaderInfoNotification.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelLoaderInfoNotification.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLoaderInfoNotification.ForeColor = System.Drawing.Color.Gray;
            this.labelLoaderInfoNotification.Location = new System.Drawing.Point(3, 172);
            this.labelLoaderInfoNotification.Name = "labelLoaderInfoNotification";
            this.labelLoaderInfoNotification.Size = new System.Drawing.Size(257, 173);
            this.labelLoaderInfoNotification.TabIndex = 3;
            this.labelLoaderInfoNotification.Text = "Validujem a načítavam údaje pre vybranú hlavičku...";
            this.labelLoaderInfoNotification.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // UserControlWithNotification
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.tableLayoutPanelLoaderNotification);
            this.Controls.Add(this.panelWarningNotification);
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UserControlWithNotification";
            this.Size = new System.Drawing.Size(791, 536);
            this.panelWarningNotification.ResumeLayout(false);
            this.panelWarningNotification.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWarningNotification)).EndInit();
            this.tableLayoutPanelLoaderNotification.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoaderNotification)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonOkNotification;
        private System.Windows.Forms.PictureBox pictureBoxWarningNotification;
        private System.Windows.Forms.Label labelMessageNotificatio;
        private System.Windows.Forms.Label labelTitleNotification;
        internal System.Windows.Forms.Panel panelWarningNotification;
        private System.Windows.Forms.Timer timerHide;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelLoaderNotification;
        private System.Windows.Forms.PictureBox pictureBoxLoaderNotification;
        private System.Windows.Forms.Label labelLoaderInfoNotification;
    }
}
