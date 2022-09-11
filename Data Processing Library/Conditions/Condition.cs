namespace cvti.data.Conditions
{
    using cvti.data.Enums;
    using cvti.data.Files;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// SQL Podmienka
    /// </summary>
    /// <remarks>
    /// Base class pre SQL podmienku
    /// </remarks>
    [Serializable]
    public abstract class Condition : ISerializableJson
    {
        private const string NegateText = "NOT";

        protected List<Tuple<Condition, ConditionOperator>> _innerConditions =
            new List<Tuple<Condition, ConditionOperator>>();

        public Condition(string name)
        {
            ConditionName = name;
        }

        public string ConditionName { get; set; }

        public bool Negate { get; set; } = false;

        public bool Wrap { get; set; } = false;

        public abstract object Value { get; }

        public IEnumerable<Tuple<Condition, ConditionOperator>> InnerConditions { get { return _innerConditions; } }

        public string Code { get => ConditionName; }

        public Condition AddCondition(Condition condition, ConditionOperator conditionOperator)
        {
            _innerConditions.Add(new Tuple<Condition, ConditionOperator>(condition, conditionOperator));
            ConditionAdded?.Invoke(this, condition);
            return this;
        }

        public Condition InsertConditionAt(Condition condition, ConditionOperator conditionOperator, int index)
        {
            _innerConditions.Insert(index, new Tuple<Condition, ConditionOperator>(condition, conditionOperator));
            ConditionAdded?.Invoke(this, condition);
            return this;
        }
        public Condition RemoveAt(int index)
        {
            var cnd = _innerConditions[index].Item1;
            _innerConditions.RemoveAt(index);
            ConditionRemoved?.Invoke(this, cnd);
            return this;
        }

        public string GetConditionString(bool deep = true)
        {
            var conditionBuilder = new StringBuilder();
            if (Negate)
                conditionBuilder.Append($"{NegateText} ");

            if (Wrap)
                conditionBuilder.Append("(");

            conditionBuilder.Append(GetThisCondition());

            if (deep && _innerConditions.Any())
            {
                foreach (var innerCondition in _innerConditions)
                {
                    conditionBuilder.Append($" {innerCondition.Item2.ToString()} {innerCondition.Item1.GetConditionString(true)}");
                }
            }

            if (Wrap)
                conditionBuilder.Append(")");

            return conditionBuilder.ToString();
        }

        protected abstract string GetThisCondition();

        public override string ToString() => ConditionName; 

        /// <summary>
        /// Naklonuje podmienku 
        /// </summary>
        /// <returns>Nova naklonovana podmienka ako <see cref="Condition"/></returns>
        public object Clone()
        {
            return CloneMe();
        }

        /// <summary>
        /// Naklonuje podmienku a vrati novu instanciu identicku s touto
        /// </summary>
        /// <param name="deep">Urcuje ci sa naklonuju aj pod-podmienky</param>
        /// <returns>Nova naklonovana podmienka ako <see cref="Condition"/></returns>
        public abstract Condition CloneMe(bool deep = true);

        /// <summary>
        /// Signalizuje pridanie novej podmienky pod vybranu ako <see cref="Condition"/>
        /// </summary>
        public event EventHandler<Condition> ConditionAdded;

        /// <summary>
        /// Signalizuje odstranenie podmienky spod vybranej podmienky ako <see cref="Condition"/>
        /// </summary>
        public event EventHandler<Condition> ConditionRemoved;
    }
}
