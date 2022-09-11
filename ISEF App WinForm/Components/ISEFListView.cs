using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cvti.isef.winformapp.Components
{
    public partial class ISEFListView : ListView
    {
        public ISEFListView()
        {
            InitializeComponent();
        }

        public ISEFListView(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
