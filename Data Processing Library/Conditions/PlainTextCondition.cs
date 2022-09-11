namespace cvti.data.Conditions
{
    public class PlainTextCondition : Condition
    {
        public PlainTextCondition(string conditionName, string conditionText)
            : base(conditionName)
        {
            Value = conditionText;
        }

        public override object Value { get; }

        public override Condition CloneMe(bool deep = true)
        { 
            var clone = new PlainTextCondition(ConditionName, Value.ToString());
            clone.Wrap = Wrap;
            return clone;
        }

        protected override string GetThisCondition()
            => Value.ToString();
    }
}
