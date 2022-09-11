namespace cvti.isef.winformapp.Helpers
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public enum MessageType
    {
        Information,
        Warning,
        Error
    }

    public enum MessageLocation
    {
        Top,
        Bottom
    }

    /// <summary>
    /// Base user control pre aplikaciu
    /// </summary>
    /// <remarks>
    /// User control obsahuje loader a vysuvaci message box
    /// </remarks>
    public partial class UserControlWithNotification : UserControl
    {
        private bool _hideOnTimer = false;
        private bool _animating = false;

        public UserControlWithNotification()
        {
            InitializeComponent();

            panelWarningNotification.Height = 0;

            HideLoader();
        }

        #region Public Methods And Properties

        public Color MessageBoardForeground
        {
            get
            {
                return labelMessageNotificatio.ForeColor;
            }
            set
            {
                labelMessageNotificatio.ForeColor = value;
            }
        }

        public Color MessageBoardBackground
        {
            get
            {
                return panelWarningNotification.BackColor;
            }
            set
            {
                panelWarningNotification.BackColor = value;
            }
        }

        /// <summary>
        /// Zobrazi loader a dostane ho do popredia
        /// </summary>
        /// <param name="text">Textova sprava ktora sa zobrazi spolocne s loaderom</param>
        public virtual void ShowLoader(string text)
        {
            labelLoaderInfoNotification.Text = text;
            tableLayoutPanelLoaderNotification.Enabled =
                tableLayoutPanelLoaderNotification.Visible = true;
            tableLayoutPanelLoaderNotification.SendToBack();
        }
        /// <summary>
        /// Schova loader
        /// </summary>
        public virtual void HideLoader()
        {
            tableLayoutPanelLoaderNotification.Enabled =
                tableLayoutPanelLoaderNotification.Visible = false;
        }

        /// <summary>
        /// Zobrazi message na urcity cas
        /// </summary>
        /// <param name="type">Typ message baru ako <see cref="MessageType"/></param>
        /// <param name="location">Pozicia kde zobrazim message bar ako <see cref="MessageLocation"/></param>
        /// <param name="title">Nadpis pre message bar</param>
        /// <param name="message">Info text pre message bar</param>
        /// <returns>True ak sa podari zobrazit message bar</returns>
        /// <remarks>
        /// Cas je konstantny see <see cref="timerHide.Interval"/>
        /// </remarks>
        public bool ShowTimedMessage(MessageType type, MessageLocation location, string title, string message)
        {
            _hideOnTimer = true;

            labelTitleNotification.Text = title;
            labelMessageNotificatio.Text = message;

            pictureBoxWarningNotification.Image = GetImageIcon(type);

            labelTitleNotification.ForeColor = GetColor(type);

            panelWarningNotification.Dock = location == MessageLocation.Bottom ? DockStyle.Bottom : DockStyle.Top;

            ShowMessageBar();

            return true;
        }

        /// <summary>
        /// Zobrazi message bar 
        /// </summary>
        /// <param name="type">Typ message baru ako <see cref="MessageType"/></param>
        /// <param name="location">Pozicia kde zobrazim message bar ako <see cref="MessageLocation"/></param>
        /// <param name="title">Nadpis pre message bar</param>
        /// <param name="message">Info text pre message bar</param>
        /// <returns>True ak sa podari zobrazit message bar</returns>
        public bool ShowPermanentMessage(MessageType type, MessageLocation location, string title, string message)
        {
            _hideOnTimer = false;

            labelTitleNotification.Text = title;
            labelMessageNotificatio.Text = message;

            pictureBoxWarningNotification.Image = GetImageIcon(type);

            labelTitleNotification.ForeColor = GetColor(type);

            panelWarningNotification.Dock = location == MessageLocation.Bottom ? DockStyle.Bottom : DockStyle.Top;

            ShowMessageBar();

            return true;
        }

        #endregion

        #region Protected And Private Methods

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (tableLayoutPanelLoaderNotification is null)
                return;

            var xLoc = Width / 2 - tableLayoutPanelLoaderNotification.Width / 2;
            var yLoc = Height / 2 - tableLayoutPanelLoaderNotification.Height / 2;
            if (tableLayoutPanelLoaderNotification.Location.X != Width / 2 - tableLayoutPanelLoaderNotification.Width / 2)
            {
                tableLayoutPanelLoaderNotification.Location =
                    new Point(xLoc, yLoc);
            }
        }      

        private void ShowMessageBar()
        {
            if (_animating)
                return;

            _animating = true;

            var transition = new Transitions.Transition(new Transitions.TransitionType_Deceleration(250));
            transition.add(panelWarningNotification, "Height", 100);

            transition.TransitionCompletedEvent += (snd, ea) =>
            {
                _animating = false;

                if (panelWarningNotification.InvokeRequired)
                    panelWarningNotification.Invoke((MethodInvoker)delegate { panelWarningNotification.Invalidate(); });
            };

            panelWarningNotification.SendToBack();

            transition.run();

            if (_hideOnTimer)
            {
                timerHide.Start();
            }
        }

        private void HideMessageBar()
        {
            if (_animating)
                return;

            _animating = true;

            var transition = new Transitions.Transition(new Transitions.TransitionType_Deceleration(250));
            transition.add(panelWarningNotification, "Height", 0);

            transition.TransitionCompletedEvent += (snd, ea) =>
            {
                _animating = false;

                if (panelWarningNotification.InvokeRequired)
                    panelWarningNotification.Invoke( (MethodInvoker) delegate { panelWarningNotification.Invalidate(); });
            };

            transition.run();
        }

        private Image GetImageIcon(MessageType type)
        {
            switch (type)
            {
                case MessageType.Error:
                    return Properties.Resources.error20;
                case MessageType.Information:
                    return Properties.Resources.info50;
                case MessageType.Warning:
                    return Properties.Resources.warning50;
                default:
                    return Properties.Resources.info50;
            }
        }

        private Color GetColor(MessageType type)
        {
            switch (type)
            {
                case MessageType.Error:
                    return Color.Red;
                case MessageType.Information:
                    return Color.FromArgb(54, 89, 127);
                case MessageType.Warning:
                    return Color.Orange;
                default:
                    return Color.FromArgb(54, 89, 127);
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            // skryjem message bar
            HideMessageBar();
        }

        private void panelWarning_Paint(object sender, PaintEventArgs e)
        {
            // Nakreslim border
            //e.Graphics.DrawRectangle(
            //    new Pen(new SolidBrush(ControlPaint.Dark(panelWarning.BackColor)), 1),
            //    new Rectangle(0, 0, panelWarning.Width - 1, panelWarning.Height - 1));
        }

        private void timerHide_Tick(object sender, EventArgs e)
        {
            // skryjem message panel a zastavim timer
            timerHide.Stop();
            HideMessageBar();
        }

        #endregion
    }
}