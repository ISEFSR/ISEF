namespace cvti.data
{
    using cvti.data.Output;
    using cvti.data.Core;
    using cvti.data.Tables;
    using OfficeOpenXml;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using cvti.data.Columns;

    public static class ExcelUtilities
    {
        public static void ExportCiselnikToExcel(Dictionary<string, IEnumerable<CiselnikRiadok>> data, string filePath)
        {
            using (var package = new ExcelPackage(new System.IO.FileInfo(filePath)))
            {
                foreach (var d in data)
                {
                    var ws = package.Workbook.Worksheets.Add(d.Key);
                    FillWorksheet(d.Value, ws);
                }

                package.Save();
            }
        }

        public static void ExportCiselnikToExcel(Dictionary<string, IEnumerable<TableRow>> data, string filePath)
        {
            using (var package = new ExcelPackage(new System.IO.FileInfo(filePath)))
            {
                foreach (var d in data)
                {
                    var ws = package.Workbook.Worksheets.Add(d.Key);
                    FillWorksheet(d.Value, ws);
                }

                package.Save();
            }
        }

        public static void ExportCiselnikToExcel(IEnumerable<TableRow> data, string filePath)
        {
            using (var package = new ExcelPackage(new System.IO.FileInfo(filePath)))
            {
                var ws = package.Workbook.Worksheets.Add("data");
                FillWorksheet(data, ws);
                // ulozenie
                package.Save();
            }
        }

        public static void ExportCiselnikToExcel(IEnumerable<CiselnikRiadok> data, string filePath)
        {
            using (var package = new ExcelPackage(new System.IO.FileInfo(filePath)))
            {
                var ws = package.Workbook.Worksheets.Add("data");
                FillWorksheet(data, ws);
                // ulozenie
                package.Save();
            }
        }

        private static void FillWorksheet(IEnumerable<TableRow> data, ExcelWorksheet ws)
        {
            int row = 1, column = 1;
            // Hlavicka
            foreach (var p in data.ElementAt(0).GetType().GetProperties())
                ws.Cells[row, column++].Value = p.Name;

            row++;
            // data
            foreach (var r in data)
            {
                column = 1;
                foreach (var p in r.GetType().GetProperties())
                    ws.Cells[row, column++].Value = p.GetValue(r);

                row++;
            }
        }

        public static void ExportDataPreZostavu(Zostava zostava, IEnumerable<DatovyRiadok> data, string saveAs, bool skipZero = true)
        {
            // Testovacia metoda na vytvaranie vystupov
            var dir = System.IO.Path.GetDirectoryName(saveAs);
            if (!System.IO.Directory.Exists(dir))
                System.IO.Directory.CreateDirectory(dir);

            using (var package = new ExcelPackage(new System.IO.FileInfo(zostava.Hlavicka.FilePath)))
            {
                // get the data worksheet
                var dataWorksheet = package.Workbook.Worksheets[0];

                // get first data row 
                var excelRow = zostava.Hlavicka.Data.RiadkyHlavicka + 1;

                // remove all rows after first data row
                dataWorksheet.Cells[$"A{excelRow}:X100"].Delete(eShiftTypeDelete.Up);

                // 
                var sumBy = new Dictionary<Column, Tuple<string, int>>();

                var nadpisyRanges = new List<string>();
                var sumyRange = new List<string>();
                var dataRange = new List<string>();



                //for (var rowIndex = 0; rowIndex < data.Count(); rowIndex++)
                //{
                //    var dataRow = data.ElementAt(rowIndex);
                //    var fullZeroRow = true;
                //    var excelColumn = 1;

                //    // Vpisujem stlpce 
                //    // Ak je potrebne zosumovat vpisem sumu
                //    // Ak je potrebne vpisat nadpis vpisem nadpis
                //    for (var clmnIndex = 0; clmnIndex < dataRow.Columns.Count(); clmnIndex++)
                //    {
                //        var dataClmn = dataRow.Columns.ElementAt(clmnIndex);
                //        var content = dataRow.Values[clmnIndex].ToString();

                //        if (!dataClmn.IsVisible)
                //            continue;

                //        // Stlpec neobsahuje agregatnu funkciu
                //        // teda nema v sebe zosumovane hodnoty
                //        if (!dataClmn.ContainsAggregateFunction() && zostava.Stlpce[clmnIndex].Subtotal)
                //        {
                //            if (rowIndex == 0)
                //            { 
                //                sumBy.Add(dataClmn, new Tuple<string, int>( content , clmnIndex));
                //                if (zostava.Stlpce[clmnIndex].HasTitle)
                //                {
                //                    dataWorksheet.Cells[excelRow, 1].Value = content;
                //                    excelRow++;
                //                    excelColumn = 1;
                //                }
                //            }
                //            else
                //            {
                //                var previousValue = sumBy[dataClmn];
                //                if (previousValue.Item1 != content)
                //                {
                //                    InsertSumRow(dataWorksheet, excelRow, dataRow.Columns, data, clmnIndex, previousValue.Item1, sumBy);
                //                    excelRow++;
                //                    sumBy[dataClmn] = new Tuple<string, int>( content, previousValue.Item2);
                //                }
                //            }
                //        }

                //        else //(dataClmn.ContainsAggregateFunction())
                //                {
                //            if (decimal.Parse(content) != 0)
                //                fullZeroRow = false;

                //            dataWorksheet.Cells[excelRow, excelColumn].Value = content;

                //            if (zostava.Stlpce[clmnIndex].MoveRight)
                //            {
                //                excelColumn++;
                //            }
                //            else
                //            {
                //                excelRow++;
                //                excelColumn = 1;
                //            }
                //        }
                //        }

                //    if (skipZero && fullZeroRow)
                //        continue;

                //    excelRow++;
                //}

                package.SaveAs(new System.IO.FileInfo(saveAs));
            }
        }

        public static void ExportDataPreHlavicku(Hlavicka hlavicka, IEnumerable<DatovyRiadok> data, string saveAs, bool skipZero = true)
        {
            // Testovacia metoda na vytvaranie vystupov
            var dir = System.IO.Path.GetDirectoryName(saveAs);
            if (!System.IO.Directory.Exists(dir))
                System.IO.Directory.CreateDirectory(dir);

            using (var package = new ExcelPackage(new System.IO.FileInfo(hlavicka.FilePath)))
            {
                // get the data worksheet
                var dataWorksheet = package.Workbook.Worksheets[0];

                // get first data row 
                var row = hlavicka.Data.RiadkyHlavicka + 1;

                // remove all rows after first data row
                dataWorksheet.Cells[$"A{row}:X100"].Delete(eShiftTypeDelete.Up);

                // Should i sum 
                var tempValues = new Dictionary<Column, string>();                
                for (var rowIndex = 0; rowIndex < data.Count(); rowIndex++)
                {
                    var dataRow = data.ElementAt(rowIndex);
                    var fullZeroRow = true;
                    var excelColumn = 1;
                    for (var clmnIndex = 0; clmnIndex < dataRow.Columns.Count(); clmnIndex++)
                    {
                        var dataClmn = dataRow.Columns.ElementAt(clmnIndex);
                        var content = dataRow.Values[clmnIndex].ToString();
                        if (dataClmn.IsVisible && !dataClmn.ContainsAggregateFunction())
                        {
                            if (rowIndex == 0)
                            {
                                tempValues.Add(dataClmn, content);
                            }
                            else
                            {
                                var previousValue = tempValues[dataClmn];
                                if (previousValue != content)
                                {
                                    //InsertSumRow(dataWorksheet, row, dataRow.Columns, data, clmnIndex, previousValue);
                                    //row++;
                                    tempValues[dataClmn] = content;
                                }
                            }
                        }

                        if (dataClmn.ContainsAggregateFunction() && dataClmn.IsVisible)
                            if (decimal.Parse(content) != 0)
                                fullZeroRow = false;

                        if (dataClmn.IsVisible)
                            dataWorksheet.Cells[row, excelColumn++].Value = content;
                    }
                    if (skipZero && fullZeroRow)
                        continue;

                    row++;
                }

                package.SaveAs(new System.IO.FileInfo(saveAs));
            }
        }

        private static void InsertSumRow(ExcelWorksheet ws, int row, IEnumerable<Column> columns, IEnumerable<DatovyRiadok> data, int clmn, string value, Dictionary<Column, Tuple<string,int>> tempValues)
        {

            var excelColumn = 1;
            ws.Cells[row, excelColumn].Value = "Suma za " + value;
            for (var i = 0; i < columns.Count(); i++)
            {
                var c = columns.ElementAt(i);
                if (c.ContainsAggregateFunction())
                {
                    var tempData = data;
                    foreach (var tv in tempValues)
                    {
                        tempData = from d in tempData where d.Values[tv.Value.Item2].ToString() == tv.Value.Item1 select d;
                    }

                    ws.Cells[row, excelColumn].Value = (from d in tempData select Decimal.Parse(d.Values[i].ToString())).Sum();
                }
                excelColumn++;
            }
        }
    }
}
