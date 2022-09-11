namespace cvti.data.Output
{
    using System.Collections.Generic;
    using System.Linq;

    public class ZostavaDataRow
    {
        private readonly List<ZostavaDataColumn> _columns
            = new List<ZostavaDataColumn>();

        public ZostavaDataRow(ZostavaRowType type)
        {
            RowType = type;
        }

        /// <summary>
        /// Vrati typ riadku
        /// </summary>
        /// <value>
        /// Typ riadku ako <see cref="ZostavaRowType"/>
        /// </value>
        /// <remarks>
        /// Na zaklade typu vie manager ako nastylovat dany riadok vo vystupnom XLSX subore
        /// </remarks>
        public ZostavaRowType RowType { get; }

        /// <summary>
        /// Vrati index riadku v exceli, 1 based
        /// </summary>
        /// <value>
        /// Index riadku vo vystupnom exceli, 1 based ako <see cref="System.Int32"/>
        /// </value>
        public int RowIndex { get { return _columns.FirstOrDefault()?.RowIndex ?? -1; } }

        public IList<ZostavaDataColumn> Columns { get => _columns; }

        public void AddColumn(ZostavaDataColumn column)
        {
            _columns.Add(column);
        }
    }
}