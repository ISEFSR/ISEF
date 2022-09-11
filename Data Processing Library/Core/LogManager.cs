namespace cvti.data.Core
{
    using System.Data.SqlClient;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using cvti.data.Enums;
    using cvti.data.Tables;

    public class LogManager
        : DataManager
    {
        public LogManager(MSSQLServer server)
            : base(server)
        { 

        }

        #region Public Methods
        /// <summary>
        /// Vrati vybrany pocet log zaznamov
        /// </summary>
        /// <param name="last">Pocet zaznamov</param>
        /// <returns>Zaznami ako <see cref="IEnumerable{T}"/> kde T je <see cref="LogMessage"/></returns>
        /// <exception cref="ArgumentException">Hodntoa musi byt vacsia ako 10 a mensia ako 1000</exception>
        public async Task<IEnumerable<LogMessage>> GetLogMessages(int last)
        {
            const int Min = 10;
            const int Max = 1000;

            if (last < 10 || last > 1000)
                throw new ArgumentException($"Value must be between {Min} and {Max}. Value: {last}.");

            using (var conn = await Server.GetConnectionAsync((true)))
            using (var cmd = new SqlCommand($"select top {last} * from [{Server.DatabaseName}].{LogMessage.TableName} order by [Id] desc", conn))
            {
                var logMessages = new List<LogMessage>();

                using (var reader = await cmd.ExecuteReaderAsync())
                    while (await reader.ReadAsync())
                        logMessages.Add(new LogMessage(reader));

                return logMessages;
            }
        }

        /// <summary>
        /// Vrati zaznami od vybraneho datumu
        /// </summary>
        /// <param name="fromDate">Vybrany datum ako <see cref="DateTime"/></param>
        /// <returns>Zaznami ako <see cref="IEnumerable{T}"/> kde T je <see cref="LogMessage"/></returns>
        public async Task<IEnumerable<LogMessage>> GetLogMessages(DateTime fromDate)
        {
            using (var conn = await Server.GetConnectionAsync((true)))
            using (var cmd = new SqlCommand($"select * from [{Server.DatabaseName}].{LogMessage.TableName} where [CreatedDate]>=@from", conn))
            {
                var logMessages = new List<LogMessage>();

                cmd.Parameters.AddWithValue("@from", fromDate);
                using (var reader = await cmd.ExecuteReaderAsync())
                    while (await reader.ReadAsync())
                        logMessages.Add(new LogMessage(reader));

                return logMessages;
            }
        }

        public void LogNewMessageSafe(LogMessageType type, string title, string info)
        {
            try
            {
                LogNewMessage(type, title, info);
            }
            catch
            {

            }
        }

        public async Task LogNewMessageSafeAsync(SqlTransaction tran, LogMessageType type, string title, string info)
        {
            try
            {
                await LogNewMessageAsync(tran, type, title, info);
            }
            catch
            {

            }
        }

        public async Task LogNewMessageSafeAsync(LogMessageType type, string title, string info)
        {
            try
            {
                await LogNewMessageAsync(type, title, info);
            }
            catch
            {

            }
        }
        #endregion

        #region Private Methods
        private void LogNewMessage(LogMessageType type, string title, string info)
        {
            using (var conn = Server.GetConnection((true)))
            using (var cmd = new SqlCommand($"insert into [{Server.DatabaseName}].{LogMessage.TableName} ([CreatedBy], [LogTitle], [LogInfo], [LogType]) " +
                $"values (@creator, @title, @info, @type)", conn))
            {
                cmd.Parameters.AddWithValue("@creator", Utilities.GetEmployeeIdentificator());
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@info", info);
                cmd.Parameters.AddWithValue("@type", (byte)type);

                cmd.ExecuteNonQuery();
            }
        }
        private async Task LogNewMessageAsync(SqlTransaction tran, LogMessageType type, string title, string info)
        {
            using (var cmd = new SqlCommand($"insert into [{Server.DatabaseName}].{LogMessage.TableName} ([CreatedBy], [LogTitle], [LogInfo], [LogType]) " +
                $"values (@creator, @title, @info, @type)", tran.Connection, tran))
            {
                cmd.Parameters.AddWithValue("@creator", Utilities.GetEmployeeIdentificator());
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@info", info);
                cmd.Parameters.AddWithValue("@type", (byte)type);

                await cmd.ExecuteNonQueryAsync();
            }
        }
        private async Task LogNewMessageAsync(LogMessageType type, string title, string info)
        { 
            using (var conn = await Server.GetConnectionAsync((true)))
            using (var cmd = new SqlCommand($"insert into [{Server.DatabaseName}].{LogMessage.TableName} ([CreatedBy], [LogTitle], [LogInfo], [LogType]) " +
                $"values (@creator, @title, @info, @type)", conn))
            {
                cmd.Parameters.AddWithValue("@creator", Utilities.GetEmployeeIdentificator());
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@info", info);
                cmd.Parameters.AddWithValue("@type", (byte)type);

                await cmd.ExecuteNonQueryAsync();
            }
        }
        #endregion
    }
}