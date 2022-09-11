namespace cvti.isef.winformapp.Forms
{
    using cvti.data.Conditions;
    using cvti.data.Core;
    using cvti.data.Enums;
    using System;
    using System.Diagnostics;
    using System.Windows.Forms;

    public partial class ChooseConditionOperator : DialogBase
    {
        private readonly Condition _righCondition;
        private readonly Condition _leftCondition;

        public ChooseConditionOperator(Condition leftHandSideCondition, Condition rightHadnSideCondition)
        {
            InitializeComponent();

            _righCondition = rightHadnSideCondition;
            _leftCondition = leftHandSideCondition;

            comboBoxOperator.SelectedIndex = 0;

            labelLeftHandSide.Text = leftHandSideCondition.GetConditionString();
            labelRightHandSide.Text = rightHadnSideCondition.GetConditionString();
        }

        public Condition GetCondition()
        {
            if (comboBoxOperator.SelectedIndex > 1)
                _righCondition.Negate = true;

            return _righCondition;
        }

        public ConditionOperator GetOperator()
        {
            return comboBoxOperator.SelectedIndex % 2 == 0 ?
                ConditionOperator.And : ConditionOperator.Or;
        }

        private void linkLabelOperatorInfo_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(linkLabelOperatorInfo.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pri pokuse o otvorenie odkazu: " + linkLabelOperatorInfo.Text + " nastala neočakávaná chyba. " + ex.Message,
                    "Chyba " + ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
