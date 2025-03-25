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
    class Zostavy_bMs_aMs : Zostavy
    {
        public void spravZostavyBMSz(string typ, int zaProgramy, int zaPodPolozky, string icoMSVVAS, string datum, int typVystupov, string zaokruhlenieMena, string zaokruhleniePercenta, char left_char, int left_num, char right_char, int right_num, int count_for_page, int strana_rel_pos, DateTime time, bool priamaTlac)
        {
            // zaProgramy 
            // 0 - len celkovy prehlad
            // 1 - prehlad aj za programy
            // zaPodPolozky
            // 0 - vypis len za polozky
            // 1 - vypis aj za podpolozky
            string connectionstring = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            string[] synteticke = najdiSynteticke(connectionstring);
            string ekonomickaKlasifikaciaKategoria = zistiEkonomickuKlasifikaciu(2, 1, connectionstring);
            string ekonomickaKlasifikaciaPol = zistiEkonomickuKlasifikaciu(2, 2, connectionstring);
            SqlCommand com = null;
            DataTable dT = new DataTable();
            double[] sum_all;

            using (var Con = new SqlConnection(connectionstring))
            {
                Con.Open();
                string[] arrZdroje, arrProgram;
                int pocetZdrojov = 0;

                if (typ != "MRV")
                {
                    com = new SqlCommand("SELECT DISTINCT Zdroj FROM TableVstup WHERE KalendarnyDen LIKE @Datum AND SyntetickyaleboFiktivny LIKE @Synteticky " + ekonomickaKlasifikaciaKategoria + " AND Podtrieda LIKE '9800' AND ICO LIKE @Ico ORDER BY Zdroj", Con);
                    com.Parameters.AddWithValue("@Synteticky", typ);

                }
                else
                    com = new SqlCommand("SELECT DISTINCT Zdroj FROM TableVstup WHERE KalendarnyDen LIKE @Datum AND SyntetickyaleboFiktivny NOT LIKE @Synteticky1 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky2 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky3 " + ekonomickaKlasifikaciaKategoria + " AND Podtrieda LIKE '9800' AND ICO LIKE @Ico ORDER BY Zdroj", Con);

                com.Parameters.AddWithValue("@Datum", datum);
                com.Parameters.AddWithValue("@Ico", icoMSVVAS);
                com.Parameters.AddWithValue("@Synteticky1", synteticke[1]);
                com.Parameters.AddWithValue("@Synteticky2", synteticke[3]);
                com.Parameters.AddWithValue("@Synteticky3", synteticke[5]);
                using (SqlDataAdapter da = new SqlDataAdapter(com))
                {
                    da.Fill(dT);
                    com.ExecuteNonQuery();
                }
                //ZDROJE DO ARRAYU
                List<string> zdrojeList = new List<string>();
                Boolean control7dmickove = false;
                //AK JE TAM 7DMICKOVY ZDROJ TAK DA DO LISTU AJ 7 AKO SUMAR
                foreach (DataRow row in dT.Rows)
                {
                    if (row[0].ToString()[0] == '7' && control7dmickove == false)
                    {
                        zdrojeList.Add("7%");
                        control7dmickove = true;
                        pocetZdrojov++;
                    }
                    zdrojeList.Add((string)row[0]);
                    pocetZdrojov++;
                }
                arrZdroje = new string[pocetZdrojov];
                arrZdroje = zdrojeList.ToArray();

                //ZISTENIE POLOZIEK ALEBO PODPOLOZIEK PRE KTORE BUDE ROBIT SELECTY
                ArrayList pool = new ArrayList();
                ArrayList stack = new ArrayList();
                SqlCommand startovacie = null;
                if (zaPodPolozky == 1)
                    startovacie = new SqlCommand("SELECT DISTINCT Pol FROM ciselnikPodpolozka WHERE Znacka >= 2 " + ekonomickaKlasifikaciaPol + " ORDER BY Pol", Con);
                else
                    startovacie = new SqlCommand("SELECT DISTINCT SUBSTRING(Pol, 1, 3) as SPol FROM ciselnikPodpolozka WHERE Znacka >= 2 " + ekonomickaKlasifikaciaPol + " ORDER BY SPol", Con);
                DataTable startovacie_table = new DataTable();
                using (SqlDataAdapter startovacie_adapter = new SqlDataAdapter(startovacie))
                {
                    startovacie_adapter.Fill(startovacie_table);
                    startovacie.ExecuteNonQuery();
                }
                foreach (DataRow row in startovacie_table.Rows)
                    stack.Add(row[0].ToString());

                //MENO TABULKY
                string nameTabulka = "bMS";
                if (zaPodPolozky == 1)
                    nameTabulka = "aMS";
                if (zaProgramy == 1)
                    nameTabulka += "p";

                //MENO SUBORA
                string name = "RZBT";
                if (typ == "MRV")
                    name = name + "M";
                name = name + nameTabulka + "z.xls";

                string kvartal = vytvorAdresare(datum);

                //VYTVORENIE EXCELU
                System.IO.File.Copy("..\\..\\Hlav\\RZB\\hl8KMzV3.xls", "Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name, true);
                Excel.Application app = new Excel.Application();
                string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                Excel.Workbook workbook = app.Workbooks.Open(path + "\\" + "bin\\Debug\\Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name);
                Excel._Worksheet worksheet = workbook.Sheets[1];//pre citanie excelu
                Excel.Range range = worksheet.UsedRange;//pre citanie excelu
                Excel.Range borderRange;

                //TEXTY V EXCELY
                if (typVystupov == 1)
                    worksheet.Cells[4, 1] = "(údaje sú v €)";
                else
                    worksheet.Cells[4, 1] = "(údaje sú v tisícoch €)";

                

                worksheet.Cells[2, 1] = "Rozbor výdavkov podľa položiek za f.k. 0980 na MŠVVaŠ SR - úrad";
                worksheet.Cells[6, 1] = "Spracovateľské obdobie: " + datum;

                if (arrZdroje.Length != 0)
                {
                    worksheet.Cells[6, 7] = nameTabulka + arrZdroje[0];
                    worksheet.Name = arrZdroje[0];
                }
                worksheet.Cells[6, 8] = "Strana: 1";


                //CYKLUS PRE KAZDY ZDROJ
                for (int k = 0; k < pocetZdrojov; k++)
                {
                    int rowCount = range.Rows.Count;//pre citanie excelu
                    int start = right_num + 1;
                    int border = start, borderIco = 0;
                    int strana = 2;
                    int pocetProgramov = 0;

                    //VYTVARANIE NOVEHO ZOSITU PLUS KOPIROVANIE HLAVICKY 
                    //VACSIE AKO 0 LEBO PRVY OTVARAM UZ PRED CYKLOM ABY SOM NASTAVIL HALVICKU KTORU POTOM KOPIRUJEM
                    if (k > 0)
                    {
                        workbook.Sheets[1].Copy(Type.Missing, workbook.Sheets[workbook.Sheets.Count]);
                        worksheet = workbook.Sheets[workbook.Sheets.Count];
                        int help = worksheet.Rows.Count;
                        range = worksheet.get_Range("A10", "H" + help);
                        range.Clear();
                        //DOPLNENIE NAZVU TABULKY D EXCELU ZA KONKRETNY ZDROJ
                        if (arrZdroje[k] != "7%")
                        {
                            worksheet.Cells[6, 7] = nameTabulka + "z" + arrZdroje[k];
                            worksheet.Name = arrZdroje[k];
                        }
                        else
                        {
                            worksheet.Cells[6, 7] = nameTabulka + "z7";
                            worksheet.Name = "7";
                        }
                    }

                    //NAZOV ZDROJA A VYPLNENIE KOLONIEK ROVNAKO PRE VSETKY
                    com = new SqlCommand("SELECT Text FROM ciselnikZdroje WHERE Zdroj LIKE @Zdroj", Con);
                    com.Parameters.AddWithValue("@Zdroj", arrZdroje[k] + "%");
                    dT = new DataTable();

                    using (SqlDataAdapter da = new SqlDataAdapter(com))
                    {
                        da.Fill(dT);
                        com.ExecuteNonQuery();
                    }
                    if (arrZdroje[k] != "7%")
                        worksheet.Cells[3, 1] = "za zdroj " + arrZdroje[k] + " - " + (string)dT.Rows[0][0];
                    else
                        worksheet.Cells[3, 1] = "za zdroj 7 - " + (string)dT.Rows[0][0];

                    //ZISTENIE PROGRAMOV KTORE SA TAM NACHADZA
                    if (zaProgramy == 1)
                    {
                        if (typ != "MRV")
                        {
                            com = new SqlCommand("SELECT DISTINCT ProjektPrvok FROM TableVstup WHERE KalendarnyDen LIKE @Datum AND Zdroj LIKE @Zdroj AND SyntetickyaleboFiktivny LIKE @Synteticky " + ekonomickaKlasifikaciaKategoria + " AND Podtrieda LIKE '9800' AND Ico LIKE @Ico ORDER BY ProjektPrvok", Con);
                            com.Parameters.AddWithValue("@Synteticky", typ);
                        }
                        else
                            com = new SqlCommand("SELECT DISTINCT ProjektPrvok FROM TableVstup WHERE KalendarnyDen LIKE @Datum AND Zdroj LIKE @Zdroj AND SyntetickyaleboFiktivny NOT LIKE @Synteticky1 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky2 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky3 " + ekonomickaKlasifikaciaKategoria + " AND Podtrieda LIKE '9800' AND Ico LIKE @Ico ORDER BY ProjektPrvok", Con);

                        com.Parameters.AddWithValue("@Datum", datum);
                        com.Parameters.AddWithValue("@Zdroj", arrZdroje[k] + "%");
                        com.Parameters.AddWithValue("@Ico", icoMSVVAS);
                        com.Parameters.AddWithValue("@Synteticky1", synteticke[1]);
                        com.Parameters.AddWithValue("@Synteticky2", synteticke[3]);
                        com.Parameters.AddWithValue("@Synteticky3", synteticke[5]);
                        dT = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(com))
                        {
                            da.Fill(dT);
                            com.ExecuteNonQuery();
                        }
                        //PROGRAMY DO ARRAYU
                        List<string> icaList = new List<string>();
                        foreach (DataRow row in dT.Rows)
                        {
                            icaList.Add((string)row[0]);
                            pocetProgramov++;
                        }

                        arrProgram = new string[pocetProgramov];
                        arrProgram = icaList.ToArray();
                    }
                    else
                    {
                        pocetProgramov = 1;
                        arrProgram = new string[1];
                        arrProgram[0] = "%";
                    }
                    //PRE KAZDY PROGRAM
                    for (int programI = 0; programI < pocetProgramov; programI++)
                    {
                        if (zaProgramy == 1)
                        {
                            if (programI > 0)
                                increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 11, 20, 20);
                            //HLADA NAZOV PROGRAMU
                            SqlCommand get_name = null;
                            get_name = new SqlCommand("SELECT Skratenytext from ciselnikProjektPrvok where Prog = '" + arrProgram[programI] + "'", Con);
                            DataTable get_name_table = new DataTable();
                            using (SqlDataAdapter get_name_adapter = new SqlDataAdapter(get_name))
                            {
                                get_name_adapter.Fill(get_name_table);
                                get_name.ExecuteNonQuery();
                            }

                            while (start % count_for_page > count_for_page - 4)
                                increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 11, 20, 20);

                            //NAZOV DO EXCELU S ODSADENIM PLUS KONTROLA CI NEROBI FINALNY SUMAR + NASTAVI HODNOTU ICO DO SELECTU
                            worksheet.get_Range(left_char + (start).ToString(), right_char + (start + 2).ToString()).Merge();
                            worksheet.get_Range(left_char + (start).ToString(), right_char + (start + 2).ToString()).VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                            worksheet.get_Range(left_char + (start).ToString(), right_char + (start + 2).ToString()).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                            if (get_name_table.Rows.Count != 0)
                                worksheet.Cells[start, 1] = "              Program: " + arrProgram[programI] + " - " + (string)get_name_table.Rows[0][0];

                            //POSUN O 3 RIADKY KEDZE 3 ZLUCUJEME
                            increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 4, 7, 20, 20);
                            increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 4, 7, 20, 20);
                            increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 4, 7, 20, 20);
                            borderIco = start;
                        }
                        sum_all = new double[3];
                        //BEHANIE CEZ VSETKY POLOZKY
                        foreach (string program in stack)
                        {
                            int valid_len = pocet_znakov_bez_nul(program.ToString());

                            SqlCommand vydavb = null;
                            if (typ != "MRV")
                            {
                                vydavb = new SqlCommand("SELECT SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup WHERE Zdroj LIKE @Zdroj AND ProjektPrvok LIKE @ProjektPrvok AND Ico LIKE @Ico AND Substring(Podpolozka, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND KalendarnyDen LIKE @Datum " + ekonomickaKlasifikaciaKategoria + " AND Podtrieda LIKE '9800' AND SyntetickyaleboFiktivny LIKE @Synteticky", Con);
                                vydavb.Parameters.AddWithValue("@Synteticky", typ);
                            }
                            else
                                vydavb = new SqlCommand("SELECT SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup WHERE Zdroj LIKE @Zdroj AND ProjektPrvok LIKE @ProjektPrvok AND Ico LIKE @Ico AND Substring(Podpolozka, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND KalendarnyDen LIKE @Datum " + ekonomickaKlasifikaciaKategoria + " AND Podtrieda LIKE '9800' AND SyntetickyaleboFiktivny NOT LIKE @Synteticky1 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky2 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky3", Con);

                            vydavb.Parameters.AddWithValue("@Datum", datum);
                            vydavb.Parameters.AddWithValue("@ProjektPrvok", arrProgram[programI] + "%");
                            vydavb.Parameters.AddWithValue("@Ico", icoMSVVAS);
                            vydavb.Parameters.AddWithValue("@Zdroj", arrZdroje[k] + "%");
                            vydavb.Parameters.AddWithValue("@Synteticky1", synteticke[1]);
                            vydavb.Parameters.AddWithValue("@Synteticky2", synteticke[3]);
                            vydavb.Parameters.AddWithValue("@Synteticky3", synteticke[5]);

                            DataTable vydavb_table = new DataTable();
                            using (SqlDataAdapter vydavb_adapter = new SqlDataAdapter(vydavb))
                            {
                                vydavb_adapter.Fill(vydavb_table);
                                vydavb.ExecuteNonQuery();
                            }
                            //AK COUNT JE VACSI AKO JEDNA TAK SA UDAJE NACHADZALIA TYM PADOM VYPISUJEME HODNOTY
                            if ((int)vydavb_table.Rows[0][3] > 0)
                            {
                                SqlCommand get_name = null;
                                get_name = new SqlCommand("SELECT textKratky FROM ciselnikPodpolozka WHERE Pol LIKE '" + program + "%' ORDER BY Pol ", Con);
                                DataTable get_name_table = new DataTable();
                                using (SqlDataAdapter get_name_adapter = new SqlDataAdapter(get_name))
                                {
                                    get_name_adapter.Fill(get_name_table);
                                    get_name.ExecuteNonQuery();
                                }

                                if (get_name_table.Rows.Count != 0)
                                    worksheet.Cells[start, 3] = (string)get_name_table.Rows[0][0];

                                worksheet.Cells[start, 1].NumberFormat = "@";
                                worksheet.Cells[start, 1] = program.ToString().Substring(0, 3);
                                if (zaPodPolozky == 1 && program.ToString().Substring(3, 3) != "000")
                                {
                                    worksheet.Cells[start, 2].NumberFormat = "@";
                                    worksheet.Cells[start, 2] = program.ToString().Substring(3, 3);
                                }
                                //VYPISUJE CISLA
                                foreach (DataRow row in vydavb_table.Rows)
                                {
                                    for (int rowCounter = 0; rowCounter <= 2; rowCounter++)
                                    {
                                        if (row[rowCounter].ToString() != "")
                                        {
                                            worksheet.Cells[start, rowCounter + 4] = (double)row[rowCounter] / typVystupov;
                                            if (valid_len == 2)
                                                sum_all[rowCounter] = sum_all[rowCounter] + (double)row[rowCounter];
                                        }
                                        else
                                            worksheet.Cells[start, rowCounter + 4] = 0;
                                    }
                                    //VYPISUJE PERCENTA
                                    if ((double)row[0] != 0)
                                        worksheet.Cells[start, 7] = ((double)row[2] / (double)row[0]) * 100;
                                    else
                                        worksheet.Cells[start, 7] = 0;
                                    if ((double)row[1] != 0)
                                        worksheet.Cells[start, 8] = ((double)row[2] / (double)row[1]) * 100;
                                    else
                                        worksheet.Cells[start, 8] = 0;
                                }

                                if (zaProgramy == 0)
                                    increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 4, 7, 20, 20);
                                else
                                    increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 4, 7, 20, 20);
                            }
                        }
                        //SPOLU SUMAR NA KONCI
                        worksheet.Cells[start, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        worksheet.Cells[start, 1].NumberFormat = "@";
                        worksheet.Cells[start, 1] = "Spolu";
                        worksheet.Cells[start, 3] = " výdavky";

                        for (int i = 4; i <= 6; i++)
                            worksheet.Cells[start, i] = sum_all[i - 4] / typVystupov;
                        if (sum_all[0] != 0)
                            worksheet.Cells[start, 7] = (sum_all[2] / sum_all[0]) * 100;
                        else
                            worksheet.Cells[start, 7] = 0;
                        if (sum_all[1] != 0)
                            worksheet.Cells[start, 8] = (sum_all[2] / sum_all[1]) * 100;
                        else
                            worksheet.Cells[start, 8] = 0;

                        if (zaProgramy == 0)
                            borderIco = border;

                        //ORAMOVANIE V EXCELE
                        //JEDNOTLIVE STLPCE
                        for (char c = left_char; c <= (char)((int)right_char); c++)//tenke oramovanie od border po start
                        {
                            borderRange = worksheet.get_Range(c.ToString() + borderIco, c.ToString() + start);
                            borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                            //ZAOKRUHLOVANIE
                            //CISLA
                            if ((c > left_char + 2 && c < left_char + 6))
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
                                //ZACIATOK VSETKO DOLAVA
                                else
                                {
                                    borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                                }
                            }
                        }
                    }
                    borderRange = worksheet.get_Range(left_char.ToString() + border, right_char.ToString() + start);//hrube po Celkovo
                    borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                    var culture = new CultureInfo("ru-RU");
                    worksheet.Cells[++start, 1] = "Dátum vytvorenia: " + time.ToString(culture);
                }
                workbook.Save();
                if (priamaTlac)
                    PrintMyExcelFile(workbook);
                Kontrola.release_excel(range, worksheet, workbook, app);
            }
        }

        public void spravZostavyBMS(string typ, int zaProgramy, int zaPodPolozky, string icoMSVVAS, string datum, int typVystupov, string zaokruhlenieMena, string zaokruhleniePercenta, char left_char, int left_num, char right_char, int right_num, int count_for_page, int strana_rel_pos, DateTime time, bool priamaTlac)
        {
            // zaProgramy 
            // 0 - len celkovy prehlad
            // 1 - prehlad aj za programy
            // zaPodPolozky
            // 0 - vypis len za polozky
            // 1 - vypis aj za podpolozky
            string connectionstring = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            string[] synteticke = najdiSynteticke(connectionstring);
            string ekonomickaKlasifikaciaKategoria = zistiEkonomickuKlasifikaciu(2, 1, connectionstring);
            string ekonomickaKlasifikaciaPol = zistiEkonomickuKlasifikaciu(2, 2, connectionstring);
            //VYNIMKY MRZ
            string vynimky = vynimkyMRZ(connectionstring);
            SqlCommand com = null;
            DataTable dT = new DataTable();
            double[] sum_all;

            using (var Con = new SqlConnection(connectionstring))
            {
                Con.Open();
                string[] arrProgram;
                //ZISTI VSETKY POLOZKY ALEBO PODPOLOZKY PRE KTORE SA BUDU ROBIT SELECTY
                ArrayList pool = new ArrayList();
                ArrayList stack = new ArrayList();
                SqlCommand startovacie = null;
                if (zaPodPolozky == 1)
                    startovacie = new SqlCommand("SELECT DISTINCT Pol FROM ciselnikPodpolozka WHERE Znacka >= 2 " + ekonomickaKlasifikaciaPol + " ORDER BY Pol", Con);
                else
                    startovacie = new SqlCommand("SELECT DISTINCT SUBSTRING(Pol, 1, 3) as SPol FROM ciselnikPodpolozka WHERE Znacka >= 2 " + ekonomickaKlasifikaciaPol + " ORDER BY SPol", Con);
                DataTable startovacie_table = new DataTable();
                using (SqlDataAdapter startovacie_adapter = new SqlDataAdapter(startovacie))
                {
                    startovacie_adapter.Fill(startovacie_table);
                    startovacie.ExecuteNonQuery();
                }
                foreach (DataRow row in startovacie_table.Rows)
                    stack.Add(row[0].ToString());

                //MENO TABULKY
                string nameTabulka = "bMS";
                if (zaPodPolozky == 1)
                    nameTabulka = "aMS";
                if (zaProgramy == 1)
                    nameTabulka += "p";

                //MENO SUBORA
                string name = "RZBT";
                if (typ == "MRV")
                    name = name + "M";
                name = name + nameTabulka + ".xls";

                string kvartal = vytvorAdresare(datum);

                //VYTVORENIE EXCELU
                System.IO.File.Copy("..\\..\\Hlav\\RZB\\hl8KMcV3.xls", "Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name, true);
                Excel.Application app = new Excel.Application();
                string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                Excel.Workbook workbook = app.Workbooks.Open(path + "\\" + "bin\\Debug\\Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name);
                Excel._Worksheet worksheet = workbook.Sheets[1];//pre citanie excelu
                Excel.Range range = worksheet.UsedRange;//pre citanie excelu
                Excel.Range borderRange;

                //TEXTY V EXCELY
                if (typVystupov == 1)
                    worksheet.Cells[3, 1] = "(údaje sú v €)";
                else
                    worksheet.Cells[3, 1] = "(údaje sú v tisícoch €)";

                worksheet.Cells[2, 1] = "Rozbor výdavkov podľa položiek za f.k. 0980 na MŠVVaŠ SR - úrad";
                worksheet.Cells[5, 1] = "Spracovateľské obdobie: " + datum;
                worksheet.Cells[5, 11] = nameTabulka;
                worksheet.Cells[5, 12] = "Strana: 1";
                worksheet.Name = "Celkovo";


                int rowCount = range.Rows.Count;//pre citanie excelu
                int start = right_num + 1;
                int border = start, borderIco = 0;
                int strana = 2;
                int pocetProgramov = 0;

                //ZISTENIE PROGRAMOV KTORE SA TAM NACHADZA
                if (zaProgramy == 1)
                {
                    if (typ != "MRV")
                    {
                        com = new SqlCommand("SELECT DISTINCT ProjektPrvok FROM TableVstup tv WHERE KalendarnyDen LIKE @Datum AND SyntetickyaleboFiktivny LIKE @Synteticky " + ekonomickaKlasifikaciaKategoria + " AND Podtrieda LIKE '9800' AND Ico LIKE @Ico ORDER BY ProjektPrvok", Con);
                        com.Parameters.AddWithValue("@Synteticky", typ);
                    }
                    else
                        com = new SqlCommand("SELECT DISTINCT ProjektPrvok FROM TableVstup tv WHERE KalendarnyDen LIKE @Datum AND SyntetickyaleboFiktivny NOT LIKE @Synteticky1 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky2 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky3 " + ekonomickaKlasifikaciaKategoria + " AND Podtrieda LIKE '9800' AND Ico LIKE @Ico ORDER BY ProjektPrvok", Con);

                    com.Parameters.AddWithValue("@Datum", datum);
                    com.Parameters.AddWithValue("@Ico", icoMSVVAS);
                    com.Parameters.AddWithValue("@Synteticky1", synteticke[1]);
                    com.Parameters.AddWithValue("@Synteticky2", synteticke[3]);
                    com.Parameters.AddWithValue("@Synteticky3", synteticke[5]);
                    dT = new DataTable();
                    using (SqlDataAdapter da = new SqlDataAdapter(com))
                    {
                        da.Fill(dT);
                        com.ExecuteNonQuery();
                    }
                    //PROGRAMY DO ARRAYU
                    List<string> icaList = new List<string>();
                    foreach (DataRow row in dT.Rows)
                    {
                        icaList.Add((string)row[0]);
                        pocetProgramov++;
                    }

                    arrProgram = new string[pocetProgramov];
                    arrProgram = icaList.ToArray();
                }
                else
                {
                    pocetProgramov = 1;
                    arrProgram = new string[1];
                    arrProgram[0] = "%";
                }
                //PRE KAZDY PROGRAM
                for (int programI = 0; programI < pocetProgramov; programI++)
                {
                    if (zaProgramy == 1)
                    {
                        if (programI > 0)
                            increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 11, 9, 11);
                        //HLADA ICO NAZOV
                        SqlCommand get_name = null;
                        get_name = new SqlCommand("SELECT Skratenytext from ciselnikProjektPrvok where Prog = '" + arrProgram[programI] + "'", Con);
                        DataTable get_name_table = new DataTable();
                        using (SqlDataAdapter get_name_adapter = new SqlDataAdapter(get_name))
                        {
                            get_name_adapter.Fill(get_name_table);
                            get_name.ExecuteNonQuery();
                        }

                        while (start % count_for_page > count_for_page - 4)
                            increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 11, 9, 11);

                        //NAZOV DO EXCELU S ODSADENIM PLUS KONTROLA CI NEROBI FINALNY SUMAR + NASTAVI HODNOTU ICO DO SELECTU
                        worksheet.get_Range(left_char + (start).ToString(), right_char + (start + 2).ToString()).Merge();
                        worksheet.get_Range(left_char + (start).ToString(), right_char + (start + 2).ToString()).VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        worksheet.get_Range(left_char + (start).ToString(), right_char + (start + 2).ToString()).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        if (get_name_table.Rows.Count != 0)
                            worksheet.Cells[start, 1] = "              Program: " + arrProgram[programI] + " - " + (string)get_name_table.Rows[0][0];

                        //POSUN O 3 RIADKY KEDZE 3 ZLUCUJEME
                        increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 4, 7, 9, 11);
                        increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 4, 7, 9, 11);
                        increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 4, 7, 9, 11);
                        borderIco = start;
                    }
                    sum_all = new double[4];
                    //BEHANIE CEZ VSETKZ POLOZKY
                    foreach (string program in stack)
                    {
                        int valid_len = pocet_znakov_bez_nul(program.ToString());

                        SqlCommand vydavb = null, vydavMRZ = null;
                        if (typ != "MRV")
                        {
                            vydavb = new SqlCommand("SELECT SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE ProjektPrvok LIKE @ProjektPrvok AND Ico LIKE @Ico AND Substring(Podpolozka, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND KalendarnyDen LIKE @Datum " + ekonomickaKlasifikaciaKategoria + " AND Podtrieda LIKE '9800' AND SyntetickyaleboFiktivny LIKE @Synteticky", Con);
                            vydavb.Parameters.AddWithValue("@Synteticky", typ);
                        }
                        else
                            vydavb = new SqlCommand("SELECT SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE ProjektPrvok LIKE @ProjektPrvok AND Ico LIKE @Ico AND Substring(Podpolozka, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND KalendarnyDen LIKE @Datum " + ekonomickaKlasifikaciaKategoria + " AND Podtrieda LIKE '9800' AND SyntetickyaleboFiktivny NOT LIKE @Synteticky1 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky2 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky3", Con);

                        vydavb.Parameters.AddWithValue("@Datum", datum);
                        vydavb.Parameters.AddWithValue("@ProjektPrvok", arrProgram[programI] + "%");
                        vydavb.Parameters.AddWithValue("@Ico", icoMSVVAS);
                        vydavb.Parameters.AddWithValue("@Synteticky1", synteticke[1]);
                        vydavb.Parameters.AddWithValue("@Synteticky2", synteticke[3]);
                        vydavb.Parameters.AddWithValue("@Synteticky3", synteticke[5]);

                        if (typ != "MRV")
                        {
                            vydavMRZ = new SqlCommand("SELECT COALESCE(SUM(Skutocnost),0) FROM TableVstup tv WHERE ProjektPrvok LIKE @ProjektPrvok AND Ico LIKE @Ico AND Substring(Podpolozka, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND KalendarnyDen LIKE @Datum " + ekonomickaKlasifikaciaKategoria + " AND Podtrieda LIKE '9800' AND SyntetickyaleboFiktivny LIKE @Synteticky " + vynimky, Con);
                            vydavMRZ.Parameters.AddWithValue("@Synteticky", typ);
                        }
                        else
                            vydavMRZ = new SqlCommand("SELECT COALESCE(SUM(Skutocnost),0) FROM TableVstup tv WHERE ProjektPrvok LIKE @ProjektPrvok AND Ico LIKE @Ico AND Substring(Podpolozka, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND KalendarnyDen LIKE @Datum " + ekonomickaKlasifikaciaKategoria + " AND Podtrieda LIKE '9800' AND SyntetickyaleboFiktivny NOT LIKE @Synteticky1 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky2 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky3 " + vynimky, Con);

                        vydavMRZ.Parameters.AddWithValue("@Datum", datum);
                        vydavMRZ.Parameters.AddWithValue("@ProjektPrvok", arrProgram[programI] + "%");
                        vydavMRZ.Parameters.AddWithValue("@Ico", icoMSVVAS);
                        vydavMRZ.Parameters.AddWithValue("@Synteticky1", synteticke[1]);
                        vydavMRZ.Parameters.AddWithValue("@Synteticky2", synteticke[3]);
                        vydavMRZ.Parameters.AddWithValue("@Synteticky3", synteticke[5]);

                        DataTable vydavb_table = new DataTable();
                        DataTable vydavMRZ_table = new DataTable();
                        using (SqlDataAdapter vydavb_adapter = new SqlDataAdapter(vydavb))
                        using (SqlDataAdapter vydavMRZ_adapter = new SqlDataAdapter(vydavMRZ))
                        {
                            vydavMRZ_adapter.Fill(vydavMRZ_table);
                            vydavMRZ.ExecuteNonQuery();
                            vydavb_adapter.Fill(vydavb_table);
                            vydavb.ExecuteNonQuery();
                        }
                        //AK COUNT JE VACSI AKO JEDNA TAK SA UDAJE NACHADZALIA  TYM PADOM VYPISUJEME HODNOTY
                        if ((int)vydavb_table.Rows[0][3] > 0)
                        {
                            SqlCommand get_name = null;
                            get_name = new SqlCommand("SELECT textKratky FROM ciselnikPodpolozka WHERE Pol LIKE '" + program + "%' ORDER BY Pol ", Con);
                            DataTable get_name_table = new DataTable();
                            using (SqlDataAdapter get_name_adapter = new SqlDataAdapter(get_name))
                            {
                                get_name_adapter.Fill(get_name_table);
                                get_name.ExecuteNonQuery();
                            }

                            if (get_name_table.Rows.Count != 0)
                                worksheet.Cells[start, 3] = (string)get_name_table.Rows[0][0];

                            worksheet.Cells[start, 1].NumberFormat = "@";
                            worksheet.Cells[start, 1] = program.ToString().Substring(0, 3);
                            if (zaPodPolozky == 1 && program.ToString().Substring(3, 3) != "000")
                            {
                                worksheet.Cells[start, 2].NumberFormat = "@";
                                worksheet.Cells[start, 2] = program.ToString().Substring(3, 3);
                            }

                            foreach (DataRow row in vydavb_table.Rows)
                            {
                                for (int rowCounter = 0; rowCounter <= 2; rowCounter++)
                                {
                                    if (row[rowCounter].ToString() != "")
                                    {
                                        worksheet.Cells[start, rowCounter + 4] = (double)row[rowCounter] / typVystupov;
                                        if (valid_len == 2)
                                            sum_all[rowCounter] = sum_all[rowCounter] + (double)row[rowCounter];
                                    }
                                    else
                                        worksheet.Cells[start, rowCounter + 4] = 0;
                                }
                                if ((double)row[0] != 0)
                                    worksheet.Cells[start, 7] = ((double)row[2] / (double)row[0]) * 100;
                                else
                                    worksheet.Cells[start, 7] = 0;
                                if ((double)row[1] != 0)
                                    worksheet.Cells[start, 8] = ((double)row[2] / (double)row[1]) * 100;
                                else
                                    worksheet.Cells[start, 8] = 0;
                            }
                            //MRZ
                            if (valid_len == 2)
                                sum_all[3] = sum_all[3] + (double)vydavb_table.Rows[0][2] - (double)vydavMRZ_table.Rows[0][0];
                            worksheet.Cells[start, 9] = (double)vydavb_table.Rows[0][2] - (double)vydavMRZ_table.Rows[0][0];
                            worksheet.Cells[start, 10] = (double)vydavMRZ_table.Rows[0][0];
                            if ((double)vydavb_table.Rows[0][0] != 0)
                                worksheet.Cells[start, 11] = ((double)vydavMRZ_table.Rows[0][0] / (double)vydavb_table.Rows[0][0]) * 100;
                            else
                                worksheet.Cells[start, 11] = 0;
                            if ((double)vydavb_table.Rows[0][1] != 0)
                                worksheet.Cells[start, 12] = ((double)vydavMRZ_table.Rows[0][0] / (double)vydavb_table.Rows[0][1]) * 100;
                            else
                                worksheet.Cells[start, 12] = 0;


                            if (zaProgramy == 0)
                                increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 4, 7, 9, 11);
                            else
                                increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 4, 7, 9, 11);
                        }
                    }

                    worksheet.Cells[start, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    worksheet.Cells[start, 1].NumberFormat = "@";
                    worksheet.Cells[start, 1] = "Spolu";
                    worksheet.Cells[start, 3] = " výdavky";

                    for (int i = 4; i <= 6; i++)
                        worksheet.Cells[start, i] = sum_all[i - 4] / typVystupov;
                    if (sum_all[0] != 0)
                        worksheet.Cells[start, 7] = (sum_all[2] / sum_all[0]) * 100;
                    else
                        worksheet.Cells[start, 7] = 0;
                    if (sum_all[1] != 0)
                        worksheet.Cells[start, 8] = (sum_all[2] / sum_all[1]) * 100;
                    else
                        worksheet.Cells[start, 8] = 0;

                    //MRZ SPOLU
                    worksheet.Cells[start, 9] = sum_all[3];
                    worksheet.Cells[start, 10] = sum_all[2] - sum_all[3];
                    if (sum_all[0] != 0 && (sum_all[2] - sum_all[3] !=0))
                        worksheet.Cells[start, 11] = ((sum_all[2] - sum_all[3]) / sum_all[0]) * 100;
                    else
                        worksheet.Cells[start, 11] = 0;
                    if (sum_all[1] != 0 && (sum_all[2] - sum_all[3] != 0))
                        worksheet.Cells[start, 12] = ((sum_all[2] - sum_all[3]) / sum_all[1]) * 100;
                    else
                        worksheet.Cells[start, 12] = 0;

                    if (zaProgramy == 0)
                        borderIco = border;

                    //ORAMOVANIE V EXCELE
                    //JEDNOTLIVE STLPCE
                    for (char c = left_char; c <= (char)((int)right_char); c++)//tenke oramovanie od border po start
                    {
                        borderRange = worksheet.get_Range(c.ToString() + borderIco, c.ToString() + start);
                        borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                        //ZAOKRUHLOVANIE
                        //CISLA
                        if ((c > left_char + 2 && c < left_char + 6) || (c > left_char + 7 && c < left_char + 10))
                        {
                            borderRange.NumberFormat = zaokruhlenieMena;
                            borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                        }
                        //PERCENTA
                        else
                        {
                            if (c > left_char + 5 && c < left_char + 8 || c > left_char + 9)
                            {
                                borderRange.NumberFormat = zaokruhleniePercenta;
                                borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                            }
                            //ZACIATOK VSETKO DOLAVA
                            else
                            {
                                borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                            }
                        }
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
            }
        }
    }
}
