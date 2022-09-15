namespace cvti.data.Output
{
    using cvti.data.Core;
    using cvti.data.Columns;
    using cvti.data.Conditions;
    using cvti.data.Functions;
    using cvti.data.Views;
    using cvti.data.Enums;
    using cvti.data.Files;
    using Newtonsoft.Json;
    using OfficeOpenXml;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    public class Hlavicka : ISerializableJson
    {
        public Hlavicka(string filePath)
            : this(filePath, System.IO.Path.GetFileNameWithoutExtension(filePath))
        {

        }

        public Hlavicka(string filePath, string name)
        {
            FilePath = filePath;
            Name = name;

            var parsed = ParseExcelFile(filePath);

            //Type = parsed.Type;

            DlhyNazov = parsed.DlhyNazov;

            Data = parsed.Data;

            HlavickaCondition = parsed.HlavickaCondition;
        }

        [JsonConstructor]
        public Hlavicka()
        {

        }

        /// <summary>
        /// Vrati typ hlavicky
        /// </summary>
        /// <value>
        /// Typ hlavicky ako <see cref="HlavickaType"/>
        /// </value>
        public HlavickaType Type
        {
            get => ParseHlavickaType(Path.GetFileNameWithoutExtension(FilePath).ToLower());
        }
        /// <summary>
        /// Vrati, nastavi meno hlavicky
        /// </summary>
        /// <value>
        /// Meno hlavicky
        /// </value>
        /// <remarks>
        /// V ramci JMON manageru musi byt meno hlavicky unikatne
        /// </remarks>
        public string Name { get; set; }
        /// <summary>.
        /// Vrati celu cestu k hlavickovemu XLSX suboru
        /// </summary>
        /// <value>
        /// Cela cesta k hlavickovemu XLSX suboru
        /// </value>
        public string FilePath { get; set; }
        /// <summary>
        /// Vrati nastavi dlhy nazov pre hlavicku 
        /// </summary>
        /// <value>
        /// Dlhy nazov pre hlavicku 
        /// </value>
        public string DlhyNazov { get; set; }

        /// <summary>
        /// Vrati, nastavi data ziskane z hlavicky
        /// </summary>
        /// <value>
        /// Data ziskane z hlavicky ako <see cref="HlavickaData"/>
        /// </value>
        public HlavickaData Data { get; set; }
        /// <summary>
        /// Vrati, nastavi podmineku specificku pre hlavicky, na zaklade stlpcov 
        /// </summary>
        /// <value>
        /// Podmienka hlavicky na zaklade typu ako <see cref="Condition"/>
        /// </value>
        public Condition HlavickaCondition { get; set; }

        public string Code { get => Name; }

        #region Public Methods
        /// <summary>
        /// Vrati SQL SELECT command pre hlavicku 
        /// </summary>
        /// <returns>SQL SELECT command ako <see cref="SelectCommand"/></returns>
        public SelectCommand GetCommand(bool withoutInvisibleColumns = false) 
        {
            if (withoutInvisibleColumns)
                return new SelectCommand(Name, from s in Data.Stlpce where s.IsVisible select s, HlavickaCondition);
            else
                return new SelectCommand(Name, Data.Stlpce, HlavickaCondition.CloneMe(true)); 
        }
        /// <summary>
        /// Skontroluje ci hlavickovy subor existuje 
        /// </summary>
        /// <returns>True ak hlavickovy subor existuje, inak false</returns>
        public bool Exists() => System.IO.File.Exists(FilePath);
        /// <summary>
        /// 
        /// </summary>
        public void ReloadData()
        {
            Data = GetDataFromFile(FilePath, System.IO.Path.GetFileNameWithoutExtension(FilePath).ToLower());
            HlavickaCondition = GetConditionForType(Type);
        }
        /// <summary>
        /// 
        /// </summary>
        public void ReloadCondition()
        {
            HlavickaCondition = GetConditionForType(Type);
        }
        public override string ToString() => Name;
        #endregion

        #region Static Members
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static Hlavicka ParseExcelFile(string filePath)
        {
            const string RequiredExtension = ".xlsx";

            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException();

            if (!System.IO.File.Exists(filePath))
                throw new System.IO.FileNotFoundException();

            if (System.IO.Path.GetExtension(filePath).ToLower() != RequiredExtension)
                throw new ArgumentException();

            var fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(filePath).ToLower();

            var type = ParseHlavickaType(fileNameWithoutExtension);

            var condition = GetConditionForType(type);

            var data = GetDataFromFile(filePath, fileNameWithoutExtension);

            var hlavicka = new Hlavicka
            {
                Data = data,
                //Type = type;
                Name = fileNameWithoutExtension,
                HlavickaCondition = condition
            };
            hlavicka.Data = data;

            return hlavicka;
        }

        private static IEnumerable<Column> ParseTextColumns(string fileNameWithoutExtension)
        {
            const char Funkcna = 'f', Stupen = 'o', Podriadenost = 'r', CirkevneSukromne = 'c';

            var columns = new List<Column>();

            //Debug.Assert(fileNameWithoutExtension.Take(2) == "hl");
            // prijmove hlavicky zacinaju hlpp hlpg hlpn a hlpt
            // vydavkove a transferove nemaju 'upresnujuci znak'
            var index = fileNameWithoutExtension[2] == 'p' ? 4 : 3;

            // dalsie 3 znaky by mali byt all alebo cel
            // neparsujem ich nechavam ich tak
            //Debug.Assert(new string[] { "all", "cel" }.Contains(
            //    fileNameWithoutExtension.Skip(index).Take(3)));

            index += 3;
            while (index < fileNameWithoutExtension.Length)
            {
                switch (fileNameWithoutExtension[index])
                {
                    case Funkcna:
                        columns.Add(AssuView.VratStlpec(AssuViewAvailableColumns.FKod5, false));
                        columns.Add(AssuView.VratStlpec(AssuViewAvailableColumns.FNazov5));
                        index += 2;
                        break;
                    case Stupen:
                        columns.Add(AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod, false));
                        columns.Add(AssuView.VratStlpec(AssuViewAvailableColumns.StupenText));
                        index++;
                        break;
                    case Podriadenost:
                        columns.Add(AssuView.VratStlpec(AssuViewAvailableColumns.PodriadenostKod, false));
                        columns.Add(AssuView.VratStlpec(AssuViewAvailableColumns.PodriadenostNazov));
                        index += 2;
                        break;
                    case CirkevneSukromne:
                        columns.Add(AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod, false));
                        columns.Add(AssuView.VratStlpec(AssuViewAvailableColumns.StupenText));
                        columns.Add(AssuView.VratStlpec(AssuViewAvailableColumns.FKod5, false));
                        columns.Add(AssuView.VratStlpec(AssuViewAvailableColumns.FNazov5));
                        index += 2;
                        break;
                    default:
                        index++;
                        break;
                }
            }

            return columns;
        }

        private static HlavickaType ParseHlavickaType(string fileNameWithoutExtension)
        {
            const char Prijmy = 'p', Vydavky = 'v', Transfery = 't';
                
            const int TypeCharacterIndex = 2;
            switch (fileNameWithoutExtension[TypeCharacterIndex])
            {
                case Prijmy:
                    return HlavickaType.Prijmy;
                case Vydavky:
                    return HlavickaType.Vydavky;
                case Transfery:
                    return HlavickaType.Transfery;
                default:
                    return HlavickaType.Nezaradene;
            }
        }

        private static Condition GetConditionForType(HlavickaType type)
        {
            var cnd = new Equals(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.Rok), "9999") { Negate = true };
            switch (type)
            {
                
                case HlavickaType.Prijmy:
                    var CondotionEK1 = new Inlist("Ek1", AssuView.VratStlpec(AssuViewAvailableColumns.EKod1), new object[] { "1", "2", "3", "4", "5" });
                    var ConditionEk2 = new Inlist("Ek2", AssuView.VratStlpec(AssuViewAvailableColumns.EKod2), new object[] { "91", "92", "93" });
                    CondotionEK1.AddCondition(ConditionEk2, ConditionOperator.Or);
                    cnd.AddCondition(CondotionEK1, ConditionOperator.And);
                    break;
                case HlavickaType.Vydavky:
                    var cndVydavky = new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod1), new object[] { "6", "7", "8", "9" });
                    cndVydavky.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod2), new object[] { "91", "92", "93" }) { Negate = true }, ConditionOperator.And);
                    cnd.AddCondition(cndVydavky, ConditionOperator.And);
                    break;
                case HlavickaType.Transfery:
                    cnd.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod1), new object[] { "6", "7" }), ConditionOperator.And);
                    break;
                case HlavickaType.Nezaradene:
                default:
                    break;
            }
            cnd.Wrap = true;
            return cnd;
        }

        private static HlavickaData GetDataFromFile(string filePath, string fileNameWithoutExtension)
        {
            // Bunky, v hlavickovom subore, kore obsahuju hodnoty potrebne pre vytvorenie vystupu z hlavicky
            const string RiadkyHlavicka = "A14", StlpceHlavicka = "A15", RiadkyStrana = "A16";

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0];

                var riadkyHlavicka = ParseString(worksheet.Cells[RiadkyHlavicka].Value?.ToString() ?? string.Empty);
                var riadkyStrana = ParseString(worksheet.Cells[RiadkyStrana].Value?.ToString() ?? string.Empty);
                var stlpceHlavicka = ParseString(worksheet.Cells[StlpceHlavicka].Value?.ToString() ?? string.Empty);

                var nesumacneStlpce = VratPocetNesumacnychStlpcov(worksheet, stlpceHlavicka);

                if (riadkyHlavicka > 0 && riadkyStrana > 0 && stlpceHlavicka > 0 && nesumacneStlpce > 0)
                {
                    var columns = new List<Column>();

                    // podla coho sumujem
                    columns.AddRange(ParseTextColumns(fileNameWithoutExtension));

                    // to co sumujem
                    columns.AddRange(Vratstlpce(worksheet, stlpceHlavicka));

                    var data = new HlavickaData(columns, stlpceHlavicka, riadkyHlavicka,
                        riadkyStrana, nesumacneStlpce);

                    return data;
                }
                return null;
            }
        }

        private static int VratPocetNesumacnychStlpcov(ExcelWorksheet worksheet, int lastColumn)
        {
            // Riadok kde sa nachadzaju kody predstavujuce podmienku vyberu pre stlpce
            const int ColumnsRow = 12;
            //Zisti pocet nenumerickych stlpcov
            var clmn = 1;
            while (worksheet.Cells[ColumnsRow, clmn++].Value is null && clmn < lastColumn)
            { }
            return clmn - 1;
        }

        private static IEnumerable<Column> Vratstlpce(ExcelWorksheet worksheet, int lastColumn)
        {
            // Riadok kde sa nachadzaju kody predstavujuce podmienku vyberu pre stlpce
            const int ColumnsRow = 12;

            var stlpce = new List<AssuViewColumn>();
            // prechdzam 12ty riadok
            // az po posledny stlpec
            // indexy v exceli zacinaju 1
            for (int i = 1; i <= lastColumn; i++)
            {
                if (string.IsNullOrWhiteSpace(worksheet.Cells[ColumnsRow, i].Value?.ToString() ?? string.Empty))
                    continue;

                stlpce.Add(RozparsujStlpec(worksheet.Cells[ColumnsRow, i].Value.ToString()));
            }
            return stlpce;
        }

        private static int ParseString(string value)
        {
            // Rozparsuje string a vrati cele cislo
            // v pripade ak sa nepodari rozparsovat string vrati -1
            Debug.Assert(value != null);
            if (int.TryParse(value,
                           out int parsed))
                return parsed;
            return -1;
        }

        private static AssuViewColumn RozparsujStlpec(string stlpec)
        {
            Debug.Assert(stlpec != null);
            if (stlpec is null)
                throw new ArgumentNullException();

            const string IrrelevantString = "x";
            const char LogicalOperatorOr = '+';
            // TODO: toto bude este treba premysliet 
            // taktiet mozno pridat aj ine stlpce ako ekonomicku
            //const char LogicalOperatorAndNot = '-';

            var column = AssuView.VratStlpec(AssuViewAvailableColumns.Skut);

            // vyhodenie x a nasledne rozdelenie podla +
            var splittedCode = stlpec.Replace(IrrelevantString, string.Empty).Split(LogicalOperatorOr);

            var condition = new Equals(string.Empty, GetEkonomickaColumn(splittedCode[0].Length), splittedCode[0]);
            foreach (var c in splittedCode.Skip(1))
            {
                var conditionColumn = GetEkonomickaColumn(c.Length);
                // Nedal som inlist pre pripad ze budu z roznych stlpcov
                // napr 9+83+84
                condition.AddCondition(new Equals(string.Empty, GetEkonomickaColumn(c.Length), c), ConditionOperator.Or);
            }

            column.AddFunction(new ConditionalSum(condition));
            //return colimn;
            return column;
        }

        private static AssuViewColumn GetEkonomickaColumn(int length)
        {
            // Vrati stlpec na zaklade dlzky polozky v hlavickovom subore
            switch (length)
            {
                case 1:
                    return AssuView.VratStlpec(AssuViewAvailableColumns.EKod1);
                case 2:
                    return AssuView.VratStlpec(AssuViewAvailableColumns.EKod2);
                case 3:
                    return AssuView.VratStlpec(AssuViewAvailableColumns.EKod3);
                case 6:
                    return AssuView.VratStlpec(AssuViewAvailableColumns.EKod6);
                default:
                    Debug.Fail($"Unknown length pre ekonomicku polozku: {length}");
                    break;
            }
            return null;
        }
        #endregion
    }
}
