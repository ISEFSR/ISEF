using cvti.data.Files;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cvti.isef.winformapp.Forms
{
    /// <summary>
    /// Form by mal umoznit vybrat nove meno pre SQL SELECT dotaz 
    /// </summary>
    /// <remarks>
    /// Prebehne tu kontrola na vsetky existujuce mena SQL SELECT dotazov
    /// </remarks>
    public partial class ComandNameForm : DialogBase
    {
        public string NewCommandname { get => nameTextBox.Text; }

        private readonly IEnumerable<string> _names;

        public ComandNameForm(IEnumerable<string> names)
        { 
            _names = names;
            InitializeComponent();
        }

        public ComandNameForm(IEnumerable<string> names, string name)
        {
            _names = names;
            InitializeComponent();

            nameTextBox.Text = name;
        }

        public ComandNameForm(CommandsManagerJson manager)
        {
            InitializeComponent();

            _names = from v in manager.Values select v.CommandName;
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (_names.Contains(nameTextBox.Text))
            {
                error.SetError(nameTextBox, "Meno už existuje");
                button1.Enabled = false;
            } else
            {
                button1.Enabled = true;
            }
        }
    }
}