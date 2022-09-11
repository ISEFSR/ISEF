
namespace cvti.isef.winformapp.Controls
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using cvti.data;
    using cvti.data.Enums;
    using cvti.isef.winformapp.Controls.Main;
    using cvti.isef.winformapp.Forms;
    using cvti.isef.winformapp.Helpers;
    using cvti.isef.winformapp.Properties;

    /// <summary>
    /// Hlavny datovy control, potrebuje inicializovany ISEFDataManager pre spravne fungovanie
    /// </summary>
    public partial class MainDataControl : UserControl
    {
        #region Variables And Constructor

        // Main manager that's reponsible for database and files communication
        private ISEFDataManager _manager;

        // Control that's within selected tab
        // Every tab should have IMainTabcontrol control 
        private IMainTabControl _selectedTab;

        // Banner a redraw timer mal sluzit ako indikator ked aplikacia pracuje
        // prezatial som to zavrhol. V ramci testu staci odkomentovat start v konstruktore
        // Ale to by sa malo spustit vzdy ked aplikacia taha data z DB
        // prezatial je vypnuty
        private Timer _redrawTimer;
        private int _index = 1;
        private readonly Image[] _banners = new Image[]
        {
            Resources.banner,
            Resources.banner1,
            Resources.banner2,
            Resources.banner3
        };

        private Image _banner;

        private bool _shouldActivate = true;

        public MainDataControl()
        {
            InitializeComponent();
            panelTitle.SetDoubleBuffered();
            _banner = _banners[0];
            _redrawTimer = new Timer() { Interval = 750 };
            _redrawTimer.Tick += (sender, e) =>
            {
                _banner = _banners[_index++];
                if (_index >= _banners.Length)
                    _index = 0;

                panelTitle.Invalidate();
            };
            _selectedTab = GetSelectedTabControl();
            LoadTabData();
        }

        #endregion

        #region Public Methods

        public async Task SetUpDataManager(ISEFDataManager manager)
        {
            _manager = manager ?? throw new ArgumentNullException(nameof(manager));
            _selectedTab?.Activate(_manager);
            importToolStripMenuItem.Enabled = true;
            exportToolStripMenuItem.Enabled = true;
            nastaveniaToolStripMenuItem.Enabled = true;

            var years = await _manager.MSSQLManager.GetAvailableYearsAsync();

            if (!years.Any())
            {
                tabControlMain.TabPages.Remove(tabPageData);
                //tabControlMain.TabPages.Remove()
            }
        }

        public void Import() 
        {
            using (var importForm = new ImportDataForm(_manager)) 
                importForm.ShowDialog(); 
        }

        public void Export()
        {
            using (var exportForm = new ExportForm(_manager))
                if (exportForm.ShowDialog() == DialogResult.OK)
                {
                    using (var sfd = new SaveFileDialog())
                    {
                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            sfd.Title = "Exportuj dáta aplikácie";
                            sfd.DefaultExt = "*.xlsx|*.xlsx";
                            sfd.CheckPathExists = true;
                            sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                            sfd.FileName = $"export_{exportForm.VybranyStupen}_{exportForm.VybranyRok}.xlsx";

                            //_manager.Export.expo
                            new WorkingForm(_manager.Output.ExportRawData(exportForm.VybranyStupen, exportForm.VybranyRok, sfd.FileName)).ShowDialog();
                        }
                    }
                }
        }

        #endregion

        private IMainTabControl GetSelectedTabControl()
        {
            if (tabControlMain.SelectedTab?.Controls.Count > 0)
                return (tabControlMain.SelectedTab?.Controls[0] as IMainTabControl);

            return null;
        }

        private void LoadTabData()
        {
            if (_selectedTab != null)
            {
                SuspendLayout();
                labelTitle.Text = _selectedTab.TitleText;
                labelInfo.Text = _selectedTab.InfoText;
                pictureBoxIcon.Image = _selectedTab.TitleImage;
                panelTitle.BackgroundImage = _selectedTab.BackImage;
                ResumeLayout();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            panelTitle.Invalidate();
        }

        private async void tabControlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_manager is null) 
                return;

            // Deaktivujem posledne zobrazeny tab control            
            _selectedTab?.Deactivate();

            // Nastavi novy aktivny tab control
            _selectedTab = GetSelectedTabControl();

            // Nacitam udaje pre novy aktivny tab
            LoadTabData();

            // Aktivujem novy akivny tab
            if(_shouldActivate)
                await _selectedTab?.Activate(_manager);
        }

        private async void dashboardControl_ShowDataForStupen(object sender, Tuple<InputType, int> e)
        {
            var years = await _manager.MSSQLManager.GetAvailableYearsAsync();
            if (!years.Any())
            {
                MessageBox.Show("Pre zobrazenie prehľadu je potrebné najprv importovať dáta.", "Neimportované dáta", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Nemala by sa spustit aktivacia po zmene tabu
            _shouldActivate = false;

            // Nastavim ako aktualny tab prehlad dat
            tabControlMain.SelectedTab = tabPageData;

            // zobrazim pre prehlad dat vybrany InputType
            await dataPreviewControl.Activate(_manager, e.Item1, e.Item2);

            // Uz sa moze spustit aktivacia po zmene tabu
            _shouldActivate = true;
        }

        private void panelTitle_Paint(object sender, PaintEventArgs e)
        {
            //e.Graphics.DrawImage(_banner,
            //    new System.Drawing.PointF(panelTitle.Width - Resources.banner.Width, 0));
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e) => Import();
        private void exportToolStripMenuItem_Click(object sender, EventArgs e) => Export();

        private void nastaveniaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var settings = new SettingsForm()) settings.ShowDialog();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();

        private void nápovedaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var helpFile = System.IO.Path.Combine(Environment.CurrentDirectory, Properties.Resources.HelpDirectory, Properties.Resources.FileHelp);
            try
            {
                Process.Start(helpFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Nepodarilo sa mi otvoriť nápovedu pre aplikáciu ({helpFile}). " + ex.Message,
                    "Chyba " + ex.GetType().ToString(),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void oAplikáciiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var aboutForm = new AboutBox()) aboutForm.ShowDialog();
        }

        private async void odstránenieDátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Zobraz dialog pre odstranenie dat
            // momentalne odstranujem data po stupni a roku
            // TODO: prerobit
            // pre data by mali byt dva typy importu excel / DBF
            // nemali by sa importovatr odstranovat po stupnoch 
            using (var removeForm = new RemoveDataForm())
            {
                if (removeForm.ShowDialog() == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;
                    Enabled = false;
                    try
                    {
                        var task = _manager.MSSQLManager.RemoveDataAsync(removeForm.Stupen, removeForm.Rok);

                        using (var wf = new WorkingForm(task))
                            wf.ShowDialog();
                            
                        var count = task.Result;
                        

                        MessageBox.Show($"{count} údajov pre stupeň {removeForm.Stupen} a rok {removeForm.Rok} úspešne odstránené.", "Dáta odstránené", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        await _manager.LogFrontendMessageSafeAsync($"{count} údajov pre stupeň {removeForm.Stupen} a rok {removeForm.Rok} úspešne odstránené.",
                            "MainDataControl.odstránenieDátToolStripMenuItem_Click");

                        await _selectedTab.Activate(_manager);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Pri pokuse o odstránenie údajov pre rok {removeForm.Rok} a stupeň {removeForm.Stupen} nastala neočakávaná chyba. " + ex.Message,
                            "Chyba " + ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        await _manager.LogFrontendErrorSafeAsync(ex, "MainDataControl.odstránenieDátToolStripMenuItem_Click");
                    }
                    finally
                    {
                        Cursor = Cursors.Default;
                        Enabled = true;
                    }
                }
            }
        }

        private void dashboardControl_TransferCreationClicked(object sender, EventArgs e)
        {
            // Pre vybran rok niesu nahrate trasnfery
            // user klikol na moznost zmeny treansferov pre rok
            // TODO: zobraz ciselniky / transfery s rokom
            tabControlMain.SelectedTab = tabPageCiselniky;
            ciselnikyControl.ChangeTransfers(dashboardControl.SelectedYear);
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
            => tabControlMain_SelectedIndexChanged(this, e);

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Restart nastaveni aplikacie
            // aplikacia odznova vygeneruje JSON subory s podmienkami prikazmi hlavicky a zostavy
            if (MessageBox.Show("Skutočne si prajete reštartovať nastavenia aplikácie. Pozor týmto stratíte všetky získané podmienka a príkazy.", "Reštart nastavení.",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Settings.Default.ShouldInitialize = true;
                Settings.Default.Save();
                Application.Restart();
            }
        }
    }
}