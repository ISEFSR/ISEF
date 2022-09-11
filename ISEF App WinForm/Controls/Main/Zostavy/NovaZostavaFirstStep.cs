namespace cvti.isef.winformapp.Controls.Main.Zostavy
{
    using System.Collections.Generic;
    using System.Windows.Forms;
    using cvti.data.Output;

    public partial class NovaZostavaFirstStep : UserControl
    {
        public NovaZostavaFirstStep()
        {
            InitializeComponent();
        }

        public void ZobrazDostupneHlavicky(IEnumerable<Hlavicka> hlavicky)
        {
            listViewHlavicky.Items.Clear();

            foreach (var h in hlavicky)
            {
                var item = new ListViewItem();
                item.Text = h.Name;
                item.Tag = h;
                item.ImageIndex = 0;
                item.SubItems.Add(h.Type.ToString());
                item.SubItems.Add(h.Data.RiadkyStrana.ToString());
                item.SubItems.Add(h.Data.RiadkyHlavicka.ToString());
                item.SubItems.Add(h.Data.StlpceHlavicka.ToString());
                item.SubItems.Add(h.Data.NesumStlpce.ToString());

                listViewHlavicky.Items.Add(item);
            }
        }

        public Hlavicka VybranaHlavicka
        {
            get
            {
                if (listViewHlavicky.SelectedItems.Count == 0)
                    return null;

                return listViewHlavicky.SelectedItems[0].Tag as Hlavicka;
            }
        }
    }
}