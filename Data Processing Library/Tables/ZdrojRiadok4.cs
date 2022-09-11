namespace cvti.data.Tables
{
    using cvti.data.Core;
    using System.Data;
    using System.Data.SqlClient;

    public class ZdrojRiadok4 : ZdrojRiadok
    {
        public static string TableName = "[dbo].[cis_zk4]";
        public static SqlCommand GetSelectCommand(SqlConnection conn, string db, int rok)
        {
            var cmd = new SqlCommand($"select [Rok], [Kod], [Zk3], [Nazov], [Popis], [Pom_Kod1], [Pom_Kod2], [Pom_Kod3], [Pom_Kod4], [Pom_Kod5], [Kde] from [{db}].{TableName} where [Rok]=@rok", conn);
            cmd.Parameters.AddWithValue("@rok", rok);
            return cmd;
        }

        protected override string GetTableName() => TableName;

        public ZdrojRiadok4(IDataRecord data)
            : base(data)
        {

        }

        [TableColumnAttribute("Zk3")]
        public string Zk3 { get; set; }
        [TableColumnAttribute("Pom_Kod1")]
        public string PomKod1 { get; set; }
        [TableColumnAttribute("Pom_Kod2")]
        public string PomKod2 { get; set; }
        [TableColumnAttribute("Pom_Kod3")]
        public string PomKod3 { get; set; }
        [TableColumnAttribute("Pom_Kod4")]
        public string PomKod4 { get; set; }
        [TableColumnAttribute("Pom_Kod5")]
        public string PomKod5 { get; set; }
        [TableColumnAttribute("Kde")]
        public string KodKde { get; set; }

        protected override SqlCommand AddSets(SqlCommand command)
        {
            base.AddSets(command);
            command.CommandText += ", Pom_Kod1=@pk1";
            command.CommandText += ", Pom_Kod2=@pk2";
            command.CommandText += ", Pom_Kod3=@pk3";
            command.CommandText += ", Pom_Kod4=@pk4";
            command.CommandText += ", Pom_Kod5=@pk5";
            command.CommandText += ", Kde=@kde";

            command.Parameters.AddWithValue("@pk1", PomKod1);
            command.Parameters.AddWithValue("@pk2", PomKod2);
            command.Parameters.AddWithValue("@pk3", PomKod3);
            command.Parameters.AddWithValue("@pk4", PomKod4);
            command.Parameters.AddWithValue("@pk5", PomKod5);
            command.Parameters.AddWithValue("@kde", KodKde);

            return command;
        }
    }
}
