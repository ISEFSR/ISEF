namespace cvti.isef.winformapp.Controls.Main.Ciselniky
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using cvti.data;
    using cvti.data.Tables;
    using System.IO;
    using cvti.data.Views;
    using cvti.data.Columns;
    using cvti.data.Enums;
    using cvti.data.Conditions;
    using cvti.data.Core;
    using cvti.isef.winformapp.Forms.Classifiers;
    using System.Drawing;
    using OfficeOpenXml;
    using System.Collections.Generic;

    public partial class PodriadenostControl : UserControl, ICiselnikControl
    {
        public PodriadenostControl()
        {
            InitializeComponent();
        }

        public int SelectedYear { get; private set; }

        public string TitleText { get => "Číselník podriadenosti"; }

        public string InfoText { get => "Číselník podriadenosti. Číselník neni viazaný na kalendárny rok."; }

        public void Deaktivuj()
        {
            comboBoxStupne.Items.Clear();
            listBoxPodriadenost.Items.Clear();
        }

        private ISEFDataManager _manager;

        public event EventHandler<Tuple<Column, object, AvailableConditions>> ConditionAdded;

        public async Task NacitajDataAsync(ISEFDataManager manager, int year)
        {
            _manager = manager;
            SelectedYear = year;

            await Task.Delay(1);

            comboBoxStupne.Items.Clear();
            foreach (var s in _manager.Stupne)
                comboBoxStupne.Items.Add(s);

            listBoxPodriadenost.Items.Clear();
            foreach (var p in _manager.Podriadenost.OrderBy(p => p.Nazov))
            {
                listBoxPodriadenost.Items.Add(p);
            }

            if (listBoxPodriadenost.Items.Count > 0 && listBoxPodriadenost.SelectedIndex == -1)
                listBoxPodriadenost.SelectedIndex = 0;
        }

        private void listBoxPodriadenost_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxPodriadenost.SelectedIndex == -1)
                return;

            ShowSelectedPodriadenost();
        }

        private PodriadenostRiadok GetSelectedPodriadenost() => listBoxPodriadenost.SelectedItem as PodriadenostRiadok;

        private void ShowSelectedPodriadenost()
        {
            var selected = GetSelectedPodriadenost();

            if (selected is null)
            {
                textBoxKod.Text = textBoxNazov.Text = textBoxPopis.Text = string.Empty;
            }
            else
            {
                textBoxKod.Text = selected.Kod;
                textBoxPopis.Text = selected.Nazov;
                textBoxNazov.Text = selected.SkratenyNazov;
            }
        }

        private void checkBoxStupenFilter_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxStupenFilter.Checked)
            {
                comboBoxStupne.Enabled = true;
                comboBoxStupne.SelectedIndex = 0;
            }
            else
            {
                comboBoxStupne.Enabled = false;
                comboBoxStupne.SelectedIndex = -1;
            }
        }

        private void comboBoxStupne_SelectedIndexChanged(object sender, EventArgs e)
        {
            NacitajPodriadenost();
        }

        private void NacitajPodriadenost()
        {
            listBoxPodriadenost.Items.Clear();
            if (comboBoxStupne.SelectedIndex == -1)
            {
                foreach (var p in _manager.Podriadenost.OrderBy(p => p.Nazov))
                    listBoxPodriadenost.Items.Add(p);
            }
            else
            {
                var stupen = comboBoxStupne.SelectedItem as StupenRiadok;
                var podKod = (from o in _manager.Organizacie where o.KodStupen == stupen.Kod select o.KodPodriadenost).Distinct();
                foreach (var p in from pd in _manager.Podriadenost where podKod.Contains(pd.Kod) orderby pd.Nazov select pd)
                    listBoxPodriadenost.Items.Add(p);
            }
        }

        public async Task ExportData(string path)
        {
            await Task.Delay(1);
            ExcelUtilities.ExportCiselnikToExcel(_manager.Podriadenost, path);
        }

        public Task GenerateData()
        {
            throw new NotImplementedException();
        }

        public Task UpdateData()
        {
            throw new NotImplementedException();
        }

        public Task RemoveData()
        {
            throw new NotImplementedException();
        }

        public bool CanGenerate() => false;

        public bool CanUpdate() => false;

        public bool CanRemove() => false;

        public async Task ReloadData()
        {
            await NacitajDataAsync(_manager, SelectedYear);
        }

        public async Task ShowPreview()
        {
            Condition cnd = null;
            var title = $"Prehľad podriadenosti pre rok {SelectedYear}";
            if (checkBoxStupenFilter.Checked)
            {
                var stupen = comboBoxStupne.SelectedItem as StupenRiadok;
                if (stupen != null)
                {
                    cnd = new Equals(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod), stupen.Kod);
                    title = $"Prehľad podriadenosti za {stupen.Nazov}";
                }
            }

            await CiselnikyUtilities.ShowPreviewForColumns(_manager,
                new AssuViewColumn[]
                {
                    AssuView.VratStlpec(AssuViewAvailableColumns.PodriadenostKod,  true, "Kod"),
                    AssuView.VratStlpec(AssuViewAvailableColumns.PodriadenostSkrateny, true, "Skrateny"),
                    AssuView.VratStlpec(AssuViewAvailableColumns.PodriadenostNazov, true, "Nazov")
                }, SelectedYear, cnd, title);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var pod = listBoxPodriadenost.SelectedItem as PodriadenostRiadok;

            var clmn = AssuView.VratStlpec(AssuViewAvailableColumns.PodriadenostKod);

            try
            {
                Enabled = false;

                await CiselnikyUtilities.ShowPreviewForColumns(_manager,
                    CiselnikyUtilities.VratStlpcePrePreview(),
                    SelectedYear, new Equals(string.Empty, clmn, pod.Kod),
                    $"Prehľad za podriadenosť {pod.Nazov}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pri pokuse o zobrazenie údajov pre číselník nastala neočakávaná chyba " + ex.Message,
                    "Neočakávaná chyba " + ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);

                await _manager.LogFrontendErrorSafeAsync(ex, "PodriadenostControl.button1_Click");
            }
            finally
            {
                Enabled = true;
            }
        }

        private void andToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CiselnikyUtilities.CreateCondition(sender, ConditionAdded);
        }

        private async void buttonSaveChanges_Click(object sender, EventArgs e)
        {
            var selectedPodriadenost = GetSelectedPodriadenost();

            if (selectedPodriadenost is null)
                return;

            selectedPodriadenost.SkratenyNazov = textBoxNazov.Text;
            selectedPodriadenost.Nazov = textBoxPopis.Text;

            try
            {
                if (await _manager.MSSQLManager.Podriadenost.UpdateRiadok(selectedPodriadenost))
                {
                    var index = listBoxPodriadenost.SelectedIndex;
                    listBoxPodriadenost.Items.RemoveAt(index);
                    listBoxPodriadenost.Items.Insert(index, selectedPodriadenost);
                    listBoxPodriadenost.SelectedIndex = index;

                    await _manager.LogFrontendMessageSafeAsync($"Textové polia pre podriadenosť s kódom {selectedPodriadenost.Kod} úspešne zmenené.",
                        "PodriadenostControl.buttonSaveChanges_Click");
                    MessageBox.Show($"Textové polia pre podriadenosť s kódom {selectedPodriadenost.Kod} úspešne zmenené.",
                        "Zmena úspešná", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Vyzerá to tak, źe sa mi nepodarilo uložiť zmeny vykonané na podriadenosti. Pre istotu skontrolujte údaje a prípadne skúste uložiť ešte raz.",
                        "Údaje neuložené", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                await _manager.LogFrontendMessageSafeAsync($"Textové polia pre podriadenosť s kódom {selectedPodriadenost.Kod} úspešne zmenené.",
                    "PodriadenostControl.buttonSaveChanges_Click");
                MessageBox.Show("Pri pokuse o uloženie údajov pre podriadenosť nastala neočakávaná chyba. " + ex.Message,
                    "Údaje neuložené", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            ShowSelectedPodriadenost();
        }

        public bool CanCreate() => true;

        public async Task CreateItem()
        {
            await Task.Delay(1);
            using (var krajForm = new NewPodriadenostForm(_manager))
            {
                if (krajForm.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Táto možnosť je momentálne nedostupná...", "Nedostupná možnosť.",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void panelControlButtons_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new System.Drawing.Pen(new SolidBrush(Color.FromArgb(210, 210, 210))),
new Point(0, 0),
new Point(panelControlButtons.Width, 0));
        }

        public string GetInfoText() => null;

        public string GetMoreInfo() => null;

        void ICiselnikControl.Import()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Title = "Import číselník",

                CheckFileExists = true,
                CheckPathExists = true,

                Filter = "XLSX files (*.XLSX)|*.XLSX",
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileInfo excel = new FileInfo(openFileDialog1.FileName);
                using (var pckg = new ExcelPackage(excel))
                {
                    var exlwkcp = pckg.Workbook.Worksheets[0];

                    if (exlwkcp.Name == "data")
                    {
                        Dictionary<string, List<string>> dictcp = LoadDataFromExcelcp(exlwkcp);
                    }
                    else
                    {
                        MessageBox.Show("Zly file");
                        return;
                    }
                }
            }
        }

        private Dictionary<string, List<string>> LoadDataFromExcelcp(ExcelWorksheet excelWS)
        {
            Dictionary<string, List<string>> ret = new Dictionary<string, List<string>>();


            for (int row = 2; row <= excelWS.Dimension.End.Row; row++)
            {
                List<String> cpRiadok = new List<string>();

                String Kod = excelWS.Cells[row, 1].Value.ToString();
                //SkratenyNazov
                cpRiadok.Add(excelWS.Cells[row, 2].Value.ToString());
                //Nazov
                cpRiadok.Add(excelWS.Cells[row, 3].Value.ToString());

                ret.Add(Kod, cpRiadok);
            }

            return ret;
        }
    }
}
