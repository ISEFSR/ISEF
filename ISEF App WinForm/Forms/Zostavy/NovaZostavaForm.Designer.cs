namespace cvti.isef.winformapp.Forms
{
    partial class NovaZostavaForm
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
            this.verticalProgress = new cvti.isef.winformapp.Controls.Main.Import.VerticalProgress();
            this.novaZostavaFirstStep = new cvti.isef.winformapp.Controls.Main.Zostavy.NovaZostavaFirstStep();
            this.panelSeparator = new System.Windows.Forms.Panel();
            this.panelButtons2 = new System.Windows.Forms.Panel();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.novaZostavaSecondStep = new cvti.isef.winformapp.Controls.Main.Zostavy.NovaZostavaSecondStep();
            this.novaZostavaFinalStep = new cvti.isef.winformapp.Controls.Main.Zostavy.NovaZostavaFinalStep();
            this.novaZostavaThirdStep1 = new cvti.isef.winformapp.Controls.Main.Zostavy.NovaZostavaThirdStep();
            this.panelButtons2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControlButtons
            // 
            this.panelControlButtons.Location = new System.Drawing.Point(0, 527);
            this.panelControlButtons.Size = new System.Drawing.Size(879, 50);
            // 
            // labelTitle
            // 
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTitle.Size = new System.Drawing.Size(804, 75);
            this.labelTitle.Text = "Generovanie novej zostavy - výber hlavičky";
            // 
            // verticalProgress
            // 
            this.verticalProgress.Dock = System.Windows.Forms.DockStyle.Left;
            this.verticalProgress.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.verticalProgress.Location = new System.Drawing.Point(0, 85);
            this.verticalProgress.Name = "verticalProgress";
            this.verticalProgress.ShowNumber = true;
            this.verticalProgress.Size = new System.Drawing.Size(259, 442);
            this.verticalProgress.TabIndex = 2;
            // 
            // novaZostavaFirstStep
            // 
            this.novaZostavaFirstStep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.novaZostavaFirstStep.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.novaZostavaFirstStep.Location = new System.Drawing.Point(0, 85);
            this.novaZostavaFirstStep.Name = "novaZostavaFirstStep";
            this.novaZostavaFirstStep.Size = new System.Drawing.Size(879, 535);
            this.novaZostavaFirstStep.TabIndex = 3;
            // 
            // panelSeparator
            // 
            this.panelSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.panelSeparator.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSeparator.Location = new System.Drawing.Point(259, 85);
            this.panelSeparator.Name = "panelSeparator";
            this.panelSeparator.Size = new System.Drawing.Size(1, 442);
            this.panelSeparator.TabIndex = 4;
            // 
            // panelButtons2
            // 
            this.panelButtons2.BackColor = System.Drawing.SystemColors.Control;
            this.panelButtons2.Controls.Add(this.buttonBack);
            this.panelButtons2.Controls.Add(this.buttonNext);
            this.panelButtons2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons2.Location = new System.Drawing.Point(0, 577);
            this.panelButtons2.Name = "panelButtons2";
            this.panelButtons2.Size = new System.Drawing.Size(879, 43);
            this.panelButtons2.TabIndex = 5;
            this.panelButtons2.Paint += new System.Windows.Forms.PaintEventHandler(this.panelButtons2_Paint);
            // 
            // buttonBack
            // 
            this.buttonBack.Enabled = false;
            this.buttonBack.Location = new System.Drawing.Point(661, 9);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(100, 25);
            this.buttonBack.TabIndex = 1;
            this.buttonBack.Text = "Naspäť";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // buttonNext
            // 
            this.buttonNext.Location = new System.Drawing.Point(767, 9);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(100, 25);
            this.buttonNext.TabIndex = 0;
            this.buttonNext.Text = "Ďalej";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // novaZostavaSecondStep
            // 
            this.novaZostavaSecondStep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.novaZostavaSecondStep.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.novaZostavaSecondStep.Location = new System.Drawing.Point(260, 85);
            this.novaZostavaSecondStep.Name = "novaZostavaSecondStep";
            this.novaZostavaSecondStep.Size = new System.Drawing.Size(619, 442);
            this.novaZostavaSecondStep.TabIndex = 6;
            // 
            // novaZostavaFinalStep
            // 
            this.novaZostavaFinalStep.BackColor = System.Drawing.SystemColors.Control;
            this.novaZostavaFinalStep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.novaZostavaFinalStep.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.novaZostavaFinalStep.Location = new System.Drawing.Point(260, 85);
            this.novaZostavaFinalStep.Name = "novaZostavaFinalStep";
            this.novaZostavaFinalStep.Size = new System.Drawing.Size(619, 442);
            this.novaZostavaFinalStep.TabIndex = 7;
            // 
            // novaZostavaThirdStep1
            // 
            this.novaZostavaThirdStep1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.novaZostavaThirdStep1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.novaZostavaThirdStep1.Location = new System.Drawing.Point(260, 85);
            this.novaZostavaThirdStep1.Name = "novaZostavaThirdStep1";
            this.novaZostavaThirdStep1.Size = new System.Drawing.Size(619, 442);
            this.novaZostavaThirdStep1.TabIndex = 8;
            // 
            // NovaZostavaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 620);
            this.Controls.Add(this.novaZostavaThirdStep1);
            this.Controls.Add(this.novaZostavaFinalStep);
            this.Controls.Add(this.novaZostavaSecondStep);
            this.Controls.Add(this.panelSeparator);
            this.Controls.Add(this.verticalProgress);
            this.Controls.Add(this.panelButtons2);
            this.Controls.Add(this.novaZostavaFirstStep);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.ImageIcon = global::cvti.isef.winformapp.Properties.Resources.report75;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NovaZostavaForm";
            this.ShowButtonsPanel = true;
            this.ShowCancelButton = true;
            this.ShowImageIcon = true;
            this.ShowOkButton = true;
            this.Text = "Nová zostava";
            this.TitleText = "Generovanie novej zostavy - výber hlavičky";
            this.Controls.SetChildIndex(this.novaZostavaFirstStep, 0);
            this.Controls.SetChildIndex(this.panelButtons2, 0);
            this.Controls.SetChildIndex(this.panelControlButtons, 0);
            this.Controls.SetChildIndex(this.verticalProgress, 0);
            this.Controls.SetChildIndex(this.panelSeparator, 0);
            this.Controls.SetChildIndex(this.novaZostavaSecondStep, 0);
            this.Controls.SetChildIndex(this.novaZostavaFinalStep, 0);
            this.Controls.SetChildIndex(this.novaZostavaThirdStep1, 0);
            this.panelButtons2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.Main.Import.VerticalProgress verticalProgress;
        private Controls.Main.Zostavy.NovaZostavaFirstStep novaZostavaFirstStep;
        private System.Windows.Forms.Panel panelSeparator;
        private System.Windows.Forms.Panel panelButtons2;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonNext;
        private Controls.Main.Zostavy.NovaZostavaSecondStep novaZostavaSecondStep;
        private Controls.Main.Zostavy.NovaZostavaFinalStep novaZostavaFinalStep;
        private Controls.Main.Zostavy.NovaZostavaThirdStep novaZostavaThirdStep1;
    }
}