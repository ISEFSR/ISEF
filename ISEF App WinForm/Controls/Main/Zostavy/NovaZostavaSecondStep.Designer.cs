namespace cvti.isef.winformapp.Controls.Main.Zostavy
{
    partial class NovaZostavaSecondStep
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NovaZostavaSecondStep));
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listViewConditions = new System.Windows.Forms.ListView();
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.checkBoxUseCondition = new System.Windows.Forms.CheckBox();
            this.panelSeparator = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(0, 36);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(15, 10, 15, 0);
            this.label2.Size = new System.Drawing.Size(671, 71);
            this.label2.TabIndex = 3;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(15, 15, 3, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(15, 15, 0, 0);
            this.label1.Size = new System.Drawing.Size(161, 36);
            this.label1.TabIndex = 2;
            this.label1.Text = "Výber podmienky";
            // 
            // listViewConditions
            // 
            this.listViewConditions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewConditions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName});
            this.listViewConditions.HideSelection = false;
            this.listViewConditions.Location = new System.Drawing.Point(10, 144);
            this.listViewConditions.Margin = new System.Windows.Forms.Padding(10, 0, 10, 10);
            this.listViewConditions.Name = "listViewConditions";
            this.listViewConditions.Size = new System.Drawing.Size(651, 324);
            this.listViewConditions.TabIndex = 4;
            this.listViewConditions.UseCompatibleStateImageBehavior = false;
            this.listViewConditions.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Názov";
            this.columnHeaderName.Width = 260;
            // 
            // checkBoxUseCondition
            // 
            this.checkBoxUseCondition.AutoSize = true;
            this.checkBoxUseCondition.Checked = true;
            this.checkBoxUseCondition.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUseCondition.Location = new System.Drawing.Point(10, 117);
            this.checkBoxUseCondition.Margin = new System.Windows.Forms.Padding(10);
            this.checkBoxUseCondition.Name = "checkBoxUseCondition";
            this.checkBoxUseCondition.Size = new System.Drawing.Size(179, 17);
            this.checkBoxUseCondition.TabIndex = 5;
            this.checkBoxUseCondition.Text = "Vyber podmienku pre zostavu";
            this.checkBoxUseCondition.UseVisualStyleBackColor = true;
            this.checkBoxUseCondition.CheckedChanged += new System.EventHandler(this.checkBoxUseCondition_CheckedChanged);
            // 
            // panelSeparator
            // 
            this.panelSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.panelSeparator.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSeparator.Location = new System.Drawing.Point(0, 107);
            this.panelSeparator.Name = "panelSeparator";
            this.panelSeparator.Size = new System.Drawing.Size(671, 1);
            this.panelSeparator.TabIndex = 6;
            // 
            // NovaZostavaSecondStep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelSeparator);
            this.Controls.Add(this.checkBoxUseCondition);
            this.Controls.Add(this.listViewConditions);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "NovaZostavaSecondStep";
            this.Size = new System.Drawing.Size(671, 478);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listViewConditions;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.CheckBox checkBoxUseCondition;
        private System.Windows.Forms.Panel panelSeparator;
    }
}
