namespace cvti.data.Columns
{
    using cvti.data.Views;
    using cvti.data.Enums;
    using Newtonsoft.Json;
    using System;
    using cvti.data.Functions;

    /// <summary>
    /// Stlpec pochadzajuci z pohladu Assu
    /// </summary>
    /// <remarks>
    /// Inicializacia pomocou <see cref="AssuViewAvailableColumns"/>
    /// </remarks>
    public class AssuViewColumn : Column
    {
        #region Constructors

        [JsonConstructor]
        private AssuViewColumn()
        {

        }

        internal AssuViewColumn(AssuViewAvailableColumns clmn, string name, bool isNumeric)
        {
            IsNumeric = isNumeric;
            ColumnName = name;
            ColumnAlias = ColumnName;
            Column = clmn;
        }

        internal AssuViewColumn(AssuViewAvailableColumns clmn, string name, bool isNumeric, string alias)
        {
            IsNumeric = isNumeric;
            ColumnAlias = alias;
            ColumnName = name;
            Column = clmn;
        }

        #endregion

        #region Public Properties

        public AssuViewAvailableColumns Column { get; private set; }

        public override string TableName => AssuView.ViewName;

        #endregion

        #region Public Methods

        public override Column CloneMe(bool deep = true)
        {
            var assuColumn = new AssuViewColumn(Column, ColumnName, IsNumeric)
            {
                ColumnAlias = ColumnAlias
            };

            if (deep)
                foreach (var f in Functions)
                    assuColumn.AddFunction(f.Clone() as Function);

            return assuColumn;
        }

        #endregion
    }
}
