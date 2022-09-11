namespace cvti.data.Tables
{
    using cvti.data.Core;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class UcetRiadok : CiselnikRiadok
    {
        public static string TableName = "dbo.cis_ucet";

        public UcetRiadok(IDataRecord record)
            : base(record)
        {

        }

        [TableColumnAttribute]
        public string Kod { get; set; }
        [TableColumnAttribute]
        public string Nazov { get; set; }
        [TableColumnAttribute]
        public string Popis { get; set; }

        public override string ToString() => $"[{Kod}] {Nazov}";

        public override SqlCommand GenerateUpdateCommand(SqlConnection conn, string db)
        {
            var cmd = new SqlCommand($"update {db}.{TableName} set Nazov=@nazov, Popis=@popis where Kod=@kod", conn);
            cmd.Parameters.AddWithValue("@kod", Kod);
            cmd.Parameters.AddWithValue("@nazov", Nazov);
            cmd.Parameters.AddWithValue("@popis", Popis);
            return cmd;
        }
    }
}
