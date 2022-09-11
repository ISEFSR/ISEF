namespace cvti.data.Conditions
{
    public class CompoundCondition
        : Condition
    {
        public CompoundCondition(string name, Condition condition) : base(name)
        {
            if (Wrap)
                condition.Wrap = true;
            Value = condition;
        }

        public override object Value { get; }

        public override Condition CloneMe(bool deep = true)
            => (Value as Condition).CloneMe(true);

        protected override string GetThisCondition()
            => (Value as Condition).GetConditionString();
    }
}
