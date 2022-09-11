namespace cvti.data.Views
{
    using cvti.data.Core;
    using System;
    using System.Data;

    /// <summary>
    /// Abstraktna reprezentacia jedneho riadku z view na prezeranie zdrojov 
    /// </summary>
    /// <remarks>
    /// Zobrazi vsetky zdroje v jednom view zdroje na 1 znak, 2 znaky, 3 znaky a 4 znaky
    /// </remarks>
    public class ZdrojView : TableRow
    {
        public static string TableName = "[dbo].[vi_zdroj]";

        internal static string[] Columns = new[]
        {
             "[ZRok]"
            ,"[ZKod1]"
            ,"[ZNazov1]"
            ,"[ZKod2]"
            ,"[ZNazov2]"
            ,"[ZKod3]"
            ,"[ZNazov3]"
            ,"[ZKod4]"
            ,"[ZNazov4]"
            ,"[Pom_kod1]"
            ,"[Pom_kod2]"
            ,"[Pom_kod3]"
            ,"[Pom_kod4]"
            ,"[Pom_kod5]"
            ,"[kde]"
        };

        public static string GetSelectCommand(string db) => $"select {string.Join(", ", Columns)} from {db}.{TableName}";

        public ZdrojView(IDataRecord data)
            : base(data)
        {
        }

        [TableColumnAttribute()]
        public int Rok { get; set; }
        [TableColumnAttribute()]
        public string ZKod1 { get; set; }
        [TableColumnAttribute()]
        public string ZNazov1 { get; set; }
        [TableColumnAttribute()]
        public string ZKod2 { get; set; }
        [TableColumnAttribute()]
        public string ZNazov2 { get; set; }
        [TableColumnAttribute()]
        public string ZKod3 { get; set; }
        [TableColumnAttribute()]
        public string ZNazov3 { get; set; }
        [TableColumnAttribute()]
        public string ZKod4 { get; set; }
        [TableColumnAttribute()]
        public string ZNazov4 { get; set; }
        [TableColumnAttribute()]
        public string Pom_Kod1 { get; set; }
        [TableColumnAttribute()]
        public string Pom_Kod2 { get; set; }
        [TableColumnAttribute()]
        public string Pom_Kod3 { get; set; }
        [TableColumnAttribute()]
        public string Pom_Kod4 { get; set; }
        [TableColumnAttribute()]
        public string Pom_Kod5 { get; set; }
        [TableColumnAttribute()]
        public string Kde { get; set; }
    }
}
