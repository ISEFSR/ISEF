namespace cvti.isef.winformapp.Forms.Dashboard
{
    using cvti.data;
    using cvti.data.Conditions;
    using cvti.data.Enums;
    using cvti.data.Functions;
    using cvti.data.Output;
    using cvti.data.Views;
    using LiveCharts.WinForms;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class PreviewMapForm : DialogBase
    {
        private ISEFDataManager _manager;
        private int _year;

        Panel _mapPanel;

        GeoMap _map;

        TableLayoutPanel _mapLegend;

        private Condition _mapCondition;

        private readonly string MapPath = "./Data/slovakia.xml";

        public PreviewMapForm(ISEFDataManager manager, int year)
        {
            InitializeComponent();
            ShowWait();

            _manager = manager;
            _year = year;

            InitializeMapControl();
            dataGridViewData.SetDoubleBuffered();
        }

        public override void ShowWait()
        {
            base.ShowWait();
            panelContent.Visible = false;
        }

        public override void HideWait()
        {
            base.HideWait();
            panelContent.Visible = true;
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await Task.WhenAll(Task.Delay(2500), LoadData());

            HideWait();
        }

        private async Task LoadData()
        {
            var lang = new Dictionary<string, string>();
            var data = new Dictionary<string, double>();

            _mapLegend.Controls.Clear();

            if (!(await _manager.MSSQLManager.AreDataPresentAsync(_year)))
            {
                _mapPanel.Visible = false;
                dataGridViewData.Visible = false;
                //buttonNewForm.Visible = false;
                return;
            }
            else
            {
                _mapPanel.Visible = true;
                dataGridViewData.Visible = true;
                //buttonNewForm.Visible = true;
            }

            var row = 0;
            foreach (var k in _manager.Kraje)
            {
                // Ignorujem nezaradeny kraj
                if (k.Kod == 900)
                    continue;

                // TODO: vytvor metodu ktora ziska udaje pre kraj a rok ( + podmienka ) 
                lang[k.Kod.ToString()] = k.Nazov;
                data[k.Kod.ToString()] = (double)await _manager.Export.GetSumForKrajAndYear(
                    cvti.data.Enums.SumColumn.Skutocnost, k.Kod, _year);

                var topPadding = row == 0 ? 10 : 10;

                var textLabel = new Label() { ForeColor = Color.FromArgb(255, 255, 255), Padding = new Padding(5, topPadding, 0, 0) };

                textLabel.Text = k.Nazov;
                var valueLabel = new Label() { ForeColor = Color.FromArgb(255, 255, 255), Padding = new Padding(0, topPadding, 5, 0) };

                valueLabel.Text = data[k.Kod.ToString()].ToString("C");

                _mapLegend.SetRow(textLabel, row);
                _mapLegend.SetColumn(textLabel, 0);
                _mapLegend.SetRow(valueLabel, row);
                _mapLegend.SetColumn(valueLabel, 1);

                _mapLegend.Controls.Add(textLabel);
                _mapLegend.Controls.Add(valueLabel);


                row++;
            }

            _map.HeatMap = data;
            _map.LanguagePack = lang;
        }

        private void toolStripButtonExportSelected_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButtonShowSQL_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void InitializeMapControl()
        {
            _mapPanel = new Panel
            {
                Dock = DockStyle.Fill
            };

            _mapLegend = new TableLayoutPanel
            {
                Width = 200,
                Dock = DockStyle.Right,
                BackColor = Color.FromArgb(161, 179, 186)
            };

            _mapLegend.ColumnCount = 2;
            _mapLegend.RowCount = 9;

            for (var i = 0; i < 9; i++)
            {
                _mapLegend.RowStyles.Add(new RowStyle());
                _mapLegend.RowStyles[i].SizeType = SizeType.AutoSize;
            }

            if (System.IO.File.Exists(MapPath))
            {
                _map = new GeoMap()
                {
                    Dock = DockStyle.Fill,
                    Source = MapPath,
                    BackColor = Color.White,
                    BackColorTransparent = true
                };

                //_map.

                _map.LandClick += _map_LandClick;
            }

            _mapPanel.Controls.Add(_map);

            _mapPanel.Controls.Add(_mapLegend);

            panelContent.Controls.Add(_mapPanel);

            ShowMap();
        }

        private void ShowMap()
        {
            _mapPanel.BringToFront();

            labelTitle.Text = "Prehľad nahratých údajov podľa krajov";

            //buttonBack.Visible = false;
        }

        private async Task ShowTable(string kraj)
        {
            if (_loadingTable)
                return;

            _mapPanel.SendToBack();

            _loadingTable = true;

            _mapCondition = new Equals(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.KrajKod), int.Parse(kraj));

            try
            {
                var command = new SelectCommand(string.Empty)
                {
                    CommandCondition = new Equals(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.Rok), _year)
                };

                command.CommandCondition.AddCondition(_mapCondition, ConditionOperator.And);

                AddColumns(command);

                var dataTable = await _manager.Export.SelectDataAsDataTableAsync(command);

                dataGridViewData.DataSource = dataTable;

                labelTitle.Text = $"Prehľad nahratých údajov pre {_manager.VrakKraj(short.Parse(kraj)).Nazov} kraj.";

                //buttonBack.Visible = true;
            }
            finally
            {
                _loadingTable = false;
            }
        }

        private bool _loadingTable = false;

        private async void _map_LandClick(object arg1, LiveCharts.Maps.MapData arg2)
        {
            await ShowTable(arg2.Id);
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            ShowMap();
        }

        private void AddColumns(SelectCommand command)
        {
            var segmentShort = AssuView.VratStlpec(AssuViewAvailableColumns.SegmentShort);
            var stupenShort = AssuView.VratStlpec(AssuViewAvailableColumns.StupenShort);
            var orgNazov = AssuView.VratStlpec(AssuViewAvailableColumns.OrgNazov);
            segmentShort.ColumnAlias = "Segment";
            stupenShort.ColumnAlias = "Stupen";
            orgNazov.ColumnAlias = "Organizacia";
            command.AddColumn(segmentShort);
            command.AddColumn(stupenShort);
            command.AddColumn(orgNazov);

            var skut = AssuView.VratStlpec(AssuViewAvailableColumns.Skut).AddFunction(new ConditionalSum(new Equals(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.Rok), _year)));
            var rozpp = AssuView.VratStlpec(AssuViewAvailableColumns.Rozpp).AddFunction(new ConditionalSum(new Equals(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.Rok), _year)));
            var rozpu = AssuView.VratStlpec(AssuViewAvailableColumns.Rozpu).AddFunction(new ConditionalSum(new Equals(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.Rok), _year)));
            skut.ColumnAlias = $"Skut{_year}";
            rozpp.ColumnAlias = $"Rozpp{_year}";
            rozpu.ColumnAlias = $"Rozpu{_year}";
            command.AddColumn(skut);
            command.AddColumn(rozpp);
            command.AddColumn(rozpu);
        }

        private void dataGridViewData_MouseMove(object sender, MouseEventArgs e)
        {
            dataGridViewData.Invalidate();
        }

        private void dataGridViewData_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            if (dataGridViewData.RectangleToScreen(e.RowBounds).Contains(MousePosition))
            {
                using (var b = new SolidBrush(Color.FromArgb(50, Color.DarkBlue)))
                using (var p = new Pen(Color.DarkBlue))
                {
                    var r = e.RowBounds;
                    r.Width = r.Width--;
                    r.Height = r.Height--;

                    e.Graphics.FillRectangle(b, r);
                    e.Graphics.DrawRectangle(p, r);
                }
            }
        }
    }
}
