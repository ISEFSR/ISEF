namespace cvti.data.Output
{
    using cvti.data.Core;
    using cvti.data.Exceptions;
    using OfficeOpenXml;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using cvti.data.Enums;
    using System.Drawing;
    using cvti.data.Conditions;

    /// <summary>
    /// Manager zodpovedny za vytvaranie vystupnych suborov aka zostav
    /// </summary>
    public class OutputManager
    {
        #region Public Events

        /// <summary>
        /// Event signalizujuci ziskanie dat pre zostavu ako <see cref="Zostava"/>
        /// </summary>
        public event EventHandler<Zostava> DataObtained;

        /// <summary>
        /// Event signalizujuci skonvertoavnie dat pre zostavu ako <see cref="Zostava"/>
        /// </summary>
        public event EventHandler<Zostava> DataConverted;

        /// <summary>
        /// Event sinalizujici uspesne nastylovanie vysutpneho xlsx suboru ako <see cref="Zostava"/>
        /// </summary>
        public event EventHandler<Zostava> DataStylingDone;

        /// <summary>
        /// Event signalizujuci uspesny export do vystupneho xslx suboru pre zostavu ako <see cref="Zostava"/>
        /// </summary>
        public event EventHandler<Zostava> ZostavaExported;

        /// <summary>
        /// Event signalizujici nepodareny export udajov ako <see cref="Tuple{T1, T2}"/> kde T1 je <see cref="Zostava"/> a T2 je <see cref="ExcelAddress"/>
        /// </summary>
        public event EventHandler<Tuple<Zostava, Exception>> CreationFailed;

        #endregion

        #region Private Variables And Constructors

        private readonly LogManager _log;

        private readonly ExportManager _export;

        private readonly MSSQLDataManager _mssqlManager;

        private readonly Image _logo;

        /// <summary>
        /// Inicializuje novu instanciu pre OutputManager zodpovedny za vytvaranie vystupnych suborov
        /// </summary>
        /// <param name="log">Manager zodpovedny za vytvaranie logov ako <see cref="LogManager"/></param>
        /// <param name="export">Manager zodpovedny za ziskavanie dat ako <see cref="ExportManager"/></param>
        /// <param name="mssqlManager">Manager zodpovedny za ziskavanie udajov z MSSQL serveru ako <see cref="MSSQLDataManager"/></param>
        /// <param name="outpuDirectory">Vystupny adresar kam sa budu ukladat vytvorene zostavy</param>
        /// <param name="logo">Logo pre vystupne zostavy ako <see cref="Image"/></param>
        public OutputManager(LogManager log, ExportManager export, MSSQLDataManager mssqlManager, string outpuDirectory, Image logo)
        {
            _logo = logo;
            _log = log;
            _export = export;
            _mssqlManager = mssqlManager;

            if (!Directory.Exists(outpuDirectory))
                throw new DirectoryNotFoundException(outpuDirectory);

            OutputDirectory = outpuDirectory;
        }

        #endregion

        #region Public Methods And Properties

        /// <summary>
        /// Vrati adresar, kam sa ulozia vysutpne subory
        /// </summary>
        /// <value>
        /// Adresar kam sa ulozia vystupne subory
        /// </value>
        public string OutputDirectory { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stupen"></param>
        /// <param name="rok"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public async Task ExportRawData(InputType stupen, int rok, string filePath)
        {
            const int COUNT = 1000;

            var dataCount = await _mssqlManager.GetDataCountAsync(stupen, rok);
            var location = 0;

            var excelRow = 1;
            using (var excelFile = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = excelFile.Workbook.Worksheets.Add("data");
                while (location < dataCount)
                {
                    var data = await _mssqlManager.GetDataAsync(rok, stupen, location, COUNT);

                    foreach (var d in data)
                    {
                        worksheet.Cells[excelRow, 1].Value = rok.ToString();
                        worksheet.Cells[excelRow, 2].Value = stupen.ToString();
                        worksheet.Cells[excelRow, 3].Value = d.Organizacia.ToString();
                        worksheet.Cells[excelRow, 4].Value = d.Funkcna.ToString();
                        worksheet.Cells[excelRow, 5].Value = d.Ekonomicka.ToString();
                        worksheet.Cells[excelRow, 6].Value = d.Zdroj.ToString();
                        worksheet.Cells[excelRow, 7].Value = d.Program.ToString();
                        worksheet.Cells[excelRow, 8].Value = d.Rozpp.ToString();
                        worksheet.Cells[excelRow, 9].Value = d.Rozpu.ToString();
                        worksheet.Cells[excelRow, 10].Value = d.Skut.ToString();
                        worksheet.Cells[excelRow, 11].Value = d.Ucet.ToString();
                        worksheet.Cells[excelRow, 12].Value = d.Druh_rozpp.ToString();

                        excelRow++;
                    }

                    location += COUNT;
                }

                excelFile.Save();
            }
        }

        /// <summary>
        /// Epxortuje vybranu zostavu pre vybrany rok
        /// </summary>
        /// <param name="zostava">Vybrana zostava ako <see cref="Zostava"/></param>
        /// <param name="rok">Rok pre ktory exporetujem zostavu</param>
        /// <remarks>
        /// Nazov vystupnej zostavy je generovany automaticky. Cesta k suboru zavisi od roku okruhu a outputdirectory
        /// </remarks>
        /// <exception cref="CouldntCreateDirectoryException">Nepodarilo sa mi vytvorit adresar pre vystupny subor</exception>
        public async Task<string> ExportZostavaSafe(Zostava zostava, int rok, bool hlavicka, Condition condition)
        {
            try
            {
                // Adresar pre vystupne subory pre vybrany rok
                // Ak nedokazem adresar vytvorit hodim exception
                var finalDirectory = Path.Combine(OutputDirectory, rok.ToString(), zostava.Okruh.ToString());
                if (!Directory.Exists(finalDirectory))
                {
                    try
                    {
                        Directory.CreateDirectory(finalDirectory);
                        _log?.LogNewMessageSafe(Enums.LogMessageType.BackEndMessage, "OutputManager.ExportZostava", $"Output directory successfully created: {finalDirectory}");
                    }
                    catch
                    {
                        _log?.LogNewMessageSafe(Enums.LogMessageType.BackEndError, "OutputManager.ExportZostava", "Couldn't create output directory...");
                        throw new CouldntCreateDirectoryException();
                    }
                }

                // Nazov vystupnej XLSX zostavy a
                // Cesta k vystupnemu XLSX suboru
                var zostavaOutputFileName = $"z{zostava.Okruh}{zostava.Hlavicka.Name}.xlsx";
                var finalPath = Path.Combine(finalDirectory, zostavaOutputFileName);

                // Ziskam raw data pre zostavu 
                var zostavaData = await _export.ExportData(zostava, rok, true, condition);
                DataObtained?.Invoke(this, zostava);

                // Pridam transfery ak je to potrebne
                List<decimal> transfer = null;
                List<decimal> withoutTransfer = null;
                if (zostava.OdpocetTransferov)
                {
                    transfer = new List<decimal>(await _mssqlManager.VratSumuTransfery(zostava, rok));
                    withoutTransfer = new List<decimal>(await _mssqlManager.VratSumuBezTransferov(zostava, rok));
                }

                // Skonvertujem data do finalnej podoby
                var convertedData = ZostavyConverter.ConvertToZostavaRows(zostava, zostavaData, transfer, withoutTransfer, hlavicka);
                DataConverted?.Invoke(this, zostava);

                // Pustim sa do vytvarania vystupneho XLSX suboru
                using (var package = new ExcelPackage(new FileInfo(zostava.Hlavicka.FilePath)))
                {
                    // get the data worksheet
                    var dataWorksheet = package.Workbook.Worksheets[0];

                    // Odstranim 500 riadkov za prvym riadkom pod hlavickou
                    dataWorksheet.Cells[$"A{zostava.Hlavicka.Data.RiadkyHlavicka + 1}:{zostava.Hlavicka.Data.PoslednyStlpec}42500"].Delete(eShiftTypeDelete.Up);

                    // sem ulozim rozsahy kde sa nachadzaju data, hlavicky, nadpisy a sumy
                    List<string> dataRanges = new List<string>(),
                                 sumRanges = new List<string>(),
                                 titleRanges = new List<string>(),
                                 headerRanges = new List<string>();

                    // rozsah dat napocitavam 
                    // aby som to nemusel menit po riadkoch
                    var dataRange = string.Empty;
                    foreach (var r in convertedData)
                    {
                        // Importujem hodnoty pre riadok
                        foreach (var c in r.Columns)
                            dataWorksheet.Cells[c.RowIndex, c.ColumnIndex].Value = c.Value;

                        // Na zaklade typu riadku pridam do rozsah do spracovacej tabulky
                        switch (r.RowType)
                        {
                            case ZostavaRowType.Data:
                                // Zacnem novy data range ak neni ziaden nacaty
                                if (string.IsNullOrEmpty(dataRange))
                                    dataRange = $"A{r.RowIndex}:";

                                break;
                            case ZostavaRowType.Sum:
                                sumRanges.Add($"A{r.RowIndex}:{zostava.Hlavicka.Data.PoslednyStlpec}{r.RowIndex}");
                                if (!string.IsNullOrEmpty(dataRange))
                                {
                                    dataRange += $"{zostava.Hlavicka.Data.PoslednyStlpec}{r.RowIndex-1}";
                                    dataRanges.Add(dataRange);
                                    dataRange = string.Empty;
                                }
                                break;
                            case ZostavaRowType.Title:
                                titleRanges.Add($"A{r.RowIndex}:{zostava.Hlavicka.Data.PoslednyStlpec}{r.RowIndex}");
                                if (!string.IsNullOrEmpty(dataRange))
                                {
                                    dataRange += $"{zostava.Hlavicka.Data.PoslednyStlpec}{r.RowIndex-1}";
                                    dataRanges.Add(dataRange);
                                    dataRange = string.Empty;
                                }
                                break;
                            case ZostavaRowType.Header:
                                headerRanges.Add($"A{r.RowIndex}");
                                if (!string.IsNullOrEmpty(dataRange))
                                {
                                    dataRange += $"{zostava.Hlavicka.Data.PoslednyStlpec}{r.RowIndex-1}";
                                    dataRanges.Add(dataRange);
                                    dataRange = string.Empty;
                                }
                                break;
                            default:
                                break;
                        }
                    }

                    // Doplnenie nadpisov do hlavicky na provom riadku
                    // TODO: zatial sa replacuje len text '{rok}' do buducna sa to kludne moze rozisirt
                    dataWorksheet.Cells[zostava.Hlavicka.Data.LeftTitleRange].Value = zostava.LeftTitle.Replace("{rok}", rok.ToString());
                    dataWorksheet.Cells[zostava.Hlavicka.Data.RightTitleRange].Value = zostava.RightTitle.Replace("{rok}", rok.ToString());

                    // Nastylovanie stlpcov v ktorych sa nachadzaju sumy
                    // tak aby nezobrazovali desatinne mieste a tak aby zonbrazovali oddelovac tisicov
                    //dataWorksheet.Cells[$"B:{zostava.Hlavicka.Data.PoslednyStlpec}"].Style.Numberformat.Format = "#,##0";

                    // Nastylovanie hlaviciek
                    // teda lepsie povedane nakopirovanie hlavicky z prveho riadku tam kam je treba
                    StyleHeaders(dataWorksheet, zostava, headerRanges.ToArray());

                    // Nastylovanie sumacnych riadkov
                    StyleSums(dataWorksheet, sumRanges.ToArray());

                    // Natylovanie nadpisovych riadkov
                    StyleTitles(dataWorksheet, titleRanges.ToArray());

                    // Nastylovanie datovych rozsahovb
                    StyleData(dataWorksheet, dataRanges.ToArray());

                    //// Nastavenie tlacoveho rozsahu na vyplnenen udaje
                    dataWorksheet.PrinterSettings.PrintArea =
                        dataWorksheet.Cells[$"A1:{zostava.Hlavicka.Data.PoslednyStlpec}{convertedData.Last().RowIndex}"];

                    foreach (var r in convertedData)
                        if (r.RowType == ZostavaRowType.Header)
                            dataWorksheet.Row(r.RowIndex - 1).PageBreak = true;

                    DataStylingDone?.Invoke(this, zostava);

                    // Ulozenie vystupnej zostavy
                    package.SaveAs(new FileInfo(finalPath));
                    // Notifikovanie o ulozeni vystupnej zostavy
                    ZostavaExported?.Invoke(this, zostava);

                    return finalPath;
                }
            }
            catch (Exception ex)
            {
                CreationFailed?.Invoke(this, new Tuple<Zostava, Exception>(zostava, ex));
                return string.Empty;
            }
        }

        #endregion

        #region Private Methods

        private void StyleHeaders(ExcelWorksheet worksheet, Zostava zostava, string[] ranges)
        {
            // Header ranges su vo formate "A1" teda ako StlpecRiadok
            foreach (var r in ranges)
            {
                // prvy znak je vzdy A (ani by tam nemusel byt) takze beriem vsetko po tom a prekonvertujem na integer
                var row = int.Parse(new String(r.Skip(1).ToArray()));

                // Skopirujem hlavicku na pozdovane miesto
                worksheet.Cells[$"A1:{zostava.Hlavicka.Data.PoslednyStlpec}{zostava.Hlavicka.Data.RiadkyHlavicka}"].Copy(
                    worksheet.Cells[r]);

                // V pripade ak je aj logo pridam lgoo
                if (_logo != null)
                {
                    var picture = worksheet.Drawings.AddPicture(r, _logo);
                    picture.SetSize(89, 89);
                    picture.SetPosition(int.Parse(new String(r.Where(Char.IsDigit).ToArray())) -1, 5, 0, 5);
                }

                // nakoniec pridam oramovanie (bolo ako shape) 
                worksheet.Cells[$"B{row + 1}:{zostava.Hlavicka.Data.PoslednyStlpec}{row + 1}"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            }
        }

        private void StyleData(ExcelWorksheet worksheet, string[] ranges)
        {
            foreach (var r in ranges)
            {
                worksheet.Cells[r].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                worksheet.Cells[r].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            }
        }

        private void StyleSums(ExcelWorksheet worksheet, string[] ranges)
        {
            foreach (var r in ranges)
            {
                worksheet.Cells[r].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                worksheet.Cells[r].Style.Font.Bold = true;
            }
        }

        private void StyleTitles(ExcelWorksheet worksheet, string[] ranges)
        {
            foreach (var r in ranges)
            {
                worksheet.Cells[r].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                worksheet.Cells[r].Style.Font.Bold = true;
            }
        }

        #endregion
    }
}
