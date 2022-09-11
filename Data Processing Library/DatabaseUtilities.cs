namespace cvti.data
{
    using cvti.data.Core;
    using cvti.data.Tables;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public static class DatabaseUtilities
    {
        public static string GetStringSafe(this IDataRecord record, int ordinal)
        {
            return record.IsDBNull(ordinal) ? null : record.GetString(ordinal);
        }

        public static int? GetInt32Safe(this IDataRecord record, int ordinal)
        {
            if (!record.IsDBNull(ordinal))
                record.GetInt32(ordinal);

            return null;
        }

        public static SqlCommand GenerateSelectCommand<T>(SqlConnection conn, string db, string table) where T : TableRow
        {
            var columns = new List<string>();
            foreach (var p in typeof(T).GetProperties())
            {
                if (p.GetCustomAttributes(typeof(TableColumnAttribute), true).FirstOrDefault() is TableColumnAttribute att)
                    columns.Add("[" + (string.IsNullOrWhiteSpace(att.ColumnName) ? p.Name : att.ColumnName) + "]");
            }

            return new SqlCommand($"select {string.Join(",", columns)} from {db}.{table}", conn);
        }

        public static SqlCommand GenerateSelectCommand<T>(SqlConnection conn, string db, string table, int rok) where T : AnalytickaEvidenciaRiadok
        {
            var columns = new List<string>();
            foreach (var p in typeof(T).GetProperties())
            {
                if (p.GetCustomAttributes(typeof(TableColumnAttribute), true).FirstOrDefault() is TableColumnAttribute att)
                    columns.Add("[" + (string.IsNullOrWhiteSpace(att.ColumnName) ? p.Name : att.ColumnName) + "]");
            }

            var cmd = new SqlCommand($"select {string.Join(",", columns)} from {db}.{table} where Rok=@rok", conn);
            cmd.Parameters.AddWithValue("@rok", rok);
            return cmd;
        }
    }
}
