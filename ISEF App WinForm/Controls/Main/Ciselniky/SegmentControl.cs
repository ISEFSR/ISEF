namespace cvti.isef.winformapp.Controls.Main.Ciselniky
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using cvti.data.Core;
    using cvti.data;
    using cvti.data.Tables;
    using System.IO;
    using cvti.data.Enums;
    using cvti.data.Columns;
    using cvti.data.Views;
    using cvti.data.Conditions;
    using System.Drawing;
    using OfficeOpenXml;
    using System.Collections.Generic;

    public partial class SegmentControl : UserControl, ICiselnikControl
    {
        const string ExportFileName = "segment.xlsx";

        public SegmentControl()
        {
            InitializeComponent();
        }

        public int SelectedYear { get; private set; }

        public string TitleText { get => "Číselník segmentov"; }

        public string InfoText { get => "Číselník segmentov. Číselník neni viazaný na kalendárny rok."; }

        public void Deaktivuj()
        {
            listBoxSegmenty.Items.Clear();
        }

        private ISEFDataManager _manager;

        public event EventHandler<Tuple<Column, object, AvailableConditions>> ConditionAdded;

        public async Task NacitajDataAsync(ISEFDataManager manager, int year)
        {
            _manager = manager;
            SelectedYear = year;

            await Task.Delay(1);

            listBoxSegmenty.Items.Clear();

            foreach (var s in _manager.Segmenty)
                listBoxSegmenty.Items.Add(s);

            if (listBoxSegmenty.Items.Count > 0 && listBoxSegmenty.SelectedIndex == -1)
                listBoxSegmenty.SelectedIndex = 0;
        }

        private void listBoxSegmenty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxSegmenty.SelectedIndex == -1)
                return;

            ShowSelectedSegment();
        }

        private SegmentRiadok GetSelectedSegment() => listBoxSegmenty.SelectedItem as SegmentRiadok;

        private void ShowSelectedSegment()
        {
            var segment = GetSelectedSegment();

            if (segment is null)
            {
                textBoxKod.Text = textBoxNazov.Text = textBoxPopis.Text = string.Empty;
            }
            else
            {
                textBoxKod.Text = segment.Kod;
                textBoxNazov.Text = segment.SkratenyText;
                textBoxPopis.Text = segment.Popis;
            }
        }

        public async Task ExportData(string path)
        {
            await Task.Delay(1);
            ExcelUtilities.ExportCiselnikToExcel(_manager.Segmenty,
                path);
        }

        public async Task GenerateData()
        {
            await Task.Delay(1);
            return;
        }

        public async Task UpdateData()
        {
            await Task.Delay(1);
            return;
        }

        public async Task RemoveData()
        {
            await Task.Delay(1);
            return;
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
                    AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod, true, "Kod"),
                    AssuView.VratStlpec(AssuViewAvailableColumns.SegmentShort, true, "Skrateny"),
                    AssuView.VratStlpec(AssuViewAvailableColumns.SegmentText, true, "Nazov")
                }, SelectedYear, null, $"Prehľad segmentov pre rok {SelectedYear}");
        }

        private void andToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CiselnikyUtilities.CreateCondition(sender, ConditionAdded);
        }

        private async void buttonSaveChanges_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedSegment();

            selected.SkratenyText = textBoxNazov.Text;
            selected.Popis = textBoxPopis.Text;

            if (await _manager.MSSQLManager.Segmenty.UpdateRiadok(selected))
            {
                var index = listBoxSegmenty.SelectedIndex;
                listBoxSegmenty.Items.RemoveAt(index);
                listBoxSegmenty.Items.Insert(index, selected);
                listBoxSegmenty.SelectedIndex = index;

                await _manager.LogFrontendMessageSafeAsync($"Textové polia pre kod semgmentu {selected.Kod} úspešne zmenené.",
                    "SegmentControl.buttonSaveChanges_Click");
                MessageBox.Show($"Textové polia pre kod semgmentu {selected.Kod} úspešne zmenené.",
                    "Zmena úspešná", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Vyzerá to tak, źe sa mi nepodarilo uložiť zmeny vykonané na segmente. Pre istotu skontrolujte údaje a prípadne skúste uložiť ešte raz.",
                    "Údaje neuložené", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            ShowSelectedSegment();
        }

        private async void buttonValidateSelected_Click(object sender, EventArgs e)
        {
            var selectedSegment = GetSelectedSegment();
            if (selectedSegment is null)
                return;

            var cnd = new Equals(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod), selectedSegment.Kod);
            await CiselnikyUtilities.ShowPreviewForColumns(_manager,
               CiselnikyUtilities.VratStlpcePrePreview(), SelectedYear, cnd,
               $"Prehľad údajov pre rok {SelectedYear} a segment {selectedSegment.Kod}");
        }

        public bool CanCreate() => false;

        public Task CreateItem()
        {
            throw new NotImplementedException();
        }

        public string GetInfoText() => Properties.Resources.InfoSegm;

        public string GetMoreInfo() => Properties.Resources.MoreInfoSegment;

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
                //SkratenyNazov
                csRiadok.Add(excelWS.Cells[row, 2].Value.ToString());
                //Nazov
                csRiadok.Add(excelWS.Cells[row, 3].Value.ToString());
                //Komentar
                csRiadok.Add(excelWS.Cells[row, 4].Value.ToString());

                ret.Add(Kod, csRiadok);
            }

            return ret;
        }
    }
}
