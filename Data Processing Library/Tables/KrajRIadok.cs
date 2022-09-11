using cvti.data.Core;
using System.Data;
using System.Data.SqlClient;

namespace cvti.data.Tables
{
    public class KrajRiadok : CiselnikRiadok, IInsertable, IDeletable
    {
        public static string TableName = "[dbo].[cis_kraj]";
        public static SqlCommand GetSelectCommand(SqlConnection conn, string db)
        {
            var cmd = new SqlCommand($"select [Kod], [SkratenyNazov], [Nazov] from [{db}].{TableName}", conn);
            return cmd;
        }

        public KrajRiadok(IDataRecord record)
            : base(record)
        {

        }

        public KrajRiadok()
        {

        }

        [TableColumnAttribute]
        public short Kod { get; set; }
        [TableColumnAttribute]
        public string SkratenyNazov { get; set; }
        [TableColumnAttribute]
        public string Nazov { get; set; }

        public override string ToString() => $"[{Kod}] {Nazov}";

        public override SqlCommand GenerateUpdateCommand(SqlConnection conn, string db)
        {
            var cmd = new SqlCommand($"update {db}.{TableName} set Nazov=@nazov, SkratenyNazov=@skrateny where Kod=@kod", conn);
            cmd.Parameters.AddWithValue("@nazov", Nazov);
            cmd.Parameters.AddWithValue("@skrateny", SkratenyNazov);
            cmd.Parameters.AddWithValue("@kod", Kod);

            return cmd;
        }

        public SqlCommand GenerateInsertCommand(SqlConnection conn, string db)
        {
            var cmd = new SqlCommand($"insert into {db}.{TableName} ([Kod], [Nazov], SkratenyNazov) values (@kod, @nazov, @skrateny)", conn);
            cmd.Parameters.AddWithValue("@kod", Kod);
            cmd.Parameters.AddWithValue("@nazov", Nazov);
            cmd.Parameters.AddWithValue("@skrateny", SkratenyNazov);
            return cmd;
        }

        public SqlCommand GenerateDeleteCommand(SqlConnection conn, string db)
        {
            var cmd = new SqlCommand($"delete from {db}.{TableName} where Kod=@kod", conn);
            cmd.Parameters.AddWithValue("@kod", Kod);
            return cmd;
        }
    }
}
