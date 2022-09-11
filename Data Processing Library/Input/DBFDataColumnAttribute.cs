namespace cvti.data.Input
{
    using System;

    public class DBFDataColumnAttribute : Attribute
    {
        public DBFDataColumnAttribute(string name, int index, Type type)
        {
            ColumnName = name;
            ColumnType = type;
            Index = index;
        }

        public string ColumnName { get; set; }
        public Type ColumnType { get; set; }
        public int Index { get; set; }
    }
}
