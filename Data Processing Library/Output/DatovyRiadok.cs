namespace cvti.data.Output
{
    using cvti.data.Columns;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public class DatovyRiadok
    {
        public DatovyRiadok(IDataRecord data, IEnumerable<Column> columns)
        {
            Values = new object[data.FieldCount];
            Columns = columns;
            data.GetValues(Values);
        }

        public IEnumerable<Column> Columns { get; private set; }

        public object[] Values { get; }

        public override string ToString() => string.Join("", from d in Values select d.ToString().PadRight(20, ' '));
    }
}
