namespace cvti.isef.winformapp.Forms
{
    using cvti.data;
    using cvti.data.Core;
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class ApplicationSetupForm : DialogBase
    {
        public MSSQLServer Server
        {
            get => applicationSetupControl2.Server;
        }

        public string OutputDirectory
        {
            get { return applicationSetupControl3.OutputDirectory; }
        }

        public string HlavickyDirectory
        {
            get { return applicationSetupControl3.HlavickyDirectory; } 
        }

        public string DataDirectory
        {
            get { return applicationSetupControl3.DataDirectory; } 
        }

        public event EventHandler<SetupState> StateChanged;

        private bool _initializationSuccessfull = false;

        public enum SetupState
        {
            Welcome,
            Server,
            Files,
            Finalization
        }

        private SetupState _actualState = SetupState.Welcome;

        public ApplicationSetupForm()
        {
            InitializeComponent();

            ShowButtonsPanel = false;

            verticalProgress.AddStep("Prvé použitie", "Nastavenie premenných aplikácie");

            verticalProgress.AddStep("Nastavenie MSSQL serveru", "Nastavenie pripojenia k MSSQL dátovému serveru");

            verticalProgress.AddStep("Nastavenie ostatných premenných", "Nastavenie adresárov a súborov potrebných pre fungovanie aplikácie");

            verticalProgress.AddStep("Dokončenie", "Finalizácia prvého spustenia aplikácie");

            applicationSetupControl1.BringToFront();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (!_initializationSuccessfull)
            {
                if (MessageBox.Show("Nedokončili ste provtné nastavenie aplikácie. Ak teraz zavriete úvodne okno aplikácia sa ukončí. Skutočne si prajete zavrieť úvodné okno?",
                    "Nedokončené nastavenie", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    DialogResult = DialogResult.Cancel;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void panelButtons_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(
                new Pen(new SolidBrush(Color.FromArgb(200, 200, 200)), 1),
                new Point(0, 0),
                new Point(panelButtons.Width, 0));
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            panelButtons?.Invalidate();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            switch (_actualState)
            {
                case SetupState.Welcome:
                    if (applicationSetupControl1.ValidateData())
                    {
                        _actualState++;
                        break;
                    }
                    else
                    {
                        MessageBox.Show("Pre správne fungovanie aplikácie si najprv musíte nainštalovať VFPOLEDB data provider z odkazu hore.", "VFPOLED data provider",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                case SetupState.Server:
                    if (applicationSetupControl2.ValidateConnection())
                    {
                        _actualState++;
                        break;
                    }
                    else
                    {
                        return;
                    }
                case SetupState.Files:
                    if (applicationSetupControl3.ValidateData())
                    {
                        _actualState++;
                        applicationSetupControl4.ShowData(applicationSetupControl3.OutputDirectory, applicationSetupControl3.DataDirectory, applicationSetupControl3.HlavickyDirectory, applicationSetupControl2.Server);
                        break;
                    }
                    else
                    {
                        return;
                    }
                case SetupState.Finalization:
                    _initializationSuccessfull = true;
                    DialogResult = DialogResult.OK;
                    return;
                default:
                    return;
            }

            verticalProgress.NextStep();
            StateChanged?.Invoke(this, _actualState);
            ShowState();
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            switch (_actualState)
            {
                case SetupState.Welcome:
                    return;
                case SetupState.Server:
                    _actualState--;
                    break;
                case SetupState.Files:
                    _actualState--;
                    break;
                case SetupState.Finalization:
                    _actualState--;
                    break;
                default:
                    return;
            }

            verticalProgress.PreviousStep();
            StateChanged?.Invoke(this, _actualState);
            ShowState();
        }

        private void ShowState()
        {
            var titleText = string.Empty;
            switch (_actualState)
            {
                case SetupState.Welcome:
                    applicationSetupControl1.BringToFront();
                    titleText = "Nastavenie prostredia pre aplikáciu";
                    break;
                case SetupState.Server:
                    applicationSetupControl2.BringToFront();
                    titleText = "Nastavenie prostredia pre aplikáciu - nastavenia MSSQL serveru";
                    break;
                case SetupState.Files:
                    titleText = "Nastavenie prostredia pre aplikáciu - nastavenie adresárov a súborov";
                    applicationSetupControl3.BringToFront();
                    buttonNext.Text = "Ďalej";
                    break;
                case SetupState.Finalization:
                    titleText = "Nastavenie prostredia pre aplikáciu - dokončenie nastavovania prostredia";
                    applicationSetupControl4.BringToFront();
                    //buttonPrevious.Visible = false;
                    buttonNext.Text = "Dokonči";
                    break;
                default:
                    break;
            }
            TitleText = titleText;
        }
    }
}
