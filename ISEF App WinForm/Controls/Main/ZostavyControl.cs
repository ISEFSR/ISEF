namespace cvti.isef.winformapp.Controls.Main
{
    using System;
    using System.Linq;
    using System.Diagnostics;
    using System.Drawing;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using cvti.data;
    using cvti.data.Core;
    using cvti.data.Output;
    using cvti.isef.winformapp.Forms;
    using cvti.isef.winformapp.Forms.Zostavy;
    using cvti.isef.winformapp.Helpers;

    /// <summary>
    /// Vizualizacia zostav pre aplikaciu
    /// </summary>
    /// <remarks>
    /// Zostavy su nahrane v JSON subore pre aplikaciu
    /// </remarks>
    public partial class ZostavyControl : UserControlWithNotification, IMainTabControl
    {
        #region Variables And Constructor

        private readonly int _collapsedHeight, _expandedHeight;

        private bool _fireEvent = false;

        private ISEFDataManager _manager;

        private bool _animating = false;

        public ZostavyControl()
        {
            InitializeComponent();
            _collapsedHeight = panelTitle.Height;
            _expandedHeight = _collapsedHeight + 100;

            zostavyPrehladControl.VybranaZostavaZmenena += ZostavyPrehladControl_VybranaZostavaZmenena;

            foreach (var o in Enum.GetValues(typeof(OkruhZostavyEnum)))
            {
                comboBoxOkruh.Items.Add(o);
            }

            comboBoxOkruh.SelectedIndex = (int)OkruhZostavyEnum.CEL;
            _fireEvent = true;
        }

        #endregion

        #region Public Properties

        public ISEFDataManager DataManager => _manager;

        public OkruhZostavyEnum VybranyOkruh { get { return (OkruhZostavyEnum)comboBoxOkruh.SelectedIndex; } }

        public string TitleText { get; set; } = Properties.Resources.ZostavyTitle;
        public string InfoText { get; set; } = Properties.Resources.ZostavyInfo;
        public Image TitleImage { get; set; } = Properties.Resources.report_white_100;
        public Image BackImage { get; set; } = null;

        #endregion

        public async Task Activate(ISEFDataManager manager)
        {
            await Task.Delay(100);
            _manager = manager ?? throw new ArgumentNullException(nameof(manager));
            zostavyPrehladControl.NastavManager(_manager.CoreFiles.Zostavy);

            zostavyPrehladControl.ZobrazZostavy(VybranyOkruh);
        }

        public void Deactivate()
        {
            
        }

        private void zostavaControl_Load(object sender, EventArgs e)
        {

        }

        private void zostavyPrehladControl_Load(object sender, EventArgs e)
        {

        }

        private void comboBoxOkruh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_fireEvent)
                return;

            zostavyPrehladControl.ZobrazZostavy(VybranyOkruh);

            labelTitle.Text = "VÝBER ZOSTÁV - " + VybranyOkruh.ToString();
            labelInfo.Text = "Prehľad dostupných zostáv za okruh " + VybranyOkruh.ToString();
        }

        private async void ZostavyPrehladControl_VybranaZostavaZmenena(object sender, Zostava e)
        {
            if (e is null)
            {

            }
            else
            {
                await zostavaControl.ZobrazZostavu(_manager, e);
            }
        }

        private void panelTitle_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(new SolidBrush(Color.FromArgb(200, 200, 200)), 1),
                new Point(0, 52), new Point(panelTitle.Width, 52));
        }

        private void panelTitle_Resize(object sender, EventArgs e)
        {
            panelTitle.Invalidate();
        }

        private void buttonRoll_Click(object sender, EventArgs e)
        {
            if (_animating)
                return;

            _animating = true;

            var transition = new Transitions.Transition(new Transitions.TransitionType_Deceleration(250));

            var text = "v";
            var toolTip = "Zobraz filtre pre zostavy";

            var targetHeight = _collapsedHeight == panelTitle.Height ? _expandedHeight : _collapsedHeight;

            transition.add(panelTitle, "Height", targetHeight);

            if (targetHeight != _collapsedHeight)
            {
                text = "^";
                toolTip = "Schovaj filtre pre zostavy";
            }

            transition.TransitionCompletedEvent += (snd, ea) =>
            {
                _animating = false;
            };

            transition.run();

            buttonRoll.Text = text;
            toolTipInfo.SetToolTip(buttonRoll, toolTip);
        }

        #region Event Handlers (ToolStrip)

        private async void toolStripButtonRemove_Click(object sender, EventArgs e)
        {
            // Odstranenie vybranej zostavy
            var zostava = zostavyPrehladControl.VybranaZostava;
            if (zostava is null)
                return;

            if (MessageBox.Show("Skutočne si prajete odstrániť zostavu " + zostava.Nazov +
                "? POZOR zostavu neni možné vrátiť naspäť.",
                "Odstránenie zostavy", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (_manager.CoreFiles.Zostavy.RemoveValue(zostava.Nazov))
                {
                    MessageBox.Show($"Zostava {zostava.Nazov} úspešne odstránená.", "Zostava odstránená", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ShowTimedMessage(MessageType.Information, MessageLocation.Bottom, "Zostava odstránená", $"Zostava {zostava.Nazov} úspešne odstránená.");

                    await _manager.LogFrontendMessageSafeAsync($"Zostava {zostava.Nazov} úspešne odstránená.", "ZostavyControl.toolStripButtonRemove_Click");
                }
                else
                {
                    ShowTimedMessage(MessageType.Warning, MessageLocation.Bottom, "Zostava neodstránená", $"Nepodarilo sa mi odstrániť zostavu {zostava.Nazov}.");
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            // Export zostav
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.DefaultExt = ".JSON";
                saveFileDialog.Title = "Export zostav";
                saveFileDialog.FileName = "zostavy_" + DateTime.Now.ToString("ddMMyyhhmmss");
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _manager.CoreFiles.Zostavy.ExportData(saveFileDialog.FileName);

                    MessageBox.Show($"Zostava úspešne exportvané do výstupného JSON súboru " + saveFileDialog.FileName, "Zostavy exportované", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ShowTimedMessage(MessageType.Information, MessageLocation.Bottom, "Zostavy exportované", $"Zostava úspešne exportvané do výstupného JSON súboru " + saveFileDialog.FileName);
                }
            }
        }

        private void toolStripButtonOpenDirectory_Click(object sender, EventArgs e)
        {
            // Otvorenie vystupneho adresara
            Process.Start(Properties.Settings.Default.OutputDirectory);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            // Import zostav z JSON suboru
            using (var importForm = new ImportZostavyForm())
            {
                if (importForm.ShowDialog() == DialogResult.OK)
                {
                    foreach (var z in importForm.SelectedZostavy)
                    {

                    }
                }
            }
        }

        private async void toolStripButtonGenerateDefault_Click(object sender, EventArgs e)
        {
            // Zmazaneni vsetkych a vytvorenie novych zostav
            // Moze sa spravit cez form a vyber ( teda nemuseli by sa mazat vsetky
            try
            {
                //DataManager.CoreFiles.Zostavy.GenerateDefault(DataManager.CoreFiles.Hlavicky.Values);

                await DataManager.LogFrontendMessageSafeAsync("Zostavy pre aplikáciu úspešne vygenerované odznova", "ZostavyControl.toolStripButtonGenerateDefault_Click");

                await Activate(DataManager);

                ShowTimedMessage(MessageType.Information, MessageLocation.Bottom, "Zostavy vygenerované", $"Zostavy pre aplikáciu úspešne zmazané a vygenerované odznova...");
            }
            catch (Exception ex)
            {
                await DataManager.LogFrontendErrorSafeAsync(ex, "ZostavyControl.toolStripButtonGenerateDefault_Click");

                ShowTimedMessage(MessageType.Error, MessageLocation.Bottom, "Zostava nevygenerované", $"Generovanie zostáv pre aplikáciu zlyhalo. Chybové hlásenie: " +ex.Message);
            }
        }

        private void toolStripButtonAddNew_Click(object sender, EventArgs e)
        {
            // Pridavanie novej zostavy na zaklade hlavickoveho suboru
            using (var novaZostava = new NovaZostavaForm(_manager))
            {
                novaZostava.ZostavaVytvorena += (snd, ea) =>
                {
                    _manager.CoreFiles.Zostavy.AddValue(
                        new Zostava(novaZostava.Hlavicka, novaZostava.Podmienka, novaZostava.OkruhZostavy,
                        novaZostava.Nazov, novaZostava.NadpisLeft, novaZostava.NadpisRight, novaZostava.ZatriedenieZostavy));

                    MessageBox.Show($"Zostava úspešne pridaná " + novaZostava.Nazov, "Zostava pridaná", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ShowTimedMessage(MessageType.Information, MessageLocation.Bottom, "Zostava pridaná", $"Zostava úspešne pridaná " + novaZostava.Nazov);

                };
                novaZostava.ShowDialog();
            }
        }

        private async void toolStripClassifiersSettings_Click(object sender, EventArgs e)
        {
//#if DEBUG
//            var transferovaZostava = (from z in zostavyPrehladControl.VratZobrazeneZostavy() where z.OdpocetTransferov select z).FirstOrDefault();
//            if (transferovaZostava != null)
//            {
//                await _manager.Output.ExportZostavaSafe(transferovaZostava, 2020, true, null);
//            }
//#endif
        }

        private void toolStripClassifiersSettings_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private async void toolStripButtonRocenka_Click(object sender, EventArgs e)
        {
            // Export zostav pre rocenku
            using (var rocenkaForm = new GenerovanieZostavForm(_manager,
                zostavyPrehladControl.VratZobrazeneZostavy(),
                await _manager.MSSQLManager.GetAvailableYearsAsync(),
                VybranyOkruh))

                rocenkaForm.ShowDialog();
        }

        #endregion
    }
}