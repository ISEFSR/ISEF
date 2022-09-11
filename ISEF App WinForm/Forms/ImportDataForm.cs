namespace cvti.isef.winformapp.Forms
{
    using cvti.data;
    using cvti.data.Enums;
    using System;

    public partial class ImportDataForm : DialogBase, IDataManagerDependant
    {
        public ImportDataForm(ISEFDataManager manager)
        {
            InitializeComponent();
            ShowButtonsPanel = false;
            Manager = manager ?? throw new ArgumentNullException(nameof(manager));
            importDataMain.Manager = manager;

            importDataMain.ImportStepChanged += ImportDataMain_ImportStepChanged;
        }

        private void ImportDataMain_ImportStepChanged(object sender, Controls.Main.Import.ImportDataMain.ImportSteps e)
        {
            switch (e)
            {
                case winformapp.Controls.Main.Import.ImportDataMain.ImportSteps.Import:
                    TitleText = $"ISEF Import údajov pre {importDataMain.SelectedImportType} a rok {importDataMain.SelectedYear}";
                    break;
                case winformapp.Controls.Main.Import.ImportDataMain.ImportSteps.VyberStupen:
                    TitleText = $"ISEF Import údajov výber stupňa";
                    break;
                case winformapp.Controls.Main.Import.ImportDataMain.ImportSteps.VyberVstup:
                    TitleText = $"ISEF Import údajov pre {importDataMain.SelectedImportType}";
                    break;
                case winformapp.Controls.Main.Import.ImportDataMain.ImportSteps.Zhrnutie:
                    TitleText = $"ISEF Import údajov pre {importDataMain.SelectedImportType} a rok {importDataMain.SelectedYear} zhrnutie";
                    break;
                case winformapp.Controls.Main.Import.ImportDataMain.ImportSteps.Moznosti:
                    TitleText = $"ISEF Import údajov pre {importDataMain.SelectedImportType} a rok {importDataMain.SelectedYear} možnosti";
                    break;
            }
        }

        public ImportDataForm(ISEFDataManager manager, InputType input) 
            : this(manager)
        {
            importDataMain.SelectStupen(input);
        }

        public ISEFDataManager Manager { get; set; }

        private void importDataMain_CloseMe(object sender, EventArgs e)
        {
            Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            importDataMain.Manager = null;
        }
    }
}
