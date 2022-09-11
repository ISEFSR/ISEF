using System.Data;
using System.Data.SqlClient;

namespace cvti.data.Tables
{
    public abstract class EkonomickaRiadok : AnalytickaEvidenciaRiadok
    {
        public EkonomickaRiadok(IDataRecord record)
            : base(record)
        {

        }

    }
}