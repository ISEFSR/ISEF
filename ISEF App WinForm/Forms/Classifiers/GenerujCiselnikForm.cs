namespace cvti.isef.winformapp.Forms
{
    using cvti.data.Enums;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    /// <summary>
    /// Umozni generovat cislenik napriek tomu, ze pre dany rok este neexistuju data
    /// </summary>
    public partial class GenerujCiselnikForm : DialogBase
    {
        public GenerujCiselnikForm(AnalytickaEvidencia ciselnik, int rok, IEnumerable<int> availableYears)
        {
            InitializeComponent();

            ShowWait();

            ShowImageIcon = false;

            TitleText = TitleText.Replace(
                "{cl}", ciselnik.ToString()).Replace("{rok}", rok.ToString());

            foreach (var y in availableYears)
                comboBoxRok.Items.Add(y);

            if (comboBoxRok.Items.Count == 0)
                radioButton3.Enabled = false;
        }

        public override void ShowWait()
        {
            base.ShowWait();

            tableLayoutPanel1.Visible = false;
        }

        public override void HideWait()
        {
            base.HideWait();

            tableLayoutPanel1.Visible = true;
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            await Task.Delay(1350);

            HideWait();
        }

        public bool FromDefault { get => radioButton2.Checked; }
        public bool FromYear { get => radioButton3.Checked; }

        public int SelectedYear { get => Convert.ToInt32(comboBoxRok.SelectedItem); }

        private void radioButton3_CheckedChanged(object sender, System.EventArgs e)
        {
            comboBoxRok.Enabled = radioButton3.Checked;
        }
    }
}
