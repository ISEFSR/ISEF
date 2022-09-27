namespace cvti.isef.winformapp.Controls.Main.Ciselniky
{
    using System;
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
    using cvti.isef.winformapp.Forms.Classifiers;
    using System.Drawing;
    using System.Collections.Generic;
    using OfficeOpenXml;

    public partial class StupneControl : UserControl, ICiselnikControl
    {
        public StupneControl()
        {
            InitializeComponent();
        }

        public int SelectedYear { get; private set; }

        public string TitleText { get => "Číselník stupňov"; }

        public string InfoText { get => "Číselník stupňov. Číselník neni viazaný na kalendárny rok."; }

        public void Deaktivuj()
        {
            //throw new NotImplementedException();
            listBoxStupne.Items.Clear();
        }

        private ISEFDataManager _manager;

        public event EventHandler<Tuple<Column, object, AvailableConditions>> ConditionAdded;

        public async Task NacitajDataAsync(ISEFDataManager manager, int year)
        {
            _manager = manager;
            SelectedYear = year;

            await Task.Delay(1);

            listBoxStupne.Items.Clear();

            foreach (var s in _manager.Stupne)
                listBoxStupne.Items.Add(s);

            if (listBoxStupne.SelectedItem is null && listBoxStupne.Items.Count > 0)
                listBoxStupne.SelectedIndex = 0;
        }

        private void listBoxStupne_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxStupne.SelectedIndex == -1)
                return;

            ShowSelectedStupen();
        }

        private StupenRiadok GetSelected() => listBoxStupne.SelectedItem as StupenRiadok;

        private void ShowSelectedStupen()
        {
            StupenRiadok stupen = GetSelected();

            if (stupen is null)
            {
                textBoxKod.Text = 
                    textBoxNazov.Text = 
                    textBoxPopis.Text = 
                    string.Empty;
            }
            else
            {
                textBoxKod.Text = stupen.Kod.ToString();
                textBoxNazov.Text = stupen.Nazov;
                textBoxPopis.Text = stupen.Popis;
            }
        }

        public async Task ExportData(string path)
        {
            await Task.Delay(1);
            ExcelUtilities.ExportCiselnikToExcel(_manager.Stupne, path);
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
            await CiselnikyUtilities.ShowPreviewForColumns(_manager,
                new AssuViewColumn[]
                {
                    AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod, true, "Kod"),
                    AssuView.VratStlpec(AssuViewAvailableColumns.StupenShort, true, "Skrateny"),
                    AssuView.VratStlpec(AssuViewAvailableColumns.StupenText, true, "Nazov")
                }, SelectedYear, null, "Prehľad stupňov pre rok " + SelectedYear.ToString());
        }

        private void andToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBoxStupne.SelectedIndex == -1)
                return;

            CiselnikyUtilities.CreateCondition(sender, ConditionAdded);
        }

        private async void buttonSaveChanges_Click(object sender, EventArgs e)
        {
            var selected = GetSelected();

            if (selected is null)
                return;

            selected.Nazov = textBoxNazov.Text;
            selected.Popis = textBoxPopis.Text;

            if (await _manager.MSSQLManager.Stupne.UpdateRiadok(selected))
            {
                var index = listBoxStupne.SelectedIndex;
                listBoxStupne.Items.RemoveAt(index);
                listBoxStupne.Items.Insert(index, selected);
                listBoxStupne.SelectedIndex = index;

                await _manager.LogFrontendMessageSafeAsync($"Textové polia pre stupeň s kódom {selected.Kod} úspešne zmenené.",
                        "StupneControl.buttonSaveChanges_Click");
                MessageBox.Show($"Textové polia pre stupeň s kódom {selected.Kod} úspešne zmenené.",
                    "Zmena úspešná", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            ShowSelectedStupen();
        }

        private async void buttonValidateSelected_Click(object sender, EventArgs e)
        {
            if (listBoxStupne.SelectedIndex == -1)
                return;

            var stupen = listBoxStupne.SelectedItem as StupenRiadok;

            var condition = new Equals(string.Empty, 
                AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod), stupen.Kod);

            await CiselnikyUtilities.ShowPreviewForColumns(_manager, 
                CiselnikyUtilities.VratStlpcePrePreview(), SelectedYear, condition,
                "Prehľad pre stupeň " + stupen.Nazov);
        }

        public bool CanCreate() => true;

        public async Task CreateItem()
        {
            await Task.Delay(1);
            using (var stupenForm = new NewStupenForm(_manager))
            {
                if (stupenForm.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Táto možnosť je momentálne nedostupná...", "Nedostupná možnosť.",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        public string GetInfoText() => null;

        public string GetMoreInfo() => null;

        private void panelControlButtons_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new System.Drawing.Pen(new SolidBrush(Color.FromArgb(210, 210, 210))),
    new Point(0, 0),
    new Point(panelControlButtons.Width, 0));
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
                    var exlwkcp = pckg.Workbook.Worksheets[0];

                    if (exlwkcp.Name == "data")
                    {
                        Dictionary<string, List<string>> dictcs = LoadDataFromExcelcs(exlwkcp);
                    }
                    else
                    {
                        MessageBox.Show("Zly file");
                        return;
                    }
                }
            }
        }

        private Dictionary<string, List<string>> LoadDataFromExcelcs(ExcelWorksheet excelWS)
        {
            Dictionary<string, List<string>> ret = new Dictionary<string, List<string>>();


            for (int row = 2; row <= excelWS.Dimension.End.Row; row++)
            {
                List<String> csRiadok = new List<string>();

                String Kod = excelWS.Cells[row, 1].Value.ToString();
                //Naziv
                csRiadok.Add(excelWS.Cells[row, 2].Value.ToString());
                //Popis
                csRiadok.Add(excelWS.Cells[row, 3].Value.ToString());
                //Komentar
                csRiadok.Add(excelWS.Cells[row, 4].Value.ToString());
                //Farba
                csRiadok.Add(excelWS.Cells[row, 5].Value.ToString());

                ret.Add(Kod, csRiadok);
            }

            return ret;
        }
    }
}
