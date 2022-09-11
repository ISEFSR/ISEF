namespace cvti.isef.winformapp.Controls.Main.Import
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public enum StepStatus
    {
        Inactive,
        Active,
        ActiveError,
        Completed,
    }

    public partial class VerticalStep : UserControl
    {
        public VerticalStep()
        {
            InitializeComponent();
        }

        public VerticalStep(string title, string info, int number, bool showNumber)
            : this()
        {
            StepInfo = info;
            StepNumber = number;
            ShowNumber = showNumber;
            StepName = title;
            UpdateTitle();
        }

        private int _number;
        private bool _showNumber;
        public int StepNumber { get { return _number; } set { _number = value; UpdateTitle(); } }
        public bool ShowNumber { get { return _showNumber; } set { _showNumber = value; UpdateTitle(); } }

        private void UpdateTitle()
        {
            if (ShowNumber)
            {
                label1.Text = $"{StepNumber}. {_name}";
            }
            else
            {
                label1.Text = _name;
            }
        }

        private StepStatus _status = StepStatus.Active;
        public StepStatus Status { get { return _status; } set { _status = value; StatusChanged(); } }

        private string _name;
        public string StepName { get { return _name; } set { _name = value; UpdateTitle(); } }
        public string StepInfo { get { return labelStepInfo.Text; } set { labelStepInfo.Text = value; } }

        private void step_Click(object sender, EventArgs e) => MouseClicked?.Invoke(this, EventArgs.Empty);

        public event EventHandler MouseClicked;

        private void StatusChanged()
        {
            switch (_status)
            {
                case StepStatus.Active:
                    label1.ForeColor = Color.FromArgb(32, 32, 32);
                    labelStepInfo.ForeColor = Color.FromArgb(64, 64, 64);
                    panelSideline.BackColor = Color.Gray;
                    tableLayoutPanel.Cursor =
                        label1.Cursor =
                        labelStepInfo.Cursor =
                        panelSideline.Cursor =
                        Cursors.Arrow;
                    break;
                case StepStatus.ActiveError:
                    label1.ForeColor = Color.DarkRed;
                    labelStepInfo.ForeColor = Color.Red;
                    panelSideline.BackColor = Color.DarkRed;
                    tableLayoutPanel.Cursor =
                        label1.Cursor =
                        labelStepInfo.Cursor =
                        panelSideline.Cursor =
                        Cursors.Arrow;
                    break;
                case StepStatus.Completed:
                    label1.ForeColor = Color.DarkGreen;
                    labelStepInfo.ForeColor = Color.Green;
                    panelSideline.BackColor = Color.DarkGreen;
                    tableLayoutPanel.Cursor =
                        label1.Cursor =
                        labelStepInfo.Cursor =
                        panelSideline.Cursor =
                        Cursors.PanWest;
                    break;
                case StepStatus.Inactive:
                    label1.ForeColor = Color.LightGray;
                    labelStepInfo.ForeColor = Color.LightGray;
                    panelSideline.BackColor = Color.LightGray;
                    tableLayoutPanel.Cursor =
                        label1.Cursor =
                        labelStepInfo.Cursor =
                        panelSideline.Cursor =
                        Cursors.No;
                    break;
                default:
                    break;
            }
        }
    }
}
