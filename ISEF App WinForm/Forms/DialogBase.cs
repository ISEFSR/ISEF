namespace cvti.isef.winformapp.Forms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Base form pre dialogove okna aplikacie
    /// </summary>
    public partial class DialogBase : Form
    {
        #region Variables And Constructors

        public DialogBase()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Vrati nastavi informacny text pri zobrazenom load paneli
        /// </summary>
        /// <value>
        /// Informacny text pri zobrazenom load paneli ako <see cref="string"/>
        /// </value>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public string LoadInfo 
        {
            get => isefLabel1.Text;
            set => isefLabel1.Text = value; 
        }

        public bool IsWaitVisible() => tableLayoutPanelWorking.Visible;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public string TitleText
        {
            get => labelTitle.Text; 
            set => labelTitle.Text = value; 
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public bool ShowOkButton
        {
            get => button1.Visible; 
            set
            {
                button1.Visible = value;

                Refresh();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public bool ShowCancelButton
        {
            get => button2.Visible; 
            set
            {
                button2.Visible = value;

                Refresh();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public bool ShowButtonsPanel
        {
            get => panelControlButtons.Visible; 
            set
            {
                panelControlButtons.Visible= value;

                Refresh();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Image ImageIcon
        {
            get
            {
                return pictureBoxImageIcon.Image;
            }
            set
            {
                pictureBoxImageIcon.Image = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public bool ShowImageIcon
        {
            get
            {
                return pictureBoxImageIcon.Visible;
            }
            set
            {
                pictureBoxImageIcon.Visible = value;

                Refresh();
            }
        }

        public bool DrawGradient { get; set; } = false;

        #endregion

        #region Public Methods

        public virtual void ShowWait()
        {
            UseWaitCursor = true;
            tableLayoutPanelWorking.Visible = tableLayoutPanelWorking.Enabled = true;
            tableLayoutPanelWorking.BringToFront();
        }

        public virtual void HideWait()
        {
            UseWaitCursor = false;
            tableLayoutPanelWorking.Visible = tableLayoutPanelWorking.Enabled = false;
        }

        #endregion

        private void panelControlButtons_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            e.Graphics.DrawLine(new Pen(Color.FromArgb(200, 200, 200), 1.5f),
                new PointF(0, 1),
                new PointF(panelControlButtons.Width, 1));
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            tableLayoutPanelWorking.Location =
                new Point(
                    Width / 2 - tableLayoutPanelWorking.Width / 2,
                    Height / 2 - tableLayoutPanelWorking.Height / 2);
        }

        protected virtual bool OnConfirmClicked(object sender, EventArgs e) { return true; }

        protected virtual bool OnCancelClicked(object sender, EventArgs e) { return true; }

        private void button1_Click(object sender, EventArgs e)
        {
            if (OnConfirmClicked(sender, e))
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (OnCancelClicked(sender, e))
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        private void panelSeparator_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.DrawLine(new Pen(Color.FromArgb(200, 200, 200), 1.5f),
                new PointF(0, 1),
                new PointF(flowLayoutPanelButtons.Width, 1));
        }
    }
}