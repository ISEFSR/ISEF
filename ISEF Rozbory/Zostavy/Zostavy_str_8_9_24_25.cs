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
    class Zostavy_str_8_9_24_25 : Zostavy
    {
        public void spravStr_24_25(string typ, string datum, int typVystupov, string zaokruhlenieMena, string zaokruhleniePercenta, char left_char, int left_num, char right_char, int right_num, int count_for_page, int strana_rel_pos, DateTime time, bool priamaTlac)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            SqlCommand com = null;
            DataTable dT = new DataTable();
            using (var Con = new SqlConnection(connectionstring))
            {
                Con.Open();
                string vynimky = vynimkyMRZ(connectionstring);

                com = new SqlCommand("SELECT DISTINCT tv.Zdroj FROM TableVstup tv WHERE tv.SyntetickyaleboFiktivny LIKE @Synteticky AND tv.KalendarnyDen LIKE @Datum " + vynimky + " ORDER BY tv.Zdroj", Con);
                com.Parameters.AddWithValue("@Synteticky", typ);
                com.Parameters.AddWithValue("@Datum", datum);
                
                using (SqlDataAdapter da = new SqlDataAdapter(com))
                {
                    da.Fill(dT);
                    com.ExecuteNonQuery();
                }

                //ZDROJE STATNEHO ROZPOCTU DO ARRAYU
                List<string> zdrojeList = new List<string>();
                int pocetZdrojov = 0;
                foreach (DataRow row in dT.Rows)
                {
                    zdrojeList.Add((string)row[0]);
                    pocetZdrojov++;
                }
                //PRIDANIE SUMARU ZA STATNY ROZPOCET
                zdrojeList.Add("Prostriedky štátneho rozpočtu");
                pocetZdrojov++;

                //OSTATNE ZDROJE DO ARRAYU
                com = new SqlCommand("SELECT DISTINCT tv.Zdroj FROM TableVstup tv WHERE tv.SyntetickyaleboFiktivny LIKE @Synteticky AND tv.KalendarnyDen LIKE @Datum AND NOT (1=1 " + vynimky + ") ORDER BY tv.Zdroj", Con);
                com.Parameters.AddWithValue("@Synteticky", typ);
                com.Parameters.AddWithValue("@Datum", datum);

                dT = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(com))
                {
                    da.Fill(dT);
                    com.ExecuteNonQuery();
                }
                //OSTATNE ZDROJE DO ARRAYU
                foreach (DataRow row in dT.Rows)
                {
                    zdrojeList.Add((string)row[0]);
                    pocetZdrojov++;
                }

                //PRIDANIE CELKOVEHO SUMARU
                zdrojeList.Add("Kapitola MŠVVaŠ SR spolu");
                pocetZdrojov++;

                //ZMENA NA ARRAY KED MAME VSETKY
                string[] arrZdroje = new string[pocetZdrojov];
                arrZdroje = zdrojeList.ToArray();

                string nameTabulka = "str._24-25";

                string name = "UCET_" + nameTabulka + ".xls";

                //PODLA DATUMU ZISTI KVARTAL A ZAROVEN VYTVORI ADRESARE AK ESTE NIESU PRE DANY ROK A KVARTAL
                string kvartal = vytvorAdresare(datum);
                //hl345zv3 JE HLAVICKA ZOSTAVY PRE TENTO TYP
                System.IO.File.Copy("..\\..\\Hlav\\ZU\\hlzu7cv3.xls", "Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name, true);
                Excel.Application app = new Excel.Application();
                string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                Excel.Workbook workbook = app.Workbooks.Open(path + "\\" + "bin\\Debug\\Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name);
                Excel._Worksheet worksheet = workbook.Sheets[1];
                Excel.Range range = worksheet.UsedRange;
                Excel.Range borderRange;
                Excel._Worksheet worksheetFirst = workbook.Sheets[1];

                //VYPISANIE HLAVICKY KTORA SA BUDE KOPIROVAT
                //TEXTY V EXCELI
                worksheet.Cells[2, 1] = worksheet.Cells[2, 1].Value2.ToString() + " " + datum;
                worksheet.Cells[4, 3] = worksheet.Cells[4, 3].Value2.ToString() + " " + datum.Substring(datum.Length - 4);
                worksheet.Cells[4, 6] = worksheet.Cells[4, 6].Value2.ToString() + " " + datum;
                worksheet.Cells[4, 9] = worksheet.Cells[4, 9].Value2.ToString() + " " + datum;

                int rowCount = range.Rows.Count;
                int start = right_num + 1;
                int border = start;
                int strana = 2;
                double[] sumar = new double[9];
                bool ineProstriedky = false;
                worksheet.Name = nameTabulka;

                //CYKLUS PRE KAZDY ZDROJ
                for (int i = 0; i < pocetZdrojov; i++)
                {
                    //NAZOV ZDROJA A VYPLNENIE KOLONIEK ROVNAKO PRE VSETKY
                    com = new SqlCommand("SELECT Text FROM ciselnikZdroje WHERE Zdroj LIKE @Zdroj", Con);
                    com.Parameters.AddWithValue("@Zdroj", arrZdroje[i] + "%");
                    dT = new DataTable();

                    using (SqlDataAdapter da = new SqlDataAdapter(com))
                    {
                        da.Fill(dT);
                        com.ExecuteNonQuery();
                    }
                    if (dT.Rows.Count != 0)
                        worksheet.Cells[start, 2] = (string)dT.Rows[0][0];
                    else
                    {
                        //TU SA VYPISUJU SUMARE
                        worksheet.Cells[start, 1] = arrZdroje[i].ToString();
                        //CISELNE HODNOTY
                        for(int counter=0;counter<9;counter++)
                            worksheet.Cells[start, 3+counter] = sumar[counter];

                        //PERCENTA
                        for(int counter=0;counter<3;counter++)
                        {
                            if (sumar[0+counter] == 0 || sumar[6+counter] == 0)
                                worksheet.Cells[start, 12 + counter] = 0;
                            else
                                worksheet.Cells[start, 12 + counter] = (sumar[6 + counter] / sumar[0 + counter]) * 100;

                            if (sumar[3 + counter] == 0 || sumar[6 + counter] == 0)
                                worksheet.Cells[start, 15 + counter] = 0;
                            else
                                worksheet.Cells[start, 15 + counter] = (sumar[6 + counter] / sumar[3 + counter]) * 100;
                        }

                        borderRange = worksheet.get_Range(left_char.ToString() + start, right_char.ToString() + start);
                        borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                        borderRange.Font.Bold = true;
                        ineProstriedky = true;

                        if (i!=(pocetZdrojov-1))
                            increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 20, 20);
                        continue;
                    }

                    worksheet.Cells[start, 1] = arrZdroje[i].ToString();

                    com = new SqlCommand("SELECT SUM(SchvalenyRozpocet),SUM(RozpocetPoZmenach),SUM(Skutocnost) FROM TableVstup WHERE Zdroj LIKE @Zdroj AND SyntetickyaleboFiktivny LIKE @Synteticky AND KalendarnyDen LIKE @Datum", Con);
                    com.Parameters.AddWithValue("@Synteticky", typ);
                    com.Parameters.AddWithValue("@Zdroj", arrZdroje[i]);
                    com.Parameters.AddWithValue("@Datum", datum);

                    SqlCommand com2 = null;
                    com2 = new SqlCommand("SELECT COALESCE(SUM(SchvalenyRozpocet),0),COALESCE(SUM(RozpocetPoZmenach),0),COALESCE(SUM(Skutocnost),0) FROM TableVstup WHERE Zdroj LIKE @Zdroj AND SyntetickyaleboFiktivny LIKE @Synteticky AND KalendarnyDen LIKE @Datum AND Polozka LIKE '7%'", Con);
                    com2.Parameters.AddWithValue("@Synteticky", typ);
                    com2.Parameters.AddWithValue("@Zdroj", arrZdroje[i]);
                    com2.Parameters.AddWithValue("@Datum", datum);

                    dT = new DataTable();
                    DataTable dT2 = new DataTable();
                    using (SqlDataAdapter da = new SqlDataAdapter(com))
                    using (SqlDataAdapter da2 = new SqlDataAdapter(com2))
                    {
                        da.Fill(dT);
                        da2.Fill(dT2);
                        com.ExecuteNonQuery();
                        com2.ExecuteNonQuery();
                    }

                    //ULOZENIE HODNOT DO POLI
                    double[] kapitaloveArray = new double[] { Double.Parse(dT2.Rows[0][0].ToString()), Double.Parse(dT2.Rows[0][1].ToString()), Double.Parse(dT2.Rows[0][2].ToString()) };
                    double[] spoluArray = new double[] { Double.Parse(dT.Rows[0][0].ToString()), Double.Parse(dT.Rows[0][1].ToString()), Double.Parse(dT.Rows[0][2].ToString()) };

                    //ZAPISOVANIE HODNOT
                    for (int counter = 1; counter <= 3; counter++)
                    {
                        worksheet.Cells[start, counter * 3] = spoluArray[counter-1] - kapitaloveArray[counter - 1];
                        worksheet.Cells[start, (counter * 3) + 1] = kapitaloveArray[counter - 1];
                        worksheet.Cells[start, (counter * 3) + 2] = spoluArray[counter - 1];
                        //UKLADANIE DO SUMARU
                        sumar[(counter*3) -3] += spoluArray[counter - 1] - kapitaloveArray[counter - 1];
                        sumar[(counter * 3) - 2] += kapitaloveArray[counter - 1];
                        sumar[(counter * 3) - 1] += spoluArray[counter - 1];
                    }

                    //ZAPISOVANIE PERCENT
                    //BEZNE
                    double bezneSR = spoluArray[0] - kapitaloveArray[0];
                    double bezneUR = spoluArray[1] - kapitaloveArray[1];
                    double bezneCerpanie = spoluArray[2] - kapitaloveArray[2];

                    if (bezneSR == 0 || bezneCerpanie == 0)
                        worksheet.Cells[start, 12] = 0;
                    else
                        worksheet.Cells[start, 12] = (bezneCerpanie / bezneSR) * 100;

                    if (bezneUR == 0 || bezneCerpanie == 0)
                        worksheet.Cells[start, 15] = 0;
                    else
                        worksheet.Cells[start, 15] = (bezneCerpanie / bezneUR) * 100;

                    //KAPITALOVE
                    if (kapitaloveArray[0] == 0 || kapitaloveArray[2] == 0)
                        worksheet.Cells[start, 13] = 0;
                    else
                        worksheet.Cells[start, 13] = (kapitaloveArray[2] / kapitaloveArray[0]) * 100;

                    if (kapitaloveArray[1] == 0 || kapitaloveArray[2] == 0)
                        worksheet.Cells[start, 16] = 0;
                    else
                        worksheet.Cells[start, 16] = (kapitaloveArray[2] / kapitaloveArray[1]) * 100;

                    //SPOLU
                    if (spoluArray[0] == 0 || spoluArray[2] == 0)
                        worksheet.Cells[start, 14] = 0;
                    else
                        worksheet.Cells[start, 14] = (spoluArray[2] / spoluArray[0]) * 100;

                    if (spoluArray[1] == 0 || spoluArray[2] == 0)
                        worksheet.Cells[start, 17] = 0;
                    else
                        worksheet.Cells[start, 17] = (spoluArray[2] / spoluArray[1]) * 100;

                    //ORAMOVANIE A TUCNYM INE PROSTRIEDKY AKO STATNE
                    if(ineProstriedky==true)
                    {
                        borderRange = worksheet.get_Range(left_char.ToString() + start, right_char.ToString() + start);
                        borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                        borderRange.Font.Bold = true;
                    }

                    increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 20, 20);
                }
                //ORAMOVANIE V EXCELE
                //JEDNOTLIVE STLPCE
                for (char c = left_char; c <= (char)((int)right_char); c++)//tenke oramovanie od border po start
                {
                    borderRange = worksheet.get_Range(c.ToString() + border, c.ToString() + start);
                    borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                    //ZAOKRUHLOVANIE
                    //CISLA
                    if (c > left_char + 1 && c < left_char + 11)
                    {
                        borderRange.NumberFormat = zaokruhlenieMena;
                        borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                    }
                    //PERCENTA
                    else
                    {
                        if (c > left_char + 10)
                        {
                            borderRange.NumberFormat = zaokruhleniePercenta;
                            borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                        }
                    }
                    //ZAROVANNIE KATEGORIE
                    if (c <= left_char + 1)
                    {
                        borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        borderRange.NumberFormat = "@";
                    }
                }

                //HLAVNE ORAMOVANIE
                borderRange = worksheet.get_Range(left_char.ToString() + border, right_char.ToString() + start);//hrube po Celkovo
                borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                var culture = new CultureInfo("ru-RU");
                worksheet.Cells[++start, 1] = "Dátum vytvorenia: " + time.ToString(culture);
                                 
                //ZAVRIE EXCEL
                workbook.Save();
                if (priamaTlac)
                    PrintMyExcelFile(workbook);
                Kontrola.release_excel(range, worksheet, workbook, app);
                Con.Close();
            }
        }

        public void spravStr_8_9(string typ, string datum, int typVystupov, string zaokruhlenieMena, string zaokruhleniePercenta, char left_char, int left_num, char right_char, int right_num, int count_for_page, int strana_rel_pos, DateTime time, bool priamaTlac)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            SqlCommand com = null;
            DataTable dT = new DataTable();
            using (var Con = new SqlConnection(connectionstring))
            {
                Con.Open();
                string vynimky = vynimkyMRZ(connectionstring);

                com = new SqlCommand("SELECT DISTINCT tv.Zdroj FROM TableVstup tv WHERE tv.SyntetickyaleboFiktivny LIKE @Synteticky AND tv.KalendarnyDen LIKE @Datum " + vynimky + " ORDER BY tv.Zdroj", Con);
                com.Parameters.AddWithValue("@Synteticky", typ);
                com.Parameters.AddWithValue("@Datum", datum);

                using (SqlDataAdapter da = new SqlDataAdapter(com))
                {
                    da.Fill(dT);
                    com.ExecuteNonQuery();
                }

                //ZDROJE STATNEHO ROZPOCTU DO ARRAYU
                List<string> zdrojeList = new List<string>();
                int pocetZdrojov = 0;
                foreach (DataRow row in dT.Rows)
                {
                    zdrojeList.Add((string)row[0]);
                    pocetZdrojov++;
                }
                //PRIDANIE SUMARU ZA STATNY ROZPOCET
                zdrojeList.Add("Prostriedky štátneho rozpočtu");
                pocetZdrojov++;

                //OSTATNE ZDROJE DO ARRAYU
                com = new SqlCommand("SELECT DISTINCT tv.Zdroj FROM TableVstup tv WHERE tv.SyntetickyaleboFiktivny LIKE @Synteticky AND tv.KalendarnyDen LIKE @Datum AND NOT (1=1 " + vynimky + ") ORDER BY tv.Zdroj", Con);
                com.Parameters.AddWithValue("@Synteticky", typ);
                com.Parameters.AddWithValue("@Datum", datum);

                dT = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(com))
                {
                    da.Fill(dT);
                    com.ExecuteNonQuery();
                }
                //OSTATNE ZDROJE DO ARRAYU
                foreach (DataRow row in dT.Rows)
                {
                    zdrojeList.Add((string)row[0]);
                    pocetZdrojov++;
                }

                //PRIDANIE SUMARU ZA MIMOROZPOCTOVE
                zdrojeList.Add("Mimorozpočtové prostriedky(MRZ 35)");
                pocetZdrojov++;

                //PRIDANIE CELKOVEHO SUMARU
                zdrojeList.Add("Kapitola MŠVVaŠ SR spolu");
                pocetZdrojov++;

                //ZMENA NA ARRAY KED MAME VSETKY
                string[] arrZdroje = new string[pocetZdrojov];
                arrZdroje = zdrojeList.ToArray();

                string nameTabulka = "str._8-9";

                string name = "UCET_" + nameTabulka + ".xls";

                //PODLA DATUMU ZISTI KVARTAL A ZAROVEN VYTVORI ADRESARE AK ESTE NIESU PRE DANY ROK A KVARTAL
                string kvartal = vytvorAdresare(datum);
                //hl345zv3 JE HLAVICKA ZOSTAVY PRE TENTO TYP
                System.IO.File.Copy("..\\..\\Hlav\\ZU\\hlpu16.xls", "Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name, true);
                Excel.Application app = new Excel.Application();
                string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                Excel.Workbook workbook = app.Workbooks.Open(path + "\\" + "bin\\Debug\\Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name);
                Excel._Worksheet worksheet = workbook.Sheets[1];
                Excel.Range range = worksheet.UsedRange;
                Excel.Range borderRange;
                Excel._Worksheet worksheetFirst = workbook.Sheets[1];

                //VYPISANIE HLAVICKY KTORA SA BUDE KOPIROVAT
                //TEXTY V EXCELI
                worksheet.Cells[2, 1] = worksheet.Cells[2, 1].Value2.ToString() + " " + datum;
                worksheet.Cells[4, 3] = worksheet.Cells[4, 3].Value2.ToString() + " " + datum.Substring(datum.Length - 4);
                worksheet.Cells[4, 4] = worksheet.Cells[4, 4].Value2.ToString() + " " + datum;
                worksheet.Cells[4, 5] = worksheet.Cells[4, 5].Value2.ToString() + " " + datum;

                int rowCount = range.Rows.Count;
                int start = right_num + 1;
                int border = start;
                int strana = 2;
                double[] sumar = new double[3];
                double[] zvysneSumar = new double[3];
                bool ineProstriedky = false;
                worksheet.Name = nameTabulka;

                //CYKLUS PRE KAZDY ZDROJ
                for (int i = 0; i < pocetZdrojov; i++)
                {
                    //NAZOV ZDROJA A VYPLNENIE KOLONIEK ROVNAKO PRE VSETKY
                    com = new SqlCommand("SELECT Text FROM ciselnikZdroje WHERE Zdroj LIKE @Zdroj", Con);
                    com.Parameters.AddWithValue("@Zdroj", arrZdroje[i] + "%");
                    dT = new DataTable();

                    using (SqlDataAdapter da = new SqlDataAdapter(com))
                    {
                        da.Fill(dT);
                        com.ExecuteNonQuery();
                    }
                    if (dT.Rows.Count != 0)
                        worksheet.Cells[start, 2] = (string)dT.Rows[0][0];
                    else
                    {
                        //TU SA VYPISUJU SUMARE
                        worksheet.Cells[start, 1] = arrZdroje[i].ToString();
                        //CISELNE HODNOTY
                        for (int counter = 0; counter < 3; counter++)
                        {
                            worksheet.Cells[start, 3 + counter] = sumar[counter];
                            if(ineProstriedky)
                                worksheet.Cells[start, 3 + counter] = zvysneSumar[counter];
                        }

                        //PERCENTA
                        if(!ineProstriedky)
                        {
                            if (sumar[0] == 0 || sumar[2] == 0)
                                worksheet.Cells[start, 6] = 0;
                            else
                                worksheet.Cells[start, 6] = (sumar[2] / sumar[0]) * 100;

                            if (sumar[1] == 0 || sumar[2] == 0)
                                worksheet.Cells[start, 7] = 0;
                            else
                                worksheet.Cells[start, 7] = (sumar[2] / sumar[1]) * 100;
                        }
                        else
                        {
                            if (zvysneSumar[0] == 0 || zvysneSumar[2] == 0)
                                worksheet.Cells[start, 6] = 0;
                            else
                                worksheet.Cells[start, 6] = (zvysneSumar[2] / zvysneSumar[0]) * 100;

                            if (zvysneSumar[1] == 0 || zvysneSumar[2] == 0)
                                worksheet.Cells[start, 7] = 0;
                            else
                                worksheet.Cells[start, 7] = (zvysneSumar[2] / zvysneSumar[1]) * 100;
                        }
                        
                        borderRange = worksheet.get_Range(left_char.ToString() + start, right_char.ToString() + start);
                        borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                        borderRange.Font.Bold = true;
                        ineProstriedky = !ineProstriedky;

                        if (i != (pocetZdrojov - 1))
                            increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 20, 20);
                        continue;
                    }

                    worksheet.Cells[start, 1] = arrZdroje[i].ToString();

                    com = new SqlCommand("SELECT SUM(SchvalenyRozpocet),SUM(RozpocetPoZmenach),SUM(Skutocnost) FROM TableVstup WHERE Zdroj LIKE @Zdroj AND SyntetickyaleboFiktivny LIKE @Synteticky AND KalendarnyDen LIKE @Datum", Con);
                    com.Parameters.AddWithValue("@Synteticky", typ);
                    com.Parameters.AddWithValue("@Zdroj", arrZdroje[i]);
                    com.Parameters.AddWithValue("@Datum", datum);

                    dT = new DataTable();
                    using (SqlDataAdapter da = new SqlDataAdapter(com))
                    {
                        da.Fill(dT);
                        com.ExecuteNonQuery();
                    }

                    //ULOZENIE HODNOT DO POLI
                    double[] spoluArray = new double[] { Double.Parse(dT.Rows[0][0].ToString()), Double.Parse(dT.Rows[0][1].ToString()), Double.Parse(dT.Rows[0][2].ToString()) };

                    //ZAPISOVANIE HODNOT
                    for (int counter = 0; counter < 3; counter++)
                    {
                        worksheet.Cells[start, 3+counter] = spoluArray[counter];
                        sumar[counter] += spoluArray[counter];
                        if(ineProstriedky)
                            zvysneSumar[counter] += spoluArray[counter];
                    }

                    //ZAPISOVANIE PERCENT
                    if (spoluArray[0] == 0 || spoluArray[2] == 0)
                        worksheet.Cells[start, 6] = 0;
                    else
                        worksheet.Cells[start, 6] = (spoluArray[2] / spoluArray[0]) * 100;

                    if (spoluArray[1] == 0 || spoluArray[2] == 0)
                        worksheet.Cells[start, 7] = 0;
                    else
                        worksheet.Cells[start, 7] = (spoluArray[2] / spoluArray[1]) * 100;

                    //ORAMOVANIE A TUCNYM INE PROSTRIEDKY AKO STATNE
                    if (ineProstriedky == true)
                    {
                        borderRange = worksheet.get_Range(left_char.ToString() + start, right_char.ToString() + start);
                        borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                        borderRange.Font.Bold = true;
                    }

                    increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 20, 20);
                }
                //ORAMOVANIE V EXCELE
                //JEDNOTLIVE STLPCE
                for (char c = left_char; c <= (char)((int)right_char); c++)//tenke oramovanie od border po start
                {
                    borderRange = worksheet.get_Range(c.ToString() + border, c.ToString() + start);
                    borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                    //ZAOKRUHLOVANIE
                    //CISLA
                    if (c > left_char + 1 && c < left_char + 5)
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
                    }
                    //ZAROVANNIE KATEGORIE
                    if (c <= left_char + 1)
                    {
                        borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        borderRange.NumberFormat = "@";
                    }
                }

                //HLAVNE ORAMOVANIE
                borderRange = worksheet.get_Range(left_char.ToString() + border, right_char.ToString() + start);//hrube po Celkovo
                borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                var culture = new CultureInfo("ru-RU");
                worksheet.Cells[++start, 1] = "Dátum vytvorenia: " + time.ToString(culture);

                //ZAVRIE EXCEL
                workbook.Save();
                if (priamaTlac)
                    PrintMyExcelFile(workbook);
                Kontrola.release_excel(range, worksheet, workbook, app);
                Con.Close();
            }
        }
    }
}
