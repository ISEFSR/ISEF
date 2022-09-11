namespace cvti.isef.winformapp.Forms.Import
{
    using cvti.isef.winformapp.Controls.Import;
    using cvti.isef.winformapp.Helpers;
    using System;
    using System.Linq;
    using System.Windows.Forms;

    public partial class SpecifyImportFile : DialogBase
    {
        /// <summary>
        /// Initializes new form based on full path to excel file
        /// </summary>
        /// <param name="filePath">esta ku vstupnemu XLSX suboru</param>
        /// <exception cref="ArgumentException">V pripade ak na vstupe nedostanem .xlsx subor</exception>
        public SpecifyImportFile(string filePath)
        {
            if (!System.IO.Path.GetExtension(filePath).ToLower().Equals(".xlsx"))
                throw new ArgumentException($"Expected .xlsx excel file. {filePath}");

            InitializeComponent();

            // Title pre okno bude cela cesta k suboru
            this.Text = filePath;

            // Nacitam dostupne worksheety
            // ak sa mi nepodari nacitat worksheety zobrazim messagebox a zavriem okno 
            LoadWorksheets();
        }

        private void LoadWorksheets()
        {
            checkedListBoxWorksheets.Items.Clear();

            try
            {
                foreach (var ws in ExcelUtilities.GetWorksheets(this.Text))
                    checkedListBoxWorksheets.Items.Add(ws);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Pri pokuse o načítanie hárkov nastala neočakávaná chyba. " + ex.Message,
                    ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);

                DialogResult = DialogResult.Cancel;
            }
        }

        /// <summary>
        /// Gets the information for import xlsx file
        /// </summary>
        /// <value>
        /// Information for import xlsx file as <see cref="ExcelImportFile"/>
        /// </value>
        public ExcelImportFile ExcelFile
        {
            get
            {
                return new ExcelImportFile(Text,
                    (int)numericUpDownColumn.Value,
                    (int)numericUpDownRow.Value,
                    (int)numericUpDownHeader.Value,
                    (from i in checkedListBoxWorksheets.CheckedItems.Cast<Object>() select i.ToString()).ToArray());
            }
        }
    }
}
