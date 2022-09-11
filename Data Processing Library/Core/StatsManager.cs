namespace cvti.data.Core
{
    using cvti.data.Views;
    using cvti.data.Enums;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    /// <summary>
    /// Manager zodpovedny za manipulaciu s tabulku kde su ulozene prehlady
    /// </summary>
    internal class StatsManager
        : DataManager
    {
        private readonly LogManager _log;

        public StatsManager(MSSQLServer server, LogManager log)
            : base(server) { _log = log ?? throw new System.ArgumentNullException(nameof(log)); }

        public async Task UpdateStats(int rok)
        {
            try
            {
                using (var conn = await Server.GetConnectionAsync(true))
                using (var cmd = new SqlCommand("exec dbo.[usp_UpdateStats] @rok", conn))
                {
                    cmd.Parameters.AddWithValue("@rok", rok);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                await _log.LogNewMessageSafeAsync(LogMessageType.BackEndError,
                    "StatsManager.UpdateStats", ex.Message + "|" + ex.StackTrace);
                throw;
            }
        }
        internal async Task UpdateStats(SqlTransaction tran, int rok)
        {
            try
            {
                using (var cmd = new SqlCommand("exec dbo.[usp_UpdateStats] @rok", tran.Connection, tran))
                {
                    cmd.Parameters.AddWithValue("@rok", rok);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                await _log.LogNewMessageSafeAsync(LogMessageType.BackEndError,
                    "StatsManager.UpdateStats", ex.Message + "|" + ex.StackTrace);
                throw;
            }
        }

        public async Task<StatsView> GetStats(InputType stupen, int rok)
        {
            try
            {
                using (var conn = await Server.GetConnectionAsync(true))
                using (var cmd = new SqlCommand("select * from dbo.[ufn_GetStupenSummary] (@rok, @stupen)", conn))
                {
                    cmd.Parameters.AddWithValue("@rok", rok);
                    cmd.Parameters.AddWithValue("@stupen", (char)stupen);
                    using (var reader = await cmd.ExecuteReaderAsync(System.Data.CommandBehavior.CloseConnection | System.Data.CommandBehavior.SingleRow))
                        if (await reader.ReadAsync())
                            return new StatsView(reader);

                    return null;
                }
            }
            catch (Exception ex)
            {
                await _log.LogNewMessageSafeAsync(LogMessageType.BackEndError,
                    "StatsManager.GetStats", ex.Message + "|" + ex.StackTrace);
                throw;
            }
        }
        public async Task<IEnumerable<StatsView>> GetStats()
        {
            try
            {
                using (var conn = await Server.GetConnectionAsync(true))
                using (var cmd = new SqlCommand(StatsView.GetSelectCommand(Server.DatabaseName), conn))
                {
                    var stats = new List<StatsView>();
                    using (var reader = await cmd.ExecuteReaderAsync())
                        while (await reader.ReadAsync())
                            stats.Add(new StatsView(reader));

                    return stats;
                }
            }
            catch (Exception ex)
            {
                await _log.LogNewMessageSafeAsync(LogMessageType.BackEndError,
                    "StatsManager.GetStats", ex.Message + "|" + ex.StackTrace);
                throw;
            }
        }
    }
}