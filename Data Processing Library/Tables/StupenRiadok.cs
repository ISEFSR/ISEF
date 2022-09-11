namespace cvti.data.Tables
{
    using cvti.data.Core;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class StupenRiadok : CiselnikRiadok, IInsertable, IDeletable
    {
        public static string TableName = "dbo.cis_stupen";

        public static SqlCommand GetSelect(SqlConnection conn, string db)
            => new SqlCommand($"select kod, nazov, popis, komentar, farba from {db}.{TableName}", conn);

        public SqlCommand GenerateDeleteCommand(SqlConnection conn, string db)
        {
            var cmd = new SqlCommand($"delete from {db}.{TableName} where Kod=@kod");
            cmd.Parameters.AddWithValue("@kod", Kod);
            return cmd;
        }

        public SqlCommand GenerateInsertCommand(SqlConnection conn, string db)
        {
            var cmd = new SqlCommand($"insert into {db}.{TableName} (kod, nazov, popis, komentar, farba) values (@kod, @nazov, @popis, @koment, @farba)", conn);
            cmd.Parameters.AddWithValue("@kod", Kod);
            cmd.Parameters.AddWithValue("@nazov", Nazov);
            cmd.Parameters.AddWithValue("@popis", Popis);
            cmd.Parameters.AddWithValue("@koment", Komentar);
            cmd.Parameters.AddWithValue("@farba", Farba);
            return cmd;
        }

        public override SqlCommand GenerateUpdateCommand(SqlConnection conn, string db)
        {
            var cmd = new SqlCommand($"update {db}.{TableName} set nazov=@nazov, popis=@popis, komentar=@koment, farba=@farba where kod=@kod", conn);
            cmd.Parameters.AddWithValue("@kod", Kod);
            cmd.Parameters.AddWithValue("@nazov", Nazov);
            cmd.Parameters.AddWithValue("@popis", Popis);
            cmd.Parameters.AddWithValue("@koment", Komentar);
            cmd.Parameters.AddWithValue("@farba", Farba);
            return cmd;
        }

        public StupenRiadok(IDataRecord record)
            : base(record)
        {

        }

        public override string ToString() => Popis;

        [TableColumnAttribute]
        public string Kod { get; set; }
        [TableColumnAttribute]
        public string Nazov { get; set; }
        [TableColumnAttribute]
        public string Popis { get; set; }
        [TableColumnAttribute]
        public string Komentar { get; set; }
        [TableColumnAttribute]
        public int Farba { get; set; }
    }
}
