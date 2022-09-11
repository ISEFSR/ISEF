using cvti.data.Core;
using System.Data.SqlClient;

namespace cvti.data.Tables
{
    public class OrganizaciaRiadok : CiselnikRiadok
    {
        public static string TableName = "[dbo].[cis_org]";
        public static SqlCommand GetSelectCommand(SqlConnection conn, string db) =>
            new SqlCommand($"select [Ico], [KodSegment], [KodStupen], [KodPodriadenost], [KodObec], [Nazov], [Ulica] from {db}.{TableName}", conn);

        public OrganizaciaRiadok(System.Data.IDataRecord record)
            : base(record)
        {
            
        }

        [TableColumnAttribute]
        public string Ico { get; set; }
        [TableColumnAttribute]
        public string KodSegment { get; set; }
        [TableColumnAttribute]
        public string KodStupen { get; set; }
        [TableColumnAttribute]
        public string KodPodriadenost { get; set; }
        [TableColumnAttribute]
        public int KodObec { get; set; }
        [TableColumnAttribute]
        public string Nazov { get; set; }
        [TableColumnAttribute]
        public string Ulica { get; set; }

        public override string ToString() => $"[{Ico}] {Nazov}";

        public override SqlCommand GenerateUpdateCommand(SqlConnection conn, string db)
        {
            var cmd = new SqlCommand($"update {db}.{TableName} set kodsegment=@segm, kodstupen=@stp, kodpodriadenost=@pod, kodobec=@obec, Nazov=@nazov, Ulica=@ulica where Ico=@ico", conn);
            cmd.Parameters.AddWithValue("@segm", KodSegment);
            cmd.Parameters.AddWithValue("@stp", KodStupen);
            cmd.Parameters.AddWithValue("@pod", KodPodriadenost);
            cmd.Parameters.AddWithValue("@obec", KodObec);
            cmd.Parameters.AddWithValue("@nazov", Nazov);
            cmd.Parameters.AddWithValue("@ulica", Ulica);
            cmd.Parameters.AddWithValue("@ico", Ico);
            return cmd;
        }
    }
}
