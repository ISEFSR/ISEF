namespace cvti.isef.winformapp.Controls.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class TileControl : UserControl
    {
        public TileControl()
        {
            InitializeComponent();
        }

        public Color ContentBorderColor { get; set; } = Color.FromArgb(210, 210, 210);

        public float ContentBorderWidth { get; set; } = 1.5f;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.DrawRectangle(new Pen(new SolidBrush(ContentBorderColor), ContentBorderWidth),
                panelContent.Location.X - ContentBorderWidth,
                panelContent.Location.Y - ContentBorderWidth, 
                panelContent.Width + ContentBorderWidth,
                panelContent.Height + ContentBorderWidth);
        }
    }
}