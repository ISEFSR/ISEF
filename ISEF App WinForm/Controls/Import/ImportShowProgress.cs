namespace cvti.isef.winformapp.Controls.Import
{
    using System;
    using System.Collections.Generic; 
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using cvti.data;
    using cvti.data.Classifiers;
    using cvti.data.Core;
    using cvti.data.Enums;
    using cvti.data.Input;
    using cvti.isef.winformapp.Controls.Main.Import;

    public partial class ImportShowProgress : UserControl, IImportStep, IDataManagerDependant
    {
        private int _year;
        private ExcelImportFile _po;
        private ExcelImportFile _ro;
        private ExcelImportFile _vvs;
        private OthersInputType _type;
        private DBFImportFile[] _obce;

        private bool _checkOrganizations = false;
        private bool _checkAe = false;
        private bool _useDefaults = true;
        private int _updateFromYear = -1;

        private bool _prebiehaImport = false;
        private bool _importUspesnePrebehol = false;

        public ImportShowProgress()
        {
            InitializeComponent();
        }

        public event EventHandler MoveNext;
        public event EventHandler MovePrev;

        public IEnumerable<HelpTileInfo> StepHelp => new HelpTileInfo[]
        {
            new HelpTileInfo("Načítanie údajov", "Prvý krok, ktorý program spraví je, že načíta potrebné údaje z jedného vstupného súboru do pamäte, jedného DBF súboru, alebo jedného XLSX zošitu.", null),
            new HelpTileInfo("Validácia číselníkov", "V druhom kroku prejde program údaje z ekonomickej, funkčnej, zdrojovej a programovej klasifikácie a ICA organizácií. Ak zistí, že v číselníkoch pre vybraný rok chýbajú údaje tak ich naimportuje a upozorní o tom používateľa. Pre takéto údaje bude treba doplniť popisný a skrátený text.", null),
            new HelpTileInfo("Zápis údajov", "V poslednom treťom kroku sa importujú údaje na MSSQL server. Tento krok môźe trvať najdlhšie keďže údaje sa importujú pomocou klasického INSERT INTO príkazu po 1000 záznamoch.", null)
        };

        private ISEFDataManager _manager;
        public ISEFDataManager Manager
        {
            get { return _manager; }
            set
            {
                if (_manager != null)
                {
                    _manager.MSSQLManager.CiselnikyManager.EkonomickaAdded -= CiselnikyManager_EkonomickaAdded;
                    _manager.MSSQLManager.CiselnikyManager.FunkcnaAdded -= CiselnikyManager_FunkcnaAdded;
                    _manager.MSSQLManager.CiselnikyManager.ProgramAdded -= CiselnikyManager_ProgramAdded;
                    _manager.MSSQLManager.CiselnikyManager.ZdrojAdded -= CiselnikyManager_ZdrojAdded;
                    _manager.MSSQLManager.OrganizacieManager.OrganizaciaAdded -= OrganizacieManager_OrganizaciaAdded;
                    _manager.MSSQLManager.CiselnikyManager.RiadokUpdated -= CiselnikyManager_RiadokUpdated;
                    _manager.MSSQLManager.ObceRowsImported -= MSSQLManager_ObceRowsImported;
                    _manager.MSSQLManager.OthersRowsImported -= MSSQLManager_OthersRowsImported;
                }
                if (value != null)
                {
                    _manager = value;
                    _manager.MSSQLManager.CiselnikyManager.EkonomickaAdded += CiselnikyManager_EkonomickaAdded;
                    _manager.MSSQLManager.CiselnikyManager.FunkcnaAdded += CiselnikyManager_FunkcnaAdded;
                    _manager.MSSQLManager.CiselnikyManager.ProgramAdded += CiselnikyManager_ProgramAdded;
                    _manager.MSSQLManager.CiselnikyManager.ZdrojAdded += CiselnikyManager_ZdrojAdded;
                    _manager.MSSQLManager.OrganizacieManager.OrganizaciaAdded += OrganizacieManager_OrganizaciaAdded;
                    _manager.MSSQLManager.CiselnikyManager.RiadokUpdated += CiselnikyManager_RiadokUpdated;
                    _manager.MSSQLManager.ObceRowsImported += MSSQLManager_ObceRowsImported;
                    _manager.MSSQLManager.OthersRowsImported += MSSQLManager_OthersRowsImported;
                }
            }
        }

        private void OrganizacieManager_OrganizaciaAdded(object sender, NovaOrganizacia e)
        {
            AppendInfo($"Nová organizácia pridaná {e.ICO}");
        }

        private async void MSSQLManager_OthersRowsImported(object sender, Tuple<OthersInputType, int> e)
        {
            progressBarSingleFile.Value += e.Item2;
            AppendInfo($"Naimportovaných {e.Item2} viet za {e.Item1}.");
            await Task.Delay(1);
        }

        private async void MSSQLManager_ObceRowsImported(object sender, int e)
        {
            progressBarSingleFile.Value += e;
            AppendInfo($"Naimportovaných {e} viet za MaO.");
            await Task.Delay(1);
        }

        private void CiselnikyManager_RiadokUpdated(object sender, data.Tables.CiselnikRiadok e) => AppendInfo($"-");
        private void CiselnikyManager_ZdrojAdded(object sender, AnalytickaEvidenciaNovaHodnota e) => AppendInfo($"Nová zdrojová položka pridaná {e.Kod}");
        private void CiselnikyManager_ProgramAdded(object sender, AnalytickaEvidenciaNovaHodnota e) => AppendInfo($"Nová programová položka pridaná {e.Kod}");
        private void CiselnikyManager_FunkcnaAdded(object sender, AnalytickaEvidenciaNovaHodnota e) => AppendInfo($"Nová funkčná položka pridaná {e.Kod}");
        private void CiselnikyManager_EkonomickaAdded(object sender, AnalytickaEvidenciaNovaHodnota e) => AppendInfo($"Nová ekonomická položka pridaná {e.Kod}");

        public bool IsValid() => true;

        public void SetOptions(bool checkOrganizations = true, bool checkAe = true, bool defaultData = true, int fromYear = -1)
        {
            _checkOrganizations = checkOrganizations;
            _checkAe = checkAe;
            _useDefaults = defaultData;
            _updateFromYear = fromYear;

            AppendInfo("Manuálna kontrola orgniazácií: " + _checkOrganizations.ToString());
            AppendInfo("Manuálna kontrola analytickej evidencie: " + _checkAe.ToString());
            AppendInfo("Update textácie pre čiselníky: " + (_useDefaults ? "DEFAULT" : "z roku " + _updateFromYear.ToString()));
        }

        public void Importuj(ExcelImportFile ro, ExcelImportFile po, int year, OthersInputType type)
        {
            Reset();
            _year = year;
            _ro = ro;
            _po = po;
            _type = type;
            SetUpImport();
        }

        public void ImportujVVS(ExcelImportFile vvs, int year)
        {
            Reset();
            _year = year;
            _vvs = vvs;
            _type = OthersInputType.VVS;
            SetUpImport();
        }

        public void ImportujObce(DBFImportFile[] files, int year) 
        {
            Reset();
            _year = year;
            _obce = files;
            SetUpImport();
        }

        public void FinalStep()
        {
            button2.Enabled = button2.Visible = false;
            button1.Text = "&OK";
            button1.DialogResult = DialogResult.OK;
        }

        private void Reset()
        {
            richTextBoxProgressReport.Clear();
            progressBarFiles.Value = progressBarSingleFile.Value = 0;

            _ro = null;
            _po = null;
            _obce = null;
            _vvs = null;
        }

        private void SetUpImport()
        {
            if (_ro != null)
            {
                progressBarFiles.Maximum = 2;
                AppendInfo($"Import {_type} zo súborov:");
                AppendInfo(_ro.FilePath);
                AppendInfo(_po.FilePath);
            }
            else if (_vvs != null)
            {
                progressBarFiles.Maximum = 1;
                AppendInfo($"Import {_type} zo súboru:");
                AppendInfo(_vvs.FilePath);
            }
            else
            {
                progressBarFiles.Maximum = _obce.Length;
                AppendInfo("Import MaO zo súborov:");
                foreach (var f in _obce)
                    AppendInfo(f.FilePath);
            }
        }

        private void AppendInfo(string text, bool timeStamp = true)
        {
            if (richTextBoxProgressReport is null)
                return;

            var ts = timeStamp ? $"[{ DateTime.Now.ToString("hh:mm")}]" : string.Empty;
            richTextBoxProgressReport.AppendText($"{ts} {text}{Environment.NewLine}");
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (_importUspesnePrebehol)
            {
                MoveNext?.Invoke(this, EventArgs.Empty);
                return;
            }

            if (_prebiehaImport)
                return;

            Cursor = Cursors.WaitCursor;
            _prebiehaImport = true;
            button1.Enabled = button2.Enabled = false;

            try
            {
                if (_vvs != null)
                {
                    await ImportVVSSafeAsync();
                }
                else if (_obce != null)
                {
                    await ImportMaOSafeAsync();
                }
                else
                {
                    await ImportOthersSafeAsync();
                }
            }
            catch (Exception ex)
            {
                await _manager.LogFrontendErrorSafeAsync(ex, "ImportShowProgress.button1_Click");
                MessageBox.Show("Pri pokuse o import údajov nastala neočakávaná chyba. " + ex.Message,
                    "Neočakávaná chyba " + ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                button1.Enabled = button2.Enabled = true;
            }
            finally
            {
                Cursor = Cursors.Default;
                _prebiehaImport = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_prebiehaImport)
                return;

            MovePrev?.Invoke(this, EventArgs.Empty);
        }

        private async Task ImportOthersSafeAsync()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                button1.Enabled = button2.Enabled = false;

                var roFile = new FileInfo(_ro.FilePath);
                var poFile = new FileInfo(_po.FilePath);

                var count = 0;
                if (roFile.Exists)
                {
                    count += await ImportExcel(roFile, _ro, _type);
                }

                if (poFile.Exists)
                {
                    count += await ImportExcel(poFile, _po, _type);
                }

                AppendInfo($"Import úspešný.");
                _importUspesnePrebehol = true;
                MessageBox.Show($"Import údajov {_type} úspešný. Naimportovaných {count} nových riadkov.", "Import úspešný",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                MoveNext?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                await Manager.LogFrontendErrorSafeAsync(ex, "ImportForm.ImportOthersSafeAsync");
                MessageBox.Show("Pri pokuse o import údajov nastala neočakávaná chyba " + ex.Message, "Import - Chyba " + ex.GetType().ToString(),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debugger.Break();
            }
            finally
            {
                Cursor = Cursors.Default;
                button1.Enabled = button2.Enabled = true;
            }
        }

        private async Task ImportVVSSafeAsync()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                button1.Enabled = button2.Enabled = false;

                var file = new FileInfo(_vvs.FilePath);

                if (file.Exists)
                {
                    var count = await ImportExcel(file, _vvs, OthersInputType.VVS);

                    _importUspesnePrebehol = true;
                    MessageBox.Show($"Import údajov {OthersInputType.VVS} úspešný. Naimportovaných {count} nových riadkov.",
                        $"Import {OthersInputType.VVS}", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    MoveNext?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    AppendInfo("Neviem nájsť " + file.FullName);
                }
            }
            catch (Exception ex)
            {
                await Manager.LogFrontendErrorSafeAsync(ex, "ImportForm.ImportVVSSafeAsync");
                MessageBox.Show("Pri pokuse o import údajov VVS nastala neočakávaná chyba " + ex.Message,
                    "Import VVŠ - Chyba " + ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debugger.Break();
            }
            finally
            {
                Cursor = Cursors.Default;
                button1.Enabled = button2.Enabled = true;
            }
        }

        private async Task ImportMaOSafeAsync()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                button1.Enabled = button2.Enabled = false;

                var count = 0;
                foreach (var ll in _obce)
                {
                    if (!System.IO.File.Exists(ll.FilePath))
                    {
                        AppendInfo("Neviem nájsť " + ll.FilePath);
                        continue;
                    }

                    AppendInfo(ll.FilePath);
                    AppendInfo("Načítavam údaje: ");

                    var data = new List<ObceDataRow>();
                    data.AddRange(await Manager.InputObce.ReadFileAsync(new FileInfo(ll.FilePath)));

                    progressBarSingleFile.Maximum = data.Count;
                    progressBarSingleFile.Value = 0;

                    AppendInfo($"{data.Count} údajov načítaných.");
                    AppendInfo($"Importujem údaje na SQL server.");

                    if (_useDefaults)
                    {
                        count += await Manager.MSSQLManager.ImportObceAsync(data, _year);
                    }
                    else
                    {
                        count += await Manager.MSSQLManager.ImportObceAsync(data, _year, _updateFromYear);
                    }

                    progressBarFiles.Value++;

                    AppendInfo($"Import úspešný.");
                    data = null;
                    GC.Collect();
                    _importUspesnePrebehol = true;
                }

                MessageBox.Show($"Import údajov MaO za rok {_year} úspešný. Naimportovaných {count} nových riadkov", "Import MaO", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                MoveNext?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                await Manager.LogFrontendErrorSafeAsync(ex, "ImportForm.ImportMaOSafeAsync");
                MessageBox.Show($"Pri pokues o import údajov za Mestá a Obce a rok {_year} nastala neočakávaná chyba. " + ex.Message,
                    ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                button1.Enabled = button2.Enabled = true;
            }
        }

        private async Task<int> ImportExcel(FileInfo file, ExcelImportFile excel, OthersInputType type)
        {
            var data = Manager.InputOthers.ReadFile(file, excel.Worksheets.ToArray(), excel.Column, excel.Row, excel.HeaderRow, false);

            AppendInfo($"Dáta pre {type} úspešne získané...");

            progressBarSingleFile.Maximum = data.Count;
            progressBarSingleFile.Value = 0;

            AppendInfo($"{data.Count} údajov načítaných.");
            AppendInfo($"Importujem údaje na SQL server.");

            var count = _useDefaults ?
                await Manager.MSSQLManager.ImportOthersAsync(data, _year, type) :
                await Manager.MSSQLManager.ImportOthersAsync(data, _year, type, _updateFromYear);

            progressBarFiles.Value++;

            AppendInfo($"Import pre {type} úspešný.");
            data = null;
            GC.Collect();

            return count;
        }
    }
}