using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Collections;

namespace ISEF_Rozbory
{
    class Zostavy_VVS : Zostavy
    {
        public void VVS_1_2_3_typ(string kategoria, bool podla_organizacii, string typ, string datum, int typVystupov, string zaokruhlenieMena, string zaokruhleniePercenta, char left_char, int left_num, char right_char, int right_num, int count_for_page, int strana_rel_pos, DateTime time, bool priamaTlac)
        {
            //podla_organizacii - true/false
            string selectString = "";
            string connectionstring = ConfigurationManager.ConnectionStrings[connection].ConnectionString;

            using (var Con = new SqlConnection(connectionstring))
            {
                Con.Open();
                string nameTabulka = "VVS_" + kategoria + "_111" + (podla_organizacii ? "a" : "");
                string hlav = "hlzu10cv3.xls";

                string kvartal = vytvorAdresare(datum);
                string name = nameTabulka + ".xls";
                System.IO.File.Copy("..\\..\\Hlav\\ZU\\" + hlav, "Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name, true);
                Excel.Application app = new Excel.Application();
                string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                Excel.Workbook workbook = app.Workbooks.Open(path + "\\" + "bin\\Debug\\Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name);
                Excel._Worksheet worksheet = workbook.Sheets[1];//pre citanie excelu
                Excel.Range range = worksheet.UsedRange;//pre citanie excelu
                Excel.Range borderRange;
                int rowCount = range.Rows.Count;//pre citanie excelu
                int start = right_num + 1;
                int border = start;
                int strana = 2;

                worksheet.Name = nameTabulka;
                worksheet.Cells[1, 1] = "Podrobný prehľad rozpočtu a čerpania na zdroji 111 v rámci položiek";
                worksheet.Cells[2, 1] = "kategórie " + kategoria.Substring(0, 3) + " " + kategoria.Substring(3) + " – Transfer VVŠ" + (podla_organizacii ? " podľa organizácií" : "");
                worksheet.Cells[3, 1] = "k " + datum + " (€)";
                worksheet.Cells[5, 3] = worksheet.Cells[5, 3].Value2.ToString() + " " + datum.Substring(6);
                worksheet.Cells[5, 4] = worksheet.Cells[5, 4].Value2.ToString() + " " + datum;
                worksheet.Cells[5, 5] = worksheet.Cells[5, 5].Value2.ToString() + " " + datum;

                List<string> icoList = new List<string>();
                if (podla_organizacii)
                {
                    SqlCommand com = new SqlCommand("SELECT DISTINCT tv.Ico,ci.CelyNazov collate Slovak_CI_AS FROM TableVstup tv JOIN ciselnikIco ci ON tv.Ico=ci.Ico WHERE tv.Zdroj LIKE '111' AND tv.Podpolozka LIKE '" + kategoria + "' AND tv.SyntetickyaleboFiktivny LIKE @typ AND KalendarnyDen LIKE @Datum ORDER BY ci.CelyNazov collate Slovak_CI_AS", Con);
                    com.Parameters.AddWithValue("@typ", typ);
                    com.Parameters.AddWithValue("@Datum", datum);

                    DataTable dT = new DataTable();
                    using (SqlDataAdapter da = new SqlDataAdapter(com))
                    {
                        da.Fill(dT);
                        com.ExecuteNonQuery();
                    }

                    foreach (DataRow row in dT.Rows) // Loop over the rows.
                        icoList.Add((string)row[0]);
                }
                else
                    icoList.Add("%");

                foreach (string ico in icoList)
                {
                    if (podla_organizacii)
                    {
                        Excel.Range range_nadpis = range.get_Range("" + left_char + start.ToString(), "" + right_char + start.ToString());
                        range_nadpis.Merge();
                        range_nadpis.Font.Bold = true;

                        SqlCommand get_name = new SqlCommand("SELECT CelyNazov from ciselnikIco where Ico = '" + ico + "'", Con);
                        DataTable get_name_table = new DataTable();
                        using (SqlDataAdapter get_name_adapter = new SqlDataAdapter(get_name))
                        {
                            get_name_adapter.Fill(get_name_table);
                            get_name.ExecuteNonQuery();
                        }

                        if (get_name_table.Rows.Count != 0)
                            worksheet.Cells[start, 1] = (string)get_name_table.Rows[0][0];
                        else
                            worksheet.Cells[start, 1] = ico;

                        borderRange = worksheet.get_Range(left_char.ToString() + (start - 1), right_char.ToString() + start);//hrube po Celkovo
                        borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 8, 11);
                    }

                    SqlCommand programy_cmd = new SqlCommand("Select DISTINCT PP.Prog, PP.Text collate Slovak_CI_AS FROM TableVstup tv JOIN ciselnikProjektPrvok PP ON tv.ProjektPrvok=PP.Prog WHERE tv.Zdroj LIKE '111' AND tv.Podpolozka LIKE '" + kategoria + "' AND tv.SyntetickyaleboFiktivny LIKE @typ AND KalendarnyDen LIKE @Datum AND Ico LIKE @Ico ORDER BY PP.Prog, PP.Text collate Slovak_CI_AS", Con);
                    programy_cmd.Parameters.AddWithValue("@Ico", ico);
                    programy_cmd.Parameters.AddWithValue("@typ", typ);
                    programy_cmd.Parameters.AddWithValue("@Datum", datum);

                    Dictionary<string, string> programyMap = new Dictionary<string, string>();
                    DataTable programy_table = new DataTable();
                    using (SqlDataAdapter programy_cmd_adapter = new SqlDataAdapter(programy_cmd))
                    {
                        programy_cmd_adapter.Fill(programy_table);
                        programy_cmd.ExecuteNonQuery();
                    }
                    foreach (DataRow row in programy_table.Rows) // Loop over the rows.
                        programyMap.Add(row[0].ToString().Trim(), row[1].ToString().Trim());
                    programyMap.Add("-1", "Spolu");

                    foreach (var program in programyMap)
                    {
                        DataTable dT = new DataTable();
                        SqlCommand com;
                        if (program.Key != "-1")
                        {
                            int valid_len = pocet_znakov_bez_nul(program.Key);
                            com = new SqlCommand("SELECT SUM(SchvalenyRozpocet),SUM(RozpocetPoZmenach),SUM(Skutocnost), Count(*) FROM TableVstup WHERE Substring(ProjektPrvok, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program.Key + "', 1, " + valid_len.ToString() + ") AND Zdroj LIKE '111' AND Podpolozka LIKE '" + kategoria + "' AND SyntetickyaleboFiktivny LIKE @typ AND KalendarnyDen LIKE @Datum AND Ico LIKE @Ico", Con);

                            worksheet.Cells[start, 1].NumberFormat = "@";
                            worksheet.Cells[start, 1] = program.Key.ToString().Substring(0, valid_len);
                        }
                        else
                        {
                            com = new SqlCommand("SELECT SUM(SchvalenyRozpocet),SUM(RozpocetPoZmenach),SUM(Skutocnost), Count(*) FROM TableVstup WHERE Zdroj LIKE '111' AND Podpolozka LIKE '" + kategoria + "' AND SyntetickyaleboFiktivny LIKE @typ AND KalendarnyDen LIKE @Datum AND Ico LIKE @Ico", Con);
                            Excel.Range range_riadok = worksheet.get_Range("B" + start, right_char.ToString() + start);
                            range_riadok.Font.Bold = true;
                            range_riadok.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                        }

                        worksheet.Cells[start, 2] = program.Value;

                        com.Parameters.AddWithValue("@Ico", ico);
                        com.Parameters.AddWithValue("@typ", typ);
                        com.Parameters.AddWithValue("@Datum", datum);

                        using (SqlDataAdapter da = new SqlDataAdapter(com))
                        {
                            da.Fill(dT);
                            com.ExecuteNonQuery();
                        }

                        if (Double.Parse(dT.Rows[0][3].ToString()) == 0)
                            continue;

                        for (int k = 0; k < 3; k++)
                        {
                            double value = 0;
                            value = Double.Parse(dT.Rows[0][k].ToString()) / typVystupov;
                            if (value != 0)
                                worksheet.Cells[start, 3 + k] = value;
                            else
                                worksheet.Cells[start, 3 + k] = 0;
                        }

                        double schvalenyRoz = Double.Parse(dT.Rows[0][0].ToString()) / typVystupov;
                        double upravenyRoz = Double.Parse(dT.Rows[0][1].ToString()) / typVystupov;
                        double plneniePrijmov = Double.Parse(dT.Rows[0][2].ToString()) / typVystupov;

                        if (plneniePrijmov == 0 || schvalenyRoz == 0)
                            worksheet.Cells[start, 6] = 0;
                        else
                            worksheet.Cells[start, 6] = 100 * (double)(plneniePrijmov / schvalenyRoz);

                        if (plneniePrijmov == 0 || upravenyRoz == 0)
                            worksheet.Cells[start, 7] = 0;
                        else
                            worksheet.Cells[start, 7] = 100 * (double)(plneniePrijmov / upravenyRoz);

                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 8, 11);
                    }

                    for (char c = left_char; c <= (char)((int)right_char); c++)//tenke oramovanie od border po start
                    {
                        borderRange = worksheet.get_Range(c.ToString() + border, c.ToString() + (start - 1));
                        borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                        //ZAOKRUHLOVANIE
                        //CISLA
                        if (c > (left_char + 1) && c < left_char + (right_char - left_char) - 1)
                        {
                            borderRange.NumberFormat = zaokruhlenieMena;
                            borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                        }
                        //PERCENTA
                        else
                        {
                            if (c > left_char + (right_char - left_char) - 2)
                            {
                                borderRange.NumberFormat = zaokruhleniePercenta;
                                borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                            }
                        }
                    }

                    borderRange = worksheet.get_Range(left_char.ToString() + border, right_char.ToString() + (start - 1));//hrube po Celkovo
                    borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                    selectString += "A" + border.ToString() + ":" + right_char + start.ToString() + System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator;
                }

                selectString = selectString.Remove(selectString.Length - 1);
                //selectString += "A" + border.ToString() + ":" + right_char + start.ToString();
                Excel.Range selectedRange = worksheet.Range[selectString].Columns[String.Concat("A:", right_char.ToString())];
                selectedRange.Columns.AutoFit();
                foreach (Excel.Range column in selectedRange.Columns)
                {
                    if (column.ColumnWidth < 10.86)//sirka stlpca tak jak ju ukazuje excel
                        column.ColumnWidth = 10.86;
                }

                var culture = new CultureInfo("ru-RU");
                worksheet.Cells[start, 1] = "Dátum vytvorenia: " + time.ToString(culture);
                worksheet.Cells[start, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

                workbook.Save();
                if (priamaTlac)
                    PrintMyExcelFile(workbook);
                Kontrola.release_excel(range, worksheet, workbook, app);
                Con.Close();
            }
        }

        public void VVS_4_5_typ(List<string> kategorie, bool bez_mrz, string typ, string datum, int typVystupov, string zaokruhlenieMena, string zaokruhleniePercenta, char left_char, int left_num, char right_char, int right_num, int count_for_page, int strana_rel_pos, DateTime time, bool priamaTlac)
        {
            string selectString = "";
            string connectionstring = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            string vynimky = vynimkyMRZ(connectionstring);

            using (var Con = new SqlConnection(connectionstring))
            {
                Con.Open();
                string nameTabulka = "VVS_" + String.Join("_", kategorie) + (bez_mrz ? "_bez_MRZ" : "_111");
                string hlav = "hlzu13_p67cv3.xls";

                string kvartal = vytvorAdresare(datum);
                string name = nameTabulka + ".xls";
                System.IO.File.Copy("..\\..\\Hlav\\ZU\\" + hlav, "Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name, true);
                Excel.Application app = new Excel.Application();
                string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                Excel.Workbook workbook = app.Workbooks.Open(path + "\\" + "bin\\Debug\\Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name);
                Excel._Worksheet worksheet = workbook.Sheets[1];//pre citanie excelu
                Excel.Range range = worksheet.UsedRange;//pre citanie excelu
                Excel.Range borderRange;
                int rowCount = range.Rows.Count;//pre citanie excelu
                int start = right_num + 1;
                int border = start;
                int strana = 2;

                worksheet.Name = nameTabulka;
                worksheet.Cells[2, 1] = "Prehľad rozpočtových programov položka " + String.Join(", ", kategorie) + (bez_mrz ? " bez iných zdrojov k " : " zdroj 111 k ") + datum + " (€)";
                worksheet.Cells[4, 3] = worksheet.Cells[4, 3].Value2.ToString() + " " + datum.Substring(6);
                worksheet.Cells[4, 5] = worksheet.Cells[4, 5].Value2.ToString() + " " + datum;
                worksheet.Cells[4, 7] = worksheet.Cells[4, 7].Value2.ToString() + " " + datum;

                ArrayList pool = new ArrayList();
                ArrayList stack = new ArrayList();

                SqlCommand startovacie = new SqlCommand("Select Prog From ciselnikProjektPrvok WHERE Znacka = 0", Con);
                DataTable startovacie_table = new DataTable();
                using (SqlDataAdapter startovacie_adapter = new SqlDataAdapter(startovacie))
                {
                    startovacie_adapter.Fill(startovacie_table);
                    startovacie.ExecuteNonQuery();
                }
                foreach (DataRow row in startovacie_table.Rows) // Loop over the rows.
                {
                    pool.Add(row[0].ToString());
                }

                while (pool.Count > 0)
                {
                    int valid_len = pocet_znakov_bez_nul(pool[0].ToString());
                    //AND SUBSTRING(Kategoria, 1, 1) IN('1', '2', '3', '4', '5')
                    //string command = "SELECT DISTINCT Prog FROM ciselnikProjektPrvok JOIN TableVstup tv ON tv.ProjektPrvok=Prog WHERE tv.Zdroj LIKE @Zdroj AND tv.Ico LIKE @Ico AND SUBSTRING(Prog, 1, " + valid_len.ToString() + ") LIKE SUBSTRING('" + pool[0].ToString() + "', 1, " + valid_len.ToString() + ") AND Prog != '" + pool[0].ToString() + "'";
                    string command = "SELECT DISTINCT Prog FROM ciselnikProjektPrvok  WHERE  SUBSTRING(Prog, 1, " + valid_len.ToString() + ") LIKE SUBSTRING('" + pool[0].ToString() + "', 1, " + valid_len.ToString() + ") AND Prog != '" + pool[0].ToString() + "'";
                    SqlCommand children = new SqlCommand(command, Con);
                    //children.Parameters.AddWithValue("@Ico", arrIco[icoI] + "%");
                    //children.Parameters.AddWithValue("@Zdroj", arrZdroje[k] + "%");
                    DataTable children_table = new DataTable();
                    DataTable sortedDT;
                    using (SqlDataAdapter children_adapter = new SqlDataAdapter(children))
                    {
                        children_adapter.Fill(children_table);
                        children.ExecuteNonQuery();
                        DataView dv = children_table.DefaultView;
                        dv.Sort = "Prog DESC";
                        sortedDT = dv.ToTable();
                    }

                    int worthy_children = 0;
                    foreach (DataRow row in sortedDT.Rows)
                    { // Loop over the rows.

                        if (!stack.Contains(row[0].ToString()) && !stack.Contains(row[0]))
                            worthy_children++;
                    }
                    if (worthy_children == 0)//ci ma dany program nejakych substring potomkov ktore este nie su v stacku
                    {
                        if (!stack.Contains(pool[0]))
                            stack.Add(pool[0]);
                        pool.RemoveAt(0);
                    }
                    else
                        foreach (DataRow row in sortedDT.Rows)
                            pool.Insert(0, row[0]);
                }
                stack.Add("%");

                List<string> icoList = new List<string>();
                SqlCommand com = null;
                if (!bez_mrz)
                    com = new SqlCommand("SELECT DISTINCT tv.Ico,ci.CelyNazov collate Slovak_CI_AS FROM TableVstup tv JOIN ciselnikIco ci ON tv.Ico=ci.Ico WHERE tv.Zdroj LIKE '111' AND (tv.Podpolozka LIKE '" + String.Join("' OR tv.Podpolozka LIKE '", kategorie) + "') AND tv.SyntetickyaleboFiktivny LIKE @typ AND KalendarnyDen LIKE @Datum ORDER BY ci.CelyNazov collate Slovak_CI_AS", Con);
                else
                    com = new SqlCommand("SELECT DISTINCT tv.Ico,ci.CelyNazov collate Slovak_CI_AS FROM TableVstup tv JOIN ciselnikIco ci ON tv.Ico=ci.Ico WHERE (tv.Podpolozka LIKE '" + String.Join("' OR tv.Podpolozka LIKE '", kategorie) + "') AND tv.SyntetickyaleboFiktivny LIKE @typ AND KalendarnyDen LIKE @Datum " + vynimky + "ORDER BY ci.CelyNazov collate Slovak_CI_AS", Con);
                com.Parameters.AddWithValue("@typ", typ);
                com.Parameters.AddWithValue("@Datum", datum);

                DataTable dT = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(com))
                {
                    da.Fill(dT);
                    com.ExecuteNonQuery();
                }

                foreach (DataRow row in dT.Rows) // Loop over the rows.
                    icoList.Add((string)row[0]);


                foreach (string ico in icoList)
                {
                    Excel.Range range_nadpis = range.get_Range("" + left_char + start.ToString(), "" + right_char + start.ToString());
                    range_nadpis.Merge();
                    range_nadpis.Font.Bold = true;

                    SqlCommand get_name = new SqlCommand("SELECT CelyNazov from ciselnikIco where Ico = '" + ico + "'", Con);
                    DataTable get_name_table = new DataTable();
                    using (SqlDataAdapter get_name_adapter = new SqlDataAdapter(get_name))
                    {
                        get_name_adapter.Fill(get_name_table);
                        get_name.ExecuteNonQuery();
                    }

                    if (get_name_table.Rows.Count != 0)
                        worksheet.Cells[start, 1] = (string)get_name_table.Rows[0][0];
                    else
                        worksheet.Cells[start, 1] = ico;

                    borderRange = worksheet.get_Range(left_char.ToString() + (start - 1), right_char.ToString() + start);//hrube po Celkovo
                    borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                    increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 8, 11);

                    foreach (string program in stack)
                    {
                        int valid_len = pocet_znakov_bez_nul(program.ToString());
                        SqlCommand com_prve = null;
                        SqlCommand com_druhe = null;
                        if (!bez_mrz)
                        {
                            com_prve = new SqlCommand("SELECT SUM(SchvalenyRozpocet),SUM(RozpocetPoZmenach),SUM(Skutocnost), Count(*) FROM TableVstup tv WHERE Substring(tv.ProjektPrvok, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND tv.Zdroj LIKE '111' AND tv.Podpolozka LIKE '" + kategorie[0] + "' AND tv.SyntetickyaleboFiktivny LIKE @typ AND tv.KalendarnyDen LIKE @Datum AND tv.Ico LIKE @Ico", Con);
                            com_druhe = new SqlCommand("SELECT SUM(SchvalenyRozpocet),SUM(RozpocetPoZmenach),SUM(Skutocnost), Count(*) FROM TableVstup tv WHERE Substring(tv.ProjektPrvok, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND tv.Zdroj LIKE '111' AND tv.Podpolozka LIKE '" + kategorie[1] + "' AND tv.SyntetickyaleboFiktivny LIKE @typ AND tv.KalendarnyDen LIKE @Datum AND tv.Ico LIKE @Ico", Con);
                        }
                        else
                        {
                            com_prve = new SqlCommand("SELECT SUM(SchvalenyRozpocet),SUM(RozpocetPoZmenach),SUM(Skutocnost), Count(*) FROM TableVstup tv WHERE Substring(tv.ProjektPrvok, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND tv.Podpolozka LIKE '" + kategorie[0] + "' AND tv.SyntetickyaleboFiktivny LIKE @typ AND tv.KalendarnyDen LIKE @Datum AND tv.Ico LIKE @Ico " + vynimky, Con);
                            com_druhe = new SqlCommand("SELECT SUM(SchvalenyRozpocet),SUM(RozpocetPoZmenach),SUM(Skutocnost), Count(*) FROM TableVstup tv WHERE Substring(tv.ProjektPrvok, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND tv.Podpolozka LIKE '" + kategorie[1] + "' AND tv.SyntetickyaleboFiktivny LIKE @typ AND tv.KalendarnyDen LIKE @Datum AND tv.Ico LIKE @Ico " + vynimky, Con);
                        }
                        com_prve.Parameters.AddWithValue("@typ", typ);
                        com_prve.Parameters.AddWithValue("@Datum", datum);
                        com_prve.Parameters.AddWithValue("@Ico", ico);

                        com_druhe.Parameters.AddWithValue("@typ", typ);
                        com_druhe.Parameters.AddWithValue("@Datum", datum);
                        com_druhe.Parameters.AddWithValue("@Ico", ico);

                        DataTable prve_table = new DataTable();
                        DataTable druhe_table = new DataTable();
                        using (SqlDataAdapter com_prve_adapter = new SqlDataAdapter(com_prve))
                        using (SqlDataAdapter com_druhe_adapter = new SqlDataAdapter(com_druhe))
                        {
                            com_prve_adapter.Fill(prve_table);
                            com_druhe_adapter.Fill(druhe_table);
                            com_prve.ExecuteNonQuery();
                            com_druhe.ExecuteNonQuery();
                        }

                        if ((int)prve_table.Rows[0][3] > 0 || (int)druhe_table.Rows[0][3] > 0)
                        {
                            worksheet.Cells[start, 1].NumberFormat = "@";
                            if (program != "%")
                            {
                                worksheet.Cells[start, 1] = program.ToString().Substring(0, valid_len);
                                var _projekt_text_cmd = new SqlCommand("SELECT Text FROM ciselnikProjektPrvok WHERE Prog LIKE '" + program + "'", Con);
                                string _projekt_text = Convert.ToString(_projekt_text_cmd.ExecuteScalar());
                                worksheet.Cells[start, 2] = _projekt_text;
                            }
                            else
                            {
                                Excel.Range range_riadok = worksheet.get_Range("B" + start, right_char.ToString() + start);
                                range_riadok.Font.Bold = true;
                                range_riadok.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                                worksheet.Cells[start, 2] = "Spolu";
                            }

                            foreach (DataRow row in prve_table.Rows)
                            {
                                for (int rowCounter = 0; rowCounter <= 2; rowCounter++)
                                {
                                    if (row[rowCounter].ToString() != "")
                                        worksheet.Cells[start, rowCounter * 2 + 3] = (double)row[rowCounter] / typVystupov;
                                    else
                                        worksheet.Cells[start, rowCounter * 2 + 3] = 0;
                                }
                                if (row[0].ToString() != "")
                                    worksheet.Cells[start, 9] = ((double)row[2] / (double)row[0]) * 100;
                                else
                                    worksheet.Cells[start, 9] = 0;
                                if (row[1].ToString() != "")
                                    worksheet.Cells[start, 11] = ((double)row[2] / (double)row[1]) * 100;
                                else
                                    worksheet.Cells[start, 11] = 0;
                            }

                            foreach (DataRow row in druhe_table.Rows)
                            {
                                for (int rowCounter = 0; rowCounter <= 2; rowCounter++)
                                {
                                    if (row[rowCounter].ToString() != "")
                                        worksheet.Cells[start, rowCounter * 2 + 4] = (double)row[rowCounter] / typVystupov;
                                    else
                                        worksheet.Cells[start, rowCounter * 2 + 4] = 0;
                                }
                                if (row[0].ToString() != "")
                                    worksheet.Cells[start, 10] = ((double)row[2] / (double)row[0]) * 100;
                                else
                                    worksheet.Cells[start, 10] = 0;
                                if (row[1].ToString() != "")
                                    worksheet.Cells[start, 12] = ((double)row[2] / (double)row[1]) * 100;
                                else
                                    worksheet.Cells[start, 12] = 0;
                            }

                            increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 8, 11);
                        }
                    }
                    for (char c = left_char; c <= (char)((int)right_char); c++)//tenke oramovanie od border po start
                    {
                        borderRange = worksheet.get_Range(c.ToString() + border, c.ToString() + (start - 1));
                        borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                        //ZAOKRUHLOVANIE
                        //CISLA
                        if (c > (left_char + 1) && c < left_char + (right_char - left_char) - 3)
                        {
                            borderRange.NumberFormat = zaokruhlenieMena;
                            borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                        }
                        //PERCENTA
                        else
                        {
                            if (c > left_char + (right_char - left_char) - 4)
                            {
                                borderRange.NumberFormat = zaokruhleniePercenta;
                                borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                            }
                        }
                    }

                    borderRange = worksheet.get_Range(left_char.ToString() + border, right_char.ToString() + (start - 1));//hrube po Celkovo
                    borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                    selectString += "A" + border.ToString() + ":" + right_char + start.ToString() + System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator;
                }
                selectString = selectString.Remove(selectString.Length - 1);
                //selectString += "A" + border.ToString() + ":" + right_char + start.ToString();
                Excel.Range selectedRange = worksheet.Range[selectString].Columns[String.Concat("A:", right_char.ToString())];
                selectedRange.Columns.AutoFit();
                foreach (Excel.Range column in selectedRange.Columns)
                {
                    if (column.ColumnWidth < 10.86)//sirka stlpca tak jak ju ukazuje excel
                        column.ColumnWidth = 10.86;
                }

                var culture = new CultureInfo("ru-RU");
                worksheet.Cells[start, 1] = "Dátum vytvorenia: " + time.ToString(culture);
                worksheet.Cells[start, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;


                workbook.Save();
                if (priamaTlac)
                    PrintMyExcelFile(workbook);
                Kontrola.release_excel(range, worksheet, workbook, app);
                Con.Close();
            }
        }

        public void VVS_small(List<string> kategorie, string typ, string datum, int typVystupov, string zaokruhlenieMena, string zaokruhleniePercenta, char left_char, int left_num, char right_char, int right_num, int count_for_page, int strana_rel_pos, DateTime time, bool priamaTlac)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            string vynimky = vynimkyMRZ(connectionstring);

            using (var Con = new SqlConnection(connectionstring))
            {
                Con.Open();
                string nameTabulka = "vš";
                string hlav = "hl_vvs.xls";
                string nadpis = "VVŠ " + datum.Substring(6);

                string kvartal = vytvorAdresare(datum);
                string name = nameTabulka + ".xls";
                System.IO.File.Copy("..\\..\\Hlav\\" + hlav, "Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name, true);
                Excel.Application app = new Excel.Application();
                string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                Excel.Workbook workbook = app.Workbooks.Open(path + "\\" + "bin\\Debug\\Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name);
                Excel._Worksheet worksheet = workbook.Sheets[1];//pre citanie excelu
                Excel.Range range = worksheet.UsedRange;//pre citanie excelu
                Excel.Range borderRange;
                int rowCount = range.Rows.Count;//pre citanie excelu
                int start = right_num + 1;
                int border = start;
                int strana = 2;

                worksheet.Name = nameTabulka;
                worksheet.Cells[1, 1] = nadpis;

                SqlCommand com = new SqlCommand("SELECT DISTINCT tv.Ico FROM TableVstup tv WHERE (tv.Podpolozka LIKE '" + String.Join("' OR tv.Podpolozka LIKE '", kategorie) + "') AND tv.SyntetickyaleboFiktivny LIKE @typ AND KalendarnyDen LIKE @Datum", Con);
                com.Parameters.AddWithValue("@typ", typ);
                com.Parameters.AddWithValue("@Datum", datum);

                DataTable dT = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(com))
                {
                    da.Fill(dT);
                    com.ExecuteNonQuery();
                }

                List<string> icoList = new List<string>();
                foreach (DataRow row in dT.Rows) // Loop over the rows.
                    icoList.Add((string)row[0]);

                Excel.Range range_riadok = null;
                double[] sum_all = new double[3];
                foreach (string ico in icoList)
                {
                    double[] sum_ico = new double[3];
                    com = new SqlCommand("SELECT DISTINCT tv.Zdroj FROM TableVstup tv WHERE (tv.Podpolozka LIKE '" + String.Join("' OR tv.Podpolozka LIKE '", kategorie) + "') AND tv.SyntetickyaleboFiktivny LIKE @typ AND KalendarnyDen LIKE @Datum AND tv.Ico LIKE '" + ico.Trim() + "'", Con);
                    com.Parameters.AddWithValue("@typ", typ);
                    com.Parameters.AddWithValue("@Datum", datum);

                    dT = new DataTable();
                    using (SqlDataAdapter da = new SqlDataAdapter(com))
                    {
                        da.Fill(dT);
                        com.ExecuteNonQuery();
                    }

                    List<string> zdrojList = new List<string>();
                    foreach (DataRow row in dT.Rows) // Loop over the rows.
                        zdrojList.Add((string)row[0]);

                    string last_zdroj = "";

                    foreach (string zdroj in zdrojList)
                    {
                        last_zdroj = zdroj;
                        foreach (string kategoria in kategorie)
                        {
                            com = new SqlCommand("SELECT DISTINCT tv.ProjektPrvok FROM TableVstup tv WHERE tv.Zdroj LIKE '" + zdroj.Trim() + "' AND tv.Podpolozka LIKE '" + kategoria + "' AND tv.SyntetickyaleboFiktivny LIKE @typ AND tv.KalendarnyDen LIKE @Datum AND tv.Ico LIKE @Ico ORDER BY tv.ProjektPrvok", Con);
                            com.Parameters.AddWithValue("@Ico", ico);
                            com.Parameters.AddWithValue("@typ", typ);
                            com.Parameters.AddWithValue("@Datum", datum);

                            dT = new DataTable();
                            using (SqlDataAdapter da = new SqlDataAdapter(com))
                            {
                                da.Fill(dT);
                                com.ExecuteNonQuery();
                            }

                            List<string> projektList = new List<string>();
                            foreach (DataRow row in dT.Rows) // Loop over the rows.
                                projektList.Add((string)row[0]);

                            foreach (string projektPrvok in projektList)
                            {
                                com = new SqlCommand("SELECT SUM(SchvalenyRozpocet),SUM(RozpocetPoZmenach),SUM(Skutocnost), Count(*) FROM TableVstup tv WHERE tv.Zdroj LIKE '" + zdroj.Trim() + "' AND tv.Podpolozka LIKE '" + kategoria + "' AND tv.SyntetickyaleboFiktivny LIKE @typ AND tv.KalendarnyDen LIKE @Datum AND tv.Ico LIKE @Ico AND tv.ProjektPrvok LIKE @ProjektPrvok", Con);
                                com.Parameters.AddWithValue("@Ico", ico);
                                com.Parameters.AddWithValue("@typ", typ);
                                com.Parameters.AddWithValue("@Datum", datum);
                                com.Parameters.AddWithValue("@ProjektPrvok", projektPrvok);

                                dT = new DataTable();
                                using (SqlDataAdapter da = new SqlDataAdapter(com))
                                {
                                    da.Fill(dT);
                                    com.ExecuteNonQuery();
                                }

                                if (Double.Parse(dT.Rows[0][3].ToString()) == 0)
                                    continue;

                                int valid_len = pocet_znakov_bez_nul(projektPrvok.ToString());

                                worksheet.Cells[start, 1].NumberFormat = "@";
                                worksheet.Cells[start, 1] = ico;
                                worksheet.Cells[start, 2] = zdroj;
                                worksheet.Cells[start, 3] = kategoria;
                                worksheet.Cells[start, 4].NumberFormat = "@";
                                worksheet.Cells[start, 4] = projektPrvok.Substring(0, valid_len);
                                worksheet.Cells[start, 5] = Double.Parse(dT.Rows[0][0].ToString()) / typVystupov;
                                worksheet.Cells[start, 6] = Double.Parse(dT.Rows[0][1].ToString()) / typVystupov;
                                worksheet.Cells[start, 7] = Double.Parse(dT.Rows[0][2].ToString()) / typVystupov;
                                increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 8, 11);

                                sum_ico[0] += Double.Parse(dT.Rows[0][0].ToString()) / typVystupov;
                                sum_ico[1] += Double.Parse(dT.Rows[0][1].ToString()) / typVystupov;
                                sum_ico[2] += Double.Parse(dT.Rows[0][2].ToString()) / typVystupov;
                            }
                        }

                        if (zdroj == "111")
                        {
                            com = new SqlCommand("SELECT SUM(SchvalenyRozpocet),SUM(RozpocetPoZmenach),SUM(Skutocnost), Count(*) FROM TableVstup tv WHERE (tv.Podpolozka LIKE '" + String.Join("' OR tv.Podpolozka LIKE '", kategorie) + "') AND tv.Zdroj LIKE '" + zdroj.Trim() + "' AND tv.SyntetickyaleboFiktivny LIKE @typ AND tv.KalendarnyDen LIKE @Datum AND tv.Ico LIKE @Ico", Con);
                            com.Parameters.AddWithValue("@Ico", ico);
                            com.Parameters.AddWithValue("@typ", typ);
                            com.Parameters.AddWithValue("@Datum", datum);

                            dT = new DataTable();
                            using (SqlDataAdapter da = new SqlDataAdapter(com))
                            {
                                da.Fill(dT);
                                com.ExecuteNonQuery();
                            }

                            if (Double.Parse(dT.Rows[0][3].ToString()) == 0)
                                continue;
                            start--;
                            range_riadok = worksheet.get_Range(right_char.ToString() + start, right_char.ToString() + start);
                            range_riadok.Font.Bold = true;
                            range_riadok.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                            //worksheet.Cells[start, 1] = "Spolu za 111";
                            worksheet.Cells[start, 8] = Double.Parse(dT.Rows[0][2].ToString()) / typVystupov;

                            increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 8, 11);
                            borderRange = worksheet.get_Range(left_char.ToString() + border, right_char.ToString() + (start - 1));
                            borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                        }
                    }

                    if (last_zdroj != "111")
                    {
                        com = new SqlCommand("SELECT SUM(SchvalenyRozpocet),SUM(RozpocetPoZmenach),SUM(Skutocnost), Count(*) FROM TableVstup tv WHERE (tv.Podpolozka LIKE '" + String.Join("' OR tv.Podpolozka LIKE '", kategorie) + "') AND tv.Zdroj NOT LIKE '111' AND tv.SyntetickyaleboFiktivny LIKE @typ AND tv.KalendarnyDen LIKE @Datum AND tv.Ico LIKE @Ico", Con);
                        com.Parameters.AddWithValue("@Ico", ico);
                        com.Parameters.AddWithValue("@typ", typ);
                        com.Parameters.AddWithValue("@Datum", datum);

                        dT = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(com))
                        {
                            da.Fill(dT);
                            com.ExecuteNonQuery();
                        }

                        if (Double.Parse(dT.Rows[0][3].ToString()) == 0)
                            continue;

                        start--;
                        range_riadok = worksheet.get_Range(right_char.ToString() + start, right_char.ToString() + start);
                        range_riadok.Font.Bold = true;
                        range_riadok.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                        //worksheet.Cells[start, 1] = "Spolu za 111";
                        worksheet.Cells[start, 8] = Double.Parse(dT.Rows[0][2].ToString()) / typVystupov;

                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 8, 11);
                        borderRange = worksheet.get_Range(left_char.ToString() + border, right_char.ToString() + (start - 1));
                        borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                        range_riadok = worksheet.get_Range(left_char.ToString() + start, right_char.ToString() + start);
                        range_riadok.Font.Bold = true;
                        worksheet.Cells[start, 1] = "Spolu";
                        worksheet.Cells[start, 5] = sum_ico[0];
                        worksheet.Cells[start, 6] = sum_ico[1];
                        worksheet.Cells[start, 7] = sum_ico[2];

                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 8, 11);
                        borderRange = worksheet.get_Range(left_char.ToString() + border, right_char.ToString() + (start - 1));
                        borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                    }

                    sum_all[0] += sum_ico[0];
                    sum_all[1] += sum_ico[1];
                    sum_all[2] += sum_ico[2];
                }

                com = new SqlCommand("SELECT SUM(SchvalenyRozpocet),SUM(RozpocetPoZmenach),SUM(Skutocnost), Count(*) FROM TableVstup tv WHERE (tv.Podpolozka LIKE '" + String.Join("' OR tv.Podpolozka LIKE '", kategorie) + "') AND tv.SyntetickyaleboFiktivny LIKE @typ AND tv.KalendarnyDen LIKE @Datum", Con);
                com.Parameters.AddWithValue("@typ", typ);
                com.Parameters.AddWithValue("@Datum", datum);

                dT = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(com))
                {
                    da.Fill(dT);
                    com.ExecuteNonQuery();
                }

                if (Double.Parse(dT.Rows[0][3].ToString()) != 0)
                {
                    range_riadok = worksheet.get_Range(right_char.ToString() + start, right_char.ToString() + start);
                    range_riadok.Font.Bold = true;
                    range_riadok.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                    worksheet.Cells[start, 1] = "Celkom";
                    worksheet.Cells[start, 5] = sum_all[0];
                    worksheet.Cells[start, 6] = sum_all[1];
                    worksheet.Cells[start, 7] = sum_all[2];
                    worksheet.Cells[start, 8] = Double.Parse(dT.Rows[0][2].ToString()) / typVystupov;
                    range_riadok = worksheet.get_Range(left_char.ToString() + start, right_char.ToString() + start);
                    range_riadok.Font.Bold = true;

                    increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 8, 11);
                }

                for (char c = left_char; c <= (char)((int)right_char); c++)//tenke oramovanie od border po start
                {
                    borderRange = worksheet.get_Range(c.ToString() + border, c.ToString() + (start - 1));
                    borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                    //ZAOKRUHLOVANIE
                    //CISLA
                    if (c > (left_char + 3))
                    {
                        borderRange.NumberFormat = zaokruhlenieMena;
                        borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                    }
                    //PERCENTA
                }
                

                borderRange = worksheet.get_Range(left_char.ToString() + border, right_char.ToString() + (start - 1));//hrube po Celkovo
                borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                string selectString = "B" + border.ToString() + ":" + right_char + start.ToString();
                Excel.Range selectedRange = worksheet.Range[selectString].Columns[String.Concat("B:", right_char.ToString())];
                selectedRange.Columns.AutoFit();

                foreach (Excel.Range column in selectedRange.Columns)
                {
                    if (column.ColumnWidth < 8)//sirka stlpca tak jak ju ukazuje excel
                        column.ColumnWidth = 8;
                }

                var culture = new CultureInfo("ru-RU");
                worksheet.Cells[start, 1] = "Dátum vytvorenia: " + time.ToString(culture);
                worksheet.Cells[start, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

                workbook.Save();
                if (priamaTlac)
                    PrintMyExcelFile(workbook);
                Kontrola.release_excel(range, worksheet, workbook, app);
                Con.Close();
            }
        }
    }
}
