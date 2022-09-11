namespace cvti.isef.winformapp.Controls.Main.Dashboard
{
    using System.Drawing;
    using System.Windows.Forms;

    public partial class DashboardBase : UserControl
    {
        public DashboardBase()
        {
            InitializeComponent();
        }

        public void SetMargin(int margin)
        {
            panelControlsWrapper.Margin = new Padding(margin);

            panelControlsWrapper.Location = new Point(margin, margin);
            panelControlsWrapper.Size = new Size(Width - (2 * margin),
                Height - (2 * margin));

            Invalidate();
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
                    new Point(panelControlsWrapper.Location.X + panelControlsWrapper.Width + i, panelControlsWrapper.Location.Y + panelControlsWrapper.Height + i - 1),
                    new Point(panelControlsWrapper.Location.X + panelControlsWrapper.Width + i, panelControlsWrapper.Location.Y + i));
                // Bottom shadow
                g.DrawLine(new Pen(Color.FromArgb(140, 140, 140), 1.0f),
                    new Point(panelControlsWrapper.Location.X + i, panelControlsWrapper.Location.Y + panelControlsWrapper.Height + i),
                    new Point(panelControlsWrapper.Location.X + panelControlsWrapper.Width + i, panelControlsWrapper.Location.Y + panelControlsWrapper.Height + i));
            }
        }
    }
}
