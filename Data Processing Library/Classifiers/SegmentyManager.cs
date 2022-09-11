namespace cvti.data.Classifiers
{
    using cvti.data.Core;
    using cvti.data.Enums;
    using cvti.data.Tables;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Manager zodpovedny za manipulaciu s ciselnikom segmentov
    /// </summary>
    public class SegmentyManager :  DataManager
    {
        #region Public Events

        public event EventHandler<SegmentRiadok> SegmentPridany;
        public event EventHandler<SegmentRiadok> SegmentOdstraneny;
        public event EventHandler<SegmentRiadok> SegmentUpdatnuty;

        #endregion

        #region Variables And Constructors

        private readonly LogManager _log;

        public SegmentyManager(MSSQLServer server, LogManager log)
            : base(server)
        {
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        #endregion

        #region Public Methods

        public async Task<IEnumerable<SegmentRiadok>> VratSegmenty()
        {
            try
            {
                var segmenty = new List<SegmentRiadok>();
                using (var conn = await Server.GetConnectionAsync(true))
                using (var cmd = DatabaseUtilities.GenerateSelectCommand<SegmentRiadok>(conn, Server.DatabaseName, SegmentRiadok.TableName))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                        while (await reader.ReadAsync())
                            segmenty.Add(new SegmentRiadok(reader));
                }
                return segmenty;
            }
            catch (Exception ex)
            {
                await _log.LogNewMessageSafeAsync(LogMessageType.BackEndError,
                    "OrganizacieManager.VratSegmenty", ex.Message + "|" + ex.StackTrace);
                throw;
            }
        }

        public async Task<bool> UpdateRiadok(SegmentRiadok riadok)
        {
            if (await Update(riadok, _log, "SegmentyManager.UpdateRiadok"))
            {
                SegmentUpdatnuty?.Invoke(this, riadok);
                return true;
            }

            return false;
        }
        public async Task<SegmentRiadok> PridajRiadok(SegmentRiadok riadok)
        {
            if (await Insert(riadok, _log, "SegmentyManager.PridajRiadok"))
            {
                SegmentPridany?.Invoke(this, riadok);
                return riadok;
            }

            return null;
        }
        public async Task<bool> OdstranRiadok(SegmentRiadok riadok)
        {
            if (await Delete(riadok, _log, "SegmentyManager.OdstranRiadok"))
            {
                SegmentOdstraneny?.Invoke(this, riadok);
                return true;
            }

            return false;
        }

        #endregion
    }
}
