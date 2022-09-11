namespace cvti.data.Tables
{
    using System.Data;
    using System.Data.SqlClient;

    public class FunkcnaRiadok2 : FunkcnaRiadok
    {
        public static string TableName = "[dbo].[cis_fk2]";
        public static SqlCommand GetSelectCommand(SqlConnection conn, string db, int rok)
        {
            var cmd = new SqlCommand($"select [Rok], [Kod], [Nazov], [Popis] from [{db}].{TableName} where [Rok]=@rok", conn);
            cmd.Parameters.AddWithValue("@rok", rok);
            return cmd;
        }

        protected override string GetTableName() => TableName;

        public FunkcnaRiadok2(IDataRecord data)
            : base(data)
        {
            
        }
    }
}