namespace cvti.data.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class InvalidWorksheetException : Exception
    {
        public InvalidWorksheetException(IEnumerable<string> worksheets)
            : base($"Vo worksheetoch je neplatná štruktúra hlavičky: " + string.Join(",", worksheets))
        {

        }
    }
}
