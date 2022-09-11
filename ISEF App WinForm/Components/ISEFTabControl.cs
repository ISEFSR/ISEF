using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cvti.isef.winformapp.Components
{
    public partial class ISEFTabControl : Component
    {
        public ISEFTabControl()
        {
            InitializeComponent();
        }

        public ISEFTabControl(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
