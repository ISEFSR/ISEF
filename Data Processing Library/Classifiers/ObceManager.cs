namespace cvti.data.Classifiers
{
    using cvti.data.Core;
    using cvti.data.Enums;
    using cvti.data.Tables;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Manager zodpovedny za manipulaciu s ciselnikom obci
    /// </summary>
    /// <remarks>
    /// Umoznuje CRUD operacie nad ciselnikom obci
    /// </remarks>
    public class ObceManager : DataManager
    {
        #region Public Events

        /// <summary>
        /// Event signalizujuici novy riadok ciselnika obci ako <see cref="ObecRiadok"/>
        /// </summary>
        public event EventHandler<ObecRiadok> ObecPridana;
        /// <summary>
        /// Event signalziujuci odstranenie riadku ciselnika obci ako <see cref="ObecRiadok"/>
        /// </summary>
        public event EventHandler<ObecRiadok> ObecOdstranena;
        /// <summary>
        /// Event signalizujuci editaciu riadku ciselnika obci ako <see cref="ObecRiadok"/>
        /// </summary>
        public event EventHandler<ObecRiadok> ObecUpdatnuta;

        #endregion

        #region Variables And Constructors

        private readonly LogManager _log;

        public ObceManager(MSSQLServer server, LogManager log)
            : base(server)
        {
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        #endregion

        #region Puiblic Methods

        public async Task<IEnumerable<ObecRiadok>> VratObce(OkresRiadok okres = null)
        {
            try
            {
                var obce = new List<ObecRiadok>();
                using (var conn = await Server.GetConnectionAsync(true))
                using (var cmd = ObecRiadok.GetSelectCommand(conn, Server.DatabaseName))
                {
                    if (okres != null)
                    {
                        cmd.CommandText += " WHERE KodOkres=@okres";
                        cmd.Parameters.AddWithValue("@okres", okres.KodOkres);
                    }
                    using (var reader = await cmd.ExecuteReaderAsync())
                        while (await reader.ReadAsync())
                        {
                            obce.Add(new ObecRiadok(reader));
                        }
                    return obce;
                }
            }
            catch (Exception ex)
            {
                await _log.LogNewMessageSafeAsync(LogMessageType.BackEndError,
                    "ObceManager.VratObce", ex.Message + "|" + ex.StackTrace);
                throw;
            }
        }

        public async Task<bool> UpdateRiadok(ObecRiadok riadok)
        {
            if (await Update(riadok, _log, "ObceManager.UpdateRiadok"))
            {
                ObecUpdatnuta?.Invoke(this, riadok);
                return true;
            }

            return false;
        }
        public async Task<ObecRiadok> PridajRiadok(ObecRiadok riadok)
        {
            if (await Insert(riadok, _log, "ObceManager.PridajRiadok"))
            {
                ObecPridana?.Invoke(this, riadok);
                return riadok;
            }

            return null;
        }
        public async Task<bool> OdstranRiadok(ObecRiadok riadok)
        {
            if (await Delete(riadok, _log, "ObceManager.OdstranRiadok"))
            {
                ObecOdstranena?.Invoke(this, riadok);
                return true;
            }

            return false;
        }

        #endregion
    }
}
