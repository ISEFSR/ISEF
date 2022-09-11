namespace DPL_Console_Test
{
    using cvti.data;
    using cvti.data.Core;
    using cvti.data.Files;
    using System;

    class Program
    {
        static MSSQLServer _server;
        static ISEFDataManager _manager;

        static string _dataFolder;

        static void Main(string[] args)
        {
            Console.Title = "ISEF Manager testing 1.0";
            ShowTitle();
            // License pre EPPlus
            OfficeOpenXml.ExcelPackage.LicenseContext =
                OfficeOpenXml.LicenseContext.NonCommercial;

            // Folder pre data a vystupne subory
            _dataFolder = System.IO.Path.Combine(Environment.CurrentDirectory, "Data");

            // Kontrola ci existuje 
            // v pripade ak nie tak ho vytvorim
            if (!System.IO.Directory.Exists(_dataFolder))
            {
                WriteLine($"Directory does not exists. Createting directory: {_dataFolder}");
                System.IO.Directory.CreateDirectory(_dataFolder);
            }

            // Generovanie defaultnych hlaviciek, zostav, prikazov a podmienok
            var hlavicky = HlavickyManagerJson.GenerateDefaultHlavicky(_dataFolder);

            ZostavyManagerJson.GenerateDefaultFile(hlavicky, _dataFolder);
            CommandsManagerJson.GenerateDefaultFile(_dataFolder);
            ConditionsManagerJson.GenerateDefaulFile(_dataFolder);

            // Inicializacia MSSQL serveru
            // isef data manageru zodpovedneho za manipulaciu s databazou
            _server = new MSSQLServer("(localdb)\\MSSQLlocaldb", "ISEF_TEST");
            _manager = new ISEFDataManager(_server, _dataFolder, _dataFolder, _dataFolder, _dataFolder, null);

            if (_server.CanConnect())
            {
                WriteLine("Server and manager successfully initialized...");
                var testing = true;

                while (testing)
                {
                    Console.Clear();
                    ShowTitle();
                    ShowTestTmenu();
                    var input = Console.ReadLine();
                    switch (input)
                    {
                        case "1":
                            var outputTest = new OutputTesting(_manager);
                            outputTest.Start();
                            break;
                        case "2":
                            testing = false;
                            break;
                        default:
                            Console.WriteLine("Please choose valid option...");
                            break;
                    }
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Can't connect to MSSQL server and database {0}", _server);
                Console.WriteLine("Please check the connection and try again...");
            }

            // Ukoncenie testovacieho programu
            WriteLine("Press any key to quit...");
            Console.ReadKey();
        }
        
        internal static void ShowTitle()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("");
            Console.WriteLine(@"________ __________.____      ___________              __               ");
            Console.WriteLine(@"\______ \\______   \    |     \__    ___/___   _______/  |_     CVTI SR ");
            Console.WriteLine(@" |    |  \|     ___/    |       |    |_/ __ \ /  ___/\   __\    2020    ");
            Console.WriteLine(@" |    `   \    |   |    |___    |    |\  ___/ \___ \  |  |              ");
            Console.WriteLine(@"/_______  /____|   |_______ \   |____| \___  >____  > |__|              ");
            Console.WriteLine(@"        \/                 \/              \/     \/                    ");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.White;
        }

        internal static void ShowTestTmenu()
        {
            Console.WriteLine("Please press key followed by 'ENTER' for specific test");
            Console.WriteLine("1. For report testing...");
            Console.WriteLine("2. For quit the test program...");
        }

        internal static void WriteLine(string text)
        {
            Console.WriteLine($"[{DateTime.Now.ToString("hh:mm:ss")}] {text}");
        }

        internal static void WriteException(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(ex.GetType().ToString());
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);

            Console.ForegroundColor = ConsoleColor.White;
        }

        internal static void WriteError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(message);

            Console.ForegroundColor = ConsoleColor.White;
        }

        internal static void WriteWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            Console.WriteLine(message);

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
