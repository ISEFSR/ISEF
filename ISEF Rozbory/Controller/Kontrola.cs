using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.OleDb;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Globalization;

namespace ISEF_Rozbory
{
    class Kontrola
    {
        /*
         * Trieda Kontrola sluzi na skontrolovanie vstupnych Excel suborov, prechadza sa riadok po riadku a udaje v danom riadku su porovnavane s ciselnikmi.
         * Ak sa hodnoty nachadzaju v ciselnikoch, pokracuje sa dalej, v pripade, ze cely riadok je v poriadku, pridaju sa jeho udaje do databazy udajov.
         * V pripade, ze nejaka hodnota nie je v ciselnikoch (napr Ico), dana hodnota je pridana do prislusneho ciselnika a ostava na pouzivatelovi doplnit danu hodnotu.
         * Po kontrole udajov sa pripadne zmeny zapisu do suboru log_file.txt. Viac informacii v navode na pouzitie.
         */
        public string connection { get; set; } = "cvti.isef.winformapp.Properties.Settings.ISEFDB";


        public static void release_excel(Excel.Range range, Excel._Worksheet worksheet, Excel.Workbook workbook, Excel.Application app)
        {
            /*
             * Funkcia na uvolnenie procesu excelu a vsetkých parametrov spojených s ním.
             */
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Marshal.ReleaseComObject(range);
            Marshal.ReleaseComObject(worksheet);
            workbook.Close();
            Marshal.ReleaseComObject(workbook);
            app.Quit();
            Marshal.ReleaseComObject(app);
        }

        public int CiselnikCheck(string controlString, string controlTyp)
        {
            /*
             * Skontrolovanie, ci sa vstupny udaj nachadza v ciselnikoch.
             */
            string constring = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd;
            switch (controlTyp)
            {
                default:
                case "Ico":
                    cmd = new SqlCommand("SELECT COUNT(*) FROM ciselnikIco WHERE Ico= @Ico", con);
                    cmd.Parameters.AddWithValue("@Ico", controlString);
                    break;

                case "Podpolozka":
                    cmd = new SqlCommand("SELECT COUNT(*) FROM ciselnikPodpolozka WHERE Pol= @Pol", con);
                    cmd.Parameters.AddWithValue("@Pol", controlString);
                    break;

                case "Podtrieda":
                    cmd = new SqlCommand("SELECT COUNT(*) FROM ciselnikPodtrieda WHERE Typ= @Typ", con);
                    cmd.Parameters.AddWithValue("@Typ", controlString);
                    break;

                case "ProjektPrvok":
                    cmd = new SqlCommand("SELECT COUNT(*) FROM ciselnikProjektPrvok WHERE Prog= @Prog", con);
                    cmd.Parameters.AddWithValue("@Prog", controlString);
                    break;

                case "Zdroje":
                    cmd = new SqlCommand("SELECT COUNT(*) FROM ciselnikZdroje WHERE Zdroj= @Zdroj", con);
                    cmd.Parameters.AddWithValue("@Zdroj", controlString);
                    break;

                case "SyntetickyUcet":
                    cmd = new SqlCommand("SELECT COUNT(*) FROM SyntetickyUcet WHERE SyntetickyUcet= @SyntetickyUcet", con);
                    cmd.Parameters.AddWithValue("@SyntetickyUcet", controlString);
                    break;
            }

            con.Open();
            var result = cmd.ExecuteScalar();
            if ((int)result != 0)
            {
                //ciselnik je v tabulke
                con.Close();
                return 0;
            }
            else
            {
                //ciselnik bude pridany
                switch (controlTyp)
                {
                    case "Ico":
                        cmd.CommandText = "Insert Into ciselnikIco(Nazov,SkratenyNazov,CelyNazov,Segment,Ico,Okruh_s,Znak,Okruh_Zus,Okruh_Zuc) Values(" + "'" + "NULL" + "', '" + "NULL" + "', '" + "NULL" + "', '" + "NULL" + "', '" + controlString + "', '" + "NULL" + "', '" + "NULL" + "', '" + "NULL" + "', '" + "NULL" + "')";
                        break;

                    case "Podpolozka":
                        cmd.CommandText = "Insert Into ciselnikPodpolozka(Pol,textDlhy,textKratky,znacka, pol_tab8, pol_oon8, pol_plzu) Values(" + "'" + controlString + "', '" + "NULL" + "', '" + "NULL" + "', '" + "NULL" + "', '" + "NULL" + "', '" + "NULL" + "', '" + "NULL" + "')";
                        break;

                    case "Podtrieda":
                        cmd.CommandText = "Insert Into ciselnikPodtrieda(Typ,Znacka,textpar) Values(" + "'" + controlString + "', '" + "NULL" + "', '" + "NULL" + "')";
                        break;

                    case "ProjektPrvok":
                        cmd.CommandText = "Insert Into ciselnikProjektPrvok(Prog,Znacka,Skupina,Text,Skratenytext) Values(" + "'" + controlString + "', '" + "NULL" + "', '" + "NULL" + "', '" + "NULL" + "', '" + "NULL" + "')";
                        break;

                    case "Zdroje":
                        cmd.CommandText = "Insert Into ciselnikZdroje(Zdroj,Text,Skratenytext,Znacka,esfasr, pom_kod1, pom_kod2, pom_kod3, pom_kod4, pom_kod5) Values(" + "'" + controlString + "', '" + "NULL" + "', '" + "NULL" + "', '" + "NULL" + "', '" + "NULL" + "', '" + "NULL" + "', '" + "NULL" + "', '" + "NULL" + "', '" + "NULL" + "', '" + "NULL" + "')";
                        break;
                    default:
                        break;
                }

                cmd.ExecuteNonQuery();
                con.Close();
                return 1;
            }
        }

        public bool verify_data(string fileName, int prvyUdaj, int prvySheet)
        {
            /*
             * Funkcia na skontrolovanie celého excel súboru.
             * Vracia bool hodnotu pre zistenie, ci kontrola prebehla v poriadku bez zmeny ciselnikov (true) alebo nie (false).
             */

            string connectionstring = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            SqlCommand com;
            string str;
            int startSheet = prvySheet + 1;
            
            Excel.Application xlApp = new Excel.Application();//pre citanie excelu
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(fileName);
            int sheetsCount = xlWorkbook.Sheets.Count;
            Excel._Worksheet xlWorksheet = null;
            Excel.Range xlRange = null;
            int rowCount = 0;
            object[,] data_whole = null;

            bool contain_return_flag = true;//vracana hodnota inicializovana na true
            bool icoControl = false;
            bool podPolozkaControl = false;
            bool podTriedaControl = false;
            bool projektPrvokControl = false;
            bool zdrojeControl = false;
            var hashSyn = new HashSet<string>();
            var hashEK = new SortedSet<char>();

            if (!Directory.Exists("Kontrola_Log"))
                Directory.CreateDirectory("Kontrola_Log");

            var time = DateTime.Now;
            string formattedTime = time.ToString("yyyy.MM.dd HH-mm-ss");
            Directory.CreateDirectory("Kontrola_Log\\" + formattedTime);
            string path = "Kontrola_Log\\" + formattedTime + "\\log_file.txt";

            using (StreamWriter sw = new StreamWriter(path)) // subor pre detaily kontroly
            {
                sw.Write("Názov kontrolovaného súboru: " + fileName.Split('\\')[fileName.Split('\\').Length - 1]);
                sw.WriteLine();

                
                for (int sheet_counter = startSheet; sheet_counter <= sheetsCount; sheet_counter++) //pre citanie vsetkych harkov v danom exceli
                {
                    int count_added = 0;
                    xlWorksheet = xlWorkbook.Sheets[sheet_counter];//pre citanie excelu
                    xlRange = xlWorksheet.UsedRange;//pre citanie excelu
                    rowCount = xlRange.Rows.Count;//pre citanie excelu
                    data_whole = (object[,])xlRange.Value2;//funguje pre nacitanie celeho excelu do c# pola

                    using (var Con = new SqlConnection(connectionstring))
                    {
                        Con.Open();

                        string ico;
                        string zdroje;
                        string podtrieda;
                        string kalendarnyden = "";
                        string podpolozka;
                        string projekt_prvok;

                        bool POControl = fileName.Contains("PO");

                        
                        sw.Write("Vysledky po kontrole udajov zo zosita c." + sheet_counter + ":\n ");
                        sw.WriteLine();
                        

                        for (int index = prvyUdaj; index <= rowCount; index++)
                        {
                            
                            /*double SR, RpZ, S;
                            double.TryParse(data_whole[index, 18].ToString().Replace('.', ','), out SR);
                            double.TryParse(data_whole[index, 19].ToString().Replace('.', ','), out );
                            double.TryParse(data_whole[index, 20].ToString().Replace('.', ','), out S);*/
                            //Nahadzovanie do databazy s konrtrolou duplicity

                            try
                            {
                                //PREROBENIE DATUMU AK JE ZLY FORMAT
                                if (data_whole[index, 3].ToString().Length == 10)
                                    kalendarnyden = data_whole[index, 3].ToString();
                                else
                                    kalendarnyden = data_whole[index, 3].ToString().Substring(0, 3) + "0" + data_whole[index, 3].ToString().Substring(3);
                            } catch (NullReferenceException e) {
                                if (index == prvyUdaj)
                                    throw e;
                                else
                                    break;
                            }

                            if (POControl == true && CiselnikCheck(data_whole[index, 6].ToString(), "SyntetickyUcet") == 1)
                            {
                                //release_excel(xlRange, xlWorksheet, xlWorkbook, xlApp);
                                //MessageBoxes.ShowMessage("Syntetický účet nieje\n      vporiadku", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                                
                            }

                            hashSyn.Add(data_whole[index, 6].ToString());

                            str = "Insert Into TableVstup(Ico,Klient,KalendarnyDen,SyntetickyaleboFiktivny,Zdroj,Podtrieda,HlavnaKategoria,Kategoria,Polozka,Podpolozka,ProjektPrvok,TypZdroja,SchvalenyRozpocet,RozpocetPoZmenach,Skutocnost) Values(" + "'" + data_whole[index, 1].ToString() + "', '" + data_whole[index, 2].ToString() + "', '" + kalendarnyden + "', '" + data_whole[index, 6].ToString() + "', '" + data_whole[index, 7].ToString() + "', '" + data_whole[index, 11].ToString() + "', '" + data_whole[index, 12].ToString() + "', '" + data_whole[index, 13].ToString() + "', '" + data_whole[index, 14].ToString() + "', '" + data_whole[index, 15].ToString() + "', '" + data_whole[index, 16].ToString() + "', '" + data_whole[index, 17].ToString() + "', '" + data_whole[index, 18].ToString().Replace(',', '.') + "', '" + data_whole[index, 19].ToString().Replace(',', '.') + "', '" + data_whole[index, 20].ToString().Replace(',', '.') + "')";

                            ico = data_whole[index, 1].ToString();
                            zdroje = data_whole[index, 7].ToString();
                            podtrieda = data_whole[index, 11].ToString();
                            podpolozka = data_whole[index, 15].ToString();
                            projekt_prvok = data_whole[index, 16].ToString();

                            //testovanie ci su ciselniky v databaze
                            if (CiselnikCheck(ico, "Ico") == 1)
                            {
                                sw.Write("Riadok c. {0}, nepoznam ICO: {1} ", index, ico);
                                sw.WriteLine();
                                icoControl = true;
                            }
                            if (CiselnikCheck(podpolozka, "Podpolozka") == 1)
                            {
                                sw.Write("Riadok c. {0}, nepoznam PODPOLOZKU: {1} ", index, podpolozka);
                                sw.WriteLine();
                                podPolozkaControl = true;
                            }
                            else
                            {
                                hashEK.Add(podpolozka[0]);
                            }


                            if (podtrieda != "#" && CiselnikCheck(podtrieda, "Podtrieda") == 1)
                            {
                                sw.Write("Riadok c. {0}, nepoznam PODTRIEDA: {1} ", index, podtrieda);
                                sw.WriteLine();
                                podTriedaControl = true;
                            }
                            if (projekt_prvok != "#" && CiselnikCheck(projekt_prvok, "ProjektPrvok") == 1)
                            {
                                sw.Write("Riadok c. {0}, nepoznam PROJEKT/PRVOK: {1} ", index, projekt_prvok);
                                sw.WriteLine();
                                projektPrvokControl = true;
                            }
                            if (CiselnikCheck(zdroje, "Zdroje") == 1)
                            {
                                sw.Write("Riadok c. {0}, nepoznam PODTRIEDA: {1} ", index, zdroje);
                                sw.WriteLine();
                                zdrojeControl = true;
                            }
                            com = new SqlCommand(str, Con);

                            try
                            {
                                com.ExecuteNonQuery();
                                count_added++;
                            }
                            catch (System.Data.SqlClient.SqlException e)
                            {
                                continue;
                                /*Con.Close(); //hodenie vynimky vyssie pre ukazanie chybovej hlasky v GUI, ze sa udaj uz v databaze nachadza a nie je potreba dalej kontrolovat vstup
                                release_excel(xlRange, xlWorksheet, xlWorkbook, xlApp);
                                throw e;*/
                            }
                        }
                        
                        sw.Write("Pocet pridanych riadkov: " + count_added + "\n");
                        sw.WriteLine();

                        ////Kontrola ci ma vybehnut upozornenie ze bol aktualizovany nejaky ciselnik alebo vsetky ciselniky su aktualne
                        //if (icoControl == false && podPolozkaControl == false && podTriedaControl == false && projektPrvokControl == false && zdrojeControl == false)
                        //{
                        //    sw.WriteLine();
                        //    sw.Write("Kontrola prebehla v poriadku.");
                        //}
                        //else
                        //{
                        //    if (icoControl == true)
                        //    {
                        //        MessageBoxes.ShowMessage("Prebehla aktualizácia \n      číselnika ičo", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    }
                        //    if (podPolozkaControl == true)
                        //    {
                        //        MessageBoxes.ShowMessage("Prebehla aktualizácia \nčíselnika podpoložiek", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    }

                        //    if (podTriedaControl == true)
                        //    {
                        //        MessageBoxes.ShowMessage("Prebehla aktualizácia \n  číselnika podtried", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    }
                        //    if (projektPrvokControl == true)
                        //    {
                        //        MessageBoxes.ShowMessage("Prebehla aktualizácia \n  číselnika projektov", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    }
                        //    if (zdrojeControl == true)
                        //    {
                        //        MessageBoxes.ShowMessage("Prebehla aktualizácia \n   číselnika zdrojov", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    }
                        //}

                        Con.Close();
                    }
                }

                //Kontrola ci ma vybehnut upozornenie ze bol aktualizovany nejaky ciselnik alebo vsetky ciselniky su aktualne
                if (icoControl == false && podPolozkaControl == false && podTriedaControl == false && projektPrvokControl == false && zdrojeControl == false)
                {
                    sw.WriteLine();
                    sw.Write("Kontrola prebehla v poriadku.\n");
                    //sw.Write("Pocet pridanych riadkov: " + count_added + "\n");
                    //MessageBoxes.ShowMessage("Kontrola prebehla vporiadku", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //if (icoControl == true)
                    //{
                    //    MessageBoxes.ShowMessage("Prebehla aktualizácia \n      číselnika ičo", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                    //if (podPolozkaControl == true)
                    //{
                    //    MessageBoxes.ShowMessage("Prebehla aktualizácia \nčíselnika podpoložiek", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}

                    //if (podTriedaControl == true)
                    //{
                    //    MessageBoxes.ShowMessage("Prebehla aktualizácia \n  číselnika podtried", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                    //if (projektPrvokControl == true)
                    //{
                    //    MessageBoxes.ShowMessage("Prebehla aktualizácia \n  číselnika projektov", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                    //if (zdrojeControl == true)
                    //{
                    //    MessageBoxes.ShowMessage("Prebehla aktualizácia \n   číselnika zdrojov", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                }

                sw.WriteLine();
                sw.WriteLine();
                sw.Write("Syntetické účty: ");
                foreach (string k in hashSyn)
                {
                    sw.Write(k + " ");
                }

                sw.WriteLine();
                sw.WriteLine();
                sw.Write("Ekonomické klasifikácie: ");
                foreach (char k in hashEK)
                {
                    sw.Write(k + " ");
                }
            }
            release_excel(xlRange, xlWorksheet, xlWorkbook, xlApp);
            System.Diagnostics.Process.Start(path); //automaticke spustenie log_file suboru

            return contain_return_flag;//vrati true ak vsetky hodnoty su spravne, false ak sa nieco napisalo do log_file
        }
    }
}
