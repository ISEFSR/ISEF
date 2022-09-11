namespace cvti.data.Output
{
    using cvti.data.Core;
    using cvti.data.Columns;
    using cvti.data.Conditions;
    using cvti.data.Views;
    using cvti.data.Enums;
    using cvti.data.Output;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    public class ExportManager : DataManager
    {
        private readonly LogManager _log;

        public ExportManager(MSSQLServer server, LogManager log)
            : base(server)
        {
            _log = log;
        }

        public async Task<decimal> GetSumForKrajAndYear(SumColumn column, short kraj, int year)
        {
            var cndYear = new Equals(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.Rok), year);
            var cndOkres = new Equals(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.KrajKod), kraj);

            return await GetSum(column, cndYear.AddCondition(cndOkres, ConditionOperator.And));
        }

        private async Task<decimal> GetSum(SumColumn column, Condition cnd)
        {
            var selectCommand = new SelectCommand(string.Empty)
            {
                CommandCondition = cnd
            };
            selectCommand.AddColumn(column.GetColumn());

            using (var conn = await Server.GetConnectionAsync(true))
            using (var cmd = selectCommand.GenerateCommand(conn))
            {
                var value = await cmd.ExecuteScalarAsync();
                if (value == DBNull.Value)
                    return 0;
                
                return Convert.ToDecimal(value);
            }
        }

        public async Task<DataTable> ExportDataToTable(Zostava zostava, int rok, Condition cnd = null)
        {
            using (var conn = await Server.GetConnectionAsync(true))
            using (var cmd = zostava.GetSqlCommand(rok, cnd))
            {
                cmd.Connection = conn;
                using (var adapter = new SqlDataAdapter(cmd))
                {
                    var data = new DataTable();
                    await Task.Run(() => { adapter.Fill(data); });
                    return data;
                }
            }
        }

        public async Task<DataTable> ExportDataToTable(Hlavicka hlavicka, Condition podmienka)
        {
            var selectCommand = hlavicka.GetCommand();
            if (selectCommand.CommandCondition is null)
            {
                selectCommand.CommandCondition = podmienka;
            }
            else
            {
                selectCommand.CommandCondition.AddCondition(podmienka, ConditionOperator.And);
            }

            using (var conn = await Server.GetConnectionAsync(true))
            using (var cmd = selectCommand.GenerateCommand(conn))
            {
                cmd.Connection = conn;
                using (var adapter = new SqlDataAdapter(cmd))
                {
                    var data = new DataTable();
                    await Task.Run(() => { adapter.Fill(data); });
                    return data;
                }
            }
        }

        public async Task<DataTable> ExportDataToTable(Hlavicka hlavicka, int rok)
        {
            var rokCondition = new Equals(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.Rok), rok);

            var selectCommand = hlavicka.GetCommand();
            if (selectCommand.CommandCondition is null)
            {
                selectCommand.CommandCondition = rokCondition;
            }
            else
            {
                selectCommand.CommandCondition.AddCondition(rokCondition, ConditionOperator.And);
            }

            using (var conn = await Server.GetConnectionAsync(true))
            using (var cmd = selectCommand.GenerateCommand(conn))
            {
                cmd.Connection = conn;
                using (var adapter = new SqlDataAdapter(cmd))
                {
                    var data = new DataTable();
                    await Task.Run(() => { adapter.Fill(data); });
                    return data;
                }
            }
        }

        public async Task<IEnumerable<DatovyRiadok>> ExportData(Zostava zostava, int rok, bool withoutInvisibleColumns = false, Condition condition = null)
        {
            var data = new List<DatovyRiadok>();
            using (var conn = await Server.GetConnectionAsync(true))
            using (var cmd = zostava.GetSelectCommand(rok, condition, withoutInvisibleColumns).GenerateCommand())
            {
                cmd.CommandTimeout = 240;
                cmd.Connection = conn;
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                        data.Add(new DatovyRiadok(reader, zostava.Hlavicka.Data.Stlpce));
                }
            }
            return data;
        }

        public async Task<IEnumerable<DatovyRiadok>> ExportData(Hlavicka hlavicka)
        {
            var data = new List<DatovyRiadok>();
            using (var conn = await Server.GetConnectionAsync(true))
            using (var cmd = hlavicka.GetCommand().GenerateCommand(conn))
            {
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                        data.Add(new DatovyRiadok(reader, hlavicka.Data.Stlpce));
                }
            }
            return data;
        }

        public async Task<IEnumerable<DatovyRiadok>> SelectDataAsync(SelectCommand command)
        {
            using (var conn = Server.GetConnection())
            using (var cmd = command.GenerateCommand(conn))
            {
                var rows = new List<DatovyRiadok>();
                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                    while (await reader.ReadAsync())
                        rows.Add(new DatovyRiadok(reader, command.Columns));

                return rows;
            }
        }

        public async Task<DataTable> SelectDataAsDataTableAsync(SelectCommand command)
        {
            var data = await SelectDataAsync(command);
            var dataTable = new DataTable();

            foreach (var c in command.Columns)
            {
                dataTable.Columns.Add(new DataColumn(c.ColumnAlias));
            }

            foreach (var r in data)
            {
                dataTable.Rows.Add(r.Values);
            }

            return dataTable;
        }

        public IEnumerable<DatovyRiadok> SelectData(SelectCommand command, int rok)
        {
            using (var conn = Server.GetConnection())
            using (var cmd = command.GenerateCommand(rok, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        yield return new DatovyRiadok(reader, command.Columns);
            }
        }
    }
}