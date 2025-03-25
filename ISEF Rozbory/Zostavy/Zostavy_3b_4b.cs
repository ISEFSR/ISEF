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
    class Zostavy_3b_4b : Zostavy
    {
        public void spravZostavy(string typ, string MSVSico, int prijmyVydavkyVeda, Boolean lenOkruhy, string datum, int typVystupov, string zaokruhlenieMena, string zaokruhleniePercenta, char left_char, int left_num, char right_char, int right_num, int count_for_page, int strana_rel_pos, DateTime time, bool priamaTlac)//1 prijmy 2 vydavky
        {
            string connectionstring = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            string[] synteticke = najdiSynteticke(connectionstring);
            string ekonomickaKlasifikacia = zistiEkonomickuKlasifikaciu(prijmyVydavkyVeda,1, connectionstring);
            SqlCommand com = null;

            bool opro = false;
            if (typ == synteticke[0] || typ == synteticke[2] || typ == synteticke[1] || typ == synteticke[3])
                opro = false;
            else
                opro = true;
            
            //AK MA ROBIT VEDU TAK Z DATABAZY VYTIAHNE VYNIMKY VEDY
            string veda = "";
            if (prijmyVydavkyVeda == 3 || prijmyVydavkyVeda == 4)
                veda = vynimkyVedy(prijmyVydavkyVeda, connectionstring);

            DataTable dT = new DataTable();
            DataTable dtOPRO = new DataTable();
            DataTable dtOPRO2 = new DataTable();
            using (var Con = new SqlConnection(connectionstring))
            {
                int typSynteticky = Array.IndexOf(synteticke, typ);
                string nameTabulka = zistiNazovZakladne(typSynteticky, prijmyVydavkyVeda, lenOkruhy);

                string name = "RZBT";
                //AK MRZ PRIDA M DO NAZVU
                if (typ == synteticke[2] || typ == synteticke[3] || typ == synteticke[6] || typ == synteticke[7])
                    name = name + "M";
                name = name + nameTabulka + ".xls";
                /*if (opro == false)
                    name = "zostava_PO_" + typ + "_" + prijmyVydavkyVeda + ".xls";
                else
                    name = "zostava_RO_" + typ + "_" + prijmyVydavkyVeda + ".xls";*/

                string kvartal = vytvorAdresare(datum);

                System.IO.File.Copy("..\\..\\Hlav\\RZB\\hl345cv3.xls", "Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name, true);
                Excel.Application app = new Excel.Application();
                string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                Excel.Workbook workbook = app.Workbooks.Open(path + "\\" + "bin\\Debug\\Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name);
                Excel._Worksheet worksheet = workbook.Sheets[1];//pre citanie excelu
                Excel.Range range = worksheet.UsedRange;//pre citanie excelu
                Excel.Range borderRange;
                int rowCount = range.Rows.Count;//pre citanie excelu
                int start = right_num + 1;
                worksheet.Cells[start - strana_rel_pos, (int)right_char - 65 + 1] = "Strana: 1";
                int border;
                int strana = 2;

                string organizacia = "";
                string organizacia_sklon = "";
                string plnenie = "";
                if (typ == synteticke[0] || typ == synteticke[2] || typ == synteticke[1] || typ == synteticke[3])
                {
                    organizacia = "Príspevkové organizácie";
                    organizacia_sklon = "príspevkových organizáciách";
                }
                else
                {
                    organizacia = "Rozpočtové organizácie";
                    organizacia_sklon = "rozpočtových organizáciách";

                    if (prijmyVydavkyVeda == 3)
                        organizacia_sklon += " za vedu";
                    if (prijmyVydavkyVeda == 4)
                        organizacia_sklon += " okrem vedy";
                }
                
                if (prijmyVydavkyVeda == 1)
                {
                    plnenie = "Plnenie príjmov na ";
                    if (lenOkruhy == true)
                    {
                        organizacia_sklon = "";
                        plnenie = "Plnenie príjmov za rezort školstva";
                    }
                }
                else
                {
                    plnenie = "Výdavky školstva na ";
                    if (lenOkruhy == true)
                    {
                        organizacia_sklon = "";
                        plnenie = "Výdavky za rezort školstva";
                    }
                }

                string doplnkovyText = "";
                if (typ == synteticke[0] || typ == synteticke[2])
                    doplnkovyText = "Príjmy";
                else if (typ == synteticke[1] || typ == synteticke[3])
                    doplnkovyText = "Výdavky";
                else if (typ == synteticke[6] || typ == synteticke[7])
                    doplnkovyText = "Mimorozpočtové účty";

                if (typ == synteticke[2] || typ == synteticke[3])
                    doplnkovyText += " nerozpočtované";
                else if (typ == synteticke[0] || typ == synteticke[1])
                    doplnkovyText += " rozpočtované";

                //TEXTY
                worksheet.Cells[4, 1] = doplnkovyText;
                worksheet.Cells[5, 2] = datum;
                worksheet.Cells[5, 10] = nameTabulka;
                worksheet.Cells[1, 10] = organizacia;
                worksheet.Cells[2, 1] = plnenie + organizacia_sklon;
                if (typVystupov == 1)
                    worksheet.Cells[3, 1] = "(údaje sú v €)";
                else
                    worksheet.Cells[3, 1] = "(údaje sú v tisícoch €)";

                double[] sum_all = new double[5];
                Con.Open();

                int i = 0;
                string[] arrIco;
                if (lenOkruhy == false)
                {
                    if (typ != "MRP" && typ != "MRV")
                    {
                        com = new SqlCommand("SELECT DISTINCT tv.Ico,ci.CelyNazov collate Slovak_CI_AS FROM TableVstup tv JOIN ciselnikIco ci ON tv.Ico=ci.Ico WHERE tv.SyntetickyaleboFiktivny LIKE @typ AND KalendarnyDen LIKE @Datum " + veda + " ORDER BY ci.CelyNazov collate Slovak_CI_AS", Con);
                        com.Parameters.AddWithValue("@typ", typ);
                    }
                    else
                    {
                        com = new SqlCommand("SELECT DISTINCT tv.Ico,ci.CelyNazov collate Slovak_CI_AS FROM TableVstup tv JOIN ciselnikIco ci ON tv.Ico=ci.Ico WHERE SyntetickyaleboFiktivny NOT LIKE @Synteticky1 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky2 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky3 " + ekonomickaKlasifikacia + " AND KalendarnyDen LIKE @Datum", Con);
                        if (typ == "MRP")
                        {
                            com.Parameters.AddWithValue("@Synteticky1", synteticke[0]);
                            com.Parameters.AddWithValue("@Synteticky2", synteticke[2]);
                            com.Parameters.AddWithValue("@Synteticky3", synteticke[4]);
                        }
                        if (typ == "MRV")
                        {
                            com.Parameters.AddWithValue("@Synteticky1", synteticke[1]);
                            com.Parameters.AddWithValue("@Synteticky2", synteticke[3]);
                            com.Parameters.AddWithValue("@Synteticky3", synteticke[5]);
                        }
                    }
                    com.Parameters.AddWithValue("@Datum", datum);

                    using (SqlDataAdapter da = new SqlDataAdapter(com))
                    {
                        da.Fill(dT);
                        com.ExecuteNonQuery();
                    }

                    if (opro == true)
                        arrIco = new string[dT.Rows.Count + 2];
                    else
                        arrIco = new string[dT.Rows.Count + 1];
                    
                    foreach (DataRow row in dT.Rows) // Loop over the rows.
                    {
                        arrIco[i] = (string)row[0];
                        i++;

                    }
                    arrIco[i++] = "Celkovo";
                    if (opro == true)
                        arrIco[i++] = "OPRO - MŠVVaŠ SR";
                }
                else
                {
                    arrIco = new string[1];
                    arrIco[0] = "Ostatné priamo riadené organizácie + MŠVVaŠ SR (Vrátane PJ)";
                }
                
                string vynimky;
                SqlCommand com2 = null;
                DataTable dT2;


                foreach (string helpString in arrIco) // Loop over the rows.
                {
                    vynimky = vynimkyMRZ(connectionstring);
                    dT = new DataTable();
                    dT2 = new DataTable();
                    border = start;

                    if (typ != "MRP" && typ != "MRV")
                    {
                        com = new SqlCommand("SELECT Kategoria,SUM(SchvalenyRozpocet),SUM(RozpocetPoZmenach),SUM(Skutocnost) FROM TableVstup tv WHERE SyntetickyaleboFiktivny LIKE @typ AND KalendarnyDen LIKE @Datum " + veda + " AND Ico LIKE @Ico GROUP BY Kategoria ORDER BY Kategoria", Con);
                        com.Parameters.AddWithValue("@typ", typ);
                        com2 = new SqlCommand("SELECT Kategoria, SUM(Skutocnost) FROM TableVstup tv WHERE SyntetickyaleboFiktivny LIKE @typ AND KalendarnyDen LIKE @Datum AND Ico LIKE @Ico " + vynimky + " " + veda + " GROUP BY Kategoria ORDER BY Kategoria", Con);
                        com2.Parameters.AddWithValue("@typ", typ);
                    }
                    else
                    {
                        com = new SqlCommand("SELECT Kategoria,SUM(SchvalenyRozpocet),SUM(RozpocetPoZmenach),SUM(Skutocnost) FROM TableVstup tv WHERE SyntetickyaleboFiktivny NOT LIKE @Synteticky1 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky2 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky3 AND KalendarnyDen LIKE @Datum " + ekonomickaKlasifikacia + " AND Ico LIKE @Ico GROUP BY Kategoria ORDER BY Kategoria", Con);
                        com2 = new SqlCommand("SELECT Kategoria, SUM(Skutocnost) FROM TableVstup tv WHERE SyntetickyaleboFiktivny NOT LIKE @Synteticky1 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky2 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky3 AND KalendarnyDen LIKE @Datum " + ekonomickaKlasifikacia + " AND Ico LIKE @Ico " + vynimky + " GROUP BY Kategoria ORDER BY Kategoria", Con);

                        if (typ == "MRP")
                        {
                            com.Parameters.AddWithValue("@Synteticky1", synteticke[0]);
                            com.Parameters.AddWithValue("@Synteticky2", synteticke[2]);
                            com.Parameters.AddWithValue("@Synteticky3", synteticke[4]);

                            com2.Parameters.AddWithValue("@Synteticky1", synteticke[0]);
                            com2.Parameters.AddWithValue("@Synteticky2", synteticke[2]);
                            com2.Parameters.AddWithValue("@Synteticky3", synteticke[4]);
                            }

                        if (typ == "MRV")
                        {
                            //com = new SqlCommand("SELECT Kategoria,SUM(SchvalenyRozpocet),SUM(RozpocetPoZmenach),SUM(Skutocnost) FROM TableVstup WHERE SyntetickyaleboFiktivny NOT LIKE @Synteticky1 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky2 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky3 AND KalendarnyDen LIKE @Datum AND (Kategoria LIKE '6%' OR Kategoria LIKE '7%' OR Kategoria LIKE '8%' OR Kategoria LIKE '9%') AND Ico LIKE '" + helpString + "' GROUP BY Kategoria ORDER BY Kategoria", Con);
                            com.Parameters.AddWithValue("@Synteticky1", synteticke[1]);
                            com.Parameters.AddWithValue("@Synteticky2", synteticke[3]);
                            com.Parameters.AddWithValue("@Synteticky3", synteticke[5]);
                            com2.Parameters.AddWithValue("@Synteticky1", synteticke[1]);
                            com2.Parameters.AddWithValue("@Synteticky2", synteticke[3]);
                            com2.Parameters.AddWithValue("@Synteticky3", synteticke[5]);
                        }
                    }
                    com.Parameters.AddWithValue("@Datum", datum);
                    com2.Parameters.AddWithValue("@Datum", datum);

                    if (helpString == "Celkovo" || helpString == "OPRO - MŠVVaŠ SR" || helpString == "Ostatné priamo riadené organizácie + MŠVVaŠ SR (Vrátane PJ)")
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
                    if (helpString != "OPRO - MŠVVaŠ SR" && helpString != "Celkovo" && helpString != "Príspev. organizácie" && helpString != "Ostatné priamo riadené organizácie + MŠVVaŠ SR (Vrátane PJ)")
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
                        if (helpString == "Ostatné priamo riadené organizácie + MŠVVaŠ SR (Vrátane PJ)")
                            skratka_org = "OPRO";
                        else
                            skratka_org = helpString;
                    }
                    worksheet.Cells[start, 1] = nazov_org;


                    int local_category = Int32.Parse(dT.Rows[0][0].ToString()) / 100;
                    double[] LokSum = new double[5];
                    double[] mem6_64 = new double[5];
                    double[] sum_2367 = new double[5];
                    double[] sum_ico = new double[5];

                    string text_2367;
                    int div_2367;
                    if (prijmyVydavkyVeda == 1)
                    {
                        text_2367 = "2xx, 3xx";
                        div_2367 = 2;
                    }
                    else if (prijmyVydavkyVeda == 2 || prijmyVydavkyVeda == 3 || prijmyVydavkyVeda == 4)
                    {
                        text_2367 = "6xx, 7xx";
                        div_2367 = 6;
                    }
                    else
                    {
                        text_2367 = "";
                        div_2367 = 0;
                    }

                    //[0] - RS
                    //[1] - RU
                    //[2] - Skutocnost
                    //[3] - MRZ
                    //[4] - SKUT - MRZ

                    int counter = 0;
                    
                    foreach (DataRow row in dT.Rows) // Loop over the rows.
                    {
                        //workbook.Save();
                        comparison = 0;
                        if (helpString == "OPRO - MŠVVaŠ SR")
                        {
                            int test2 = dtOPRO.Rows.Count;

                            foreach (DataRow rowOpro in dtOPRO.Rows)//prve 3 miesta RS RU SKUT
                            {
                                string test1 = rowOpro[0].ToString();
                                if ((comparison = test1.CompareTo(row[0])) == 0)
                                {
                                    for (int l = 1; l < 4; l++)
                                    {
                                        row[l] = (Double.Parse(row[l].ToString()) - Double.Parse(rowOpro[l].ToString())).ToString();//tu odcitane
                                    }
                                }
                            }

                            foreach (DataRow rowOpro in dtOPRO2.Rows)//MRZ pre OPRO
                            {
                                string test1 = rowOpro[0].ToString();
                                if ((comparison = test1.CompareTo(row[0])) == 0)
                                {
                                    for (int l = 1; l < 2; l++)
                                    {
                                        dT2.Rows[i][l] = (Double.Parse(dT2.Rows[i][l].ToString()) - Double.Parse(rowOpro[l].ToString())).ToString();//tu odcitane
                                    }
                                }
                            }
                        }

                        int current_category = (Int32.Parse(row[0].ToString()) / 100);
                        if (current_category != local_category)
                        {
                            //VYPIS TU
                            if (typ == synteticke[5] && prijmyVydavkyVeda != 1)
                            {
                                //ZAPISANIE UDAJOV
                                for (int counterPrint = 0; counterPrint <= 4; counterPrint++)
                                {
                                    int helpPrint = counterPrint + 3;
                                    if (counterPrint > 2)
                                        helpPrint = helpPrint + 2;

                                    if (LokSum[counterPrint] - mem6_64[counterPrint] != 0)
                                        worksheet.Cells[start, helpPrint] = (LokSum[counterPrint] - mem6_64[counterPrint]) / typVystupov;
                                    else
                                        worksheet.Cells[start, helpPrint] = 0;
                                }
                                worksheet.Cells[start, 2].NumberFormat = "@";
                                worksheet.Cells[start, 2] = "6-64";

                                //ZAPISANIE PERCENT
                                if (LokSum[0] - mem6_64[0] == 0)
                                {
                                    worksheet.Cells[start, 6] = 0;
                                    worksheet.Cells[start, 10] = 0;
                                }
                                else
                                {
                                    worksheet.Cells[start, 6] = 100 * (LokSum[2] - mem6_64[2]) / (LokSum[0] - mem6_64[0]);
                                    worksheet.Cells[start, 10] = 100 * (LokSum[4] - mem6_64[4]) / (LokSum[0] - mem6_64[0]);
                                }

                                if (LokSum[1] - mem6_64[1] == 0)
                                {
                                    worksheet.Cells[start, 7] = 0;
                                    worksheet.Cells[start, 11] = 0;
                                }
                                else
                                {
                                    worksheet.Cells[start, 7] = 100 * (LokSum[2] - mem6_64[2]) / (LokSum[1] - mem6_64[1]);
                                    worksheet.Cells[start, 11] = 100 * (LokSum[4] - mem6_64[4]) / (LokSum[1] - mem6_64[1]);
                                }

                                mem6_64 = new double[5];
                                increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 8, 11);
                            }

                            //ZAPISANIE UDAJOV
                            for (int counterPrint = 0; counterPrint <= 4; counterPrint++)
                            {
                                int helpPrint = counterPrint + 3;
                                if (counterPrint > 2)
                                    helpPrint = helpPrint + 2;

                                if (LokSum[counterPrint] != 0)
                                    worksheet.Cells[start, helpPrint] = LokSum[counterPrint] / typVystupov;
                                else
                                    worksheet.Cells[start, helpPrint] = 0;
                            }
                            worksheet.Cells[start, 2] = local_category;

                            //ZAPISANIE PERCENT
                            if (LokSum[0] == 0)
                            {
                                worksheet.Cells[start, 6] = 0;
                                worksheet.Cells[start, 10] = 0;
                            }
                            else
                            {
                                worksheet.Cells[start, 6] = 100 * LokSum[2] / LokSum[0];
                                worksheet.Cells[start, 10] = 100 * LokSum[4] / LokSum[0];
                            }

                            if (LokSum[1] == 0)
                            {
                                worksheet.Cells[start, 7] = 0;
                                worksheet.Cells[start, 11] = 0;
                            }
                            else
                            {
                                worksheet.Cells[start, 7] = 100 * LokSum[2] / LokSum[1];
                                worksheet.Cells[start, 11] = 100 * LokSum[4] / LokSum[1];
                            }

                            LokSum = new double[5];
                            local_category = current_category;
                            increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 8, 11);
                        }
                        counter++;

                        int index_column = 2;
                        foreach (DataColumn column in dT.Columns)
                        {//DT columnns -> Kategoria,SUM(SchvalenyRozpocet),SUM(RozpocetPoZmenach),SUM(Skutocnost)
                            if (index_column > 2)
                            {
                                if (row[0].ToString() == "640")
                                    mem6_64[index_column - 3] = (double)row[column];

                                LokSum[index_column - 3] += (double)row[column];
                                sum_ico[index_column - 3] += (double)row[column];//0 - RS, 1 -RU, 2 - Skut
                                if (Int32.Parse(row[0].ToString()) / 100 == div_2367 || Int32.Parse(row[0].ToString()) / 100 == div_2367 + 1)
                                    sum_2367[index_column - 3] += (double)row[column];
                            }
                            if (index_column == 2)
                                worksheet.Cells[start, index_column++] = row[column];
                            else
                            {
                                if ((double)row[column] == 0)
                                    worksheet.Cells[start, index_column++] = row[column];
                                else
                                    worksheet.Cells[start, index_column++] = (double)row[column] / typVystupov;
                            }
                        }

                        if (i < dT2.Rows.Count && (comparison = row[0].ToString().CompareTo(dT2.Rows[i][0].ToString())) == 0)
                        {//DT2 columnns -> Kategoria, SUM(Skutocnost)
                            if ((double)dT2.Rows[i][1] == 0)
                                worksheet.Cells[start, index_column + 3] = dT2.Rows[i][1]; //zapisujem SKUTOCNOST - MRZ
                            else
                                worksheet.Cells[start, index_column + 3] = (double)dT2.Rows[i][1] / typVystupov;

                            if (row[0].ToString() == "640")
                                mem6_64[4] = (double)dT2.Rows[i][1];

                            LokSum[4] += (double)dT2.Rows[i][1];
                            sum_ico[4] += (double)dT2.Rows[i][1];// 4 - SKUT - MRZ
                            if (Int32.Parse(row[0].ToString()) / 100 == div_2367 || Int32.Parse(row[0].ToString()) / 100 == div_2367 + 1)
                                sum_2367[4] += (double)dT2.Rows[i][1];

                            double skut_mrz = 0;
                            skut_mrz = Math.Abs(Convert.ToDouble(row[3].ToString()) - (double)dT2.Rows[i][1]);
                            if (skut_mrz == 0)
                                worksheet.Cells[start, index_column + 2] = skut_mrz;//zapisujem MRZ
                            else
                                worksheet.Cells[start, index_column + 2] = skut_mrz / typVystupov;

                            if (row[0].ToString() == "640")
                                mem6_64[3] = Math.Abs((double)skut_mrz);

                            LokSum[3] += Math.Abs((double)skut_mrz);
                            sum_ico[3] += Math.Abs((double)skut_mrz);// 3 - MRZ
                            if (Int32.Parse(row[0].ToString()) / 100 == div_2367 || Int32.Parse(row[0].ToString()) / 100 == div_2367 + 1)
                                sum_2367[3] += Math.Abs(skut_mrz);

                            if ((double)row[1] == 0)
                                worksheet.Cells[start, index_column + 4] = 0;
                            else
                                worksheet.Cells[start, index_column + 4] = 100 * (double)dT2.Rows[i][1] / (double)row[1];

                            if ((double)row[2] == 0)
                                worksheet.Cells[start, index_column + 5] = 0;
                            else
                                worksheet.Cells[start, index_column + 5] = 100 * (double)dT2.Rows[i][1] / (double)row[2];
                            i++;
                        }
                        else
                        {
                            worksheet.Cells[start, index_column + 3] = 0;
                            if ((double)row[3] == 0)
                                worksheet.Cells[start, index_column + 2] = (double)row[3];
                            else
                                worksheet.Cells[start, index_column + 2] = (double)row[3] / typVystupov;

                            if (row[0].ToString() == "640")
                                mem6_64[3] = (double)row[3];

                            LokSum[3] += (double)row[3];
                            sum_ico[3] += (double)row[3];// 3 - MRZ
                            if (Int32.Parse(row[0].ToString()) / 100 == div_2367 || Int32.Parse(row[0].ToString()) / 100 == div_2367 + 1)
                                sum_2367[3] += (double)row[3];

                            worksheet.Cells[start, index_column + 4] = 0;
                            worksheet.Cells[start, index_column + 5] = 0;
                        }
                        //% k rs 1 -> 3/1     % k RU 3/2,        % k rs 7/1      7/2
                        if ((double)row[1] == 0)
                            worksheet.Cells[start, index_column] = 0;
                        else
                            worksheet.Cells[start, index_column] = 100 * (double)row[3] / (double)row[1];

                        if ((double)row[2] == 0)
                            worksheet.Cells[start, index_column + 1] = 0;
                        else
                            worksheet.Cells[start, index_column + 1] = 100 * (double)row[3] / (double)row[2];

                        if (counter == dT.Rows.Count)
                        {
                            if (typ == synteticke[5] && prijmyVydavkyVeda != 1 && current_category == 6)
                            {
                                increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 8, 11);
                                //ZAPISANIE UDAJOV
                                for (int counterPrint = 0; counterPrint <= 4; counterPrint++)
                                {
                                    int helpPrint = counterPrint + 3;
                                    if (counterPrint > 2)
                                        helpPrint = helpPrint + 2;

                                    if (LokSum[counterPrint] - mem6_64[counterPrint] != 0)
                                        worksheet.Cells[start, helpPrint] = (LokSum[counterPrint] - mem6_64[counterPrint]) / typVystupov;
                                    else
                                        worksheet.Cells[start, helpPrint] = 0;
                                }
                                worksheet.Cells[start, 2].NumberFormat = "@";
                                worksheet.Cells[start, 2] = "6-64";

                                if (LokSum[0] - mem6_64[0] == 0)
                                {
                                    worksheet.Cells[start, 6] = 0;
                                    worksheet.Cells[start, 10] = 0;
                                }
                                else
                                {
                                    worksheet.Cells[start, 6] = 100 * (LokSum[2] - mem6_64[2]) / (LokSum[0] - mem6_64[0]);
                                    worksheet.Cells[start, 10] = 100 * (LokSum[4] - mem6_64[4]) / (LokSum[0] - mem6_64[0]);
                                }

                                if (LokSum[1] - mem6_64[1] == 0)
                                {
                                    worksheet.Cells[start, 7] = 0;
                                    worksheet.Cells[start, 11] = 0;
                                }
                                else
                                {
                                    worksheet.Cells[start, 7] = 100 * (LokSum[2] - mem6_64[2]) / (LokSum[1] - mem6_64[1]);
                                    worksheet.Cells[start, 11] = 100 * (LokSum[4] - mem6_64[4]) / (LokSum[1] - mem6_64[1]);
                                }

                                mem6_64 = new double[5];
                            }


                            increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 8, 11);
                            //ZAPISANIE UDAJOV
                            for (int counterPrint = 0; counterPrint <= 4; counterPrint++)
                            {
                                int helpPrint = counterPrint + 3;
                                if (counterPrint > 2)
                                    helpPrint = helpPrint + 2;

                                if (LokSum[counterPrint] != 0)
                                    worksheet.Cells[start, helpPrint] = LokSum[counterPrint] / typVystupov;
                                else
                                    worksheet.Cells[start, helpPrint] = 0;
                            }
                            worksheet.Cells[start, 2] = local_category;

                            if (LokSum[0] == 0)
                            {
                                worksheet.Cells[start, 6] = 0;
                                worksheet.Cells[start, 10] = 0;
                            }
                            else
                            {
                                worksheet.Cells[start, 6] = 100 * LokSum[2] / LokSum[0];
                                worksheet.Cells[start, 10] = 100 * LokSum[4] / LokSum[0];
                            }

                            

                            if (LokSum[1] == 0)
                            {
                                worksheet.Cells[start, 7] = 0;
                                worksheet.Cells[start, 11] = 0;
                            }
                            else
                            {
                                worksheet.Cells[start, 7] = 100 * LokSum[2] / LokSum[1];
                                worksheet.Cells[start, 11] = 100 * LokSum[4] / LokSum[1];
                            }

                            LokSum = new double[5];
                            local_category = current_category;
                        }
                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 8, 11);
                    }

                    if (typ != synteticke[2] && typ != synteticke[3])
                    {
                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 8, 11);
                        //ZAPISANIE UDAJOV
                        for (int counterPrint = 0; counterPrint <= 4; counterPrint++)
                        {
                            int helpPrint = counterPrint + 3;
                            if (counterPrint > 2)
                                helpPrint = helpPrint + 2;

                            if (sum_2367[counterPrint] != 0)
                                worksheet.Cells[start, helpPrint] = sum_2367[counterPrint] / typVystupov;
                            else
                                worksheet.Cells[start, helpPrint] = 0;
                        }
                        worksheet.Cells[start, 2] = text_2367;

                        if (sum_2367[0] == 0)
                        {
                            worksheet.Cells[start, 6] = 0;
                            worksheet.Cells[start, 10] = 0;
                        }
                        else
                        {
                            worksheet.Cells[start, 6] = 100 * sum_2367[2] / sum_2367[0];
                            worksheet.Cells[start, 10] = 100 * sum_2367[4] / sum_2367[0];
                        }

                        if (sum_2367[1] == 0)
                        {
                            worksheet.Cells[start, 7] = 0;
                            worksheet.Cells[start, 11] = 0;
                        }
                        else
                        {
                            worksheet.Cells[start, 7] = 100 * sum_2367[2] / sum_2367[1];
                            worksheet.Cells[start, 11] = 100 * sum_2367[4] / sum_2367[1];
                        }
                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 8, 11);
                    }

                    increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 8, 11);
                    //ZAPISANIE UDAJOV
                    for (int counterPrint = 0; counterPrint <= 4; counterPrint++)
                    {
                        int helpPrint = counterPrint + 3;
                        if (counterPrint > 2)
                            helpPrint = helpPrint + 2;

                        if (sum_ico[counterPrint] != 0)
                            worksheet.Cells[start, helpPrint] = sum_ico[counterPrint] / typVystupov;
                        else
                            worksheet.Cells[start, helpPrint] = 0;
                    }
                    worksheet.Cells[start, 1] = "Spolu org. " + skratka_org;

                    if (sum_ico[0] == 0)
                    {
                        worksheet.Cells[start, 6] = 0;
                        worksheet.Cells[start, 10] = 0;
                    }
                    else
                    {
                        worksheet.Cells[start, 6] = 100 * sum_ico[2] / sum_ico[0];
                        worksheet.Cells[start, 10] = 100 * sum_ico[4] / sum_ico[0];
                    }

                    if (sum_ico[1] == 0)
                    {
                        worksheet.Cells[start, 7] = 0;
                        worksheet.Cells[start, 11] = 0;
                    }
                    else
                    {
                        worksheet.Cells[start, 7] = 100 * sum_ico[2] / sum_ico[1];
                        worksheet.Cells[start, 11] = 100 * sum_ico[4] / sum_ico[1];
                    }
                    //  double[] sum_ico = new double[5];
                    //[0] - RS
                    //[1] - RU
                    //[2] - Skutocnost
                    //[3] - MRZ
                    //[4] - SKUT - MRZ
                    increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 8, 11);
                    for (char c = left_char; c <= (char)((int)right_char); c++)//tenke oramovanie od border po start
                    {
                        borderRange = worksheet.get_Range(c.ToString() + border, c.ToString() + start);
                        borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                        //ZAOKRUHLOVANIE
                        //CISLA
                        if ((c > left_char + 1 && c < left_char + 5) || (c > left_char + 6 && c < left_char + 9))
                        {
                            borderRange.NumberFormat = zaokruhlenieMena;
                            borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                        }
                        //PERCENTA
                        else
                        {
                            if ((c > left_char + 4 && c < left_char + 7) || c > left_char + 8)
                            {
                                borderRange.NumberFormat = zaokruhleniePercenta;
                                borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                            }
                        }
                        //ZAROVNANIE KATEGORIE
                        if (c == left_char + 1)
                            borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    }

                    //HLAVNE ORAMOVANIE
                    borderRange = worksheet.get_Range(left_char.ToString() + border, right_char.ToString() + start);//hrube po Celkovo
                    borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                    if ((helpString == "Celkovo" && opro == false) || helpString == "OPRO - MŠVVaŠ SR")
                    {
                        var culture = new CultureInfo("ru-RU");
                        worksheet.Cells[++start, 1] = "Dátum vytvorenia: " + time.ToString(culture);
                    }
                    else
                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 8, 11);
                }

                workbook.Save();
                if (priamaTlac)
                    PrintMyExcelFile(workbook);
                Kontrola.release_excel(range, worksheet, workbook, app);
                Con.Close();
            }
        }

        public void spravPrveZostavyMimorozpoctoveUcty(string typ, string MSVSico, int prijmyVydavkyVeda, Boolean lenOkruhy, Boolean lenMSVS, string datum, int typVystupov, string zaokruhlenieMena, string zaokruhleniePercenta, char left_char, int left_num, char right_char, int right_num, int count_for_page, int strana_rel_pos, DateTime time, bool priamaTlac)//1 prijmy 2 vydavky
        {
            string connectionstring = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            string[] synteticke = najdiSynteticke(connectionstring);
            int typSynteticky = Array.IndexOf(synteticke, typ);
            string ekonomickaKlasifikacia = zistiEkonomickuKlasifikaciu(prijmyVydavkyVeda, 1, connectionstring);
            SqlCommand com = null;

            //AK MA ROBIT VEDU TAK Z DATABAZY VYTIAHNE VYNIMKY VEDY
            string veda = "";
            if (prijmyVydavkyVeda == 3 || prijmyVydavkyVeda == 4)
                veda = vynimkyVedy(prijmyVydavkyVeda, connectionstring);
            string doplnokMSVS = "";
            if (lenMSVS)
                doplnokMSVS = "AND tv.Ico LIKE '" + MSVSico + "%' AND tv.Podtrieda LIKE '9800' ";

            DataTable dT = new DataTable();
            DataTable dtOPRO = new DataTable();
            DataTable dtOPRO2 = new DataTable();
            using (var Con = new SqlConnection(connectionstring))
            {
                string name = "RZBT";
                string nameTabulka = zistiNazovZakladne(typSynteticky, prijmyVydavkyVeda, lenOkruhy);
                if (typ == synteticke[2] || typ == synteticke[3] || typ == synteticke[6] || typ == synteticke[7])
                    name += "M";
                nameTabulka += "x";
                if (lenMSVS)
                    nameTabulka += "MSVS";

                ////AK MRZ PRIDA M DO NAZVU
                //if (typ == synteticke[2] || typ == synteticke[3] || typ == synteticke[6] || typ == synteticke[7])
                //    name = name + "M";
                name = name + nameTabulka + ".xls";
                /*if (opro == false)
                    name = "zostava_PO_" + typ + "_" + prijmyVydavkyVeda + ".xls";
                else
                    name = "zostava_RO_" + typ + "_" + prijmyVydavkyVeda + ".xls";*/

                string kvartal = vytvorAdresare(datum);

                System.IO.File.Copy("..\\..\\Hlav\\RZB\\hl345cv3MU.xls", "Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name, true);
                Excel.Application app = new Excel.Application();
                string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                Excel.Workbook workbook = app.Workbooks.Open(path + "\\" + "bin\\Debug\\Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name);
                Excel._Worksheet worksheet = workbook.Sheets[1];//pre citanie excelu
                Excel.Range range = worksheet.UsedRange;//pre citanie excelu
                Excel.Range borderRange;
                int rowCount = range.Rows.Count;//pre citanie excelu
                int start = right_num + 1;
                worksheet.Cells[start - strana_rel_pos, (int)right_char - 65 + 1] = "Strana: 1";
                int strana = 2;
                int border = start;

                string organizacia = "";
                string organizacia_sklon = "";
                string plnenie = "";
                if (typ == synteticke[0] || typ == synteticke[2] || typ == synteticke[1] || typ == synteticke[3])
                {
                    organizacia = "Príspevkové organizácie";
                    organizacia_sklon = "príspevkových organizáciách";
                }
                else
                {
                    organizacia = "Rozpočtové organizácie";
                    organizacia_sklon = "rozpočtových organizáciách";

                    if (prijmyVydavkyVeda == 3)
                        organizacia_sklon += " za vedu";
                    if (prijmyVydavkyVeda == 4)
                        organizacia_sklon += " okrem vedy";
                }

                if (prijmyVydavkyVeda == 1)
                {
                    plnenie = "Plnenie príjmov na ";
                    if (lenOkruhy == true)
                    {
                        organizacia_sklon = "";
                        plnenie = "Plnenie príjmov za rezort školstva";
                    }
                }
                else
                {
                    plnenie = "Výdavky školstva na ";
                    if (lenOkruhy == true)
                    {
                        organizacia_sklon = "";
                        plnenie = "Výdavky za rezort školstva";
                    }
                }

                if (lenMSVS)
                    organizacia_sklon += " za f.k. 0980 na MŠVVaŠ SR - úrad";

                string doplnkovyText = "";
                if (typ == synteticke[6] || typ == synteticke[7])
                    doplnkovyText = "Mimorozpočtové účty";

                //TEXTY
                worksheet.Cells[4, 1] = doplnkovyText;
                worksheet.Cells[5, 1] = "Spracovateľské obdobie: " +  datum;
                worksheet.Cells[5, 12] = nameTabulka;
                worksheet.Cells[1, 11] = organizacia;
                worksheet.Cells[2, 1] = plnenie + organizacia_sklon;
                if (typVystupov == 1)
                    worksheet.Cells[3, 1] = "(údaje sú v €)";
                else
                    worksheet.Cells[3, 1] = "(údaje sú v tisícoch €)";

                double[] sum_all = new double[5];
                Con.Open();

                List<string> listSynteticke = new List<string>();
                List<int> listUsporiadanieSynteticke = new List<int>();
                if (typ == "MRP" || typ == "MRV")
                {

                    com = new SqlCommand("SELECT DISTINCT tv.SyntetickyaleboFiktivny FROM TableVstup tv WHERE tv.SyntetickyaleboFiktivny NOT LIKE @Synteticky1 AND tv.SyntetickyaleboFiktivny NOT LIKE @Synteticky2 AND tv.SyntetickyaleboFiktivny NOT LIKE @Synteticky3 " + doplnokMSVS + ekonomickaKlasifikacia + " AND tv.KalendarnyDen LIKE @Datum ORDER BY tv.SyntetickyaleboFiktivny", Con);
                    if (typ == "MRP")
                    {
                        com.Parameters.AddWithValue("@Synteticky1", synteticke[0]);
                        com.Parameters.AddWithValue("@Synteticky2", synteticke[2]);
                        com.Parameters.AddWithValue("@Synteticky3", synteticke[4]);
                    }
                    if (typ == "MRV")
                    {
                        com.Parameters.AddWithValue("@Synteticky1", synteticke[1]);
                        com.Parameters.AddWithValue("@Synteticky2", synteticke[3]);
                        com.Parameters.AddWithValue("@Synteticky3", synteticke[5]);
                    }
                    com.Parameters.AddWithValue("@Datum", datum);

                    dT = new DataTable();
                    using (SqlDataAdapter da = new SqlDataAdapter(com))
                    {
                        da.Fill(dT);
                        com.ExecuteNonQuery();
                    }

                    //Najde vsetky synteticke okrem obycajnych pre prijmy alebo vydavky podla vstupu
                    foreach (DataRow row in dT.Rows)
                        listUsporiadanieSynteticke.Add(Int32.Parse(row[0].ToString()));

                    //Usporiadanie a prehodenie na stringy
                    listUsporiadanieSynteticke.Sort();
                    listSynteticke = listUsporiadanieSynteticke.ConvertAll<string>(x => x.ToString());
                    //pridanie kumulativu
                    listSynteticke.Add("Spolu za kódy účtov");
                }
                else
                {
                    //Nahodi typ z parametrov ak je jasne dany
                    listSynteticke.Add(typ);
                }

                //Cyklus pre všetky synteticke ucty
                foreach (var synteticky in listSynteticke)
                {
                    border = start;
                    borderRange = worksheet.get_Range(left_char.ToString() + start, right_char.ToString() + start);
                    borderRange.Font.Bold = true;
                    borderRange.Clear();
                    borderRange.Merge();
                    borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                    SqlCommand get_name_synteticky = new SqlCommand("SELECT su.Text FROM SyntetickyUcet su WHERE su.SyntetickyUcet LIKE '" + synteticky.ToString() + "%' ORDER BY su.SyntetickyUcet", Con);
                    DataTable get_name_table_synteticky = new DataTable();
                    using (SqlDataAdapter get_name_adapter_synteticky = new SqlDataAdapter(get_name_synteticky))
                    {
                        get_name_adapter_synteticky.Fill(get_name_table_synteticky);
                        get_name_synteticky.ExecuteNonQuery();
                    }

                    if(get_name_table_synteticky.Rows.Count!=0)
                        worksheet.Cells[start, 1] = synteticky + ": " + get_name_table_synteticky.Rows[0][0].ToString();
                    else
                        worksheet.Cells[start, 1] = synteticky;

                    increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 5, 8, 10, 13);

                    //Najdeme vsetky druhy rozpoctu pre synteticky ucet
                    List<string> listDruhyRozpoctov = new List<string>();
                    if (synteticky != "Spolu za kódy účtov")
                    {
                        com = new SqlCommand("SELECT DISTINCT tv.TypZdroja FROM TableVstup tv WHERE tv.SyntetickyaleboFiktivny LIKE @Synteticky " + doplnokMSVS + ekonomickaKlasifikacia + " AND tv.KalendarnyDen LIKE @Datum ORDER BY tv.TypZdroja", Con);

                        com.Parameters.AddWithValue("@Synteticky", synteticky);

                        com.Parameters.AddWithValue("@Datum", datum);

                        dT = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(com))
                        {
                            da.Fill(dT);
                            com.ExecuteNonQuery();
                        }

                        //Najde vsetky synteticke okrem obycajnych pre prijmy alebo vydavky podla vstupu
                        foreach (DataRow row in dT.Rows)
                            listDruhyRozpoctov.Add((string)row[0]);
                    }
                    //pridanie kumulativu
                    listDruhyRozpoctov.Add("Celkom");

                    foreach (var druhRozpoctu in listDruhyRozpoctov)
                    {
                        borderRange = worksheet.get_Range(((char)(left_char + 1)).ToString() + start, right_char.ToString() + start);
                        borderRange.Font.Bold = true;
                        borderRange.Clear();
                        borderRange.Merge();

                        if (druhRozpoctu == "Celkom" && synteticky != "Spolu za kódy účtov")
                            worksheet.Cells[start, 2] = "Spolu za kód účtu " + synteticky;
                        else
                        {
                            if (druhRozpoctu == "#")
                                worksheet.Cells[start, 2] = "Nepriradené";
                            else
                                worksheet.Cells[start, 2] = druhRozpoctu;
                        }

                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 5, 8, 10, 13);

                        int i = 0;
                        string[] arrIco;
                        if (lenOkruhy == false)
                        {
                            if (synteticky != "Spolu za kódy účtov" && druhRozpoctu != "Celkom")
                            {
                                com = new SqlCommand("SELECT DISTINCT tv.Ico,ci.CelyNazov collate Slovak_CI_AS FROM TableVstup tv JOIN ciselnikIco ci ON tv.Ico=ci.Ico WHERE SyntetickyaleboFiktivny LIKE @Synteticky AND TypZdroja LIKE @DruhRozpoctu " + doplnokMSVS + ekonomickaKlasifikacia + " AND KalendarnyDen LIKE @Datum", Con);

                                //if (synteticky == "Celkom")
                                //    com.Parameters.AddWithValue("@Synteticky", "%");
                                //else
                                    com.Parameters.AddWithValue("@Synteticky", synteticky);
                                //if (druhRozpoctu == "Celkom")
                                //    com.Parameters.AddWithValue("@DruhRozpoctu", "%");
                                //else
                                    com.Parameters.AddWithValue("@DruhRozpoctu", druhRozpoctu);

                                com.Parameters.AddWithValue("@Datum", datum);
                                dT = new DataTable();
                                using (SqlDataAdapter da = new SqlDataAdapter(com))
                                {
                                    da.Fill(dT);
                                    com.ExecuteNonQuery();
                                }

                                arrIco = new string[dT.Rows.Count + 1];

                                foreach (DataRow row in dT.Rows) // Loop over the rows.
                                {
                                    arrIco[i] = (string)row[0];
                                    i++;
                                }
                            }
                            else
                                arrIco = new string[1];


                            arrIco[i++] = "Spolu za druh rozpočtu";
                        }
                        else
                        {
                            arrIco = new string[1];
                            arrIco[0] = "Ostatné priamo riadené organizácie";
                        }

                        string vynimky;
                        SqlCommand com2 = null;
                        DataTable dT2;

                        foreach (string helpString in arrIco) // Loop over the rows.
                        {
                            vynimky = vynimkyMRZ(connectionstring);
                            dT = new DataTable();
                            dT2 = new DataTable();

                            string querrySynteticke = "AND (SyntetickyaleboFiktivny LIKE";
                            //NASTAVENIE VYBERU ZA SYNTETICKY
                            if (synteticky == "Spolu za kódy účtov")
                            {
                                foreach (var item in listSynteticke)
                                {
                                    if (item != synteticky)
                                        querrySynteticke += " '" + item + "'";

                                    if (listSynteticke.IndexOf(item) < (listSynteticke.Count - 2))
                                        querrySynteticke += " OR SyntetickyaleboFiktivny LIKE";
                                }
                                querrySynteticke += ") ";
                            }
                            else
                                querrySynteticke += " '" + synteticky + "') ";

                            com = new SqlCommand("SELECT Kategoria,SUM(SchvalenyRozpocet),SUM(RozpocetPoZmenach),SUM(Skutocnost) FROM TableVstup tv WHERE TypZdroja LIKE @DruhRozpoctu " + querrySynteticke + " AND KalendarnyDen LIKE @Datum " + doplnokMSVS + veda + " "  + ekonomickaKlasifikacia + " AND Ico LIKE @Ico GROUP BY Kategoria ORDER BY Kategoria", Con);
                            com2 = new SqlCommand("SELECT Kategoria, SUM(Skutocnost) FROM TableVstup tv WHERE TypZdroja LIKE @DruhRozpoctu " + querrySynteticke + " AND KalendarnyDen LIKE @Datum AND Ico LIKE @Ico " + doplnokMSVS + vynimky + " " + veda + " "  + ekonomickaKlasifikacia + " GROUP BY Kategoria ORDER BY Kategoria", Con);

                            //if (synteticky == "Spolu za kódy účtov")
                            //{
                            //    com.Parameters.AddWithValue("@typ", "%");
                            //    com2.Parameters.AddWithValue("@typ", "%");
                            //}
                            //else
                            //{
                            //    com.Parameters.AddWithValue("@typ", synteticky);
                            //    com2.Parameters.AddWithValue("@typ", synteticky);
                            //}

                            if (druhRozpoctu == "Celkom")
                            {
                                com.Parameters.AddWithValue("@DruhRozpoctu", "%");
                                com2.Parameters.AddWithValue("@DruhRozpoctu", "%");
                            }
                            else
                            {
                                com.Parameters.AddWithValue("@DruhRozpoctu", druhRozpoctu);
                                com2.Parameters.AddWithValue("@DruhRozpoctu", druhRozpoctu);
                            }

                            com.Parameters.AddWithValue("@Datum", datum);
                            com2.Parameters.AddWithValue("@Datum", datum);

                            if (helpString == "Spolu za druh rozpočtu" || helpString == "OPRO - MŠVVaŠ SR" || helpString == "Ostatné priamo riadené organizácie")
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
                            if (helpString != "OPRO - MŠVVaŠ SR" && helpString != "Spolu za druh rozpočtu" && helpString != "Príspev. organizácie" && helpString != "Ostatné priamo riadené organizácie")
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
                                if (helpString == "Ostatné priamo riadené organizácie")
                                    skratka_org = "OPRO";
                                else
                                    skratka_org = helpString;
                            }

                            if (druhRozpoctu == "Celkom")
                            {
                                nazov_org = synteticky;
                                skratka_org = synteticky;
                            }

                            if (helpString == "Spolu za druh rozpočtu")
                                nazov_org = nazov_org + " " + druhRozpoctu;
                           
                            if (druhRozpoctu != "Celkom")
                                worksheet.Cells[start, 3] = nazov_org;


                            int local_category = Int32.Parse(dT.Rows[0][0].ToString()) / 100;
                            double[] LokSum = new double[5];
                            double[] mem6_64 = new double[5];
                            double[] sum_2367 = new double[5];
                            double[] sum_ico = new double[5];

                            string text_2367;
                            int div_2367;
                            if (prijmyVydavkyVeda == 1)
                            {
                                text_2367 = "2xx, 3xx";
                                div_2367 = 2;
                            }
                            else if (prijmyVydavkyVeda == 2 || prijmyVydavkyVeda == 3 || prijmyVydavkyVeda == 4)
                            {
                                text_2367 = "6xx, 7xx";
                                div_2367 = 6;
                            }
                            else
                            {
                                text_2367 = "";
                                div_2367 = 0;
                            }

                            //[0] - RS
                            //[1] - RU
                            //[2] - Skutocnost
                            //[3] - MRZ
                            //[4] - SKUT - MRZ

                            int counter = 0;

                            foreach (DataRow row in dT.Rows) // Loop over the rows.
                            {
                                //workbook.Save();
                                comparison = 0;
                                if (helpString == "OPRO - MŠVVaŠ SR")
                                {
                                    int test2 = dtOPRO.Rows.Count;

                                    foreach (DataRow rowOpro in dtOPRO.Rows)//prve 3 miesta RS RU SKUT
                                    {
                                        string test1 = rowOpro[0].ToString();
                                        if ((comparison = test1.CompareTo(row[0])) == 0)
                                        {
                                            for (int l = 1; l < 4; l++)
                                            {
                                                row[l] = (Double.Parse(row[l].ToString()) - Double.Parse(rowOpro[l].ToString())).ToString();//tu odcitane
                                            }
                                        }
                                    }

                                    foreach (DataRow rowOpro in dtOPRO2.Rows)//MRZ pre OPRO
                                    {
                                        string test1 = rowOpro[0].ToString();
                                        if ((comparison = test1.CompareTo(row[0])) == 0)
                                        {
                                            for (int l = 1; l < 2; l++)
                                            {
                                                dT2.Rows[i][l] = (Double.Parse(dT2.Rows[i][l].ToString()) - Double.Parse(rowOpro[l].ToString())).ToString();//tu odcitane
                                            }
                                        }
                                    }
                                }

                                int current_category = (Int32.Parse(row[0].ToString()) / 100);
                                if (current_category != local_category)
                                {
                                    //VYPIS TU
                                    if (typ == synteticke[5] && prijmyVydavkyVeda != 1)
                                    {
                                        //ZAPISANIE UDAJOV
                                        for (int counterPrint = 0; counterPrint <= 4; counterPrint++)
                                        {
                                            int helpPrint = counterPrint + 5;
                                            if (counterPrint > 2)
                                                helpPrint = helpPrint + 2;

                                            if (LokSum[counterPrint] - mem6_64[counterPrint] != 0)
                                                worksheet.Cells[start, helpPrint] = (LokSum[counterPrint] - mem6_64[counterPrint]) / typVystupov;
                                            else
                                                worksheet.Cells[start, helpPrint] = 0;
                                        }
                                        worksheet.Cells[start, 4].NumberFormat = "@";
                                        worksheet.Cells[start, 4] = "6-64";

                                        //ZAPISANIE PERCENT
                                        if (LokSum[0] - mem6_64[0] == 0)
                                        {
                                            worksheet.Cells[start, 8] = 0;
                                            worksheet.Cells[start, 12] = 0;
                                        }
                                        else
                                        {
                                            worksheet.Cells[start, 8] = 100 * (LokSum[2] - mem6_64[2]) / (LokSum[0] - mem6_64[0]);
                                            worksheet.Cells[start, 12] = 100 * (LokSum[4] - mem6_64[4]) / (LokSum[0] - mem6_64[0]);
                                        }

                                        if (LokSum[1] - mem6_64[1] == 0)
                                        {
                                            worksheet.Cells[start, 9] = 0;
                                            worksheet.Cells[start, 13] = 0;
                                        }
                                        else
                                        {
                                            worksheet.Cells[start, 9] = 100 * (LokSum[2] - mem6_64[2]) / (LokSum[1] - mem6_64[1]);
                                            worksheet.Cells[start, 13] = 100 * (LokSum[4] - mem6_64[4]) / (LokSum[1] - mem6_64[1]);
                                        }

                                        mem6_64 = new double[5];
                                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 5, 8, 10, 13);
                                    }

                                    //ZAPISANIE UDAJOV
                                    for (int counterPrint = 0; counterPrint <= 4; counterPrint++)
                                    {
                                        int helpPrint = counterPrint + 5;
                                        if (counterPrint > 2)
                                            helpPrint = helpPrint + 2;

                                        if (LokSum[counterPrint] != 0)
                                            worksheet.Cells[start, helpPrint] = LokSum[counterPrint] / typVystupov;
                                        else
                                            worksheet.Cells[start, helpPrint] = 0;
                                    }
                                    worksheet.Cells[start, 4] = local_category;

                                    //ZAPISANIE PERCENT
                                    if (LokSum[0] == 0)
                                    {
                                        worksheet.Cells[start, 8] = 0;
                                        worksheet.Cells[start, 12] = 0;
                                    }
                                    else
                                    {
                                        worksheet.Cells[start, 8] = 100 * LokSum[2] / LokSum[0];
                                        worksheet.Cells[start, 12] = 100 * LokSum[4] / LokSum[0];
                                    }

                                    if (LokSum[1] == 0)
                                    {
                                        worksheet.Cells[start, 9] = 0;
                                        worksheet.Cells[start, 13] = 0;
                                    }
                                    else
                                    {
                                        worksheet.Cells[start, 9] = 100 * LokSum[2] / LokSum[1];
                                        worksheet.Cells[start, 13] = 100 * LokSum[4] / LokSum[1];
                                    }

                                    LokSum = new double[5];
                                    local_category = current_category;
                                    increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 5, 8, 10, 13);
                                }
                                counter++;

                                int index_column = 4;
                                foreach (DataColumn column in dT.Columns)
                                {//DT columnns -> Kategoria,SUM(SchvalenyRozpocet),SUM(RozpocetPoZmenach),SUM(Skutocnost)
                                    if (index_column > 4)
                                    {
                                        if (row[0].ToString() == "640")
                                            mem6_64[index_column - 5] = (double)row[column];

                                        LokSum[index_column - 5] += (double)row[column];
                                        sum_ico[index_column - 5] += (double)row[column];//0 - RS, 1 -RU, 2 - Skut
                                        if (Int32.Parse(row[0].ToString()) / 100 == div_2367 || Int32.Parse(row[0].ToString()) / 100 == div_2367 + 1)
                                            sum_2367[index_column - 5] += (double)row[column];
                                    }
                                    if (index_column == 4)
                                        worksheet.Cells[start, index_column++] = row[column];
                                    else
                                    {
                                        if ((double)row[column] == 0)
                                            worksheet.Cells[start, index_column++] = row[column];
                                        else
                                            worksheet.Cells[start, index_column++] = (double)row[column] / typVystupov;
                                    }
                                }

                                if (i < dT2.Rows.Count && (comparison = row[0].ToString().CompareTo(dT2.Rows[i][0].ToString())) == 0)
                                {//DT2 columnns -> Kategoria, SUM(Skutocnost)
                                    if ((double)dT2.Rows[i][1] == 0)
                                        worksheet.Cells[start, index_column + 3] = dT2.Rows[i][1]; //zapisujem SKUTOCNOST - MRZ
                                    else
                                        worksheet.Cells[start, index_column + 3] = (double)dT2.Rows[i][1] / typVystupov;

                                    if (row[0].ToString() == "640")
                                        mem6_64[4] = (double)dT2.Rows[i][1];

                                    LokSum[4] += (double)dT2.Rows[i][1];
                                    sum_ico[4] += (double)dT2.Rows[i][1];// 4 - SKUT - MRZ
                                    if (Int32.Parse(row[0].ToString()) / 100 == div_2367 || Int32.Parse(row[0].ToString()) / 100 == div_2367 + 1)
                                        sum_2367[4] += (double)dT2.Rows[i][1];

                                    double skut_mrz = 0;
                                    skut_mrz = Math.Abs(Convert.ToDouble(row[3].ToString()) - (double)dT2.Rows[i][1]);
                                    if (skut_mrz == 0)
                                        worksheet.Cells[start, index_column + 2] = skut_mrz;//zapisujem MRZ
                                    else
                                        worksheet.Cells[start, index_column + 2] = skut_mrz / typVystupov;

                                    if (row[0].ToString() == "640")
                                        mem6_64[3] = Math.Abs((double)skut_mrz);

                                    LokSum[3] += Math.Abs((double)skut_mrz);
                                    sum_ico[3] += Math.Abs((double)skut_mrz);// 3 - MRZ
                                    if (Int32.Parse(row[0].ToString()) / 100 == div_2367 || Int32.Parse(row[0].ToString()) / 100 == div_2367 + 1)
                                        sum_2367[3] += Math.Abs(skut_mrz);

                                    if ((double)row[1] == 0)
                                        worksheet.Cells[start, index_column + 4] = 0;
                                    else
                                        worksheet.Cells[start, index_column + 4] = 100 * (double)dT2.Rows[i][1] / (double)row[1];

                                    if ((double)row[2] == 0)
                                        worksheet.Cells[start, index_column + 5] = 0;
                                    else
                                        worksheet.Cells[start, index_column + 5] = 100 * (double)dT2.Rows[i][1] / (double)row[2];
                                    i++;
                                }
                                else
                                {
                                    worksheet.Cells[start, index_column + 3] = 0;
                                    if ((double)row[3] == 0)
                                        worksheet.Cells[start, index_column + 2] = (double)row[3];
                                    else
                                        worksheet.Cells[start, index_column + 2] = (double)row[3] / typVystupov;

                                    if (row[0].ToString() == "640")
                                        mem6_64[3] = (double)row[3];

                                    LokSum[3] += (double)row[3];
                                    sum_ico[3] += (double)row[3];// 3 - MRZ
                                    if (Int32.Parse(row[0].ToString()) / 100 == div_2367 || Int32.Parse(row[0].ToString()) / 100 == div_2367 + 1)
                                        sum_2367[3] += (double)row[3];

                                    worksheet.Cells[start, index_column + 4] = 0;
                                    worksheet.Cells[start, index_column + 5] = 0;
                                }
                                //% k rs 1 -> 3/1     % k RU 3/2,        % k rs 7/1      7/2
                                if ((double)row[1] == 0)
                                    worksheet.Cells[start, index_column] = 0;
                                else
                                    worksheet.Cells[start, index_column] = 100 * (double)row[3] / (double)row[1];

                                if ((double)row[2] == 0)
                                    worksheet.Cells[start, index_column + 1] = 0;
                                else
                                    worksheet.Cells[start, index_column + 1] = 100 * (double)row[3] / (double)row[2];

                                if (counter == dT.Rows.Count)
                                {
                                    if (typ == synteticke[5] && prijmyVydavkyVeda != 1 && current_category == 6)
                                    {
                                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 5, 8, 10, 13);
                                        //ZAPISANIE UDAJOV
                                        for (int counterPrint = 0; counterPrint <= 4; counterPrint++)
                                        {
                                            int helpPrint = counterPrint + 5;
                                            if (counterPrint > 2)
                                                helpPrint = helpPrint + 2;

                                            if (LokSum[counterPrint] - mem6_64[counterPrint] != 0)
                                                worksheet.Cells[start, helpPrint] = (LokSum[counterPrint] - mem6_64[counterPrint]) / typVystupov;
                                            else
                                                worksheet.Cells[start, helpPrint] = 0;
                                        }
                                        worksheet.Cells[start, 4].NumberFormat = "@";
                                        worksheet.Cells[start, 4] = "6-64";

                                        if (LokSum[0] - mem6_64[0] == 0)
                                        {
                                            worksheet.Cells[start, 8] = 0;
                                            worksheet.Cells[start, 12] = 0;
                                        }
                                        else
                                        {
                                            worksheet.Cells[start, 8] = 100 * (LokSum[2] - mem6_64[2]) / (LokSum[0] - mem6_64[0]);
                                            worksheet.Cells[start, 12] = 100 * (LokSum[4] - mem6_64[4]) / (LokSum[0] - mem6_64[0]);
                                        }

                                        if (LokSum[1] - mem6_64[1] == 0)
                                        {
                                            worksheet.Cells[start, 9] = 0;
                                            worksheet.Cells[start, 13] = 0;
                                        }
                                        else
                                        {
                                            worksheet.Cells[start, 9] = 100 * (LokSum[2] - mem6_64[2]) / (LokSum[1] - mem6_64[1]);
                                            worksheet.Cells[start, 13] = 100 * (LokSum[4] - mem6_64[4]) / (LokSum[1] - mem6_64[1]);
                                        }

                                        mem6_64 = new double[5];
                                    }


                                    increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 5, 8, 10, 13);
                                    //ZAPISANIE UDAJOV
                                    for (int counterPrint = 0; counterPrint <= 4; counterPrint++)
                                    {
                                        int helpPrint = counterPrint + 5;
                                        if (counterPrint > 2)
                                            helpPrint = helpPrint + 2;

                                        if (LokSum[counterPrint] != 0)
                                            worksheet.Cells[start, helpPrint] = LokSum[counterPrint] / typVystupov;
                                        else
                                            worksheet.Cells[start, helpPrint] = 0;
                                    }
                                    worksheet.Cells[start, 4] = local_category;

                                    if (LokSum[0] == 0)
                                    {
                                        worksheet.Cells[start, 8] = 0;
                                        worksheet.Cells[start, 12] = 0;
                                    }
                                    else
                                    {
                                        worksheet.Cells[start, 8] = 100 * LokSum[2] / LokSum[0];
                                        worksheet.Cells[start, 12] = 100 * LokSum[4] / LokSum[0];
                                    }



                                    if (LokSum[1] == 0)
                                    {
                                        worksheet.Cells[start, 9] = 0;
                                        worksheet.Cells[start, 13] = 0;
                                    }
                                    else
                                    {
                                        worksheet.Cells[start, 9] = 100 * LokSum[2] / LokSum[1];
                                        worksheet.Cells[start, 13] = 100 * LokSum[4] / LokSum[1];
                                    }

                                    LokSum = new double[5];
                                    local_category = current_category;
                                }
                                increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 5, 8, 10, 13);
                            }

                            if (typ != synteticke[2] && typ != synteticke[3])
                            {
                                increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 5, 8, 10, 13);
                                //ZAPISANIE UDAJOV
                                for (int counterPrint = 0; counterPrint <= 4; counterPrint++)
                                {
                                    int helpPrint = counterPrint + 5;
                                    if (counterPrint > 2)
                                        helpPrint = helpPrint + 2;

                                    if (sum_2367[counterPrint] != 0)
                                        worksheet.Cells[start, helpPrint] = sum_2367[counterPrint] / typVystupov;
                                    else
                                        worksheet.Cells[start, helpPrint] = 0;
                                }
                                worksheet.Cells[start, 4] = text_2367;

                                if (sum_2367[0] == 0)
                                {
                                    worksheet.Cells[start, 8] = 0;
                                    worksheet.Cells[start, 12] = 0;
                                }
                                else
                                {
                                    worksheet.Cells[start, 8] = 100 * sum_2367[2] / sum_2367[0];
                                    worksheet.Cells[start, 12] = 100 * sum_2367[4] / sum_2367[0];
                                }

                                if (sum_2367[1] == 0)
                                {
                                    worksheet.Cells[start, 9] = 0;
                                    worksheet.Cells[start, 13] = 0;
                                }
                                else
                                {
                                    worksheet.Cells[start, 9] = 100 * sum_2367[2] / sum_2367[1];
                                    worksheet.Cells[start, 13] = 100 * sum_2367[4] / sum_2367[1];
                                }
                                increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 5, 8, 10, 13);
                            }

                            increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 5, 8, 10, 13);
                            //ZAPISANIE UDAJOV
                            for (int counterPrint = 0; counterPrint <= 4; counterPrint++)
                            {
                                int helpPrint = counterPrint + 5;
                                if (counterPrint > 2)
                                    helpPrint = helpPrint + 2;

                                if (sum_ico[counterPrint] != 0)
                                    worksheet.Cells[start, helpPrint] = sum_ico[counterPrint] / typVystupov;
                                else
                                    worksheet.Cells[start, helpPrint] = 0;
                            }

                            
                            if(skratka_org == synteticky || helpString == "Spolu za druh rozpočtu")
                                worksheet.Cells[start, 3] = "Celkom";
                            else
                                worksheet.Cells[start, 3] = "Celkom org. " + skratka_org;
                               

                            if (sum_ico[0] == 0)
                            {
                                worksheet.Cells[start, 8] = 0;
                                worksheet.Cells[start, 12] = 0;
                            }
                            else
                            {
                                worksheet.Cells[start, 8] = 100 * sum_ico[2] / sum_ico[0];
                                worksheet.Cells[start, 12] = 100 * sum_ico[4] / sum_ico[0];
                            }

                            if (sum_ico[1] == 0)
                            {
                                worksheet.Cells[start, 9] = 0;
                                worksheet.Cells[start, 13] = 0;
                            }
                            else
                            {
                                worksheet.Cells[start, 9] = 100 * sum_ico[2] / sum_ico[1];
                                worksheet.Cells[start, 13] = 100 * sum_ico[4] / sum_ico[1];
                            }
                            increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 5, 8, 10, 13);
                            //  double[] sum_ico = new double[5];
                            //[0] - RS
                            //[1] - RU
                            //[2] - Skutocnost
                            //[3] - MRZ
                            //[4] - SKUT - MRZ

                            for (char c = left_char; c <= (char)((int)right_char); c++)//tenke oramovanie od border po start
                            {
                                borderRange = worksheet.get_Range(c.ToString() + border, c.ToString() + start);
                                borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                                //ZAOKRUHLOVANIE
                                //CISLA
                                if ((c > left_char + 3 && c < left_char + 7) || (c > left_char + 8 && c < left_char + 11))
                                {
                                    borderRange.NumberFormat = zaokruhlenieMena;
                                    borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                                }
                                //PERCENTA
                                else
                                {
                                    if ((c > left_char + 6 && c < left_char + 9) || c > left_char + 10)
                                    {
                                        borderRange.NumberFormat = zaokruhleniePercenta;
                                        borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                                    }
                                }
                                //ZAROVNANIE KATEGORIE
                                if (c <= left_char + 3)
                                {
                                    if(c == left_char + 3) 
                                        borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                    else
                                        borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                                    borderRange.NumberFormat = "@";
                                }
                            }

                            //if(!(synteticky == "Spolu za kódy účtov" && druhRozpoctu == "Celkom" && helpString == "Spolu za druh rozpočtu"))
                            increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 5, 8, 10, 13);

                        }
                    }
                    //HLAVNE ORAMOVANIE KAPITOLY SYNTETICKOEHO
                    borderRange = worksheet.get_Range(left_char.ToString() + border, right_char.ToString() + (start-1));//hrube po Celkovo
                    borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                    if (synteticky == "Spolu za kódy účtov")
                        borderRange.Font.Bold = true;
                    //if ((helpString == "Celkovo" && opro == false) || helpString == "OPRO - MŠVVaŠ SR")
                    //{

                }

                //HLAVNE ORAMOVANIE
                borderRange = worksheet.get_Range(left_char.ToString() + border, right_char.ToString() + (start-1));//hrube po Celkovo
                borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                //if ((helpString == "Celkovo" && opro == false) || helpString == "OPRO - MŠVVaŠ SR")
                //{
                var culture = new CultureInfo("ru-RU");
                worksheet.Cells[start, 1] = "Dátum vytvorenia: " + time.ToString(culture);
                
                workbook.Save();
                if (priamaTlac)
                    PrintMyExcelFile(workbook);
                Kontrola.release_excel(range, worksheet, workbook, app);
                Con.Close();
            }
        }

        public void spravDruheZostavy(string typ, string MSVSico, int prijmyVydavkyVeda, Boolean lenOkruhy, string datum, int typVystupov, string zaokruhlenieMena, string zaokruhleniePercenta, char left_char, int left_num, char right_char, int right_num, int count_for_page, int strana_rel_pos, DateTime time, bool priamaTlac)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            string[] synteticke = najdiSynteticke(connectionstring);
            string ekonomickaKlasifikacia = zistiEkonomickuKlasifikaciu(prijmyVydavkyVeda,1, connectionstring);
            //AK MA ROBIT VEDU TAK Z DATABAZY VYTIAHNE VYNIMKY VEDY
            string veda = "";
            if (prijmyVydavkyVeda == 3 || prijmyVydavkyVeda == 4)
                veda = vynimkyVedy(prijmyVydavkyVeda, connectionstring);

            SqlCommand com = null;

            DataTable dT = new DataTable();
            using (var Con = new SqlConnection(connectionstring))
            {
                Con.Open();
                
                //PODMIENKA CI ROBI OBYCAJNY SELECT S KONKRETNYM SYNTETICKYM UCTOM ALEBO PRE MRZ S NEZNAMIM SYNTETICKYM UCTOM A PODLA KATEGORII ALEBO VEDU
                if (typ != "MRP" && typ != "MRV")
                {
                    com = new SqlCommand("SELECT DISTINCT Zdroj FROM TableVstup WHERE SyntetickyaleboFiktivny LIKE @Synteticky AND KalendarnyDen LIKE @Datum " + veda + " ORDER BY Zdroj", Con);
                    com.Parameters.AddWithValue("@Synteticky", typ);
                }
                else
                {
                    com = new SqlCommand("SELECT DISTINCT Zdroj FROM TableVstup WHERE SyntetickyaleboFiktivny NOT LIKE @Synteticky1 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky2 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky3 " + ekonomickaKlasifikacia + " AND KalendarnyDen LIKE @Datum ORDER BY Zdroj", Con);

                    if (typ == "MRP")
                    {
                        //com = new SqlCommand("SELECT DISTINCT Zdroj FROM TableVstup WHERE SyntetickyaleboFiktivny NOT LIKE @Synteticky1 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky2 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky3 AND (Kategoria LIKE '2%' OR Kategoria LIKE '3%' OR Kategoria LIKE '4%' OR Kategoria LIKE '5%') AND KalendarnyDen LIKE @Datum ORDER BY Zdroj", Con);
                        com.Parameters.AddWithValue("@Synteticky1", synteticke[0]);
                        com.Parameters.AddWithValue("@Synteticky2", synteticke[2]);
                        com.Parameters.AddWithValue("@Synteticky3", synteticke[4]);
                    }
                    if (typ == "MRV")
                    {
                        //com = new SqlCommand("SELECT DISTINCT Zdroj FROM TableVstup WHERE SyntetickyaleboFiktivny NOT LIKE @Synteticky1 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky2 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky3 AND (Kategoria LIKE '6%' OR Kategoria LIKE '7%' OR Kategoria LIKE '8%' OR Kategoria LIKE '9%') AND KalendarnyDen LIKE @Datum ORDER BY Zdroj", Con);
                        com.Parameters.AddWithValue("@Synteticky1", synteticke[1]);
                        com.Parameters.AddWithValue("@Synteticky2", synteticke[3]);
                        com.Parameters.AddWithValue("@Synteticky3", synteticke[5]);
                    }
                }
                com.Parameters.AddWithValue("@Datum", datum);

                using (SqlDataAdapter da = new SqlDataAdapter(com))
                {
                    da.Fill(dT);
                    com.ExecuteNonQuery();
                }

                //ZDROJE DO ARRAYU
                List<string> zdrojeList = new List<string>();
                int pocetZdrojov = 0;
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

                string[] arrZdroje = new string[pocetZdrojov];
                arrZdroje = zdrojeList.ToArray();

                int typSynteticky = Array.IndexOf(synteticke, typ);
                string nameTabulka = zistiNazovZakladne(typSynteticky, prijmyVydavkyVeda,lenOkruhy);

                string name = "RZBT";
                //AK MRZ PRIDA M DO NAZVU
                if (typ == synteticke[2] || typ == synteticke[3] || typ == synteticke[6] || typ == synteticke[7])
                    name = name + "M";
                name = name + nameTabulka + "z.xls";
                
                //PODLA DATUMU ZISTI KVARTAL A ZAROVEN VYTVORI ADRESARE AK ESTE NIESU PRE DANY ROK A KVARTAL
                string kvartal = vytvorAdresare(datum);
                //hl345zv3 JE HLAVICKA ZOSTAVY PRE TENTO TYP
                System.IO.File.Copy("..\\..\\Hlav\\RZB\\hl345zv3.xls", "Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name, true);
                Excel.Application app = new Excel.Application();
                string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                Excel.Workbook workbook = app.Workbooks.Open(path + "\\" + "bin\\Debug\\Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name);
                Excel._Worksheet worksheet = workbook.Sheets[1];
                Excel.Range range = worksheet.UsedRange;
                Excel.Range borderRange;
                Excel._Worksheet worksheetFirst = workbook.Sheets[1];

                //VYPISANIE HLAVICKY KTORA SA BUDE KOPIROVAT
                //TEXTY V EXCELI
                if (typ == synteticke[0] || typ == synteticke[1] || typ == synteticke[2] || typ == synteticke[3])
                {
                    worksheet.Cells[1, 6] = "Príspevkové organizácie";

                    if (typ == synteticke[0] || typ == synteticke[2])
                        worksheet.Cells[2, 1] = "Plnenie príjmov na príspevkových organizáciách";
                    else
                        worksheet.Cells[2, 1] = "Výdavky školstva na príspevkových organizáciach";
                }
                else
                {
                    worksheet.Cells[1, 6] = "Rozpočtové organizácie";
                    if (typ == synteticke[4] || typ == "MRP")
                    { 
                        if(lenOkruhy==false)
                            worksheet.Cells[2, 1] = "Plnenie príjmov na rozpočtových organizáciách";
                        else
                            worksheet.Cells[2, 1] = "Plnenie príjmov za rezort školstva";
                    }
                        
                    else
                    {
                        if (typ == synteticke[5] || typ == "MRV")
                        {
                            if (lenOkruhy == false)
                                worksheet.Cells[2, 1] = "Výdavky školstva na rozpočtových organizáciach";
                            else
                                worksheet.Cells[2, 1] = "Výdavky za rezort školstva";

                            if (prijmyVydavkyVeda == 3)
                                worksheet.Cells[2, 1] = "Výdavky školstva na rozpočtových organizáciach za vedu";
                            if (prijmyVydavkyVeda == 4)
                                worksheet.Cells[2, 1] = "Výdavky školstva na rozpočtových organizáciach okrem vedy";
                        }
                    }
                }

                string doplnkovyText = "";
                if (typ == synteticke[0] || typ == synteticke[2])
                    doplnkovyText = "Príjmy";
                else if (typ == synteticke[1] || typ == synteticke[3])
                        doplnkovyText = "Výdavky";
                    else if (typ == synteticke[6] || typ == synteticke[7])
                        doplnkovyText = "Mimorozpočtové účty";

                if (typ == synteticke[2] || typ == synteticke[3])
                    doplnkovyText += " nerozpočtované";
                else if (typ == synteticke[0] || typ == synteticke[1])
                        doplnkovyText += " rozpočtované";

                worksheet.Cells[5, 1] = doplnkovyText;
                worksheet.Cells[6, 2] = datum;
                worksheet.Cells[6, (int)right_char - 65 + 1] = "Strana: 1";
                if (typVystupov == 1)
                    worksheet.Cells[4, 1] = "(údaje sú v €)";
                else
                    worksheet.Cells[4, 1] = "(údaje sú v tisícoch €)";

                //CYKLUS PRE KAZDY ZDROJ
                for (int i = 0; i < pocetZdrojov; i++)
                {
                    //VYTVARANIE NOVEHO ZOSITU PLUS KOPIROVANIE HLAVICKY 
                    //VACSIE AKO 0 LEBO PRVY OTVARAM UZ PRED CYKLOM ABY SOM NASTAVIL HLAVICKU KTORU POTOM KOPIRUJEM
                    if (i > 0)
                    {
                        workbook.Sheets[1].Copy(Type.Missing, workbook.Sheets[workbook.Sheets.Count]);
                        worksheet = workbook.Sheets[workbook.Sheets.Count];
                        int help = worksheet.Rows.Count;
                        range = worksheet.get_Range("A10", "G" + help);
                        range.Clear();
                    }
                    
                    int rowCount = range.Rows.Count;
                    int start = right_num + 1;
                    int border;
                    int strana = 2;
                    //DOPLNENIE NAZVU TABULKY DO EXCELU ZA KONKRETNY ZDROJ
                    if (arrZdroje[i] != "7%")
                    {
                        worksheet.Cells[6, 6] = nameTabulka + "z" + arrZdroje[i];
                        worksheet.Name = arrZdroje[i];
                    }
                    else
                    {
                        worksheet.Cells[6, 6] = nameTabulka + "z7";
                        worksheet.Name = "7";
                    }

                    //NAZOV ZDROJA A VYPLNENIE KOLONIEK ROVNAKO PRE VSETKY
                    com = new SqlCommand("SELECT Text FROM ciselnikZdroje WHERE Zdroj LIKE @Zdroj", Con);
                    com.Parameters.AddWithValue("@Zdroj", arrZdroje[i] + "%");
                    dT = new DataTable();

                    using (SqlDataAdapter da = new SqlDataAdapter(com))
                    {
                        da.Fill(dT);
                        com.ExecuteNonQuery();
                    }
                    if(arrZdroje[i] != "7%")
                        worksheet.Cells[3, 1] = "za zdroj " + arrZdroje[i] + " - " + (string)dT.Rows[0][0];
                    else
                        worksheet.Cells[3, 1] = "za zdroj 7 - " + (string)dT.Rows[0][0];

                    string[] arrIco;
                    int j = 0;
                    if (lenOkruhy == false)
                    {
                        //VSETKY ICO PRE JEDNOTLIVY ZDROJ
                        if (typ != "MRP" && typ != "MRV")
                        {
                            com = new SqlCommand("SELECT DISTINCT tv.Ico,ci.CelyNazov collate Slovak_CI_AS FROM TableVstup tv JOIN ciselnikIco ci ON tv.Ico=ci.Ico WHERE tv.SyntetickyaleboFiktivny LIKE @Synteticky " + veda + " " + ekonomickaKlasifikacia + " AND tv.Zdroj LIKE @Zdroj AND KalendarnyDen LIKE @Datum ORDER BY ci.CelyNazov collate Slovak_CI_AS", Con);
                            com.Parameters.AddWithValue("@Synteticky", typ);
                        }
                        else
                        {
                            com = new SqlCommand("SELECT DISTINCT tv.Ico,ci.CelyNazov FROM TableVstup tv JOIN ciselnikIco ci ON tv.Ico=ci.Ico WHERE SyntetickyaleboFiktivny NOT LIKE @Synteticky1 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky2 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky3 " + veda + " " + ekonomickaKlasifikacia + " AND Zdroj LIKE @Zdroj AND KalendarnyDen LIKE @Datum", Con);

                            if (typ == "MRP")
                            {
                                //com = new SqlCommand("SELECT DISTINCT tv.Ico,ci.CelyNazov FROM TableVstup tv JOIN ciselnikIco ci ON tv.Ico=ci.Ico WHERE SyntetickyaleboFiktivny NOT LIKE @Synteticky1 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky2 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky3 AND (Kategoria LIKE '2%' OR Kategoria LIKE '3%' OR Kategoria LIKE '4%' OR Kategoria LIKE '5%') AND Zdroj LIKE @Zdroj AND KalendarnyDen LIKE @Datum", Con);
                                com.Parameters.AddWithValue("@Synteticky1", synteticke[0]);
                                com.Parameters.AddWithValue("@Synteticky2", synteticke[2]);
                                com.Parameters.AddWithValue("@Synteticky3", synteticke[4]);
                            }
                            if (typ == "MRV")
                            {
                                //com = new SqlCommand("SELECT DISTINCT tv.Ico,ci.CelyNazov FROM TableVstup tv JOIN ciselnikIco ci ON tv.Ico=ci.Ico WHERE SyntetickyaleboFiktivny NOT LIKE @Synteticky1 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky2 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky3 AND (Kategoria LIKE '6%' OR Kategoria LIKE '7%' OR Kategoria LIKE '8%' OR Kategoria LIKE '9%') AND Zdroj LIKE @Zdroj AND KalendarnyDen LIKE @Datum", Con);
                                com.Parameters.AddWithValue("@Synteticky1", synteticke[1]);
                                com.Parameters.AddWithValue("@Synteticky2", synteticke[3]);
                                com.Parameters.AddWithValue("@Synteticky3", synteticke[5]);
                            }
                        }
                        //NASTAVENIE ZDROJA JE PRE VSETKY SELECTY VYSSIE ROVNAKE
                        com.Parameters.AddWithValue("@Zdroj", arrZdroje[i]);
                        com.Parameters.AddWithValue("@Datum", datum);
                        dT = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(com))
                        {
                            da.Fill(dT);
                            com.ExecuteNonQuery();
                        }

                        // NASTAVENIE VELKOSTI POLA PODLA TOHO CI TAM BUDE OPRO -MSSR / AK AJ SUCET ZA PRISPEVKOVE TAK VZDY +2 V INICIALIZACII POLA
                        if (typ == synteticke[4] || typ == synteticke[5] || typ == "MRP" || typ == "MRV")
                            arrIco = new string[dT.Rows.Count + 2];
                        else
                            arrIco = new string[dT.Rows.Count + 1];
                        
                        //ICA DO ARRAYU
                        foreach (DataRow row in dT.Rows)
                        {
                            arrIco[j] = (string)row[0];
                            j++;
                        }
                        //PRIDANIE JEDNEHO ALEBO DVOCH SUMAROV AKO JE TREBA - VERZIA BEZ SUCTU ZA PRISPEVKOVE
                        arrIco[j] = "Celkovo";
                        if (typ == synteticke[4] || typ == synteticke[5] || typ == "MRP" || typ == "MRV")
                            arrIco[j + 1] = "OPRO - MŠVVaŠ SR";
                    }
                    else
                    {
                        arrIco = new string[1];
                        arrIco[0] = "Ostatné priamo riadené organizácie + MŠVVaŠ SR (Vrátane PJ)";
                    }

                    //PRIDANIE DVOCH SUMAROV NAKONIEC - VERZIA SO SUCTOM ZA PRISPEVKOVE
                    //if (typ == "PRR" || typ == "VYR")
                    //    arrIco[j++] = "Príspev.organizácie";
                    //else
                    //    arrIco[j++] = "OPRO - MŠVVaŠ SR";
                    //arrIco[j--] = "Celkovo";

                    Boolean opro = false;
                    //CYKLUS PRE KAZDE ICO PRI JEDNOTLIVOM ZDROJI
                    foreach (string helpString in arrIco)
                    {
                        SqlCommand get_name = new SqlCommand("SELECT Nazov from ciselnikIco where Ico = '" + helpString + "'", Con);
                        DataTable get_name_table = new DataTable();
                        using (SqlDataAdapter get_name_adapter = new SqlDataAdapter(get_name))
                        {
                            get_name_adapter.Fill(get_name_table);
                            get_name.ExecuteNonQuery();
                        }

                        border = start;
                        //NAZOV DO EXCELU PLUS KONTROLA CI NEROBI FINALNY SUMAR
                        if (get_name_table.Rows.Count != 0)
                        {
                            worksheet.Cells[start, 1] = (string)get_name_table.Rows[0][0];
                        }
                        else
                        {
                            worksheet.Cells[start, 1] = arrIco[j];
                            if (opro == false && j == (arrIco.Length - 1) && lenOkruhy==false)
                                opro = true;
                            else
                                opro = false;
                        }

                        //PODMIENKA CI ROBI OBYCAJNY SELECT SO ZADANYM TYPOM AKO SYNTETICKY UCET ALEBO ROBI MRZ S NEZNAMYM SYNTETICKYM UCTOM A PODLA KATEGORII
                        if (typ != "MRP" && typ != "MRV")
                        {
                            com = new SqlCommand("SELECT Kategoria,SUM(SchvalenyRozpocet),SUM(RozpocetPoZmenach),SUM(Skutocnost) FROM TableVstup WHERE Zdroj LIKE @Zdroj AND SyntetickyaleboFiktivny LIKE @Synteticky " + veda + " " + ekonomickaKlasifikacia + " AND Ico LIKE @Ico AND KalendarnyDen LIKE @Datum GROUP BY Kategoria ORDER BY Kategoria", Con);
                            com.Parameters.AddWithValue("@Synteticky", typ);
                        }
                        else
                        {
                            com = new SqlCommand("SELECT Kategoria,SUM(SchvalenyRozpocet),SUM(RozpocetPoZmenach),SUM(Skutocnost) FROM TableVstup WHERE Zdroj LIKE @Zdroj AND SyntetickyaleboFiktivny NOT LIKE @Synteticky1 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky2 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky3 " + veda + " " + ekonomickaKlasifikacia + " AND Ico LIKE @Ico AND KalendarnyDen LIKE @Datum GROUP BY Kategoria ORDER BY Kategoria", Con);

                            if (typ == "MRP")
                            {
                                //com = new SqlCommand("SELECT Kategoria,SUM(SchvalenyRozpocet),SUM(RozpocetPoZmenach),SUM(Skutocnost) FROM TableVstup WHERE Zdroj LIKE @Zdroj AND SyntetickyaleboFiktivny NOT LIKE @Synteticky1 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky2 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky3 AND (Kategoria LIKE '2%' OR Kategoria LIKE '3%' OR Kategoria LIKE '4%' OR Kategoria LIKE '5%') AND Ico LIKE @Ico AND KalendarnyDen LIKE @Datum GROUP BY Kategoria ORDER BY Kategoria", Con);
                                com.Parameters.AddWithValue("@Synteticky1", synteticke[0]);
                                com.Parameters.AddWithValue("@Synteticky2", synteticke[2]);
                                com.Parameters.AddWithValue("@Synteticky3", synteticke[4]);
                            }
                            if (typ == "MRV")
                            {
                                //com = new SqlCommand("SELECT Kategoria,SUM(SchvalenyRozpocet),SUM(RozpocetPoZmenach),SUM(Skutocnost) FROM TableVstup WHERE Zdroj LIKE @Zdroj AND SyntetickyaleboFiktivny NOT LIKE @Synteticky1 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky2 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky3 AND (Kategoria LIKE '6%' OR Kategoria LIKE '7%' OR Kategoria LIKE '8%' OR Kategoria LIKE '9%') AND Ico LIKE @Ico AND KalendarnyDen LIKE @Datum GROUP BY Kategoria ORDER BY Kategoria", Con);
                                com.Parameters.AddWithValue("@Synteticky1", synteticke[1]);
                                com.Parameters.AddWithValue("@Synteticky2", synteticke[3]);
                                com.Parameters.AddWithValue("@Synteticky3", synteticke[5]);
                            }
                        }

                        //NASTAVENIE ZDROJA A ICA JE ROVNAKE PRE VSETKY SELECTY VYSSIE
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

                        int comparison = 0;
                        //INICIALIZACIA POLI NA UKLADANIE MEDZISUCTOV, celsum=konecny sucet za ico, LokSum=sum za jednotlive velke kategorie-2,3,6 , hsum = sum za sucet 2 a 3 priklad
                        int sum = Int32.Parse(dT.Rows[0][0].ToString()) / 100;
                        double[] CelSum = new double[3];
                        double[] LokSum = new double[3];
                        double[] HSum = new double[3];
                        double[] RO6Sum = new double[3];
                        int counter = 0;
                        Boolean controlVypisHSum = false;
                        //CYKLUS PRE VSETKY UDAJE PRE DANE ICO A DANY ZDROJ
                        foreach (DataRow row in dT.Rows)
                        {
                            int index_column = 2;
                            comparison = 0;
                            if (opro == true)
                            {
                                SqlCommand comOpro = new SqlCommand();

                                //ZISTENIE HODNOT MSVS PRE OPRO - MSVS
                                //PODMIENKA CI ROBI OBYCAJNY SELECT SO ZADANYM TYPOM AKO SYNTETICKY UCET ALEBO ROBI MRZ S NEZNAMYMI SYNTETICKYMI UCTAMI A PODLA KATEGORII
                                if (typ != "MRP" && typ != "MRV")
                                {
                                    comOpro = new SqlCommand("SELECT Kategoria,SUM(SchvalenyRozpocet),SUM(RozpocetPoZmenach),SUM(Skutocnost) FROM TableVstup WHERE Zdroj LIKE @Zdroj AND SyntetickyaleboFiktivny LIKE @Synteticky " + veda + " " + ekonomickaKlasifikacia + " AND Ico LIKE @Ico AND KalendarnyDen LIKE @Datum GROUP BY Kategoria", Con);
                                    comOpro.Parameters.AddWithValue("@Synteticky", typ);
                                }
                                else
                                {
                                    comOpro = new SqlCommand("SELECT Kategoria,SUM(SchvalenyRozpocet),SUM(RozpocetPoZmenach),SUM(Skutocnost) FROM TableVstup WHERE Zdroj LIKE @Zdroj AND SyntetickyaleboFiktivny NOT LIKE @Synteticky1 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky2 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky3 " + veda + " " + ekonomickaKlasifikacia + " AND Ico LIKE @Ico AND KalendarnyDen LIKE @Datum GROUP BY Kategoria", Con);

                                    if (typ == "MRP")
                                    {
                                        //comOpro = new SqlCommand("SELECT Kategoria,SUM(SchvalenyRozpocet),SUM(RozpocetPoZmenach),SUM(Skutocnost) FROM TableVstup WHERE Zdroj LIKE @Zdroj AND SyntetickyaleboFiktivny NOT LIKE @Synteticky1 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky2 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky3 AND (Kategoria LIKE '2%' OR Kategoria LIKE '3%' OR Kategoria LIKE '4%' OR Kategoria LIKE '5%') AND Ico LIKE @Ico AND KalendarnyDen LIKE @Datum GROUP BY Kategoria", Con);
                                        comOpro.Parameters.AddWithValue("@Synteticky1", synteticke[0]);
                                        comOpro.Parameters.AddWithValue("@Synteticky2", synteticke[2]);
                                        comOpro.Parameters.AddWithValue("@Synteticky3", synteticke[4]);
                                    }
                                    if (typ == "MRV")
                                    {
                                        //comOpro = new SqlCommand("SELECT Kategoria,SUM(SchvalenyRozpocet),SUM(RozpocetPoZmenach),SUM(Skutocnost) FROM TableVstup WHERE Zdroj LIKE @Zdroj AND SyntetickyaleboFiktivny NOT LIKE @Synteticky1 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky2 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky3 AND (Kategoria LIKE '6%' OR Kategoria LIKE '7%' OR Kategoria LIKE '8%' OR Kategoria LIKE '9%') AND Ico LIKE @Ico AND KalendarnyDen LIKE @Datum GROUP BY Kategoria", Con);
                                        comOpro.Parameters.AddWithValue("@Synteticky1", synteticke[1]);
                                        comOpro.Parameters.AddWithValue("@Synteticky2", synteticke[3]);
                                        comOpro.Parameters.AddWithValue("@Synteticky3", synteticke[5]);
                                    }
                                }
                                //NASTAVENIE ICA A ZDROJU JE ROVNAKE PRE VSETKY SELECTY VYSSIE
                                comOpro.Parameters.AddWithValue("@Ico", MSVSico);
                                comOpro.Parameters.AddWithValue("@Zdroj", arrZdroje[i]);
                                comOpro.Parameters.AddWithValue("@Datum", datum);

                                DataTable dTOpro = new DataTable();
                                using (SqlDataAdapter da = new SqlDataAdapter(comOpro))
                                {
                                    da.Fill(dTOpro);
                                    comOpro.ExecuteNonQuery();
                                }

                                foreach (DataRow rowOpro in dTOpro.Rows)
                                {
                                    if ((comparison = rowOpro[0].ToString().CompareTo(row[0])) == 0)
                                    {
                                        for (int l = 1; l < 4; l++)
                                            row[l] = (Double.Parse(row[l].ToString()) - Double.Parse(rowOpro[l].ToString())).ToString();
                                    }
                                }
                            }
                            //PREPISE KATEGORIU DO EXCELU
                            worksheet.Cells[start, index_column++] = row[0];

                            //PRIRATAVANIE HODNOT + PISANIE HODNOT DO EXCELU
                            for (int k = 0; k < 3; k++)
                            {
                                worksheet.Cells[start, index_column++] = (double)row[k + 1] / typVystupov;
                                CelSum[k] = CelSum[k] + (double)row[k + 1];
                                LokSum[k] = LokSum[k] + (double)row[k + 1];
                                HSum[k] = HSum[k] + (double)row[k + 1];
                            }

                            if (prijmyVydavkyVeda != 1 && typ != synteticke[1] && typ != synteticke[3] && (Int32.Parse(dT.Rows[counter][0].ToString()) / 10 == 64))
                                for (int k = 0; k < 3; k++)
                                    RO6Sum[k] = RO6Sum[k] + (double)row[k + 1];

                            //TEST PRI PERCENTACH AK MA DELIT 0 NECH NESPADNE ALE ZAPISE NULU
                            for (int k = 1; k < 3; k++)
                            {
                                if ((double)row[k] == 0)
                                    worksheet.Cells[start, index_column++] = 0;
                                else
                                    worksheet.Cells[start, index_column++] = 100 * (double)row[3] / (double)row[k];
                            }
                            increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 20, 20);
                            counter++;

                            //TEST CI SA NEMENI SKUPINA KATEGORII A NETREBA VYPISAT MEDZISUCET 
                            if (counter == dT.Rows.Count || sum != (Int32.Parse(dT.Rows[counter][0].ToString()) / 100))
                            {
                                //6-64 DOPLNENIE
                                if (prijmyVydavkyVeda != 1 && typ != synteticke[1] && typ != synteticke[3] && sum == 6)
                                {
                                    index_column = 2;
                                    worksheet.Cells[start, index_column].NumberFormat = "@";
                                    worksheet.Cells[start, index_column++] = "6-64";

                                    for (int k = 0; k < 3; k++)
                                    {
                                        if (LokSum[k] - RO6Sum[k] == 0)
                                            worksheet.Cells[start, index_column++] = LokSum[k] - RO6Sum[k];
                                        else
                                            worksheet.Cells[start, index_column++] = (double)(LokSum[k] - RO6Sum[k]) / typVystupov;
                                    }

                                    for (int k = 0; k < 2; k++)
                                    {
                                        if (LokSum[k] - RO6Sum[k] == 0)
                                            worksheet.Cells[start, index_column++] = 0;
                                        else
                                            worksheet.Cells[start, index_column++] = (double)(LokSum[2] - RO6Sum[2]) / (LokSum[k] - RO6Sum[k]) * 100;
                                    }

                                    increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 20, 20);
                                }

                                index_column = 2;
                                worksheet.Cells[start, index_column++] = sum;

                                for (int k = 0; k < 3; k++)
                                {
                                    if ((double)LokSum[k] == 0)
                                        worksheet.Cells[start, index_column++] = LokSum[k];
                                    else
                                        worksheet.Cells[start, index_column++] = (double)LokSum[k] / typVystupov;
                                }

                                for (int k = 0; k < 2; k++)
                                {
                                    if (LokSum[k] == 0)
                                        worksheet.Cells[start, index_column++] = 0;
                                    else
                                        worksheet.Cells[start, index_column++] = (double)LokSum[2] / LokSum[k] * 100;
                                }
                                for (int k = 0; k < 3; k++)
                                    LokSum[k] = 0;

                                increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 20, 20);
                                if (counter == dT.Rows.Count)
                                    sum = 0;
                                else
                                    sum = Int32.Parse(dT.Rows[counter][0].ToString()) / 100;

                            }

                            //TEST CI NEMA ROBIT MEDZISUCET ZA VIACERO KATEGORII
                            if ((sum == 0 || sum == 4 || sum == 8) && controlVypisHSum == false && typ != synteticke[2] && typ != synteticke[3])
                            {
                                increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 20, 20);
                                index_column = 2;
                                if (prijmyVydavkyVeda == 1)
                                    worksheet.Cells[start, index_column++] = "2xx,3xx";
                                else
                                    worksheet.Cells[start, index_column++] = "6xx,7xx";

                                for (int k = 0; k < 3; k++)
                                {
                                    if ((double)HSum[k] == 0)
                                        worksheet.Cells[start, index_column++] = (double)HSum[k];
                                    else
                                        worksheet.Cells[start, index_column++] = (double)HSum[k] / typVystupov;
                                }

                                for (int k = 0; k < 2; k++)
                                {
                                    if (HSum[k] == 0)
                                        worksheet.Cells[start, index_column++] = 0;
                                    else
                                        worksheet.Cells[start, index_column++] = (double)HSum[2] / HSum[k] * 100;
                                }
                                increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 20, 20);
                                controlVypisHSum = true;
                            }
                        }

                        //SPOLU ZA ICO
                        get_name = new SqlCommand("SELECT SkratenyNazov FROM ciselnikIco WHERE Ico = '" + helpString + "'", Con);
                        get_name_table = new DataTable();
                        using (SqlDataAdapter get_name_adapter = new SqlDataAdapter(get_name))
                        {
                            get_name_adapter.Fill(get_name_table);
                            get_name.ExecuteNonQuery();
                        }
                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 20, 20);

                        //SKRATENY NAZOV DO EXCELU PLUS KONTROLA CI NEROBI NEJAKY KONECNY SUMAR
                        if (get_name_table.Rows.Count != 0)
                            worksheet.Cells[start, 1] = "Spolu " + (string)get_name_table.Rows[0][0];
                        else
                        {
                            if (arrIco[j] == "Ostatné priamo riadené organizácie + MŠVVaŠ SR (Vrátane PJ)")
                                worksheet.Cells[start, 1] = "Spolu OPRO";
                            else
                                worksheet.Cells[start, 1] = "Spolu " + arrIco[j];
                            j++;
                        }

                        for (int k = 0; k < 3; k++)
                        {
                            if ((double)CelSum[k] == 0)
                                worksheet.Cells[start, k + 3] = (double)CelSum[k];
                            else
                                worksheet.Cells[start, k + 3] = (double)CelSum[k] / typVystupov;
                        }

                        for (int k = 0; k < 2; k++)
                        {
                            if (CelSum[k] == 0)
                                worksheet.Cells[start, k + 6] = 0;
                            else
                                worksheet.Cells[start, k + 6] = (double)CelSum[2] / CelSum[k] * 100;
                        }
                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 20, 20);
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
                            if (c == left_char + 1)
                            {
                                borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                borderRange.NumberFormat = "@";
                            }
                        }

                        //HLAVNE ORAMOVANIE
                        borderRange = worksheet.get_Range(left_char.ToString() + border, right_char.ToString() + start);//hrube po Celkovo
                        borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                        if ((helpString == "Celkovo" && opro == true) || helpString == "OPRO - MŠVVaŠ SR")
                        {
                            var culture = new CultureInfo("ru-RU");
                            worksheet.Cells[++start, 1] = "Dátum vytvorenia: " + time.ToString(culture);
                        }
                        else
                            increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 20, 20);
                    }
                }
                //ZAVRIE EXCEL
                workbook.Save();
                if (priamaTlac)
                    PrintMyExcelFile(workbook);
                Kontrola.release_excel(range, worksheet, workbook, app);
                Con.Close();
            }
        }

        public void spravDruheZostavyMimorozpoctoveUcty(string typ, string MSVSico, int prijmyVydavkyVeda, Boolean lenOkruhy, Boolean lenMSVS, string datum, int typVystupov, string zaokruhlenieMena, string zaokruhleniePercenta, char left_char, int left_num, char right_char, int right_num, int count_for_page, int strana_rel_pos, DateTime time, bool priamaTlac)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            string[] synteticke = najdiSynteticke(connectionstring);
            string ekonomickaKlasifikacia = zistiEkonomickuKlasifikaciu(prijmyVydavkyVeda, 1, connectionstring);
            //AK MA ROBIT VEDU TAK Z DATABAZY VYTIAHNE VYNIMKY VEDY
            string veda = "";
            string doplnokMSVS = "";
            if (lenMSVS)
                doplnokMSVS = "AND tv.Ico LIKE '" + MSVSico + "%' AND tv.Podtrieda LIKE '9800'";
            if (prijmyVydavkyVeda == 3 || prijmyVydavkyVeda == 4)
                veda = vynimkyVedy(prijmyVydavkyVeda, connectionstring);

            SqlCommand com = null;

            DataTable dT = new DataTable();
            using (var Con = new SqlConnection(connectionstring))
            {
                Con.Open();

                //PODMIENKA CI ROBI OBYCAJNY SELECT S KONKRETNYM SYNTETICKYM UCTOM ALEBO PRE MRZ S NEZNAMIM SYNTETICKYM UCTOM A PODLA KATEGORII ALEBO VEDU
                if (typ != "MRP" && typ != "MRV")
                {
                    com = new SqlCommand("SELECT DISTINCT Zdroj FROM TableVstup tv WHERE SyntetickyaleboFiktivny LIKE @Synteticky AND KalendarnyDen LIKE @Datum " + doplnokMSVS + veda + " ORDER BY Zdroj", Con);
                    com.Parameters.AddWithValue("@Synteticky", typ);
                }
                else
                {
                    com = new SqlCommand("SELECT DISTINCT Zdroj FROM TableVstup tv WHERE SyntetickyaleboFiktivny NOT LIKE @Synteticky1 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky2 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky3 " + doplnokMSVS +  ekonomickaKlasifikacia + " AND KalendarnyDen LIKE @Datum ORDER BY Zdroj", Con);

                    if (typ == "MRP")
                    {
                        //com = new SqlCommand("SELECT DISTINCT Zdroj FROM TableVstup WHERE SyntetickyaleboFiktivny NOT LIKE @Synteticky1 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky2 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky3 AND (Kategoria LIKE '2%' OR Kategoria LIKE '3%' OR Kategoria LIKE '4%' OR Kategoria LIKE '5%') AND KalendarnyDen LIKE @Datum ORDER BY Zdroj", Con);
                        com.Parameters.AddWithValue("@Synteticky1", synteticke[0]);
                        com.Parameters.AddWithValue("@Synteticky2", synteticke[2]);
                        com.Parameters.AddWithValue("@Synteticky3", synteticke[4]);
                    }
                    if (typ == "MRV")
                    {
                        //com = new SqlCommand("SELECT DISTINCT Zdroj FROM TableVstup WHERE SyntetickyaleboFiktivny NOT LIKE @Synteticky1 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky2 AND SyntetickyaleboFiktivny NOT LIKE @Synteticky3 AND (Kategoria LIKE '6%' OR Kategoria LIKE '7%' OR Kategoria LIKE '8%' OR Kategoria LIKE '9%') AND KalendarnyDen LIKE @Datum ORDER BY Zdroj", Con);
                        com.Parameters.AddWithValue("@Synteticky1", synteticke[1]);
                        com.Parameters.AddWithValue("@Synteticky2", synteticke[3]);
                        com.Parameters.AddWithValue("@Synteticky3", synteticke[5]);
                    }
                }
                com.Parameters.AddWithValue("@Datum", datum);

                using (SqlDataAdapter da = new SqlDataAdapter(com))
                {
                    da.Fill(dT);
                    com.ExecuteNonQuery();
                }

                //ZDROJE DO ARRAYU
                List<string> zdrojeList = new List<string>();
                int pocetZdrojov = 0;
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

                string[] arrZdroje = new string[pocetZdrojov];
                arrZdroje = zdrojeList.ToArray();

                int typSynteticky = Array.IndexOf(synteticke, typ);
                string nameTabulka = zistiNazovZakladne(typSynteticky, prijmyVydavkyVeda, lenOkruhy);

                string name = "RZBT";
                //AK MRZ PRIDA M DO NAZVU
                if (typ == synteticke[2] || typ == synteticke[3] || typ == synteticke[6] || typ == synteticke[7])
                    name = name + "M";
                nameTabulka += "zx";
                if (lenMSVS)
                    nameTabulka += "MSVS";

                name = name + nameTabulka + ".xls";

                //PODLA DATUMU ZISTI KVARTAL A ZAROVEN VYTVORI ADRESARE AK ESTE NIESU PRE DANY ROK A KVARTAL
                string kvartal = vytvorAdresare(datum);
                //hl345zv3 JE HLAVICKA ZOSTAVY PRE TENTO TYP
                System.IO.File.Copy("..\\..\\Hlav\\RZB\\hl345zv3MU.xls", "Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name, true);
                Excel.Application app = new Excel.Application();
                app.DisplayAlerts = false;
                string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                Excel.Workbook workbook = app.Workbooks.Open(path + "\\" + "bin\\Debug\\Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name);
                Excel._Worksheet worksheet = workbook.Sheets[1];
                Excel.Range range = worksheet.UsedRange;
                Excel.Range borderRange;
                Excel._Worksheet worksheetFirst = workbook.Sheets[1];

                //VYPISANIE HLAVICKY KTORA SA BUDE KOPIROVAT
                //TEXTY V EXCELI
                if (typ == synteticke[0] || typ == synteticke[1] || typ == synteticke[2] || typ == synteticke[3])
                {
                    worksheet.Cells[1, 6] = "Príspevkové organizácie";

                    if (typ == synteticke[0] || typ == synteticke[2])
                        worksheet.Cells[2, 1] = "Plnenie príjmov na príspevkových organizáciách";
                    else
                        worksheet.Cells[2, 1] = "Výdavky školstva na príspevkových organizáciach";
                }
                else
                {
                    worksheet.Cells[1, 6] = "Rozpočtové organizácie";
                    if (typ == synteticke[4] || typ == "MRP")
                    {
                        if (lenOkruhy == false)
                            worksheet.Cells[2, 1] = "Plnenie príjmov na rozpočtových organizáciách";
                        else
                            worksheet.Cells[2, 1] = "Plnenie príjmov za rezort školstva";
                    }

                    else
                    {
                        if (typ == synteticke[5] || typ == "MRV")
                        {
                            if (lenOkruhy == false)
                                if (lenMSVS)
                                    worksheet.Cells[2, 1] = "Výdavky školstva na rozpočtových organizáciach za f.k. 0980 na MŠVVaŠ SR - úrad ";
                                else
                                    worksheet.Cells[2, 1] = "Výdavky školstva na rozpočtových organizáciach";
                            else
                                worksheet.Cells[2, 1] = "Výdavky za rezort školstva";

                            if (prijmyVydavkyVeda == 3)
                                worksheet.Cells[2, 1] = "Výdavky školstva na rozpočtových organizáciach za vedu";
                            if (prijmyVydavkyVeda == 4)
                                worksheet.Cells[2, 1] = "Výdavky školstva na rozpočtových organizáciach okrem vedy";
                        }
                    }
                }

                string doplnkovyText = "";
                if (typ == synteticke[6] || typ == synteticke[7])
                    doplnkovyText = "Mimorozpočtové účty";

                worksheet.Cells[5, 1] = doplnkovyText;
                worksheet.Cells[6, 2] = "Spracovateľské obdobie: " + datum;
                worksheet.Cells[6, (int)right_char - 65 + 1] = "Strana: 1";
                if (typVystupov == 1)
                    worksheet.Cells[4, 1] = "(údaje sú v €)";
                else
                    worksheet.Cells[4, 1] = "(údaje sú v tisícoch €)";

                //CYKLUS PRE KAZDY ZDROJ
                for (int i = 0; i < pocetZdrojov; i++)
                {
                    //VYTVARANIE NOVEHO ZOSITU PLUS KOPIROVANIE HLAVICKY 
                    //VACSIE AKO 0 LEBO PRVY OTVARAM UZ PRED CYKLOM ABY SOM NASTAVIL HLAVICKU KTORU POTOM KOPIRUJEM
                    if (i > 0)
                    {
                        workbook.Sheets[1].Copy(Type.Missing, workbook.Sheets[workbook.Sheets.Count]);
                        worksheet = workbook.Sheets[workbook.Sheets.Count];
                        int help = worksheet.Rows.Count;
                        range = worksheet.get_Range("A10", "I" + help);
                        range.Clear();
                    }

                    int rowCount = range.Rows.Count;
                    int start = right_num + 1;
                    int border = start;
                    int strana = 2;
                    //DOPLNENIE NAZVU TABULKY DO EXCELU ZA KONKRETNY ZDROJ
                    if (arrZdroje[i] != "7%")
                    {
                        worksheet.Cells[6, 8] = nameTabulka + "z" + arrZdroje[i];
                        worksheet.Name = arrZdroje[i];
                    }
                    else
                    {
                        worksheet.Cells[6, 8] = nameTabulka + "z7";
                        worksheet.Name = "7";
                    }

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

                    //Synteticke ucty
                    List<string> listSynteticke = new List<string>();
                    List<int> listUsporiadanieSynteticke = new List<int>();
                    if (typ == "MRP" || typ == "MRV")
                    {

                        com = new SqlCommand("SELECT DISTINCT tv.SyntetickyaleboFiktivny FROM TableVstup tv WHERE tv.SyntetickyaleboFiktivny NOT LIKE @Synteticky1 AND tv.SyntetickyaleboFiktivny NOT LIKE @Synteticky2 AND tv.SyntetickyaleboFiktivny NOT LIKE @Synteticky3 " + doplnokMSVS + veda + " " + ekonomickaKlasifikacia + " AND tv.KalendarnyDen LIKE @Datum AND tv.Zdroj LIKE @Zdroj ORDER BY tv.SyntetickyaleboFiktivny", Con);
                        if (typ == "MRP")
                        {
                            com.Parameters.AddWithValue("@Synteticky1", synteticke[0]);
                            com.Parameters.AddWithValue("@Synteticky2", synteticke[2]);
                            com.Parameters.AddWithValue("@Synteticky3", synteticke[4]);
                        }
                        if (typ == "MRV")
                        {
                            com.Parameters.AddWithValue("@Synteticky1", synteticke[1]);
                            com.Parameters.AddWithValue("@Synteticky2", synteticke[3]);
                            com.Parameters.AddWithValue("@Synteticky3", synteticke[5]);
                        }
                        com.Parameters.AddWithValue("@Datum", datum);
                        com.Parameters.AddWithValue("@Zdroj", arrZdroje[i]);

                        dT = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(com))
                        {
                            da.Fill(dT);
                            com.ExecuteNonQuery();
                        }

                        //Najde vsetky synteticke okrem obycajnych pre prijmy alebo vydavky podla vstupu
                        foreach (DataRow row in dT.Rows)
                            listUsporiadanieSynteticke.Add(Int32.Parse(row[0].ToString()));

                        //Usporiadanie a prehodenie na stringy
                        listUsporiadanieSynteticke.Sort();
                        listSynteticke = listUsporiadanieSynteticke.ConvertAll<string>(x => x.ToString());
                        //pridanie kumulativu
                        listSynteticke.Add("Spolu za kódy účtov");
                    }
                    else
                    {
                        //Nahodi typ z parametrov ak je jasne dany
                        listSynteticke.Add(typ);
                    }

                    //Cyklus pre všetky synteticke ucty
                    foreach (var synteticky in listSynteticke)
                    {
                        border = start;
                        borderRange = worksheet.get_Range(left_char.ToString() + start, right_char.ToString() + start);
                        borderRange.Font.Bold = true;
                        //borderRange.Clear();
                        borderRange.Merge();
                        borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                        SqlCommand get_name_synteticky = new SqlCommand("SELECT su.Text FROM SyntetickyUcet su WHERE su.SyntetickyUcet LIKE '" + synteticky.ToString() + "%' ORDER BY su.SyntetickyUcet", Con);
                        DataTable get_name_table_synteticky = new DataTable();
                        using (SqlDataAdapter get_name_adapter_synteticky = new SqlDataAdapter(get_name_synteticky))
                        {
                            get_name_adapter_synteticky.Fill(get_name_table_synteticky);
                            get_name_synteticky.ExecuteNonQuery();
                        }

                        if (get_name_table_synteticky.Rows.Count != 0)
                            worksheet.Cells[start, 1] = synteticky + ": " + get_name_table_synteticky.Rows[0][0].ToString();
                        else
                            worksheet.Cells[start, 1] = synteticky;
                        

                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 5, 8, 10, 13);

                        //Najdeme vsetky druhy rozpoctu pre synteticky ucet
                        List<string> listDruhyRozpoctov = new List<string>();
                        if (synteticky != "Spolu za kódy účtov")
                        {
                            com = new SqlCommand("SELECT DISTINCT tv.TypZdroja FROM TableVstup tv WHERE tv.SyntetickyaleboFiktivny LIKE @Synteticky " + doplnokMSVS + veda + " " + ekonomickaKlasifikacia + " AND tv.KalendarnyDen LIKE @Datum AND Zdroj LIKE @Zdroj ORDER BY tv.TypZdroja", Con);

                            com.Parameters.AddWithValue("@Synteticky", synteticky);
                            com.Parameters.AddWithValue("@Zdroj", arrZdroje[i]);
                            com.Parameters.AddWithValue("@Datum", datum);

                            dT = new DataTable();
                            using (SqlDataAdapter da = new SqlDataAdapter(com))
                            {
                                da.Fill(dT);
                                com.ExecuteNonQuery();
                            }

                            //Najde vsetky druhy rozpoctu podla vstupu
                            foreach (DataRow row in dT.Rows)
                                listDruhyRozpoctov.Add((string)row[0]);
                        }
                        //pridanie kumulativu
                        listDruhyRozpoctov.Add("Celkom");

                        foreach (var druhRozpoctu in listDruhyRozpoctov)
                        {
                            borderRange = worksheet.get_Range(((char)(left_char + 1)).ToString() + start, right_char.ToString() + start);
                            borderRange.Font.Bold = true;
                            //borderRange.Clear();
                            borderRange.Merge();

                            if (druhRozpoctu == "Celkom" && synteticky != "Spolu za kódy účtov")
                                worksheet.Cells[start, 2] = "Spolu za kód účtu " + synteticky;
                            else
                            {
                                if (druhRozpoctu == "#")
                                    worksheet.Cells[start, 2] = "Nepriradené";
                                else
                                    worksheet.Cells[start, 2] = druhRozpoctu;
                            }

                            increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 5, 8, 10, 13);

                            string[] arrIco;
                            int j = 0;
                            if (lenOkruhy == false)
                            {
                                if (synteticky != "Spolu za kódy účtov" && druhRozpoctu != "Celkom")
                                {
                                    //VSETKY ICO PRE JEDNOTLIVY ZDROJ
                                    com = new SqlCommand("SELECT DISTINCT tv.Ico,ci.CelyNazov collate Slovak_CI_AS FROM TableVstup tv JOIN ciselnikIco ci ON tv.Ico=ci.Ico WHERE tv.SyntetickyaleboFiktivny LIKE @Synteticky AND tv.TypZdroja LIKE @DruhRozpoctu " + doplnokMSVS + veda + " " + ekonomickaKlasifikacia + " AND tv.Zdroj LIKE @Zdroj AND tv.KalendarnyDen LIKE @Datum ORDER BY ci.CelyNazov collate Slovak_CI_AS", Con);
                                    com.Parameters.AddWithValue("@Synteticky", synteticky);
                                    com.Parameters.AddWithValue("@DruhRozpoctu", druhRozpoctu);
                                    com.Parameters.AddWithValue("@Zdroj", arrZdroje[i]);
                                    com.Parameters.AddWithValue("@Datum", datum);

                                    dT = new DataTable();

                                    using (SqlDataAdapter da = new SqlDataAdapter(com))
                                    {
                                        da.Fill(dT);
                                        com.ExecuteNonQuery();
                                    }

                                    arrIco = new string[dT.Rows.Count + 1];
                                    //ICA DO ARRAYU
                                    foreach (DataRow row in dT.Rows)
                                    {
                                        arrIco[j] = (string)row[0];
                                        j++;
                                    }
                                }
                                else
                                {
                                    arrIco = new string[1];
                                }
                                arrIco[j] = "Spolu za druh rozpočtu";
                            }
                            else
                            {
                                arrIco = new string[1];
                                arrIco[0] = "Ostatné priamo riadené organizácie";
                            }
                            //bool opro = false;
                            //CYKLUS PRE KAZDE ICO PRI JEDNOTLIVOM ZDROJI
                            foreach (string helpString in arrIco)
                            {
                                SqlCommand get_name = new SqlCommand("SELECT Nazov from ciselnikIco where Ico = '" + helpString + "'", Con);
                                DataTable get_name_table = new DataTable();
                                using (SqlDataAdapter get_name_adapter = new SqlDataAdapter(get_name))
                                {
                                    get_name_adapter.Fill(get_name_table);
                                    get_name.ExecuteNonQuery();
                                }

                                //NAZOV DO EXCELU PLUS KONTROLA CI NEROBI FINALNY SUMAR
                                if (get_name_table.Rows.Count != 0)
                                {
                                    worksheet.Cells[start, 3] = (string)get_name_table.Rows[0][0];
                                }
                                else
                                {
                                    if (druhRozpoctu == "Celkom")
                                        worksheet.Cells[start, 3] = helpString;
                                }

                                string querrySynteticke = "AND (SyntetickyaleboFiktivny LIKE";
                                //NASTAVENIE VYBERU ZA SYNTETICKY
                                if (synteticky == "Spolu za kódy účtov")
                                {
                                    foreach(var item in listSynteticke)
                                    {
                                        if(item != synteticky)
                                            querrySynteticke += " '" + item + "'";

                                        if (listSynteticke.IndexOf(item) < (listSynteticke.Count - 2))
                                            querrySynteticke += " OR SyntetickyaleboFiktivny LIKE";
                                    }
                                    querrySynteticke += ") ";
                                }
                                else
                                    querrySynteticke += " '" + synteticky + "') ";

                                com = new SqlCommand("SELECT Kategoria,SUM(SchvalenyRozpocet),SUM(RozpocetPoZmenach),SUM(Skutocnost) FROM TableVstup tv WHERE Zdroj LIKE @Zdroj " + querrySynteticke + doplnokMSVS + veda + " "  + ekonomickaKlasifikacia + " AND TypZdroja LIKE @DruhRozpoctu AND Ico LIKE @Ico AND KalendarnyDen LIKE @Datum GROUP BY Kategoria ORDER BY Kategoria", Con);
                                
                                //NASTAVENIE ZDROJA A ICA JE ROVNAKE PRE VSETKY SELECTY VYSSIE
                                if (druhRozpoctu == "Celkom")
                                {
                                    com.Parameters.AddWithValue("@DruhRozpoctu", "%");
                                }
                                else
                                {
                                    com.Parameters.AddWithValue("@DruhRozpoctu", druhRozpoctu);
                                }
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

                                //INICIALIZACIA POLI NA UKLADANIE MEDZISUCTOV, celsum=konecny sucet za ico, LokSum=sum za jednotlive velke kategorie-2,3,6 , hsum = sum za sucet 2 a 3 priklad
                                int sum = Int32.Parse(dT.Rows[0][0].ToString()) / 100;
                                double[] CelSum = new double[3];
                                double[] LokSum = new double[3];
                                double[] HSum = new double[3];
                                double[] RO6Sum = new double[3];
                                int counter = 0;
                                Boolean controlVypisHSum = false;
                                //CYKLUS PRE VSETKY UDAJE PRE DANE ICO A DANY ZDROJ
                                foreach (DataRow row in dT.Rows)
                                {
                                    int index_column = 4;
                                    
                                    //PREPISE KATEGORIU DO EXCELU
                                    worksheet.Cells[start, index_column++] = row[0];

                                    //PRIRATAVANIE HODNOT + PISANIE HODNOT DO EXCELU
                                    for (int k = 0; k < 3; k++)
                                    {
                                        worksheet.Cells[start, index_column++] = (double)row[k + 1] / typVystupov;
                                        CelSum[k] = CelSum[k] + (double)row[k + 1];
                                        LokSum[k] = LokSum[k] + (double)row[k + 1];
                                        HSum[k] = HSum[k] + (double)row[k + 1];
                                    }

                                    if (prijmyVydavkyVeda != 1 && typ != synteticke[1] && typ != synteticke[3] && (Int32.Parse(dT.Rows[counter][0].ToString()) / 10 == 64))
                                        for (int k = 0; k < 3; k++)
                                            RO6Sum[k] = RO6Sum[k] + (double)row[k + 1];

                                    //TEST PRI PERCENTACH AK MA DELIT 0 NECH NESPADNE ALE ZAPISE NULU
                                    for (int k = 1; k < 3; k++)
                                    {
                                        if ((double)row[k] == 0)
                                            worksheet.Cells[start, index_column++] = 0;
                                        else
                                            worksheet.Cells[start, index_column++] = 100 * (double)row[3] / (double)row[k];
                                    }
                                    increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 20, 20);
                                    counter++;

                                    //TEST CI SA NEMENI SKUPINA KATEGORII A NETREBA VYPISAT MEDZISUCET 
                                    if (counter == dT.Rows.Count || sum != (Int32.Parse(dT.Rows[counter][0].ToString()) / 100))
                                    {
                                        //6-64 DOPLNENIE
                                        if (prijmyVydavkyVeda != 1 && typ != synteticke[1] && typ != synteticke[3] && sum == 6)
                                        {
                                            index_column = 4;
                                            worksheet.Cells[start, index_column].NumberFormat = "@";
                                            worksheet.Cells[start, index_column++] = "6-64";

                                            for (int k = 0; k < 3; k++)
                                            {
                                                if (LokSum[k] - RO6Sum[k] == 0)
                                                    worksheet.Cells[start, index_column++] = LokSum[k] - RO6Sum[k];
                                                else
                                                    worksheet.Cells[start, index_column++] = (double)(LokSum[k] - RO6Sum[k]) / typVystupov;
                                            }

                                            for (int k = 0; k < 2; k++)
                                            {
                                                if (LokSum[k] - RO6Sum[k] == 0)
                                                    worksheet.Cells[start, index_column++] = 0;
                                                else
                                                    worksheet.Cells[start, index_column++] = (double)(LokSum[2] - RO6Sum[2]) / (LokSum[k] - RO6Sum[k]) * 100;
                                            }

                                            increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 20, 20);
                                        }

                                        index_column = 4;
                                        worksheet.Cells[start, index_column++] = sum;

                                        for (int k = 0; k < 3; k++)
                                        {
                                            if ((double)LokSum[k] == 0)
                                                worksheet.Cells[start, index_column++] = LokSum[k];
                                            else
                                                worksheet.Cells[start, index_column++] = (double)LokSum[k] / typVystupov;
                                        }

                                        for (int k = 0; k < 2; k++)
                                        {
                                            if (LokSum[k] == 0)
                                                worksheet.Cells[start, index_column++] = 0;
                                            else
                                                worksheet.Cells[start, index_column++] = (double)LokSum[2] / LokSum[k] * 100;
                                        }
                                        for (int k = 0; k < 3; k++)
                                            LokSum[k] = 0;

                                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 20, 20);
                                        if (counter == dT.Rows.Count)
                                            sum = 0;
                                        else
                                            sum = Int32.Parse(dT.Rows[counter][0].ToString()) / 100;

                                    }

                                    //TEST CI NEMA ROBIT MEDZISUCET ZA VIACERO KATEGORII
                                    if ((sum == 0 || sum == 4 || sum == 8) && controlVypisHSum == false && typ != synteticke[2] && typ != synteticke[3])
                                    {
                                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 20, 20);
                                        index_column = 4;
                                        if (prijmyVydavkyVeda == 1)
                                            worksheet.Cells[start, index_column++] = "2xx,3xx";
                                        else
                                            worksheet.Cells[start, index_column++] = "6xx,7xx";

                                        for (int k = 0; k < 3; k++)
                                        {
                                            if ((double)HSum[k] == 0)
                                                worksheet.Cells[start, index_column++] = (double)HSum[k];
                                            else
                                                worksheet.Cells[start, index_column++] = (double)HSum[k] / typVystupov;
                                        }

                                        for (int k = 0; k < 2; k++)
                                        {
                                            if (HSum[k] == 0)
                                                worksheet.Cells[start, index_column++] = 0;
                                            else
                                                worksheet.Cells[start, index_column++] = (double)HSum[2] / HSum[k] * 100;
                                        }
                                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 20, 20);
                                        controlVypisHSum = true;
                                    }
                                }

                                //SPOLU ZA ICO
                                get_name = new SqlCommand("SELECT SkratenyNazov FROM ciselnikIco WHERE Ico = '" + helpString + "'", Con);
                                get_name_table = new DataTable();
                                using (SqlDataAdapter get_name_adapter = new SqlDataAdapter(get_name))
                                {
                                    get_name_adapter.Fill(get_name_table);
                                    get_name.ExecuteNonQuery();
                                }
                                increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 20, 20);

                                //SKRATENY NAZOV DO EXCELU PLUS KONTROLA CI NEROBI NEJAKY KONECNY SUMAR
                                if (get_name_table.Rows.Count != 0)
                                    worksheet.Cells[start, 3] = "Spolu " + (string)get_name_table.Rows[0][0];
                                else
                                {
                                    if (synteticky == "Spolu za kódy účtov" || helpString == "Spolu za druh rozpočtu")
                                        worksheet.Cells[start, 3] = "Celkom";
                                    else
                                        worksheet.Cells[start, 3] = "Celkom org. " + arrIco[j];

                                    //if (arrIco[j] == "Ostatné priamo riadené organizácie")
                                    //    worksheet.Cells[start, 3] = "Spolu OPRO";
                                    //else
                                    //    worksheet.Cells[start, 3] = "Spolu " + arrIco[j];
                                    j++;
                                }

                                for (int k = 0; k < 3; k++)
                                {
                                    if ((double)CelSum[k] == 0)
                                        worksheet.Cells[start, k + 5] = (double)CelSum[k];
                                    else
                                        worksheet.Cells[start, k + 5] = (double)CelSum[k] / typVystupov;
                                }

                                for (int k = 0; k < 2; k++)
                                {
                                    if (CelSum[k] == 0)
                                        worksheet.Cells[start, k + 8] = 0;
                                    else
                                        worksheet.Cells[start, k + 8] = (double)CelSum[2] / CelSum[k] * 100;
                                }
                                increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 20, 20);
                                //ORAMOVANIE V EXCELE
                                //JEDNOTLIVE STLPCE
                                for (char c = left_char; c <= (char)((int)right_char); c++)//tenke oramovanie od border po start
                                {
                                    borderRange = worksheet.get_Range(c.ToString() + border, c.ToString() + start);
                                    borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                                    //ZAOKRUHLOVANIE
                                    //CISLA
                                    if (c > left_char + 3 && c < left_char + 7)
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
                                    //ZAROVNANIE KATEGORIE
                                    if (c <= left_char + 3)
                                    {
                                        if (c == left_char + 3)
                                            borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                        else
                                            borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                                        borderRange.NumberFormat = "@";
                                    }
                                }

                                //HLAVNE ORAMOVANIE
                                borderRange = worksheet.get_Range(left_char.ToString() + border, right_char.ToString() + start);//hrube po Celkovo
                                borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                                //if ((helpString == "Celkovo" && opro == true) || helpString == "OPRO - MŠVVaŠ SR")
                                //{
                                //    var culture = new CultureInfo("ru-RU");
                                //    worksheet.Cells[++start, 1] = "Dátum vytvorenia: " + time.ToString(culture);
                                //}
                                //else
                                if(!(helpString == "Celkovo" && synteticky == "Spolu za kódy účtov" && druhRozpoctu == "Celkom"))
                                    increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 3, 6, 20, 20);
                            }
                        }
                        //HLAVNE ORAMOVANIE KAPITOLY SYNTETICKOEHO
                        borderRange = worksheet.get_Range(left_char.ToString() + border, right_char.ToString() + (start - 1));//hrube po Celkovo
                        borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                        if (synteticky == "Spolu za kódy účtov")
                            borderRange.Font.Bold = true;
                    }
                    ////HLAVNE ORAMOVANIE
                    borderRange = worksheet.get_Range(left_char.ToString() + border, right_char.ToString() + (start-1));//hrube po Celkovo
                    borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                    //if ((helpString == "Celkovo" && opro == true) || helpString == "OPRO - MŠVVaŠ SR")
                    //{
                    var culture = new CultureInfo("ru-RU");
                    worksheet.Cells[++start, 1] = "Dátum vytvorenia: " + time.ToString(culture);
                }
                //ZAVRIE EXCEL
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
