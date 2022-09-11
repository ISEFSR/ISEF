namespace cvti.isef.winformapp.Controls.Main
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Data;
    using System.Linq;
    using OfficeOpenXml;
    using System.IO;

    using cvti.data;
    using cvti.data.Enums;
    using cvti.data.Output;
    using cvti.isef.winformapp.Helpers;
    using cvti.isef.winformapp.Controls.Main.Data;
    using cvti.data.Views;
    using cvti.data.Functions;
    using cvti.isef.winformapp.Forms;
    using cvti.data.Conditions;
    using cvti.data.Columns;
    using cvti.isef.winformapp.Forms.Commands;
    using cvti.isef.winformapp.Forms.Conditions;
    using System.Threading;

    public partial class DataPreviewControl : UserControlWithNotification, IMainTabControl
    {
        private readonly BindingSource _bindingSource
            = new BindingSource();

        public DataPreviewControl()
        {
            InitializeComponent();

            dataGridViewData.DataSource = _bindingSource;
            dataGridViewData.SetDoubleBuffered();
        }

        #region IMainTabControl Members

        private ISEFDataManager _manager;
        public ISEFDataManager DataManager { get => _manager; set => _manager = value; }

        public string TitleText { get; set; } = Properties.Resources.DataTitle;
        public string InfoText { get; set; } = Properties.Resources.DataInfo;
        public Image TitleImage { get; set; } = Properties.Resources.data_white_100;
        public Image BackImage { get; set; } = null;

        public async Task Activate(ISEFDataManager manager, InputType type, int year)
        {
            _manager = manager ?? throw new ArgumentNullException(nameof(manager));

            if (selectCommandControl.Command is null || selectCommandControl.Command.Columns.Count() == 0)
                await ShowDataForInputType(type, year);
        }

        public async Task Activate(ISEFDataManager manager) => await Activate(manager, InputType.MaO, 2020);
        public void Deactivate()
        { 
            //
        }

        #endregion

        public Stack<Condition> Conditions { get; private set; }
            = new Stack<Condition>();

        private void selectCommandControl_CommandChanged(object sender, EventArgs e)
        {
            LoadColumns();
        }

        private void selectCommandControl_AliasChanged(object sender, AliasChangedEventArgs e)
        {
            var indexer = 0;
            foreach (var c in selectCommandControl.Command.Columns)
            {
                if (dataGridViewData.Columns[indexer].HeaderText == e.PreviousAlias)
                {
                    dataGridViewData.Columns[indexer].HeaderText = e.Alias;
                }
                indexer++;
            }
        }

        private void dataGridViewObceData_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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

        private void dataGridViewData_MouseMove(object sender, MouseEventArgs e)
        {
            dataGridViewData.Invalidate();
        }

        private async void dataGridViewData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex < 0) || (e.RowIndex < 0))
                return;

            var Value = dataGridViewData[e.ColumnIndex, e.RowIndex].Value;
            var cl = selectCommandControl.Command.Columns.ElementAt(e.ColumnIndex);
            if (cl.Functions.Count > 0)
            {
                foreach (var c in cl.Functions)
                {
                    if (c is Sum)
                        return;

                }
            }

            var condition = new Equals("name", cl, Value);
            if (selectCommandControl.Command.CommandCondition is null)
            {
                selectCommandControl.SetCondition(condition);
                Conditions.Push(condition);
            }
            else
            {
                selectCommandControl.Command.CommandCondition.AddCondition(condition, ConditionOperator.And);
                Conditions.Push(selectCommandControl.Command.CommandCondition);
            }
            await LoadData();
        }

        private async void backButton_Click(object sender, EventArgs e)
        {
            if (Conditions.Count <= 0)
                return;

            var condition = Conditions.Pop();
            if (selectCommandControl.Command.CommandCondition.InnerConditions.Count() > 0)
            {
                selectCommandControl.Command.CommandCondition.RemoveAt(selectCommandControl.Command.CommandCondition.InnerConditions.Count() - 1);
            }
            else
            {
                selectCommandControl.SetCondition(null);
            }

            await LoadData();
        }

        #region Event Handlers (ToolTip)

        private void toolStripButtonDataExport_Click(object sender, EventArgs e)
        {
            // TODO: rework as generic method that exports data from data grid
            // add SQL command into separate worksheet
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Title = "Ulož dáta";
                saveDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                saveDialog.FileName = $"export{DateTime.Now.ToString("ddMMyyHHmmss")}.xlsx";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    //license 
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    // TODO: create worbook 

                    using (var package = new ExcelPackage(new FileInfo(saveDialog.FileName)))
                    {
                        var workbookData = package.Workbook.Worksheets.Add("Data");
                        var workbookComand = package.Workbook.Worksheets.Add("SQLCommand");
                        // fill in data
                        var actualRow = 1;
                        var actualCol = 0;
                        foreach (DataGridViewColumn col in dataGridViewData.Columns)
                        {
                            actualCol++;
                            workbookData.Cells[actualRow, actualCol].Value = col.HeaderText;
                        }
                        actualCol = 0;
                        foreach (DataGridViewRow row in dataGridViewData.Rows)
                        {
                            actualRow++;

                            foreach (DataGridViewCell item in row.Cells)
                            {
                                actualCol++;
                                workbookData.Cells[actualRow, actualCol].Value = item.Value;
                            }
                            actualCol = 0;
                        }
                        workbookComand.Cells[1, 1].Value = selectCommandControl.Command.GenerateCommand().CommandText;
                        //FileInfo excelFile = ;
                        package.Save();
                    }
                }
            }
        }

        private void toolStripButtonNewColumn_Click(object sender, EventArgs e)
        {
            using (var newColumnForm = new NewColumnControl(_manager.CoreFiles.Conditions))
            {
                if (newColumnForm.ShowDialog() == DialogResult.OK)
                {
                    var c = newColumnForm.GetColumn();
                    selectCommandControl.AddColumn(c);
                }
            }
        }

        private async void toolStripButtonReload_Click(object sender, EventArgs e)
            => await LoadData();

        private async void toolStripButtonChooseCommand_Click(object sender, EventArgs e)
        {
            ChooseCommandForm form = new ChooseCommandForm(_manager.CoreFiles.Commands.Values);

            if (form.ShowDialog() == DialogResult.OK)
            {
                selectCommandControl.Command = form.GetSelectedCndition();
                await LoadData();
            }
        }

        private void toolStripButtonSaveCommand_Click(object sender, EventArgs e)
        {
            // TODO: allow user to save command as new command
            // 1. zobraz dialog kde si user vyberie meno pre command a dialog skontroluje ci je meno uniquue

            // 2 ak je dialog response ok
            var names = (from c in _manager.CoreFiles.Commands.Values select c.CommandName);


            //selectCommandControl.Command.CommandName
            //selectCommandControl.Command.CommandCondition
            //foreach (var c in selectCommandControl.Command.Columns ) { }
            // zobraz messsage box command usepesne pridany
            using (var comandNameForm = new ComandNameForm(names))
            {
                if (comandNameForm.ShowDialog() == DialogResult.OK)
                {
                    var command = selectCommandControl.Command;
                    command.CommandName = comandNameForm.NewCommandname;
                    _manager.CoreFiles.Commands.AddValue(command);

                    MessageBox.Show($"Príkaz úspešne uložený pod názvom: {command.CommandName}", "Príkaz uložený",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void toolStripButtonSaveCond_Click(object sender, EventArgs e)
        {
            var names = (from c in _manager.CoreFiles.Conditions.Values select c.ConditionName);

            using (var comandNameForm = new ComandNameForm(names))
            {
                if (comandNameForm.ShowDialog() == DialogResult.OK)
                {
                    var condition = selectCommandControl.Command.CommandCondition;
                    condition.ConditionName = comandNameForm.NewCommandname;
                    _manager.CoreFiles.Conditions.AddValue(condition);

                    MessageBox.Show($"Podmienka úspešne uložená pod názvom: {condition.ConditionName}", "Podmienka uložená", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private async void toolStripButtonPickCond_Click(object sender, EventArgs e)
        {
            ChooseConditionForm form = new ChooseConditionForm(_manager.CoreFiles.Conditions.Values);

            if (form.ShowDialog() == DialogResult.OK)
            {
                selectCommandControl.SetCondition(form.GetSelectedCondition());
                await LoadData();
            }
        }

        private async void toolStripButton1_Click(object sender, EventArgs e)
        {
            selectCommandControl.SetCondition(null);
            await LoadData();
        }

        #endregion

        private void LoadColumns()
        {
            var index = 0;
            foreach (var coll in selectCommandControl.Command.Columns)
            {
                dataGridViewData.Columns[coll.ColumnAlias].DisplayIndex = index;
                index++;

            }

            ShowCommandAndCondition();
        }

        private void ShowCommandAndCondition()
        {
            scintillaCommand.Text = selectCommandControl.Command.GenerateCommand().CommandText;
            scintillaCondition.Text = selectCommandControl.Command.CommandCondition?.GetConditionString(true);
        }

        public override void ShowLoader(string text)
        {
            base.ShowLoader(text);

            //Enabled = false;
            Cursor = Cursors.WaitCursor;

            if (toolStrip1 != null)
            {
                toolStripMenu.Enabled = false;
                tabControl1.Visible = false;
                selectCommandControl.Visible = false;

                toolStripButton3.Visible = true;
                toolStripButton3.Enabled = true;
            }
        }

        public override void HideLoader()
        {
            base.HideLoader();

            //Enabled = true;
            Cursor = Cursors.Default;

            if (toolStrip1 != null)
            {
                toolStripMenu.Enabled = true;
                tabControl1.Visible = true;
                selectCommandControl.Visible = true;

                toolStripButton3.Visible = false;
                toolStripButton3.Enabled = false;
            }
        }

        private async Task LoadData()
        {
            try
            {
                // Zobrazim loader
                ShowLoader("Načítavam dáta čakajte prosím...");

                // Bude toto stacit? 
                if (_bindingSource.DataSource != null && _bindingSource.DataSource is DataTable)
                    (_bindingSource.DataSource as DataTable).Dispose();

                // Ziskaj data na zaklade vybraneho commandu
                var data = await DataManager.MSSQLManager.SelectData(selectCommandControl.Command);

                // Nastav data source pre binding source na zaklade ziskanych dat
                _bindingSource.DataSource = CreateDataTable(data);

                // Nastav autosize mode pre stlpce v gride na zaklade zobrazenych dat
                for (var columnIndex = 0; columnIndex < dataGridViewData.Columns.Count; columnIndex++)
                    dataGridViewData.Columns[columnIndex].AutoSizeMode =
                        selectCommandControl.Command.Columns.ElementAt(columnIndex).IsNumeric ?
                        DataGridViewAutoSizeColumnMode.Fill :
                        DataGridViewAutoSizeColumnMode.DisplayedCells;

                ShowCommandAndCondition();
            }
            catch (Exception ex)
            {
                await _manager.LogFrontendErrorSafeAsync(ex, "DataPreviewControl.LoadData");
                MessageBox.Show("Pri pokuse o zobrazenie údajov nastala neočakávaná chyba. " + ex.Message, "Chyba " + ex.GetType().ToString(),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HideLoader();
            }
        }

        private DataTable CreateDataTable(IEnumerable<DatovyRiadok> data)
        {
            var table = new DataTable();
            var clmns = new HashSet<string>();
            var index = 0;

            foreach (var c in selectCommandControl.Command.Columns)
            {
                var clmnName = string.IsNullOrWhiteSpace(c.ColumnAlias) ? $"Column{index++}" : c.ColumnAlias;

                if (string.IsNullOrWhiteSpace(c.ColumnAlias))
                    c.ColumnAlias = clmnName;

                while (!clmns.Add(clmnName))
                    clmnName = "_" + clmnName;

                c.ColumnAlias = clmnName;

                var type = c.IsNumeric ? typeof(decimal) : typeof(string);
                var dataColumn = new DataColumn(clmnName, type);

                table.Columns.Add(dataColumn);
            }
            selectCommandControl.RefreshVisual();
            foreach (var r in data)
                table.Rows.Add(r.Values);

            return table;
        }

        private async Task ShowDataForInputType(InputType type, int year)
        {
            var command = new SelectCommand(string.Empty, new Column[] {
                AssuView.VratStlpec(AssuViewAvailableColumns.Rok), 
                AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod), 
                AssuView.VratStlpec(AssuViewAvailableColumns.StupenText),
                AssuView.VratStlpec(AssuViewAvailableColumns.FKod3),
                AssuView.VratStlpec(AssuViewAvailableColumns.Skut).AddFunction(new Sum()),
                AssuView.VratStlpec(AssuViewAvailableColumns.Rozpu).AddFunction(new Sum()),
                AssuView.VratStlpec(AssuViewAvailableColumns.Rozpp).AddFunction(new Sum())
            });

            command.CommandCondition = new Equals(String.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.Rok), year)
                .AddCondition(new Equals(String.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod), (char)type), ConditionOperator.And);

            selectCommandControl.Command = command;

            await LoadData();
        }

        private CancellationToken _token;

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            
        }
    }
}