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
    class Zostavy_Tab7_8_9_15 : Zostavy
    {
        public void spravTab_7(string typ, string MSVSico, string datum, int typVystupov, string zaokruhlenieMena, string zaokruhleniePercenta, char left_char, int left_num, char right_char, int right_num, int count_for_page, int strana_rel_pos, DateTime time, bool priamaTlac)//1 prijmy 2 vydavky
        {
            string connectionstring = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            SqlCommand com = null;
            bool opro = true;

            DataTable dT = new DataTable();
            DataTable dtOPRO = new DataTable();
            DataTable dtOPRO2 = new DataTable();
            using (var Con = new SqlConnection(connectionstring))
            {
                string nameTabulka = "Tab_7";

                string name = "UCET_";
                name = name + nameTabulka + ".xls";

                string kvartal = vytvorAdresare(datum);

                System.IO.File.Copy("..\\..\\Hlav\\ZU\\hlpr1cv3.xls", "Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name, true);
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

                //DATUM
                worksheet.Cells[1, 8] = nameTabulka;
                worksheet.Cells[3, 1] = "vrátane iných zdrojov k " + datum;
                worksheet.Cells[5, 2] = worksheet.Cells[5, 2].Value2.ToString() + " " + datum.Substring(6);
                worksheet.Cells[5, 3] = worksheet.Cells[5, 3].Value2.ToString() + " " + datum;
                worksheet.Cells[5, 4] = worksheet.Cells[5, 4].Value2.ToString() + " " + datum;
                worksheet.Cells[5, 5] = worksheet.Cells[5, 5].Value2.ToString() + " " + datum;
                worksheet.Cells[5, 6] = worksheet.Cells[5, 6].Value2.ToString() + " " + datum;

                double[] sum_all = new double[5];
                Con.Open();

                com = new SqlCommand("SELECT DISTINCT tv.Ico,ci.CelyNazov collate Slovak_CI_AS FROM TableVstup tv JOIN ciselnikIco ci ON tv.Ico=ci.Ico WHERE tv.SyntetickyaleboFiktivny LIKE @typ AND KalendarnyDen LIKE @Datum ORDER BY ci.CelyNazov collate Slovak_CI_AS", Con);
                com.Parameters.AddWithValue("@typ", typ);
                com.Parameters.AddWithValue("@Datum", datum);

                using (SqlDataAdapter da = new SqlDataAdapter(com))
                {
                    da.Fill(dT);
                    com.ExecuteNonQuery();
                }

                string[] arrIco;
                if (opro == true)
                    arrIco = new string[dT.Rows.Count + 2];
                else
                    arrIco = new string[dT.Rows.Count + 1];

                arrIco[0] = MSVSico;
                int i = 1;

                foreach (DataRow row in dT.Rows) // Loop over the rows.
                {
                    if((string)row[0]!=MSVSico)
                    {
                        arrIco[i] = (string)row[0];
                        i++;

                    }
                }
                if (opro == true)
                    arrIco[i++] = "Ostatné priamo riadené organizácie";
                arrIco[i++] = "Kapitola MŠVVaŠ SR spolu";

                string vynimky;
                SqlCommand com2 = null;
                DataTable dT2;


                foreach (string helpString in arrIco) // Loop over the rows.
                {
                    vynimky = vynimkyMRZ(connectionstring);
                    dT = new DataTable();
                    dT2 = new DataTable();

                    com = new SqlCommand("SELECT SUM(SchvalenyRozpocet),SUM(RozpocetPoZmenach),SUM(Skutocnost) FROM TableVstup tv WHERE SyntetickyaleboFiktivny LIKE @typ AND KalendarnyDen LIKE @Datum AND Ico LIKE @Ico ", Con);
                    com2 = new SqlCommand("SELECT SUM(Skutocnost) FROM TableVstup tv WHERE SyntetickyaleboFiktivny LIKE @typ AND KalendarnyDen LIKE @Datum AND Ico LIKE @Ico " + vynimky + "", Con);
                    com2.Parameters.AddWithValue("@typ", typ);
                    com2.Parameters.AddWithValue("@Datum", datum);
                    com.Parameters.AddWithValue("@typ", typ);
                    com.Parameters.AddWithValue("@Datum", datum);

                    if (helpString == "Kapitola MŠVVaŠ SR spolu" || helpString == "Ostatné priamo riadené organizácie")
                    {
                        com.Parameters.AddWithValue("@Ico", "%");
                        com2.Parameters.AddWithValue("@Ico", "%");
                    }
                    else
                    {
                        com.Parameters.AddWithValue("@Ico", helpString);
                        com2.Parameters.AddWithValue("@Ico", helpString);
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(com))
                    using (SqlDataAdapter da2 = new SqlDataAdapter(com2))
                    {
                        da.Fill(dT);
                        da2.Fill(dT2);
                        com.ExecuteNonQuery();
                        com2.ExecuteNonQuery();
                    }

                    int comparison = 0;
                    if ((comparison = helpString.CompareTo(MSVSico)) == 0)
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(com))
                        using (SqlDataAdapter da2 = new SqlDataAdapter(com2))
                        {
                            da.Fill(dtOPRO);
                            da2.Fill(dtOPRO2);
                            com.ExecuteNonQuery();
                            com2.ExecuteNonQuery();
                        }
                    }

                    i = 0;
                    //zavolanie mena podla ica
                    String nazov_org = "";
                    String skratka_org = "";

                    SqlCommand get_name = new SqlCommand("SELECT CelyNazov, SkratenyNazov from ciselnikIco where Ico = '" + helpString + "'", Con);
                    DataTable get_name_table = new DataTable();
                    using (SqlDataAdapter get_name_adapter = new SqlDataAdapter(get_name))
                    {
                        get_name_adapter.Fill(get_name_table);
                        get_name.ExecuteNonQuery();
                    }
                    if (get_name_table.Rows.Count != 0)
                    {
                        nazov_org = (string)get_name_table.Rows[0][0];
                        skratka_org = (string)get_name_table.Rows[0][1];
                    }
                    else
                    {
                        nazov_org = helpString;
                        skratka_org = helpString;
                    }

                    if(helpString == MSVSico || helpString == "Ostatné priamo riadené organizácie" || helpString == "Kapitola MŠVVaŠ SR spolu")
                    {
                        range = worksheet.get_Range(left_char.ToString() + start, right_char.ToString() + start);
                        range.Font.Bold = true;
                    }

                    worksheet.Cells[start, 1] = nazov_org;

                    for (int j = 0; j < dT.Rows.Count; j++)// Loop over the rows.
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            double value = 0;
                            if (helpString == "Ostatné priamo riadené organizácie")
                                value = (Double.Parse(dT.Rows[j][k].ToString()) - Double.Parse(dtOPRO.Rows[0][k].ToString()));
                            else
                                value = Double.Parse(dT.Rows[j][k].ToString());
                            if (value != 0)
                                worksheet.Cells[start, 2 + k] = value;
                            else
                                worksheet.Cells[start, 2 + k] = 0;
                        }
                        //HODNOTY ROZPOCTOV A HODNOT MRZ A BEZMRZ
                        double schvalenyRoz = Double.Parse(dT.Rows[j][0].ToString());
                        double upravenyRoz = Double.Parse(dT.Rows[j][1].ToString());
                        double plneniePrijmov = Double.Parse(dT.Rows[j][2].ToString());
                        double bezmrz = Double.Parse(dT2.Rows[j][0].ToString());
                        double mrz = Double.Parse(dT.Rows[j][2].ToString());
                        if (helpString == "Ostatné priamo riadené organizácie")
                        {
                            //ODRATANIE MSVS
                            schvalenyRoz -= Double.Parse(dtOPRO.Rows[0][0].ToString());
                            upravenyRoz -= Double.Parse(dtOPRO.Rows[0][1].ToString());
                            plneniePrijmov -= Double.Parse(dtOPRO.Rows[0][2].ToString());
                            bezmrz -= Double.Parse(dtOPRO2.Rows[0][0].ToString());
                            mrz -= Double.Parse(dtOPRO.Rows[0][2].ToString());
                        }
                        mrz -= bezmrz;

                        //HODNOTY S A BEZ MRZ
                        worksheet.Cells[start, 5] = mrz;
                        worksheet.Cells[start, 6] = bezmrz;

                        //PERCENTA
                        if (bezmrz == 0 || schvalenyRoz == 0)
                            worksheet.Cells[start, 7] = 0;
                        else
                            worksheet.Cells[start, 7] = 100 * (double)(bezmrz / schvalenyRoz);

                        if (bezmrz == 0 || upravenyRoz == 0)
                            worksheet.Cells[start, 8] = 0;
                        else
                            worksheet.Cells[start, 8] = 100 * (double)(bezmrz / upravenyRoz);
                    }

                    if(helpString != "Kapitola MŠVVaŠ SR spolu")
                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 8, 11);
                }
                for (char c = left_char; c <= (char)((int)right_char); c++)//tenke oramovanie od border po start
                {
                    borderRange = worksheet.get_Range(c.ToString() + border, c.ToString() + start);
                    borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                    //ZAOKRUHLOVANIE
                    //CISLA
                    if (c > left_char && c < left_char + 6)
                    {
                        borderRange.NumberFormat = zaokruhlenieMena;
                        borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                    }
                    //PERCENTA
                    else
                    {
                        if (c > left_char + 5)
                        {
                            borderRange.NumberFormat = zaokruhleniePercenta;
                            borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                        }
                    }
                }

                //ORAMOVANIE MSVS
                borderRange = worksheet.get_Range(left_char.ToString() + (right_num + 1), right_char.ToString() + (right_num + 1));
                borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                //ORAMOVANIE OPRO A CELKOVO
                borderRange = worksheet.get_Range(left_char.ToString() + (start - 1), right_char.ToString() + (start - 1));
                borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

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

        public void spravTab_8_9_15(string typ, string MSVSico, string datum, int typVystupov, string zaokruhlenieMena, string zaokruhleniePercenta, char left_char, int left_num, char right_char, int right_num, int count_for_page, int strana_rel_pos, DateTime time, bool priamaTlac)//1 prijmy 2 vydavky
        {
            string connectionstring = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            SqlCommand com = null;
            bool opro = true;
            string[][] zdrojeList = new string[3][];
            zdrojeList[0] = new string[]
            {
                "111"
            };
            zdrojeList[1] = new string[]
            {
                "131K","131L","131M", "131N","11S1", "11S2", "11T1", "11T2", "13S1", "13T1", "1AA1", "1AA2", "1AA3", "1AC1", "1AC2", "1AC3", "3AA1", "3AC1","1AY1","1BA1","1BB1","3AY1","3P01","3P02", "3BB1"
            };
            zdrojeList[2] = new string[]
            {
                "131J","131K","131M", "1AJ1", "3AA1", "3AA2", "3AA3", "3AC1", "3AC2", "3AC3", "3AJ1","1AA1", "1AA2", "1AA3","1AC1", "1AC2", "1AC3","1BB1","1BB2","1P01","1P02","3AM1","3AM2","3P01","3P02","14", "11E3", "11E4","11GR", "11GS","35","13E3", "13E4", "13GR","46", "48" ,"72d", "111", "131L", "131N", "11UA", "13GS", "1AJ3", "1BA1", "3BA2", "3BB1", "3BB2"
            };

            DataTable dT = new DataTable();
            DataTable dtOPRO = new DataTable();
            using (var Con = new SqlConnection(connectionstring))
            {
                Con.Open();
                string nameTabulka = "Tab_8";

                string name = "UCET_";
                name += "Tab_8_9_15.xls";

                string kvartal = vytvorAdresare(datum);
                
                System.IO.File.Copy("..\\..\\Hlav\\ZU\\hlpr3_4cv3.xls", "Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name, true);
                Excel.Application app = new Excel.Application();
                string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                Excel.Workbook workbook = app.Workbooks.Open(path + "\\" + "bin\\Debug\\Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name);
                Excel._Worksheet worksheet = workbook.Sheets[1];
                Excel.Range range = worksheet.UsedRange;
                Excel.Range borderRange;
                Excel._Worksheet worksheetFirst = workbook.Sheets[1];

                //DATUM
                worksheet.Cells[1, 6] = nameTabulka;
                worksheet.Cells[4, 1] = "k " + datum;
                worksheet.Cells[6, 2] = worksheet.Cells[6, 2].Value2.ToString() + " " + datum.Substring(6);
                worksheet.Cells[6, 3] = worksheet.Cells[6, 3].Value2.ToString() + " " + datum;
                worksheet.Cells[6, 4] = worksheet.Cells[6, 4].Value2.ToString() + " " + datum;

                double[] sum_all = new double[5];

                //VSETKY TYPY DOKOPY
                for (int i = 0; i < 3; i++)
                {                         
                    //VYTVARANIE NOVEHO ZOSITU PLUS KOPIROVANIE HLAVICKY 
                    //VACSIE AKO 0 LEBO PRVY OTVARAM UZ PRED CYKLOM ABY SOM NASTAVIL HLAVICKU KTORU POTOM KOPIRUJEM
                    if (i > 0)
                    {
                        opro = false;
                        workbook.Sheets[1].Copy(Type.Missing, workbook.Sheets[workbook.Sheets.Count]);
                        worksheet = workbook.Sheets[workbook.Sheets.Count];
                        int help = worksheet.Rows.Count;
                        range = worksheet.get_Range("A10", "F" + help);
                        range.Clear();

                        if (i == 1)
                        {
                            nameTabulka = "Tab_9";
                            worksheet.Cells[1, 6] = nameTabulka;
                            worksheet.Cells[2, 1] = "Schválený rozpočet, upravený rozpočet a plnenie príjmov kapitoly školstva podľa organizácií";
                            worksheet.Cells[3, 1] = "za ostatné rozpočtové zdroje - bez zdroja 111";
                        }
                        if (i == 2)
                        {
                            nameTabulka = "Tab_15";
                            worksheet.Cells[1, 6] = nameTabulka;
                            worksheet.Cells[2, 1] = "Schválený rozpočet, upravený rozpočet a plnenie príjmov príspevkových organizácií";
                            worksheet.Cells[3, 1] = "za ostatné rozpočtové zdroje - bez zdroja 111";
                        }
                    }

                    int rowCount = range.Rows.Count;
                    int start = right_num + 1;
                    int border = start;
                    int strana = 2;
                    worksheet.Name = nameTabulka;
                    for (int j = 0; j < zdrojeList[i].Count(); j++)
                    {
                        range = worksheet.get_Range("A" + start , "F" + start);
                        range.Merge();
                        range.Font.Bold = true;

                        SqlCommand get_name_zdroj = new SqlCommand("SELECT Text FROM ciselnikZdroje WHERE Zdroj = '" + zdrojeList[i][j] + "'", Con);
                        DataTable get_name_zdroj_table = new DataTable();
                        using (SqlDataAdapter get_name_zdroj_adapter = new SqlDataAdapter(get_name_zdroj))
                        {
                            get_name_zdroj_adapter.Fill(get_name_zdroj_table);
                            get_name_zdroj.ExecuteNonQuery();
                        }

                        com = new SqlCommand("SELECT DISTINCT tv.Ico,ci.CelyNazov collate Slovak_CI_AS FROM TableVstup tv JOIN ciselnikIco ci ON tv.Ico=ci.Ico WHERE tv.SyntetickyaleboFiktivny LIKE @Typ AND Zdroj LIKE @Zdroj AND KalendarnyDen LIKE @Datum ORDER BY ci.CelyNazov collate Slovak_CI_AS", Con);
                        com.Parameters.AddWithValue("@Zdroj", zdrojeList[i][j]);
                        com.Parameters.AddWithValue("@Datum", datum);
                        if (i != 2)
                            com.Parameters.AddWithValue("@Typ", typ);
                        else
                            com.Parameters.AddWithValue("@Typ", "PRR");
                        dT = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(com))
                        {
                            da.Fill(dT);
                            com.ExecuteNonQuery();
                        }

                        string[] arrIco;
                        int k = 0;
                        if (opro == true)
                        {
                            arrIco = new string[dT.Rows.Count + 2];
                            arrIco[0] = MSVSico;
                            k++;
                        }
                        else
                            arrIco = new string[dT.Rows.Count];
                        
                        foreach (DataRow row in dT.Rows) // Loop over the rows.
                        {
                            if ((string)row[0] != MSVSico || opro == false)
                            {
                                arrIco[k] = (string)row[0];
                                k++;
                            }
                        }
                        if (opro == true)
                        {
                            arrIco[k++] = "Ostatné priamo riadené organizácie";
                            arrIco[k++] = "Kapitola MŠVVaŠ SR spolu";
                        }

                        if (arrIco.Length > 0)
                        {
                            worksheet.Cells[start, 1] = "Zdroj " + zdrojeList[i][j] + " - " + (string)get_name_zdroj_table.Rows[0][0];
                            increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 8, 11);
                        }
                        else
                            continue;

                        string vynimky;
                        foreach (string helpString in arrIco) // Loop over the rows.
                        {
                            vynimky = vynimkyMRZ(connectionstring);
                            dT = new DataTable();

                            com = new SqlCommand("SELECT SUM(SchvalenyRozpocet),SUM(RozpocetPoZmenach),SUM(Skutocnost) FROM TableVstup tv WHERE SyntetickyaleboFiktivny LIKE @Typ AND Zdroj LIKE @Zdroj AND KalendarnyDen LIKE @Datum AND Ico LIKE @Ico ", Con);
                            com.Parameters.AddWithValue("@Zdroj", zdrojeList[i][j]);
                            com.Parameters.AddWithValue("@Datum", datum);
                            if (i != 2)
                                com.Parameters.AddWithValue("@Typ", typ);
                            else
                                com.Parameters.AddWithValue("@Typ", "PRR");
                            if (helpString == "Kapitola MŠVVaŠ SR spolu" || helpString == "Ostatné priamo riadené organizácie")
                                com.Parameters.AddWithValue("@Ico", "%");
                            else
                                com.Parameters.AddWithValue("@Ico", helpString);

                            using (SqlDataAdapter da = new SqlDataAdapter(com))
                            {
                                da.Fill(dT);
                                com.ExecuteNonQuery();
                            }

                            int comparison = 0;
                            if ((comparison = helpString.CompareTo(MSVSico)) == 0)
                            {
                                using (SqlDataAdapter da = new SqlDataAdapter(com))
                                {
                                    da.Fill(dtOPRO);
                                    com.ExecuteNonQuery();
                                }
                            }

                            k = 0;
                            //zavolanie mena podla ica
                            String nazov_org = "";
                            String skratka_org = "";

                            SqlCommand get_name = new SqlCommand("SELECT CelyNazov, SkratenyNazov from ciselnikIco where Ico = '" + helpString + "'", Con);
                            DataTable get_name_table = new DataTable();
                            using (SqlDataAdapter get_name_adapter = new SqlDataAdapter(get_name))
                            {
                                get_name_adapter.Fill(get_name_table);
                                get_name.ExecuteNonQuery();
                            }
                            if (get_name_table.Rows.Count != 0)
                            {
                                nazov_org = (string)get_name_table.Rows[0][0];
                                skratka_org = (string)get_name_table.Rows[0][1];
                            }
                            else
                            {
                                nazov_org = helpString;
                                skratka_org = helpString;
                            }

                            if ((helpString == MSVSico || helpString == "Ostatné priamo riadené organizácie" || helpString == "Kapitola MŠVVaŠ SR spolu") && i==0)
                            {
                                range = worksheet.get_Range(left_char.ToString() + start, right_char.ToString() + start);
                                range.Font.Bold = true;
                            }

                            worksheet.Cells[start, 1] = nazov_org;

                            for (int j2 = 0; j2 < dT.Rows.Count; j2++)// Loop over the rows.
                            {
                                for (int k2 = 0; k2 < 3; k2++)
                                {
                                    double value = 0;
                                    if (helpString == "Ostatné priamo riadené organizácie")
                                        value = (Double.Parse(dT.Rows[j2][k2].ToString()) - Double.Parse(dtOPRO.Rows[0][k2].ToString()));
                                    else
                                        value = Double.Parse(dT.Rows[j2][k2].ToString());
                                    if (value != 0)
                                        worksheet.Cells[start, 2 + k2] = value;
                                    else
                                        worksheet.Cells[start, 2 + k2] = 0;
                                }
                                double schvalenyRoz = Double.Parse(dT.Rows[j2][0].ToString());
                                double upravenyRoz = Double.Parse(dT.Rows[j2][1].ToString());
                                double plneniePrijmov = Double.Parse(dT.Rows[j2][2].ToString());
                                if (helpString == "Ostatné priamo riadené organizácie")
                                {
                                    //ODRATANIE MSVS
                                    schvalenyRoz -= Double.Parse(dtOPRO.Rows[0][0].ToString());
                                    upravenyRoz -= Double.Parse(dtOPRO.Rows[0][1].ToString());
                                    plneniePrijmov -= Double.Parse(dtOPRO.Rows[0][2].ToString());
                                }

                                //PERCENTA
                                if (plneniePrijmov == 0 || schvalenyRoz == 0)
                                    worksheet.Cells[start, 5] = 0;
                                else
                                    worksheet.Cells[start, 5] = 100 * (double)(plneniePrijmov / schvalenyRoz);

                                if (plneniePrijmov == 0 || upravenyRoz == 0)
                                    worksheet.Cells[start, 6] = 0;
                                else
                                    worksheet.Cells[start, 6] = 100 * (double)(plneniePrijmov / upravenyRoz);
                            }

                            if (helpString != arrIco.Last())
                                increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 8, 11);
                        }
                        //oramovanie za zdrojom kazdym hrube preto nie pre prve kde je jeden zdroj
                        if(i>0)
                        {
                            borderRange = worksheet.get_Range(left_char.ToString() + border, right_char.ToString() + start);//hrube po Celkovo
                            borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                        }

                        if (zdrojeList[i][j] != zdrojeList[i].Last())
                            increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 8, 11);
                    }

                    if(i==0)
                    {
                        //ORAMOVANIE MSVS
                        borderRange = worksheet.get_Range(left_char.ToString() + (right_num + 2), right_char.ToString() + (right_num + 2));
                        borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                        //ORAMOVANIE OPRO A CELKOVO
                        borderRange = worksheet.get_Range(left_char.ToString() + (start - 1), right_char.ToString() + (start - 1));
                        borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                    }
                    for (char c = left_char; c <= (char)((int)right_char); c++)//tenke oramovanie od border po start
                    {
                        borderRange = worksheet.get_Range(c.ToString() + border, c.ToString() + start);
                        if(i==0)
                            borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                        else
                        {
                            Excel.Borders borderSide = borderRange.Borders;
                            borderSide[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlBorderWeight.xlThin;
                            borderSide[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                        }
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
                  
                    //HLAVNE ORAMOVANIE OKOLO CELEHO
                    borderRange = worksheet.get_Range(left_char.ToString() + border, right_char.ToString() + start);//hrube po Celkovo
                    borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                    var culture = new CultureInfo("ru-RU");
                    worksheet.Cells[++start, 1] = "Dátum vytvorenia: " + time.ToString(culture);
                }

                workbook.Save();
                if (priamaTlac)
                    PrintMyExcelFile(workbook);
                Kontrola.release_excel(range, worksheet, workbook, app);
                Con.Close();
            }
        }

        public void spravTab_7_upravene(string typ, string MSVSico, string datum, int typVystupov, string zaokruhlenieMena, string zaokruhleniePercenta, char left_char, int left_num, char right_char, int right_num, int count_for_page, int strana_rel_pos, DateTime time, bool priamaTlac)//1 prijmy 2 vydavky
        {
            string connectionstring = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            SqlCommand com = null;
            bool opro = true;

            DataTable dT = new DataTable();
            DataTable dtOPRO = new DataTable();
            DataTable dtOPRO2 = new DataTable();
            using (var Con = new SqlConnection(connectionstring))
            {
                string nameTabulka = "Tab_7_upravene";

                string name = "UCET_";
                name = name + nameTabulka + ".xls";

                string kvartal = vytvorAdresare(datum);

                System.IO.File.Copy("..\\..\\Hlav\\ZU\\hlpr1cv3_upravene.xls", "Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name, true);
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

                //DATUM
                worksheet.Cells[1, 6] = nameTabulka;
                worksheet.Cells[3, 1] = "za všetky zdroje vrátane iných zdrojov k " + datum;
                worksheet.Cells[5, 2] = worksheet.Cells[5, 2].Value2.ToString() + " " + datum.Substring(6);
                worksheet.Cells[5, 3] = worksheet.Cells[5, 3].Value2.ToString() + " " + datum;
                worksheet.Cells[5, 4] = worksheet.Cells[5, 4].Value2.ToString() + " " + datum;

                double[] sum_all = new double[5];
                Con.Open();

                com = new SqlCommand("SELECT DISTINCT tv.Ico,ci.CelyNazov collate Slovak_CI_AS FROM TableVstup tv JOIN ciselnikIco ci ON tv.Ico=ci.Ico WHERE tv.SyntetickyaleboFiktivny LIKE @typ AND KalendarnyDen LIKE @Datum ORDER BY ci.CelyNazov collate Slovak_CI_AS", Con);
                com.Parameters.AddWithValue("@typ", typ);
                com.Parameters.AddWithValue("@Datum", datum);

                using (SqlDataAdapter da = new SqlDataAdapter(com))
                {
                    da.Fill(dT);
                    com.ExecuteNonQuery();
                }

                string[] arrIco;
                if (opro == true)
                    arrIco = new string[dT.Rows.Count + 2];
                else
                    arrIco = new string[dT.Rows.Count + 1];

                arrIco[0] = MSVSico;
                int i = 1;

                foreach (DataRow row in dT.Rows) // Loop over the rows.
                {
                    if ((string)row[0] != MSVSico)
                    {
                        arrIco[i] = (string)row[0];
                        i++;

                    }
                }
                if (opro == true)
                    arrIco[i++] = "Ostatné priamo riadené organizácie";
                arrIco[i++] = "Kapitola MŠVVaŠ SR spolu";

                string vynimky;
                SqlCommand com2 = null;
                DataTable dT2;


                foreach (string helpString in arrIco) // Loop over the rows.
                {
                    vynimky = vynimkyMRZ(connectionstring);
                    dT = new DataTable();
                    dT2 = new DataTable();

                    com = new SqlCommand("SELECT SUM(SchvalenyRozpocet),SUM(RozpocetPoZmenach),SUM(Skutocnost) FROM TableVstup tv WHERE SyntetickyaleboFiktivny LIKE @typ AND KalendarnyDen LIKE @Datum AND Ico LIKE @Ico ", Con);
                    com2 = new SqlCommand("SELECT SUM(Skutocnost) FROM TableVstup tv WHERE SyntetickyaleboFiktivny LIKE @typ AND KalendarnyDen LIKE @Datum AND Ico LIKE @Ico " + vynimky + "", Con);
                    com2.Parameters.AddWithValue("@typ", typ);
                    com2.Parameters.AddWithValue("@Datum", datum);
                    com.Parameters.AddWithValue("@typ", typ);
                    com.Parameters.AddWithValue("@Datum", datum);

                    if (helpString == "Kapitola MŠVVaŠ SR spolu" || helpString == "Ostatné priamo riadené organizácie")
                    {
                        com.Parameters.AddWithValue("@Ico", "%");
                        com2.Parameters.AddWithValue("@Ico", "%");
                    }
                    else
                    {
                        com.Parameters.AddWithValue("@Ico", helpString);
                        com2.Parameters.AddWithValue("@Ico", helpString);
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(com))
                    using (SqlDataAdapter da2 = new SqlDataAdapter(com2))
                    {
                        da.Fill(dT);
                        da2.Fill(dT2);
                        com.ExecuteNonQuery();
                        com2.ExecuteNonQuery();
                    }

                    int comparison = 0;
                    if ((comparison = helpString.CompareTo(MSVSico)) == 0)
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(com))
                        using (SqlDataAdapter da2 = new SqlDataAdapter(com2))
                        {
                            da.Fill(dtOPRO);
                            da2.Fill(dtOPRO2);
                            com.ExecuteNonQuery();
                            com2.ExecuteNonQuery();
                        }
                    }

                    i = 0;
                    //zavolanie mena podla ica
                    String nazov_org = "";
                    String skratka_org = "";

                    SqlCommand get_name = new SqlCommand("SELECT CelyNazov, SkratenyNazov from ciselnikIco where Ico = '" + helpString + "'", Con);
                    DataTable get_name_table = new DataTable();
                    using (SqlDataAdapter get_name_adapter = new SqlDataAdapter(get_name))
                    {
                        get_name_adapter.Fill(get_name_table);
                        get_name.ExecuteNonQuery();
                    }
                    if (get_name_table.Rows.Count != 0)
                    {
                        nazov_org = (string)get_name_table.Rows[0][0];
                        skratka_org = (string)get_name_table.Rows[0][1];
                    }
                    else
                    {
                        nazov_org = helpString;
                        skratka_org = helpString;
                    }

                    if (helpString == MSVSico || helpString == "Ostatné priamo riadené organizácie" || helpString == "Kapitola MŠVVaŠ SR spolu")
                    {
                        range = worksheet.get_Range(left_char.ToString() + start, right_char.ToString() + start);
                        range.Font.Bold = true;
                    }

                    worksheet.Cells[start, 1] = nazov_org;

                    for (int j = 0; j < dT.Rows.Count; j++)// Loop over the rows.
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            double value = 0;
                            if (helpString == "Ostatné priamo riadené organizácie")
                                value = (Double.Parse(dT.Rows[j][k].ToString()) - Double.Parse(dtOPRO.Rows[0][k].ToString()));
                            else
                                value = Double.Parse(dT.Rows[j][k].ToString());
                            if (value != 0)
                                worksheet.Cells[start, 2 + k] = value;
                            else
                                worksheet.Cells[start, 2 + k] = 0;
                        }
                        //HODNOTY ROZPOCTOV A HODNOT MRZ A BEZMRZ
                        double schvalenyRoz = Double.Parse(dT.Rows[j][0].ToString());
                        double upravenyRoz = Double.Parse(dT.Rows[j][1].ToString());
                        double plneniePrijmov = Double.Parse(dT.Rows[j][2].ToString());
                        double bezmrz = Double.Parse(dT2.Rows[j][0].ToString());
                        double mrz = Double.Parse(dT.Rows[j][2].ToString());
                        if (helpString == "Ostatné priamo riadené organizácie")
                        {
                            //ODRATANIE MSVS
                            schvalenyRoz -= Double.Parse(dtOPRO.Rows[0][0].ToString());
                            upravenyRoz -= Double.Parse(dtOPRO.Rows[0][1].ToString());
                            plneniePrijmov -= Double.Parse(dtOPRO.Rows[0][2].ToString());
                            bezmrz -= Double.Parse(dtOPRO2.Rows[0][0].ToString());
                            mrz -= Double.Parse(dtOPRO.Rows[0][2].ToString());
                        }
                        mrz -= bezmrz;

                        

                        //PERCENTA
                        if (bezmrz == 0 || schvalenyRoz == 0)
                            worksheet.Cells[start, 5] = 0;
                        else
                            worksheet.Cells[start, 5] = 100 * (double)(bezmrz / schvalenyRoz);

                        if (bezmrz == 0 || upravenyRoz == 0)
                            worksheet.Cells[start, 6] = 0;
                        else
                            worksheet.Cells[start, 6] = 100 * (double)(bezmrz / upravenyRoz);
                    }

                    if (helpString != "Kapitola MŠVVaŠ SR spolu")
                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 8, 11);
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

                //ORAMOVANIE MSVS
                borderRange = worksheet.get_Range(left_char.ToString() + (right_num + 1), right_char.ToString() + (right_num + 1));
                borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                //ORAMOVANIE OPRO A CELKOVO
                borderRange = worksheet.get_Range(left_char.ToString() + (start - 1), right_char.ToString() + (start - 1));
                borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

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

        public void spravTab_8_9_15_upravene(string typ, string MSVSico, string datum, int typVystupov, string zaokruhlenieMena, string zaokruhleniePercenta, char left_char, int left_num, char right_char, int right_num, int count_for_page, int strana_rel_pos, DateTime time, bool priamaTlac)//1 prijmy 2 vydavky
        {
            string connectionstring = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            SqlCommand com = null;
            bool opro = true;
            string[][] zdrojeList = new string[3][];
            zdrojeList[0] = new string[]
            {
                "111"
            };
            zdrojeList[1] = new string[]
            {
                "131K","131L","11S1", "11S2", "11T1", "11T2", "13S1", "13T1", "1AA1", "1AA2", "1AA3", "1AC1", "1AC2", "1AC3", "3AA1", "3AC1"
            };
            zdrojeList[2] = new string[]
            {
                "131G", "131H", "131I", "131J", "1AJ1", "3AA1", "3AA2", "3AA3", "3AC1", "3AC2", "3AC3", "3AJ1"
            };

            DataTable dT = new DataTable();
            DataTable dtOPRO = new DataTable();
            using (var Con = new SqlConnection(connectionstring))
            {
                Con.Open();
                string nameTabulka = "Tab_8_upravene";

                string name = "UCET_";
                name += "Tab_8_9_15_upravene.xls";

                string kvartal = vytvorAdresare(datum);

                System.IO.File.Copy("..\\..\\Hlav\\ZU\\hlpr3_4cv3_upravene.xls", "Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name, true);
                Excel.Application app = new Excel.Application();
                string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                Excel.Workbook workbook = app.Workbooks.Open(path + "\\" + "bin\\Debug\\Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name);
                Excel._Worksheet worksheet = workbook.Sheets[1];
                Excel.Range range = worksheet.UsedRange;
                Excel.Range borderRange;
                Excel._Worksheet worksheetFirst = workbook.Sheets[1];

                //DATUM
                worksheet.Cells[1, 5] = nameTabulka;
                worksheet.Cells[4, 1] = "k " + datum;
                worksheet.Cells[6, 2] = worksheet.Cells[6, 2].Value2.ToString() + " " + datum.Substring(6);
                worksheet.Cells[6, 3] = worksheet.Cells[6, 3].Value2.ToString() + " " + datum;
                worksheet.Cells[6, 4] = worksheet.Cells[6, 4].Value2.ToString() + " " + datum;

                double[] sum_all = new double[5];

                //VSETKY TYPY DOKOPY
                for (int i = 0; i < 3; i++)
                {
                    //VYTVARANIE NOVEHO ZOSITU PLUS KOPIROVANIE HLAVICKY 
                    //VACSIE AKO 0 LEBO PRVY OTVARAM UZ PRED CYKLOM ABY SOM NASTAVIL HLAVICKU KTORU POTOM KOPIRUJEM
                    if (i > 0)
                    {
                        opro = false;
                        workbook.Sheets[1].Copy(Type.Missing, workbook.Sheets[workbook.Sheets.Count]);
                        worksheet = workbook.Sheets[workbook.Sheets.Count];
                        int help = worksheet.Rows.Count;
                        range = worksheet.get_Range("A10", "F" + help);
                        range.Clear();

                        if (i == 1)
                        {
                            nameTabulka = "Tab_9_upravene";
                            worksheet.Cells[1, 5] = nameTabulka;
                            worksheet.Cells[2, 1] = "Schválený rozpočet, upravený rozpočet a plnenie príjmov kapitoly školstva podľa organizácií";
                            worksheet.Cells[3, 1] = "za ostatné rozpočtové zdroje - bez zdroja 111";
                        }
                        if (i == 2)
                        {
                            nameTabulka = "Tab_15_upravene";
                            worksheet.Cells[1, 5] = nameTabulka;
                            worksheet.Cells[2, 1] = "Schválený rozpočet, upravený rozpočet a plnenie príjmov príspevkových organizácií";
                            worksheet.Cells[3, 1] = "za ostatné rozpočtové zdroje - bez zdroja 111";
                        }
                    }

                    int rowCount = range.Rows.Count;
                    int start = right_num + 1;
                    int border = start;
                    int strana = 2;
                    worksheet.Name = nameTabulka;
                    for (int j = 0; j < zdrojeList[i].Count(); j++)
                    {
                        range = worksheet.get_Range("A" + start, right_char.ToString() + start);
                        range.Merge();
                        range.Font.Bold = true;

                        SqlCommand get_name_zdroj = new SqlCommand("SELECT Text FROM ciselnikZdroje WHERE Zdroj = '" + zdrojeList[i][j] + "'", Con);
                        DataTable get_name_zdroj_table = new DataTable();
                        using (SqlDataAdapter get_name_zdroj_adapter = new SqlDataAdapter(get_name_zdroj))
                        {
                            get_name_zdroj_adapter.Fill(get_name_zdroj_table);
                            get_name_zdroj.ExecuteNonQuery();
                        }

                        worksheet.Cells[start, 1] = "Zdroj " + zdrojeList[i][j] + " - " + (string)get_name_zdroj_table.Rows[0][0];
                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 8, 11);

                        com = new SqlCommand("SELECT DISTINCT tv.Ico,ci.CelyNazov collate Slovak_CI_AS FROM TableVstup tv JOIN ciselnikIco ci ON tv.Ico=ci.Ico WHERE tv.SyntetickyaleboFiktivny LIKE @Typ AND Zdroj LIKE @Zdroj AND KalendarnyDen LIKE @Datum ORDER BY ci.CelyNazov collate Slovak_CI_AS", Con);
                        com.Parameters.AddWithValue("@Zdroj", zdrojeList[i][j]);
                        com.Parameters.AddWithValue("@Datum", datum);
                        if (i != 2)
                            com.Parameters.AddWithValue("@Typ", typ);
                        else
                            com.Parameters.AddWithValue("@Typ", "PRR%");
                        dT = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(com))
                        {
                            da.Fill(dT);
                            com.ExecuteNonQuery();
                        }

                        string[] arrIco;
                        int k = 0;
                        if (opro == true)
                        {
                            arrIco = new string[dT.Rows.Count + 2];
                            arrIco[0] = MSVSico;
                            k++;
                        }
                        else
                            arrIco = new string[dT.Rows.Count];

                        foreach (DataRow row in dT.Rows) // Loop over the rows.
                        {
                            if ((string)row[0] != MSVSico || opro == false)
                            {
                                arrIco[k] = (string)row[0];
                                k++;
                            }
                        }
                        if (opro == true)
                        {
                            arrIco[k++] = "Ostatné priamo riadené organizácie";
                            arrIco[k++] = "Kapitola MŠVVaŠ SR spolu";
                        }

                        string vynimky;
                        foreach (string helpString in arrIco) // Loop over the rows.
                        {
                            vynimky = vynimkyMRZ(connectionstring);
                            dT = new DataTable();

                            com = new SqlCommand("SELECT SUM(SchvalenyRozpocet),SUM(RozpocetPoZmenach),SUM(Skutocnost) FROM TableVstup tv WHERE SyntetickyaleboFiktivny LIKE @Typ AND Zdroj LIKE @Zdroj AND KalendarnyDen LIKE @Datum AND Ico LIKE @Ico ", Con);
                            com.Parameters.AddWithValue("@Zdroj", zdrojeList[i][j]);
                            com.Parameters.AddWithValue("@Datum", datum);
                            if (i != 2)
                                com.Parameters.AddWithValue("@Typ", typ);
                            else
                                com.Parameters.AddWithValue("@Typ", "PRR");
                            if (helpString == "Kapitola MŠVVaŠ SR spolu" || helpString == "Ostatné priamo riadené organizácie")
                                com.Parameters.AddWithValue("@Ico", "%");
                            else
                                com.Parameters.AddWithValue("@Ico", helpString);

                            using (SqlDataAdapter da = new SqlDataAdapter(com))
                            {
                                da.Fill(dT);
                                com.ExecuteNonQuery();
                            }

                            int comparison = 0;
                            if ((comparison = helpString.CompareTo(MSVSico)) == 0)
                            {
                                using (SqlDataAdapter da = new SqlDataAdapter(com))
                                {
                                    da.Fill(dtOPRO);
                                    com.ExecuteNonQuery();
                                }
                            }

                            k = 0;
                            //zavolanie mena podla ica
                            String nazov_org = "";
                            String skratka_org = "";

                            SqlCommand get_name = new SqlCommand("SELECT CelyNazov, SkratenyNazov from ciselnikIco where Ico = '" + helpString + "'", Con);
                            DataTable get_name_table = new DataTable();
                            using (SqlDataAdapter get_name_adapter = new SqlDataAdapter(get_name))
                            {
                                get_name_adapter.Fill(get_name_table);
                                get_name.ExecuteNonQuery();
                            }
                            if (get_name_table.Rows.Count != 0)
                            {
                                nazov_org = (string)get_name_table.Rows[0][0];
                                skratka_org = (string)get_name_table.Rows[0][1];
                            }
                            else
                            {
                                nazov_org = helpString;
                                skratka_org = helpString;
                            }

                            if ((helpString == MSVSico || helpString == "Ostatné priamo riadené organizácie" || helpString == "Kapitola MŠVVaŠ SR spolu") && i == 0)
                            {
                                range = worksheet.get_Range(left_char.ToString() + start, right_char.ToString() + start);
                                range.Font.Bold = true;
                            }

                            worksheet.Cells[start, 1] = nazov_org;

                            for (int j2 = 0; j2 < dT.Rows.Count; j2++)// Loop over the rows.
                            {
                                for (int k2 = 0; k2 < 3; k2++)
                                {
                                    double value = 0;
                                    if (helpString == "Ostatné priamo riadené organizácie")
                                        value = (Double.Parse(dT.Rows[j2][k2].ToString()) - Double.Parse(dtOPRO.Rows[0][k2].ToString()));
                                    else
                                        value = Double.Parse(dT.Rows[j2][k2].ToString());
                                    if (value != 0)
                                        worksheet.Cells[start, 2 + k2] = value;
                                    else
                                        worksheet.Cells[start, 2 + k2] = 0;
                                }
                                double schvalenyRoz = Double.Parse(dT.Rows[j2][0].ToString());
                                double upravenyRoz = Double.Parse(dT.Rows[j2][1].ToString());
                                double plneniePrijmov = Double.Parse(dT.Rows[j2][2].ToString());
                                if (helpString == "Ostatné priamo riadené organizácie")
                                {
                                    //ODRATANIE MSVS
                                    schvalenyRoz -= Double.Parse(dtOPRO.Rows[0][0].ToString());
                                    upravenyRoz -= Double.Parse(dtOPRO.Rows[0][1].ToString());
                                    plneniePrijmov -= Double.Parse(dtOPRO.Rows[0][2].ToString());
                                }
                                

                                if (plneniePrijmov == 0 || upravenyRoz == 0)
                                    worksheet.Cells[start, 5] = 0;
                                else
                                    worksheet.Cells[start, 5] = 100 * (double)(plneniePrijmov / upravenyRoz);
                            }

                            if (helpString != arrIco.Last())
                                increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 8, 11);
                        }
                        //oramovanie za zdrojom kazdym hrube preto nie pre prve kde je jeden zdroj
                        if (i > 0)
                        {
                            borderRange = worksheet.get_Range(left_char.ToString() + border, right_char.ToString() + start);//hrube po Celkovo
                            borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                        }

                        if (zdrojeList[i][j] != zdrojeList[i].Last())
                            increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 8, 11);
                    }

                    if (i == 0)
                    {
                        //ORAMOVANIE MSVS
                        borderRange = worksheet.get_Range(left_char.ToString() + (right_num + 2), right_char.ToString() + (right_num + 2));
                        borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                        //ORAMOVANIE OPRO A CELKOVO
                        borderRange = worksheet.get_Range(left_char.ToString() + (start - 1), right_char.ToString() + (start - 1));
                        borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                    }
                    for (char c = left_char; c <= (char)((int)right_char); c++)//tenke oramovanie od border po start
                    {
                        borderRange = worksheet.get_Range(c.ToString() + border, c.ToString() + start);
                        if (i == 0)
                            borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                        else
                        {
                            Excel.Borders borderSide = borderRange.Borders;
                            borderSide[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlBorderWeight.xlThin;
                            borderSide[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                        }
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

                    //HLAVNE ORAMOVANIE OKOLO CELEHO
                    borderRange = worksheet.get_Range(left_char.ToString() + border, right_char.ToString() + start);//hrube po Celkovo
                    borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                    var culture = new CultureInfo("ru-RU");
                    worksheet.Cells[++start, 1] = "Dátum vytvorenia: " + time.ToString(culture);
                }

                workbook.Save();
                if (priamaTlac)
                    PrintMyExcelFile(workbook);
                Kontrola.release_excel(range, worksheet, workbook, app);
                Con.Close();
            }
        }


        private string[,] syntetickeTejtoZostavy(string[] synteticke)
        {
            string[,] hotoveSynteticke = new string[10, 2];
            hotoveSynteticke[0, 0] = synteticke[0];
            hotoveSynteticke[0, 1] = "1";
            hotoveSynteticke[1, 0] = synteticke[1];
            hotoveSynteticke[1, 1] = "2";
            hotoveSynteticke[2, 0] = synteticke[4];
            hotoveSynteticke[2, 1] = "1";
            hotoveSynteticke[3, 0] = synteticke[5];
            hotoveSynteticke[3, 1] = "2";
            hotoveSynteticke[4, 0] = synteticke[2];
            hotoveSynteticke[4, 1] = "1";
            hotoveSynteticke[5, 0] = synteticke[3];
            hotoveSynteticke[5, 1] = "2";
            hotoveSynteticke[6, 0] = "MRP";
            hotoveSynteticke[6, 1] = "1";
            hotoveSynteticke[7, 0] = "MRV";
            hotoveSynteticke[7, 1] = "2";
            hotoveSynteticke[8, 0] = synteticke[5];
            hotoveSynteticke[8, 1] = "3";
            hotoveSynteticke[9, 0] = synteticke[5];
            hotoveSynteticke[9, 1] = "4";

            return hotoveSynteticke;
        }
    }
}
