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
    using cvti.data.Core;
    using cvti.data.Columns;
    using cvti.data.Conditions;
    using cvti.data.Views;
    using cvti.data.Enums;
    using cvti.data.Tables;
    using cvti.isef.winformapp.Forms.Classifiers;
    using System.Media;
    using OfficeOpenXml;

    public partial class ZdrojovaKlasifikaciaControl : UserControl, ICiselnikControl
    {
        private ISEFDataManager _manager;

        private readonly Control[] _pomocnyKod;

        public event EventHandler<Tuple<Column, object, AvailableConditions>> ConditionAdded;

        public ZdrojovaKlasifikaciaControl()
        {
            InitializeComponent();

            _pomocnyKod = new Control[]
            {
                labelPK1,
                labelPK2,
                labelPK3,
                labelPK4,
                labelPK5,
                textBoxPK1,
                textBoxPK2,
                textBoxPK3,
                textBoxPK4,
                textBoxPK5,
                label5,
                textBoxKodKde
            };
        }

        public int SelectedYear { get; private set; }

        public string TitleText { get => "Číselník zdrojovej klasifkácie"; }

        public string InfoText { get => "Číselník zdrojovej klasifikácie pre vybraný kalednárny rok."; }
        
        public bool CanGenerate() => true;

        public bool CanRemove() => true;

        public bool CanUpdate() => true;

        public void Deaktivuj()
        {
            treeViewZdroje.Nodes.Clear();
        }

        private List<ZdrojRiadok1> zk1;
        private List<ZdrojRiadok2> zk2;
        private List<ZdrojRiadok3> zk3;
        private List<ZdrojRiadok4> zk4;

        public async Task ExportData(string path)
        {
            zk1 = new List<ZdrojRiadok1>(await _manager.MSSQLManager.CiselnikyManager.VratCiselnikPreRokAsync<ZdrojRiadok1>(SelectedYear));
            zk2 = new List<ZdrojRiadok2>(await _manager.MSSQLManager.CiselnikyManager.VratCiselnikPreRokAsync<ZdrojRiadok2>(SelectedYear));
            zk3 = new List<ZdrojRiadok3>(await _manager.MSSQLManager.CiselnikyManager.VratCiselnikPreRokAsync<ZdrojRiadok3>(SelectedYear));
            zk4 = new List<ZdrojRiadok4>(await _manager.MSSQLManager.CiselnikyManager.VratCiselnikPreRokAsync<ZdrojRiadok4>(SelectedYear));

            ExcelUtilities.ExportCiselnikToExcel(new Dictionary<string, IEnumerable<CiselnikRiadok>>()
            {
                { "zk1", zk1 },
                { "zk2", zk2 },
                { "zk3", zk3 },
                { "zk4", zk4 },
            }, path);
        }

        public async Task GenerateData()
        {
            if (treeViewZdroje.Nodes.Count == 0)
            {
                if (await CiselnikyUtilities.GenerujCiselnik(
                    data.Enums.AnalytickaEvidencia.ZdrojovaKlasifikacia, _manager, SelectedYear))
                {
                    MessageBox.Show($"Číselník zdrojovej klasifikácie pre rok {SelectedYear} úspešne vygenerovaný.", "Číselník vygenerovaný",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    await _manager.LogFrontendMessageSafeAsync($"Číselník zdrojovej klasifikácie pre rok {SelectedYear} úspešne vygenerovaný...", 
                        "ZdrojovaKlasifikaciaControl.GenerateData");

                    await ReloadData();
                }
            }
            else
            {
                MessageBox.Show($"Nemôžem generovať dáta pre číselník zdrojov a rok {SelectedYear} kedže číselník už obsahuje dáta. ", "Číselník už obsahuje dáta.",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public async Task UpdateData()
        {
            if (treeViewZdroje.Nodes.Count != 0)
            {
                if (await CiselnikyUtilities.AktualizujCiselnik(
                    data.Enums.AnalytickaEvidencia.ZdrojovaKlasifikacia, _manager, SelectedYear))
                {
                    MessageBox.Show($"Číselník zdrojovej klasifikácie pre rok {SelectedYear} úspešne aktualizovaný.", "Číselník aktualizovaný",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                    await _manager.LogFrontendMessageSafeAsync($"Číselník zdrojovej klasifikácie pre rok {SelectedYear} úspešne aktualizovaný...",
                        "ZdrojovaKlasifikaciaControl.UpdateData");

                    await ReloadData();
                }
            }
            else
            {
                MessageBox.Show($"Nemožem updatovať číselník zdrojov pre rok {SelectedYear} kedže pre rok niesú nahraté dáta. ", "Nemožem updatovať",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public async Task NacitajDataAsync(ISEFDataManager manager, int year)
        {
            _manager = manager;
            SelectedYear = year;

            Enabled = false;
            try
            {
                var zk1 = await _manager.MSSQLManager.CiselnikyManager.VratCiselnikPreRokAsync<ZdrojRiadok1>(year);
                var zk2 = await _manager.MSSQLManager.CiselnikyManager.VratCiselnikPreRokAsync<ZdrojRiadok2>(year);
                var zk3 = await _manager.MSSQLManager.CiselnikyManager.VratCiselnikPreRokAsync<ZdrojRiadok3>(year);
                var zk4 = await _manager.MSSQLManager.CiselnikyManager.VratCiselnikPreRokAsync<ZdrojRiadok4>(year);

                treeViewZdroje.Nodes.Clear();

                foreach (var f2 in zk1)
                {
                    var n2 = new TreeNode(f2.ToString()) { Tag = f2 };
                    treeViewZdroje.Nodes.Add(n2);

                    foreach (var f3 in zk2.Where(f => f.Zk1 == f2.Kod))
                    {
                        var n3 = new TreeNode(f3.ToString()) { Tag = f3 };
                        n2.Nodes.Add(n3);

                        foreach (var f4 in zk3.Where(f => f.Zk2 == f3.Kod))
                        {
                            var n4 = new TreeNode(f4.ToString()) { Tag = f4 };
                            n3.Nodes.Add(n4);

                            foreach (var f5 in zk4.Where(f => f.Zk3 == f4.Kod))
                            {
                                var n5 = new TreeNode(f5.ToString()) { Tag = f5 };
                                n4.Nodes.Add(n5);
                            }
                        }
                    }
                }

                treeViewZdroje.ExpandAll();

                if (treeViewZdroje.SelectedNode is null && treeViewZdroje.Nodes.Count > 0)
                    treeViewZdroje.SelectedNode = treeViewZdroje.Nodes[0];
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

        public async Task ReloadData()
        {
            await NacitajDataAsync(_manager, SelectedYear);
        }

        public async Task RemoveData()
        {
            await Task.Delay(1);
            MessageBox.Show("Táto možnosť momentálne nieje dostupná...", "Nedostupná možnosť!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public async Task ShowPreview()
        {
            using (var specify = new SpecifyZdrojForm())
            {
                specify.ShowImageIcon = false;
                if (specify.ShowDialog() == DialogResult.OK)
                {
                    await CiselnikyUtilities.ShowPreviewForColumns(_manager,
                        specify.GetSelectedColumns(), SelectedYear, null, specify.GetSelectedTitle());
                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var zk = (ZdrojRiadok)e.Node.Tag;
            ZobrazZdroj(zk);

            foreach (var c in _pomocnyKod)
                c.Visible = c.Enabled = zk is ZdrojRiadok4;

            
        }

        private async void buttonSaveChanges_Click(object sender, EventArgs e)
        {
            if (treeViewZdroje.SelectedNode == null) return;

            var zk = treeViewZdroje.SelectedNode.Tag as ZdrojRiadok;

            if (zk is null)
                return;

            zk.Nazov = textBoxNazov.Text;
            zk.Popis = textBoxPopis.Text;
           

            if (zk is ZdrojRiadok4)
            {
                var zk4 = zk as ZdrojRiadok4;
                zk4.PomKod1 = textBoxPK1.Text;
                zk4.PomKod2 = textBoxPK2.Text;
                zk4.PomKod3 = textBoxPK3.Text;
                zk4.PomKod4 = textBoxPK4.Text;
                zk4.PomKod5 = textBoxPK5.Text;
                zk4.KodKde = textBoxKodKde.Text;
            }

            if (await _manager.MSSQLManager.CiselnikyManager.UpdateCiselnikRiadokAsync(zk))
            {
                treeViewZdroje.SelectedNode.Text = $"[{zk.Kod}] {zk.Nazov}";

                await _manager.LogFrontendMessageSafeAsync($"Textové pole pre položku zdrojovej klasifikácie {zk.Kod} úspešne zmenené.",
                    "ZdrojovaKlasifikaciaControl.buttonSaveChanges_Click");
                MessageBox.Show($"Textové pole pre položku zdrojovej klasifikácie {zk.Kod} úspešne zmenené.",
                    "Zmena úspešná", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonDiscardChanges_Click(object sender, EventArgs e)
        {
            if (treeViewZdroje.SelectedNode is null)
                return;

            ZobrazZdroj(treeViewZdroje.SelectedNode.Tag as ZdrojRiadok);
        }

        private async void buttonShowPreview_Click(object sender, EventArgs e)
        {
            if (treeViewZdroje.SelectedNode is null)
            {
                MessageBox.Show("Pre zobrazenie prehľadu musíte najprv vybrať položku číselníku zdrojovo", "Nevybraná položka",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var selected = treeViewZdroje.SelectedNode.Tag as ZdrojRiadok;
            AssuViewColumn clmn = null;
            if (selected is ZdrojRiadok1)
            {
                clmn = AssuView.VratStlpec(AssuViewAvailableColumns.ZKod1);
            }
            else if (selected is ZdrojRiadok2)
            {
                clmn = AssuView.VratStlpec(AssuViewAvailableColumns.ZKod2);
            }
            else if (selected is ZdrojRiadok3)
            {
                clmn = AssuView.VratStlpec(AssuViewAvailableColumns.ZKod3);
            }
            else if (selected is ZdrojRiadok4)
            {
                clmn = AssuView.VratStlpec(AssuViewAvailableColumns.ZKod4);
            }

            var cnd = new Equals(string.Empty, clmn, selected.Kod);
            await CiselnikyUtilities.ShowPreviewForColumns(_manager, 
                CiselnikyUtilities.VratStlpcePrePreview(),
                SelectedYear, cnd, "Prehľad pre zdrojovú položku " + selected.Kod + ", " + selected.Nazov);
        }

        private void andToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeViewZdroje.SelectedNode is null)
                return;

            // Contextove menu pridanei novej podmienky
            CiselnikyUtilities.CreateCondition(sender, ConditionAdded);
        }

        private void treeViewZdroje_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // select on right click pre treeview so zdrojmi
            if (e.Button == MouseButtons.Right)
                treeViewZdroje.SelectedNode = e.Node;
        }

        private void ZobrazZdroj(ZdrojRiadok zk)
        {
            if (zk is null)
            {
                textBoxKod.Text =
                    textBoxRok.Text =
                    textBoxNazov.Text =
                    textBoxPopis.Text =
                    textBoxPK1.Text =
                    textBoxPK2.Text =
                    textBoxPK3.Text =
                    textBoxPK4.Text =
                    textBoxPK5.Text =
                    textBoxKodKde.Text =  string.Empty;
            }
            else
            {
                textBoxRok.Text = zk.Rok.ToString();
                textBoxKod.Text = zk.Kod;
                textBoxNazov.Text = zk.Nazov;
                textBoxPopis.Text = zk.Popis;

                var nazovTag = 38;
                var kodTag = 37;

                if (zk is ZdrojRiadok2)
                {
                    nazovTag = 40;
                    kodTag = 39;
                }
                else if (zk is ZdrojRiadok3)
                {
                    nazovTag = 42;
                    kodTag = 41;
                }
                else if (zk is ZdrojRiadok4)
                {
                    nazovTag = 44;
                    kodTag = 43;

                    var zk4 = zk as ZdrojRiadok4;
                    textBoxPK1.Text = zk4.PomKod1;
                    textBoxPK2.Text = zk4.PomKod2;
                    textBoxPK3.Text = zk4.PomKod3;
                    textBoxPK4.Text = zk4.PomKod4;
                    textBoxPK5.Text = zk4.PomKod5;

                    textBoxKodKde.Text = zk4.KodKde;
                }

                textBoxNazov.Tag = nazovTag;
                textBoxKod.Tag = kodTag;
                treeViewZdroje.Tag = $"{kodTag};{zk.Kod}";
            }
        }

        public bool CanCreate() => false;

        public async Task CreateItem()
        {
            // Zdrojove podmienky user nemoze vytvarat
            await Task.Delay(1);
        }

        public string GetInfoText() => Properties.Resources.InfoZdroj;

        public string GetMoreInfo() => Properties.Resources.MoreInfoZdroj;

        private void panelControlButtons_Paint(object sender, PaintEventArgs e) => 
            e.Graphics.DrawLine(new Pen(new SolidBrush(Color.FromArgb(210, 210, 210))),
                    new Point(0, 0), new Point(panelControlButtons.Width, 0));

        private void textBoxKodKde_TextChanged(object sender, EventArgs e)
        {
            if (textBoxKodKde.Text.Length > 1)
            {
                textBoxKodKde.Text = textBoxKodKde.Text[0].ToString();
                SystemSounds.Beep.Play();
            }
        }

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
                    var exlwkZk1 = pckg.Workbook.Worksheets[0];
                    var exlwkZk2 = pckg.Workbook.Worksheets[1];
                    var exlwkZk3 = pckg.Workbook.Worksheets[2];
                    var exlwkZk4 = pckg.Workbook.Worksheets[3];

                    if (exlwkZk1.Name == "zk1")
                    {
                        Dictionary<string, List<string>> dictZk1 = LoadDataFromExcelZk(exlwkZk1, 2, 1, 3, 4);
                        Dictionary<string, List<string>> dictZk2 = LoadDataFromExcelZk(exlwkZk2, 3, 2, 4, 5);
                        Dictionary<string, List<string>> dictZk3 = LoadDataFromExcelZk(exlwkZk3, 3, 2, 4, 5);
                        Dictionary<string, List<string>> dictZk4 = LoadDataFromExcelZk(exlwkZk4, 9, 8, 10, 11);

                    }
                    else
                    {
                        MessageBox.Show("Zly file");
                        return;
                    }
                }
            }
        }

        private Dictionary<string, List<string>> LoadDataFromExcelZk(ExcelWorksheet excelWS, int sKod, int sRok, int sNazov, int sPopis)
        {
            Dictionary<string, List<string>> ret = new Dictionary<string, List<string>>();


            for (int row = 2; row <= excelWS.Dimension.End.Row; row++)
            {
                List<String> zkRiadok = new List<string>();

                String kod = excelWS.Cells[row, sKod].Value.ToString();
                zkRiadok.Add(excelWS.Cells[row, sRok].Value.ToString());
                zkRiadok.Add(excelWS.Cells[row, sNazov].Value.ToString());
                zkRiadok.Add(excelWS.Cells[row, sPopis].Value.ToString());

                ret.Add(kod, zkRiadok);
            }

            return ret;
        }
    }
}
