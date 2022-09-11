namespace cvti.data.Core
{
    using System;
    using System.Data;
    using System.Linq;

    public abstract class TableRow
    {
        public TableRow(IDataRecord record)
        {
            // get all class properties, public private protected, internal...
            var properties = GetType().GetProperties(System.Reflection.BindingFlags.Public |
                 System.Reflection.BindingFlags.Instance |
                 System.Reflection.BindingFlags.NonPublic);

            foreach (var property in properties)
            {
                // check if property has TableColumnAttribute on it
                if (!(property.GetCustomAttributes(typeof(TableColumnAttribute), true).FirstOrDefault() is TableColumnAttribute attribute))
                    continue;

                var columnName = string.IsNullOrWhiteSpace(attribute.ColumnName) ? property.Name : attribute.ColumnName;

                var ordinal = record.GetOrdinal(columnName);

                if (property.PropertyType == typeof(string))
                {
                    property.SetValue(this, record.GetStringSafe(ordinal));     
                }
                else if (property.PropertyType == typeof(int))
                {
                    property.SetValue(this, record.GetInt32(ordinal));
                }
                else if (property.PropertyType == typeof(bool))
                {
                    property.SetValue(this, record.GetBoolean(ordinal));
                }
                else if (property.PropertyType == typeof(short))
                {
                    property.SetValue(this, record.GetInt16(ordinal));
                }
                else if (property.PropertyType == typeof(DateTime))
                {
                    property.SetValue(this, record.GetDateTime(ordinal));
                }
                else if (property.PropertyType == typeof(byte))
                {
                    property.SetValue(this, record.GetByte(ordinal));
                }
                else if (property.PropertyType == typeof(decimal))
                {
                    property.SetValue(this, record.GetDecimal(ordinal));
                }
                else
                {
                    throw new NotImplementedException($"{property.PropertyType} not implemented...");
                }
            }
        }

        protected TableRow()
        {

        }
    }
}
