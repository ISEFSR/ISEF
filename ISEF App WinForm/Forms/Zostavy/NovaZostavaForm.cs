namespace cvti.isef.winformapp.Forms
{
    using cvti.data;
    using cvti.data.Conditions;
    using cvti.data.Output;
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class NovaZostavaForm : DialogBase
    {
        public Hlavicka Hlavicka { get { return novaZostavaFirstStep.VybranaHlavicka; } }
        public Condition Podmienka { get { return novaZostavaSecondStep.VybranaPodmienka; } }
        public string Nazov { get { return novaZostavaThirdStep1.MenoZostavy; } }

        public string NadpisLeft { get { return novaZostavaThirdStep1.LeftTitle; } }
        public string NadpisRight { get { return novaZostavaThirdStep1.RightTitle; } }

        public string ZatriedenieZostavy { get { return novaZostavaThirdStep1.Zaradenie; } }

        public OkruhZostavyEnum OkruhZostavy { get { return novaZostavaThirdStep1.VybranyOkruh; } }

        public event EventHandler ZostavaVytvorena;

        enum NovaZostavaStep
        {
            Hlavicka,
            Podmienky,
            Finalizacia,
            Zhrnutie
        }

        ISEFDataManager _manager;

        private NovaZostavaStep _step = NovaZostavaStep.Hlavicka;

        public NovaZostavaForm(ISEFDataManager manager)
        {
            _manager = manager;

            InitializeComponent();

            ShowButtonsPanel = false;

            novaZostavaFirstStep.ZobrazDostupneHlavicky(manager.CoreFiles.Hlavicky.Values);

            novaZostavaSecondStep.ZobrazDostupnePodmienky(manager.CoreFiles.Conditions.Values);

            verticalProgress.AddStep("Výber hlavičky",
                "Proces výberu hlavičky. Hlavička predstavuje základe zostavy.");

            verticalProgress.AddStep("Špecifikovanie podmienky", 
                "Špecifikácia podmienky výberu dát pre zostavu.");

            verticalProgress.AddStep("Potvrdenie hlavičky",
                "Finalizácia procesu vytvárania novej zostavy.");

            ShowStep();
        }

        public SelectCommand Command { get; set; }

        public string HeaderFile { get; set; }

        public int FirstDataRow { get; set; }

        private void panelButtons2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new System.Drawing.Pen(new SolidBrush(Color.FromArgb(150, 150, 150))),
                new Point(0, 0),
                new Point(panelButtons2.Width, 0));
        }

        private void buttonNext_Click(object sender, System.EventArgs e)
        {
            switch (_step)
            {
                case NovaZostavaStep.Hlavicka:
                    if (novaZostavaFirstStep.VybranaHlavicka != null)
                    {
                        _step++;
                        verticalProgress.NextStep();
                    }
                    else
                    {
                        MessageBox.Show("Pre vytvorenie novej zostavy musíte najprv vybrať hlavičku, podľa ktorej bude zostava vytvorená.",
                            "Nevybraná hlavička", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    break;
                case NovaZostavaStep.Podmienky:
                    _step++;
                    verticalProgress.NextStep();
                    break;
                case NovaZostavaStep.Finalizacia:
                    if (novaZostavaThirdStep1.IsValid())
                    {
                        _step++;
                    }
                    else
                    {
                        MessageBox.Show("Pre vytvorenie novej zostavy musíte najprv zadefinovať nadpisy.",
                            "Nevybraná nadpisy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    break;
                case NovaZostavaStep.Zhrnutie:
                    DialogResult = DialogResult.OK;
                    break;
                default:
                    break;
            }

            ShowStep();
        }

        private void buttonBack_Click(object sender, System.EventArgs e)
        {
            if (_step == NovaZostavaStep.Hlavicka)
                return;

            _step = _step - 1;
            verticalProgress.PreviousStep();
            ShowStep();
        }

        private void ShowStep()
        {
            buttonBack.Enabled = _step != NovaZostavaStep.Hlavicka;
            buttonNext.Text = _step == NovaZostavaStep.Finalizacia ? "Dokonči" : "Ďalej";

            switch (_step)
            {
                case NovaZostavaStep.Hlavicka:
                    TitleText = "Nová zostava - výber hlavičky";
                    novaZostavaFirstStep.BringToFront();
                    break;
                case NovaZostavaStep.Podmienky:
                    TitleText = "Nová zostava - výber podmienky";
                    novaZostavaSecondStep.BringToFront();
                    break;
                case NovaZostavaStep.Finalizacia:
                    TitleText = "Nová zostava - výber nadpisov";
                    novaZostavaThirdStep1.BringToFront();
                    break;
                case NovaZostavaStep.Zhrnutie:
                    TitleText = "Nová zostava - výber dokončenie";
                    novaZostavaFinalStep.BringToFront();
                    buttonBack.Visible = buttonBack.Enabled = false;
                    verticalProgress.Visible = verticalProgress.Enabled = false;
                    ZostavaVytvorena?.Invoke(this, EventArgs.Empty);
                    buttonNext.Text = "Ok";
                    break;
                default:
                    break;
            }
        }
    }
}
