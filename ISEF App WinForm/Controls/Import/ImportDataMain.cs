namespace cvti.isef.winformapp.Controls.Main.Import
{
    using cvti.data;
    using cvti.data.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    public partial class ImportDataMain : UserControl, IDataManagerDependant
    {
        /// <summary>
        /// Event signalizujuci zmenu kroku na ktorom sa import nachadza ako <see cref="ImportSteps"/>
        /// </summary>
        internal event EventHandler<ImportSteps> ImportStepChanged;

        /// <summary>
        /// Kroky importu dat
        /// </summary>
        internal enum ImportSteps
        {
            VyberStupen,
            VyberVstup,
            Moznosti,
            Import,
            Zhrnutie
        }
        
        #region Constructor & Variables
        private InputType _type = InputType.MaO;
        private OthersInputType _othersType = OthersInputType.MV;
        private ImportSteps _step = ImportSteps.VyberStupen;

        // Active and previously active control
        private Control _activeControl;

        private Stack<Control> _controls = new Stack<Control>();

        // Dostupne help tiles
        readonly HelpControl[] _availableHelp;

        // Dynamicke info pre prvy krok
        // Ked user nabehne cursorom do tilu ktory odpoveda stupnu tak sa mu zobrazi prisluchajuci help
        readonly Dictionary<InputType, HelpTileInfo> _stupenSpecificInfo = new Dictionary<InputType, HelpTileInfo>()
        {
            { InputType.MaO, new HelpTileInfo("Mestá a obce", "Vstup pre mestá a obce pozostáva z ôsmich DBF súborov, kde každý jeden súbor predstavuje príjmy a výdavky škôl a školských zariadení za jeden samoysprávny kraj", Properties.Resources.mao) },
            { InputType.MV, new HelpTileInfo("Ministerstvo vnútra", "Vstup pre ministerstvo vnútra pozostáva z dvoch excelovských súborov (musia byť prekonvertované na novši XLSX formát). Jeden súbor predstavúje príjmy a výdavky za rozpočtové organiácie a druhý za príspevkové organizácie.", Properties.Resources.mv) },
            { InputType.OPRO, new HelpTileInfo("Priamo riadené org.", "Vstup pre priamo riadené organizácie pozostáva z dvoch excelovských súborov (musia byť prekonvertované na novší XLSX formát). Jeden súbor obsahuje príjmy a výdavky za rozpočtové organizácie a druhý za príspevkové organizácie", Properties.Resources.pro) },
            { InputType.VUC, new HelpTileInfo("Vyššie územné celky", "Vstup pre vyššie územné celky pozostáva z dvoch excelovských súborov (musia byť prekonvertované na novší XLSX formát). Jeden súbor obsahuje príjmy a výdavky za rozpočtové organizácie a druhý za príspevkové organizácie", Properties.Resources.vuc) },
            { InputType.VVS, new HelpTileInfo("Verejné vysoké školy", "Vstup pre verejné vysoké školy pozostáva z jedného excelovského zošita. Zošit musí byť novšieho XLSX formátu.", Properties.Resources.vvs) },
        };

        public ImportDataMain()
        {
            InitializeComponent();
            _activeControl = importData1;
            _availableHelp = new HelpControl[]
            {
                helpControl2,
                helpControl1,
                helpControl3,
                helpControl4
            };

            verticalProgress.AddStep("Stupeň", "Výber kalendárneho stupňa pre ktorý importujem údaje.");
            verticalProgress.AddStep("Vstupný súbor", "Výber vstupného súboru / súborov a kalendárneho roku pre vybraný stupeň.");
            verticalProgress.AddStep("Možnosti", "Nastavenie možností importu.");
            verticalProgress.AddStep("Import", "Import údajov a report importu.");
            verticalProgress.AddStep("Dokončenie", "Finalizácia a zhrnutie importovaných dát.");
            ShowHelp();

        }
        #endregion

        private ISEFDataManager _manager;
        public ISEFDataManager Manager
        {
            get => _manager;
            set
            {
                _manager = value;
                importShowProgress1.Manager = value;
                Enabled = (_manager != null);
            }
        }

        public void SelectStupen(InputType e)
        {
            if (_step != ImportSteps.VyberStupen)
                return;

            // momentalne to funguje tak ze prvy step je vzdy valid
            if (!importData1.IsValid())
                return;

            _type = e;
            Control nextControl;
            if (e == InputType.MaO)
            {
                nextControl = importChooseDBFFiles1;
            }
            else if (e == InputType.VVS)
            {
                nextControl = importChooseExcelFile1;
            }
            else
            {
                nextControl = importChooseExcelFiles;

                _othersType = (e == InputType.MV ? OthersInputType.MV : e == InputType.OPRO ? OthersInputType.OPRO : OthersInputType.VUC);
            }

            StepForward(nextControl);
        }

        #region EventHadnlers (1st Step)
        private void importData1_StupenMouseEntered(object sender, InputType e)
        {
            if (_stupenSpecificInfo.ContainsKey(e))
            {
                _availableHelp[0].Visible = helpControl2.Enabled = true;
                _availableHelp[0].SetHelpInfo(_stupenSpecificInfo[e]);
            }
            else
            {
                _availableHelp[0].Visible = helpControl2.Enabled = false;
            }
        }
        private void importData1_StupenSelected(object sender, InputType e) => SelectStupen(e);
        #endregion

        #region EventHandlers (2nd Step)
        private async void importChooseDBFFiles1_MoveNext(object sender, System.EventArgs e)
        {
            if (await Manager.MSSQLManager.AreDataPresentAsync(InputType.MaO, importChooseDBFFiles1.SelectedYear))
            {
                MessageBox.Show(ParentForm, $"Dáta pre rok { importChooseDBFFiles1.SelectedYear} a Mestá a obce sa už nachádzajú v databáze, ak chcete nahrať nové údaje musíte najprv pôvodné údaje ostrániäť", "Existujúce dáta",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SelectedYear = importChooseDBFFiles1.SelectedYear;
            await importRequiredOptions1.LoadYears(_manager);

            if (importChooseDBFFiles1.IsValid())
            {
                importShowProgress1.ImportujObce(importChooseDBFFiles1.SelectedFiles, importChooseDBFFiles1.SelectedYear);
                StepForward(importRequiredOptions1);
            }
            else
            {
                MessageBox.Show("Pre nahratie údajov pre mestá a obce musíte najprv vybrať všetkých 8 vstupných súborov", 
                    "Neplatný vstup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private async void importChooseExcelFile1_MoveNext(object sender, System.EventArgs e)
        {
            if (await Manager.MSSQLManager.AreDataPresentAsync(InputType.VVS, importChooseExcelFile1.SelectedYear))
            {
                MessageBox.Show(ParentForm, $"Dáta pre rok { importChooseExcelFile1.SelectedYear} a VVŠ sa už nachádzajú v databáze, ak chcete nahrať nové údaje musíte najprv pôvodné údaje ostrániäť", "Existujúce dáta",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SelectedYear = importChooseExcelFile1.SelectedYear;
            await importRequiredOptions1.LoadYears(_manager);

            if (importChooseExcelFile1.IsValid())
            {
                //await importShowProgress1.LoadYears();

                importShowProgress1.ImportujVVS(importChooseExcelFile1.SelectedFile, importChooseExcelFile1.SelectedYear);
                StepForward(importRequiredOptions1);
            }
            else
            {
                MessageBox.Show("Pre nahratie údajov pre VVŠ musíte vybrať vstupný XLSX súbor.",
                    "Neplatný vstup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private async void importChooseExcelFiles_MoveNext(object sender, System.EventArgs e)
        {
            if (await Manager.MSSQLManager.AreDataPresentAsync(_type, importChooseExcelFiles.SelectedYear))
            {
                MessageBox.Show(ParentForm, $"Dáta pre rok { importChooseExcelFiles.SelectedYear} a {_othersType} sa už nachádzajú v databáze, ak chcete nahrať nové údaje musíte najprv pôvodné údaje ostrániäť", "Existujúce dáta",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SelectedYear = importChooseExcelFiles.SelectedYear;
            await importRequiredOptions1.LoadYears(_manager);

            if (importChooseExcelFiles.IsValid())
            {
                //await importShowProgress1.LoadYears();

                importShowProgress1.Importuj(importChooseExcelFiles.ROFile, importChooseExcelFiles.POFile, importChooseExcelFiles.SelectedYear, _othersType);
                StepForward(importRequiredOptions1);
            }
            else
            {
                MessageBox.Show($"Pre nahratie údajov za {_othersType} musíte vybrať oba vstupné súbory za prispevkové aj za rozpočtové organizácie.",
    "Neplatný vstup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void importChooseFile_MovePrev(object sender, EventArgs e)
        {
            StepBack();
            //labelTitle.Text = _titleBase;
            importChooseExcelFile1.ResetControl();
        }
        #endregion

        #region EventHandlers (3rd step)
        private void importRequiredOptions1_MoveNext(object sender, EventArgs e)
        {
            if (importRequiredOptions1.IsValid())
            {
                importShowProgress1.SetOptions(importRequiredOptions1.CheckOrganizations, importRequiredOptions1.CheckAe, importRequiredOptions1.UseDefault, importRequiredOptions1.FromYear);
                StepForward(importShowProgress1);
            }
        }

        private void importRequiredOptions1_MovePrev(object sender, EventArgs e)
        {
            StepBack();
            //labelTitle.Text = _titleBase;
            importChooseExcelFile1.ResetControl();
        }
        #endregion

        #region EventHandlers (4th Step)
        private void importShowProgress1_MoveNext(object sender, System.EventArgs e)
        {
            // TODO: zobraz zhrnutie - resp zatial mozes zavriet form 
            //CloseMe?.Invoke(this, EventArgs.Empty);
            importShowProgress1.FinalStep();
            verticalProgress.NextStep();
        }
        private void importShowProgress1_MovePrev(object sender, System.EventArgs e)
        {
            StepBack();
            //labelTitle.Text = _titleBase + " - " + _type.ToString();
        }
        #endregion

        #region Private Methods
        private void ShowHelp()
        {
            // Hide and disable all help tiles
            foreach (var h in _availableHelp.Skip(1))
                h.Visible = h.Enabled = false;

            var importStep = _activeControl as IImportStep;

            var helpIndex = 0;
             _availableHelp[helpIndex++].SetHelpInfo(_stupenSpecificInfo[_type]);

            //helpIndex++;
            foreach (var h in importStep.StepHelp)
            {
                _availableHelp[helpIndex].Enabled = _availableHelp[helpIndex].Visible = true;
                _availableHelp[helpIndex++].SetHelpInfo(h);
            }
        }
        private bool StepBack()
        {
            if (!_controls.Any())
                return false;

            verticalProgress.PreviousStep();
            _activeControl.Visible = _activeControl.Enabled = false;
            _activeControl = _controls.Pop();
            _activeControl.Visible = _activeControl.Enabled = true;
            _activeControl.BringToFront();

            _step = (ImportSteps)(((int)_step) - 1);

            ShowHelp();

            ImportStepChanged?.Invoke(this, _step);

            return true;
        }
        private void StepForward(Control c)
        {
            verticalProgress.NextStep();
            _activeControl.Visible = _activeControl.Enabled = false;
            _controls.Push(_activeControl);

            _activeControl = c;
            _activeControl.Visible = _activeControl.Enabled = true;
            _activeControl.BringToFront();

            _step = (ImportSteps)(((int)_step) + 1);

            ShowHelp();

            ImportStepChanged?.Invoke(this, _step);
        }
        #endregion

        public event EventHandler CloseMe;

        public InputType SelectedImportType
        {
            get => _type;
        }

        public int SelectedYear
        {
            get; private set;
        }
    }
}