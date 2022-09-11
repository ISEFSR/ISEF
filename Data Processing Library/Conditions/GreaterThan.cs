using cvti.data.Columns;

namespace cvti.data.Conditions
{
    public class GreaterThan : Condition
    {
        public GreaterThan(string name, Column clmn, object value)
            : base(name)
        {
            Column = clmn;
            Value = value;
        }

        public Column Column { get; set; }
        public override object Value { get; }

        public override Condition CloneMe(bool deep)
        {
            var cnd = new GreaterThan(ConditionName, Column, Value)
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
            return $"{Column} > {character}{Value}{character}";
        }
    }
}
