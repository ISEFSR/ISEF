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

    public class SegmentRiadok : CiselnikRiadok, IInsertable, IDeletable
    {
        public static string TableName = "dbo.cis_segment";

        public static SqlCommand GetSelect(SqlConnection conn, string db)
            => new SqlCommand($"select kod, skratenytext, popis, komentar from {db}.{TableName}", conn);

        public override SqlCommand GenerateUpdateCommand(SqlConnection conn, string db)
        {
            var cmd = new SqlCommand($"update {db}.{TableName} set SkratenyText=@skr, Popis=@popis, Komentar=@koment where Kod=@kod", conn);
            cmd.Parameters.AddWithValue("@kod", Kod);
            cmd.Parameters.AddWithValue("@skr", SkratenyText);
            cmd.Parameters.AddWithValue("@popis", Popis);
            cmd.Parameters.AddWithValue("@koment", Komentar);
            return cmd;
        }

        public SqlCommand GenerateDeleteCommand(SqlConnection conn, string db)
        {
            throw new NotImplementedException();
        }

        public SqlCommand GenerateInsertCommand(SqlConnection conn, string db)
        {
            throw new NotImplementedException();
        }

        public SegmentRiadok(IDataRecord record)
            : base(record)
        {

        }

        public override string ToString() => Popis;

        [TableColumnAttribute]
        public string Kod { get; set; }
        [TableColumnAttribute]
        public string SkratenyText { get; set; }
        [TableColumnAttribute]
        public string Popis { get; set; }
        [TableColumnAttribute]
        public string Komentar { get; set; }
    }
}
