using System.Data;

namespace cvti.data.Tables
{
    public abstract class ProgramRiadok : AnalytickaEvidenciaRiadok
    {
        public ProgramRiadok(IDataRecord record)
            : base(record)
        {

        }
    }
}
