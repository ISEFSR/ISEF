namespace cvti.isef.winformapp.Controls.Main.Generator.Conditios
{
    using System;
    using System.Linq;
    using System.Windows.Forms;
    using cvti.data.Conditions;
    using cvti.data.Enums;
    using cvti.data.Files;
    using cvti.isef.winformapp.Forms.Conditions;
    using cvti.isef.winformapp.Helpers;

    /// <summary>
    /// Vizualna reprezentacia podmienky
    /// </summary>
    public partial class SelectedConditionControl : UserControlWithNotification
    {
        #region Public Events

        /// <summary>
        /// Event signalizujuci ulozenie zmenenej podmienky
        /// </summary>
        public event EventHandler ConditionSaved;

        /// <summary>
        /// Event signalizujuci vytvorenie novej podmienky ako <see cref="Condition"/>
        /// </summary>
        public event EventHandler<Condition> NewConditionCreated;

        #endregion

        #region Private Variables And Constructor

        private Condition _originalCondition;
        private Condition _clonedCondition;

        private ConditionsManagerJson _manager;

        /// <summary>
        /// Inicializuje instanciu <see cref="SelectedConditionControl"/>
        /// </summary>
        /// <remarks>
        /// Naplni comboboxy na zakade enumeracii
        /// </remarks>
        public SelectedConditionControl()
        {
            InitializeComponent();

            foreach (var t in Enum.GetNames(typeof(ConditionType)))
                comboBoxConditionType.Items.Add(t);

            foreach (var o in Enum.GetNames(typeof(ConditionOperator)))
                comboBoxConditionOperator.Items.Add(o);

            foreach (var c in Enum.GetNames(typeof(AssuViewAvailableColumns)))
                comboBoxColumn.Items.Add(c);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Vrati zobrazenu podmienku ( ROOT )
        /// </summary>
        /// <value>
        /// Zobrazena podmienka ako <see cref="Condition"/>
        /// </value>
        public Condition ShownCondition => treeViewCondition.Nodes[0].Tag as Condition;

        /// <summary>
        /// Vrati prave vybranu podmienku
        /// </summary>
        /// <value>
        /// Vybrana podmienka ako <see cref="Condition"/>
        /// </value>
        public Condition SelectedCondition => treeViewCondition.SelectedNode?.Tag as Condition;

        #endregion

        #region Public Methods

        /// <summary>
        /// Zobrazi podmienku 
        /// </summary>
        /// <param name="manager">JSON manager ako <see cref="ConditionsManagerJson`"/></param>
        /// <param name="condition">Podmienka ako <see cref="Condition"/></param>
        public void ShowCondition(ConditionsManagerJson manager, Condition condition)
        {
            this._manager = manager;

            this.labelName.Text = condition.ConditionName;

            this._originalCondition = condition;
            this._clonedCondition = condition.CloneMe(true);

            treeViewCondition.Nodes.Clear();

            AddConditionToTreeRecursive(this._clonedCondition, treeViewCondition.Nodes, ConditionOperator.And, true);

            if (SelectedCondition is null)
                treeViewCondition.SelectedNode = treeViewCondition.Nodes[0];

            ShowTimedMessage(MessageType.Information, MessageLocation.Bottom, "Nová podmienka zobrazená",
                $"Podmienka {_clonedCondition.ConditionName} úspešne zobrazená. Zobrazená podmienka je naklonovaná takže pre zmenu je potrebné podmienku explicitne uložiť.");
        }

        #endregion

        #region Private Methods

        private void AddConditionToTreeRecursive(Condition condition, TreeNodeCollection nodeCollection, ConditionOperator op, bool root = false)
        {
            var node = root ? CreateNode(condition) : CreateNode(condition, op);
            nodeCollection.Add(node);

            foreach (var c in condition.InnerConditions)
            {
                AddConditionToTreeRecursive(c.Item1, node.Nodes, c.Item2);
            }

            treeViewCondition.ExpandAll();
        }

        private TreeNode CreateNode(Condition cnd) => new TreeNode(cnd.GetConditionString(false)) { Tag = cnd };

        private TreeNode CreateNode(Condition cnd, ConditionOperator op) => new TreeNode(op.ToString() + ": " + cnd.GetConditionString(false)) { Tag = cnd };

        private void AddNew(TreeNode node)
        {
            if (node is null)
                return;

            var selectedCondition = node.Tag as Condition;

            // Todo bude podla vyberu
            using (var cndForm = new NewConditionForm(_manager, selectedCondition))
            {
                if (cndForm.ShowDialog() == DialogResult.OK)
                {
                    //var newCondition = new Equals(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod1), "6");
                    var cnd = cndForm.SelectedCondition;

                    AddConditionToTreeRecursive(cnd, node.Nodes, cndForm.Operator);

                    selectedCondition.AddCondition(cnd, cndForm.Operator);

                    ShowTimedMessage(MessageType.Information, MessageLocation.Bottom, "Nová podmienka pridaná",
                        $"Nová podmienka úspešne pridaná pod podmienku {_clonedCondition.ConditionName}. Zobrazená podmienka je naklonovaná takže pre zmenu je potrebné podmienku explicitne uložiť.");
                }
            }
        }

        #endregion

        private void toolStripButtonAddSameLevel_Click(object sender, EventArgs e)
        {
            AddNew(treeViewCondition.SelectedNode?.Parent);
            _changeMadeByUser = false;
            ShowSelected();
            _changeMadeByUser = true;
        }

        private void toolStripButtonAddUnder_Click(object sender, EventArgs e)
        {
            AddNew(treeViewCondition.SelectedNode);
            _changeMadeByUser = false;
            ShowSelected();
            _changeMadeByUser = true;
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            ConditionSaved?.Invoke(this, EventArgs.Empty);
        }

        private void toolStripButtonSaveAs_Click(object sender, EventArgs e)
        {
            NewConditionCreated?.Invoke(this, VyskladajPodmienku(treeViewCondition.Nodes[0]));
        }

        private void toolStripButtonSaveSelected_Click(object sender, EventArgs e)
        {
            if (treeViewCondition.SelectedNode is null)
                return;

            NewConditionCreated?.Invoke(this, VyskladajPodmienku(treeViewCondition.SelectedNode));
        }

        private void toolStripButtonRestore_Click(object sender, EventArgs e) => ShowCondition(_manager, _originalCondition);

        private void toolStripButtonRemove_Click(object sender, EventArgs e)
        {
            if (treeViewCondition.SelectedNode is null)
                return;

            if (treeViewCondition.SelectedNode.Parent is null)
            {
                MessageBox.Show("Pre odstránenie root podmienky musíte použiť tlačidlo v ToolBare.", "Nemôžem odstrániť root", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            treeViewCondition.Nodes.Remove(treeViewCondition.SelectedNode);

            _changeMadeByUser = false;
            ShowSelected();
            _changeMadeByUser = true;

            ShowTimedMessage(MessageType.Information, MessageLocation.Bottom, "Podmienka odstránená",
                $"Podmienka úspešne odstránena aj s podstromom pre podmienku {_clonedCondition.ConditionName}. Zobrazená podmienka je naklonovaná takže pre zmenu je potrebné podmienku explicitne uložiť.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBoxSQL.Text = (treeViewCondition.Nodes[0].Tag as Condition).GetConditionString(true);
        }

        private bool _changeMadeByUser = true;

        private void treeViewCondition_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Vybrana podmienka v ramci stromu zmenena
            _changeMadeByUser = false;
            ShowSelected();
            _changeMadeByUser = true;
        }

        private void ShowSelected()
        {
            var cnd = treeViewCondition.SelectedNode.Tag as Condition;

            textBoxName.Text = cnd.ConditionName;
            textBoxValue.Text = cnd.Value.ToString();

            labelConditionType.Text = cnd.GetType().ToString();
            labelConditionNmae.Text = cnd.GetConditionString(false);

            checkBoxNegate.Checked = cnd.Negate;
            checkBoxWrap.Checked = cnd.Wrap;
            //comboBoxConditionType.SelectedIndex = cnd
            
            // TODO: rework
            if (cnd is Inlist)
            {
                comboBoxColumn.SelectedIndex = (int)(AssuViewAvailableColumns)Enum.Parse(typeof(AssuViewAvailableColumns), (cnd as Inlist).Column.ColumnName);
                comboBoxConditionType.SelectedIndex = (int)ConditionType.Inlist;
            }
            else if (cnd is GreaterThan)
            {
                comboBoxColumn.SelectedIndex = (int)(AssuViewAvailableColumns)Enum.Parse(typeof(AssuViewAvailableColumns), (cnd as GreaterThan).Column.ColumnName);
                comboBoxConditionType.SelectedIndex = (int)ConditionType.GreaterThan;
            }
            else if (cnd is LessThan)
            {
                comboBoxColumn.SelectedIndex = (int)(AssuViewAvailableColumns)Enum.Parse(typeof(AssuViewAvailableColumns), (cnd as LessThan).Column.ColumnName);
                comboBoxConditionType.SelectedIndex = (int)ConditionType.LessThan;
            }
            else if (cnd is Equals)
            {
                comboBoxColumn.SelectedIndex = (int)(AssuViewAvailableColumns)Enum.Parse(typeof(AssuViewAvailableColumns), (cnd as Equals).Column.ColumnName);
                comboBoxConditionType.SelectedIndex = (int)ConditionType.Equals;
            }
            else if (cnd is PlainTextCondition)
            {
                // TODO: dorobit
            }

            if (treeViewCondition.SelectedNode.Text.Substring(0, 3) == "And")
                comboBoxConditionOperator.SelectedIndex = (int)ConditionOperator.And;
            else
                comboBoxConditionOperator.SelectedIndex = (int)ConditionOperator.Or;

            textBoxSQL.Text = cnd.GetConditionString(true);
        }

        private void comboBoxConditionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_changeMadeByUser)
                return;
        }

        private void comboBoxConditionOperator_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_changeMadeByUser)
                return;
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            if (!_changeMadeByUser)
                return;

            // meno tiez nemozem menit 
        }

        private void textBoxValue_TextChanged(object sender, EventArgs e)
        {
            if (!_changeMadeByUser)
                return;

            // Atm nemozem menit hodnotu podmienky ( zmenit do buducna? )
        }

        private void checkBoxNegate_CheckedChanged(object sender, EventArgs e)
        {
            if (!_changeMadeByUser)
                return;

            SelectedCondition.Negate = checkBoxNegate.Checked;

            textBoxSQL.Text = SelectedCondition.GetConditionString(true);
        }

        private void toolStripButtonRemoveSubTree_Click(object sender, EventArgs e)
        {
            //throw new Exception();

            if (treeViewCondition.SelectedNode is null)
                return;

            while ((treeViewCondition.SelectedNode.Tag as Condition).InnerConditions.Count() > 0)
                (treeViewCondition.SelectedNode.Tag as Condition).RemoveAt(0);

            treeViewCondition.SelectedNode.Nodes.Clear();

            _changeMadeByUser = false;
            ShowSelected();
            _changeMadeByUser = true;
        }

        private void checkBoxWrap_CheckedChanged(object sender, EventArgs e)
        {
            if (!_changeMadeByUser)
                return;

            SelectedCondition.Wrap = checkBoxWrap.Checked;

            textBoxSQL.Text= SelectedCondition.GetConditionString(true);
        }

        private Condition VyskladajPodmienku(TreeNode node)
        {
            var podmienka = (node.Tag as Condition).CloneMe(false);
            VyksladajPodmienkuRec(podmienka, node);
            return podmienka;
        }

        private void VyksladajPodmienkuRec(Condition podmienka, TreeNode node)
        {
            foreach (TreeNode n in node.Nodes)
            {
                var p = (n.Tag as Condition).CloneMe(false);
                VyksladajPodmienkuRec(p, n);
                podmienka.AddCondition(p, (ConditionOperator)Enum.Parse(typeof(ConditionOperator), n.Text.Split(':')[0]));
            }
        }
    }
}