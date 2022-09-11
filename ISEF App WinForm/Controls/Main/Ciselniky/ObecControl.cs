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
    using cvti.data.Columns;
    using cvti.data.Views;
    using cvti.data.Enums;
    using cvti.data.Conditions;
    using cvti.data.Core;
    using cvti.isef.winformapp.Forms.Classifiers;
    using System.Drawing;
    using OfficeOpenXml;
    using System.Collections.Generic;

    public partial class ObecControl : UserControl, ICiselnikControl
    {
        public ObecControl()
        {
            InitializeComponent();
        }

        public int SelectedYear { get; private set; }

        public string TitleText { get => "Číselník obcí"; }

        public string InfoText { get => "Číselník obcí. Číselník neni viazaný na kalendárny rok."; }

        public void Deaktivuj()
        {
            comboBoxKraje.Items.Clear();
            listBoxObce.Items.Clear();
        }

        private ISEFDataManager _manager;

        public event EventHandler<Tuple<Column, object, AvailableConditions>> ConditionAdded;

        public async Task NacitajDataAsync(ISEFDataManager manager, int year)
        {
            _manager = manager;
            SelectedYear = year;

            await Task.Delay(1);

            comboBoxKraje.Items.Clear();
            foreach (var k in _manager.Kraje.OrderBy(o => o.Nazov))
                comboBoxKraje.Items.Add(k);

            ZobrazObce();
        }

        private void listBoxObce_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxObce.SelectedIndex == -1)
                return;

            var obec = listBoxObce.SelectedItem as ObecRiadok;

            if (obec is null)
            {
                textBoxKod.Text = string.Empty;
                textBoxNazov.Text = string.Empty;
                textBoxPopis.Text = string.Empty;
            }
            else
            {
                textBoxKod.Text = obec.Kod.ToString();
                textBoxNazov.Text = obec.Skrateny ?? string.Empty;
                textBoxPopis.Text = obec.Nazov;
            }
        }

        private void comboBoxKraje_SelectedIndexChanged(object sender, EventArgs e)
        {
            ZobrazObce();
        }

        private void checkBoxKrajFilter_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxKrajFilter.Checked)
            {
                comboBoxKraje.Enabled = true;
                comboBoxKraje.SelectedIndex = 0;
            }
            else
            {
                comboBoxKraje.Enabled = false;
                comboBoxKraje.SelectedIndex = -1;
            }
        }

        private void ZobrazObce()
        {
            listBoxObce.Items.Clear();
            var filter = cueTextBoxFilter.Text.ToLower().Trim();

            if (comboBoxKraje.SelectedIndex == -1)
            {
                foreach (var o in _manager.Obce.OrderBy(o => o.Nazov))
                {
                    if (!string.IsNullOrWhiteSpace(filter))
                    {
                        if (!o.Nazov.ToLower().Contains(filter) &&
                            !o.Kod.ToString().Contains(filter))
                            continue;
                    }
                    listBoxObce.Items.Add(o);
                }
            }
            else
            {
                var kraj = comboBoxKraje.SelectedItem as KrajRiadok;
                var okresy = (from ok in _manager.Okresy where ok.KodKraj == kraj.Kod select ok.KodOkres);
                foreach (var o in from ob in _manager.Obce where okresy.Contains(ob.KodOkres) orderby ob.Nazov select ob)
                {
                    if (!string.IsNullOrWhiteSpace(filter))
                    {
                        if (!o.Nazov.ToLower().Contains(filter) &&
                            !o.Skrateny.ToLower().Contains(filter) &&
                            !o.Kod.ToString().Contains(filter))
                            continue;
                    }
                    listBoxObce.Items.Add(o);
                }
            }
        }

        public async Task ExportData(string path)
        {
            await Task.Delay(1);
            ExcelUtilities.ExportCiselnikToExcel(_manager.Obce, path);
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

        private async void buttonSaveChanges_Click(object sender, EventArgs e)
        {
            if (listBoxObce.SelectedIndex == -1)
                return;

            var obec = listBoxObce.SelectedItem as ObecRiadok;

            obec.Nazov = textBoxPopis.Text;
            obec.Skrateny = textBoxNazov.Text;

            if (await _manager.MSSQLManager.OrganizacieManager.UpdateRiadok(obec))
            {
                // TODO checkni ci sa aj tak updatol?
                //listBox1.selec.Text = textBoxNazov.Text;
                //listBoxObce.SelectedItem = obec;
                var index = listBoxObce.SelectedIndex;
                listBoxObce.Items.RemoveAt(index);
                listBoxObce.Items.Insert(index, obec);
                listBoxObce.SelectedIndex = index;

                await _manager.LogFrontendMessageSafeAsync($"Obec {obec.Kod} úspešne zmenená...", "ObecControl.buttonSaveChanges_Click");
                MessageBox.Show($"Údaje pre obec {obec.Kod} úspešne zmenené", "Obec zmenená", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void buttonReload_Click(object sender, EventArgs e)
        {
            await NacitajDataAsync(_manager, SelectedYear);
        }
        
        private async void button1_Click(object sender, EventArgs e)
        {
            var selected = listBoxObce.SelectedItem as ObecRiadok;
            if (selected is null)
                return;

            var condition = new Equals($"Obec [{selected.Kod}] {selected.Nazov}",
                AssuView.VratStlpec(AssuViewAvailableColumns.ObecKod), selected.Kod);

            await CiselnikyUtilities.ShowPreviewForColumns(_manager,
                new AssuViewColumn[]
                {
                    AssuView.VratStlpec(AssuViewAvailableColumns.ObecKod, true, "Kod"),
                    AssuView.VratStlpec(AssuViewAvailableColumns.ObecNazov, true, "Nazov")
                }, SelectedYear, condition);
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
            var title = $"Prehľad obcí za rok {SelectedYear}";

            if (checkBoxKrajFilter.Checked)
            {
                var kraj = comboBoxKraje.SelectedItem as KrajRiadok;
                if (kraj != null)
                {
                    cnd = new Equals(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.KrajKod), kraj.Kod);
                    title = $"Prehľad obcí za rok {SelectedYear} a kraj {kraj.Nazov}";
                }
            }

            try
            {
                Enabled = false;

                await CiselnikyUtilities.ShowPreviewForColumns(_manager,
                    CiselnikyUtilities.VratStlpcePrePreview(),
                    SelectedYear, cnd, title);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pri pokuse o zobrazenie údajov pre číselník nastala neočakávaná chyba " + ex.Message,
                    "Neočakávaná chyba " + ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);

                await _manager.LogFrontendErrorSafeAsync(ex, "ObecControl.button1_Click");
            }
            finally
            {
                Enabled = true;
            }
        }

        private void andToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBoxObce.SelectedIndex == -1)
                return;

            CiselnikyUtilities.CreateCondition(sender, ConditionAdded);
        }

        public bool CanCreate() => true;

        public async Task CreateItem()
        {
            try
            {
                await Task.Delay(1);
                using (var obecForm = new NewObecForm(_manager))
                {
                    if (obecForm.ShowDialog() == DialogResult.OK)
                    {
                        var obec = new ObecRiadok()
                        {
                            Kod = int.Parse(obecForm.Kod),
                            KodOkres = obecForm.Okres.KodOkres,
                            Skrateny = obecForm.ShortText,
                            Nazov = obecForm.LongText,
                        };

                        var novyOkres = await _manager.MSSQLManager.Obce.PridajRiadok(obec);
                        if (novyOkres is null)
                        {
                            MessageBox.Show("Z neznámych dôvodov sa mi nepodarilo pridať novú obec.", "Obec nepridaná",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show("Nová obec úspešne pridaná a bude evidovaná pod kódom " + novyOkres.KodOkres.ToString(), "Obec pridaná",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            await NacitajDataAsync(_manager, SelectedYear);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pri pokuse a pridanie novej obce nastala chyba: " + ex.Message, "Obec nepridaná " + ex.GetType().ToString(),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void textBoxFilter_TextChanged(object sender, EventArgs e)
        {
            if (timerFilterDelay.Enabled)
                timerFilterDelay.Stop();

            timerFilterDelay.Start();
        }

        private void timerFilterDelay_Tick(object sender, EventArgs e)
        {
            timerFilterDelay.Stop();

            ZobrazObce();
        }

        private void panelControlButtons_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new System.Drawing.Pen(new SolidBrush(Color.FromArgb(210, 210, 210))),
    new Point(0, 0),
    new Point(panelControlButtons.Width, 0));
        }

        public string GetInfoText() => Properties.Resources.InfoKraj;

        public string GetMoreInfo() => Properties.Resources.MoreInfoKraj;

        private ObecRiadok VratVybranuObec()
            => listBoxObce.SelectedItem as ObecRiadok;

        private async void buttonRemove_Click(object sender, EventArgs e)
        {
            if (VratVybranuObec() is null)
                return;

            if (MessageBox.Show($"Skutočne si prajete odstrániť položku {VratVybranuObec().KodOkres}:{VratVybranuObec().Skrateny}:{VratVybranuObec().Nazov} z číselníka obcí? Položku pôjde odstrániť iba v prípade ak na obec niesú naviazané žiadne organizácie.", "Odstránenie položky",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (await _manager.MSSQLManager.Obce.OdstranRiadok(VratVybranuObec()))
                    {
                        MessageBox.Show("Položka z číselníka obcí úspešne odstránená.", "Položka odstránená", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await NacitajDataAsync(_manager, SelectedYear);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Pri pokuse o odstránenie obce nastala neočakávaná chyba. " + ex.Message, "Okres neodstránený " + ex.GetType().ToString(),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void buttonNew_Click(object sender, EventArgs e)
            => await CreateItem();

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

                String Kod = excelWS.Cells[row, 1].Value.ToString();
                //KodOkres
                coRiadok.Add(excelWS.Cells[row, 2].Value.ToString());
                //Nazov
                coRiadok.Add(excelWS.Cells[row, 3].Value.ToString());




                ret.Add(Kod, coRiadok);
            }

            return ret;
        }
    }
}
