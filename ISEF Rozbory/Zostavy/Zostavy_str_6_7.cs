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
    class Zostavy_str_6_7 : Zostavy
    {
        public void sprav_str_6_7(string typP, string typV, string datum, int typVystupov, string zaokruhlenieMena, string zaokruhleniePercenta, char left_char, int left_num, char right_char, int right_num, int count_for_page, int strana_rel_pos, DateTime time, bool priamaTlac)//1 prijmy 2 vydavky
        {
            string connectionstring = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            SqlCommand com = null;
            SqlCommand com2 = null;
            bool opro = false;

            DataTable dT = new DataTable();
            DataTable dT2 = new DataTable();
            DataTable dtOPRO = new DataTable();
            DataTable dtOPRO2 = new DataTable();
            using (var Con = new SqlConnection(connectionstring))
            {
                string nameTabulka = "str.6-7";

                if (typVystupov == 1000)
                    nameTabulka += "_tis._eur";

                string name = "UCET_";
                name = name + nameTabulka + "_stare.xls";

                string kvartal = vytvorAdresare(datum);
                string vynimky = vynimkyMRZ(connectionstring);

                System.IO.File.Copy("..\\..\\Hlav\\ZU\\hlpu15.xls", "Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name, true);
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
                int indexCelkovychVydavkov = 0;

                //DATUM
                //worksheet.Cells[1, 7] = nameTabulka;
                worksheet.Name = nameTabulka;
                worksheet.Cells[2, 1] = "Záväzné ukazovatele kapitoly MŠVVaŠ SR k " + datum + " (€) spolu s MRZ  a bez MRZ";
                worksheet.Cells[2, 7] = "Strana: 1";
                worksheet.Cells[4, 2] = worksheet.Cells[4, 2].Value2.ToString() + " " + datum.Substring(6);
                worksheet.Cells[4, 3] = worksheet.Cells[4, 3].Value2.ToString() + " " + datum;
                worksheet.Cells[4, 4] = worksheet.Cells[4, 4].Value2.ToString() + " " + datum;
                worksheet.Cells[4, 5] = worksheet.Cells[4, 5].Value2.ToString() + " " + datum;
                Con.Open();

                for (int kapitola = 0; kapitola < 2; kapitola++)
                {
                    com = new SqlCommand("SELECT DISTINCT tv.Zdroj FROM TableVstup tv JOIN ciselnikZdroje cz ON tv.Zdroj=cz.Zdroj WHERE tv.SyntetickyaleboFiktivny LIKE @Typ AND KalendarnyDen LIKE @Datum AND cz.esfasr LIKE @TypEsfasr ORDER BY tv.Zdroj", Con);
                    com.Parameters.AddWithValue("@Datum", datum);

                    if (kapitola == 0)
                    {
                        com.Parameters.AddWithValue("@TypEsfasr", "E%");
                        com.Parameters.AddWithValue("@Typ", typP);
                    }
                    else
                    {
                        com.Parameters.AddWithValue("@TypEsfasr", "S%");
                        com.Parameters.AddWithValue("@Typ", typV);
                    }
                    dT = new DataTable();
                    using (SqlDataAdapter da = new SqlDataAdapter(com))
                    {
                        da.Fill(dT);
                        com.ExecuteNonQuery();
                    }

                    string[] arrZdroje;
                    //arrZdroje = new string[dT.Rows.Count + 3];

                    int i = 1;
                    if (kapitola == 0)
                    {
                        arrZdroje = new string[dT.Rows.Count + 3];
                        arrZdroje[0] = "I. Prijmy kapitoly (ŠR a EÚ)";
                    }
                    else
                    {
                        arrZdroje = new string[dT.Rows.Count + 4];
                        arrZdroje[0] = "II. Výdavky kapitoly spolu (A + B + C)";
                        arrZdroje[1] = "A. Výdavky spolu bez prostriedkov z rozpočtu EÚ:";
                        i++;
                    }
                    arrZdroje[i] = "111";
                    i++;
                    if (kapitola == 0)
                    {
                        arrZdroje[i] = "B. Prostriedky z rozpočtu EÚ spolu, v tom:";
                        i++;
                    }
                    else
                    {
                        arrZdroje[i++] = "A.2.prostriedky na spolufinancovanie spolu, z toho:";
                    }

                    foreach (DataRow row in dT.Rows) // Loop over the rows.
                    {
                        arrZdroje[i] = (string)row[0];
                        i++;
                    }

                    if (kapitola > 0)
                    {
                        com.Parameters["@TypEsfasr"].Value = "E%";
                        dT = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(com))
                        {
                            da.Fill(dT);
                            com.ExecuteNonQuery();
                        }

                        if (dT.Rows.Count > 0)
                        {
                            Array.Resize<string>(ref arrZdroje, arrZdroje.Length + dT.Rows.Count + 2);
                            arrZdroje[i++] = "A.4. kapitálové výdavky (700) - bez prostriedkov na  spolufinancovanie";
                            arrZdroje[i] = "B. Prostriedky z rozpočtu EÚ spolu, v tom";
                            i++;
                            foreach (DataRow row in dT.Rows) // Loop over the rows.
                            {
                                arrZdroje[i] = (string)row[0];
                                i++;
                            }
                        }
                    }
                    //ORAMOVANIE KAPITOLY
                    borderRange = worksheet.get_Range(left_char.ToString() + start, right_char.ToString() + start);
                    borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                    borderRange.Font.Bold = true;

                    foreach (string helpString in arrZdroje) // Loop over the rows.
                    {
                        if (helpString == "E")
                        {
                            worksheet.Cells[start, 1] = helpString;
                            start++;
                            continue;
                        }
                        String nazov_org = "";
                        String skratka_org = "";
                        //MEDZI SUCET NAPRIKLAD ZA EURO VSETKY
                        String doplnenie = "";
                        if (helpString.Substring(0, 3) == "B. ")
                            doplnenie = "AND cz.esfasr LIKE 'E%'";
                        else
                        {
                            if (helpString.Substring(0, 3) == "A. ")
                                doplnenie = "AND cz.esfasr NOT LIKE 'E%'";
                            else
                            {
                                if (helpString.Substring(0, 3) == "A.2")
                                    doplnenie = "AND cz.esfasr LIKE 'S%'";
                                else
                                {
                                    if (helpString.Substring(0, 3) == "A.4")
                                    {
                                        //
                                        //DOPLNIT VYNIMKU PRE KAPITALOVE A4
                                        //
                                        doplnenie = "AND tv.polozka LIKE '7%' AND tv.Zdroj LIKE '111'";
                                        //TEXTY KTORE SU V ZOSTAVACH NO NEVYPLNAME ICH MY
                                        //VYPISEME SEM
                                        worksheet.Cells[start, 1] = "A.3. mzdy, platy, služobné príjmy a ostatné osobné vyrovnania (610), zdroj 111, z toho:";
                                        worksheet.Cells[start, 1].Font.Bold = true;
                                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 6, 8, 11);
                                        worksheet.Cells[start, 1] = " - mzdy, platy, služobné príjmy a ostatné osobné vyrovnania ústredného orgánu, okrem štátnych zamestnancov";
                                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 6, 8, 11);
                                        worksheet.Cells[start, 1] = " - počet zamestnancov rozpočtových organizácií, okrem štátnych zamestnancov v zmysle prílohy č. 1 k UV SR č. 461/2016, z toho:";
                                        worksheet.Cells[start, 1].Characters[99, 19].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 6, 8, 11);
                                        worksheet.Cells[start, 1] = " - aparát ústredného orgánu";
                                        worksheet.Cells[start, 1].Font.Bold = true;
                                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 6, 8, 11);
                                        worksheet.Cells[start, 1] = " - administratívne kapacity rozpočtových organizácií osobitne sledované, podľa prílohy č. 1 k UV SR č. 461/2016";
                                        worksheet.Cells[start, 1].Characters[93, 19].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 6, 8, 11);
                                        worksheet.Cells[start, 1] = " - aparát ústredného orgánu";
                                        worksheet.Cells[start, 1].Font.Bold = true;
                                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 6, 8, 11);
                                    }
                                }
                            }
                        }

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
                            if (helpString == "111")
                            {
                                if (kapitola == 0)
                                    nazov_org = "A) Záväzný ukazovateľ (ŠR) - zdroj 111";
                                else
                                    nazov_org = "A.1. prostriedky štátneho rozpočtu (kód zdroja 111)";
                                worksheet.Cells[start, 1].Font.Bold = true;
                            }
                            else
                                nazov_org = helpString + " - " + (string)get_name_table.Rows[0][0];
                        }
                        else
                        {
                            worksheet.Cells[start, 1].Font.Bold = true;
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

                        i = 0;

                        for (int j = 0; j < dT.Rows.Count; j++)// Loop over the rows.
                        {
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
                        }

                        //ULOZENIE INDEXU KDE SA NACHADZAJU UZ VYSELECTOVANE CELKOVE VYDAVKY
                        if (helpString == arrZdroje[0] && kapitola == 1)
                            indexCelkovychVydavkov = start;

                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 6, 8, 11);
                    }
                }

                worksheet.Cells[start, 1] = "CS. Rozpočet kapitoly podľa prog.(príloha č.2 listu MF/021528/2016-441), z toho:";
                worksheet.Cells[start, 1].Style.WrapText = true;
                borderRange = worksheet.get_Range(left_char.ToString() + start, right_char.ToString() + start);
                borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                borderRange.Font.Bold = true;

                //PREPIS UZ VYSELECTOVANYCH HODNOT
                for (int counter = 2; counter < 8; counter++)
                    worksheet.Cells[start, counter] = Double.Parse(worksheet.Cells[indexCelkovychVydavkov, counter].Value2.ToString());

                increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 6, 8, 11);

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
                    double[] sumArray = new double[4];
                    int sumIndex = start;
                    increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 6, 8, 11);

                    //PODLA PROGRAMOV YA JEDNOTLIVE SKUPINY
                    com = new SqlCommand("SELECT DISTINCT tv.ProjektPrvok FROM TableVstup tv JOIN ciselnikProjektPrvok cpp ON tv.ProjektPrvok=cpp.Prog WHERE tv.SyntetickyaleboFiktivny LIKE @Typ AND KalendarnyDen LIKE @Datum AND cpp.Skupina LIKE @Skupina ORDER BY tv.ProjektPrvok", Con);
                    com.Parameters.AddWithValue("@Datum", datum);
                    com.Parameters.AddWithValue("@Typ", typV);
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

                        SqlCommand get_name = new SqlCommand("SELECT Text FROM ciselnikProjektPrvok WHERE Prog LIKE @Prog", Con);
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
                        com = new SqlCommand("SELECT SUM(tv.SchvalenyRozpocet),SUM(tv.RozpocetPoZmenach),SUM(tv.Skutocnost) FROM TableVstup tv WHERE tv.SyntetickyaleboFiktivny LIKE @Typ AND tv.KalendarnyDen LIKE @Datum AND tv.ProjektPrvok LIKE @ProjektPrvok", Con);
                        com2 = new SqlCommand("SELECT SUM(tv.Skutocnost) FROM TableVstup tv WHERE tv.SyntetickyaleboFiktivny LIKE @Typ AND tv.KalendarnyDen LIKE @Datum AND tv.ProjektPrvok LIKE @ProjektPrvok " + vynimky, Con);
                        com2.Parameters.AddWithValue("@Datum", datum);
                        com.Parameters.AddWithValue("@Datum", datum);
                        com2.Parameters.AddWithValue("@Typ", typV);
                        com.Parameters.AddWithValue("@Typ", typV);
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

                        //SCITANIE SUMARU
                        if (poslednePatZnakove != posledneTrojZnakove)
                        {
                            sumArray[0] += schvalenyRoz;
                            sumArray[1] += upravenyRoz;
                            sumArray[2] += plneniePrijmov;
                            sumArray[3] += bezmrz;
                        }
                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 6, 8, 11);
                    }

                    //VYPIS SUMARU
                    //CISLA
                    worksheet.Cells[sumIndex, 2] = sumArray[0];
                    worksheet.Cells[sumIndex, 3] = sumArray[1];
                    worksheet.Cells[sumIndex, 4] = sumArray[2];
                    worksheet.Cells[sumIndex, 5] = sumArray[3];
                    //PERCENTA
                    if (sumArray[3] == 0 || sumArray[0] == 0)
                        worksheet.Cells[sumIndex, 6] = 0;
                    else
                        worksheet.Cells[sumIndex, 6] = 100 * (double)(sumArray[3] / sumArray[0]);

                    if (sumArray[3] == 0 || sumArray[1] == 0)
                        worksheet.Cells[sumIndex, 7] = 0;
                    else
                        worksheet.Cells[sumIndex, 7] = 100 * (double)(sumArray[3] / sumArray[1]);
                }

                worksheet.Cells[start, 1] = "D. Dotácia na prenesený výkon štátnej správy na vyššie územné celky (bežné výdavky) za programovú klasifikáciu 0781F, 0781G (ekon. položka 641014  a zdroj 111)";
                worksheet.Cells[start, 1].Style.WrapText = true;
                borderRange = worksheet.get_Range(left_char.ToString() + start, right_char.ToString() + start);
                worksheet.Cells[start, 1].Characters[111, 19].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                borderRange.Font.Bold = true;

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


        public void sprav_str_6_7_nove(string typP, string typV, string MSVSico, string polozka_VUC, List<string> zvolene_klasifikacie_list, string datum, int typVystupov, string zaokruhlenieMena, string zaokruhleniePercenta, char left_char, int left_num, char right_char, int right_num, int count_for_page, int strana_rel_pos, DateTime time, bool priamaTlac)//1 prijmy 2 vydavky
        {
            string connectionstring = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            SqlCommand com = null;
            //SqlCommand com2 = null;
            bool opro = false;

            DataTable dT = new DataTable();
            //DataTable dT2 = new DataTable();
            using (var Con = new SqlConnection(connectionstring))
            {
                string nameTabulka = "str.6-7";

                if (typVystupov == 1000)
                    nameTabulka += "_tis._eur";

                string name = "UCET_";
                name = name + nameTabulka + ".xls";

                string kvartal = vytvorAdresare(datum);
                string vynimky = vynimkyMRZ(connectionstring);

                System.IO.File.Copy("..\\..\\Hlav\\ZU\\hlpu15_upravene.xls", "Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name, true);
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
                int indexCelkovychVydavkov = 0;


                string hlavnyText = "Záväzné ukazovatele kapitoly MŠVVaŠ SR k " + datum + " ";
                if (typVystupov == 1)
                    hlavnyText += "(€)";
                else
                    hlavnyText += "(tis. €)";

                //DATUM
                //worksheet.Cells[1, 7] = nameTabulka;
                worksheet.Name = nameTabulka;
                worksheet.Cells[2, 1] = hlavnyText;
                worksheet.Cells[2, 6] = "Strana: 1";
                worksheet.Cells[4, 2] = worksheet.Cells[4, 2].Value2.ToString() + " " + datum.Substring(6);
                worksheet.Cells[4, 3] = worksheet.Cells[4, 3].Value2.ToString() + " " + datum;
                worksheet.Cells[4, 4] = worksheet.Cells[4, 4].Value2.ToString() + " " + datum;
                //worksheet.Cells[4, 5] = worksheet.Cells[4, 5].Value2.ToString() + " " + datum;
                Con.Open();

                for (int kapitola = 0; kapitola < 2; kapitola++)
                {
                    com = new SqlCommand("SELECT DISTINCT tv.Zdroj FROM TableVstup tv JOIN ciselnikZdroje cz ON tv.Zdroj=cz.Zdroj WHERE tv.SyntetickyaleboFiktivny LIKE @Typ AND KalendarnyDen LIKE @Datum AND cz.esfasr LIKE @TypEsfasr ORDER BY tv.Zdroj", Con);
                    com.Parameters.AddWithValue("@Datum", datum);

                    if (kapitola == 0)
                    {
                        com.Parameters.AddWithValue("@TypEsfasr", "E%");
                        com.Parameters.AddWithValue("@Typ", typP);
                    }
                    else
                    {
                        com.Parameters.AddWithValue("@TypEsfasr", "S%");
                        com.Parameters.AddWithValue("@Typ", typV);
                    }
                    dT = new DataTable();
                    using (SqlDataAdapter da = new SqlDataAdapter(com))
                    {
                        da.Fill(dT);
                        com.ExecuteNonQuery();
                    }

                    string[] arrZdroje;
                    //arrZdroje = new string[dT.Rows.Count + 3];

                    int i = 2;
                    if (kapitola == 0)
                    {
                        arrZdroje = new string[dT.Rows.Count + 4];
                        arrZdroje[0] = "I. Prijmy kapitoly (ŠR a EÚ)";
                        arrZdroje[1] = "-z toho ostatné iné zdroje";
                    }
                    else
                    {
                        arrZdroje = new string[dT.Rows.Count + 4];
                        arrZdroje[0] = "II. Výdavky kapitoly spolu (A + B + C)";
                        arrZdroje[1] = "A. Výdavky spolu bez prostriedkov podľa § 17 ods. 4 zákona č.523/2004 Z.z a z rozpočtu EÚ:";
                    }
                    arrZdroje[i] = "111";
                    i++;
                    if (kapitola == 0)
                    {
                        arrZdroje[i] = "B. Prostriedky z rozpočtu EÚ spolu, v tom:";
                        i++;
                    }
                    else
                    {
                        arrZdroje[i++] = "A.2.prostriedky na spolufinancovanie spolu, z toho:";
                    }

                    foreach (DataRow row in dT.Rows) // Loop over the rows.
                    {
                        arrZdroje[i] = (string)row[0];
                        i++;
                    }

                    if (kapitola > 0)
                    {
                        com.Parameters["@TypEsfasr"].Value = "E%";
                        dT = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(com))
                        {
                            da.Fill(dT);
                            com.ExecuteNonQuery();
                        }

                        if (dT.Rows.Count > 0)
                        {
                            Array.Resize<string>(ref arrZdroje, arrZdroje.Length + dT.Rows.Count + 4);
                            arrZdroje[i++] = "A.4. kapitálové výdavky (700) - bez prostriedkov na  spolufinancovanie";
                            arrZdroje[i++] = "-z toho zdroj 111";
                            arrZdroje[i++] = "B. Prostriedky podľa § 17 ods. 4 zákona č. 523/2004 Z.z :";
                            arrZdroje[i++] = "C. Prostriedky z rozpočtu EÚ spolu, v tom:";
                            foreach (DataRow row in dT.Rows) // Loop over the rows.
                            {
                                arrZdroje[i] = (string)row[0];
                                i++;
                            }
                        }
                    }
                    //ORAMOVANIE KAPITOLY
                    borderRange = worksheet.get_Range(left_char.ToString() + start, right_char.ToString() + start);
                    borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                    borderRange.Font.Bold = true;

                    int indexSum = start;
                    foreach (string helpString in arrZdroje) // Loop over the rows.
                    {
                        if (helpString == "E")
                        {
                            worksheet.Cells[start, 1] = helpString;
                            start++;
                            continue;
                        }
                        //DoplnenieZdroja kvoli doplnenym zdrojom pri niektorych vyberoch
                        String doplnenieZdroja = ") ";
                        String nazov_org = "";
                        String skratka_org = "";
                        //MEDZI SUCET NAPRIKLAD ZA EURO VSETKY
                        String doplnenie = "";
                        if (helpString.Substring(0, 3) == "C. ")
                            doplnenie = "AND cz.esfasr LIKE 'E%'";
                        else
                        {
                            if (helpString.Substring(0, 3) == "B. " && kapitola == 0)
                                doplnenie = "AND cz.esfasr LIKE 'E%'";
                            else
                            {
                                if (helpString.Substring(0, 3) == "A. ")
                                    doplnenie = "AND cz.esfasr NOT LIKE 'E%'";
                                else
                                {
                                    if (helpString.Substring(0, 3) == "A.2")
                                        doplnenie = "AND cz.esfasr LIKE 'S%'";
                                    else
                                    {
                                        if (helpString.Substring(0, 3) == "A.4")
                                        {
                                            doplnenie = "AND tv.polozka LIKE '7%' AND cz.esfasr NOT LIKE 'S%' AND cz.esfasr NOT LIKE 'E%'";
                                            //TEXTY KTORE SU V ZOSTAVACH NO NEVYPLNAME ICH MY
                                            //VYPISEME SEM
                                            if (helpString.Substring(0, 3) == "A.4")
                                            {
                                                worksheet.Cells[start, 1] = "A.3. mzdy, platy, služobné príjmy a ostatné osobné vyrovnania (610), (kód zdroja 111+11H), z toho:";
                                                worksheet.Cells[start, 1].Font.Bold = true;
                                                increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 5, 8, 11);
                                                worksheet.Cells[start, 1] = " - mzdy, platy, služobné príjmy a ostatné osobné vyrovnania aparátu ústredného orgánu (kód zdroja 111+11H)";
                                                increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 5, 8, 11);
                                                worksheet.Cells[start, 1] = " - počet zamestnancov rozpočtových organizácií, podľa prílohy č. 1 k UV SR č. 471/2017, z toho:";
                                                worksheet.Cells[start, 1].Characters[68, 19].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                                increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 5, 8, 11);
                                                worksheet.Cells[start, 1] = " - aparát ústredného orgánu";
                                                worksheet.Cells[start, 1].Font.Bold = true;
                                                increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 5, 8, 11);
                                                worksheet.Cells[start, 1] = " - administratívne kapacity rozpočtových organizácií osobitne sledované, podľa prílohy č. 1 k UV SR č. 471/2017, z toho:";
                                                worksheet.Cells[start, 1].Characters[93, 19].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                                increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 5, 8, 11);
                                                worksheet.Cells[start, 1] = " - aparát ústredného orgánu";
                                                worksheet.Cells[start, 1].Font.Bold = true;
                                                increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 5, 8, 11);
                                            }
                                        }
                                        else
                                        {
                                            if (helpString == "-z toho zdroj 111:")
                                                doplnenie = "AND tv.polozka LIKE '7%' AND tv.Zdroj LIKE '111'";
                                        }
                                    }
                                }
                            }
                        }

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
                            if (helpString == "111")
                            {
                                if (kapitola == 0)
                                    nazov_org = "A) Záväzný ukazovateľ (ŠR) - zdroj 111";
                                else
                                {
                                    doplnenieZdroja = "OR tv.Zdroj LIKE '11H'" + doplnenieZdroja;
                                    nazov_org = "A.1. prostriedky štátneho rozpočtu (kód zdroja 111 + 11H)";
                                }
                                worksheet.Cells[start, 1].Font.Bold = true;
                            }
                            else
                                nazov_org = helpString + " - " + (string)get_name_table.Rows[0][0];
                        }
                        else
                        {
                            worksheet.Cells[start, 1].Font.Bold = true;
                            nazov_org = helpString;
                        }

                        worksheet.Cells[start, 1] = nazov_org;
                        doplnenie = doplnenieZdroja + doplnenie;
                        dT = new DataTable();

                        //Konkretny select pre B. vo vydavjkoch = MRZ
                        if (helpString == "-z toho ostatné iné zdroje" || helpString.Substring(0, 3) == "B. " && kapitola ==1)
                        {
                            com = new SqlCommand("SELECT SUM(tv.SchvalenyRozpocet),SUM(tv.RozpocetPoZmenach),SUM(tv.Skutocnost) FROM TableVstup tv JOIN ciselnikZdroje cz ON tv.Zdroj=cz.Zdroj WHERE tv.SyntetickyaleboFiktivny LIKE @typ AND tv.KalendarnyDen LIKE @Datum AND (tv.Zdroj LIKE @Zdroj " + doplnenie + " " + vynimky, Con);
                            //com2 = new SqlCommand("SELECT SUM(tv.Skutocnost) FROM TableVstup tv JOIN ciselnikZdroje cz ON tv.Zdroj=cz.Zdroj WHERE tv.SyntetickyaleboFiktivny LIKE @typ AND tv.KalendarnyDen LIKE @Datum AND (tv.Zdroj LIKE @Zdroj " + doplnenie + " " + vynimky, Con);
                        }
                        //Pre vsetky ostatne
                        else
                        {
                            com = new SqlCommand("SELECT SUM(tv.SchvalenyRozpocet),SUM(tv.RozpocetPoZmenach),SUM(tv.Skutocnost) FROM TableVstup tv JOIN ciselnikZdroje cz ON tv.Zdroj=cz.Zdroj WHERE tv.SyntetickyaleboFiktivny LIKE @typ AND tv.KalendarnyDen LIKE @Datum AND (tv.Zdroj LIKE @Zdroj " + doplnenie, Con);
                            //com2 = new SqlCommand("SELECT SUM(tv.Skutocnost) FROM TableVstup tv JOIN ciselnikZdroje cz ON tv.Zdroj=cz.Zdroj WHERE tv.SyntetickyaleboFiktivny LIKE @typ AND tv.KalendarnyDen LIKE @Datum AND (tv.Zdroj LIKE @Zdroj " + doplnenie + " " + vynimky, Con);
                        }

                        //com2.Parameters.AddWithValue("@Datum", datum);
                        com.Parameters.AddWithValue("@Datum", datum);

                        if (get_name_table.Rows.Count != 0)
                        {
                            com.Parameters.AddWithValue("@Zdroj", helpString);
                            //com2.Parameters.AddWithValue("@Zdroj", helpString);
                        }
                        else
                        {
                            com.Parameters.AddWithValue("@Zdroj", "%");
                            //com2.Parameters.AddWithValue("@Zdroj", "%");
                        }
                        if (kapitola == 0)
                        {
                            //com2.Parameters.AddWithValue("@typ", typP);
                            com.Parameters.AddWithValue("@typ", typP);
                        }
                        else
                        {
                            //com2.Parameters.AddWithValue("@typ", typV);
                            com.Parameters.AddWithValue("@typ", typV);
                        }


                        using (SqlDataAdapter da = new SqlDataAdapter(com))
                        //using (SqlDataAdapter da2 = new SqlDataAdapter(com2))
                        {
                            da.Fill(dT);
                            //da2.Fill(dT2);
                            com.ExecuteNonQuery();
                            //com2.ExecuteNonQuery();
                        }

                        i = 0;

                        for (int j = 0; j < dT.Rows.Count; j++)// Loop over the rows.
                        {
                            //HODNOTY ROZPOCTOV A HODNOT MRZ A BEZMRZ
                            double schvalenyRoz = Double.Parse(dT.Rows[j][0].ToString()) / typVystupov;
                            double upravenyRoz = Double.Parse(dT.Rows[j][1].ToString()) / typVystupov;
                            double plneniePrijmov = Double.Parse(dT.Rows[j][2].ToString()) / typVystupov;
                            //double bezmrz = Double.Parse(dT2.Rows[j][0].ToString()) / typVystupov;
                            //double mrz = Double.Parse(dT.Rows[j][2].ToString()) / typVystupov;
                            //mrz -= bezmrz;

                            if(helpString == "-z toho ostatné iné zdroje" || (helpString.Substring(0, 3) == "B. " && kapitola == 1))
                            {
                                schvalenyRoz = worksheet.Cells[indexSum, 2].Value - schvalenyRoz;
                                upravenyRoz = worksheet.Cells[indexSum, 3].Value - upravenyRoz;
                                plneniePrijmov = worksheet.Cells[indexSum, 4].Value - plneniePrijmov;
                            }

                            worksheet.Cells[start, 2] = schvalenyRoz;
                            worksheet.Cells[start, 3] = upravenyRoz;
                            worksheet.Cells[start, 4] = plneniePrijmov;
                            

                            //HODNOTY S A BEZ MRZ
                            //worksheet.Cells[start, 5] = bezmrz;

                            //PERCENTA
                            //if (bezmrz == 0 || schvalenyRoz == 0)
                            if (plneniePrijmov == 0 || schvalenyRoz == 0)
                                worksheet.Cells[start, 5] = 0;
                            else
                                worksheet.Cells[start, 5] = 100 * (double)(plneniePrijmov / schvalenyRoz);
                                //worksheet.Cells[start, 6] = 100 * (double)(bezmrz / schvalenyRoz);

                            //if (bezmrz == 0 || upravenyRoz == 0)
                            if (plneniePrijmov == 0 || upravenyRoz == 0)
                                worksheet.Cells[start, 6] = 0;
                            else
                                worksheet.Cells[start, 6] = 100 * (double)(plneniePrijmov / upravenyRoz);
                                //worksheet.Cells[start, 7] = 100 * (double)(bezmrz / upravenyRoz);
                        }

                        //ULOZENIE INDEXU KDE SA NACHADZAJU UZ VYSELECTOVANE CELKOVE VYDAVKY
                        if (helpString == arrZdroje[0] && kapitola == 1)
                            indexCelkovychVydavkov = start;

                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 5, 8, 11);
                    }
                }

                worksheet.Cells[start, 1] = "D. Výdavky štátneho rozpočtu na realizáciu programov a častí  programov vlády SR (príloha č.2 listu MF/020612/2017-441), z toho:";
                worksheet.Cells[start, 1].Style.WrapText = true;
                worksheet.Cells[start, 1].Characters[101, 18].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                borderRange = worksheet.get_Range(left_char.ToString() + start, right_char.ToString() + start);
                borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                borderRange.Font.Bold = true;

                //PREPIS UZ VYSELECTOVANYCH HODNOT
                //for (int counter = 2; counter < 8; counter++)
                for (int counter = 2; counter < 7; counter++)
                    worksheet.Cells[start, counter] = Double.Parse(worksheet.Cells[indexCelkovychVydavkov, counter].Value2.ToString());

                increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 5, 8, 11);

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
                    double[] sumArray = new double[3];
                    int sumIndex = start;
                    increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 5, 8, 11);

                    //PODLA PROGRAMOV YA JEDNOTLIVE SKUPINY
                    com = new SqlCommand("SELECT DISTINCT tv.ProjektPrvok FROM TableVstup tv JOIN ciselnikProjektPrvok cpp ON tv.ProjektPrvok=cpp.Prog WHERE tv.SyntetickyaleboFiktivny LIKE @Typ AND KalendarnyDen LIKE @Datum AND cpp.Skupina LIKE @Skupina ORDER BY tv.ProjektPrvok", Con);
                    com.Parameters.AddWithValue("@Datum", datum);
                    com.Parameters.AddWithValue("@Typ", typV);
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
                        com = new SqlCommand("SELECT SUM(tv.SchvalenyRozpocet),SUM(tv.RozpocetPoZmenach),SUM(tv.Skutocnost) FROM TableVstup tv WHERE tv.SyntetickyaleboFiktivny LIKE @Typ AND tv.KalendarnyDen LIKE @Datum AND tv.ProjektPrvok LIKE @ProjektPrvok", Con);
                        //com2 = new SqlCommand("SELECT SUM(tv.Skutocnost) FROM TableVstup tv WHERE tv.SyntetickyaleboFiktivny LIKE @Typ AND tv.KalendarnyDen LIKE @Datum AND tv.ProjektPrvok LIKE @ProjektPrvok " + vynimky, Con);
                        //com2.Parameters.AddWithValue("@Datum", datum);
                        com.Parameters.AddWithValue("@Datum", datum);
                        //com2.Parameters.AddWithValue("@Typ", typV);
                        com.Parameters.AddWithValue("@Typ", typV);
                        //com2.Parameters.AddWithValue("@ProjektPrvok", poslednePatZnakove + "%");
                        com.Parameters.AddWithValue("@ProjektPrvok", poslednePatZnakove + "%");

                        dT = new DataTable();
                        //dT2 = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(com))
                        //using (SqlDataAdapter da2 = new SqlDataAdapter(com2))
                        {
                            da.Fill(dT);
                            //da2.Fill(dT2);
                            com.ExecuteNonQuery();
                            //com2.ExecuteNonQuery();
                        }

                        //VYPIS
                        double schvalenyRoz = Double.Parse(dT.Rows[0][0].ToString()) / typVystupov;
                        double upravenyRoz = Double.Parse(dT.Rows[0][1].ToString()) / typVystupov;
                        double plneniePrijmov = Double.Parse(dT.Rows[0][2].ToString()) / typVystupov;
                        //double bezmrz = Double.Parse(dT2.Rows[0][0].ToString()) / typVystupov;
                        //double mrz = Double.Parse(dT.Rows[0][2].ToString()) / typVystupov;
                        //mrz -= bezmrz;

                        worksheet.Cells[start, 2] = schvalenyRoz;
                        worksheet.Cells[start, 3] = upravenyRoz;
                        worksheet.Cells[start, 4] = plneniePrijmov;

                        //HODNOTY S A BEZ MRZ
                        //worksheet.Cells[start, 5] = bezmrz;

                        //PERCENTA
                        //if (bezmrz == 0 || schvalenyRoz == 0)
                        if (plneniePrijmov == 0 || schvalenyRoz == 0)
                            worksheet.Cells[start, 5] = 0;
                        else
                            worksheet.Cells[start, 5] = 100 * (double)(plneniePrijmov / schvalenyRoz);
                        //worksheet.Cells[start, 6] = 100 * (double)(bezmrz / schvalenyRoz);

                        //if (bezmrz == 0 || upravenyRoz == 0)
                        if (plneniePrijmov == 0 || upravenyRoz == 0)
                            worksheet.Cells[start, 6] = 0;
                        else
                            worksheet.Cells[start, 6] = 100 * (double)(plneniePrijmov / upravenyRoz);
                        //worksheet.Cells[start, 7] = 100 * (double)(bezmrz / upravenyRoz);

                        //SCITANIE SUMARU
                        if (poslednePatZnakove != posledneTrojZnakove)
                        {
                            sumArray[0] += schvalenyRoz;
                            sumArray[1] += upravenyRoz;
                            sumArray[2] += plneniePrijmov;
                            //sumArray[3] += bezmrz;
                        }
                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 5, 8, 11);

                        if(poslednePatZnakove.ToString() == "07813")
                        {
                            worksheet.Cells[start, 1] = "- z toho stavovské a profesijné organizácie pre systém duálneho vzdelávania";
                            worksheet.Cells[start, 1].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                            increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 5, 8, 11);
                        }
                    }

                    //VYPIS SUMARU
                    //CISLA
                    worksheet.Cells[sumIndex, 2] = sumArray[0];
                    worksheet.Cells[sumIndex, 3] = sumArray[1];
                    worksheet.Cells[sumIndex, 4] = sumArray[2];
                    //worksheet.Cells[sumIndex, 5] = sumArray[3];
                    //PERCENTA
                    //if (sumArray[3] == 0 || sumArray[0] == 0)
                    if (sumArray[2] == 0 || sumArray[0] == 0)
                        worksheet.Cells[sumIndex, 5] = 0;
                    else
                        worksheet.Cells[sumIndex, 5] = 100 * (double)(sumArray[2] / sumArray[0]);
                    //worksheet.Cells[sumIndex, 6] = 100 * (double)(sumArray[3] / sumArray[0]);

                    //if (sumArray[3] == 0 || sumArray[1] == 0)
                    if (sumArray[2] == 0 || sumArray[1] == 0)
                        worksheet.Cells[sumIndex, 6] = 0;
                    else
                        worksheet.Cells[sumIndex, 6] = 100 * (double)(sumArray[2] / sumArray[1]);
                    //worksheet.Cells[sumIndex, 7] = 100 * (double)(sumArray[3] / sumArray[1]);
                }

                worksheet.Cells[start, 1] = "E. Dotácia na prenesený výkon štátnej správy na vyššie územné celky (bežné výdavky) za programovú klasifikáciu 0781B, 0781F, 0781G (ekon. položka 641014  a zdroj 111)";
                worksheet.Cells[start, 1].Style.WrapText = true;
                borderRange = worksheet.get_Range(left_char.ToString() + start, right_char.ToString() + start);
                worksheet.Cells[start, 1].Characters[111, 21].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                borderRange.Font.Bold = true;

                com = new SqlCommand("SELECT SUM(SchvalenyRozpocet),SUM(RozpocetPoZmenach),SUM(Skutocnost) FROM TableVstup WHERE Zdroj LIKE '111' AND Podpolozka LIKE '" + polozka_VUC + "' AND SyntetickyaleboFiktivny LIKE @typ AND KalendarnyDen LIKE @Datum AND Ico LIKE @Ico AND " + helper_ProjektPrvok_concat(zvolene_klasifikacie_list), Con);
                com.Parameters.AddWithValue("@typ", typV);
                com.Parameters.AddWithValue("@Datum", datum);
                com.Parameters.AddWithValue("@Ico", MSVSico);

                dT = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(com))
                {
                    da.Fill(dT);
                    com.ExecuteNonQuery();
                }

                for (int k = 0; k < 3; k++)
                {
                    double value = 0;
                    value = Double.Parse(dT.Rows[0][k].ToString());
                    if (value != 0)
                        worksheet.Cells[start, 2 + k] = value/typVystupov;
                    else
                        worksheet.Cells[start, 2 + k] = 0;
                }

                double main_schvalenyRoz = Double.Parse(dT.Rows[0][0].ToString());
                double main_upravenyRoz = Double.Parse(dT.Rows[0][1].ToString());
                double main_plneniePrijmov = Double.Parse(dT.Rows[0][2].ToString());
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



                for (char c = left_char; c <= (char)((int)right_char); c++)//tenke oramovanie od border po start
                {
                    borderRange = worksheet.get_Range(c.ToString() + border, c.ToString() + start);
                    borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                    //ZAOKRUHLOVANIE
                    //CISLA
                    if ((c > left_char) && (c < left_char + 4))
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

        private string helper_ProjektPrvok_concat(List<string> ProjektPrvok_list)
        {//(ProjektPrvok LIKE 65465 or ProjektPrvok LIKE 465456 or ProjektPrvok LIKE 65456465)
            string ret_string = "(";
            foreach (var ico in ProjektPrvok_list)
            {
                ret_string += "ProjektPrvok LIKE '" + ico + "%'";
                if (ico != ProjektPrvok_list[ProjektPrvok_list.Count - 1])
                    ret_string += " OR ";
            }

            ret_string += ")";
            return ret_string;
        }
    }
}
