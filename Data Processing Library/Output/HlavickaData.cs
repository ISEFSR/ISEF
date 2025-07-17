namespace cvti.data.Output
{
    using System.Collections.Generic;
    using cvti.data.Columns;
    using cvti.data.Core;
    using Newtonsoft.Json;

    /// <summary>
    /// Predstavuje data priamo ziskane z hlavickoveho XLSX suboru
    /// </summary>
    public class HlavickaData
    {
        public HlavickaData(IEnumerable<Column> stlpce, int stlpceHlavicka, int riadkyHlavicky, int riadkyStrana, int nesumStlpce)
        {

            StlpceHlavicka = stlpceHlavicka;
            RiadkyHlavicka = riadkyHlavicky;
            RiadkyStrana = riadkyStrana;
            NesumStlpce = nesumStlpce;

            _stlpce.AddRange(stlpce);
        }

        [JsonConstructor]
        public HlavickaData()
        {

        }

        /// <summary>
        /// Vrati, nastavi pocet stlpcov na hlavicku
        /// </summary>
        public int StlpceHlavicka { get; set; }
        /// <summary>
        /// Vrati, nastavi pocet riadkov na hlavicku
        /// </summary>
        public int RiadkyHlavicka { get; set; }
        /// <summary>
        /// Vrati, nastavi pocet riadkov na stranu
        /// </summary>
        public int RiadkyStrana { get; set; }
        /// <summary>
        /// Vrati, nastavi pocet nesumacnych stlpcov
        /// </summary>
        public int NesumStlpce { get; set; }

        public string LeftTitleRange { get => "A4:A4"; }
        public string RightTitleRange { get => $"{GetColumn(StlpceHlavicka - 2)}2:{GetColumn(StlpceHlavicka - 2)}2"; }

        public char PoslednyStlpec { get => GetColumn(StlpceHlavicka); }

        private readonly List<Column> _stlpce = new List<Column>();

        private char GetColumn(int index)
        {
            // za predpokladu ze sa nedostanem do stlpcov AA ...
            return (char)(64 + index);
        }

        /// <summary>
        /// Vrati stlpce pre hlavicku ako
        /// </summary>
        /// <value>
        /// Stlpce ziskane z hlavicky ako <see cref="IEnumerable{T}"/> kde T je <see cref="Column"/>
        /// </value>
        public IEnumerable<Column> Stlpce { get => _stlpce; }
    }
}
