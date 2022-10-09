namespace cvti.isef.winformapp.Controls.Main
{
    using System;
    using System.Drawing;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using cvti.data;
    using cvti.data.Output;
    using System.Collections.Generic;
    using System.Diagnostics;

    public partial class HlavickyControl : UserControl, IMainTabControl
    {
        private readonly int _collapsedHeight, _expandedHeight;
        // dostupne hlavicky
        // a prisluchajuci listviewitem pre hlavicku
        private readonly Dictionary<Hlavicka, ListViewItem> _hlavickyItems
            = new Dictionary<Hlavicka, ListViewItem>();

        private bool _animating = false;

        public HlavickyControl()
        {
            InitializeComponent();

            // Collapsed height je vyska panelu s nastaveniami
            _collapsedHeight = panelHlavickaFilter.Height;

            // Expanded height je plne rozbalena vyska controlu
            _expandedHeight = _collapsedHeight + 100;

            // Pridam vsetky typy hlavicky pod combo box umoznujuci filtrovanie 
            foreach (var t in Enum.GetValues(typeof(HlavickaType)))
                comboBoxTypHlavicky.Items.Add(t);
        }

        #region IMainTabControl Implementation

        public ISEFDataManager DataManager { get; private set; }

        public string TitleText { get; set; } = Properties.Resources.HlavickyTitle;
        public string InfoText { get; set; } = Properties.Resources.HlavickyInfp;
        public Image TitleImage { get; set; } = Properties.Resources.excel_white_100;
        public Image BackImage { get; set; } = null;

        public async Task Activate(ISEFDataManager manager)
        {
            // Ak uz predtym bol nastaveny manager unsubscirbnem event handlers
            if (DataManager != null)
            {
                // V pripade ak pridam / odstranim hlavicku
                DataManager.CoreFiles.Hlavicky.NewValueAdded -= HlavickyManagerInstance_NewHlavickaAdded;
                DataManager.CoreFiles.Hlavicky.ValueRemoved -= HlavickyManagerInstance_HlavickaRemoved;
            }

            // Nastavim novy manager
            DataManager = manager ?? throw new ArgumentNullException(nameof(manager));

            // V pripade ak pridam / odstranim hlavicku
            DataManager.CoreFiles.Hlavicky.NewValueAdded += HlavickyManagerInstance_NewHlavickaAdded;
            DataManager.CoreFiles.Hlavicky.ValueRemoved += HlavickyManagerInstance_HlavickaRemoved;

            // Nacitam hlavicky
            await ReloadHlavicky();
        }

        public void Deactivate()
        {
            listViewHlavicky.Items.Clear();
            _hlavickyItems.Clear();
        }

        #endregion

        private void HlavickyManagerInstance_HlavickaRemoved(object sender, Hlavicka e)
        {
            // Hlavicka bola odstranena 
            // Event z hlavicky managera
            if (_hlavickyItems.ContainsKey(e))
            {
                var item = _hlavickyItems[e];
                _hlavickyItems.Remove(e);
                listViewHlavicky.Items.Remove(item);
            }
        }

        private void HlavickyManagerInstance_NewHlavickaAdded(object sender, data.Output.Hlavicka e)
        {
            // Hlavicka bola pridana
            // Event z hlavicky managera
            AddListViewItem(e);
        }

        #region EventHandlers (ToolStrip)

        private async void toolStripButtonAddNew_Click(object sender, EventArgs e)
        {
            // Umoznuje pridat novy hlavickovy subor 
            openFileDialogHlavicka.InitialDirectory = Environment.CurrentDirectory;

            // Otovrim dialog na vyber vstupneho XLSX suboru predstavujuceho hlavicku
            if (openFileDialogHlavicka.ShowDialog() == DialogResult.OK)
            {
                foreach (var file in openFileDialogHlavicka.FileNames)
                {
                    await PridajHlavicku(file);
                }
            }
        }

        private void toolStripButtonRemove_Click(object sender, EventArgs e)
        {
            var hlavicka = VratVybranuHlavicku();
            if (hlavicka is null)
                return;

            // TODO: skontroluj ci su na hlavicke zavesesne zostavy ak ano zobraz informacnu spravu a ukonci
            if (MessageBox.Show($"Skutočne si prajete odstrániť hlavičku {hlavicka.Name}?" +
                " Myslite na to, že hlavička môže mať napárované zostavy a po odstránení nemusia zostavy fungnovať.", "Odstránenie hlavičky.",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (!DataManager.CoreFiles.Hlavicky.RemoveValue(hlavicka.Name))
                {
                    MessageBox.Show("Hlavičku sa z nenznámych dôvodov nepodarilo odstrániť",
                        "Hlavička neodstránená", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private async void toolStripButtonShow_Click(object sender, EventArgs e)
        {
            var h = VratVybranuHlavicku();
            if (h is null)
                return;

            try
            {
                var p = Process.Start(h.FilePath);
                p.Exited += (snd, ea) =>
                {
                    MessageBox.Show("Nezabudnite pre istotu reloadnúť hlavičku po editácii", "Editácia hlavičky",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                };
            }
            catch (Exception ex)
            {
                await DataManager.LogFrontendErrorSafeAsync(ex,
                    "HlavickyControl.toolStripButtonShow_Click");

                MessageBox.Show("Pri pokuse o otvorenie hlavičky nastala neočakávaná chyba. " + ex.Message,
                    "Chyba. " + ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButtonCreateDefault_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Táto možnosť bola dočasne vypnutá. Pre viac info sa obráte na IT oddelenie.", "Vypnutá možnosť", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //using (var updateDefault = new UpdateDefaultHlavicky())
            //{
            //    if (updateDefault.ShowDialog() == DialogResult.OK)
            //    {
            //        try
            //        {
            //            // For each hlavica pridaj hlavicku...
            //            await DataManager.LogFrontendMessageSafeAsync("", "HlavickyControl.toolStripButtonCreateDefault_Click");
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            await DataManager.LogFrontendErrorSafeAsync(ex,
            //                "HlavickyControl.toolStripButtonCreateDefault_Click");
            //        }
            //    }
            //}
        }

        private async void toolStripButtonReload_Click(object sender, EventArgs e)
        {
            await ReloadHlavicky();
        }

        #endregion

        #region Private Methods

        private async Task ReloadHlavicky()
        {
            // Vyprazdnim hlavicky
            listViewHlavicky.Items.Clear();
            _hlavickyItems.Clear();

            try
            {
                Cursor = Cursors.WaitCursor;
                foreach (var h in DataManager.CoreFiles.Hlavicky.Values)
                {
                    if (checkBoxilltruj.Checked)
                    {
                        var typ = (HlavickaType)comboBoxTypHlavicky.SelectedItem;
                        if (h.Type != typ)
                            continue;
                    }

                    if (!AddListViewItem(h))
                    {
                        // Toto by sa nemalo stat
                        // znamena to ze uz hlavicka existuje
                        await DataManager.LogFrontendMessageSafeAsync($"NEMALO BY SA STAT{Environment.NewLine}Hlavicka {h} uz existuje", "HlavickyControl.ReloadHlavicky");
                    }
                }

                // Vyberiem prvu hlavicku
                if (listViewHlavicky.SelectedItems.Count == 0 && listViewHlavicky.Items.Count > 0)
                    listViewHlavicky.Items[0].Selected = true;
            }
            catch (Exception ex)
            {
                if (DataManager != null)
                    await DataManager.LogFrontendErrorSafeAsync(ex, "HlavickyControl.ReloadHlavicky");

                MessageBox.Show(Properties.Resources.AktivaciaErrorMessage.Replace("{t}", TitleText).Replace("{e}", ex.Message),
                    Properties.Resources.AktivaciaErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private Hlavicka VratVybranuHlavicku()
        {
            // Vratim vybranu hlavicku
            if (listViewHlavicky.SelectedItems.Count == 0)
                return null;

            return listViewHlavicky.SelectedItems[0].Tag as Hlavicka;
        }

        private bool AddListViewItem(Hlavicka h)
        {
            // Pridam hlavicku do list view ak sa tam uz nenachadza
            if (_hlavickyItems.ContainsKey(h))
                return false;

            var item = new ListViewItem(h.Name);
            item.Tag = h;
            item.SubItems.Add(h.Data.RiadkyHlavicka.ToString());
            item.SubItems.Add(h.Data.RiadkyStrana.ToString());
            item.SubItems.Add(h.Data.StlpceHlavicka.ToString());
            item.ImageIndex = 0;
            listViewHlavicky.Items.Add(item);
            _hlavickyItems.Add(h, item);

            return true;
        }

        private async Task PridajHlavicku(string file)
        {
            try
            {
                var hlavicka = new Hlavicka(file);
                if (DataManager.CoreFiles.Hlavicky.AddValue(hlavicka))
                {
                    MessageBox.Show($"Nepodarilo sa mi pridať novú hlavičku {file}", "Hlavička nepridaná", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await DataManager.LogFrontendMessageSafeAsync($"Hlavicka uspesne pridana {hlavicka}", "HlavickyControl.PridajHlavicku");
                    AddListViewItem(hlavicka);
                }
            }
            catch (Exception ex)
            {
                await DataManager.LogFrontendErrorSafeAsync(ex, "HlavickyControl.PridajHlavicku");
                MessageBox.Show("Pri pokuse o nahranie hlavičky nastala neočakávaná chyba. " + ex.Message,
                    "Chyba, " + ex.GetType().ToString(),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region EventHandlers (Others)

        private async void listViewHlavicky_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Zmena vybranej hlavicky
            // Ak je nejaka vybrana tak ju zobrazim v hlavicky controle
            var hlavicka = VratVybranuHlavicku();
            if (hlavicka is null)
                return;

            await hlavickaControl.ZobrazHlavicku(DataManager, hlavicka);
        }

        private void listViewHlavicky_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                toolStripButtonRemove_Click(this, EventArgs.Empty);
            }
            else if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                toolStripButtonShow_Click(this, EventArgs.Empty);
            }
        }

        private void checkBoxilltruj_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxTypHlavicky.Enabled = checkBoxilltruj.Checked;

            if (checkBoxilltruj.Checked)
            {
                comboBoxTypHlavicky.SelectedIndex = 0;
            }
            else
            {
                comboBoxTypHlavicky.SelectedIndex = -1;
            }
        }

        private async void comboBoxTypHlavicky_SelectedIndexChanged(object sender, EventArgs e)
        {
            await ReloadHlavicky();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            foreach (var h in _hlavickyItems)
                h.Key.ReloadData();
        }

        private void buttonRoll_Click(object sender, EventArgs e)
        {
            if (_animating)
                return;

            _animating = true;

            var transition = new Transitions.Transition(new Transitions.TransitionType_Deceleration(250));

            var text = "v";
            var toolTip = "Zobraz filtre pre hlavičky";

            var targetHeight = _collapsedHeight == panelHlavickaFilter.Height ? _expandedHeight : _collapsedHeight;

            transition.add(panelHlavickaFilter, "Height", targetHeight);

            if (targetHeight != _collapsedHeight)
            {
                text = "^";
                toolTip = "Schovaj filtre pre hlavičky";
            }

            transition.TransitionCompletedEvent += (snd, ea) =>
            {
                _animating = false;
            };

            transition.run();

            buttonRoll.Text = text;
            toolTipInfo.SetToolTip(buttonRoll, toolTip);
        }

        #endregion
    }
}