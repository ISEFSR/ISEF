namespace cvti.isef.winformapp.Forms.Classifiers
{
    using cvti.data;
    using System;

    public partial class NewKrajForm : DialogBase
    {
        public NewKrajForm(ISEFDataManager manager)
        {
            InitializeComponent();

            ShowImageIcon = false;
        }

        public string Kod { get; private set; }

        public string ShortText { get; private set; }

        public string LongText { get; private set; }

        private bool _fire1 = true, _fire2 = true, _fire3 = true;
        private void cueTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (!_fire1)
                return;

            //short kod = 0;
            if (Int16.TryParse(cueTextBox1.Text, out short kod))
            {
                errorProvider1.SetError(cueTextBox1, string.Empty);
                Kod = cueTextBox1.Text;
            }
            else
            {
                var text = "Kód pre číselník krajov musí byť celé číslo";
                errorProvider1.SetError(cueTextBox1, text);
                toolTipWarning.Show(text, cueTextBox1);

                _fire1 = false;
                cueTextBox1.Text = Kod;
                _fire1 = true;
            }
        }

        private void cueTextBox2_TextChanged(object sender, EventArgs e)
        {
            if (!_fire2)
                return;

            if (cueTextBox2.Text.Length > 2)
            {
                var text = "Maximálny počet znakov pre skráteny text je 2";
                errorProvider1.SetError(cueTextBox2, text);
                toolTipWarning.Show(text, cueTextBox2);

                _fire2 = false;
                cueTextBox2.Text = ShortText;
                _fire2 = true;
            }
            else
            {
                errorProvider1.SetError(cueTextBox2, string.Empty);
                ShortText = cueTextBox2.Text;
            }
        }

        private void cueTextBox3_TextChanged(object sender, EventArgs e)
        {
            if (!_fire3)
                return;

            if (cueTextBox3.Text.Length > 150)
            {
                var text = "Maximálny počet znakov pre text je 150";
                errorProvider1.SetError(cueTextBox3, text);
                toolTipWarning.Show(text, cueTextBox3);

                _fire3 = false;
                cueTextBox3.Text = LongText;
                _fire3 = true;
            }
            else
            {
                errorProvider1.SetError(cueTextBox3, string.Empty);
                LongText = cueTextBox3.Text;
            }
        }
    }
}
