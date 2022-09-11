namespace cvti.isef.winformapp.Forms
{
    partial class CreateConditionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateConditionForm));
            this.verticalProgress1 = new cvti.isef.winformapp.Controls.Main.Import.VerticalProgress();
            this.flowLayoutPanelHelp = new System.Windows.Forms.FlowLayoutPanel();
            this.helpControl1 = new cvti.isef.winformapp.Controls.Main.Import.HelpControl();
            this.helpControl2 = new cvti.isef.winformapp.Controls.Main.Import.HelpControl();
            this.flowLayoutPanelConditionType = new System.Windows.Forms.FlowLayoutPanel();
            this.tileControlPlain = new cvti.isef.winformapp.Controls.Main.Import.TileControl();
            this.tileControl1 = new cvti.isef.winformapp.Controls.Main.Import.TileControl();
            this.tileControl2 = new cvti.isef.winformapp.Controls.Main.Import.TileControl();
            this.tileControl3 = new cvti.isef.winformapp.Controls.Main.Import.TileControl();
            this.tileControl4 = new cvti.isef.winformapp.Controls.Main.Import.TileControl();
            this.tileControl5 = new cvti.isef.winformapp.Controls.Main.Import.TileControl();
            this.tileControl6 = new cvti.isef.winformapp.Controls.Main.Import.TileControl();
            this.panelSeparator = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.universalConditionControl = new cvti.isef.winformapp.Controls.Main.Generator.Conditios.UniversalConditionControl();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImageIcon)).BeginInit();
            this.flowLayoutPanelHelp.SuspendLayout();
            this.flowLayoutPanelConditionType.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxImageIcon
            // 
            this.pictureBoxImageIcon.Image = global::cvti.isef.winformapp.Properties.Resources.sql751;
            // 
            // labelTitle
            // 
            this.labelTitle.Text = "Výber logického operátoru";
            // 
            // panelControlButtons
            // 
            this.panelControlButtons.Location = new System.Drawing.Point(0, 631);
            this.panelControlButtons.Size = new System.Drawing.Size(990, 50);
            // 
            // verticalProgress1
            // 
            this.verticalProgress1.BackColor = System.Drawing.SystemColors.Control;
            this.verticalProgress1.Dock = System.Windows.Forms.DockStyle.Left;
            this.verticalProgress1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.verticalProgress1.Location = new System.Drawing.Point(0, 85);
            this.verticalProgress1.Name = "verticalProgress1";
            this.verticalProgress1.ShowNumber = true;
            this.verticalProgress1.Size = new System.Drawing.Size(222, 546);
            this.verticalProgress1.TabIndex = 0;
            // 
            // flowLayoutPanelHelp
            // 
            this.flowLayoutPanelHelp.AutoScroll = true;
            this.flowLayoutPanelHelp.BackColor = System.Drawing.SystemColors.Control;
            this.flowLayoutPanelHelp.Controls.Add(this.helpControl1);
            this.flowLayoutPanelHelp.Controls.Add(this.helpControl2);
            this.flowLayoutPanelHelp.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanelHelp.Location = new System.Drawing.Point(729, 85);
            this.flowLayoutPanelHelp.Name = "flowLayoutPanelHelp";
            this.flowLayoutPanelHelp.Size = new System.Drawing.Size(261, 546);
            this.flowLayoutPanelHelp.TabIndex = 1;
            // 
            // helpControl1
            // 
            this.helpControl1.AnimationSpeed = ((ushort)(200));
            this.helpControl1.AutoSizeHelp = true;
            this.helpControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.helpControl1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.helpControl1.HelpImage = ((System.Drawing.Image)(resources.GetObject("helpControl1.HelpImage")));
            this.helpControl1.HelpImageVisible = false;
            this.helpControl1.InfoText1 = resources.GetString("helpControl1.InfoText1");
            this.helpControl1.Location = new System.Drawing.Point(5, 5);
            this.helpControl1.Margin = new System.Windows.Forms.Padding(5);
            this.helpControl1.Name = "helpControl1";
            this.helpControl1.Size = new System.Drawing.Size(225, 233);
            this.helpControl1.TabIndex = 0;
            this.helpControl1.TitleText = "HELP TITLE";
            // 
            // helpControl2
            // 
            this.helpControl2.AnimationSpeed = ((ushort)(200));
            this.helpControl2.AutoSizeHelp = true;
            this.helpControl2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.helpControl2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.helpControl2.HelpImage = ((System.Drawing.Image)(resources.GetObject("helpControl2.HelpImage")));
            this.helpControl2.HelpImageVisible = false;
            this.helpControl2.InfoText1 = resources.GetString("helpControl2.InfoText1");
            this.helpControl2.Location = new System.Drawing.Point(5, 248);
            this.helpControl2.Margin = new System.Windows.Forms.Padding(5);
            this.helpControl2.Name = "helpControl2";
            this.helpControl2.Size = new System.Drawing.Size(225, 233);
            this.helpControl2.TabIndex = 1;
            this.helpControl2.TitleText = "HELP TITLE";
            // 
            // flowLayoutPanelConditionType
            // 
            this.flowLayoutPanelConditionType.AutoScroll = true;
            this.flowLayoutPanelConditionType.Controls.Add(this.tileControlPlain);
            this.flowLayoutPanelConditionType.Controls.Add(this.tileControl1);
            this.flowLayoutPanelConditionType.Controls.Add(this.tileControl2);
            this.flowLayoutPanelConditionType.Controls.Add(this.tileControl3);
            this.flowLayoutPanelConditionType.Controls.Add(this.tileControl4);
            this.flowLayoutPanelConditionType.Controls.Add(this.tileControl5);
            this.flowLayoutPanelConditionType.Controls.Add(this.tileControl6);
            this.flowLayoutPanelConditionType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelConditionType.Location = new System.Drawing.Point(223, 85);
            this.flowLayoutPanelConditionType.Name = "flowLayoutPanelConditionType";
            this.flowLayoutPanelConditionType.Size = new System.Drawing.Size(505, 546);
            this.flowLayoutPanelConditionType.TabIndex = 2;
            // 
            // tileControlPlain
            // 
            this.tileControlPlain.AnimationSpeed = ((uint)(200u));
            this.tileControlPlain.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tileControlPlain.Location = new System.Drawing.Point(3, 3);
            this.tileControlPlain.MouseOverTileImage = global::cvti.isef.winformapp.Properties.Resources.between100h;
            this.tileControlPlain.Name = "tileControlPlain";
            this.tileControlPlain.Size = new System.Drawing.Size(218, 215);
            this.tileControlPlain.TabIndex = 6;
            this.tileControlPlain.TileBackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.tileControlPlain.TileImage = global::cvti.isef.winformapp.Properties.Resources.between100;
            this.tileControlPlain.TitleText = "Plain text";
            this.tileControlPlain.MouseEnteredTile += new System.EventHandler(this.conditionTile_Enter);
            this.tileControlPlain.MouseClicked += new System.EventHandler(this.tileControl1_MouseClicked);
            this.tileControlPlain.Load += new System.EventHandler(this.tileControlPlain_Load);
            // 
            // tileControl1
            // 
            this.tileControl1.AnimationSpeed = ((uint)(200u));
            this.tileControl1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tileControl1.Location = new System.Drawing.Point(227, 3);
            this.tileControl1.MouseOverTileImage = global::cvti.isef.winformapp.Properties.Resources.equals100h;
            this.tileControl1.Name = "tileControl1";
            this.tileControl1.Size = new System.Drawing.Size(218, 215);
            this.tileControl1.TabIndex = 0;
            this.tileControl1.TileBackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.tileControl1.TileImage = global::cvti.isef.winformapp.Properties.Resources.equals100;
            this.tileControl1.TitleText = "Equals";
            this.tileControl1.MouseEnteredTile += new System.EventHandler(this.conditionTile_Enter);
            this.tileControl1.MouseClicked += new System.EventHandler(this.tileControl1_MouseClicked);
            this.tileControl1.Load += new System.EventHandler(this.tileControl1_Load);
            // 
            // tileControl2
            // 
            this.tileControl2.AnimationSpeed = ((uint)(200u));
            this.tileControl2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tileControl2.Location = new System.Drawing.Point(3, 224);
            this.tileControl2.MouseOverTileImage = global::cvti.isef.winformapp.Properties.Resources.gt100h;
            this.tileControl2.Name = "tileControl2";
            this.tileControl2.Size = new System.Drawing.Size(218, 215);
            this.tileControl2.TabIndex = 1;
            this.tileControl2.TileBackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.tileControl2.TileImage = global::cvti.isef.winformapp.Properties.Resources.gt100;
            this.tileControl2.TitleText = "Greater Than";
            this.tileControl2.MouseEnteredTile += new System.EventHandler(this.conditionTile_Enter);
            this.tileControl2.MouseClicked += new System.EventHandler(this.tileControl1_MouseClicked);
            this.tileControl2.Load += new System.EventHandler(this.tileControl2_Load);
            // 
            // tileControl3
            // 
            this.tileControl3.AnimationSpeed = ((uint)(200u));
            this.tileControl3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tileControl3.Location = new System.Drawing.Point(227, 224);
            this.tileControl3.MouseOverTileImage = global::cvti.isef.winformapp.Properties.Resources.lt100h;
            this.tileControl3.Name = "tileControl3";
            this.tileControl3.Size = new System.Drawing.Size(218, 215);
            this.tileControl3.TabIndex = 2;
            this.tileControl3.TileBackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.tileControl3.TileImage = global::cvti.isef.winformapp.Properties.Resources.lt100;
            this.tileControl3.TitleText = "Less Than";
            this.tileControl3.MouseEnteredTile += new System.EventHandler(this.conditionTile_Enter);
            this.tileControl3.MouseClicked += new System.EventHandler(this.tileControl1_MouseClicked);
            // 
            // tileControl4
            // 
            this.tileControl4.AnimationSpeed = ((uint)(200u));
            this.tileControl4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tileControl4.Location = new System.Drawing.Point(3, 445);
            this.tileControl4.MouseOverTileImage = global::cvti.isef.winformapp.Properties.Resources.in100h;
            this.tileControl4.Name = "tileControl4";
            this.tileControl4.Size = new System.Drawing.Size(218, 215);
            this.tileControl4.TabIndex = 3;
            this.tileControl4.TileBackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.tileControl4.TileImage = global::cvti.isef.winformapp.Properties.Resources.in100;
            this.tileControl4.TitleText = "Inlist";
            this.tileControl4.MouseEnteredTile += new System.EventHandler(this.conditionTile_Enter);
            this.tileControl4.MouseClicked += new System.EventHandler(this.tileControl1_MouseClicked);
            // 
            // tileControl5
            // 
            this.tileControl5.AnimationSpeed = ((uint)(200u));
            this.tileControl5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tileControl5.Location = new System.Drawing.Point(227, 445);
            this.tileControl5.MouseOverTileImage = global::cvti.isef.winformapp.Properties.Resources.like100h;
            this.tileControl5.Name = "tileControl5";
            this.tileControl5.Size = new System.Drawing.Size(218, 215);
            this.tileControl5.TabIndex = 4;
            this.tileControl5.TileBackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.tileControl5.TileImage = global::cvti.isef.winformapp.Properties.Resources.like100;
            this.tileControl5.TitleText = "Like";
            this.tileControl5.MouseEnteredTile += new System.EventHandler(this.conditionTile_Enter);
            this.tileControl5.MouseClicked += new System.EventHandler(this.tileControl1_MouseClicked);
            // 
            // tileControl6
            // 
            this.tileControl6.AnimationSpeed = ((uint)(200u));
            this.tileControl6.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tileControl6.Location = new System.Drawing.Point(3, 666);
            this.tileControl6.MouseOverTileImage = global::cvti.isef.winformapp.Properties.Resources.between100h;
            this.tileControl6.Name = "tileControl6";
            this.tileControl6.Size = new System.Drawing.Size(218, 215);
            this.tileControl6.TabIndex = 5;
            this.tileControl6.TileBackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.tileControl6.TileImage = global::cvti.isef.winformapp.Properties.Resources.between100;
            this.tileControl6.TitleText = "Between";
            this.tileControl6.MouseEnteredTile += new System.EventHandler(this.conditionTile_Enter);
            this.tileControl6.MouseClicked += new System.EventHandler(this.tileControl1_MouseClicked);
            // 
            // panelSeparator
            // 
            this.panelSeparator.BackColor = System.Drawing.Color.LightGray;
            this.panelSeparator.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSeparator.Location = new System.Drawing.Point(222, 85);
            this.panelSeparator.Name = "panelSeparator";
            this.panelSeparator.Size = new System.Drawing.Size(1, 546);
            this.panelSeparator.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(728, 85);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1, 546);
            this.panel1.TabIndex = 5;
            // 
            // universalConditionControl
            // 
            this.universalConditionControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.universalConditionControl.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.universalConditionControl.Location = new System.Drawing.Point(0, 85);
            this.universalConditionControl.Manager = null;
            this.universalConditionControl.Name = "universalConditionControl";
            this.universalConditionControl.Size = new System.Drawing.Size(990, 596);
            this.universalConditionControl.TabIndex = 6;
            // 
            // CreateConditionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(990, 681);
            this.Controls.Add(this.flowLayoutPanelConditionType);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelSeparator);
            this.Controls.Add(this.flowLayoutPanelHelp);
            this.Controls.Add(this.verticalProgress1);
            this.Controls.Add(this.universalConditionControl);
            this.MinimumSize = new System.Drawing.Size(519, 357);
            this.Name = "CreateConditionForm";
            this.Text = "Nová podmienka";
            this.Controls.SetChildIndex(this.universalConditionControl, 0);
            this.Controls.SetChildIndex(this.panelControlButtons, 0);
            this.Controls.SetChildIndex(this.verticalProgress1, 0);
            this.Controls.SetChildIndex(this.flowLayoutPanelHelp, 0);
            this.Controls.SetChildIndex(this.panelSeparator, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.flowLayoutPanelConditionType, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImageIcon)).EndInit();
            this.flowLayoutPanelHelp.ResumeLayout(false);
            this.flowLayoutPanelConditionType.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.Main.Import.VerticalProgress verticalProgress1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelHelp;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelConditionType;
        private Controls.Main.Import.TileControl tileControl1;
        private Controls.Main.Import.TileControl tileControl2;
        private Controls.Main.Import.TileControl tileControl3;
        private Controls.Main.Import.TileControl tileControl4;
        private Controls.Main.Import.TileControl tileControl5;
        private Controls.Main.Import.TileControl tileControl6;
        private Controls.Main.Import.HelpControl helpControl1;
        private Controls.Main.Import.HelpControl helpControl2;
        private System.Windows.Forms.Panel panelSeparator;
        private System.Windows.Forms.Panel panel1;
        private Controls.Main.Generator.Conditios.UniversalConditionControl universalConditionControl;
        private Controls.Main.Import.TileControl tileControlPlain;
    }
}