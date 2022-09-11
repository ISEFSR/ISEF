namespace cvti.isef.winformapp.Forms.Commands
{
    using cvti.data.Conditions;
    using cvti.data.Functions;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public partial class NewConditionalSumFunctionForm : DialogBase
    {
        public NewConditionalSumFunctionForm(IEnumerable<Condition> conditions)
        {
            InitializeComponent();

            ShowImageIcon = false;

            ShowWait();

            foreach (var c in conditions)
                listBoxConditions.Items.Add(c);
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            await Task.Delay(1350);

            HideWait();
        }

        public override void ShowWait()
        {
            base.ShowWait();

            panelConditions.Visible = false;
        }

        public override void HideWait()
        {
            base.HideWait();

            panelConditions.Visible = true;
        }

        private Condition GetSelectedCondition()
            => listBoxConditions.SelectedItem as Condition;

        public Function GetSelectedFunction()
            => new ConditionalSum(GetSelectedCondition());
    }
}
