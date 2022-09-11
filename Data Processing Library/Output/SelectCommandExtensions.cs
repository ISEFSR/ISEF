namespace cvti.data.Output
{
    using cvti.data.Columns;
    using cvti.data.Conditions;
    using cvti.data.Core;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;

    internal static class SelectCommandExtensions
    {
        internal static SqlCommand AddFromAndWhere(this SqlCommand commnad, Condition cnd)
        {
            commnad.CommandText += $" FROM (SELECT {SelectMesta()} union SELECT {SelectOstatne()}) as o {GenerateJoins()}";
            if (cnd != null)
                commnad.CommandText += $" WHERE {cnd.GetConditionString()} ";

            return commnad;
        }
        internal static SqlCommand AddGroupBy(this SqlCommand command, IEnumerable<Column> clmns)
        {
            if (clmns.Any(s => s.ContainsAggregateFunction()) && clmns.Any(s => !s.ContainsAggregateFunction()))
                command.CommandText += $" GROUP BY {string.Join(",", clmns.Where(s => !s.ContainsAggregateFunction()))}";

            return command;
        }
        internal static SqlCommand AddOrderBy(this SqlCommand command, IEnumerable<Column> clmns)
        {
            if (clmns.Any(s => !s.ContainsAggregateFunction()))
                command.CommandText += $" ORDER BY {string.Join(",", clmns.Where(s => !s.ContainsAggregateFunction()))}";
            return command;
        }

        private static string GenerateJoins() =>
            $@" left join		dbo.vi_organizacie org on o.Ico = org.OrgIco
                left join		dbo.vi_funkcna fk on o.Fk = fk.FKod5 and o.Rok = fk.FRok
                left join		dbo.vi_ekonomicka ek on o.Ek = ek.EKod6 and o.Rok = ek.ERok
                left join		dbo.vi_zdroj zk on o.Zk = zk.ZKod4 and o.Rok = zk.ZRok
                left join		dbo.vi_program pk on o.Pk = pk.PKod7 and o.Rok = pk.PRok";

        private static string SelectMesta() =>
            $@"     			Rok,
				                Ico,
				                Fk,
				                Ek,
				                Zk,
				                Pk,
				                Ucet,
				                Druh_rozp,
				                Skut,
				                Rozpp,
				                Rozpu
             from			dbo.mao";
        private static string SelectOstatne() =>
            $@"     			Rok,
				                Ico,
				                fk,
				                ek,
				                zk,
				                pk,
				                Ucet,
				                Druh_rozp,
				                Skut,
				                Rozpp,
				                Rozpu
                from			dbo.ostatne";
    }
}
