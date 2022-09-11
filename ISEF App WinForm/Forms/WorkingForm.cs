namespace cvti.isef.winformapp.Helpers
{
    using System;
    using System.Drawing;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    /// <summary>
    /// Form indikujuci, ze program pracuje 
    /// </summary>
    public partial class WorkingForm : Form
    {
        private readonly Task _task;

        public WorkingForm(Task task)
        {
            InitializeComponent();
            this._task = task;
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            await _task;
            await Task.Delay(450);

            DialogResult = DialogResult.OK;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.DrawRectangle(new Pen(Brushes.LightGray),
                new Rectangle(0,0, Width -1, Height -1));
        }

        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }
    }
}
