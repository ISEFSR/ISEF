namespace cvti.isef.winformapp.Controls.Main.Zostavy
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using cvti.data.Output;

    public partial class NovaZostavaThirdStep : UserControl
    {
        public NovaZostavaThirdStep()
        {
            InitializeComponent();

            foreach (var o in Enum.GetValues(typeof(OkruhZostavyEnum)))
                comboBoxOkruh.Items.Add(o);
        }

        public OkruhZostavyEnum VybranyOkruh { get { return (OkruhZostavyEnum)comboBoxOkruh.SelectedIndex; } }

        public string Zaradenie
        {
            get { return textBoxZaradenie.Text; }
        }

        public string LeftTitle
        {
            get { return textBoxLeft.Text; }
        }
        
        public string RightTitle
        {
            get { return textBoxRight.Text; }
        }

        public string MenoZostavy
        {
            get { return textBoxName.Text; }
        }

        public bool IsValid()
        {
            var isValid = true;
            if (string.IsNullOrWhiteSpace(textBoxLeft.Text))
            {
                errorProvider1.SetError(textBoxLeft, "Text pre nadpis nemôže byť prázdny reťazec");
                isValid = false;
            }
            else
            {
                errorProvider1.SetError(textBoxLeft, string.Empty);
            }
            
            if (string.IsNullOrWhiteSpace(textBoxRight.Text))
            {
                errorProvider1.SetError(textBoxRight, "Text pre nadpis nemôže byť prázdny reťazec");
                isValid = false;
            }
            else
            {
                errorProvider1.SetError(textBoxRight, string.Empty);
            }

            if (string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                errorProvider1.SetError(textBoxName, "Meno zostavy nemôźe byť prázdny reťazec");
                isValid = false;
            }
            else
            {
                errorProvider1.SetError(textBoxName, string.Empty);
            }

            return isValid;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        private void NovaZostavaThirdStep_Paint(object sender, PaintEventArgs e)
        {
            
        }
    }
}
