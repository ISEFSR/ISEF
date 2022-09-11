namespace cvti.isef.winformapp.Controls.Main.Dashboard
{
    using System;
    using System.Drawing;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using cvti.data;
    using cvti.data.Core;
    using cvti.data.Enums;

    public partial class DashboardTileControl : UserControl, 
        IDashboardActivableTile, IYearDependant
    {
        public DashboardTileControl()
        {
            InitializeComponent();
        }

        public string TitleText { get { return labelTitle.Text; } set { labelTitle.Text = value; } }
        public string InfoText { get { return labelInfo.Text; } set { labelInfo.Text = value; } }
        public Image TitleImage { get { return panelTitle.BackgroundImage; } set { panelTitle.BackgroundImage = value; } }
        public Color TileColor { get { return panelColorLine.BackColor; } set { panelColorLine.BackColor = labelTitle.ForeColor = value; } }

        public int Rok { get; set; } = DateTime.Now.Year;
        public InputType Stupen { get; set; }

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

        public async Task LoadData(ISEFDataManager manager)
        {
            //Rok = rok;
            //await Task.Delay(650);
            var stats = await manager.GetStats(Rok, Stupen);
            dashbardTilePreviewControl.ShowStats(stats);
        }

        public async Task LoadData(ISEFDataManager manager, int rok)
        {
            Rok = rok;
            //await Task.Delay(650);
            var stats = await manager.GetStats(Rok, Stupen);
            dashbardTilePreviewControl.ShowStats(stats);
        }

        private void SwitchControls(Control show, Control hide)
        {
            hide.Enabled = false;
            show.Location = new Point(show.Width, show.Location.Y);
            show.Size = hide.Size;  

            var t = new Transitions.Transition(new Transitions.TransitionType_EaseInEaseOut(450));
            tableLayoutPanelPreview.Dock = DockStyle.None;

            t.add(hide, "Left", -hide.Width);
            t.add(show, "Left", 0);

            t.TransitionCompletedEvent += (snd, ea) =>
            {
                show.Invoke(new Action(() => { show.Enabled = true; }));
            };

            t.run();
        }

        public event EventHandler<LinkLabelLinkClickedEventArgs> DataImportLinkLabelClicked;
        public event EventHandler ShowDataClicked;

        private void dashbardTilePreviewControl_BackClicked(object sender, EventArgs e) => SwitchControls(tableLayoutPanelPreview, dashbardTilePreviewControl);
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => ShowDataClicked?.Invoke(this, EventArgs.Empty);
        private void linkLabelImport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => DataImportLinkLabelClicked?.Invoke(this, e);
        private void linkLabelDataImport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => DataImportLinkLabelClicked?.Invoke(this, e);
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => SwitchControls(dashbardTilePreviewControl, tableLayoutPanelPreview);
    }
}