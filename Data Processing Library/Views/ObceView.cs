namespace cvti.data.Views
{
    using cvti.data.Core;
    using System.Data;

    public class ObceView : TableRow
    {
        public static string ViewName = "[dbo].[vi_obce]";

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

        public ObceView(IDataRecord record)
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
