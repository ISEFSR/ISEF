namespace cvti.isef.winformapp.Controls.Main.Ciselniky
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using cvti.data.Core;
    using cvti.data;
    using cvti.data.Tables;
    using System.IO;
    using cvti.data.Columns;
    using cvti.data.Views;
    using cvti.data.Enums;
    using cvti.data.Conditions;
    using System.Diagnostics;
    using System.Drawing;
    using OfficeOpenXml;
    using System.Collections.Generic;

    public partial class OrganizacieControl : UserControl, ICiselnikControl
    {
        private ISEFDataManager _manager;

        public event EventHandler<Tuple<Column, object, AvailableConditions>> ConditionAdded;

        public OrganizacieControl()
        {
            InitializeComponent();
        }

        public int SelectedYear { get; private set; }

        public string TitleText { get => "Číselník organizácií"; }

        public string InfoText { get => "Číselník organizácií. Číselník neni viazaný na kalendárny rok."; }

        public async Task NacitajDataAsync(ISEFDataManager manager, int year)
        { 
            _manager = manager;
            SelectedYear = year;

            Deaktivuj();

            foreach (var s in _manager.Stupne)
            {
                comboBoxStupen.Items.Add(s);
                comboBoxStupenFilter.Items.Add(s);
            }

            foreach (var s in _manager.Segmenty)
                comboBoxSegm.Items.Add(s);

            foreach (var o in _manager.Obce.OrderBy(o => o.Nazov))
                comboBoxObec.Items.Add(o);

            foreach (var p in _manager.Podriadenost.OrderBy(p => p.Nazov))
                comboBoxPodr.Items.Add(p);

            await Task.Delay(1);

            comboBoxStupenFilter.SelectedIndex = 0;
        }

        public void Deaktivuj()
        {
            comboBoxPodr.Items.Clear();
            comboBoxObec.Items.Clear();
            comboBoxSegm.Items.Clear();
            comboBoxStupen.Items.Clear();
            comboBoxStupenFilter.Items.Clear();

            listBoxOrganizacie.Items.Clear();
        }

        private async void buttonSave_Click(object sender, EventArgs e)
        {
            if (listBoxOrganizacie.SelectedIndex == -1)
                return;

            var org = listBoxOrganizacie.SelectedItem as OrganizaciaRiadok;

            if (org is null)
                return;

            org.Nazov = textBoxShort.Text;
            org.Ulica = textBoxLong.Text;

            var stupenChanged = org.KodStupen != (comboBoxStupen.SelectedItem as StupenRiadok).Kod;

            org.KodStupen = (comboBoxStupen.SelectedItem as StupenRiadok).Kod;
            org.KodSegment = (comboBoxSegm.SelectedItem as SegmentRiadok).Kod;
            org.KodPodriadenost = (comboBoxPodr.SelectedItem as PodriadenostRiadok).Kod;
            org.KodObec = (comboBoxObec.SelectedItem as ObecRiadok).Kod;

            if (await _manager.MSSQLManager.OrganizacieManager.UpdateOrganizacia(org))
            {
                if (stupenChanged)
                {
                    await NacitajDataAsync(_manager, SelectedYear);
                }
                else
                {
                    var index = listBoxOrganizacie.SelectedIndex;
                    listBoxOrganizacie.Items.RemoveAt(index);
                    listBoxOrganizacie.Items.Insert(index, org);
                    listBoxOrganizacie.SelectedIndex = index;
                }
                await _manager.LogFrontendMessageSafeAsync($"Meniteľné údaje pre organizáciu s ICO {org.Ico} úspešne zmenené.",
                    "OrganizacieControl.buttonSave_Click");
                MessageBox.Show($"Meniteľné údaje pre organizáciu s ICO {org.Ico} úspešne zmenené.",
                    "Zmena úspešná", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            if (listBoxOrganizacie.SelectedIndex == -1)
                return;

            ZobrazOrganizaciu(listBoxOrganizacie.SelectedItem as OrganizaciaRiadok);
        }

        private void comboBoxKraj_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxStupenFilter.SelectedIndex == -1)
                return;

            ZobrazOrganizacie();
        }

        private void ZobrazOrganizacie()
        {
            listBoxOrganizacie.Items.Clear();

            var filter = cueTextBoxFilter.Text;
            var stupen = comboBoxStupenFilter.SelectedItem as StupenRiadok;
            foreach (var o in from org in _manager.Organizacie where org.KodStupen == stupen.Kod orderby org.Nazov select org)
            {
                if (!string.IsNullOrWhiteSpace(filter))
                {
                    if (!o.Nazov.ToLower().Contains(filter) &&
                        !o.Ulica.ToLower().Contains(filter) &&
                        !o.Ico.ToString().Contains(filter))
                        continue;
                }

                listBoxOrganizacie.Items.Add(o);
            }

            if (listBoxOrganizacie.Items.Count > 0 && listBoxOrganizacie.SelectedIndex == -1)
                listBoxOrganizacie.SelectedIndex = 0;
        }

        private void listBoxOrganizacie_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxOrganizacie.SelectedIndex == -1)
                return;

            ZobrazOrganizaciu(listBoxOrganizacie.SelectedItem as OrganizaciaRiadok);
        }

        public async Task ExportData(string path)
        {
            await Task.Delay(1);
            ExcelUtilities.ExportCiselnikToExcel(_manager.Organizacie, path);
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
            var stupen = comboBoxStupenFilter.SelectedItem as StupenRiadok;
            var cnd = new Equals(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod), stupen.Kod);
            await CiselnikyUtilities.ShowPreviewForColumns(_manager,
                new AssuViewColumn[]
                {
                    AssuView.VratStlpec(AssuViewAvailableColumns.OrgIco, true, "ICO"),
                    AssuView.VratStlpec(AssuViewAvailableColumns.OrgNazov, true, "Nazov")
                }, SelectedYear, cnd, $"Prehľad pre {stupen.Nazov}");
        }

        private void ZobrazOrganizaciu(OrganizaciaRiadok org)
        {
            if (org is null)
            {
                textBoxICO.Text =
                    textBoxLong.Text =
                    textBoxShort.Text = string.Empty;
            }
            else
            {
                textBoxICO.Text = org.Ico;

                comboBoxStupen.SelectedItem = _manager.VratStupen(org.KodStupen);
                comboBoxSegm.SelectedItem = _manager.VratSegment(org.KodSegment);
                comboBoxPodr.SelectedItem = _manager.VratPodriadenost(org.KodPodriadenost);
                comboBoxObec.SelectedItem = _manager.VratObec(org.KodObec);

                textBoxShort.Text = org.Nazov;
                textBoxLong.Text = org.Ulica;
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var org = listBoxOrganizacie.SelectedItem as OrganizaciaRiadok;

            var clmn = AssuView.VratStlpec(AssuViewAvailableColumns.OrgIco);

            try
            {
                Enabled = false;

                await CiselnikyUtilities.ShowPreviewForColumns(_manager,
                    CiselnikyUtilities.VratStlpcePrePreview(), 
                    SelectedYear, new Equals(string.Empty, clmn, org.Ico), $"Prehľad pre [{org.Ico}]{org.Nazov}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pri pokuse o zobrazenie údajov pre číselník nastala neočakávaná chyba " + ex.Message,
                    "Neočakávaná chyba " + ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);

                await _manager.LogFrontendErrorSafeAsync(ex, "OrganizacieControl.button1_Click");
            }
            finally
            {
                Enabled = true;
            }
        }

        private void andToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBoxOrganizacie.SelectedIndex == -1)
                return;

            CiselnikyUtilities.CreateCondition(sender, ConditionAdded);
        }

        public bool CanCreate() => false;

        public Task CreateItem()
        {
            throw new NotImplementedException();
        }

        private void buttonWeb_Click(object sender, EventArgs e)
        {
            try
            {
                var organizacia = listBoxOrganizacie.SelectedItem as OrganizaciaRiadok;
                if (organizacia is null)
                    return;

                var url = "https://www.google.com/search?q=ICO organizácie " + organizacia.Ico;
                using (var p = Process.Start(url))
                {

                }
                    
            }
            catch (Exception ex)
            {

            }
        }

        public string GetInfoText() => Properties.Resources.InfoOrg;

        public string GetMoreInfo() => Properties.Resources.MoreInfoOrg;

        private void panelControlButtons_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new System.Drawing.Pen(new SolidBrush(Color.FromArgb(210, 210, 210))),
new Point(0, 0),
new Point(panelControlButtons.Width, 0));
        }

        private void cueTextBoxFilter_TextChanged(object sender, EventArgs e)
        {
            if (timerFilterDelay.Enabled)
                timerFilterDelay.Stop();

            timerFilterDelay.Start();
        }

        private void timerFilterDelay_Tick(object sender, EventArgs e)
        {
            timerFilterDelay.Stop();

            ZobrazOrganizacie();
        }

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
                    var exlwkco = pckg.Workbook.Worksheets[0];

                    if (exlwkco.Name == "data")
                    {
                        Dictionary<string, List<string>> dictco = LoadDataFromExcelco(exlwkco);
                    }
                    else
                    {
                        MessageBox.Show("Zly file");
                        return;
                    }
                }
            }
        }

        private Dictionary<string, List<string>> LoadDataFromExcelco(ExcelWorksheet excelWS)
        {
            Dictionary<string, List<string>> ret = new Dictionary<string, List<string>>();


            for (int row = 2; row <= excelWS.Dimension.End.Row; row++)
            {
                List<String> coRiadok = new List<string>();

                String ico = excelWS.Cells[row, 1].Value.ToString();
                //KodSegment
                coRiadok.Add(excelWS.Cells[row, 2].Value.ToString());
                //KodStupen
                coRiadok.Add(excelWS.Cells[row, 3].Value.ToString());
                //KodPodriadenost
                coRiadok.Add(excelWS.Cells[row, 4].Value.ToString());
                //KodObec
                coRiadok.Add(excelWS.Cells[row, 5].Value.ToString());
                //Nazov
                coRiadok.Add(excelWS.Cells[row, 6].Value.ToString());
                //Ulica
                coRiadok.Add(excelWS.Cells[row, 7].Value.ToString());

                ret.Add(ico, coRiadok);
            }

            return ret;
        }
    }
}
