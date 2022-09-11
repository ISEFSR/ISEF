namespace cvti.isef.winformapp.Components
{
    using cvti.data.Output;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class HlavickaItem
    {
        public HlavickaItem(Hlavicka hlavicka)
        {
            Hlavicka = hlavicka;
        }

        public Hlavicka Hlavicka { get; private set; }
    }

    public partial class HlavickyListBox : ListBox
    {
        public HlavickyListBox()
        {
            InitializeComponent();
        }

        public HlavickyListBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private void HlavickyListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            Rectangle rect = e.Bounds;
            rect.Offset(0, -rect.Top);
            using (Bitmap bitmap = new Bitmap(rect.Width, rect.Height))
            {
                var item = Items[e.Index] as Hlavicka;
                if (item != null)
                {
                    Control control = (Control)Items[e.Index];
                    control.DrawToBitmap(bitmap, rect);
                    rect = e.Bounds;
                    e.Graphics.DrawImage(bitmap, e.Bounds);
                }
            }
        }
    }
}
