using System.Collections;
using System.Collections.Generic;

namespace cvti.isef.winformapp.Controls.Import
{
    /// <summary>
    ///  Importny XLSX subor
    /// </summary>
    public class ExcelImportFile
    {
        public ExcelImportFile(string filePath, int clmn, int row, int headerRow, IEnumerable<string> worksheets)
        {
            FilePath = filePath;
            Row = row;
            HeaderRow = headerRow;
            Column = clmn;
            Worksheets.AddRange(worksheets);
        }

        /// <summary>
        /// Cesta ku vstupnemu XLSX suboru
        /// </summary>
        public string FilePath { get; private set; }

        /// <summary>
        /// Stlpec index stlpca na ktorom sa zacinaju data
        /// </summary>
        public int Column { get; private set; }

        /// <summary>
        /// Index riadoku na ktorom sa zacinaju data
        /// </summary>
        public int Row { get; private set; }

        /// <summary>
        /// index riadka s hlavickou 
        /// </summary>
        public int HeaderRow { get; private set; }

        /// <summary>
        /// Worksheety, ktore spracuvam 
        /// </summary>
        public List<string> Worksheets { get; private set; } =
            new List<string>();
    }
}
