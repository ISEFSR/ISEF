namespace cvti.isef.winformapp.Controls.Main.Ciselniky
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using cvti.data;
    using cvti.data.Columns;
    using cvti.data.Conditions;
    using cvti.data.Views;
    using cvti.data.Enums;
    using cvti.data.Tables;
    using cvti.isef.winformapp.Forms.Classifiers;
    using OfficeOpenXml;

    public partial class ProgramovaKlasifikacia : UserControl, ICiselnikControl
    {
        private ISEFDataManager _manager;

        public event EventHandler<Tuple<Column, object, AvailableConditions>> ConditionAdded;

        public ProgramovaKlasifikacia()
        {
            InitializeComponent();
        }

        public int SelectedYear { get; private set; }

        public string TitleText { get => "Číselník programovej klasifikácie"; }

        public string InfoText { get => "Číselník programovej klasifikácie pre vybraný rok."; }

        private List<ProgramRiadok3> pk3;
        private List<ProgramRiadok5> pk5;
        private List<ProgramRiadok7> pk7;

        public async Task NacitajDataAsync(ISEFDataManager manager, int year)
        {
            _manager = manager;
            SelectedYear = year;

            Enabled = false;

            try
            {
                pk3 = new List<ProgramRiadok3>(await _manager.MSSQLManager.CiselnikyManager.VratCiselnikPreRokAsync<ProgramRiadok3>(year));
                pk5 = new List<ProgramRiadok5>(await _manager.MSSQLManager.CiselnikyManager.VratCiselnikPreRokAsync<ProgramRiadok5>(year));
                pk7 = new List<ProgramRiadok7>(await _manager.MSSQLManager.CiselnikyManager.VratCiselnikPreRokAsync<ProgramRiadok7>(year));

                treeViewFunkcna.Nodes.Clear();

                foreach (var p3 in pk3)
                {
                    var n1 = new TreeNode(p3.ToString()) { Tag = p3 };
                    treeViewFunkcna.Nodes.Add(n1);

                    foreach (var p5 in pk5.Where(p => p.Pk3 == p3.Kod))
                    {
                        var n2 = new TreeNode(p5.ToString()) { Tag = p5 };
                        n1.Nodes.Add(n2);

                        foreach (var p7 in pk7.Where(p => p.Pk5 == p5.Kod))
                        {
                            var n3 = new TreeNode(p7.ToString()) { Tag = p7 };
                            n2.Nodes.Add(n3);
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

        private async void button1_Click(object sender, EventArgs e)
        {

            if (treeViewFunkcna.SelectedNode == null) return;

            var pk = (ProgramRiadok)treeViewFunkcna.SelectedNode.Tag;

            if (pk is null)
                return;

            pk.Nazov = textBoxNazov.Text;
            pk.Popis = textBoxPopis.Text;

            if (await _manager.MSSQLManager.CiselnikyManager.UpdateCiselnikRiadokAsync(pk))
            {
                treeViewFunkcna.SelectedNode.Text = pk.ToString();

                await _manager.LogFrontendMessageSafeAsync($"Textové pole pre položku programovej klasifikácie {pk.Kod} úspešne zmenené.",
                    "ProgramovaKlasifikacia.buttonSaveChanges_Click");
                MessageBox.Show($"Textové pole pre položku programovej klasifikácie {pk.Kod} úspešne zmenené.",
                    "Zmena úspešná", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (treeViewFunkcna.SelectedNode is null)
                return;

            ZobrazProgram(treeViewFunkcna.SelectedNode.Tag as ProgramRiadok);
        }

        private void treeViewFunkcna_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node is null)
                return;

            ZobrazProgram(e.Node.Tag as ProgramRiadok);
        }

        private void ZobrazProgram(ProgramRiadok pk)
        {
            if (pk is null)
            {
                textBoxKod.Text =
                    textBoxRok.Text =
                    textBoxNazov.Text =
                    textBoxPopis.Text = string.Empty;
            }
            else
            {
                textBoxRok.Text = pk.Rok.ToString();
                textBoxKod.Text = pk.Kod;
                textBoxNazov.Text = pk.Nazov;
                textBoxPopis.Text = pk.Popis;

                var nazovTag = 46;
                var kodTag = 45;

                if (pk is ProgramRiadok5)
                {
                    nazovTag = 48;
                    kodTag = 47;
                }
                else if (pk is ProgramRiadok7)
                {
                    nazovTag = 50;
                    kodTag = 49;
                }

                textBoxNazov.Tag = nazovTag;
                textBoxKod.Tag = kodTag;

                treeViewFunkcna.Tag = $"{kodTag};{pk.Kod}";
            }
        }

        public void Deaktivuj()
        {
            treeViewFunkcna.Nodes.Clear();
        }

        public async Task ExportData(string path)
        {
            var pk3 = await _manager.MSSQLManager.CiselnikyManager.VratCiselnikPreRokAsync<ProgramRiadok3>(SelectedYear);
            var pk5 = await _manager.MSSQLManager.CiselnikyManager.VratCiselnikPreRokAsync<ProgramRiadok5>(SelectedYear);
            var pk7 = await _manager.MSSQLManager.CiselnikyManager.VratCiselnikPreRokAsync<ProgramRiadok7>(SelectedYear);

            ExcelUtilities.ExportCiselnikToExcel(new Dictionary<string, IEnumerable<CiselnikRiadok>>()
            {
                { "pk3", pk3 },
                { "pk5", pk5 },
                { "pk7", pk7 }
            }, path);
        }

        public async Task GenerateData()
        {
            if (treeViewFunkcna.Nodes.Count == 0)
            {
                if (await CiselnikyUtilities.GenerujCiselnik(
                    data.Enums.AnalytickaEvidencia.ProgramovaKlasifikacia, _manager, SelectedYear))
                {
                    MessageBox.Show($"Číselník programovej klasifikácie pre rok {SelectedYear} úspešne vygenerovaný.", "Číselník vygenerovaný",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    await _manager.LogFrontendMessageSafeAsync($"Číselník programovej klasifikácie pre rok {SelectedYear} úspešne vygenerovaný...",
                        "ProgramovaKlasifikacia.GenerateData");

                    await ReloadData();
                }
            }
            else
            {
                MessageBox.Show($"Nemožem generovať dáta pre číselník programovej klasifikácie. Keďže kalendárny rok {SelectedYear} už obsahuje dáta", "Nemožem generovať dáta",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public async Task UpdateData()
        {
            if (treeViewFunkcna.Nodes.Count != 0)
            {
                if (await CiselnikyUtilities.AktualizujCiselnik(
                    data.Enums.AnalytickaEvidencia.ProgramovaKlasifikacia, _manager, SelectedYear))
                {
                    MessageBox.Show($"Číselník programovej klasifikácie pre rok {SelectedYear} úspešne aktualizovaný.", "Číselník aktualizovaný",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    await _manager.LogFrontendMessageSafeAsync($"Číselník programovej klasifikácie pre rok {SelectedYear} úspešne aktualizovaný...",
                        "ProgramovaKlasifikacia.UpdateData");

                    await ReloadData();
                }
            }
            else
            {
                MessageBox.Show($"Nemôžem aktualizovať údaje pre programovú klasifikácie kedže pre rok {SelectedYear} ešte niesú nahrané údaje.", "Nenahrané dáta",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public Task RemoveData()
        {
            throw new NotImplementedException();
        }

        public bool CanGenerate() => true;

        public bool CanUpdate() => true;

        public bool CanRemove() => true;

        public async Task ReloadData()
        {
            await NacitajDataAsync(_manager, SelectedYear);
        }

        public async Task ShowPreview()
        {
            using (var specify = new SpecifyProgramForm())
            {
                specify.ShowImageIcon = false;
                if (specify.ShowDialog() == DialogResult.OK)
                {
                    await CiselnikyUtilities.ShowPreviewForColumns(_manager,
                        specify.GetSelectedColumns(), SelectedYear, null, specify.GetSelectedTitle());
                }
            }
        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            if (treeViewFunkcna.SelectedNode is null)
            {
                MessageBox.Show("Pre zobrazenie prehľadu musíte najprv vybrať položku číselníku", "Nevybraná položka",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Zobrazenie prehladu
            // Prehlad sa zobrazuje pre vyranu polozku
            var pk = treeViewFunkcna.SelectedNode.Tag as ProgramRiadok;
            if (pk is null)
                return;

            Column clmn = null;
            if (pk is ProgramRiadok3)
            {
                clmn = AssuView.VratStlpec(AssuViewAvailableColumns.PKod3);
            }
            else if (pk is ProgramRiadok5)
            {
                clmn = AssuView.VratStlpec(AssuViewAvailableColumns.PKod5);
            }
            else
            {
                clmn = AssuView.VratStlpec(AssuViewAvailableColumns.PKod7);
            }

            var cnd = new Equals(string.Empty, clmn, pk.Kod);
            await CiselnikyUtilities.ShowPreviewForColumns(_manager,
               CiselnikyUtilities.VratStlpcePrePreview(), SelectedYear, cnd, "Prehľad pre programovú položku " + pk.Kod + ", " + pk.Nazov);
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

        public string GetInfoText() => Properties.Resources.InfoProgram;

        public string GetMoreInfo() => Properties.Resources.MoreInfoProgram;

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
                    var exlwkPk3 = pckg.Workbook.Worksheets[0];
                    var exlwkPk5 = pckg.Workbook.Worksheets[1];
                    var exlwkPk7 = pckg.Workbook.Worksheets[2];

                    if (exlwkPk3.Name == "pk3")
                    {
                        Dictionary<string, List<string>> dictPk3 = LoadDataFromExcelPk(exlwkPk3, 2, 1, 3, 4);
                        Dictionary<string, List<string>> dictPk5 = LoadDataFromExcelPk(exlwkPk5, 3, 2, 4, 5);
                        Dictionary<string, List<string>> dictPk7 = LoadDataFromExcelPk(exlwkPk7, 3, 2, 4, 5);

                    }
                    else
                    {
                        MessageBox.Show("Zly file");
                        return;
                    }
                }
            }
        }

        private Dictionary<string, List<string>> LoadDataFromExcelPk(ExcelWorksheet excelWS, int sKod, int sRok, int sNazov, int sPopis)
        {
            Dictionary<string, List<string>> ret = new Dictionary<string, List<string>>();


            for (int row = 2; row <= excelWS.Dimension.End.Row; row++)
            {
                List<String> pkRiadok = new List<string>();

                String kod = excelWS.Cells[row, sKod].Value.ToString();
                pkRiadok.Add(excelWS.Cells[row, sRok].Value.ToString());
                pkRiadok.Add(excelWS.Cells[row, sNazov].Value.ToString());
                pkRiadok.Add(excelWS.Cells[row, sPopis].Value.ToString());

                ret.Add(kod, pkRiadok);
            }

            return ret;
        }
    }
}
