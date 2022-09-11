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

    public class ProgramRiadok5 : ProgramRiadok
    {
        public static string TableName = "[dbo].[cis_pk5]";
        public static SqlCommand GetSelectCommand(SqlConnection conn, string db, int rok)
        {
            var cmd = new SqlCommand($"select [Rok], [Kod], [pk3], [Nazov], [Popis] from [{db}].{TableName} where [Rok]=@rok", conn);
            cmd.Parameters.AddWithValue("@rok", rok);
            return cmd;
        }

        public ProgramRiadok5(IDataRecord data)
            : base(data)
        {
        }

        [TableColumnAttribute]
        public string Pk3 { get; set; }

        protected override string GetTableName() => TableName;
    }
}
