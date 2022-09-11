namespace cvti.isef.winformapp.Controls
{
    partial class MainControl
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
            this.components = new System.ComponentModel.Container();
            this.notifyIconApplication = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStripApplication = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.importÚdajovToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportÚdajovToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.zavriAplikáciuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.initializationControl = new cvti.isef.winformapp.Controls.InitializationControl();
            this.mainDataControl = new cvti.isef.winformapp.Controls.MainDataControl();
            this.connectionErrorControl = new cvti.isef.winformapp.Controls.Main.ErrorControl();
            this.contextMenuStripApplication.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIconApplication
            // 
            this.notifyIconApplication.ContextMenuStrip = this.contextMenuStripApplication;
            this.notifyIconApplication.Text = "ISEF Spracovanie dát";
            this.notifyIconApplication.Visible = true;
            // 
            // contextMenuStripApplication
            // 
            this.contextMenuStripApplication.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importÚdajovToolStripMenuItem,
            this.exportÚdajovToolStripMenuItem,
            this.toolStripSeparator1,
            this.zavriAplikáciuToolStripMenuItem});
            this.contextMenuStripApplication.Name = "contextMenuStripApplication";
            this.contextMenuStripApplication.Size = new System.Drawing.Size(151, 76);
            // 
            // importÚdajovToolStripMenuItem
            // 
            this.importÚdajovToolStripMenuItem.Name = "importÚdajovToolStripMenuItem";
            this.importÚdajovToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.importÚdajovToolStripMenuItem.Text = "&Import údajov";
            this.importÚdajovToolStripMenuItem.Click += new System.EventHandler(this.importÚdajovToolStripMenuItem_Click);
            // 
            // exportÚdajovToolStripMenuItem
            // 
            this.exportÚdajovToolStripMenuItem.Name = "exportÚdajovToolStripMenuItem";
            this.exportÚdajovToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.exportÚdajovToolStripMenuItem.Text = "&Export údajov";
            this.exportÚdajovToolStripMenuItem.Click += new System.EventHandler(this.exportÚdajovToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(147, 6);
            // 
            // zavriAplikáciuToolStripMenuItem
            // 
            this.zavriAplikáciuToolStripMenuItem.Name = "zavriAplikáciuToolStripMenuItem";
            this.zavriAplikáciuToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.zavriAplikáciuToolStripMenuItem.Text = "&Zavri aplikáciu";
            this.zavriAplikáciuToolStripMenuItem.Click += new System.EventHandler(this.zavriAplikáciuToolStripMenuItem_Click);
            // 
            // initializationControl
            // 
            this.initializationControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.initializationControl.BackgroundImage = global::cvti.isef.winformapp.Properties.Resources.isefinit5;
            this.initializationControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.initializationControl.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.initializationControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.initializationControl.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.initializationControl.Location = new System.Drawing.Point(0, 0);
            this.initializationControl.Name = "initializationControl";
            this.initializationControl.Size = new System.Drawing.Size(968, 671);
            this.initializationControl.TabIndex = 1;
            this.initializationControl.InitializationFailed += new System.EventHandler<cvti.isef.winformapp.Controls.InitializationReport>(this.initializationControl_InitializationFailed);
            this.initializationControl.SuccessfullyInitialized += new System.EventHandler<cvti.data.ISEFDataManager>(this.initializationControl_SuccessfullyInitialized);
            // 
            // mainDataControl
            // 
            this.mainDataControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainDataControl.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.mainDataControl.Location = new System.Drawing.Point(0, 0);
            this.mainDataControl.Name = "mainDataControl";
            this.mainDataControl.Size = new System.Drawing.Size(968, 671);
            this.mainDataControl.TabIndex = 0;
            // 
            // connectionErrorControl
            // 
            this.connectionErrorControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.connectionErrorControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.connectionErrorControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.connectionErrorControl.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.connectionErrorControl.Location = new System.Drawing.Point(0, 0);
            this.connectionErrorControl.Name = "connectionErrorControl";
            this.connectionErrorControl.Size = new System.Drawing.Size(968, 671);
            this.connectionErrorControl.TabIndex = 3;
            this.connectionErrorControl.TryAgainClicked += new System.EventHandler(this.connectionErrorControl_TryAgainClicked);
            // 
            // MainControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.initializationControl);
            this.Controls.Add(this.mainDataControl);
            this.Controls.Add(this.connectionErrorControl);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "MainControl";
            this.Size = new System.Drawing.Size(968, 671);
            this.contextMenuStripApplication.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MainDataControl mainDataControl;
        private InitializationControl initializationControl;
        //private ChangeServerControl changeServerControl;
        private Main.ErrorControl connectionErrorControl;
        private System.Windows.Forms.NotifyIcon notifyIconApplication;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripApplication;
        private System.Windows.Forms.ToolStripMenuItem importÚdajovToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportÚdajovToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem zavriAplikáciuToolStripMenuItem;
        //private Init.InvalidDatabaseControl invalidDatabaseControl;
    }
}
