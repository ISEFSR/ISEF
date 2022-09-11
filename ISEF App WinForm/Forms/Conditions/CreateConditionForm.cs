namespace cvti.isef.winformapp.Forms
{
    using cvti.data.Columns;
    using cvti.data.Conditions;
    using cvti.data.Enums;
    using cvti.data.Views;
    using cvti.isef.winformapp.Controls.Main.Generator.Conditios;
    using cvti.isef.winformapp.Controls.Main.Import;
    using System;

    public partial class CreateConditionForm : DialogBase
    {
        readonly HelpTileInfo[] _conditionTypeHelp = 
        {
            new HelpTileInfo("Rovná sa =", "Porovnáva ľavú a pravú stranu kde na ľavo vystupuje stĺpec zo vstupnej tabuľky a na pravej porovnávaná hodnota. Vracia true ak sa obe strany rovnajú.", null),
            new HelpTileInfo("Väčšie ako >", "Porovnáva ľavú a pravú stranu kde na ľavo vystupuje stĺpec zo vstupnej tabuľky a na pravej porovnávaná hodnota. Vracia true ak je hodnota naĺavo väčšia ako tá napravo.", null),
            new HelpTileInfo("Menšie ako <", "Porovnáva ľavú a pravú stranu kde na ľavo vystupuje stĺpec zo vstupnej tabuľky a na pravej porovnávaná hodnota. Vracia true ak je hodnota napravo väčšia ako tá naľavo.", null),
            new HelpTileInfo("Inlist", "Porovnáva ľavú a pravú stranu kde na ľavo vystupuje stĺpec zo vstupnej tabuľky a na pravej zoznam hodnôt. Vracia true ak sa hodnota naľavo nachádza v zozname.", null),
            new HelpTileInfo("Between", "Porovnáva ľavú a pravú stranu kde na ľavo vystupuje stĺpec zo vstupnej tabuľky a na pravej dve hodnoty. Vracia true ak je hodnota naĺavo medzi tými napravo. Teda ak je hodnota naľavo väčšia ako prvá hodntoa naprava a zároveň menšia ako druhá hodnota napravo.", null),
            new HelpTileInfo("Like", "Porovnáva ľavú a pravú stranu kde na ľavo vystupuje stĺpec zo vstupnej tabuľky a na pravej porovnávaná hodnota. Vracia true ak hodnota naĺavo spĺňa zadefinovaný vzor", null),
            new HelpTileInfo("Plain text", "Napíšte si vlastnú SQL podmienku takú akú práve potrebujete. Na úspešné vytovrenie podmienky je možné použiť nasledovné názvy stĺpcov:", null)
        };

        public CreateConditionForm()
        {
            InitializeComponent();

            ShowButtonsPanel = false;

            foreach (var clmn in Enum.GetNames(typeof(AssuViewAvailableColumns)))
                _conditionTypeHelp[6].Info1 += Environment.NewLine + clmn;

            verticalProgress1.AddStep("Výber operátoru", "Výber logického operátoru pre podmienku");
            verticalProgress1.AddStep("Zadefinovanie podmienky", "Výber stĺpca a porovnávanej hodnoty. Porovnávaná hodnota / hodnoty je vždy konštantná");
            //verticalProgress1.AddStep("Vytvorenie podmienky", "Finalizácia a zhrnutie vytvárania podnienky");

            helpControl2.SetHelpInfo(_conditionTypeHelp[0]);

            helpControl2.SetHelpInfo(new HelpTileInfo(
                "Vytvorenie podmienky",
                "Proces vytvorenia novej základnej podmienky pozostáva z dvoch krokov. Prvým je výber podmienky ako takej. V druhom kroku je potrebné vybrať stĺpec na ktorý podmienku aplikujem a hodnotu, ktorú porovnávam.",
                null));
        }

        public CreateConditionForm(ConditionType condition)
            : this()
        {
            universalConditionControl.SetType(condition);

            universalConditionControl.BringToFront();

            verticalProgress1.NextStep();

            ShowButtonsPanel = true;
        }

        public Condition GetCreatedCondition()
            => universalConditionControl.GetCondition();

        private void conditionTile_Enter(object sender, EventArgs e)
        {
            var tile = sender as TileControl;

            helpControl1.SetHelpInfo(_conditionTypeHelp[tile.TabIndex]);
        }

        private void tileControl1_MouseClicked(object sender, EventArgs e)
        {
            if (!(sender is TileControl tile))
                return;

            var type = (ConditionType)tile.TabIndex;

            universalConditionControl.SetType(type);

            universalConditionControl.BringToFront();

            verticalProgress1.NextStep();

            ShowButtonsPanel = true;
        }

        private void tileControl2_Load(object sender, EventArgs e)
        {

        }

        private void tileControl1_Load(object sender, EventArgs e)
        {

        }

        private void tileControlPlain_Load(object sender, EventArgs e)
        {

        }
    }
}
