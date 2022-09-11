namespace cvti.data.Functions
{
    public class Count : Function
    {
        public override bool IsAggregate => true;
        public override bool IsNumeric => true;

        public override string Apply(string stlpec) => $"COUNT({stlpec})";

        public override Function CloneMe()
        {
            return new Count();
        }

        public override string ToString() => $"COUNT";
    }
}
