namespace cvti.data.Files
{
    using cvti.data.Core;
    using System;
    using System.Linq;
    using System.IO;

    /// <summary>
    /// Manager zodpovedny za manipulaciu s JSON subormi a hlavickovymi XLSX subormi
    /// </summary>
    public class FilesManager
    {
        #region Constructor And Variables

        private string _backupDirectory;

        /// <summary>
        /// Inicializuje novy manager zodpovedny za manipulaciu s JSON subormi a hlavickovymi subormi na zaklade potrebnych adresarov
        /// </summary>
        /// <param name="log">LogManager zodpovedny za evidovanie logov ako <see cref="LogManager"/></param>
        /// <param name="dataDirectory">Adresar kde by sa mali nachadzat JSON subory pre aplikaciu ako <see cref="string"/></param>
        /// <param name="hlavickyDirectory">Adresar kde by sa mali nachadzat hlavickove XLSX subory pre aplikaciu ako <see cref="string"/></param>
        /// <param name="outputDirectory">Adresar kam aplikacia uklada vystupne XLSX reporty ako <see cref="string"/></param>
        /// <exception cref="ArgumentNullException">V pripade ak je aspon jeden z adresarov null</exception>
        /// <exception cref="DirectoryNotFoundException">V pripade ak aspon jeden z adresarov neexistuje</exception>
        public FilesManager(LogManager log, string dataDirectory, string hlavickyDirectory, string outputDirectory, string backupDirectory)
        {
            if (dataDirectory is null || hlavickyDirectory is null || outputDirectory is null || backupDirectory is null)
                throw new ArgumentNullException();

            if (!System.IO.Directory.Exists(dataDirectory))
                throw new DirectoryNotFoundException("Data directory does not exists: " + dataDirectory);

            if (!System.IO.Directory.Exists(hlavickyDirectory))
                throw new DirectoryNotFoundException("Hlavicky directory does not exists: " + hlavickyDirectory);

            if (!System.IO.Directory.Exists(outputDirectory))
                throw new DirectoryNotFoundException("Output directory does not exists: " + outputDirectory);

            if (!System.IO.Directory.Exists(outputDirectory))
                throw new DirectoryNotFoundException("Backup directory does not exists: " + backupDirectory);

            _backupDirectory = backupDirectory;

            Commands = new CommandsManagerJson(log, dataDirectory);
            Conditions = new ConditionsManagerJson(log, dataDirectory);
            Hlavicky = new HlavickyManagerJson(log, dataDirectory, hlavickyDirectory);
            Zostavy = new ZostavyManagerJson(log, dataDirectory, hlavickyDirectory);

            DataDirectory = dataDirectory;
            OutputDirectory = outputDirectory;
            HlavickyDirectory = hlavickyDirectory;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Vrati adresar kde sa nachdzaju JSON subory 
        /// </summary>
        /// <value>
        /// Adresar kde sa nachadzaju JSON subory ako <see cref="string"/>
        /// </value>
        public string DataDirectory { get; private set; }
        /// <summary>
        /// Vrati adresar kde sa nachadzaju hlavickove subory
        /// </summary>
        /// <value>
        /// Adresar kde sa nachadzaju hlavickove subory ako <see cref="string"/>
        /// </value>
        public string HlavickyDirectory { get; private set; }
        /// <summary>
        /// Vrati adreasr kde sa nachadzaju / ukladaju vystupne reporty
        /// </summary>
        /// <value>
        /// Adresar pre vystupne subory ako <see cref="string"/>
        /// </value>
        public string OutputDirectory { get; private set; }

        /// <summary>
        /// Vrati manager zodpovedny za select prikazy
        /// </summary>
        /// <value>
        /// manager zodpovedny za select prikazy ako <see cref="CommandsManagerJson"/>
        /// </value>
        public CommandsManagerJson Commands { get; private set; }

        /// <summary>
        /// Vrati manager zodpovedny za podmienky aplikacie 
        /// </summary>
        /// <value>
        /// Manager zodpovedny za podmienky aplikacie ako <see cref="ConditionsManagerJson"/>
        /// </value>
        public ConditionsManagerJson Conditions { get; private set; }

        /// <summary>
        /// Vrati manager zodpovedny za hlavickove subory aplikacie
        /// </summary>
        /// <value>
        /// Mnaager zodpovedny za hlavickove subory aplikacie ako <see cref="HlavickyManagerJson"/>
        /// </value>
        public HlavickyManagerJson Hlavicky { get; private set; }

        /// <summary>
        /// Vrati manager zodpovedny za zostavy aplikacie 
        /// </summary>
        /// <value>
        /// Manager zodpovedny za zostavy aplikacie ako <see cref="ZostavyManagerJson"/>
        /// </value>
        public ZostavyManagerJson Zostavy { get; private set; }

        #endregion

        /// <summary>
        /// Nacita vsetky data odznova 
        /// </summary>
        public void Initialize()
        {
            Commands.ReadData();
            Conditions.ReadData();
            Hlavicky.ReadData();
            Zostavy.ReadData();
            Zostavy.UpdateHlavicky(Hlavicky);
        }

        /// <summary>
        /// Ulozi vsetky data
        /// </summary>
        public void Save()
        {
            Commands.SaveData();
            Conditions.SaveData();
            Hlavicky.SaveData();
            Zostavy.SaveData();
        }

        public void CreateBackup()
        {
            if (Commands.Values.Any())
                Commands.ExportData(System.IO.Path.Combine(_backupDirectory, "_commands.json"));

            if (Conditions.Values.Any())
                Conditions.ExportData(System.IO.Path.Combine(_backupDirectory, "_conditions.json"));

            if (Hlavicky.Values.Any())
                Hlavicky.ExportData(System.IO.Path.Combine(_backupDirectory, "_hlavicky.json"));

            if (Zostavy.Values.Any())
                Zostavy.ExportData(System.IO.Path.Combine(_backupDirectory, "_zostavy.json"));
        }

        #region Public Static Methods

        /// <summary>
        /// Vygeneruje nanovo defaultne subory a hlavicky
        /// </summary>
        /// <param name="dataDirectory">Adresar pre JSON subory</param>
        /// <param name="hlavickyDirectory">Adresar pre hlavickove XLSX subory</param>
        /// <param name="outputDirectory">Adresar pre vystupne zostavove reporty</param>
        public static void CreateDefault(string dataDirectory, string hlavickyDirectory, string outputDirectory)
        {
            // TODO: presunut pod nejaky manager
            // Vytvorenie adresara pre vystupne subory
            if (!System.IO.Directory.Exists(outputDirectory))
            {
                System.IO.Directory.CreateDirectory(outputDirectory);
            }

            // Adresar pre hlavickove subory
            if (!System.IO.Directory.Exists(hlavickyDirectory))
            {
                System.IO.Directory.CreateDirectory(hlavickyDirectory);
            }

            // Vytvorenie adresara pre hlavickove subory
            if (!System.IO.Directory.Exists(dataDirectory))
            {
                System.IO.Directory.CreateDirectory(dataDirectory);
            }

            // Vygenerujem JSON s defaultnymi prikazmi
            CommandsManagerJson.GenerateDefaultFile(dataDirectory);

            // Vygenrujem JSON s defaultnymi podmienkami
            ConditionsManagerJson.GenerateDefaulFile(dataDirectory);

            // Vygenerujem hlavickove subory do adresaru
            var hlavicky = HlavickyManagerJson.GenerateDefaultHlavicky(hlavickyDirectory);

            // Vygenerujem JSON na zaklade hlavickovych suborov
            HlavickyManagerJson.GenerateDefaultFile(hlavicky, dataDirectory);

            // Vygenerujem JSON pre zostavy na zaklade hlaviciek
            ZostavyManagerJson.GenerateDefaultFile(hlavicky, dataDirectory);

            #endregion

        }
    }
}
