namespace cvti.data.Core
{
    using System;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    /// <summary>
    /// Reprezentuje spojenie na MSSQL server
    /// </summary>
    public class MSSQLServer
    {
        #region Variables And Constructors

        private readonly ConfigKey _key;
        private readonly string _login;
        private readonly string _pass;

        /// <summary>
        /// Inicializuje pripojenie na MSSQL server na zaklade configuracneho klucu a nazvu databazy
        /// </summary>
        /// <param name="configKey">Kluc pre config file ako <see cref="ConfigKey"/></param>
        /// <param name="databaseName">Nazov databazy ako <see cref="string"/></param>
        public MSSQLServer(ConfigKey configKey, string databaseName)
        {
            _key = configKey;
            DatabaseName = databaseName;
        }

        /// <summary>
        /// Inicializuje pripojenie na MSSQL server na zaklade adresy serveru, prihlasovacieho mena, hesla a nazvu databnazy
        /// </summary>
        /// <param name="serverAddress">Adresa serveru ako <see cref="string"/></param>
        /// <param name="login">Prihlasovacie meno ako <see cref="string"/></param>
        /// <param name="pass">Heslo ako <see cref="string"/></param>
        /// <param name="database">Nazov databazy ako <see cref="string"/></param>
        /// <param name="encrypted">Zasifrujem meno, heslo ked ich ulozim ako lokalne premene</param>
        public MSSQLServer(string serverAddress, string login, string pass, string database, bool encrypted = false)
        {
            ServerAddress = serverAddress;
            DatabaseName = database;

            if (!encrypted)
            { 
                _login = StringUtilities.Encrypt(login, GetType().ToString());
                _pass = StringUtilities.Encrypt(pass, GetType().ToString());
            }
            else
            {
                _login = login;
                _pass = pass;
            }
        }

        /// <summary>
        /// Inicializuje pripojenie na MSSQL server ako Truste Connection na zaklade domenoveho uctu
        /// </summary>
        /// <param name="serverAddress">Adresa serveru ako <see cref="string"/></param>
        /// <param name="database">Nazov databazy ako <see cref="string"/></param>
        public MSSQLServer(string serverAddress, string database)
        {
            ServerAddress = serverAddress;
            DatabaseName = database;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Vrati meno databazy
        /// </summary>
        /// <value>
        /// Meno databazy ako <see cref="string"/>
        /// </value>
        public string DatabaseName { get; private set; }

        /// <summary>
        /// Vrati adresu servera
        /// </summary>
        /// <value>
        /// Adresa serveru ako <see cref="string"/>
        /// </value>
        public string ServerAddress { get; private set; }
        
        /// <summary>
        /// Vrati hodnotu hovoriacu o tom, ci pripojenie prebehne na zaklade mena a hesla 
        /// </summary>
        /// <value>
        /// True ak prihlasenie prebehne pomocou mena a hesla inak false
        /// </value>
        public bool NamePasswordLogin { get; private set; }

        #endregion

        #region Public Methods

        public bool DoesDatabaseExists()
        {
            // check if database with secified name exists on server
            using (var sqlConnection = GetConnection(true))
            using (var sqlCmd = new SqlCommand("select count(*) from master.dbo.sysdatabases where name=@database", sqlConnection))
            {
                sqlCmd.Parameters.Add("@database", System.Data.SqlDbType.NVarChar).Value = DatabaseName;
                return Convert.ToInt32(sqlCmd.ExecuteScalar()) == 1;
            }
        }

        public async Task<bool> DoesDatabaseExistsAsync()
        {
            // check if database with secified name exists on server
            using (var sqlConnection = await GetConnectionAsync(true))
            using (var sqlCmd = new SqlCommand("select count(*) from master.dbo.sysdatabases where name=@database", sqlConnection))
            {
                sqlCmd.Parameters.Add("@database", System.Data.SqlDbType.NVarChar).Value = DatabaseName;
                return Convert.ToInt32(await sqlCmd.ExecuteScalarAsync()) == 1;
            }
        }

        public string GetConnectionString()
        {
            if (_key != null)
                return ConfigurationManager.ConnectionStrings[_key.Key].ConnectionString;

            if (_login is null || _pass is null)
                return $"Data Source={ServerAddress};Initial Catalog={DatabaseName};Integrated Security=True";

            return $"Data Source={ServerAddress};Initial Catalog={DatabaseName};User Id={StringUtilities.Decrypt(_login, GetType().ToString())};Password={StringUtilities.Decrypt(_pass, GetType().ToString())}";
        }

        public bool CanConnect()
        {
            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CanConnectAsync()
        {
            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public SqlConnection GetConnection(bool opened = false)
        {
            var conn = new SqlConnection(GetConnectionString());
            if (opened) conn.Open();
            return conn;
        }

        public async Task<SqlConnection> GetConnectionAsync(bool opened =false)
        {
            var conn = new SqlConnection(GetConnectionString());
            if (opened)
                await conn.OpenAsync();
            return conn;
        }

        public string GetEncryptedPass()
        {
            return StringUtilities.Encrypt(_pass, GetType().ToString());
        }

        public string GetEncryptedLogin()
        {
            return StringUtilities.Encrypt(_login, GetType().ToString());
        }

        #endregion
    }
}
