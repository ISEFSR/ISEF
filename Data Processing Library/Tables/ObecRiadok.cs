namespace cvti.data.Tables
{
    using cvti.data.Core;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ObecRiadok : CiselnikRiadok, IInsertable, IDeletable
    {
        public static string TableName = "[dbo].[cis_obec]";
        public static SqlCommand GetSelectCommand(SqlConnection conn, string db)
        {
            var cmd = new SqlCommand($"select [Kod], [KodOkres], [Nazov], [Skrateny] from [{db}].{TableName}", conn);
            return cmd;
        }

        public ObecRiadok(IDataRecord record)
            : base(record)
        {

        }

        public ObecRiadok()
        {

        }

        [TableColumnAttribute]
        public int Kod { get; set; }
        [TableColumnAttribute]
        public Int16 KodOkres { get; set; }
        [TableColumnAttribute]
        public string Nazov { get; set; }
        [TableColumnAttribute]
        public string Skrateny { get; set; }

        public override string ToString() => $"[{Kod}] {Nazov}";

        public override SqlCommand GenerateUpdateCommand(SqlConnection conn, string db)
        {
            var cmd = new SqlCommand($"update {db}.{TableName} set Nazov=@nazov, Skrateny=@skrateny where Kod=@kod", conn);
            cmd.Parameters.AddWithValue("@nazov", Nazov);
            cmd.Parameters.AddWithValue("@skrateny", Skrateny);
            cmd.Parameters.AddWithValue("@kod", Kod);

            return cmd;
        }

        public SqlCommand GenerateInsertCommand(SqlConnection conn, string db)
        {
            var cmd = new SqlCommand($"insert into {db}.{TableName} ([Kod], [KodOkres], [Nazov], [Skrateny]) values (@kodo, @kodokres, @nazov, @skrateny)", conn);
            cmd.Parameters.AddWithValue("@kodo", Kod);
            cmd.Parameters.AddWithValue("@kodokres", KodOkres);
            cmd.Parameters.AddWithValue("@nazov", Nazov);
            cmd.Parameters.AddWithValue("@skrateny", Skrateny);
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
