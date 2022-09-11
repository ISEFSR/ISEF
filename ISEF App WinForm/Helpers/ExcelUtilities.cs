namespace cvti.isef.winformapp.Helpers
{
    using OfficeOpenXml;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.IO;
    using System.Windows.Forms;

    public class ExcelUtilities
    {
        /// <summary>
        /// Gets all the worksheets from workbook specified by its file path
        /// </summary>
        /// <param name="filePath">File path of the workbook</param>
        /// <returns></returns>
        public static string[] GetWorksheets(string filePath)
        {
            using (var fileInfo = new ExcelPackage(new FileInfo(filePath)))
            {
                var sheets = new List<string>();

                var wb = fileInfo.Workbook;

                if (wb is null || wb.Worksheets is null) return new string[] { };          
                foreach (var ws in wb.Worksheets)
                {
                    Debug.WriteLine(ws);
                    sheets.Add(ws.Name);
                }


                return sheets.ToArray();
            }
        }

        public static void ExportDataGridData(DataGridView data, string filePath)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets[0];

                AddHeader(data, worksheet);

                AddData(data, worksheet);

                StyleWorksheet(worksheet);

                package.SaveAs(new FileInfo(filePath));
            }
        }

        private  static void AddHeader(DataGridView dataGrid, ExcelWorksheet worksheet)
        {
            const int Row = 1;

            var clmn = 1;
            foreach (DataGridViewColumn c in dataGrid.Columns)
            {
                worksheet.Cells[Row, clmn++].Value = c.Name;
            }
        }

        private static void AddData(DataGridView dataGrid, ExcelWorksheet worksheet)
        {
            var row = 2;
            var clmn = 1;

            foreach (DataGridViewRow r in dataGrid.Rows)
            {
                foreach (DataGridViewCell c in r.Cells)
                {
                    worksheet.Cells[row, clmn++].Value = c.Value;
                }
                row++;
            }
        }

        private static void StyleWorksheet(ExcelWorksheet worksheet)
        {

        }
    }
}