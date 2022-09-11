namespace cvti.isef.winformapp.Controls.Main.Zostavy
{
    using System.Collections.Generic;
    using System.Windows.Forms;
    using cvti.data.Conditions;
    using cvti.data.Core;

    public partial class NovaZostavaSecondStep : UserControl
    {
        public NovaZostavaSecondStep()
        {
            InitializeComponent();
        }

        public void ZobrazDostupnePodmienky(IEnumerable<Condition> podmienky)
        {
            listViewConditions.Items.Clear();

            foreach (var p in podmienky)
            {
                var item = new ListViewItem();
                item.Text = p.ConditionName;
                item.Tag = p;
                item.ImageIndex = 0;

                listViewConditions.Items.Add(item);
            }
        }

        public Condition VybranaPodmienka
        {
            get
            {
                if (listViewConditions.SelectedItems.Count == 0 || !checkBoxUseCondition.Checked)
                    return null;

                return listViewConditions.SelectedItems[0].Tag as Condition;
            }
        }

        private void checkBoxUseCondition_CheckedChanged(object sender, System.EventArgs e)
        {
            listViewConditions.Enabled = checkBoxUseCondition.Checked;
        }
    }
}
