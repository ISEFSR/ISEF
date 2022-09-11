namespace cvti.data.Views
{
    using cvti.data.Core;
    using System.Data;

    /// <summary>
    /// 
    /// </summary>
    public class ProgramView : TableRow
    {
        public static string TableName = "[dbo].[vi_program]";

        internal static string[] Columns = new[]
        {
            "[PRok]"
            ,"[PKod3]"
            ,"[PNazov3]"
            ,"[PKod5]"
            ,"[PNazov5]"
            ,"[PKod7]"
            ,"[PNazov7]"
        };

        public static string GetSelectCommand(string db) => $"select {string.Join(", ", Columns)} from {db}.{TableName}";

        public ProgramView(IDataRecord data)
            : base(data)
        {
            
        }

        [TableColumnAttribute]
        public int PRok { get; set; }
        [TableColumnAttribute]
        public string PKod3 { get; set; }
        [TableColumnAttribute]
        public string PNazov3 { get; set; }
        [TableColumnAttribute]
        public string PKod5 { get; set; }
        [TableColumnAttribute]
        public string PNazov5 { get; set; }
        [TableColumnAttribute]
        public string PKod7 { get; set; }
        [TableColumnAttribute]
        public string PNazov7 { get; set; }
    }
}
