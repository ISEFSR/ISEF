namespace cvti.isef.winformapp.Controls.Main.Dashboard
{
    using System;
    using System.Windows.Forms;
    using cvti.data.Views;
    using cvti.data.Tables;

    public partial class DashbardTilePreviewControl : UserControl
    {
        public DashbardTilePreviewControl()
        {
            InitializeComponent();
        }

        private void linkLabelBack_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            BackClicked?.Invoke(this, EventArgs.Empty);
        }

        public void ShowStats(StatsView stats)
        {

            if (stats is null)
            {
                labelCount.Text = "-";
                labelRozpp.Text = "-";
                labelRozpu.Text = "-";

                labelSkut.Text = "-";
                //labelVydavky.Text = "-";
                //labelPrijmy.Text = "-";

                label5.Text = string.Empty;
            }
            else
            {
                labelCount.Text = stats.TotalCount.ToString();
                labelRozpp.Text = stats.TotalSum.ToString("C");
                labelRozpu.Text = stats.TotalSum.ToString("C");

                labelSkut.Text = stats.TotalSum.ToString("C");
                //labelVydavky.Text = stats.Vydavky.ToString("C");
                //labelPrijmy.Text = stats.Prijmy.ToString("C");
                label5.Text = $"Stručné zhrnutie za stupeň {stats.StupenNazov}({stats.StupenSkratenyNazov}) a rok {stats.Rok}";
            }
        }

        public event EventHandler BackClicked;

        public int SelectedYear { get; set; }
    }
}
