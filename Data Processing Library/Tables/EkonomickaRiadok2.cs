namespace cvti.data.Tables
{
    using cvti.data.Core;
    using System.Data;
    using System.Data.SqlClient;

    public class EkonomickaRiadok2 : EkonomickaRiadok
    {
        public static string TableName = "[dbo].[cis_ek2]";
        public static SqlCommand GetSelectCommand(SqlConnection conn, string db, int rok)
        {
            var cmd = new SqlCommand($"select [Rok], [Kod], [Ek1], [Nazov], [Popis] from [{db}].{TableName} where [Rok]=@rok", conn);
            cmd.Parameters.AddWithValue("@rok", rok);
            return cmd;
        }

        public EkonomickaRiadok2(IDataRecord data)
            : base(data)
        {
        }

        [TableColumnAttribute("Ek1")]
        public string Ek1 { get; set; }

        protected override string GetTableName() => TableName;
    }
}
