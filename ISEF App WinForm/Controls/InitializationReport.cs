using System;

namespace cvti.isef.winformapp.Controls
{
    public class InitializationReport
    {
        public InitializationReport(bool canConnect, bool dbExists, bool dbValid, Exception ex)
        {
            CanConnect = canConnect;
            DatabaseExists = dbExists;
            DatabaseValid = dbValid;
            Exception = ex;
        }

        public bool CanConnect { get; }
        public bool DatabaseExists { get; }
        public bool DatabaseValid { get; }
        public Exception  Exception { get; }
    }
}
