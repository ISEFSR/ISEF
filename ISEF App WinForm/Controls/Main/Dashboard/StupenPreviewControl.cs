namespace cvti.isef.winformapp.Controls.Main.Dashboard
{
    using cvti.data;
    using cvti.data.Columns;
    using cvti.data.Conditions;
    using cvti.data.Enums;
    using cvti.data.Functions;
    using cvti.data.Output;
    using cvti.data.Views;
    using System;
    using System.Linq;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Collections.Generic;
    using System.Drawing;

    /// <summary>
    /// Prehlad nahranych udajov pre vybrany stupen a vybrany rok
    /// </summary>
    public partial class StupenPreviewControl : UserControl
    {
        private ISEFDataManager _manager;

        public StupenPreviewControl()
        {
            InitializeComponent();

            comboBoxSumColumn.SelectedIndex = 0;

            dataGridViewData.SetDoubleBuffered();
        }         

        public async Task LoadData(ISEFDataManager manager, int year, string stupen, string stupenText)
        {
            _manager = manager;

            label11.Text = stupenText;

            var command = GetCommandForData(year, stupen);

            var statsData = _manager.MSSQLManager.SelectData(GetCommandsForStats());
            var mainData = manager.MSSQLManager.SelectData(command);

            await Task.WhenAll(statsData, mainData);

            dataGridViewData.DataSource = Utilities.ConvertDataToTable(mainData.Result);

            for (var clmnIndex = 0; clmnIndex < dataGridViewData.Columns.Count; clmnIndex++)
            {
                if (command.Columns.ElementAt(clmnIndex).ContainsAggregateFunction())
                {
                    dataGridViewData.Columns[clmnIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                else
                {
                    dataGridViewData.Columns[clmnIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }

            // Rok, Rozpoctove, Prispevkove, Prijmy, Vydavky, Kraje (101 - 108)
            var labels = new Label[] { labelRO, label1PO, labelPP, labelVV, labelBA, labelTT, labelTN, labelNR, labelZA, labelBB, labelPR, labelKE };
            for (var i = 0; i < labels.Length; i++)
                labels[i].Text = statsData.Result.ElementAt(0).Values[i].ToString();
        }

        private AssuViewAvailableColumns VratVybranySumacnyStlpec()
            => (AssuViewAvailableColumns)(comboBoxSumColumn.SelectedIndex + 53);

        private SelectCommand GetCommandForData(int rok, string stupen)
        {
            var command = new SelectCommand(string.Empty);

            command.AddColumn(AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod, true, "Segment"));
            command.AddColumn(AssuView.VratStlpec(AssuViewAvailableColumns.SegmentShort, true, "Segment názov"));

            command.AddColumn(AssuView.VratStlpec(AssuViewAvailableColumns.PodriadenostKod, true, "Podriadenosť"));
            command.AddColumn(AssuView.VratStlpec(AssuViewAvailableColumns.PodriadenostNazov, true, "Podriadenosť názov"));

            command.AddColumn(AssuView.VratStlpec(AssuViewAvailableColumns.FKod2, true, "Funkčná2"));
            command.AddColumn(AssuView.VratStlpec(AssuViewAvailableColumns.FNazov2, true, "Funkčná2 názov"));

            command.AddColumn(AssuView.VratStlpec(AssuViewAvailableColumns.Skut, true, "Skutočnosť").AddFunction(new Sum()));
            command.AddColumn(AssuView.VratStlpec(AssuViewAvailableColumns.Rozpp, true, "Schválený").AddFunction(new Sum()));
            command.AddColumn(AssuView.VratStlpec(AssuViewAvailableColumns.Rozpu, true, "Upravený").AddFunction(new Sum()));

            var conditionRok = new Equals(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.Rok), rok);
            var conditionStupen = new Equals(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod), stupen);

            conditionRok.AddCondition(conditionStupen, ConditionOperator.And);

            command.CommandCondition = conditionRok;

            return command;
        }

        private SelectCommand GetCommandsForStats()
        {
            var command = new SelectCommand(string.Empty);

            command.AddColumn(AssuView.VratStlpec(data.Enums.AssuViewAvailableColumns.Rok));

            command.AddColumn(VratStlpecRozpoctove(VratVybranySumacnyStlpec()));
            command.AddColumn(VratStlpecPrispevkove(VratVybranySumacnyStlpec()));
            command.AddColumn(VratStlpecPrijmy(VratVybranySumacnyStlpec()));
            command.AddColumn(VratStlpecVydavky(VratVybranySumacnyStlpec()));

            foreach (var c in VratStlpceKraje(VratVybranySumacnyStlpec()))
                command.AddColumn(c);

            return command;
        }

        private Column VratStlpecRozpoctove(AssuViewAvailableColumns stlpec)
        {
            Debug.Assert(stlpec == AssuViewAvailableColumns.Skut || stlpec == AssuViewAvailableColumns.Rozpp || stlpec == AssuViewAvailableColumns.Rozpu);

            var condition = new Equals(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod), "06");
            var rozpoctove = AssuView.VratStlpec(stlpec);
            rozpoctove.AddFunction(new ConditionalSum(condition));
            rozpoctove.ColumnAlias = $"Rozpočtové organizácie({stlpec.ToString()})";

            return rozpoctove;
        }

        private Column VratStlpecPrispevkove(AssuViewAvailableColumns stlpec)
        {
            Debug.Assert(stlpec == AssuViewAvailableColumns.Skut || stlpec == AssuViewAvailableColumns.Rozpp || stlpec == AssuViewAvailableColumns.Rozpu);

            var condition = new Equals(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod), "08");
            var rozpoctove = AssuView.VratStlpec(stlpec);
            rozpoctove.AddFunction(new ConditionalSum(condition));
            rozpoctove.ColumnAlias = $"Príspevkové organizácie({stlpec.ToString()})";

            return rozpoctove;
        }

        private Column VratStlpecPrijmy(AssuViewAvailableColumns stlpec)
        {
            Debug.Assert(stlpec == AssuViewAvailableColumns.Skut || stlpec == AssuViewAvailableColumns.Rozpp || stlpec == AssuViewAvailableColumns.Rozpu);

            var condition = new Equals(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.FKod2), "  ");
            var rozpoctove = AssuView.VratStlpec(stlpec);
            rozpoctove.AddFunction(new ConditionalSum(condition));
            rozpoctove.ColumnAlias = $"Celkové príjmy({stlpec.ToString()})";

            return rozpoctove;
        }

        private Column VratStlpecVydavky(AssuViewAvailableColumns stlpec)
        {
            Debug.Assert(stlpec == AssuViewAvailableColumns.Skut || stlpec == AssuViewAvailableColumns.Rozpp || stlpec == AssuViewAvailableColumns.Rozpu);

            var condition = new Equals(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.FKod2), "  ") { Negate = true };
            var rozpoctove = AssuView.VratStlpec(stlpec);
            rozpoctove.AddFunction(new ConditionalSum(condition));
            rozpoctove.ColumnAlias = $"Celkové výdavky({stlpec.ToString()})";

            return rozpoctove;
        }

        private IEnumerable<Column> VratStlpceKraje(AssuViewAvailableColumns stlpec)
        {
            Debug.Assert(stlpec == AssuViewAvailableColumns.Skut || stlpec == AssuViewAvailableColumns.Rozpp || stlpec == AssuViewAvailableColumns.Rozpu);

            var columns = new List<Column>();
            for (var i = 1; i < 9; i++)
            {
                var krajKod = int.Parse($"{i}00");

                columns.Add(AssuView.VratStlpec(stlpec)
                    .AddFunction(new ConditionalSum(
                        new Equals(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.KrajKod, true, $"Kraj {krajKod}"), krajKod))));
            }
            return columns;
        }

        private void dataGridViewData_MouseMove(object sender, MouseEventArgs e)
        {
            dataGridViewData.Invalidate();
        }

        private async void comboBoxSumColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_manager is null)
                return;

            var labels = new Label[] { labelRO, label1PO, labelPP, labelVV, labelBA, labelTT, labelTN, labelNR, labelZA, labelBB, labelPR, labelKE };

            for (var i = 0; i < labels.Length; i++)
                labels[i].Text = "-";

            comboBoxSumColumn.Enabled = false;

            try
            {
                var statsData = await _manager.MSSQLManager.SelectData(GetCommandsForStats());
                // Rok, Rozpoctove, Prispevkove, Prijmy, Vydavky, Kraje (101 - 108)
                
                for (var i = 0; i < labels.Length; i++)
                    labels[i].Text = statsData.ElementAt(0).Values[i].ToString();
            }
            finally
            {
                comboBoxSumColumn.Enabled = true;
            }
        }

        public void ExportData()
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    cvti.isef.winformapp.Helpers.ExcelUtilities.ExportDataGridData(dataGridViewData, saveFileDialog.FileName);
                    MessageBox.Show("Údaje úspešne exportované do " + saveFileDialog.FileName, "Export úspešný", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Pri pokuse o export údajov nastala neočakávaná chyba. " + ex.Message, "Chyba " + ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new System.Drawing.Pen(new SolidBrush(Color.Gray), 1.0f),
                new Rectangle(new Point(tableLayoutPanel1.Location.X - 1, tableLayoutPanel1.Location.Y - 1),
                new Size(flowLayoutPanel1.Width + 2, flowLayoutPanel1.Height + 2)));
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
