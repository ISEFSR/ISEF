namespace cvti.data.Tables
{
    using cvti.data.Core;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class OkresRiadok : CiselnikRiadok, IInsertable, IDeletable
    {
        public static string TableName = "[dbo].[cis_okres]";
        public static SqlCommand GetSelectCommand(SqlConnection conn, string db)
        {
            var cmd = new SqlCommand($"select [Kod], [KodKraj], [SkratenyNazov], [Nazov] from [{db}].{TableName}", conn);
            return cmd;
        }

        public OkresRiadok(IDataRecord record)
            : base(record)
        {

        }

        public OkresRiadok()
        {

        }

        [TableColumnAttribute("Kod")]
        public Int16 KodOkres { get; set; }
        [TableColumnAttribute]
        public Int16 KodKraj { get; set; }
        [TableColumnAttribute]
        public string Nazov { get; set; }
        [TableColumnAttribute]
        public string SkratenyNazov { get; set; }

        public override string ToString() => $"[{KodOkres}] {Nazov}";

        public override SqlCommand GenerateUpdateCommand(SqlConnection conn, string db)
        {
            var cmd = new SqlCommand($"update {db}.{TableName} set Nazov=@nazov, SkratenyNazov=@skrateny where Kod=@kod", conn);
            cmd.Parameters.AddWithValue("@nazov", Nazov);
            cmd.Parameters.AddWithValue("@skrateny", SkratenyNazov);
            cmd.Parameters.AddWithValue("@kod", KodOkres);

            return cmd;
        }

        public SqlCommand GenerateInsertCommand(SqlConnection conn, string db)
        {
            var cmd = new SqlCommand($"insert into {db}.{TableName} ([Kod], [KodKraj], [Nazov], [SkratenyNazov]) values (@kodo, @kodk, @nazov, @skrateny)", conn);
            cmd.Parameters.AddWithValue("@kodo", KodOkres);
            cmd.Parameters.AddWithValue("@kodk", KodKraj);
            cmd.Parameters.AddWithValue("@nazov", Nazov);
            cmd.Parameters.AddWithValue("@skrateny", SkratenyNazov);
            return cmd;
        }

        public SqlCommand GenerateDeleteCommand(SqlConnection conn, string db)
        {
            var cmd = new SqlCommand($"delete from {db}.{TableName} where Kod=@kod", conn);
            cmd.Parameters.AddWithValue("@kod", KodOkres);
            return cmd;
        }
    }
}
