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
    class Zostavy_P8 : Zostavy
    {
        public void spravZostavyZaPolozky(string typ, int prijmyVydavkyVeda, Boolean lenOkruhy, string MSVSico, string datum, int typVystupov, string zaokruhlenieMena, string zaokruhleniePercenta, char left_char, int left_num, char right_char, int right_num, int count_for_page, int strana_rel_pos, DateTime time, bool priamaTlac)
        {
            // int prijmyVydavkyVeda:
            //  1 - prijmy
            //  2 - vydavky
            //  3, 4 - veda

            string connectionstring = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            string[] synteticke = najdiSynteticke(connectionstring);
            string ekonomickaKlasifikacia = zistiEkonomickuKlasifikaciu(prijmyVydavkyVeda,2, connectionstring);
            string specialnePolozky = " AND (";
            string atribut = "pol_tab8";
            //VYNIMKY VEDY
            string veda = "";
            if (prijmyVydavkyVeda == 3 || prijmyVydavkyVeda == 4)
                veda = vynimkyVedy(prijmyVydavkyVeda, connectionstring);
            SqlCommand com = null;

            DataTable dT = new DataTable();
            using (var Con = new SqlConnection(connectionstring))
            {
                Con.Open();
                //VYBER PODPOLOZKY PODLA SPECIFICKEHO VYBERU
                SqlCommand comPodpolozka = null;
                DataTable dTPodpolozka = new DataTable();
                comPodpolozka = new SqlCommand("SELECT Pol, znacka FROM ciselnikPodpolozka WHERE " + atribut + " LIKE 'A%' " + ekonomickaKlasifikacia + " ORDER BY Pol", Con);

                using (SqlDataAdapter daPodpolozka = new SqlDataAdapter(comPodpolozka))
                {
                    daPodpolozka.Fill(dTPodpolozka);
                    comPodpolozka.ExecuteNonQuery();
                }
                //VYTVORENIE PODMIENKY DO SELECTU
                for (int i = 0; i < dTPodpolozka.Rows.Count; i++)
                {
                    if (i > 0)
                        specialnePolozky = specialnePolozky + "OR ";
                    string helper = dTPodpolozka.Rows[i][0].ToString().Substring(0, Int32.Parse(dTPodpolozka.Rows[i][1].ToString()));
                    specialnePolozky = specialnePolozky + " Podpolozka LIKE '" + helper + "%' ";
                }
                specialnePolozky = specialnePolozky + ") ";

                //VYHLADA VSETKY ROZNE ZDROJE ZO VSTUPOV 
                com = new SqlCommand("SELECT DISTINCT Zdroj FROM TableVstup WHERE SyntetickyaleboFiktivny LIKE @Synteticky AND KalendarnyDen LIKE @Datum  " + veda + " " + specialnePolozky + " ORDER BY Zdroj", Con);
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

                int typSynteticky = Array.IndexOf(synteticke, typ);
                string nameTabulka = "";
                string kvartal = vytvorAdresare(datum);
                
                if (typ == synteticke[0] || typ == synteticke[1])
                    nameTabulka = nameTabulka + "p";

                if (typ == synteticke[0] || typ == synteticke[4])
                {
                    if(lenOkruhy)
                        nameTabulka = nameTabulka + "8d";
                    else
                        nameTabulka = nameTabulka + "8c";
                }
                else
                {
                    if (lenOkruhy)
                        nameTabulka = nameTabulka + "8b";
                    else
                        nameTabulka = nameTabulka + "8a";

                    if (prijmyVydavkyVeda == 3)
                        nameTabulka = nameTabulka + "1";
                    if (prijmyVydavkyVeda == 5)
                        nameTabulka = nameTabulka + "11";
                    if (prijmyVydavkyVeda == 6)
                        nameTabulka = nameTabulka + "12";
                }
                string name = "RZBT" + nameTabulka + "vz.xls";
                
                //hl8KMzV3 JE HLAVICKA ZOSTAVY PRE TENTO TYP
                System.IO.File.Copy("..\\..\\Hlav\\RZB\\hl8KMzV3.xls", "Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name, true);

                Excel.Application app = new Excel.Application();
                string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                Excel.Workbook workbook = app.Workbooks.Open(path + "\\" + "bin\\Debug\\Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name);
                Excel._Worksheet worksheet = workbook.Sheets[1];//pre citanie excelu
                Excel.Range range = worksheet.UsedRange;//pre citanie excelu
                Excel.Range borderRange;
                //Excel._Worksheet worksheetFirst = workbook.Sheets[1];//pre citanie excelu

                //VYPISANIE HLAVICKY KTORA SA BUDE KOPIROVAT
                //TEXTY V EXCELI
                if (typ == synteticke[0]|| typ == synteticke[1])
                    worksheet.Cells[1, 7] = "Príspevkové organizácie";
                else
                    worksheet.Cells[1, 7] = "Rozpočtové organizácie";
                if (prijmyVydavkyVeda == 1 && !lenOkruhy)
                    worksheet.Cells[2, 1] = "Plnenie príjmov za vybrane položky podľa org. v rezorte školstva";
                else if (prijmyVydavkyVeda == 1 && lenOkruhy)
                    worksheet.Cells[2, 1] = "Plnenie príjmov za vybrane položky podľa okruhov v rezorte školstva";
                else
                {
                    if (prijmyVydavkyVeda == 2)
                    {
                        if (!lenOkruhy)
                            worksheet.Cells[2, 1] = "Rozbor výdavkov za vybrane položky podľa org. v rezorte školstva";
                        else
                            worksheet.Cells[2, 1] = "Rozbor výdavkov za vybrane položky podľa okruhov v rezorte školstva";
                    }
                    else
                    {
                        if (!lenOkruhy)
                            worksheet.Cells[2, 1] = "Rozbor výdavkov za vybrane položky podľa org. v rezorte školstva za vedu";
                        else
                            worksheet.Cells[2, 1] = "Rozbor výdavkov za vybrane položky podľa okruhov v rezorte školstva za vedu";
                    }
                }
                if (typVystupov == 1)
                    worksheet.Cells[3, 1] = "(údaje sú v €)";
                else
                    worksheet.Cells[3, 1] = "(údaje sú v tisícoch €)";

                worksheet.Cells[6, 1].Style.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                worksheet.Cells[6, 1] = "Spracovateľské obdobie: " + datum;
                worksheet.Cells[6, 8] = "Strana: 1";
                worksheet.Name = "Celkovo";
                worksheet.Cells[6, 7] = nameTabulka + "v";

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
                        range = worksheet.get_Range(left_char + (right_num + 1).ToString(), right_char + help.ToString());
                        range.Clear();
                        worksheet.Name = arrZdroje[i];
                        //DOPLNENIE NAZVU TABULKY DO EXCELU ZA KONKRETNY ZDROJ
                        worksheet.Cells[6, 7] = nameTabulka + "z" + arrZdroje[i];
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
                        if (arrZdroje[i] != "7%")
                            worksheet.Cells[3, 1] = "za zdroj " + arrZdroje[i] + " - " + (string)dT.Rows[0][0];
                        else
                            worksheet.Cells[3, 1] = "za zdroj 7 - " + (string)dT.Rows[0][0];
                    }

                    int rowCount = range.Rows.Count;//pre citanie excelu
                    int start = right_num + 1;
                    int border, borderIco = 0;
                    int strana = 2;
                    string[] arrIco ;

                    //KED ROBI ZA NORMALNE , ELSE KED ROBI LEN OKRUHY
                    if (lenOkruhy==false)
                    {
                        //VYBER VSETKYCH ROZNYCH ICO ZA JENDOTLIVY ZDROJ
                        com = new SqlCommand("SELECT DISTINCT tv.Ico,ci.CelyNazov collate Slovak_CI_AS FROM TableVstup tv JOIN ciselnikIco ci ON tv.Ico=ci.Ico WHERE tv.SyntetickyaleboFiktivny LIKE @Synteticky AND tv.Zdroj LIKE @Zdroj AND KalendarnyDen LIKE @Datum " + veda + " " + specialnePolozky + "  ORDER BY ci.CelyNazov collate Slovak_CI_AS", Con);
                        com.Parameters.AddWithValue("@Synteticky", typ);
                        com.Parameters.AddWithValue("@Zdroj", arrZdroje[i]);
                        com.Parameters.AddWithValue("@Datum", datum);
                        dT = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(com))
                        {
                            da.Fill(dT);
                            com.ExecuteNonQuery();
                        }
                        arrIco = new string[dT.Rows.Count + 1];

                        int j = 0;

                        //ICA DO ARRAYU
                        foreach (DataRow row in dT.Rows)
                        {
                            arrIco[j] = (string)row[0];
                            j++;
                        }
                        arrIco[j] = "Celkom:";
                    }
                    else
                    {
                        arrIco = new string[2];
                        if(typ == synteticke[0] || typ == synteticke[1] || typ == synteticke[2] || typ == synteticke[3])
                            arrIco[0] = "Príspevkové organizácie";
                        else
                            arrIco[0] = "Ostatné priamo riadené organizácie + MŠVVaŠ SR (Vrátane PJ)";
                        arrIco[1] = "Celkom";
                    }
                    border = start;
                    ////VYBER VSETKYCH ROZNYCH ICO ZA JENDOTLIVY ZDROJ
                    //com = new SqlCommand("SELECT DISTINCT tv.Ico,ci.CelyNazov collate Slovak_CI_AS FROM TableVstup tv JOIN ciselnikIco ci ON tv.Ico=ci.Ico WHERE tv.SyntetickyaleboFiktivny LIKE @Synteticky AND tv.Zdroj LIKE @Zdroj AND KalendarnyDen LIKE @Datum " + veda + " " + specialnePolozky + "  ORDER BY ci.CelyNazov collate Slovak_CI_AS", Con);
                    //com.Parameters.AddWithValue("@Synteticky", typ);
                    //com.Parameters.AddWithValue("@Zdroj", arrZdroje[i]);
                    //com.Parameters.AddWithValue("@Datum", datum);
                    //dT = new DataTable();

                    //using (SqlDataAdapter da = new SqlDataAdapter(com))
                    //{
                    //    da.Fill(dT);
                    //    com.ExecuteNonQuery();
                    //}

                    //string[] arrIco = new string[dT.Rows.Count + 1];

                    //int j = 0;

                    ////ICA DO ARRAYU
                    //foreach (DataRow row in dT.Rows)
                    //{
                    //    arrIco[j] = (string)row[0];
                    //    j++;
                    //}
                    //arrIco[j] = "Celkom:";
                    //border = start;
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

                        while (start % count_for_page > count_for_page - 4)
                            increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 4, 7, 20, 20);

                        //NAZOV DO EXCELU S ODSADENIM PLUS KONTROLA CI NEROBI FINALNY SUMAR + NASTAVI HODNOTU ICO DO SELECTU
                        worksheet.get_Range(left_char + (start).ToString(), right_char + (start + 2).ToString()).Merge();
                        worksheet.get_Range(left_char + (start).ToString(), right_char + (start + 2).ToString()).VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        worksheet.get_Range(left_char + (start).ToString(), right_char + (start + 2).ToString()).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        if (get_name_table.Rows.Count != 0)
                            worksheet.Cells[start, 1] = "              Organizácia: " + (string)get_name_table.Rows[0][0];
                        else
                        {
                            worksheet.Cells[start, 1] = "              Organizácia: " + helpString;
                        }

                        //POSUN O 3 RIADKY KEDZE 3 ZLUCUJEME
                        increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 4, 7, 20, 20);
                        increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 4, 7, 20, 20);
                        increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 4, 7, 20, 20);
                        borderIco = start;

                        //CYKLUS PRE VSETKY MOZNE PODPOLOZKY
                        foreach (DataRow rowPodpolozka in dTPodpolozka.Rows)
                        {
                            double[] arrayMSVS = new double[3];
                            //SELECT NA VYBER UDAJOV NA VYPIS
                            com = new SqlCommand("SELECT COALESCE(SUM(SchvalenyRozpocet), 0),COALESCE(SUM(RozpocetPoZmenach), 0),COALESCE(SUM(Skutocnost), 0) FROM TableVstup WHERE Zdroj LIKE @Zdroj AND SyntetickyaleboFiktivny LIKE @Synteticky " + veda + " AND Podpolozka LIKE @Podpolozka AND Ico LIKE @Ico AND KalendarnyDen LIKE @Datum", Con);
                            com.Parameters.AddWithValue("@Synteticky", typ);
                            com.Parameters.AddWithValue("@Zdroj", arrZdroje[i]);
                            com.Parameters.AddWithValue("@Datum", datum);
                            if (Int32.Parse(rowPodpolozka[1].ToString()) != 4)
                                com.Parameters.AddWithValue("@Podpolozka", rowPodpolozka[0].ToString().Substring(0, Int32.Parse(rowPodpolozka[1].ToString())) + "%");
                            else
                                com.Parameters.AddWithValue("@Podpolozka", rowPodpolozka[0].ToString().Substring(0, 6) + "%");
                            if (get_name_table.Rows.Count != 0)
                                com.Parameters.AddWithValue("@Ico", helpString);
                            else
                                com.Parameters.AddWithValue("@Ico", "%");

                            /*if (get_name_table.Rows.Count != 0)
                                com.Parameters.AddWithValue("@Ico", helpString);
                            else
                            {
                                com.Parameters.AddWithValue("@Ico", "%");

                                /*if (helpString == "Ostatné priamo riadené organizácie")
                                {
                                    SqlCommand comOpro = new SqlCommand("SELECT COALESCE(SUM(SchvalenyRozpocet), 0),COALESCE(SUM(RozpocetPoZmenach), 0),COALESCE(SUM(Skutocnost), 0) FROM TableVstup WHERE Zdroj LIKE @Zdroj AND SyntetickyaleboFiktivny LIKE @Synteticky " + veda + " AND Podpolozka LIKE @Podpolozka AND Ico LIKE @Ico AND KalendarnyDen LIKE @Datum", Con);
                                    comOpro.Parameters.AddWithValue("@Synteticky", typ);
                                    comOpro.Parameters.AddWithValue("@Zdroj", arrZdroje[i]);
                                    comOpro.Parameters.AddWithValue("@Datum", datum);
                                    comOpro.Parameters.AddWithValue("@Ico", MSVSico);
                                    if (Int32.Parse(rowPodpolozka[1].ToString()) != 4)
                                        comOpro.Parameters.AddWithValue("@Podpolozka", rowPodpolozka[0].ToString().Substring(0, Int32.Parse(rowPodpolozka[1].ToString())) + "%");
                                    else
                                        comOpro.Parameters.AddWithValue("@Podpolozka", rowPodpolozka[0].ToString().Substring(0, 6) + "%");
                                    DataTable dTOpro = new DataTable();
                                    using (SqlDataAdapter da = new SqlDataAdapter(comOpro))
                                    {
                                        da.Fill(dTOpro);
                                        comOpro.ExecuteNonQuery();
                                    }
                                    for (int oprocounter = 0; oprocounter < 3; oprocounter++)
                                        arrayMSVS[oprocounter] = (double)dT.Rows[0][oprocounter];
                                }
                            }*/


                            dT = new DataTable();
                                using (SqlDataAdapter da = new SqlDataAdapter(com))
                                {
                                    da.Fill(dT);
                                    com.ExecuteNonQuery();
                                }


                                if ((double)dT.Rows[0][0] != 0 || (double)dT.Rows[0][1] != 0 || (double)dT.Rows[0][2] != 0)
                                {
                                    //pomocne pole pre zaratanie - MSVS ked je celkovy sucet
                                    /*double[] sucet = new double[3];
                                    sucet[0] = (double)dT.Rows[0][0] - arrayMSVS[0];
                                    sucet[1] = (double)dT.Rows[0][1] - arrayMSVS[1];
                                    sucet[2] = (double)dT.Rows[0][2] - arrayMSVS[2];*/

                                    SqlCommand get_name_podpolozka = new SqlCommand("SELECT textKratky FROM ciselnikPodpolozka WHERE Pol = '" + rowPodpolozka[0] + "'", Con);
                                    DataTable get_name_podpolozka_table = new DataTable();
                                    using (SqlDataAdapter get_name_podpolozka_adapter = new SqlDataAdapter(get_name_podpolozka))
                                    {
                                        get_name_podpolozka_adapter.Fill(get_name_podpolozka_table);
                                        get_name_podpolozka.ExecuteNonQuery();
                                    }

                                    //PRE DANU PDOPOLOZKU VYPIS
                                    if (get_name_podpolozka_table.Rows.Count != 0)
                                    {
                                        if (Int32.Parse(rowPodpolozka[0].ToString().Substring(3)) != 0)
                                        {
                                            worksheet.Cells[start, 2].NumberFormat = "@";
                                            worksheet.Cells[start, 2] = rowPodpolozka[0].ToString().Substring(3, 3);
                                            worksheet.Cells[start, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                        }

                                        worksheet.Cells[start, 1] = rowPodpolozka[0].ToString().Substring(0, 3);
                                        worksheet.Cells[start, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                        worksheet.Cells[start, 3] = (string)get_name_podpolozka_table.Rows[0][0];
                                    }
                                    worksheet.Cells[start, 4] = (double)dT.Rows[0][0] / typVystupov;
                                    worksheet.Cells[start, 5] = (double)dT.Rows[0][1] / typVystupov;
                                    worksheet.Cells[start, 6] = (double)dT.Rows[0][2] / typVystupov;

                                    if ((double)dT.Rows[0][0] != 0)
                                        worksheet.Cells[start, 7] = ((double)dT.Rows[0][2] / (double)dT.Rows[0][0] * 100);
                                    else
                                        worksheet.Cells[start, 7] = 0;

                                    if ((double)dT.Rows[0][1] != 0)
                                        worksheet.Cells[start, 8] = ((double)dT.Rows[0][2] / (double)dT.Rows[0][1] * 100);
                                    else
                                        worksheet.Cells[start, 8] = 0;

                                    increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 4, 7, 20, 20);
                                }
                            }
                        //ORAMOVANIE V EXCELE
                        //JEDNOTLIVE STLPCE
                        for (char c = left_char; c <= (char)((int)right_char); c++)//tenke oramovanie od border po start
                        {
                            borderRange = worksheet.get_Range(c.ToString() + borderIco, c.ToString() + (start - 1));
                            borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                            //ZAOKRUHLOVANIE
                            if (c > left_char + 2)
                            {
                                if (c > left_char + 5)
                                    borderRange.NumberFormat = zaokruhleniePercenta;
                                else
                                    borderRange.NumberFormat = zaokruhlenieMena;
                                borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                            }
                            else
                            {
                                if (c == left_char + 2)
                                    borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                                if (c < left_char + 2)
                                    borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                            }
                        }
                    }
                    //HLAVNE ORAMOVANIE
                    borderRange = worksheet.get_Range(left_char.ToString() + border, right_char.ToString() + (start - 1));//hrube po Celkovo
                    borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                    var culture = new CultureInfo("ru-RU");
                    worksheet.Cells[start, 1] = "Dátum vytvorenia: " + time.ToString(culture);
                    worksheet.Cells[start, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                }
                //ZAVRIE EXCEL
                workbook.Save();
                if (priamaTlac)
                    PrintMyExcelFile(workbook);
                Kontrola.release_excel(range, worksheet, workbook, app);
            }
        }

        public void spravZostavyZaPolozkyVelkeNaSirku(string typ, int prijmyVydavkyVedaOON, Boolean lenOkruhy, string MSVSico, string datum, int typVystupov, string zaokruhlenieMena, string zaokruhleniePercenta, char left_char, int left_num, char right_char, int right_num, int count_for_page, int strana_rel_pos, DateTime time, bool priamaTlac)
        {
            // int prijmyVydavkyVeda:
            //  1 - prijmy
            //  2 - vydavky
            //  3, 4 - veda
            //  5,6 - OON
            string connectionstring = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            string ekonomickaKlasifikacia = zistiEkonomickuKlasifikaciu(prijmyVydavkyVedaOON, 2, connectionstring);
            string[] synteticke = najdiSynteticke(connectionstring);
            string specialnePolozky = " AND (";
            //CI ROBIME POL ALEBO OON
            string atribut = "pol_tab8";
            if (prijmyVydavkyVedaOON == 5 || prijmyVydavkyVedaOON == 6)
                atribut = "pol_oon8";
            //VYNIMKY MRZ
            string vynimky = vynimkyMRZ(connectionstring);
            //VYNIMKY VEDY
            string veda = "";
            if (prijmyVydavkyVedaOON == 3 || prijmyVydavkyVedaOON == 6)
                veda = vynimkyVedy(3, connectionstring);
            SqlCommand com = null, com2 = null;

            DataTable dT2, dT = new DataTable();
            using (var Con = new SqlConnection(connectionstring))
            {
                Con.Open();
                //VYBER PODPOLOZKY PODLA SPECIFICKEHO VYBERU
                SqlCommand comPodpolozka = null;
                DataTable dTPodpolozka = new DataTable();
                /*string podmienka = "AND (Pol LIKE '1%' OR Pol LIKE '2%' OR Pol LIKE '3%' OR Pol LIKE '4%' OR Pol LIKE '5%')";
                if (prijmyVydavkyVedaOON != 1)
                    podmienka = "AND (Pol LIKE '6%' OR Pol LIKE '7%' OR Pol LIKE '8%' OR Pol LIKE '9%')";*/
                comPodpolozka = new SqlCommand("SELECT Pol, znacka FROM ciselnikPodpolozka WHERE " + atribut + " LIKE 'A%' " + ekonomickaKlasifikacia + " ORDER BY Pol", Con);

                using (SqlDataAdapter daPodpolozka = new SqlDataAdapter(comPodpolozka))
                {
                    daPodpolozka.Fill(dTPodpolozka);
                    comPodpolozka.ExecuteNonQuery();
                }
                //VYTVORENIE PODMIENKY DO SELECTU
                for (int i = 0; i < dTPodpolozka.Rows.Count; i++)
                {
                    if (i > 0)
                        specialnePolozky = specialnePolozky + "OR ";
                    string helper = dTPodpolozka.Rows[i][0].ToString().Substring(0, Int32.Parse(dTPodpolozka.Rows[i][1].ToString()));
                    specialnePolozky = specialnePolozky + " Podpolozka LIKE '" + helper + "%' ";
                }
                specialnePolozky = specialnePolozky + ") ";

                //NAZOV TABULKY
                int typSynteticky = Array.IndexOf(synteticke, typ);
                string nameTabulka = "";
                string kvartal = vytvorAdresare(datum);

                if (typ == synteticke[0] || typ == synteticke[1])
                    nameTabulka = nameTabulka + "p";

                if (typ == synteticke[0] || typ == synteticke[4])
                {
                    if (lenOkruhy)
                        nameTabulka = nameTabulka + "8d";
                    else
                        nameTabulka = nameTabulka + "8c";
                }
                else
                {
                    if (lenOkruhy)
                        nameTabulka = nameTabulka + "8b";
                    else
                        nameTabulka = nameTabulka + "8a";

                    if (prijmyVydavkyVedaOON == 3)
                        nameTabulka = nameTabulka + "1";
                    if (prijmyVydavkyVedaOON == 5)
                        nameTabulka = nameTabulka + "11";
                    if (prijmyVydavkyVedaOON == 6)
                        nameTabulka = nameTabulka + "12";
                }

                string name = "RZBT" + nameTabulka + ".xls";
                
                //hl8KMcV3 JE HLAVICKA ZOSTAVY PRE TENTO TYP
                System.IO.File.Copy("..\\..\\Hlav\\RZB\\hl8KMcV3.xls", "Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name, true);

                Excel.Application app = new Excel.Application();
                string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                Excel.Workbook workbook = app.Workbooks.Open(path + "\\" + "bin\\Debug\\Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name);
                Excel._Worksheet worksheet = workbook.Sheets[1];//pre citanie excelu
                Excel.Range range = worksheet.UsedRange;//pre citanie excelu
                Excel.Range borderRange;
                //Excel._Worksheet worksheetFirst = workbook.Sheets[1];//pre citanie excelu

                //VYPISANIE HLAVICKY KTORA SA BUDE KOPIROVAT
                //TEXTY V EXCELI
                if (typ == synteticke[0] || typ == synteticke[1])
                    worksheet.Cells[1, 11] = "Príspevkové organizácie";
                else
                    worksheet.Cells[1, 11] = "Rozpočtové organizácie";

                if (prijmyVydavkyVedaOON == 1 && !lenOkruhy)
                    worksheet.Cells[2, 1] = "Plnenie príjmov za vybrane položky podľa org. v rezorte školstva";
                else if (prijmyVydavkyVedaOON == 1 && lenOkruhy)
                    worksheet.Cells[2, 1] = "Plnenie príjmov za vybrane položky podľa okruhov v rezorte školstva";
                else
                {
                    if (prijmyVydavkyVedaOON == 2)
                    {
                        if (!lenOkruhy)
                            worksheet.Cells[2, 1] = "Rozbor výdavkov za vybrane položky podľa org. v rezorte školstva";
                        else
                            worksheet.Cells[2, 1] = "Rozbor výdavkov za vybrane položky podľa okruhov v rezorte školstva";
                    }
                    else
                    {
                        if (prijmyVydavkyVedaOON == 5 || prijmyVydavkyVedaOON == 6 || prijmyVydavkyVedaOON == 7 || prijmyVydavkyVedaOON == 8)
                        {
                            if (prijmyVydavkyVedaOON == 5)
                                worksheet.Cells[2, 1] = "Rozbor výdavkov za vybrane položky podľa org. v rezorte školstva len za OON";
                            else if (prijmyVydavkyVedaOON == 6)
                                worksheet.Cells[2, 1] = "Rozbor výdavkov za vybrane položky podľa org. v rezorte školstva len za OON za vedu";
                            /*else if (prijmyVydavkyVedaOON == 7)
                                worksheet.Cells[2, 1] = "Rozbor príjmov za vybrané položky podľa okruhov v rezorte školstva";
                            else if (prijmyVydavkyVedaOON == 8)
                                worksheet.Cells[2, 1] = "Rozbor výdavkov za vybrané položky podľa okruhov v rezorte školstva";*/
                        }
                        else if (!lenOkruhy)
                            worksheet.Cells[2, 1] = "Rozbor výdavkov za vybrane položky podľa org. v rezorte školstva za vedu";
                        else
                            worksheet.Cells[2, 1] = "Rozbor výdavkov za vybrane položky podľa okruhov v rezorte školstva za vedu";
                    }
                }
                if (typVystupov == 1)
                    worksheet.Cells[3, 1] = "(údaje sú v €)";
                else
                    worksheet.Cells[3, 1] = "(údaje sú v tisícoch €)";

                worksheet.Cells[5, 1].Style.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                worksheet.Cells[5, 1] = "Spracovateľské obdobie: " + datum;
                worksheet.Cells[5, 12] = "Strana: 1";
                worksheet.Name = "Celkovo";
                worksheet.Cells[5, 11] = nameTabulka;

                int rowCount = range.Rows.Count;//pre citanie excelu
                int start = right_num + 1;
                int border, borderIco = 0;
                int strana = 2;

                //VYBER VSETKYCH ROZNYCH ICO ZA JENDOTLIVY ZDROJ
                dT = new DataTable();
                string[] arrIco = null;
                if (lenOkruhy == false)
                {
                    com = new SqlCommand("SELECT DISTINCT tv.Ico,ci.CelyNazov collate Slovak_CI_AS FROM TableVstup tv JOIN ciselnikIco ci ON tv.Ico=ci.Ico WHERE tv.SyntetickyaleboFiktivny LIKE @Synteticky AND KalendarnyDen LIKE @Datum " + veda + " " + specialnePolozky + "  ORDER BY ci.CelyNazov collate Slovak_CI_AS", Con);
                    com.Parameters.AddWithValue("@Synteticky", typ);
                    com.Parameters.AddWithValue("@Datum", datum);

                    using (SqlDataAdapter da = new SqlDataAdapter(com))
                    {
                        da.Fill(dT);
                        com.ExecuteNonQuery();
                    }

                    arrIco = new string[dT.Rows.Count + 1];
                    int j = 0;

                    //ICA DO ARRAYU
                    foreach (DataRow row in dT.Rows)
                    {
                        arrIco[j] = (string)row[0];
                        j++;
                    }
                    arrIco[j] = "Celkom:";
                }
                else
                {
                    arrIco = new string[2];
                    if (typ == synteticke[0] || typ == synteticke[1] || typ == synteticke[2] || typ == synteticke[3])
                        arrIco[0] = "Príspevkové organizácie";
                    else
                        arrIco[0] = "Ostatné priamo riadené organizácie + MŠVVaŠ SR (Vrátane PJ)";
                    arrIco[1] = "Celkom";
                }
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
                    while (start % count_for_page > count_for_page - 4)
                        increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 4, 7, 9, 11);

                    //NAZOV DO EXCELU S ODSADENIM PLUS KONTROLA CI NEROBI FINALNY SUMAR + NASTAVI HODNOTU ICO DO SELECTU
                    worksheet.get_Range(left_char + (start).ToString(), right_char + (start + 2).ToString()).Merge();
                    worksheet.get_Range(left_char + (start).ToString(), right_char + (start + 2).ToString()).VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    worksheet.get_Range(left_char + (start).ToString(), right_char + (start + 2).ToString()).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    if (get_name_table.Rows.Count != 0)
                        worksheet.Cells[start, 1] = "              Organizácia: " + (string)get_name_table.Rows[0][0];
                    else
                        worksheet.Cells[start, 1] = "              Organizácia: " + helpString;

                    //POSUN O 3 RIADKY KEDZE 3 ZLUCUJEME
                    increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 4, 7, 9, 11);
                    increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 4, 7, 9, 11);
                    increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 4, 7, 9, 11);
                    borderIco = start;

                    //CYKLUS PRE VSETKY MOZNE PODPOLOZKY
                    foreach (DataRow rowPodpolozka in dTPodpolozka.Rows)
                    {
                        //SELECT NA VYBER UDAJOV NA VYPIS
                        /*if (prijmyVydavkyVedaOON != 7 && prijmyVydavkyVedaOON != 8)
                        {*/
                            com = new SqlCommand("SELECT COALESCE(SUM(SchvalenyRozpocet), 0),COALESCE(SUM(RozpocetPoZmenach), 0),COALESCE(SUM(Skutocnost), 0) FROM TableVstup tv WHERE SyntetickyaleboFiktivny LIKE @Synteticky " + veda + " AND Podpolozka LIKE @Podpolozka AND Ico LIKE @Ico AND KalendarnyDen LIKE @Datum", Con);
                            com.Parameters.AddWithValue("@Synteticky", typ);
                            com.Parameters.AddWithValue("@Datum", datum);
                        //}
                        /*else
                        {
                            com = new SqlCommand("SELECT COALESCE(SUM(SchvalenyRozpocet), 0),COALESCE(SUM(RozpocetPoZmenach), 0),COALESCE(SUM(Skutocnost), 0) FROM TableVstup WHERE SyntetickyaleboFiktivny LIKE @Synteticky " + veda + " AND Podpolozka LIKE @Podpolozka AND Ico IN (Select ci.Ico from ciselnikIco ci where ci.Okruh_s LIKE 'PO%') AND KalendarnyDen LIKE @Datum", Con);
                            com.Parameters.AddWithValue("@Synteticky", typ);
                            com.Parameters.AddWithValue("@Datum", datum);
                        }*/

                        if (Int32.Parse(rowPodpolozka[1].ToString()) != 4)
                            com.Parameters.AddWithValue("@Podpolozka", rowPodpolozka[0].ToString().Substring(0, Int32.Parse(rowPodpolozka[1].ToString())) + "%");
                        else
                            com.Parameters.AddWithValue("@Podpolozka", rowPodpolozka[0].ToString().Substring(0, 6) + "%");
                        if (get_name_table.Rows.Count != 0)
                            com.Parameters.AddWithValue("@Ico", helpString);
                        else
                            com.Parameters.AddWithValue("@Ico", "%");


                        /*if (get_name_table.Rows.Count != 0 && prijmyVydavkyVedaOON != 7 && prijmyVydavkyVedaOON != 8)
                            com.Parameters.AddWithValue("@Ico", helpString);
                        else if (prijmyVydavkyVedaOON != 7 && prijmyVydavkyVedaOON != 8)
                            com.Parameters.AddWithValue("@Ico", "%");*/

                        dT = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(com))
                        {
                            da.Fill(dT);
                            com.ExecuteNonQuery();
                        }

                        //SELECT UDAJOV MRZ
                       /* if (prijmyVydavkyVedaOON != 7 && prijmyVydavkyVedaOON != 8)
                        {*/
                            com2 = new SqlCommand("SELECT COALESCE(SUM(Skutocnost), 0) FROM TableVstup tv WHERE SyntetickyaleboFiktivny LIKE @Synteticky " + veda + " " + vynimky + " AND Podpolozka LIKE @Podpolozka AND Ico LIKE @Ico AND KalendarnyDen LIKE @Datum ", Con);
                            com2.Parameters.AddWithValue("@Synteticky", typ);
                            com2.Parameters.AddWithValue("@Datum", datum);
                        /*}
                        else
                        {
                            com2 = new SqlCommand("SELECT COALESCE(SUM(Skutocnost), 0) FROM TableVstup tv JOIN ciselnikIco ci ON ci.Ico=tv.Ico WHERE SyntetickyaleboFiktivny LIKE @Synteticky " + veda + " " + vynimky + " AND Podpolozka LIKE @Podpolozka AND ci.Okruh_s Like 'PO' AND KalendarnyDen LIKE @Datum ", Con);
                            com2.Parameters.AddWithValue("@Synteticky", typ);
                            com2.Parameters.AddWithValue("@Datum", datum);
                        }*/
                        if (Int32.Parse(rowPodpolozka[1].ToString()) != 4)
                            com2.Parameters.AddWithValue("@Podpolozka", rowPodpolozka[0].ToString().Substring(0, Int32.Parse(rowPodpolozka[1].ToString())) + "%");
                        else
                            com2.Parameters.AddWithValue("@Podpolozka", rowPodpolozka[0].ToString().Substring(0, 6) + "%");
                        if (get_name_table.Rows.Count != 0)
                            com2.Parameters.AddWithValue("@Ico", helpString);
                        else
                            com2.Parameters.AddWithValue("@Ico", "%");

                        /*if (get_name_table.Rows.Count != 0 && prijmyVydavkyVedaOON != 7 && prijmyVydavkyVedaOON != 8)
                            com2.Parameters.AddWithValue("@Ico", helpString);
                        else if (prijmyVydavkyVedaOON != 7 && prijmyVydavkyVedaOON != 8)
                            com2.Parameters.AddWithValue("@Ico", "%");*/

                        dT2 = new DataTable();
                        using (SqlDataAdapter da2 = new SqlDataAdapter(com2))
                        {
                            da2.Fill(dT2);
                            com2.ExecuteNonQuery();
                        }

                        if ((double)dT.Rows[0][0] != 0 || (double)dT.Rows[0][1] != 0 || (double)dT.Rows[0][2] != 0)
                        {
                            SqlCommand get_name_podpolozka = new SqlCommand("SELECT textKratky FROM ciselnikPodpolozka WHERE Pol = '" + rowPodpolozka[0] + "'", Con);
                            DataTable get_name_podpolozka_table = new DataTable();
                            using (SqlDataAdapter get_name_podpolozka_adapter = new SqlDataAdapter(get_name_podpolozka))
                            {
                                get_name_podpolozka_adapter.Fill(get_name_podpolozka_table);
                                get_name_podpolozka.ExecuteNonQuery();
                            }

                            //PRE DANU PDOPOLOZKU VYPIS
                            if (get_name_podpolozka_table.Rows.Count != 0)
                            {
                                if (Int32.Parse(rowPodpolozka[0].ToString().Substring(3)) != 0)
                                {
                                    worksheet.Cells[start, 2].NumberFormat = "@";
                                    worksheet.Cells[start, 2] = rowPodpolozka[0].ToString().Substring(3,3);
                                    worksheet.Cells[start, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                }

                                worksheet.Cells[start, 1] = rowPodpolozka[0].ToString().Substring(0, 3);
                                worksheet.Cells[start, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                worksheet.Cells[start, 3] = (string)get_name_podpolozka_table.Rows[0][0];
                            }
                            //ZAPISANIE HODNOT
                            double skutocnostMrz = (double)dT.Rows[0][2] - (double)dT2.Rows[0][0];
                            worksheet.Cells[start, 4] = (double)dT.Rows[0][0] / typVystupov;
                            worksheet.Cells[start, 5] = (double)dT.Rows[0][1] / typVystupov;
                            worksheet.Cells[start, 6] = (double)dT.Rows[0][2] / typVystupov;
                            worksheet.Cells[start, 9] = skutocnostMrz / typVystupov;
                            worksheet.Cells[start, 10] = (double)dT2.Rows[0][0] / typVystupov;

                            //PERCENTA K RS AJ SKUTOCNOST AJ SKUTOCNOST MINUS MRZ
                            if ((double)dT.Rows[0][0] != 0)
                            {
                                worksheet.Cells[start, 7] = (((double)dT.Rows[0][2] / (double)dT.Rows[0][0]) * 100);
                                worksheet.Cells[start, 11] = (((double)dT2.Rows[0][0] / (double)dT.Rows[0][0]) * 100);
                            }
                            else
                            {
                                worksheet.Cells[start, 7] = 0;
                                worksheet.Cells[start, 11] = 0;
                            }
                            //PERCENTA K RU AJ SKUTOCNOST AJ SKUTOCNOST MINUS MRZ
                            if ((double)dT.Rows[0][1] != 0)
                            {
                                worksheet.Cells[start, 8] = (((double)dT.Rows[0][2] / (double)dT.Rows[0][1]) * 100);
                                worksheet.Cells[start, 12] = (((double)dT2.Rows[0][0] / (double)dT.Rows[0][1]) * 100);
                            }
                            else
                            {
                                worksheet.Cells[start, 8] = 0;
                                worksheet.Cells[start, 12] = 0;
                            }
                            increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 4, 7, 9, 11);
                        }
                    }
                    //ORAMOVANIE V EXCELE
                    //JEDNOTLIVE STLPCE
                    for (char c = left_char; c <= (char)((int)right_char); c++)//tenke oramovanie od border po start
                    {
                        borderRange = worksheet.get_Range(c.ToString() + borderIco, c.ToString() + (start - 1));
                        borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                        //ZAOKRUHLOVANIE
                        if (c > left_char + 2)
                        {
                            if ((c > left_char + 5 && c < left_char + 8) || c > left_char + 9)
                                borderRange.NumberFormat = zaokruhleniePercenta;
                            else
                                borderRange.NumberFormat = zaokruhlenieMena;
                            borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                        }
                        else
                        {
                            if (c == left_char + 2)
                                borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                            if (c < left_char + 2)
                                borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        }
                    }
                }
                //HLAVNE ORAMOVANIE
                borderRange = worksheet.get_Range(left_char.ToString() + border, right_char.ToString() + (start - 1));//hrube po Celkovo
                borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                var culture = new CultureInfo("ru-RU");
                worksheet.Cells[start, 1] = "Dátum vytvorenia: " + time.ToString(culture);
                worksheet.Cells[start, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                //ZAVRIE EXCEL
                workbook.Save();
                if (priamaTlac)
                    PrintMyExcelFile(workbook);
                Kontrola.release_excel(range, worksheet, workbook, app);
            }
        }
    }
}
