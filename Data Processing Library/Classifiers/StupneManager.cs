namespace cvti.data.Classifiers
{
    using cvti.data.Core;
    using cvti.data.Enums;
    using cvti.data.Tables;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Manager zodpovedny za manipulaciu s ciselnikom stupnov
    /// </summary>
    /// <remarks>
    /// Umoznuje pridavat, odoberat a editovat  polozky cislenika stupnov
    /// </remarks>
    public class StupneManager : DataManager
    {
        #region Public Events

        /// <summary>
        /// Event signalizujuci vytvorenie novej polozky ciselnika stupnov ako <see cref="StupenRiadok"/>
        /// </summary>
        public event EventHandler<StupenRiadok> StupenVytvoreny;
        /// <summary>
        /// Event signalizujuci odstranenie polozky ciselnika stupnov ako <see cref="StupenRiadok"/>
        /// </summary>
        public event EventHandler<StupenRiadok> StupenOdstraneny;
        /// <summary>
        /// Event signalizujuci update polozky ciselnika stupnov ako <see cref="StupenRiadok"/>
        /// </summary>
        public event EventHandler<StupenRiadok> StupenUpdatnuty;

        #endregion

        #region Variables And Constructors

        private readonly LogManager _log;

        public StupneManager(MSSQLServer server, LogManager log)
            : base(server)
        {
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        #endregion

        #region Public Methods

        public async Task<IEnumerable<StupenRiadok>> VratStupne()
        {
            try
            {
                var stupne = new List<StupenRiadok>();
                using (var conn = await Server.GetConnectionAsync(true))
                using (var cmd = DatabaseUtilities.GenerateSelectCommand<StupenRiadok>(conn, Server.DatabaseName, StupenRiadok.TableName))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                        while (await reader.ReadAsync())
                            stupne.Add(new StupenRiadok(reader));
                }
                return stupne;
            }
            catch (Exception ex)
            {
                await _log.LogNewMessageSafeAsync(LogMessageType.BackEndError,
                    "StupneManager.VratStupne", ex.Message + "|" + ex.StackTrace);
                throw;
            }
        }

        public async Task<bool> UpdateRiadok(StupenRiadok riadok)
        {
            if (await Update(riadok, _log, "StupneManager.UpdateRiadok"))
            {
                StupenUpdatnuty?.Invoke(this, riadok);
                return true;
            }

            return false;
        }
        public async Task<StupenRiadok> PridajRiadok(StupenRiadok riadok)
        {
            if (await Insert(riadok, _log, "StupneManager.PridajRiadok"))
            {
                StupenVytvoreny?.Invoke(this, riadok);
                return riadok;
            }

            return null;
        }
        public async Task<bool> OdstranRiadok(StupenRiadok riadok)
        {
            if (await Delete(riadok, _log, "StupneManager.OdstranRiadok"))
            {
                StupenOdstraneny?.Invoke(this, riadok);
                return true;
            }

            return false;
        }

        #endregion
    }
}