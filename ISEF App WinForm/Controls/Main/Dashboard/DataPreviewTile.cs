namespace cvti.isef.winformapp.Controls.Main.Dashboard
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using LiveCharts;
    using LiveCharts.Wpf;
    using System.Linq;
    using cvti.data;
    using cvti.data.Enums;
    using cvti.data.Tables;
    using System;

    public partial class DataPreviewTile : UserControl, IDashboardActivableTile, IYearDependant
    {
        private ISEFDataManager _manager;

        public DataPreviewTile()
        {
            InitializeComponent();
            
            // X-ova os pre graf obsahuje kalendarny rok
            cartesianChartPreview.AxisX.Add(new Axis
            {
                Title = "Kalendárny rok",
                LabelFormatter = value => value.ToString()
            });

            // Y-ova os pre graf urcuje pocet udajov
            // TODO: kludne prerobit na SUM(CASE WHEN.. ) stlpec
            cartesianChartPreview.AxisY.Add(new Axis
            {
                Title = "Počet údajov",
                LabelFormatter = value => value.ToString()
            });

            cartesianChartPreview.LegendLocation = LegendLocation.Right;
        }

        private readonly Dictionary<int, int> YearsDictionary = new Dictionary<int, int>();

        public int Rok { get; set; }

        public async Task LoadData(ISEFDataManager manager)
        {
            _manager = manager ?? throw new System.ArgumentNullException(nameof(manager));

            var stats = await manager.MSSQLManager.GetStatsAsync();
            var dataPresent = stats.Any();

            if (!dataPresent)
            {
                Visible = false;
                return;
            }
            else
            {
                Visible = false;
                return;
            }

            var index = 0;
            YearsDictionary.Clear();
            foreach (var y in await _manager.MSSQLManager.GetAvailableYearsAsync())
                YearsDictionary.Add(y, index++);

            cartesianChartPreview.AxisX[0].Labels = new List<string>();
            foreach (var y in YearsDictionary.Keys)
            {
                cartesianChartPreview.AxisX[0].Labels.Add(y.ToString());
                foreach (var s in cartesianChartPreview.Series)
                    s.Values.Add(0);
            }

            await DrawMainOverviewChart();
        }

        private System.Windows.Media.Brush GetBrush(System.Windows.Media.Color clr)
            => (System.Windows.Media.Brush)new System.Windows.Media.BrushConverter().ConvertFromString(new System.Windows.Media.SolidColorBrush(clr).ToString());

        private async Task DrawMainOverviewChart()
        {
            cartesianChartPreview.Series.Clear();

            foreach (var s in _manager.Stupne)
            {
                var drawingColor = Color.FromArgb(s.Farba);

                var serie = new LineSeries()
                {
                    Tag = s.Kod,
                    Title = s.Nazov,
                    PointForeground = GetBrush(System.Windows.Media.Color.FromRgb(drawingColor.R, drawingColor.G, drawingColor.B)),
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 15,
                    Values = new ChartValues<int>()
                };

                cartesianChartPreview.Series.Add(serie);

                foreach (var r in YearsDictionary.Keys)
                {
                    try
                    {
                        serie.Values[YearsDictionary[r]] = await _manager.MSSQLManager.GetDataCountAsync(GetInputType(s), r);
                    }
                    catch (Exception ex)
                    {
                        await _manager.LogFrontendErrorSafeAsync(ex, "DataPreviewTile.DrawMainOverviewChart");
                    }
                }
            }
        }

        private void cartesianChartPreview_DataClick(object sender, ChartPoint chartPoint)
        {
            labelTitle.Text = $"Prehľad údajov pre {chartPoint.SeriesView.Title} a rok ";

            cartesianChartPreview.Series.Clear();
            cartesianChartPreview.Series.Add(new LineSeries() { Tag = "skut", Title = "Skutočnosť", PointForeground = System.Windows.Media.Brushes.Blue, PointGeometry = DefaultGeometries.Square, PointGeometrySize = 15 });
            cartesianChartPreview.Series.Add(new LineSeries() { Tag = "rozpp", Title = "Rozpočet schválený", PointForeground = System.Windows.Media.Brushes.Red, PointGeometry = DefaultGeometries.Square, PointGeometrySize = 15 });
            cartesianChartPreview.Series.Add(new LineSeries() { Tag = "rozpu", Title = "Rozpočet upravený", PointForeground = System.Windows.Media.Brushes.DarkGreen, PointGeometry = DefaultGeometries.Square, PointGeometrySize = 15 });

            foreach (var s in cartesianChartPreview.Series)
                s.Values = new ChartValues<decimal>();
        }

        private void buttonBack_Click(object sender, System.EventArgs e)
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawShadow(e.Graphics);
        }

        private void DrawShadow(Graphics g)
        {
            const int ShadowSize = 5;
            for (var i = 0; i < ShadowSize; i++)
            {
                // Right shadow
                g.DrawLine(new Pen(Color.FromArgb(140, 140, 140), 1.0f),
                    new Point(panelContent.Location.X + panelContent.Width + i, panelContent.Location.Y + panelContent.Height + i - 1),
                    new Point(panelContent.Location.X + panelContent.Width + i, panelContent.Location.Y + i));
                // Bottom shadow
                g.DrawLine(new Pen(Color.FromArgb(140, 140, 140), 1.0f),
                    new Point(panelContent.Location.X + i, panelContent.Location.Y + panelContent.Height + i),
                    new Point(panelContent.Location.X + panelContent.Width + i, panelContent.Location.Y + panelContent.Height + i));
            }
        }

        private void labelTitle_Click(object sender, System.EventArgs e)
        {

        }

        private void buttonNewForm_Click(object sender, System.EventArgs e)
        {
            // TODO: show full screen stats ( in dialog window )
            MessageBox.Show("Táto možnosť neni momentálne implementovaná", "Feature not implemented yet.", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private InputType GetInputType(StupenRiadok stupen)
        {
            switch(stupen.Kod[0])
            {
                case 'a': return InputType.VVS;
                case 'v': return InputType.MV;
                case '9': return InputType.VUC;
                case 'o': return InputType.MaO;
                case '2': return InputType.OPRO;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
