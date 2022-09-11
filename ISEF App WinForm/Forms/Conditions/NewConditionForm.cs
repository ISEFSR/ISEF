namespace cvti.isef.winformapp.Forms.Conditions
{
    using cvti.data.Conditions;
    using cvti.data.Core;
    using cvti.data.Enums;
    using cvti.data.Files;
    using cvti.data.Views;
    using cvti.isef.winformapp.Controls.Main.Generator.Conditios;
    using System;
    using System.Windows.Forms;

    public partial class NewConditionForm : DialogBase
    {
        public NewConditionForm(ConditionsManagerJson cManager, Condition core)
        {
            InitializeComponent();
            ControlUtilities.AlterConditionsComboBox(comboBoxCondition, cManager);

            foreach (var op in Enum.GetNames(typeof(ConditionOperator)))
                comboBoxOperator.Items.Add(op);

            comboBoxOperator.SelectedIndex = 0;

            foreach (var c in Enum.GetNames(typeof(AssuViewAvailableColumns)))
                comboBoxColumn.Items.Add(c);

            comboBoxColumn.SelectedIndex = 0;

            foreach (var p in Enum.GetNames(typeof(ConditionType)))
                comboBoxConditionType.Items.Add(p);

            comboBoxConditionType.SelectedIndex = 0;

            labelCoreCondition.Text = core.GetConditionString(false);
        }

        public Condition SelectedCondition 
        {
            get 
            {
                Condition cnd = null;
                if (tabControlCondition.SelectedIndex == 0)
                {
                    // Existujuca podmienka
                    cnd = (comboBoxCondition.SelectedItem as Condition).CloneMe(true);
                }
                else
                {
                    // Nova podmienka
                    var clmn = AssuView.VratStlpec((AssuViewAvailableColumns)comboBoxColumn.SelectedIndex);
                    var value = textBoxValue.Text;

                    switch (comboBoxConditionType.SelectedIndex)
                    {
                        case 0:
                            cnd = new Equals(string.Empty, clmn, value);
                            break;
                        case 1:
                            cnd = new GreaterThan(string.Empty, clmn, value);
                            break;
                        case 2:
                            cnd = new LessThan(string.Empty, clmn, value);
                            break;
                        case 3:
                            cnd = new Inlist(string.Empty, clmn, value.Split(';'));
                            break;
                        default:
                            return cnd;
                    }
                }

                cnd.Negate = checkBoxNegate.Checked;
                return cnd;
            }
        }

        public ConditionOperator Operator { get { return (ConditionOperator)comboBoxOperator.SelectedIndex; } }

        private void comboBoxCondition_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {

        }

        private void NewConditionForm_Load(object sender, EventArgs e)
        {

        }
    }
}
