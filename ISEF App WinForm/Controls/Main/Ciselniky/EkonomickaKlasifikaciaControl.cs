namespace cvti.isef.winformapp.Controls.Main.Ciselniky
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using cvti.data;
    using cvti.data.Core;
    using cvti.data.Conditions;
    using cvti.data.Views;
    using cvti.data.Enums;
    using cvti.data.Tables;
    using cvti.isef.winformapp.Forms.Classifiers;
    using cvti.data.Columns;
    using System.IO;
    using OfficeOpenXml;

    public partial class EkonomickaKlasifikaciaControl : UserControl, ICiselnikControl
    {
        #region Private Variables And Constructors
        private ISEFDataManager _manager;

        //private EkonomickaRiadok _selectedRow;

        public event EventHandler<Tuple<Column, object, AvailableConditions>> ConditionAdded;

        public EkonomickaKlasifikaciaControl()
        {
            InitializeComponent();
        }
        #endregion

        #region Public Properties
        public int SelectedYear { get; private set; }

        public string TitleText { get => "Ekonomická klasifikácia"; }

        public string InfoText { get => "Číselník ekonomickej klasifikácie pre vybraný rok"; }

        private List<EkonomickaRiadok1> ek1;
        private List<EkonomickaRiadok2> ek2;
        private List<EkonomickaRiadok3> ek3;
        private List<EkonomickaRiadok6> ek6;
        #endregion

        #region Public Methods
        public async Task NacitajDataAsync(ISEFDataManager manager, int year)
        {
            _manager = manager;
            SelectedYear = year;

            Enabled = false;
            try
            {
                ek1 = new List<EkonomickaRiadok1> (await _manager.MSSQLManager.CiselnikyManager.VratCiselnikPreRokAsync<EkonomickaRiadok1>(year));
                ek2 = new List<EkonomickaRiadok2> (await _manager.MSSQLManager.CiselnikyManager.VratCiselnikPreRokAsync<EkonomickaRiadok2>(year));
                ek3 = new List<EkonomickaRiadok3> (await _manager.MSSQLManager.CiselnikyManager.VratCiselnikPreRokAsync<EkonomickaRiadok3>(year));
                ek6 = new List<EkonomickaRiadok6> (await _manager.MSSQLManager.CiselnikyManager.VratCiselnikPreRokAsync<EkonomickaRiadok6>(year));

                textBoxKod.Text =
                    textBoxNazov.Text =
                    textBoxPopis.Text =
                    textBoxRok.Text = string.Empty;

                treeViewEkonomickePolozky.Nodes.Clear();

                foreach (var e1 in ek1)
                {
                    var n1 = new TreeNode($"[{e1.Kod}] {e1.Nazov}") { Tag = e1 };
                    treeViewEkonomickePolozky.Nodes.Add(n1);

                    foreach (var e2 in ek2.Where(e => e.Ek1 == e1.Kod))
                    {
                        var n2 = new TreeNode($"[{e2.Kod}] {e2.Nazov}") { Tag = e2 };
                        n1.Nodes.Add(n2);

                        foreach (var e3 in ek3.Where(e => e.Ek2 == e2.Kod))
                        {
                            var n3 = new TreeNode($"[{e3.Kod}] {e3.Nazov}") { Tag = e3 };
                            n2.Nodes.Add(n3);

                            foreach (var e6 in ek6.Where(e => e.Ek3 == e3.Kod))
                            {
                                var n6 = new TreeNode($"[{e6.Kod}] {e6.Nazov}") { Tag = e6 };
                                n3.Nodes.Add(n6);
                            }
                        }
                    }
                }

                treeViewEkonomickePolozky.ExpandAll();

                if (treeViewEkonomickePolozky.SelectedNode is null && treeViewEkonomickePolozky.Nodes.Count > 0)
                    treeViewEkonomickePolozky.SelectedNode = treeViewEkonomickePolozky.Nodes[0];
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

        public void Deaktivuj()
        {
            treeViewEkonomickePolozky.Nodes.Clear();
        }

        public async Task ExportData(string path)
        {
            var ek1 = await _manager.MSSQLManager.CiselnikyManager.VratCiselnikPreRokAsync<EkonomickaRiadok1>(SelectedYear);
            var ek2 = await _manager.MSSQLManager.CiselnikyManager.VratCiselnikPreRokAsync<EkonomickaRiadok2>(SelectedYear);
            var ek3 = await _manager.MSSQLManager.CiselnikyManager.VratCiselnikPreRokAsync<EkonomickaRiadok3>(SelectedYear);
            var ek6 = await _manager.MSSQLManager.CiselnikyManager.VratCiselnikPreRokAsync<EkonomickaRiadok6>(SelectedYear);

            ExcelUtilities.ExportCiselnikToExcel(new Dictionary<string, IEnumerable<CiselnikRiadok>>()
                {
                    { "ek1", ek1 },
                    { "ek2", ek2 },
                    { "ek3", ek3 },
                    { "ek6", ek6 },
                }, path);
        }

        public bool CanGenerate() => true;

        public bool CanUpdate() => true;

        public async Task GenerateData()
        {
            if (treeViewEkonomickePolozky.Nodes.Count == 0)
            {
                if (await CiselnikyUtilities.GenerujCiselnik(
                    data.Enums.AnalytickaEvidencia.EkonomickaKlasifikacia, _manager, SelectedYear))
                {
                    MessageBox.Show($"Číselník ekonomickej klasifikácie pre rok {SelectedYear} úspešne vygenerovaný.", "Číselník vygenerovaný",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    await _manager.LogFrontendMessageSafeAsync($"Číselník ekonomickej klasifikácie pre rok {SelectedYear} úspešne vygenerovaný...",
                        "EkonomickaKlasifikaciaControl.GenerateData");

                    await ReloadData();
                }
            }
            else
            {
                MessageBox.Show($"Nemožem generovať dáta pre číselník ekonomickej klasifikácie. Keďže kalendárny rok {SelectedYear} už obsahuje dáta", "Nemožem generovať dáta",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public async Task UpdateData()
        {
            if (treeViewEkonomickePolozky.Nodes.Count != 0)
            {
                if (await CiselnikyUtilities.AktualizujCiselnik(
                    data.Enums.AnalytickaEvidencia.EkonomickaKlasifikacia, _manager, SelectedYear))
                {
                    MessageBox.Show($"Číselník ekonomickej klasifikácie pre rok {SelectedYear} úspešne aktualizovaný.", "Číselník aktualizovaný",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    await _manager.LogFrontendMessageSafeAsync($"Číselník ekonomickej klasifikácie pre rok {SelectedYear} úspešne aktualizovaný...",
                        "EkonomickaKlasifikaciaControl.UpdateData");

                    await ReloadData();
                }
            }
            else
            {
                MessageBox.Show($"Nemôžem aktualizovať údaje pre ekonomickú klasifikácie kedže pre rok {SelectedYear} ešte niesú nahrané údaje.", "Nenahrané dáta",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public bool CanRemove() => true;

        public async Task RemoveData()
        {
            var dataYears = await _manager.MSSQLManager.GetAvailableYearsAsync();
            if (dataYears.Contains(SelectedYear))
            {
                MessageBox.Show("Pre vybraný kalendárny rok sú už nahrané dáta a tým aj číselník ekonomickej klasifikácie.",
                    "Dáta existujú", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (MessageBox.Show($"Skutočne si prajete odstrániť číselník ekonomickej kalsifkácie pre rok {SelectedYear}?",
                    "Odstránenie číselníku", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //await _manager.MSSQLManager.a
                    MessageBox.Show("Táto možnosť momentálne nieje dostupná...", "Nedostupná možnosť!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await ReloadData();
                }
            }
        }

        public async Task ReloadData()
        {
            await NacitajDataAsync(_manager, SelectedYear);
        }

        public async Task ShowPreview()
        {
            using (var specify = new SpecifyEkonomickaForm())
            {
                specify.ShowImageIcon = false;
                if (specify.ShowDialog() == DialogResult.OK)
                {
                    await CiselnikyUtilities.ShowPreviewForColumns(_manager, 
                        specify.GetSelectedColumns(), SelectedYear, null, specify.GetSelectedTitle());
                }
            }
        }
        #endregion

        private void ZobrazEkonomicku(EkonomickaRiadok ek)
        {
            if (ek is null)
            {
                textBoxKod.Text =
                    textBoxNazov.Text =
                    textBoxPopis.Text =
                    textBoxRok.Text = string.Empty;
            }
            else
            {
                //_selectedRow = ek;

                textBoxRok.Text = ek.Rok.ToString();
                textBoxKod.Text = ek.Kod;
                textBoxNazov.Text = ek.Nazov;
                textBoxPopis.Text = ek.Popis;

                var nazovTag = 22;
                var kodTag = 21;
                if (ek is EkonomickaRiadok2)
                {
                    nazovTag = 24;
                    kodTag = 23;
                }
                else if (ek is EkonomickaRiadok3)
                {
                    nazovTag = 26;
                    kodTag = 25;
                }
                else if (ek is EkonomickaRiadok6)
                {
                    nazovTag = 28;
                    kodTag = 27;
                }
                textBoxNazov.Tag = nazovTag;
                textBoxKod.Tag = kodTag;

                treeViewEkonomickePolozky.Tag = $"{kodTag};{ek.Kod}";
            }
        }

        private void treeViewEkonomickePolozky_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ZobrazEkonomicku(e.Node.Tag as EkonomickaRiadok);
        }

        private async void buttonSaveChanges_Click(object sender, EventArgs e)
        {
            if (treeViewEkonomickePolozky.SelectedNode == null) 
                return;

            var ek = treeViewEkonomickePolozky.SelectedNode.Tag as EkonomickaRiadok;

            if (ek is null)
                return;

            ek.Nazov = textBoxNazov.Text;
            ek.Popis = textBoxPopis.Text;

            if (await _manager.MSSQLManager.CiselnikyManager.UpdateCiselnikRiadokAsync(ek))
            {
                treeViewEkonomickePolozky.SelectedNode.Text = $"[{ek.Kod}] {ek.Nazov}";//textBoxNazov.Text;

                await _manager.LogFrontendMessageSafeAsync($"Textové pole pre položku ekonomickej klasifikácie {ek.Kod} úspešne zmenené.",
                    "EkonomickaKlasifikaciaControl.buttonSaveChanges_Click");
                MessageBox.Show($"Textové pole pre položku ekonomickej klasifikácie {ek.Kod} úspešne zmenené.",
                    "Zmena úspešná", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonDiscardChanges_Click(object sender, EventArgs e)
        {
            if (treeViewEkonomickePolozky.SelectedNode is null)
                return;
            ZobrazEkonomicku(treeViewEkonomickePolozky.SelectedNode.Tag as EkonomickaRiadok);
        }

        private async void buttonShowPreview_Click(object sender, EventArgs e)
        {
            if (treeViewEkonomickePolozky.SelectedNode is null)
            {
                MessageBox.Show("Pre zobrazenie prehľadu musíte najprv vybrať položku číselníku", "Nevybraná položka",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Zobrazenie prehladu
            // Prehlad sa zobrazuje pre vyranu polozku
            var ek = treeViewEkonomickePolozky.SelectedNode.Tag as EkonomickaRiadok;
            if (ek is null)
                return;

            Column clmn = null;
            if (ek is EkonomickaRiadok1)
            {
                clmn = AssuView.VratStlpec(AssuViewAvailableColumns.EKod1);
            }
            else if (ek is EkonomickaRiadok2)
            {
                clmn = AssuView.VratStlpec(AssuViewAvailableColumns.EKod2);
            }
            else if (ek is EkonomickaRiadok3)
            {
                clmn = AssuView.VratStlpec(AssuViewAvailableColumns.EKod3);
            }
            else if (ek is EkonomickaRiadok6)
            {
                clmn = AssuView.VratStlpec(AssuViewAvailableColumns.EKod6);
            }

            var cnd = new Equals(string.Empty, clmn, ek.Kod);
            await CiselnikyUtilities.ShowPreviewForColumns(_manager,
               CiselnikyUtilities.VratStlpcePrePreview(), SelectedYear, cnd, "Prehľad pre ekonomickú položku " + ek.Kod + " " + ek.Nazov);
        }

        private void treeViewEkonomickePolozky_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // Zobrazenie contextoveho menu pri right clicku na treeview item
            if (e.Button == MouseButtons.Right)
                treeViewEkonomickePolozky.SelectedNode = e.Node;
        }

        private void andToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Context menu item 
            // potrebuje vybranu polozku
            // Ak je vybrana tak prida podmiebnku
            if (treeViewEkonomickePolozky.SelectedNode is null)
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

        public string GetInfoText() => Properties.Resources.InfoEkonomicka;

        public string GetMoreInfo() => Properties.Resources.MoreInfoEkonomicka;

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
                    var exlwkEk1 = pckg.Workbook.Worksheets[0];
                    var exlwkEk2 = pckg.Workbook.Worksheets[1];
                    var exlwkEk3 = pckg.Workbook.Worksheets[2];
                    var exlwkEk6 = pckg.Workbook.Worksheets[3];

                    if (exlwkEk1.Name == "ek1")
                    {
                        Dictionary<string, List<string>> dictEk1 = LoadDataFromExcelEk(exlwkEk1, 2, 1, 3, 4);
                        Dictionary<string, List<string>> dictEk2 = LoadDataFromExcelEk(exlwkEk2, 3, 2, 4, 5);
                        Dictionary<string, List<string>> dictEk3 = LoadDataFromExcelEk(exlwkEk3, 3, 2, 4, 5);
                        Dictionary<string, List<string>> dictEk6 = LoadDataFromExcelEk(exlwkEk6, 3, 2, 4, 5);

                    }
                    else
                    {
                        MessageBox.Show("Zly file");
                        return;
                    }
                }
            }
        }

        private Dictionary<string, List<string>> LoadDataFromExcelEk(ExcelWorksheet excelWS, int sKod, int sRok, int sNazov, int sPopis)
        {
            Dictionary<string, List<string>> ret = new Dictionary<string, List<string>>();


            for (int row = 2; row <= excelWS.Dimension.End.Row; row++)
            {
                List<String> ekRiadok = new List<string>();

                String kod = excelWS.Cells[row, sKod].Value.ToString();
                ekRiadok.Add(excelWS.Cells[row, sRok].Value.ToString());
                ekRiadok.Add(excelWS.Cells[row, sNazov].Value.ToString());
                ekRiadok.Add(excelWS.Cells[row, sPopis].Value.ToString());

                ret.Add(kod, ekRiadok);
            }

            return ret;
        }
    }
}
