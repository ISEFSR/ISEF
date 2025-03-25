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
    class Zostavy_6b : Zostavy
    {
        public void spravZostavy6b(string datum, int typVystupov, string zaokruhlenieMena, string zaokruhleniePercenta, char left_char, int left_num, char right_char, int right_num, int count_for_page, int strana_rel_pos, DateTime time, bool priamaTlac)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            string[] synteticke = najdiSynteticke(connectionstring);
            SqlCommand com = null;
            DataTable dT = new DataTable();


            using (var Con = new SqlConnection(connectionstring))
            {
                string name = "RZBT6b.xls";
                string kvartal = vytvorAdresare(datum);
                System.IO.File.Copy("..\\..\\Hlav\\RZB\\hl6Abcv3.xls", "Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name, true);

                Excel.Application app = new Excel.Application();
                string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                Excel.Workbook workbook = app.Workbooks.Open(path + "\\" + "bin\\Debug\\Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name);
                Excel._Worksheet worksheet = workbook.Sheets[1];//pre citanie excelu
                Excel.Range range = worksheet.UsedRange;//pre citanie excelu
                Excel.Range borderRange;
                int rowCount = range.Rows.Count;//pre citanie excelu
                int start = right_num + 1;
                worksheet.Cells[start - strana_rel_pos, (int)right_char - 65 + 1] = "Strana: 1";
                int border = start;
                int strana = 2;

                worksheet.Cells[5, 2] = datum;
                worksheet.Cells[5, 10] = "6b";
                worksheet.Cells[1, 10] = "Rozpočtové organizácie";
                worksheet.Cells[2, 1] = "Prehľad použitia iných prostriedkov na RO";
                if (typVystupov == 1)
                    worksheet.Cells[3, 1] = "(údaje sú v €)";
                else
                    worksheet.Cells[3, 1] = "(údaje sú v tisícoch €)";


                Con.Open();

                string vynimky = vynimkyMRZ(connectionstring);
                com = new SqlCommand("SELECT DISTINCT  tv.Ico,ci.SkratenyNazov, ci.Okruh_s, ci.CelyNazov  collate Slovak_CI_AS, ci.Segment FROM TableVstup tv JOIN ciselnikIco ci ON tv.Ico = ci.Ico WHERE KalendarnyDen LIKE @Datum AND tv.HlavnaKategoria IN ('200','300','600','700') AND ci.Segment = ' 6' AND((Zdroj NOT LIKE '1%' OR Zdroj IN('11P3', '13P3', '11O3', '11O5', '13O3', '13O5', '1AM1')) AND Zdroj NOT LIKE '3A%') AND (SyntetickyaleboFiktivny LIKE @Synteticky1 OR SyntetickyaleboFiktivny LIKE @Synteticky2 ) ORDER BY ci.CelyNazov  collate Slovak_CI_AS", Con);
                com.Parameters.AddWithValue("@Datum", datum);
                com.Parameters.AddWithValue("@Synteticky1", synteticke[4]);
                com.Parameters.AddWithValue("@Synteticky2", synteticke[5]);
                using (SqlDataAdapter da = new SqlDataAdapter(com))
                {
                    da.Fill(dT);
                    com.ExecuteNonQuery();
                }

                string[] arrIco = new string[dT.Rows.Count + 1];
                int i = 0;
                foreach (DataRow row in dT.Rows) // Loop over the rows.
                {
                    arrIco[i] = (string)row[0];
                    i++;

                }
                arrIco[i++] = "Celkovo";

                double[] lok_sum;
                double[] sum_all = new double[6];
                //ArrayList podtriedy = new ArrayList();
                foreach (string helpString in arrIco) // Loop over the rows.
                {

                    lok_sum = new double[6];
                    //lok_sum = new double[6];
                    String nazov_org = "";
                    String skratka_org = "";
                    if (helpString != "Celkovo")
                    {
                        SqlCommand get_name = new SqlCommand("SELECT Nazov, SkratenyNazov from ciselnikIco where Ico = '" + helpString + "'", Con);
                        DataTable get_name_table = new DataTable();
                        using (SqlDataAdapter get_name_adapter = new SqlDataAdapter(get_name))
                        {
                            get_name_adapter.Fill(get_name_table);
                            get_name.ExecuteNonQuery();
                        }


                        foreach (DataRow row in get_name_table.Rows) // Loop over the rows.
                        {
                            nazov_org = row[0].ToString();
                            skratka_org = row[1].ToString();
                        }

                    }
                    else
                    {
                        nazov_org = helpString;
                        skratka_org = helpString;
                    }

                    worksheet.Cells[start, 1] = nazov_org;

                    //VYBERIE DANE PODTRIEDY 9800 1400...
                    if (helpString != "Celkovo")
                    {
                        SqlCommand get_podtriedy = new SqlCommand("SELECT DISTINCT  tv.Podtrieda FROM TableVstup tv JOIN ciselnikIco ci ON tv.Ico = ci.Ico WHERE tv.Ico = '" + helpString + "' AND KalendarnyDen LIKE @Datum AND tv.HlavnaKategoria IN ('200','300','600','700') AND ci.Segment = ' 6' AND((Zdroj NOT LIKE '1%' OR Zdroj IN('11P3', '13P3', '11O3', '11O5', '13O3', '13O5', '1AM1')) AND Zdroj NOT LIKE '3A%') AND (SyntetickyaleboFiktivny LIKE @Synteticky1 OR SyntetickyaleboFiktivny LIKE @Synteticky2)", Con);
                        get_podtriedy.Parameters.AddWithValue("@Datum", datum);
                        get_podtriedy.Parameters.AddWithValue("@Synteticky1", synteticke[4]);
                        get_podtriedy.Parameters.AddWithValue("@Synteticky2", synteticke[5]);
                        DataTable get_podtrieda_table = new DataTable();
                        using (SqlDataAdapter get_podtrieda_adapter = new SqlDataAdapter(get_podtriedy))
                        {
                            get_podtrieda_adapter.Fill(get_podtrieda_table);
                            get_podtriedy.ExecuteNonQuery();

                        }
                        foreach (DataRow podtrieda in get_podtrieda_table.Rows) // Loop over the rows.
                        {//PRE KAZDU PODTRIEDU VYBRAT KATEGORIE V RAMCI NEJ, AK JE # tak VIEM ZE SU TO PRIJMY


                            SqlCommand get_kategorie = new SqlCommand("SELECT DISTINCT  tv.Kategoria FROM TableVstup tv JOIN ciselnikIco ci ON tv.Ico = ci.Ico WHERE tv.Ico = '" + helpString + "' AND tv.Podtrieda LIKE '" + podtrieda[0].ToString() + "'  AND KalendarnyDen LIKE @Datum AND tv.HlavnaKategoria IN ('200','300','600','700') AND ci.Segment = ' 6' AND((Zdroj NOT LIKE '1%' OR Zdroj IN('11P3', '13P3', '11O3', '11O5', '13O3', '13O5', '1AM1')) AND Zdroj NOT LIKE '3A%') AND (SyntetickyaleboFiktivny LIKE @Synteticky1 OR SyntetickyaleboFiktivny LIKE @Synteticky2)", Con);
                            get_kategorie.Parameters.AddWithValue("@Datum", datum);
                            get_kategorie.Parameters.AddWithValue("@Synteticky1", synteticke[4]);
                            get_kategorie.Parameters.AddWithValue("@Synteticky2", synteticke[5]);
                            DataTable get_kategorie_table = new DataTable();
                            using (SqlDataAdapter get_kategorie_adapter = new SqlDataAdapter(get_kategorie))
                            {
                                get_kategorie_adapter.Fill(get_kategorie_table);
                                get_kategorie.ExecuteNonQuery();
                            }
                            foreach (DataRow kategoria in get_kategorie_table.Rows) // Loop over the rows.
                            {//pre kazdu kategoriu vybrat nazov, bezne, kapitalove prijmy vydavky, zmenit formu podtriedy na string z ciselnika
                                string write_podtrieda;
                                if (podtrieda[0].ToString() == "#")
                                    write_podtrieda = " ";
                                else
                                {
                                    write_podtrieda = podtrieda[0].ToString();
                                    write_podtrieda = string.Concat("0", write_podtrieda);
                                    if (write_podtrieda[write_podtrieda.Length - 1] == '0')
                                        write_podtrieda = write_podtrieda.Substring(0, write_podtrieda.Length - 1);
                                }
                                worksheet.Cells[start, 2] = write_podtrieda;
                                worksheet.Cells[start, 3] = kategoria[0].ToString();

                                SqlCommand get_nazov = new SqlCommand("SELECT textKratky FROM ciselnikPodpolozka WHERE Pol = '" + string.Concat(kategoria[0].ToString(), "000") + "'", Con);
                                DataTable get_nazov_table = new DataTable();
                                using (SqlDataAdapter get_nazov_adapter = new SqlDataAdapter(get_nazov))
                                {
                                    get_nazov_adapter.Fill(get_nazov_table);
                                    get_nazov.ExecuteNonQuery();
                                }
                                foreach (DataRow nazov in get_nazov_table.Rows) // Loop over the rows.
                                {
                                    worksheet.Cells[start, 4] = nazov[0].ToString();
                                }


                                SqlCommand prijmyb = new SqlCommand("SELECT Skutocnost FROM TableVstup tv WHERE tv.Ico = '" + helpString + "' AND tv.Podtrieda LIKE '" + podtrieda[0].ToString() + "' AND tv.Kategoria LIKE '" + kategoria[0].ToString() + "'  AND KalendarnyDen LIKE @Datum AND tv.HlavnaKategoria IN ('200','300','600','700') AND((Zdroj NOT LIKE '1%' OR Zdroj IN('11P3', '13P3', '11O3', '11O5', '13O3', '13O5', '1AM1')) AND Zdroj NOT LIKE '3A%') AND SyntetickyaleboFiktivny LIKE @Synteticky AND SUBSTRING(Kategoria, 1, 1) IN ('1','2','3','4','5') AND SUBSTRING(Kategoria, 1, 2) NOT LIKE '23' AND SUBSTRING(Kategoria, 1, 3) NOT LIKE '332'  ", Con);
                                prijmyb.Parameters.AddWithValue("@Datum", datum);
                                prijmyb.Parameters.AddWithValue("@Synteticky", synteticke[4]);
                                double prijmy_bezne = 0;
                                DataTable prijmyb_table = new DataTable();
                                using (SqlDataAdapter prijmyb_adapter = new SqlDataAdapter(prijmyb))
                                {
                                    prijmyb_adapter.Fill(prijmyb_table);
                                    prijmyb.ExecuteNonQuery();
                                }
                                foreach (DataRow h in prijmyb_table.Rows) // Loop over the rows.
                                {
                                    prijmy_bezne += (double)h[0];
                                }
                                worksheet.Cells[start, 5] = prijmy_bezne / typVystupov;
                                lok_sum[0] += prijmy_bezne;


                                SqlCommand prijmyk = new SqlCommand("Select Skutocnost FROM TableVstup tv WHERE tv.Ico = '" + helpString + "' AND tv.Podtrieda LIKE '" + podtrieda[0].ToString() + "' AND tv.Kategoria LIKE '" + kategoria[0].ToString() + "'  AND KalendarnyDen LIKE @Datum AND tv.HlavnaKategoria IN ('200','300','600','700') AND((Zdroj NOT LIKE '1%' OR Zdroj IN('11P3', '13P3', '11O3', '11O5', '13O3', '13O5', '1AM1')) AND Zdroj NOT LIKE '3A%') AND SyntetickyaleboFiktivny LIKE @Synteticky AND (SUBSTRING(Kategoria, 1, 2) LIKE '23' OR SUBSTRING(Kategoria, 1, 3) IN ('320','332'))", Con);
                                prijmyk.Parameters.AddWithValue("@Datum", datum);
                                prijmyk.Parameters.AddWithValue("@Synteticky", synteticke[4]);
                                double prijmy_kapit = 0;
                                DataTable prijmyk_table = new DataTable();
                                using (SqlDataAdapter prijmyk_adapter = new SqlDataAdapter(prijmyk))
                                {
                                    prijmyk_adapter.Fill(prijmyk_table);
                                    prijmyk.ExecuteNonQuery();
                                }
                                foreach (DataRow h in prijmyk_table.Rows) // Loop over the rows.
                                {
                                    prijmy_kapit += (double)h[0];
                                }
                                worksheet.Cells[start, 6] = prijmy_kapit / typVystupov;
                                lok_sum[1] += prijmy_kapit;
                                worksheet.Cells[start, 7] = (prijmy_bezne + prijmy_kapit) / typVystupov;
                                lok_sum[2] += prijmy_bezne + prijmy_kapit;


                                SqlCommand vydavb = new SqlCommand("Select Skutocnost FROM TableVstup tv WHERE tv.Ico = '" + helpString + "' AND tv.Podtrieda LIKE '" + podtrieda[0].ToString() + "' AND tv.Kategoria LIKE '" + kategoria[0].ToString() + "'  AND KalendarnyDen LIKE @Datum AND tv.HlavnaKategoria IN ('200','300','600','700') AND((Zdroj NOT LIKE '1%' OR Zdroj IN('11P3', '13P3', '11O3', '11O5', '13O3', '13O5', '1AM1')) AND Zdroj NOT LIKE '3A%') AND SyntetickyaleboFiktivny LIKE @Synteticky AND SUBSTRING(Kategoria, 1, 1) LIKE '6'", Con);
                                vydavb.Parameters.AddWithValue("@Datum", datum);
                                vydavb.Parameters.AddWithValue("@Synteticky", synteticke[5]);
                                double vydav_bezne = 0;
                                DataTable vydavb_table = new DataTable();
                                using (SqlDataAdapter vydavb_adapter = new SqlDataAdapter(vydavb))
                                {
                                    vydavb_adapter.Fill(vydavb_table);
                                    vydavb.ExecuteNonQuery();
                                }
                                foreach (DataRow h in vydavb_table.Rows) // Loop over the rows.
                                {
                                    vydav_bezne += (double)h[0];
                                }
                                worksheet.Cells[start, 8] = vydav_bezne / typVystupov;
                                lok_sum[3] += vydav_bezne;



                                SqlCommand vydavk = new SqlCommand("Select Skutocnost FROM TableVstup tv WHERE tv.Ico = '" + helpString + "' AND tv.Podtrieda LIKE '" + podtrieda[0].ToString() + "' AND tv.Kategoria LIKE '" + kategoria[0].ToString() + "'  AND KalendarnyDen LIKE @Datum AND tv.HlavnaKategoria IN ('200','300','600','700') AND((Zdroj NOT LIKE '1%' OR Zdroj IN('11P3', '13P3', '11O3', '11O5', '13O3', '13O5', '1AM1')) AND Zdroj NOT LIKE '3A%') AND SyntetickyaleboFiktivny LIKE @Synteticky AND SUBSTRING(Kategoria, 1, 1) LIKE '7'", Con);
                                vydavk.Parameters.AddWithValue("@Datum", datum);
                                vydavk.Parameters.AddWithValue("@Synteticky", synteticke[5]);
                                double vydav_kapit = 0;
                                DataTable vydavk_table = new DataTable();
                                using (SqlDataAdapter vydavk_adapter = new SqlDataAdapter(vydavk))
                                {
                                    vydavk_adapter.Fill(vydavk_table);
                                    vydavk.ExecuteNonQuery();
                                }
                                foreach (DataRow h in vydavk_table.Rows) // Loop over the rows.
                                {
                                    vydav_kapit += (double)h[0];
                                }
                                worksheet.Cells[start, 9] = vydav_kapit / typVystupov;
                                lok_sum[4] += vydav_kapit;

                                worksheet.Cells[start, 10] = (vydav_bezne + vydav_kapit) / typVystupov;
                                lok_sum[5] += vydav_bezne + vydav_kapit;


                                increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 5, 20, 20, 20);
                            }
                        }
                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 5, 20, 20, 20);
                        worksheet.Cells[start, 2] = "Spolu:";
                        worksheet.Cells[start, 5] = lok_sum[0] / typVystupov;
                        worksheet.Cells[start, 6] = lok_sum[1] / typVystupov;
                        worksheet.Cells[start, 7] = lok_sum[2] / typVystupov;
                        worksheet.Cells[start, 8] = lok_sum[3] / typVystupov;
                        worksheet.Cells[start, 9] = lok_sum[4] / typVystupov;
                        worksheet.Cells[start, 10] = lok_sum[5] / typVystupov;
                        worksheet.Cells[start, 11] = (lok_sum[2] - lok_sum[5]) / typVystupov;

                        sum_all[0] += lok_sum[0];
                        sum_all[1] += lok_sum[1];
                        sum_all[2] += lok_sum[2];
                        sum_all[3] += lok_sum[3];
                        sum_all[4] += lok_sum[4];
                        sum_all[5] += lok_sum[5];
                    }
                    else
                    {
                        worksheet.Cells[start, 5] = sum_all[0] / typVystupov;
                        worksheet.Cells[start, 6] = sum_all[1] / typVystupov;
                        worksheet.Cells[start, 7] = sum_all[2] / typVystupov;
                        worksheet.Cells[start, 8] = sum_all[3] / typVystupov;
                        worksheet.Cells[start, 9] = sum_all[4] / typVystupov;
                        worksheet.Cells[start, 10] = sum_all[5] / typVystupov;
                        worksheet.Cells[start, 11] = (sum_all[2] - sum_all[5]) / typVystupov;
                    }
                    increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 5, 20, 20, 20);
                    for (char c = left_char; c <= (char)((int)right_char); c++)//tenke oramovanie od border po start
                    {
                        borderRange = worksheet.get_Range(c.ToString() + border, c.ToString() + start);
                        borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                        //ZAOKRUHLOVANIE
                        //CISLA
                        if (c > left_char + 3)
                        {
                            borderRange.NumberFormat = zaokruhlenieMena;
                            borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                        }

                        //ZAROVNANIE KATEGORIE
                        if (c == left_char + 1 || c == left_char + 2)
                            borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                        if (c == left_char + 3)
                            borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    }

                    //HLAVNE ORAMOVANIE
                    borderRange = worksheet.get_Range(left_char.ToString() + border, right_char.ToString() + start);//hrube po Celkovo
                    borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                    if (helpString == "Celkovo")
                    {
                        var culture = new CultureInfo("ru-RU");
                        worksheet.Cells[++start, 1] = "Dátum vytvorenia: " + time.ToString(culture);
                    }
                    else
                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 5, 20, 20, 20);

                }
                workbook.Save();
                if (priamaTlac)
                    PrintMyExcelFile(workbook);
                Kontrola.release_excel(range, worksheet, workbook, app);
            }
        }
    }
}
