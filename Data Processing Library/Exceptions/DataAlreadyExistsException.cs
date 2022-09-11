namespace cvti.data.Exceptions
{
    public class DataAlreadyExistsException : System.Exception
    {
        public DataAlreadyExistsException(string message, int rok, char stupen) 
            : base(message)
        {
            Rok = rok;
            Stupen = stupen;
        }

        public int Rok { get; private set; }
        public char Stupen { get; private set; }
    }
}
