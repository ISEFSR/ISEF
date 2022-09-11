 namespace cvti.isef.winformapp.Forms
{
    using cvti.data.Enums;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Formular na update ciselniku 
    /// </summary>
    /// <remarks>
    /// Update mozem prebehnut bud z predchadzjuceho roku, alebo z defaultnych dat
    /// </remarks>
    public partial class AktualizujCiselnikForm : DialogBase
    {
        public AktualizujCiselnikForm(AnalytickaEvidencia ciselnik, int rok, IEnumerable<int> availableYears)
        {
            InitializeComponent();

            ShowWait();

            TitleText = TitleText.Replace(
                "{cl}", ciselnik.ToString()).Replace("{rok}", rok.ToString());

            ShowImageIcon = false;

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

        public int SelectedYear 
        {
            get
            {
                if (comboBoxRok.SelectedIndex == -1)
                    return -1;
                return Convert.ToInt32(comboBoxRok.SelectedItem);
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxRok.Enabled = radioButton3.Checked;
            if (!comboBoxRok.Enabled)
                comboBoxRok.SelectedIndex = -1;
        }

        private void tableLayoutPanel1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

        }
    }
}
