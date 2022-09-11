using cvti.data;
using cvti.isef.winformapp.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cvti.isef.winformapp.Controls.Main.Ciselniky
{
    public partial class NewPodriadenostForm : DialogBase
    {
        public NewPodriadenostForm(ISEFDataManager manager)
        {
            InitializeComponent();

            ShowImageIcon = false;
        }
    }
}
