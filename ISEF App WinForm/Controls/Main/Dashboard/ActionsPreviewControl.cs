namespace cvti.isef.winformapp.Controls.Main.Dashboard
{
    using System;
    using System.Drawing;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using cvti.data;
    using cvti.data.Core;

    public partial class ActionsPreviewControl : UserControl, IDashboardActivableTile
    {
        const int BlurDelay = 750;
        const int Count = 10;
        readonly Random _rng = new Random();
        readonly ActionControl[] _actions;

        public ActionsPreviewControl()
        {
            InitializeComponent();
            ControlUtilities.SetDoubleBuffered(flowLayoutPanelActions);
            _actions = new ActionControl[]
            {
                actionControl1,
                actionControl2,
                actionControl3,
                actionControl4,
                actionControl5,
                actionControl6,
                actionControl7,
                actionControl8,
                actionControl9,
                actionControl10,
            };
            foreach (var a in _actions)
                a.MouseClicked += action_Clicked;
        }

        private ISEFDataManager _manager;
        private bool _loadingActions = false;
        public async Task LoadData(ISEFDataManager manager)
        {
            if (_loadingActions)
                return;

            _loadingActions = true;
            _manager = manager;


            flowLayoutPanelActions.SuspendLayout();
            // vyprazdnim kazdu jednu akciu, vratim ju do blured stavu
            // a nastavim jej visible proprty na true
            foreach (var l in _actions) 
            {
                l.Visible = true;
                l.ShowAction(null); 
            }
            flowLayoutPanelActions.Invalidate();
            await Task.Delay(BlurDelay);
            var index = 0;

            try
            {
                foreach (var l in await manager.GetLogMessagesAsync(Count))
                {
                    await Task.Delay(_rng.Next(50, 150));
                    _actions[index++].ShowAction(l);
                }
            }
            catch (Exception ex) 
            {
                await manager.LogFrontendErrorSafeAsync(ex, "ActionsPreviewControl.LoadActions");
            }

            // v pripade ak nacitalo menej akcii 
            // zneviditelnim tie controls ktore neni treba mat zobrazene
            while (index < _actions.Length)
                _actions[index++].Visible = false;

            flowLayoutPanelActions.ResumeLayout();
            _loadingActions = false;
        }

        private void action_Clicked(object sender, EventArgs e)
        {
            var action = sender as ActionControl;
            using (var previewForm = new Form())
            {
                var actionPreview = new ActionPreview();
                actionPreview.ShowAction(action.LogMessage);
                actionPreview.Dock = DockStyle.Fill;
                previewForm.Controls.Add(actionPreview);

                var bmp = (action.LogMessage.LogType == 0 || action.LogMessage.LogType == 2) ? Properties.Resources.info20 : Properties.Resources.error20;
                previewForm.Icon = Icon.FromHandle(bmp.GetHicon());

                previewForm.StartPosition = FormStartPosition.CenterParent;
                previewForm.Size = previewForm.MinimumSize = new System.Drawing.Size(640, 480);
                previewForm.Text = "Log message " + action.lblWhen.Text;
                previewForm.ShowDialog(this.ParentForm);
            }
            // TODO: dopln kod 'zobrazenie log message '
            // (sender as ActionControl).LogMessage...
        }

        private void buttonFilters_Click(object sender, System.EventArgs e)
        {
            contextMenuStripMenu.Show(buttonFilters,
                new Point(-contextMenuStripMenu.Width + buttonFilters.Width, buttonFilters.Height));
        }

        private async void načítajOdznovaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await LoadData(_manager);
        }
    }
}
