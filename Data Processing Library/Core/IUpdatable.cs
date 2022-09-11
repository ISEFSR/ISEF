namespace cvti.data.Core
{
    using System.Data.SqlClient;

    public interface IUpdatable
    {
        SqlCommand GenerateUpdateCommand(SqlConnection conn, string db);
    }
}
