namespace cvti.isef.winformapp.Controls.Main.Generator.Conditios
{
    using System.Data;
    using System.Linq;
    using System.Windows.Forms;
    using cvti.data.Core;
    using System;
    using cvti.data.Views;
    using cvti.data.Enums;
    using cvti.data.Files;
    using cvti.data.Conditions;

    public partial class UniversalConditionControl : UserControl
    {
        readonly string[] Headers = new string[]
        {
            "TSQL = Operator",
            "TSQL > Operator",
            "TSQL IN Operator",
            "TSQL < Operator",
            "Plain text condition",
            "Plain text condition",
            "Plain text condition"
        };

        readonly string[] Info = new string[]
        {
            "The IN operator allows you to specify multiple values in a WHERE clause. The IN operator is a shorthand for multiple OR conditions.",
            "The IN operator allows you to specify multiple values in a WHERE clause. The IN operator is a shorthand for multiple OR conditions.",
            "The IN operator allows you to specify multiple values in a WHERE clause. The IN operator is a shorthand for multiple OR conditions.",
            "The IN operator allows you to specify multiple values in a WHERE clause. The IN operator is a shorthand for multiple OR conditions.",
            "The IN operator allows you to specify multiple values in a WHERE clause. The IN operator is a shorthand for multiple OR conditions.",
            "The IN operator allows you to specify multiple values in a WHERE clause. The IN operator is a shorthand for multiple OR conditions.",
            "The IN operator allows you to specify multiple values in a WHERE clause. The IN operator is a shorthand for multiple OR conditions."
        };

        private ConditionType _type;

        public ConditionsManagerJson Manager { get; set; }

        public UniversalConditionControl()
        {
            InitializeComponent();
            foreach (var c in Enum.GetNames(typeof(ConditionType)))
                comboBoxConditionType.Items.Add(c);

            comboBoxConditionType.SelectedIndex = 0;

            foreach (var c in Enum.GetNames(typeof(AssuViewAvailableColumns)))
                comboBoxColumns.Items.Add(c);

            comboBoxColumns.SelectedIndex = 0;
        }

        public void SetType(ConditionType type) => comboBoxConditionType.SelectedIndex = (int)type;

        public void ShowCondition(Condition cnd)
        {
            if (cnd is Equals)
            {
                _type = ConditionType.Equals;
            }
            else if (cnd is GreaterThan)
            {
                _type = ConditionType.GreaterThan;
            }
            else if (cnd is LessThan)
            {
                _type = ConditionType.LessThan;
            }
            else if (cnd is Inlist)
            {
                _type = ConditionType.Inlist;
            }
            else
            {

            }

            textBoxValue.Text = cnd.Value.ToString();
            comboBoxConditionType.SelectedIndex = (int)_type;
            textBoxConditionName.Text = cnd.ConditionName;
            checkBoxNegate.Checked = cnd.Negate;
        }

        public Condition GetCondition()
        {
            var clmn = AssuView.VratStlpec((AssuViewAvailableColumns)comboBoxColumns.SelectedIndex);
            Condition cnd = null;
            switch ((ConditionType)comboBoxConditionType.SelectedIndex)
            {
                case ConditionType.Equals:
                    cnd = new Equals(textBoxConditionName.Text, clmn, textBoxValue.Text);
                    break;
                case ConditionType.GreaterThan:
                    cnd = new GreaterThan(textBoxConditionName.Text, clmn, textBoxValue.Text);
                    break;
                case ConditionType.LessThan:
                    cnd = new LessThan(textBoxConditionName.Text, clmn, textBoxValue.Text);
                    break;
                case ConditionType.Inlist:
                    cnd = new Inlist(textBoxConditionName.Text, clmn, textBoxValue.Text.Split(';'));
                    break;
                case ConditionType.PlainText:
                    cnd = new PlainTextCondition(textBoxConditionName.Text, textBoxValue.Text);
                    break;
                default:
                    break;
            }
            cnd.Negate = checkBoxNegate.Checked;
            return cnd;
        }

        private void comboBoxConditionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            label1.Text = Headers[comboBoxConditionType.SelectedIndex];
            label2.Text = Info[comboBoxConditionType.SelectedIndex];

            label3.Visible = comboBoxColumns.Visible = (comboBoxConditionType.SelectedIndex != (int)ConditionType.PlainText);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBoxConditionName_TextChanged(object sender, EventArgs e)
        {
            if (Manager != null)
            {
                if (Manager.Values.FirstOrDefault(c => c.ConditionName == textBoxConditionName.Text) == null &&
                    !string.IsNullOrWhiteSpace( textBoxConditionName.Text ))
                {

                }
                else
                {

                }
            }
        }
    }
}
