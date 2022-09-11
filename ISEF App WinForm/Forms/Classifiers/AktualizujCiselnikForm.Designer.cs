namespace cvti.isef.winformapp.Forms
{
    partial class AktualizujCiselnikForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AktualizujCiselnikForm));
            this.labelInfo = new System.Windows.Forms.Label();
            this.comboBoxRok = new System.Windows.Forms.ComboBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.panelHorizonatalSeparator = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImageIcon)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.Size = new System.Drawing.Size(475, 75);
            this.labelTitle.Text = "Aktualizovanie číselníku {cl} pre rok {rok} ";
            // 
            // panelControlButtons
            // 
            this.panelControlButtons.Location = new System.Drawing.Point(0, 362);
            this.panelControlButtons.Size = new System.Drawing.Size(550, 50);
            // 
            // labelInfo
            // 
            this.labelInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelInfo.Location = new System.Drawing.Point(3, 0);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Padding = new System.Windows.Forms.Padding(10);
            this.labelInfo.Size = new System.Drawing.Size(498, 63);
            this.labelInfo.TabIndex = 12;
            this.labelInfo.Text = resources.GetString("labelInfo.Text");
            // 
            // comboBoxRok
            // 
            this.comboBoxRok.Enabled = false;
            this.comboBoxRok.FormattingEnabled = true;
            this.comboBoxRok.Location = new System.Drawing.Point(10, 133);
            this.comboBoxRok.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.comboBoxRok.Name = "comboBoxRok";
            this.comboBoxRok.Size = new System.Drawing.Size(178, 21);
            this.comboBoxRok.TabIndex = 8;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(10, 87);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(210, 17);
            this.radioButton2.TabIndex = 14;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Aktualizácia z prednahratých údajov";
            this.toolTipInfo.SetToolTip(this.radioButton2, "Aktualizuje číselnik pre vybraný rok z prednahratýcch údajov");
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(10, 110);
            this.radioButton3.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(178, 17);
            this.radioButton3.TabIndex = 15;
            this.radioButton3.Text = "Aktualizácia z vybraného roku";
            this.toolTipInfo.SetToolTip(this.radioButton3, "Aktualizuje číselník z už nahratého roku");
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // panelHorizonatalSeparator
            // 
            this.panelHorizonatalSeparator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelHorizonatalSeparator.BackColor = System.Drawing.Color.LightGray;
            this.panelHorizonatalSeparator.Location = new System.Drawing.Point(10, 73);
            this.panelHorizonatalSeparator.Margin = new System.Windows.Forms.Padding(10);
            this.panelHorizonatalSeparator.Name = "panelHorizonatalSeparator";
            this.panelHorizonatalSeparator.Size = new System.Drawing.Size(530, 1);
            this.panelHorizonatalSeparator.TabIndex = 16;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.labelInfo, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxRok, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.radioButton3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panelHorizonatalSeparator, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.radioButton2, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 85);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(550, 277);
            this.tableLayoutPanel1.TabIndex = 17;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // AktualizujCiselnikForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 412);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(566, 451);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(566, 451);
            this.Name = "AktualizujCiselnikForm";
            this.ShowButtonsPanel = true;
            this.ShowCancelButton = true;
            this.ShowImageIcon = true;
            this.ShowOkButton = true;
            this.Text = "Aktualizuj číselník";
            this.TitleText = "Aktualizovanie číselníku {cl} pre rok {rok} ";
            this.Controls.SetChildIndex(this.panelControlButtons, 0);
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImageIcon)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.ComboBox comboBoxRok;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private System.Windows.Forms.Panel panelHorizonatalSeparator;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}