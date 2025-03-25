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
using System.Reflection;

//Zostavy_tab10_11_12_13_14_16_17 zostavy7 = new Zostavy_tab10_11_12_13_14_16_17();
//zostavy7.spravTab_10_10a(string_225, string_ICO, getVybranyDatum(), getTypVystupu(), getZaokruhlenieMena(), getZaokruhleniePercenta(), 'A', 1, 'P', 11, 49, 4, time);
//zostavy7.spravTab_11_12_13_14_16_17(string_225, string_ICO, getVybranyDatum(), getTypVystupu(), getZaokruhlenieMena(), getZaokruhleniePercenta(), 'A', 1, 'P', 11, 49, 4, time);

namespace ISEF_Rozbory
{
    class Zostavy_tab10_11_12_13_14_16_17 : Zostavy
    {

        public void spravTab_10_10a(string typ, string MSVSico, string datum, int typVystupov, string zaokruhlenieMena, string zaokruhleniePercenta, char left_char, int left_num, char right_char, int right_num, int count_for_page, int strana_rel_pos, DateTime time, bool priamaTlac)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            SqlCommand com = null;
            bool opro = true;
            

            DataTable dT = new DataTable();
            DataTable dtOPROb = new DataTable();
            DataTable dtOPROk = new DataTable();
            using (var Con = new SqlConnection(connectionstring))
            {
                Con.Open();
                string nameTabulka = "Tab_10_10a";

                string name = "UCET_";
                name = name + nameTabulka + ".xls";


                string kvartal = vytvorAdresare(datum);
                System.IO.File.Copy("..\\..\\Hlav\\ZU\\hlpr5cv3.xls", "Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name, true);
                Excel.Application app = new Excel.Application();
                string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                Excel.Workbook workbook = app.Workbooks.Open(path + "\\" + "bin\\Debug\\Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name);
                Excel._Worksheet worksheet = workbook.Sheets[1];
                Excel.Range range = worksheet.UsedRange;
                Excel.Range borderRange;
                Excel._Worksheet worksheetFirst = workbook.Sheets[1];

                
                worksheet.Cells[4, 1] = "len rozpočtové prostriedky k " + datum + " (v €)";
                worksheet.Cells[7, 2] = worksheet.Cells[7, 2].Value2.ToString() + " " + datum.Substring(6);
                worksheet.Cells[7, 5] = worksheet.Cells[7, 5].Value2.ToString() + " " + datum;
                worksheet.Cells[7, 8] = worksheet.Cells[7, 8].Value2.ToString() + " " + datum;

                //double[] sum_all = new double[5];


                //VSETKY TYPY DOKOPY
                for (int tabs = 0; tabs < 2; tabs++)
                {
                    string selectString = "";
                    //VYTVARANIE NOVEHO ZOSITU PLUS KOPIROVANIE HLAVICKY 
                    //VACSIE AKO 0 LEBO PRVY OTVARAM UZ PRED CYKLOM ABY SOM NASTAVIL HLAVICKU KTORU POTOM KOPIRUJEM
                    if (tabs > 0)
                    {
                        workbook.Sheets[1].Copy(Type.Missing, workbook.Sheets[workbook.Sheets.Count]);
                        worksheet = workbook.Sheets[workbook.Sheets.Count];
                        int help = worksheet.Rows.Count;
                        range = worksheet.get_Range("A12", "P" + help);
                        range.Clear();
                        
                        
                    }

                    int rowCount = range.Rows.Count;
                    int start = right_num + 1;
                    int border = start;
                    int strana = 2;

                    string worksheet_name = "Tab_10";
                    worksheet.Cells[1, 15] = "Tabuľka č. 10";
                    if (tabs > 0)
                    {
                        worksheet_name += "a";
                        worksheet.Cells[1, 15] = "Tabuľka č. 10a";
                        worksheet.Cells[4, 1] = "vrátane iných zdrojov k " + datum + " (v €)";
                    }
                    worksheet.Name = worksheet_name;

                    
                    worksheet.Columns[1].WrapText = true;
                    
                    //worksheet.Columns[2].AutoFit();
                    //worksheet.Columns[4].AutoFit();
                    //worksheet.Columns[5].AutoFit();
                    //worksheet.Columns[6].AutoFit();


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

                        SqlCommand vydavb = null;
                        SqlCommand vydavk = null;
                        if (tabs == 0)
                        {
                            vydavb = new SqlCommand("Select SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Ico LIKE @Ico AND KalendarnyDen LIKE @Datum AND HlavnaKategoria IN ('200','300','600','700') AND SyntetickyaleboFiktivny LIKE @Synteticky AND SUBSTRING(Kategoria, 1, 1) LIKE '6' " + vynimky, Con);
                            vydavk = new SqlCommand("Select SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Ico LIKE @Ico AND KalendarnyDen LIKE @Datum AND HlavnaKategoria IN ('200','300','600','700') AND SyntetickyaleboFiktivny LIKE @Synteticky AND SUBSTRING(Kategoria, 1, 1) LIKE '7' " + vynimky, Con);
                        }
                        else if(tabs==1)
                        {
                            vydavb = new SqlCommand("Select SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Ico LIKE @Ico AND KalendarnyDen LIKE @Datum AND HlavnaKategoria IN ('200','300','600','700') AND SyntetickyaleboFiktivny LIKE @Synteticky AND SUBSTRING(Kategoria, 1, 1) LIKE '6'", Con);
                            vydavk = new SqlCommand("Select SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Ico LIKE @Ico AND KalendarnyDen LIKE @Datum AND HlavnaKategoria IN ('200','300','600','700') AND SyntetickyaleboFiktivny LIKE @Synteticky AND SUBSTRING(Kategoria, 1, 1) LIKE '7'", Con);
                        }
                        vydavb.Parameters.AddWithValue("@Datum", datum);
                        vydavk.Parameters.AddWithValue("@Datum", datum);
                        vydavb.Parameters.AddWithValue("@Synteticky", typ);
                        vydavk.Parameters.AddWithValue("@Synteticky", typ);

                        if (helpString == "Kapitola MŠVVaŠ SR spolu" || helpString == "Ostatné priamo riadené organizácie")
                        {
                            vydavb.Parameters.AddWithValue("@Ico", "%");
                            vydavk.Parameters.AddWithValue("@Ico", "%");
                        }
                        else
                        {
                            vydavb.Parameters.AddWithValue("@Ico", helpString);
                            vydavk.Parameters.AddWithValue("@Ico", helpString);
                        }

                        DataTable vydavb_table = new DataTable();
                        DataTable vydavk_table = new DataTable();
                        using (SqlDataAdapter vydavb_adapter = new SqlDataAdapter(vydavb))
                        using (SqlDataAdapter vydavk_adapter = new SqlDataAdapter(vydavk))
                        {
                            vydavb_adapter.Fill(vydavb_table);
                            vydavk_adapter.Fill(vydavk_table);
                            vydavb.ExecuteNonQuery();
                            vydavk.ExecuteNonQuery();
                        }

                        int comparison = 0;
                        if ((comparison = helpString.CompareTo(MSVSico)) == 0)
                        {
                            using (SqlDataAdapter vydavb_adapter = new SqlDataAdapter(vydavb))
                            using (SqlDataAdapter vydavk_adapter = new SqlDataAdapter(vydavk))
                            {
                                vydavb_adapter.Fill(dtOPROb);
                                vydavk_adapter.Fill(dtOPROk);
                                vydavb.ExecuteNonQuery();
                                vydavk.ExecuteNonQuery();
                            }
                        }

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

                        //worksheet.Cells[start, 1] = nazov_org;

                        if ((int)vydavb_table.Rows[0][3] > 0 || (int)vydavk_table.Rows[0][3] > 0)
                        {
                            worksheet.Cells[start, 1] = nazov_org;

                            double[] columns = new double[9];

                            

                            foreach (DataRow row in vydavb_table.Rows)
                            {
                                for(int j = 0; j < 3; j++)
                                {
                                    double value = 0;
                                    
                                    if (row[j].ToString() != "")
                                    {
                                        if (helpString == "Ostatné priamo riadené organizácie")
                                            value = (double)row[j] - Double.Parse(dtOPROb.Rows[0][j].ToString());
                                        else
                                            value = (double)row[j];

                                        worksheet.Cells[start, 3 * j + 2] = value / typVystupov ;
                                        columns[2*j] = value;
                                    }
                                    else
                                    {
                                        worksheet.Cells[start, 3*j+2] = 0;
                                        columns[2*j] = 0;
                                    }
                                }
                            }

                            foreach (DataRow row in vydavk_table.Rows)
                            {
                                
                                for (int j = 0; j < 3; j++)
                                {
                                    double value = 0;

                                    if (row[j].ToString() != "")
                                    {
                                        if (helpString == "Ostatné priamo riadené organizácie")
                                            value = (double)row[j] - Double.Parse(dtOPROk.Rows[0][j].ToString());
                                        else
                                            value = (double)row[j];

                                        worksheet.Cells[start, 3 * j + 3] = value / typVystupov;
                                        columns[2 * j + 1] = value;
                                    }
                                    else
                                    {
                                        worksheet.Cells[start, 3 * j + 3] = 0;
                                        columns[2 * j + 1] = 0;
                                    }
                                }
                                
                            }

                            

                            columns[6] = columns[0] + columns[1];
                            columns[7] = columns[2] + columns[3];
                            columns[8] = columns[4] + columns[5];

                            //if (valid_len == 2)
                            //{
                            //    sum_all[0] += columns[0];
                            //    sum_all[1] += columns[1];
                            //    sum_all[2] += columns[6];
                            //    sum_all[3] += columns[2];
                            //    sum_all[4] += columns[3];
                            //    sum_all[5] += columns[7];
                            //    sum_all[6] += columns[4];
                            //    sum_all[7] += columns[5];
                            //    sum_all[8] += columns[8];
                            //}

                            

                            worksheet.Cells[start, 4] = columns[6] / typVystupov;
                            worksheet.Cells[start, 7] = columns[7] / typVystupov;
                            worksheet.Cells[start, 10] = columns[8] / typVystupov;

                            if (columns[0] != 0)
                                worksheet.Cells[start, 11] = (columns[4] / columns[0]) * 100;
                            else
                                worksheet.Cells[start, 11] = "0";
                            if (columns[1] != 0)
                                worksheet.Cells[start, 12] = (columns[5] / columns[1]) * 100;
                            else
                                worksheet.Cells[start, 12] = "0";
                            if (columns[6] != 0)
                                worksheet.Cells[start, 13] = (columns[8] / columns[6]) * 100;
                            else
                                worksheet.Cells[start, 13] = "0";

                            if (columns[2] != 0)
                                worksheet.Cells[start, 14] = (columns[4] / columns[2]) * 100;
                            else
                                worksheet.Cells[start, 14] = "0";
                            if (columns[3] != 0)
                                worksheet.Cells[start, 15] = (columns[5] / columns[3]) * 100;
                            else
                                worksheet.Cells[start, 15] = "0";
                            if (columns[7] != 0)
                                worksheet.Cells[start, 16] = (columns[8] / columns[7]) * 100;
                            else
                                worksheet.Cells[start, 16] = "0";

                            if (helpString != "Kapitola MŠVVaŠ SR spolu")
                            {
                                bool ret;
                                int last_border = border;
                                int last_start = start;
                                ret = increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 11, 2, 11);
                                if (ret == true)
                                    selectString += "B" + last_border.ToString() + ":" + "J" + last_start.ToString() + System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator;
                            }
                        }
                        for (char c = left_char; c <= (char)((int)right_char); c++)//tenke oramovanie od border po start
                        {
                            borderRange = worksheet.get_Range(c.ToString() + border, c.ToString() + start);
                            borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                            //ZAOKRUHLOVANIE
                            //CISLA
                            if ((c >= left_char + 1 && c < left_char + 10))
                            {
                                borderRange.NumberFormat = zaokruhlenieMena;
                                borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                            }
                            //PERCENTA
                            else
                            {
                                if (c > left_char + 9)
                                {
                                    borderRange.NumberFormat = zaokruhleniePercenta;
                                    borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                                }
                            }
                        }
                        
                    }
                    borderRange = worksheet.get_Range(left_char.ToString() + border, right_char.ToString() + start);//hrube po Celkovo
                    borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                    var culture = new CultureInfo("ru-RU");
                    worksheet.Cells[++start, 1] = "Dátum vytvorenia: " + time.ToString(culture);


                    selectString += "B" + border.ToString() + ":" + "J" + start.ToString();
                    //zvacsenie iba stlpcov kde ukazuje vacsie cisla ako ########
                    Excel.Range selectedRange = worksheet.Range[selectString].Columns["A:I"];
                    selectedRange.Columns.AutoFit();
                    foreach (Excel.Range column in selectedRange.Columns)
                    {
                        if (column.ColumnWidth < 10.86)//sirka stlpca tak jak ju ukazuje excel
                            column.ColumnWidth = 10.86;
                    }
                }
                workbook.Save();
                if (priamaTlac)
                    PrintMyExcelFile(workbook);
                Kontrola.release_excel(range, worksheet, workbook, app);
            }
        }


        public void spravTab_11_12_13_14_16_17(string typ, string MSVSico, string datum, int typVystupov, string zaokruhlenieMena, string zaokruhleniePercenta, char left_char, int left_num, char right_char, int right_num, int count_for_page, int strana_rel_pos, DateTime time, bool priamaTlac)
        {
            //16,17 su prispevkove

            string connectionstring = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            SqlCommand com = null;
            //
            //
            //PODMIENKA VYPNUT OPRO AK ROBI OSTATNE ORGANIZACIE
            //
            //
            bool opro = true;


            string[][] text_hlavicka = new string[6][];
            text_hlavicka[0] = new string[]
            {
                "Schválený rozpočet, upravený rozpočet a čerpanie výdavkov kapitoly školstva podľa okruhov a organizácií","za zdroj 111 - Rozpočtové prostriedky kapitoly ","k "
            };
            text_hlavicka[1] = new string[]
            {
                "Schválený rozpočet, upravený rozpočet a čerpanie výdavkov kapitoly školstva podľa okruhov a organizácií","a za  programové obdobie 2014-2020 - kódy zdroja 1AA1, 1AA2, 1AA3, 1AC1, 1AC2, 1AC3, 1AY1, 1BI1, 1BI2 ","k "
            };
            text_hlavicka[2] = new string[]
            {
                "Schválený rozpočet, upravený rozpočet a čerpanie výdavkov kapitoly školstva podľa okruhov a organizácií  za zdroje z predchádzajúcich rokov","a za  programové obdobie 2014-2020 - kódy zdroja 3AA1, 3AA2, 3AA3, 3AC1, 3AC2, 3AC3, 3AY1","k "
            };
            text_hlavicka[3] = new string[]
            {
                "Schválený rozpočet, upravený rozpočet a čerpanie výdavkov kapitoly školstva podľa okruhov a organizácií","za ostatné rozpočtové zdroje 131L, 131M, 11UA, 1P01, 1P02, 3P01, 3P02","k "
            };
            text_hlavicka[4] = new string[]
            {
                "Schválený rozpočet, upravený rozpočet a čerpanie výdavkov príspevkových organizácií za zdroje z predchádzajúcich rokov","a za programové obdobie 2014-2020  - kódy zdroja 3AA1, 3AA2, 3AA3, 3AC1, 3AC2, 3AC3","k "
            };
            text_hlavicka[5] = new string[]
            {
                "Schválený rozpočet, upravený rozpočet a čerpanie výdavkov príspevkových organizácií", "a za zdroje z predchádzajúcich rokov - kódy zdroja  11H, 131I, 131J, 131K, 131M, 131N, 14, 3P01, 3P02", "k "
            };



            string[][] zdrojeList = new string[6][];
            zdrojeList[0] = new string[]
            {
                "111"
            };
            zdrojeList[1] = new string[]
            {
                "1AA1", "1AA2", "1AA3","1AC1", "1AC2","1AC3","1AY1","1BI1","1BI2"
            };
            zdrojeList[2] = new string[]
            {
                "3AA1", "3AA2", "3AA3", "3AC1", "3AC2", "3AC3","3AY1"
            };
            zdrojeList[3] = new string[]
            {
                "131J","131K","131L","131M","131N","11H","11UA","1P01","1P02","3P02","3P01"
            };
            zdrojeList[4] = new string[]
            {
                "3AA1", "3AA2", "3AA3", "3AC1", "3AC2", "3AC3", "3AM1", "3AM2","1AA1","1AA2","1AA3","1AC1","1AC2","1AC3"
            };
            zdrojeList[5] = new string[]
            {
                "131I", "131J", "131K", "131L", "131M","131N"
            };

            DataTable dT = new DataTable();
            DataTable dtOPROb = new DataTable();
            DataTable dtOPROk = new DataTable();
            using (var Con = new SqlConnection(connectionstring))
            {
                Con.Open();

                string nameTabulka = "Tab_11_12_13_14_16_17";

                string name = "UCET_";
                name = name + nameTabulka + ".xls";


                string kvartal = vytvorAdresare(datum);
                System.IO.File.Copy("..\\..\\Hlav\\ZU\\hlpr5cv3.xls", "Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name, true);
                Excel.Application app = new Excel.Application();
                string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                Excel.Workbook workbook = app.Workbooks.Open(path + "\\" + "bin\\Debug\\Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name);
                Excel._Worksheet worksheet = workbook.Sheets[1];
                Excel.Range range = worksheet.UsedRange;
                Excel.Range borderRange;
                Excel._Worksheet worksheetFirst = workbook.Sheets[1];

                //worksheet.Cells[5, 1] = "za zdroj 111 - Rozpočtové prostriedky kapitoly k " + datum + " (€)";
                worksheet.Cells[7, 2] = worksheet.Cells[7, 2].Value2.ToString() + " " + datum.Substring(6);
                worksheet.Cells[7, 5] = worksheet.Cells[7, 5].Value2.ToString() + " " + datum;
                worksheet.Cells[7, 8] = worksheet.Cells[7, 8].Value2.ToString() + " " + datum;


                int jump = 0;
                for (int i = 0; i < 6; i++)
                {
                    

                    string selectString = "";
                    //VYTVARANIE NOVEHO ZOSITU PLUS KOPIROVANIE HLAVICKY 
                    //VACSIE AKO 0 LEBO PRVY OTVARAM UZ PRED CYKLOM ABY SOM NASTAVIL HLAVICKU KTORU POTOM KOPIRUJEM
                    if (i > 0)
                    {
                        opro = false;
                        workbook.Sheets[1].Copy(Type.Missing, workbook.Sheets[workbook.Sheets.Count]);
                        worksheet = workbook.Sheets[workbook.Sheets.Count];
                        int help = worksheet.Rows.Count;
                        range = worksheet.get_Range("A12", "P" + help);
                        range.Clear();
                    }

                    int rowCount = range.Rows.Count;
                    int start = right_num + 1;
                    int border = start;
                    int strana = 2;

                    if (i > 3)
                        jump = 1;
                    string worksheet_name = "Tab_" + (11 + i + jump);
                    worksheet.Cells[1, 15] = "Tabuľka č. " + (11 + i + jump);

                    worksheet.Cells[3, 1] = text_hlavicka[i][0];
                    worksheet.Cells[4, 1] = text_hlavicka[i][1];
                    worksheet.Cells[5, 1] = text_hlavicka[i][2] + datum + " (v €)";


                    //worksheet.Cells[5, 1] = "za zdroj 111 - Rozpočtové prostriedky kapitoly k " + datum + " (€)";
                    
                    //tu este poriesit texty

                    worksheet.Name = worksheet_name;
                    worksheet.Columns[1].WrapText = true;

                    for (int j = 0; j < zdrojeList[i].Count(); j++)
                    {
                        bool jediny = false;
                        range = worksheet.get_Range("A" + start, "P" + start);
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
                        //com.Parameters.AddWithValue("@typ", typ);
                        com.Parameters.AddWithValue("@Datum", datum);

                        if (i < 4)
                            com.Parameters.AddWithValue("@Typ", typ);
                        else
                            com.Parameters.AddWithValue("@Typ", "VYR");

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
                        else if(i > 0 && dT.Rows.Count > 1)
                            arrIco = new string[dT.Rows.Count + 1];
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
                            arrIco[k++] = "Priamo riadené rozpočtové organizácie";
                            arrIco[k++] = "Kapitola MŠVVaŠ SR spolu";
                        }
                        else if (i > 0 && i < 4 && k > 1)
                        {
                            arrIco[k++] = "Kapitola MŠVVaŠ SR spolu";
                        }
                        else if (i >= 4 && k > 1)
                        {
                            arrIco[k++] = "Príspevkové organizácie spolu";
                        }
                        else if (i > 0 && k == 1)
                            jediny = true;



                        string vynimky;
                        foreach (string helpString in arrIco) // Loop over the rows.
                        {
                            vynimky = vynimkyMRZ(connectionstring);
                            dT = new DataTable();

                            SqlCommand vydavb = new SqlCommand("Select SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Ico LIKE @Ico AND Zdroj LIKE @Zdroj AND KalendarnyDen LIKE @Datum AND HlavnaKategoria IN ('200','300','600','700') AND SyntetickyaleboFiktivny LIKE @Synteticky AND SUBSTRING(Kategoria, 1, 1) LIKE '6' " + vynimky, Con);
                            SqlCommand vydavk = new SqlCommand("Select SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Ico LIKE @Ico AND Zdroj LIKE @Zdroj AND KalendarnyDen LIKE @Datum AND HlavnaKategoria IN ('200','300','600','700') AND SyntetickyaleboFiktivny LIKE @Synteticky AND SUBSTRING(Kategoria, 1, 1) LIKE '7' " + vynimky, Con);
                            vydavb.Parameters.AddWithValue("@Datum", datum);
                            vydavk.Parameters.AddWithValue("@Datum", datum);
                            
                            vydavb.Parameters.AddWithValue("@Zdroj", zdrojeList[i][j]);
                            vydavk.Parameters.AddWithValue("@Zdroj", zdrojeList[i][j]);

                            if (i < 4)
                            {
                                vydavb.Parameters.AddWithValue("@Synteticky", typ);
                                vydavk.Parameters.AddWithValue("@Synteticky", typ);
                            }
                            else
                            {
                                vydavb.Parameters.AddWithValue("@Synteticky", "VYR");
                                vydavk.Parameters.AddWithValue("@Synteticky", "VYR");
                            }

                            if (helpString == "Kapitola MŠVVaŠ SR spolu" || helpString == "Priamo riadené rozpočtové organizácie" || helpString == "Príspevkové organizácie spolu")
                            {
                                vydavb.Parameters.AddWithValue("@Ico", "%");
                                vydavk.Parameters.AddWithValue("@Ico", "%");
                            }
                            else
                            {
                                vydavb.Parameters.AddWithValue("@Ico", helpString);
                                vydavk.Parameters.AddWithValue("@Ico", helpString);
                            }

                            DataTable vydavb_table = new DataTable();
                            DataTable vydavk_table = new DataTable();
                            using (SqlDataAdapter vydavb_adapter = new SqlDataAdapter(vydavb))
                            using (SqlDataAdapter vydavk_adapter = new SqlDataAdapter(vydavk))
                            {
                                vydavb_adapter.Fill(vydavb_table);
                                vydavk_adapter.Fill(vydavk_table);
                                vydavb.ExecuteNonQuery();
                                vydavk.ExecuteNonQuery();
                            }

                            int comparison = 0;
                            if ((comparison = helpString.CompareTo(MSVSico)) == 0)
                            {
                                using (SqlDataAdapter vydavb_adapter = new SqlDataAdapter(vydavb))
                                using (SqlDataAdapter vydavk_adapter = new SqlDataAdapter(vydavk))
                                {
                                    vydavb_adapter.Fill(dtOPROb);
                                    vydavk_adapter.Fill(dtOPROk);
                                    vydavb.ExecuteNonQuery();
                                    vydavk.ExecuteNonQuery();
                                }
                            }

                            //k = 0;
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

                            if (((helpString == MSVSico || helpString == "Priamo riadené rozpočtové organizácie" || helpString == "Kapitola MŠVVaŠ SR spolu" || helpString == "Príspevkové organizácie spolu") && i == 0) || jediny || ((helpString == "Kapitola MŠVVaŠ SR spolu" || helpString == "Príspevkové organizácie spolu") && i > 0))
                            {
                                range = worksheet.get_Range(left_char.ToString() + start, right_char.ToString() + start);
                                range.Font.Bold = true;
                            }

                            if ((int)vydavb_table.Rows[0][3] > 0 || (int)vydavk_table.Rows[0][3] > 0)
                            {
                                worksheet.Cells[start, 1] = nazov_org;

                                double[] columns = new double[9];



                                foreach (DataRow row in vydavb_table.Rows)
                                {
                                    for (int j2 = 0; j2 < 3; j2++)
                                    {
                                        double value = 0;

                                        if (row[j2].ToString() != "")
                                        {
                                            if (helpString == "Priamo riadené rozpočtové organizácie")
                                                value = (double)row[j2] - Double.Parse(dtOPROb.Rows[0][j2].ToString());
                                            else
                                                value = (double)row[j2];

                                            worksheet.Cells[start, 3 * j2 + 2] = value / typVystupov;
                                            columns[2 * j2] = value;
                                        }
                                        else
                                        {
                                            worksheet.Cells[start, 3 * j2 + 2] = 0;
                                            columns[2 * j2] = 0;
                                        }
                                    }
                                }

                                foreach (DataRow row in vydavk_table.Rows)
                                {

                                    for (int j2 = 0; j2 < 3; j2++)
                                    {
                                        double value = 0;

                                        if (row[j2].ToString() != "")
                                        {
                                            if (helpString == "Priamo riadené rozpočtové organizácie")
                                                value = (double)row[j2] - Double.Parse(dtOPROk.Rows[0][j2].ToString());
                                            else
                                                value = (double)row[j2];

                                            worksheet.Cells[start, 3 * j2 + 3] = value / typVystupov;
                                            columns[2 * j2 + 1] = value;
                                        }
                                        else
                                        {
                                            worksheet.Cells[start, 3 * j2 + 3] = 0;
                                            columns[2 * j2 + 1] = 0;
                                        }
                                    }

                                }



                                columns[6] = columns[0] + columns[1];
                                columns[7] = columns[2] + columns[3];
                                columns[8] = columns[4] + columns[5];

                                //if (valid_len == 2)
                                //{
                                //    sum_all[0] += columns[0];
                                //    sum_all[1] += columns[1];
                                //    sum_all[2] += columns[6];
                                //    sum_all[3] += columns[2];
                                //    sum_all[4] += columns[3];
                                //    sum_all[5] += columns[7];
                                //    sum_all[6] += columns[4];
                                //    sum_all[7] += columns[5];
                                //    sum_all[8] += columns[8];
                                //}



                                worksheet.Cells[start, 4] = columns[6] / typVystupov;
                                worksheet.Cells[start, 7] = columns[7] / typVystupov;
                                worksheet.Cells[start, 10] = columns[8] / typVystupov;

                                if (columns[0] != 0)
                                    worksheet.Cells[start, 11] = (columns[4] / columns[0]) * 100;
                                else
                                    worksheet.Cells[start, 11] = "0";
                                if (columns[1] != 0)
                                    worksheet.Cells[start, 12] = (columns[5] / columns[1]) * 100;
                                else
                                    worksheet.Cells[start, 12] = "0";
                                if (columns[6] != 0)
                                    worksheet.Cells[start, 13] = (columns[8] / columns[6]) * 100;
                                else
                                    worksheet.Cells[start, 13] = "0";

                                if (columns[2] != 0)
                                    worksheet.Cells[start, 14] = (columns[4] / columns[2]) * 100;
                                else
                                    worksheet.Cells[start, 14] = "0";
                                if (columns[3] != 0)
                                    worksheet.Cells[start, 15] = (columns[5] / columns[3]) * 100;
                                else
                                    worksheet.Cells[start, 15] = "0";
                                if (columns[7] != 0)
                                    worksheet.Cells[start, 16] = (columns[8] / columns[7]) * 100;
                                else
                                    worksheet.Cells[start, 16] = "0";

                                if (helpString != "Kapitola MŠVVaŠ SR spolu" && helpString != "Príspevkové organizácie spolu" && !(j == zdrojeList[i].Count() - 1 && helpString == arrIco[arrIco.Length-1]))
                                {
                                    bool ret;
                                    int last_border = border;
                                    int last_start = start;
                                    ret = increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 11, 2, 11);
                                    if (ret == true)
                                        selectString += "B" + last_border.ToString() + ":" + "J" + last_start.ToString() + System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator;
                                }
                                else if((helpString == "Kapitola MŠVVaŠ SR spolu" || helpString == "Príspevkové organizácie spolu") && i > 0 && !(j == zdrojeList[i].Count() - 1 && helpString == arrIco[arrIco.Length - 1]))
                                {
                                    bool ret;
                                    int last_border = border;
                                    int last_start = start;
                                    ret = increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 11, 2, 11);
                                    if (ret == true)
                                        selectString += "B" + last_border.ToString() + ":" + "J" + last_start.ToString() + System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator;
                                }
                            }
                            for (char c = left_char; c <= (char)((int)right_char); c++)//tenke oramovanie od border po start
                            {
                                borderRange = worksheet.get_Range(c.ToString() + border, c.ToString() + start);
                                borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                                //ZAOKRUHLOVANIE
                                //CISLA
                                if ((c >= left_char + 1 && c < left_char + 10))
                                {
                                    borderRange.NumberFormat = zaokruhlenieMena;
                                    borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                                }
                                //PERCENTA
                                else
                                {
                                    if (c > left_char + 9)
                                    {
                                        borderRange.NumberFormat = zaokruhleniePercenta;
                                        borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                                    }
                                }
                            }
                        }
                        
                    }
                    borderRange = worksheet.get_Range(left_char.ToString() + border, right_char.ToString() + start);//hrube po Celkovo
                    borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                    var culture = new CultureInfo("ru-RU");
                    worksheet.Cells[++start, 1] = "Dátum vytvorenia: " + time.ToString(culture);


                    selectString += "B" + border.ToString() + ":" + "J" + start.ToString();

                    //zvacsenie iba stlpcov kde ukazuje vacsie cisla ako ########
                    Excel.Range selectedRange = worksheet.Range[selectString].Columns["A:I"];
                    selectedRange.Columns.AutoFit();
                    foreach (Excel.Range column in selectedRange.Columns)
                    {
                        if (column.ColumnWidth < 10.86)//sirka stlpca tak jak ju ukazuje excel
                        column.ColumnWidth = 10.86;
                    }

                    //for (char col = 'A'; col <= 'H'; ++col)
                    //{
                    //    worksheet.Range[selectString].Columns[/*"A:I"*/col + ":" + (char)(col + 1)].AutoFit();
                    //    if (worksheet.Range[selectString].Columns[/*"A:I"*/col + ":" + (char)(col + 1)].Width > 5000)
                    //        //if (worksheet.Range[selectString].Columns[/*"A:I"*/col + ":" + (char)(col + 1)].WrapText != null)
                    //        worksheet.Range[selectString].Columns[/*"A:I"*/col + ":" + (char)(col + 1)].AutoFit();
                    //}
                }
                workbook.Save();
                if (priamaTlac)
                    PrintMyExcelFile(workbook);
                Kontrola.release_excel(range, worksheet, workbook, app);
            }
        }


        public void spravTab_10_10a_upravene(string typ, string MSVSico, string datum, int typVystupov, string zaokruhlenieMena, string zaokruhleniePercenta, char left_char, int left_num, char right_char, int right_num, int count_for_page, int strana_rel_pos, DateTime time, bool priamaTlac)
        {
            string nameTabulka = "Tab_10_10a";

            string name = "UCET_";
            name = name + nameTabulka + ".xls";

            string name2 = "UCET_" + nameTabulka + "_upravene.xls";


            string kvartal = vytvorAdresare(datum);
            System.IO.File.Copy("Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name, "Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name2, true);
            Excel.Application app = new Excel.Application();
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            Excel.Workbook workbook = app.Workbooks.Open(path + "\\" + "bin\\Debug\\Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name2);
            Excel._Worksheet worksheet = workbook.Sheets[1];
            Excel.Range range2 = worksheet.UsedRange;
            

            for (int i = 0; i < workbook.Sheets.Count; i++)
            {
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[i+1];

                //range2 = worksheet.get_Range("K1", "M1");
                //range2.EntireColumn.Delete(Excel.XlDirection.xlToLeft);

                ((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[10, 1]).EntireColumn.Delete(null);

                //range2 = worksheet.get_Range("K1", Missing.Value);
                //range2.EntireColumn.Delete(Missing.Value);

                //range2 = worksheet.get_Range("K1", Missing.Value);
                //range2.EntireColumn.Delete(Missing.Value);

                ////range2 = (Excel.Range)worksheet.get_Range("K1", "M1");
                ////range2.EntireColumn.Delete(Missing.Value);
                ////System.Runtime.InteropServices.Marshal.ReleaseComObject(range2);

                //range2 = (Excel.Range)worksheet.Application.Worksheets.get_Item(i + 1).Columns[11, Type.Missing];
                //range2.Select();
                //range2.Delete(Excel.XlDirection.xlToLeft);

                //range2 = (Excel.Range)worksheet.Application.Columns[11, Type.Missing];
                //range2.Select();
                //range2.Delete(Excel.XlDirection.xlToLeft);

                //range2 = (Excel.Range)worksheet.Application.Columns[11, Type.Missing];
                //range2.Select();
                //range2.Delete(Excel.XlDirection.xlToLeft);

                //if (i != workbook.Sheets.Count - 1)
                //{
                //    System.Runtime.InteropServices.Marshal.ReleaseComObject(range2);
                //    System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                //}


            }


            //range2 = (Excel.Range)worksheet.get_Range("K1", "M1");
            //range2.EntireColumn.Delete(Missing.Value);
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(range2);

            //range2 = (Excel.Range)worksheet.get_Range("K1", "M1");
            //range2.EntireColumn.Delete(Missing.Value);
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(range2);

            workbook.Save();
            if (priamaTlac)
                PrintMyExcelFile(workbook);
            Kontrola.release_excel(range2, worksheet, workbook, app);
        }


        //public void spravTab_11_12_13_14_16_17_upravene(string typ, string MSVSico, string datum, int typVystupov, string zaokruhlenieMena, string zaokruhleniePercenta, char left_char, int left_num, char right_char, int right_num, int count_for_page, int strana_rel_pos, DateTime time, bool priamaTlac)
        //{
        //    //16,17 su prispevkove

        //    string connectionstring = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        //    SqlCommand com = null;
        //    //
        //    //
        //    //PODMIENKA VYPNUT OPRO AK ROBI OSTATNE ORGANIZACIE
        //    //
        //    //
        //    bool opro = true;


        //    string[][] text_hlavicka = new string[6][];
        //    text_hlavicka[0] = new string[]
        //    {
        //        "Schválený rozpočet, upravený rozpočet a čerpanie výdavkov kapitoly školstva podľa okruhov a organizácií","za zdroj 111 - Rozpočtové prostriedky kapitoly ","k "
        //    };
        //    text_hlavicka[1] = new string[]
        //    {
        //        "Schválený rozpočet, upravený rozpočet a čerpanie výdavkov kapitoly školstva podľa okruhov a organizácií","a za  programové obdobie 2014-2020 - kódy zdroja 1AA1, 1AA2, 1AA3, 1AC1, 1AC2,  1AC3","k "
        //    };
        //    text_hlavicka[2] = new string[]
        //    {
        //        "Schválený rozpočet, upravený rozpočet a čerpanie výdavkov kapitoly školstva podľa okruhov a organizácií  za zdroje z predchádzajúcich rokov","a za  programové obdobie 2014-2020 - kódy zdroja 3AA1, 3AA2, 3AA3,  3AC1, 3AC2","k "
        //    };
        //    text_hlavicka[3] = new string[]
        //    {
        //        "Schválený rozpočet, upravený rozpočet a čerpanie výdavkov kapitoly školstva podľa okruhov a organizácií","za ostatné rozpočtové zdroje 131H ","k "
        //    };
        //    text_hlavicka[4] = new string[]
        //    {
        //        "Schválený rozpočet, upravený rozpočet a čerpanie výdavkov príspevkových organizácií za zdroje z predchádzajúcich rokov","a za programové obdobie 2014-2020  - kódy zdroja 3AA1, 3AA2, 3AA3, 3AC1, 3AC2","k "
        //    };
        //    text_hlavicka[5] = new string[]
        //    {
        //        "Schválený rozpočet, upravený rozpočet a čerpanie výdavkov príspevkových organizácií", "a za zdroje z predchádzajúcich rokov - kódy zdroja  131G, 131H", "k "
        //    };



        //    string[][] zdrojeList = new string[6][];
        //    zdrojeList[0] = new string[]
        //    {
        //        "111"
        //    };
        //    zdrojeList[1] = new string[]
        //    {
        //        "1AA1", "1AA2", "1AA3", "1AC1", "1AC2",  "1AC3",
        //    };
        //    zdrojeList[2] = new string[]
        //    {
        //        "3AA1", "3AA2", "3AA3",  "3AC1", "3AC2"
        //    };
        //    zdrojeList[3] = new string[]
        //    {
        //        "131H","131J","131K","131L","11H","11UA","1P01","1P02","3P02","3P01"
        //    };
        //    zdrojeList[4] = new string[]
        //    {
        //        "3AA1", "3AA2", "3AA3",  "3AC1", "3AC2"
        //    };
        //    zdrojeList[5] = new string[]
        //    {
        //        "131G", "131H"
        //    };

        //    DataTable dT = new DataTable();
        //    DataTable dtOPROb = new DataTable();
        //    DataTable dtOPROk = new DataTable();
        //    using (var Con = new SqlConnection(connectionstring))
        //    {
        //        Con.Open();

        //        string nameTabulka = "Tab_11_12_13_14_16_17";

        //        string name = "RZBT";
        //        name = name + nameTabulka + ".xls";


        //        string kvartal = vytvorAdresare(datum);
        //        System.IO.File.Copy("..\\..\\Hlav\\ZU\\hlpr5cv3.xls", "Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name, true);
        //        Excel.Application app = new Excel.Application();
        //        string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        //        Excel.Workbook workbook = app.Workbooks.Open(path + "\\" + "bin\\Debug\\Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name);
        //        Excel._Worksheet worksheet = workbook.Sheets[1];
        //        Excel.Range range = worksheet.UsedRange;
        //        Excel.Range borderRange;
        //        Excel._Worksheet worksheetFirst = workbook.Sheets[1];

        //        //worksheet.Cells[5, 1] = "za zdroj 111 - Rozpočtové prostriedky kapitoly k " + datum + " (€)";
        //        worksheet.Cells[7, 2] = worksheet.Cells[7, 2].Value2.ToString() + " " + datum.Substring(6);
        //        worksheet.Cells[7, 5] = worksheet.Cells[7, 5].Value2.ToString() + " " + datum;
        //        worksheet.Cells[7, 8] = worksheet.Cells[7, 8].Value2.ToString() + " " + datum;


        //        int jump = 0;
        //        for (int i = 0; i < 6; i++)
        //        {


        //            string selectString = "";
        //            //VYTVARANIE NOVEHO ZOSITU PLUS KOPIROVANIE HLAVICKY 
        //            //VACSIE AKO 0 LEBO PRVY OTVARAM UZ PRED CYKLOM ABY SOM NASTAVIL HLAVICKU KTORU POTOM KOPIRUJEM
        //            if (i > 0)
        //            {
        //                opro = false;
        //                workbook.Sheets[1].Copy(Type.Missing, workbook.Sheets[workbook.Sheets.Count]);
        //                worksheet = workbook.Sheets[workbook.Sheets.Count];
        //                int help = worksheet.Rows.Count;
        //                range = worksheet.get_Range("A12", "P" + help);
        //                range.Clear();
        //            }

        //            int rowCount = range.Rows.Count;
        //            int start = right_num + 1;
        //            int border = start;
        //            int strana = 2;

        //            if (i > 3)
        //                jump = 1;
        //            string worksheet_name = "Tab_" + (11 + i + jump);
        //            worksheet.Cells[1, 15] = "Tabuľka č. " + (11 + i + jump);

        //            worksheet.Cells[3, 1] = text_hlavicka[i][0];
        //            worksheet.Cells[4, 1] = text_hlavicka[i][1];
        //            worksheet.Cells[5, 1] = text_hlavicka[i][2] + datum + " (€)";


        //            //worksheet.Cells[5, 1] = "za zdroj 111 - Rozpočtové prostriedky kapitoly k " + datum + " (€)";

        //            //tu este poriesit texty

        //            worksheet.Name = worksheet_name;
        //            worksheet.Columns[1].WrapText = true;

        //            for (int j = 0; j < zdrojeList[i].Count(); j++)
        //            {
        //                bool jediny = false;
        //                range = worksheet.get_Range("A" + start, "P" + start);
        //                range.Merge();
        //                range.Font.Bold = true;

        //                SqlCommand get_name_zdroj = new SqlCommand("SELECT Text FROM ciselnikZdroje WHERE Zdroj = '" + zdrojeList[i][j] + "'", Con);
        //                DataTable get_name_zdroj_table = new DataTable();
        //                using (SqlDataAdapter get_name_zdroj_adapter = new SqlDataAdapter(get_name_zdroj))
        //                {
        //                    get_name_zdroj_adapter.Fill(get_name_zdroj_table);
        //                    get_name_zdroj.ExecuteNonQuery();
        //                }

        //                worksheet.Cells[start, 1] = "Zdroj " + zdrojeList[i][j] + " - " + (string)get_name_zdroj_table.Rows[0][0];
        //                increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 8, 11);

        //                com = new SqlCommand("SELECT DISTINCT tv.Ico,ci.CelyNazov collate Slovak_CI_AS FROM TableVstup tv JOIN ciselnikIco ci ON tv.Ico=ci.Ico WHERE tv.SyntetickyaleboFiktivny LIKE @Typ AND Zdroj LIKE @Zdroj AND KalendarnyDen LIKE @Datum ORDER BY ci.CelyNazov collate Slovak_CI_AS", Con);
        //                com.Parameters.AddWithValue("@Zdroj", zdrojeList[i][j]);
        //                //com.Parameters.AddWithValue("@typ", typ);
        //                com.Parameters.AddWithValue("@Datum", datum);

        //                if (i < 4)
        //                    com.Parameters.AddWithValue("@Typ", typ);
        //                else
        //                    com.Parameters.AddWithValue("@Typ", "VYR");

        //                using (SqlDataAdapter da = new SqlDataAdapter(com))
        //                {
        //                    da.Fill(dT);
        //                    com.ExecuteNonQuery();
        //                }

        //                string[] arrIco;
        //                int k = 0;
        //                if (opro == true)
        //                {
        //                    arrIco = new string[dT.Rows.Count + 2];
        //                    arrIco[0] = MSVSico;
        //                    k++;
        //                }
        //                else if (i > 0 && dT.Rows.Count > 1)
        //                    arrIco = new string[dT.Rows.Count + 1];
        //                else
        //                    arrIco = new string[dT.Rows.Count];

        //                foreach (DataRow row in dT.Rows) // Loop over the rows.
        //                {
        //                    if ((string)row[0] != MSVSico || opro == false)
        //                    {
        //                        arrIco[k] = (string)row[0];
        //                        k++;
        //                    }
        //                }

        //                if (opro == true)
        //                {
        //                    arrIco[k++] = "Ostatné priamo riadené organizácie";
        //                    arrIco[k++] = "Kapitola MŠVVaŠ SR spolu";
        //                }
        //                else if (i > 0 && i < 4 && k > 1)
        //                {
        //                    arrIco[k++] = "Kapitola MŠVVaŠ SR spolu";
        //                }
        //                else if (i >= 4 && k > 1)
        //                {
        //                    arrIco[k++] = "Príspevkové organizácie spolu";
        //                }
        //                else if (i > 0 && k == 1)
        //                    jediny = true;



        //                string vynimky;
        //                foreach (string helpString in arrIco) // Loop over the rows.
        //                {
        //                    vynimky = vynimkyMRZ(connectionstring);
        //                    dT = new DataTable();

        //                    SqlCommand vydavb = new SqlCommand("Select SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Ico LIKE @Ico AND Zdroj LIKE @Zdroj AND KalendarnyDen LIKE @Datum AND HlavnaKategoria IN ('200','300','600','700') AND SyntetickyaleboFiktivny LIKE @Synteticky AND SUBSTRING(Kategoria, 1, 1) LIKE '6' " + vynimky, Con);
        //                    SqlCommand vydavk = new SqlCommand("Select SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Ico LIKE @Ico AND Zdroj LIKE @Zdroj AND KalendarnyDen LIKE @Datum AND HlavnaKategoria IN ('200','300','600','700') AND SyntetickyaleboFiktivny LIKE @Synteticky AND SUBSTRING(Kategoria, 1, 1) LIKE '7' " + vynimky, Con);
        //                    vydavb.Parameters.AddWithValue("@Datum", datum);
        //                    vydavk.Parameters.AddWithValue("@Datum", datum);

        //                    vydavb.Parameters.AddWithValue("@Zdroj", zdrojeList[i][j]);
        //                    vydavk.Parameters.AddWithValue("@Zdroj", zdrojeList[i][j]);

        //                    if (i < 4)
        //                    {
        //                        vydavb.Parameters.AddWithValue("@Synteticky", typ);
        //                        vydavk.Parameters.AddWithValue("@Synteticky", typ);
        //                    }
        //                    else
        //                    {
        //                        vydavb.Parameters.AddWithValue("@Synteticky", "VYR");
        //                        vydavk.Parameters.AddWithValue("@Synteticky", "VYR");
        //                    }

        //                    if (helpString == "Kapitola MŠVVaŠ SR spolu" || helpString == "Ostatné priamo riadené organizácie" || helpString == "Príspevkové organizácie spolu")
        //                    {
        //                        vydavb.Parameters.AddWithValue("@Ico", "%");
        //                        vydavk.Parameters.AddWithValue("@Ico", "%");
        //                    }
        //                    else
        //                    {
        //                        vydavb.Parameters.AddWithValue("@Ico", helpString);
        //                        vydavk.Parameters.AddWithValue("@Ico", helpString);
        //                    }

        //                    DataTable vydavb_table = new DataTable();
        //                    DataTable vydavk_table = new DataTable();
        //                    using (SqlDataAdapter vydavb_adapter = new SqlDataAdapter(vydavb))
        //                    using (SqlDataAdapter vydavk_adapter = new SqlDataAdapter(vydavk))
        //                    {
        //                        vydavb_adapter.Fill(vydavb_table);
        //                        vydavk_adapter.Fill(vydavk_table);
        //                        vydavb.ExecuteNonQuery();
        //                        vydavk.ExecuteNonQuery();
        //                    }

        //                    int comparison = 0;
        //                    if ((comparison = helpString.CompareTo(MSVSico)) == 0)
        //                    {
        //                        using (SqlDataAdapter vydavb_adapter = new SqlDataAdapter(vydavb))
        //                        using (SqlDataAdapter vydavk_adapter = new SqlDataAdapter(vydavk))
        //                        {
        //                            vydavb_adapter.Fill(dtOPROb);
        //                            vydavk_adapter.Fill(dtOPROk);
        //                            vydavb.ExecuteNonQuery();
        //                            vydavk.ExecuteNonQuery();
        //                        }
        //                    }

        //                    //k = 0;
        //                    //zavolanie mena podla ica
        //                    String nazov_org = "";
        //                    String skratka_org = "";

        //                    SqlCommand get_name = new SqlCommand("SELECT CelyNazov, SkratenyNazov from ciselnikIco where Ico = '" + helpString + "'", Con);
        //                    DataTable get_name_table = new DataTable();
        //                    using (SqlDataAdapter get_name_adapter = new SqlDataAdapter(get_name))
        //                    {
        //                        get_name_adapter.Fill(get_name_table);
        //                        get_name.ExecuteNonQuery();
        //                    }
        //                    if (get_name_table.Rows.Count != 0)
        //                    {
        //                        nazov_org = (string)get_name_table.Rows[0][0];
        //                        skratka_org = (string)get_name_table.Rows[0][1];
        //                    }
        //                    else
        //                    {
        //                        nazov_org = helpString;
        //                        skratka_org = helpString;
        //                    }

        //                    if (((helpString == MSVSico || helpString == "Ostatné priamo riadené organizácie" || helpString == "Kapitola MŠVVaŠ SR spolu" || helpString == "Príspevkové organizácie spolu") && i == 0) || jediny || ((helpString == "Kapitola MŠVVaŠ SR spolu" || helpString == "Príspevkové organizácie spolu") && i > 0))
        //                    {
        //                        range = worksheet.get_Range(left_char.ToString() + start, right_char.ToString() + start);
        //                        range.Font.Bold = true;
        //                    }

        //                    if ((int)vydavb_table.Rows[0][3] > 0 || (int)vydavk_table.Rows[0][3] > 0)
        //                    {
        //                        worksheet.Cells[start, 1] = nazov_org;

        //                        double[] columns = new double[9];



        //                        foreach (DataRow row in vydavb_table.Rows)
        //                        {
        //                            for (int j2 = 0; j2 < 3; j2++)
        //                            {
        //                                double value = 0;

        //                                if (row[j2].ToString() != "")
        //                                {
        //                                    if (helpString == "Ostatné priamo riadené organizácie")
        //                                        value = (double)row[j2] - Double.Parse(dtOPROb.Rows[0][j2].ToString());
        //                                    else
        //                                        value = (double)row[j2];

        //                                    worksheet.Cells[start, 3 * j2 + 2] = value / typVystupov;
        //                                    columns[2 * j2] = value;
        //                                }
        //                                else
        //                                {
        //                                    worksheet.Cells[start, 3 * j2 + 2] = 0;
        //                                    columns[2 * j2] = 0;
        //                                }
        //                            }
        //                        }

        //                        foreach (DataRow row in vydavk_table.Rows)
        //                        {

        //                            for (int j2 = 0; j2 < 3; j2++)
        //                            {
        //                                double value = 0;

        //                                if (row[j2].ToString() != "")
        //                                {
        //                                    if (helpString == "Ostatné priamo riadené organizácie")
        //                                        value = (double)row[j2] - Double.Parse(dtOPROk.Rows[0][j2].ToString());
        //                                    else
        //                                        value = (double)row[j2];

        //                                    worksheet.Cells[start, 3 * j2 + 3] = value / typVystupov;
        //                                    columns[2 * j2 + 1] = value;
        //                                }
        //                                else
        //                                {
        //                                    worksheet.Cells[start, 3 * j2 + 3] = 0;
        //                                    columns[2 * j2 + 1] = 0;
        //                                }
        //                            }

        //                        }



        //                        columns[6] = columns[0] + columns[1];
        //                        columns[7] = columns[2] + columns[3];
        //                        columns[8] = columns[4] + columns[5];

        //                        //if (valid_len == 2)
        //                        //{
        //                        //    sum_all[0] += columns[0];
        //                        //    sum_all[1] += columns[1];
        //                        //    sum_all[2] += columns[6];
        //                        //    sum_all[3] += columns[2];
        //                        //    sum_all[4] += columns[3];
        //                        //    sum_all[5] += columns[7];
        //                        //    sum_all[6] += columns[4];
        //                        //    sum_all[7] += columns[5];
        //                        //    sum_all[8] += columns[8];
        //                        //}



        //                        worksheet.Cells[start, 4] = columns[6] / typVystupov;
        //                        worksheet.Cells[start, 7] = columns[7] / typVystupov;
        //                        worksheet.Cells[start, 10] = columns[8] / typVystupov;

        //                        if (columns[0] != 0)
        //                            worksheet.Cells[start, 11] = (columns[4] / columns[0]) * 100;
        //                        else
        //                            worksheet.Cells[start, 11] = "0";
        //                        if (columns[1] != 0)
        //                            worksheet.Cells[start, 12] = (columns[5] / columns[1]) * 100;
        //                        else
        //                            worksheet.Cells[start, 12] = "0";
        //                        if (columns[6] != 0)
        //                            worksheet.Cells[start, 13] = (columns[8] / columns[6]) * 100;
        //                        else
        //                            worksheet.Cells[start, 13] = "0";

        //                        if (columns[2] != 0)
        //                            worksheet.Cells[start, 14] = (columns[4] / columns[2]) * 100;
        //                        else
        //                            worksheet.Cells[start, 14] = "0";
        //                        if (columns[3] != 0)
        //                            worksheet.Cells[start, 15] = (columns[5] / columns[3]) * 100;
        //                        else
        //                            worksheet.Cells[start, 15] = "0";
        //                        if (columns[7] != 0)
        //                            worksheet.Cells[start, 16] = (columns[8] / columns[7]) * 100;
        //                        else
        //                            worksheet.Cells[start, 16] = "0";

        //                        if (helpString != "Kapitola MŠVVaŠ SR spolu" && helpString != "Príspevkové organizácie spolu" && !(j == zdrojeList[i].Count() - 1 && helpString == arrIco[arrIco.Length - 1]))
        //                        {
        //                            bool ret;
        //                            int last_border = border;
        //                            int last_start = start;
        //                            ret = increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 11, 2, 11);
        //                            if (ret == true)
        //                                selectString += "B" + last_border.ToString() + ":" + "J" + last_start.ToString() + System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator;
        //                        }
        //                        else if ((helpString == "Kapitola MŠVVaŠ SR spolu" || helpString == "Príspevkové organizácie spolu") && i > 0 && !(j == zdrojeList[i].Count() - 1 && helpString == arrIco[arrIco.Length - 1]))
        //                        {
        //                            bool ret;
        //                            int last_border = border;
        //                            int last_start = start;
        //                            ret = increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 11, 2, 11);
        //                            if (ret == true)
        //                                selectString += "B" + last_border.ToString() + ":" + "J" + last_start.ToString() + System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator;
        //                        }
        //                    }
        //                    for (char c = left_char; c <= (char)((int)right_char); c++)//tenke oramovanie od border po start
        //                    {
        //                        borderRange = worksheet.get_Range(c.ToString() + border, c.ToString() + start);
        //                        borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
        //                        //ZAOKRUHLOVANIE
        //                        //CISLA
        //                        if ((c >= left_char + 1 && c < left_char + 10))
        //                        {
        //                            borderRange.NumberFormat = zaokruhlenieMena;
        //                            borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
        //                        }
        //                        //PERCENTA
        //                        else
        //                        {
        //                            if (c > left_char + 9)
        //                            {
        //                                borderRange.NumberFormat = zaokruhleniePercenta;
        //                                borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
        //                            }
        //                        }
        //                    }
        //                }

        //            }
        //            borderRange = worksheet.get_Range(left_char.ToString() + border, right_char.ToString() + start);//hrube po Celkovo
        //            borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
        //            var culture = new CultureInfo("ru-RU");
        //            worksheet.Cells[++start, 1] = "Dátum vytvorenia: " + time.ToString(culture);


        //            selectString += "B" + border.ToString() + ":" + "J" + start.ToString();

        //            //zvacsenie iba stlpcov kde ukazuje vacsie cisla ako ########
        //            Excel.Range selectedRange = worksheet.Range[selectString].Columns["A:I"];
        //            selectedRange.Columns.AutoFit();
        //            foreach (Excel.Range column in selectedRange.Columns)
        //            {
        //                if (column.ColumnWidth < 10.86)//sirka stlpca tak jak ju ukazuje excel
        //                    column.ColumnWidth = 10.86;
        //            }

        //            //for (char col = 'A'; col <= 'H'; ++col)
        //            //{
        //            //    worksheet.Range[selectString].Columns[/*"A:I"*/col + ":" + (char)(col + 1)].AutoFit();
        //            //    if (worksheet.Range[selectString].Columns[/*"A:I"*/col + ":" + (char)(col + 1)].Width > 5000)
        //            //        //if (worksheet.Range[selectString].Columns[/*"A:I"*/col + ":" + (char)(col + 1)].WrapText != null)
        //            //        worksheet.Range[selectString].Columns[/*"A:I"*/col + ":" + (char)(col + 1)].AutoFit();
        //            //}
        //        }
        //        workbook.Save();
        //        if (priamaTlac)
        //            PrintMyExcelFile(workbook);
        //        Kontrola.release_excel(range, worksheet, workbook, app);
        //    }
        //}

    }
}
