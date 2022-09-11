namespace cvti.isef.winformapp.Controls.Main.Dashboard
{
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using cvti.data;
    using System.Linq;
    using System.Collections.Generic;
    using System;
    using cvti.data.Classifiers;

    /// <summary>
    /// 
    /// </summary>
    public partial class WarningPanel : UserControl
    {
        private ISEFDataManager _manager;

        public WarningPanel()
        {
            InitializeComponent();
            flowLayoutPanelContent.SetDoubleBuffered();
        }

        private readonly Queue<ChybajuciRiadok> _warning =
            new Queue<ChybajuciRiadok>();

        public async Task LoadData(ISEFDataManager manager)
        {
            _manager = manager;

            flowLayoutPanelContent.Controls.Clear();

            _warning.Clear();

            foreach (SingleWarning i in flowLayoutPanelContent.Controls)
                i.Dispose();

            var unasignedItems = await _manager.GetMissingRows();

            if (!unasignedItems.Any())
            {
                Enabled = Visible = false;
                return;
            }
            else
            {
                Enabled = Visible = true;
            }

            foreach (var i in unasignedItems)
                _warning.Enqueue(i);

            var count = 0;
            while (count++ < 10)
            {
                if (_warning.Count > 0)
                    flowLayoutPanelContent.Controls.Add(CreateWarningItem(_warning.Dequeue()));
            }

            UpdateInfoMessage();
        }

        private SingleWarning CreateWarningItem(ChybajuciRiadok i)
        {
            var warningItem = new SingleWarning();

            warningItem.ShowWarning(new SingleWarningItem(i), i);

            warningItem.TextSuccessfullyChanged += singleWarning1_TextSuccessfullyChanged;

            return warningItem;
        }

        public bool ContainsAnyItems { get => flowLayoutPanelContent.Controls.Count > 0; }

        private async void singleWarning1_TextSuccessfullyChanged(object sender, TextsChangedEventArgs e)
        {
            var warningItem = sender as SingleWarning;

            if (await _manager.UpdateMissingRow(warningItem.ChybajuciRiadok, e.TextShort, e.TextLong))
            {
                warningItem.Dispose();

                if (_warning.Count > 0)
                    flowLayoutPanelContent.Controls.Add(CreateWarningItem(_warning.Dequeue()));

                UpdateInfoMessage();

                ItemTextSuccessfullyChanged?.Invoke(this, EventArgs.Empty);

                MessageBox.Show($"Text pre hodnotu {warningItem.ChybajuciRiadok.Kod} číselníka {warningItem.ChybajuciRiadok.TableName} pre rok {warningItem.ChybajuciRiadok.Rok} úspešne zmenený.", "Text zmenený",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Textáciu sa nepodarilo zmeniť skúste načítať údaje odznova.", "Textácia nezmenená", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UpdateInfoMessage()
            => labelInfo.Text = labelInfo.Tag.ToString().Replace("{x}", (_warning.Count() + flowLayoutPanelContent.Controls.Count).ToString());

        /// <summary>
        /// Indikuje uspesne updatnutie udajov pre polozku ciselnika analytickej evidencie
        /// </summary>
        public event EventHandler ItemTextSuccessfullyChanged;
    }
}
