namespace cvti.isef.winformapp.Forms.Commands
{
    partial class NewCommandForm
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
            this.textBoxCommandName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonRemoveSelected = new System.Windows.Forms.Button();
            this.listBoxSelectedColumns = new System.Windows.Forms.ListBox();
            this.buttonMoveSelected = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.listBoxAvailableColumns = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.errorProviderInvalidName = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderInvalidName)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControlButtons
            // 
            this.panelControlButtons.Location = new System.Drawing.Point(0, 421);
            this.panelControlButtons.Size = new System.Drawing.Size(609, 50);
            // 
            // labelTitle
            // 
            this.labelTitle.Text = "Generovanie SQL SELECT príkazu";
            // 
            // textBoxCommandName
            // 
            this.textBoxCommandName.Location = new System.Drawing.Point(12, 152);
            this.textBoxCommandName.Name = "textBoxCommandName";
            this.textBoxCommandName.Size = new System.Drawing.Size(270, 22);
            this.textBoxCommandName.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Cursor = System.Windows.Forms.Cursors.Help;
            this.label5.Location = new System.Drawing.Point(9, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Meno SELECT prikazu:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Cursor = System.Windows.Forms.Cursors.Help;
            this.label4.Location = new System.Drawing.Point(320, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Selected columns:";
            // 
            // buttonRemoveSelected
            // 
            this.buttonRemoveSelected.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonRemoveSelected.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonRemoveSelected.Location = new System.Drawing.Point(10, 115);
            this.buttonRemoveSelected.Name = "buttonRemoveSelected";
            this.buttonRemoveSelected.Size = new System.Drawing.Size(25, 25);
            this.buttonRemoveSelected.TabIndex = 20;
            this.buttonRemoveSelected.Text = "<";
            this.buttonRemoveSelected.UseVisualStyleBackColor = true;
            this.buttonRemoveSelected.Click += new System.EventHandler(this.buttonRemoveSelected_Click_1);
            // 
            // listBoxSelectedColumns
            // 
            this.listBoxSelectedColumns.AllowDrop = true;
            this.listBoxSelectedColumns.FormattingEnabled = true;
            this.listBoxSelectedColumns.Location = new System.Drawing.Point(320, 16);
            this.listBoxSelectedColumns.Name = "listBoxSelectedColumns";
            this.listBoxSelectedColumns.Size = new System.Drawing.Size(262, 212);
            this.listBoxSelectedColumns.TabIndex = 19;
            this.listBoxSelectedColumns.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBoxSelectedColumns_DragDrop);
            this.listBoxSelectedColumns.DragOver += new System.Windows.Forms.DragEventHandler(this.listBoxSelectedColumns_DragOver);
            this.listBoxSelectedColumns.DoubleClick += new System.EventHandler(this.listBoxSelectedColumns_DoubleClick);
            this.listBoxSelectedColumns.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxSelectedColumns_MouseDoubleClick);
            this.listBoxSelectedColumns.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBoxSelectedColumns_MouseDown);
            // 
            // buttonMoveSelected
            // 
            this.buttonMoveSelected.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonMoveSelected.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonMoveSelected.Location = new System.Drawing.Point(10, 84);
            this.buttonMoveSelected.Name = "buttonMoveSelected";
            this.buttonMoveSelected.Size = new System.Drawing.Size(25, 25);
            this.buttonMoveSelected.TabIndex = 18;
            this.buttonMoveSelected.Text = ">";
            this.buttonMoveSelected.UseVisualStyleBackColor = true;
            this.buttonMoveSelected.Click += new System.EventHandler(this.buttonMoveSelected_Click_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Help;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Available columns:";
            // 
            // listBoxAvailableColumns
            // 
            this.listBoxAvailableColumns.FormattingEnabled = true;
            this.listBoxAvailableColumns.Location = new System.Drawing.Point(3, 16);
            this.listBoxAvailableColumns.Name = "listBoxAvailableColumns";
            this.listBoxAvailableColumns.Size = new System.Drawing.Size(261, 212);
            this.listBoxAvailableColumns.TabIndex = 14;
            this.listBoxAvailableColumns.DoubleClick += new System.EventHandler(this.listBoxAvailableColumns_DoubleClick);
            this.listBoxAvailableColumns.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxAvailableColumns_MouseDoubleClick);
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 85);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.label2.Size = new System.Drawing.Size(609, 47);
            this.label2.TabIndex = 13;
            this.label2.Text = "Umožňuje vytvoriť nový SQL SELECT príkaz na základe výberu dostupných dátových st" +
    "ĺpcov. Príkaz musí mať taktiež zadefinované meno, ktoré musí byť unikátne. ";
            // 
            // errorProviderInvalidName
            // 
            this.errorProviderInvalidName.ContainerControl = this;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.listBoxAvailableColumns, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.listBoxSelectedColumns, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 180);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(585, 235);
            this.tableLayoutPanel1.TabIndex = 24;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonMoveSelected);
            this.panel1.Controls.Add(this.buttonRemoveSelected);
            this.panel1.Location = new System.Drawing.Point(270, 3);
            this.panel1.Name = "panel1";
            this.tableLayoutPanel1.SetRowSpan(this.panel1, 2);
            this.panel1.Size = new System.Drawing.Size(44, 225);
            this.panel1.TabIndex = 22;
            // 
            // NewCommandForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 471);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.textBoxCommandName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ImageIcon = global::cvti.isef.winformapp.Properties.Resources.sql75;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(625, 510);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(625, 510);
            this.Name = "NewCommandForm";
            this.ShowButtonsPanel = true;
            this.ShowCancelButton = true;
            this.ShowImageIcon = true;
            this.ShowOkButton = true;
            this.Text = "Nový SQL SELECT príkaz";
            this.TitleText = "Generovanie SQL SELECT príkazu";
            this.Controls.SetChildIndex(this.panelControlButtons, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.textBoxCommandName, 0);
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderInvalidName)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxCommandName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonRemoveSelected;
        private System.Windows.Forms.ListBox listBoxSelectedColumns;
        private System.Windows.Forms.Button buttonMoveSelected;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBoxAvailableColumns;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ErrorProvider errorProviderInvalidName;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
    }
}