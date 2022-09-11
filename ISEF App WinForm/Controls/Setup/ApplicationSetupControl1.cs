using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;

namespace cvti.isef.winformapp.Controls.Setup
{
    public partial class ApplicationSetupControl1 : UserControl
    {
        public ApplicationSetupControl1()
        {
            InitializeComponent();

            linkLabelDownloadVFPOLEDB.Text = Properties.Resources.VFPOLEDBLink;

            linkLabel1.Text = Properties.Resources.VFPOLEDB8Link;
        }

        public bool ValidateData()
        {
            return IsVFPOLEDBInstalled();
        }

        private bool IsVFPOLEDBInstalled()
        {
            return Registry.ClassesRoot.OpenSubKey("TypeLib\\{50BAEECA-ED25-11D2-B97B-000000000000}") != null;
        }

        private void linkLabelDownloadVFPOLEDB_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(linkLabelDownloadVFPOLEDB.Text);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(linkLabel1.Text);
        }
    }
}
