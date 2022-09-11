namespace cvti.data.Core
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IDeletable
    {
        SqlCommand GenerateDeleteCommand(SqlConnection conn, string db);
    }
}
