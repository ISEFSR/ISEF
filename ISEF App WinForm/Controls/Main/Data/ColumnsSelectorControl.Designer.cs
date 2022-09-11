namespace cvti.isef.winformapp.Controls.Main.Data
{
    partial class ColumnsSelectorControl
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
            this.tableLayoutPanelData = new System.Windows.Forms.TableLayoutPanel();
            this.listBoxSelected = new System.Windows.Forms.ListBox();
            this.listBoxAvailable = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonReturn = new System.Windows.Forms.Button();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.tableLayoutPanelData.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelData
            // 
            this.tableLayoutPanelData.ColumnCount = 3;
            this.tableLayoutPanelData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanelData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelData.Controls.Add(this.listBoxSelected, 0, 0);
            this.tableLayoutPanelData.Controls.Add(this.listBoxAvailable, 2, 0);
            this.tableLayoutPanelData.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanelData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelData.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelData.Name = "tableLayoutPanelData";
            this.tableLayoutPanelData.RowCount = 1;
            this.tableLayoutPanelData.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelData.Size = new System.Drawing.Size(427, 362);
            this.tableLayoutPanelData.TabIndex = 0;
            // 
            // listBoxSelected
            // 
            this.listBoxSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxSelected.FormattingEnabled = true;
            this.listBoxSelected.Items.AddRange(new object[] {
            "Rok",
            "Segment",
            "Stupen",
            "Kraj",
            "Okres",
            "Obec",
            "Organizacia",
            "Fk2",
            "Fk3",
            "Fk4",
            "Fk5",
            "Ek1",
            "Ek2",
            "Ek3",
            "Ek6",
            "Zk1",
            "Zk2",
            "Zk3",
            "Zk4",
            "Pk3",
            "Pk5",
            "Pk7"});
            this.listBoxSelected.Location = new System.Drawing.Point(3, 3);
            this.listBoxSelected.Name = "listBoxSelected";
            this.listBoxSelected.Size = new System.Drawing.Size(187, 356);
            this.listBoxSelected.TabIndex = 2;
            // 
            // listBoxAvailable
            // 
            this.listBoxAvailable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxAvailable.FormattingEnabled = true;
            this.listBoxAvailable.Items.AddRange(new object[] {
            "Rok",
            "Segment",
            "Stupen",
            "Kraj",
            "Okres",
            "Obec",
            "Organizacia",
            "Fk2",
            "Fk3",
            "Fk4",
            "Fk5",
            "Ek1",
            "Ek2",
            "Ek3",
            "Ek6",
            "Zk1",
            "Zk2",
            "Zk3",
            "Zk4",
            "Pk3",
            "Pk5",
            "Pk7"});
            this.listBoxAvailable.Location = new System.Drawing.Point(236, 3);
            this.listBoxAvailable.Name = "listBoxAvailable";
            this.listBoxAvailable.Size = new System.Drawing.Size(188, 356);
            this.listBoxAvailable.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.Controls.Add(this.buttonReturn);
            this.panel1.Controls.Add(this.buttonSelect);
            this.panel1.Location = new System.Drawing.Point(196, 144);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(34, 74);
            this.panel1.TabIndex = 1;
            // 
            // buttonReturn
            // 
            this.buttonReturn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonReturn.Location = new System.Drawing.Point(0, 40);
            this.buttonReturn.Name = "buttonReturn";
            this.buttonReturn.Size = new System.Drawing.Size(34, 34);
            this.buttonReturn.TabIndex = 1;
            this.buttonReturn.Text = ">";
            this.buttonReturn.UseVisualStyleBackColor = true;
            // 
            // buttonSelect
            // 
            this.buttonSelect.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonSelect.Location = new System.Drawing.Point(0, 0);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(34, 34);
            this.buttonSelect.TabIndex = 0;
            this.buttonSelect.Text = "<";
            this.buttonSelect.UseVisualStyleBackColor = true;
            // 
            // ColumnsSelectorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanelData);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "ColumnsSelectorControl";
            this.Size = new System.Drawing.Size(427, 362);
            this.tableLayoutPanelData.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelData;
        private System.Windows.Forms.ListBox listBoxSelected;
        private System.Windows.Forms.ListBox listBoxAvailable;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonReturn;
        private System.Windows.Forms.Button buttonSelect;
    }
}
