namespace cvti.isef.winformapp.Forms.Classifiers
{
    using cvti.data.Columns;
    using cvti.data.Enums;
    using cvti.data.Views;

    public partial class SpecifyProgramForm : DialogBase
    {
        public SpecifyProgramForm()
        {
            InitializeComponent();

            ShowImageIcon = false;
        }

        public string GetSelectedTitle()
        {
            if (radioButton1.Checked)
            {
                return "Programové položky na 3 znaky";
            }
            else if (radioButton3.Checked)
            {
                return "Programové položky na 5 znakov";
            }
            else 
            {
                return "Programové položky na 7 znakov";
            }
        }

        public AssuViewColumn[] GetSelectedColumns()
        {
            if (radioButton1.Checked)
            {
                return new AssuViewColumn[]
                {
                    AssuView.VratStlpec(AssuViewAvailableColumns.PKod3, true, "Kod"),
                    AssuView.VratStlpec(AssuViewAvailableColumns.PNazov3, true, "Názov")
                };
            }
            else if (radioButton3.Checked)
            {
                return new AssuViewColumn[]
                {
                    AssuView.VratStlpec(AssuViewAvailableColumns.PKod5, true, "Kod"),
                    AssuView.VratStlpec(AssuViewAvailableColumns.PNazov5, true, "Názov")
                };
            }
            else
            {
                return new AssuViewColumn[]
                {
                    AssuView.VratStlpec(AssuViewAvailableColumns.PKod7, true, "Kod"),
                    AssuView.VratStlpec(AssuViewAvailableColumns.PNazov7, true, "Názov")
                };
            }
        }
    }
}
