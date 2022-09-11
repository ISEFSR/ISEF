namespace cvti.isef.winformapp.Controls
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Diagnostics;
    using cvti.data;
    using cvti.isef.winformapp.Properties;
    using cvti.isef.winformapp.Forms;
    using cvti.data.Files;
    using cvti.data.Core;

    /// <summary>
    /// User control that should just show initialization proggress and report back with events signalizing success / failure
    /// User control returining instance of mssqldatamanager on successfull initialization and returningh instance of InitializationReport on failure
    /// </summary>
    public partial class InitializationControl : UserControl
    {
        #region Public Events

        /// <summary>
        /// Initialization process failed see report <see cref="InitializationReport"/>
        /// </summary>
        public event EventHandler<InitializationReport> InitializationFailed;
        /// <summary>
        /// Successfully initialized data manager, checked connection and vlaidate databsae as <see cref="ISEFDataManager"/>
        /// </summary>
        public event EventHandler<ISEFDataManager> SuccessfullyInitialized;

        #endregion

        #region Variables And Constructors

        private volatile bool _initializationRunning = false;

        public InitializationControl()
        {
            InitializeComponent();
        }

        #endregion

        public async Task<bool> Initalize()
        {
            if (_initializationRunning)
                return false;

            _initializationRunning = true;

            progressBarLoadingIndicator.Value = 50;
            progressBarLoadingIndicator.Style = ProgressBarStyle.Marquee;

            // minimal wait time in miliseconds
            const int MinimalWaitTime = 4500; 
            var timer = new Stopwatch();
            timer.Start();

            await Task.Delay(350);
            BackgroundImage = Resources.isefinit4;
            await Task.Delay(850);

            bool dataDirectory = true;
            bool hlavickyDirectory = true;
            bool outputDirectory = true;
            // Pri prvom spusteni prebehne inicializacia
            // Aplikacia je spustena prvy krat treba spustit inicializacny proces 
            // Nastavit MSSQL server, vytvorit potrebne subory a adresare
            if (Properties.Settings.Default.ShouldInitialize)
            {
                if (InitializeWithDialogForm())
                {
                    Properties.Settings.Default.ShouldInitialize = false;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    Application.Exit();
                    return false;
                }
            }
            else
            {
                // Skontroluj adresare a JSON subory, ak neexistuju vytvor odznova
                if (!System.IO.Directory.Exists(Properties.Settings.Default.DataDirectory))
                {
                    dataDirectory = false;
                }

                if (!System.IO.Directory.Exists(Properties.Settings.Default.HlavickyDirectory))
                {
                    hlavickyDirectory = false;
                }

                if (!System.IO.Directory.Exists(Properties.Settings.Default.OutputDirectory))
                {
                    outputDirectory = false;
                }
            }
            
            if (!dataDirectory || !hlavickyDirectory || !outputDirectory)
            {

            }

            // Check connection to MSSQL
            // If can't connect let user find MSSQL servers IP
            // If DB not present let user create new empty database for application with custom name
            // Check the database and load some values 
            MSSQLServer server = null;
            if (Properties.Settings.Default.ServerNamePassLogin)
            {
                server = new MSSQLServer(Properties.Settings.Default.ServerAddress,
                    Properties.Settings.Default.DatabaseLogin, 
                    Properties.Settings.Default.DatabasePassword,
                    Properties.Settings.Default.DatabaseName, true);
            }
            else
            {
                server = new MSSQLServer(Properties.Settings.Default.ServerAddress, 
                    Properties.Settings.Default.DatabaseName);
            }
            
            var dataManager = new ISEFDataManager(server, 
                Properties.Settings.Default.DataDirectory, 
                Properties.Settings.Default.HlavickyDirectory, 
                Properties.Settings.Default.OutputDirectory,
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Properties.Resources.logo94);

            bool canConnect = false, databaseExists = false, databaseValid = false;
            Exception exception = null;

            await Task.Delay(350);
            BackgroundImage = Resources.isefinit3;
            try
            {
                if (canConnect = await server.CanConnectAsync())
                    if (databaseExists = await dataManager.Server.DoesDatabaseExistsAsync())
                    {
                        databaseValid = true;
                        await dataManager.InitializeManager();
                    }

                dataManager.CoreFiles.CreateBackup();
                await Task.Delay(350);
                BackgroundImage = Resources.isefinit2;
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            finally
            {
                await Task.Delay(350);
                BackgroundImage = Resources.isefinit1;
                await Task.Delay(550);
                BackgroundImage = Resources.isefinit;
                timer.Stop();
                if (timer.ElapsedMilliseconds < MinimalWaitTime)
                    await Task.Delay(MinimalWaitTime - (int)timer.ElapsedMilliseconds);

                if (canConnect && databaseExists && databaseValid)
                {
                    SuccessfullyInitialized?.Invoke(this, dataManager);
                }
                else
                {
                    InitializationFailed?.Invoke(this, new InitializationReport(canConnect, databaseExists, databaseValid, exception));
                }

                _initializationRunning = false;
            }

            return exception is null;
        }

        private bool InitializeWithDialogForm()
        {
            using (var applicationInitialize = new ApplicationSetupForm())
            {
                if (applicationInitialize.ShowDialog() != DialogResult.OK)
                {
                    return false;
                }
                else
                {
                    try
                    {
                        var s = applicationInitialize.Server;

                        // Nastavim login na databazu a server
                        if (s.NamePasswordLogin)
                        {
                            Settings.Default.ServerNamePassLogin = true;
                            Settings.Default.DatabaseLogin = s.GetEncryptedLogin();
                            Settings.Default.DatabasePassword = s.GetEncryptedPass();
                        }
                        else
                        {
                            Settings.Default.ServerNamePassLogin = false;
                        }

                        // Nastavim adresu severu a meno databazy
                        Settings.Default.DatabaseName = s.DatabaseName;
                        Settings.Default.ServerAddress = s.ServerAddress;

                        // Nastavi adresare ako settings pre aplikaciu
                        Settings.Default.DataDirectory = System.IO.Path.Combine(applicationInitialize.DataDirectory, Resources.DataDirectory);
                        Settings.Default.HlavickyDirectory = System.IO.Path.Combine(applicationInitialize.HlavickyDirectory, Resources.HlavickyDirectory);
                        Settings.Default.OutputDirectory = System.IO.Path.Combine(applicationInitialize.OutputDirectory, Resources.OutputDirectory);

                        // Vytvorim adresar vyssie a tiez vytvorim 'defaultne' JSON subory a hlavickove XLSX subory
                        FilesManager.CreateDefault(Settings.Default.DataDirectory, Settings.Default.HlavickyDirectory, Settings.Default.OutputDirectory);

                        return true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Mrzí ma to, ale počas inicializácie nastala neočakávaná chyba. Chybové hlásenie: " + ex.Message,
                            "Inicializácia zlyhala: " + ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return false;
                    }
                }
            }
        }
    }
}
 