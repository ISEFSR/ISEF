namespace cvti.data.Tables
{
    using cvti.data.Core;
    using System.Data;
    using System.Data.SqlClient;

    public class FunkcnaRiadok4 : FunkcnaRiadok
    {
        public static string TableName = "[dbo].[cis_fk4]";
        public static SqlCommand GetSelectCommand(SqlConnection conn, string db, int rok)
        {
            var cmd = new SqlCommand($"select [Rok], [Kod], [fk3], [Nazov], [Popis] from [{db}].{TableName} where [Rok]=@rok", conn);
            cmd.Parameters.AddWithValue("@rok", rok);
            return cmd;
        }

        public FunkcnaRiadok4(IDataRecord data)
            : base(data)
        {
            
        }

        [TableColumnAttribute("Fk3")]
        public string Fk3 { get; set; }

        protected override string GetTableName() => TableName;
    }
}
