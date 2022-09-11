namespace cvti.isef.winformapp.Controls.Main.Dashboard
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;

    /// <summary>
    /// Predstavuje control, ktory by mal zobrazovat spravy a novinky
    /// </summary>
    public partial class WelcomeBoard : UserControl
    {
        #region Variables And Constructors

        private bool _moving = false;

        private int _index = 0;
        private readonly List<WelcomeBoardItem> _items =
            new List<WelcomeBoardItem>();

        /// <summary>
        /// Inicializuje welcome board
        /// </summary>
        public WelcomeBoard()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Zobrazi polozky 
        /// </summary>
        /// <remarks>
        /// ak je argument null alebo prazdny tak iba vyprazdni welcome board
        /// </remarks>
        /// <param name="items">Polozky ako <see cref="IEnumerable{T}"/> kde T je <see cref="WelcomeBoardItem"/></param>
        public void ShowItems(IEnumerable<WelcomeBoardItem> items)
        {
            var wasRunning = timerMoveNews.Enabled;
            timerMoveNews.Stop();
            _items.Clear();
            _index = 0;

            // ak neboli posunute ziadne polozky tak nic nerobim
            if (items is null || items.Count() == 0)
                return;

            // naplnim polozky
            // a zobrazim prvu polozku
            _items.AddRange(items);
            ShowItemAtIndex();

            // Ak predtym timer bezal spustam ho aj teraz
            if (wasRunning)
            {
                timerMoveNews.Start();
            }
        }

        /// <summary>
        /// Vrati hodnotu hovoriacu o tom ci welcome board zobrazuje polozky
        /// </summary>
        /// <value>
        /// Hodnota hovoriaca o tom, ci welcome board zobrazuje polzoky
        /// </value>
        public bool IsRunning { get => timerMoveNews.Enabled; }

        /// <summary>
        /// Spusti prepinananie medzi polozkami ak nejake polozky su v zozname
        /// </summary>
        public void Start()
        {
            if (_items.Count == 0)
                return;

            timerMoveNews.Start();
        }

        /// <summary>
        /// Zastavi prepinanie poloziek
        /// </summary>
        public void Stop()
        {
            timerMoveNews.Stop();
        }

        #endregion

        #region Protected And Private Members

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawShadow(e.Graphics);
        }

        private void DrawShadow(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            const int ShadowSize = 5;
            for (var i = 0; i < ShadowSize; i++)
            {
                // Right shadow
                g.DrawLine(new Pen(Color.FromArgb(140, 140, 140), 1.0f),
                    new Point(panelContent.Location.X + panelContent.Width + i, panelContent.Location.Y + panelContent.Height + i - 1),
                    new Point(panelContent.Location.X + panelContent.Width + i, panelContent.Location.Y + i));
                // Bottom shadow
                g.DrawLine(new Pen(Color.FromArgb(140, 140, 140), 1.0f),
                    new Point(panelContent.Location.X + i, panelContent.Location.Y + panelContent.Height + i),
                    new Point(panelContent.Location.X + panelContent.Width + i, panelContent.Location.Y + panelContent.Height + i));
            }
        }

        private void timerMoveNews_Tick(object sender, EventArgs e)
            => ShowNext();

        private void ShowNext(bool resetTimer = false)
        {
            if (_items.Count == 0 || _moving) 
                return;

            var wasRunning = IsRunning;
            if (resetTimer && wasRunning)
                timerMoveNews.Stop();

            _moving = true;

            // TODO: dorob animaciu cez transiotions
            _index++;
            if (_index == _items.Count)
                _index = 0;

            ShowItemAtIndex();
            _moving = false;
            
            if (resetTimer && wasRunning)
                timerMoveNews.Start();
        }

        private void ShowPrev(bool resetTimer = false)
        {
            if (_items.Count == 0 || _moving) 
                return;

            var wasRunning = IsRunning;
            if (resetTimer && wasRunning)
                timerMoveNews.Stop();

            _moving = true;

            // TODO: dorob animaciu cez transiotions
            _index--;
            if (_index == -1)
                _index = _items.Count - 1;

            ShowItemAtIndex();
            _moving = false;

            if (resetTimer && wasRunning)
                timerMoveNews.Start();
        }

        private void ShowItemAtIndex()
        {
            var item = _items.ElementAt(_index);

            labelTitle.Text = item.Title;
            labelInfo.Text = item.InfoText;

            tableLayoutPanelData.BackgroundImage = item.BackgroundImage;

            if (item.SmallImage ==  null)
            {
                pictureBoxIcon.Visible = false;

            }
            else
            {
                pictureBoxIcon.Visible = true;
                pictureBoxIcon.Image = item.SmallImage;
            }

            BackColor = item.BackgroundColor;
            ForeColor = item.ForegroundColor;
        }

        private void buttonPrevious_Click(object sender, EventArgs e) => ShowPrev(true);

        private void buttonNext_Click(object sender, EventArgs e) => ShowNext(true);

        private void WelcomeBoard_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanelData_Paint(object sender, PaintEventArgs e)
        {

        }

        #endregion
    }
}
