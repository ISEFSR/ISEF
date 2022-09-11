namespace cvti.data.Functions
{
    public class Round : Function
    {
        public Round(int places)
        {
            Places = places;
        }

        public int Places { get; }
        public override bool IsNumeric { get; } = true;
        public override bool IsAggregate { get; } = false;

        public override string Apply(string stlpec) => $"ROUND({stlpec}, {Places})";

        public override Function CloneMe()
        {
            return new Round(Places);
        }

        public override string ToString() => "ROUND";
    }
}
