namespace cvti.isef.winformapp.Controls.Main.Generator.Commands
{
    using System;
    using System.Linq;
    using System.Drawing;
    using System.Windows.Forms;
    using cvti.data.Core;
    using cvti.isef.winformapp.Forms;
    using cvti.isef.winformapp.Forms.Commands;
    using cvti.data.Output;
    using cvti.data.Files;
    using cvti.data;
    using cvti.data.Columns;
    using System.Collections.Generic;
    using cvti.data.Conditions;
    using cvti.data.Functions;
    using cvti.isef.winformapp.Helpers;
    using cvti.isef.winformapp.Forms.Conditions;

    /// <summary>
    /// Sluzi na editaciu SQL SELECT dotazu, edituje klonovany dotaz, pre ulozenie treba prepisat povodny
    /// </summary>
    public partial class SelectedCommandControl : UserControlWithNotification
    {
        public event EventHandler CommandSaved;
        public event EventHandler CommandCreated;

        #region Variables And Constructors

        private SelectCommand _originalCommand;
        private SelectCommand _clonedCommand;

        private ISEFDataManager _manager;

        private bool _shouldFire = false;

        public SelectedCommandControl()
        {
            InitializeComponent();

            ScintillaUtilities.SetScintillaControl(scintillaCommand);
            ScintillaUtilities.SetScintillaControl(scintillaCondition);
        }

        #endregion

        #region Public Methds

        /// <summary>
        /// Vrati vybrany SQL SELECT doataz
        /// </summary>
        /// <returns>Vybrany SQL SELECT dotaz ako <see cref="SelectCommand"/></returns>
        public SelectCommand GetCommand() => _clonedCommand;

        /// <summary>
        /// Zobrazi SQL SELECT dotaz
        /// </summary>
        /// <param name="command"></param>
        /// <param name="commands"></param>
        /// <param name="manager"></param>
        public void ShowCommand(SelectCommand command, ISEFDataManager manager)
        {
            _manager = manager;

            _originalCommand = command;

            _clonedCommand = command.CloneMe();

            _clonedCommand.CommandName = command.CommandName;

            labelCommandName.Text = command.CommandName;

            ShowCommandAndCondition();

            treeViewColumns.Nodes.Clear();
            foreach (var c in _clonedCommand.Columns)
            {
                var columnNode = new TreeNode(c.ColumnName) { Tag = c };

                foreach (var f in c.Functions)
                {
                    var functionNode = new TreeNode(f.ToString()) { Tag = f };
                    columnNode.Nodes.Add(functionNode);
                }

                treeViewColumns.Nodes.Add(columnNode);
            }

            treeViewColumns.ExpandAll();

            ShowTimedMessage(MessageType.Information, MessageLocation.Bottom,
                "Nový SQL dotaz zobrazený", $"Nový SQL dotaz '{command.CommandName}' úspešne zobrazený. Zmeny vykonané na dotaze sa uložia až po stlačení tlačidla.");
        }

        #endregion

        private void buttonSave_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripButtonAddColumn_Click(object sender, EventArgs e)
        {
            // TODO: zobraz dialog na pridanie noveho stlpca, pouzi dialog z prehladu
            var clmn = GetNewColumn();
            if (clmn is null)
                return;

            _clonedCommand.AddColumn(clmn);
            treeViewColumns.Nodes.Add(new TreeNode(clmn.ColumnName) { Tag = clmn } );

            ShowTimedMessage(MessageType.Information, MessageLocation.Bottom,
                "Nový stĺpec úspešne pridaný", $"Nový stĺpec {clmn.ToString()} úspešne pridaný pre SQL SELECT dotaz '{_clonedCommand.CommandName}'. Zmeny vykonané na dotaze sa uložia až po stlačení tlačidla.");

            ShowCommandAndCondition();
        }

        private void toolStripButtonInsertColumn_Click(object sender, EventArgs e)
        {
            // TODO: zobraz dialog na pridanie noveho stlpca, pouzi dialog z prehladu
            var clmn = GetNewColumn();
            if (clmn is null)
                return;

            _clonedCommand.AddColumn(clmn);
            treeViewColumns.Nodes.Insert(treeViewColumns.SelectedNode?.Index == -1 ? 0 : treeViewColumns.SelectedNode.Index,
                new TreeNode(clmn.ColumnName) { Tag = clmn });

            ShowTimedMessage(MessageType.Information, MessageLocation.Bottom,
                "Nový stĺpec úspešne vložený", $"Nový stĺpec {clmn.ToString()} úspešne vložený pre SQL SELECT dotaz '{_clonedCommand.CommandName}'. Zmeny vykonané na dotaze sa uložia až po stlačení tlačidla.");

            ShowCommandAndCondition();
        }

        private void toolStripButtonRemoveColumn_Click(object sender, EventArgs e)
        {
            var clmn = GetSelectedColumn();
            if (clmn is null)
                return;

            if (MessageBox.Show("Skutočne si prajete odstrániť vybraný stĺpec zo SELECT príkazu?",
                "Odstránenie stĺpca", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _clonedCommand.RemoveColumn(clmn);
                treeViewColumns.SelectedNode.Remove();
                //_command.RemoveColumn(clmnControl.Column);
                //clmnControl.Dispose();
                ShowTimedMessage(MessageType.Information, MessageLocation.Bottom,
                    "Stĺpec úspešne odstránený", $"Stĺpec {clmn.ToString()} úspešne odstránený z SQL SELECT dotazu '{_clonedCommand.CommandName}'. Zmeny vykonané na dotaze sa uložia až po stlačení tlačidla.");

                ShowCommandAndCondition();
            }
        }

        private Column GetNewColumn()
        {
            using (var newColumnForm = new NewColumnControl(_manager.CoreFiles.Conditions))
            {
                if (newColumnForm.ShowDialog() == DialogResult.OK)
                {
                    return newColumnForm.GetColumn();
                }
            }

            return null;
        }

        private void ShowCommandAndCondition()
        {
            scintillaCommand.Text = _clonedCommand.GenerateCommand().CommandText.ToLower();

            linkLabelRemoveCondition.Visible = linkLabelRemoveCondition.Enabled = (_clonedCommand.CommandCondition != null);

            if (linkLabelRemoveCondition.Visible)
            {
                linkLabelCreateCondition.Text = string.IsNullOrWhiteSpace(_clonedCommand.CommandCondition.ConditionName) ? "_" : _clonedCommand.CommandCondition.ConditionName;
            }
            else
            {
                linkLabelCreateCondition.Text = "Vyber podmienku";
            }

            scintillaCondition.Text = _clonedCommand.CommandCondition?.GetConditionString().ToLower();
        }

        private Column GetSelectedColumn() => treeViewColumns.SelectedNode.Tag as Column;
        private Function GetSelectedFunction() => treeViewColumns.SelectedNode.Tag as Function;

        private bool SelectNodeColumn => GetSelectedFunction() is null;

        private void ShowSelectedFunction()
        {
            var function = GetSelectedFunction();

            if (function is null)
                return;

            labelFunctionType.Text = function.GetType().ToString();

            labelFunctionName.Text = function.ToString();

            checkBoxAgg.Checked = function.IsAggregate;

            checkBoxNum.Checked = function.IsNumeric;

            //panelColumn.Height = 0;
            //panelFunction.Height = 250;
        }

        private void ShowSelectedColumn()
        {
            // Zobrazi vybrany stlpec
            _shouldFire = false;

            var column = GetSelectedColumn();
            if (column is null)
                return;

            labelColumn.Text = $"{column.TableName}.{column.ColumnName}";

            labelColumnType.Text = column.IsNumeric ? "Numerický stĺpec" : "Textový stĺpec";

            textBoxAlias.Text = column.ColumnAlias;

            checkBoxVisible.Checked = column.IsVisible;

            //panelColumn.Height = 250;
            //panelFunction.Height = 0;

            _shouldFire = true;
        }

        private void checkBoxVisible_CheckedChanged(object sender, EventArgs e)
        {
            if (!_shouldFire)
                return;

            // Zmena viditelnosti vybraneho stlpca
            GetSelectedColumn().IsVisible = checkBoxVisible.Checked;

            ShowCommandAndCondition();
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            CommandSaved?.Invoke(this, EventArgs.Empty);
        }

        private void toolStripButtonSaveAs_Click(object sender, EventArgs e)
        {
            CommandCreated?.Invoke(this, EventArgs.Empty);
        }

        private void toolStripDropDownButton1_DropDownOpening(object sender, EventArgs e)
        {
            var selected = GetSelectedColumn();

            toolStripMenuItemSUMIF.Enabled = selected != null && selected.IsNumeric;
            toolStripMenuItemSUM.Enabled = selected != null && selected.IsNumeric;
            toolStripMenuItemCOUNT.Enabled = selected != null && selected.IsNumeric; ;
            toolStripMenuItemMAX.Enabled = selected != null && selected.IsNumeric; ;
            toolStripMenuItemMIN.Enabled = selected != null && selected.IsNumeric; ;
            toolStripMenuItemROUND.Enabled = selected != null && selected.IsNumeric; ;

            toolStripMenuItemSUBSTRING.Enabled = selected != null && !selected.IsNumeric;
        }

        private void treeViewColumns_AfterSelect(object sender, TreeViewEventArgs e)
        {
            toolStripButtonSave.Enabled = true;
            toolStripButtonAddColumn.Enabled = true;
            toolStripButtonInsertColumn.Enabled = false;
            toolStripButtonRemoveColumn.Enabled = false;
            toolStripDropDownButtonAddFunction.Enabled = false;
            toolStripButtonRemoveFunction.Enabled = false;

            if (e.Node is null)
                return;

            toolStripButtonAddColumn.Enabled = false;

            if (e.Node.Tag is Column)
            {
                tableLayoutPanel1.Visible = tableLayoutPanel1.Enabled = true;
                tableLayoutPanel2.Visible = tableLayoutPanel2.Enabled = false;

                toolStripButtonAddColumn.Enabled = true;
                toolStripButtonInsertColumn.Enabled = true;
                toolStripButtonRemoveColumn.Enabled = true;
                toolStripDropDownButtonAddFunction.Enabled = true;

                ShowSelectedColumn();
            }
            else if (e.Node.Tag is Function)
            {
                tableLayoutPanel1.Visible = tableLayoutPanel1.Enabled = false;
                tableLayoutPanel2.Visible = tableLayoutPanel2.Enabled = true;

                toolStripButtonRemoveFunction.Enabled = true;

                ShowSelectedFunction();
            }
        }

        private void toolStrip_addFunction(object sender, EventArgs e)
        {
            // Pridavam novu funkciu
            // pre pouzitie by mal byt vybrany nejaky stlpec
            var parentColumn = GetSelectedColumn();
            if (parentColumn is null)
                return;
            
            // Tag pri polozke urcuje funkciu
            var toolStripItem = sender as ToolStripMenuItem;
            var tagValue = int.Parse( toolStripItem.Tag.ToString() );

            Function newFunction = null;
            switch (tagValue)
            {
                case 0: // sumif
                    using (var newSum = new NewConditionalSumFunctionForm(_manager.CoreFiles.Conditions.Values))
                        if (newSum.ShowDialog() == DialogResult.OK)
                            newFunction = newSum.GetSelectedFunction();
                    break;
                case 1: // sum
                    newFunction = new Sum();
                    break;
                case 2: // count
                    newFunction = new Count();
                    break;
                case 3: // max
                    newFunction = new Max();
                    break;
                case 4: // min
                    newFunction = new Min();
                    break; 
                case 5: // round
                    using (var newRound = new NewRoundFunctionForm())
                        if (newRound.ShowDialog() == DialogResult.OK)
                            newFunction = newRound.GetSelectedFunction();
                    break;
                case 6: // substring
                    using (var newSubstring = new NewSubstringFunctionForm())
                        if (newSubstring.ShowDialog() == DialogResult.OK)
                            newFunction = newSubstring.GetSelectedFunction();
                    break;

            }

            if (newFunction is null)
                return;

            // Pridam funkciu pod stlpec
            parentColumn.AddFunction(newFunction);

            // Pridam funckiu pod tree view
            var functionNode = new TreeNode(newFunction.ToString()) { Tag = newFunction };
            treeViewColumns.SelectedNode.Nodes.Add(functionNode);
            treeViewColumns.SelectedNode = functionNode;

            // Zobraz notifikaciu o uspesnom ostraneni
            ShowTimedMessage(MessageType.Information, MessageLocation.Bottom,
                "Funkcia pridaná", $"Nová funkcia {newFunction.ToString()} úspešne pridaná pod stĺpec {parentColumn.ToString()} pre príkaz {_clonedCommand.CommandName}");

            ShowCommandAndCondition();
        }

        private void toolStripButtonRemoveFunction_Click(object sender, EventArgs e)
        {
            // Odstranujem vybranu funkciu
            // funkcia musi byt vybrana
            var function = GetSelectedFunction();
            if (function is null)
                return;

            // parent node by mal byt column v kazdom pripade
            // kontrola iba pre istotu
            var parentNode = treeViewColumns.SelectedNode.Parent;
            var column = parentNode.Tag as Column;
            if (column is null)
                return;

            // Odstranim funkciu zo stlpca
            column.RemoveFunction(function);

            // Odstranim funkciu z tree view
            treeViewColumns.SelectedNode.Remove();

            // Nastavim vybrany node na parent stlpec
            treeViewColumns.SelectedNode = parentNode;

            // Zobraz notifikaciu o uspesnom ostraneni
            ShowTimedMessage(MessageType.Information, MessageLocation.Bottom,
                "Funkcia odstránená", $"Funkcia {function.ToString()} úspešne odstránená zo stĺpca {column.ToString()} pre príkaz {_clonedCommand.CommandName}");

            ShowCommandAndCondition();
        }

        private void textBoxAlias_TextChanged(object sender, EventArgs e)
        {
            var clmn = GetSelectedColumn();
            if (clmn is null)
                return;

            clmn.ColumnAlias = textBoxAlias.Text;

            ShowCommandAndCondition();
        }

        private void linkLabelRemoveCondition_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MessageBox.Show("Skutočne si prajete odstrániť podmienku pre príkaz", "Odstránenie podmienky", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _clonedCommand.CommandCondition = null;

                ShowCommandAndCondition();

                ShowTimedMessage(MessageType.Information, MessageLocation.Bottom, "Podmienka odstránená",
                    $"Podmienka pre príkaz '{_clonedCommand.CommandName}' úspešne odstránená. POZOR pre uchovanie zmien musíte príkaz uložiť, alebo vytvoriť nový.");
            }
        }

        private void linkLabelCreateCondition_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var chooseConditionForm = new ChooseConditionForm(_manager.CoreFiles.Conditions.Values))
                if (chooseConditionForm.ShowDialog() == DialogResult.OK)
                {
                    _clonedCommand.CommandCondition = chooseConditionForm.GetSelectedCondition();

                    ShowCommandAndCondition();

                    ShowTimedMessage(MessageType.Information, MessageLocation.Bottom, "Podmienka pridaná",
                        $"Nová podmienka pre príkaz '{_clonedCommand.CommandName}' úspešne pridaná. POZOR pre uchovanie zmien musíte príkaz uložiť, alebo vytvoriť nový.");
                }
        }

        private void panelColumn_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}