namespace cvti.isef.winformapp.Controls.Import
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using cvti.isef.winformapp.Controls.Main.Import;
    using cvti.data;

    public partial class ImportRequiredOptions : UserControl, IImportStep
    {
        public ImportRequiredOptions()
        {
            InitializeComponent();
        }

        public IEnumerable<HelpTileInfo> StepHelp => new HelpTileInfo[]
        {
            new HelpTileInfo("Zdroj údajov", 
                "Čo sa položiek analtickej evidencie týka takp ri vzniku nových položiek v čiselníkoch sa ako zdroj pre textáciu môžu slúžiť prednahraté dáta, ktoré sa dajú zmeniť v záložke číselníky, alebo číselníky z už nahratého roku.", null),
            new HelpTileInfo("Kontrola nových údajov", "Pri vzniku nových položiek v číselníku organizácii a analytickej evidencie je možné kontroovať každú novú položku a upravit jej textáciu.", null)
        };

        public event EventHandler MoveNext;
        public event EventHandler MovePrev;

        public bool IsValid() => true;

        public bool UseDefault { get => radioButtonDefaultne.Checked; }

        public int FromYear { get => Convert.ToInt32(comboBoxYear.SelectedItem); }

        public bool CheckOrganizations { get => checkBoxOrgCheck.Checked; }

        public bool CheckAe { get => checkBoxAeCheck.Checked; }

        public async Task LoadYears(ISEFDataManager manager)
        {
            comboBoxYear.Items.Clear();
            foreach (var y in await manager.MSSQLManager.GetAvailableYearsAsync())
            {
                comboBoxYear.Items.Add(y);
            }

            radioButtonYear.Enabled = comboBoxYear.Items.Count > 0;
            if (radioButtonYear.Enabled)
                comboBoxYear.SelectedIndex = 0;
        }

        private void buttonNext_Click(object sender, EventArgs e) => MoveNext?.Invoke(this, EventArgs.Empty);

        private void buttonPrevious_Click(object sender, EventArgs e) => MovePrev?.Invoke(this, EventArgs.Empty);

        private void radioButtonYear_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxYear.Enabled = radioButtonYear.Checked;
        }
    }
}
