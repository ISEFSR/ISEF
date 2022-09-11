namespace cvti.isef.winformapp.Forms
{
    using cvti.data.Files;
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    /// <summary>
    /// Zobrazi a umozni zmenit nastavenia aplikacie, ako su pripojenie na databazovy server a klucove subory aplikacie
    /// </summary>
    /// <remarks>
    /// Neumoznuje zmeny nastaveni, nastavenia sa daju zmenit len pri restarte nastaveni cez hlavne menu aplikacie
    /// </remarks>
    public partial class SettingsForm : DialogBase
    {
        public SettingsForm()
        {
            InitializeComponent();

            ShowWait();
        }

        public override void ShowWait()
        {
            base.ShowWait();

            tabControlMain.Visible = false;
        }

        public override void HideWait()
        {
            base.HideWait();

            tabControlMain.Visible = true;
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            await Task.Delay(1350);

            HideWait();

            linkLabelOutputDirectory.Text = Properties.Settings.Default.OutputDirectory;
            linkLabelHeadersDirectory.Text = Properties.Settings.Default.HlavickyDirectory;
            linkLabelDataDirectory.Text = Properties.Settings.Default.DataDirectory;

            linkLabelCommands.Text = System.IO.Path.Combine(Properties.Settings.Default.DataDirectory, CommandsManagerJson.FileName);
            linkLabelConditions.Text = System.IO.Path.Combine(Properties.Settings.Default.DataDirectory, ConditionsManagerJson.FileName);
            linkLabelHeaders.Text = System.IO.Path.Combine(Properties.Settings.Default.DataDirectory, HlavickyManagerJson.FileName);
            linkLabelReports.Text = System.IO.Path.Combine(Properties.Settings.Default.DataDirectory, ZostavyManagerJson.FileName);

            comboBoxServerName.Items.Add(Properties.Settings.Default.ServerAddress);
            comboBoxServerName.SelectedIndex = 0;

            comboBox2.Items.Add(Properties.Settings.Default.DatabaseName);
            comboBox2.SelectedIndex = 0;

            comboBoxAuth.SelectedIndex = Properties.Settings.Default.ServerNamePassLogin ? 1 : 0;

            if (Properties.Settings.Default.ServerNamePassLogin)
            {
                textBox1.Text = "**********";
                textBox2.Text = "**********";
                textBox1.Visible = true;
                textBox2.Visible = true;
                buttonShowPass.Visible = true;
                buttonShowName.Visible = true;

                label3.Visible = true;
                label4.Visible = true;
            }
            else
            {
                textBox1.Visible = false;
                textBox2.Visible = false;
                buttonShowPass.Visible = false;
                buttonShowName.Visible = false;

                label3.Visible = false;
                label4.Visible = false;
            }
        }

        public string OutputDirectory
        {
            get => linkLabelOutputDirectory.Text;
        }

        public string DataDirectory
        {
            get => linkLabelDataDirectory.Text;
        }

        public string HlavickyDirectory
        {
            get => linkLabelHeadersDirectory.Text;
        }

        private void ll_StartProcess(object sender, LinkLabelLinkClickedEventArgs e)
            => Process.Start((sender as LinkLabel).Text);

        private void buttonShowName_Click(object sender, EventArgs e)
        {
            textBox1.Text = Properties.Settings.Default.DatabaseLogin;
        }

        private void buttonShowPass_Click(object sender, EventArgs e)
        {
            textBox2.Text = Properties.Settings.Default.DatabasePassword;
        }
    }
}
