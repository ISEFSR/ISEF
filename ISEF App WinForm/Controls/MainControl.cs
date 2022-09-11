namespace cvti.isef.winformapp.Controls
{
    using System;
    using System.Drawing;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using cvti.data;

    /// <summary>
    /// Hlavny datovy control / v podstate by mohlo vsetko prejst do main formu
    /// </summary>
    /// <remarks>
    /// Hlavny user control pozostava z troch controlov
    /// InitializeControl - zodpovedny za proces inicializacie premennyc
    /// ErrorControl - zodpovedny za zobrazenie pripadnej chyby
    /// MainDatacontrol - zodpovedny za azobraznenie dat
    /// </remarks>
    public partial class MainControl : UserControl
    {
        // Datovy manager zodpovedny za manipulaciu s datami aplikacie, ziskany po uspesnej inicializacii
        private ISEFDataManager _manager;

        public MainControl()
        {
            InitializeComponent();

            // Application icon pre ostatne eventy ( data nahrane etc.. )
            notifyIconApplication.Icon = Icon.FromHandle(Properties.Resources.app_logo.GetHicon());
        }

        public async Task Initialize()
        {
            // Nastartuje inicializaciu 
            // Tu vytvori manager skontroluje pripojenie a vsetky potrebne subory
            // Uspesna inicializacia je indikovana eventom successfully initialzied
            initializationControl.BringToFront();
            await initializationControl.Initalize();
        }

        public async Task Quit()
        {
            // Ukonci aplikacu
            // Ak existuje manager tak ulozi zmeny na core suboroch
            await _manager?.SaveManager();
            Properties.Settings.Default.Save();

            if (_manager != null)
                await _manager.LogFrontendMessageSafeAsync("Closing application", "MainControl.Quit");
        }

        private async void initializationControl_SuccessfullyInitialized(object sender, ISEFDataManager e)
        {
            // Manager bol uspesne inicializovany
            // Nastavim manager a nacitam main data control
            // initialization control mozem disposnut kedze ho uz nebudem potrebovat

            _manager = e;
            await _manager.LogFrontendMessageSafeAsync("Successfully initialzied. Loading up application dashboard.",
                "MainControl.initializationControl_SuccessfullyInitialized");

            initializationControl.Dispose();
            connectionErrorControl.Dispose();

            mainDataControl.BringToFront();
            await mainDataControl.SetUpDataManager(e);

            e.ZostavaExportovana += E_ZostavaExportovana;

            notifyIconApplication.ShowBalloonTip(
                4500, "Aplikácia inicializovaná",
                "Aplikácia sa úspešne pripojila na databázu a načítala obsah konfiguračných súborov.", 
                ToolTipIcon.Info);
        }

        private void E_ZostavaExportovana(object sender, data.Output.Zostava e)
        {
            //notifyIconZostavy.ShowBalloonTip(4500,
            //    "Zostava exportovaná",
            //    $"Zostava {e.Nazov} úspešne exportovaná.", ToolTipIcon.Info);
        }

        private void initializationControl_InitializationFailed(object sender, InitializationReport e)
        {
            // Initialization failed
            // get the reason and do appropriate acction
            // A. Setup a new server connection
            // B. Create empty database on selected server 
            connectionErrorControl.ShowError(e);
            connectionErrorControl.BringToFront();

            notifyIconApplication.ShowBalloonTip(4500,
                "Inicializácia zlyhala",
                $"Inicializácia zlyhala. Pripojenie na server: {e.CanConnect} Pripojenie na databázu: {e.DatabaseExists} Databáza zvalidovaná: {e.DatabaseValid}. {e?.Exception?.Message}", ToolTipIcon.Info);
        }

        private async void connectionErrorControl_TryAgainClicked(object sender, EventArgs e)
        {
            initializationControl.BringToFront();
            await initializationControl.Initalize();
        }

        private void importÚdajovToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_manager is null)
            {
                System.Media.SystemSounds.Beep.Play();
                return;
            }

            mainDataControl.Import();
        }

        private void exportÚdajovToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_manager is null)
            {
                System.Media.SystemSounds.Beep.Play();
                return;
            }

            mainDataControl.Export();
        }

        private void zavriAplikáciuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
 