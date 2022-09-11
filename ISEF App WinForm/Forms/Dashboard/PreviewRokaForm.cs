namespace cvti.isef.winformapp.Forms
{
    using cvti.data;
    using cvti.data.Conditions;
    using cvti.data.Functions;
    using cvti.data.Output;
    using cvti.data.Views;
    using cvti.isef.winformapp.Controls.Main.Dashboard;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class PreviewRokaForm : DialogBase
    {
        private readonly ISEFDataManager _manager;

        private readonly int _rok;

        private readonly Equals _rokCondition;

        public PreviewRokaForm(ISEFDataManager manager, int rok)
        {
            InitializeComponent();

            _manager = manager;

            _rok = rok;

            _rokCondition = new Equals(string.Empty, AssuView.VratStlpec(data.Enums.AssuViewAvailableColumns.Rok), _rok);

            TitleText = Tag.ToString().Replace("{r}", rok.ToString());

            ShowImageIcon = false;

            ShowWait();
        }

        public override void HideWait()
        {
            base.HideWait();
            panel1.Visible = panel1.Enabled = true;
        }

        public override void ShowWait()
        {
            base.ShowWait();
            panel1.Visible = panel1.Enabled = false;
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var tasks = new List<Task>();
            foreach (TabPage t in tabControlOkruhy.TabPages) 
            {
                //var stupenCondition = new Equals(string.Empty, AssuView.VratStlpec(data.Enums.AssuViewAvailableColumns.StupenKod), t.Tag.ToString());
                //var command = GetDefaultCommand();
                //command.CommandCondition.AddCondition(stupenCondition, data.Enums.ConditionOperator.And);

                var previewControl = t.Controls[0] as StupenPreviewControl;
                if (previewControl is null)
                    return;

                tasks.Add(previewControl.LoadData(_manager, _rok, previewControl.Tag.ToString(), t.Text));
            }
            await Task.WhenAll(tasks);

            HideWait();
        }

        private SelectCommand GetDefaultCommand()
        {
            var command = new SelectCommand(string.Empty);
            var sum = AssuView.VratStlpec(data.Enums.AssuViewAvailableColumns.Skut);
            sum.AddFunction(new Sum());
            sum.ColumnAlias = "Skutočnosť";

            var podKod = AssuView.VratStlpec(data.Enums.AssuViewAvailableColumns.PodriadenostKod, true, "Kód");
            var podNazov = AssuView.VratStlpec(data.Enums.AssuViewAvailableColumns.PodriadenostNazov, true, "Názov");
            command.AddColumn(podKod);
            command.AddColumn(podNazov);
            command.AddColumn(sum);

            command.CommandCondition = _rokCondition.CloneMe(true);
            return command;
        }

        private void toolStripButtonExportSelected_Click(object sender, EventArgs e)
        {
            (tabControlOkruhy.SelectedTab.Controls[0] as StupenPreviewControl).ExportData();
        }

        private void toolStripButtonShowSQL_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
