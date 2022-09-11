namespace cvti.isef.winformapp.Forms.Commands
{
    using cvti.data.Functions;
    using System;
    using System.Threading.Tasks;

    public partial class NewRoundFunctionForm : DialogBase
    {
        public NewRoundFunctionForm()
        {
            InitializeComponent();

            ShowImageIcon = false;

            ShowWait();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            await Task.Delay(1350);

            HideWait();
        }

        public override void HideWait()
        {
            base.HideWait();

            panelWrapper.Visible = true;
        }

        public override void ShowWait()
        {
            base.ShowWait();

            panelWrapper.Visible = false;
        }

        public Function GetSelectedFunction()
            => new Round((int)numericUpDownDecimalPlaces.Value);

        public int DecimalPlaces { get => (int)numericUpDownDecimalPlaces.Value; set => numericUpDownDecimalPlaces.Value = value; }
    }
}