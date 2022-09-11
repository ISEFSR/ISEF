namespace cvti.data.Tables
{
    using cvti.data.Core;
    using System.Data;
    using System.Data.SqlClient;

    public abstract class AnalytickaEvidenciaRiadok : CiselnikRiadok
    {
        public AnalytickaEvidenciaRiadok(IDataRecord record)
            : base(record)
        {

        }

        public AnalytickaEvidenciaRiadok()
            : base()
        {

        }

        [TableColumnAttribute("Rok")]
        public int Rok { get; set; }
        [TableColumnAttribute("Kod")]
        public string Kod { get; set; }
        [TableColumnAttribute("Nazov")]
        public string Nazov { get; set; }
        [TableColumnAttribute("Popis")]
        public string Popis { get; set; }

        public override string ToString() => $"[{Kod}] {Nazov}";

        protected abstract string GetTableName();

        public override SqlCommand GenerateUpdateCommand(SqlConnection conn, string db)
            => CreateCommand(conn, db, GetTableName());

        private SqlCommand CreateCommand(SqlConnection conn, string db, string table)
            => AddCondition( AddSets( new SqlCommand($"update {db}.{table}", conn)  ) );

        protected virtual SqlCommand AddSets(SqlCommand command)
        {
            command.CommandText += " set Nazov=@nazov, Popis=@popis";
            command.Parameters.AddWithValue("@nazov", Nazov);
            command.Parameters.AddWithValue("@popis", Popis);
            return command;
        }

        protected virtual SqlCommand AddCondition(SqlCommand command)
        {
            command.CommandText += " where rok=@rok and kod=@kod";
            command.Parameters.AddWithValue("@rok", Rok);
            command.Parameters.AddWithValue("@kod", Kod);
            return command;
        }
    }
}
