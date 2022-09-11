namespace cvti.isef.winformapp.Forms.Conditions
{
    using cvti.data.Files;
    using System;

    public partial class SaveConditionAsForm : DialogBase
    {
        ConditionsManagerJson _conditionsManager;

        public SaveConditionAsForm(ConditionsManagerJson conditionsManager)
        {
            InitializeComponent();
            _conditionsManager = conditionsManager;
        }

        public string ConditionName
        {
            get { return textBoxName.Text; }
            set { textBoxName.Text = value; }
        }

        protected override bool OnConfirmClicked(object sender, EventArgs e)
        {
            if (_conditionsManager.GetValue(textBoxName.Text) != null)
            {
                errorProvider.SetError(textBoxName, "Podmienka s vybraným menom už existuje...");
                return false;
            }

            return base.OnConfirmClicked(sender, e);
        }
    }
}
