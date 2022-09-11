
namespace cvti.isef.winformapp.Forms.Commands
{
    partial class NewRoundFunctionForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.panelWrapper = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownDecimalPlaces = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelWrapper.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDecimalPlaces)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControlButtons
            // 
            this.panelControlButtons.Location = new System.Drawing.Point(0, 367);
            this.panelControlButtons.Size = new System.Drawing.Size(579, 50);
            // 
            // labelTitle
            // 
            this.labelTitle.Text = "Nova ROUND funkcia";
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.label2.Size = new System.Drawing.Size(579, 63);
            this.label2.TabIndex = 14;
            this.label2.Text = "Vyberte prosim pocet desatinnych miest na ktore chcete zaokruhlit hodnotu. Minima" +
    "lny pocet je 1 maximalny pocet je 6.";
            // 
            // panelWrapper
            // 
            this.panelWrapper.Controls.Add(this.tableLayoutPanel1);
            this.panelWrapper.Controls.Add(this.label2);
            this.panelWrapper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelWrapper.Location = new System.Drawing.Point(0, 85);
            this.panelWrapper.Name = "panelWrapper";
            this.panelWrapper.Size = new System.Drawing.Size(579, 282);
            this.panelWrapper.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(15, 0, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Desatinne miesta:";
            // 
            // numericUpDownDecimalPlaces
            // 
            this.numericUpDownDecimalPlaces.Location = new System.Drawing.Point(15, 16);
            this.numericUpDownDecimalPlaces.Margin = new System.Windows.Forms.Padding(15, 3, 3, 3);
            this.numericUpDownDecimalPlaces.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numericUpDownDecimalPlaces.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownDecimalPlaces.Name = "numericUpDownDecimalPlaces";
            this.numericUpDownDecimalPlaces.Size = new System.Drawing.Size(206, 22);
            this.numericUpDownDecimalPlaces.TabIndex = 1;
            this.numericUpDownDecimalPlaces.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDownDecimalPlaces, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 63);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(579, 125);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // NewRoundFunctionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 417);
            this.Controls.Add(this.panelWrapper);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewRoundFunctionForm";
            this.ShowButtonsPanel = true;
            this.ShowCancelButton = true;
            this.ShowImageIcon = true;
            this.ShowOkButton = true;
            this.Text = "Nova ROUND funkcia";
            this.TitleText = "Nova ROUND funkcia";
            this.Controls.SetChildIndex(this.panelControlButtons, 0);
            this.Controls.SetChildIndex(this.panelWrapper, 0);
            this.panelWrapper.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDecimalPlaces)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelWrapper;
        private System.Windows.Forms.NumericUpDown numericUpDownDecimalPlaces;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}