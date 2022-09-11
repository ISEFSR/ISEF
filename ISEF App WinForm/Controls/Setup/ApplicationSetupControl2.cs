namespace cvti.isef.winformapp.Controls.Setup
{
    using System;
    using System.Windows.Forms;
    using System.Data.SqlClient;
    using cvti.data;
    using cvti.data.Core;

    public partial class ApplicationSetupControl2 : UserControl
    {
        public ApplicationSetupControl2()
        {
            InitializeComponent();

            // Zatial iba name pass login 
#if RELEASE
            comboBoxLoginType.SelectedIndex = 1;
            comboBoxLoginType.Enabled = false;
#else 
            comboBoxLoginType.SelectedIndex = 0;
#endif
            
        }

        public MSSQLServer Server
        {
            get
            {
                if (comboBoxLoginType.SelectedIndex == 0)
                {
                    return new MSSQLServer(textBoxServerAddress.Text, textBoxDatabaseName.Text);
                }
                else
                {
                    return new MSSQLServer(textBoxServerAddress.Text, textBoxLogin.Text, textBoxPassword.Text, textBoxDatabaseName.Text);
                }
            }
        }

        private void comboBoxLoginType_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelLoginName.Enabled =
                labelPassword.Enabled =
                textBoxLogin.Enabled =
                textBoxPassword.Enabled = (comboBoxLoginType.SelectedIndex == 1);

            textBoxPassword.Text = string.Empty;
            if (comboBoxLoginType.SelectedIndex == 0)
            {
                // Windows authentication
                textBoxLogin.Text = $"{Environment.UserDomainName}/{Environment.UserName}";
            }
            else
            {
                // Name pass login
                textBoxLogin.Text = string.Empty;
            }
        }

        private void linkLabelTestServer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ValidateConnection())
            {
                MessageBox.Show($"Prihlásenie na MSSQL server {textBoxServerAddress.Text} a databázu {textBoxDatabaseName.Text} pomocou zadaných prihlasovacích údajov prebehlo úspešne.", "Prihlásenie úspešné", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void linkLabelCreateEmptyDatabase_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Enabled = false;
            UseWaitCursor = true;

            try
            {
                MessageBox.Show("Táto možnosť je momentálne nedostupná. Databázu musíte na server nahrať manuálne. https://docs.microsoft.com/en-us/sql/ssdt/how-to-change-target-platform-and-publish-a-database-project?view=sql-server-ver15",
                    "Nedostupná možnosť", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pri pokuse o nahratie databázy na server nastala neočakávaná cyhba. Chybové hlásenie: " + ex.Message,
                    "Chyba: " + ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Enabled = true;
                UseWaitCursor = false;
            }
        }

        private string GetConnectionString(bool withDatabase = true)
        {
            var database = withDatabase ? $";Database={textBoxDatabaseName.Text}" : string.Empty;
            if (comboBoxLoginType.SelectedIndex == 0)
            {
                return $"Server={textBoxServerAddress.Text}{database};Trusted_Connection=True;";
            }
            else
            {
                return $"Server={textBoxServerAddress.Text}{database};User Id ={textBoxLogin.Text };Password ={textBoxPassword.Text};";
            }
        }

        public bool ValidateConnection()
        {
            Enabled = false;
            UseWaitCursor = true;
            var connected = false;
            try
            {
                using (var conn = new SqlConnection(GetConnectionString(false)))
                    conn.Open();

                connected = true;
                using (var conn = new SqlConnection(GetConnectionString(true)))
                    conn.Open();

                return true;
            }
            catch (Exception ex)
            {
                if (connected)
                {
                    MessageBox.Show($"Prihlásenie na MSSQL server {textBoxServerAddress.Text} za pomoci prihlasovacich údajov prebehlo úśpešne avšak nepodarilo sa mi pripojiť na databázu {textBoxDatabaseName.Text}. Skontrolujte prosím meno databázy. {ex.Message}", "Nenájdená databáza", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show($"Prihlásenie na MSSQL server {textBoxServerAddress.Text} za pomoci zadaných prihlasovacích údajov sa nepodarilo. Skontrolujte prosím, či ste zadali správnu adresu serveru, alebo prihlasovacie meno / heslo. {ex.Message}", "Prihlásenie zlyhalo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                Enabled = true;
                UseWaitCursor = false;
            }

            return false;
        }
    }
}
