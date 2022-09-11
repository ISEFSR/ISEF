namespace cvti.isef.winformapp.Controls.Main.Dashboard
{
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using cvti.data;

    public partial class YearsPreviewPanel : UserControl
    {
        #region Variables, Constants And Constructor

        private static string NoDataText = "Vyzerá to tak, že v databáze nemáte nahrané žiadne dáta.";
        private static string WorkingText = "Načítavam údaje ohľadom údajov nahratých v databáze...";

        private static Image WorkingImage = Properties.Resources.loading3;
        private static Image NoDataImage = Properties.Resources.info96;

        private static int LoadingDelay = 650;

        private ISEFDataManager _manager;

        private bool _working = false;

        public YearsPreviewPanel()
        {
            InitializeComponent();
            flowLayoutPanelYearsInfo.Dock = DockStyle.Fill;
        }

        #endregion

        #region Public Members

        public async Task LoadData(ISEFDataManager manager)
        {
            if (_working) return;

            _working = true;
            flowLayoutPanelYearsInfo.Enabled = flowLayoutPanelYearsInfo.Visible = false;

            _manager = manager;
            Cursor = Cursors.WaitCursor;

            pictureBoxInfo.Image = WorkingImage;
            labelInfo.Text = WorkingText;

            try
            {
                var stats = await _manager.MSSQLManager.GetStatsAsync();

                while (flowLayoutPanelYearsInfo.Controls.Count > 0)
                    flowLayoutPanelYearsInfo.Controls[0].Dispose();
                
                await Task.Delay(LoadingDelay);

                foreach (var y in stats.Select(s => s.Rok).Distinct())
                {
                    var yearStats = stats.Where(s => s.Rok == y);
                    var yc = new SingleYearControl(yearStats, y);
                    yc.Manager = manager;
                    //yc.Margin = new Padding(2, 5, 2, 5);
                    yc.Width = flowLayoutPanelYearsInfo.Width - 25;
                    flowLayoutPanelYearsInfo.Controls.Add(yc);
                }
            }
            catch (Exception ex)
            {
                _manager.LogFrontendErrorSafeAsync(ex, "YearsPreviewPanel.LoadData").GetAwaiter().GetResult();
            }
            finally
            {
                if (flowLayoutPanelYearsInfo.Controls.Count > 0)
                {
                    flowLayoutPanelYearsInfo.Enabled = flowLayoutPanelYearsInfo.Visible = true;
                }

                pictureBoxInfo.Image = NoDataImage;
                labelInfo.Text = NoDataText;

                Cursor = Cursors.Default;
                _working = false;
            }
        }

        #endregion

        #region Private Members

        private void buttonMenu_Click(object sender, EventArgs e) 
        {
            contextMenuStripSettings.Show(buttonMenu, new Point(-contextMenuStripSettings.Width + buttonMenu.Width, buttonMenu.Height));
        }

        private async void načítajOdznovaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await LoadData(_manager);
        }

        private async void získajDátaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await _manager.ReloadAllStats();
            await LoadData(_manager);
        }

        #endregion
    }
}
