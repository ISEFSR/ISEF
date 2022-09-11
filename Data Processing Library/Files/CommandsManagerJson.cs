namespace cvti.data.Files
{
    using cvti.data.Core;
    using cvti.data.Conditions;
    using cvti.data.Functions;
    using cvti.data.Views;
    using cvti.data.Enums;
    using cvti.data.Output;
    using cvti.data.Columns;

    using System.Linq;
    using System.Collections.Generic;

    public class CommandsManagerJson : FileManagerJson<SelectCommand>
    {
        public readonly static string FileName = "commands.json";

        internal CommandsManagerJson(LogManager log, string directory)
            : base(log, directory, FileName)
        {

        }

        protected override IEnumerable<SelectCommand> GetDefault()
            => GenerateDefaultCommands();

        public static void GenerateDefaultFile(string filePath)
        {
            var manager = new CommandsManagerJson(null, filePath);

            var commands = GenerateDefaultCommands();

            foreach (var c in commands)
            {
                manager.AddValue(c);
            }

            manager.SaveData();
        }

        public static IEnumerable<SelectCommand> GenerateDefaultCommands()
        {
            var defaultConditions = ConditionsManagerJson.GenerateDefaultConditions();

            var stupenKod = AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod); stupenKod.IsVisible = false;
            var stupenNazov = AssuView.VratStlpec(AssuViewAvailableColumns.StupenText);

            var podriadenostKod = AssuView.VratStlpec(AssuViewAvailableColumns.PodriadenostKod); podriadenostKod.IsVisible = false;
            var podriadenostNazov = AssuView.VratStlpec(AssuViewAvailableColumns.PodriadenostSkrateny);

            var funkcnaKod5 = AssuView.VratStlpec(AssuViewAvailableColumns.FKod5); 
            var funkcnaText5 = AssuView.VratStlpec(AssuViewAvailableColumns.FNazov5);

            var ekonomickaKod6 = AssuView.VratStlpec(AssuViewAvailableColumns.EKod6);
            var ekonomickaText6 = AssuView.VratStlpec(AssuViewAvailableColumns.ENazov6);

            var zdrojKod4 = AssuView.VratStlpec(AssuViewAvailableColumns.ZKod4);
            var zdrojText4 = AssuView.VratStlpec(AssuViewAvailableColumns.ZNazov4);
            //var funkcnaKod3 = AssuView.VratStlpec(AssuViewAvailableColumns.FKod3);
            //var funkcnaText3 = AssuView.VratStlpec(AssuViewAvailableColumns.FNazov3);

            var icoKod = AssuView.VratStlpec(AssuViewAvailableColumns.OrgIco);

            var skutocnost = AssuView.VratStlpec(AssuViewAvailableColumns.Skut);

            var ek1KodColumn = AssuView.VratStlpec(AssuViewAvailableColumns.EKod1);

            var skutocnostSum = skutocnost.CloneMe(true).AddFunction(new Sum());

            var skutocnostEK2 = skutocnost.CloneMe(true).AddFunction(new ConditionalSum(new Equals("ekcond", ek1KodColumn, "2"))); skutocnostEK2.ColumnAlias = "EK2";
            var skutocnostEK3 = skutocnost.CloneMe(true).AddFunction(new ConditionalSum(new Equals("ekcond", ek1KodColumn, "3"))); skutocnostEK3.ColumnAlias = "EK3";
            var skutocnostEK4 = skutocnost.CloneMe(true).AddFunction(new ConditionalSum(new Equals("ekcond", ek1KodColumn, "4"))); skutocnostEK4.ColumnAlias = "EK4";
            var skutocnostEK5 = skutocnost.CloneMe(true).AddFunction(new ConditionalSum(new Equals("ekcond", ek1KodColumn, "5"))); skutocnostEK5.ColumnAlias = "EK5";

            var skutocnostEK6 = skutocnost.CloneMe(true).AddFunction(new ConditionalSum(new Equals("ekcond", ek1KodColumn, "6"))); skutocnostEK6.ColumnAlias = "EK6";
            var skutocnostEK7 = skutocnost.CloneMe(true).AddFunction(new ConditionalSum(new Equals("ekcond", ek1KodColumn, "7"))); skutocnostEK7.ColumnAlias = "EK7";
            var skutocnostEK8 = skutocnost.CloneMe(true).AddFunction(new ConditionalSum(new Equals("ekcond", ek1KodColumn, "8"))); skutocnostEK8.ColumnAlias = "EK8";
            var skutocnostEK9 = skutocnost.CloneMe(true).AddFunction(new ConditionalSum(new Equals("ekcond", ek1KodColumn, "9"))); skutocnostEK9.ColumnAlias = "EK9";

            var rozpoctoveOrganizacie = new Equals("rocnd", AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod), "06");

            var cmdPrijmyICO = new SelectCommand("ORO Prehlad prijmov za stupne a org.",
                new Column[]
                {
                    stupenKod, stupenNazov,
                    podriadenostKod, podriadenostNazov,
                    skutocnostEK2, skutocnostEK3, skutocnostEK4, skutocnostEK5
                });
            var cmdVydavkyICO = new SelectCommand("ORO Prehlad vydavkov za stupne a org.",
                new Column[]
                {
                    stupenKod, stupenNazov,
                    podriadenostKod, podriadenostNazov,
                    skutocnostEK6, skutocnostEK7, skutocnostEK8, skutocnostEK9
                });
            var cmdVydavkyICO_FK = new SelectCommand("OROFK Prehlad vydavkov stupne, org. fk.",
                new Column[]
                {
                    stupenKod, stupenNazov,
                    podriadenostKod, podriadenostNazov,
                    funkcnaKod5, funkcnaText5,
                    skutocnostEK6, skutocnostEK7, skutocnostEK8, skutocnostEK9
                });
            var cmdVydavkyFK = new SelectCommand("OFK Prehlad vydavkov stupne a fk.",
                new Column[]
                {
                    stupenKod, stupenNazov,
                    funkcnaKod5, funkcnaText5,
                    skutocnostEK6, skutocnostEK7, skutocnostEK8, skutocnostEK9
                });

            // Command pre C1 
            // ---------------------------------------------------------
            var cmdC1 = new SelectCommand("C1 prehlad",
                new Column[]
                {
                    stupenKod, stupenNazov, funkcnaKod5, funkcnaText5, podriadenostKod, podriadenostNazov, skutocnostSum
                })
            { CommandCondition = defaultConditions.Where(c => c.ConditionName == "C1_ostatne").FirstOrDefault() };

            // Command pre C1 vvs
            // ---------------------------------------------------------
            var cmdC1_vvs = new SelectCommand("C1 VVS prehlad",
                new Column[]
                {
                    zdrojKod4, zdrojText4, skutocnostSum
                })
            { CommandCondition = defaultConditions.Where(c => c.ConditionName == "C1_vvs").FirstOrDefault() };

            // Command pre C2
            // ---------------------------------------------------------
            var cmdC2 = new SelectCommand("C2 prehlad",
                new Column[]
                {
                    stupenKod, stupenNazov, funkcnaKod5, funkcnaText5, podriadenostKod, podriadenostNazov, skutocnostSum
                })
            { CommandCondition = defaultConditions.Where(c => c.ConditionName == "C2").FirstOrDefault() };

            // Command pre C5a
            // ---------------------------------------------------------
            var cmdC5a = new SelectCommand("C5a prehlad",
                new Column[]
                {
                    stupenKod, stupenNazov, funkcnaKod5, funkcnaText5, podriadenostKod, podriadenostNazov, skutocnostSum
                })
            { CommandCondition = defaultConditions.Where(c => c.ConditionName == "C5a").FirstOrDefault() };

            // Command pre C8
            // ---------------------------------------------------------
            var cmdC8 = new SelectCommand("C8 prehlad",
                new Column[]
                {
                    stupenKod, stupenNazov, ekonomickaKod6, ekonomickaText6, funkcnaKod5, funkcnaText5, podriadenostKod, podriadenostNazov, skutocnostSum
                })
            { CommandCondition = defaultConditions.Where(c => c.ConditionName == "C8").FirstOrDefault() };

            // Command pre C10
            // ---------------------------------------------------------
            var cmdC10 = new SelectCommand("C10 prehlad",
                new Column[]
                {
                    stupenKod, stupenNazov, funkcnaKod5, funkcnaText5, podriadenostKod, podriadenostNazov, skutocnostSum
                })
            { CommandCondition = defaultConditions.Where(c => c.ConditionName == "C10").FirstOrDefault() };

            // Command pre C13
            // ---------------------------------------------------------
            var cmdC13 = new SelectCommand("C13 prehlad",
                new Column[]
                {
                    stupenKod, stupenNazov, funkcnaKod5, funkcnaText5, podriadenostKod, podriadenostNazov, skutocnostSum
                })
            { CommandCondition = defaultConditions.Where(c => c.ConditionName == "C13").FirstOrDefault() };

            // Command pre L1
            // ---------------------------------------------------------
            var cmdL1 = new SelectCommand("L1 prehlad",
                new Column[]
                {
                    stupenKod, stupenNazov, podriadenostKod, podriadenostNazov, icoKod, funkcnaKod5, funkcnaText5, skutocnostSum
                })
            { CommandCondition = defaultConditions.Where(c => c.ConditionName == "L1").FirstOrDefault() };

            // Command pre L2
            // ---------------------------------------------------------
            var cmdL2 = new SelectCommand("L2 prehlad",
                new Column[]
                {
                    stupenKod, stupenNazov, podriadenostKod, podriadenostNazov, icoKod, funkcnaKod5, funkcnaText5, skutocnostSum
                })
            { CommandCondition = defaultConditions.Where(c => c.ConditionName == "L2").FirstOrDefault() };

            // Command pre L5a
            // ---------------------------------------------------------
            var cmdL5a = new SelectCommand("L5a prehlad",
                new Column[]
                {
                    stupenKod, stupenNazov, podriadenostKod, podriadenostNazov, icoKod, funkcnaKod5, funkcnaText5, skutocnostSum
                })
            { CommandCondition = defaultConditions.Where(c => c.ConditionName == "L5a").FirstOrDefault() };

            // Command pre L10
            // ---------------------------------------------------------
            var cmdL10 = new SelectCommand("L10 prehlad",
                new Column[]
                {
                    stupenKod, stupenNazov, podriadenostKod, podriadenostNazov, icoKod, funkcnaKod5, funkcnaText5, skutocnostSum
                })
            { CommandCondition = defaultConditions.Where(c => c.ConditionName == "L10").FirstOrDefault() };

            // Command pre L13
            // ---------------------------------------------------------
            var cmdL13 = new SelectCommand("L13 prehlad",
                new Column[]
                {
                    stupenKod, stupenNazov, podriadenostKod, podriadenostNazov, icoKod, funkcnaKod5, funkcnaText5, skutocnostSum
                })
            { CommandCondition = defaultConditions.Where(c => c.ConditionName == "L13").FirstOrDefault() };

            // Command pre X15
            // ---------------------------------------------------------
            var cmdX15 = new SelectCommand("X15 prehlad",
                new Column[]
                {
                    stupenKod, stupenNazov, podriadenostKod, podriadenostNazov, icoKod, funkcnaKod5, funkcnaText5, skutocnostSum
                })
            { CommandCondition = defaultConditions.Where(c => c.ConditionName == "X15").FirstOrDefault() };

            // Command pre E1
            // ---------------------------------------------------------
            var cmdE1 = new SelectCommand("E1 prehlad",
                new Column[]
                {
                    stupenKod, stupenNazov,skutocnostSum
                })
            { CommandCondition = defaultConditions.Where(c => c.ConditionName == "E1").FirstOrDefault() };

            // Command pre E1
            // ---------------------------------------------------------
            var cmdE1stp = new SelectCommand("E1 stip. prehlad",
                new Column[]
                {
                    stupenKod, stupenNazov, funkcnaKod5, funkcnaText5, skutocnostSum
                })
            { CommandCondition = defaultConditions.Where(c => c.ConditionName == "E1_stip").FirstOrDefault() };

            // Command pre E5c
            // ---------------------------------------------------------
            var cmdE5c = new SelectCommand("E5c prehlad",
                new Column[]
                {
                    stupenKod, stupenNazov, funkcnaKod5, funkcnaText5, skutocnostSum
                })
            { CommandCondition = defaultConditions.Where(c => c.ConditionName == "E5c").FirstOrDefault() };

            // Command pre H1
            // ---------------------------------------------------------
            var cmdH1 = new SelectCommand("H1 prehlad",
                new Column[]
                {
                    stupenKod, stupenNazov, funkcnaKod5, funkcnaText5, skutocnostSum
                })
            { CommandCondition = defaultConditions.Where(c => c.ConditionName == "H1").FirstOrDefault() };

            // Command pre H1
            // ---------------------------------------------------------
            var cmdH1kap = new SelectCommand("H1 kap. prehlad",
                new Column[]
                {
                    stupenKod, stupenNazov, funkcnaKod5, funkcnaText5, skutocnostSum
                })
            { CommandCondition = defaultConditions.Where(c => c.ConditionName == "H1_kap").FirstOrDefault() };

            // Command pre H5b
            // ---------------------------------------------------------
            var cmdH5b = new SelectCommand("H5b prehlad",
                new Column[]
                {
                    stupenKod, stupenNazov, funkcnaKod5, funkcnaText5, skutocnostSum
                })
            { CommandCondition = defaultConditions.Where(c => c.ConditionName == "H5b").FirstOrDefault() };

            // Command pre F1
            // ---------------------------------------------------------
            var cmdF1 = new SelectCommand("F1 prehlad",
                new Column[]
                {
                    stupenKod, stupenNazov, funkcnaKod5, funkcnaText5, zdrojKod4, zdrojText4, icoKod, skutocnostSum
                })
            { CommandCondition = defaultConditions.Where(c => c.ConditionName == "F1").FirstOrDefault() };

            cmdPrijmyICO.CommandCondition = rozpoctoveOrganizacie;
            cmdVydavkyICO.CommandCondition = rozpoctoveOrganizacie;
            cmdVydavkyICO_FK.CommandCondition = rozpoctoveOrganizacie;
            cmdVydavkyFK.CommandCondition = rozpoctoveOrganizacie;

            return new SelectCommand[]
            {
                cmdPrijmyICO,
                cmdVydavkyICO,
                cmdVydavkyICO_FK,
                cmdVydavkyFK,
                cmdC1,
                cmdC1_vvs,
                cmdC2,
                cmdC5a,
                cmdC8,
                cmdC10,
                cmdC13,
                cmdL1,
                cmdL2,
                cmdL5a,
                cmdL10,
                cmdL13,
                cmdX15,
                cmdE1,
                cmdE1stp,
                cmdE5c,
                cmdH1,
                cmdH1kap,
                cmdH5b,
                cmdF1
            };
        }
    }
}