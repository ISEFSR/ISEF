namespace cvti.isef.winformapp.Controls.Main.Ciselniky
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using cvti.data;
    using cvti.data.Conditions;
    using cvti.data.Views;
    using cvti.data.Enums;
    using cvti.data.Tables;
    using cvti.isef.winformapp.Forms.Classifiers;
    using cvti.data.Columns;
    using System.IO;
    using OfficeOpenXml;

    public partial class FunkcnaKlasifikaciaControl : UserControl, ICiselnikControl
    {
        private ISEFDataManager _manager;

        public event EventHandler<Tuple<Column, object, AvailableConditions>> ConditionAdded;

        public FunkcnaKlasifikaciaControl()
        {
            InitializeComponent();
        }

        public int SelectedYear { get; private set; }

        public string TitleText { get => "Funkčná klasifikácia"; }

        public string InfoText { get => "Číselník funkčnej klasifikácie pre vybraný rok"; }

        private List<FunkcnaRiadok2> fk2;
        private List<FunkcnaRiadok3> fk3;
        private List<FunkcnaRiadok4> fk4;
        private List<FunkcnaRiadok5> fk5;


        public async Task NacitajDataAsync(ISEFDataManager manager, int year)
        {
            _manager = manager;
            SelectedYear = year;

            Enabled = false;
            try
            {
                fk2 = new List<FunkcnaRiadok2>(await _manager.MSSQLManager.CiselnikyManager.VratCiselnikPreRokAsync<FunkcnaRiadok2>(year));
                fk3 = new List<FunkcnaRiadok3>(await _manager.MSSQLManager.CiselnikyManager.VratCiselnikPreRokAsync<FunkcnaRiadok3>(year));
                fk4 = new List<FunkcnaRiadok4>(await _manager.MSSQLManager.CiselnikyManager.VratCiselnikPreRokAsync<FunkcnaRiadok4>(year));
                fk5 = new List<FunkcnaRiadok5>(await _manager.MSSQLManager.CiselnikyManager.VratCiselnikPreRokAsync<FunkcnaRiadok5>(year));

                textBoxRok.Text = textBoxPopis.Text = textBoxNazov.Text = textBoxKod.Text = string.Empty;

                treeViewFunkcna.Nodes.Clear();

                foreach (var f2 in fk2)
                {
                    var n2 = new TreeNode(f2.ToString()) { Tag = f2 };
                    treeViewFunkcna.Nodes.Add(n2);

                    foreach (var f3 in fk3.Where(f => f.Fk2 == f2.Kod))
                    {
                        var n3 = new TreeNode(f3.ToString()) { Tag = f3 };
                        n2.Nodes.Add(n3);

                        foreach (var f4 in fk4.Where(f => f.Fk3 == f3.Kod))
                        {
                            var n4 = new TreeNode(f4.ToString()) { Tag = f4 };
                            n3.Nodes.Add(n4);

                            foreach (var f5 in fk5.Where(f => f.Fk4 == f4.Kod))
                            {
                                var n5 = new TreeNode(f5.ToString()) { Tag = f5 };
                                n4.Nodes.Add(n5);
                            }
                        }
                    }
                }

                treeViewFunkcna.ExpandAll();

                if (treeViewFunkcna.SelectedNode is null && treeViewFunkcna.Nodes.Count != 0)
                    treeViewFunkcna.SelectedNode = treeViewFunkcna.Nodes[0];
            }
            catch
            {
                throw;
            }
            finally
            {
                Enabled = true;
            }
        }

        private void treeViewFunkcna_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ZobrazFunkcnu(e.Node.Tag as FunkcnaRiadok);
        }

        private void ZobrazFunkcnu(FunkcnaRiadok fk)
        {
            if (fk is null)
            {
                textBoxRok.Text = textBoxPopis.Text = textBoxNazov.Text = textBoxKod.Text = string.Empty;
            }
            else
            {
                textBoxRok.Text = fk.Rok.ToString();
                textBoxKod.Text = fk.Kod;
                textBoxNazov.Text = fk.Nazov;
                textBoxPopis.Text = fk.Popis;

                var nazovTag = 30;
                var kodTag = 29;
                if (fk is FunkcnaRiadok3)
                {
                    nazovTag = 32;
                    kodTag = 31;
                }
                else if (fk is FunkcnaRiadok4)
                {
                    nazovTag = 34;
                    kodTag = 33;
                }
                else if (fk is FunkcnaRiadok5)
                {
                    nazovTag = 36;
                    kodTag = 35;
                }
                textBoxNazov.Tag = nazovTag;
                textBoxKod.Tag = kodTag;

                treeViewFunkcna.Tag = $"{kodTag};{fk.Kod}";
            }
        }

        public void Deaktivuj()
        {
            treeViewFunkcna.Nodes.Clear();
        }

        public async Task ExportData(string path)
        {
            var fk2 = await _manager.MSSQLManager.CiselnikyManager.VratCiselnikPreRokAsync<FunkcnaRiadok2>(SelectedYear);
            var fk3 = await _manager.MSSQLManager.CiselnikyManager.VratCiselnikPreRokAsync<FunkcnaRiadok3>(SelectedYear);
            var fk4 = await _manager.MSSQLManager.CiselnikyManager.VratCiselnikPreRokAsync<FunkcnaRiadok4>(SelectedYear);
            var fk5 = await _manager.MSSQLManager.CiselnikyManager.VratCiselnikPreRokAsync<FunkcnaRiadok5>(SelectedYear);

            ExcelUtilities.ExportCiselnikToExcel(new Dictionary<string, IEnumerable<CiselnikRiadok>>()
            {
                { "fk2", fk2 },
                { "fk3", fk3 },
                { "fk4", fk4 },
                { "fk5", fk5 },
            }, path);
        }

        public bool CanGenerate() => true;

        public bool CanUpdate() => true;

        public async Task GenerateData()
        {
            if (treeViewFunkcna.Nodes.Count == 0)
            {
                if (await CiselnikyUtilities.GenerujCiselnik(
                    data.Enums.AnalytickaEvidencia.FunkcnaKlasifikacia, _manager, SelectedYear))
                {
                    MessageBox.Show($"Číselník funkčnej klasifikácie pre rok {SelectedYear} úspešne vygenerovaný.", "Číselník vygenerovaný",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    await _manager.LogFrontendMessageSafeAsync($"Číselník funkčnej klasifikácie pre rok {SelectedYear} úspešne vygenerovaný...",
                        "FunkcnaKlasifikaciaControl.GenerateData");

                    await ReloadData();
                }
            }
            else
            {
                MessageBox.Show($"Nemožem generovať dáta pre číselník funkčnej klasifikácie. Keďže kalendárny rok {SelectedYear} už obsahuje dáta", "Nemožem generovať dáta",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public async Task UpdateData()
        {
            if (treeViewFunkcna.Nodes.Count != 0)
            {
                if (await CiselnikyUtilities.AktualizujCiselnik(
                    data.Enums.AnalytickaEvidencia.FunkcnaKlasifikacia, _manager, SelectedYear))
                {
                    MessageBox.Show($"Číselník funkčnej klasifikácie pre rok {SelectedYear} úspešne aktualizovaný.", "Číselník aktualizovaný",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                    await _manager.LogFrontendMessageSafeAsync($"Číselník funkčnej klasifikácie pre rok {SelectedYear} úspešne aktualizovaný...",
                        "FunkcnaKlasifikaciaControl.UpdateData");

                    await ReloadData();
                }
            }
            else
            {
                MessageBox.Show($"Nemôžem aktualizovať údaje pre funkčnú klasifikácie kedže pre rok {SelectedYear} ešte niesú nahrané údaje.", "Nenahrané dáta",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public bool CanRemove() => true;

        public async Task RemoveData()
        {
            var dataYears = await _manager.MSSQLManager.GetAvailableYearsAsync();
            if (dataYears.Contains(SelectedYear))
            {
                MessageBox.Show("Pre vybraný kalendárny rok sú už nahrané dáta a tým aj číselník funkčnej klasifikácie.",
                    "Dáta existujú", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (MessageBox.Show($"Skutočne si prajete odstrániť číselník funkčnej kalsifkácie pre rok {SelectedYear}?",
                    "Odstránenie číselníku", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    MessageBox.Show("Táto možnosť momentálne nieje dostupná...", "Nedostupná možnosť!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await ReloadData();
                }
            }
        }

        public async Task ReloadData() => await NacitajDataAsync(_manager, SelectedYear);

        public async Task ShowPreview()
        {
            using (var specify = new SpecifyFunkcnaForm())
            {
                specify.ShowImageIcon = false;
                if (specify.ShowDialog() == DialogResult.OK)
                {
                    await CiselnikyUtilities.ShowPreviewForColumns(_manager,
                        specify.GetSelectedColumns(), SelectedYear, null, specify.GetSelectedTitle());
                }
            }
        }

        private async void buttonSaveChanges_Click(object sender, EventArgs e)
        {
            if (treeViewFunkcna.SelectedNode == null)
                return;

            var fk = treeViewFunkcna.SelectedNode.Tag as FunkcnaRiadok;

            if (fk is null)
                return;

            fk.Nazov = textBoxNazov.Text;
            fk.Popis = textBoxPopis.Text;

            if (await _manager.MSSQLManager.CiselnikyManager.UpdateCiselnikRiadokAsync(fk))
            {
                treeViewFunkcna.SelectedNode.Text = fk.ToString();// textBoxNazov.Text;
                await _manager.LogFrontendMessageSafeAsync($"Textové pole pre položku funkčnej klasifikácie {fk.Kod} úspešne zmenené.",
                    "FunkcnaKlasifikaciaControl.buttonSaveChanges_Click");
                MessageBox.Show($"Textové pole pre položku funkčnej klasifikácie {fk.Kod} úspešne zmenené.",
                    "Zmena úspešná", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonDiscardChanges_Click(object sender, EventArgs e)
        {
            var fk = treeViewFunkcna.SelectedNode.Tag as FunkcnaRiadok;

            ZobrazFunkcnu(fk);
        }

        private async void buttonShowPreview_Click(object sender, EventArgs e)
        {
            if (treeViewFunkcna.SelectedNode is null)
            {
                MessageBox.Show("Pre zobrazenie prehľadu musíte najprv vybrať položku číselníku", "Nevybraná položka",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var fk = treeViewFunkcna.SelectedNode.Tag as FunkcnaRiadok;
            if (fk is null)
                return;

            Column clmn = null;
            if (fk is FunkcnaRiadok2)
            {
                clmn = AssuView.VratStlpec(AssuViewAvailableColumns.FKod2);
            }
            else if (fk is FunkcnaRiadok3)
            {
                clmn = AssuView.VratStlpec(AssuViewAvailableColumns.FKod3);
            }
            else if (fk is FunkcnaRiadok4)
            {
                clmn = AssuView.VratStlpec(AssuViewAvailableColumns.FKod4);
            }
            else if (fk is FunkcnaRiadok5)
            {
                clmn = AssuView.VratStlpec(AssuViewAvailableColumns.FKod5);
            }

            var cnd = new Equals(string.Empty, clmn, fk.Kod);

            await CiselnikyUtilities.ShowPreviewForColumns(_manager,
               CiselnikyUtilities.VratStlpcePrePreview(), SelectedYear, cnd, "Prehľad pre funkčnú položku " + fk.Kod + " " + fk.Nazov);
        }

        private void treeViewFunkcna_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                treeViewFunkcna.SelectedNode = e.Node;
        }

        private void andToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeViewFunkcna.SelectedNode is null)
                return;

            CiselnikyUtilities.CreateCondition(sender, ConditionAdded);
        }

        public bool CanCreate() => false;

        public Task CreateItem()
        {
            throw new NotImplementedException();
        }

        private void panelControlButtons_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new System.Drawing.Pen(new SolidBrush(Color.FromArgb(210, 210, 210))),
    new Point(0, 0),
    new Point(panelControlButtons.Width, 0));
        }

        public string GetInfoText() => Properties.Resources.InfoFunkcna;

        public string GetMoreInfo() => Properties.Resources.MoreInfoFunkcna;

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
                    var exlwkFk2 = pckg.Workbook.Worksheets[0];
                    var exlwkFk3 = pckg.Workbook.Worksheets[1];
                    var exlwkFk4 = pckg.Workbook.Worksheets[2];
                    var exlwkFk5 = pckg.Workbook.Worksheets[3];

                    if (exlwkFk2.Name == "fk2")
                    {
                        Dictionary<string, List<string>> dictFk2 = LoadDataFromExcelFk(exlwkFk2, 2, 1, 3, 4);
                        Dictionary<string, List<string>> dictFk3 = LoadDataFromExcelFk(exlwkFk3, 3, 2, 4, 5);
                        Dictionary<string, List<string>> dictFk4 = LoadDataFromExcelFk(exlwkFk4, 3, 2, 4, 5);
                        Dictionary<string, List<string>> dictFk5 = LoadDataFromExcelFk(exlwkFk5, 3, 2, 4, 5);

                    }
                    else
                    {
                        MessageBox.Show("Zly file");
                        return;
                    }
                }
            }
        }

        private Dictionary<string, List<string>> LoadDataFromExcelFk(ExcelWorksheet excelWS, int sKod, int sRok, int sNazov, int sPopis)
        {
            Dictionary<string, List<string>> ret = new Dictionary<string, List<string>>();


            for (int row = 2; row <= excelWS.Dimension.End.Row; row++)
            {
                List<String> fkRiadok = new List<string>();

                String kod = excelWS.Cells[row, sKod].Value.ToString();
                fkRiadok.Add(excelWS.Cells[row, sRok].Value.ToString());
                fkRiadok.Add(excelWS.Cells[row, sNazov].Value.ToString());
                fkRiadok.Add(excelWS.Cells[row, sPopis].Value.ToString());

                ret.Add(kod, fkRiadok);
            }

            return ret;
        }
    }
}
