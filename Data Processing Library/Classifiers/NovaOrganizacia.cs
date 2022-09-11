namespace cvti.data.Classifiers
{
    public class NovaOrganizacia
    {
        public NovaOrganizacia(string ico)
        {
            ICO = ico;
        }
        public string ICO { get; private set; }

        public bool IsChanged { get; private set; }

        private string _nazov;
        public string Nazov { get => _nazov; set { _nazov = value; IsChanged = true; } }

        private string _ulica;
        public string Ulica { get => _ulica; set { _ulica = value; IsChanged = true; } }

        private int _obec = 999999;
        public int KodObce { get => _obec; set { _obec = value; IsChanged = true; } }
    }
}
