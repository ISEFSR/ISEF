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
    public partial class ISEFDataGridView : DataGridView
    {
        public ISEFDataGridView()
        {
            InitializeComponent();

            DoubleBuffered = true;
        }

        public ISEFDataGridView(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            DoubleBuffered = true;
        }

        private void dgv_MouseEvent(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void ISEFDAtaGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // row highlight
        }
    }
}
