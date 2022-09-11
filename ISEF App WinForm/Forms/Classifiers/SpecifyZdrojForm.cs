namespace cvti.isef.winformapp.Forms.Classifiers
{
    using cvti.data.Columns;
    using cvti.data.Enums;
    using cvti.data.Views;

    public partial class SpecifyZdrojForm : DialogBase
    {
        public SpecifyZdrojForm()
        {
            InitializeComponent();

            ShowImageIcon = false;
        }

        public string GetSelectedTitle()
        {
            if (radioButton1.Checked)
            {
                return "Zdrojové položky na 1 znak";
            }
            else if (radioButton3.Checked)
            {
                return "Zdrojové položky na 2 znaky";
            }
            else if (radioButton2.Checked)
            {
                return "Zdrojové položky na 3 znaky";
            }
            else
            {
                return "Zdrojové položky na 4 znaky";
            }
        }

        public AssuViewColumn[] GetSelectedColumns()
        {
            if (radioButton1.Checked)
            {
                return new AssuViewColumn[]
                {
                    AssuView.VratStlpec(AssuViewAvailableColumns.ZKod1, true, "Kod"),
                    AssuView.VratStlpec(AssuViewAvailableColumns.ZNazov1, true, "Názov")
                };
            }
            else if (radioButton3.Checked)
            {
                return new AssuViewColumn[]
                {
                    AssuView.VratStlpec(AssuViewAvailableColumns.ZKod2, true, "Kod"),
                    AssuView.VratStlpec(AssuViewAvailableColumns.ZNazov2, true, "Názov")
                };
            }
            else if (radioButton2.Checked)
            {
                return new AssuViewColumn[]
                {
                    AssuView.VratStlpec(AssuViewAvailableColumns.ZKod3, true, "Kod"),
                    AssuView.VratStlpec(AssuViewAvailableColumns.ZNazov3, true, "Názov")
                };
            }
            else
            {
                return new AssuViewColumn[]
                {
                    AssuView.VratStlpec(AssuViewAvailableColumns.ZKod4, true, "Kod"),
                    AssuView.VratStlpec(AssuViewAvailableColumns.ZNazov4, true, "Názov")
                };
            }
        }
    }
}
