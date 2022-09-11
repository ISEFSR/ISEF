namespace cvti.isef.winformapp.Forms
{
    using cvti.data;
    using cvti.data.Classifiers;
    using cvti.data.Tables;
    using System.ComponentModel;

    public partial class NovaOrganizaciaForm : DialogBase
    {
        public NovaOrganizaciaForm(ISEFDataManager manager, NovaOrganizacia org)
        {
            InitializeComponent();

            ShowImageIcon = false;

            textBox1.Text = org.ICO;
            textBox2.Text = org.Nazov;

            comboBox1.DataSource = manager.Segmenty;
            comboBox2.DataSource = manager.Stupne;
            comboBox3.DataSource = manager.Obce;
        }

        [Browsable(false)]
        public SegmentRiadok VybranySegment { get => comboBox1.SelectedItem as SegmentRiadok; }

        [Browsable(false)]
        public StupenRiadok VybranyStupen { get => comboBox2.SelectedItem as StupenRiadok; }

        [Browsable(false)]
        public ObecRiadok VybranaObec { get => comboBox3.SelectedItem as ObecRiadok; }

        [Browsable(false)]
        public string VybranyNazov { get => textBox2.Text; }
    }
}
