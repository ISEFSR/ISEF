namespace cvti.data.Core
{
    using System.Data.SqlClient;

    public interface IInsertable
    {
        SqlCommand GenerateInsertCommand(SqlConnection conn, string db);
    }
}
