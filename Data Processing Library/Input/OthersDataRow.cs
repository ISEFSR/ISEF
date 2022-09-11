namespace cvti.data.Input
{
    public class OthersDataRow : InputDataRow
    {
        public override string Fk => PodTrieda;
        public override string Ek => PodPolozka;
        public override string Zk => Zdroj;
        public override string Pk => Projekt;
        public override decimal Rozpp { get { return SchvalRozp; } set { SchvalRozp = value; } }
        public override decimal Rozpu { get { return RozpPoZmen; } set { RozpPoZmen = value; } }
        public override decimal Skut { get { return Skutoc; } set { Skutoc = value; } }

        public string Stupen { get; set; }

        [ExcelDataColumn("Organizácia - IČO", new[] { 0 }, typeof(string))]
        public override string Ico { get; set; }

        [ExcelDataColumn("Klient", new[] { 1 }, typeof(string))]
        public string Klient { get; set; }

        [ExcelDataColumn("Kalendárny deň", new[] { 2 }, typeof(string))]
        public string KalDen { get; set; }

        [ExcelDataColumn("Druh rozp.", new[] { 3 }, typeof(string))]
        public string DruhRozp { get; set; }

        [ExcelDataColumn("Kapitola/ŠF/....", new[] { 4 }, typeof(string))]
        public string KapitSF { get; set; }

        [ExcelDataColumn("Syntetický alebo fik", new[] { 5 }, typeof(string))]
        public string SyntAlFik { get; set; }

        [ExcelDataColumn("Oddiel", new[] { 6, 7 }, typeof(string))]
        public string Oddiel { get; set; }

        [ExcelDataColumn("Skupina", new[] { 7, 8 } , typeof(string))]
        public string Skupina { get; set; }

        [ExcelDataColumn("Trieda", new[] { 8, 9 }, typeof(string))]
        public string Trieda { get; set; }

        [ExcelDataColumn("Podtrieda", new[] { 9, 10 }, typeof(string))]
        public string PodTrieda { get; set; }

        [ExcelDataColumn("Hl.kateg.", new[] { 10, 11 }, typeof(string))]
        public string HlKateg { get; set; }

        [ExcelDataColumn("Kategória", new[] { 11, 12 }, typeof(string))]
        public string Kateg { get; set; }

        [ExcelDataColumn("Položka", new[] { 12, 13 }, typeof(string))]
        public string Polozka { get; set; }

        [ExcelDataColumn("Podpoložka", new[] { 13, 14 }, typeof(string))]
        public string PodPolozka { get; set; }

        [ExcelDataColumn("Zdroj (druh rozp.)", new[] { 14, 6 }, typeof(string))]
        public string Zdroj { get; set; }

        [ExcelDataColumn("Projekt / Prvok", new[] { 15 }, typeof(string))]
        public string Projekt { get; set; }

        [ExcelDataColumn("Druh rozpočtu RO", new[] { 16 }, typeof(string))]
        [ExcelDataColumn("Typ zdroja", new[] { 16 }, typeof(string))]
        public string TypZdroja { get; set; }
        
        [ExcelDataColumn("Schválený rozpočet", new[] { 17, 18 }, typeof(decimal))]
        public decimal SchvalRozp { get; set; }

        [ExcelDataColumn("Upravený rozpočet", new[] { 18, 19 }, typeof(decimal))]
        [ExcelDataColumn("Rozpočet po zmenách", new[] { 18, 19 }, typeof(decimal))]
        public decimal RozpPoZmen { get; set; }

        [ExcelDataColumn("Výsl. od zač. roka", new[] { 19, 20 }, typeof(decimal))]
        [ExcelDataColumn("Skutočnosť", new[] { 19, 20 }, typeof(decimal))]
        public decimal Skutoc { get; set; }

        public override string GetInsert(int rok)
        {
            return $"({rok}, '{Stupen}', '{Ico}', '{Klient}', '{DruhRozp}', '{KapitSF}', '{SyntAlFik}','{PodTrieda}', '{PodPolozka}', '{Zdroj}','{Projekt}','{TypZdroja}',{SchvalRozp.ToString().Replace(",", ".")},{RozpPoZmen.ToString().Replace(",", ".")},{Skutoc.ToString().Replace(",", ".")})";

        }
    }
}