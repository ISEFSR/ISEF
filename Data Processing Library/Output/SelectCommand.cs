namespace cvti.data.Output
{
    using cvti.data.Conditions;
    using cvti.data.Views;
    using cvti.data.Enums;
    using cvti.data.Tables;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Linq;
    using cvti.data.Columns;
    using cvti.data.Files;

    public class SelectCommand : ICloneable, ISerializableJson
    {
        private readonly List<Column> _stlpce = new List<Column>();

        public SelectCommand(string commandName)
        {
            CommandName = commandName;
        }

        public SelectCommand(string commandName, IEnumerable<Column> stlpce)
            : this(commandName)
        {
            _stlpce.AddRange(stlpce);

            for (var i = 0; i < _stlpce.Count(); i++)
                if (_stlpce[i].ColumnAlias == string.Empty)
                    _stlpce[i].ColumnAlias = $"Column{i}";
        }

        public SelectCommand(string commandName, IEnumerable<Column> columns, Condition cond) 
            : this (commandName, columns)
        {
            CommandCondition = cond;
        }

        [JsonConstructor]
        private SelectCommand()
        {

        }

        public string CommandName { get; set; }
        public Condition CommandCondition { get; set; }
        public IEnumerable<Column> Columns { get { return _stlpce; } }

        public string Code { get => CommandName; }

        public void ClearColumns()
            => _stlpce.Clear();

        public SelectCommand AddColumn(Column s) { if (s.ColumnAlias == string.Empty) s.ColumnAlias = GetNextAlias(); _stlpce.Add(s); return this; }
        public SelectCommand InsertColumn(Column s, int index) { _stlpce.Insert(index, s); return this; }
        public SelectCommand RemoveColumn(Column s) { _stlpce.Remove(s); return this; }
        public SelectCommand RemoveColumnAt(int index) { _stlpce.RemoveAt(index); return this; }

        private string GetNextAlias() 
        {
            var i = 0;
            var alias = $"Column{i}";
            do
            {
                i++;
                alias = $"Column{i}";
            } while ((from s in _stlpce where s.ColumnAlias == alias select s).Any());
            return alias;
        }

        public SqlCommand GenerateCommand(SqlConnection conn = null, int timeout = 240)
        {
            var cmd = CreateCommand(conn).AddFromAndWhere(CommandCondition).AddGroupBy(Columns).AddOrderBy(Columns);

            cmd.CommandTimeout = timeout;

            Debug.WriteLine(cmd.CommandText);

            return cmd;
        }

        public SqlCommand GenerateCommand(int rok, SqlConnection conn = null, int timeout = 240)
        {
            var conditionRok = new Equals("tmpRok", AssuView.VratStlpec(AssuViewAvailableColumns.Rok), rok);
            if (CommandCondition != null)
                conditionRok.AddCondition(CommandCondition, ConditionOperator.And);
            
            var cmd = CreateCommand(conn).AddFromAndWhere(CommandCondition).AddGroupBy(Columns).AddOrderBy(Columns);

            cmd.CommandTimeout = timeout;

            Debug.WriteLine(cmd.CommandText);

            return cmd;
        }

        public SqlCommand GenerateCommand(SegmentRiadok segment, int rok, SqlConnection conn = null, int timeout = 240)
        {
            var cnd = new Equals("tmpRok", AssuView.VratStlpec(AssuViewAvailableColumns.Rok), rok)
                .AddCondition(new Equals("tmpSegm", AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod), segment.Kod), ConditionOperator.And);

            if (CommandCondition != null)
                cnd.AddCondition(CommandCondition, ConditionOperator.And);

            var cmd = CreateCommand(conn).AddFromAndWhere(CommandCondition).AddGroupBy(Columns).AddOrderBy(Columns);

            cmd.CommandTimeout = timeout;

            Debug.WriteLine(cmd.CommandText);

            return cmd;
        }

        private SqlCommand CreateCommand(SqlConnection conn = null, int timeout = 240)
        {
            var cmd = new SqlCommand($"SELECT {string.Join(",", from s in _stlpce select s)} ");
            if (conn != null)
                cmd.Connection = conn;

            cmd.CommandTimeout = timeout;

            Debug.WriteLine(cmd.CommandText);

            return cmd;
        }

        public string PeekCommandText()
        {
            using (var cmd = GenerateCommand())
                return cmd.CommandText;
        }

        public override string ToString()
        {
            return CommandName;
        }

        public object Clone()
        {
            return CloneMe();
        }

        public SelectCommand CloneMe()
        {
            var cmd = new SelectCommand();

            if (CommandCondition != null)
                cmd.CommandCondition = CommandCondition.CloneMe(true);

            foreach (var clmn in Columns)
                cmd.AddColumn(clmn.CloneMe(true));

            return cmd;
        }
    }
}