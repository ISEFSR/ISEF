
namespace cvti.isef.winformapp.Forms.Commands
{
    partial class NewSubstringFunctionForm
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
            this.panelWrapper = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownStartPosition = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownPlaces = new System.Windows.Forms.NumericUpDown();
            this.panelWrapper.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPlaces)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlButtons
            // 
            this.panelControlButtons.Location = new System.Drawing.Point(0, 317);
            this.panelControlButtons.Size = new System.Drawing.Size(504, 50);
            // 
            // labelTitle
            // 
            this.labelTitle.Text = "Vytvor SUBSTRING funkciu";
            // 
            // panelWrapper
            // 
            this.panelWrapper.Controls.Add(this.tableLayoutPanel1);
            this.panelWrapper.Controls.Add(this.label2);
            this.panelWrapper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelWrapper.Location = new System.Drawing.Point(0, 85);
            this.panelWrapper.Name = "panelWrapper";
            this.panelWrapper.Size = new System.Drawing.Size(504, 232);
            this.panelWrapper.TabIndex = 16;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.numericUpDownPlaces, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDownStartPosition, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 63);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(504, 125);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(15, 0, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Začiatočná pozícia:";
            // 
            // numericUpDownStartPosition
            // 
            this.numericUpDownStartPosition.Location = new System.Drawing.Point(15, 16);
            this.numericUpDownStartPosition.Margin = new System.Windows.Forms.Padding(15, 3, 3, 3);
            this.numericUpDownStartPosition.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numericUpDownStartPosition.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownStartPosition.Name = "numericUpDownStartPosition";
            this.numericUpDownStartPosition.Size = new System.Drawing.Size(206, 22);
            this.numericUpDownStartPosition.TabIndex = 1;
            this.numericUpDownStartPosition.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.label2.Size = new System.Drawing.Size(504, 63);
            this.label2.TabIndex = 14;
            this.label2.Text = "Vyberte prosím začiatočnú pozíciu pre SBUSTRING a tiež počet znakov ktoré SUBSTRI" +
    "NG zaberie.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 41);
            this.label3.Margin = new System.Windows.Forms.Padding(15, 0, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Počet miest:";
            // 
            // numericUpDownPlaces
            // 
            this.numericUpDownPlaces.Location = new System.Drawing.Point(15, 57);
            this.numericUpDownPlaces.Margin = new System.Windows.Forms.Padding(15, 3, 3, 3);
            this.numericUpDownPlaces.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numericUpDownPlaces.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownPlaces.Name = "numericUpDownPlaces";
            this.numericUpDownPlaces.Size = new System.Drawing.Size(206, 22);
            this.numericUpDownPlaces.TabIndex = 3;
            this.numericUpDownPlaces.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // NewSubstringFunctionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 367);
            this.Controls.Add(this.panelWrapper);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewSubstringFunctionForm";
            this.ShowButtonsPanel = true;
            this.ShowCancelButton = true;
            this.ShowImageIcon = true;
            this.ShowOkButton = true;
            this.Text = "Nová SUBSTRING funkcia";
            this.TitleText = "Vytvor SUBSTRING funkciu";
            this.Controls.SetChildIndex(this.panelControlButtons, 0);
            this.Controls.SetChildIndex(this.panelWrapper, 0);
            this.panelWrapper.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPlaces)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelWrapper;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownStartPosition;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownPlaces;
        private System.Windows.Forms.Label label3;
    }
}