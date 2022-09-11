namespace cvti.isef.winformapp.Controls.Main.Zostavy
{
    partial class ZostavyPrehladControl
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
            this.panelPrijmy = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBoxPrijmy = new System.Windows.Forms.PictureBox();
            this.buttonRollPrijmy = new System.Windows.Forms.Button();
            this.panelSeparator = new System.Windows.Forms.Panel();
            this.panelVydavky = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBoxVydavky = new System.Windows.Forms.PictureBox();
            this.buttonRollVydavky = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelTransfery = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBoxTransfery = new System.Windows.Forms.PictureBox();
            this.buttonRollTransfery = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.panelTransferyData = new System.Windows.Forms.Panel();
            this.listBoxTransferove = new System.Windows.Forms.ListBox();
            this.panelVydavkyData = new System.Windows.Forms.Panel();
            this.listBoxVydavkove = new System.Windows.Forms.ListBox();
            this.panelPrijmyData = new System.Windows.Forms.Panel();
            this.listBoxPrijmove = new System.Windows.Forms.ListBox();
            this.panelPrijmy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPrijmy)).BeginInit();
            this.panelVydavky.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVydavky)).BeginInit();
            this.panelTransfery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTransfery)).BeginInit();
            this.panelTransferyData.SuspendLayout();
            this.panelVydavkyData.SuspendLayout();
            this.panelPrijmyData.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelPrijmy
            // 
            this.panelPrijmy.Controls.Add(this.label1);
            this.panelPrijmy.Controls.Add(this.pictureBoxPrijmy);
            this.panelPrijmy.Controls.Add(this.buttonRollPrijmy);
            this.panelPrijmy.Controls.Add(this.panelSeparator);
            this.panelPrijmy.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPrijmy.Location = new System.Drawing.Point(0, 0);
            this.panelPrijmy.Name = "panelPrijmy";
            this.panelPrijmy.Size = new System.Drawing.Size(264, 35);
            this.panelPrijmy.TabIndex = 0;
            this.panelPrijmy.MouseEnter += new System.EventHandler(this.panelPrijmy_MouseEnter);
            this.panelPrijmy.MouseLeave += new System.EventHandler(this.panelPrijmy_MouseLeave);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(34, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.label1.Size = new System.Drawing.Size(109, 34);
            this.label1.TabIndex = 1;
            this.label1.Text = "Príjmové zostavy";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.MouseEnter += new System.EventHandler(this.panelPrijmy_MouseEnter);
            this.label1.MouseLeave += new System.EventHandler(this.panelPrijmy_MouseLeave);
            // 
            // pictureBoxPrijmy
            // 
            this.pictureBoxPrijmy.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBoxPrijmy.Image = global::cvti.isef.winformapp.Properties.Resources.red35;
            this.pictureBoxPrijmy.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxPrijmy.Name = "pictureBoxPrijmy";
            this.pictureBoxPrijmy.Size = new System.Drawing.Size(34, 34);
            this.pictureBoxPrijmy.TabIndex = 5;
            this.pictureBoxPrijmy.TabStop = false;
            // 
            // buttonRollPrijmy
            // 
            this.buttonRollPrijmy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonRollPrijmy.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonRollPrijmy.FlatAppearance.BorderSize = 0;
            this.buttonRollPrijmy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRollPrijmy.Location = new System.Drawing.Point(229, 0);
            this.buttonRollPrijmy.Name = "buttonRollPrijmy";
            this.buttonRollPrijmy.Size = new System.Drawing.Size(35, 34);
            this.buttonRollPrijmy.TabIndex = 0;
            this.buttonRollPrijmy.Text = "^";
            this.toolTipInfo.SetToolTip(this.buttonRollPrijmy, "Schovaj");
            this.buttonRollPrijmy.UseVisualStyleBackColor = true;
            this.buttonRollPrijmy.Click += new System.EventHandler(this.buttonRollPrijmy_Click);
            // 
            // panelSeparator
            // 
            this.panelSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.panelSeparator.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelSeparator.Location = new System.Drawing.Point(0, 34);
            this.panelSeparator.Name = "panelSeparator";
            this.panelSeparator.Size = new System.Drawing.Size(264, 1);
            this.panelSeparator.TabIndex = 6;
            // 
            // panelVydavky
            // 
            this.panelVydavky.Controls.Add(this.label2);
            this.panelVydavky.Controls.Add(this.pictureBoxVydavky);
            this.panelVydavky.Controls.Add(this.buttonRollVydavky);
            this.panelVydavky.Controls.Add(this.panel1);
            this.panelVydavky.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelVydavky.Location = new System.Drawing.Point(0, 222);
            this.panelVydavky.Name = "panelVydavky";
            this.panelVydavky.Size = new System.Drawing.Size(264, 35);
            this.panelVydavky.TabIndex = 2;
            this.panelVydavky.MouseEnter += new System.EventHandler(this.panelVydavky_MouseEnter);
            this.panelVydavky.MouseLeave += new System.EventHandler(this.panelVydavky_MouseLeave);
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(34, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.label2.Size = new System.Drawing.Size(119, 34);
            this.label2.TabIndex = 2;
            this.label2.Text = "Výdavkové zostavy";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.MouseEnter += new System.EventHandler(this.panelVydavky_MouseEnter);
            this.label2.MouseLeave += new System.EventHandler(this.panelVydavky_MouseLeave);
            // 
            // pictureBoxVydavky
            // 
            this.pictureBoxVydavky.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBoxVydavky.Image = global::cvti.isef.winformapp.Properties.Resources.blue35;
            this.pictureBoxVydavky.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxVydavky.Name = "pictureBoxVydavky";
            this.pictureBoxVydavky.Size = new System.Drawing.Size(34, 34);
            this.pictureBoxVydavky.TabIndex = 5;
            this.pictureBoxVydavky.TabStop = false;
            // 
            // buttonRollVydavky
            // 
            this.buttonRollVydavky.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonRollVydavky.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonRollVydavky.FlatAppearance.BorderSize = 0;
            this.buttonRollVydavky.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRollVydavky.Location = new System.Drawing.Point(229, 0);
            this.buttonRollVydavky.Name = "buttonRollVydavky";
            this.buttonRollVydavky.Size = new System.Drawing.Size(35, 34);
            this.buttonRollVydavky.TabIndex = 1;
            this.buttonRollVydavky.Text = "^";
            this.toolTipInfo.SetToolTip(this.buttonRollVydavky, "Schovaj");
            this.buttonRollVydavky.UseVisualStyleBackColor = true;
            this.buttonRollVydavky.Click += new System.EventHandler(this.buttonRollVydavky_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 34);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(264, 1);
            this.panel1.TabIndex = 7;
            // 
            // panelTransfery
            // 
            this.panelTransfery.Controls.Add(this.label3);
            this.panelTransfery.Controls.Add(this.pictureBoxTransfery);
            this.panelTransfery.Controls.Add(this.buttonRollTransfery);
            this.panelTransfery.Controls.Add(this.panel2);
            this.panelTransfery.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTransfery.Location = new System.Drawing.Point(0, 444);
            this.panelTransfery.Name = "panelTransfery";
            this.panelTransfery.Size = new System.Drawing.Size(264, 35);
            this.panelTransfery.TabIndex = 4;
            this.panelTransfery.MouseEnter += new System.EventHandler(this.panelTransfery_MouseEnter);
            this.panelTransfery.MouseLeave += new System.EventHandler(this.panelTransfery_MouseLeave);
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(34, 0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.label3.Size = new System.Drawing.Size(128, 34);
            this.label3.TabIndex = 3;
            this.label3.Text = "Transferové zostavy";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.MouseEnter += new System.EventHandler(this.panelTransfery_MouseEnter);
            this.label3.MouseLeave += new System.EventHandler(this.panelTransfery_MouseLeave);
            // 
            // pictureBoxTransfery
            // 
            this.pictureBoxTransfery.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBoxTransfery.Image = global::cvti.isef.winformapp.Properties.Resources.green35;
            this.pictureBoxTransfery.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxTransfery.Name = "pictureBoxTransfery";
            this.pictureBoxTransfery.Size = new System.Drawing.Size(34, 34);
            this.pictureBoxTransfery.TabIndex = 4;
            this.pictureBoxTransfery.TabStop = false;
            // 
            // buttonRollTransfery
            // 
            this.buttonRollTransfery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonRollTransfery.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonRollTransfery.FlatAppearance.BorderSize = 0;
            this.buttonRollTransfery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRollTransfery.Location = new System.Drawing.Point(229, 0);
            this.buttonRollTransfery.Name = "buttonRollTransfery";
            this.buttonRollTransfery.Size = new System.Drawing.Size(35, 34);
            this.buttonRollTransfery.TabIndex = 1;
            this.buttonRollTransfery.Text = "^";
            this.toolTipInfo.SetToolTip(this.buttonRollTransfery, "Schovaj");
            this.buttonRollTransfery.UseVisualStyleBackColor = true;
            this.buttonRollTransfery.Click += new System.EventHandler(this.buttonRollTransfery_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 34);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(264, 1);
            this.panel2.TabIndex = 8;
            // 
            // panelTransferyData
            // 
            this.panelTransferyData.BackColor = System.Drawing.Color.White;
            this.panelTransferyData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelTransferyData.Controls.Add(this.listBoxTransferove);
            this.panelTransferyData.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTransferyData.Location = new System.Drawing.Point(0, 479);
            this.panelTransferyData.Name = "panelTransferyData";
            this.panelTransferyData.Size = new System.Drawing.Size(264, 187);
            this.panelTransferyData.TabIndex = 6;
            // 
            // listBoxTransferove
            // 
            this.listBoxTransferove.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxTransferove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxTransferove.FormattingEnabled = true;
            this.listBoxTransferove.Location = new System.Drawing.Point(0, 0);
            this.listBoxTransferove.Name = "listBoxTransferove";
            this.listBoxTransferove.Size = new System.Drawing.Size(264, 187);
            this.listBoxTransferove.TabIndex = 0;
            this.listBoxTransferove.SelectedIndexChanged += new System.EventHandler(this.listBoxTransferove_SelectedIndexChanged);
            // 
            // panelVydavkyData
            // 
            this.panelVydavkyData.BackColor = System.Drawing.Color.White;
            this.panelVydavkyData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelVydavkyData.Controls.Add(this.listBoxVydavkove);
            this.panelVydavkyData.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelVydavkyData.Location = new System.Drawing.Point(0, 257);
            this.panelVydavkyData.Name = "panelVydavkyData";
            this.panelVydavkyData.Size = new System.Drawing.Size(264, 187);
            this.panelVydavkyData.TabIndex = 7;
            // 
            // listBoxVydavkove
            // 
            this.listBoxVydavkove.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxVydavkove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxVydavkove.FormattingEnabled = true;
            this.listBoxVydavkove.Location = new System.Drawing.Point(0, 0);
            this.listBoxVydavkove.Name = "listBoxVydavkove";
            this.listBoxVydavkove.Size = new System.Drawing.Size(264, 187);
            this.listBoxVydavkove.TabIndex = 0;
            this.listBoxVydavkove.SelectedIndexChanged += new System.EventHandler(this.listBoxVydavkove_SelectedIndexChanged);
            // 
            // panelPrijmyData
            // 
            this.panelPrijmyData.BackColor = System.Drawing.Color.White;
            this.panelPrijmyData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelPrijmyData.Controls.Add(this.listBoxPrijmove);
            this.panelPrijmyData.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPrijmyData.Location = new System.Drawing.Point(0, 35);
            this.panelPrijmyData.Name = "panelPrijmyData";
            this.panelPrijmyData.Size = new System.Drawing.Size(264, 187);
            this.panelPrijmyData.TabIndex = 8;
            // 
            // listBoxPrijmove
            // 
            this.listBoxPrijmove.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxPrijmove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxPrijmove.FormattingEnabled = true;
            this.listBoxPrijmove.Location = new System.Drawing.Point(0, 0);
            this.listBoxPrijmove.Name = "listBoxPrijmove";
            this.listBoxPrijmove.Size = new System.Drawing.Size(264, 187);
            this.listBoxPrijmove.TabIndex = 0;
            this.listBoxPrijmove.SelectedIndexChanged += new System.EventHandler(this.listBoxPrijmove_SelectedIndexChanged);
            // 
            // ZostavyPrehladControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.panelTransferyData);
            this.Controls.Add(this.panelTransfery);
            this.Controls.Add(this.panelVydavkyData);
            this.Controls.Add(this.panelVydavky);
            this.Controls.Add(this.panelPrijmyData);
            this.Controls.Add(this.panelPrijmy);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ZostavyPrehladControl";
            this.Size = new System.Drawing.Size(264, 624);
            this.panelPrijmy.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPrijmy)).EndInit();
            this.panelVydavky.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVydavky)).EndInit();
            this.panelTransfery.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTransfery)).EndInit();
            this.panelTransferyData.ResumeLayout(false);
            this.panelVydavkyData.ResumeLayout(false);
            this.panelPrijmyData.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelPrijmy;
        private System.Windows.Forms.Panel panelVydavky;
        private System.Windows.Forms.Panel panelTransfery;
        private System.Windows.Forms.Button buttonRollPrijmy;
        private System.Windows.Forms.Button buttonRollVydavky;
        private System.Windows.Forms.Button buttonRollTransfery;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private System.Windows.Forms.Panel panelTransferyData;
        private System.Windows.Forms.Panel panelVydavkyData;
        private System.Windows.Forms.Panel panelPrijmyData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxPrijmove;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBoxTransferove;
        private System.Windows.Forms.ListBox listBoxVydavkove;
        private System.Windows.Forms.PictureBox pictureBoxPrijmy;
        private System.Windows.Forms.PictureBox pictureBoxVydavky;
        private System.Windows.Forms.PictureBox pictureBoxTransfery;
        private System.Windows.Forms.Panel panelSeparator;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}
