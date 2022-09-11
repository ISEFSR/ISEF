namespace cvti.data.Tables
{
    using System.Data;
    using System.Data.SqlClient;

    public class ProgramRiadok3 : ProgramRiadok
    {
        public static string TableName = "[dbo].[cis_pk3]";
        public static SqlCommand GetSelectCommand(SqlConnection conn, string db, int rok)
        {
            var cmd = new SqlCommand($"select [Rok], [Kod], [Nazov], [Popis] from [{db}].{TableName} where [Rok]=@rok", conn);
            cmd.Parameters.AddWithValue("@rok", rok);
            return cmd;
        }

        public ProgramRiadok3(IDataRecord data)
            : base(data)
        {
        }

        protected override string GetTableName() => TableName;
    }
}
