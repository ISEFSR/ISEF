namespace cvti.isef.winformapp.Controls.Main
{
    using System;
    using System.Linq;
    using System.Drawing;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using cvti.isef.winformapp.Forms;
    using cvti.isef.winformapp.Controls.Main.Dashboard;
    using cvti.data;
    using cvti.data.Enums;
    using System.Collections.Generic;

    /// <summary>
    /// Dashboard aplikacie
    /// </summary>
    public partial class DashboardControl : UserControl, IMainTabControl
    {
        public event EventHandler TransferCreationClicked;

        private ISEFDataManager _manager;

        private int _year = 2021;

        public DashboardControl()
        {
            InitializeComponent();

            InitializeWelcomeBoard();
        }

        public ISEFDataManager DataManager
        {
            get { return _manager; }
        }

        public int SelectedYear { get => _year; }

        public string TitleText { get; set; } = Properties.Resources.DashboardTitle;
        public string InfoText { get; set; } = Properties.Resources.DashboardInfo;
        public Image TitleImage { get; set; } = Properties.Resources.report_white_100;
        public Image BackImage { get; set; } = null;

        // info mssage ohladom oecd
        private readonly WelcomeBoardItem _oecd = new WelcomeBoardItem() 
        {
            Title = "OECD",
            InfoText = "Nezabudni vyplniť a odoslať údaje pre OECD. Organizácia pre hospodársku spoluprácu a rozvoj (OECD z angl. Organisation for Economic Co-operation and Development) je medzivládna organizácia tridsiatich šiestich ekonomicky najrozvinutejších štátov sveta, ktoré prijali princípy demokracie a trhovej ekonomiky. OECD vznikla v roku 1961 (zakladajúci dokument bol podpísaný 14. decembra 1960) transformáciou Organizácie pre európsku hospodársku spoluprácu (OEEC, Organisation for European Economic Co-operation), ktorá bola pôvodne zriadená v roku 1948 na administráciu povojnového Marshallovho plánu. Slovensko sa členom OECD stalo v decembri v roku 2000.",
            BackgroundImage = Properties.Resources.oecd,
            BackgroundColor = Color.White,
            ForegroundColor = Color.FromArgb(64,64,64)
        };

        // info message pre nahranie vstupnych dat                
        private readonly WelcomeBoardItem _input = new WelcomeBoardItem() 
        {
            Title = "Nezabudni nahrať dáta",
            InfoText = "Údaje pre aplikáciu sú nahrávané jeden krát za rok. Údaje prichádzajú zo SUSR. Prichádzajú zvlášt za ministerstvo vnútra, vysoké školy, vyššie územné celky a priamo riadené organizácie vo forme XLS súborov s dohodnutým formátom. Údaje za mestá a obce prichádzaju vo forme ôsmich DBF súborov kde každý súbor predstavuje výdavky a príjmy organizácií spadajúcich pod mestá a obce za kraj.", 
            SmallImage = Properties.Resources.importtest,
            BackgroundColor = Color.White,
            ForegroundColor = Color.FromArgb(64, 64, 64)
        };

        // reminder pre vytvorenie rocenky
        private readonly WelcomeBoardItem _rocenka = new WelcomeBoardItem()
        {
            Title = "Ročenka",
            InfoText = "Oddelenie ISEF školstva každoročne vytvára Súbor ekonomických ukazovateľov. Publikácia slúži len pre internú potrebu Spracovávané údaje a z nich vytvárané informácie sú poskytované len na základe písomného súhlasu a odporučenia Ministerstva školstva vedy, výskumu a športu SR. DB údajov nie sú verejne dostupné. Požadované informácie v danej oblasti je možné vypracovať na vyžiadanie v rozsahu aktuálneho obsahu a štruktúry archivovaných údajov. V ďalšej prezentácii je popísaný obsah spracovávaných údajov pre orientáciu a predstavu, ak by bol záujem o definovanie vlastných požiadaviek.",
            SmallImage = Properties.Resources.rocenka,
            BackgroundColor = Color.White,
            ForegroundColor = Color.FromArgb(64, 64, 64)
        };

        // reminder pre vytvorenie rozborov hospodarenia
        private readonly WelcomeBoardItem _rozbory = new WelcomeBoardItem()
        {
            Title = "Rozbory hospodárenia",
            InfoText = "Spracovanie údajov pre tvorbu povinných a mimoriadnych výstupov, rozborov, analýz a ekonomických ukazovateľov za rezort školstva sa vykonáva polročne, za školy/školské zariadenia v riadení VÚC z úrovne Štátnej pokladnice po odsúhlasení účtových závierok ročne. Za školy/školské zariadenia v riadení MaO sú údaje preberané z ročných odsúhlasených údajov z úrovne Datacentra MF SR. Pre medzinárodné vykazovanie sú do monitoringu zahrnuté aj údaje o financovaní vzdelávania z rezortov MV SR, MO SR, MZ SR, MPSVaR SR a Vysokoškolského pôžičkového fondu. Údaje získané v rámci monitoringu predstavujú informačné zabezpečenie systému.",
            SmallImage = Properties.Resources.rozbory,
            BackgroundColor = Color.White,
            ForegroundColor = Color.FromArgb(64, 64, 64)
        };

        /// <summary>
        /// Aktivuje dashboard na zaklade manager
        /// </summary>
        /// <param name="manager">Manager poskytujuci pristup k datam ako <see cref="ISEFDataManager"/></param>
        /// <exception cref="ArgumentNullException">V pripade ak je parameter null</exception>
        public async Task Activate(ISEFDataManager manager)
        {
            _manager = manager ?? throw new ArgumentNullException(nameof(manager));

            // Ziskam dostupne roky pre ktore su uz nahrane data
            var years = await _manager.MSSQLManager.GetAvailableYearsAsync();

            // Vyprazdnim contextove menu pre vyber roku
            contextMenuStrip.Items.Clear();

            // naplnim contextove menu pre vyber roku7
            foreach (var y in years)
            {
                var item = contextMenuStrip.Items.Add(y.ToString());
                item.Click += async (snd, ea) =>
                {
                    // pre click na polozke nemusim updatovat actions ani zhrnutie ( left a right panel )
                    _year = int.Parse((snd as ToolStripItem).Text);
                    linkLabelSelectedYear.Text = _year.ToString();
                    await Task.WhenAll(ReloadDashboard(new List<Task>()));
                };

                _year = y;
                linkLabelSelectedYear.Text = _year.ToString();
            }

            // Panel na vyber roku je enabled a visible iba v pripade ak su v aplikacii nahrane nejake data
            panelYearSelectionWrapper.Visible = panelYearSelectionWrapper.Enabled = years.Any();

            // Spustim welcome board ak welcome board este nebezi
            //if (!welcomeBoard1.IsRunning)
            //    welcomeBoard1.Start();

            // Load data for all the tiles 
            var tasks = new List<Task>
            {
                actionsPreviewControl.LoadData(manager),
                yearsPreviewPanel.LoadData(manager),
                warningPanel.LoadData(manager)
                /*dataPreviewTile1.LoadData(manager)*/
            };

            // Ziska a spusti tasky pre polozky vo flow layout paneli
            // pre tie polozky ktore su zavisle na roku tak nastavi aj rok
            ReloadDashboard(tasks);

            // pockam kym dobehnu vsetky tasky
            await Task.WhenAll(tasks);

            warningPanel.Visible = warningPanel.Enabled = warningPanel.ContainsAnyItems;
        }

        private void InitializeWelcomeBoard()
        {
            var items = new List<WelcomeBoardItem>
            {
                _oecd,
                _input,
                _rocenka,
                _rozbory
            };

            //welcomeBoard1.ShowItems(items);

            //welcomeBoard1.Start();
        }

        private IList<Task> ReloadDashboard(IList<Task> tasks)
        {
            foreach (var p in new[] { flowLayoutPanelTiles /*, panelWelcomeWrapper */ })
            foreach (var tile in p.Controls.Cast<Control>())
            {
                if (tile is IYearDependant)
                {
                    (tile as IYearDependant).Rok = _year;
                }

                if (tile is IDashboardActivableTile)
                {
                    tasks.Add((tile as IDashboardActivableTile)?.LoadData(_manager));
                }
            }
            return tasks;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            //if (panelWelcomeWrapper is null)
            //    return;
            //panelWelcomeWrapper.Width = flowLayoutPanelTiles.Width - 30;
        }

        public void Deactivate()
        {
            //welcomeBoard1.Stop();
        }

        private void dashboardTileControlObce_DataImportLinkLabelClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var tile = sender as DashboardTileControl;

            try
            {
                using (var import = new ImportDataForm(_manager, GetInputTypeBasedOnIndex(tile.TabIndex)))
                    import.ShowDialog(ParentForm);
            }
            catch (NotImplementedException)
            {
                using (var import = new ImportDataForm(_manager))
                    import.ShowDialog(ParentForm);
            }
        }

        private void dashboardTile_ShowData(object sender, EventArgs e)
        {
            var tile = sender as DashboardTileControl;

            try
            {
                ShowDataForStupen?.Invoke(this, new Tuple<InputType, int>( GetInputTypeBasedOnIndex(tile.TabIndex), _year));
            }
            catch (NotImplementedException)
            {
                MessageBox.Show("Prehľad pre vybraný stupeň neni implentovaný, kontaktujte prosím zodpovednú osobu", "Neimplementovnaý stupeň", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public event EventHandler<Tuple<InputType,int>> ShowDataForStupen;

        private InputType GetInputTypeBasedOnIndex(int index)
        {
            switch (index)
            {
                case 0:
                    return InputType.MaO;
                case 1:
                    return InputType.VVS;
                case 2:
                    return InputType.VUC;
                case 3:
                    return InputType.OPRO;
                case 4:
                    return InputType.MV;
                default:
                    throw new NotImplementedException();
            }
        }

        private void panelYearSelectionWrapper_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonHideYearSelection_Click(object sender, EventArgs e)
            => contextMenuStripSettings.Show(buttonHideYearSelection, 
                new Point(-contextMenuStripSettings.Width + buttonHideYearSelection.Width, buttonHideYearSelection.Height));

        private void linkLabelSelectedYear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
            => contextMenuStrip.Show(linkLabelSelectedYear, 
                new Point(-contextMenuStrip.Width + linkLabelSelectedYear.Width, linkLabelSelectedYear.Height));

        private void transfersPreview_SetTransfertsClicked(object sender, EventArgs e)
        {
            TransferCreationClicked?.Invoke(this, EventArgs.Empty);
        }

        private void buttonZmenRok_Click(object sender, EventArgs e)
        {
            //yearsPreviewPanel_EnableChangeClicked(this, EventArgs.Empty);

            using (var f = new Form())
            {
                var gm = new GeoMapControl() { Dock = DockStyle.Fill };
                f.Controls.Add(gm);
                f.Load += async (snd, ea) =>
                {
                    gm.Rok = _year;
                    await gm.LoadData(_manager);
                };

                f.ShowDialog();
            }
        }

        private void warningPanel_ItemTextSuccessfullyChanged(object sender, EventArgs e)   
        {
            //ShowTimedMessage(MessageType.Information, MessageLocation.Bottom, "", "");   
            // notify ? 
        }

        private async void načítajOdznovaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tasks = new List<Task>();
            ReloadDashboard(tasks);

            await Task.WhenAll(tasks);
        }

        private void warningPanel_VisibleChanged(object sender, EventArgs e)
        {
            panelTitle.Visible = warningPanel.Visible;
        }
    }
}