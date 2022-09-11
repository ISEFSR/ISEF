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

namespace cvti.isef.winformapp.Controls.Main.Dashboard
{
    public partial class ActionPreview : UserControl
    {
        public ActionPreview()
        {
            InitializeComponent();
        }

        public void ShowAction(LogMessage log)
        {
            label1.Text = log.LogTitle;
            label2.Text = log.LogInfo;
        }
    }
}
