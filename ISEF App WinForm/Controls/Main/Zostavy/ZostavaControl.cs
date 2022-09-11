namespace cvti.isef.winformapp.Controls.Main.Zostavy
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using cvti.data;
    using cvti.data.Core;
    using cvti.isef.winformapp.Helpers;
    using System.Drawing;
    using cvti.data.Output;
    using cvti.data.Conditions;

    /// <summary>
    /// Vizualuzacia jednej zostavy
    /// </summary>
    public partial class ZostavaControl : UserControlWithNotification
    {
        #region Constants, Variables And Constructor

        private const string LoaderText = "Načítavam potrebné údaje pre vybranú zostavu...";

        private ISEFDataManager _manager;
        private Zostava _zostava;

        // Kontrola pre event handlers ci maju vyk
        private bool _fireHandler = true;

        public ZostavaControl()
        {
            InitializeComponent();
            dataGridViewZostavaData.SetDoubleBuffered();
        }

        #endregion

        #region Public Methods

        public async Task ZobrazZostavu(ISEFDataManager manager, Zostava zostava)
        {
            _manager = manager;
            _zostava = zostava;

            ShowLoader(LoaderText);

            try
            {
                await FillComboBoxes();

                var dataTask = ShowData();

                ShowInfo();

                ShowCondition();

                ShowColumns();

                await dataTask;
            }
            finally
            {
                HideLoader();
            }
        }

        #endregion

        #region Private Methods

        private void ShowCondition()
        {

        }

        private void ShowColumns()
        {
            if (_zostava is null || _manager is null)
                return;

            listViewColumns.Items.Clear();
            foreach (var c in _zostava.Hlavicka.Data.Stlpce)
            {
                var item = new ListViewItem();
                item.Text = c.ColumnName;
                item.SubItems.Add(c.ColumnAlias);
                item.SubItems.Add(c.TableName);
                item.SubItems.Add(c.IsNumeric.ToString());
                item.SubItems.Add(c.IsVisible.ToString());
                item.SubItems.Add(c.ContainsAggregateFunction().ToString());
                item.SubItems.Add(c.Functions.Count.ToString());
                listViewColumns.Items.Add(item);
            }
        }

        private void ShowInfo()
        {
            labelHlavickaTitle.Text = _zostava.Nazov;

            textBoxName.Text = _zostava.Nazov;
            textBoxLeftTitle.Text = _zostava.LeftTitle;
            textBoxRightTitle.Text = _zostava.RightTitle;
            textBoxZatriedenie.Text = _zostava.ZaradenieZostavy;
        }

        private async Task ShowData()
        {
            if (!IsYearSelected())
                return;

            try
            {
                Enabled = false;   

                await Task.Delay(300);

                var data = await _manager.Export.ExportDataToTable(_zostava, GetSelectedYear(), GetSelectedCondtion());

                dataGridViewZostavaData.DataSource = data;

                for (var i = 0; i < _zostava.Hlavicka.Data.Stlpce.Count(); i++)
                {
                    if (dataGridViewZostavaData.Columns.Contains(_zostava.Hlavicka.Data.Stlpce.ElementAt(i).ColumnName))
                        dataGridViewZostavaData.Columns[_zostava.Hlavicka.Data.Stlpce.ElementAt(i).ColumnName].DisplayIndex = i;

                    if (_zostava.Hlavicka.Data.Stlpce.ElementAt(i).ContainsAggregateFunction())
                    {
                        dataGridViewZostavaData.Columns[i].DefaultCellStyle.Format = "C";
                        dataGridViewZostavaData.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                        dataGridViewZostavaData.Columns[i].ToolTipText = _zostava.Hlavicka.Data.Stlpce.ElementAt(i).ToString();
                    }
                    else
                    {
                        dataGridViewZostavaData.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    }
                }

                ShowTimedMessage(MessageType.Information, MessageLocation.Bottom,
                    "Údaje úspešne načítané", $"Údaje pre zostavu {_zostava.Nazov} úspešne načítané...");
            }
            catch (Exception ex)
            {
                var message = "Pri pokuse o zobrazenie zostavy nastala neočkakávaná cyhba, skúste prosím operáciu zopakovať. Ak problém pretrváva kontaktujte zodpovednú osobu. Chybové hlásenie: " + ex.Message;
                var title = "Zostava nezobrazená";
                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);

                await _manager.LogFrontendErrorSafeAsync(ex, "ZostavaControl.ZobrazZostavu");

                ShowPermanentMessage(MessageType.Error, MessageLocation.Top, title, message);
            }
            finally
            {
                Enabled = true;
            }
        }

        private async Task FillComboBoxes()
        {
            comboBoxCond.Items.Clear();
            comboBoxRok.Items.Clear();

            _fireHandler = false;

            foreach (var c in _manager.CoreFiles.Conditions.Values)
                comboBoxCond.Items.Add(c);

            foreach (var r in await _manager.MSSQLManager.GetAvailableYearsAsync())
                comboBoxRok.Items.Add(r);

            checkBoxCondition.Checked = false;
            comboBoxCond.SelectedIndex = -1;

            if (comboBoxRok.Items.Count > 0)
                comboBoxRok.SelectedIndex = 0;

            _fireHandler = true;
        }

        private bool IsYearSelected() => comboBoxRok.SelectedIndex != -1;

        private int GetSelectedYear() => int.Parse(comboBoxRok.SelectedItem.ToString());

        private Condition GetSelectedCondtion() => comboBoxCond.SelectedItem as Condition;

        public override void ShowLoader(string text)
        {
            base.ShowLoader(text);

            panelContentWrapper.Visible = false;
            panelContentWrapper.Enabled = false;
        }

        public override void HideLoader()
        {
            base.HideLoader();

            if (panelContentWrapper is null)
                return;

            panelContentWrapper.Visible = true;
            panelContentWrapper.Enabled = true;
        }

        #endregion

        #region Event Handlers

        private void dataGridViewZostavaData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewZostavaData_MouseMove(object sender, MouseEventArgs e)
        {
            dataGridViewZostavaData.Invalidate();
        }

        private void dataGridViewZostavaData_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            if (dataGridViewZostavaData.RectangleToScreen(e.RowBounds).Contains(MousePosition))
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

        private async void comboBoxRok_SelectedIndexChanged(object sender, EventArgs e)
        {
            await ShowData();
        }

        private async void comboBoxCond_SelectedIndexChanged(object sender, EventArgs e)
        {
            await ShowData();
        }

        private void checkBoxCondition_CheckedChanged(object sender, EventArgs e)
        {
            if (!_fireHandler)
                return;

            comboBoxCond.Enabled = checkBoxCondition.Checked;
            if (comboBoxCond.Enabled)
            {
                comboBoxCond.SelectedIndex = 0;
            }
            else
            {
                comboBoxCond.SelectedIndex = -1;
            }
        }

        private async void buttonExport_Click(object sender, EventArgs e)
        {
            if (_zostava is null)
                return;

            try
            {
                using (var sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Excel file (.xlsx)|*.xlsx";
                    sfd.Title = "Export údajov na základe zostavy";
                    sfd.FileName = $"output_{_zostava.Okruh}_{_zostava.Hlavicka.Name}";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        dataGridViewZostavaData.SaveToExcel(_zostava.Hlavicka, _zostava.Condition, sfd.FileName);

                        MessageBox.Show($"Zostava {_zostava.Nazov} úspešne vyexportovaná do {sfd.FileName}", "Zostava vyexportovaná", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ShowTimedMessage(MessageType.Information, MessageLocation.Top, "Export úspešný", $"Zostava {_zostava.Nazov} úspešne vyexportovaná do {sfd.FileName}");
                    }
                }
            }
            catch (Exception ex)
            {
                await _manager.LogFrontendErrorSafeAsync(ex, "ZostavaControl.buttonExport_Click");

                MessageBox.Show("Pri pokuse o export údajov zostavy nastala neočakávaná chyba. " + ex.Message,
                    "Chyba. " + ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);

                ShowTimedMessage(MessageType.Error, MessageLocation.Top, "Export zlyhal", "Pri pokuse o export údajov do XLSX súboru nastala neočakávaná chyba " + ex.Message);
            }
        }

        #endregion
    }
}
