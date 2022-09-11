namespace cvti.isef.winformapp.Controls.Output
{
    using System;
    using System.Drawing;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using cvti.data.Core;
    using cvti.data;
    using System.Diagnostics;
    using cvti.data.Output;
    using cvti.data.Conditions;

    /// <summary>
    /// Predstavuje jednu exportovanu zostavu 
    /// </summary>
    /// <remarks>
    /// Export zostavy pozostava zo styrcoh krokov
    /// Obdrzanie dat - obdrzanie dat z MSSQL serveru
    /// Skonvertovanie dat - konvertovanie dat do finalnej podoby pre export
    /// Nastylovanie dat - nastylovnie worksheetu
    /// Vyexportovanie zostavy - kompletne vyexportovanie upravenych dayt
    /// </remarks>
    public partial class ZostavaOutputControl : UserControl
    {
        internal class ExportStatus
        {
            public bool DataObtained { get; set; } = false;
            public bool DataConverter { get; set; } = false;
            public bool DataStylingDone { get; set; } = false;
            public bool ZostavaExported { get; set; } = false;
        }

        private ISEFDataManager _manager;
        private Zostava _zostava;

        internal ExportStatus ZostavaStatus { get; private set; }
            = new ExportStatus();

        private bool _running = false;

        private Color _back;

        private bool _animating = false;
        private int _coreHeight;
        private int _expandedHeight = 225;

        public ZostavaOutputControl()
        {
            InitializeComponent();

            foreach (Control c in Controls)
            {
                c.MouseEnter += C_MouseEnter;
                c.MouseLeave += C_MouseLeave;
            }
        }

        /// <summary>
        /// Nastavi manager a spracuvanu zostavu
        /// </summary>
        /// <param name="maager">DAtovy manager ako <see cref="ISEFDataManager"/></param>
        /// <param name="zostava">Spracovana zostava ako <see cref="Zostava"/></param>
        public void SetZostava(ISEFDataManager maager, Zostava zostava)
        {
            _manager = maager;
            _zostava = zostava;

            _manager.Output.DataObtained += Output_DataObtained;
            _manager.Output.DataConverted += Output_DataConverted;
            _manager.Output.DataStylingDone += Output_DataStylingDone;
            _manager.Output.ZostavaExported += Output_ZostavaExported;

            _manager.Output.CreationFailed += Output_CreationFailed;

            labelName.Text = _zostava.Nazov;
            labelType.Text = _zostava.Hlavicka.Type.ToString();
        }

        /// <summary>
        /// Spusti export zostavy pre vybrany rok
        /// </summary>
        /// <param name="year">Rok pre ktory exportujem data</param>
        /// <returns>False ak export uz bezi, inak true</returns>
        public async Task<bool> Run(int year, bool hlavicka, Condition condition)
        {
            // ak uz bezi tak vraciam false
            if (_running )
                return false;

            _running = true;

            // natavim image icon na working image icon
            pictureBoxStatus.Image = Properties.Resources.loading1;

            // spustim export
            var path = await _manager.Output.ExportZostavaSafe(_zostava, year, hlavicka, condition);

            // nastavim finalnu cestu k vystupnemu suboru ak sa mi ho podari vyexportovat
            linkLabelPath.Text = path;

            // ukoncim beh
            _running = false;

            // vratim true
            return true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _back = BackColor;
            _coreHeight = Height;
        }

        private void C_MouseLeave(object sender, EventArgs e)
        {
            BackColor = _back;
        }

        private void C_MouseEnter(object sender, EventArgs e)
        {
            BackColor = SystemColors.Control;
        }

        private async void Output_CreationFailed(object sender, Tuple<Zostava, Exception> e)
        {
            if (e.Item1.Nazov != _zostava.Nazov)
                return;

            await _manager.LogFrontendErrorSafeAsync(e.Item2, "ZostavaOutputControl.Output_CreationFailed");

            pictureBoxStatus.Image = Properties.Resources.error20;

            labelErrorMessage.Text = e.Item2.GetType().ToString() + ": " + e.Item2.Message;
            labelStackTrace.Text = e.Item2.StackTrace;

            tableLayoutPanelInfo.Visible = false;

            tableLayoutPanelError.Visible = true;
            tableLayoutPanelError.BringToFront();
        }

        private void Output_ZostavaExported(object sender, Zostava e)
        {
            if (e.Nazov != _zostava.Nazov)
                return;

            ZostavaStatus.ZostavaExported = true;

            labelExported.ForeColor = Color.Green;
            labelExportedTime.ForeColor = Color.DarkGreen;

            pictureBoxStatus.Image = Properties.Resources.ok30;

            labelExportedTime.Text = DateTime.Now.ToString();
            linkLabelPath.Visible = true;
            //linkLabelPath.Text = 
        }

        private void Output_DataStylingDone(object sender, Zostava e)
        {
            if (e.Nazov != _zostava.Nazov)
                return;

            ZostavaStatus.DataStylingDone = true;

            labelStyle.ForeColor = Color.Green;
            labelStyeTime.ForeColor = Color.DarkGreen;

            labelStyeTime.Text = DateTime.Now.ToString();
        }

        private void Output_DataConverted(object sender, Zostava e)
        {
            if (e.Nazov != _zostava.Nazov)
                return;

            ZostavaStatus.DataConverter = true;

            labelSkonvertovane.ForeColor = Color.Green;
            labelSkonvertovaneTime.ForeColor = Color.DarkGreen;

            labelSkonvertovaneTime.Text = DateTime.Now.ToString();
        }

        private void Output_DataObtained(object sender, Zostava e)
        {
            if (e.Nazov != _zostava.Nazov)
                return;

            ZostavaStatus.DataObtained = true;

            labelZiskane.ForeColor = Color.Green;
            labelZiskaneTime.ForeColor = Color.DarkGreen;

            labelZiskaneTime.Text = DateTime.Now.ToString();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            C_MouseEnter(this, e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            C_MouseLeave(this, e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.DrawRectangle(new Pen(Brushes.LightGray), 
                new Rectangle(new Point(0, 0), new Size(Width - 1, Height -1 )));
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            Invalidate();
        }

        private void buttonRoll_Click(object sender, EventArgs e)
        {
            if (_animating)
                return;

            _animating = true;

            var targetHeight = Height == _expandedHeight ? _coreHeight : _expandedHeight;
            var targetText = Height == _expandedHeight ? "v" : "^";

            var transition = new Transitions.Transition(new Transitions.TransitionType_Acceleration(250));
            transition.add(this, "Height", targetHeight);

            transition.TransitionCompletedEvent += (snd, ea) =>
            {
                _animating = false;
            };

            transition.run();

            buttonRoll.Text = targetText;
        }

        internal void Expand()
        {
            if (Height == _coreHeight)
            {
                buttonRoll_Click(this, EventArgs.Empty);
            }
        }

        internal void Collapse()
        {
            if (Height == _coreHeight)
                return;

            buttonRoll_Click(this, EventArgs.Empty);
        }

        private void pictureBoxStatus_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanelInfo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabelPath_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(linkLabelPath.Text);
        }
    }
}
