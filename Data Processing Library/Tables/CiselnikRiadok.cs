namespace cvti.data.Tables
{
    using cvti.data.Core;
    using System.Data;
    using System.Data.SqlClient;

    public abstract class CiselnikRiadok : TableRow, IUpdatable
    {
        public CiselnikRiadok(IDataRecord record)
            : base(record)
        {
            
        }

        public CiselnikRiadok()
        {

        }

        public abstract SqlCommand GenerateUpdateCommand(SqlConnection conn, string db);
    }
}
