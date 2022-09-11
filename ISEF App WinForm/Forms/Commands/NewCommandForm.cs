namespace cvti.isef.winformapp.Forms.Commands
{
    using cvti.data.Views;
    using cvti.data.Enums;
    using cvti.data.Files;
    using cvti.data.Output;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using System.Threading.Tasks;

    /// <summary>
    /// Umoznuje vytvorit novy SQL SELECT command, umoznuje vyber stlpcov
    /// </summary>
    public partial class NewCommandForm : DialogBase
    {
        private CommandsManagerJson _commands;

        public NewCommandForm(CommandsManagerJson commands)
        {
            _commands = commands;

            InitializeComponent();

            ShowWait();

            foreach (var c in Enum.GetNames(typeof(AssuViewAvailableColumns)))
                listBoxAvailableColumns.Items.Add(c);
        }

        public override void ShowWait()
        {
            base.ShowWait();

            label2.Visible = false;

            label5.Visible = false;

            textBoxCommandName.Visible = false;

            tableLayoutPanel1.Visible = false;
        }

        public override void HideWait()
        {
            base.HideWait();

            label2.Visible = true;

            label5.Visible = true;

            textBoxCommandName.Visible = true;

            tableLayoutPanel1.Visible = true;
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            await Task.Delay(1350);

            HideWait();
        }

        public string CommandName 
        {
            get { return textBoxCommandName.Text; }
            set { textBoxCommandName.Text = value; }
        }

        public IEnumerable<AssuViewAvailableColumns> SelectedColumns
        {
            get
            {
                return from i in listBoxSelectedColumns.Items.Cast<object>() select (AssuViewAvailableColumns)Enum.Parse(typeof(AssuViewAvailableColumns), i.ToString());
            }
        }

        public SelectCommand CreateCommand()
        {
            var cmd = new SelectCommand(CommandName);
            foreach (var c in SelectedColumns)
                cmd.AddColumn(AssuView.VratStlpec(c));

            return cmd;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_commands.GetValue(textBoxCommandName.Text) != null)
            {
                MessageBox.Show(this, $"Command named {textBoxCommandName.Text} already exists. Please choose a different name.",
                    "Command name taken", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                errorProviderInvalidName.SetError(textBoxCommandName, $"Command named {textBoxCommandName.Text} already exists. Please choose a different name.");
                return;
            }

            DialogResult = DialogResult.OK;
        }

        private void buttonMoveSelected_Click_1(object sender, EventArgs e)
        {
            if (listBoxAvailableColumns.SelectedIndex == -1)
                return;

            listBoxSelectedColumns.Items.Add(listBoxAvailableColumns.SelectedItem.ToString());
        }

        private void buttonRemoveSelected_Click_1(object sender, EventArgs e)
        {
            if (listBoxSelectedColumns.SelectedIndex == -1)
                return;

            listBoxSelectedColumns.Items.RemoveAt(listBoxSelectedColumns.SelectedIndex);
        }

        private void listBoxAvailableColumns_DoubleClick(object sender, EventArgs e)
        {

        }

        private void listBoxAvailableColumns_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.listBoxAvailableColumns.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                listBoxSelectedColumns.Items.Add(listBoxAvailableColumns.SelectedItem.ToString());
            }
        }

        private void listBoxSelectedColumns_DragDrop(object sender, DragEventArgs e)
        {
            Point point = listBoxSelectedColumns.PointToClient(new Point(e.X, e.Y));
            int index = this.listBoxSelectedColumns.IndexFromPoint(point);
            if (index < 0) index = this.listBoxSelectedColumns.Items.Count - 1;
            object data = listBoxSelectedColumns.SelectedItem;
            this.listBoxSelectedColumns.Items.Remove(data);
            this.listBoxSelectedColumns.Items.Insert(index, data);
        }

        private void listBoxSelectedColumns_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void listBoxSelectedColumns_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.listBoxSelectedColumns.SelectedItem == null) return;
            this.listBoxSelectedColumns.DoDragDrop(this.listBoxSelectedColumns.SelectedItem, DragDropEffects.Move);
        }

        private void listBoxSelectedColumns_DoubleClick(object sender, EventArgs e)
        {

        }

        private void listBoxSelectedColumns_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.listBoxSelectedColumns.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                listBoxSelectedColumns.Items.RemoveAt(index);
            }
        }
    }
}
