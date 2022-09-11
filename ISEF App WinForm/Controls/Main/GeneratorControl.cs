namespace cvti.isef.winformapp.Controls.Main
{
    using System.Drawing;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using cvti.data;

    public partial class GeneratorControl : UserControl, IMainTabControl
    {
        public GeneratorControl()
        {
            InitializeComponent();
        }

        public ISEFDataManager DataManager { get; set; }

        public string TitleText { get; set; } = Properties.Resources.GeneratorTitle;
        public string InfoText { get; set; } = Properties.Resources.GeneratorInfo;
        public Image TitleImage { get; set; } = Properties.Resources.sql_white_100;
        public Image BackImage { get; set; }

        public async Task Activate(ISEFDataManager manager)
        {
            await Task.Delay(1);

            DataManager = manager;
            commandsControl.ReloadCommands(manager.CoreFiles.Commands, manager);
            conditionsControl.ReloadConditions(manager.CoreFiles.Conditions, manager);
        }

        public void Deactivate()
        {
            commandsControl.Deactivate();
            conditionsControl.Deactivate();
        }
    }
}
