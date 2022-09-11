namespace cvti.isef.winformapp.Controls.Main.Import
{
    using System;
    using System.Windows.Forms;

    public partial class VerticalProgress : UserControl
    {
        private int _acitveStep = -1;

        public VerticalProgress()
        {
            InitializeComponent();
        }

        private bool _show = true;
        public bool ShowNumber
        {
            get { return _show; }
            set
            {
                _show = value;
                foreach (var c in flowLayoutPanel.Controls)
                    (c as VerticalStep).ShowNumber = _show;
            }
        }

        public void AddStep(string title, string info)
        {
            var step = new VerticalStep(title, info, flowLayoutPanel.Controls.Count + 1, ShowNumber);

            step.MouseClicked += (snd, ea) =>
            {
                StepClicked?.Invoke(this, (snd as VerticalStep));
            };
            if (flowLayoutPanel.Controls.Count == 0)
            {
                _acitveStep = 0;
                step.Status = StepStatus.Active;
            }
            else
            {
                step.Status = StepStatus.Inactive;
            }

            flowLayoutPanel.Controls.Add(step);
            step.SendToBack();
        }

        public void ClearSteps()
        {
            _acitveStep = -1;
            while (flowLayoutPanel.Controls.Count > 0)
                flowLayoutPanel.Controls[0].Dispose();
        }

        public void NextStep()
        {
            if (_acitveStep + 1 == flowLayoutPanel.Controls.Count)
                return;

            (flowLayoutPanel.Controls[_acitveStep++] as VerticalStep).Status = StepStatus.Completed;
            (flowLayoutPanel.Controls[_acitveStep] as VerticalStep).Status = StepStatus.Active;
        }

        public void ShowError(int time = -1)
        {
            if (_acitveStep >= 0 && _acitveStep < flowLayoutPanel.Controls.Count)
            {
                (flowLayoutPanel.Controls[_acitveStep] as VerticalStep).Status = StepStatus.ActiveError;
                if (time != -1)
                {
                    timerError.Interval = time;
                    timerError.Start();
                }
            }
        }

        public void PreviousStep()
        {
            if (_acitveStep > 0)
            {
                (flowLayoutPanel.Controls[_acitveStep--] as VerticalStep).Status = StepStatus.Inactive;
                (flowLayoutPanel.Controls[_acitveStep] as VerticalStep).Status = StepStatus.Active;
            }
        }

        public event EventHandler<VerticalStep> StepClicked;

        private void timerError_Tick(object sender, EventArgs e)
        {
            timerError.Stop();

            try
            {
                var prog = flowLayoutPanel.Controls[_acitveStep] as VerticalStep;
                if (prog != null && prog.Status == StepStatus.ActiveError)
                {
                    prog.Status = StepStatus.Active;
                }
            }
            catch
            {

            }
        }
    }
}
