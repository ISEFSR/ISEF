namespace cvti.isef.winformapp.Controls.Main.Generator
{
    using System;
    using System.Windows.Forms;
    using cvti.isef.winformapp.Forms.Commands;
    using cvti.data.Output;
    using cvti.data.Files;
    using cvti.data;
    using cvti.isef.winformapp.Helpers;
    using cvti.isef.winformapp.Forms;
    using System.Linq;

    /// <summary>
    /// Vizualizacia prikazov nacitanych z JSON suboru
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    public partial class CommandsControl : UserControlWithNotification
    {
        #region Private Variables And Constructor

        // Indikuje ci sa prave rozbaluju nastavenia pre priakzy
        private bool _animating = false;

        // MAnager zodpovedny za manipulaciu s prikazmi
        private CommandsManagerJson _commands;

        // manager zodpovedny za manipulaciu s datami
        private ISEFDataManager _manager;

        // Premenne predstavujuce velkost rolujuceho okna nastaveni`
        private readonly int _collapsedHeight, _expandedHeight;

        public CommandsControl()
        {
            InitializeComponent();
            _collapsedHeight = panelTitle.Height;
            _expandedHeight = _collapsedHeight + 100;
        }

        #endregion

        #region Public Methods

        public void ReloadCommands(CommandsManagerJson commands, ISEFDataManager manager)
        {
            if (_commands != null)
            {
                _commands.NewValueAdded -= _commands_NewCommandCreated;
                _commands.ValueRemoved -= _commands_CommandRemoved;
            }

            _manager = manager;
            _commands = commands;

            _commands.NewValueAdded += _commands_NewCommandCreated;
            _commands.ValueRemoved += _commands_CommandRemoved;

            listViewCommands.Items.Clear();
            foreach (var cmd in _commands.Values)
            {
                AddListViewItem(cmd);
            }

            if (listViewCommands.Items.Count > 0)
                listViewCommands.Items[0].Selected = true;
        }

        public void Deactivate()
        {

        }

        #endregion

        #region Event Handlers(Commands)

        private void _commands_CommandRemoved(object sender, SelectCommand e)
        {
            ListViewItem remove = null;
            foreach (ListViewItem i in listViewCommands.Items)
            {
                var cmd = i.Tag as SelectCommand;
                if (cmd.CommandName == e.CommandName)
                {
                    remove = i;
                    break;
                }
            }

            if (remove != null)
                listViewCommands.Items.Remove(remove);
        }

        private void _commands_NewCommandCreated(object sender, SelectCommand e)
        {
            AddListViewItem(e);
        }

        #endregion

        #region Event Handlers(ToolStrip)

        private void toolStripButtonAddCommand_Click(object sender, EventArgs e)
        {
            // Otvori dialogove okno, ktore umozni pridat novy SQL SELECT command
            using (var f = new NewCommandForm(_commands))
                if (f.ShowDialog(ParentForm) == DialogResult.OK)
                {
                    _commands.AddValue(f.CreateCommand());
                    ShowTimedMessage(MessageType.Information, MessageLocation.Bottom,
                        "SQL SELECT dotaz vytvorený", "Nový SQL SELECT dotaz úspešne vytvorený, môžete ho nájsť pod menom: " + f.CommandName);
                }
        }

        private void toolStripButtonClone_Click(object sender, EventArgs e)
        {
            // Naklonuje existujuci SQL SELECT command
            var command = GetSelectedCommand();
            if (command is null)
            {
                MessageBox.Show("Pre použitie funkcie klonovania musíte najprv vybrať SQL SELECT príkaz, ktorý chcete naklonovať",
                    "Nevybraný príkaz", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var clone = command.CloneMe();
            var name = clone.CommandName ?? command.CommandName;
            while (_commands.GetValue(name) != null)
                name = $"_{name}";

            clone.CommandName = name;

            if (_commands.AddValue(clone))
            {
                MessageBox.Show("Vybraný SQL SELECT príkaz úspešne naklonovaný. Naklonovaný príkaz je uložený pod menom: " + name,
                    "Príkaz naklonovaný", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ShowTimedMessage(MessageType.Information, MessageLocation.Bottom,
                    "Príkaz naklonovaný", "Vybraný SQL SELECT príkaz úspešne naklonovaný. Naklonovaný príkaz je uložený pod menom: " + name);
            }
            else
            {
                MessageBox.Show("Pri pokuse o naklonovanie príkazu nastala neočakávaná chyba. Ak problém pretváva kontaktujte IT oddelenie.",
                    "Naklonovanie zlyhalo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                ShowTimedMessage(MessageType.Error, MessageLocation.Bottom,
                    "Naklonovanie zlyhalo", "Pri pokuse o naklonovanie príkazu nastala neočakávaná chyba. Ak problém pretváva kontaktujte IT oddelenie.");
            }
        }

        private void toolStripButtonRemoveCommand_Click(object sender, EventArgs e)
        {
            // Odstrani vybraty SQL SEELCT command
            if (listViewCommands.SelectedItems.Count == 0)
                return;

            OdstranCommand(GetSelectedCommand());
        }

        private void OdstranCommand(SelectCommand command)
        {
            if (MessageBox.Show($"Skutočne si prajete odstrániť vybraný SELECT príkaz? {command.CommandName}", "Odstránenie príkazu" +
                "Odstránenie príkazu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _commands.RemoveValue(command.CommandName);

                MessageBox.Show($"SQL SELECT príkaz '{command.CommandName}' úspešne odstránený", "Príkaz odstránený", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ShowTimedMessage(MessageType.Information, MessageLocation.Bottom, "Príkaz odstránený", $"SQL SELECT príkaz '{command.CommandName}' úspešne odstránený");

                if (listViewCommands.Items.Count > 0)
                    listViewCommands.Items[0].Selected = true;
            }
        }

        private void toolStripButtonExport_Click(object sender, EventArgs e)
        {
            // Exportuje priakzy do JSON suboru
            if (folderBrowserDialogExport.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var filePath = System.IO.Path.Combine(folderBrowserDialogExport.SelectedPath, $"commands{DateTime.Now.ToString("HHmmss")}.json");
                    _commands.ExportData(filePath);

                    MessageBox.Show("SELECT príkazy úspešne exportované do: " + filePath, "Export úspešný",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ShowTimedMessage(MessageType.Information, MessageLocation.Bottom, "Príkazy exportované", "SELECT príkazy úspešne exportované do: " + filePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Pri pokuse o export SELECT príkazov nastala neočakávaná chyba: " + ex.Message,
                        "Neočakávaná chyba " + ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);

                    ShowTimedMessage(MessageType.Error, MessageLocation.Bottom, "Neočakávaná chyba " + ex.GetType().ToString(), "Pri pokuse o export SELECT príkazov nastala neočakávaná chyba: " + ex.Message);
                }
            }
        }

        private void toolStripButtonImport_Click(object sender, EventArgs e)
        {
            if (openFileDialogImportFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileManagerJson<SelectCommand>.ReadJsonFileData(openFileDialogImportFile.FileName);
                }
                catch
                {
                    MessageBox.Show("Vybraný súbor neobsahuje žiadne SQL SELECT príkazy.",
                        "Súbor neobsahuje SELECT príkazy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (var importForm = new ImportCommandsForm(openFileDialogImportFile.FileName))
                    if (importForm.ShowDialog() == DialogResult.OK)
                    {
                        foreach (var newCommand in importForm.GetSelectedCommands())
                        {
                            while (_commands.GetValue(newCommand.CommandName) != null)
                                newCommand.CommandName = "_" + newCommand.CommandName;
                            _commands.AddValue(newCommand);
                        }

                        MessageBox.Show("Ímport príkazov zo súboru : " + openFileDialogImportFile.FileName + ", prebehol úspešne.", "Import úspešný",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ShowTimedMessage(MessageType.Information, MessageLocation.Bottom, "Import úspešný", "Ímport príkazov zo súboru : " + openFileDialogImportFile.FileName + ", prebehol úspešne.");
                    }
                    else
                    {
                        if (importForm != null)
                            importForm.Dispose();
                    }
            }
        }

        private void toolStripButtonGenerateDefault_Click(object sender, EventArgs e)
        {
            using (var importForm = new ImportCommandsForm(CommandsManagerJson.GenerateDefaultCommands()))
                if (importForm.ShowDialog() == DialogResult.OK)
                {
                    foreach (var newCommand in importForm.GetSelectedCommands())
                    {
                        while (_commands.GetValue(newCommand.CommandName) != null)
                            newCommand.CommandName = "_" + newCommand.CommandName;
                        _commands.AddValue(newCommand);
                    }

                    MessageBox.Show("Ímport default príkazov prebehol úspešne.", "Import úspešný",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ShowTimedMessage(MessageType.Information, MessageLocation.Bottom, "Import úspešný", "Ímport default príkazov prebehol úspešne.");
                }
                else
                {
                    if (importForm != null)
                        importForm.Dispose();
                }
        }

        #endregion

        #region Event Handlers(Others)

        private void listBoxCommands_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                //toolStripButtonRemoveCommand_Click(this, EventArgs.Empty);
                if (listViewCommands.SelectedItems.Count == -1)
                    return;

                var selectedCommands = listViewCommands.SelectedItems.Cast<ListViewItem>().ToArray();
                foreach (var c in selectedCommands)
                    OdstranCommand(c.Tag as SelectCommand);
            }
        }

        private void listBoxCommands_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var selected = GetSelectedCommand();

                selectedCommandControl.Enabled = (selected != null);
                selectedCommandControl.Visible = (selected != null);

                if (selected != null)
                    selectedCommandControl.ShowCommand(selected, _manager);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Pri zobrazovani príkazu nastala neočakávaná chyba: {ex.Message}",
                    $"Chyba {ex.GetType().ToString()}", MessageBoxButtons.OK, MessageBoxIcon.Error);

                selectedCommandControl.Enabled = false;
                selectedCommandControl.Visible = false;
            }
        }

        private void trackBarListView_ValueChanged(object sender, EventArgs e)
        {
            var view = (View)trackBarListView.Value;
            labelView.Text = view.ToString();
            listViewCommands.View = view;
        }

        private void buttonRoll_Click(object sender, EventArgs e)
        {
            if (_animating)
                return;

            _animating = true;

            var transition = new Transitions.Transition(new Transitions.TransitionType_Deceleration(250));

            var text = "v";
            var toolTip = "Zobraz filtre pre príkazy";

            var targetHeight = _collapsedHeight == panelTitle.Height ? _expandedHeight : _collapsedHeight;

            transition.add(panelTitle, "Height", targetHeight);

            if (targetHeight != _collapsedHeight)
            {
                text = "^";
                toolTip = "Schovaj filtre pre príkazy";
            }

            transition.TransitionCompletedEvent += (snd, ea) =>
            {
                _animating = false;
            };

            transition.run();

            buttonRoll.Text = text;
            toolTipInfo.SetToolTip(buttonRoll, toolTip);
        }

        #endregion

        #region Private Methods

        private SelectCommand GetSelectedCommand() => listViewCommands.SelectedItems.Count == 0 ? null : listViewCommands.SelectedItems[0].Tag as SelectCommand;

        private void selectedCommandControl_CommandCreated(object sender, EventArgs e)
        {
            // vytvoreny novy prikaz zo stareho
            using (var newCommand = new ComandNameForm(_manager.CoreFiles.Commands))
            {
                if (newCommand.ShowDialog() == DialogResult.OK)
                {
                    var cmd = selectedCommandControl.GetCommand();
                    cmd.CommandName = newCommand.NewCommandname;

                    var title = "SQL SELECT dotaz vytvorený"; ;
                    var message = $"SQL SELECT dotaz '{cmd.CommandName}' úspešne vytvorený a uložený";
                    MessageType type = MessageType.Information;
                    MessageBoxIcon icon = MessageBoxIcon.Information;

                    if (!_manager.CoreFiles.Commands.AddValue(cmd))
                    {
                        title = "SQL SELECT dotaz neuložený";
                        message = $"Nepodarilo sa mi uložiť SQL SELECT dotaz '{cmd.CommandName}'.";

                        type = MessageType.Warning;
                        icon = MessageBoxIcon.Warning;
                    }

                    // Reportuj progress
                    MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
                    ShowTimedMessage(type, MessageLocation.Bottom, title, message);
                }
            }
        }

        private void selectedCommandControl_CommandSaved(object sender, EventArgs e)
        {
            // overwrite seleected command
            var changedCommand = selectedCommandControl.GetCommand();
            var actuallCommand = GetSelectedCommand();

            // naklonujem podmienku
            actuallCommand.CommandCondition = changedCommand.CommandCondition?.CloneMe(true);

            // Vyprazdnim aktualne stlpce
            actuallCommand.ClearColumns();

            // Replacni vsetky stlpce
            foreach (var c in changedCommand.Columns)
                actuallCommand.AddColumn(c.CloneMe(true));

            // Reportuj uspesny progress
            MessageBox.Show($"SQL SELECT dotaz '{actuallCommand.CommandName}' úspešne zmenený a uložený", "SQL SELECT dotaz zmenený", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ShowTimedMessage(MessageType.Information, MessageLocation.Bottom,
                "SQL SELECT dotaz zmenený", $"SQL SELECT dotaz '{actuallCommand.CommandName}' úspešne zmenený a uložený");
        }

        private void AddListViewItem(SelectCommand command)
            => listViewCommands.Items.Add(new ListViewItem(command.CommandName)
            {
                Tag = command,
                ImageIndex = 0
            });

        #endregion
    }
}