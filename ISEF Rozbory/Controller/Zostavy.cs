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
    class Zostavy
    {
        /*
         * Trieda Zostavy sluzi ako rodicovska trieda pre vsetky triedy Zostav, obsahuje funkcie nutne pre kazdu z nich.
         */
        public string connection { get; set; } = "cvti.isef.winformapp.Properties.Settings.ISEFDB";
        public bool increment_start(ref int start, Excel._Worksheet worksheet, char left_char, int left_num, char right_char, int right_num, int count_for_page, ref int border, ref int strana, int strana_rel_pos, string zaokruhlenieMena, string zaokruhleniePercenta, DateTime time, int zaciatokZaokruhlovania, int percentaZaciatok, int cisla2, int percenta2)
        {
            /*
             * Funkcia sluzi na presun na dalsi riadok vo vystupe zostavy s kontrolou, ci je potrebny presun aj na dalsiu stranu spolu
             * s kompletnym oramovanim, formatovanim hodnot a skopirovanim hlavicky
             * Parametre:    
             *     start - pocitadlo riadku v exceli
             *     worksheet - kniznicna premenna excelu
             *     left_char
             *     left_num
             *     right_char
             *     right_num
             *        opisuju poziciu hlavicky, tj ak je hlavicka umiestnena na poziciach A1 az K9, prislusne hodnoty budu v poradi A, 1, K, 9
             *     count_for_page - kolko riadkov sa zmesti do daneho excelu (sluzi na spravne strankovanie pre tlac)
             *     border - pomocny counter pre spravne riadkovanie
             *     strana - momentalna strana
             *     strana_rel_pos - relativna pozicia policka, kde je vypisovana strana od spodku praveho dolneho rohu, tj. ked hlavicka konci na mieste K9, 
             *                        strana_rel_pos = 3 hovori, ze miesto na vypisovanie je o 3 policka vyssie, tj H9
             *     time - cas spustenia realizacie zostav                   
             *     ostatne parametre su nastavenia vypisu zostavy (desatinne miesta, percenta a pod.)                   
             */
            if ((++start) % (count_for_page) == 0)
            {
                var culture = new CultureInfo("ru-RU");
                worksheet.Cells[start, 1] = "Dátum vytvorenia: " + time.ToString(culture);
                worksheet.Cells[start, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                worksheet.HPageBreaks.Add(worksheet.Range["A" + (start + 1).ToString()]);
                start++;
                Excel.Range from = worksheet.Range[left_char + left_num.ToString() + ":" + right_char + right_num.ToString()];
                Excel.Range to = worksheet.Range[left_char + start.ToString() + ":" + right_char + (right_num - left_num + start).ToString()];
                from.Copy(to);

                //ORAMOVANIE
                Excel.Range borderRange;
                for (char c = left_char; c <= (char)((int)right_char); c++)//tenke oramovanie od border po start
                {
                    borderRange = worksheet.get_Range(c.ToString() + border, c.ToString() + (start - 2));
                    if (c > left_char + zaciatokZaokruhlovania - 2)
                    {
                        if (c < left_char + percentaZaciatok - 1 || (c < left_char + percenta2 - 1 && c > left_char + cisla2 - 2))
                            borderRange.NumberFormat = zaokruhlenieMena;
                        else
                            borderRange.NumberFormat = zaokruhleniePercenta;
                        borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                    }
                    else
                    {
                        /*if (c < left_char + (zaciatokZaokruhlovania - 2))
                            borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;*/
                        borderRange.NumberFormat = "@";
                    }
                    if (count_for_page == 67 && c == left_char + 1)
                        borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

                    if((count_for_page == 48 && (right_char - left_char) == 12) || (count_for_page == 71 && (right_char - left_char) == 8))
                    {
                        int helper = count_for_page == 48 ? 3 : 3;
                        if (c <= left_char + helper)
                        {
                            if (c == left_char + helper)
                                borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                            else
                                borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                            borderRange.NumberFormat = "@";
                        }
                    }

                    borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                }
                borderRange = worksheet.get_Range(left_char.ToString() + border, right_char.ToString() + (start - 2));
                borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                start += right_num - left_num + 1;
                worksheet.Cells[start - strana_rel_pos, (int)right_char - 65 + 1] = "Strana: " + strana;
                border = start;
                strana++;
                return true;
            }
            return false;
        }

        public void increment_start_odsekove(ref int start, Excel._Worksheet worksheet, char left_char, int left_num, char right_char, int right_num, int count_for_page, ref int border, ref int borderIco, ref int strana, int strana_rel_pos, string zaokruhlenieMena, string zaokruhleniePercenta, DateTime time, int zaciatokZaokruhlovania, int percentaZaciatok, int cisla2, int percenta2)
        {
            /*
             * rozsirena verzia predchadzajucej funkcie, rozsirena o jeden parameter (borderIco), ktory sluzi na spravne oramovanie v pripade zlozitejsej hlavicky
             */
            int control = 0;
            if (count_for_page == 68)
                control = 1;
            if ((++start + control) % count_for_page == 0)
            {
                var culture = new CultureInfo("ru-RU");
                worksheet.Cells[start, 1] = "Dátum vytvorenia: " + time.ToString(culture);
                worksheet.Cells[start, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                worksheet.HPageBreaks.Add(worksheet.Range["A" + (start + 1).ToString()]);
                start++;
                Excel.Range from = worksheet.Range[left_char + left_num.ToString() + ":" + right_char + right_num.ToString()];
                Excel.Range to = worksheet.Range[left_char + start.ToString() + ":" + right_char + (right_num - left_num + start).ToString()];
                from.Copy(to);

                //ORAMOVANIE
                Excel.Range borderRange;
                for (char c = left_char; c <= (char)((int)right_char); c++)//tenke oramovanie od border po start
                {
                    borderRange = worksheet.get_Range(c.ToString() + borderIco, c.ToString() + (start - 2));
                    borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                    //ZAOKRUHLOVANIE
                    if (c > left_char + zaciatokZaokruhlovania - 2)
                    {
                        if (c < left_char + percentaZaciatok - 1 || (c < left_char + percenta2 - 1 && c > left_char + cisla2 - 2))
                            borderRange.NumberFormat = zaokruhlenieMena;
                        else
                            borderRange.NumberFormat = zaokruhleniePercenta;
                        borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                    }
                    else
                    {
                        if (c == left_char + (zaciatokZaokruhlovania - 2))
                            borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        if (c < left_char + (zaciatokZaokruhlovania - 2))
                            borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        if (count_for_page == 67 && c == left_char + 1)
                            borderRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    }
                }
                borderRange = worksheet.get_Range(left_char.ToString() + border, right_char.ToString() + (start - 2));
                borderRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                start += right_num - left_num + 1;
                worksheet.Cells[start - strana_rel_pos, (int)right_char - 65 + 1] = "Strana: " + strana;
                strana++;
                border = start;
                borderIco = start;
            }
        }

        public int pocet_znakov_bez_nul(string input)//pocita, po kolky znak ze cislo platne napr 02101000 iba prvych 5 lebo okrem nul
        {
            string stripped = input.Trim();
            int index = stripped.Length - 1;
            while (index != -1 && stripped[index] == '0')
            {
                index--;
            }
            return index + 1;
        }

        public string vynimkyVedy(int typ, string connectionstring)
        {
            /*
             * Funkcia pre vytvorenie stringu vynimiek, ktory je priamo vkladany do neskorsich query, z nastaveni vynimiek vysklada SQL AND(...) tak, aby zaskrnute hodnoty vedy boli pritomne
             * a nezaskrnute naopak.
             */

            string vynimky = "AND (";
            SqlCommand com = null;
            DataTable dT = new DataTable();

            using (var Con = new SqlConnection(connectionstring))
            {
                Con.Open();
                com = new SqlCommand("SELECT * FROM Veda", Con);

                using (SqlDataAdapter da = new SqlDataAdapter(com))
                {
                    da.Fill(dT);
                    com.ExecuteNonQuery();
                }
            }

            for (int i = 0; i < dT.Rows.Count; i++)
            {
                if (typ == 4)
                {
                    if (i > 0)
                        vynimky = vynimky + "AND ";
                    vynimky = vynimky + "Podtrieda NOT LIKE '" + dT.Rows[i][0] + "' ";
                }
                else
                {
                    if (i > 0)
                        vynimky = vynimky + "OR ";
                    vynimky = vynimky + "Podtrieda LIKE '" + dT.Rows[i][0] + "' ";
                }
            }
            vynimky = vynimky + ") ";

            return vynimky;
        }

        public string vynimkyMRZ(string connectionstring)
        {
            /*
             * Funkcia pre vytvorenie stringu vynimiek, ktory je priamo vkladany do neskorsich query, z nastaveni vynimiek vysklada SQL AND(...) tak, aby zaskrnute hodnoty MRZ boli pritomne
             * a nezaskrnute naopak.
             * Typovy vystup: vynimky = "AND ((Zdroj LIKE '3A%' OR Zdroj LIKE '1%') AND Zdroj NOT LIKE '11O3%' AND Zdroj NOT LIKE '13O3%' AND ZDROJ NOT LIKE '11O5%' AND ZDROJ NOT LIKE '13O5%' AND Zdroj NOT LIKE '11P3%' AND Zdroj NOT LIKE '13P3%' AND Zdroj NOT LIKE '1AM1%')";
             */

            string vynimky = "AND (";
            DataTable dT = new DataTable();
            string like = "";
            string not_like = "";

            using (var Con = new SqlConnection(connectionstring))
            {
                Con.Open();
                var com = new SqlCommand("SELECT * FROM Vynimky", Con);

                using (SqlDataAdapter da = new SqlDataAdapter(com))
                {
                    da.Fill(dT);
                    com.ExecuteNonQuery();
                }
            }

            int count_like = 0;
            int count_not_like = 0;
            foreach (DataRow row in dT.Rows) // Loop over the rows.
            {
                if (row[1].ToString() == "False")
                {
                    if (count_like > 0)
                        like += "OR ";
                    like += "tv.Zdroj LIKE '" + row[0] + "' ";
                    count_like++;
                }
                if (row[1].ToString() == "True")
                {
                    if (count_not_like > 0)
                        not_like += "AND ";
                    not_like += "tv.Zdroj NOT LIKE '" + row[0] + "' ";
                    count_not_like++;
                }
            }
            vynimky = vynimky + "(" + like + ") AND " + not_like + ")";
            return vynimky;
        }


        public string zistiNazovZakladne(int typSyntetickeho, int prijmyAleboVydavky, Boolean lenOkruhy)
        {
            /*
             * Funkcia na urcovanie nazvov excelov vystupnych zostav.
             */
            string meno = null;
            //if (typ == "PRR" || typ == "PRN")
            if (typSyntetickeho == 0 || typSyntetickeho == 2)
                meno = "p3b";
            else
            {
                //if(typ == "VYR" || typ == "VYN")
                if (typSyntetickeho == 1 || typSyntetickeho == 3)
                    meno = "p4b";
                else
                {
                    //if(typ == "224" || typ == "MRP")
                    if (typSyntetickeho == 4 || typSyntetickeho == 6)
                    {
                        if (lenOkruhy == false)
                            meno = "3b";
                        else
                            meno = "5a";
                    }
                    else
                    {
                        //if(typ=="225" || typ == "MRV")
                        if (typSyntetickeho == 5 || typSyntetickeho == 7)
                        {
                            if (prijmyAleboVydavky < 3)
                            {
                                if (lenOkruhy == false)
                                    meno = "4b";
                                else
                                    meno = "5b";
                            }
                            else
                            {
                                if (prijmyAleboVydavky == 3)
                                    meno = "4c";
                                if (prijmyAleboVydavky == 4)
                                    meno = "4b1";
                            }
                        }
                    }
                }
            }
            return meno;
        }

        public string zistiNazovPodlaPolozky(int typSyntetickeho, int prijmyVydavkyVedaOON)
        {
            /*
             * Funkcia na urcovanie nazvov excelov vystupnych zostav za polozky.
             */
            string meno = null;
            //if (typ == "PRR" || typ == "224")
            if (typSyntetickeho == 0 || typSyntetickeho == 4)
                meno = "8c";
            else
            {
                //if (typ == "VYR" || typ == "225")
                if (typSyntetickeho == 1 || typSyntetickeho == 5)
                {
                    if (prijmyVydavkyVedaOON == 2)
                        meno = "8a";
                    if (prijmyVydavkyVedaOON == 3)
                        meno = "8a1";
                    if (prijmyVydavkyVedaOON == 5)
                        meno = "8a11";
                    if (prijmyVydavkyVedaOON == 6)
                        meno = "8a12";
                }
            }
            //if (typ == "PRR" || typ == "VYR")
            if (typSyntetickeho == 0 || typSyntetickeho == 3)
                meno = "p" + meno;
            return meno;
        }

        public string[] najdiSynteticke(string connectionstring)
        {
            /*
             * Funkcia na vytiahnutie syntetickych uctov z databazy.
             */
            DataTable dT = new DataTable();

            using (var Con = new SqlConnection(connectionstring))
            {
                Con.Open();
                var com = new SqlCommand("SELECT SyntetickyUcet FROM SyntetickyUcet ORDER BY Poradie", Con);

                using (SqlDataAdapter da = new SqlDataAdapter(com))
                {
                    da.Fill(dT);
                    com.ExecuteNonQuery();
                }
            }
            string[] array = new string[dT.Rows.Count];
            for (int i = 0; i < dT.Rows.Count; i++)
                array[i] = (string)dT.Rows[i][0];

            return array;
        }

        public string vytvorAdresare(string datum)
        {
            /*
             * Funkcia na vytvorenie spravnych adresarov, do ktorych su umiestnovane zostavy, podla nastaveni roku a kvartalu.
             */
            string returnValue = null;
            if (!Directory.Exists("Zostavy\\" + datum.Substring(datum.Length - 4)))
                Directory.CreateDirectory("Zostavy\\" + datum.Substring(datum.Length - 4));
            switch (datum.Substring(datum.Length - 7, 2))
            {
                case "03":
                    if (!Directory.Exists("Zostavy\\" + datum.Substring(datum.Length - 4) + "\\q1"))
                        Directory.CreateDirectory("Zostavy\\" + datum.Substring(datum.Length - 4) + "\\q1");
                    returnValue = "q1";
                    break;
                case "06":
                    if (!Directory.Exists("Zostavy\\" + datum.Substring(datum.Length - 4) + "\\q2"))
                        Directory.CreateDirectory("Zostavy\\" + datum.Substring(datum.Length - 4) + "\\q2");
                    returnValue = "q2";
                    break;
                case "09":
                    if (!Directory.Exists("Zostavy\\" + datum.Substring(datum.Length - 4) + "\\q3"))
                        Directory.CreateDirectory("Zostavy\\" + datum.Substring(datum.Length - 4) + "\\q3");
                    returnValue = "q3";
                    break;
                case "12":
                    if (!Directory.Exists("Zostavy\\" + datum.Substring(datum.Length - 4) + "\\q4"))
                        Directory.CreateDirectory("Zostavy\\" + datum.Substring(datum.Length - 4) + "\\q4");
                    returnValue = "q4";
                    break;
            }
            return returnValue;
        }

        public string zistiEkonomickuKlasifikaciu(int prijemVydavok, int control, string connectionstring)
        {
            /*
             * Funkcia na vytiahnutie ekonomickzch klasifikacii pre prijmy alebo vydavky.
             */
            string vynimky = "AND (";
            DataTable dT = new DataTable();

            using (var Con = new SqlConnection(connectionstring))
            {
                Con.Open();
                string helpstring = null;
                if (prijemVydavok == 1)
                    helpstring = "1";
                else
                    helpstring = "0";

                var com = new SqlCommand("SELECT EkonomickaKlasifikacia FROM EkonomickaKlasifikacia WHERE Prijem LIKE '" + helpstring + "'", Con);

                using (SqlDataAdapter da = new SqlDataAdapter(com))
                {
                    da.Fill(dT);
                    com.ExecuteNonQuery();
                }
            }

            for (int i = 0; i < dT.Rows.Count; i++)
            {
                if (i > 0)
                    vynimky = vynimky + "OR ";
                if (control == 1)
                    vynimky = vynimky + "Kategoria LIKE '";
                else
                    vynimky = vynimky + "Pol LIKE '";

                vynimky = vynimky + dT.Rows[i][0].ToString().Replace(" ", string.Empty) + "' ";
            }
            vynimky = vynimky + ") ";

            return vynimky;
        }

        public void PrintMyExcelFile(Excel.Workbook workbook)
        {
            // Get the first worksheet.
            // (Excel uses base 1 indexing, not base 0.)
            Excel.Worksheet ws = (Excel.Worksheet)workbook.Worksheets[1];

            // Print out 1 copy to the default printer:
            ws.PrintOut(
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        }
    }
}
