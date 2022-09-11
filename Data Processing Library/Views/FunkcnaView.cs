namespace cvti.data.Views
{
    using cvti.data.Core;
    using System.Data;

    public class FunkcnaView : TableRow
    {
        internal static string[] Columns = new[]
       {
            "[FRok]"
            ,"[FKod2]"
          ,"[FNazov2]"
            ,"[FKod3]"
          ,"[FNazov3]"
            ,"[FKod4]"
          ,"[FNazov4]"
          ,"[FKod5]"
          ,"[FNazov5]"
        };

        public static string TableName = "[dbo].[vi_funkkcna]";

        public static string GetSelectCommand(string db) => $"select {string.Join(", ", Columns)} from {db}.{TableName}";

        public FunkcnaView(IDataRecord data)
            : base(data)
        {

        }

        [TableColumnAttribute]
        public int FRok { get; set; }
        [TableColumnAttribute]
        public string FKod2 { get; set; }
        [TableColumnAttribute]
        public string FNazov2 { get; set; }
        [TableColumnAttribute]
        public string FKod3 { get; set; }
        [TableColumnAttribute]
        public string FNazov3 { get; set; }
        [TableColumnAttribute]
        public string FKod4 { get; set; }
        [TableColumnAttribute]
        public string FNazov4 { get; set; }
        [TableColumnAttribute]
        public string FKod5 { get; set; }
        [TableColumnAttribute]
        public string FNazov5 { get; set; }
    }
}
