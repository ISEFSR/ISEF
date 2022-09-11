namespace cvti.data.Core
{
    using System;
    using System.Threading.Tasks;

    public abstract class DataManager
    {
        public DataManager(MSSQLServer server)
        {
            Server = server ?? throw new ArgumentNullException(nameof(server));
        }

        public MSSQLServer Server { get; private set; }

        protected async Task<bool> Update(IUpdatable riadok, LogManager _log = null, string from = null)
        {
            try
            {
                using (var conn = await Server.GetConnectionAsync(true))
                using (var cmd = riadok.GenerateUpdateCommand(conn, Server.DatabaseName))
                {
                    return 1 == await cmd.ExecuteNonQueryAsync();
                }
            }
            catch(Exception ex)
            {
                if (_log != null)
                    await _log.LogNewMessageSafeAsync(Enums.LogMessageType.BackEndError,
                        string.IsNullOrWhiteSpace(from) ? "DataManager.Insert" : from, ex.Message + "|" + ex.StackTrace);
                throw;
            }
        }

        protected async Task<bool> Delete(IDeletable riadok, LogManager _log = null, string from = null)
        {
            try 
            {
                using (var conn = await Server.GetConnectionAsync(true))
                using (var cmd = riadok.GenerateDeleteCommand(conn, Server.DatabaseName))
                {
                    return 1 == await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                if (_log != null)
                    await _log.LogNewMessageSafeAsync(Enums.LogMessageType.BackEndError,
                        string.IsNullOrWhiteSpace(from) ? "DataManager.Insert" : from, ex.Message + "|" + ex.StackTrace);
                throw;
            }
        }

        protected async Task<bool> Insert(IInsertable riadok, LogManager _log = null, string from = null)
        {
            try {
                using (var conn = await Server.GetConnectionAsync(true))
                using (var cmd = riadok.GenerateInsertCommand(conn, Server.DatabaseName))
                {
                    return 1 == await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                if (_log != null)
                    await _log.LogNewMessageSafeAsync(Enums.LogMessageType.BackEndError,
                        string.IsNullOrWhiteSpace(from) ? "DataManager.Insert" : from, ex.Message + "|" + ex.StackTrace);
                throw;
            }
        }
    }
}