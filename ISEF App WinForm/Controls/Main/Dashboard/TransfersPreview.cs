namespace cvti.isef.winformapp.Controls.Main.Dashboard
{
    using cvti.data;
    using cvti.data.Tables;
    using System;
    using System.Linq;
    using System.Drawing;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class TransfersPreview : UserControl, IDashboardActivableTile, IYearDependant
    {
        public event EventHandler SetTransfertsClicked;

        public TransfersPreview()
        {
            InitializeComponent();
        }

        private bool _isDrawingShadow = true;
        public bool IsDrawingShadow
        {
            get => _isDrawingShadow;
            set
            {
                _isDrawingShadow = value;
                Invalidate();
            }
        }

        private int _rok;
        public int Rok { get => _rok;
            set
            {
                _rok = value;
                labelSubtitle.Text = labelSubtitle.Tag.ToString().Replace("{rok}", _rok.ToString());
                labelTitle.Text = labelTitle.Tag.ToString().Replace("{rok}", _rok.ToString());
            }
        }

        public async Task LoadData(ISEFDataManager manager)
        {
            var transfers = await manager.MSSQLManager.Transfery.GetTransfers(Rok);

            flowLayoutPanelTransfers.Controls.Clear();

            pictureBox1.Visible = pictureBox1.Enabled =
                    linkLabel1.Visible = linkLabel1.Enabled = transfers.Count() == 0;

            if (pictureBox1.Visible)
            {
                return;
            }

            foreach (var t in transfers)
            {
                var ekonomicke = await manager.MSSQLManager.CiselnikyManager.VratCiselnikPreRokAsync<EkonomickaRiadok6>(_rok);
                var ek = (from e in ekonomicke where e.Kod == t.Polozka select e).First();

                var transferControl = new SingleTransferControl();
                var sum = await manager.MSSQLManager.VratTransfer(t);
                transferControl.Zobraz(ek, manager.VratStupen(t.FromStupen), manager.VratStupen(t.ToStupen), sum);
                flowLayoutPanelTransfers.Controls.Add(transferControl);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (IsDrawingShadow)
                DrawShadow(e.Graphics);
        }
        private void DrawShadow(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SetTransfertsClicked?.Invoke(this, EventArgs.Empty);
        }

        private void panelContent_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
