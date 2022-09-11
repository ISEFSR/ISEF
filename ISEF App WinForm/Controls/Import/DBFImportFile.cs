namespace cvti.isef.winformapp.Controls.Import
{
    /// <summary>
    /// Importny DBF subor
    /// </summary>
    public class DBFImportFile
    {
        public DBFImportFile(string filePath)
        {
            FilePath = filePath;
        }

        public string FilePath { get; private set; }

        public override string ToString() => FilePath;
    }
}