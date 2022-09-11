namespace cvti.isef.winformapp.Controls.Setup
{
    using System;
    using System.Windows.Forms;

    public enum DirectoryOptions
    {
        Application,
        AppData,
        Documents
    }

    public partial class ApplicationSetupControl3 : UserControl
    {
        public ApplicationSetupControl3()
        {
            InitializeComponent();

            foreach (var dOption in Enum.GetValues(typeof(DirectoryOptions)))
            {
                comboBoxData.Items.Add(dOption);
                comboBoxHlavicky.Items.Add(dOption);
                comboBoxVystup.Items.Add(dOption);
            }

            comboBoxData.SelectedIndex = 0;
            comboBoxHlavicky.SelectedIndex = 0;
            comboBoxVystup.SelectedIndex = 0;
        }

        public string DataDirectory { get { return GetDirectory((DirectoryOptions)comboBoxData.SelectedIndex); } }
        public string HlavickyDirectory { get { return GetDirectory((DirectoryOptions)comboBoxHlavicky.SelectedIndex); } }
        public string OutputDirectory { get { return GetDirectory((DirectoryOptions)comboBoxVystup.SelectedIndex); } }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        public bool ValidateData()
        {
            return true;
        }

        private string GetDirectory(DirectoryOptions o)
        {
            switch (o)
            {
                case DirectoryOptions.Application:
                    return Environment.CurrentDirectory;
                case DirectoryOptions.AppData:
                    return System.IO.Path.Combine( Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CVTI SR", "ISEF Data Processing" );
                case DirectoryOptions.Documents:
                    return System.IO.Path.Combine( Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "CVTI SR", "ISEF Data Processing" );
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
