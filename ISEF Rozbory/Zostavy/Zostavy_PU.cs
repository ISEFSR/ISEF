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
    class Zostavy_PU : Zostavy
    {
        public void tab_53_54(string typ, string MSVSico, string datum, int typVystupov, string zaokruhlenieMena, string zaokruhleniePercenta, char left_char, int left_num, char right_char, int right_num, int count_for_page, int strana_rel_pos, DateTime time, bool priamaTlac)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            string[] synteticke = najdiSynteticke(connectionstring);
            double[] sum_all = new double[5];

            using (var Con = new SqlConnection(connectionstring))
            {
                Con.Open();
                string[] arrZdroje, arrIco;
                int pocetZdrojov = 1, pocetIca = 0;
                string vynimky = vynimkyMRZ(connectionstring);
                //UPRAVENIE MRZ VYNIMIEK
                vynimky = vynimky.Insert(3, " (NOT ");
                vynimky = vynimky.Insert(vynimky.Length, ")");

                string nameTabulka = "UCET_str_53-54";

                string kvartal = vytvorAdresare(datum);
                string name = nameTabulka + ".xls";
                //VYTVORENIE EXCELU
                System.IO.File.Copy("..\\..\\Hlav\\ZU\\hlzu12cv3.xls", "Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name, true);
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

                worksheet.Name = "str. 53-54";
                worksheet.Cells[2, 1] = "Schválený rozpočet, upravený rozpočet a čerpanie výdavkov kapitoly školstva podľa funkčnej klasifikácie k " + datum + " (€)";
                worksheet.Cells[4, 3] = worksheet.Cells[4, 3].Value2.ToString() + " " + datum.Substring(6);
                worksheet.Cells[4, 4] = worksheet.Cells[4, 4].Value2.ToString() + " " + datum;
                worksheet.Cells[4, 5] = worksheet.Cells[4, 5].Value2.ToString() + " " + datum;
                worksheet.Cells[4, 6] = worksheet.Cells[4, 6].Value2.ToString() + " " + datum;
                worksheet.Cells[4, 7] = worksheet.Cells[4, 7].Value2.ToString() + " " + datum;
                

                SqlCommand startovacie = new SqlCommand("SELECT DISTINCT Typ,Znacka, textpar FROM ciselnikPodtrieda WHERE Znacka = 2 ORDER BY TYP ASC", Con);
                DataTable startovacie_table = new DataTable();
                using (SqlDataAdapter startovacie_adapter = new SqlDataAdapter(startovacie))
                {
                    startovacie_adapter.Fill(startovacie_table);
                    startovacie.ExecuteNonQuery();
                }

                List<string[]> pool = new List<string[]>();
                
                int valid_len = 0;
                foreach (DataRow row in startovacie_table.Rows) // Loop over the rows.
                {
                    string[] podtrieda = new string[3];
                    podtrieda[0] = row[0].ToString();//typ
                    podtrieda[1] = row[1].ToString();//znacka
                    podtrieda[2] = row[2].ToString();//text
                    pool.Add(podtrieda);

                    valid_len = pocet_znakov_bez_nul(podtrieda[0].ToString());
                    SqlCommand trojky = new SqlCommand("SELECT DISTINCT Typ,Znacka, textpar FROM ciselnikPodtrieda WHERE Znacka = 3 AND SUBSTRING(Typ, 1, " + valid_len.ToString() + ") LIKE SUBSTRING('" + podtrieda[0].ToString() + "', 1, " + valid_len.ToString() + ") AND Znacka != '" + podtrieda[1].ToString() + "' ORDER BY TYP ASC", Con);
                    DataTable trojky_table = new DataTable();
                    using (SqlDataAdapter trojky_adapter = new SqlDataAdapter(trojky))
                    {
                        trojky_adapter.Fill(trojky_table);
                        trojky.ExecuteNonQuery();
                    }

                    foreach (DataRow trojka in trojky_table.Rows) // Loop over the rows.
                    {
                        podtrieda = new string[3];
                        podtrieda[0] = trojka[0].ToString();//typ
                        podtrieda[1] = trojka[1].ToString();//znacka
                        podtrieda[2] = trojka[2].ToString();//text
                        pool.Add(podtrieda);

                        valid_len = pocet_znakov_bez_nul(podtrieda[0].ToString());
                        SqlCommand patky = new SqlCommand("SELECT DISTINCT Typ,Znacka, textpar FROM ciselnikPodtrieda WHERE Znacka = 5 AND SUBSTRING(Typ, 1, " + valid_len.ToString() + ") LIKE SUBSTRING('" + podtrieda[0].ToString() + "', 1, " + valid_len.ToString() + ") AND Znacka != '" + podtrieda[1].ToString() + "' ORDER BY TYP ASC", Con);
                        DataTable patky_table = new DataTable();
                        using (SqlDataAdapter patky_adapter = new SqlDataAdapter(patky))
                        {
                            patky_adapter.Fill(patky_table);
                            patky.ExecuteNonQuery();
                        }

                        foreach (DataRow patka in patky_table.Rows) // Loop over the rows.
                        {
                            podtrieda = new string[3];
                            podtrieda[0] = patka[0].ToString();//typ
                            podtrieda[1] = patka[1].ToString();//znacka
                            podtrieda[2] = patka[2].ToString();//text
                            pool.Add(podtrieda);
                        }
                    }
                }


                string[] sumar = new string[3];
                sumar[0] = "%";//typ
                sumar[1] = "0";//znacka
                sumar[2] = "Výdavky kapitoly spolu";//text
                pool.Add(sumar);

                string selectString = "";
                vynimky = vynimkyMRZ(connectionstring);
                DataTable dT = new DataTable();
                DataTable dT2 = new DataTable();
                foreach (string[] curr in pool)
                {
                    dT = new DataTable();
                    dT2 = new DataTable();
                    valid_len = pocet_znakov_bez_nul(curr[0].ToString());
                    var test = curr[0].Trim();


                    SqlCommand com = new SqlCommand("SELECT SUM(SchvalenyRozpocet),SUM(RozpocetPoZmenach),SUM(Skutocnost), count(*) FROM TableVstup tv WHERE SyntetickyaleboFiktivny LIKE @typ AND KalendarnyDen LIKE @Datum AND Ico LIKE @Ico AND Substring(Podtrieda, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + curr[0].Trim() + "', 1, " + valid_len.ToString() + ")", Con);
                    SqlCommand com2 = new SqlCommand("SELECT SUM(Skutocnost) FROM TableVstup tv WHERE SyntetickyaleboFiktivny LIKE @typ AND KalendarnyDen LIKE @Datum AND Ico LIKE @Ico " + vynimky + " AND Substring(Podtrieda, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + curr[0].Trim() + "', 1, " + valid_len.ToString() + ")", Con);
                    com2.Parameters.AddWithValue("@typ", typ);
                    com2.Parameters.AddWithValue("@Datum", datum);
                    com.Parameters.AddWithValue("@typ", typ);
                    com.Parameters.AddWithValue("@Datum", datum);
                    //com.Parameters.AddWithValue("@Ico", MSVSico);
                    //com2.Parameters.AddWithValue("@Ico", MSVSico);
                    com.Parameters.AddWithValue("@Ico", "%");
                    com2.Parameters.AddWithValue("@Ico", "%");

                    using (SqlDataAdapter da = new SqlDataAdapter(com))
                    using (SqlDataAdapter da2 = new SqlDataAdapter(com2))
                    {
                        da.Fill(dT);
                        da2.Fill(dT2);
                        com.ExecuteNonQuery();
                        com2.ExecuteNonQuery();
                    }

                    

                    if (dT.Rows[0][0] != null && dT.Rows[0][1] != null && dT.Rows[0][2] != null && (int)dT.Rows[0][3] > 0)
                    {
                        string FK_with_dots = "0";
                        if (curr[0].ToString() != "%")
                        {
                            for (int c = 0; c < valid_len - 1; ++c)
                                FK_with_dots += curr[0].ToString()[c] + ".";

                            FK_with_dots += curr[0].ToString()[valid_len - 1];
                            if (Double.Parse(curr[1]) - 2 > valid_len)
                                FK_with_dots += ".0";
                        }

                        worksheet.Cells[start, 1] = FK_with_dots.ToString();
                        worksheet.Cells[start, 2] = curr[2].ToString();

                        for (int j = 0; j < dT.Rows.Count; j++)// Loop over the rows.
                        {
                            if (Double.Parse(curr[1]) <= 3)
                            {
                                range = worksheet.get_Range(left_char.ToString() + start, right_char.ToString() + start);
                                range.Font.Bold = true;
                            }
                            for (int k = 0; k < 3; k++)
                            {
                                var x = dT.Rows[j][0].ToString();
                                x = dT.Rows[j][1].ToString();
                                x = dT.Rows[j][2].ToString();
                                double value = Double.Parse(dT.Rows[j][k].ToString()) / typVystupov;
                                if (value != 0)
                                    worksheet.Cells[start, 3 + k] = value;
                                else
                                    worksheet.Cells[start, 3 + k] = 0;
                            }
                            //HODNOTY ROZPOCTOV A HODNOT MRZ A BEZMRZ
                            double schvalenyRoz = Double.Parse(dT.Rows[j][0].ToString()) / typVystupov;
                            double upravenyRoz = Double.Parse(dT.Rows[j][1].ToString()) / typVystupov;
                            double plneniePrijmov = Double.Parse(dT.Rows[j][2].ToString()) / typVystupov;
                            double bezmrz = Double.Parse(dT2.Rows[j][0].ToString()) / typVystupov;
                            double mrz = Double.Parse(dT.Rows[j][2].ToString()) / typVystupov;
                            mrz -= bezmrz;

                            //HODNOTY S A BEZ MRZ
                            worksheet.Cells[start, 6] = mrz;
                            worksheet.Cells[start, 7] = bezmrz;

                            //PERCENTA
                            if (bezmrz == 0 || schvalenyRoz == 0)
                                worksheet.Cells[start, 8] = 0;
                            else
                                worksheet.Cells[start, 8] = 100 * (double)(bezmrz / schvalenyRoz);

                            if (bezmrz == 0 || upravenyRoz == 0)
                                worksheet.Cells[start, 9] = 0;
                            else
                                worksheet.Cells[start, 9] = 100 * (double)(bezmrz / upravenyRoz);

                            if (curr[0].ToString() != "%")
                            {
                                int last_border = border;
                                int last_start = start;
                                bool ret = increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 8, 2, 8);
                                if (ret == true)
                                    selectString += "B" + last_border.ToString() + ":" + "F" + last_start.ToString() + System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator;
                            }
                        }
                    }
                }
                for (char c = left_char; c <= (char)((int)right_char); c++)//tenke oramovanie od border po start
                {
                    borderRange = worksheet.get_Range(c.ToString() + border, c.ToString() + start);
                    borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                    //ZAOKRUHLOVANIE
                    //CISLA
                    if ((c >= left_char + 2 && c < left_char + 7))
                    {
                        borderRange.NumberFormat = zaokruhlenieMena;
                        borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                    }
                    //PERCENTA
                    else
                    {
                        if (c > left_char + 6)
                        {
                            borderRange.NumberFormat = zaokruhleniePercenta;
                            borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                        }
                    }
                }
                
                //HLAVNE ORAMOVANIE OKOLO CELEHO
                borderRange = worksheet.get_Range(left_char.ToString() + border, right_char.ToString() + start);//hrube po Celkovo
                borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                var culture = new CultureInfo("ru-RU");
                worksheet.Cells[++start, 1] = "Dátum vytvorenia: " + time.ToString(culture);

                selectString += "B" + border.ToString() + ":" + "F" + start.ToString();

                //zvacsenie iba stlpcov kde ukazuje vacsie cisla ako ########
                Excel.Range selectedRange = worksheet.Range[selectString].Columns["A:F"];
                selectedRange.Columns.AutoFit();
                foreach (Excel.Range column in selectedRange.Columns)
                {
                    if (column.ColumnWidth < 12.86)//sirka stlpca tak jak ju ukazuje excel
                        column.ColumnWidth = 12.86;
                }

                workbook.Save();
                if (priamaTlac)
                    PrintMyExcelFile(workbook);
                Kontrola.release_excel(range, worksheet, workbook, app);
                Con.Close();
            }
        }


        public void tab_46(string typ, string MSVSico, string polozka_VUC, List<string> zvolene_klasifikacie_list, string datum, int typVystupov, string zaokruhlenieMena, string zaokruhleniePercenta, char left_char, int left_num, char right_char, int right_num, int count_for_page, int strana_rel_pos, DateTime time, bool priamaTlac)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            

            using (var Con = new SqlConnection(connectionstring))
            {
                Con.Open();
                string nameTabulka = "UCET_str_46";
                string kvartal = vytvorAdresare(datum);
                string name = nameTabulka + ".xls";
                //VYTVORENIE EXCELU
                System.IO.File.Copy("..\\..\\Hlav\\hl_dotacie.xls", "Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name, true);
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

                worksheet.Name = "str. 46";
                worksheet.Cells[3, 1] = worksheet.Cells[3, 1].Value2.ToString() + " " + datum;
                worksheet.Cells[5, 2] = worksheet.Cells[5, 2].Value2.ToString() + " " + datum.Substring(6);
                worksheet.Cells[5, 3] = worksheet.Cells[5, 3].Value2.ToString() + " " + datum;
                worksheet.Cells[5, 4] = worksheet.Cells[5, 4].Value2.ToString() + " " + datum;

                SqlCommand com = new SqlCommand("SELECT SUM(SchvalenyRozpocet),SUM(RozpocetPoZmenach),SUM(Skutocnost) FROM TableVstup WHERE Zdroj LIKE '111' AND Podpolozka LIKE '" + polozka_VUC + "' AND SyntetickyaleboFiktivny LIKE @typ AND KalendarnyDen LIKE @Datum AND Ico LIKE @Ico AND " + helper_ProjektPrvok_concat(zvolene_klasifikacie_list), Con);
                com.Parameters.AddWithValue("@typ", typ);
                com.Parameters.AddWithValue("@Datum", datum);
                com.Parameters.AddWithValue("@Ico", MSVSico);

                DataTable dT = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(com))
                {
                    da.Fill(dT);
                    com.ExecuteNonQuery();
                }

                string nazov_org = "Dotácia na prenesený výkon štátnej správy na VÚC (bežné výdavky) za programovú klasifikáciu ";
                range = worksheet.get_Range(left_char.ToString() + start, left_char.ToString() + start);
                range.Font.Bold = true;

                string main_name = nazov_org + helper_ProjektPrvok_commas(zvolene_klasifikacie_list);
                worksheet.Cells[start, 1] = main_name;
                for (int k = 0; k < 3; k++)
                {
                    double value = 0;
                    value = Double.Parse(dT.Rows[0][k].ToString()) / typVystupov;
                    if (value != 0)
                        worksheet.Cells[start, 2 + k] = value;
                    else
                        worksheet.Cells[start, 2 + k] = 0;
                }

                double main_schvalenyRoz = Double.Parse(dT.Rows[0][0].ToString()) / typVystupov;
                double main_upravenyRoz = Double.Parse(dT.Rows[0][1].ToString()) / typVystupov;
                double main_plneniePrijmov = Double.Parse(dT.Rows[0][2].ToString()) / typVystupov;
                double main_percentaRS = 0;
                double main_percentaRU = 0;

                if (main_plneniePrijmov == 0 || main_schvalenyRoz == 0)
                    worksheet.Cells[start, 5] = 0;
                else
                {
                    main_percentaRS = 100 * (double)(main_plneniePrijmov / main_schvalenyRoz);
                    worksheet.Cells[start, 5] = main_percentaRS;
                }

                if (main_plneniePrijmov == 0 || main_upravenyRoz == 0)
                    worksheet.Cells[start, 6] = 0;
                else
                {
                    main_percentaRU = 100 * (double)(main_plneniePrijmov / main_upravenyRoz);
                    worksheet.Cells[start, 6] = main_percentaRU;
                }
                //increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 8, 11);
                //for (char c = left_char; c <= (char)((int)right_char); c++)//tenke oramovanie od border po start
                //{
                //    borderRange = worksheet.get_Range(c.ToString() + border, c.ToString() + start);
                //    borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                //    //ZAOKRUHLOVANIE
                //    //CISLA
                //    if (c > left_char && c < left_char + 4)
                //    {
                //        borderRange.NumberFormat = zaokruhlenieMena;
                //        borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                //    }
                //    //PERCENTA
                //    else
                //    {
                //        if (c > left_char + 3)
                //        {
                //            borderRange.NumberFormat = zaokruhleniePercenta;
                //            borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                //        }
                //    }
                //}

                //borderRange = worksheet.get_Range(left_char.ToString() + border, right_char.ToString() + start);//hrube po Celkovo
                //borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                ////2

                //start += 4;
                //Excel.Range from = worksheet.Range[left_char + left_num.ToString() + ":" + right_char + (right_num+1).ToString()];
                //Excel.Range to = worksheet.Range[left_char + start.ToString() + ":" + right_char + (right_num - left_num + start).ToString()];
                //from.Copy(to);

                //start += right_num - left_num + 2;
                //border = start;
                increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 8, 11);

                SqlCommand com2 = new SqlCommand("SELECT DISTINCT ProjektPrvok FROM TableVstup WHERE Zdroj LIKE '111' AND Podpolozka LIKE '" + polozka_VUC + "' AND SyntetickyaleboFiktivny LIKE @typ AND KalendarnyDen LIKE @Datum AND Ico LIKE @Ico AND NOT " + helper_ProjektPrvok_concat(zvolene_klasifikacie_list), Con);
                com2.Parameters.AddWithValue("@typ", typ);
                com2.Parameters.AddWithValue("@Datum", datum);
                com2.Parameters.AddWithValue("@Ico", MSVSico);

                DataTable dT2 = new DataTable();
                using (SqlDataAdapter da2 = new SqlDataAdapter(com2))
                {
                    da2.Fill(dT2);
                    com2.ExecuteNonQuery();
                }

                List<string> projektPrvokList = new List<string>();
                foreach (DataRow row in dT2.Rows)
                    projektPrvokList.Add((string)row[0]);

                double[] sum_all = new double[3];
                sum_all[0] = main_schvalenyRoz;
                sum_all[1] = main_upravenyRoz;
                sum_all[2] = main_plneniePrijmov;
                foreach (string projektPrvok in projektPrvokList)
                {
                    com = new SqlCommand("SELECT SUM(SchvalenyRozpocet),SUM(RozpocetPoZmenach),SUM(Skutocnost) FROM TableVstup WHERE Zdroj LIKE '111' AND Podpolozka LIKE '" + polozka_VUC + "' AND SyntetickyaleboFiktivny LIKE @typ AND KalendarnyDen LIKE @Datum AND Ico LIKE @Ico AND ProjektPrvok LIKE '" + projektPrvok + "'", Con);
                    com.Parameters.AddWithValue("@typ", typ);
                    com.Parameters.AddWithValue("@Datum", datum);
                    com.Parameters.AddWithValue("@Ico", MSVSico);

                    dT = new DataTable();
                    using (SqlDataAdapter da = new SqlDataAdapter(com))
                    {
                        da.Fill(dT);
                        com.ExecuteNonQuery();
                    }

                    nazov_org = "Dotácia na prenesený výkon štátnej správy na VÚC (bežné výdavky) za programovú klasifikáciu ";
                    range = worksheet.get_Range(left_char.ToString() + start, left_char.ToString() + start);
                    range.Font.Bold = true;
                    range.WrapText = true;

                    
                    worksheet.Cells[start, 1] = nazov_org + projektPrvok;
                    for (int k = 0; k < 3; k++)
                    {
                        double value = 0;
                        value = Double.Parse(dT.Rows[0][k].ToString()) / typVystupov;
                        if (value != 0)
                            worksheet.Cells[start, 2 + k] = value;
                        else
                            worksheet.Cells[start, 2 + k] = 0;
                    }

                    double schvalenyRoz = Double.Parse(dT.Rows[0][0].ToString()) / typVystupov;
                    double upravenyRoz = Double.Parse(dT.Rows[0][1].ToString()) / typVystupov;
                    double plneniePrijmov = Double.Parse(dT.Rows[0][2].ToString()) / typVystupov;
                    double percentaRS = 0;
                    double percentaRU = 0;

                    if (plneniePrijmov == 0 || schvalenyRoz == 0)
                        worksheet.Cells[start, 5] = 0;
                    else
                    {
                        percentaRS = 100 * (double)(plneniePrijmov / schvalenyRoz);
                        worksheet.Cells[start, 5] = percentaRS;
                    }

                    if (plneniePrijmov == 0 || upravenyRoz == 0)
                        worksheet.Cells[start, 6] = 0;
                    else
                    {
                        percentaRU = 100 * (double)(plneniePrijmov / upravenyRoz);
                        worksheet.Cells[start, 6] = percentaRU;
                    }

                    sum_all[0] += schvalenyRoz;
                    sum_all[1] += upravenyRoz;
                    sum_all[2] += plneniePrijmov;

                    borderRange = worksheet.get_Range(left_char.ToString() + border, right_char.ToString() + start);//hrube po Celkovo
                    borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                    if (projektPrvok != projektPrvokList[projektPrvokList.Count -1])
                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 8, 11);
                    
                }

                increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 8, 11);
                worksheet.Cells[start, 1] = "Spolu:";
                worksheet.Cells[start, 2] = sum_all[0];
                worksheet.Cells[start, 3] = sum_all[1];
                worksheet.Cells[start, 4] = sum_all[2];
                if (sum_all[2] == 0 || sum_all[0] == 0)
                    worksheet.Cells[start, 5] = 0;
                else
                    worksheet.Cells[start, 5] = 100 * (double)(sum_all[2] / sum_all[0]);
                if (sum_all[2] == 0 || sum_all[1] == 0)
                    worksheet.Cells[start, 6] = 0;
                else
                    worksheet.Cells[start, 6] = 100 * (double)(sum_all[2] / sum_all[1]);

                for (char c = left_char; c <= (char)((int)right_char); c++)//tenke oramovanie od border po start
                {
                    borderRange = worksheet.get_Range(c.ToString() + border, c.ToString() + start);
                    borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                    //ZAOKRUHLOVANIE
                    //CISLA
                    if (c > left_char && c < left_char + 4)
                    {
                        borderRange.NumberFormat = zaokruhlenieMena;
                        borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                    }
                    //PERCENTA
                    else
                    {
                        if (c > left_char + 3)
                        {
                            borderRange.NumberFormat = zaokruhleniePercenta;
                            borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                        }
                    }
                }

                string selectString = "B" + border.ToString() + ":" + "F" + start.ToString();
                //zvacsenie iba stlpcov kde ukazuje vacsie cisla ako ########
                Excel.Range selectedRange = worksheet.Range[selectString].Columns["A:F"];
                selectedRange.Columns.AutoFit();
                foreach (Excel.Range column in selectedRange.Columns)
                {
                    if (column.ColumnWidth < 10.86)//sirka stlpca tak jak ju ukazuje excel
                        column.ColumnWidth = 10.86;
                }

                borderRange = worksheet.get_Range(left_char.ToString() + border, right_char.ToString() + start);//hrube po Celkovo
                borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                var culture = new CultureInfo("ru-RU");
                worksheet.Cells[++start, 1] = "Dátum vytvorenia: " + time.ToString(culture);

                workbook.Save();
                if (priamaTlac)
                    PrintMyExcelFile(workbook);
                Kontrola.release_excel(range, worksheet, workbook, app);
                Con.Close();
            }
        }

        public void tab_34(string typ, string kategoria, string datum, int typVystupov, string zaokruhlenieMena, string zaokruhleniePercenta, char left_char, int left_num, char right_char, int right_num, int count_for_page, int strana_rel_pos, DateTime time, bool priamaTlac)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings[connection].ConnectionString;

            using (var Con = new SqlConnection(connectionstring))
            {
                Con.Open();
                string nameTabulka = "UCET_str_34";
                string kvartal = vytvorAdresare(datum);
                string name = nameTabulka + ".xls";
                //VYTVORENIE EXCELU
                System.IO.File.Copy("..\\..\\Hlav\\ZU\\hlzu9cv3.xls", "Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name, true);
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

                worksheet.Name = "str. 34";
                worksheet.Cells[2, 1] = worksheet.Cells[2, 1].Value2.ToString() + kategoria + " k " + datum;
                worksheet.Cells[4, 2] = worksheet.Cells[4, 2].Value2.ToString() + " " + datum.Substring(6);
                worksheet.Cells[4, 3] = worksheet.Cells[4, 3].Value2.ToString() + " " + datum;
                worksheet.Cells[4, 4] = worksheet.Cells[4, 4].Value2.ToString() + " " + datum;



                SqlCommand com = new SqlCommand("SELECT DISTINCT tv.Ico,ci.CelyNazov collate Slovak_CI_AS FROM TableVstup tv JOIN ciselnikIco ci ON tv.Ico=ci.Ico WHERE tv.Zdroj LIKE '111' AND tv.Podpolozka LIKE '" + kategoria + "' AND tv.SyntetickyaleboFiktivny LIKE @typ AND KalendarnyDen LIKE @Datum ORDER BY ci.CelyNazov collate Slovak_CI_AS", Con);
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

                icoList.Add("Priamoriadené PO spolu");
                foreach (string helpString in icoList) // Loop over the rows.
                {
                    com = new SqlCommand("SELECT SUM(SchvalenyRozpocet),SUM(RozpocetPoZmenach),SUM(Skutocnost) FROM TableVstup WHERE Zdroj LIKE '111' AND Podpolozka LIKE '" + kategoria + "' AND SyntetickyaleboFiktivny LIKE @typ AND KalendarnyDen LIKE @Datum AND Ico LIKE @Ico", Con);
                    com.Parameters.AddWithValue("@typ", typ);
                    com.Parameters.AddWithValue("@Datum", datum);
                    if (helpString != "Priamoriadené PO spolu")
                        com.Parameters.AddWithValue("@Ico", helpString);
                    else
                        com.Parameters.AddWithValue("@Ico", "%");

                    dT = new DataTable();
                    using (SqlDataAdapter da = new SqlDataAdapter(com))
                    {
                        da.Fill(dT);
                        com.ExecuteNonQuery();
                    }

                    String nazov_org = "";

                    if (helpString != "Priamoriadené PO spolu")
                    {
                        SqlCommand get_name = new SqlCommand("SELECT CelyNazov from ciselnikIco where Ico = '" + helpString + "'", Con);
                        DataTable get_name_table = new DataTable();
                        using (SqlDataAdapter get_name_adapter = new SqlDataAdapter(get_name))
                        {
                            get_name_adapter.Fill(get_name_table);
                            get_name.ExecuteNonQuery();
                        }

                        if (get_name_table.Rows.Count != 0)
                            nazov_org = (string)get_name_table.Rows[0][0];
                        else
                            nazov_org = helpString;
                    }
                    else
                    {
                        range = worksheet.get_Range(left_char.ToString() + start, right_char.ToString() + start);
                        range.Font.Bold = true;
                        nazov_org = "Priamoriadené PO spolu";
                    }

                    if (Double.Parse(dT.Rows[0][2].ToString()) > 0)
                    {
                        worksheet.Cells[start, 1] = nazov_org;

                        for (int k = 0; k < 3; k++)
                        {
                            double value = 0;
                            value = Double.Parse(dT.Rows[0][k].ToString()) / typVystupov;
                            if (value != 0)
                                worksheet.Cells[start, 2 + k] = value;
                            else
                                worksheet.Cells[start, 2 + k] = 0;
                        }

                        for (int k = 0; k < 3; k++)
                        {
                            double value = 0;
                            value = Double.Parse(dT.Rows[0][k].ToString()) / typVystupov;
                            if (value != 0)
                                worksheet.Cells[start, 2 + k] = value;
                            else
                                worksheet.Cells[start, 2 + k] = 0;
                        }

                        double schvalenyRoz = Double.Parse(dT.Rows[0][0].ToString()) / typVystupov;
                        double upravenyRoz = Double.Parse(dT.Rows[0][1].ToString()) / typVystupov;
                        double plneniePrijmov = Double.Parse(dT.Rows[0][2].ToString()) / typVystupov;
                        double percentaRS = 0;
                        double percentaRU = 0;

                        if (plneniePrijmov == 0 || schvalenyRoz == 0)
                            worksheet.Cells[start, 5] = 0;
                        else
                        {
                            percentaRS = 100 * (double)(plneniePrijmov / schvalenyRoz);
                            worksheet.Cells[start, 5] = percentaRS;
                        }

                        if (plneniePrijmov == 0 || upravenyRoz == 0)
                            worksheet.Cells[start, 6] = 0;
                        else
                        {
                            percentaRU = 100 * (double)(plneniePrijmov / upravenyRoz);
                            worksheet.Cells[start, 6] = percentaRU;
                        }
                        if (helpString!= icoList[icoList.Count - 1])
                            increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 8, 11);
                    }
                }

                for (char c = left_char; c <= (char)((int)right_char); c++)//tenke oramovanie od border po start
                {
                    borderRange = worksheet.get_Range(c.ToString() + border, c.ToString() + start);
                    borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                    //ZAOKRUHLOVANIE
                    //CISLA
                    if (c > left_char && c < left_char + 4)
                    {
                        borderRange.NumberFormat = zaokruhlenieMena;
                        borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                    }
                    //PERCENTA
                    else
                    {
                        if (c > left_char + 3)
                        {
                            borderRange.NumberFormat = zaokruhleniePercenta;
                            borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                        }
                    }
                }

                string selectString = "A" + border.ToString() + ":" + "F" + start.ToString();
                //zvacsenie iba stlpcov kde ukazuje vacsie cisla ako ########
                Excel.Range selectedRange = worksheet.Range[selectString].Columns["A:F"];
                selectedRange.Columns.AutoFit();
                foreach (Excel.Range column in selectedRange.Columns)
                {
                    if (column.ColumnWidth < 10.86)//sirka stlpca tak jak ju ukazuje excel
                        column.ColumnWidth = 10.86;
                }

                borderRange = worksheet.get_Range(left_char.ToString() + border, right_char.ToString() + (start - 1));//hrube po Celkovo
                borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                var culture = new CultureInfo("ru-RU");
                worksheet.Cells[++start, 1] = "Dátum vytvorenia: " + time.ToString(culture);

                workbook.Save();
                if (priamaTlac)
                    PrintMyExcelFile(workbook);
                Kontrola.release_excel(range, worksheet, workbook, app);
                Con.Close();
            }
        }

        public void tab_10_11_26_27(string typ, string MSVSico, string zdroj, string datum, int typVystupov, string zaokruhlenieMena, string zaokruhleniePercenta, char left_char, int left_num, char right_char, int right_num, int count_for_page, int strana_rel_pos, DateTime time, bool priamaTlac)
        {
            // ZDROJ 
            // "111" alebo "%"

            string selectString = "";
            string connectionstring = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            string vynimky = vynimkyMRZ(connectionstring);

            using (var Con = new SqlConnection(connectionstring))
            {
                Con.Open();
                string nameTabulka = "";
                string hlav = "";
                string nadpis = "";
                if (zdroj == "%")
                {
                    if (typ == "224")
                    {
                        hlav = "hlzu4cv3.xls";
                        nadpis = "Schválený rozpočet, upravený rozpočet a plnenie príjmov kapitoly školstva podľa kategórie ekonomickej klasifikácie k ";
                        nameTabulka = "UCET_str_10-11";
                    }
                    else if (typ == "225")
                    {
                        hlav = "hlzu8cv3.xls";
                        nadpis = "Schválený rozpočet, upravený rozpočet a čerpanie výdavkov kapitoly školstva podľa jednotlivých okruhov organizácií a kategórie ekonomickej klasifikácie k ";
                        nameTabulka = "UCET_str_26-27";
                    }
                }
                else
                {
                    var _zdroj_text_cmd = new SqlCommand("SELECT Text FROM ciselnikZdroje WHERE Zdroj = '" + zdroj + "'", Con);
                    string _zdroj_text = Convert.ToString(_zdroj_text_cmd.ExecuteScalar());

                    if (typ == "224")
                    {
                        hlav = "hlzu6zxcv3.xls";
                        nadpis = "Plnenie príjmov kapitoly školstva za kód zdroja: " + zdroj + " - " + _zdroj_text + " k ";
                        nameTabulka = "UCET_str_11";
                    }
                    else if (typ == "225")
                    {
                        hlav = "hlzu6zxcv3_typ2.xls";
                        nadpis = "Schválený rozpočet, upravený rozpočet a čerpanie výdavkov kapitoly školstva podľa kategórie ekonomickej klasifikácie za zdroj " + zdroj + " k ";
                        nameTabulka = "UCET_str_27";
                    }
                }
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

                worksheet.Name = "str. " + nameTabulka.Substring(7);
                worksheet.Cells[2, 1] = nadpis + datum;
                worksheet.Cells[4, 3] = worksheet.Cells[4, 3].Value2.ToString() + " " + datum.Substring(6);
                worksheet.Cells[4, 4] = worksheet.Cells[4, 4].Value2.ToString() + " " + datum;
                worksheet.Cells[4, 5] = worksheet.Cells[4, 5].Value2.ToString() + " " + datum;
                if(zdroj == "%")
                {
                    worksheet.Cells[4, 6] = worksheet.Cells[4, 6].Value2.ToString() + " " + datum;
                    worksheet.Cells[4, 7] = worksheet.Cells[4, 7].Value2.ToString() + " " + datum;
                }

                for(int i_oddiel = 0; i_oddiel < 3; i_oddiel++)
                {
                    range = worksheet.get_Range(left_char.ToString() + start, right_char.ToString() + start);
                    range.Font.Bold = true;

                    string oddiel_ico_select = "";
                    if (i_oddiel == 0)
                    {
                        oddiel_ico_select = "AND Ico LIKE '%'";
                        if (typ == "224")
                            worksheet.Cells[start, 2] = "Príjmy kapitoly spolu";
                        else if (typ == "225")
                            worksheet.Cells[start, 2] = "Výdavky spolu";
                    }
                    else if (i_oddiel == 1)
                    {
                        oddiel_ico_select = "AND Ico LIKE '" + MSVSico + "'";
                        worksheet.Cells[start, 2] = "Vlastný úrad MŠVVaŠ SR (vrátane PJ)";
                    }
                    else if (i_oddiel == 2)
                    {
                        oddiel_ico_select = "AND Ico NOT LIKE '" + MSVSico + "'";
                        worksheet.Cells[start, 2] = "Ostatné priamo riadené organizácie";
                    }

                    fill_row_10_11_26_27("", typ, zdroj, datum, oddiel_ico_select, vynimky, typVystupov, worksheet, start, Con, range, left_char);
                    increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 8, 11);

                    int skup = 0;
                    if (typ == "224")
                        skup = 2;
                    else
                        skup = 6;

                    for (int i_skupina = 0; i_skupina < 2; i_skupina++)
                    {
                        //var _podpolozka_text_cmd = new SqlCommand("SELECT textDlhy FROM ciselnikPodpolozka WHERE znacka = 1 AND Substring(Pol, 1, 1) LIKE '" + (skup + i_skupina).ToString() + "'", Con);
                        //string _podpolozka_text = Convert.ToString(_podpolozka_text_cmd.ExecuteScalar());

                        //range = worksheet.get_Range(left_char.ToString() + start, left_char.ToString() + start);
                        //range.Font.Bold = true;
                        //worksheet.Cells[start, 2] = _podpolozka_text;

                        fill_row_10_11_26_27((skup + i_skupina).ToString() + "00", typ, zdroj, datum, oddiel_ico_select, vynimky, typVystupov, worksheet, start, Con, range, left_char);
                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 8, 11);

                        

                        worksheet.Cells[start, 2] = "z toho:";
                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 8, 11);

                        SqlCommand kategorie_cmd = new SqlCommand("SELECT DISTINCT SUBSTRING(Pol, 1, 2) as SPol FROM ciselnikPodpolozka WHERE Znacka >= 2 AND SUBSTRING(Pol, 1, 1) = " + (skup + i_skupina).ToString() + " ORDER BY SPol", Con);
                        DataTable dtKategorie = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(kategorie_cmd))
                        {
                            da.Fill(dtKategorie);
                            kategorie_cmd.ExecuteNonQuery();
                        }

                        foreach(DataRow row in dtKategorie.Rows)//row[0].ToString()
                        {
                            bool filled = fill_row_10_11_26_27(row[0].ToString() + "0", typ, zdroj, datum, oddiel_ico_select, vynimky, typVystupov, worksheet, start, Con, range, left_char);
                            if (filled)
                                increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 8, 11);
                        }
                    }

                    for (char c = left_char; c <= (char)((int)right_char); c++)//tenke oramovanie od border po start
                    {
                        borderRange = worksheet.get_Range(c.ToString() + border, c.ToString() + (start - 1));
                        borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                        //ZAOKRUHLOVANIE
                        //CISLA
                        if (c > left_char + 1 && c < left_char + (right_char - left_char) - 1 )
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

                    selectString += "B" + border.ToString() + ":" + right_char + start.ToString() + System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator;
                    border = start;
                }
                
                selectString += "B" + border.ToString() + ":" + right_char + start.ToString();
                Excel.Range selectedRange = worksheet.Range[selectString].Columns[String.Concat("A:",right_char.ToString())];
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

        private bool fill_row_10_11_26_27(string kategoria, string typ, string zdroj, string datum, string oddiel_ico_select, string vynimky, int typVystupov, Excel._Worksheet worksheet, int start, SqlConnection Con, Excel.Range range, char left_char)
        {
            int valid_len = pocet_znakov_bez_nul(kategoria.ToString());
            SqlCommand com = new SqlCommand("SELECT SUM(SchvalenyRozpocet),SUM(RozpocetPoZmenach),SUM(Skutocnost), Count(*) FROM TableVstup WHERE Zdroj LIKE '" + zdroj + "' AND Substring(Podpolozka, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + kategoria + "', 1, " + valid_len.ToString() + ") AND SyntetickyaleboFiktivny LIKE @typ AND KalendarnyDen LIKE @Datum " + oddiel_ico_select, Con);
            com.Parameters.AddWithValue("@Datum", datum);
            com.Parameters.AddWithValue("@typ", typ);

            DataTable dT = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter(com))
            {
                da.Fill(dT);
                com.ExecuteNonQuery();
            }
            if (Double.Parse(dT.Rows[0][3].ToString()) == 0)
                return false;

            if (kategoria != "")
            {
                if (valid_len == 1)
                {
                    range = worksheet.get_Range("B" + start, "B" + start);
                    range.Font.Bold = true;
                }


                var _podpolozka_text_cmd = new SqlCommand("SELECT textDlhy FROM ciselnikPodpolozka WHERE Pol LIKE '" + kategoria.Substring(0, valid_len) + new String('0', 6 - valid_len) + "%'", Con);
                string _podpolozka_text = Convert.ToString(_podpolozka_text_cmd.ExecuteScalar());
                worksheet.Cells[start, 2] = _podpolozka_text;
            }

            worksheet.Cells[start, 1] = kategoria;
            double value = 0;
            for (int k = 0; k < 3; k++)
            {
                value = 0;
                value = Double.Parse(dT.Rows[0][k].ToString()) / typVystupov;
                if (value != 0)
                    worksheet.Cells[start, 3 + k] = value;
                else
                    worksheet.Cells[start, 3 + k] = 0;
            }

            double schvalenyRoz = Double.Parse(dT.Rows[0][0].ToString()) / typVystupov;
            double upravenyRoz = Double.Parse(dT.Rows[0][1].ToString()) / typVystupov;
            double plneniePrijmov = value;
            DataTable dTOpro;
            if (zdroj == "%")
            {
                SqlCommand com2 = new SqlCommand("SELECT COALESCE(SUM(Skutocnost),0) FROM TableVstup tv WHERE Zdroj LIKE '" + zdroj + "' AND Substring(Podpolozka, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + kategoria + "', 1, " + valid_len.ToString() + ") AND SyntetickyaleboFiktivny LIKE @typ AND KalendarnyDen LIKE @Datum " + oddiel_ico_select + " " + vynimky, Con);
                com2.Parameters.AddWithValue("@Datum", datum);
                com2.Parameters.AddWithValue("@typ", typ);

                dTOpro = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(com2))
                {
                    da.Fill(dTOpro);
                    com2.ExecuteNonQuery();
                }

                value = Double.Parse(dTOpro.Rows[0][0].ToString()) / typVystupov;
                if (value != 0)
                    worksheet.Cells[start, 7] = value;
                else
                    worksheet.Cells[start, 7] = 0;

                worksheet.Cells[start, 6] = Double.Parse(dT.Rows[0][2].ToString()) / typVystupov - value;
                plneniePrijmov = value;
            }

            if (plneniePrijmov == 0 || schvalenyRoz == 0)
                worksheet.Cells[start, 6 + (zdroj == "%" ? 2 : 0)] = 0;
            else
                worksheet.Cells[start, 6 + (zdroj == "%" ? 2 : 0)] = 100 * (double)(plneniePrijmov / schvalenyRoz);

            if (plneniePrijmov == 0 || upravenyRoz == 0)
                worksheet.Cells[start, 7 + (zdroj == "%" ? 2 : 0)] = 0;
            else
                worksheet.Cells[start, 7 + (zdroj == "%" ? 2 : 0)] = 100 * (double)(plneniePrijmov / upravenyRoz);

            return true;
        }

        public void tab_9(string typ, string MSVSico, string datum, int typVystupov, string zaokruhlenieMena, string zaokruhleniePercenta, char left_char, int left_num, char right_char, int right_num, int count_for_page, int strana_rel_pos, DateTime time, bool priamaTlac)
        {
            string selectString = "";
            string connectionstring = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            string vynimky = vynimkyMRZ(connectionstring);

            using (var Con = new SqlConnection(connectionstring))
            {
                Con.Open();
                string nameTabulka = "UCET_str_9";
                string hlav = "hlzu3cv3.xls";
                string nadpis = "Schválený rozpočet, upravený rozpočet a plnenie príjmov kapitoly školstva k ";

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

                worksheet.Name = "str. " + nameTabulka.Substring(7);
                worksheet.Cells[2, 1] = nadpis + datum;
                worksheet.Cells[4, 2] = worksheet.Cells[4, 2].Value2.ToString() + " " + datum.Substring(6);
                worksheet.Cells[4, 3] = worksheet.Cells[4, 3].Value2.ToString() + " " + datum;
                worksheet.Cells[4, 4] = worksheet.Cells[4, 4].Value2.ToString() + " " + datum;

                for (int i_oddiel = 0; i_oddiel < 3; i_oddiel++)
                {
                    range = worksheet.get_Range(left_char.ToString() + start, right_char.ToString() + start);
                    range.Font.Bold = true;
                    string oddiel = "";

                    string oddiel_ico_select = "";
                    if (i_oddiel == 0)
                    {
                        oddiel_ico_select = "AND Ico LIKE '%'";
                        oddiel = "Príjmy kapitoly spolu";
                    }
                    else if (i_oddiel == 1)
                    {
                        oddiel_ico_select = "AND Ico LIKE '" + MSVSico + "'";
                        oddiel = "Vlastný úrad MŠVVaŠ SR (vrátane PJ)";
                    }
                    else if (i_oddiel == 2)
                    {
                        oddiel_ico_select = "AND Ico NOT LIKE '" + MSVSico + "'";
                        oddiel = "Ostatné priamo riadené organizácie";
                    }

                    DataTable dT = new DataTable();
                    DataTable dTOpro = new DataTable();
                    for (int j = 0; j < 3; j++)
                    {
                        SqlCommand com;
                        List<double> values = null;
                        if (j == 0)
                        {
                            com = new SqlCommand("SELECT COALESCE(SUM(SchvalenyRozpocet),0), COALESCE(SUM(RozpocetPoZmenach),0), COALESCE(SUM(Skutocnost),0), Count(*) FROM TableVstup WHERE SyntetickyaleboFiktivny LIKE @typ AND KalendarnyDen LIKE @Datum " + oddiel_ico_select, Con);
                            com.Parameters.AddWithValue("@Datum", datum);
                            com.Parameters.AddWithValue("@typ", typ);
                            
                            using (SqlDataAdapter da = new SqlDataAdapter(com))
                            {
                                da.Fill(dT);
                                com.ExecuteNonQuery();
                            }

                            values = new List<double>()
                            {
                                 Double.Parse(dT.Rows[0][0].ToString()), Double.Parse(dT.Rows[0][1].ToString()), Double.Parse(dT.Rows[0][2].ToString())
                            };
                        }
                        else if (j == 1)
                        {
                            oddiel = "v tom: mimorozpočtové zdroje";
                            com = new SqlCommand("SELECT COALESCE(SUM(SchvalenyRozpocet),0), COALESCE(SUM(RozpocetPoZmenach),0), COALESCE(SUM(Skutocnost),0), Count(*) FROM TableVstup tv WHERE SyntetickyaleboFiktivny LIKE @typ AND KalendarnyDen LIKE @Datum " + oddiel_ico_select + " " + vynimky, Con);
                            com.Parameters.AddWithValue("@Datum", datum);
                            com.Parameters.AddWithValue("@typ", typ);
                            
                            using (SqlDataAdapter da = new SqlDataAdapter(com))
                            {
                                da.Fill(dTOpro);
                                com.ExecuteNonQuery();
                            }

                            values = new List<double>()
                            {
                                Double.Parse(dT.Rows[0][0].ToString()) - Double.Parse(dTOpro.Rows[0][0].ToString()),
                                Double.Parse(dT.Rows[0][1].ToString()) - Double.Parse(dTOpro.Rows[0][1].ToString()),
                                Double.Parse(dT.Rows[0][2].ToString()) - Double.Parse(dTOpro.Rows[0][2].ToString())
                            };
                        }
                        else if (j == 2)
                        {
                            oddiel = "          príjmy bez mimorozpočtových zdrojov";

                            values = new List<double>()
                            {
                                 Double.Parse(dTOpro.Rows[0][0].ToString()), Double.Parse(dTOpro.Rows[0][1].ToString()), Double.Parse(dTOpro.Rows[0][2].ToString())
                            };
                        }

                        worksheet.Cells[start, 1] = oddiel;
                        worksheet.Cells[start, 2] = values[0] / typVystupov;
                        worksheet.Cells[start, 3] = values[1] / typVystupov;
                        worksheet.Cells[start, 4] = values[2] / typVystupov;
                        if (values[2] == 0 || values[0] == 0)
                            worksheet.Cells[start, 5] = 0;
                        else
                            worksheet.Cells[start, 5] = 100 * (double)(values[2] / values[0]);

                        if (values[2] == 0 || values[1] == 0)
                            worksheet.Cells[start, 6] = 0;
                        else
                            worksheet.Cells[start, 6] = 100 * (double)(values[2] / values[1]);

                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 8, 11);
                    }

                    for (char c = left_char; c <= (char)((int)right_char); c++)//tenke oramovanie od border po start
                    {
                        borderRange = worksheet.get_Range(c.ToString() + border, c.ToString() + (start - 1));
                        borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                        //ZAOKRUHLOVANIE
                        //CISLA
                        if (c > left_char && c < left_char + (right_char - left_char) - 1)
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
                    border = start;
                }

                selectString += "A" + border.ToString() + ":" + right_char + start.ToString();
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

        public void tab_50(string typ, string podpolozka, string datum, int typVystupov, string zaokruhlenieMena, string zaokruhleniePercenta, char left_char, int left_num, char right_char, int right_num, int count_for_page, int strana_rel_pos, DateTime time, bool priamaTlac)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            string selectString = "";

            using (var Con = new SqlConnection(connectionstring))
            {
                Con.Open();
                string nameTabulka = "UCET_str_50";
                string kvartal = vytvorAdresare(datum);
                string name = nameTabulka + ".xls";
                //VYTVORENIE EXCELU
                System.IO.File.Copy("..\\..\\Hlav\\ZU\\hlzu2_631cv3.xls", "Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name, true);
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

                worksheet.Name = "str. 50";
                worksheet.Cells[1, 1] = worksheet.Cells[1, 1].Value2.ToString() + podpolozka + ") k " + datum;
                worksheet.Cells[3, 2] = worksheet.Cells[3, 2].Value2.ToString() + " " + datum.Substring(6);
                worksheet.Cells[3, 3] = worksheet.Cells[3, 3].Value2.ToString() + " " + datum;
                worksheet.Cells[3, 4] = worksheet.Cells[3, 4].Value2.ToString() + " " + datum;


                SqlCommand com = new SqlCommand("SELECT DISTINCT tv.Zdroj FROM TableVstup tv WHERE tv.SyntetickyaleboFiktivny LIKE @Synteticky AND tv.KalendarnyDen LIKE @Datum AND tv.Podpolozka LIKE @Podpolozka ORDER BY tv.Zdroj", Con);
                com.Parameters.AddWithValue("@Synteticky", typ);
                com.Parameters.AddWithValue("@Datum", datum);
                com.Parameters.AddWithValue("@Podpolozka", podpolozka);

                DataTable dT = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(com))
                {
                    da.Fill(dT);
                    com.ExecuteNonQuery();
                }

                List<string> zdrojeList = new List<string>();
                int pocetZdrojov = 0;
                foreach (DataRow row in dT.Rows)
                {
                    zdrojeList.Add((string)row[0]);
                    pocetZdrojov++;
                }

                zdrojeList.Add("Spolu");
                pocetZdrojov++;

                for (int i = 0; i < pocetZdrojov; i++)
                {
                    com = new SqlCommand("SELECT Text FROM ciselnikZdroje WHERE Zdroj LIKE @Zdroj", Con);
                    com.Parameters.AddWithValue("@Zdroj", zdrojeList[i] + "%");
                    dT = new DataTable();

                    using (SqlDataAdapter da = new SqlDataAdapter(com))
                    {
                        da.Fill(dT);
                        com.ExecuteNonQuery();
                    }

                    if (dT.Rows.Count != 0 && zdrojeList[i] != "Spolu")
                    {
                        worksheet.Cells[start, 1] = zdrojeList[i] + " - " + (string)dT.Rows[0][0];
                    }
                    else if (zdrojeList[i] == "Spolu")
                    {
                        borderRange = worksheet.get_Range(left_char.ToString() + start, right_char.ToString() + start);
                        borderRange.Font.Bold = true;
                        worksheet.Cells[start, 1] = zdrojeList[i];
                    }

                    com = new SqlCommand("SELECT SUM(SchvalenyRozpocet),SUM(RozpocetPoZmenach),SUM(Skutocnost) FROM TableVstup tv WHERE tv.Zdroj LIKE @Zdroj AND tv.SyntetickyaleboFiktivny LIKE @Synteticky AND tv.KalendarnyDen LIKE @Datum AND tv.Podpolozka LIKE @Podpolozka", Con);
                    com.Parameters.AddWithValue("@Synteticky", typ);
                    if (zdrojeList[i] != "Spolu")
                        com.Parameters.AddWithValue("@Zdroj", zdrojeList[i] + "%");
                    else
                        com.Parameters.AddWithValue("@Zdroj", "%");

                    com.Parameters.AddWithValue("@Datum", datum);
                    com.Parameters.AddWithValue("@Podpolozka", podpolozka);

                    dT = new DataTable();
                    using (SqlDataAdapter da = new SqlDataAdapter(com))
                    {
                        da.Fill(dT);
                        com.ExecuteNonQuery();
                    }

                    double[] spoluArray = new double[] { Double.Parse(dT.Rows[0][0].ToString()) / typVystupov, Double.Parse(dT.Rows[0][1].ToString()) / typVystupov, Double.Parse(dT.Rows[0][2].ToString()) / typVystupov };

                    for (int counter = 0; counter < 3; counter++)
                    {
                        worksheet.Cells[start, 2 + counter] = spoluArray[counter];
                    }

                    if (spoluArray[0] == 0 || spoluArray[2] == 0)
                        worksheet.Cells[start, 5] = 0;
                    else
                        worksheet.Cells[start, 5] = (spoluArray[2] / spoluArray[0]) * 100;

                    if (spoluArray[1] == 0 || spoluArray[2] == 0)
                        worksheet.Cells[start, 6] = 0;
                    else
                        worksheet.Cells[start, 6] = (spoluArray[2] / spoluArray[1]) * 100;

                    if (zdrojeList[i] != "Spolu")
                    {
                        int last_border = border;
                        int last_start = start;
                        bool ret = increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 20, 20);
                        if (ret == true)
                            selectString += "A" + last_border.ToString() + ":" + "F" + last_start.ToString() + System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator;
                    }
                }
                

                for (char c = left_char; c <= (char)((int)right_char); c++)//tenke oramovanie od border po start
                {
                    borderRange = worksheet.get_Range(c.ToString() + border, c.ToString() + start);
                    borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                    //ZAOKRUHLOVANIE
                    //CISLA
                    if (c > left_char && c < left_char + 4)
                    {
                        borderRange.NumberFormat = zaokruhlenieMena;
                        borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                    }
                    //PERCENTA
                    else
                    {
                        if (c > left_char + 3)
                        {
                            borderRange.NumberFormat = zaokruhleniePercenta;
                            borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                        }
                    }
                    //ZAROVANNIE KATEGORIE
                    if (c <= left_char)
                    {
                        borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        borderRange.NumberFormat = "@";
                    }
                }

                borderRange = worksheet.get_Range(left_char.ToString() + border, right_char.ToString() + start);//hrube po Celkovo
                borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                var culture = new CultureInfo("ru-RU");
                worksheet.Cells[++start, 1] = "Dátum vytvorenia: " + time.ToString(culture);

                selectString += "A" + border.ToString() + ":" + "F" + start.ToString();
                Excel.Range selectedRange = worksheet.Range[selectString].Columns["A:F"];
                selectedRange.Columns.AutoFit();
                foreach (Excel.Range column in selectedRange.Columns)
                {
                    if (column.ColumnWidth < 10.86)//sirka stlpca tak jak ju ukazuje excel
                        column.ColumnWidth = 10.86;
                }

                //ZAVRIE EXCEL
                workbook.Save();
                if (priamaTlac)
                    PrintMyExcelFile(workbook);
                Kontrola.release_excel(range, worksheet, workbook, app);
                Con.Close();
            }
        }

        public void tab_58_62ab(string typ, string znacka, string datum, int typVystupov, string zaokruhlenieMena, string zaokruhleniePercenta, char left_char, int left_num, char right_char, int right_num, int count_for_page, int strana_rel_pos, DateTime time, bool priamaTlac)
        //znacka "600+700" - vydavky spolu = 58-62
        //znacka "600" - bezne = 58-62a
        //znacka "700" - kapitalove 58-62b
        {
            string connectionstring = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            string selectString = "";

            using (var Con = new SqlConnection(connectionstring))
            {
                Con.Open();
                
                string cislo = "";
                if (znacka == "600+700")
                    cislo = "58-62";
                if (znacka == "600")
                    cislo = "58-62a";
                if (znacka == "700")
                    cislo = "58-62b";
                string nameTabulka = "UCET_str_" + cislo;

                string kvartal = vytvorAdresare(datum);
                string name = nameTabulka + ".xls";
                //VYTVORENIE EXCELU
                System.IO.File.Copy("..\\..\\Hlav\\ZU\\hlzuP3cv3.xls", "Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name, true);
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

                worksheet.Name = "str. " + cislo;
                
                worksheet.Cells[2, 1] = worksheet.Cells[2, 1].Value2.ToString() + datum;
                worksheet.Cells[5, 2] = worksheet.Cells[5, 2].Value2.ToString() + " " + datum.Substring(6);
                worksheet.Cells[5, 3] = worksheet.Cells[5, 3].Value2.ToString() + " " + datum;
                worksheet.Cells[5, 4] = worksheet.Cells[5, 4].Value2.ToString() + " " + datum;
                worksheet.Cells[5, 5] = worksheet.Cells[5, 5].Value2.ToString() + " " + datum;

                if (znacka == "600+700")
                    worksheet.Cells[4, 1] = "Výdavky spolu (600+700)";
                if (znacka == "600")
                    worksheet.Cells[4, 1] = "Bežné výdavky (600)";
                if (znacka == "700")
                    worksheet.Cells[4, 1] = "Kapitálové výdavky (700)";
                
                SqlCommand com = null;
                SqlCommand com2 = null;
                bool opro = false;
                DataTable dT = new DataTable();
                DataTable dT2 = new DataTable();
                DataTable dtOPRO = new DataTable();
                DataTable dtOPRO2 = new DataTable();
                string vynimky = vynimkyMRZ(connectionstring);
                //UPLNY SUMAR ZA ZOSTAVU
                double[] totalSumArray = new double[5];

                for (int skupinaProgramu = 1; skupinaProgramu <= 3; skupinaProgramu++)
                {
                    if (skupinaProgramu == 1)
                        worksheet.Cells[start, 1] = "Programy rezortu školstva";
                    else
                    {
                        if (skupinaProgramu == 2)
                            worksheet.Cells[start, 1] = "Medzirezortné programy a podprogramy, ktorých je MŠ SR gestorom a účastníkom";
                        else
                            worksheet.Cells[start, 1] = "Podprogramy, ktoré kapitola rieši ako účastník medzirezortného programu";
                    }
                    borderRange = worksheet.get_Range(left_char.ToString() + start, right_char.ToString() + start);
                    borderRange.Font.Bold = true;

                    //SUMAR ZA CELOK
                    double[] sumArray = new double[5];
                    int sumIndex = start;
                    increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 6, 8, 11);

                    //PODLA PROGRAMOV YA JEDNOTLIVE SKUPINY
                    if (znacka == "600+700")
                        com = new SqlCommand("SELECT DISTINCT tv.ProjektPrvok FROM TableVstup tv JOIN ciselnikProjektPrvok cpp ON tv.ProjektPrvok=cpp.Prog WHERE tv.SyntetickyaleboFiktivny LIKE @Typ AND KalendarnyDen LIKE @Datum AND cpp.Skupina LIKE @Skupina ORDER BY tv.ProjektPrvok", Con);
                    if (znacka == "600")
                        com = new SqlCommand("SELECT DISTINCT tv.ProjektPrvok FROM TableVstup tv JOIN ciselnikProjektPrvok cpp ON tv.ProjektPrvok=cpp.Prog WHERE tv.SyntetickyaleboFiktivny LIKE @Typ AND KalendarnyDen LIKE @Datum AND cpp.Skupina LIKE @Skupina AND SUBSTRING(tv.Kategoria, 1, 1) LIKE '6' ORDER BY tv.ProjektPrvok", Con);
                    if (znacka == "700")
                        com = new SqlCommand("SELECT DISTINCT tv.ProjektPrvok FROM TableVstup tv JOIN ciselnikProjektPrvok cpp ON tv.ProjektPrvok=cpp.Prog WHERE tv.SyntetickyaleboFiktivny LIKE @Typ AND KalendarnyDen LIKE @Datum AND cpp.Skupina LIKE @Skupina AND SUBSTRING(tv.Kategoria, 1, 1) LIKE '7' ORDER BY tv.ProjektPrvok", Con);

                    com.Parameters.AddWithValue("@Datum", datum);
                    com.Parameters.AddWithValue("@Typ", typ);
                    com.Parameters.AddWithValue("@Skupina", skupinaProgramu + "%");

                    dT = new DataTable();
                    using (SqlDataAdapter da = new SqlDataAdapter(com))
                    {
                        da.Fill(dT);
                        com.ExecuteNonQuery();
                    }
                    int i = 1;
                    string[] arrProjektPrvok;
                    arrProjektPrvok = new string[dT.Rows.Count + 1];
                    arrProjektPrvok[0] = "helper";
                    foreach (DataRow row in dT.Rows) // Loop over the rows.
                    {
                        arrProjektPrvok[i] = (row[0]).ToString().Substring(0, 5);
                        i++;
                    }

                    string posledneTrojZnakove = "";
                    string poslednePatZnakove = "";

                    for (int j = 1; j < arrProjektPrvok.Length; j++)
                    {
                        if (skupinaProgramu < 3)
                        {
                            if (posledneTrojZnakove != arrProjektPrvok[j].Substring(0, 3))
                            {
                                posledneTrojZnakove = arrProjektPrvok[j].Substring(0, 3);
                                j--;
                                arrProjektPrvok[j] = posledneTrojZnakove;
                            }
                        }

                        if (poslednePatZnakove == arrProjektPrvok[j])
                            continue;

                        poslednePatZnakove = arrProjektPrvok[j];

                        SqlCommand get_name = new SqlCommand("SELECT Text FROM ciselnikProjektPrvok WHERE Prog LIKE @Prog ORDER BY Prog", Con);
                        get_name.Parameters.AddWithValue("@Prog", poslednePatZnakove + "%");
                        DataTable get_name_table = new DataTable();
                        using (SqlDataAdapter get_name_adapter = new SqlDataAdapter(get_name))
                        {
                            get_name_adapter.Fill(get_name_table);
                            get_name.ExecuteNonQuery();
                        }
                        string nameProjektu = "";
                        if (get_name_table.Rows.Count != 0)
                            nameProjektu = (string)get_name_table.Rows[0][0];
                        else
                            nameProjektu = "CHYBA NAZOV";

                        worksheet.Cells[start, 1] = poslednePatZnakove.ToString() + " - " + nameProjektu;
                        worksheet.Cells[start, 1].Style.WrapText = true;

                        //SUMAR ZA PROJEKT
                        if (znacka == "600+700")
                        {
                            com = new SqlCommand("SELECT SUM(tv.SchvalenyRozpocet),SUM(tv.RozpocetPoZmenach),SUM(tv.Skutocnost) FROM TableVstup tv WHERE tv.SyntetickyaleboFiktivny LIKE @Typ AND tv.KalendarnyDen LIKE @Datum AND tv.ProjektPrvok LIKE @ProjektPrvok", Con);
                            com2 = new SqlCommand("SELECT SUM(tv.Skutocnost) FROM TableVstup tv WHERE tv.SyntetickyaleboFiktivny LIKE @Typ AND tv.KalendarnyDen LIKE @Datum AND tv.ProjektPrvok LIKE @ProjektPrvok " + vynimky, Con);
                        }
                        if (znacka == "600")
                        {
                            com = new SqlCommand("SELECT SUM(tv.SchvalenyRozpocet),SUM(tv.RozpocetPoZmenach),SUM(tv.Skutocnost) FROM TableVstup tv WHERE tv.SyntetickyaleboFiktivny LIKE @Typ AND tv.KalendarnyDen LIKE @Datum AND tv.ProjektPrvok LIKE @ProjektPrvok AND SUBSTRING(tv.Kategoria, 1, 1) LIKE '6'", Con);
                            com2 = new SqlCommand("SELECT SUM(tv.Skutocnost) FROM TableVstup tv WHERE tv.SyntetickyaleboFiktivny LIKE @Typ AND tv.KalendarnyDen LIKE @Datum AND tv.ProjektPrvok LIKE @ProjektPrvok " + vynimky + " AND SUBSTRING(tv.Kategoria, 1, 1) LIKE '6'", Con);
                        }
                        if (znacka == "700")
                        {
                            com = new SqlCommand("SELECT SUM(tv.SchvalenyRozpocet),SUM(tv.RozpocetPoZmenach),SUM(tv.Skutocnost) FROM TableVstup tv WHERE tv.SyntetickyaleboFiktivny LIKE @Typ AND tv.KalendarnyDen LIKE @Datum AND tv.ProjektPrvok LIKE @ProjektPrvok AND SUBSTRING(tv.Kategoria, 1, 1) LIKE '7'", Con);
                            com2 = new SqlCommand("SELECT SUM(tv.Skutocnost) FROM TableVstup tv WHERE tv.SyntetickyaleboFiktivny LIKE @Typ AND tv.KalendarnyDen LIKE @Datum AND tv.ProjektPrvok LIKE @ProjektPrvok " + vynimky + " AND SUBSTRING(tv.Kategoria, 1, 1) LIKE '7'", Con);
                        }

                        com2.Parameters.AddWithValue("@Datum", datum);
                        com.Parameters.AddWithValue("@Datum", datum);
                        com2.Parameters.AddWithValue("@Typ", typ);
                        com.Parameters.AddWithValue("@Typ", typ);
                        com2.Parameters.AddWithValue("@ProjektPrvok", poslednePatZnakove + "%");
                        com.Parameters.AddWithValue("@ProjektPrvok", poslednePatZnakove + "%");

                        dT = new DataTable();
                        dT2 = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(com))
                        using (SqlDataAdapter da2 = new SqlDataAdapter(com2))
                        {
                            da.Fill(dT);
                            da2.Fill(dT2);
                            com.ExecuteNonQuery();
                            com2.ExecuteNonQuery();
                        }

                        //VYPIS
                        double schvalenyRoz = Double.Parse(dT.Rows[0][0].ToString()) / typVystupov;
                        double upravenyRoz = Double.Parse(dT.Rows[0][1].ToString()) / typVystupov;
                        double plneniePrijmov = Double.Parse(dT.Rows[0][2].ToString()) / typVystupov;
                        double bezmrz = Double.Parse(dT2.Rows[0][0].ToString()) / typVystupov;
                        double mrz = Double.Parse(dT.Rows[0][2].ToString()) / typVystupov;
                        mrz -= bezmrz;
                        
                        worksheet.Cells[start, 2] = schvalenyRoz;
                        worksheet.Cells[start, 3] = upravenyRoz;
                        worksheet.Cells[start, 4] = plneniePrijmov;

                        //HODNOTY S A BEZ MRZ
                        worksheet.Cells[start, 5] = mrz;

                        //PERCENTA
                        if (bezmrz == 0 || schvalenyRoz == 0)
                            worksheet.Cells[start, 6] = 0;
                        else
                            worksheet.Cells[start, 6] = 100 * (double)(bezmrz / schvalenyRoz);

                        if (bezmrz == 0 || upravenyRoz == 0)
                            worksheet.Cells[start, 7] = 0;
                        else
                            worksheet.Cells[start, 7] = 100 * (double)(bezmrz / upravenyRoz);

                        //SCITANIE SUMARU
                        if (poslednePatZnakove != posledneTrojZnakove)
                        {
                            sumArray[0] += schvalenyRoz;
                            sumArray[1] += upravenyRoz;
                            sumArray[2] += plneniePrijmov;
                            sumArray[3] += bezmrz;
                            sumArray[4] += mrz;
                        }
                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 6, 8, 11);
                    }

                    //VYPIS SUMARU
                    //CISLA
                    worksheet.Cells[sumIndex, 2] = sumArray[0];
                    worksheet.Cells[sumIndex, 3] = sumArray[1];
                    worksheet.Cells[sumIndex, 4] = sumArray[2];
                    worksheet.Cells[sumIndex, 5] = sumArray[4];
                    //PERCENTA
                    if (sumArray[3] == 0 || sumArray[0] == 0)
                        worksheet.Cells[sumIndex, 6] = 0;
                    else
                        worksheet.Cells[sumIndex, 6] = 100 * (double)(sumArray[3] / sumArray[0]);

                    if (sumArray[3] == 0 || sumArray[1] == 0)
                        worksheet.Cells[sumIndex, 7] = 0;
                    else
                        worksheet.Cells[sumIndex, 7] = 100 * (double)(sumArray[3] / sumArray[1]);

                    //PRIDANIE SUMARA ZA SKUPINU DO CELKOVEHO ZA ZOSTAVU
                    for(int j=0;j<5;j++)
                        totalSumArray[j] += sumArray[j];
                }

                //VYPIS TOTALNEHO SUMARU ZA ZOSTAVU
                worksheet.Cells[start, 1] = "Kapitola MŠVVaŠ SR spolu";
                borderRange = worksheet.get_Range(left_char.ToString() + start, right_char.ToString() + start);
                borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                borderRange.Font.Bold = true;

                worksheet.Cells[start, 2] = totalSumArray[0];
                worksheet.Cells[start, 3] = totalSumArray[1];
                worksheet.Cells[start, 4] = totalSumArray[2];
                worksheet.Cells[start, 5] = totalSumArray[4];
                //PERCENTA
                if (totalSumArray[3] == 0 || totalSumArray[0] == 0)
                    worksheet.Cells[start, 6] = 0;
                else
                    worksheet.Cells[start, 6] = 100 * (double)(totalSumArray[3] / totalSumArray[0]);

                if (totalSumArray[3] == 0 || totalSumArray[1] == 0)
                    worksheet.Cells[start, 7] = 0;
                else
                    worksheet.Cells[start, 7] = 100 * (double)(totalSumArray[3] / totalSumArray[1]);


                for (char c = left_char; c <= (char)((int)right_char); c++)//tenke oramovanie od border po start
                {
                    borderRange = worksheet.get_Range(c.ToString() + border, c.ToString() + start);
                    borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                    //ZAOKRUHLOVANIE
                    //CISLA
                    if ((c > left_char) && (c < left_char + 5))
                    {
                        borderRange.NumberFormat = zaokruhlenieMena;
                        borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                    }
                    //PERCENTA
                    else
                    {
                        if (c > left_char + 4)
                        {
                            borderRange.NumberFormat = zaokruhleniePercenta;
                            borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                        }
                        else
                            borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    }
                }

                //HLAVNE ORAMOVANIE OKOLO CELEHO
                borderRange = worksheet.get_Range(left_char.ToString() + border, right_char.ToString() + start);//hrube po Celkovo
                borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                var culture = new CultureInfo("ru-RU");
                worksheet.Cells[++start, 1] = "Dátum vytvorenia: " + time.ToString(culture);

                workbook.Save();
                if (priamaTlac)
                    PrintMyExcelFile(workbook);

                Kontrola.release_excel(range, worksheet, workbook, app);
                Con.Close();
            }
        }


        public void tab_4_SALDO(string typP, string typV, string datum, int typVystupov, string zaokruhlenieMena, string zaokruhleniePercenta, char left_char, int left_num, char right_char, int right_num, int count_for_page, int strana_rel_pos, DateTime time, bool priamaTlac)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            string selectString = "";
            SqlCommand com = null;
            SqlCommand com2 = null;
            bool opro = false;

            DataTable dT = new DataTable();
            DataTable dT2 = new DataTable();
            DataTable dtOPRO = new DataTable();
            DataTable dtOPRO2 = new DataTable();
            double[,] sumArray = new double[4, 4];
            int sumArrayCounter = 0;

            using (var Con = new SqlConnection(connectionstring))
            {
                Con.Open();

                string vynimky = vynimkyMRZ(connectionstring);
                string nameTabulka = "UCET_str_4_SALDO";
                string kvartal = vytvorAdresare(datum);
                string name = nameTabulka + ".xls";
                //VYTVORENIE EXCELU
                System.IO.File.Copy("..\\..\\Hlav\\ZU\\hlpu15_SALDO.xls", "Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name, true);
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

                worksheet.Name = "str. 4 SALDO";
                worksheet.Cells[2, 1] = "Výsledok rozpočtového hospodárenia kapitoly MŠVVaŠ SR k " + datum;
                worksheet.Cells[4, 2] = worksheet.Cells[4, 2].Value2.ToString() + " " + datum.Substring(6);
                worksheet.Cells[4, 3] = worksheet.Cells[4, 3].Value2.ToString() + " " + datum;
                worksheet.Cells[4, 4] = worksheet.Cells[4, 4].Value2.ToString() + " " + datum;
                worksheet.Cells[4, 5] = worksheet.Cells[4, 5].Value2.ToString() + " " + datum;

                for (int kapitola = 0; kapitola < 2; kapitola++)
                {
                    com = new SqlCommand("SELECT DISTINCT tv.Zdroj FROM TableVstup tv JOIN ciselnikZdroje cz ON tv.Zdroj=cz.Zdroj WHERE tv.SyntetickyaleboFiktivny LIKE @Typ AND KalendarnyDen LIKE @Datum AND cz.esfasr LIKE @TypEsfasr ORDER BY tv.Zdroj", Con);
                    com.Parameters.AddWithValue("@Datum", datum);
                    com.Parameters.AddWithValue("@TypEsfasr", "E%");

                    if (kapitola == 0)
                    {
                        com.Parameters.AddWithValue("@Typ", typP);
                    }
                    else
                    {
                        com.Parameters.AddWithValue("@Typ", typV);
                    }

                    dT = new DataTable();
                    using (SqlDataAdapter da = new SqlDataAdapter(com))
                    {
                        da.Fill(dT);
                        com.ExecuteNonQuery();
                    }

                    string[] arrZdroje;
                    arrZdroje = new string[dT.Rows.Count + 2];

                    
                    if (kapitola == 0)
                    {
                        arrZdroje[0] = "Príjmy spolu: ";
                        arrZdroje[1] = "z toho prijaté prostriedky z EÚ";
                    }
                    else
                    {
                        arrZdroje[0] = "Výdavky spolu:";
                        arrZdroje[1] = "z toho kryté prostriedkami z EÚ";
                    }
                    int i = 2;

                    foreach (DataRow row in dT.Rows) // Loop over the rows.
                    {
                        arrZdroje[i++] = (string)row[0];
                    }

                    foreach (string helpString in arrZdroje) // Loop over the rows.
                    {
                        String nazov_org = "";
                        String skratka_org = "";
                        //MEDZI SUCET NAPRIKLAD ZA EURO VSETKY
                        String doplnenie = "";
                        if(!helpString.Contains("spolu"))
                            doplnenie = "AND cz.esfasr LIKE 'E%'";

                        SqlCommand get_name = new SqlCommand("SELECT Text FROM ciselnikZdroje WHERE Zdroj LIKE @Zdroj", Con);
                        get_name.Parameters.AddWithValue("@Zdroj", helpString + "%");
                        DataTable get_name_table = new DataTable();
                        using (SqlDataAdapter get_name_adapter = new SqlDataAdapter(get_name))
                        {
                            get_name_adapter.Fill(get_name_table);
                            get_name.ExecuteNonQuery();
                        }

                        if (get_name_table.Rows.Count != 0)
                        {
                            nazov_org = helpString + " - " + (string)get_name_table.Rows[0][0];
                        }
                        else
                        {
                            borderRange = worksheet.get_Range(left_char.ToString() + start, right_char.ToString() + start);
                            borderRange.Font.Bold = true;
                            nazov_org = helpString;
                        }

                        worksheet.Cells[start, 1] = nazov_org;
                        dT = new DataTable();
                        dT2 = new DataTable();

                        com = new SqlCommand("SELECT SUM(tv.SchvalenyRozpocet),SUM(tv.RozpocetPoZmenach),SUM(tv.Skutocnost) FROM TableVstup tv JOIN ciselnikZdroje cz ON tv.Zdroj=cz.Zdroj WHERE tv.SyntetickyaleboFiktivny LIKE @typ AND tv.KalendarnyDen LIKE @Datum AND tv.Zdroj LIKE @Zdroj " + doplnenie, Con);
                        com2 = new SqlCommand("SELECT SUM(tv.Skutocnost) FROM TableVstup tv JOIN ciselnikZdroje cz ON tv.Zdroj=cz.Zdroj WHERE tv.SyntetickyaleboFiktivny LIKE @typ AND tv.KalendarnyDen LIKE @Datum AND tv.Zdroj LIKE @Zdroj " + vynimky + " " + doplnenie, Con);
                        com2.Parameters.AddWithValue("@Datum", datum);
                        com.Parameters.AddWithValue("@Datum", datum);

                        if (get_name_table.Rows.Count != 0)
                        {
                            com.Parameters.AddWithValue("@Zdroj", helpString);
                            com2.Parameters.AddWithValue("@Zdroj", helpString);
                        }
                        else
                        {
                            com.Parameters.AddWithValue("@Zdroj", "%");
                            com2.Parameters.AddWithValue("@Zdroj", "%");
                            borderRange = worksheet.get_Range(left_char.ToString() + start, right_char.ToString() + start);
                            borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                            borderRange.Font.Bold = true;
                        }
                        if (kapitola == 0)
                        {
                            com2.Parameters.AddWithValue("@typ", typP);
                            com.Parameters.AddWithValue("@typ", typP);
                        }
                        else
                        {
                            com2.Parameters.AddWithValue("@typ", typV);
                            com.Parameters.AddWithValue("@typ", typV);
                        }

                        using (SqlDataAdapter da = new SqlDataAdapter(com))
                        using (SqlDataAdapter da2 = new SqlDataAdapter(com2))
                        {
                            da.Fill(dT);
                            da2.Fill(dT2);
                            com.ExecuteNonQuery();
                            com2.ExecuteNonQuery();
                        }
                        Console.WriteLine(dT2);
                        Console.WriteLine(dT2.Rows);
                        Console.WriteLine(dT2.Rows[0]);
                        Console.WriteLine(dT2.Rows[0][0]);
                        var h = dT2.Rows[0][0];
                        var y = dT2.Rows[0][0].ToString();
                        i = 0;
                        for (int j = 0; j < dT.Rows.Count; j++)// Loop over the rows.
                        {
                            var u = dT2.Rows[0][0].ToString();
                            if (u == "")
                            {
                                continue;
                            }
                            //HODNOTY ROZPOCTOV A HODNOT MRZ A BEZMRZ
                            double schvalenyRoz = Double.Parse(dT.Rows[j][0].ToString()) / typVystupov;
                            double upravenyRoz = Double.Parse(dT.Rows[j][1].ToString()) / typVystupov;
                            double plneniePrijmov = Double.Parse(dT.Rows[j][2].ToString()) / typVystupov;
                            double bezmrz = Double.Parse(dT2.Rows[j][0].ToString()) / typVystupov;
                            double mrz = Double.Parse(dT.Rows[j][2].ToString()) / typVystupov;
                            mrz -= bezmrz;

                            worksheet.Cells[start, 2] = schvalenyRoz;
                            worksheet.Cells[start, 3] = upravenyRoz;
                            worksheet.Cells[start, 4] = plneniePrijmov;

                            //HODNOTY S A BEZ MRZ
                            worksheet.Cells[start, 5] = bezmrz;

                            //PERCENTA
                            if (bezmrz == 0 || schvalenyRoz == 0)
                                worksheet.Cells[start, 6] = 0;
                            else
                                worksheet.Cells[start, 6] = 100 * (double)(bezmrz / schvalenyRoz);

                            if (bezmrz == 0 || upravenyRoz == 0)
                                worksheet.Cells[start, 7] = 0;
                            else
                                worksheet.Cells[start, 7] = 100 * (double)(bezmrz / upravenyRoz);

                            if (get_name_table.Rows.Count == 0)
                            {
                                sumArray[sumArrayCounter, 0] = schvalenyRoz;
                                sumArray[sumArrayCounter, 1] = upravenyRoz;
                                sumArray[sumArrayCounter, 2] = plneniePrijmov;
                                sumArray[sumArrayCounter, 3] = bezmrz;
                                sumArrayCounter++;
                            }
                        }

                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 6, 8, 11);
                    }
                }

                for (int j = 0; j < 2; j++)
                {
                    borderRange = worksheet.get_Range(left_char.ToString() + start, right_char.ToString() + start);
                    borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                    borderRange.Font.Bold = true;
                    if (j == 0)
                        worksheet.Cells[start, 1] = "Saldo príjmov a výdavkov";
                    else
                        worksheet.Cells[start, 1] = "z toho: z prostriedkov EÚ";
                    double schvalenyRoz = sumArray[0 + j, 0] - sumArray[2 + j, 0];
                    double upravenyRoz = sumArray[0 + j, 1] - sumArray[2 + j, 1];
                    double plneniePrijmov = sumArray[0 + j, 2] - sumArray[2 + j, 2];
                    double bezmrz = sumArray[0 + j, 3] - sumArray[2 + j, 3];

                    worksheet.Cells[start, 2] = schvalenyRoz;
                    worksheet.Cells[start, 3] = upravenyRoz;
                    worksheet.Cells[start, 4] = plneniePrijmov;

                    //HODNOTY S A BEZ MRZ
                    worksheet.Cells[start, 5] = bezmrz;

                    //PERCENTA
                    if (bezmrz == 0 || schvalenyRoz == 0)
                        worksheet.Cells[start, 6] = 0;
                    else
                        worksheet.Cells[start, 6] = 100 * (double)(bezmrz / schvalenyRoz);

                    if (bezmrz == 0 || upravenyRoz == 0)
                        worksheet.Cells[start, 7] = 0;
                    else
                        worksheet.Cells[start, 7] = 100 * (double)(bezmrz / upravenyRoz);

                    if (j == 0)
                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 6, 8, 11);
                }

                for (char c = left_char; c <= (char)((int)right_char); c++)//tenke oramovanie od border po start
                {
                    borderRange = worksheet.get_Range(c.ToString() + border, c.ToString() + start);
                    borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                    //ZAOKRUHLOVANIE
                    //CISLA
                    if ((c > left_char) && (c < left_char + 5))
                    {
                        borderRange.NumberFormat = zaokruhlenieMena;
                        borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                    }
                    //PERCENTA
                    else
                    {
                        if (c > left_char + 4)
                        {
                            borderRange.NumberFormat = zaokruhleniePercenta;
                            borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                        }
                        else
                            borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    }
                }

                borderRange = worksheet.get_Range(left_char.ToString() + border, right_char.ToString() + start);//hrube po Celkovo
                borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                var culture = new CultureInfo("ru-RU");
                worksheet.Cells[++start, 1] = "Dátum vytvorenia: " + time.ToString(culture);

                workbook.Save();
                if (priamaTlac)
                    PrintMyExcelFile(workbook);

                Kontrola.release_excel(range, worksheet, workbook, app);
                Con.Close();

            }
        }


        private string helper_ProjektPrvok_concat(List<string> ProjektPrvok_list)
        {//(ProjektPrvok LIKE 65465 or ProjektPrvok LIKE 465456 or ProjektPrvok LIKE 65456465)
            string ret_string = "(";
            foreach(var ico in ProjektPrvok_list)
            {
                ret_string += "ProjektPrvok LIKE '" + ico + "%'";
                if(ico != ProjektPrvok_list[ProjektPrvok_list.Count - 1])
                    ret_string += " OR ";
            }

            ret_string += ")";
            return ret_string;
        }

        private string helper_ProjektPrvok_commas(List<string> ProjektPrvok_list)
        {//(ProjektPrvok LIKE 65465 or ProjektPrvok LIKE 465456 or ProjektPrvok LIKE 65456465)
            string ret_string = "";
            foreach (var item in ProjektPrvok_list)
            {
                ret_string += item;
                if (item != ProjektPrvok_list[ProjektPrvok_list.Count - 1])
                    ret_string += ", ";
            }
           
            return ret_string;
        }
    }
}
