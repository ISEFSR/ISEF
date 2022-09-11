namespace cvti.isef.winformapp.Forms.Classifiers
{
    using cvti.data.Columns;
    using cvti.data.Views;
    using cvti.data.Enums;
    using System;
    using System.Collections.Generic;

    public partial class SpecifyEkonomickaForm : DialogBase
    {
        public SpecifyEkonomickaForm()
        {
            InitializeComponent();

            ShowImageIcon = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public string GetSelectedTitle()
        {
            if (radioButton1.Checked)
            {
                return "Ekonomické položky na 1 znak";
            }
            else if (radioButton3.Checked)
            {
                return "Ekonomické položky na 2 znaky";
            }
            else if (radioButton2.Checked)
            {
                return "Ekonomické položky na 3 znaky";
            }
            else
            {
                return "Ekonomické položky na 6 znakov";
            }
        }

        public AssuViewColumn[] GetSelectedColumns()
        {
            var clmns = new List<AssuViewColumn>();

            clmns.Add(AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod, true, "Segment"));
            clmns.Add(AssuView.VratStlpec(AssuViewAvailableColumns.SegmentShort, true, "Segment názov"));

            if (radioButton1.Checked)
            {
                clmns.AddRange( new AssuViewColumn[]
                {
                    AssuView.VratStlpec(AssuViewAvailableColumns.EKod1, true, "Kod"),
                    AssuView.VratStlpec(AssuViewAvailableColumns.ENazov1, true, "Názov")
                } );
            }
            else if (radioButton3.Checked)
            {
                clmns.AddRange( new AssuViewColumn[]
                {
                    AssuView.VratStlpec(AssuViewAvailableColumns.EKod2, true, "Kod"),
                    AssuView.VratStlpec(AssuViewAvailableColumns.ENazov2, true, "Názov")
                } );
            }
            else if (radioButton2.Checked)
            {
                clmns.AddRange( new AssuViewColumn[]
                {
                    AssuView.VratStlpec(AssuViewAvailableColumns.EKod3, true, "Kod"),
                    AssuView.VratStlpec(AssuViewAvailableColumns.ENazov3, true, "Názov")
                } );
            }
            else
            {
                clmns.AddRange( new AssuViewColumn[]
                {
                    AssuView.VratStlpec(AssuViewAvailableColumns.EKod6, true, "Kod"),
                    AssuView.VratStlpec(AssuViewAvailableColumns.ENazov6, true, "Názov")
                } );
            }

            return clmns.ToArray();
        }
    }
}
