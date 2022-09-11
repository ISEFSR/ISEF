namespace cvti.isef.winformapp.Forms
{
    using cvti.data.Functions;
    using cvti.data.Views;
    using cvti.data.Enums;
    using cvti.data.Files;
    using System;
    using cvti.data.Columns;
    using cvti.data.Conditions;
    using System.Threading.Tasks;

    public partial class NewColumnControl : DialogBase
    {
        public NewColumnControl(ConditionsManagerJson conditions)
        {
            InitializeComponent();

            //ShowWait();

            foreach (var c in Enum.GetNames(typeof(AssuViewAvailableColumns)))
                comboBoxColumns.Items.Add(c);

            comboBoxColumns.SelectedIndex = 0;

            // TODO: load the conditions
            foreach (var c in conditions.Values)
                comboBoxConditions.Items.Add(c);
        }

        public override void ShowWait()
        {
            base.ShowWait();

            label2.Visible = false;
            tableLayoutPanel1.Visible = false;
        }

        public override void HideWait()
        {
            base.HideWait();

            tableLayoutPanel1.Visible = true;
            label2.Visible = true;
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            await Task.Delay(1350);

            HideWait();
        }

        public Column GetColumn()
        {
            var c = AssuView.VratStlpec((AssuViewAvailableColumns)Enum.Parse(typeof(AssuViewAvailableColumns), comboBoxColumns.SelectedItem.ToString()));

            c.ColumnAlias = string.IsNullOrWhiteSpace(textBoxAlias.Text) ? comboBoxColumns.SelectedItem.ToString() : textBoxAlias.Text;

            if (checkBoxConditionalSum.Checked)
            {
                if (comboBoxConditions.SelectedIndex != -1)
                {
                    var cnd = comboBoxConditions.SelectedItem as Condition;
                    c.Functions.Add(new ConditionalSum(cnd));
                }
                else
                {
                    c.Functions.Add(new Sum());
                }
            }
            else if (checkBoxSum.Checked)
            {
                c.Functions.Add(new Sum());
            }

            return c;
        }

        private void checkBoxSum_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxConditionalSum.Enabled = checkBoxSum.Checked;
            if (!checkBoxSum.Checked)
            {
                checkBoxConditionalSum.Checked = false;
            }
        }

        private void checkBoxConditionalSum_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxConditionalSum.Checked)
            {
                comboBoxConditions.Enabled = true;
                if (comboBoxConditions.Items.Count > 0)
                    comboBoxConditions.SelectedIndex = 0;
            }
            else
            {
                comboBoxConditions.Enabled = false;
                comboBoxConditions.SelectedItem = null;
            }
        }

        private void comboBoxColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxColumns.SelectedIndex == -1)
                return;

            var c = AssuView.VratStlpec((AssuViewAvailableColumns)Enum.Parse(typeof(AssuViewAvailableColumns), comboBoxColumns.SelectedItem.ToString()));

            checkBoxSum.Visible = checkBoxSum.Enabled = checkBoxConditionalSum.Visible
                = comboBoxConditions.Visible = c.IsNumeric;
        }
    }
}