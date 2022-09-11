namespace cvti.isef.winformapp.Forms.Classifiers
{
    using cvti.data.Columns;
    using cvti.data.Enums;
    using cvti.data.Views;
    using System;
    using System.Collections.Generic;

    public partial class SpecifyFunkcnaForm : DialogBase
    {
        public SpecifyFunkcnaForm()
        {
            InitializeComponent();

            ShowImageIcon = false;
        }

        public string GetSelectedTitle()
        {
            if (radioButton1.Checked)
            {
                return "Funkčné položky na 2 znaky";
            }
            else if (radioButton2.Checked)
            {
                return "Funkčné položky na 3 znaky";
            }
            else if (radioButton3.Checked)
            {
                return "Funkčné položky na 4 znaky";
            }
            else
            {
                return "Funkčné položky na 5 znakov";
            }
        }

        public AssuViewColumn[] GetSelectedColumns()
        {
            var clmns = new List<AssuViewColumn>();
            clmns.Add(AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod, true, "Segment"));
            clmns.Add(AssuView.VratStlpec(AssuViewAvailableColumns.SegmentShort, true, "Segment názov"));

            if (radioButton1.Checked)
            {
                clmns.AddRange(new AssuViewColumn[]
                {
                    AssuView.VratStlpec(AssuViewAvailableColumns.FKod2, true, "Kod"),
                    AssuView.VratStlpec(AssuViewAvailableColumns.FNazov2, true, "Názov")
                });
            }
            else if (radioButton2.Checked)
            {
                clmns.AddRange(new AssuViewColumn[]
                {
                    AssuView.VratStlpec(AssuViewAvailableColumns.FKod3, true, "Kod"),
                    AssuView.VratStlpec(AssuViewAvailableColumns.FNazov3, true, "Názov")
                });
            }
            else if (radioButton3.Checked)
            {
                clmns.AddRange(new AssuViewColumn[]
                {
                    AssuView.VratStlpec(AssuViewAvailableColumns.FKod4, true, "Kod"),
                    AssuView.VratStlpec(AssuViewAvailableColumns.FNazov4, true, "Názov")
                });
            }
            else
            {
                clmns.AddRange(new AssuViewColumn[]
                {
                    AssuView.VratStlpec(AssuViewAvailableColumns.FKod5, true, "Kod"),
                    AssuView.VratStlpec(AssuViewAvailableColumns.FNazov5, true, "Názov")
                });
            }

            return clmns.ToArray();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
