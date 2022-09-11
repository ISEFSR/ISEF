namespace cvti.isef.winformapp.Forms.Conditions
{
    partial class NewConditionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewConditionForm));
            this.checkBoxNegate = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxCondition = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxOperator = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControlCondition = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBoxValue = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxConditionType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxColumn = new System.Windows.Forms.ComboBox();
            this.labelCoreCondition = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControlCondition.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControlButtons
            // 
            this.panelControlButtons.Location = new System.Drawing.Point(0, 361);
            this.panelControlButtons.Size = new System.Drawing.Size(534, 50);
            // 
            // checkBoxNegate
            // 
            this.checkBoxNegate.AutoSize = true;
            this.checkBoxNegate.Location = new System.Drawing.Point(15, 138);
            this.checkBoxNegate.Name = "checkBoxNegate";
            this.checkBoxNegate.Size = new System.Drawing.Size(119, 17);
            this.checkBoxNegate.TabIndex = 37;
            this.checkBoxNegate.Text = "Neguj podmienku";
            this.checkBoxNegate.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Cursor = System.Windows.Forms.Cursors.Help;
            this.label4.Location = new System.Drawing.Point(8, 8);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 5, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 34;
            this.label4.Text = "Podmienka:";
            // 
            // comboBoxCondition
            // 
            this.comboBoxCondition.FormattingEnabled = true;
            this.comboBoxCondition.Location = new System.Drawing.Point(11, 24);
            this.comboBoxCondition.Name = "comboBoxCondition";
            this.comboBoxCondition.Size = new System.Drawing.Size(279, 21);
            this.comboBoxCondition.TabIndex = 33;
            this.comboBoxCondition.SelectedIndexChanged += new System.EventHandler(this.comboBoxCondition_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Help;
            this.label3.Location = new System.Drawing.Point(12, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "Operátor podmienky:";
            // 
            // comboBoxOperator
            // 
            this.comboBoxOperator.FormattingEnabled = true;
            this.comboBoxOperator.Location = new System.Drawing.Point(15, 111);
            this.comboBoxOperator.Name = "comboBoxOperator";
            this.comboBoxOperator.Size = new System.Drawing.Size(200, 21);
            this.comboBoxOperator.TabIndex = 31;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(10, 5, 10, 0);
            this.label2.Size = new System.Drawing.Size(534, 62);
            this.label2.TabIndex = 30;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // tabControlCondition
            // 
            this.tabControlCondition.Controls.Add(this.tabPage1);
            this.tabControlCondition.Controls.Add(this.tabPage2);
            this.tabControlCondition.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControlCondition.Location = new System.Drawing.Point(0, 164);
            this.tabControlCondition.Name = "tabControlCondition";
            this.tabControlCondition.SelectedIndex = 0;
            this.tabControlCondition.Size = new System.Drawing.Size(534, 112);
            this.tabControlCondition.TabIndex = 39;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.comboBoxCondition);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(526, 86);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Existujúca podmienka";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBoxValue);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.comboBoxConditionType);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.comboBoxColumn);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(526, 86);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Nová podmienka";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBoxValue
            // 
            this.textBoxValue.Location = new System.Drawing.Point(239, 24);
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.Size = new System.Drawing.Size(225, 22);
            this.textBoxValue.TabIndex = 40;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Cursor = System.Windows.Forms.Cursors.Help;
            this.label8.Location = new System.Drawing.Point(236, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 39;
            this.label8.Text = "Hodnota:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Cursor = System.Windows.Forms.Cursors.Help;
            this.label6.Location = new System.Drawing.Point(8, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 36;
            this.label6.Text = "Podmienka:";
            // 
            // comboBoxConditionType
            // 
            this.comboBoxConditionType.FormattingEnabled = true;
            this.comboBoxConditionType.Location = new System.Drawing.Point(11, 64);
            this.comboBoxConditionType.Name = "comboBoxConditionType";
            this.comboBoxConditionType.Size = new System.Drawing.Size(200, 21);
            this.comboBoxConditionType.TabIndex = 35;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Cursor = System.Windows.Forms.Cursors.Help;
            this.label5.Location = new System.Drawing.Point(8, 8);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 5, 3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 34;
            this.label5.Text = "Stĺpec:";
            // 
            // comboBoxColumn
            // 
            this.comboBoxColumn.FormattingEnabled = true;
            this.comboBoxColumn.Location = new System.Drawing.Point(11, 24);
            this.comboBoxColumn.Name = "comboBoxColumn";
            this.comboBoxColumn.Size = new System.Drawing.Size(200, 21);
            this.comboBoxColumn.TabIndex = 33;
            // 
            // labelCoreCondition
            // 
            this.labelCoreCondition.AutoSize = true;
            this.labelCoreCondition.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelCoreCondition.Location = new System.Drawing.Point(0, 62);
            this.labelCoreCondition.Name = "labelCoreCondition";
            this.labelCoreCondition.Padding = new System.Windows.Forms.Padding(10, 5, 0, 0);
            this.labelCoreCondition.Size = new System.Drawing.Size(190, 18);
            this.labelCoreCondition.TabIndex = 40;
            this.labelCoreCondition.Text = "AssuView.Column = \"some_value\"";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboBoxOperator);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.labelCoreCondition);
            this.panel1.Controls.Add(this.tabControlCondition);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.checkBoxNegate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 85);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(534, 276);
            this.panel1.TabIndex = 41;
            // 
            // NewConditionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(534, 411);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(550, 450);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(550, 450);
            this.Name = "NewConditionForm";
            this.ShowButtonsPanel = true;
            this.ShowCancelButton = true;
            this.ShowImageIcon = true;
            this.ShowOkButton = true;
            this.Text = "Pridaj podmienku";
            this.Load += new System.EventHandler(this.NewConditionForm_Load);
            this.Controls.SetChildIndex(this.panelControlButtons, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.tabControlCondition.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxNegate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxCondition;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxOperator;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabControlCondition;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox textBoxValue;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxConditionType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxColumn;
        private System.Windows.Forms.Label labelCoreCondition;
        private System.Windows.Forms.Panel panel1;
    }
}