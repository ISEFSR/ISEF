namespace cvti.isef.winformapp.Controls.Main.Ciselniky
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using cvti.data;
    using cvti.data.Tables;
    using cvti.data.Columns;
    using cvti.data.Views;
    using cvti.data.Enums;
    using cvti.data.Conditions;
    using cvti.data.Core;
    using cvti.isef.winformapp.Forms.Classifiers;
    using System.Drawing;
    using System.IO;
    using OfficeOpenXml;
    using System.Collections.Generic;

    //using System.Windows.Controls;

    public partial class OkresControl : UserControl, ICiselnikControl
    {
        public OkresControl()
        {
            InitializeComponent();
        }

        public int SelectedYear { get; private set; }

        public string TitleText { get => "Číselník okresov"; }

        public string InfoText { get => "Číselník okresov. Číselník neni viazaný na kalendárny rok."; }

        public void Deaktivuj()
        {
            comboBoxKraje.Items.Clear();
            listBoxOkresy.Items.Clear();
        }

        ISEFDataManager _manager;

        public event EventHandler<Tuple<Column, object, AvailableConditions>> ConditionAdded;

        public async Task NacitajDataAsync(ISEFDataManager manager, int year)
        {
            _manager = manager;
            SelectedYear = year;

            await Task.Delay(1);
            comboBoxKraje.Items.Clear();
            foreach (var k in _manager.Kraje)
                comboBoxKraje.Items.Add(k);

            ZobrazOkresy();
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
                comboBoxKraje.SelectedIndex = -1;
                comboBoxKraje.Enabled = false;
            }
        }

        private void listBoxOkresy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxOkresy.SelectedIndex == -1)
                return;

            var okres = listBoxOkresy.SelectedItem as OkresRiadok;

            ZobrazOkres(okres);
        }

        private void ZobrazOkres(OkresRiadok okres)
        {
            if (okres is null)
            {
                textBoxKod.Text = string.Empty;

                textBoxNazov.Text = string.Empty;
                textBoxPopis.Text = string.Empty;
            }
            else
            {
                textBoxKod.Text = okres.KodOkres.ToString();

                textBoxNazov.Text = okres.SkratenyNazov;
                textBoxPopis.Text = okres.Nazov;
            }
        }

        private void comboBoxKraje_SelectedIndexChanged(object sender, EventArgs e)
        {
            ZobrazOkresy();
        }

        private void ZobrazOkresy()
        {
            listBoxOkresy.Items.Clear();

            var filter = cueTextBoxFilter.Text;

            if (comboBoxKraje.SelectedIndex == -1)
            {
                foreach (var o in _manager.Okresy)
                {
                    if (!string.IsNullOrWhiteSpace(filter))
                    {
                        if (!o.Nazov.ToLower().Contains(filter) &&
                            !o.KodOkres.ToString().Contains(filter))
                            continue;
                    }

                    listBoxOkresy.Items.Add(o);
                }
            }
            else
            {
                var kraj = comboBoxKraje.SelectedItem as KrajRiadok;
                foreach (var o in _manager.Okresy.Where(o => o.KodKraj == kraj.Kod).OrderBy(o => o.Nazov))
                {
                    if (!string.IsNullOrWhiteSpace(filter))
                    {
                        if (!o.Nazov.ToLower().Contains(filter) &&
                            !o.KodOkres.ToString().Contains(filter))
                            continue;
                    }

                    listBoxOkresy.Items.Add(o);
                }
            }

            if (listBoxOkresy.Items.Count > 0)
                listBoxOkresy.SelectedIndex = 0;
        }

        private async void buttonSaveChanges_Click(object sender, EventArgs e)
        {
            if (listBoxOkresy.SelectedIndex == -1)
                return;

            var okres = listBoxOkresy.SelectedItem as OkresRiadok;

            okres.Nazov = textBoxPopis.Text;
            okres.SkratenyNazov = textBoxNazov.Text;

            if (await _manager.MSSQLManager.OrganizacieManager.UpdateRiadok(okres))
            {
                // TODO checkni ci sa aj tak updatol?
                //listBox1.selec.Text = textBoxNazov.Text;
                //listBoxObce.SelectedItem = obec;
                var index = listBoxOkresy.SelectedIndex;
                listBoxOkresy.Items.RemoveAt(index);
                listBoxOkresy.Items.Insert(index, okres);
                listBoxOkresy.SelectedIndex = index;

                await _manager.LogFrontendMessageSafeAsync($"Okres {okres.KodOkres} úspešne zmenená...", "ObecControl.buttonSaveChanges_Click");
                MessageBox.Show($"Údaje pre okres {okres.KodOkres} úspešne zmenené", "Okres zmenená", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            var okres = listBoxOkresy.SelectedItem as OkresRiadok;

            ZobrazOkres(okres);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var okres = listBoxOkresy.SelectedItem as OkresRiadok;

            var clmn = AssuView.VratStlpec(AssuViewAvailableColumns.OkresKod);

            try
            {
                Enabled = false;

                await CiselnikyUtilities.ShowPreviewForColumns(_manager,
                    CiselnikyUtilities.VratStlpcePrePreview(),
                    SelectedYear, new Equals(string.Empty, clmn, okres.KodOkres),
                    $"Prehľad údajov za okres {okres.KodOkres}, {okres.Nazov}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pri pokuse o zobrazenie údajov pre číselník nastala neočakávaná chyba " + ex.Message,
                    "Neočakávaná chyba " + ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);

                await _manager.LogFrontendErrorSafeAsync(ex, "OkresControl.button1_Click");
            }
            finally
            {
                Enabled = true;
            }
        }

        public async Task ExportData(string path)
        {
            await Task.Delay(1);
            ExcelUtilities.ExportCiselnikToExcel(_manager.Okresy, path);
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
            var title = $"Prehľad údajov za rok {SelectedYear} podľa krajov";
            if (checkBoxKrajFilter.Checked)
            {
                var kraj = comboBoxKraje.SelectedItem as KrajRiadok;
                if (kraj != null)
                {
                    cnd = new Equals(string.Empty,
                        AssuView.VratStlpec(AssuViewAvailableColumns.KrajKod), kraj.Kod);
                    title = $"Prehľad okresov za rok {SelectedYear} kraj {kraj.Kod}";
                }
            }

            await CiselnikyUtilities.ShowPreviewForColumns(_manager,
                new AssuViewColumn[]
                {
                    AssuView.VratStlpec(AssuViewAvailableColumns.OkresKod, true, "Kod"),
                    AssuView.VratStlpec(AssuViewAvailableColumns.OkresShort, true, "Skrateny"),
                    AssuView.VratStlpec(AssuViewAvailableColumns.OkresNazov, true, "Nazov")
                }, SelectedYear, cnd, title);
        }

        private void andToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CiselnikyUtilities.CreateCondition(sender, ConditionAdded);
        }

        public bool CanCreate() => true;

        public async Task CreateItem()
        {
            try
            {
                await Task.Delay(1);
                using (var okresForm = new NewOkresForm(_manager))
                {
                    if (okresForm.ShowDialog() == DialogResult.OK)
                    {
                        var okres = new OkresRiadok()
                        {
                            KodKraj = okresForm.Kraj.Kod,
                            KodOkres = short.Parse(okresForm.Kod),
                            SkratenyNazov = okresForm.ShortText,
                            Nazov = okresForm.LongText,
                        };

                        var novyOkres = await _manager.MSSQLManager.Okresy.PridajRiadok(okres);
                        if (novyOkres is null)
                        {
                            MessageBox.Show("Z neznámych dôvodov sa mi nepodarilo pridať nový okres.", "Okres nepridaný",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show("Nový okres úspešne pridaný a bude evidovaný pod kódom " + novyOkres.KodOkres.ToString(), "Okres pridaný",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            await NacitajDataAsync(_manager, SelectedYear);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pri pokuse a pridanie nového okresu nastala chyba: " + ex.Message, "Okres nepridaný " + ex.GetType().ToString(),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void panelControlButtons_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new System.Drawing.Pen(new SolidBrush(Color.FromArgb(210, 210, 210))),
                new Point(0, 0),
                new Point(panelControlButtons.Width, 0));
        }

        public string GetInfoText() => Properties.Resources.InfoOkres;

        public string GetMoreInfo() => Properties.Resources.MoreInfoOkres;

        private void cueTextBoxFilter_TextChanged(object sender, EventArgs e)
        {
            if (timerFilterDelay.Enabled)
                timerFilterDelay.Stop();

            timerFilterDelay.Start();
        }

        private void timerFilterDelay_Tick(object sender, EventArgs e)
        {
            timerFilterDelay.Stop();

            ZobrazOkresy();
        }

        private OkresRiadok VratVybranyOkres()
            => listBoxOkresy.SelectedItem as OkresRiadok;

        private async void buttonRemove_Click(object sender, EventArgs e)
        {
            if (VratVybranyOkres() is null)
                return;

            if (MessageBox.Show($"Skutočne si prajete odstrániť položku {VratVybranyOkres().KodOkres}:{VratVybranyOkres().SkratenyNazov}:{VratVybranyOkres().Nazov} z číselníka okresov? Položku pôjde odstrániť iba v prípade ak na kraj niesú naviazané žiadne obce.", "Odstránenie položky",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (await _manager.MSSQLManager.Okresy.OdstranRiadok(VratVybranyOkres()))
                    {
                        MessageBox.Show("Položka z číselníka okresov úspešne odstránená.", "Položka odstránená", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await NacitajDataAsync(_manager, SelectedYear);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Pri pokuse o odstránenie okresu nastala neočakávaná chyba. " + ex.Message, "Okres neodstránený " + ex.GetType().ToString(),
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
                //KodKraj
                coRiadok.Add(excelWS.Cells[row, 2].Value.ToString());
                //Nazov
                coRiadok.Add(excelWS.Cells[row, 3].Value.ToString());
                //SkratenyNazov
                coRiadok.Add(excelWS.Cells[row, 4].Value.ToString());



                ret.Add(Kod, coRiadok);
            }

            return ret;
        }
    }
}
