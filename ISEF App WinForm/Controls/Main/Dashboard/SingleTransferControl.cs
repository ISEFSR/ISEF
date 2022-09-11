namespace cvti.isef.winformapp.Controls.Main.Dashboard
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
    using cvti.data.Tables;

    public partial class SingleTransferControl : UserControl
    {
        public SingleTransferControl()
        {
            InitializeComponent();
        }

        public void Zobraz(EkonomickaRiadok6 ekonomicka, StupenRiadok fromStupen, StupenRiadok toStupen, decimal sum)
        {
            label1.Text = $"{ekonomicka.Kod} - {ekonomicka.Nazov}";

            label2.Text = fromStupen.Popis + " >";

            label3.Text = toStupen.Popis;

            label4.Text = sum.ToString("c");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
