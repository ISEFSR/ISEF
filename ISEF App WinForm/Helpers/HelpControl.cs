namespace cvti.isef.winformapp.Controls.Main.Import
{
    using System.Drawing;
    using System.Windows.Forms;
    using Transitions;

    public partial class HelpControl : UserControl
    {
        public HelpControl()
        {
            InitializeComponent();
        }

        private bool _shouldResize = true;
        public void SetHelpInfo(HelpTileInfo help)
        {
            if (help != null)
            {
                _shouldResize = false;
                TitleText = help.Title;
                InfoText1 = help.Info1;

                pictureBoxInfo.Enabled = pictureBoxInfo.Visible = help.InfoImage != null;
                if (pictureBoxInfo.Enabled)
                {
                    pictureBoxInfo.Image = help.InfoImage;
                }
                _shouldResize = true;
            }

            if (AutoSizeHelp)
            {
                SetControlSize();
            }
        }

        public bool AutoSizeHelp { get; set; } = true;

        public string TitleText { get { return labelTitle.Text; } set { labelTitle.Text = value; } }
        public string InfoText1 { get { return labelInfoText.Text; } set { labelInfoText.Text = value; if (_shouldResize) SetControlSize(); } }
        public Image HelpImage { get { return pictureBoxInfo.Image; } set { pictureBoxInfo.Image = value; } }
        public bool HelpImageVisible { get { return pictureBoxInfo.Visible; } set { pictureBoxInfo.Visible = value; if(_shouldResize) SetControlSize(); }  }

        public ushort AnimationSpeed { get; set; } = 200;

        private void SetControlSize()
        {
            _targetHeight = CalculateControlHeight();
            AnimateNewHeight();
        }

        private int _targetHeight = 0;
        private void AnimateNewHeight()
        {
            var t = new Transition(new TransitionType_Linear(AnimationSpeed));
            t.add(this, "Height", _targetHeight);
            t.TransitionCompletedEvent += (snd, ea) =>
            {
                if (Height != _targetHeight)
                    AnimateNewHeight();
            };
            t.run();
        }

        private int CalculateControlHeight()
        {
            var g = labelInfoText.CreateGraphics();
            var size1 = g.MeasureString(labelInfoText.Text, labelInfoText.Font, labelInfoText.Width);
            var imageSize = pictureBoxInfo.Visible ? pictureBoxInfo.Height : 0;

            return labelTitle.Height + (int)size1.Height + imageSize + 20;
        }
    }
}
