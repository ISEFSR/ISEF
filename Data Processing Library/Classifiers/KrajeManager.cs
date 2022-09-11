namespace cvti.data.Classifiers
{
    using cvti.data.Core;
    using cvti.data.Enums;
    using cvti.data.Tables;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Manager zodpovedny za manipulaciu s ciselnikom krajov
    /// </summary>
    /// <remarks>
    /// Umoznuje CRUD operacie nad ciselnikom krajov
    /// </remarks>
    public class KrajeManager : DataManager
    {
        #region Public Events

        /// <summary>
        /// Event signalizujuci updatnuty kraj ako <see cref="KrajRiadok"/>
        /// </summary>
        public event EventHandler<KrajRiadok> KrajUpdated;
        /// <summary>
        /// Event signalizujuci novy kraj ako <see cref="KrajRiadok"/>
        /// </summary>
        public event EventHandler<KrajRiadok> KrajAdded;
        /// <summary>
        /// Event signalizujuci ostraneny kraj ako <see cref="KrajRiadok"/>
        /// </summary>
        public event EventHandler<KrajRiadok> KrajRemoved;

        #endregion

        #region Variables And Constructors

        private readonly LogManager _log;

        /// <summary>
        /// Inicializuje manager pre cislenik krajov na zaklade serveru a log manager
        /// </summary>
        /// <param name="server">Server ako <see cref="MSSQLServer"/></param>
        /// <param name="log">Log manager ako <see cref="LogManager"/></param>
        public KrajeManager(MSSQLServer server, LogManager log)
            : base(server)
        {
            _log = log;
        }

        #endregion

        #region Public Methods

        public async Task<IEnumerable<KrajRiadok>> VratKraje()
        {
            try
            {
                var kraje = new List<KrajRiadok>();
                using (var conn = await Server.GetConnectionAsync(true))
                using (var cmd = DatabaseUtilities.GenerateSelectCommand<KrajRiadok>(conn,
                    Server.DatabaseName, KrajRiadok.TableName))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                        while (await reader.ReadAsync())
                            kraje.Add(new KrajRiadok(reader));
                }
                return kraje;
            }
            catch (Exception ex)
            {
                await _log.LogNewMessageSafeAsync(LogMessageType.BackEndError,
                    "KrajeManager.VratKraje", ex.Message + "|" + ex.StackTrace);
                throw;
            }
        }

        public async Task<bool> UpdateKraj(KrajRiadok kraj)
        {
            if (await Update(kraj, _log, "KrajeManager.UpdateKraj"))
            {
                KrajUpdated?.Invoke(this, kraj);
                return true;
            }

            return false;
        }

        public async Task<KrajRiadok> PridajKraj(KrajRiadok kraj)
        {
            if (await Insert(kraj, _log, "KrajeManager.PridajKraj"))
            {
                KrajAdded?.Invoke(this, kraj);
                return kraj;
            }

            return null;
        }

        public async Task<bool> RemoveKraj(KrajRiadok kraj)
        {
            if (await Delete(kraj, _log, "KrajeManager.RemoveKraj"))
            {
                KrajRemoved?.Invoke(this, kraj);
                return true;
            }

            return false;
        }

        #endregion
    }
}
