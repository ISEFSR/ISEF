namespace cvti.isef.winformapp.Controls.Main
{
    using System;
    using System.Windows.Forms;

    public partial class ErrorControl : UserControl
    {
        public ErrorControl()
        {
            InitializeComponent();
        }

        public void ShowError(InitializationReport report)
        {
            ResumeLayout(true);

            textBoxStackTrace.Text = "Pri pokuse o vytvorenie pripojenia na datbázový server nstala neočakávaná chyba. Ak problém pretrváva kontaktujte prosím zodpovednú osobu.";

            textBoxStackTrace.Text += $"{Environment.NewLine}Kontrola:{Environment.NewLine}";
            textBoxStackTrace.Text += $"Môžem sa pripojiť na server:{report.CanConnect}{Environment.NewLine}";
            textBoxStackTrace.Text += $"Existuje databáza na servery:{report.DatabaseExists}{Environment.NewLine}";
            textBoxStackTrace.Text += $"Je databáza na servery valídna:{report.DatabaseValid}{Environment.NewLine}";

            textBoxStackTrace.Text += $"Chybové hlásenie:{Environment.NewLine}";

            textBoxStackTrace.Text += report.Exception?.GetType().ToString() + Environment.NewLine + report.Exception?.Message + Environment.NewLine + report.Exception?.StackTrace;
        }

        private void buttonTryAgain_Click(object sender, EventArgs e) { TryAgainClicked?.Invoke(this, EventArgs.Empty); }

        public event EventHandler TryAgainClicked;

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Skutočne si prajete odznova nastaviť všetky dátové súbory a pripojenie na MSSQL server?",
                "Resetovanie nastavení", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Properties.Settings.Default.ShouldInitialize = true;
                Properties.Settings.Default.Save();

                Application.Restart();
                Environment.Exit(0);
            }
        }
    }
}