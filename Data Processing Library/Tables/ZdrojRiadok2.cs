namespace cvti.data.Tables
{
    using cvti.data.Core;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ZdrojRiadok2 : ZdrojRiadok
    {
        public static string TableName = "[dbo].[cis_zk2]";
        public static SqlCommand GetSelectCommand(SqlConnection conn, string db, int rok)
        {
            var cmd = new SqlCommand($"select [Rok], [Kod], [Zk1], [Nazov], [Popis] from [{db}].{TableName} where [Rok]=@rok", conn);
            cmd.Parameters.AddWithValue("@rok", rok);
            return cmd;
        }

        protected override string GetTableName() => TableName;

        public ZdrojRiadok2(IDataRecord data)
            : base(data)
        {
        }

        [TableColumnAttribute]
        public string Zk1 { get; set; }
    }
}
