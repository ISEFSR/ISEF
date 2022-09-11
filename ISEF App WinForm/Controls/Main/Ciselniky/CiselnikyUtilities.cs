namespace cvti.isef.winformapp.Controls.Main.Ciselniky
{
    using cvti.data;
    using cvti.data.Columns;
    using cvti.data.Conditions;
    using cvti.data.Functions;
    using cvti.data.Output;
    using cvti.data.Views;
    using cvti.data.Enums;
    using cvti.isef.winformapp.Forms;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    static class CiselnikyUtilities
    {
        public static AssuViewColumn[] VratStlpcePrePreview() =>
            new AssuViewColumn[]
                {
                    AssuView.VratStlpec(AssuViewAvailableColumns.SegmentKod, false, "Segment kód"),
                    AssuView.VratStlpec(AssuViewAvailableColumns.SegmentShort, true, "Segment skratený"),
                    AssuView.VratStlpec(AssuViewAvailableColumns.SegmentText, true, "Segment text"),
                    AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod, false, "Stupeň kód"),
                    AssuView.VratStlpec(AssuViewAvailableColumns.StupenShort, true, "Stupeň skrátený"),
                    AssuView.VratStlpec(AssuViewAvailableColumns.StupenText, true, "Stupeň text")
                };

        public static void CreateCondition(object sender, EventHandler<Tuple<Column, object, AvailableConditions>> e)
        {
            // Try to cast the sender to a MenuItem
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;

            var condition = (AvailableConditions)Convert.ToInt32(menuItem.Tag);

            if (menuItem != null)
            {
                // Retrieve the ContextMenuStrip that owns this ToolStripItem
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;

                if (owner != null)
                {
                    // Get the control that is displaying this context menu
                    Control sourceControl = owner.SourceControl;

                    if (sourceControl is TextBox)
                    {
                        var index = Convert.ToInt32((sourceControl as TextBox).Tag);
                        var clmn = (AssuViewAvailableColumns)index;
                        var clmnInstance = AssuView.VratStlpec(clmn);
                        e?.Invoke(null, new Tuple<Column, object, AvailableConditions>(clmnInstance,
                            (sourceControl as TextBox).Text, condition));
                    }
                    else if (sourceControl is TreeView)
                    {
                        // och toto je hrozny workaround ale nemam cas na nic ine teraz
                        var data = (sourceControl as TreeView).Tag.ToString().Split(';');
                        if (data.Length == 2)
                        {
                            if (int.TryParse(data[0], out int index))
                            {
                                var clmn = (AssuViewAvailableColumns)index;
                                var clmnInstance = AssuView.VratStlpec(clmn);
                                e?.Invoke(null, new Tuple<Column, object, AvailableConditions>(clmnInstance,
                                   data[1], condition));
                            }
                        }
                    }
                }
            }
        }
           
        public static async Task<bool> AktualizujCiselnik(AnalytickaEvidencia ae, ISEFDataManager manager, int year)
        {
            var availableYears = await manager.MSSQLManager.GetAvailableYearsAsync();
            using (var aktualizujCiselnik = new AktualizujCiselnikForm(
                ae, year, availableYears))
            {
                if (aktualizujCiselnik.ShowDialog() == DialogResult.OK)
                {
                    if (aktualizujCiselnik.FromDefault)
                    {
                        await manager.MSSQLManager.CiselnikyManager.UpdateClassifierFromDefaultAsync(
                            ae, year);

                        return true;
                    }
                    else
                    {
                        await manager.MSSQLManager.CiselnikyManager.UpdateClassifierFromYearAsync(
                            ae, year, aktualizujCiselnik.SelectedYear);

                        return true;
                    }
                }
            }

            return false;
        }

        public static async Task<bool> GenerujCiselnik(AnalytickaEvidencia ae, ISEFDataManager manager, int year)
        {
            var availableYears = await manager.MSSQLManager.GetAvailableYearsAsync();
            using (var generujCiselnik = new GenerujCiselnikForm(
                ae, year, availableYears))
            {
                if (generujCiselnik.ShowDialog() == DialogResult.OK)
                {
                    if (generujCiselnik.FromDefault)
                    {
                        MessageBox.Show("Generovanie číselníku z prednahratých dát momentálne nieje možné.", "Neimplementovaná funkcia.",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;   
                    }


                    await manager.MSSQLManager.CiselnikyManager.GenerujCiselnik(
                        ae, generujCiselnik.SelectedYear, year);

                    return true;
                }
            }
            return false;
        }

        public static async Task ShowPreviewForColumns(ISEFDataManager manager, IEnumerable<AssuViewColumn> clmns, int year, Condition cnd = null, string title = null)
        {
            var columns = new List<Column>(clmns);

            var vydavky = new GreaterThan("vydavky_cond", AssuView.VratStlpec(AssuViewAvailableColumns.EKod1), "5");
            var prijmy = new LessThan("prijmy_cond", AssuView.VratStlpec(AssuViewAvailableColumns.EKod1), "6");

            columns.AddRange(new Column[]
            {
                AssuView.VratStlpec(AssuViewAvailableColumns.Skut, true, "Výdavky").AddFunction(new ConditionalSum(vydavky)),
                AssuView.VratStlpec(AssuViewAvailableColumns.Skut, true, "Príjmy").AddFunction(new ConditionalSum(prijmy)),
                AssuView.VratStlpec(AssuViewAvailableColumns.Rozpp, true, "Schválený").AddFunction(new Sum()),
                AssuView.VratStlpec(AssuViewAvailableColumns.Rozpu, true, "Upravený").AddFunction(new Sum()),
            });

            var selectCommand = new SelectCommand(string.Empty, columns);

            selectCommand.CommandCondition = new Equals(string.Empty,
                AssuView.VratStlpec(AssuViewAvailableColumns.Rok), year);

            if (cnd != null)
            {
                selectCommand.CommandCondition.AddCondition(cnd, ConditionOperator.And);
            }

            var data = await manager.MSSQLManager.SelectData(selectCommand);

            // TODO: create form in Forms namespace
            using (var dataForm = new DataForm(data, selectCommand, title))
                dataForm.ShowDialog();
        }

        public static Condition GetCondition(Column clmn, object value, AvailableConditions cnd)
        {
            Condition condition = null;

            switch (cnd)
            {
                case AvailableConditions.Equals:
                    condition = new Equals(string.Empty, clmn, value);
                    break;
                case AvailableConditions.GreaterThan:
                    condition = new GreaterThan(string.Empty, clmn, value);
                    break;
                case AvailableConditions.LessThan:
                    condition = new LessThan(string.Empty, clmn, value);
                    break;
                case AvailableConditions.Inlist:
                    //condition = new Inlist(string.Empty, clmn, value);
                    break;
                default:
                    break;
            }

            return condition;
        }
    }
}
