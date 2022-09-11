namespace cvti.isef.winformapp.Forms.Conditions
{
    using cvti.data.Conditions;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public partial class ChooseConditionForm : DialogBase
    {
        public ChooseConditionForm(IEnumerable<Condition> conditions)
        {
            InitializeComponent();

            ShowWait();

            ShowImageIcon = false;

            foreach (var c in conditions)
                listBoxConditions.Items.Add(c);
        }

        public override void ShowWait()
        {
            base.ShowWait();

            panelPodmienka.Visible = false;
        }

        public override void HideWait()
        {
            base.HideWait();

            panelPodmienka.Visible = true;
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            await Task.Delay(1350);

            HideWait();
        }

        public Condition GetSelectedCondition()
            => listBoxConditions.SelectedItem as Condition;
    }
}
