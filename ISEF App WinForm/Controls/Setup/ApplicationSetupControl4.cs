using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using cvti.data;
using cvti.data.Core;

namespace cvti.isef.winformapp.Controls.Setup
{
    public partial class ApplicationSetupControl4 : UserControl
    {
        public ApplicationSetupControl4()
        {
            InitializeComponent();
        }

        public void ShowData(string outputDir, string dataDir, string headerDir, MSSQLServer server)
        {
            labelServer.Text = server.ServerAddress;
            labelDatabase.Text = server.DatabaseName;
            labelPrihlasenie.Text = server.NamePasswordLogin ? "Meno a heslo" : "Windows authentication";
            labelOutputDir.Text = outputDir;
            labelDataDir.Text = dataDir;
            labelHeaderDir.Text = headerDir;
        }
    }
}
