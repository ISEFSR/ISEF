namespace cvti.data.Files
{
    using cvti.data.Core;
    using cvti.data.Conditions;
    using cvti.data.Views;
    using cvti.data.Enums;
    using cvti.data.Output;
    
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;

    public class ZostavyManagerJson : FileManagerJson<Zostava>
    {
        public static readonly string FileName = "zostavy.json";

        private readonly HlavickyManagerJson _hlavickyManager;

        internal ZostavyManagerJson(LogManager log, string directory, string hlavickyDirectory)
            : base(log, directory, FileName)
        {
            _hlavickyManager = new HlavickyManagerJson(log, directory, hlavickyDirectory);
        }

        protected override IEnumerable<Zostava> GetDefault()
        {
            _hlavickyManager.ReadData();

            return ZostavyManagerJson.GenerujDefaultne(_hlavickyManager.Values);
        }

        public static void GenerateDefaultFile(IEnumerable<Hlavicka> hlavicky, string directory)
        {
            var filePath = System.IO.Path.Combine(directory, FileName);
            var data = GenerujDefaultne(hlavicky);
            System.IO.File.WriteAllText(filePath, JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            }));
        }

        public void UpdateHlavicky(HlavickyManagerJson hlavicky)
        {
            foreach (var z in Values)
            {
                var h = hlavicky.GetValue(z.Hlavicka.Code);
                if (h != null)
                    z.Hlavicka = h;
            }
        }

        public static IEnumerable<Zostava> GenerujDefaultne(IEnumerable<Hlavicka> hlavicky)
        {
            var zostavy = new List<Zostava>();
            zostavy.AddRange(GenerujPrijmoveZostavy(hlavicky));
            zostavy.AddRange(GenerujVydavkoveZostavy(hlavicky));
            zostavy.AddRange(GenerujTransferoveZostavy(hlavicky));
            return zostavy;
        }

        private static IEnumerable<Zostava> GenerujPrijmoveZostavy(IEnumerable<Hlavicka> hlavicky)
        {
            // Vygeneruje prijmove zostavy
            // Pre prijmove zostavy je potrebne aby existovali iste hlavicky
            // Prijmove zostavy sa robia iba pre rozpoctove ogranizacie
            var zostavy = new List<Zostava>();
            var rozpoctoveOrganizacie = new Equals("ro", AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod), "06");

            var hlavickaVsetky = (from h in hlavicky where h.Name == "hlppalloro" select h).First();
            var hlavickaNedanove = (from h in hlavicky where h.Name == "hlpnalloro" select h).First();
            var hlavickaGranty = (from h in hlavicky where h.Name == "hlpgalloro" select h).First();
            var hlavickaTransfery = (from h in hlavicky where h.Name == "hlptalloro" select h).First();

            foreach (var okruh in (OkruhZostavyEnum[])Enum.GetValues(typeof(OkruhZostavyEnum)))
            {

                var vsektyPrijmy = new Zostava(hlavickaVsetky, rozpoctoveOrganizacie, okruh, $"Všetky príjmy podľa {okruh} podľa okruhov a rozp. organizácií", "Príjmy " + okruh.ToString() + " v r. {rok}", "Všetky zdroje - príjmy", string.Empty);
                var nedanovePrijmy = new Zostava(hlavickaNedanove, rozpoctoveOrganizacie, okruh, $"Nedaňové príjmy podľa {okruh} podľa okruhov a rozp. organizácií", "Nedaňové príjmy " + okruh.ToString() + " v r. {rok}", "Všeteky zdroje - nedaňové príjmy", string.Empty);
                var grantyPrijmy = new Zostava(hlavickaGranty, rozpoctoveOrganizacie, okruh, $"Granty podľa {okruh} podľa okruhov a rozp. organizácií", "Granty " + okruh.ToString() + " v r. {rok}", "Všetky zdroje - granty", string.Empty);
                var transferyPrijmy = new Zostava(hlavickaTransfery, rozpoctoveOrganizacie, okruh, $"Transfery podľa {okruh} podľa okruhov a rozp. organizácií", "Transfery " + okruh.ToString() + " v r. {rok}", "Všetky zdroje - transfery", string.Empty);

                zostavy.Add(vsektyPrijmy);
                zostavy.Add(nedanovePrijmy);
                zostavy.Add(grantyPrijmy);
                zostavy.Add(transferyPrijmy);
            }

            return zostavy;
        }

        private static IEnumerable<Zostava> GenerujVydavkoveZostavy(IEnumerable<Hlavicka> hlavicky)
        {
            // Vygeneruje defaultne vydavkove zostavy
            // Pre vydavkove zostavy je potrebne aby existovali isrte hlavicky 
            var zostavy = new List<Zostava>();
            var rozpoctoveOrganizacie = new Equals("ro", AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod), "06");

            Func<OkruhZostavyEnum, string> getCode = (o) =>
            {
                switch (o)
                {
                    case OkruhZostavyEnum.MSVVaS:
                    case OkruhZostavyEnum.MV:
                    case OkruhZostavyEnum.MaO:
                    case OkruhZostavyEnum.VUC:
                        return "all";
                    case OkruhZostavyEnum.CEL:
                    default:
                        return "all";
                }
            };

            Func<OkruhZostavyEnum, string> getLeftTitle = (o) => "Výdavky " + o.ToString() + " v r. {rok}";
            Func<OkruhZostavyEnum, string> getRightTitle = (o) => "Všetky zdroje - výdavky";

            foreach (var okruh in (OkruhZostavyEnum[])Enum.GetValues(typeof(OkruhZostavyEnum)))
            {
                // ORO - okruh a rozpoctove organizacie
                // OROTS - okruh a rozpoctove organizacie s rozpisom tovarov a sluzieb
                zostavy.Add(new Zostava((from h in hlavicky where h.Name == $"hlv{getCode(okruh)}orot" select h).First(),
                    rozpoctoveOrganizacie, okruh, $"Výdavky {okruh} podľa okruhov a rozpočtových organizácií",
                    getLeftTitle(okruh), getRightTitle(okruh), string.Empty, okruh == OkruhZostavyEnum.CEL));
                zostavy.Add(new Zostava((from h in hlavicky where h.Name == $"hlv{getCode(okruh)}orotst" select h).First(),
                    rozpoctoveOrganizacie, okruh, $"Výdavky {okruh} podľa okruhov a rozpočtových organizácií s rozpisom tovarov a služieb",
                    getLeftTitle(okruh), getRightTitle(okruh), string.Empty, okruh == OkruhZostavyEnum.CEL));

                // OFK - okruh a funkcna klasifikacia
                // OFKTS - okruh a funkcna klasifikacia s rozpisom tovarov a sluzieb
                zostavy.Add(new Zostava((from h in hlavicky where h.Name == $"hlv{getCode(okruh)}ofkt" select h).First(),
                    rozpoctoveOrganizacie, okruh, $"Výdavky {okruh} podľa okruhov a funkčnej klasifkácie",
                    getLeftTitle(okruh), getRightTitle(okruh), string.Empty, okruh == OkruhZostavyEnum.CEL));
                zostavy.Add(new Zostava((from h in hlavicky where h.Name == $"hlv{getCode(okruh)}ofktst" select h).First(),
                    rozpoctoveOrganizacie, okruh, $"Výdavky {okruh} podľa okruhov a funkčnej klasifikácie s rozpisom tovarov a služieb",
                    getLeftTitle(okruh), getRightTitle(okruh), string.Empty, okruh == OkruhZostavyEnum.CEL));

                // OROFK - okruh, rozpoctova organizacia a funkcna klasifikacia
                // OROFKTS - okruh, rozpoctova organizacia a funkcna klasifikacia s rozpisom tovarov a sluzieb
                zostavy.Add(new Zostava((from h in hlavicky where h.Name == $"hlv{getCode(okruh)}orofkt" select h).First(),
                    rozpoctoveOrganizacie, okruh, $"Výdavky {okruh} podľa okruhov, rozp. org a funkčnej klasifkácie",
                    getLeftTitle(okruh), getRightTitle(okruh), string.Empty, okruh == OkruhZostavyEnum.CEL));
                zostavy.Add(new Zostava((from h in hlavicky where h.Name == $"hlv{getCode(okruh)}orofktst" select h).First(),
                    rozpoctoveOrganizacie, okruh, $"Výdavky {okruh} podľa okruhov, rozp. org a funkčnej klasifikácie s rozpisom tovarov a služieb",
                    getLeftTitle(okruh), getRightTitle(okruh), string.Empty, okruh == OkruhZostavyEnum.CEL));

                // OFKRO - okruh, funkcna klasifikacia a rozpoctove organizacie
                // OFKROTS - okruh, funkcna a rozp. organizaice s rozpisom tovarov a sluzieb
                zostavy.Add(new Zostava((from h in hlavicky where h.Name == $"hlv{getCode(okruh)}ofkrot" select h).First(),
                    rozpoctoveOrganizacie, okruh, $"Výdavky {okruh} podľa okruhov, funkčnej klasifkácie a rozp. org",
                    getLeftTitle(okruh), getRightTitle(okruh), string.Empty, okruh == OkruhZostavyEnum.CEL));
                zostavy.Add(new Zostava((from h in hlavicky where h.Name == $"hlv{getCode(okruh)}ofkrotst" select h).First(),
                    rozpoctoveOrganizacie, okruh, $"Výdavky {okruh} podľa okruhov, funkčnej klasifikácie a rozp. org s rozpisom tovarov a služieb",
                    getLeftTitle(okruh), getRightTitle(okruh), string.Empty, okruh == OkruhZostavyEnum.CEL));

                // FKORO - funkcna, okruh a rozp org.
                // FKOROTS - funckna okruh a rozp org. s rozpisom tovarov a sluzieb
                zostavy.Add(new Zostava((from h in hlavicky where h.Name == $"hlv{getCode(okruh)}fkorot" select h).First(),
                    rozpoctoveOrganizacie, okruh, $"Výdavky {okruh} podľa funkčnej, okruhov a rozp. org.",
                    getLeftTitle(okruh), getRightTitle(okruh), string.Empty, okruh == OkruhZostavyEnum.CEL));
                zostavy.Add(new Zostava((from h in hlavicky where h.Name == $"hlv{getCode(okruh)}fkorotst" select h).First(),
                    rozpoctoveOrganizacie, okruh, $"Výdavky {okruh} podľa funkčnej, okruhov a rozp. org. s rozpisom tovarov a služieb",
                    getLeftTitle(okruh), getRightTitle(okruh), string.Empty, okruh == OkruhZostavyEnum.CEL));
            }
            return zostavy;
        }

        private static IEnumerable<Zostava> GenerujTransferoveZostavy(IEnumerable<Hlavicka> hlavicky)
        {
            var zostavy = new List<Zostava>();
            var rozpoctoveOrganizacie = new Equals("ro", AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod), "06");


            var hl_oro = (from h in hlavicky where h.Name == "hltalloro" select h).First();
            var hl_ofk = (from h in hlavicky where h.Name == "hltallofk" select h).First();
            var hl_orofk = (from h in hlavicky where h.Name == "hltallorofk" select h).First();
            var hl_ofkro = (from h in hlavicky where h.Name == "hltallofkro" select h).First();
            var hl_fkoro = (from h in hlavicky where h.Name == "hltallfkoro" select h).First();
            var hl_cel_oro = (from h in hlavicky where h.Name == "hltceloro" select h).First();
            var hl_cel_ofk = (from h in hlavicky where h.Name == "hltcelofk" select h).First();
            var hl_cel_orofk = (from h in hlavicky where h.Name == "hltcelorofk" select h).First();
            var hl_cel_ofkro = (from h in hlavicky where h.Name == "hltcelofkro" select h).First();
            var hl_cel_fkoro = (from h in hlavicky where h.Name == "hltcelfkoro" select h).First();

            Func<OkruhZostavyEnum, string> getLeftTitle = (o) => "Transfery " + o.ToString() + " v r. {rok}";
            Func<OkruhZostavyEnum, string> getRightTitle = (o) => "Všetky zdroje - transfery";

            foreach (var okruh in (OkruhZostavyEnum[])Enum.GetValues(typeof(OkruhZostavyEnum)))
            {
                if (okruh == OkruhZostavyEnum.CEL)
                {
                    zostavy.Add(new Zostava(hl_cel_oro, rozpoctoveOrganizacie, okruh, "Rozpis transferov podľa okruhov a rozpočtových organizácií", getLeftTitle(okruh), getRightTitle(okruh), string.Empty, false));
                    zostavy.Add(new Zostava(hl_cel_ofk, rozpoctoveOrganizacie, okruh, "Rozpis transferov podľa okruhov a funkčnej klasifikácie", getLeftTitle(okruh), getRightTitle(okruh), string.Empty, false));
                    zostavy.Add(new Zostava(hl_cel_ofkro, rozpoctoveOrganizacie, okruh, "Rozpis transferov podľa okruhov, funkčnej klasifikácie a rozpočtových organizáci", getLeftTitle(okruh), getRightTitle(okruh), string.Empty, false));
                    zostavy.Add(new Zostava(hl_cel_orofk, rozpoctoveOrganizacie, okruh, "Rozpis transferov podľa okruhov, rozpočtových organizáci a funkčnej klasifikácie", getLeftTitle(okruh), getRightTitle(okruh), string.Empty, false));
                    zostavy.Add(new Zostava(hl_cel_fkoro, rozpoctoveOrganizacie, okruh, "Rozpis transferov podľa funkčnej klasifikácie, okruhov a rozpočtových organizáci", getLeftTitle(okruh), getRightTitle(okruh), string.Empty, false));
                }
                else if(okruh == OkruhZostavyEnum.MaO)
                {
                    zostavy.Add(new Zostava(hl_oro, rozpoctoveOrganizacie, okruh, "Rozpis transferov Mao s regionálnym začlenením podľa krajov", getLeftTitle(okruh), getRightTitle(okruh), string.Empty, false));
                    zostavy.Add(new Zostava(hl_ofk, rozpoctoveOrganizacie, okruh, "Rozpis transferov Mao s regionálnym začlenením funkčnej klasifikácie", getLeftTitle(okruh), getRightTitle(okruh), string.Empty, false));
                    zostavy.Add(new Zostava(hl_orofk, rozpoctoveOrganizacie, okruh, "Rozpis transferov Mao s regionálnym začlenením podľa krajov a funkčnej klasifikácie", getLeftTitle(okruh), getRightTitle(okruh), string.Empty, false));
                    zostavy.Add(new Zostava(hl_cel_oro, rozpoctoveOrganizacie, okruh, "Rozpis transferov Mao podľa funkčnej klasifikácie s regionálnym začlenením podľa krajov", getLeftTitle(okruh), getRightTitle(okruh), string.Empty, false));
                }
                else if(okruh == OkruhZostavyEnum.VUC)
                {
                    zostavy.Add(new Zostava(hl_oro, rozpoctoveOrganizacie, okruh, "Rozpis transferov VÚC podľa rozpočtových organizácií", getLeftTitle(okruh), getRightTitle(okruh), string.Empty, false));
                    zostavy.Add(new Zostava(hl_ofk, rozpoctoveOrganizacie, okruh, "Rozpis transferov VÚC podľa funkčnej klasifikácie", getLeftTitle(okruh), getRightTitle(okruh), string.Empty, false));
                    zostavy.Add(new Zostava(hl_orofk, rozpoctoveOrganizacie, okruh, "Rozpis transferov VÚC podľa rozpočtových organizáci a funkčnej klasifikácie", getLeftTitle(okruh), getRightTitle(okruh), string.Empty, false));
                    zostavy.Add(new Zostava(hl_ofkro, rozpoctoveOrganizacie, okruh, "Rozpis transferov VÚC podľa funkčnej klasifikácie a rozpočtových organizáci", getLeftTitle(okruh), getRightTitle(okruh), string.Empty, false));
                }
                else
                {
                    zostavy.Add(new Zostava(hl_oro, rozpoctoveOrganizacie, okruh, $"Rozpis transferov {okruh} podľa okruhov a rozpočtových organizácií", getLeftTitle(okruh), getRightTitle(okruh), string.Empty, false));
                    zostavy.Add(new Zostava(hl_ofk, rozpoctoveOrganizacie, okruh, $"Rozpis transferov {okruh} podľa okruhov a funkčnej klasifikácie", getLeftTitle(okruh), getRightTitle(okruh), string.Empty, false));
                    zostavy.Add(new Zostava(hl_orofk, rozpoctoveOrganizacie, okruh, $"Rozpis transferov {okruh} podľa okruhov, rozpočtových organizáci a funkčnej klasifikácie", getLeftTitle(okruh), getRightTitle(okruh), string.Empty, false));
                    zostavy.Add(new Zostava(hl_ofkro, rozpoctoveOrganizacie, okruh, $"Rozpis transferov {okruh} podľa okruhov, funkčnej klasifikácie a rozpočtových organizáci", getLeftTitle(okruh), getRightTitle(okruh), string.Empty, false));
                    zostavy.Add(new Zostava(hl_fkoro, rozpoctoveOrganizacie, okruh, $"Rozpis transferov {okruh} podľa funkčnej klasifikácie, okruhov a rozpočtových organizáci", getLeftTitle(okruh), getRightTitle(okruh), string.Empty, false));
                }
            }

            return zostavy;
        }
    }
}
