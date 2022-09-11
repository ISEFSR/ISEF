namespace cvti.data.Tables
{
    using cvti.data.Core;
    using System.Data;
    using System.Data.SqlClient;

    public class EkonomickaRiadok3 : EkonomickaRiadok
    {
        public static string TableName = "[dbo].[cis_ek3]";
        public static SqlCommand GetSelectCommand(SqlConnection conn, string db, int rok)
        {
            var cmd = new SqlCommand($"select [Rok], [Kod], [Ek2], [Nazov], [Popis] from [{db}].{TableName} where [Rok]=@rok", conn);
            cmd.Parameters.AddWithValue("@rok", rok);
            return cmd;
        }

        public EkonomickaRiadok3(IDataRecord data)
            : base(data)
        {
        }

        [TableColumnAttribute("Ek2")]
        public string Ek2 { get; set; }

        protected override string GetTableName() => TableName;
    }
}
