namespace cvti.isef.winformapp.Forms.Commands
{
    using cvti.data.Functions;
    using System;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class NewSubstringFunctionForm : DialogBase
    {
        public NewSubstringFunctionForm()
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
            => new Substring((int)numericUpDownStartPosition.Value, (int)numericUpDownPlaces.Value);
    }
}
