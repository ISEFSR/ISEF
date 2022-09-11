namespace cvti.isef.winformapp.Forms
{
    partial class ApplicationSetupForm
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
            this.verticalProgress = new cvti.isef.winformapp.Controls.Main.Import.VerticalProgress();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonPrevious = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.panelVerticalSeparator = new System.Windows.Forms.Panel();
            this.applicationSetupControl1 = new cvti.isef.winformapp.Controls.Setup.ApplicationSetupControl1();
            this.applicationSetupControl2 = new cvti.isef.winformapp.Controls.Setup.ApplicationSetupControl2();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.applicationSetupControl3 = new cvti.isef.winformapp.Controls.Setup.ApplicationSetupControl3();
            this.applicationSetupControl4 = new cvti.isef.winformapp.Controls.Setup.ApplicationSetupControl4();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImageIcon)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxImageIcon
            // 
            this.pictureBoxImageIcon.Image = global::cvti.isef.winformapp.Properties.Resources.start1001;
            // 
            // labelTitle
            // 
            this.labelTitle.Text = "Nastavenie prostredia pre aplikáciu ISEF";
            // 
            // panelControlButtons
            // 
            this.panelControlButtons.Location = new System.Drawing.Point(0, 450);
            this.panelControlButtons.Size = new System.Drawing.Size(841, 50);
            // 
            // verticalProgress
            // 
            this.verticalProgress.Dock = System.Windows.Forms.DockStyle.Left;
            this.verticalProgress.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.verticalProgress.Location = new System.Drawing.Point(0, 85);
            this.verticalProgress.Name = "verticalProgress";
            this.verticalProgress.ShowNumber = true;
            this.verticalProgress.Size = new System.Drawing.Size(222, 415);
            this.verticalProgress.TabIndex = 2;
            // 
            // panelButtons
            // 
            this.panelButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelButtons.Controls.Add(this.buttonPrevious);
            this.panelButtons.Controls.Add(this.buttonNext);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 500);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(841, 50);
            this.panelButtons.TabIndex = 3;
            this.panelButtons.Paint += new System.Windows.Forms.PaintEventHandler(this.panelButtons_Paint);
            // 
            // buttonPrevious
            // 
            this.buttonPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPrevious.Location = new System.Drawing.Point(623, 13);
            this.buttonPrevious.Name = "buttonPrevious";
            this.buttonPrevious.Size = new System.Drawing.Size(100, 25);
            this.buttonPrevious.TabIndex = 1;
            this.buttonPrevious.Text = "Naspäť";
            this.toolTipInfo.SetToolTip(this.buttonPrevious, "Návrat späť na predchádzajúci krok");
            this.buttonPrevious.UseVisualStyleBackColor = true;
            this.buttonPrevious.Click += new System.EventHandler(this.buttonPrevious_Click);
            // 
            // buttonNext
            // 
            this.buttonNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNext.Location = new System.Drawing.Point(729, 13);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(100, 25);
            this.buttonNext.TabIndex = 0;
            this.buttonNext.Text = "Ďalej";
            this.toolTipInfo.SetToolTip(this.buttonNext, "Ďaľší krok");
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // panelVerticalSeparator
            // 
            this.panelVerticalSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.panelVerticalSeparator.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelVerticalSeparator.Location = new System.Drawing.Point(222, 85);
            this.panelVerticalSeparator.Name = "panelVerticalSeparator";
            this.panelVerticalSeparator.Size = new System.Drawing.Size(1, 415);
            this.panelVerticalSeparator.TabIndex = 4;
            // 
            // applicationSetupControl1
            // 
            this.applicationSetupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.applicationSetupControl1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.applicationSetupControl1.Location = new System.Drawing.Point(223, 85);
            this.applicationSetupControl1.Name = "applicationSetupControl1";
            this.applicationSetupControl1.Size = new System.Drawing.Size(618, 415);
            this.applicationSetupControl1.TabIndex = 5;
            // 
            // applicationSetupControl2
            // 
            this.applicationSetupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.applicationSetupControl2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.applicationSetupControl2.Location = new System.Drawing.Point(223, 85);
            this.applicationSetupControl2.Name = "applicationSetupControl2";
            this.applicationSetupControl2.Size = new System.Drawing.Size(618, 415);
            this.applicationSetupControl2.TabIndex = 6;
            // 
            // applicationSetupControl3
            // 
            this.applicationSetupControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.applicationSetupControl3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.applicationSetupControl3.Location = new System.Drawing.Point(223, 85);
            this.applicationSetupControl3.Name = "applicationSetupControl3";
            this.applicationSetupControl3.Size = new System.Drawing.Size(618, 415);
            this.applicationSetupControl3.TabIndex = 7;
            // 
            // applicationSetupControl4
            // 
            this.applicationSetupControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.applicationSetupControl4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.applicationSetupControl4.Location = new System.Drawing.Point(223, 85);
            this.applicationSetupControl4.Name = "applicationSetupControl4";
            this.applicationSetupControl4.Size = new System.Drawing.Size(618, 415);
            this.applicationSetupControl4.TabIndex = 8;
            // 
            // ApplicationSetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(841, 550);
            this.Controls.Add(this.applicationSetupControl4);
            this.Controls.Add(this.applicationSetupControl3);
            this.Controls.Add(this.applicationSetupControl2);
            this.Controls.Add(this.applicationSetupControl1);
            this.Controls.Add(this.panelVerticalSeparator);
            this.Controls.Add(this.verticalProgress);
            this.Controls.Add(this.panelButtons);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.ImageIcon = global::cvti.isef.winformapp.Properties.Resources.start1001;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ApplicationSetupForm";
            this.ShowImageIcon = true;
            this.Text = "Nastavenie prostredia";
            this.TitleText = "Nastavenie prostredia pre aplikáciu ISEF";
            this.Controls.SetChildIndex(this.panelButtons, 0);
            this.Controls.SetChildIndex(this.panelControlButtons, 0);
            this.Controls.SetChildIndex(this.verticalProgress, 0);
            this.Controls.SetChildIndex(this.panelVerticalSeparator, 0);
            this.Controls.SetChildIndex(this.applicationSetupControl1, 0);
            this.Controls.SetChildIndex(this.applicationSetupControl2, 0);
            this.Controls.SetChildIndex(this.applicationSetupControl3, 0);
            this.Controls.SetChildIndex(this.applicationSetupControl4, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImageIcon)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.Main.Import.VerticalProgress verticalProgress;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button buttonPrevious;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Panel panelVerticalSeparator;
        private Controls.Setup.ApplicationSetupControl1 applicationSetupControl1;
        private Controls.Setup.ApplicationSetupControl2 applicationSetupControl2;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private Controls.Setup.ApplicationSetupControl3 applicationSetupControl3;
        private Controls.Setup.ApplicationSetupControl4 applicationSetupControl4;
    }
}