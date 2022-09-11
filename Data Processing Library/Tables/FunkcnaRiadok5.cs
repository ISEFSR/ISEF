namespace cvti.data.Tables
{
    using cvti.data.Core;
    using System.Data;
    using System.Data.SqlClient;

    public class FunkcnaRiadok5 : FunkcnaRiadok
    {
        public static string TableName = "[dbo].[cis_fk5]";
        public static SqlCommand GetSelectCommand(SqlConnection conn, string db, int rok)
        {
            var cmd = new SqlCommand($"select [Rok], [Kod], [fk4], [Nazov], [Popis] from [{db}].{TableName} where [Rok]=@rok", conn);
            cmd.Parameters.AddWithValue("@rok", rok);
            return cmd;
        }

        protected override string GetTableName() => TableName;

        public FunkcnaRiadok5(IDataRecord data)
            : base(data)
        {
            
        }

        [TableColumnAttribute("Fk4")]
        public string Fk4 { get; set; }
    }
}
