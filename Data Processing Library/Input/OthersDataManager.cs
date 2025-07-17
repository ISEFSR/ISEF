namespace cvti.data.Input
{
    using cvti.data.Core;
    using cvti.data.Enums;
    using cvti.data.Exceptions;
    using OfficeOpenXml;
    using System;
    using System.Collections.Generic;
    using System.Drawing.Text;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    public class OthersDataManager
    {
        #region Variables And Constructor
        private readonly LogManager _log;

        public OthersDataManager(LogManager log = null)
        {
            _log = log;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Nacita udaje zo vstupneho XLSX suboru 
        /// </summary>
        /// <param name="file">Vstupny XLSX subor s datami ako <see cref="FileInfo"/></param>
        /// <param name="worksheets">Nazvy wokrsheetov, z ktorych beriem udaje</param>
        /// <param name="column">Index stlpca na ktorom zacinaju udaje</param>
        /// <param name="row">Index riadka na ktorom zacinaju udaje</param>
        /// <returns>Nacitane udaje ako <see cref="IList{T}"/> kde T je <see cref="OthersDataRow"/></returns>
        /// <exception cref="InvalidWorksheetException">Nespravna hlavicka pre worksheet</exception>
        /// <remarks>
        /// Predpoklad je, ze vo vsetkych worksheetoch su udaje na rovnakom riadku / stlpci
        /// </remarks>
        public IList<OthersDataRow> ReadFile(FileInfo file, string[] worksheets, int column, int row, int headerRow, bool zdrojVpredu)
        {
            if (!file.Exists) throw new FileNotFoundException();
            if (!file.Extension.ToLower().Equals(".xlsx")) throw new ArgumentException();

            var data = new List<OthersDataRow>();
            try
            {
                using (var pckg = new ExcelPackage(file))
                {
                    var wb = pckg.Workbook;
                    foreach (var w in worksheets)
                    {
                        ExtractDataFromWorksheet(wb.Worksheets[w], data, column, row, headerRow, zdrojVpredu);
                    }
                }

                if (_log != null)
                {
                    _log.LogNewMessageSafe(LogMessageType.BackEndMessage, $"OthersDataManager.ReadFile({file})", "Successfully red others data: " + data.Count.ToString());
                }
            }
            catch (Exception e)
            {
                if (_log != null)
                {
                    _log.LogNewMessageSafe(LogMessageType.BackEndError, $"OthersDataManager.ReadFile({file})", "Error while loading others data. " + e.Message + "." + e.StackTrace);
                }
                throw;
            }

            return data;
        }
        #endregion

        public string[] ExpectedHeader
        {
            get => header;
        }

        readonly string[] header = new string[]
        {
                "Organizácia - IČO",
                "Klient",
                "Kalendárny deň",
                "Druh rozp.",
                "Kapitola/ŠF/....",
                "Syntetický alebo fik",
                "Oddiel",
                "Skupina",
                "Trieda",
                "Podtrieda",
                "Hl.kateg.",
                "Kategória",
                "Položka",
                "Podpoložka",
                "Zdroj (druh rozp.)",
                "Projekt / Prvok",
                "Typ zdroja",
                "Schválený rozpočet",
                "Rozpočet po zmenách",
                "Skutočnosť"
        };

        readonly string[] header1 = new string[]
        {
            "Organizácia - IČO",
            "Klient",
            "Kalendárny deň",
            "Druh rozp.",
            "Kapitola/ŠF/....",
            "Syntetický alebo fik",
            "Oddiel",
            "Skupina",
            "Trieda",
            "Podtrieda",
            "Hl.kateg.",
            "Kategória",
            "Položka",
            "Podpoložka",
            "Zdroj (druh rozp.)",
            "Projekt / Prvok",
            "Druh rozpočtu RO",
            "Schválený rozpočet",
            "Upravený rozpočet",
            "Výsl. od zač. roka"
        };

        #region Private Methods

        private string EditValue(ExcelDataColumnAttribute att, string Value) {
            switch (att.ColumnName) {
                case "Zdroj (druh rozp.)":
                    return Value.PadRight(4, ' ');

                case "Oddiel":
                    return RepTrim(Value, 2).PadRight(2, ' ');

                case "Skupina":
                    return RepTrim(Value, 3).PadRight(3, ' ');

                case "Trieda":
                    return RepTrim(Value, 4).PadRight(4, ' ');

                case "Podtrieda":
                    var val = Value.Replace("#", "").Trim();

                    if (val == "") val = "     ";
                    else if (val.First() != '0') val = "0" + val;

                    val = val.PadRight(5, ' ');

                    if (val.Last() == '0')
                        val = new String(val.Take(4).ToArray()) + " ";
                    return val;
                case "Podpoložka":
                    // TODO: odstran 3 nuly na konci 
                    val = Value.PadRight(6, ' ');
                    if (val.EndsWith("000")) val = new String(val.Take(3).ToArray()) + "   ";
                    return val;
                case "Projekt / Prvok":
                    return Value.Replace("#", "").PadRight(7, ' ');
            }
            return Value;
        
        }

        private string RepTrim(string Value, int Spaces) {
            var val = Value.Replace("#", "").Trim();
            if (val == "") val = new string(' ', Spaces);
            else if (val.First() != '0') val = "0" + val;
           
            return val;
        }
        private void ExtractDataFromWorksheet(ExcelWorksheet worksheet, List<OthersDataRow> data, int column, int row, int headerRow, bool zdrojVpredu)
        {
            while (row <= worksheet.Dimension.End.Row)
            {
                var dataRow = new OthersDataRow();

                foreach(var property in typeof(OthersDataRow).GetProperties(System.Reflection.BindingFlags.Instance | 
                    System.Reflection.BindingFlags.Public))
                {
                    var attributes = property.GetCustomAttributes(typeof(ExcelDataColumnAttribute), true);
                    bool found = attributes.Count() == 0;
                    foreach (ExcelDataColumnAttribute a in attributes)
                    {
                        foreach (var i in a.Index)
                        {
                            //found = true;
                            if (worksheet.Cells[headerRow, i + column].Value != null && worksheet.Cells[headerRow, i + column].Value.ToString() == a.ColumnName)
                            {
                                //property.SetValue(dataRow, worksheet.Cells[headerRow, a.Index + column].Value);
                                found = true;

                                if (Type.Equals(a.ColumnType, typeof(String)))
                                {
                                    var Value = worksheet.Cells[row, i + column].Value.ToString();
                                    property.SetValue(dataRow, EditValue(a, Value));
                                }
                                else if (Type.Equals(a.ColumnType, typeof(Decimal)))
                                    property.SetValue(dataRow, Convert.ToDecimal(worksheet.Cells[row, i + column].Value));

                                break;
                            }
                        }
                        if (!found)
                        {
                            var headerColumnIndex = column;
                            while (worksheet.Cells[headerRow, headerColumnIndex].Value != null)
                            {
                                if (worksheet.Cells[headerRow, headerColumnIndex].Value.ToString() == a.ColumnName)
                                {
                                    found = true;
                                    if (Type.Equals(a.ColumnType, typeof(String)))
                                    {
                                        var Value = worksheet.Cells[row, headerColumnIndex].Value.ToString();
                                        property.SetValue(dataRow, EditValue(a, Value));
                                    }
                                    else if (Type.Equals(a.ColumnType, typeof(Decimal)))
                                        property.SetValue(dataRow, Convert.ToDecimal(worksheet.Cells[row, headerColumnIndex].Value));

                                    break;
                                }
                                headerColumnIndex++;
                            }
                        }
                    }
                    if (!found)
                    {
                        throw new Exception("Nenasiel som stlpec pre " + property.Name);
                        //chyba
                    }
                }
                row++;

                data.Add(dataRow);
            }
        }
        private bool SkontrolujHlavicku(ExcelWorksheet worksheet, int firstColumn, int headerRow)
        {
            
            // Na zaklade riadku s hlavickou a prvym datovym stlpcom skontrolujem ci je poradie stlpcov korektne
            for (var i = 0; i < header.Length; i++)
            {
                if (worksheet.Cells[headerRow, firstColumn + i].Value is null ||
                    (worksheet.Cells[headerRow, firstColumn + i].Value.ToString() != header[i] && worksheet.Cells[headerRow, firstColumn + i].Value.ToString() != header1[i]))
                    return false;
            }

            // ak som sa dostal az sem vsetky stlpce boli skontrolovane a na spravnom mieste ( teda aspon hlavicka )
            return true;
        }
        #endregion
    }
}