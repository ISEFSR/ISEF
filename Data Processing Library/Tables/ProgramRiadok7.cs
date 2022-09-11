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

    public class ProgramRiadok7 : ProgramRiadok
    {
        public static string TableName = "[dbo].[cis_pk7]";
        public static SqlCommand GetSelectCommand(SqlConnection conn, string db, int rok)
        {
            var cmd = new SqlCommand($"select [Rok], [Kod], [pk5], [Nazov], [Popis] from [{db}].{TableName} where [Rok]=@rok", conn);
            cmd.Parameters.AddWithValue("@rok", rok);
            return cmd;
        }

        protected override string GetTableName() => TableName;

        public ProgramRiadok7(IDataRecord data)
            : base(data)
        {
        }

        [TableColumnAttribute]
        public string Pk5 { get; set; }
    }
}
