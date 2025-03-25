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
    class Zostavy_7 : Zostavy
    {
        public void spravZostavyOCerpaniKapitalovych(string typ, int typZostavy, string datum, int typVystupov, string zaokruhlenieMena, string zaokruhleniePercenta, char left_char, int left_num, char right_char, int right_num, int count_for_page, int strana_rel_pos, DateTime time, bool priamaTlac)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            string[] arrVystupneHodnoty = new string[] { "711", "712", "713", "714", "716", "717", "718", "719", "710", "721", "722", "723", "725", "720", "700" };
            //AK MA ROBIT VEDU TAK Z DATABAZY VYTIAHNE VYNIMKY VEDY
            string veda = "";
            if (typZostavy == 3 || typZostavy == 4)
                veda = vynimkyVedy(3, connectionstring);
            SqlCommand com = null;

            DataTable dT = new DataTable();
            using (var Con = new SqlConnection(connectionstring))
            {
                Con.Open();
                com = new SqlCommand("SELECT DISTINCT Zdroj FROM TableVstup WHERE Skutocnost > 0 AND SyntetickyaleboFiktivny LIKE @Synteticky AND Polozka LIKE '7%' AND KalendarnyDen LIKE @Datum ORDER BY Zdroj", Con);
                com.Parameters.AddWithValue("@Synteticky", typ);
                com.Parameters.AddWithValue("@Datum", datum);

                using (SqlDataAdapter da = new SqlDataAdapter(com))
                {
                    da.Fill(dT);
                    com.ExecuteNonQuery();
                }

                //ZDROJE DO ARRAYU
                List<string> zdrojeList = new List<string>();
                int pocetZdrojov = 1;
                zdrojeList.Add("%");

                foreach (DataRow row in dT.Rows)
                {
                    zdrojeList.Add((string)row[0]);
                    pocetZdrojov++;
                }
                string[] arrZdroje = new string[pocetZdrojov];
                arrZdroje = zdrojeList.ToArray();
                //NASTAVENIE NAZVU ZOSTAVY
                string nameTabulka = null;
                if (typZostavy == 1 || typZostavy == 3)
                    nameTabulka = "7a";
                else
                    nameTabulka = "7b";

                if (typZostavy == 3 || typZostavy == 4)
                    nameTabulka += "1";

                string kvartal = vytvorAdresare(datum);

                //hl7adzV3 JE HLAVICKA ZOSTAVY PRE TENTO TYP
                string name = "RZBT" + nameTabulka + ".xls";
                System.IO.File.Copy("..\\..\\Hlav\\RZB\\hl7adzV3.xls", "Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name, true);

                Excel.Application app = new Excel.Application();
                string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                Excel.Workbook workbook = app.Workbooks.Open(path + "\\" + "bin\\Debug\\Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name);
                Excel._Worksheet worksheet = workbook.Sheets[1];//pre citanie excelu
                Excel.Range range = worksheet.UsedRange;//pre citanie excelu
                Excel.Range borderRange;
                Excel._Worksheet worksheetFirst = workbook.Sheets[1];//pre citanie excelu

                //VYPISANIE HLAVICKY KTORA SA BUDE KOPIROVAT
                //TEXTY V EXCELI
                if (typZostavy == 1 || typZostavy == 3)
                    worksheet.Cells[7, 1] = "Členenie podľa organizácie";
                else
                    worksheet.Cells[7, 1] = "Členenie podľa funkčnej klasifikácie";

                if (typZostavy == 1 || typZostavy == 2)
                    worksheet.Cells[2, 1] = " Prehľad  o čerpaní kapitálových výdavkov celkom";
                else
                    worksheet.Cells[2, 1] = " Prehľad  o čerpaní kapitálových výdavkov za vedu a techniku";

                if (typVystupov == 1)
                    worksheet.Cells[3, 1] = "(údaje sú v €)";
                else
                    worksheet.Cells[3, 1] = "(údaje sú v tisícoch €)";

                worksheet.Cells[6, 1] = "Spracovateľské obdobie: " + datum;
                //1 - organizacie , 2 - podtrieda , 3 - organizacie veda , 4 - podtrieda veda 
                worksheet.Cells[6, 16] = "Strana: 1";
                worksheet.Name = "Celkovo";
                worksheet.Cells[6, 15] = nameTabulka;

                //CYKLUS PRE KAZDY ZDROJ
                for (int i = 0; i < pocetZdrojov; i++)
                {
                    //VYTVARANIE NOVEHO ZOSITU PLUS KOPIROVANIE HLAVICKY 
                    //VACSIE AKO 0 LEBO PRVY OTVARAM UZ PRED CYKLOM ABY SOM NASTAVIL HALVICKU KTORU POTOM KOPIRUJEM
                    if (i > 0)
                    {
                        workbook.Sheets[1].Copy(Type.Missing, workbook.Sheets[workbook.Sheets.Count]);
                        worksheet = workbook.Sheets[workbook.Sheets.Count];
                        int help = worksheet.Rows.Count;
                        range = worksheet.get_Range("A11", "P" + help);
                        range.Clear();
                        worksheet.Name = arrZdroje[i];
                        //DOPLNENIE NAZVU TABULKY DO EXCELU ZA KONKRETNY ZDROJ
                        worksheet.Cells[6, 15] = nameTabulka + "z" + arrZdroje[i];
                        worksheet.Cells[4, 1] = worksheet.Cells[3, 1];
                        //NAZOV ZDROJA A VYPLNENIE KOLONIEK ROVNAKO PRE VSETKY
                        com = new SqlCommand("SELECT Text FROM ciselnikZdroje WHERE Zdroj LIKE @Zdroj", Con);
                        com.Parameters.AddWithValue("@Zdroj", arrZdroje[i] + "%");
                        dT = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(com))
                        {
                            da.Fill(dT);
                            com.ExecuteNonQuery();
                        }
                        worksheet.Cells[3, 1] = "za zdroj " + arrZdroje[i] + " - " + (string)dT.Rows[0][0];
                    }

                    int rowCount = range.Rows.Count;//pre citanie excelu
                    int start = right_num + 1;
                    int border;
                    int strana = 2;

                    //1 - organizacie , 2 - podtrieda , 3 - organizacie veda , 4 - podtrieda veda 
                    if (typZostavy == 1 || typZostavy == 3)
                        com = new SqlCommand("SELECT DISTINCT tv.Ico,ci.CelyNazov collate Slovak_CI_AS FROM TableVstup tv JOIN ciselnikIco ci ON tv.Ico=ci.Ico WHERE tv.Skutocnost > 0 AND tv.SyntetickyaleboFiktivny LIKE @Synteticky AND tv.Zdroj LIKE @Zdroj AND KalendarnyDen LIKE @Datum AND Polozka LIKE '7%' " + veda + " ORDER BY ci.CelyNazov collate Slovak_CI_AS", Con);

                    if (typZostavy == 2 || typZostavy == 4)
                        com = new SqlCommand("SELECT DISTINCT Podtrieda FROM TableVstup WHERE Skutocnost > 0 AND SyntetickyaleboFiktivny LIKE @Synteticky AND Zdroj LIKE @Zdroj AND KalendarnyDen LIKE @Datum AND Polozka LIKE '7%' " + veda + " ORDER BY Podtrieda", Con);

                    com.Parameters.AddWithValue("@Synteticky", typ);
                    com.Parameters.AddWithValue("@Zdroj", arrZdroje[i]);
                    com.Parameters.AddWithValue("@Datum", datum);
                    dT = new DataTable();

                    using (SqlDataAdapter da = new SqlDataAdapter(com))
                    {
                        da.Fill(dT);
                        com.ExecuteNonQuery();
                    }

                    string[] arrIco = new string[dT.Rows.Count + 1];

                    int j = 0;

                    //ICA ALEBO PODTRIEDY DO ARRAYU
                    foreach (DataRow row in dT.Rows)
                    {
                        arrIco[j] = (string)row[0];
                        j++;
                    }
                    arrIco[j] = "Celkovo:";
                    border = start;
                    //CYKLUS PRE KAZDE ICO ALEBO PODTRIEDU PRI JEDNOTLIVOM ZDROJI
                    foreach (string helpString in arrIco)
                    {
                        SqlCommand get_name = null;
                        //CI HLADA ICO NAZOV ALEBO PODTRIEDA NAZOV
                        //1 - organizacie , 2 - podtrieda , 3 - organizacie veda , 4 - podtrieda veda
                        if (typZostavy == 1 || typZostavy == 3)
                            get_name = new SqlCommand("SELECT Nazov from ciselnikIco where Ico = '" + helpString + "'", Con);
                        if (typZostavy == 2 || typZostavy == 4)
                            get_name = new SqlCommand("SELECT textpar from ciselnikPodtrieda where Typ = '" + helpString + "'", Con);

                        DataTable get_name_table = new DataTable();
                        using (SqlDataAdapter get_name_adapter = new SqlDataAdapter(get_name))
                        {
                            get_name_adapter.Fill(get_name_table);
                            get_name.ExecuteNonQuery();
                        }

                        //SELECT PRE JEDNOTLIVE ICO ZA ZDROJ SUMA SKUTOCNOSTI ZA JEDNOTLIVE POLOZKY
                        if (typZostavy == 1 || typZostavy == 3)
                        {
                            com = new SqlCommand("SELECT Polozka,SUM(Skutocnost) FROM TableVstup WHERE Skutocnost > 0 AND SyntetickyaleboFiktivny LIKE @Synteticky AND Zdroj LIKE @Zdroj AND KalendarnyDen LIKE @Datum AND Ico LIKE @Ico AND Polozka LIKE '7%' " + veda + " GROUP BY Polozka ORDER BY Polozka", Con);
                            if (get_name_table.Rows.Count != 0)
                                com.Parameters.AddWithValue("@Ico", helpString);
                            else
                                com.Parameters.AddWithValue("@Ico", "%");
                        }
                        if (typZostavy == 2 || typZostavy == 4)
                        {
                            com = new SqlCommand("SELECT Polozka,SUM(Skutocnost) FROM TableVstup WHERE Skutocnost > 0 AND SyntetickyaleboFiktivny LIKE @Synteticky AND Zdroj LIKE @Zdroj AND KalendarnyDen LIKE @Datum AND Podtrieda LIKE @Podtrieda AND Polozka LIKE '7%' " + veda + " GROUP BY Polozka ORDER BY Polozka", Con);
                            if (get_name_table.Rows.Count != 0)
                                com.Parameters.AddWithValue("@Podtrieda", helpString);
                            else
                                com.Parameters.AddWithValue("@Podtrieda", "%");
                        }

                        com.Parameters.AddWithValue("@Synteticky", typ);
                        com.Parameters.AddWithValue("@Zdroj", arrZdroje[i]);
                        com.Parameters.AddWithValue("@Datum", datum);

                        dT = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(com))
                        {
                            da.Fill(dT);
                            com.ExecuteNonQuery();
                        }

                        //NAZOV DO EXCELU PLUS KONTROLA CI NEROBI FINALNY SUMAR
                        if (get_name_table.Rows.Count != 0)
                        {
                            //AK TYP ZOSTAVY 2 ALEBO 4 TREBA RIADOK NA CISLO PODTRIEDY A NASLEDNE NAZOV A SUMY
                            if (typZostavy == 2 || typZostavy == 4)
                            {
                                worksheet.Cells[start, 1].NumberFormat = "@";
                                worksheet.Cells[start, 1] = "0" + helpString.ToString();
                                worksheet.Cells[start, 1].Style.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                                //worksheet.Range[('A' + start - 1) + "1"].Style.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                                increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 20, 20, 20);
                            }

                            worksheet.Cells[start, 1] = (string)get_name_table.Rows[0][0];
                        }
                        else
                        {
                            increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 20, 20, 20);
                            worksheet.Cells[start, 1] = arrIco[j];
                        }

                        double sum1 = 0, sum2 = 0;
                        for (int k = 0; k < arrVystupneHodnoty.Length; k++) //string hladanaHodnota in arrVystupneHodnoty)
                        {
                            if (arrVystupneHodnoty[k] == "710" || arrVystupneHodnoty[k] == "720" || arrVystupneHodnoty[k] == "700")
                            {
                                if (arrVystupneHodnoty[k] == "710")
                                    worksheet.Cells[start, k + 2] = sum1;
                                else
                                {
                                    if (arrVystupneHodnoty[k] == "720")
                                        worksheet.Cells[start, k + 2] = sum2;
                                    else
                                    {
                                        if (arrVystupneHodnoty[k] == "700")
                                            worksheet.Cells[start, k + 2] = ( sum1 + sum2);
                                    }
                                }
                            }
                            else
                            {
                                foreach (DataRow row in dT.Rows)
                                {
                                    Boolean controlCiZapisal = false;
                                    if (row[0].ToString() == arrVystupneHodnoty[k])
                                    {
                                        if (arrVystupneHodnoty[k][1] == '1')
                                            sum1 = sum1 + (double)row[1] / typVystupov;
                                        else
                                            sum2 = sum2 + (double)row[1] / typVystupov;
                                        worksheet.Cells[start, k + 2] = (double)row[1] / typVystupov;
                                        controlCiZapisal = true;
                                        break;
                                    }
                                    if (controlCiZapisal == false)
                                        worksheet.Cells[start, k + 2] = 0;
                                }
                            }
                        }
                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 20, 20, 20);
                    }
                    //ORAMOVANIE V EXCELE
                    //JEDNOTLIVE STLPCE
                    for (char c = left_char; c <= (char)((int)right_char); c++)//tenke oramovanie od border po start
                    {
                        borderRange = worksheet.get_Range(c.ToString() + border, c.ToString() + start);
                        borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                        //ZAOKRUHLOVANIE
                        if (c != left_char)
                        {
                            borderRange.NumberFormat = zaokruhlenieMena;
                            borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                        }
                    }

                    //HLAVNE ORAMOVANIE
                    borderRange = worksheet.get_Range(left_char.ToString() + border, right_char.ToString() + start);//hrube po Celkovo
                    borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                    var culture = new CultureInfo("ru-RU");
                    worksheet.Cells[++start, 1] = "Dátum vytvorenia: " + time.ToString(culture);
                }
                //ZAVRIE EXCEL
                workbook.Save();
                if (priamaTlac)
                    PrintMyExcelFile(workbook);
                Kontrola.release_excel(range, worksheet, workbook, app);
            }
        }

        public void spravZostavyOCerpaniKapitalovychCelkovo(string typ, int typZostavy, string datum, int typVystupov, string zaokruhlenieMena, string zaokruhleniePercenta, char left_char, int left_num, char right_char, int right_num, int count_for_page, int strana_rel_pos, DateTime time, bool priamaTlac)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            string[] arrVystupneHodnoty = new string[] { "711", "712", "713", "714", "716", "717", "718", "719", "710", "721", "722", "723", "725", "720", "700" };
            //AK MA ROBIT VEDU TAK Z DATABAZY VYTIAHNE VYNIMKY VEDY
            string veda = "";
            if (typZostavy == 2)
                veda = vynimkyVedy(3, connectionstring);
            SqlCommand com = null;

            DataTable dT = new DataTable();
            using (var Con = new SqlConnection(connectionstring))
            {
                Con.Open();
                //VYHLADA VSETKY ROZNE ZDROJE ZO VSTUPOV 
                com = new SqlCommand("SELECT DISTINCT Zdroj FROM TableVstup WHERE Skutocnost > 0 AND SyntetickyaleboFiktivny LIKE @Synteticky AND Polozka LIKE '7%' AND KalendarnyDen LIKE @Datum  " + veda + " ORDER BY Zdroj", Con);
                com.Parameters.AddWithValue("@Synteticky", typ);
                com.Parameters.AddWithValue("@Datum", datum);

                using (SqlDataAdapter da = new SqlDataAdapter(com))
                {
                    da.Fill(dT);
                    com.ExecuteNonQuery();
                }

                //ZDROJE DO ARRAYU
                List<string> zdrojeList = new List<string>();
                int pocetZdrojov = 1;
                zdrojeList.Add("%");

                foreach (DataRow row in dT.Rows)
                {
                    zdrojeList.Add((string)row[0]);
                    pocetZdrojov++;
                }

                string[] arrZdroje = new string[pocetZdrojov];
                arrZdroje = zdrojeList.ToArray();
                string nameTabulka = "7c";
                if (typZostavy == 2)
                    nameTabulka += "1";

                string kvartal = vytvorAdresare(datum);

                //hl7adzV3 JE HLAVICKA ZOSTAVY PRE TENTO TYP
                string name = "RZBT" + nameTabulka + ".xls";
                System.IO.File.Copy("..\\..\\Hlav\\RZB\\hl7adzV3.xls", "Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name, true);

                Excel.Application app = new Excel.Application();
                string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                Excel.Workbook workbook = app.Workbooks.Open(path + "\\" + "bin\\Debug\\Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name);
                Excel._Worksheet worksheet = workbook.Sheets[1];//pre citanie excelu
                Excel.Range range = worksheet.UsedRange;//pre citanie excelu
                Excel.Range borderRange;
                //Excel._Worksheet worksheetFirst = workbook.Sheets[1];//pre citanie excelu

                //VYPISANIE HLAVICKY KTORA SA BUDE KOPIROVAT
                //TEXTY V EXCELI
                if (typZostavy == 1)
                    worksheet.Cells[2, 1] = " Prehľad  o čerpaní kapitálových výdavkov celkom";
                else
                    worksheet.Cells[2, 1] = " Prehľad  o čerpaní kapitálových výdavkov celkom za vedu a techniku";
                if (typVystupov == 1)
                    worksheet.Cells[3, 1] = "(údaje sú v €)";
                else
                    worksheet.Cells[3, 1] = "(údaje sú v tisícoch €)";

                worksheet.Cells[6, 1] = "Spracovateľské obdobie: " + datum;
                worksheet.Cells[7, 1] = "Členenie podľa organizácie";
                worksheet.Cells[6, 16] = "Strana: 1";
                worksheet.Name = "Celkovo";
                worksheet.Cells[6, 15] = nameTabulka;

                //CYKLUS PRE KAZDY ZDROJ
                for (int i = 0; i < pocetZdrojov; i++)
                {
                    //VYTVARANIE NOVEHO ZOSITU PLUS KOPIROVANIE HLAVICKY 
                    //VACSIE AKO 0 LEBO PRVY OTVARAM UZ PRED CYKLOM ABY SOM NASTAVIL HALVICKU KTORU POTOM KOPIRUJEM
                    if (i > 0)
                    {
                        workbook.Sheets[1].Copy(Type.Missing, workbook.Sheets[workbook.Sheets.Count]);
                        worksheet = workbook.Sheets[workbook.Sheets.Count];
                        int help = worksheet.Rows.Count;
                        range = worksheet.get_Range("A11", right_char + help.ToString());
                        range.UnMerge();
                        range.Clear();
                        worksheet.Name = arrZdroje[i];
                        //DOPLNENIE NAZVU TABULKY DO EXCELU ZA KONKRETNY ZDROJ
                        worksheet.Cells[6, 15] = nameTabulka + "z" + arrZdroje[i];
                        worksheet.Cells[4, 1] = worksheet.Cells[3, 1];
                        //NAZOV ZDROJA A VYPLNENIE KOLONIEK ROVNAKO PRE VSETKY
                        com = new SqlCommand("SELECT Text FROM ciselnikZdroje WHERE Zdroj LIKE @Zdroj", Con);
                        com.Parameters.AddWithValue("@Zdroj", arrZdroje[i] + "%");
                        dT = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(com))
                        {
                            da.Fill(dT);
                            com.ExecuteNonQuery();
                        }
                        worksheet.Cells[3, 1] = "za zdroj " + arrZdroje[i] + " - " + (string)dT.Rows[0][0];
                    }

                    //int rowCount = range.Rows.Count;//pre citanie excelu
                    int start = right_num+1;
                    int border, borderIco = 0;
                    int strana = 2;

                    //VYBER VSETKYCH ROZNYCH ICO ZA JENDOTLIVY ZDROJ
                    com = new SqlCommand("SELECT DISTINCT tv.Ico,ci.CelyNazov collate Slovak_CI_AS FROM TableVstup tv JOIN ciselnikIco ci ON tv.Ico=ci.Ico WHERE tv.Skutocnost > 0 AND tv.SyntetickyaleboFiktivny LIKE @Synteticky AND tv.Zdroj LIKE @Zdroj AND KalendarnyDen LIKE @Datum AND Polozka LIKE '7%' " + veda + "  ORDER BY ci.CelyNazov  collate Slovak_CI_AS", Con);
                    com.Parameters.AddWithValue("@Synteticky", typ);
                    com.Parameters.AddWithValue("@Zdroj", arrZdroje[i]);
                    com.Parameters.AddWithValue("@Datum", datum);
                    dT = new DataTable();

                    using (SqlDataAdapter da = new SqlDataAdapter(com))
                    {
                        da.Fill(dT);
                        com.ExecuteNonQuery();
                    }

                    string[] arrIco = new string[dT.Rows.Count + 1];

                    int j = 0;

                    //ICA DO ARRAYU
                    foreach (DataRow row in dT.Rows)
                    {
                        arrIco[j] = (string)row[0];
                        j++;
                    }
                    arrIco[j] = "Celkovo:";
                    border = start;
                    //CYKLUS PRE KAZDE ICO PRI JEDNOTLIVOM ZDROJI
                    foreach (string helpString in arrIco)
                    {
                        //HLADA ICO NAZOV
                        SqlCommand get_name = null;
                        get_name = new SqlCommand("SELECT Nazov from ciselnikIco where Ico = '" + helpString + "'", Con);
                        DataTable get_name_table = new DataTable();
                        using (SqlDataAdapter get_name_adapter = new SqlDataAdapter(get_name))
                        {
                            get_name_adapter.Fill(get_name_table);
                            get_name.ExecuteNonQuery();
                        }

                        //NAZOV DO EXCELU S ODSADENIM PLUS KONTROLA CI NEROBI FINALNY SUMAR + NASTAVI HODNOTU ICO DO SELECTU
                        if (get_name_table.Rows.Count != 0)
                        {
                            while (start % count_for_page > count_for_page - 4)
                                increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 20, 20, 20);

                            worksheet.get_Range(left_char + start.ToString(), right_char + (start + 2).ToString()).Merge();
                            worksheet.Cells[start, 1].VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                            worksheet.Cells[start, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                            worksheet.Cells[start, 1] = "                                                       Organizácia: " + (string)get_name_table.Rows[0][0];

                            //POSUN O 3 RIADKY KEDZE 3 ZLUCUJEME
                            increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 20, 20, 20);
                            increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 20, 20, 20);
                            increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 20, 20, 20);
                            borderIco = start;
                        }

                        //AK IDE CELKOM TAK TAM NESPAJAME RIADKY TAKZE AJ ORAMOVANIE NORMALNE
                        else
                        {
                            borderIco = start;
                        }

                        //VYBER VSETKYCH PODTRIED PRE JEDNOTLIVE ICO
                        com = new SqlCommand("SELECT DISTINCT Podtrieda FROM TableVstup WHERE Skutocnost > 0 AND SyntetickyaleboFiktivny LIKE @Synteticky AND Zdroj LIKE @Zdroj AND Ico LIKE @Ico AND KalendarnyDen LIKE @Datum AND Polozka LIKE '7%' " + veda + " ORDER BY Podtrieda", Con);
                        com.Parameters.AddWithValue("@Synteticky", typ);
                        com.Parameters.AddWithValue("@Zdroj", arrZdroje[i]);
                        com.Parameters.AddWithValue("@Datum", datum);
                        if (get_name_table.Rows.Count != 0)
                            com.Parameters.AddWithValue("@Ico", helpString);
                        else
                            com.Parameters.AddWithValue("@Ico", "%");

                        dT = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(com))
                        {
                            da.Fill(dT);
                            com.ExecuteNonQuery();
                        }

                        //PODTRIEDY PRE JEDNOTLIVE ICA DO ARRAYU
                        string[] arrPodtrieda = null;
                        int a = 0;
                        if (get_name_table.Rows.Count != 0)
                        {
                            arrPodtrieda = new string[dT.Rows.Count + 1];
                            foreach (DataRow row in dT.Rows)
                            {
                                arrPodtrieda[a] = (string)row[0];
                                a++;
                            }
                            arrPodtrieda[a] = "Spolu:";
                        }
                        //AK JE TO SUMAR
                        else
                        {
                            arrPodtrieda = new string[1];
                            arrPodtrieda[a] = "Celkom:";
                        }

                        foreach (string helpPodtrieda in arrPodtrieda)
                        {
                            //NAZOV PODTRIEDY
                            SqlCommand get_name_podtrieda = new SqlCommand("SELECT textpar FROM ciselnikPodtrieda where Typ = '" + helpPodtrieda + "'", Con);
                            DataTable get_name_podtrieda_table = new DataTable();
                            using (SqlDataAdapter get_name_podtrieda_adapter = new SqlDataAdapter(get_name_podtrieda))
                            {
                                get_name_podtrieda_adapter.Fill(get_name_podtrieda_table);
                                get_name_podtrieda.ExecuteNonQuery();
                            }
                            //AK NENASIEL MENO PODTRIEDY ROBI SUMAR INAK NORMALNE PRE DANU PODTRIEDU
                            if (get_name_podtrieda_table.Rows.Count != 0)
                            {
                                worksheet.Cells[start, 1].NumberFormat = "@";
                                worksheet.Cells[start, 1] = "0" + helpPodtrieda.ToString();
                                worksheet.Cells[start, 1].Style.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                                increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 20, 20, 20);

                                worksheet.Cells[start, 1] = (string)get_name_podtrieda_table.Rows[0][0];
                            }
                            else
                            {
                                increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 20, 20, 20);
                                worksheet.Cells[start, 1] = arrPodtrieda[a];
                            }

                            //SELECT SUM SKUTOCNOSTI ZA JEDNOTLIVE POLOZKY ZA DANE ICO
                            com = new SqlCommand("SELECT Polozka,SUM(Skutocnost) FROM TableVstup WHERE Skutocnost > 0 AND SyntetickyaleboFiktivny LIKE @Synteticky AND Zdroj LIKE @Zdroj AND KalendarnyDen LIKE @Datum AND Ico LIKE @Ico AND Podtrieda LIKE @Podtrieda AND Polozka LIKE '7%' " + veda + " GROUP BY Polozka ORDER BY Polozka", Con);
                            if (get_name_table.Rows.Count != 0)
                                com.Parameters.AddWithValue("@Ico", helpString);
                            else
                                com.Parameters.AddWithValue("@Ico", "%");

                            if (get_name_podtrieda_table.Rows.Count != 0)
                                com.Parameters.AddWithValue("@Podtrieda", helpPodtrieda);
                            else
                                com.Parameters.AddWithValue("@Podtrieda", "%");

                            com.Parameters.AddWithValue("@Synteticky", typ);
                            com.Parameters.AddWithValue("@Zdroj", arrZdroje[i]);
                            com.Parameters.AddWithValue("@Datum", datum);

                            dT = new DataTable();
                            using (SqlDataAdapter da = new SqlDataAdapter(com))
                            {
                                da.Fill(dT);
                                com.ExecuteNonQuery();
                            }
                            //SAMOTNY VYPIS HODNOT
                            double sum1 = 0, sum2 = 0;
                            for (int k = 0; k < arrVystupneHodnoty.Length; k++)
                            {
                                if (arrVystupneHodnoty[k] == "710" || arrVystupneHodnoty[k] == "720" || arrVystupneHodnoty[k] == "700")
                                {
                                    if (arrVystupneHodnoty[k] == "710")
                                        worksheet.Cells[start, k + 2] = sum1;
                                    else
                                    {
                                        if (arrVystupneHodnoty[k] == "720")
                                            worksheet.Cells[start, k + 2] = sum2;
                                        else
                                        {
                                            if (arrVystupneHodnoty[k] == "700")
                                                worksheet.Cells[start, k + 2] = (sum1 + sum2);
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (DataRow row in dT.Rows)
                                    {
                                        Boolean controlCiZapisal = false;
                                        if (row[0].ToString() == arrVystupneHodnoty[k])
                                        {
                                            if (arrVystupneHodnoty[k][1] == '1')
                                                sum1 = sum1 + (double)row[1] / typVystupov;
                                            else
                                                sum2 = sum2 + (double)row[1] / typVystupov;
                                            worksheet.Cells[start, k + 2] = (double)row[1] / typVystupov;
                                            controlCiZapisal = true;
                                            break;
                                        }
                                        if (controlCiZapisal == false)
                                            worksheet.Cells[start, k + 2] = 0;
                                    }
                                }
                            }
                            increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 20, 20, 20);
                        }
                        //ORAMOVANIE V EXCELE
                        //JEDNOTLIVE STLPCE
                        for (char c = left_char; c <= (char)((int)right_char); c++)//tenke oramovanie od border po start
                        {
                            borderRange = worksheet.get_Range(c.ToString() + borderIco, c.ToString() + start);
                            borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                            //ZAOKRUHLOVANIE
                            if (c != left_char)
                            {
                                borderRange.NumberFormat = zaokruhlenieMena;
                                borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                            }
                        }
                        if (get_name_table.Rows.Count != 0)
                            increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 20, 20, 20);
                    }
                    //HLAVNE ORAMOVANIE
                    borderRange = worksheet.get_Range(left_char.ToString() + border, right_char.ToString() + start);//hrube po Celkovo
                    borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                    var culture = new CultureInfo("ru-RU");
                    worksheet.Cells[++start, 1] = "Dátum vytvorenia: " + time.ToString(culture);
                }
                //ZAVRIE EXCEL
                workbook.Save();
                if (priamaTlac)
                    PrintMyExcelFile(workbook);
                Kontrola.release_excel(range, worksheet, workbook, app);
            }
        }
    }
}
