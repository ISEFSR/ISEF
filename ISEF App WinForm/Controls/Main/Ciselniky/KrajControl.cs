namespace cvti.isef.winformapp.Controls.Main.Ciselniky
{
    using System;
    using System.Drawing;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using cvti.data;
    using cvti.data.Core;
    using cvti.data.Columns;
    using cvti.data.Conditions;
    using cvti.data.Views;
    using cvti.data.Enums;
    using cvti.data.Tables;
    using cvti.isef.winformapp.Forms.Classifiers;
    using System.IO;
    using System.Collections.Generic;
    using OfficeOpenXml;

    public partial class KrajControl : UserControl, ICiselnikControl
    {
        private ISEFDataManager _manager;

        public event EventHandler<Tuple<Column, object, AvailableConditions>> ConditionAdded;

        public KrajControl()
        {
            InitializeComponent();
        }

        public int SelectedYear { get; private set; }

        public string TitleText { get => "Číselník krajov"; }

        public string InfoText { get => "Číselník krajov. Číselník neni viazaný na kalendárny rok."; }

        public void Deaktivuj()
        {
            listBox1.Items.Clear();
        }

        public async Task NacitajDataAsync(ISEFDataManager manager, int year)
        {
            _manager = manager;
            SelectedYear = year;

            await Task.Delay(1);

            listBox1.Items.Clear();
            foreach (var k in _manager.Kraje)
                listBox1.Items.Add(k);

            if (listBox1.Items.Count > 0 && listBox1.SelectedIndex == -1)
                listBox1.SelectedIndex = 0;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                return;

            ZobrazVybranyKraj();
        }

        private void ZobrazVybranyKraj()
        {
            var kraj = listBox1.SelectedItem as KrajRiadok;
            if (kraj is null)
            {
                textBoxKod.Text = string.Empty;
                textBoxNazov.Text = string.Empty;
                textBoxPopis.Text = string.Empty;
            }
            else
            {
                textBoxKod.Text = kraj.Kod.ToString();
                textBoxNazov.Text = kraj.SkratenyNazov;
                textBoxPopis.Text = kraj.Nazov;
            }
        }

        private async void buttonSaveChanges_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                return;

            var kraj = listBox1.SelectedItem as KrajRiadok;

            kraj.Nazov = textBoxPopis.Text;
            kraj.SkratenyNazov = textBoxNazov.Text;

            if (await _manager.MSSQLManager.OrganizacieManager.UpdateRiadok(kraj))
            {
                var index = listBox1.SelectedIndex;
                listBox1.Items.RemoveAt(index);
                listBox1.Items.Insert(index, kraj);
                listBox1.SelectedIndex = index;

                await _manager.LogFrontendMessageSafeAsync(
                    $"Editovateľne polia pre Kraj {kraj.Kod} úspešne zmenené...",
                    "KrajControl.buttonSaveChanges_Click");
                MessageBox.Show($"Editovateľne polia pre Kraj {kraj.Kod} úspešne zmenené...", "Kraj zmenený",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void buttonReload_Click(object sender, EventArgs e)
        {
            await NacitajDataAsync(_manager, SelectedYear);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var kraj = listBox1.SelectedItem as KrajRiadok;

            Column clmn = AssuView.VratStlpec(AssuViewAvailableColumns.KrajKod);

            try
            {
                Enabled = false;

                await CiselnikyUtilities.ShowPreviewForColumns(_manager, 
                    CiselnikyUtilities.VratStlpcePrePreview(),
                    SelectedYear, new Equals(string.Empty, clmn, kraj.Kod),
                    $"Prehľad údajov za kraj {kraj.Kod}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pri pokuse o zobrazenie údajov pre číselník a rok nastala neočakávaná chyba " + ex.Message,
                    "Neočakávaná chyba " + ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);

                await _manager.LogFrontendErrorSafeAsync(ex, "KrajControl.button1_Click");
            }
            finally
            {
                Enabled = true;
            }
        }

        public async Task ExportData(string path)
        {
            await Task.Delay(1);
            ExcelUtilities.ExportCiselnikToExcel(_manager.Kraje, path);
        }

        public bool CanGenerate() => false;

        public Task GenerateData()
        {
            throw new NotImplementedException();
        }

        public bool CanUpdate() => false;

        public Task UpdateData()
        {
            throw new NotImplementedException();
        }

        public bool CanRemove() => false;

        public Task RemoveData()
        {
            throw new NotImplementedException();
        }

        public async Task ReloadData()
        {
            await NacitajDataAsync(_manager, SelectedYear);
        }

        public async Task ShowPreview()
        {
            await CiselnikyUtilities.ShowPreviewForColumns(_manager,
                new AssuViewColumn[]
                {
                    AssuView.VratStlpec(AssuViewAvailableColumns.KrajKod, true, "Kod"),
                    AssuView.VratStlpec(AssuViewAvailableColumns.KrajShort, true, "Skrateny"),
                    AssuView.VratStlpec(AssuViewAvailableColumns.KrajNazov, true, "Nazov")

                }, SelectedYear, null, $"Prehľad údajov za kraje a rok {SelectedYear}");
        }

        private void andToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                return;

            CiselnikyUtilities.CreateCondition(sender, ConditionAdded);
        }

        public bool CanCreate() => true;

        public async Task CreateItem()
        {
            try
            {
                await Task.Delay(1);
                using (var krajForm = new NewKrajForm(_manager))
                {
                    if (krajForm.ShowDialog() == DialogResult.OK)
                    {
                        var kraj = new KrajRiadok()
                        {
                            Kod = short.Parse(krajForm.Kod),
                            SkratenyNazov = krajForm.ShortText,
                            Nazov = krajForm.LongText,
                        };

                        var novyKraj = await _manager.MSSQLManager.Kraje.PridajKraj(kraj);
                        if (novyKraj is null)
                        {
                            MessageBox.Show("Z neznámych dôvodov sa mi nepodarilo pridať nový kraj.", "Kraj nepridaný", 
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);   
                        }
                        else
                        {
                            MessageBox.Show("Nový kraj úspešne pridaný a bude evidovaný pod kódom " + novyKraj.Kod.ToString(), "Kraj pridaný",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            await NacitajDataAsync(_manager, SelectedYear);
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Pri pokuse a pridanie nového kraja nastala chyba: " + ex.Message, "Kraj nepridaný "+ ex.GetType().ToString(),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void buttonNew_Click(object sender, EventArgs e)
            => await CreateItem();

        private void panelControlButtons_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new System.Drawing.Pen(new SolidBrush(Color.FromArgb(210, 210, 210))),
                new Point(0, 0),
                new Point(panelControlButtons.Width, 0));
        }

        public string GetInfoText() => Properties.Resources.InfoKraj;

        public string GetMoreInfo() => Properties.Resources.MoreInfoKraj;

        private async void buttonRemove_Click(object sender, EventArgs e)
        {
            if (VratVybranyKraj() is null)
                return;

            if (MessageBox.Show($"Skutočne si prajete odstrániť položku {VratVybranyKraj().Kod}:{VratVybranyKraj().SkratenyNazov}:{VratVybranyKraj().Nazov} z číselníka krajov? Položku pôjde odstrániť iba v prípade ak na kraj niesú naviazané žiadne okresy.", "Odstránenie položky",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (await _manager.MSSQLManager.Kraje.RemoveKraj(VratVybranyKraj()))
                    {
                        MessageBox.Show("Položka z číselníka krajov úspešne odstránená.", "Položka odstránená", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await NacitajDataAsync(_manager, SelectedYear);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Pri pokuse o odstránenie kraju nastala neočakávaná chyba. " + ex.Message, "Kraj neodstránený " + ex.GetType().ToString(),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private KrajRiadok VratVybranyKraj()
            => listBox1.SelectedItem as KrajRiadok;

        async Task ICiselnikControl.Import()
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
                    var exlwkck = pckg.Workbook.Worksheets[0];

                    if (exlwkck.Name == "data")
                    {
                        Dictionary<string, List<string>> dictck = LoadDataFromExcelck(exlwkck);
                    }
                    else
                    {
                        MessageBox.Show("Zly file");
                        return;
                    }
                }
            }
        }

        private Dictionary<string, List<string>> LoadDataFromExcelck(ExcelWorksheet excelWS)
        {
            Dictionary<string, List<string>> ret = new Dictionary<string, List<string>>();


            for (int row = 2; row <= excelWS.Dimension.End.Row; row++)
            {
                List<String> ckRiadok = new List<string>();

                String Kod = excelWS.Cells[row, 1].Value.ToString();
                //SkratenzNazov
                ckRiadok.Add(excelWS.Cells[row, 2].Value.ToString());
                //Nazov
                ckRiadok.Add(excelWS.Cells[row, 3].Value.ToString());

                ret.Add(Kod, ckRiadok);
            }

            return ret;
        }
    }
}
