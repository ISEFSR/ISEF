namespace cvti.data.Views
{
    using cvti.data.Columns;
    using cvti.data.Core;
    using cvti.data.Enums;
    using System;
    using System.Data;
    using System.Linq;

    public class AssuView : TableRow
    {
        private static bool IsNumeric(AssuViewAvailableColumns clmn)
        {
            var numericColumns = new AssuViewAvailableColumns[]
            {
                AssuViewAvailableColumns.Skut,
                AssuViewAvailableColumns.Rozpp,
                AssuViewAvailableColumns.Rozpu,
                AssuViewAvailableColumns.Rok,
                AssuViewAvailableColumns.ObecKod,
                AssuViewAvailableColumns.OkresKod,
                AssuViewAvailableColumns.KrajKod
            };
            return numericColumns.Contains(clmn);
        }
        public static AssuViewColumn VratStlpec(AssuViewAvailableColumns stlpec, bool isVisible = true, string alias = null)
        {
            var clmn = new AssuViewColumn(stlpec, stlpec.ToString(), IsNumeric(stlpec))
            {
                IsVisible = isVisible
            };
            if (alias != null)
                clmn.ColumnAlias = alias;
            return clmn;
        }

        public static string ViewName = "[dbo].[vi_assu]";

        static readonly string[] Columns = new[]
        {
            "[Ucet]",
            "[Druh_rozpp]",
            "[Skut]",
            "[Rozpp]",
            "[Rozpu]"
        };

        public static string GetSelectCommand(string db)
            => $"select {string.Join(",", Columns)}," +
            $"{string.Join(",", OrganizaciaView.Columns)}," +
            $" {string.Join(", ", FunkcnaView.Columns)}," +
            $" {string.Join(", ", EkonomickaView.Columns)}," +
            $" {string.Join(", ", ZdrojView.Columns)}," +
            $" {string.Join(", ", ProgramView.Columns)}" +
            $" from {db}.{ViewName}";

        public AssuView(IDataRecord record)
            : base(record)
        {
            Ekonomicka = new EkonomickaView(record);
            Funkcna = new FunkcnaView(record);
            Zdroj = new ZdrojView(record);
            Program = new ProgramView(record);
            Organizacia = new OrganizaciaView(record);
        }

        [TableColumnAttribute]
        public string Ucet { get; set; }
        [TableColumnAttribute]
        public string Druh_rozpp { get; set; }
        [TableColumnAttribute]
        public decimal Skut { get; set; }
        [TableColumnAttribute]
        public decimal Rozpp { get; set; }
        [TableColumnAttribute]
        public decimal Rozpu { get; set; }

        public EkonomickaView Ekonomicka { get; set; }
        public FunkcnaView Funkcna { get; set; }
        public ZdrojView Zdroj { get; set; }
        public ProgramView Program { get; set; }
        public OrganizaciaView Organizacia { get; set; }
    }
}
