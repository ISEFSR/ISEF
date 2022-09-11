namespace cvti.isef.winformapp.Controls.Main.Generator
{
    using System;
    using System.Linq;
    using System.Windows.Forms;
    using cvti.isef.winformapp.Forms.Conditions;
    using cvti.data.Conditions;
    using cvti.isef.winformapp.Forms;
    using cvti.data.Files;
    using cvti.data;
    using cvti.isef.winformapp.Helpers;
    using cvti.data.Enums;
    using cvti.isef.winformapp.Controls.Main.Generator.Conditios;

    /// <summary>
    /// Vizualizacia podmienok nacitanych z JSON suboru
    /// </summary>
    public partial class ConditionsControl : UserControlWithNotification
    {
        #region Variables And Constructor

        // Indikuje ci sa prave rozbaluju nastavenia pre priakzy
        private bool _animating = false;

        // Premenne predstavujuce velkost rolujuceho okna nastaveni`
        private readonly int _collapsedHeight, _expandedHeight;

        // MAnager zodpovedny za manipulaciu s prikazmi
        private ConditionsManagerJson _conditions;

        // manager zodpovedny za manipulaciu s datami
        private ISEFDataManager _manager;

        public ConditionsControl()
        {
            InitializeComponent();
            _collapsedHeight = panelTopTitle.Height;
            _expandedHeight = _collapsedHeight + 100;
        }

        #endregion

        #region Public Methods

        public void ReloadConditions(ConditionsManagerJson conditions, ISEFDataManager manager)
        {
            if (_conditions != null)
            {
                _conditions.ValueRemoved -= _conditions_ConditionRemoved;
                _conditions.NewValueAdded -= _conditions_NewConditionCreated;
            }

            _conditions = conditions;
            _manager = manager;

            _conditions.ValueRemoved += _conditions_ConditionRemoved;
            _conditions.NewValueAdded += _conditions_NewConditionCreated;

            // vyprazdnim a naplnim zoznam podmienakmi
            listViewConditions.Items.Clear();
            foreach (var c in conditions.Values.OrderBy(c => c.ConditionName))
            {
                var item = new ListViewItem(c.ConditionName)
                {
                    Tag = c,
                    ImageIndex = 0
                };
                listViewConditions.Items.Add(item);
            }

            // v pripade ak ziadna podmienka neni vybrana vyberam prvu
            if (listViewConditions.Items.Count > 0 && listViewConditions.SelectedItems.Count == 0)
                listViewConditions.Items[0].Selected = true;
        }

        public void Deactivate()
        {
            listViewConditions.Items.Clear();
        }

        #endregion

        #region Event Handlers(Conditions)

        private void _conditions_NewConditionCreated(object sender, Condition e)
        {
            var item = new ListViewItem(e.ConditionName)
            {
                Tag = e,
                ImageIndex = 0
            };
            listViewConditions.Items.Add(item);
        }

        private void _conditions_ConditionRemoved(object sender, Condition e)
        {
            var listItem = (from i in listViewConditions.Items.Cast<ListViewItem>() where i.Text == e.ConditionName select i).FirstOrDefault();
            if (listItem != null)
                listViewConditions.Items.Remove(listItem);
        }

        #endregion

        #region Event Handlers(ToolStrip)

        private void toolStripButtonRemoveSelected_Click(object sender, EventArgs e)
        {
            // Pokusi sa odstranit vybranu podmienku z vyberu
            // Event handler sa postara  o odstranenie podmienky z listview
            if (listViewConditions.SelectedItems.Count == 0)
                return;

            OdstranPodmienku(SelectedCondition());
        }

        private void OdstranPodmienku(Condition c)
        {
            if (MessageBox.Show($"Skutočne si prajete odstrániť vybranú podmienku {c.ConditionName}. Pozor táto operácia je nezvratná", "Odstránenie podmienky", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _conditions.RemoveValue(c.ConditionName);

                MessageBox.Show($"SQL podmienka '{c.ConditionName}' úspešne odstránená", "Podmienka odstránená", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ShowTimedMessage(MessageType.Information, MessageLocation.Bottom, "Podmienka odstránená", $"SQL podmienka '{c.ConditionName}' úspešne odstránená");

                if (listViewConditions.Items.Count > 0)
                    listViewConditions.Items[0].Selected = true;
            }
        }

        private void conditionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var newConditionForm = new CreateConditionForm((ConditionType)Convert.ToInt32((sender as ToolStripMenuItem).Tag)))
                if (newConditionForm.ShowDialog() == DialogResult.OK)
                    _manager.CoreFiles.Conditions.AddValue(newConditionForm.GetCreatedCondition());
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            // Naklonuje vybranu podmienku
            var cnd = SelectedCondition();
            if (cnd is null)
            {
                MessageBox.Show("Pre použitie funkcie klonovania musíte najprv vybrať podmienku, ktoru chcete naklonovať",
                    "Nevybrana podmienka", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var clone = cnd.CloneMe(true);
            var name = clone.ConditionName ?? cnd.ConditionName;

            while (_conditions.GetValue(name) != null)
                name = $"_{name}";

            clone.ConditionName = name;

            if (_conditions.AddValue(clone))
            {
                MessageBox.Show("Vybrana podmienka úspešne naklonovana. Naklonovana podmienka je uložena pod menom: " + name,
                    "Podmienka naklonovana", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Pri pokuse o naklonovanie podmienky nastala neočakávaná chyba. Ak problém pretváva kontaktujte IT oddelenie.",
                    "Naklonovanie zlyhalo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {
            // TODO create new condition
            using (var newConditionForm = new CreateConditionForm())
                if (newConditionForm.ShowDialog() == DialogResult.OK)
                    _manager.CoreFiles.Conditions.AddValue(newConditionForm.GetCreatedCondition());
        }

        private void toolStripButtonExportAll_Click(object sender, EventArgs e)
        {
            // Exportuje podmienky do JSON suboru
            // na vybratu cvesut
            if (folderBrowserDialogFolder.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var filePath = System.IO.Path.Combine(folderBrowserDialogFolder.SelectedPath, $"conditions{DateTime.Now:HHmmss}.json");
                    _conditions.ExportData(filePath);

                    MessageBox.Show("Podmienky úspešne exportované do: " + filePath, "Export úspešný",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Pri pokuse o export podmienok nastala neočakávaná chyba: " + ex.Message,
                        "Neočakávaná chyba " + ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void toolStripButtonImport_Click(object sender, EventArgs e)
        {
            // Importuje podmienky zo vstupneho JSON suboru
            // samozrejme JSON musi byt platnym JSON suborom pre podmienky
            if (openFileDialogImportExport.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileManagerJson<Condition>.ReadJsonFileData(openFileDialogImportExport.FileName);
                }
                catch
                {
                    MessageBox.Show("Vybraný súbor neobsahuje žiadne SQL podmienky.",
                        "Súbor neobsahuje podmienky", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (var importForm = new ImportConditionsForm(openFileDialogImportExport.FileName))
                    if (importForm.ShowDialog() == DialogResult.OK)
                    {
                        foreach (var cnd in importForm.GetSelectedConditions())
                        {
                            while (_conditions.GetValue(cnd.ConditionName) != null)
                                cnd.ConditionName = "_" + cnd.ConditionName;
                            _conditions.AddValue(cnd);
                        }

                        MessageBox.Show("Ímport podmineok zo súboru : " + openFileDialogImportExport.FileName + ", prebehol úspešne.", "Import úspešný",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (importForm != null)
                            importForm.Dispose();
                    }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            // Generuj defaultne podmienky
            // poskytne dialogove okno kde si user odklika ktore chce vytvorit
            using (var importForm = new ImportConditionsForm(ConditionsManagerJson.GenerateDefaultConditions()))
                if (importForm.ShowDialog() == DialogResult.OK)
                {
                    foreach (var cnd in importForm.GetSelectedConditions())
                    {
                        while (_conditions.GetValue(cnd.ConditionName) != null)
                            cnd.ConditionName = "_" + cnd.ConditionName;
                        _conditions.AddValue(cnd);
                    }

                    MessageBox.Show("Ímport defaultnych podmineok prebehol úspešne.", "Import úspešný",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (importForm != null)
                        importForm.Dispose();
                }
        }

        #endregion

        #region Private Methods(Others)

        private void listBoxConditions_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void listViewConditions_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var selected = SelectedCondition();

                selectedConditionControl.Enabled = (selected != null);
                selectedConditionControl.Visible = (selected != null);

                if (selected != null)
                    selectedConditionControl.ShowCondition(_conditions, SelectedCondition());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Pri zobrazovani podmienky nastala neočakávaná chyba: {ex.Message}",
                    $"Chyba {ex.GetType().ToString()}", MessageBoxButtons.OK, MessageBoxIcon.Error);

                selectedConditionControl.Enabled = false;
                selectedConditionControl.Visible = false;
            }
            
        }

        private void trackBarListView_ValueChanged(object sender, EventArgs e)
        {
            var view = (View)trackBarListView.Value;
            labelView.Text = view.ToString();
            listViewConditions.View = view;
        }

        private void buttonRoll_Click(object sender, EventArgs e)
        {
            if (_animating)
                return;

            _animating = true;

            var transition = new Transitions.Transition(new Transitions.TransitionType_Deceleration(250));

            var text = "v";
            var toolTip = "Zobraz filtre pre podmienky";

            var targetHeight = _collapsedHeight == panelTopTitle.Height ? _expandedHeight : _collapsedHeight;

            transition.add(panelTopTitle, "Height", targetHeight);

            if (targetHeight != _collapsedHeight)
            {
                text = "^";
                toolTip = "Schovaj filtre pre podmienky";
            }

            transition.TransitionCompletedEvent += (snd, ea) =>
            {
                _animating = false;
            };

            transition.run();

            buttonRoll.Text = text;
            toolTipInfo.SetToolTip(buttonRoll, toolTip);
        }

        private void selectedConditionControl_ConditionSaved(object sender, EventArgs e)
        {
            MessageBox.Show("Pre uloženie podmienky použite možnosť uložiť ako.",
                "Neimplementovaná možnosť", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void selectedConditionControl_NewConditionCreated(object sender, Condition e)
        {
            using (var cndForm = new SaveConditionAsForm(_manager.CoreFiles.Conditions))
            {
                if (cndForm.ShowDialog() == DialogResult.OK)
                {
                    var cnd = e.CloneMe(true);
                    cnd.ConditionName = cndForm.ConditionName;

                    _manager.CoreFiles.Conditions.AddValue(cnd);

                    MessageBox.Show("Nová podmienka úspešne vytvorená. Môžete ju nájsť pod menom " + cnd.ConditionName, "Podmienka vytvorená",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void listViewConditions_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (listViewConditions.SelectedItems.Count == -1)
                    return;

                var selectedConditions = listViewConditions.SelectedItems.Cast<ListViewItem>().ToArray();
                foreach (var c in selectedConditions)
                    OdstranPodmienku(c.Tag as Condition);
            }
        }

        #endregion

        #region Private Methods

        private Condition SelectedCondition() => 
            listViewConditions.SelectedItems.Cast<ListViewItem>().FirstOrDefault()?.Tag as Condition;   

        #endregion
    }
}
