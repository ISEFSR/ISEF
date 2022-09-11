namespace cvti.isef.winformapp
{
    using cvti.data.Core;
    using cvti.data.Files;
    using cvti.data.Output;
    using OfficeOpenXml;
    using System;
    using System.Linq;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using cvti.data.Conditions;
    using cvti.data.Enums;

    static class ControlUtilities
    {
        public static bool SaveToExcel(this DataGridView view, Hlavicka hlv, Condition cnd, string filePath)
        {
            // zatial iba pre data table
            using (var pckg = new ExcelPackage(new System.IO.FileInfo(filePath)))
            {
                //var source = view.DataSource as DataTable;
                //if (source is null)
                //    return false;

                var dataSheet = pckg.Workbook.Worksheets.Add(hlv.Name + "_data");
                var cmdSheet = pckg.Workbook.Worksheets.Add(hlv.Name + "_command");

                var cmd = hlv.GetCommand();
                cmd.CommandCondition.AddCondition(cnd, ConditionOperator.And);
                cmdSheet.Cells[1, 1].Value = cmd.GenerateCommand().CommandText;

                var clmnIndex = 1;
                foreach (DataGridViewColumn clmn in view.Columns)
                    dataSheet.Cells[1, clmnIndex++].Value = clmn.HeaderText;

                var rowIndex = 2;
                foreach (DataGridViewRow row in view.Rows)
                {
                    clmnIndex = 0;
                    while (clmnIndex < view.Columns.Count)
                        dataSheet.Cells[rowIndex, clmnIndex+1].Value = row.Cells[clmnIndex++].Value;
                    
                    rowIndex++;
                }

                pckg.Save();
                return true;
            }
        }

        public static Icon GetIconFromEmbeddedResource(string name, Size size)
        {
            var asm = System.Reflection.Assembly.GetExecutingAssembly();
            var rnames = asm.GetManifestResourceNames();
            var tofind = "." + name + ".ICO";
            foreach (string rname in rnames)
            {
                if (rname.EndsWith(tofind, StringComparison.CurrentCultureIgnoreCase))
                {
                    using (var stream = asm.GetManifestResourceStream(rname))
                    {
                        return new Icon(stream, size);
                    }
                }
            }
            throw new ArgumentException("Icon not found");
        }

        public static void DrawShadow(Control control, Graphics g, bool right = true, bool bottom = true)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            const int ShadowSize = 5;
            for (var i = 0; i < ShadowSize; i++)
            {
                // Right shadow
                if (right)
                g.DrawLine(new Pen(Color.FromArgb(140, 140, 140), 1.0f),
                    new Point(control.Location.X + control.Width + i, control.Location.Y + control.Height + i - 1),
                    new Point(control.Location.X + control.Width + i, control.Location.Y + i));
                // Bottom shadow
                if (bottom)
                g.DrawLine(new Pen(Color.FromArgb(140, 140, 140), 1.0f),
                    new Point(control.Location.X + i, control.Location.Y + control.Height + i),
                    new Point(control.Location.X + control.Width + i, control.Location.Y + control.Height + i));
            }
        }

        public static void AlterConditionsComboBox(ComboBox cbConditions, ConditionsManagerJson conditionsManager)
        {
            foreach (var cnd in conditionsManager.Values)
                cbConditions.Items.Add(cnd);

            if (cbConditions.Items.Count > 0)
                cbConditions.SelectedIndex = 0;

            EventHandler<Condition> conditionCreated = (snd, ea) =>
            {
                if (cbConditions != null)
                    cbConditions.Items.Add(ea);
            };

            EventHandler<Condition> conditionRemoved = (snd, ea) =>
            {
                if (cbConditions != null)
                    cbConditions.Items.Remove(ea);
            };

            conditionsManager.NewValueAdded += conditionCreated;
            conditionsManager.ValueRemoved += conditionRemoved;

            cbConditions.Disposed += (snd, ea) =>
            {
                conditionsManager.NewValueAdded -= conditionCreated;
                conditionsManager.ValueRemoved -= conditionRemoved;
            };
        }

        public static ComboBox CreateConditionsComboBox(ConditionsManagerJson conditionsManager)
        {
            var cbConditions = new ComboBox();

            AlterConditionsComboBox(cbConditions, conditionsManager);

            return cbConditions;
        }

        public static void SetDoubleBuffered(this Control ctrl)
        {
            ctrl.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(ctrl, true);
        }
    }
}