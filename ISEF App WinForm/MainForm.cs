namespace cvti.isef.winformapp
{
    using Microsoft.Win32;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Hlavne okno aplikacie
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Licencia pre EPPlus
            OfficeOpenXml.ExcelPackage.LicenseContext = 
                OfficeOpenXml.LicenseContext.NonCommercial;

            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers", 
                System.Reflection.Assembly.GetEntryAssembly().Location, "HIGHDPIAWARE");

            Properties.Settings.Default.ShouldInitialize = true;
#if DEBUGRESET
            Properties.Settings.Default.ShouldInitialize = true; 
#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        /// <summary>
        /// Inicializuje hlavne okno aplikacie a nastavi ikonu
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            // Nastavenie ikony hlavneho okna aplikacie
            Icon = Icon.FromHandle(Properties.Resources.app_logo.GetHicon());
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Spustenie inicializacie main controlu
            await mainControl.Initialize();
        }

        protected override async void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            // Ukoncenie cinnosti, ulozenie suborov 
            await mainControl.Quit();
        }
    }
}