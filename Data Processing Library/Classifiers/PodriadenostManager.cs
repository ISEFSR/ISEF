namespace cvti.data.Classifiers
{
    using cvti.data.Core;
    using cvti.data.Enums;
    using cvti.data.Tables;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Manager zodpovedny za manipulaciu c ciselnikom podriadenosti
    /// </summary>
    public class PodriadenostManager : DataManager
    {
        #region Public Events

        public event EventHandler<PodriadenostRiadok> PodriadenostPridana;
        public event EventHandler<PodriadenostRiadok> PodriadenostOdstranena;
        public event EventHandler<PodriadenostRiadok> PodriadenostUpdatnuta;

        #endregion

        #region Variables And Constructors

        private readonly LogManager _log;

        public PodriadenostManager(MSSQLServer server, LogManager log)
            : base(server)
        {
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        #endregion

        #region Public Methods

        public async Task<IEnumerable<PodriadenostRiadok>> VratPodriadenost()
        {
            try
            {
                var pod = new List<PodriadenostRiadok>();
                using (var conn = await Server.GetConnectionAsync(true))
                using (var cmd = DatabaseUtilities.GenerateSelectCommand<PodriadenostRiadok>(conn,
                    Server.DatabaseName, PodriadenostRiadok.TableName))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                        while (await reader.ReadAsync())
                            pod.Add(new PodriadenostRiadok(reader));
                }
                return pod;
            }
            catch (Exception ex)
            {
                await _log.LogNewMessageSafeAsync(LogMessageType.BackEndError,
                    "PodriadenostManager.VratPodriadenost", ex.Message + "|" + ex.StackTrace);
                throw;
            }
        }

        public async Task<bool> UpdateRiadok(PodriadenostRiadok riadok)
        {
            if (await Update(riadok, _log, "PodriadenostManager.UpdateRiadok"))
            {
                PodriadenostUpdatnuta?.Invoke(this, riadok);
                return true;
            }

            return false;
        }
        public async Task<PodriadenostRiadok> PridajRiadok(PodriadenostRiadok riadok)
        {
            if (await Insert(riadok, _log, "PodriadenostManager.PridajRiadok"))
            {
                PodriadenostPridana?.Invoke(this, riadok);
                return riadok;
            }

            return null;
        }
        public async Task<bool> OdstranRIadok(PodriadenostRiadok riadok)
        {
            if (await Delete(riadok, _log, "PodriadenostManager.OdstranRIadok"))
            {
                PodriadenostOdstranena?.Invoke(this, riadok);
                return true;
            }

            return false;
        }

        #endregion
    }
}
