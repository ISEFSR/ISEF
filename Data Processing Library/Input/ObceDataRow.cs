namespace cvti.data.Input
{
    using System;
    using System.Data;
    using System.Globalization;

    /// <summary>
    /// Riadok zo vstupneho suboru pre mesta a obce
    /// </summary>
    /// <remarks>
    /// Skor by to malo byt riesenie nejakym configom kde by si user nadefinoval vstupne subory. 
    /// Prip toto by aspon mohlo vystupovat ako DBFDataRow
    /// </remarks>
    public class ObceDataRow : InputDataRow
    {
        public ObceDataRow(IDataRecord record, string fileName)
        {
            FileName = fileName;

            KodOkres = record.GetString(0);
            KodObec = record.GetString(1);
            Ico = record.GetString(11);
            Segment = record.GetString(3);
            Su = record.GetString(4);
            Cast = record.GetString(5);
            Ae = record.GetString(6);
            ObrrMd = record.GetDecimal(7);
            ObrrDal = record.GetDecimal(8);
            Rozpu = record.GetDecimal(9);
            Rozpp = record.GetDecimal(10);
            IcoNadriadene = record.GetString(2);
            Nazov = record.GetString(12);
            Ulica = record.GetString(13);
            Mesto = record.GetString(14);
            Sknace = record.GetString(15);
            TypZdroja = record.GetString(16);

            Validate();
        }

        public override void Validate()
        {
            base.Validate();
            if (Ae.Length < 23) 
                throw new ArgumentException("Očakávaná dĺžka analytickej evidencie je > 23");
        }

        public override string Ico { get; set; }
        public override decimal Rozpp { get; set; }
        public override decimal Rozpu { get; set; }
        public override decimal Skut { get { return Math.Abs(ObrrDal - ObrrMd); } set { } }
        public override string Fk { get { return Ae.Substring(0, 5); } }
        public override string Ek { get { return Ae.Substring(5, 6); } }
        public override string Zk { get { return Ae.Substring(11, 4); } }
        public override string Pk { get { return Ae.Substring(15, 7); } }

        public string FileName { get; set; }

        public string Ae { get; set; }
        public string Segment { get; set; }
        public string Su { get; set; }
        public string Cast { get; set; }  
        public string Sknace { get; set; }
        public string TypZdroja { get; set; }
        /// <summary>
        /// Vrati, nastavi rocny obrat na strane dal
        /// </summary>
        public decimal ObrrDal { get; set; }
        /// <summary>
        /// Vrati, nastavi rocny obrat na strane ma dat
        /// </summary>
        public decimal ObrrMd { get; set; }

        public string KodOkres { get; set; }
        public string KodObec { get; set; }
        public string IcoNadriadene { get; set; }
        public string Nazov { get; set; }
        public string Ulica { get; set; }
        public string Mesto { get; set; }

        public override string GetInsert(int rok)
        {
            var nfi = new NumberFormatInfo() { NumberDecimalSeparator = "." };
            return $"({rok}, '{Ico}', '{IcoNadriadene}', '{KodOkres}', '{KodObec}', '{Segment}', '{Su}', '{Cast}', '{Fk}', '{Ek}', '{Zk}', '{Pk}', {(ObrrDal + ObrrMd).ToString(nfi)}, {Rozpp.ToString(nfi)}, {Rozpu.ToString(nfi)}, '{Sknace}', '{TypZdroja}', 0)";
        }
    }
}