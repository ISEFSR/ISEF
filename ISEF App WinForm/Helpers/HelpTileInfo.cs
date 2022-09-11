using System.Drawing;

namespace cvti.isef.winformapp.Controls.Main.Import
{
    public class HelpTileInfo
    {
        public HelpTileInfo(string title, string info1, Image image)
        {
            Title = title ?? throw new System.ArgumentNullException(nameof(title));
            Info1 = info1 ?? throw new System.ArgumentNullException(nameof(info1));
            InfoImage = image;
        }

        public string Title { get; private set; }
        public string Info1 { get; internal set; }
        public Image InfoImage { get; private set; }
    }
}
