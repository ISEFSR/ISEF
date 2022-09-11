using cvti.data.Conditions;
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

namespace cvti.isef.winformapp.Forms.Conditions
{
    public partial class ImportConditionsForm : DialogBase
    { 
        public ImportConditionsForm(string fromFile)
            : this(FileManagerJson<Condition>.ReadJsonFileData(fromFile))
        {

        }

        public ImportConditionsForm(IEnumerable<Condition> conditions)
        {
            InitializeComponent();

            //ShowWait();

            ReadConditions(conditions);
        }

        private void ReadConditions(IEnumerable<Condition> conditions)
        {
            
            try
            {
                foreach (var c in conditions)
                    checkedListBoxConditions.Items.Add(c, true);

            }
            catch (Exception e)
            {
                MessageBox.Show("Pri pokuse o načítanie podmienok z importného súboru nastala neočakávaná chyba: " + e.Message,
                    "Neočakávaná chyba " + e.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //await Task.Delay(1350);

            //HideWait();

            if (checkedListBoxConditions.Items.Count == 0)
                DialogResult = DialogResult.Cancel;
        }

        public override void HideWait()
        {
            base.HideWait();

            checkedListBoxConditions.Visible = true;

            labelTitle.Visible = true;
        }

        public override void ShowWait()
        {
            base.ShowWait();

            checkedListBoxConditions.Visible = false;

            labelTitle.Visible = false;
        }

        public IEnumerable<Condition> GetSelectedConditions() 
            => checkedListBoxConditions.CheckedItems.Cast<Condition>();
    }
}
