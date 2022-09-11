namespace cvti.isef.winformapp.Controls.Main.Import
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using cvti.isef.winformapp.Controls.Import;

    public partial class ImportChooseDBFFiles : UserControl, IImportStep
    {
        public event EventHandler MoveNext;
        public event EventHandler MovePrev;

        readonly LinkLabel[] _kraje;
        readonly Dictionary<string, LinkLabel> _mapaObce;

        public ImportChooseDBFFiles()
        {
            InitializeComponent();
            _mapaObce = new Dictionary<string, LinkLabel>();
            _mapaObce.Add(Properties.Settings.Default.FileObceBA, linkLabelBA);
            _mapaObce.Add(Properties.Settings.Default.FileObceBB, linkLabelBB);
            _mapaObce.Add(Properties.Settings.Default.FileObceTT, linkLabelTT);
            _mapaObce.Add(Properties.Settings.Default.FileObceTN, linkLabelTN);
            _mapaObce.Add(Properties.Settings.Default.FileObceNR, linkLabelNR);
            _mapaObce.Add(Properties.Settings.Default.FileObceZA, linkLabelZA);
            _mapaObce.Add(Properties.Settings.Default.FileObceKE, linkLabelKE);
            _mapaObce.Add(Properties.Settings.Default.FileObcePR, linkLabelFilePR);

            _kraje = new LinkLabel[]
            {
                linkLabelBA,
                linkLabelBB,
                linkLabelTT,
                linkLabelTN,
                linkLabelNR,
                linkLabelZA,
                linkLabelKE,
                linkLabelFilePR
            };
            if (DateTime.Now.Year <= numericUpDownYear.Maximum)
                numericUpDownYear.Value = DateTime.Now.Year;
        }

        public IEnumerable<HelpTileInfo> StepHelp => new HelpTileInfo[]
        {
            new HelpTileInfo("DBF Vstup", "Pre úspešný import údajov z DBF súborv je potrebné mať na PC nainštalovaný VFPOLEDB.1 data provider. Ten sa dá stiahnúť na stránkach Microsoftu. Následne je potrebné nastaviť jeden DBF súbor reprezentujúci údaje za jeden kraj za kalendárny rok.", null),
            new HelpTileInfo("Kraje", "Údaje za mestá a obce sú importované po krajoch zo vstuných DBF súborov. Štruktúra týchto súborov musí byť podľa vzoru. Do budúcna sa to môže preprogramovať variabilne a umožniť používateľovi namapovať minimálnu štruktúru.", null)

        };

        public DBFImportFile[] SelectedFiles => (from ll in _kraje select ll.Tag as DBFImportFile).ToArray();

        public bool IsValid() => !(from ll in _kraje where ll.Tag is null select ll).Any();

        public int SelectedYear { get => (int)numericUpDownYear.Value; set => numericUpDownYear.Value = value; }

        private void button1_Click(object sender, EventArgs e) => MoveNext?.Invoke(this, EventArgs.Empty);

        private void button2_Click(object sender, EventArgs e) => MovePrev?.Invoke(this, EventArgs.Empty);

        private void button3_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog(ParentForm) == DialogResult.OK)
            {
                foreach (var f in System.IO.Directory.GetFiles(folderBrowserDialog1.SelectedPath))
                {
                    if (System.IO.Path.GetExtension(f).ToLower() != ".dbf")
                        continue;

                    var file = System.IO.Path.GetFileNameWithoutExtension(f);
                    if (_mapaObce.ContainsKey(file))
                    {
                        _mapaObce[file].Tag = new DBFImportFile(f);
                        _mapaObce[file].Text = System.IO.Path.GetFileName(f);
                    }
                }
            }
        }

        private void linkLabelBA_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var ll = sender as LinkLabel;
            if (openFileDialog1.ShowDialog(ParentForm) == DialogResult.OK)
            {
                ll.Text = openFileDialog1.FileName;
                ll.Tag = new DBFImportFile(openFileDialog1.FileName);
            }
        }

        public void ResetControl()
        {
            if (DateTime.Now.Year <= numericUpDownYear.Maximum)
                numericUpDownYear.Value = DateTime.Now.Year;

            foreach (var ll in _kraje)
            {
                ll.Text = "Vyber súbor...";
                ll.Tag = null;
            }
        }
    }
}
