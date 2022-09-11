namespace cvti.data.Core
{
    using System;

    public class TableColumnAttribute : Attribute
    {
        public TableColumnAttribute()
        {
            ColumnName = null;
        }

        public TableColumnAttribute(string columnName)
        {
            ColumnName = columnName;
        }

        public TableColumnAttribute(string columnName, bool nullable)
            : this (columnName)
        {
            Nullable = nullable;
        }

        public string ColumnName { get; private set; }

        public bool Nullable { get; set; } = false;
    }
}
