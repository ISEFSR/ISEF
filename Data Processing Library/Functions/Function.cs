namespace cvti.data.Functions
{
    using System;

    public abstract class Function : ICloneable
    {
        public abstract bool IsAggregate { get; }
        public abstract bool IsNumeric { get; }

        public abstract string Apply(string stlpec);

        public object Clone()
        {
            return CloneMe();
        }

        public abstract Function CloneMe();
    }
}
