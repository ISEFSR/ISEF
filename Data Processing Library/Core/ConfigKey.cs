namespace cvti.data.Core
{
    /// <summary>
    /// Config key pre DB connection
    /// </summary>
    public class ConfigKey
    {
        /// <summary>
        /// Inicializuje novy config key
        /// </summary>
        /// <param name="configKey">Identifikator pre config key</param>
        public ConfigKey(string configKey)
        {
            Key = configKey;
        }

        /// <summary>
        /// Vrati identifikator pre config key
        /// </summary>
        public string Key { get; private set; }
    }
}
