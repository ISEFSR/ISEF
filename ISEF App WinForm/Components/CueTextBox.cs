namespace cvti.isef.winformapp.Components
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    /// <summary>
    /// TextBox s placeholder textom 
    /// </summary>
    public partial class CueTextBox : TextBox
    {
        public CueTextBox()
        {
            InitializeComponent();

            this.SetDoubleBuffered();
        }

        public CueTextBox(IContainer container)
            : this()
        {
            container.Add(this);
        }

        private string _cue;

        /// <summary>
        /// Placeholder ( napoveda )
        /// </summary>
        /// <value>
        /// Placeholder text ako <see cref="string"/>
        /// </value>
        [Localizable(true)]
        public string Cue
        {
            get
            {
                return _cue;
            }
            set
            {
                _cue = value;
                UpdateCue();
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, string lp);

        private void UpdateCue()
        {
            if (IsHandleCreated && _cue != null)
            {
                SendMessage(Handle, 0x1501, (IntPtr)1, _cue);
            }
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            UpdateCue();
        }
    }
}
