using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cvti.data.Conditions
{
    public class EmptyCondition
        : Condition
    {
        public EmptyCondition(string name) : base(name)
        {
            Wrap = true;
            Value = null;
        }

        public override object Value { get; }

        public override Condition CloneMe(bool deep = true)
        {
            var b = new EmptyCondition(this.ConditionName);

            if (deep)
                foreach (var c in InnerConditions)
                    b.AddCondition(c.Item1.CloneMe(true), c.Item2);

            return b;
        }

        protected override string GetThisCondition()
        {
            var conditionString = "(";

            if (InnerConditions.Any())
            {
                foreach (var c in InnerConditions)
                    conditionString += c.Item2 + " " + c.Item1.GetConditionString();
            }

            return conditionString + ")";
        }
    }
}
