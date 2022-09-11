namespace cvti.data.Functions
{
    using cvti.data.Conditions;
    using Newtonsoft.Json;

    public class ConditionalSum : Function
    {
        public ConditionalSum(Condition cond)
        {
            Condition = cond ?? throw new System.ArgumentNullException(nameof(cond));
        }

        [JsonConstructor]
        private ConditionalSum()
        {

        }

        public Condition Condition { get; set; }
        public override bool IsNumeric { get; } = true;
        public override bool IsAggregate { get; } = true;
        public override string Apply(string stlpec) => $"SUM(CASE WHEN {Condition.GetConditionString(true)} THEN {stlpec} ELSE 0 END)";

        public override Function CloneMe()
        {
            return new ConditionalSum(Condition.CloneMe());
        }

        public override string ToString() => $"SUMIF";
    }
}
