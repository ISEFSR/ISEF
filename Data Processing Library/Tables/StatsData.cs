namespace cvti.data.Tables
{
    using cvti.data.Core;
    using System.Data;

    public class StatsData : TableRow
    {
        public StatsData(IDataRecord record)
            : base(record)
        {

        }

        [TableColumnAttribute("Count")]
        public int Count { get; set; }
        [TableColumnAttribute("Skutocnost")]
        public decimal Skutocnost { get; set; }
        [TableColumnAttribute("Vydavky")]
        public decimal Vydavky { get; set; }
        [TableColumnAttribute("Prijmy")]
        public decimal Prijmy { get; set; }
        [TableColumnAttribute("Rozpp")]
        public decimal Rozpp { get; set; }
        [TableColumnAttribute("Rozpu")]
        public decimal Rozpu { get; set; }
    }
}
