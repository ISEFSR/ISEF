namespace cvti.data.Files
{
    using cvti.data.Core;
    using cvti.data.Conditions;
    using cvti.data.Views;
    using cvti.data.Enums;

    using System.Collections.Generic;

    public class ConditionsManagerJson : FileManagerJson<Condition>
    {
        public readonly static string FileName = "conditions.json";

        internal ConditionsManagerJson(LogManager log, string directory)
            : base(log, directory, FileName)
        {

        }

        protected override IEnumerable<Condition> GetDefault()
            => GenerateDefaultConditions();

        public static void GenerateDefaulFile(string path)
        {
            var conditions = GenerateDefaultConditions();

            var manager = new ConditionsManagerJson(null, path);

            foreach (var c in conditions)
                manager.AddValue(c);

            manager.SaveData();
        }

        public static IEnumerable<Condition> GenerateDefaultConditions()
        {
            var year2015 = new Equals("Rok 2017", AssuView.VratStlpec(AssuViewAvailableColumns.Rok), "2017");
            var year2016 = new Equals("Rok 2018", AssuView.VratStlpec(AssuViewAvailableColumns.Rok), "2018");
            var year2017 = new Equals("Rok 2019", AssuView.VratStlpec(AssuViewAvailableColumns.Rok), "2019");
            var year2018 = new Equals("Rok 2020", AssuView.VratStlpec(AssuViewAvailableColumns.Rok), "2020");

            var prijymyFK = new Equals("Prijmy podla FK", AssuView.VratStlpec(AssuViewAvailableColumns.FKod2), "  ");
            var vydavlyFK = new Equals("Vydavky podla FK", AssuView.VratStlpec(AssuViewAvailableColumns.FKod2), "  ") { Negate = true };

            var ro = new Equals("Rozp. org.", AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod), "06");
            var po = new Equals("Prisp. org.", AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod), "06");

            var conditions = new List<Condition>
            {
                year2015,
                year2016,
                year2017,
                year2018,
                prijymyFK,
                vydavlyFK,
                ro,
                po,
            };
            conditions.AddRange(GenerateDefaultOkruhy());
            conditions.AddRange(GenerateDefaultTyp());

            conditions.AddRange(GenerateDefaultC());
            conditions.AddRange(GenerateDefaultE());
            conditions.AddRange(GenerateDefaultL());
            conditions.AddRange(GenerateDefaultH());
            conditions.AddRange(GenerateDefaultF());
            conditions.AddRange(GenerateDefaultG());

            return conditions;
        }

        /// <summary>
        /// Vygeneruje defaultne okruhy ako podmienky
        /// </summary>
        /// <returns>Defaultne okruhy ako podmienky ako <see cref="IEnumerable{T}"/> kde T je <see cref="Condition"/></returns>
        /// <remarks>
        /// celky, kms, kmv, obce
        /// </remarks>
        public static IEnumerable<Condition> GenerateDefaultOkruhy()
        {
            var celky = new Inlist("Celky", AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod), new object[] { "o", "a", "9", "2", "v", "s" });
            var kms = new Inlist("Kapitola ministerstva školstva", AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod), new object[] { "a", "2", "9" });
            var kmv = new Inlist("Kapitola ministerstva vnútra", AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod), new object[] { "a", "2", "u" });
            var obce = new Equals("Mestá a obce", AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod), "o");

            return new Condition[] 
            {
                celky, 
                kms,
                kmv, 
                obce 
            };
        }

        /// <summary>
        /// Vygeneruje defaultne typy ako podmienky
        /// </summary>
        /// <returns>Defaultne typy ako podmienky ako <see cref="IEnumerable{T}"/> kde T je <see cref="Condition"/></returns>
        /// <remarks>
        /// prijmy, vydavky, transfery
        /// </remarks>
        public static IEnumerable<Condition> GenerateDefaultTyp()
        {
            var prijmy = new LessThan("Príjmy", AssuView.VratStlpec(AssuViewAvailableColumns.EKod1), "6");
            var vydavky = new GreaterThan("Výdavky", AssuView.VratStlpec(AssuViewAvailableColumns.EKod1), "5");
            var transfery = new Inlist("Transfery", AssuView.VratStlpec(AssuViewAvailableColumns.EKod2), new object[] { "64", "72" });

            return new Condition[] 
            {
                prijmy, 
                vydavky, 
                transfery
            };
        }

        /// <summary>
        /// Vygeneruje OECD podmienky pre riadok C
        /// </summary>
        /// <returns>OECD podmienky pre riadok C ako <see cref="IEnumerable{T}"/> kde T je <see cref="Condition"/></returns>
        /// <remarks>
        /// C1_ostatne, C1_vvs, C2, C5a, C8, C10, C13
        /// </remarks>
        public static IEnumerable<Condition> GenerateDefaultC()
        {
            // C1
            // mimo vvs
            var c1_ostatne = new Equals("C1_ostatne", AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod), "06");
            c1_ostatne.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod), new object[] { "2", "v" }), ConditionOperator.And);
            c1_ostatne.AddCondition(new Inlist("bezne+kapitalove", AssuView.VratStlpec(AssuViewAvailableColumns.EKod1), new object[] { "6", "7" }), ConditionOperator.And);
            c1_ostatne.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod6), new object[] { "637001", "637002", "637003", "637011", "637015", "637034", "642001", "642002", "642007", "642009", "642014", "642200" }) { Negate = true }, ConditionOperator.And);
            c1_ostatne.AddCondition(new Inlist("nie+transfery+stip", AssuView.VratStlpec(AssuViewAvailableColumns.EKod6), new object[] { "641008", "641013", "641014", "642004", "642005", "721003", "721006", "721007", "722002", "642036" }) { Negate = true }, ConditionOperator.And);
            c1_ostatne.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.Kde), new object[] { "V", "Z", "X" }), ConditionOperator.And);

            // C1
            // verejne vysoke skoly
            var c1_vvs = new Equals("C1_vvs", AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod), "06");
            var c1_vvs_p1 = new Equals("C1_vvs_p1", AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod), "a");
            c1_vvs_p1.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod1), new object[] { "6", "7", "8" }), ConditionOperator.And);
            c1_vvs_p1.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.Kde), new object[] { "V", "Z" }), ConditionOperator.And);
            c1_vvs_p1.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.ZKod2), new object[] { "35", "37" }) { Negate = true }, ConditionOperator.And);

            var c1_vvs_p2 = new Equals("C1_vvs_p2", AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod), "a");
            c1_vvs_p2.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod1), new object[] { "6", "7", "8" }), ConditionOperator.And);
            c1_vvs_p2.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.ZKod2), new object[] { "46" }), ConditionOperator.And);
            c1_vvs_p1.AddCondition(c1_vvs_p2, ConditionOperator.Or);
            c1_vvs.AddCondition(c1_vvs_p1, ConditionOperator.And);

            // C2
            var c2 = new Equals("C2", AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod), "06");
            c2.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.Kde), new object[] { "V", "Z", "X" }), ConditionOperator.And);
            c2.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod), new object[] { "2", "v" }), ConditionOperator.And);
            c2.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod6), new object[] { "642004", "642005", "722002" }), ConditionOperator.And);
            
            // C5a
            var c5a = new Equals("C5a", AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod), "06");
            c5a.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod1), new object[] { "7" }), ConditionOperator.And);
            c5a.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod6), new object[] { "721003", "721006", "721007", "722002" }) { Negate = true }, ConditionOperator.And);
            c5a.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod), new object[] { "2", "v" }), ConditionOperator.And);
            c5a.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.Kde), new object[] { "V", "Z", "X" }), ConditionOperator.And);

            // C8
            var c8 = new Equals("C8", AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod), "06");
            c8.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod6), new object[] { "641013", "641014", "721006", "721007" }), ConditionOperator.And);
            c8.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod), new object[] { "2", "v" }), ConditionOperator.And);
            c8.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.Kde), new object[] { "V", "Z", "X" }), ConditionOperator.And);

            // C10
            var c10 = new Equals("C10", AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod), "06");
            c10.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod6), new object[] { "642036" }), ConditionOperator.And);
            c10.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod), new object[] { "2", "v" }), ConditionOperator.And);
            c10.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.Kde), new object[] { "V", "Z", "X" }), ConditionOperator.And);

            // C13
            var c13 = new Equals("C13", AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod), "06");
            c13.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod6), new object[] { "637001", "637002", "637003", "637011", "637015", "637034", "642001", "642002", "642007", "642009", "642014", "642200" }), ConditionOperator.And);
            c13.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod), new object[] { "2", "v" }), ConditionOperator.And);
            c13.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.Kde), new object[] { "V", "Z", "X" }), ConditionOperator.And);

            return new List<Condition>()
            {
                c1_ostatne,
                c1_vvs,
                c2,
                c5a,
                c8,
                c10,
                c13
            };
        }

        /// <summary>
        /// Vygeneruje OECD podmienky pre riadok L
        /// </summary>
        /// <returns>OECD podmienky pre riadok L ako <see cref="IEnumerable{T}"/> kde T je <see cref="Condition"/></returns>
        /// <remarks>
        /// L1, L2, L5a, L10, L13, X15
        /// </remarks>
        public static IEnumerable<Condition> GenerateDefaultL()
        {
            // L1
            var l1 = new Equals("L1", AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod), "06");
            l1.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod), new object[] { "9", "o" }), ConditionOperator.And);
            l1.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod1), new object[] { "6", "7" }), ConditionOperator.And);
            l1.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod6), new object[] { "637001", "637002", "637003", "637011", "637015", "637034", "642001", "642002", "642007", "642009", "642014", "642200" }) { Negate = true }, ConditionOperator.And);
            l1.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod6), new object[] { "642004", "642005", "722002", "642036" }) { Negate = true }, ConditionOperator.And);
            l1.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.Kde), new object[] { "V", "Z", "X" }), ConditionOperator.And);
            l1.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.ZKod2), new object[] { "35", "37" }) { Negate = true }, ConditionOperator.And);

            // L2
            var l2 = new Equals("L2", AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod), "06");
            l2.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod), new object[] { "9", "o" }), ConditionOperator.And);
            l2.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod6), new object[] { "642004", "642005", "722002" }) { Negate = true }, ConditionOperator.And);
            l2.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.Kde), new object[] { "V", "Z", "X" }), ConditionOperator.And);
            l2.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.ZKod2), new object[] { "35", "37" }) { Negate = true }, ConditionOperator.And);

            // L5a
            var l5a = new Equals("L5a", AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod), "06");
            l5a.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod), new object[] { "9", "o" }), ConditionOperator.And);
            l5a.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod1), new object[] { "7" }), ConditionOperator.And);
            l5a.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod6), new object[] { "722002" }) { Negate = true }, ConditionOperator.And);
            l5a.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.Kde), new object[] { "V", "Z", "X" }), ConditionOperator.And);
            l5a.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.ZKod2), new object[] { "35", "37" }) { Negate = true }, ConditionOperator.And);

            // L10
            var l10 = new Equals("L10", AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod), "06");
            l10.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod), new object[] { "9", "o" }), ConditionOperator.And);
            l10.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod6), new object[] { "642036" }), ConditionOperator.And);
            l10.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.Kde), new object[] { "V", "Z", "X" }), ConditionOperator.And);
            l10.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.ZKod2), new object[] { "35", "37" }) { Negate = true }, ConditionOperator.And);

            // L13
            var l13 = new Equals("L13", AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod), "06");
            l13.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod), new object[] { "9", "o" }), ConditionOperator.And);
            l13.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod6), new object[] { "637001", "637002", "637003", "637011", "637015", "637034", "642001", "642002", "642007", "642009", "642014", "642200" }), ConditionOperator.And);
            l13.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.Kde), new object[] { "V", "Z", "X" }), ConditionOperator.And);
            l13.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.ZKod2), new object[] { "35", "37" }) { Negate = true }, ConditionOperator.And);

            // X15
            var x15 = new Equals("X15", AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod), "06");
            x15.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod), new object[] { "9", "o" }), ConditionOperator.And);
            x15.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod1), new object[] { "7" }), ConditionOperator.And);
            x15.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod6), new object[] { "722002" }) { Negate = true }, ConditionOperator.And);
            x15.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.Kde), new object[] { "V", "Z", "X", "M" }), ConditionOperator.And);

            return new List<Condition>() 
            {
                l1,
                l2,
                l5a,
                l10,
                l13,
                x15
            };
        }

        /// <summary>
        /// Vygeneruje OECD podmienky pre riadok E
        /// </summary>
        /// <returns>OECD podmienky pre riadok E ako <see cref="IEnumerable{T}"/> kde T je <see cref="Condition"/></returns>
        /// <remarks>
        /// E1, E1_stip, E5c
        /// </remarks>
        public static IEnumerable<Condition> GenerateDefaultE()
        {
            // E1
            // TODO: dopln SKNACE
            var e1 = new Equals("E1", AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod), "06");
            e1.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.Kde), new object[] { "M" }), ConditionOperator.And);
            var e1_eko = new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod6), new object[] { "211004", "212002", "212003", "212004", "223001", "223004", "233001", "291001", "291002", "291003", "291004", "291007", "292027" });
            e1_eko.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod3), new object[] { "311", "321" }), ConditionOperator.Or);
            e1.AddCondition(e1_eko, ConditionOperator.And);

            // E1_stipendia 
            // TODO: dopln SKNACE
            var e1_stip = new Equals("E1_stip", AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod), "06");
            e1_stip.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod6), new object[] { "642036" }), ConditionOperator.And);
            e1_stip.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.Kde), new object[] { "M" }), ConditionOperator.And);

            // E5c
            // TODO: dopln SKNACE
            var e5c = new Equals("E5c", AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod), "06");
            e5c.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod1), new object[] { "6", "7" }), ConditionOperator.And);
            e5c.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.FKod5), new object[] { "01401", "01402", "09702" }), ConditionOperator.And);
            e5c.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.Kde), new object[] { "M" }), ConditionOperator.And);

            return new List<Condition>() 
            {
                e1,
                e1_stip,
                e5c
            };
        }

        /// <summary>
        /// Vygeneruje OECD podmienky pre riadok H
        /// </summary>
        /// <returns>OECD podmienky pre riadok H ako <see cref="IEnumerable{T}"/> kde T jke <see cref="Condition"/></returns>
        /// <remarks>
        /// H1, H1_kap, H5b
        /// </remarks>
        public static IEnumerable<Condition> GenerateDefaultH()
        {
            var h1 = new Equals("H1", AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod), "06");
            h1.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod1), new object[] { "6", "7" }), ConditionOperator.And);
            h1.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod), new object[] { "o", "9", "2", "v", "a", "s" }), ConditionOperator.And);
            h1.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.Kde), new object[] { "M" }), ConditionOperator.And);
            h1.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod6), new object[] { "642036" }) { Negate = true }, ConditionOperator.And);
            var h1_funkcna = new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.FKod4), new object[] { "0113", "0133", "0970", "0850", "0750", "0150", "0487" }) { Negate = true };
            h1_funkcna.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.FKod5), new object[] { "01401", "01404", "01402", "09701", "09702" }) { Negate = true }, ConditionOperator.And);
            h1.AddCondition(h1_funkcna, ConditionOperator.And);

            // H1 kap
            // ----------------------------------------------------------
            var h1_kap = new Equals("H1_kap", AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod), "06");
            h1_kap.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod), new object[] { "o", "9", "2", "v", "a", "s" }), ConditionOperator.And);
            h1_kap.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod1), new object[] { "7" }), ConditionOperator.And);
            h1_kap.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.Kde), new object[] { "M" }), ConditionOperator.And);
            var h1_kapfunkcna = new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.FKod4), new object[] { "0113", "0133", "0970", "0850", "0750", "0150", "0487" }) { Negate = true };
            h1_kapfunkcna.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.FKod5), new object[] { "01401", "01404", "01402", "09701", "09702" }) { Negate = true }, ConditionOperator.And);
            h1_kap.AddCondition(h1_kapfunkcna, ConditionOperator.And);
            // ----------------------------------------------------------

            // H5b 
            // ----------------------------------------------------------
            var h5b = new Equals("H5b", AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod), "06");
            h5b.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod), new object[] { "o", "9", "2", "v", "a"}), ConditionOperator.And);
            h5b.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod1), new object[] { "6", "7" }), ConditionOperator.And);
            h5b.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.Kde), new object[] { "M" }), ConditionOperator.And);
            h5b.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod6), new object[] { "642036" }) { Negate = true }, ConditionOperator.And);
            h5b.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.FKod3), new object[] { "096", "082" }), ConditionOperator.And);
            // ----------------------------------------------------------

            return new List<Condition>() 
            {
                h1,
                h1_kap,
                h5b
            };
        }

        /// <summary>
        /// Vygeneruje OECD podmineky pre riadok F
        /// </summary>
        /// <returns>OECD podmienky pre riadok F ako <see cref="IEnumerable{T}"/> kde T je <see cref="Condition"/></returns>
        /// <remarks>
        /// F6, F67_vsetko, F8
        /// </remarks>
        public static IEnumerable<Condition> GenerateDefaultF()
        {
            // F6
            // ----------------------------------------------------------
            // TODO: spytaj sa misa co je 15,1
            // proste musime najst inu logiku na tom zdroji :)
            var f6 = new Equals("F6", AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod), "06");
            f6.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod), new object[] { "2", "v" }), ConditionOperator.And);
            f6.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod1), new object[] { "6", "7" }), ConditionOperator.And);
            f6.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.ZKod2), new[] { "35", "37" }) { Negate = true }, ConditionOperator.And);
            f6.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.ZKod4), new[] { "11E5", "11G5", "3AA3" }) { Negate = true }, ConditionOperator.And);
            // ----------------------------------------------------------

            // F6-7 vsetko
            // ----------------------------------------------------------
            var f67 = new Equals("F67_vsetko", AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod), "06");
            f67.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod), new object[] { "o", "9", "2", "v" }), ConditionOperator.And);
            f67.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod1), new object[] { "6", "7" }), ConditionOperator.And);
            // ----------------------------------------------------------

            // F8
            // ----------------------------------------------------------
            // TODO: spytaj sa misa co je 15,1
            // proste musime najst inu logiku na tom zdroji :)
            var f8 = new Equals("F8", AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod), "06");
            f8.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.ZKod2), new[] { "35", "37" }) { Negate = true }, ConditionOperator.And);
            f8.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.ZKod4), new[] { "11E5", "11G5", "3AA3" }) { Negate = true }, ConditionOperator.And);
            f8.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod), new object[] { "o", "9" }), ConditionOperator.And);
            f8.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod1), new object[] { "6", "7" }), ConditionOperator.And);
            // ----------------------------------------------------------

            return new List<Condition>()
            {
                f6,
                f67,
                f8
            };
        }

        /// <summary>
        /// Vygeneruje OECD podmienky pre riadok G
        /// </summary>
        /// <returns>OECD podmienky pre riadok G ako <see cref="IEnumerable{T}"/> kde T je <see cref="Condition"/></returns>
        /// <remarks>
        /// G5b_vs, G5b_test, G5b, G5c
        /// </remarks>
        public static IEnumerable<Condition> GenerateDefaultG()
        {
            // G5b vs
            // ----------------------------------------------------------
            // OECD Gckove vybery
            var g5b_vs = new Equals("G5b_vs", AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod), "06");
            g5b_vs.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod), new object[] { "a", "s" }), ConditionOperator.And);
            g5b_vs.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.Kde), new object[] { "V", "Z", "X" }), ConditionOperator.And);
            g5b_vs.AddCondition(new Equals(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.ZKod2), "32") { Negate = true }, ConditionOperator.And);
            g5b_vs.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod1), new object[] { "6", "7" }), ConditionOperator.And);
            g5b_vs.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod6), new object[] { "641008", "641014", "721003", "721006", "721007" }) { Negate = true }, ConditionOperator.And);
            g5b_vs.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.FKod4), new object[] { "0850", "091", "092", "093", "094", "095", "09802", "0970", "09701", "0113", "0133", "01401", "01402", "0150", "0210", "0220", "0121", "0412", "0422", "0731", "0750", "0760", "0530" }) { Negate = true }, ConditionOperator.And);
            g5b_vs.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.FKod4), new object[] { "0850", "09802", "0970", "09701", "0113", "0133", "01401", "01402", "0150", "0210", "0220", "0121", "0412", "0422", "0731", "0750", "0760", "0530" }) { Negate = true }, ConditionOperator.And);
            g5b_vs.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.FKod3), new object[] { "091", "092", "093", "094", "095" }) { Negate = true }, ConditionOperator.And);
            // ----------------------------------------------------------

            // G5b
            // ----------------------------------------------------------
            var g5b_test = new Equals("G5b_test", AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod), "06");
            g5b_test.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod), new object[] { "2", "v", "o", "9" }), ConditionOperator.And);
            g5b_test.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod6), new object[] { "641008", "641014", "721003", "721006", "721007" }) { Negate = true }, ConditionOperator.And);
            g5b_test.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod1), new object[] { "6", "7" }), ConditionOperator.And);
            g5b_test.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.Kde), new object[] { "V", "Z", "X" }), ConditionOperator.And);
            g5b_test.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.ZKod2), new[] { "35" }) { Negate = true }, ConditionOperator.And);
            var g5b_funkcna = new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.FKod5), new object[] { "01401", "01402", "09701", "09802" }) { Negate = true };
            g5b_funkcna.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.FKod4), new object[] { "0850", "0970", "0113", "0133", "0150", "0210", "0220" }) { Negate = true }, ConditionOperator.And);
            g5b_funkcna.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.FKod3), new object[] { "091", "092", "093", "094", "095" }) { Negate = true }, ConditionOperator.And);
            g5b_test.AddCondition(g5b_funkcna, ConditionOperator.And);
            // ----------------------------------------------------------

            // G5b
            // ----------------------------------------------------------
            var g5b = new Equals("G5b", AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod), "06");
            g5b.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod), new object[] { "2", "v", "o", "9" }), ConditionOperator.And);
            g5b.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod1), new object[] { "6", "7" }), ConditionOperator.And);
            g5b.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.Kde), new object[] { "V", "Z", "X" }), ConditionOperator.And);
            g5b.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod6), new object[] { "641008", "641014", "721003", "721006", "721007" }) { Negate = true }, ConditionOperator.And);
            g5b.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.ZKod2), new[] { "35" }) { Negate = true }, ConditionOperator.And);
            var g5bfunkcna = new Equals(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.FKod2), "08");
            g5bfunkcna.AddCondition(new Equals(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.FKod4), "0850") { Negate = true }, ConditionOperator.And);
            g5b.AddCondition(g5bfunkcna, ConditionOperator.And);
            // ----------------------------------------------------------

            // G5C
            // ----------------------------------------------------------
            // Nikde tu nevidim segment 06 takze je to otazne!!
            // TODO: spytaj sa Misa
            var g5c = new Equals("G5c", AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod), "06");
            g5c.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod), new object[] { "a", "s" }), ConditionOperator.And);
            g5c.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod1), new object[] { "6", "7" }), ConditionOperator.And);
            g5c.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.Kde), new object[] { "V", "Z", "X" }), ConditionOperator.And);
            g5c.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.ZKod2), new[] { "35", "37" }) { Negate = true }, ConditionOperator.And);
            var g5c_funkcna = new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.FKod5), new object[] { "01404", "09701", "09702", });
            g5c_funkcna.AddCondition(new Inlist(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.FKod4), new object[] { "0487", "01402", "0150", "0750", "0850", "0970", "0133" }), ConditionOperator.Or);
            g5c.AddCondition(g5c_funkcna, ConditionOperator.And);
            // ----------------------------------------------------------

            return new List<Condition>() 
            {
                g5b_vs,
                g5b_test,
                g5b,
                g5c
            };
        }
    }
}

