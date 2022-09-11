namespace cvti.isef.winformapp.Controls.Main.Import
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using cvti.data.Core;
    using cvti.data.Enums;

    /// <summary>
    /// Prvy krok importu udajov, vyber stupna
    /// </summary>
    public partial class ImportDataStupen : UserControl, IImportStep
    {
        public IEnumerable<HelpTileInfo> StepHelp => new HelpTileInfo[]
        {
             new HelpTileInfo("STUPEŇ", "Výber stupňa pre ktorý chcem naimportovať nové dáta do aplikácie. Každý stupeň ma svoju špecifickú formu importu. Napríklade Mestá a Obce sú importovańe z ôsmich DBF súborv. Verejné vysoké školy z jedného Excelu atď...", null)
        };

        public ImportDataStupen()
        {
            InitializeComponent();
            // TODO: generate the tiles automaticaly from dbo.cis_stupen table
            // you gonna need to add color, 2 images(normal and mouseover) and maybe some other columns into the tabl tho
        }

        private void tileControl3_MouseClicked(object sender, EventArgs e)
        {
            var tile = sender as TileControl;
            StupenSelected?.Invoke(this, (InputType)Convert.ToInt32(tile.Tag));
            MoveNext?.Invoke(tile, EventArgs.Empty);
        }

        public event EventHandler<InputType> StupenSelected;
        public event EventHandler<InputType> StupenMouseEntered;
        public event EventHandler MoveNext;
        public event EventHandler MovePrev;

        private void tileControl2_MouseEnteredTile(object sender, EventArgs e)
        {
            var tile = sender as TileControl;
            StupenMouseEntered?.Invoke(this, (InputType)Convert.ToInt32(tile.Tag));
        }

        public bool IsValid() => true;
    }
}
