namespace cvti.data.Input
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ExcelDataColumnAttribute : Attribute
    {
        public ExcelDataColumnAttribute(string name, int[] index, Type type)
        {
            ColumnName = name;
            ColumnType = type;
            Index = index;
        }

        public string ColumnName { get; set; }
        public Type ColumnType { get; set; }
        public int[] Index { get; set; }
    }
}