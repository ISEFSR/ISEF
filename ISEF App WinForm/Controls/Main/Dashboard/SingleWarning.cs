namespace cvti.isef.winformapp.Controls.Main.Dashboard
{
    using cvti.data.Classifiers;
    using cvti.isef.winformapp.Forms.Dashboard;
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Polozka pre warning panel. Polozka predstavuje jeden udaj z ciselnika analytickej evidencie s chybajucou textaciou
    /// </summary>
    public partial class SingleWarning : UserControl
    {
        public SingleWarning()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        public SingleWarningItem WarningItem { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        public ChybajuciRiadok ChybajuciRiadok { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="riadok"></param>
        public void ShowWarning(SingleWarningItem item, ChybajuciRiadok riadok)
        {
            WarningItem = item;

            ChybajuciRiadok = riadok;

            ShowItem();
        }

        private void ShowItem()
        {
            labelTitle.Text = WarningItem.Item.TableName;
            labelText.Text = $"Položka {WarningItem.Item.Kod} číselníku {WarningItem.Item.TableName} za rok {WarningItem.Item.Rok} nemá vyplnenú textáciu.";
        }

        private void linkLabelTakeAction_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var changeForm = new ZmenTextaciuForm(WarningItem))
            {
                if (changeForm.ShowDialog() == DialogResult.OK)
                {
                    TextSuccessfullyChanged?.Invoke(this, new TextsChangedEventArgs(changeForm.KratkyText, changeForm.DlhyText));
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var color = Color.LightGray;//Color.FromArgb(30, 91, 130);

            if (_isMouseIn)
            {
                color = Color.Yellow;
            }

            e.Graphics.DrawRectangle(new Pen(new SolidBrush(color), 1f),
                new Rectangle(tableLayoutPanel1.Left - 1, tableLayoutPanel1.Top - 1, tableLayoutPanel1.Width + 1, tableLayoutPanel1.Height + 1));
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<TextsChangedEventArgs> TextSuccessfullyChanged;

        private bool _isMouseIn = false;

        private void ctrl_MouseEnter(object sender, EventArgs e)
        {
            tableLayoutPanel1.BackColor = Color.FromArgb(34, 69, 107);
            Invalidate();
            _isMouseIn = true;
        }

        private void ctrl_MouseLeave(object sender, EventArgs e)
        {
            tableLayoutPanel1.BackColor = Color.FromArgb(54, 89, 127);
            Invalidate();
            _isMouseIn = false;
        }

        private void labelText_Click(object sender, EventArgs e)
        {
            using (var changeForm = new ZmenTextaciuForm(WarningItem))
            {
                if (changeForm.ShowDialog() == DialogResult.OK)
                {
                    TextSuccessfullyChanged?.Invoke(this, new TextsChangedEventArgs(changeForm.KratkyText, changeForm.DlhyText));
                }
            }
        }
    }
}
