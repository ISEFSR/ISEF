using cvti.data.Columns;

namespace cvti.data.Conditions
{
    /// <summary>
    /// SQL 
    /// </summary>
    public class NotEquals : Condition
    {
        public NotEquals(string name, Column clmn, object value)
            : base(name)
        {
            Column = clmn;
            Value = value;
        }

        public Column Column { get; set; }
        public override object Value { get; }

        protected override string GetThisCondition()
        {
            var character = Column.IsNumeric ? "" : "'";
            return $"{Column} <> {character}{Value}{character}";
        }

        public override Condition CloneMe(bool deep)
        {
            var cnd = new Equals(ConditionName, Column, Value)
            {
                Negate = Negate
            };

            cnd.Wrap = Wrap;

            if (deep)
                foreach (var c in InnerConditions)
                    cnd.AddCondition(c.Item1.CloneMe(), c.Item2);

            return cnd;
        }
    }
}
