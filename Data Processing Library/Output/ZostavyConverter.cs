namespace cvti.data.Output
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Sluzi na konvertovanie udajov ziskanych z databazy do finalnej formy
    /// Teda z <see cref="DatovyRiadok"/> na <see cref="ZostavaDataRow"/>
    /// </summary>
    internal static class ZostavyConverter
    {
        /// <summary>
        /// Skonvertuje data ziskane pre zostavu 
        /// </summary>
        /// <param name="zostava">Zostava ako <see cref="Zostava"/></param>
        /// <param name="data">Data pre zostavu ako <see cref="IEnumerable{T}"/> kde T je <see cref="DatovyRiadok"/></param>
        /// <param name="transfer">Transferove sumy odpovedajuce stlpcom zostavy ako <see cref="List{T}"/> kde T je <see cref="decimal"/></param>
        /// <param name="withoutTransfer">Sumy bez transferov odpovedajuce stlpcom zostavy ako <see cref="List{T}"/> kde T je <see cref="decimal"/></param>
        /// <returns>Skonvertovane riadky pre zsotavu ako <see cref="IEnumerable{T}"/> kde T je <see cref="ZostavaDataRow"/></returns>
        /// <exception cref="ArgumentNullException">V pripade ak je argument predastavujuci zostavu, alebo data null</exception>
        /// <exception cref="ArgumentException">Ak niesu poskytnute data, alebo pocet stlpcov v sumach za trasnfery a bez trasferov neodpoveda poctu stlpcov v hlavicke zostavy</exception>
        /// <exception cref="NotImplementedException">V pripade ak ma zostava iny pocet nesumacnych stlpcov ako 2, alebo 3</exception>
        public static IEnumerable<ZostavaDataRow> ConvertToZostavaRows(Zostava zostava, IEnumerable<DatovyRiadok> data, List<decimal> transfer = null, List<decimal> withoutTransfer = null, bool hlavicka = true)
        {
            // zostava ani data pre zostavu nemozu byt null
            if (zostava is null || data is null)
                throw new ArgumentNullException();

            // exportujem iba v pripade ak existuju nejake data pre zostavu
            if (!data.Any())
                throw new ArgumentException("No data provided");

            //// ziskam pocet sumacnych stlpcov pre zostavu (hlavicku)
            //var pocetSumacnychStlpcov = zostava.Hlavicka.Data.Stlpce.Count() - zostava.Hlavicka.Data.NesumStlpce;

            //// v pripade ak boli poskytnute transfery a zaroven pocet stlpcov je rozny od poctu sumacnych stlpcov 
            //if (transfer != null && withoutTransfer != null &&
            //    (transfer.Count != pocetSumacnychStlpcov || withoutTransfer.Count != pocetSumacnychStlpcov))
            //    throw new ArgumentException($"Number of sum columns {pocetSumacnychStlpcov} is not the same as number of transfer columns {transfer.Count}");

            if (zostava.Hlavicka.Data.NesumStlpce == 2)
            { 
                return ConvertToZostavaRows2(zostava, data, transfer, withoutTransfer, hlavicka);
            }
            else if (zostava.Hlavicka.Data.NesumStlpce == 3)
            {
                return ConvertToZostavaRows3(zostava, data, transfer, withoutTransfer, hlavicka);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        #region Private Methods

        private static IEnumerable<ZostavaDataRow> ConvertToZostavaRows2(Zostava zostava, IEnumerable<DatovyRiadok> data, List<decimal> transfer = null, List<decimal> withoutTransfer = null, bool hlavicka = true)
        {
            // Spracovanie 'dvojkovej zsotavy'
            // teda zostavy s dvoma nadpismi
            // Skonvertovane vstupne riadky
            var convertedRows = new List<ZostavaDataRow>();

            // Index riadku a stlpca vo vystupnom exceli
            var excelRowIndex = zostava.Hlavicka.Data.RiadkyHlavicka + 1;

            // Vlozim prvy nadpis pre 'dvojokovu zostavu'
            var title = new ZostavaDataRow(ZostavaRowType.Title);
            title.AddColumn(new ZostavaDataColumn(excelRowIndex++,
                1, data.First().Values.First().ToString()));

            // Pridam prvy nadpis
            convertedRows.Add(title);

            // Postupne prechadzam vsetky datove riadky
            foreach (var r in data)
            {
                // skontrolujem ci sa nahodou nezmenil nadpis ak sa zmenil treba zosumovat a nastavit novy
                if (r.Values.First().ToString() != title.Columns[0].Value.ToString())
                {
                    // zmenil sa nadpis treba zosumovat
                    convertedRows.Add(CreateSum(title.Columns[0].Value.ToString(), excelRowIndex, data));

                    // inkrementujem riadok nezajima ma ci skocil na dalsiu stranu lebo davam hlavicku tak ci tak
                    IncrementRow(ref excelRowIndex, zostava, convertedRows, true, hlavicka);

                    // davam novy nadpis
                    title = new ZostavaDataRow(ZostavaRowType.Title);
                    title.AddColumn(new ZostavaDataColumn(excelRowIndex, 1, r.Values.First().ToString()));
                    convertedRows.Add(title);
                    IncrementRow(ref excelRowIndex, zostava, convertedRows, false, hlavicka);
                }

                var dataRow = new ZostavaDataRow(ZostavaRowType.Data);
                for (var clmnIndex = 1; clmnIndex < r.Values.Count(); clmnIndex++)
                    dataRow.AddColumn(new ZostavaDataColumn(excelRowIndex, clmnIndex, r.Values[clmnIndex]));
                convertedRows.Add(dataRow);

                if (IncrementRow(ref excelRowIndex, zostava, convertedRows, false, hlavicka))
                {
                    title = new ZostavaDataRow(ZostavaRowType.Title);
                    title.AddColumn(new ZostavaDataColumn(excelRowIndex, 1, r.Values.First().ToString()));
                    convertedRows.Add(title);
                    IncrementRow(ref excelRowIndex, zostava, convertedRows, false, hlavicka);
                }
            }

            convertedRows.Add(CreateSum(title.Columns[0].Value.ToString(), excelRowIndex, data));
            IncrementRow(ref excelRowIndex, zostava, convertedRows, false, hlavicka);
            convertedRows.Add(CreateSum(string.Empty, excelRowIndex, data));

            if (transfer != null)
            {
                // inkrementujem riadok po poslednej pridanej sumy
                IncrementRow(ref excelRowIndex, zostava, convertedRows, false, hlavicka);

                // nastavim index spracuvaneho stlpca
                var clmnIndex = 1;

                // Odpocet transferov
                var transfersRow = new ZostavaDataRow(ZostavaRowType.Sum);

                // Pridavam hlavicku pre riadok
                transfersRow.AddColumn(new ZostavaDataColumn(excelRowIndex, clmnIndex++, "Suma za transfery"));

                // Prebehnem vsetky zosumovane transfery a postupne ich pridam
                foreach (var t in transfer)
                    transfersRow.AddColumn(new ZostavaDataColumn(excelRowIndex, clmnIndex++, t));
                convertedRows.Add(transfersRow);

                // Inkrementujem riadok
                IncrementRow(ref excelRowIndex, zostava, convertedRows, false,hlavicka);

                clmnIndex = 1;
                // Celkovo bez transferov
                transfersRow = new ZostavaDataRow(ZostavaRowType.Sum);

                // Pridam hlavicku pre sumu bez transfoerv
                transfersRow.AddColumn(new ZostavaDataColumn(excelRowIndex, clmnIndex++, "Suma bez transferov"));

                // Prebehnem vsetky zosumovane transfery a postupne ich pridam
                foreach (var t in withoutTransfer)
                    transfersRow.AddColumn(new ZostavaDataColumn(excelRowIndex, clmnIndex++, t));
                convertedRows.Add(transfersRow);
            }

            return convertedRows;
        }

        private static IEnumerable<ZostavaDataRow> ConvertToZostavaRows3(Zostava zostava, IEnumerable<DatovyRiadok> data, List<decimal> transfer = null, List<decimal> withoutTransfer = null, bool hlavicka = true)
        {
            // Spracovanie 'trojkovej zsotavy'
            // teda zostavy s troma nadpismi
            // Skonvertovane vstupne riadky
            var convertedRows = new List<ZostavaDataRow>();

            // Musim ziskat title1 a title2 pre trojkovu zostavu
            // Respektive index kde sa title1 a title2 nachadzaju
            // Asi to mozem robit cez visible stlpce
            var title1 = -1;
            var title2 = -1;
            for (var i = 0; i < zostava.Hlavicka.Data.Stlpce.Count(); i++)
            {
                if (title1 != -1 && title2 != -1)
                    break;

                if (zostava.Hlavicka.Data.Stlpce.ElementAt(i).IsVisible && !zostava.Hlavicka.Data.Stlpce.ElementAt(i).IsNumeric)
                {
                    if (title1 == -1)
                        title1 = i;
                    else
                        title2 = i;
                }
            }

            if (title2 == -1)
                throw new ArgumentException("Nepodarilo sa mi najst dve polozky podla ktorych sumujem");

            // Ok v tomto bode premenna title1 obsahuje index kde budem porovnavat title1 pre medzisucty
            //a title2 obsahuje index riadky pre druhy stlpec podla ktoreho sumujem
            var excelRow = zostava.Hlavicka.Data.RiadkyHlavicka + 1;
            var excelColumn = 1;

            // Vlozim prvy nadpis pre 'trojkovu zostavu'
            var firstTitle = data.ElementAt(0).Values[0].ToString();
            var secondTitle = data.ElementAt(0).Values[1].ToString();

            // pridam prvy nadpis
            // druhy nadpisovy riadok nepridam ( nadpis sa schova do datoveho riadku na stlpec A )
            var titleRow = new ZostavaDataRow(ZostavaRowType.Title);
            titleRow.AddColumn(new ZostavaDataColumn(excelRow++, excelColumn, firstTitle));
            convertedRows.Add(titleRow);

            var dataRow = new ZostavaDataRow(ZostavaRowType.Data);
            dataRow.AddColumn(new ZostavaDataColumn(excelRow, excelColumn++, secondTitle));

            var title = false;
            foreach (var r in data)
            {
                // Kontrolujem ci sa mi zmenil druhy nadpis
                // ak ano tak potrebujem sumovat 
                if (!r.Values[1].ToString().Equals(secondTitle) || !r.Values[0].ToString().Equals(firstTitle))
                {
                    // zmenil sa nadpis treba zosumovat
                    convertedRows.Add(CreateSum(firstTitle, secondTitle, excelRow, data));
                    // inkrementujem riadok
                    // nezajjima ma ci skocil na dalsiu stranu lebo davam hlavicku tak ci tak
                    IncrementRow(ref excelRow, zostava, convertedRows, true, hlavicka);

                    title = true;
                    secondTitle = r.Values[1].ToString();
                }
            
                // skontroluj prvy title
                // staci ked kontrolujem v ramci zmeny druheho
                if (!r.Values[0].ToString().Equals(firstTitle))
                {
                    // zmenil sa nadpis treba zosumovat
                    convertedRows.Add(CreateSum(firstTitle, excelRow, data, true));
                    // inkrementujem riadok
                    // nezajjima ma ci skocil na dalsiu stranu lebo davam hlavicku tak ci tak
                    IncrementRow(ref excelRow, zostava, convertedRows, true, hlavicka);

                    firstTitle = r.Values[0].ToString();
                    var nadpis = new ZostavaDataRow(ZostavaRowType.Title);
                    nadpis.AddColumn(new ZostavaDataColumn(excelRow, 1, firstTitle));
                    convertedRows.Add(nadpis);
                    IncrementRow(ref excelRow, zostava, convertedRows, false, hlavicka);
                }

                if (title)
                {
                    dataRow.AddColumn(new ZostavaDataColumn(excelRow, 1, secondTitle));
                    title = false;
                }
                excelColumn = 2;
                for (var clmnIndex = 2; clmnIndex < r.Values.Count(); clmnIndex++)
                {
                    dataRow.AddColumn(new ZostavaDataColumn(excelRow, excelColumn++, r.Values[clmnIndex]));
                }

                convertedRows.Add(dataRow);
                dataRow = new ZostavaDataRow(ZostavaRowType.Data);
                IncrementRow(ref excelRow, zostava, convertedRows, false, hlavicka);
                excelColumn = 2;
            }

            convertedRows.Add(CreateSum(firstTitle, secondTitle, excelRow, data));
            IncrementRow(ref excelRow, zostava, convertedRows, false, hlavicka);
            convertedRows.Add(CreateSum(firstTitle, excelRow, data, true));
            IncrementRow(ref excelRow, zostava, convertedRows, false, hlavicka);
            convertedRows.Add(CreateSum(string.Empty, excelRow, data, true));

            if (transfer != null)
            {
                // inkrementujem riadok po poslednej pridanej sumy
                IncrementRow(ref excelRow, zostava, convertedRows, false, hlavicka);

                // nastavim index spracuvaneho stlpca
                var clmnIndex = 1;

                // Odpocet transferov
                var transfersRow = new ZostavaDataRow(ZostavaRowType.Sum);

                // Pridavam hlavicku pre riadok
                transfersRow.AddColumn(new ZostavaDataColumn(excelRow, clmnIndex++, "Suma za transfery"));
                clmnIndex++;

                // Prebehnem vsetky zosumovane transfery a postupne ich pridam
                foreach (var t in transfer)
                    transfersRow.AddColumn(new ZostavaDataColumn(excelRow, clmnIndex++, t));
                convertedRows.Add(transfersRow);

                // Inkrementujem riadok
                IncrementRow(ref excelRow, zostava, convertedRows, false, hlavicka);

                clmnIndex = 1;
                // Celkovo bez transferov
                transfersRow = new ZostavaDataRow(ZostavaRowType.Sum);

                // Pridam hlavicku pre sumu bez transfoerv
                transfersRow.AddColumn(new ZostavaDataColumn(excelRow, clmnIndex++, "Suma bez transferov"));
                clmnIndex++;

                // Prebehnem vsetky zosumovane transfery a postupne ich pridam
                foreach (var t in withoutTransfer)
                    transfersRow.AddColumn(new ZostavaDataColumn(excelRow, clmnIndex++, t));
                convertedRows.Add(transfersRow);
            }

            return convertedRows;
        }

        private static bool IncrementRow(ref int rowIndex, Zostava zostava, List<ZostavaDataRow> convertedRows, bool headerIncoming = false, bool hlavicka = true)
        {
            rowIndex++;
            var nextPage = false;
            if (hlavicka)
            {
                if ((rowIndex - 1) % zostava.Hlavicka.Data.RiadkyStrana == 0)
                {
                    nextPage = true;
                }
                if (headerIncoming && rowIndex % zostava.Hlavicka.Data.RiadkyStrana == 0)
                {
                    nextPage = true;
                    rowIndex++;
                }

                if (nextPage)
                {
                    var zostavaRow = new ZostavaDataRow(ZostavaRowType.Header);
                    zostavaRow.AddColumn(new ZostavaDataColumn(rowIndex, 1, string.Empty));
                    convertedRows.Add(zostavaRow);
                    rowIndex += zostava.Hlavicka.Data.RiadkyHlavicka;
                }
            }
            return nextPage;
        }
        
        private static ZostavaDataRow CreateSum(string sumBy1, string sumBy2, int rowIndex, IEnumerable<DatovyRiadok> data)
        {
            var sumRow = new ZostavaDataRow(ZostavaRowType.Sum);
            sumRow.AddColumn(new ZostavaDataColumn(rowIndex, 1, $"{sumBy2}"));

            var columnIndex = 0;
            foreach (var c in (from clmn in data.First().Columns where clmn.IsVisible select clmn))
            {
                if (c.ContainsAggregateFunction())
                {
                    sumRow.AddColumn(new ZostavaDataColumn(rowIndex, columnIndex,
                        (from d in data
                         where d.Values.ElementAt(0).ToString().Equals(sumBy1) &&
                              d.Values.ElementAt(1).ToString().Equals(sumBy2)
                         select Convert.ToDecimal(d.Values[columnIndex])).Sum()));
                }
                columnIndex++;
            }

            return sumRow;
        }

        private static ZostavaDataRow CreateSum(string sumBy, int rowIndex, IEnumerable<DatovyRiadok> data, bool zostava3 = false)
        {
            // zmenil sa nadpis treba zosumovat
            var sumRow = new ZostavaDataRow(ZostavaRowType.Sum);

            sumRow.AddColumn(new ZostavaDataColumn(rowIndex, 1, (string.IsNullOrEmpty(sumBy) ? "Celkovo" : "Spolu " + sumBy)));

            var columnIndex = zostava3 ? 3 : 2;
            foreach (var c in (from clmn in data.First().Columns where clmn.IsVisible && clmn.ContainsAggregateFunction() select clmn))
            {
                if (c.ContainsAggregateFunction())
                {
                    if (string.IsNullOrEmpty(sumBy))
                    {
                        sumRow.AddColumn(new ZostavaDataColumn(rowIndex, columnIndex,
                            (from d in data
                             select Decimal.Parse(d.Values[columnIndex].ToString())).Sum()));
                    }
                    else
                    {
                        sumRow.AddColumn(new ZostavaDataColumn(rowIndex, columnIndex,
                            (from d in data
                             where d.Values.First().ToString() == sumBy
                             select Decimal.Parse(d.Values[columnIndex].ToString())).Sum()));
                    }
                }
                columnIndex++;
            }

            return sumRow;
        }

        #endregion
    }
}