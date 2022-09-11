namespace cvti.isef.winformapp.Controls.Main
{
    partial class GeneratorControl
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
            this.tabControlGenerator = new System.Windows.Forms.TabControl();
            this.tabPageConditions = new System.Windows.Forms.TabPage();
            this.conditionsControl = new cvti.isef.winformapp.Controls.Main.Generator.ConditionsControl();
            this.tabPageCommands = new System.Windows.Forms.TabPage();
            this.commandsControl = new cvti.isef.winformapp.Controls.Main.Generator.CommandsControl();
            this.tabControlGenerator.SuspendLayout();
            this.tabPageConditions.SuspendLayout();
            this.tabPageCommands.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlGenerator
            // 
            this.tabControlGenerator.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControlGenerator.Controls.Add(this.tabPageConditions);
            this.tabControlGenerator.Controls.Add(this.tabPageCommands);
            this.tabControlGenerator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlGenerator.HotTrack = true;
            this.tabControlGenerator.Location = new System.Drawing.Point(0, 0);
            this.tabControlGenerator.Name = "tabControlGenerator";
            this.tabControlGenerator.SelectedIndex = 0;
            this.tabControlGenerator.Size = new System.Drawing.Size(816, 508);
            this.tabControlGenerator.TabIndex = 0;
            // 
            // tabPageConditions
            // 
            this.tabPageConditions.Controls.Add(this.conditionsControl);
            this.tabPageConditions.Location = new System.Drawing.Point(4, 4);
            this.tabPageConditions.Name = "tabPageConditions";
            this.tabPageConditions.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConditions.Size = new System.Drawing.Size(808, 482);
            this.tabPageConditions.TabIndex = 0;
            this.tabPageConditions.Text = "Generovanie podmienok";
            this.tabPageConditions.UseVisualStyleBackColor = true;
            // 
            // conditionsControl
            // 
            this.conditionsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.conditionsControl.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.conditionsControl.Location = new System.Drawing.Point(3, 3);
            this.conditionsControl.Name = "conditionsControl";
            this.conditionsControl.Size = new System.Drawing.Size(802, 476);
            this.conditionsControl.TabIndex = 0;
            // 
            // tabPageCommands
            // 
            this.tabPageCommands.Controls.Add(this.commandsControl);
            this.tabPageCommands.Location = new System.Drawing.Point(4, 4);
            this.tabPageCommands.Name = "tabPageCommands";
            this.tabPageCommands.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCommands.Size = new System.Drawing.Size(663, 482);
            this.tabPageCommands.TabIndex = 1;
            this.tabPageCommands.Text = "Generovanie príkazov";
            this.tabPageCommands.UseVisualStyleBackColor = true;
            // 
            // commandsControl
            // 
            this.commandsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commandsControl.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.commandsControl.Location = new System.Drawing.Point(3, 3);
            this.commandsControl.Name = "commandsControl";
            this.commandsControl.Size = new System.Drawing.Size(657, 476);
            this.commandsControl.TabIndex = 0;
            // 
            // GeneratorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlGenerator);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "GeneratorControl";
            this.Size = new System.Drawing.Size(816, 508);
            this.tabControlGenerator.ResumeLayout(false);
            this.tabPageConditions.ResumeLayout(false);
            this.tabPageCommands.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlGenerator;
        private System.Windows.Forms.TabPage tabPageConditions;
        private System.Windows.Forms.TabPage tabPageCommands;
        private Generator.ConditionsControl conditionsControl;
        private Generator.CommandsControl commandsControl;
    }
}
