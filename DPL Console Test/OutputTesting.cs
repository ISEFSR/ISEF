namespace DPL_Console_Test
{
    using cvti.data;
    using System;
    using System.Linq;

    public class OutputTesting : ITestingUnit
    {
        private ISEFDataManager _manager;
        public OutputTesting(ISEFDataManager manager)
        {
            _manager = manager;

            _manager.Output.DataConverted += Output_DataConverted;
            _manager.Output.DataObtained += Output_DataObtained;
            _manager.Output.DataStylingDone += Output_DataStylingDone;
            _manager.Output.ZostavaExported += Output_ZostavaExported;
        }

        private void Output_ZostavaExported(object sender, cvti.data.Output.Zostava e)
        {
            Program.WriteWarning("\tExport successfull: " + e.Nazov);
        }

        private void Output_DataStylingDone(object sender, cvti.data.Output.Zostava e)
        {
            Program.WriteWarning("\tData styling done: " + e.Nazov);
        }

        private void Output_DataObtained(object sender, cvti.data.Output.Zostava e)
        {
            Program.WriteWarning("\tData obtained: " + e.Nazov);
        }

        private void Output_DataConverted(object sender, cvti.data.Output.Zostava e)
        {
            Program.WriteWarning("\tData converted: " + e.Nazov);
        }

        public void Start()
        {
            Program.WriteLine($"Output testing starting...");
            _manager.CoreFiles.Zostavy.ReadData();

            if (_manager.CoreFiles.Zostavy.Values.Count() == 0)
            {
                Program.WriteError("Zostavy pre testovacie účely neexistujú...");
                return;
            }

            Program.WriteLine("Testujem zostavy pre rok 2020");
            foreach (var zostava in from z in _manager.CoreFiles.Zostavy.Values where z.Okruh == cvti.data.Output.OkruhZostavyEnum.CEL select z)
            {
                try
                {
                    Program.WriteLine($"Vytváram zostavu: {zostava.Nazov}");
                    _manager.Output.ExportZostavaSafe(zostava, 2020, true, null).GetAwaiter().GetResult();
                    //Program.WriteLine($"Zostavu {zostava.Nazov} úspešne vytvorená");
                }
                catch (Exception ex)
                {
                    Program.WriteException(ex);
                }
            }

            Program.WriteLine($"Output testing finished...");
        }
    }
}
