namespace cvti.isef.winformapp.Forms
{
    using cvti.data;
    using cvti.data.Output;
    using cvti.isef.winformapp.Controls.Output;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using System.Windows;

    public partial class GenerovanieZostavForm : DialogBase
    {
        public GenerovanieZostavForm(ISEFDataManager manager, IEnumerable<Zostava> zostavy, IEnumerable<int> roky, OkruhZostavyEnum okruh)
        {
            InitializeComponent();

            TitleText += $" pre okruh {okruh}";

            comboBoxYear.DataSource = roky;
            comboBoxYear.SelectedIndex = 0;

            foreach (var z in zostavy)
            {
                var zostavaControl = new ZostavaOutputControl();
                zostavaControl.SetZostava(manager, z);
                flowLayoutPanelZostavy.Controls.Add(zostavaControl);
            }

            comboBoxPodmienky.Items.Add("Bez podmienky");
            foreach (var p in manager.CoreFiles.Conditions.Values)
                comboBoxPodmienky.Items.Add(p);

            comboBoxPodmienky.SelectedIndex = 0;

            ShowButtonsPanel = false;
        }

        private int GetRok() => Convert.ToInt32(comboBoxYear.SelectedItem);

        private bool _exporting;
        private async Task ExportujVsetky()
        {
            if (_exporting)
                return;

            buttonExport.Enabled = false;

            _exporting = true;

            var podmienka = comboBoxPodmienky.SelectedIndex == 0 ?
                null : (comboBoxPodmienky.SelectedItem as  cvti.data.Conditions.Condition);

            var tasks = new List<Task>();
            foreach (ZostavaOutputControl z in flowLayoutPanelZostavy.Controls)
                tasks.Add(z.Run(GetRok(), checkBoxHeader.Checked, podmienka));

            await Task.WhenAll(tasks);

            _exporting = false;

            buttonExport.Enabled = true;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (_exporting)
            {
                MessageBox.Show("Musíte počkať kým program dokončí spracovanie výstupných zostáv", "Prebieha spracovanie",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                e.Cancel = true;
            }
        }

        private async void buttonExport_Click(object sender, EventArgs e)
        {
            await ExportujVsetky();

            if (MessageBox.Show("Všetky zostavy úspešne vytvorené. Prajete si otvoriť adresár s vytvorenými zostavami?", "Zostavy vytvorené",
                MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                Process.Start(Properties.Settings.Default.OutputDirectory);
            }
        }

        private void buttonOpenDirectory_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.OutputDirectory);
        }

        private void buttonCollapseAll_Click(object sender, EventArgs e)
        {
            foreach (ZostavaOutputControl z in flowLayoutPanelZostavy.Controls)
                z.Collapse();
        }

        private void buttonExpandAll_Click(object sender, EventArgs e)
        {
            foreach (ZostavaOutputControl z in flowLayoutPanelZostavy.Controls)
                z.Expand();
        }
    }
}
