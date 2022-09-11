using System.Drawing;

namespace cvti.isef.winformapp.Controls.Main.Dashboard
{
    public class WelcomeBoardItem
    {
        public string Title { get; set; }
        public string InfoText { get; set; }

        public Image BackgroundImage { get; set; }
        public Image SmallImage { get; set; }

        public Color ForegroundColor { get; set; } = Color.FromArgb(64, 64, 64);
        public Color BackgroundColor { get; set; } = Color.White;
    }
}
