namespace cvti.isef.winformapp.Forms
{
    using cvti.data.Enums;
    using System;
    using System.Threading.Tasks;

    public partial class RemoveDataForm : DialogBase
    {
        public RemoveDataForm()
        {
            InitializeComponent();

            ShowWait();

            foreach (var s in Enum.GetNames(typeof(InputType)))
                comboBoxStupen.Items.Add(s);

            numericUpDownYear.Value = DateTime.Now.Year;

            comboBoxStupen.SelectedIndex = 0;
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

            await Task.Delay(1350);

            HideWait();
        }

        public InputType Stupen 
        {
            get 
            {
                return (InputType)Enum.Parse(typeof(InputType),
                    comboBoxStupen.SelectedItem.ToString());
            }
            set 
            {
                comboBoxStupen.SelectedItem = value; 
            }
        }
        public int Rok 
        {
            get
            {
                return (int)numericUpDownYear.Value; 
            }
            set 
            {
                numericUpDownYear.Value = value; 
            }
        }
    }
}
