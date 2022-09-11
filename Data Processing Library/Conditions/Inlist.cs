namespace cvti.data.Conditions
{
    using cvti.data.Columns;
    using System.Collections.Generic;
    using System.Linq;

    public class Inlist : Condition
    {
        private readonly object[] _values;
        public Inlist(string name, Column clmn, object[] values)
            : base(name)
        {
            Column = clmn;
            _values = values;
        }

        public Column Column { get; set; }
        public IEnumerable<object> Values { get { return _values; } }

        public override object Value => string.Join(";", from v in Values select v.ToString());

        public override Condition CloneMe(bool deep = true)
        {
            var cnd = new Inlist(ConditionName, Column, _values)
            {
                Negate = Negate
            };

            cnd.Wrap = Wrap;

            if (deep)
                foreach (var c in InnerConditions)
                    cnd.AddCondition(c.Item1.CloneMe(), c.Item2);

            return cnd;
        }

        protected override string GetThisCondition()
        {
            var character = Column.IsNumeric ? "" : "'";
            return $"{Column} IN ({string.Join(",", from v in Values select character + v + character)})";
        }
    }
}
