namespace cvti.data.Functions
{
    public class Substring : Function
    {
        public Substring(int from, int length)
        {
            From = from;
            Length = length;
        }

        public override bool IsAggregate { get; } = false;
        public override bool IsNumeric { get; } = false;

        public int From { get; set; }
        public int Length { get; set; }

        public override string Apply(string stlpec) => $"SUBSTR({stlpec}, {From}, {Length})";

        public override Function CloneMe()
        {
            return new Substring(From, Length);
        }

        public override string ToString() => $"SUBSTRING";
    }
}
