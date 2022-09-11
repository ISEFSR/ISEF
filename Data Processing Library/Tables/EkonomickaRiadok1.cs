using System.Data;
using System.Data.SqlClient;

namespace cvti.data.Tables
{
    public  class EkonomickaRiadok1 : EkonomickaRiadok
    {
        public static string TableName = "[dbo].[cis_ek1]";
        public static SqlCommand GetSelectCommand(SqlConnection conn, string db, int rok)
        {
            var cmd = new SqlCommand($"select [Rok], [Kod], [Nazov], [Popis] from [{db}].{TableName} where [Rok]=@rok", conn);
            cmd.Parameters.AddWithValue("@rok", rok);
            return cmd;
        }

        public EkonomickaRiadok1(IDataRecord data)
            : base(data)
        {
        }

        protected override string GetTableName() => TableName;
    }
}