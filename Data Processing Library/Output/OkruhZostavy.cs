namespace cvti.data.Output
{
    using cvti.data.Conditions;
    using cvti.data.Views;
    using cvti.data.Tables;
    using System.Collections.Generic;
    using System.Linq;

    public class OkruhZostavy : Inlist
    {
        public OkruhZostavy(string name, string[] stupne)
            : base(name, AssuView.VratStlpec(Enums.AssuViewAvailableColumns.StupenKod), stupne)
        {
            Stupne = stupne;
        }

        public string[] Stupne { get; set; }

        public static IEnumerable<OkruhZostavy> VratOkruhy()
        {
            return new OkruhZostavy[]
            {
                new OkruhZostavy("MŠVVaŠ SR", new string[]{ "u", "2", "r", "a" }),
                new OkruhZostavy("MV SR", new string[]{ "v", "s" }),
                new OkruhZostavy("Mestá a obce", new string[]{ "o" }),
                new OkruhZostavy("Vyššie územné celky", new string[]{ "9" }),
                new OkruhZostavy("Celky", new string[]{ "u", "2", "r" ,"a", "v", "s", "o", "9" })
            };
        }

        public static OkruhZostavy VratOkruh(OkruhZostavyEnum okruh) => VratOkruhy().ElementAt((int)okruh);

        public bool PotrebujeOdpocetTransferov(IEnumerable<TransferRiadok> transfery)
        {
            var potrebuje = true;
            foreach (var t in transfery)
            {
                if (!(Stupne.Contains(t.FromStupen) && Stupne.Contains(t.ToStupen)))
                    potrebuje = false;

            }
            return potrebuje;
        }
    }
}
