using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cvti.data.core.Input
{
    public static class VysokeSkolyDataManager
    {
        public static List<OthersDataRow> ReadFile(FileInfo file)
        {

            if (!file.Exists) throw new FileNotFoundException();
            if (!file.Extension.ToLower().Equals(".xlsx")) throw new ArgumentException();

            var data = new List<OthersDataRow>();

            // read file with VVS

            return data;
        }
    }
}
