namespace cvti.isef.winformapp.Forms.Commands
{
    using cvti.data.Files;
    using cvti.data.Output;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    /// <summary>
    /// Umozni importovat prikazy zo suboru alebo priamo zo zoznamu prikazov
    /// </summary>
    public partial class ImportCommandsForm : DialogBase
    {
        public ImportCommandsForm(string fromFile)
            : this(FileManagerJson<SelectCommand>.ReadJsonFileData(fromFile))
        {
            
        }

        public ImportCommandsForm(IEnumerable<SelectCommand> commands)
        {
            InitializeComponent();

            //ShowWait();

            ReadConmmands(commands);
        }

        private void ReadConmmands(IEnumerable<SelectCommand> commands)
        {
            try
            {
                foreach (var c in commands)
                    checkedListBoxCommands.Items.Add(c, true);
            }
            catch (Exception e)
            {
                MessageBox.Show("Pri pokuse o načítanie podmienok z importného súboru nastala neočakávaná chyba: " + e.Message,
                    "Neočakávaná chyba " + e.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Cancel;
            }
            //finally
            //{
            //    HideWait();
            //}
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //await Task.Delay(1350);

            if (checkedListBoxCommands.Items.Count == 0)
                DialogResult = DialogResult.Cancel;

            //HideWait();
        }

        public override void HideWait()
        {
            base.HideWait();

            checkedListBoxCommands.Visible = true;

            labelTitle.Visible = true;
        }

        public override void ShowWait()
        {
            base.ShowWait();

            checkedListBoxCommands.Visible = false;

            labelTitle.Visible = false;
        }

        public IEnumerable<SelectCommand> GetSelectedCommands() 
            => checkedListBoxCommands.CheckedItems.Cast<SelectCommand>();
    }
}