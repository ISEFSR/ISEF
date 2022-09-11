namespace cvti.isef.winformapp.Forms
{
    using cvti.data.Tables;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class PridajTransferovuPolozkuForm : DialogBase
    {
        public PridajTransferovuPolozkuForm(
            IEnumerable<EkonomickaRiadok6> ekonomickePolozky, 
            IEnumerable<StupenRiadok> stupne)
        {
            InitializeComponent();

            ShowWait();

            if (ekonomickePolozky is null || ekonomickePolozky.Count() == 0 ||
                stupne is null || stupne.Count() == 0)
            {
                MessageBox.Show("Pre pridanie novej trasnferovej položky je potrebné poskytnúť dostupné ekonomické položky a stupne",
                    "Neúplné dáta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.Cancel;
            }

            foreach (var ep in ekonomickePolozky)
                comboBoxEkonomickaPolozka.Items.Add(ep);

            foreach (var s in stupne)
            {
                comboBoxFrom.Items.Add(s);
                comboBoxTo.Items.Add(s);
            }
        }

        public override void ShowWait()
        {
            base.ShowWait();
            tableLayoutPanel1.Visible = false;
            label1.Visible = false;
        }

        public override void HideWait()
        {
            base.HideWait();

            tableLayoutPanel1.Visible = true;
            label1.Visible = true;
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            await Task.Delay(1350);

            HideWait();
        }

        public EkonomickaRiadok6 VybranaEkonomickaPolozka 
        {
            get
            {
                return comboBoxEkonomickaPolozka.SelectedItem as EkonomickaRiadok6;
            }
        }

        public StupenRiadok Odkial
        {
            get
            {
                return comboBoxFrom.SelectedItem as StupenRiadok;
            }
        }

        public StupenRiadok Kam
        {
            get
            {
                return comboBoxTo.SelectedItem as StupenRiadok;
            }
        }
    }
}
