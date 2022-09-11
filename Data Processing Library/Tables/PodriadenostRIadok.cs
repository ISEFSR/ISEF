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

    public class PodriadenostRiadok : CiselnikRiadok, IDeletable, IInsertable
    {
        public static string TableName = "dbo.cis_pod";

        public PodriadenostRiadok(IDataRecord record)
            : base(record)
        {

        }

        [TableColumnAttribute("Kod")]
        public string Kod { get; set; }
        [TableColumnAttribute]
        public string SkratenyNazov { get; set; }
        [TableColumnAttribute]
        public string Nazov { get; set; }

        public override string ToString() => Nazov;

        public SqlCommand GenerateDeleteCommand(SqlConnection conn, string db)
        {
            var cmd = new SqlCommand($"delete from {db}.{TableName} where Kod=@kod", conn);
            cmd.Parameters.AddWithValue("@kod", Kod);
            return cmd;
        }

        public SqlCommand GenerateInsertCommand(SqlConnection conn, string db)
        {
            var cmd = new SqlCommand($"insert into {db}.{TableName} (kod, SkratenyNazov, Nazov) values (@kod, @skr, @nazov)", conn);
            cmd.Parameters.AddWithValue("@kod", Kod);
            cmd.Parameters.AddWithValue("@skr", SkratenyNazov);
            cmd.Parameters.AddWithValue("@nazov", Nazov);
            return cmd;
        }

        public override SqlCommand GenerateUpdateCommand(SqlConnection conn, string db)
        {
            var cmd = new SqlCommand($"update {db}.{TableName} set nazov=@nazov, skratenynazov=@skr where kod=@kod", conn);
            cmd.Parameters.AddWithValue("@kod", Kod);
            cmd.Parameters.AddWithValue("@skr", SkratenyNazov);
            cmd.Parameters.AddWithValue("@nazov", Nazov);
            return cmd;
        }
    }
}
