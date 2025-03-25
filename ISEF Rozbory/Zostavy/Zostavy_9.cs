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
    class Zostavy_9 : Zostavy
    {
        public void spravZostavyCerpania(int druh_rozpoctu, string MSVSico, int podlaIca, string datum, int typVystupov, string zaokruhlenieMena, string zaokruhleniePercenta, char left_char, int left_num, char right_char, int right_num, int count_for_page, int strana_rel_pos, DateTime time, bool priamaTlac)
        {
            // druh rozpoctu 
            // 1 - Rozpoctove aj mimorozpoctove
            // 2 - len rozpoctove
            // 3 - len mimorozpoctove
            // podlaIca
            // 0 - celkovy prehlad
            // 1 - podla ica prehlad
            // 2 - MSVVas + opro
            // 3 - MSVVas opro -> spolu
            // vystupuje tu pool a stack, stack je konecna usporiadana mnozina Programov tak ako vo vypisanych zostavach, pool je mnozina moznych Programov ktore sa do stacku daju ak uz nemaju substring "potomkov" a patria tam podla druhu 1 2 3
            string connectionstring = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            string[] synteticke = najdiSynteticke(connectionstring);
            string ekonomickaKlasifikacia = zistiEkonomickuKlasifikaciu(2, 1, connectionstring);
            SqlCommand com = null;
            DataTable dT = new DataTable();
            double[] sum_all;
            
            using (var Con = new SqlConnection(connectionstring))
            {
                Con.Open();
                string[] arrZdroje, arrIco;
                int pocetZdrojov = 1, pocetIca = 0;
                string vynimky = vynimkyMRZ(connectionstring);
                //UPRAVENIE MRZ VYNIMIEK
                if (druh_rozpoctu == 3)
                {
                    vynimky = vynimky.Insert(3, " (NOT ");
                    vynimky = vynimky.Insert(vynimky.Length, ")");
                }
                //PODLA ZDROJOV SA ROBI JEDINE ROZPOCTOVE AJ MIOROZPOCTOVE PRI CELKOVOM PREHLADE
                if (druh_rozpoctu == 1 && podlaIca == 0)
                {
                    com = new SqlCommand("SELECT DISTINCT Zdroj FROM TableVstup tv WHERE KalendarnyDen LIKE @Datum AND SyntetickyaleboFiktivny LIKE @Synteticky " + ekonomickaKlasifikacia + " AND ProjektPrvok NOT LIKE '#' ORDER BY Zdroj", Con);
                    com.Parameters.AddWithValue("@Datum", datum);
                    com.Parameters.AddWithValue("@Synteticky", synteticke[5]);
                    using (SqlDataAdapter da = new SqlDataAdapter(com))
                    {
                        da.Fill(dT);
                        com.ExecuteNonQuery();
                    }
                    //ZDROJE DO ARRAYU
                    List<string> zdrojeList = new List<string>();
                    Boolean control7dmickove = false;
                    //AK JE TAM 7DMICKOVY ZDROJ TAK DA DO LISTU AJ 7 AKO SUMAR
                    zdrojeList.Add("%");
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
                }
                else
                {
                    arrZdroje = new string[1];
                    arrZdroje[0] = "%";
                }
                //MENO TABULKY
                string nameTabulka = "9.2";
                if (podlaIca == 1)
                    nameTabulka = "9.1";
                else if(podlaIca == 2 || podlaIca == 3)
                    nameTabulka = "9.3";//za mvvas + opro

                if (druh_rozpoctu <= 3)
                    nameTabulka = nameTabulka + (char)('a' + druh_rozpoctu - 1);

                if (podlaIca == 2)//za mvvas + opro
                    nameTabulka += "1";

                string kvartal = vytvorAdresare(datum);
                string name = "RZBT" + nameTabulka + ".xls";
                //VYTVORENIE EXCELU
                System.IO.File.Copy("..\\..\\Hlav\\RZB\\hl9__cV3.xls", "Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name, true);
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
                if (druh_rozpoctu == 1)
                    worksheet.Cells[5, 1] = "Celkové výdavky = Rozpočtové aj iné zdroje";
                else
                {
                    if (druh_rozpoctu == 2)
                        worksheet.Cells[5, 1] = "Celkové výdavky = len rozpočtové zdroje";
                    else
                        worksheet.Cells[5, 1] = "Celkové výdavky = len iné zdroje";
                }
                if (podlaIca == 1)
                    worksheet.Cells[2, 1] = "Prehľad čerpania podľa programov za jednotlivé organizácie";
                else if(podlaIca == 2)
                    worksheet.Cells[2, 1] = "Prehľad čerpania podľa programov za OPRO a MŠVVaŠ SR";
                else if (podlaIca == 3)
                    worksheet.Cells[2, 1] = "Prehľad čerpania podľa programov za jednotlivé okruhy organizácií";
                
                worksheet.Cells[6, 1] = "Spracovateľské obdobie: " + datum;
                worksheet.Cells[6, 15] = nameTabulka;
                worksheet.Cells[6, 16] = "Strana: 1";
                worksheet.Name = "Celkovo";

                //CYKLUS PRE KAZDY ZDROJ
                for (int k = 0; k < pocetZdrojov; k++)
                {
                    int rowCount = range.Rows.Count;//pre citanie excelu
                    int start = right_num + 1;
                    int border = start, borderIco = 0;
                    int strana = 2;
                    sum_all = new double[9];
                    //VYTVARANIE NOVEHO ZOSITU PLUS KOPIROVANIE HLAVICKY 
                    //VACSIE AKO 0 LEBO PRVY OTVARAM UZ PRED CYKLOM ABY SOM NASTAVIL HALVICKU KTORU POTOM KOPIRUJEM
                    if (k > 0)
                    {
                        workbook.Sheets[1].Copy(Type.Missing, workbook.Sheets[workbook.Sheets.Count]);
                        worksheet = workbook.Sheets[workbook.Sheets.Count];
                        int help = worksheet.Rows.Count;
                        range = worksheet.get_Range("A11", "P" + help);
                        range.Clear();
                        //DOPLNENIE NAZVU TABULKY D EXCELU ZA KONKRETNY ZDROJ
                        if (arrZdroje[k] != "7%")
                        {
                            worksheet.Cells[6, 15] = nameTabulka + "z" + arrZdroje[k];
                            worksheet.Name = arrZdroje[k];
                        }
                        else
                        {
                            worksheet.Cells[6, 15] = nameTabulka + "z7";
                            worksheet.Name = "7";
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
                        worksheet.Cells[4, 1] = worksheet.Cells[3, 1];
                        if (arrZdroje[k] != "7%")
                            worksheet.Cells[3, 1] = "za zdroj " + arrZdroje[k] + " - " + (string)dT.Rows[0][0];
                        else
                            worksheet.Cells[3, 1] = "za zdroj 7 - " + (string)dT.Rows[0][0];
                    }

                    //ZISTENIE ICA KTORE SA TAM NACHADZA
                    if (podlaIca == 1)
                    {
                        if (druh_rozpoctu == 1)//vsetky
                        {
                            //com = new SqlCommand("SELECT DISTINCT Ico FROM TableVstup WHERE KalendarnyDen LIKE @Datum AND SyntetickyaleboFiktivny LIKE @Synteticky AND HlavnaKategoria IN ('200','300','600','700') AND ProjektPrvok NOT LIKE '#' ORDER BY Ico", Con);
                            com = new SqlCommand("SELECT DISTINCT tv.Ico,ci.CelyNazov collate Slovak_CI_AS FROM TableVstup tv JOIN ciselnikIco ci ON tv.Ico=ci.Ico WHERE KalendarnyDen LIKE @Datum AND SyntetickyaleboFiktivny LIKE @Synteticky " + ekonomickaKlasifikacia + " AND ProjektPrvok NOT LIKE '#' ORDER BY ci.CelyNazov collate Slovak_CI_AS", Con);
                        }
                        if (druh_rozpoctu == 2 || druh_rozpoctu == 3)//len rozpoctove
                        {
                            //com = new SqlCommand("SELECT DISTINCT Ico FROM TableVstup WHERE KalendarnyDen LIKE @Datum AND SyntetickyaleboFiktivny LIKE @Synteticky AND HlavnaKategoria IN ('200','300','600','700') AND ProjektPrvok NOT LIKE '#' " + vynimky + " ORDER BY Ico ", Con);
                            com = new SqlCommand("SELECT DISTINCT tv.Ico,ci.CelyNazov collate Slovak_CI_AS FROM TableVstup tv JOIN ciselnikIco ci ON tv.Ico=ci.Ico WHERE KalendarnyDen LIKE @Datum AND SyntetickyaleboFiktivny LIKE @Synteticky " + ekonomickaKlasifikacia + " AND ProjektPrvok NOT LIKE '#' " + vynimky + " ORDER BY ci.CelyNazov collate Slovak_CI_AS", Con);
                        }
                        com.Parameters.AddWithValue("@Datum", datum);
                        com.Parameters.AddWithValue("@Synteticky", synteticke[5]);
                        using (SqlDataAdapter da = new SqlDataAdapter(com))
                        {
                            da.Fill(dT);
                            com.ExecuteNonQuery();
                        }
                        //ICA DO ARRAYU
                        List<string> icaList = new List<string>();
                        foreach (DataRow row in dT.Rows)
                        {
                            icaList.Add((string)row[0]);
                            pocetIca++;
                        }

                        arrIco = new string[pocetIca];
                        arrIco = icaList.ToArray();
                        icaList.Add("%");
                    }
                    else if (podlaIca == 0)
                    {
                        pocetIca = 1;
                        arrIco = new string[1];
                        arrIco[0] = "%";
                    }
                    else if (podlaIca == 2)
                    {
                        //com = new SqlCommand("SELECT Ico FROM ciselnikIco WHERE Nazov LIKE 'MŠVVaŠ SR Bratislava%'", Con);
                        //using (SqlDataAdapter da = new SqlDataAdapter(com))
                        //{
                        //    da.Fill(dT);
                        //    com.ExecuteNonQuery();
                        //}
                        List<string> icaList = new List<string>();
                        //foreach (DataRow row in dT.Rows)
                        //{
                        //    icaList.Add((string)row[0]);
                        //    pocetIca++;
                        //}

                        icaList.Add(MSVSico);
                        pocetIca++;
                        icaList[0] = icaList[0].Trim();
                        icaList.Add("0");//znacka OPRO - momentalne useless, len aby tam daco bolo
                        pocetIca++;
                        arrIco = new string[pocetIca + 1];
                        arrIco = icaList.ToArray();
                        icaList.Add("%");
                    }
                    else //if (podlaIca == 3) //ceknut ci sa to neda hore spojit
                    {
                        pocetIca++;
                        arrIco = new string[pocetIca];
                        arrIco[0] = "%";
                    }

                        for (int icoI = 0; icoI < pocetIca; icoI++)
                    {
                        if (podlaIca == 1 || podlaIca == 2 || podlaIca == 3)
                        {
                            /*if (start >= 110)
                                ;*/
                            if (icoI > 0)
                                increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 11, 20, 20);
                            //HLADA ICO NAZOV

                            string org = "";
                            
                            SqlCommand get_name = null;
                            get_name = new SqlCommand("SELECT Nazov from ciselnikIco where Ico = '" + arrIco[icoI] + "'", Con);
                            DataTable get_name_table = new DataTable();
                            if (!(podlaIca == 2 && icoI == 1) && !(podlaIca == 3 && icoI == 0))
                            {
                                using (SqlDataAdapter get_name_adapter = new SqlDataAdapter(get_name))
                                {
                                    get_name_adapter.Fill(get_name_table);
                                    get_name.ExecuteNonQuery();
                                }
                                if (get_name_table.Rows.Count != 0)
                                    org = (string)get_name_table.Rows[0][0];
                            }
                            else if (podlaIca == 2 && icoI == 1)
                                org = "OPRO (bez MŠVVaŠ SR)";
                            else// if (podlaIca == 3 && icoI == 0)
                                org = "Ostatné priamo riadené organizácie + MŠVVaŠ SR (Vrátane PJ)";

                            while (start % count_for_page > count_for_page - 4)
                                increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 11, 20, 20);

                            //NAZOV DO EXCELU S ODSADENIM PLUS KONTROLA CI NEROBI FINALNY SUMAR + NASTAVI HODNOTU ICO DO SELECTU
                            worksheet.get_Range(left_char + (start).ToString(), right_char + (start + 2).ToString()).Merge();
                            worksheet.get_Range(left_char + (start).ToString(), right_char + (start + 2).ToString()).VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                            worksheet.get_Range(left_char + (start).ToString(), right_char + (start + 2).ToString()).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                            if (get_name_table.Rows.Count != 0 || org == "OPRO (bez MŠVVaŠ SR)" || org == "Ostatné priamo riadené organizácie + MŠVVaŠ SR (Vrátane PJ)")
                                worksheet.Cells[start, 1] = "              Organizácia: " + org;
                            else
                                worksheet.Cells[start, 1] = "              Organizácia: Celkom";

                            //POSUN O 3 RIADKY KEDZE 3 ZLUCUJEME
                            increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 11, 20, 20);
                            increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 11, 20, 20);
                            increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 11, 20, 20);
                            borderIco = start;
                        }

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

                        foreach (string program in stack)
                        {
                            int valid_len = pocet_znakov_bez_nul(program.ToString());
                            SqlCommand vydavb = null;
                            SqlCommand vydavk = null;

                            if (podlaIca != 2 || icoI == 0)//ak to nie je opro typ, alebo je to opro typ a som na prvom ici, teda MSVVaS
                            {
                                if (druh_rozpoctu == 1)//vsetky
                                {
                                    vydavb = new SqlCommand("Select SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Zdroj LIKE @Zdroj AND Ico LIKE @Ico AND Substring(ProjektPrvok, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND KalendarnyDen LIKE @Datum " + ekonomickaKlasifikacia + " AND SyntetickyaleboFiktivny LIKE @Synteticky AND SUBSTRING(Kategoria, 1, 1) LIKE '6'", Con);
                                    vydavk = new SqlCommand("Select SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Zdroj LIKE @Zdroj AND Ico LIKE @Ico AND Substring(ProjektPrvok, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND KalendarnyDen LIKE @Datum " + ekonomickaKlasifikacia + " AND SyntetickyaleboFiktivny LIKE @Synteticky AND SUBSTRING(Kategoria, 1, 1) LIKE '7'", Con);
                                }
                                if (druh_rozpoctu == 2)//len rozpoctove
                                {
                                    vydavb = new SqlCommand("Select SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Zdroj LIKE @Zdroj AND Ico LIKE @Ico AND Substring(ProjektPrvok, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND KalendarnyDen LIKE @Datum " + ekonomickaKlasifikacia + " AND SyntetickyaleboFiktivny LIKE @Synteticky AND SUBSTRING(Kategoria, 1, 1) LIKE '6' " + vynimky, Con);
                                    vydavk = new SqlCommand("Select SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Zdroj LIKE @Zdroj AND Ico LIKE @Ico AND Substring(ProjektPrvok, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND KalendarnyDen LIKE @Datum " + ekonomickaKlasifikacia + " AND SyntetickyaleboFiktivny LIKE @Synteticky AND SUBSTRING(Kategoria, 1, 1) LIKE '7' " + vynimky, Con);
                                }
                                if (druh_rozpoctu == 3)//len mimorozpoctove
                                {
                                    vydavb = new SqlCommand("Select SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Zdroj LIKE @Zdroj AND Ico LIKE @Ico AND Substring(ProjektPrvok, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND KalendarnyDen LIKE @Datum " + ekonomickaKlasifikacia + " AND SyntetickyaleboFiktivny LIKE @Synteticky AND SUBSTRING(Kategoria, 1, 1) LIKE '6' " + vynimky, Con);
                                    vydavk = new SqlCommand("Select SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Zdroj LIKE @Zdroj AND Ico LIKE @Ico AND Substring(ProjektPrvok, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND KalendarnyDen LIKE @Datum " + ekonomickaKlasifikacia + " AND SyntetickyaleboFiktivny LIKE @Synteticky AND SUBSTRING(Kategoria, 1, 1) LIKE '7' " + vynimky, Con);
                                }
                                vydavb.Parameters.AddWithValue("@Ico", arrIco[icoI] + "%");
                                vydavk.Parameters.AddWithValue("@Ico", arrIco[icoI] + "%");
                            }
                            else //inak som opro 
                            {
                                if (druh_rozpoctu == 1)//vsetky
                                {
                                    vydavb = new SqlCommand("Select SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Zdroj LIKE @Zdroj AND Ico NOT LIKE @Ico AND Substring(ProjektPrvok, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND KalendarnyDen LIKE @Datum " + ekonomickaKlasifikacia + " AND SyntetickyaleboFiktivny LIKE @Synteticky AND SUBSTRING(Kategoria, 1, 1) LIKE '6'", Con);
                                    vydavk = new SqlCommand("Select SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Zdroj LIKE @Zdroj AND Ico NOT LIKE @Ico AND Substring(ProjektPrvok, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND KalendarnyDen LIKE @Datum " + ekonomickaKlasifikacia + " AND SyntetickyaleboFiktivny LIKE @Synteticky AND SUBSTRING(Kategoria, 1, 1) LIKE '7'", Con);
                                }
                                if (druh_rozpoctu == 2)//len rozpoctove
                                {
                                    vydavb = new SqlCommand("Select SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Zdroj LIKE @Zdroj AND Ico NOT LIKE @Ico AND Substring(ProjektPrvok, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND KalendarnyDen LIKE @Datum " + ekonomickaKlasifikacia + " AND SyntetickyaleboFiktivny LIKE @Synteticky AND SUBSTRING(Kategoria, 1, 1) LIKE '6' " + vynimky, Con);
                                    vydavk = new SqlCommand("Select SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Zdroj LIKE @Zdroj AND Ico NOT LIKE @Ico AND Substring(ProjektPrvok, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND KalendarnyDen LIKE @Datum " + ekonomickaKlasifikacia + " AND SyntetickyaleboFiktivny LIKE @Synteticky AND SUBSTRING(Kategoria, 1, 1) LIKE '7' " + vynimky, Con);
                                }
                                if (druh_rozpoctu == 3)//len mimorozpoctove
                                {
                                    vydavb = new SqlCommand("Select SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Zdroj LIKE @Zdroj AND Ico NOT LIKE @Ico AND Substring(ProjektPrvok, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND KalendarnyDen LIKE @Datum " + ekonomickaKlasifikacia + " AND SyntetickyaleboFiktivny LIKE @Synteticky AND SUBSTRING(Kategoria, 1, 1) LIKE '6' " + vynimky, Con);
                                    vydavk = new SqlCommand("Select SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Zdroj LIKE @Zdroj AND Ico NOT LIKE @Ico AND Substring(ProjektPrvok, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND KalendarnyDen LIKE @Datum " + ekonomickaKlasifikacia + " AND SyntetickyaleboFiktivny LIKE @Synteticky AND SUBSTRING(Kategoria, 1, 1) LIKE '7' " + vynimky, Con);
                                }
                                vydavb.Parameters.AddWithValue("@Ico", arrIco[0] + "%");
                                vydavk.Parameters.AddWithValue("@Ico", arrIco[0] + "%");
                            }
                            vydavb.Parameters.AddWithValue("@Datum", datum);
                            vydavb.Parameters.AddWithValue("@Zdroj", arrZdroje[k] + "%");
                            vydavb.Parameters.AddWithValue("@Synteticky", synteticke[5]);

                            vydavk.Parameters.AddWithValue("@Datum", datum);
                            vydavk.Parameters.AddWithValue("@Zdroj", arrZdroje[k] + "%");
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

                                if (podlaIca == 0)
                                    increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 11, 20, 20);
                                else
                                    increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 11, 20, 20);
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

                        if (podlaIca == 0 || podlaIca == 2)
                            borderIco = border;

                        //ORAMOVANIE V EXCELE
                        //JEDNOTLIVE STLPCE
                        for (char c = left_char; c <= (char)((int)right_char); c++)//tenke oramovanie od border po start
                        {
                            borderRange = worksheet.get_Range(c.ToString() + borderIco, c.ToString() + start);
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
                }
                workbook.Save();
                if (priamaTlac)
                    PrintMyExcelFile(workbook);
                Kontrola.release_excel(range, worksheet, workbook, app);
            }
        }

        public void spravZostavyPodlaFunkcnejKlasifikacie(int druh_rozpoctu, string MSVSico, int podlaIca, string datum, int typVystupov, string zaokruhlenieMena, string zaokruhleniePercenta, char left_char, int left_num, char right_char, int right_num, int count_for_page, int strana_rel_pos, DateTime time, bool priamaTlac)
        {
            // druh rozpoctu 
            // 1 - Rozpoctove aj mimorozpoctove
            // 2 - len rozpoctove
            // 3 - len mimorozpoctove
            // podlaIca
            // 0 - celkovy prehlad
            // 1 - podla ica prehlad
            // 2 - MSVVas + opro
            // 3 - opro
            // vystupuje tu pool a stack, stack je konecna usporiadana mnozina Programov tak ako vo vypisanych zostavach, pool je mnozina moznych Programov ktore sa do stacku daju ak uz nemaju substring "potomkov" a patria tam podla druhu 1 2 3
            string connectionstring = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            string[] synteticke = najdiSynteticke(connectionstring);
            string ekonomickaKlasifikacia = zistiEkonomickuKlasifikaciu(2, 1, connectionstring);
            SqlCommand com = null;
            DataTable dT = new DataTable();
            double[] sum_all;
            string controlPodtrieda = "ahahah";
            
            using (var Con = new SqlConnection(connectionstring))
            {
                Con.Open();
                string[] arrZdroje, arrIco;
                int pocetZdrojov = 1, pocetIca = 0;
                string vynimky = vynimkyMRZ(connectionstring);
                //UPRAVENIE MRZ VYNIMIEK
                if (druh_rozpoctu == 3)
                {
                    vynimky = vynimky.Insert(3, " (NOT ");
                    vynimky = vynimky.Insert(vynimky.Length, ")");
                }
                //PODLA ZDROJOV SA ROBI JEDINE ROZPOCTOVE AJ MIOROZPOCTOVE PRI CELKOVOM PREHLADE
                if (druh_rozpoctu == 1 && podlaIca == 0)
                {
                    com = new SqlCommand("SELECT DISTINCT Zdroj FROM TableVstup tv WHERE KalendarnyDen LIKE @Datum AND SyntetickyaleboFiktivny LIKE @Synteticky " + ekonomickaKlasifikacia + " AND ProjektPrvok NOT LIKE '#' ORDER BY Zdroj", Con);
                    com.Parameters.AddWithValue("@Datum", datum);
                    com.Parameters.AddWithValue("@Synteticky", synteticke[5]);
                    using (SqlDataAdapter da = new SqlDataAdapter(com))
                    {
                        da.Fill(dT);
                        com.ExecuteNonQuery();
                    }
                    //ZDROJE DO ARRAYU
                    List<string> zdrojeList = new List<string>();
                    Boolean control7dmickove = false;
                    //AK JE TAM 7DMICKOVY ZDROJ TAK DA DO LISTU AJ 7 AKO SUMAR
                    zdrojeList.Add("%");
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
                }
                else
                {
                    arrZdroje = new string[1];
                    arrZdroje[0] = "%";
                }

                ArrayList pool = new ArrayList();
                ArrayList stack = new ArrayList();

                SqlCommand startovacie = new SqlCommand("SELECT Typ FROM ciselnikPodtrieda WHERE Znacka = 2 ORDER BY TYP ASC", Con);
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


                //TOTO TU CELY WHILE TREBA DAT MIMO FUNKCIE LEBO TRVA ASI 2 MINUTY, VOLA SA V KAZDOM VOLANI FUNKCIE A NEZAVISI VOBEC OD VSTUPNYCH PARAMETROV CIZE STACI SA RAZ ZAVOLAT MIMO NEJ A TU VOLAT LEN VYSLEDOK
                while (pool.Count > 0)
                {
                    int valid_len = pocet_znakov_bez_nul(pool[0].ToString());
                    //AND SUBSTRING(Kategoria, 1, 1) IN('1', '2', '3', '4', '5')
                    string command = "SELECT DISTINCT Typ FROM ciselnikPodtrieda WHERE SUBSTRING(Typ, 1, " + valid_len.ToString() + ") LIKE SUBSTRING('" + pool[0].ToString() + "', 1, " + valid_len.ToString() + ") AND Typ != '" + pool[0].ToString() + "'";
                    SqlCommand children = new SqlCommand(command, Con);
                    DataTable children_table = new DataTable();
                    DataTable sortedDT;
                    using (SqlDataAdapter children_adapter = new SqlDataAdapter(children))
                    {
                        children_adapter.Fill(children_table);
                        children.ExecuteNonQuery();
                        DataView dv = children_table.DefaultView;
                        dv.Sort = "Typ DESC";
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

                //MENO TABULKY
                string nameTabulka = "9f.2";
                if (podlaIca == 1)
                    nameTabulka = "9f.1";
                else if (podlaIca == 2 || podlaIca == 3)
                    nameTabulka = "9f.3";//za (mvvas + opro) alebo za (iba opro)
                if (druh_rozpoctu <= 3) 
                    nameTabulka = nameTabulka + (char)('a' + druh_rozpoctu - 1);

                if (podlaIca == 2)//za mvvas + opro
                    nameTabulka += "1";




                string kvartal = vytvorAdresare(datum);
                string name = "RZBT" + nameTabulka + ".xls";

                //VYTVORENIE EXCELU
                System.IO.File.Copy("..\\..\\Hlav\\RZB\\hl9FKcV3.xls", "Zostavy\\" + datum.Substring(datum.Length - 4) + "\\" + kvartal + "\\" + name, true);
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
                if (druh_rozpoctu == 1)
                    worksheet.Cells[5, 1] = "Celkové výdavky = Rozpočtové aj iné zdroje";
                else
                {
                    if (druh_rozpoctu == 2)
                        worksheet.Cells[5, 1] = "Celkové výdavky = len rozpočtové zdroje";
                    else
                        worksheet.Cells[5, 1] = "Celkové výdavky = len iné zdroje";
                }
                if (podlaIca == 1)
                    worksheet.Cells[2, 1] = "Prehľad čerpania podľa funkčnej klasifikácie za jednotlivé organizácie";
                else if (podlaIca == 2)
                    worksheet.Cells[2, 1] = "Prehľad čerpania podľa funkčnej klasifikácie za OPRO a MŠVVaŠ SR";
                worksheet.Cells[6, 1] = "Spracovateľské obdobie: " + datum;
                worksheet.Cells[6, 7] = nameTabulka;
                worksheet.Cells[6, 8] = "Strana: 1";
                worksheet.Name = "Celkovo";

                //CYKLUS PRE KAZDY ZDROJ
                for (int k = 0; k < pocetZdrojov; k++)
                {
                    int rowCount = range.Rows.Count;//pre citanie excelu
                    int start = right_num + 1;
                    int border = start, borderIco = 0;
                    int strana = 2;

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

                        //NAZOV ZDROJA A VYPLNENIE KOLONIEK ROVNAKO PRE VSETKY
                        com = new SqlCommand("SELECT Text FROM ciselnikZdroje WHERE Zdroj LIKE @Zdroj", Con);
                        com.Parameters.AddWithValue("@Zdroj", arrZdroje[k] + "%");
                        dT = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(com))
                        {
                            da.Fill(dT);
                            com.ExecuteNonQuery();
                        }
                        worksheet.Cells[4, 1] = worksheet.Cells[3, 1];
                        if (arrZdroje[k] != "7%")
                            worksheet.Cells[3, 1] = "za zdroj " + arrZdroje[k] + " - " + (string)dT.Rows[0][0];
                        else
                            worksheet.Cells[3, 1] = "za zdroj 7 - " + (string)dT.Rows[0][0];
                    }

                    //ZISTENIE ICA KTORE SA TAM NACHADZA
                    if (podlaIca == 1)
                    {
                        if (druh_rozpoctu == 1)//vsetky
                        {
                            com = new SqlCommand("SELECT DISTINCT tv.Ico,ci.CelyNazov collate Slovak_CI_AS FROM TableVstup tv JOIN ciselnikIco ci ON tv.Ico=ci.Ico WHERE KalendarnyDen LIKE @Datum AND SyntetickyaleboFiktivny LIKE @Synteticky " + ekonomickaKlasifikacia + " AND ProjektPrvok NOT LIKE '#' ORDER BY ci.CelyNazov collate Slovak_CI_AS", Con);
                        }
                        if (druh_rozpoctu == 2 || druh_rozpoctu == 3)//len rozpoctove
                        {
                            com = new SqlCommand("SELECT DISTINCT tv.Ico,ci.CelyNazov collate Slovak_CI_AS FROM TableVstup tv JOIN ciselnikIco ci ON tv.Ico=ci.Ico WHERE KalendarnyDen LIKE @Datum AND SyntetickyaleboFiktivny LIKE @Synteticky " + ekonomickaKlasifikacia + " AND ProjektPrvok NOT LIKE '#' " + vynimky + " ORDER BY ci.CelyNazov collate Slovak_CI_AS", Con);
                        }
                        com.Parameters.AddWithValue("@Datum", datum);
                        com.Parameters.AddWithValue("@Synteticky", synteticke[5]);
                        using (SqlDataAdapter da = new SqlDataAdapter(com))
                        {
                            da.Fill(dT);
                            com.ExecuteNonQuery();
                        }
                        //ICA DO ARRAYU
                        List<string> icaList = new List<string>();
                        foreach (DataRow row in dT.Rows)
                        {
                            icaList.Add((string)row[0]);
                            pocetIca++;
                        }

                        arrIco = new string[pocetIca];
                        arrIco = icaList.ToArray();
                        icaList.Add("%");
                    }
                    else if (podlaIca == 0)
                    {
                        pocetIca = 1;
                        arrIco = new string[1];
                        arrIco[0] = "%";
                    }
                    else if (podlaIca == 2) //msvvas + opro
                    {
                        //com = new SqlCommand("SELECT Ico FROM ciselnikIco WHERE Nazov LIKE 'MŠVVaŠ SR Bratislava%'", Con);
                        //using (SqlDataAdapter da = new SqlDataAdapter(com))
                        //{
                        //    da.Fill(dT);
                        //    com.ExecuteNonQuery();
                        //}
                        List<string> icaList = new List<string>();
                        //foreach (DataRow row in dT.Rows)
                        //{
                        //    icaList.Add((string)row[0]);
                        //    pocetIca++;
                        //}

                        icaList.Add(MSVSico);
                        pocetIca++;
                        icaList[0] = icaList[0].Trim();
                        icaList.Add("0");//znacka OPRO - momentalne useless, len aby tam daco bolo
                        pocetIca++;
                        arrIco = new string[pocetIca];
                        arrIco = icaList.ToArray();
                        icaList.Add("%");
                    }
                    else /*if (podlaIca == 3)*/ // iba opro
                    {
                        //com = new SqlCommand("SELECT Ico FROM ciselnikIco WHERE Nazov LIKE 'MŠVVaŠ SR Bratislava%'", Con);
                        //using (SqlDataAdapter da = new SqlDataAdapter(com))
                        //{
                        //    da.Fill(dT);
                        //    com.ExecuteNonQuery();
                        //}
                        List<string> icaList = new List<string>();
                        //foreach (DataRow row in dT.Rows)
                        //{
                        //    icaList.Add((string)row[0]);
                        //    pocetIca++;
                        //}
                        icaList.Add(MSVSico);
                        pocetIca++;
                        icaList[0] = icaList[0].Trim();

                        arrIco = new string[pocetIca];
                        arrIco = icaList.ToArray();
                        icaList.Add("%");
                    }

                    //PRE KAZDE ICO
                    for (int icoI = 0; icoI < pocetIca; icoI++)
                    {
                        if (podlaIca == 1 || podlaIca == 2 || podlaIca == 3)
                        {
                            if (icoI > 0)
                                increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 11, 20, 20);
                            //HLADA ICO NAZOV

                            string org = "";

                            SqlCommand get_name = null;
                            get_name = new SqlCommand("SELECT Nazov from ciselnikIco where Ico = '" + arrIco[icoI] + "'", Con);
                            DataTable get_name_table = new DataTable();
                            if (!(podlaIca == 2 && icoI == 1) && podlaIca!=3)
                            {
                                using (SqlDataAdapter get_name_adapter = new SqlDataAdapter(get_name))
                                {
                                    get_name_adapter.Fill(get_name_table);
                                    get_name.ExecuteNonQuery();
                                }
                                if (get_name_table.Rows.Count != 0)
                                    org = (string)get_name_table.Rows[0][0];
                            }
                            else if (podlaIca == 2 && icoI == 1)
                                org = "OPRO (bez MŠVVaŠ SR)";
                            else if (podlaIca == 3)
                                org = "Ostatné priamo riadené organizácie + MŠVVaŠ SR (Vrátane PJ)";

                            while (start % count_for_page > count_for_page - 4)
                                increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 2, 11, 20, 20);

                            //NAZOV DO EXCELU S ODSADENIM PLUS KONTROLA CI NEROBI FINALNY SUMAR + NASTAVI HODNOTU ICO DO SELECTU
                            worksheet.get_Range(left_char + (start).ToString(), right_char + (start + 2).ToString()).Merge();
                            worksheet.get_Range(left_char + (start).ToString(), right_char + (start + 2).ToString()).VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                            worksheet.get_Range(left_char + (start).ToString(), right_char + (start + 2).ToString()).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                            if (get_name_table.Rows.Count != 0 || org == "OPRO (bez MŠVVaŠ SR)" || org == "Ostatné priamo riadené organizácie + MŠVVaŠ SR (Vrátane PJ)")
                                worksheet.Cells[start, 1] = "              Organizácia: " + org;
                            else
                                worksheet.Cells[start, 1] = "              Organizácia: Celkom";

                            //POSUN O 3 RIADKY KEDZE 3 ZLUCUJEME
                            increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 4, 7, 20, 20);
                            increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 4, 7, 20, 20);
                            increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 4, 7, 20, 20);
                            borderIco = start;
                        }

                        //TO ISTE PRE POLOZKY
                        ArrayList poolPolozka = new ArrayList();
                        ArrayList stackPolozka = new ArrayList();

                        SqlCommand startovaciePolozka = new SqlCommand("SELECT SUBSTRING(Pol, 1, 3) as PolSub FROM ciselnikPodpolozka WHERE Znacka = 1 AND (Pol LIKE '6%' OR POL LIKE '7%') ORDER BY PolSub ASC", Con);/////////////////////////tu som zmenil desc na asc
                        DataTable startovaciePolozka_table = new DataTable();
                        using (SqlDataAdapter startovaciePolozka_adapter = new SqlDataAdapter(startovaciePolozka))
                        {
                            startovaciePolozka_adapter.Fill(startovaciePolozka_table);
                            startovaciePolozka.ExecuteNonQuery();
                        }
                        foreach (DataRow row in startovaciePolozka_table.Rows) // Loop over the rows.
                        {
                            poolPolozka.Add(row[0].ToString());
                        }

                        while (poolPolozka.Count > 0)
                        {
                            //string command = "SELECT DISTINCT Prog FROM ciselnikProjektPrvok JOIN TableVstup tv ON tv.ProjektPrvok=Prog WHERE tv.Zdroj LIKE @Zdroj AND tv.Ico LIKE @Ico AND SUBSTRING(Prog, 1, " + valid_len.ToString() + ") LIKE SUBSTRING('" + pool[0].ToString() + "', 1, " + valid_len.ToString() + ") AND Prog != '" + pool[0].ToString() + "'";
                            //string command = "SELECT DISTINCT Prog FROM ciselnikProjektPrvok  WHERE  SUBSTRING(Prog, 1, " + valid_len.ToString() + ") LIKE SUBSTRING('" + pool[0].ToString() + "', 1, " + valid_len.ToString() + ") AND Prog != '" + pool[0].ToString() + "'";


                            int valid_lenPolozka = pocet_znakov_bez_nul(poolPolozka[0].ToString());
                            //string commandPolozka = "SELECT DISTINCT SUBSTRING(Pol, 1, 3) as PolSub FROM ciselnikPodpolozka JOIN TableVstup tv ON tv.Podpolozka=Pol WHERE tv.Zdroj LIKE @Zdroj AND tv.Ico LIKE @Ico AND SUBSTRING(Pol, 1, " + valid_lenPolozka.ToString() + ") LIKE SUBSTRING('" + poolPolozka[0].ToString() + "', 1, " + valid_lenPolozka.ToString() + ") AND SUBSTRING(Pol, 1, 3) != '" + poolPolozka[0].ToString() + "'";
                            string commandPolozka = "SELECT DISTINCT SUBSTRING(Pol, 1, 3) as PolSub FROM ciselnikPodpolozka WHERE SUBSTRING(Pol, 1, " + valid_lenPolozka.ToString() + ") LIKE SUBSTRING('" + poolPolozka[0].ToString() + "', 1, " + valid_lenPolozka.ToString() + ") AND SUBSTRING(Pol, 1, 3) != '" + poolPolozka[0].ToString() + "'";
                            SqlCommand childrenPolozka = new SqlCommand(commandPolozka, Con);
                            childrenPolozka.Parameters.AddWithValue("@Zdroj", arrZdroje[k] + "%");
                            childrenPolozka.Parameters.AddWithValue("@Ico", arrIco[icoI] + "%");
                            DataTable childrenPolozka_table = new DataTable();
                            DataTable sortedDTPolozka;
                            using (SqlDataAdapter childrenPolozka_adapter = new SqlDataAdapter(childrenPolozka))
                            {
                                childrenPolozka_adapter.Fill(childrenPolozka_table);
                                childrenPolozka.ExecuteNonQuery();
                                DataView dvPolozka = childrenPolozka_table.DefaultView;
                                dvPolozka.Sort = "PolSub DESC";
                                sortedDTPolozka = dvPolozka.ToTable();
                            }

                            int worthy_childrenPolozka = 0;
                            foreach (DataRow row in sortedDTPolozka.Rows)
                            { // Loop over the rows.

                                if (!stackPolozka.Contains(row[0].ToString()) && !stackPolozka.Contains(row[0]))
                                    worthy_childrenPolozka++;
                            }
                            if (worthy_childrenPolozka == 0)//ci ma dany program nejakych substring potomkov ktore este nie su v stacku
                            {
                                if (!stackPolozka.Contains(poolPolozka[0]))
                                    stackPolozka.Add(poolPolozka[0]);
                                poolPolozka.RemoveAt(0);
                            }
                            else
                                foreach (DataRow row in sortedDTPolozka.Rows)
                                    poolPolozka.Insert(0, row[0]);
                        }
                        stack.Add("0000");
                        //BEHANIE CEZ VSETKZ FUNKCNE KLASIFIKACIE
                        Boolean controlCiZapisal = false;
                        int counter = 0;
                        foreach (string program in stack)
                        {
                            controlCiZapisal = false;
                            int valid_len = 0;
                            bool lonely = false;
                            if (program != "0000")
                                valid_len = pocet_znakov_bez_nul(program.ToString());

                            if ((counter == 0 && stack.Count > 0 && program.Substring(0, 2) != stack[1].ToString().Substring(0, 2)) ||
                                (counter == stack.Count - 1 && program.Substring(0, 2) != stack[counter - 1].ToString().Substring(0, 2)) ||
                                (counter > 0 && counter < stack.Count - 1 && program.Substring(0, 2) != stack[counter - 1].ToString().Substring(0, 2) && program.Substring(0, 2) != stack[counter + 1].ToString().Substring(0, 2)))
                            {
                                valid_len += 1;
                                lonely = true;
                            }


                            //KONTROLA CI TREBA ROBIT ZA JEDNOTLIVE POLOZKY ALEBO LEN CELKOVY ZA DANU FUNKCNU KLASIFIKACIU
                            if (controlPodtrieda.Substring(0, valid_len) != program.Substring(0, valid_len)) // KAMIIIIIL
                            {
                                sum_all = new double[3];
                                //CYKLUS CEZ VSETKY POLOZKY
                                foreach (string programPolozka in stackPolozka)
                                {
                                    int valid_lenPolozka = pocet_znakov_bez_nul(programPolozka.ToString());
                                    SqlCommand vydavb = null;

                                    if ((podlaIca != 2 || icoI == 0) && podlaIca != 3)//ak to nie je msvvas + opro typ, alebo je to msvvas + opro typ a som na prvom ici, teda MSVVaS
                                    {
                                        if (druh_rozpoctu == 1)//vsetky
                                            vydavb = new SqlCommand("SELECT SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Zdroj LIKE @Zdroj AND Ico LIKE @Ico AND Substring(Podtrieda, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND Substring(Polozka, 1, " + valid_lenPolozka.ToString() + ") LIKE  SUBSTRING('" + programPolozka + "', 1, " + valid_lenPolozka.ToString() + ") AND KalendarnyDen LIKE @Datum " + ekonomickaKlasifikacia + " AND SyntetickyaleboFiktivny LIKE @Synteticky", Con);

                                        if (druh_rozpoctu == 2)//len rozpoctove
                                            vydavb = new SqlCommand("SELECT SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Zdroj LIKE @Zdroj AND Ico LIKE @Ico AND Substring(Podtrieda, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND Substring(Polozka, 1, " + valid_lenPolozka.ToString() + ") LIKE  SUBSTRING('" + programPolozka + "', 1, " + valid_lenPolozka.ToString() + ") AND KalendarnyDen LIKE @Datum " + ekonomickaKlasifikacia + " AND SyntetickyaleboFiktivny LIKE @Synteticky " + vynimky, Con);

                                        if (druh_rozpoctu == 3)//len mimorozpoctove
                                            vydavb = new SqlCommand("SELECT SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Zdroj LIKE @Zdroj AND Ico LIKE @Ico AND Substring(Podtrieda, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND Substring(Polozka, 1, " + valid_lenPolozka.ToString() + ") LIKE  SUBSTRING('" + programPolozka + "', 1, " + valid_lenPolozka.ToString() + ") AND KalendarnyDen LIKE @Datum " + ekonomickaKlasifikacia + " AND SyntetickyaleboFiktivny LIKE @Synteticky " + vynimky, Con);
                                        vydavb.Parameters.AddWithValue("@Ico", arrIco[icoI] + "%");
                                    }
                                    else if (podlaIca == 3)//IBA OPRO typ, ico nezalezi
                                    {
                                        if (druh_rozpoctu == 1)//vsetky
                                            vydavb = new SqlCommand("SELECT SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Zdroj LIKE @Zdroj AND Substring(Podtrieda, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND Substring(Polozka, 1, " + valid_lenPolozka.ToString() + ") LIKE  SUBSTRING('" + programPolozka + "', 1, " + valid_lenPolozka.ToString() + ") AND KalendarnyDen LIKE @Datum " + ekonomickaKlasifikacia + " AND SyntetickyaleboFiktivny LIKE @Synteticky", Con);

                                        if (druh_rozpoctu == 2)//len rozpoctove
                                            vydavb = new SqlCommand("SELECT SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Zdroj LIKE @Zdroj AND Substring(Podtrieda, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND Substring(Polozka, 1, " + valid_lenPolozka.ToString() + ") LIKE  SUBSTRING('" + programPolozka + "', 1, " + valid_lenPolozka.ToString() + ") AND KalendarnyDen LIKE @Datum " + ekonomickaKlasifikacia + " AND SyntetickyaleboFiktivny LIKE @Synteticky " + vynimky, Con);

                                        if (druh_rozpoctu == 3)//len mimorozpoctove
                                            vydavb = new SqlCommand("SELECT SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Zdroj LIKE @Zdroj AND Substring(Podtrieda, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND Substring(Polozka, 1, " + valid_lenPolozka.ToString() + ") LIKE  SUBSTRING('" + programPolozka + "', 1, " + valid_lenPolozka.ToString() + ") AND KalendarnyDen LIKE @Datum " + ekonomickaKlasifikacia + " AND SyntetickyaleboFiktivny LIKE @Synteticky " + vynimky, Con);
                                    }
                                    else//inak som msvvas + opro nastaveny na organizacii opro
                                    {
                                        if (druh_rozpoctu == 1)//vsetky
                                            vydavb = new SqlCommand("SELECT SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Zdroj LIKE @Zdroj AND Ico NOT LIKE @Ico AND Substring(Podtrieda, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND Substring(Polozka, 1, " + valid_lenPolozka.ToString() + ") LIKE  SUBSTRING('" + programPolozka + "', 1, " + valid_lenPolozka.ToString() + ") AND KalendarnyDen LIKE @Datum " + ekonomickaKlasifikacia + " AND SyntetickyaleboFiktivny LIKE @Synteticky", Con);

                                        if (druh_rozpoctu == 2)//len rozpoctove
                                            vydavb = new SqlCommand("SELECT SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Zdroj LIKE @Zdroj AND Ico NOT LIKE @Ico AND Substring(Podtrieda, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND Substring(Polozka, 1, " + valid_lenPolozka.ToString() + ") LIKE  SUBSTRING('" + programPolozka + "', 1, " + valid_lenPolozka.ToString() + ") AND KalendarnyDen LIKE @Datum " + ekonomickaKlasifikacia + " AND SyntetickyaleboFiktivny LIKE @Synteticky " + vynimky, Con);

                                        if (druh_rozpoctu == 3)//len mimorozpoctove
                                            vydavb = new SqlCommand("SELECT SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Zdroj LIKE @Zdroj AND Ico NOT LIKE @Ico AND Substring(Podtrieda, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND Substring(Polozka, 1, " + valid_lenPolozka.ToString() + ") LIKE  SUBSTRING('" + programPolozka + "', 1, " + valid_lenPolozka.ToString() + ") AND KalendarnyDen LIKE @Datum " + ekonomickaKlasifikacia + " AND SyntetickyaleboFiktivny LIKE @Synteticky " + vynimky, Con);
                                        vydavb.Parameters.AddWithValue("@Ico", arrIco[0] + "%");
                                    }

                                    vydavb.Parameters.AddWithValue("@Datum", datum);
                                    vydavb.Parameters.AddWithValue("@Zdroj", arrZdroje[k] + "%");
                                    vydavb.Parameters.AddWithValue("@Synteticky", synteticke[5]);

                                    DataTable vydavb_table = new DataTable();
                                    using (SqlDataAdapter vydavb_adapter = new SqlDataAdapter(vydavb))
                                    {
                                        vydavb_adapter.Fill(vydavb_table);
                                        vydavb.ExecuteNonQuery();
                                    }
                                    //AK COUNT JE VACSI AKO JEDNA TAK SA UDAJE NACHADZALIA  TYM PADOM VYPISUJEME HODNOTY
                                    if (vydavb_table.Rows[0][0] != null && vydavb_table.Rows[0][1] != null && vydavb_table.Rows[0][2] != null && (int)vydavb_table.Rows[0][3] > 0)
                                    {
                                        controlCiZapisal = true;
                                        controlPodtrieda = program;
                                        SqlCommand get_name = null;
                                        get_name = new SqlCommand("SELECT textKratky FROM ciselnikPodpolozka WHERE SUBSTRING(Pol, 1, 3) = '" + programPolozka + "' ORDER BY Pol ", Con);
                                        DataTable get_name_table = new DataTable();
                                        using (SqlDataAdapter get_name_adapter = new SqlDataAdapter(get_name))
                                        {
                                            get_name_adapter.Fill(get_name_table);
                                            get_name.ExecuteNonQuery();
                                        }

                                        if (get_name_table.Rows.Count != 0)
                                            worksheet.Cells[start, 3] = (string)get_name_table.Rows[0][0];

                                        worksheet.Cells[start, 1].NumberFormat = "@";
                                        worksheet.Cells[start, 1] = "0" + program.ToString().Substring(0, valid_len);
                                        worksheet.Cells[start, 2].NumberFormat = "@";
                                        worksheet.Cells[start, 2] = programPolozka.ToString().Substring(0, valid_lenPolozka);

                                        foreach (DataRow row in vydavb_table.Rows)
                                        {
                                            for (int rowCounter = 0; rowCounter <= 2; rowCounter++)
                                            {
                                                if (row[rowCounter].ToString() != "")
                                                {
                                                    worksheet.Cells[start, rowCounter + 4] = (double)row[rowCounter] / typVystupov;
                                                    if (valid_lenPolozka == 1)
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

                                        if (podlaIca == 0)
                                            increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 4, 7, 20, 20);
                                        else
                                            increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 4, 7, 20, 20);
                                    }
                                }
                                //SUMAR ZA FUNKCNU KLASIFIKACIU
                                if (controlCiZapisal == true)
                                {
                                    worksheet.Cells[start, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                                    worksheet.Cells[start, 1].NumberFormat = "@";
                                    worksheet.Cells[start, 1] = "0" + program.Substring(0, valid_len);
                                    worksheet.Cells[start, 2] = "f.k.";

                                    SqlCommand get_name = null;
                                    get_name = new SqlCommand("SELECT textPar FROM ciselnikPodtrieda WHERE Typ = '" + program + "' ORDER BY Typ ", Con);
                                    DataTable get_name_table = new DataTable();
                                    using (SqlDataAdapter get_name_adapter = new SqlDataAdapter(get_name))
                                    {
                                        get_name_adapter.Fill(get_name_table);
                                        get_name.ExecuteNonQuery();
                                    }

                                    if (get_name_table.Rows.Count != 0)
                                        worksheet.Cells[start, 3] = (string)get_name_table.Rows[0][0];
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

                                    if (podlaIca == 0)
                                        increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 4, 7, 20, 20);
                                    else
                                        increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 4, 7, 20, 20);

                                }
                            }
                            //OSTATNE SUMARY ZA SKUPINU, ODDIEL, A CELKOVO
                            if (lonely)
                                valid_len -= 1;
                            if (controlPodtrieda.Substring(0, valid_len) == program.Substring(0, valid_len) || lonely)
                            {
                                worksheet.Cells[start, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                                worksheet.Cells[start, 1].NumberFormat = "@";
                                worksheet.Cells[start, 1] = "0" + program.Substring(0, valid_len);
                                if (valid_len == 1)
                                    worksheet.Cells[start, 2] = "oddiel";
                                else
                                {
                                    if (valid_len == 2)
                                        worksheet.Cells[start, 2] = "skup.";
                                    else
                                        if (valid_len == 0)
                                        worksheet.Cells[start, 1] = "0";
                                }

                                SqlCommand vydavb = null;
                                if ((podlaIca != 2 || icoI == 0) && podlaIca != 3)//ak to nie je msvvas + opro typ, alebo je to msvvas + opro typ a som na prvom ici, teda MSVVaS
                                {
                                    if (druh_rozpoctu == 1)//vsetky
                                        vydavb = new SqlCommand("SELECT SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Zdroj LIKE @Zdroj AND Ico LIKE @Ico AND Substring(Podtrieda, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND KalendarnyDen LIKE @Datum " + ekonomickaKlasifikacia + " AND SyntetickyaleboFiktivny LIKE @Synteticky", Con);

                                    if (druh_rozpoctu == 2)//len rozpoctove
                                        vydavb = new SqlCommand("SELECT SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Zdroj LIKE @Zdroj AND Ico LIKE @Ico AND Substring(Podtrieda, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND KalendarnyDen LIKE @Datum " + ekonomickaKlasifikacia + " AND SyntetickyaleboFiktivny LIKE @Synteticky " + vynimky, Con);

                                    if (druh_rozpoctu == 3)//len mimorozpoctove
                                        vydavb = new SqlCommand("SELECT SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Zdroj LIKE @Zdroj AND Ico LIKE @Ico AND Substring(Podtrieda, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND KalendarnyDen LIKE @Datum " + ekonomickaKlasifikacia + " AND SyntetickyaleboFiktivny LIKE @Synteticky " + vynimky, Con);
                                    vydavb.Parameters.AddWithValue("@Ico", arrIco[icoI] + "%");
                                }
                                else if (podlaIca == 3)//IBA OPRO typ, ico nezalezi
                                {
                                    if (druh_rozpoctu == 1)//vsetky
                                        vydavb = new SqlCommand("SELECT SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Zdroj LIKE @Zdroj AND Substring(Podtrieda, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND KalendarnyDen LIKE @Datum " + ekonomickaKlasifikacia + " AND SyntetickyaleboFiktivny LIKE @Synteticky", Con);

                                    if (druh_rozpoctu == 2)//len rozpoctove
                                        vydavb = new SqlCommand("SELECT SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Zdroj LIKE @Zdroj AND Substring(Podtrieda, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND KalendarnyDen LIKE @Datum " + ekonomickaKlasifikacia + " AND SyntetickyaleboFiktivny LIKE @Synteticky " + vynimky, Con);

                                    if (druh_rozpoctu == 3)//len mimorozpoctove
                                        vydavb = new SqlCommand("SELECT SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Zdroj LIKE @Zdroj AND Substring(Podtrieda, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND KalendarnyDen LIKE @Datum " + ekonomickaKlasifikacia + " AND SyntetickyaleboFiktivny LIKE @Synteticky " + vynimky, Con);
                                }
                                else//inak som msvvas + opro nastaveny na organizacii opro
                                {
                                    if (druh_rozpoctu == 1)//vsetky
                                        vydavb = new SqlCommand("SELECT SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Zdroj LIKE @Zdroj AND Ico NOT LIKE @Ico AND Substring(Podtrieda, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND KalendarnyDen LIKE @Datum " + ekonomickaKlasifikacia + " AND SyntetickyaleboFiktivny LIKE @Synteticky", Con);

                                    if (druh_rozpoctu == 2)//len rozpoctove
                                        vydavb = new SqlCommand("SELECT SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Zdroj LIKE @Zdroj AND Ico NOT LIKE @Ico AND Substring(Podtrieda, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND KalendarnyDen LIKE @Datum " + ekonomickaKlasifikacia + " AND SyntetickyaleboFiktivny LIKE @Synteticky " + vynimky, Con);

                                    if (druh_rozpoctu == 3)//len mimorozpoctove
                                        vydavb = new SqlCommand("SELECT SUM(SchvalenyRozpocet), SUM(RozpocetPoZmenach), SUM(Skutocnost), count(*) FROM TableVstup tv WHERE Zdroj LIKE @Zdroj AND Ico NOT LIKE @Ico AND Substring(Podtrieda, 1, " + valid_len.ToString() + ") LIKE  SUBSTRING('" + program + "', 1, " + valid_len.ToString() + ") AND KalendarnyDen LIKE @Datum " + ekonomickaKlasifikacia + " AND SyntetickyaleboFiktivny LIKE @Synteticky " + vynimky, Con);
                                    vydavb.Parameters.AddWithValue("@Ico", arrIco[0] + "%");
                                }
                                vydavb.Parameters.AddWithValue("@Datum", datum);
                                vydavb.Parameters.AddWithValue("@Zdroj", arrZdroje[k] + "%");
                                vydavb.Parameters.AddWithValue("@Synteticky", synteticke[5]);

                                DataTable vydavb_table = new DataTable();
                                using (SqlDataAdapter vydavb_adapter = new SqlDataAdapter(vydavb))
                                {
                                    vydavb_adapter.Fill(vydavb_table);
                                    vydavb.ExecuteNonQuery();
                                }

                                SqlCommand get_name = null;
                                get_name = new SqlCommand("SELECT textPar FROM ciselnikPodtrieda WHERE Typ = '" + program + "' ORDER BY Typ ", Con);
                                DataTable get_name_table = new DataTable();
                                using (SqlDataAdapter get_name_adapter = new SqlDataAdapter(get_name))
                                {
                                    get_name_adapter.Fill(get_name_table);
                                    get_name.ExecuteNonQuery();
                                }

                                if (vydavb_table.Rows[0][0] != null && vydavb_table.Rows[0][1] != null && vydavb_table.Rows[0][2] != null && (int)vydavb_table.Rows[0][3] > 0)
                                {
                                    controlCiZapisal = true;

                                    //controlCiZapisal = true;
                                    controlPodtrieda = program;


                                    if (get_name_table.Rows.Count != 0)
                                        worksheet.Cells[start, 3] = (string)get_name_table.Rows[0][0];

                                    foreach (DataRow row in vydavb_table.Rows)
                                    {
                                        for (int rowCounter = 0; rowCounter <= 2; rowCounter++)
                                        {
                                            if (row[rowCounter].ToString() != "")
                                                worksheet.Cells[start, rowCounter + 4] = (double)row[rowCounter] / typVystupov;
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
                                    if (valid_len != 0)
                                    {
                                        if (podlaIca == 0)
                                            increment_start(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 4, 7, 20, 20);
                                        else
                                            increment_start_odsekove(ref start, worksheet, left_char, left_num, right_char, right_num, count_for_page, ref border, ref borderIco, ref strana, strana_rel_pos, zaokruhlenieMena, zaokruhleniePercenta, time, 4, 7, 20, 20);
                                    }
                                }
                                else
                                {
                                    if (get_name_table.Rows.Count != 0)
                                        worksheet.Cells[start, 3] = (string)get_name_table.Rows[0][0];
                                    for (int rowCounter = 0; rowCounter < 5; rowCounter++)
                                    {
                                        worksheet.Cells[start, rowCounter + 4] = 0;
                                    }
                                }
                            }
                            ++counter;
                        }

                        if (podlaIca == 0)
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
    }
}
