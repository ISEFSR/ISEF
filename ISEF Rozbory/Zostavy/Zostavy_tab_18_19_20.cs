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
    class Zostavy_tab_18_19_20 : Zostavy
    {
        public void spravTab_18_19_20(string typ, string cislo, string MSVSico, string datum, int typVystupov, string zaokruhlenieMena, string zaokruhleniePercenta, char left_char, int left_num, char right_char, int right_num, int count_for_page, int strana_rel_pos, DateTime time, bool priamaTlac)
        //cislo 18 = tab 18
        //cislo 19 = tab 19
        //cislo 19a = tab 19a - to iste, len nie su len rozpoctove ale vsetky
        //cislo 20 = tab 20
        {
            string connectionstring = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            string selectString = "";

            using (var Con = new SqlConnection(connectionstring))
            {
                Con.Open();
                string nameTabulka = "Tab_" + cislo;

                string name = "UCET_";
                name = name + nameTabulka + ".xls";

                string kvartal = vytvorAdresare(datum);

                System.IO.File.Copy("..\\..\\Hlav\\ZU\\hl9__cV3.xls", "Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name, true);
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
                worksheet.Cells[1, 15] = worksheet.Cells[1, 15].Value2.ToString() + cislo;
                worksheet.Cells[2, 16] = worksheet.Cells[2, 16].Value2.ToString() + "1";

                if (cislo == "18")
                    worksheet.Cells[3, 1] = worksheet.Cells[3, 1].Value2.ToString() + "- len rozpočtové zdroje";
                if (cislo == "19")
                    worksheet.Cells[3, 1] = worksheet.Cells[3, 1].Value2.ToString() + "- bez kódu zdroja 111 (len za rozpočtové zdroje)";
                if (cislo == "19a")
                    worksheet.Cells[3, 1] = worksheet.Cells[3, 1].Value2.ToString() + "- bez kódu zdroja 111 (za všetky ostatné zdroje)";
                if (cislo == "20")
                    worksheet.Cells[3, 1] = worksheet.Cells[3, 1].Value2.ToString() + "- kód zdroja 111";
                worksheet.Cells[4, 1] = "k " + datum;


                ArrayList pool = new ArrayList();
                ArrayList stack = new ArrayList();

                SqlCommand startovacie = new SqlCommand("Select Prog From ciselnikProjektPrvok WHERE Znacka = 0", Con);
                DataTable startovacie_table = new DataTable();
                using (SqlDataAdapter startovacie_adapter = new SqlDataAdapter(startovacie))
                {
                    startovacie_adapter.Fill(startovacie_table);
                    startovacie.ExecuteNonQuery();
                }
                foreach (DataRow row in startovacie_table.Rows) // Loop over the rows.
                {
                    pool.Add(row[0].ToString());
                }

                while (pool.Count > 0)
                {
                    int valid_len = pocet_znakov_bez_nul(pool[0].ToString());
                    //AND SUBSTRING(Kategoria, 1, 1) IN('1', '2', '3', '4', '5')
                    //string command = "SELECT DISTINCT Prog FROM ciselnikProjektPrvok JOIN TableVstup tv ON tv.ProjektPrvok=Prog WHERE tv.Zdroj LIKE @Zdroj AND tv.Ico LIKE @Ico AND SUBSTRING(Prog, 1, " + valid_len.ToString() + ") LIKE SUBSTRING('" + pool[0].ToString() + "', 1, " + valid_len.ToString() + ") AND Prog != '" + pool[0].ToString() + "'";
                    string command = "SELECT DISTINCT Prog FROM ciselnikProjektPrvok  WHERE  SUBSTRING(Prog, 1, " + valid_len.ToString() + ") LIKE SUBSTRING('" + pool[0].ToString() + "', 1, " + valid_len.ToString() + ") AND Prog != '" + pool[0].ToString() + "'";
                    SqlCommand children = new SqlCommand(command, Con);
                    //children.Parameters.AddWithValue("@Ico", arrIco[icoI] + "%");
                    //children.Parameters.AddWithValue("@Zdroj", arrZdroje[k] + "%");
                    DataTable children_table = new DataTable();
                    DataTable sortedDT;
                    using (SqlDataAdapter children_adapter = new SqlDataAdapter(children))
                    {
                        children_adapter.Fill(children_table);
                        children.ExecuteNonQuery();
                        DataView dv = children_table.DefaultView;
                        dv.Sort = "Prog DESC";
                        sortedDT = dv.ToTable();
                    }

                    int worthy_children = 0;
                    foreach (DataRow row in sortedDT.Rows)
                    { // Loop over the rows.

                        if (!stack.Contains(row[0].ToString()) && !stack.Contains(row[0]))
                            worthy_children++;
                    }
                    if (worthy_children == 0)//ci ma dany program nejakych substring potomkov ktore este nie su v stacku
                    {
                        if (!stack.Contains(pool[0]))
                            stack.Add(pool[0]);
                        pool.RemoveAt(0);
                    }
                    else
                        foreach (DataRow row in sortedDT.Rows)
                            pool.Insert(0, row[0]);
                }

                string vynimky = vynimkyMRZ(connectionstring);
                string[] synteticke = najdiSynteticke(connectionstring);
                double[] sum_all = new double[9];
                foreach (string program in stack)
                {
                    int valid_len = pocet_znakov_bez_nul(program.ToString());
                    SqlCommand vydavb = null;
                    SqlCommand vydavk = null;


                    if (cislo == "18")//len rozp
                    {
                        vydavb = new SqlCommand("Select SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Substring(ProjektPrvok, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND KalendarnyDen LIKE @Datum AND HlavnaKategoria IN ('200','300','600','700') AND SyntetickyaleboFiktivny LIKE @Synteticky AND SUBSTRING(Kategoria, 1, 1) LIKE '6' " + vynimky, Con);
                        vydavk = new SqlCommand("Select SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Substring(ProjektPrvok, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND KalendarnyDen LIKE @Datum AND HlavnaKategoria IN ('200','300','600','700') AND SyntetickyaleboFiktivny LIKE @Synteticky AND SUBSTRING(Kategoria, 1, 1) LIKE '7' " + vynimky, Con);
                    }
                    if (cislo == "19")//bez 111
                    {
                        vydavb = new SqlCommand("Select SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE tv.Zdroj NOT LIKE '111%' AND Substring(ProjektPrvok, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND KalendarnyDen LIKE @Datum AND HlavnaKategoria IN ('200','300','600','700') AND SyntetickyaleboFiktivny LIKE @Synteticky AND SUBSTRING(Kategoria, 1, 1) LIKE '6' " + vynimky, Con);
                        vydavk = new SqlCommand("Select SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE tv.Zdroj NOT LIKE '111%' AND Substring(ProjektPrvok, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND KalendarnyDen LIKE @Datum AND HlavnaKategoria IN ('200','300','600','700') AND SyntetickyaleboFiktivny LIKE @Synteticky AND SUBSTRING(Kategoria, 1, 1) LIKE '7' " + vynimky, Con);
                    }
                    if (cislo == "19a")//bez 111, vsetky
                    {
                        vydavb = new SqlCommand("Select SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE tv.Zdroj NOT LIKE '111%' AND Substring(ProjektPrvok, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND KalendarnyDen LIKE @Datum AND HlavnaKategoria IN ('200','300','600','700') AND SyntetickyaleboFiktivny LIKE @Synteticky AND SUBSTRING(Kategoria, 1, 1) LIKE '6' ", Con);
                        vydavk = new SqlCommand("Select SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE tv.Zdroj NOT LIKE '111%' AND Substring(ProjektPrvok, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND KalendarnyDen LIKE @Datum AND HlavnaKategoria IN ('200','300','600','700') AND SyntetickyaleboFiktivny LIKE @Synteticky AND SUBSTRING(Kategoria, 1, 1) LIKE '7' ", Con);
                    }
                    if (cislo == "20")//len 111
                    {
                        vydavb = new SqlCommand("Select SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE tv.Zdroj LIKE '111%' AND Substring(ProjektPrvok, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND KalendarnyDen LIKE @Datum AND HlavnaKategoria IN ('200','300','600','700') AND SyntetickyaleboFiktivny LIKE @Synteticky AND SUBSTRING(Kategoria, 1, 1) LIKE '6' " + vynimky, Con);
                        vydavk = new SqlCommand("Select SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE tv.Zdroj LIKE '111%' AND Substring(ProjektPrvok, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND KalendarnyDen LIKE @Datum AND HlavnaKategoria IN ('200','300','600','700') AND SyntetickyaleboFiktivny LIKE @Synteticky AND SUBSTRING(Kategoria, 1, 1) LIKE '7' " + vynimky, Con);
                    }

                    vydavb.Parameters.AddWithValue("@Datum", datum);
                    vydavb.Parameters.AddWithValue("@Synteticky", synteticke[5]);

                    vydavk.Parameters.AddWithValue("@Datum", datum);
                    vydavk.Parameters.AddWithValue("@Synteticky", synteticke[5]);

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

                    if ((int)vydavb_table.Rows[0][3] > 0 || (int)vydavk_table.Rows[0][3] > 0)
                    {
                        worksheet.Cells[start, 1].NumberFormat = "@";
                        worksheet.Cells[start, 1] = program.ToString().Substring(0, valid_len);
                        double[] columns = new double[9];

                        foreach (DataRow row in vydavb_table.Rows)
                        {
                            if (row[0].ToString() != "")
                            {
                                worksheet.Cells[start, 2] = (double)row[0] / typVystupov;
                                columns[0] = (double)row[0];
                            }
                            else
                            {
                                worksheet.Cells[start, 2] = 0;
                                columns[0] = 0;
                            }

                            if (row[1].ToString() != "")
                            {
                                worksheet.Cells[start, 5] = (double)row[1] / typVystupov;
                                columns[2] = (double)row[1];
                            }
                            else
                            {
                                worksheet.Cells[start, 5] = 0;
                                columns[2] = 0;
                            }

                            if (row[2].ToString() != "")
                            {
                                worksheet.Cells[start, 8] = (double)row[2] / typVystupov;
                                columns[4] = (double)row[2];
                            }
                            else
                            {
                                worksheet.Cells[start, 8] = "0";
                                columns[4] = 0;
                            }
                        }

                        foreach (DataRow row in vydavk_table.Rows)
                        {
                            if (row[0].ToString() != "")
                            {
                                worksheet.Cells[start, 3] = (double)row[0] / typVystupov;
                                columns[1] = (double)row[0];
                            }
                            else
                            {
                                worksheet.Cells[start, 3] = "0";
                                columns[1] = 0;
                            }

                            if (row[1].ToString() != "")
                            {
                                worksheet.Cells[start, 6] = (double)row[1] / typVystupov;
                                columns[3] = (double)row[1];
                            }
                            else
                            {
                                worksheet.Cells[start, 6] = "0";
                                columns[3] = 0;
                            }

                            if (row[2].ToString() != "")
                            {
                                worksheet.Cells[start, 9] = (double)row[2] / typVystupov;
                                columns[5] = (double)row[2];
                            }
                            else
                            {
                                worksheet.Cells[start, 9] = "0";
                                columns[5] = 0;
                            }
                        }

                        columns[6] = columns[0] + columns[1];
                        columns[7] = columns[2] + columns[3];
                        columns[8] = columns[4] + columns[5];

                        if (valid_len == 2)
                        {
                            sum_all[0] += columns[0];
                            sum_all[1] += columns[1];
                            sum_all[2] += columns[6];
                            sum_all[3] += columns[2];
                            sum_all[4] += columns[3];
                            sum_all[5] += columns[7];
                            sum_all[6] += columns[4];
                            sum_all[7] += columns[5];
                            sum_all[8] += columns[8];
                        }

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

                        int last_border = border;
                        int last_start = start;
                        bool ret = increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 11, 20, 20);
                        if (ret == true)
                            selectString += "B" + last_border.ToString() + ":" + "P" + last_start.ToString() + System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator;
                    }
                }
                worksheet.Cells[start, 1] = "Spolu";
                for (int i = 2; i <= 10; i++)
                    worksheet.Cells[start, i] = sum_all[i - 2] / typVystupov;

                if (sum_all[0] != 0)
                    worksheet.Cells[start, 11] = (sum_all[6] / sum_all[0]) * 100;
                else
                    worksheet.Cells[start, 11] = 0;
                if (sum_all[1] != 0)
                    worksheet.Cells[start, 12] = (sum_all[7] / sum_all[1]) * 100;
                else
                    worksheet.Cells[start, 12] = 0;
                if (sum_all[2] != 0)
                    worksheet.Cells[start, 13] = (sum_all[8] / sum_all[2]) * 100;
                else
                    worksheet.Cells[start, 13] = 0;

                if (sum_all[3] != 0)
                    worksheet.Cells[start, 14] = (sum_all[6] / sum_all[3]) * 100;
                else
                    worksheet.Cells[start, 14] = 0;
                if (sum_all[4] != 0)
                    worksheet.Cells[start, 15] = (sum_all[7] / sum_all[4]) * 100;
                else
                    worksheet.Cells[start, 15] = 0;
                if (sum_all[5] != 0)
                    worksheet.Cells[start, 16] = (sum_all[8] / sum_all[5]) * 100;
                else
                    worksheet.Cells[start, 16] = 0;
                for (int i = 2; i <= 10; i++)
                    sum_all[i - 2] = 0;

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

                borderRange = worksheet.get_Range(left_char.ToString() + border, right_char.ToString() + start);//hrube po Celkovo
                borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                var culture = new CultureInfo("ru-RU");
                worksheet.Cells[++start, 1] = "Dátum vytvorenia: " + time.ToString(culture);

                //selectString += "B" + border.ToString() + ":" + "P" + (start-1).ToString();
                //Excel.Range selectedRange = worksheet.Range[selectString].Columns["A:P"];
                //selectedRange.Columns.AutoFit();
                //foreach (Excel.Range column in selectedRange.Columns)
                //{
                //    if (column.ColumnWidth < 10.86)//sirka stlpca tak jak ju ukazuje excel
                //        column.ColumnWidth = 10.86;
                //}

                workbook.Save();
                if (priamaTlac)
                    PrintMyExcelFile(workbook);
                Kontrola.release_excel(range, worksheet, workbook, app);
            }
        }


    }
}
