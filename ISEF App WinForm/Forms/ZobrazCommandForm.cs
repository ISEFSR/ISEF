using cvti.data.Output;

namespace cvti.isef.winformapp.Forms
{
    /// <summary>
    /// Sluzi na zobrazenie jedneho SQL select commandu
    /// </summary>
    public partial class ZobrazCommandForm : DialogBase
    {
        public ZobrazCommandForm(SelectCommand command)
        {
            InitializeComponent();

            scintilla1.Text = command.GenerateCommand().CommandText;
        }
    }
}
