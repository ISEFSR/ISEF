namespace cvti.data.Output
{
    using cvti.data.Conditions;
    using cvti.data.Views;
    using cvti.data.Output;
    using Newtonsoft.Json;
    using System.Data.SqlClient;
    using cvti.data.Enums;
    using cvti.data.Files;

    public class Zostava : ISerializableJson
    {
        public Zostava(Hlavicka hlavicka, Condition podmienka, OkruhZostavyEnum okruh, 
            string nazov, string leftTitle, string rightTitle, string zaradenie, bool odpocet = false)
        {
            Nazov = nazov;
            Hlavicka = hlavicka;
            Condition = podmienka;

            Okruh = okruh;

            LeftTitle = leftTitle;
            RightTitle = rightTitle;
            ZaradenieZostavy = zaradenie;

            OdpocetTransferov = odpocet;
        }

        [JsonConstructor]
        public Zostava() { }

        public string Nazov { get; set; }

        public string LeftTitle { get; set; }

        public string RightTitle { get; set; }

        public string ZaradenieZostavy { get; set; }

        public Hlavicka Hlavicka { get; set; }

        public Condition Condition { get; set; }

        public OkruhZostavyEnum Okruh { get; set; }

        public bool OdpocetTransferov { get; set; }

        public string Code { get => Nazov; }

        public SelectCommand GetSelectCommand(int rok, Condition cnd = null, bool withoutInvisibleColumns = false)
        {
            var command = Hlavicka.GetCommand(withoutInvisibleColumns);

            var defaultCondition = command.CommandCondition;

            var rokCond = new Equals("rok",
                AssuView.VratStlpec(AssuViewAvailableColumns.Rok), rok);

            rokCond.AddCondition(OkruhZostavy.VratOkruh(Okruh), ConditionOperator.And);

            if (cnd != null)
                rokCond.AddCondition(cnd.CloneMe(true), ConditionOperator.And);

            if (defaultCondition != null)
            {
                rokCond.AddCondition(defaultCondition, ConditionOperator.And);
            }
            
            if (Condition != null)
                rokCond.AddCondition(Condition, ConditionOperator.And);

            command.CommandCondition = rokCond;

            return command;
        }

        public SqlCommand GetSqlCommand(int rok, Condition cnd = null) => GetSelectCommand(rok, cnd).GenerateCommand();

        public override string ToString()
        {
            return Nazov;
        }
    }
}