namespace cvti.isef.winformapp.Forms
{
    using cvti.data;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using cvti.data.Enums;
    using System.Windows;

    public partial class ExportForm : DialogBase
    {
        private ISEFDataManager _manager;

        public ExportForm(ISEFDataManager manager)
        {
            InitializeComponent();

            _manager = manager;

            ShowWait();
        }

        public override void ShowWait()
        {
            base.ShowWait();

            tableLayoutPanel1.Visible = false;
        }

        public override void HideWait()
        {
            base.HideWait();

            tableLayoutPanel1.Visible = true;
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var roky = await _manager.MSSQLManager.GetAvailableYearsAsync();
            if (roky.Any())
            {
                foreach (var s in Enum.GetNames(typeof(InputType)))
                    comboBox1.Items.Add(s);

                comboBox1.SelectedIndex = 0;

                foreach (var r in roky)
                    comboBoxRok.Items.Add(r);

                comboBoxRok.SelectedIndex = 0;

                await Task.Delay(1350);

                HideWait();
            }
            else
            {
                MessageBox.Show("Nemáte nahrané žiadne dáta, ktoré by ste mohli exportovať.", "Nenahrané dáta", MessageBoxButton.OK, MessageBoxImage.Information);

                DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
        }

        public int VybranyRok
        {
            get { return Convert.ToInt32(comboBoxRok.SelectedItem); }
        }

        public InputType VybranyStupen { get { return (InputType)comboBox1.SelectedIndex; } }

        private void ExportForm_Load(object sender, EventArgs e)
        {

        }
    }
}
