namespace cvti.isef.winformapp.Forms
{
    using cvti.data.Conditions;
    using cvti.data.Core;
    using cvti.data.Files;
    using System;
    using System.Drawing;

    public partial class SaveGeneratedCondition : DialogBase
    {
        private readonly Icon _icoError;
        private readonly Icon _icoOk;

        public SaveGeneratedCondition(ConditionsManagerJson manager, Condition condition)
        {
            _manager = manager;
            _condition = condition;

            InitializeComponent();
        
            _icoError = Icon.FromHandle(Properties.Resources.cancel30.GetHicon());
            _icoOk = Icon.FromHandle(Properties.Resources.ok30.GetHicon());

            textBoxConditionText.Text = _condition.GetConditionString(true);

            errorProvider.SetIconPadding(textBoxConditonName, 5);
        }

        public string ConditionName { get { return textBoxConditonName.Text; } }

        private readonly Condition _condition;
        private readonly ConditionsManagerJson _manager;

        private void SaveGeneratedCondition_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(new SolidBrush(Color.LightGray), 1),
                new Point(5, labelInfo.Location.Y + labelInfo.Height + 1),
                new Point(Width - 25, labelInfo.Location.Y + labelInfo.Height + 1));

            if (string.IsNullOrWhiteSpace(errorProvider.GetError(textBoxConditonName)))
                return;

            Color borderColor = Color.Red;  
            
            if (errorProvider.Icon == _icoOk)
            {
                borderColor = Color.Green;
            }

            e.Graphics.DrawRectangle(new Pen(new SolidBrush(borderColor)),
                new Rectangle(textBoxConditonName.Location.X - 1, textBoxConditonName.Location.Y - 1,
                    textBoxConditonName.Width + 1, textBoxConditonName.Height + 1));
        }

        private void textBoxConditonName_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxConditonName.Text) ||
                _manager.GetValue(textBoxConditonName.Text) != null)
            {
                errorProvider.Icon = _icoError;
                errorProvider.SetError(textBoxConditonName, "Podmienka s vybraným meno už existuje");
            }
            else
            {
                errorProvider.Icon = _icoOk;
                errorProvider.SetError(textBoxConditonName, "Podmienka s vybraným meno ešte neexistuje");
            }

            Invalidate();
        }
    } 
}
