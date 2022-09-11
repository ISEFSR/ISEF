using cvti.data.Tables;
using System.Data;
using System.Data.SqlClient;

namespace cvti.data.Tables
{
    public abstract class ZdrojRiadok : AnalytickaEvidenciaRiadok
    {
        public ZdrojRiadok(IDataRecord record)
            : base(record)
        {

        }
    }
}