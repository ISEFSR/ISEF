namespace cvti.data.Classifiers
{
    using cvti.data.Core;
    using cvti.data.Enums;
    using cvti.data.Tables;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Manager zodpovedny za manipulaciu s ciselnikomn okresov
    /// </summary>
    /// <remarks>
    /// Umoznuje CRUD operacie nad ciselnikom okresov
    /// </remarks>
    public class OkresyManager : DataManager
    {
        #region Public Events

        public event EventHandler<OkresRiadok> OkresPridany;
        public event EventHandler<OkresRiadok> OkresOdstraneny;
        public event EventHandler<OkresRiadok> OkresUpdatnuty;

        #endregion

        #region Variables And Constructors

        private readonly LogManager _log;

        public OkresyManager(MSSQLServer server, LogManager log)
            : base(server)
        {
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        #endregion

        #region Public Methods

        public async Task<IEnumerable<OkresRiadok>> VratOkresy(KrajRiadok kraj = null)
        {
            try
            {
                var okresy = new List<OkresRiadok>();
                using (var conn = await Server.GetConnectionAsync(true))
                using (var cmd = OkresRiadok.GetSelectCommand(conn, Server.DatabaseName))
                {
                    if (kraj != null)
                    {
                        cmd.CommandText += " WHERE KodKraj=@kraj";
                        cmd.Parameters.AddWithValue("@kraj", kraj.Kod);
                    }
                    using (var reader = await cmd.ExecuteReaderAsync())
                        while (await reader.ReadAsync())
                        {
                            okresy.Add(new OkresRiadok(reader));
                        }
                }
                return okresy;
            }
            catch (Exception ex)
            {
                await _log.LogNewMessageSafeAsync(LogMessageType.BackEndError,
                    "OkresyManager.VratOkresy", ex.Message + "|" + ex.StackTrace);
                throw;
            }
        }

        public async Task<bool> UpdateRiadok(OkresRiadok riadok)
        {
            if (await Update(riadok, _log, "OkresyManager.UpdateRiadok"))
            {
                OkresUpdatnuty?.Invoke(this, riadok);
                return true;
            }

            return false;
        }
        public async Task<OkresRiadok> PridajRiadok(OkresRiadok riadok)
        {
            if (await Insert(riadok, _log, "OkresyManager.PridajRiadok"))
            {
                OkresPridany?.Invoke(this, riadok);
                return riadok;
            }

            return null;
        }
        public async Task<bool> OdstranRiadok(OkresRiadok riadok)
        {
            if (await Delete(riadok, _log, "OkresyManager.OdstranRiadok"))
            {
                OkresOdstraneny?.Invoke(this, riadok);
                return true;
            }

            return false;
        }

        #endregion
    }
}