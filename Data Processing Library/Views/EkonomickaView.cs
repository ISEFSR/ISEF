namespace cvti.data.Views
{
    using cvti.data.Core;
    using System.Data;

    public class EkonomickaView : TableRow
    {
        internal static string[] Columns = new[]
       {
           "[ERok]"
          ,"[EKod1]"
          ,"[ENazov1]"
          ,"[EKod2]"
          ,"[ENazov2]"
          ,"[EKod3]"
          ,"[ENazov3]"
          ,"[EKod6]"
          ,"[ENazov6]"
          ,"[JeTransfer]"
        };

        public static string TableName = "[dbo].[vi_ekonomicka]";

        public static string GetSelectCommand(string db) => $"select {string.Join(", ", Columns)} from {db}.{TableName}";

        public EkonomickaView(IDataRecord data)
            : base(data)
        {

        }

        [TableColumnAttribute]
        public int ERok { get; set; }
        [TableColumnAttribute]
        public string EKod1 { get; set; }
        [TableColumnAttribute]
        public string ENazov1 { get; set; }
        [TableColumnAttribute]
        public string ENazov2 { get; set; }
        [TableColumnAttribute]
        public string EKod2 { get; set; }
        [TableColumnAttribute]
        public string EKod3 { get; set; }
        [TableColumnAttribute]
        public string ENazov3 { get; set; }
        [TableColumnAttribute]
        public string EKod6 { get; set; }
        [TableColumnAttribute]
        public string ENazov6 { get; set; }
    }
}
