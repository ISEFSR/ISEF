namespace cvti.data.Output
{
    public class ZostavaDataColumn
    {
        public ZostavaDataColumn(int row, int column, object value)
        {
            RowIndex = row;
            ColumnIndex = column;
            Value = value;
        }
        
        public int RowIndex { get; }
        public int ColumnIndex { get; }
        public object Value { get; }
    }
}