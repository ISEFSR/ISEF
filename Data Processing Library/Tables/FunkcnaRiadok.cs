namespace cvti.data.Tables
{
    using System.Data;
    using System.Data.SqlClient;

    public abstract class FunkcnaRiadok : AnalytickaEvidenciaRiadok
    {
        public FunkcnaRiadok(IDataRecord record)
            : base(record)
        {

        }
    }
}
