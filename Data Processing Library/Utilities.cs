namespace cvti.data
{
    using cvti.data.Output;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Data;

    public static class Utilities
    {
        /// <summary>
        /// Vrati IDcko pouzivatela, zatial iba pri logoch mozno sa pouzije aj inde
        /// </summary>
        /// <returns>IDcko pouzivatela, zatial iba pri logoch mozno sa pouzije aj inde</returns>
        public static string GetEmployeeIdentificator() => $"{Environment.MachineName}|{Environment.UserDomainName}|{Environment.UserName}|";

        public static DataTable ConvertDataToTable(IEnumerable<DatovyRiadok> data)
        {
            var table = new DataTable();

            if (data.Any())
            {
                foreach (var c in data.First().Columns)
                    table.Columns.Add(c.ColumnAlias);

                foreach (var r in data)
                    table.Rows.Add(r.Values);

            }

            return table;
        }
    }
}