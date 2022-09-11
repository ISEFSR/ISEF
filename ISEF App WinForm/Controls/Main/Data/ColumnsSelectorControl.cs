using System.Collections.Generic;
using System.Windows.Forms;

namespace cvti.isef.winformapp.Controls.Main.Data
{
    public partial class ColumnsSelectorControl : UserControl
    {
        public ColumnsSelectorControl()
        {
            InitializeComponent();
        }

        public IEnumerable<string> SelectedColumns { get; set; }
    }
}
