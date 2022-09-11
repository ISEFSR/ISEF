using cvti.data;
using cvti.data.Conditions;
using cvti.data.Tables;
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
    public partial class ExportHlavickyForm : DialogBase
    {
        private ISEFDataManager _manager;

        public ExportHlavickyForm(ISEFDataManager manager)
        {
            _manager = manager;

            InitializeComponent();
            ShowImageIcon = false;

            foreach (var s in manager.Segmenty)
                comboBoxSegment.Items.Add(s);

            foreach (var c in manager.CoreFiles.Conditions.Values)
                comboBoxCondition.Items.Add(c);
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            foreach (var r in await _manager.MSSQLManager.GetAvailableYearsAsync())
                comboBoxRok.Items.Add(r);
        }

        public SegmentRiadok VybranySegment
        {
            get
            {
                return comboBoxSegment.SelectedItem as SegmentRiadok;
            }
        }

        public Condition VybranaPodmienka
        {
            get
            {
                return comboBoxCondition.SelectedItem as Condition;
            }
        }

        public int VybranyRok
        {
            get
            {
                return Convert.ToInt32(comboBoxRok.SelectedItem);
            }
        }
    }
}
