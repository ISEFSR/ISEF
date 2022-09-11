namespace cvti.isef.winformapp.Controls.Main
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Controls.Main.Ciselniky;
    using System.Drawing;
    using cvti.data;
    using cvti.data.Core;
    using cvti.data.Enums;
    using cvti.isef.winformapp.Forms;
    using System.Diagnostics;
    using cvti.data.Columns;
    using cvti.data.Conditions;

    public partial class CiselnikyControl : UserControl, IMainTabControl
    {
        #region Variables and constructor

        // Zoznam dostupnych controlov pre manipulaciu s datami ciselnikov
        private readonly ICiselnikControl[] _ciselnikyControls;

        // Prave vybrany tab control 
        // a k nemu odpovedajuci cisselnik control
        private ICiselnikControl _vybranyCiselnik;

        private Condition _generatedCondition = null;
        private bool _wrap = false;

        private string _previousTitle;

        private bool _shouldFire = true;

        public CiselnikyControl()
        {
            InitializeComponent();

            // Inicializacia ciselnikov
            // zoznam vsetkych dostupnych ciselnikov
            // Ak bude pridany ciselnik treba ho dat aj sem!
            _ciselnikyControls = new ICiselnikControl[]
            {
                funkcnaKlasifikaciaControl,
                ekonomickaKlasifikaciaControl,
                zdrojovaKlasifikaciaControl,
                programovaKlasifikacia,
                organizacieControl,
                krajControl,
                okresControl,
                obecControl,
                podriadenostControl,
                segmentControl,
                stupneControl,
                ucetControl,
                transferControl
            };

            // Event handler pre editaciu podmienky 
            foreach (var ctrl in _ciselnikyControls)
                ctrl.ConditionAdded += Ctrl_ConditionAdded;

            _vybranyCiselnik = _ciselnikyControls[0];
            // tu sa spusti event handler a nacaita data pre rok
            numericUpDownYear.Value = DateTime.Now.Year;

            //HideInfo();

            panelInfoTitle.Visible = false;
        }

        #endregion

        #region IMainTabControl Members

        public ISEFDataManager DataManager { get; private set; }

        public string TitleText { get; set; } = Properties.Resources.CiselnikyTitle;
        public string InfoText { get; set; } = Properties.Resources.CiselnokyInfo;
        public Image TitleImage { get; set; } = Properties.Resources.lookup_white_100;
        public Image BackImage { get; set; } = null;

        public async Task Activate(ISEFDataManager manager)
        {
            DataManager = manager ?? throw new ArgumentNullException(nameof(manager));

            var availableYears = await manager.MSSQLManager.GetAvailableYearsAsync();

            if (availableYears.Any())
            {
                _shouldFire = false;
                numericUpDownYear.Value = availableYears.Last();
                _shouldFire = true;
            }

            await ReloadData();
        }

        public void Deactivate()
        {
            try
            {
                _vybranyCiselnik.Deaktivuj();
            }
            catch (Exception ex)
            {
                DataManager.LogFrontendErrorSafeAsync(ex, "CiselnikyControl.Deactivate").GetAwaiter().GetResult();
            }
        }

        #endregion

        #region Event Handlers

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Skutočne si prajete zrušiť vytváranie novej podmienky?", "Zrušenie vytvárania",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _generatedCondition = null;
                textBoxConditionText.Text = string.Empty;
                panelConditionGeneratorPanel.Visible = false;

                panelConditionGeneratorWrapper.Invalidate();
            }
        }

        private void buttonSAve_Click(object sender, EventArgs e)
        {
            // generated condition by nemalo byt null ak je save buttton viditelne a pouzitelne
            if (_generatedCondition is null)
                return;

            using (var saveCondition = new SaveGeneratedCondition(DataManager.CoreFiles.Conditions, _generatedCondition))
            {
                if (saveCondition.ShowDialog() == DialogResult.OK)
                {
                    _generatedCondition.ConditionName = saveCondition.ConditionName;
                    if (DataManager.CoreFiles.Conditions.AddValue(_generatedCondition))
                    {
                        MessageBox.Show("Nová podmienka úspešne pridaná", "Podmienka pridaná",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        _generatedCondition = null;
                        textBoxConditionText.Text = string.Empty;
                        panelConditionGeneratorPanel.Visible = false;

                        panelConditionGeneratorWrapper.Invalidate();
                    }
                    else
                    {
                        MessageBox.Show("Pri pokuse o pridanie novej podmienky nastala neočakávaná chyba.", "Podmienka nepridaná",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void buttonWrap_Click(object sender, EventArgs e)
        {
            if (_wrap)
            {
                _wrap = false;
                labelConditionTitle.Text = _previousTitle;
            }
            else
            {
                _wrap = true;
                _previousTitle = labelConditionTitle.Text;
                labelConditionTitle.Text += " * ( )";
            }
        }

        private async void numericUpDownYear_ValueChanged(object sender, EventArgs e) 
        {
            if (!_shouldFire)
                return;

            await ReloadData(); 
        }

        private async void tabControlClassifiers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DataManager is null) return;
            if (_vybranyCiselnik != null)
                _vybranyCiselnik.Deaktivuj();

            _vybranyCiselnik = _ciselnikyControls[tabControlClassifiers.SelectedIndex];

            // prezatial to mozem vsetko nehat na can remove
            // ono to je zatial  v podstate jedno lebo 
            // can remove, can update a can generate su iba pre analyticku evidenciu
            toolStripSeparator2.Visible =
                toolStripSeparator3.Visible =
                toolStripSeparator4.Visible =
                toolStripButtonGenerateData.Enabled =
                toolStripButtonGenerateData.Visible =
                toolStripButtonRemove.Enabled =
                toolStripButtonRemove.Visible =
                toolStripButtonUpdate.Enabled =
                toolStripButtonUpdate.Visible =
                    _vybranyCiselnik.CanRemove();

            labelTitle.Text = _vybranyCiselnik.TitleText;
            labelInfo.Text = _vybranyCiselnik.InfoText;

            await ReloadData();
        }

        private void Ctrl_ConditionAdded(object sender, Tuple<Column, object, AvailableConditions> e)
        {
            var condition = CiselnikyUtilities.GetCondition(e.Item1, e.Item2, e.Item3);
            if (panelConditionGeneratorPanel.Visible)
            {
                using (var f = new ChooseConditionOperator(_generatedCondition, condition))
                {
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        if (_wrap)
                        {
                            _generatedCondition = f.GetCondition().AddCondition(_generatedCondition, f.GetOperator());
                            _generatedCondition.Wrap = true;
                            labelConditionTitle.Text = _previousTitle;
                        }
                        else
                        {
                            _generatedCondition.AddCondition(f.GetCondition(), f.GetOperator());
                        }
                    }
                }
            }
            else
            {
                _generatedCondition = condition;
                panelConditionGeneratorPanel.Visible = true;
                panelConditionGeneratorWrapper.Invalidate();
            }

            textBoxConditionText.Text = _generatedCondition.GetConditionString();
        }

        #endregion

        #region Event Handlers (ToolStrip)

        private async void toolStripButtonExportSelected_Click(object sender, EventArgs e)
        {
            try
            {
                Enabled = false;

                saveFileDialog.FileName = _vybranyCiselnik.TitleText;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {

                    await _vybranyCiselnik.ExportData(saveFileDialog.FileName);

                    MessageBox.Show("Údaje pre číselník úspešne vyexportované.", "Export úspešný",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pri pokuse o export údajov do excelu nastala neočakávaná chyba. " + ex.Message,
                    "Chyba " + ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                await DataManager.LogFrontendErrorSafeAsync(ex, "CiselnikyControl.toolStripButtonExportSelected_Click");
            }
            finally
            {
                Enabled = true;
            }
        }

        private async void toolStripButtonLoadData_Click(object sender, EventArgs e)
        {
            try
            {
                Enabled = false;
                await _vybranyCiselnik.GenerateData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pri pokuse o generovanie číselníka nastala neočakávaná chyba " + ex.Message,
                    "Chyba " + ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                await DataManager.LogFrontendErrorSafeAsync(ex, "CiselnikyControl.toolStripButtonLoadData_Click");
            }
            finally
            {
                Enabled = true;
            }
        }

        private async void toolStripButtonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Enabled = false;
                await _vybranyCiselnik.UpdateData();
                await _vybranyCiselnik.ReloadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pri pokuse o update číselníka nastala neočakávaná chyba "+ ex.Message,
                    "Chyba " + ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                await DataManager.LogFrontendErrorSafeAsync(ex, "CiselnikyControl.toolStripButtonUpdate_Click");
            }
            finally
            {
                Enabled = true;
            }
        }

        private async void toolStripButtonRemove_Click(object sender, EventArgs e)
        {
            try
            {
                Enabled = false;
                await _vybranyCiselnik.RemoveData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pri pokuse o odstránenie číselníka nastala neočakávaná chyba " + ex.Message,
                    "Chyba " + ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                await DataManager.LogFrontendErrorSafeAsync(ex, "CiselnikyControl.toolStripButtonRemove_Click");
            }
            finally
            {
                Enabled = true;
            }
        }

        private async void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                Enabled = false;
                await _vybranyCiselnik.ReloadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pri pokuse o opätovné načítanie číselníka nastala neočakávaná chyba " + ex.Message,
                    "Chyba " + ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                await DataManager.LogFrontendErrorSafeAsync(ex, "CiselnikyControl.toolStripButton1_Click");
            }
            finally
            {
                Enabled = true;
            }
        }

        private async void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                Enabled = false;
                await _vybranyCiselnik.ShowPreview();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pri pokuse o zobrazene prehĺadu číselníka nastala neočakávaná chyba " + ex.Message,
                    "Chyba " + ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                await DataManager.LogFrontendErrorSafeAsync(ex, "CiselnikyControl.toolStripButton2_Click");
            }
            finally
            {
                Enabled = true;
            }
        }

        private void toolStripButtonImport_Click(object sender, EventArgs e)
        {
            _vybranyCiselnik.Import();
        }

        #endregion

        #region Private Methods

        private async Task ReloadData()
        {
            if (DataManager is null || _vybranyCiselnik is null) 
                return;

            Enabled = false;
            Cursor = Cursors.WaitCursor;
            try
            {
                labelTitle.Text = tabControlClassifiers.SelectedTab.Text;
                await _vybranyCiselnik?.NacitajDataAsync(DataManager, (int)numericUpDownYear.Value);
            }
            catch (Exception ex)
            {
                await DataManager.LogFrontendErrorSafeAsync(ex, "ZostavyControl.ReloadData");
                MessageBox.Show(Properties.Resources.AktivaciaErrorMessage.Replace("{t}", TitleText).Replace("{e}", ex.Message),
                    Properties.Resources.AktivaciaErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                Enabled = true;
            }
        }

        private void panelConditionGeneratorWrapper_Paint(object sender, PaintEventArgs e)
        {
            if (panelConditionGeneratorPanel.Visible)
                ControlUtilities.DrawShadow(panelConditionGeneratorPanel, e.Graphics);
        }

        #endregion

        private void folderBrowserDialog_HelpRequest(object sender, EventArgs e)
        {

        }

        private bool _animating = false;
        private void HideInfo()
        {
            if (_animating)
                return;

            _animating = true;

            var hideTransition = new Transitions.Transition(new Transitions.TransitionType_Deceleration(350));
            hideTransition.add(panelClassifierInfo, "Left", Width);

            hideTransition.TransitionCompletedEvent += (sender, ea) =>
            {
                _animating = false;
            };

            hideTransition.run();
        }

        private void ShowInfoForSelected()
        {
            //if (_animating)
            //    return;

            //_animating = true;

            //var showTransition = new Transitions.Transition(new Transitions.TransitionType_Deceleration(350));
            //showTransition.add(panelClassifierInfo, "Left", Width - 500);

            //showTransition.TransitionCompletedEvent += (sender, ea) =>
            //{
            //    _animating = false;
            //};

            //showTransition.run();

            //richTextBoxClassifierInfo.Focus();

            //ShowInfo();
        }

        private void ShowInfo()
        {
            //label2.Text = _vybranyCiselnik.TitleText;
        }

        private void richTextBox1_Leave(object sender, EventArgs e)
        {
            //HideInfo();
        }

        private void toolStripButtonInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Pre vybraný číselnik neexistuje, alebo nie je zadeifnovaný žiaden informačný materiál.",
                    "Neexistujúce info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //return;

            //if (string.IsNullOrWhiteSpace(_vybranyCiselnik.GetInfoText()))
            //{
            //    MessageBox.Show("Pre vybraný číselnik neexistuje, alebo nie je zadeifnovaný žiaden informačný materiál.",
            //        "Neexistujúce info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            //richTextBoxClassifierInfo.Rtf = _vybranyCiselnik.GetInfoText();

            //ShowInfoForSelected();
        }

        private async void buttonMoreInfo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_vybranyCiselnik.GetMoreInfo()))
            {
                MessageBox.Show("Dodatočné informácie ohľadom vybraného číselniku nie sú dostupné.",
                    "Informácie nedostupné", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                Process.Start(_vybranyCiselnik.GetMoreInfo());
            }
            catch (Exception ex)
            {
                await DataManager.LogFrontendErrorSafeAsync(ex, "CiselnikyControl.buttonMoreInfo_Click");
                MessageBox.Show("Pri pokuse o zobrazenie dodatočných informácií pre vybrnaý číselník nastala neočakávaná cyhba. " + ex.Message,
                    "Neznáma chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async void ChangeTransfers(int year)
        {
            tabControlClassifiers.SelectedTab = tabPageTransfer;
            await _vybranyCiselnik?.NacitajDataAsync(DataManager, (int)numericUpDownYear.Value);
        }
    }
}