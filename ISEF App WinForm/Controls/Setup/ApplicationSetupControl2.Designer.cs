namespace cvti.isef.winformapp.Controls.Setup
{
    partial class ApplicationSetupControl2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationSetupControl2));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelLoginName = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelInfo = new System.Windows.Forms.Label();
            this.comboBoxLoginType = new System.Windows.Forms.ComboBox();
            this.textBoxLogin = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.linkLabelCreateEmptyDatabase = new System.Windows.Forms.LinkLabel();
            this.linkLabelTestServer = new System.Windows.Forms.LinkLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxDatabaseName = new System.Windows.Forms.TextBox();
            this.textBoxServerAddress = new System.Windows.Forms.TextBox();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Help;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(10, 5, 50, 5);
            this.label1.Size = new System.Drawing.Size(185, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Názov / adresa serveru:";
            this.toolTipInfo.SetToolTip(this.label1, "Adresa MSSQL serveru, kde bude aplikácia ukladať dáta.");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.Help;
            this.label2.Location = new System.Drawing.Point(3, 28);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(10, 5, 50, 5);
            this.label2.Size = new System.Drawing.Size(150, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Názov databázy:";
            this.toolTipInfo.SetToolTip(this.label2, "Názov databázy na MSSQL serveri");
            // 
            // labelLoginName
            // 
            this.labelLoginName.AutoSize = true;
            this.labelLoginName.Cursor = System.Windows.Forms.Cursors.Help;
            this.labelLoginName.Enabled = false;
            this.labelLoginName.Location = new System.Drawing.Point(3, 83);
            this.labelLoginName.Name = "labelLoginName";
            this.labelLoginName.Padding = new System.Windows.Forms.Padding(10, 5, 50, 5);
            this.labelLoginName.Size = new System.Drawing.Size(168, 23);
            this.labelLoginName.TabIndex = 2;
            this.labelLoginName.Text = "Prihlasovacie meno:";
            this.toolTipInfo.SetToolTip(this.labelLoginName, "Prihlasovacie meno v prípade prihlásenia pomocou mena / hesla");
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Cursor = System.Windows.Forms.Cursors.Help;
            this.labelPassword.Enabled = false;
            this.labelPassword.Location = new System.Drawing.Point(3, 111);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Padding = new System.Windows.Forms.Padding(10, 5, 50, 5);
            this.labelPassword.Size = new System.Drawing.Size(99, 23);
            this.labelPassword.TabIndex = 3;
            this.labelPassword.Text = "Heslo:";
            this.toolTipInfo.SetToolTip(this.labelPassword, "Prihlasovacie heslo v prípade prihlásenia pomocou mena / hesla.");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Cursor = System.Windows.Forms.Cursors.Help;
            this.label5.Location = new System.Drawing.Point(3, 56);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(10, 5, 50, 5);
            this.label5.Size = new System.Drawing.Size(169, 23);
            this.label5.TabIndex = 4;
            this.label5.Text = "Spôsob prihlásenia:";
            this.toolTipInfo.SetToolTip(this.label5, "Spôsob prihlásenia na MSSQL server");
            // 
            // labelInfo
            // 
            this.labelInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelInfo.Location = new System.Drawing.Point(0, 0);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Padding = new System.Windows.Forms.Padding(10);
            this.labelInfo.Size = new System.Drawing.Size(759, 100);
            this.labelInfo.TabIndex = 5;
            this.labelInfo.Text = resources.GetString("labelInfo.Text");
            // 
            // comboBoxLoginType
            // 
            this.comboBoxLoginType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLoginType.FormattingEnabled = true;
            this.comboBoxLoginType.Items.AddRange(new object[] {
            "Windows authentication",
            "Prihlásenie pomocou mena / hesla"});
            this.comboBoxLoginType.Location = new System.Drawing.Point(194, 59);
            this.comboBoxLoginType.Name = "comboBoxLoginType";
            this.comboBoxLoginType.Size = new System.Drawing.Size(299, 21);
            this.comboBoxLoginType.TabIndex = 8;
            this.comboBoxLoginType.SelectedIndexChanged += new System.EventHandler(this.comboBoxLoginType_SelectedIndexChanged);
            // 
            // textBoxLogin
            // 
            this.textBoxLogin.Enabled = false;
            this.textBoxLogin.Location = new System.Drawing.Point(241, 86);
            this.textBoxLogin.Margin = new System.Windows.Forms.Padding(50, 3, 3, 3);
            this.textBoxLogin.Name = "textBoxLogin";
            this.textBoxLogin.Size = new System.Drawing.Size(252, 22);
            this.textBoxLogin.TabIndex = 9;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Enabled = false;
            this.textBoxPassword.Location = new System.Drawing.Point(241, 114);
            this.textBoxPassword.Margin = new System.Windows.Forms.Padding(50, 3, 3, 3);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(252, 22);
            this.textBoxPassword.TabIndex = 10;
            // 
            // linkLabelCreateEmptyDatabase
            // 
            this.linkLabelCreateEmptyDatabase.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.linkLabelCreateEmptyDatabase, 2);
            this.linkLabelCreateEmptyDatabase.Location = new System.Drawing.Point(3, 368);
            this.linkLabelCreateEmptyDatabase.Name = "linkLabelCreateEmptyDatabase";
            this.linkLabelCreateEmptyDatabase.Padding = new System.Windows.Forms.Padding(10, 5, 10, 15);
            this.linkLabelCreateEmptyDatabase.Size = new System.Drawing.Size(171, 33);
            this.linkLabelCreateEmptyDatabase.TabIndex = 11;
            this.linkLabelCreateEmptyDatabase.TabStop = true;
            this.linkLabelCreateEmptyDatabase.Text = "Vytvor databázu na serveri...";
            this.toolTipInfo.SetToolTip(this.linkLabelCreateEmptyDatabase, "Pokúsi sa vytvoriť databázu ISEF na vybranom MSSQL serveri za pomoci vybraných pr" +
        "ihlasovacích údajov");
            this.linkLabelCreateEmptyDatabase.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelCreateEmptyDatabase_LinkClicked);
            // 
            // linkLabelTestServer
            // 
            this.linkLabelTestServer.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.linkLabelTestServer, 2);
            this.linkLabelTestServer.Location = new System.Drawing.Point(3, 345);
            this.linkLabelTestServer.Name = "linkLabelTestServer";
            this.linkLabelTestServer.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.linkLabelTestServer.Size = new System.Drawing.Size(205, 23);
            this.linkLabelTestServer.TabIndex = 12;
            this.linkLabelTestServer.TabStop = true;
            this.linkLabelTestServer.Text = "Skontrolujte pripojenie na server...";
            this.toolTipInfo.SetToolTip(this.linkLabelTestServer, "Skontroluje, či sa aplikácia dokáźe pripojiť na MSSQL server a databázu pomocou p" +
        "rihlasovacích údajov");
            this.linkLabelTestServer.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelTestServer_LinkClicked);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.textBoxDatabaseName, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.linkLabelCreateEmptyDatabase, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.linkLabelTestServer, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBoxPassword, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBoxLogin, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelPassword, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxLoginType, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelLoginName, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.textBoxServerAddress, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 100);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(759, 401);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // textBoxDatabaseName
            // 
            this.textBoxDatabaseName.Location = new System.Drawing.Point(194, 31);
            this.textBoxDatabaseName.Name = "textBoxDatabaseName";
            this.textBoxDatabaseName.Size = new System.Drawing.Size(299, 22);
            this.textBoxDatabaseName.TabIndex = 14;
            this.textBoxDatabaseName.Text = "ISEF";
            // 
            // textBoxServerAddress
            // 
            this.textBoxServerAddress.Location = new System.Drawing.Point(194, 3);
            this.textBoxServerAddress.Name = "textBoxServerAddress";
            this.textBoxServerAddress.Size = new System.Drawing.Size(299, 22);
            this.textBoxServerAddress.TabIndex = 13;
            this.textBoxServerAddress.Text = "(LocalDb)\\MSSQLLocalDB";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // ApplicationSetupControl2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.labelInfo);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ApplicationSetupControl2";
            this.Size = new System.Drawing.Size(759, 501);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelLoginName;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.ComboBox comboBoxLoginType;
        private System.Windows.Forms.TextBox textBoxLogin;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.LinkLabel linkLabelCreateEmptyDatabase;
        private System.Windows.Forms.LinkLabel linkLabelTestServer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private System.Windows.Forms.TextBox textBoxDatabaseName;
        private System.Windows.Forms.TextBox textBoxServerAddress;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}
