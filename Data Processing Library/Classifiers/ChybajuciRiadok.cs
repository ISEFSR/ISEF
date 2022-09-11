namespace cvti.data.Classifiers
{
    public class ChybajuciRiadok
    {
        public ChybajuciRiadok(int rok, string kod, string table)
        {
            Rok = rok;
            Kod = kod;
            TableName = table;
        }

        public string TableName { get; private set; }
        public int Rok { get; private set; }
        public string Kod { get; private set; }
    }
}
