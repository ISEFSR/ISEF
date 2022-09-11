namespace cvti.data
{
    using cvti.data.Columns;
    using cvti.data.Enums;
    using cvti.data.Functions;
    using cvti.data.Views;

    public static class Extensions
    {
        public static Column GetColumn(this SumColumn column)
        {
            Column columnObject = null;
            switch (column)
            {
                case SumColumn.Skutocnost:
                    columnObject = AssuView.VratStlpec(Enums.AssuViewAvailableColumns.Skut);
                    break;
                case SumColumn.SchvalenyRozpocet:
                    columnObject = AssuView.VratStlpec(Enums.AssuViewAvailableColumns.Rozpp);
                    break;
                case SumColumn.UpravenyRozpocet:
                    columnObject = AssuView.VratStlpec(Enums.AssuViewAvailableColumns.Rozpu);
                    break;
                default:
                    break;
            }

            columnObject?.AddFunction(new Sum());
            return columnObject;
        }
    }
}
