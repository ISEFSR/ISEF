namespace cvti.isef.winformapp.Controls.Main.Import
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using cvti.isef.winformapp.Forms.Import;
    using cvti.isef.winformapp.Controls.Import;

    public partial class ImportChooseExcelFile : UserControl, IImportStep
    {
        public event EventHandler MoveNext;
        public event EventHandler MovePrev;

        public ImportChooseExcelFile()
        {
            InitializeComponent();
            if (DateTime.Now.Year <= numericUpDownYear.Maximum)
                numericUpDownYear.Value = DateTime.Now.Year;
        }

        public ExcelImportFile SelectedFile { get; private set; }

        private void linkLabelFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (openFileDialogFile.ShowDialog() == DialogResult.OK)
            {
                using (var specifyForm = new SpecifyImportFile(openFileDialogFile.FileName))
                {
                    if (specifyForm.ShowDialog() == DialogResult.OK)
                    {
                        linkLabelFile.Text = openFileDialogFile.FileName;
                        SelectedFile = specifyForm.ExcelFile;
                    }
                }
            }
        }

        public int SelectedYear { get { return (int)numericUpDownYear.Value; } set { numericUpDownYear.Value = value; } }

        public string Titletext { get { return label1.Text; } set { label1.Text = value; } }
        public string InfoText { get { return label2.Text; } set { label2.Text = value; } }

        public IEnumerable<HelpTileInfo> StepHelp => new HelpTileInfo[]
        {
            new HelpTileInfo("Kalendárny rok", "Výber kalendárneho roku pre vstupné dáta. Program neumožní import údajov v prípade ak sú v databáze už nahráte dáta za vybraný stupeň a vybraný kalendárny rok.", null),
            new HelpTileInfo("Vstupný súbor", "Vstupný excelovský súbor obsahujúci potrebné údaje pre import. Súbor musí byť vo formáte XLSX a hárky ktoré chceme importovať musia mať špecifickú štruktúru. Musia obsahovať 16 stĺpcov v takomto poradí: ", null)
        };

        private void button1_Click(object sender, EventArgs e)=> MoveNext?.Invoke(this, EventArgs.Empty);

        private void button2_Click(object sender, EventArgs e) => MovePrev?.Invoke(this, EventArgs.Empty);

        public bool IsValid() => SelectedFile != null;

        public void ResetControl()
        {
            if (DateTime.Now.Year <= numericUpDownYear.Maximum)
                numericUpDownYear.Value = DateTime.Now.Year;
            linkLabelFile.Text = "Vyber súbor...";
            SelectedFile = null;
        }

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
    }
}
