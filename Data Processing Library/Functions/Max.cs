namespace cvti.data.Functions
{
    public class Max : Function
    {
        public override bool IsAggregate { get; } = true;
        public override bool IsNumeric { get; } = true;
        public override string Apply(string stlpec) => $"Max({stlpec})";

        public override Function CloneMe()
        {
            return new Max();
        }

        public override string ToString() => $"MAX";
    }
}
