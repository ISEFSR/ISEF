namespace cvti.isef.winformapp.Controls.Main.Hlavicky
{
    using System;
    using System.Linq;
    using System.Drawing;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using cvti.data;
    using cvti.data.Output;
    using System.Diagnostics;
    using cvti.isef.winformapp.Helpers;
    using cvti.data.Enums;
    using cvti.data.Views;
    using cvti.data.Conditions;
    using cvti.isef.winformapp.Forms;
    using cvti.data.Tables;
    using cvti.data.Core;

    /// <summary>
    /// Vizualna reprezentacia jedneho hlavickoveho suboru
    /// </summary>
    public partial class HlavickaControl : UserControlWithNotification
    {
        #region Constants, Variables And Constructor

        // Text pre laoder
        const string LoaderText = "Validuje a načítavam údaje pre hlavičku.";

        // Minimal load time pre hlavicku
        const int MinimalWaitTime = 1500;

        // Data manager zodpovedny za manipulaciu s datami
        private ISEFDataManager _manager;

        // Zobrazena hlavicka
        private Hlavicka _hlavicka;

        // Kontrola pre event handlers ci maju vyk
        private bool _fireHandler = true;

        public HlavickaControl()
        {
            InitializeComponent();
            ControlUtilities.SetDoubleBuffered(dataGridViewDataHlavicky);
        }

        #endregion

        #region Public Methods

        public override void ShowLoader(string text)
        {
            base.ShowLoader(text);

            if (panelContentWrapper is null)
                return;

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

        /// <summary>
        /// Zobrazi hlavicku pre manager
        /// </summary>
        /// <param name="manager">Datovy manager zodpovedny za manipulaciu s datami ako <see cref="ISEFDataManager"/></param>
        /// <param name="hlavicka">Hlavicka ktoru zorbazujem ako <see cref="Hlavicka"/></param>
        /// <exception cref="ArgumentNullException">V pripade ak je jeden z arguemntov null</exception>
        public async Task ZobrazHlavicku(ISEFDataManager manager, Hlavicka hlavicka)
        {
            // Zobrazim loader a schovam data
            ShowLoader(LoaderText);

            // Nastavim zobrazovanu hlavicku a manager
            _manager = manager ?? throw new ArgumentNullException(nameof(manager));
            _hlavicka = hlavicka ?? throw new ArgumentNullException(nameof(hlavicka));

            // Spustim stopky
            var watch = new Stopwatch();
            watch.Start();
            await Task.Delay(250);

            try
            {
                // nastavim nazov hlavicky pre title
                labelHlavickaTitle.Text = _hlavicka.Name;

                // Naplnim combo boxy ( segmenty, roky a podmienky )
                await FillComboBoxes();

                // Nacitam a zobrazim data pre hlavicku a vybranu podmieku
                await ShowData();

                // Zobrazim v lavom stlpci info 
                ShowInfo();

                // Zorbazim v lavom stlpci stlpce hlavicky
                ShowColumns();

                // Zobrazim v lavom stlpci zostavy
                ShowZostavy();
                
                watch.Stop();
                if (watch.ElapsedMilliseconds < MinimalWaitTime)
                    await Task.Delay(MinimalWaitTime - (int)watch.ElapsedMilliseconds);

                ShowTimedMessage(MessageType.Information, MessageLocation.Bottom, "Hlavička zobrazená",
                    "Údaje pre hlavičku úspešne načítane a dáta pre hlavičku zobrazené...");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pri pokuse o načitanie údajov pre hlavičku nastala neočakávaná chyba. Ak problém pretrváva kontaktujte prosím administrátora s chybovým hlásením" 
                    + ex.Message, "Chyba " + ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);

                ShowTimedMessage(MessageType.Error, MessageLocation.Top, "Zobrazenie zlyhalo",
                    "Pokus o zobrazenie informácií o hlavičkovom súbore a zobrazení údajov pre hlavičkový súbor. " + ex.Message);

                await _manager.LogFrontendErrorSafeAsync(ex, "HlavickaControl.ZobrazHlavicku");
            }
            finally
            {
                HideLoader();
            }
        }

        #endregion

        #region Private Methods

        private async Task FillComboBoxes()
        {
            comboBoxCond.Items.Clear();
            comboBoxRok.Items.Clear();
            comboBoxSegment.Items.Clear();

            _fireHandler = false;

            foreach (var s in _manager.Segmenty)
                comboBoxSegment.Items.Add(s);

            foreach (var c in _manager.CoreFiles.Conditions.Values)
                comboBoxCond.Items.Add(c);

            foreach (var r in await _manager.MSSQLManager.GetAvailableYearsAsync())
                comboBoxRok.Items.Add(r);

            checkBoxCondition.Checked = checkBoxSegment.Checked = false;
            comboBoxCond.SelectedIndex = comboBoxSegment.SelectedIndex = -1;

            if (comboBoxRok.Items.Count > 0)
                comboBoxRok.SelectedIndex = 0;

            _fireHandler = true;
        }

        private async Task ShowData()
        {
            if (comboBoxRok.Items.Count == 0 || comboBoxRok.SelectedIndex == -1)
                return;

            try
            {
                Enabled = false;
                var data = await _manager.Export.ExportDataToTable(_hlavicka, GetConditionFromComboBoxes());
                dataGridViewDataHlavicky.DataSource = data;

                for (var i = 0; i < _hlavicka.Data.Stlpce.Count(); i++)
                {
                    if (dataGridViewDataHlavicky.Columns.Contains(_hlavicka.Data.Stlpce.ElementAt(i).ColumnName))
                        dataGridViewDataHlavicky.Columns[_hlavicka.Data.Stlpce.ElementAt(i).ColumnName].DisplayIndex = i;

                    if (_hlavicka.Data.Stlpce.ElementAt(i).ContainsAggregateFunction())
                    {
                        dataGridViewDataHlavicky.Columns[i].DefaultCellStyle.Format = "C";
                        //dataGridViewDataHlavicky.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridViewDataHlavicky.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                    else
                    {
                        //dataGridViewDataHlavicky.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dataGridViewDataHlavicky.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    }
                }
            }
            catch (Exception ex)
            {
                await _manager.LogFrontendErrorSafeAsync(ex, "HlavickaControl.ShowData");

                ShowTimedMessage(MessageType.Error, MessageLocation.Bottom, "Načítanie zlyhalo",
                    "Pokus o načítanie dát pre hlavičkový súbor zlyhalo z neznámych dôvodov: " + ex.Message);
            }
            finally
            {
                Enabled = true;
            }
        }

        private void ShowZostavy()
        {
            if (_hlavicka is null || _manager is null)
                return;

            listViewZostavy.Items.Clear();
            foreach (var z in
                (from zs in _manager.CoreFiles.Zostavy.Values
                 where zs.Hlavicka.Name == _hlavicka.Name
                 select zs))
            {
                var item = new ListViewItem
                {
                    Text = z.Okruh.ToString()
                };
                item.SubItems.Add(z.Nazov);
                listViewZostavy.Items.Add(z.Nazov);
            }
        }

        private void ShowColumns()
        {
            if (_hlavicka is null || _manager is null)
                return;

            listViewColumns.Items.Clear();
            foreach (var c in _hlavicka.Data.Stlpce)
            {
                var item = new ListViewItem
                {
                    Text = c.ColumnName
                };
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
            if (_hlavicka is null || _manager is null)
                return;

            labelFullPath.Text = _hlavicka.FilePath;
            labelName.Text = _hlavicka.Name;
            labelType.Text = _hlavicka.Type.ToString();
            
            labelPodmienka.Text = _hlavicka.HlavickaCondition?.GetConditionString();
            labelRiadkyHlavicka.Text = _hlavicka.Data.RiadkyHlavicka.ToString();
            labelRiadkyStrana.Text = _hlavicka.Data.RiadkyStrana.ToString();
            labelStlpceHlavicka.Text = _hlavicka.Data.StlpceHlavicka.ToString();
            labelTextoveStlpce.Text = _hlavicka.Data.NesumStlpce.ToString();
        }

        private Condition GetConditionFromComboBoxes()
        {
            // Rok je vzdy povoleny 
            var cnd = new Equals(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.Rok), 
                Convert.ToInt32(comboBoxRok.SelectedItem));

            // Kontrolujem ci je vybrany segment
            if (checkBoxSegment.Checked && comboBoxSegment.SelectedIndex != -1)
                cnd.AddCondition(new Equals(string.Empty,
                    AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod),
                    (comboBoxSegment.SelectedItem as SegmentRiadok).Kod), ConditionOperator.And);

            // kontrolujem ci je vybrana podmienka
            if (checkBoxCondition.Checked && comboBoxCond.SelectedIndex != -1)
                cnd.AddCondition(comboBoxCond.SelectedItem as Condition, ConditionOperator.And);

            // vracim vytvorenu podmienku na zaklade roku semgnetu a dodatocenj podmienky
            return cnd;
        }

        #endregion

        #region Event Handlers (DataGridView)

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            if (dataGridViewDataHlavicky.RectangleToScreen(e.RowBounds).Contains(MousePosition))
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

        private void dataGridViewDataHlavicky_MouseMove(object sender, MouseEventArgs e)
        {
            dataGridViewDataHlavicky.Invalidate();
        }

        #endregion

        #region EventH Handlers (Buttons)

        private void buttonValidate_Click(object sender, EventArgs e)
        {
            var type = MessageType.Information;

            ShowPermanentMessage(MessageType.Error, MessageLocation.Top, "Hlavička zvalidovaná",
                "Pokus o načítanie dát pre hlavičkový súbor zlyhalo z neznámych dôvodov: ");
        }

        private async void buttonReloadData_Click(object sender, EventArgs e)
        {
            Enabled = false;
            Cursor = Cursors.WaitCursor;
            try
            {
                _hlavicka.ReloadData();
                ShowColumns();
                ShowInfo();
                await ShowData();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private async void buttonSaveCommand_Click(object sender, EventArgs e)
        {
            var saveWithCondition = MessageBox.Show("Prajete si SQL SELECT dotaz exportovať spoločne aj s podmienkou na základe rozbaľovacích okien?", 
                "Export s podmienkou", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            using (var cmdForm = new ComandNameForm(from z in _manager.CoreFiles.Zostavy.Values select z.Nazov, _hlavicka.Name))
            {
                if (cmdForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var cmd = _hlavicka.GetCommand();
                        cmd.CommandName = cmdForm.NewCommandname;
                        _manager.CoreFiles.Commands.AddValue(cmd);

                        MessageBox.Show("Nový SQL SELECT dotaz úspešne uložený pod menom " + cmd.CommandName, "Dotaz uložený", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ShowTimedMessage(MessageType.Information, MessageLocation.Top, "Dotaz uložený", "Nový SQL SELECT dotaz úspešne uložený pod menom " + cmd.CommandName + 
                            " Dotaz môžete nájsť v záložke generátor pod príkazmi.");
                    }
                    catch (Exception ex)
                    {
                        await _manager.LogFrontendErrorSafeAsync(ex, "HlavickaControl.buttonSaveCommand_Click");

                        MessageBox.Show("Pri pokuse o uloženie SQL SELECT dotazu z hlavičky nastala neočakávaná chyba " + ex.Message,
                            "Dotaz neuložený", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ShowTimedMessage(MessageType.Error, MessageLocation.Top, "Dotaz neuložený", "Pri pokuse o uloženie SQL SELECT dotazu z hlavičky nastala neočakávaná chyba " + ex.Message);
                    }
                }
            }
        }

        private void buttonShowData_Click_1(object sender, EventArgs e)
        {

        }

        private async void buttonExport_Click_1(object sender, EventArgs e)
        {
            if (_hlavicka is null)
                return;

            try
            {
                using (var saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel file (.xlsx)|*.xlsx";
                    saveFileDialog.Title = "Export údajov na základe hlavičky";
                    saveFileDialog.FileName = "output_" + _hlavicka.Name;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        dataGridViewDataHlavicky.SaveToExcel(_hlavicka, GetConditionFromComboBoxes(), saveFileDialog.FileName);

                        MessageBox.Show($"Zostava {_hlavicka.Name} úspešne vyexportovaná do {saveFileDialog.FileName}", "Zostava vyexportovaná", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ShowTimedMessage(MessageType.Information, MessageLocation.Top, "Export úspešný", $"Zostava {_hlavicka.Name} úspešne vyexportovaná do {saveFileDialog.FileName}");
                    }
                }
            }
            catch (Exception ex)
            {
                await _manager.LogFrontendErrorSafeAsync(ex, "HlavickyControl.toolStripButton1_Click");

                MessageBox.Show("Pri pokuse o export údajov hlavičky nastala neočakávaná chyba. " + ex.Message,
                    "Chyba. " + ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);

                ShowTimedMessage(MessageType.Error, MessageLocation.Top, "Export zlyhal", "Pri pokuse o export údajov do XLSX súboru nastala neočakávaná chyba " + ex.Message);
            }
        }

        #endregion

        #region Event Handlers (DataFilters)

        private void checkBoxSegment_CheckedChanged(object sender, EventArgs e)
        {
            if (!_fireHandler)
                return;

            comboBoxSegment.Enabled = checkBoxSegment.Checked;
            if (comboBoxSegment.Enabled)
            {
                comboBoxSegment.SelectedIndex = 0;
            }
            else
            {
                comboBoxSegment.SelectedIndex = -1;
            }
        }

        private void checkBoxRok_CheckedChanged(object sender, EventArgs e)
        {
            if (!_fireHandler)
                return;

            comboBoxRok.Enabled = checkBoxRok.Checked;
            if (comboBoxRok.Enabled)
            {
                comboBoxRok.SelectedIndex = 0;
            }
            else
            {
                comboBoxRok.SelectedIndex = -1;
            }
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

        private async void comboBoxSegment_SelectedIndexChanged(object sender, EventArgs e)
        {
            await ShowData();
        }

        private async void comboBoxRok_SelectedIndexChanged(object sender, EventArgs e)
        {
            await ShowData();
        }

        private async void comboBoxCond_SelectedIndexChanged(object sender, EventArgs e)
        {
            await ShowData();
        }

        #endregion
    }
}