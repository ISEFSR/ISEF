namespace cvti.data.Tables
{
    using cvti.data.Core;
    using System.Data;
    using System.Data.SqlClient;

    public class EkonomickaRiadok6 : EkonomickaRiadok
    {
        public static string TableName = "[dbo].[cis_ek6]";
        public static SqlCommand GetSelectCommand(SqlConnection conn, string db, int rok)
        {
            var cmd = new SqlCommand($"select [Rok], [Kod], [Ek3], [Nazov], [Popis] from [{db}].{TableName} where [Rok]=@rok", conn);
            cmd.Parameters.AddWithValue("@rok", rok);
            return cmd;
        }

        public EkonomickaRiadok6(IDataRecord data)
            : base(data)
        {
        }

        [TableColumnAttribute("Ek3")]
        public string Ek3 { get; set; }

        protected override string GetTableName() => TableName;
    }
}
