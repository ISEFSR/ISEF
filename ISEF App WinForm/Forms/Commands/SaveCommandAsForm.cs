namespace cvti.isef.winformapp.Forms.Commands
{
    using cvti.data.Files;
    using System;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class SaveCommandAsForm : DialogBase
    {
        private CommandsManagerJson _commands;

        public SaveCommandAsForm(CommandsManagerJson commands)
        {
            InitializeComponent();

            ShowWait();

            ShowImageIcon = false;

            _commands = commands;
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            await Task.Delay(1350);

            HideWait();
        }

        public override void HideWait()
        {
            base.HideWait();

            label2.Visible = true;

            label3.Visible = true;

            textBoxName.Visible = true;
        }

        public override void ShowWait()
        {
            base.ShowWait();

            label2.Visible = false;

            label3.Visible = false;

            textBoxName.Visible = false;
        }

        public string CommandName
        {
            get { return textBoxName.Text; }
            set { textBoxName.Text = value; }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (_commands.GetValue(textBoxName.Text) != null)
            {
                errorProvider.SetError(textBoxName, "Príkaz s takýmto menom už existuje, vyberte iné prosím...");
                return;
            }

            DialogResult = DialogResult.OK;
        }
    }
}
