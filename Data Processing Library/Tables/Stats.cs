namespace cvti.data.Tables
{
    using cvti.data.Core;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Stats : TableRow
    {
        public static string TableName = "[dbo].[stats]";

        public static readonly string[] Columns = new string[]
        {
            "Rok",
            "Stupen",
            "TotalCount",
            "TotalSum",
            "DateCreated"
        };

        public Stats(IDataRecord data)
            : base(data)
        {
        }

        [TableColumnAttribute]
        public int Rok { get; set; }
        [TableColumnAttribute]
        public string Stupen { get; set; }
        [TableColumnAttribute]
        public int TotalCount { get; set; }
        [TableColumnAttribute]
        public decimal TotalSum { get; set; }
        [TableColumnAttribute]
        public DateTime DateCreated { get; set; }
    }
}
