namespace cvti.data.Functions
{
    public class Sum : Function
    {
        public override bool IsAggregate { get; } = true;
        public override bool IsNumeric { get; } = true;
        public override string Apply(string stlpec) => $"SUM({stlpec})";

        public override Function CloneMe()
        {
            return new Sum();
        }

        public override string ToString() => "SUM";
    }
}
