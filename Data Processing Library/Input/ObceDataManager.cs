namespace cvti.data.Input
{
    using cvti.data.Core;
    using cvti.data.Enums;
    using System;
    using System.Collections.Generic;
    using System.Data.OleDb;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// Data manager responsible for reading input DBF files that contains data for Obce
    /// </summary>
    /// <remarks>
    /// TODO: there prolly should be some kind of DBFFileReader etc... 
    /// </remarks>
    public class ObceDataManager
    {
        #region Variables And Constructors
        private readonly LogManager _log;

        /// <summary>
        /// Initializes new instancde of obce data manager 
        /// </summary>
        /// <param name="log">Instance of log manager as <see cref="LogManager"/>. Can be null</param>
        public ObceDataManager(LogManager log = null)
        {
            this._log = log;
        }
        #endregion

        #region Public Members
        /// <summary>
        /// Asynchronously gets all the data from input DBF file containing data for obce
        /// </summary>
        /// <param name="file">Input DBF file as <see cref="FileInfo"/></param>
        /// <returns>All the data from DBF file as <see cref="IList{T}"/> where T is of type <see cref="ObceDataRow"/></returns>
        /// <exception cref="ArgumentNullException">In case that instance of file is null</exception>
        /// <exception cref="FileNotFoundException">In case that system can't find file specified</exception>
        /// <exception cref="ArgumentException">In case that file is not of right type (DBF file)</exception>
        public async Task<IList<ObceDataRow>> ReadFileAsync(FileInfo file)
        {
            if (file is null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            CheckInputFile(file);

            var data = new List<ObceDataRow>();

            try
            {
                using (var conn = new OleDbConnection(GetConnectionString(file)))
                using (var cmd = new OleDbCommand($"select * from {Path.GetFileNameWithoutExtension(file.FullName)}", conn))
                {
                    await conn.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                        while (await reader.ReadAsync())
                            data.Add(new ObceDataRow(reader, System.IO.Path.GetFileNameWithoutExtension(file.FullName)));

                    if (_log != null)
                    {
                        await _log.LogNewMessageSafeAsync(LogMessageType.BackEndMessage, $"ObceDataManager.ReadFile({file})", "Successfully red data for MaO: " + data.Count.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                if (_log != null)
                {
                    _log.LogNewMessageSafe(LogMessageType.BackEndError, $"ObceDataManager.ReadFile({file})", "Error while loading MaO data. " + e.Message + "." + e.StackTrace);
                }
            }

            return data;
        }
        #endregion

        #region Private Methods
        private void CheckInputFile(FileInfo file)
        {
            if (!file.Exists) throw new FileNotFoundException();
            if (!file.Extension.ToLower().Equals(".dbf")) throw new ArgumentException();
            // TODO: check file structure
        }
        private string GetConnectionString(FileInfo file) => $"Provider=vfpoledb;Data Source={file.FullName};Collating Sequence=machine;";
        #endregion
    }
}