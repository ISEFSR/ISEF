namespace cvti.isef.winformapp.Controls.Main.Ciselniky
{
    using System;
    using System.Drawing;
    using System.IO;
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
    using OfficeOpenXml;
    using System.Collections.Generic;

    public partial class UcetControl : UserControl, ICiselnikControl
    {
        private ISEFDataManager _manager;

        public event EventHandler<Tuple<Column, object, AvailableConditions>> ConditionAdded;

        public UcetControl()
        {
            InitializeComponent();
        }

        public int SelectedYear { get; private set; }

        public string TitleText { get => "Číselník účtov"; }

        public string InfoText { get => "Číselník účtov. Číselník neni viazaný na kalendárny rok."; }

        public bool CanGenerate() => false;

        public bool CanRemove() => false;

        public bool CanUpdate() => false;

        public void Deaktivuj()
        {
            listBoxUcty.Items.Clear();
        }

        public async Task ExportData(string path)
        {
            await Task.Delay(1);
            ExcelUtilities.ExportCiselnikToExcel(_manager.Ucty, path);
        }

        public Task GenerateData()
        {
            throw new NotImplementedException();
        }

        public async Task NacitajDataAsync(ISEFDataManager manager, int year)
        {
            _manager = manager;
            SelectedYear = year;

            await Task.Delay(1);
            listBoxUcty.Items.Clear();
            foreach (var u in _manager.Ucty)
                listBoxUcty.Items.Add(u);
        }

        public async Task ReloadData()
        {
            await NacitajDataAsync(_manager, SelectedYear);
        }

        public Task RemoveData()
        {
            throw new NotImplementedException();
        }

        public async Task ShowPreview()
        {
            await CiselnikyUtilities.ShowPreviewForColumns(_manager,
                new AssuViewColumn[]
                {
                    AssuView.VratStlpec(AssuViewAvailableColumns.Ucet, true, "Ucet"),
                }, SelectedYear, null, $"Prehľad účtov za rok {SelectedYear}");
        }

        public Task UpdateData()
        {
            throw new NotImplementedException();
        }

        private async void buttonShowPreview_Click(object sender, EventArgs e)
        {
            if (listBoxUcty.SelectedIndex == -1)
                return;

            var ucet = listBoxUcty.SelectedItem as UcetRiadok;
            var condition = new Equals(string.Empty, 
                AssuView.VratStlpec(AssuViewAvailableColumns.Ucet), ucet.Kod);

            await CiselnikyUtilities.ShowPreviewForColumns(_manager,
               CiselnikyUtilities.VratStlpcePrePreview(), SelectedYear, condition,
               $"Prehľad údajov pre rok {SelectedYear} a účet {ucet.Nazov}");
        }

        private async void buttonSaveChanges_Click(object sender, EventArgs e)
        {
            if (listBoxUcty.SelectedIndex == -1)
                return;

            var ucet = listBoxUcty.SelectedItem as UcetRiadok;

            if (ucet is null)
                return;

            ucet.Nazov = textBoxShort.Text;
            ucet.Popis = textBoxLong.Text;

            if (await _manager.MSSQLManager.Ucty.UpdateRiadok(ucet))
            {
                var index = listBoxUcty.SelectedIndex;
                listBoxUcty.Items.RemoveAt(index);
                listBoxUcty.Items.Insert(index, ucet);
                listBoxUcty.SelectedIndex = index;

                await _manager.LogFrontendMessageSafeAsync($"Textové polia pre účet s kódom {ucet.Kod} úspešne zmenené.",
                        "StupneControl.buttonSaveChanges_Click");
                MessageBox.Show($"Textové polia pre účet s kódom {ucet.Kod} úspešne zmenené.",
                    "Zmena úspešná", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonDiscard_Click(object sender, EventArgs e)
        {
            ZobrazVybranyUcet();
        }

        private void andToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBoxUcty.SelectedIndex == -1)
                return;

            CiselnikyUtilities.CreateCondition(sender, ConditionAdded);
        }

        private void ZobrazVybranyUcet()
        {
            var ucet = listBoxUcty.SelectedItem as UcetRiadok;
            if (ucet is null)
            {
                textBoxKod.Text =
                    textBoxLong.Text =
                    textBoxShort.Text = string.Empty;
            }
            else
            {
                textBoxKod.Text = ucet.Kod;
                textBoxLong.Text = ucet.Popis;
                textBoxShort.Text = ucet.Nazov;
            }
        }

        private void listBoxUcty_SelectedIndexChanged(object sender, EventArgs e)
        {
            ZobrazVybranyUcet();
        }

        public bool CanCreate() => true;

        public async Task CreateItem()
        {
            await Task.Delay(1);
            using (var stupenForm = new NewUcetForm(_manager))
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
                        Dictionary<string, List<string>> dictcu = LoadDataFromExcelcu(exlwkcp);
                    }
                    else
                    {
                        MessageBox.Show("Zly file");
                        return;
                    }
                }
            }
        }

        private Dictionary<string, List<string>> LoadDataFromExcelcu(ExcelWorksheet excelWS)
        {
            Dictionary<string, List<string>> ret = new Dictionary<string, List<string>>();


            for (int row = 2; row <= excelWS.Dimension.End.Row; row++)
            {
                List<String> cuRiadok = new List<string>();

                String Kod = excelWS.Cells[row, 1].Value.ToString();
                //Nazov
                cuRiadok.Add(excelWS.Cells[row, 2].Value.ToString());
                //Popis
                cuRiadok.Add(excelWS.Cells[row, 3].Value.ToString());
                

                ret.Add(Kod, cuRiadok);
            }

            return ret;
        }
    }
}
