namespace cvti.data.Classifiers
{
    public class AnalytickaEvidenciaNovaHodnota
    {
        private string _popis;
        private string _skrateny;
        private bool _changed;

        public AnalytickaEvidenciaNovaHodnota(int rok, string kod, string info)
        {
            Rok = rok;
            Kod = kod;
            Info = info;
        }

        public int Rok { get; private set; }
        public string Kod { get; private set; }
        public string Info { get; private set; }

        public string Popis { get => _popis; set { _popis = value; _changed = true; } }
        public string Skrateny { get => _skrateny; set { _skrateny = value; _changed = true; } }

        public bool IsChanged { get => _changed; }
    }
}
