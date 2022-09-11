namespace cvti.data.Views
{
    using cvti.data.Core;
    using System.Data;

    public class OrganizaciaView : TableRow
    {
        public static string TableName = "[dbo].[vi_organizacie]";

        internal static string[] Columns = new[]
        {
            "[SegmentKod]",
            "[SegmentShort]",
            "[SegmentText]",
            "[StupenKod]",
            "[StupenShort]",
            "[StupenText]",
            "[PodriadenostKod]",
            "[PodriadenostSkrateny]",
            "[PodriadenostNazov]",
            "[KrajKod]",
            "[KrajShort]",
            "[KrajNazov]",
            "[OkresKod]",
            "[OkresShort]",
            "[OkresNazov]",
            "[ObecKod]",
            "[ObecNazov]",
            "[OrgIco]",
            "[OrgNazov]",
            "[OrgUlica]"
        };

        public static string GetSelectCommand(string db) => $"select {string.Join(", ", Columns)} from {db}.{TableName}";

        public OrganizaciaView(IDataRecord data)
            : base(data)
        {

        }

        [TableColumnAttribute]
        public string  SegmentKod { get; set; }
        [TableColumnAttribute]
        public string  SegmentShort { get; set; }
        [TableColumnAttribute]
        public string  SegmentText { get; set; }

        [TableColumnAttribute]
        public string  StupenKod { get; set; }
        [TableColumnAttribute]
        public string  StupenShort { get; set; }
        [TableColumnAttribute]
        public string  StupenText { get; set; }

        [TableColumnAttribute]
        public string  PodriadenostKod { get; set; }
        [TableColumnAttribute]
        public string  PodriadenostSkrateny { get; set; }
        [TableColumnAttribute]
        public string  PodriadenostNazov { get; set; }

        [TableColumnAttribute]
        public short  KrajKod { get; set; }
        [TableColumnAttribute]
        public string  KrajShort { get; set; }
        [TableColumnAttribute]
        public string  KrajNazov { get; set; }

        [TableColumnAttribute]
        public short  OkresKod { get; set; }
        [TableColumnAttribute]
        public string  OkresShort { get; set; }
        [TableColumnAttribute]
        public string  OkresNazov { get; set; }

        [TableColumnAttribute]
        public int  ObecKod { get; set; }
        [TableColumnAttribute]
        public string  ObecNazov { get; set; }

        [TableColumnAttribute]
        public string  OrgIco { get; set; }
        [TableColumnAttribute]
        public string  OrgNazov { get; set; }
        [TableColumnAttribute]
        public string  OrgUlica { get; set; }

        public override string ToString() => OrgNazov;
    }
}
