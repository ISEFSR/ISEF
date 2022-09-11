namespace cvti.data.Functions
{
    public class Min : Function
    {
        public override bool IsAggregate { get; } = true;
        public override bool IsNumeric { get; } = true;
        public override string Apply(string stlpec) => $"MIN({stlpec})";

        public override Function CloneMe()
        {
            return new Min();
        }

        public override string ToString() => $"MIN";
    }
}
