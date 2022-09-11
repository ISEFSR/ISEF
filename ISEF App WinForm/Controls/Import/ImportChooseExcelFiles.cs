namespace cvti.isef.winformapp.Controls.Import
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using cvti.isef.winformapp.Controls.Main.Import;
    using cvti.isef.winformapp.Forms.Import;

    public partial class ImportChooseExcelFiles : UserControl, IImportStep
    {
        public event EventHandler MoveNext;
        public event EventHandler MovePrev;

        public ImportChooseExcelFiles()
        {
            InitializeComponent();
            if (DateTime.Now.Year <= numericUpDownYear.Maximum)
                numericUpDownYear.Value = DateTime.Now.Year;
        }

        public IEnumerable<HelpTileInfo> StepHelp => new HelpTileInfo[]
        {
            new HelpTileInfo("Kalendárny rok", "Výber kalendárneho roku pre vstupné dáta. Program neumožní import údajov v prípade ak sú v databáze už nahráte dáta za vybraný stupeň a vybraný kalendárny rok.", null),
            new HelpTileInfo("RO Súbor", "Výber kalendárneho roku pre vstupné dáta. Program neumožní import údajov v prípade ak sú v databáze už nahráte dáta za vybraný stupeň a vybraný kalendárny rok.", null),
            new HelpTileInfo("PO Súbor", "Výber kalendárneho roku pre vstupné dáta. Program neumožní import údajov v prípade ak sú v databáze už nahráte dáta za vybraný stupeň a vybraný kalendárny rok.", null)
        };

        public ExcelImportFile ROFile { get; private set; }
        public ExcelImportFile POFile { get; private set; }

        public bool IsValid()
        {
            return ROFile != null && POFile != null;
        }

        public int SelectedYear { get { return (int)numericUpDownYear.Value; } set { numericUpDownYear.Value = value; } }

        private void linkLabelFileRO_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var ll = sender as LinkLabel;

            if (openFileDialogFile.ShowDialog() == DialogResult.OK)
            {
                using (var specifyForm = new SpecifyImportFile(openFileDialogFile.FileName))
                {
                    if (specifyForm.ShowDialog() == DialogResult.OK)
                    {
                        if (ll.Tag.ToString() == "po")
                        {
                            linkLabelFiilePO.Text = openFileDialogFile.FileName;
                            POFile = specifyForm.ExcelFile;
                        }
                        else
                        {
                            linkLabelFileRO.Text = openFileDialogFile.FileName;
                            ROFile = specifyForm.ExcelFile;
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) => MoveNext?.Invoke(this, EventArgs.Empty);

        private void button2_Click(object sender, EventArgs e) => MovePrev?.Invoke(this, EventArgs.Empty);

        private void button3_Click(object sender, EventArgs e)
        {
            // Ok tlacidlo je disabled
            // tu by bolo treba potom este niekde zobrazit userovi tie nastavenia
            // ako riadok / stlpec kde su data riadok s hlavickou a worksheety
            if (folderBrowserDialog1.ShowDialog(ParentForm) == DialogResult.OK)
            {
                foreach (var f in System.IO.Directory.GetFiles(folderBrowserDialog1.SelectedPath))
                {
                    if (System.IO.Path.GetExtension(f).ToLower() != ".xlsx")
                        continue;

                    var file = System.IO.Path.GetFileNameWithoutExtension(f);
                    if (file.ToLower().Contains("po"))
                    {

                    }
                    else if (file.ToLower().Contains("ro"))
                    {

                    }
                }
            }
        }

        public void ResetControl()
        {
            if (DateTime.Now.Year <= numericUpDownYear.Maximum)
                numericUpDownYear.Value = DateTime.Now.Year;

            ROFile = null;
            POFile = null;

            linkLabelFiilePO.Text = "Vyber súbor...";
            linkLabelFileRO.Text = "Vyber súbor...";
        }
    }
}