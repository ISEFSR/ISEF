using cvti.data.Core;
using System.Data;
using System.Data.SqlClient;

namespace cvti.data.Tables
{
    public class FunkcnaRiadok3 : FunkcnaRiadok
    {
        public static string TableName = "[dbo].[cis_fk3]";
        public static SqlCommand GetSelectCommand(SqlConnection conn, string db, int rok)
        {
            var cmd = new SqlCommand($"select [Rok], [Kod], [fk2], [Nazov], [Popis] from [{db}].{TableName} where [Rok]=@rok", conn);
            cmd.Parameters.AddWithValue("@rok", rok);
            return cmd;
        }

        public FunkcnaRiadok3(IDataRecord data)
            : base(data)
        {
            
        }

        [TableColumnAttribute("Fk2")]
        public string Fk2 { get; set; }

        protected override string GetTableName() => TableName;
    }
}
