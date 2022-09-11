namespace cvti.data.Files
{
    using cvti.data.Core;
    using cvti.data.Enums;

    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Base class pre manipulaciu s JSON subormi
    /// </summary>
    /// <remarks>
    /// Predpoklad je ze polozky v JSON subore implementuju <see cref="ISerializableJson"/>
    /// </remarks>
    /// <typeparam name="T">T je typu <see cref="ISerializableJson"/> kazdy jeden udaj v JSON subore by mal implementovat interface</typeparam>
    public abstract class FileManagerJson<T> where T : ISerializableJson
    {
        public static IEnumerable<T> ReadJsonFileData(string filePath) =>
            JsonConvert.DeserializeObject<T[]>(
                System.IO.File.ReadAllText(filePath), new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });

        #region Public Events

        /// <summary>
        /// Event signalizujuci uspesne pridanie novej hodnoty ako <see cref="T"/>
        /// </summary>
        public event EventHandler<T> NewValueAdded;

        /// <summary>
        /// Event signalizujuci odstranenie hodnoty ako <see cref="T"/>
        /// </summary>
        public event EventHandler<T> ValueRemoved;

        #endregion
        
        #region Variables And Constructors

        protected readonly LogManager _log;

        protected readonly string _fileName;

        protected readonly string _directory;

        protected readonly Dictionary<string, T> _values
            = new Dictionary<string, T>();

        /// <summary>
        /// Inicializuje novy manager na zaklade adresaru a nazvu suboru
        /// </summary>
        /// <param name="log"></param>
        /// <param name="directory"></param>
        /// <param name="fileName"></param>
        internal FileManagerJson(LogManager log, string directory, string fileName)
        {
            _log = log;
            _fileName = fileName;
            _directory = directory;
        }

        #endregion

        /// <summary>
        /// Vrati celu cestu k JSON suboru
        /// </summary>
        /// <value>
        /// Cesta k JSON suboru ako <see cref="string"/>
        /// </value>
        public string FullFilePath { get => System.IO.Path.Combine(_directory, _fileName); }

        public IEnumerable<T> Values
        {
            get => from v in _values select v.Value;
        }

        #region Public Methods

        /// <summary>
        /// Nacita hodnoty pre aktualne nastaveny subor
        /// </summary>
        /// <param name="generateDefaultIfFailed">Ak subor neobsahuje ziadne hodnoty, alebo pocas nacitania nastane chyba nacita defualtne</param>
        public virtual void ReadData(bool generateDefaultIfFailed = true)
        {
            _values.Clear();

            if (!System.IO.File.Exists(FullFilePath))
            {
                if (generateDefaultIfFailed)
                    PopulateWithDefault();

                return;
            }

            try
            {
                var data = ReadJsonFileData(FullFilePath);
                foreach (var d in data)
                    _values.Add(d.Code, d);
            }
            catch (Exception ex)
            {
                _log?.LogNewMessageSafe(LogMessageType.BackEndError, "FileManagerJson.ReadData", ex.Message + ex.StackTrace);
                if (generateDefaultIfFailed)
                    PopulateWithDefault();
            }
        }

        

        /// <summary>
        /// Ulozi ( prepise ) aktualny subor s hodnotami, ktore su nahrane
        /// </summary>
        public virtual void SaveData() => ExportData(FullFilePath);

        /// <summary>
        /// Exportuje honodty do vybraneho JSON suboru
        /// </summary>
        /// <param name="filePath">Cesta k JSON suboru ako <see cref="string"/></param>
        public virtual void ExportData(string filePath)
        {
            try
            {
                var data = (from i in _values select i.Value).ToArray();
                System.IO.File.WriteAllText(filePath, JsonConvert.SerializeObject(data, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                }));
            }
            catch (Exception ex)
            {
                _log?.LogNewMessageSafe(LogMessageType.BackEndError, "FileManagerJson.ExportData", ex.Message + ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// Odstrani hodnotu zo zoznamu na zakalde unikatneho kodu
        /// </summary>
        /// <param name="code">Unikatny kod hodnoty</param>
        /// <returns>True ak sa podari odstranit hodnotu, false ak sa hodnota s kodom nenachadza medzi hodnotami</returns>
        /// <remarks>
        /// Invokne event <see cref="ValueRemoved"/> pri uspesnom odstraneni
        /// </remarks>
        public virtual bool RemoveValue(string code)
        {
            if (!_values.ContainsKey(code))
                return false;

            var value = _values[code];
            _values.Remove(code);
            ValueRemoved?.Invoke(this, value);
            return true;
        }

        /// <summary>
        /// Prida hodnotu 
        /// </summary>
        /// <param name="value">Honodta ako <see cref="T"/></param>
        /// <returns>False ak sa nepodarilo pridat hodnotu kedze hodnota s kodom sa uz nachadza v zozname, inak true</returns>
        /// <remarks>
        /// Invokne event <see cref="NewValueAdded"/> pri uspesnom pridani hodnoty
        /// </remarks>
        public virtual bool AddValue(T value)
        {
            if (_values.ContainsKey(value.Code))
                return false;

            _values.Add(value.Code, value);
            NewValueAdded?.Invoke(this, value);
            return true;
        }

        /// <summary>
        /// Ziska hodnotu na zaklade jej unikatneho kodu
        /// </summary>
        /// <param name="code">Kod pre hodnotu</param>
        /// <returns>Hodnota ako <see cref="T"/></returns>
        public virtual T GetValue(string code)
        {
            if (_values.ContainsKey(code))
                return _values[code];

            return default;
        }

        #endregion

        #region Protected And Private Methods

        protected abstract IEnumerable<T> GetDefault();

        private void PopulateWithDefault()
        {
            foreach (var c in GetDefault())
                _values.Add(c.Code, c);
        }

        #endregion
    }
}
