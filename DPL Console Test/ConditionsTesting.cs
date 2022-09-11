using cvti.data.Conditions;
using cvti.data.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPL_Console_Test
{
    class ConditionsTesting
    {
        public void Test()
        {
            var main = new Equals(string.Empty, AssuView.VratStlpec(cvti.data.Enums.AssuViewAvailableColumns.Rok), 2020);

            var t1 = new CompoundCondition(string.Empty, new Equals(string.Empty, AssuView.VratStlpec(cvti.data.Enums.AssuViewAvailableColumns.EKod6), "641013").
                AddCondition(new Equals(string.Empty, AssuView.VratStlpec(cvti.data.Enums.AssuViewAvailableColumns.StupenKod), "v"), cvti.data.Enums.ConditionOperator.And));

            var t2 = new CompoundCondition(string.Empty, new Equals(string.Empty, AssuView.VratStlpec(cvti.data.Enums.AssuViewAvailableColumns.EKod6), "641014").
                AddCondition(new Equals(string.Empty, AssuView.VratStlpec(cvti.data.Enums.AssuViewAvailableColumns.StupenKod), "2"), cvti.data.Enums.ConditionOperator.And));

            var t3 = new CompoundCondition(string.Empty, new Equals(string.Empty, AssuView.VratStlpec(cvti.data.Enums.AssuViewAvailableColumns.EKod6), "721003").
                AddCondition(new Equals(string.Empty, AssuView.VratStlpec(cvti.data.Enums.AssuViewAvailableColumns.StupenKod), "2"), cvti.data.Enums.ConditionOperator.And));

            t1.AddCondition(t2, cvti.data.Enums.ConditionOperator.Or);
            t2.AddCondition(t3, cvti.data.Enums.ConditionOperator.Or);

            //var transferCondition = new CompoundCondition(string.Empty, t1, false);
            //transferCondition.Wrap = true;
            main.AddCondition(t1, cvti.data.Enums.ConditionOperator.And);

            Console.WriteLine(main.GetConditionString());
        }
    }
}
