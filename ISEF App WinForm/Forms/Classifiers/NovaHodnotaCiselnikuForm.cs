using cvti.data.Classifiers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cvti.isef.winformapp.Forms
{
    public partial class NovaHodnotaCiselnikuForm : DialogBase
    {
        public NovaHodnotaCiselnikuForm(AnalytickaEvidenciaNovaHodnota hodnota)
        {
            InitializeComponent();

            ShowImageIcon = false;

            textBox1.Text = hodnota.Rok.ToString();
            textBox2.Text = hodnota.Kod;
            textBox3.Text = hodnota.Skrateny;
            textBox4.Text = hodnota.Popis;
        }

        [Browsable(false)]
        public string SkratenyNazov { get => textBox3.Text; }

        [Browsable(false)]
        public string PopisnyNazov { get => textBox4.Text; }
    }
}
