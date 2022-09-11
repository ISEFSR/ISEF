namespace cvti.data.Views
{
    using cvti.data.Core;
    using System;
    using System.Data;

    /// <summary>
    /// 
    /// </summary>
    public class StatsView : TableRow
    {
        static readonly string[] Columns = new []
        {
            "Rok",
            "DateCreated",
            "TotalCount",
            "TotalSum",
            "Nazov",
            "Popis"
        };

        public static string TableName = "[dbo].[vi_stats]";

        public StatsView(IDataRecord data)
            : base(data)
        {
        }

        public static string GetSelectCommand(string db) => $"select {string.Join(", ", Columns)} from {db}.{TableName}";

        [TableColumnAttribute()]
        public int Rok { get; set; }
        [TableColumnAttribute("DateCreated")]
        public DateTime CreatedDate { get; set; }
        [TableColumnAttribute()]
        public decimal TotalSum { get; set; }
        [TableColumnAttribute()]
        public int TotalCount { get; set; }
        [TableColumnAttribute("Nazov")]
        public string StupenSkratenyNazov { get; set; }
        [TableColumnAttribute("Popis")]
        public string StupenNazov { get; set; }

        public override string ToString()
        {
            return $"{Rok} - {StupenSkratenyNazov}: {TotalCount} - {TotalSum}";
        }
    }
}
