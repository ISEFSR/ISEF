using cvti.data.Output;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cvti.isef.winformapp.Forms.Zostavy
{
    public partial class ImportZostavyForm : Form
    {
        public ImportZostavyForm()
        {
            InitializeComponent();
        }

        public IEnumerable<Zostava> SelectedZostavy { get; private set; }
                = new List<Zostava>();
    }
}
