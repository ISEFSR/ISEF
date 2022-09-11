namespace cvti.isef.winformapp.Controls.Main.Import
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Diagnostics;

    public partial class TileControl : UserControl
    {
        public TileControl()
        {
            InitializeComponent();
        }

        public string TitleText { get { return labelTitle.Text; } set { labelTitle.Text = value; } }

        public Color TileBackgroundColor { get { return panelContent.BackColor; } set { panelContent.BackColor = value; } }

        private Image _tileImage;
        public Image TileImage { get { return _tileImage; } set { _tileImage = value; pictureBoxTypeImage.Image = (IsSelected ? MouseOverTileImage : _tileImage); } }

        public Image MouseOverTileImage { get; set; } = Properties.Resources.notfound140b;

        public bool IsSelected { get; private set; }

        private bool _resizing = false;
        private void panelContent_MouseEnter(object sender, EventArgs e)
        {
            Debug.WriteLine(Name + " im in...");

            if (!IsSelected)
            {
                IsSelected = true;

                pictureBoxTypeImage.Image = MouseOverTileImage;

                if (_resizing) return;

                MakeBig();

                MouseEnteredTile?.Invoke(this, EventArgs.Empty);
            }
        }

        //public event EventHandler MouseEnteredTile;

        private void panelContent_MouseLeave(object sender, EventArgs e)
        {
            // skutocne opustil panelContent
            if (!panelContent.ClientRectangle.Contains(panelContent.PointToClient(Cursor.Position)))
            {
                IsSelected = false;

                Debug.WriteLine(Name + " im out...");

                pictureBoxTypeImage.Image = _tileImage;

                if (_resizing) return;

                MakeSmall();

                MouseLeftTile?.Invoke(this, EventArgs.Empty);
            }
        }

        public uint AnimationSpeed { get; set; } = 200;

        private void MakeBig()
        {
            _resizing = true;
            var t = new Transitions.Transition(new Transitions.TransitionType_Acceleration((int)AnimationSpeed));
            t.add(panelContent, "Left", 10);
            t.add(panelContent, "Top", 10);
            t.add(panelContent, "Width", panelContent.Width + 10);
            t.add(panelContent, "Height", panelContent.Height + 10);
            t.TransitionCompletedEvent += (snd, ea) =>
            {
                _resizing = false;
                if (!IsSelected)
                    MakeSmall();
            };
            t.run();
        }

        private void MakeSmall()
        {
            _resizing = true;
            var t = new Transitions.Transition(new Transitions.TransitionType_Acceleration((int)AnimationSpeed));
            t.add(panelContent, "Left", 15);
            t.add(panelContent, "Top", 15);
            t.add(panelContent, "Width", panelContent.Width - 10);
            t.add(panelContent, "Height", panelContent.Height - 10);
            t.TransitionCompletedEvent += (snd, ea) =>
            {
                _resizing = false;
                if (IsSelected)
                    MakeBig();
            };
            t.run();
        }

        public event EventHandler MouseEnteredTile;

        public event EventHandler MouseLeftTile;

        public event EventHandler MouseClicked;

        private void tileContent_Click(object sender, EventArgs e) => MouseClicked?.Invoke(this, EventArgs.Empty);
    }
}
