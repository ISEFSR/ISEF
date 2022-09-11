namespace cvti.isef.winformapp.Forms
{
    using cvti.data;
    using cvti.data.Output;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class DataForm : DialogBase
    {
        private IEnumerable<DatovyRiadok> _data;
        private readonly SelectCommand _command;

        const int WaitTime = 2500;

        private readonly ISEFDataManager _manager;

        public DataForm(IEnumerable<DatovyRiadok> data, SelectCommand cmd, string title = null)
        {
            _data = data;
            _command = cmd;

            InitializeComponent();

            panelSelectCommand.BringToFront();

            if (!string.IsNullOrWhiteSpace(title))
                TitleText = title;

            ControlUtilities.SetDoubleBuffered(dataGridViewData);

            ShowWait();
        }

        public DataForm(ISEFDataManager manager, SelectCommand cmd)
        {
            _manager = manager;
            _command = cmd;

            InitializeComponent();

            ControlUtilities.SetDoubleBuffered(dataGridViewData);

            ShowWait();
        }

        public override void ShowWait()
        {
            base.ShowWait();

            toolStripActions.Visible =
                toolStripActions.Enabled =
                dataGridViewData.Visible =
                dataGridViewData.Enabled = false;
        }

        public override void HideWait()
        {
            base.HideWait();

            toolStripActions.Visible =
                toolStripActions.Enabled =
                dataGridViewData.Visible =
                dataGridViewData.Enabled = true;
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (_manager is null)
            {
                await Task.Delay(WaitTime);
            }
            else
            {
                var watch = new Stopwatch();
                watch.Start();

                _data = await _manager.MSSQLManager.SelectData(_command);

                watch.Stop();

                if (watch.ElapsedMilliseconds < WaitTime)
                    await Task.Delay(WaitTime - (int)watch.ElapsedMilliseconds);
            }

            textBoxSelectCommand.Text = _command.GenerateCommand(null).CommandText;
            //dataGridViewData.DataSource = _data;
            foreach (var c in _command.Columns)
            {
                var clmn = dataGridViewData.Columns[dataGridViewData.Columns.Add(c.ColumnAlias, c.ColumnAlias)];
                clmn.AutoSizeMode = c.IsNumeric ? DataGridViewAutoSizeColumnMode.AllCells : DataGridViewAutoSizeColumnMode.Fill;
            }

            foreach (var r in _data)
            {
                dataGridViewData.Rows.Add(r.Values);
            }
            //labelInfoText.Visible = true;
            HideWait();
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

        private void dataGridViewData_MouseMove(object sender, MouseEventArgs e)
        {
            dataGridViewData.Invalidate();
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {

        }

        private void textBoxSelectCommand_Leave(object sender, EventArgs e)
        {
            Hide();
        }

        private bool _running = false;
        private void toolStripButtonShowSQL_Click(object sender, EventArgs e)
        {
            if (_running)
                return;

            _running = true;

            panelSelectCommand.Visible = true;

            var transition = new Transitions.Transition(new Transitions.TransitionType_Deceleration(250));
            transition.add(panelSelectCommand, "Left", panelSelectCommand.Left - panelSelectCommand.Width);
            transition.TransitionCompletedEvent += (snd, ea) =>
            {
                _running = false;
            };

            textBoxSelectCommand.Focus();
            transition.run();
        }

        private void linkLabelClipboard_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetText(textBoxSelectCommand.Text);
            if (_running)
                return;

            _running = true;

            var transition = new Transitions.Transition(new Transitions.TransitionType_Deceleration(250));
            transition.add(panelSelectCommand, "Left", panelSelectCommand.Left + panelSelectCommand.Width);
            transition.TransitionCompletedEvent += (snd, ea) =>
            {
                _running = false;
            };


            transition.run();
        }

        private void toolStripButtonExportSelected_Click(object sender, EventArgs e)
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
                    MessageBox.Show("Pri pokuse o export údajov nastala neočakávaná chyba. " +ex.Message , "Chyba " + ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void panelSelectCommand_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(Brushes.LightGray, 2), new Point(0, 0), new Point(0, Height));
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
