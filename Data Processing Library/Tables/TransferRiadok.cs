namespace cvti.data.Tables
{
    using cvti.data.Conditions;
    using cvti.data.Core;
    using cvti.data.Views;
    using System.Data;
    using System.Data.SqlClient;

    public class TransferRiadok : TableRow, IUpdatable, IInsertable, IDeletable
    {
        public static string TableName = "dbo.transfer";

        public static SqlCommand GetSelectCommand(SqlConnection conn, string db)
            => DatabaseUtilities.GenerateSelectCommand<TransferRiadok>(conn, db, TableName);

        public static SqlCommand GetSelectCommand(SqlConnection conn, string db, int rok)
        {
            var cmd = DatabaseUtilities.GenerateSelectCommand<TransferRiadok>(conn, db, TableName);
            cmd.CommandText += " where Rok=@rok";
            cmd.Parameters.AddWithValue("@rok", rok);
            return cmd;
        }

        internal TransferRiadok() { }

        public TransferRiadok(IDataRecord record)
            : base(record)
        {

        }

        [TableColumnAttribute("Rok")]
        public int Rok { get; set; }
        [TableColumnAttribute("From")]
        public string FromStupen { get; set; }
        [TableColumnAttribute("To")]
        public string ToStupen { get; set; }
        [TableColumnAttribute("Polozka")]
        public string Polozka { get; set; }

        public override string ToString()
        {
            return Polozka;
        }

        public Condition GetCondition()
        {
            return new Equals(string.Empty, AssuView.VratStlpec(Enums.AssuViewAvailableColumns.EKod6), Polozka).
                AddCondition(new Equals(string.Empty, AssuView.VratStlpec(Enums.AssuViewAvailableColumns.StupenKod), FromStupen), Enums.ConditionOperator.And);
        }

        public SqlCommand GenerateInsertCommand(SqlConnection conn, string db)
        {
            var cmd = new SqlCommand($"insert into {db}.{TableName} (Rok, [From], [To], Polozka) values (@rok, @from, @to, @polozka)", conn);
            cmd.Parameters.AddWithValue("@rok", Rok);
            cmd.Parameters.AddWithValue("@from", FromStupen);
            cmd.Parameters.AddWithValue("@to", ToStupen);
            cmd.Parameters.AddWithValue("@polozka", Polozka);
            return cmd;
        }

        public SqlCommand GenerateUpdateCommand(SqlConnection conn, string db)
        {
            var cmd = new SqlCommand($"update {db}.{TableName} set [From]=@from, [To]=@to where Rok=@rok and Polozka=@polozka", conn);
            cmd.Parameters.AddWithValue("@rok", Rok);
            cmd.Parameters.AddWithValue("@from", FromStupen);
            cmd.Parameters.AddWithValue("@to", ToStupen);
            cmd.Parameters.AddWithValue("@polozka", Polozka);
            return cmd;
        }

        public SqlCommand GenerateDeleteCommand(SqlConnection conn, string db)
        {
            var cmd = new SqlCommand($"delete from {db}.{TableName} where Rok=@rok and Polozka=@polozka", conn);
            cmd.Parameters.AddWithValue("@rok", Rok);
            cmd.Parameters.AddWithValue("@polozka", Polozka);
            return cmd;
        }
    }
}
