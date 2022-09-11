namespace cvti.isef.winformapp.Controls.Main.Data
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;
    using cvti.data.Columns;

    /// <summary>
    /// Vizualna reprezentacia pre <see cref="cvti.data.core.Column"/>
    /// </summary>
    /// <remarks>
    /// Sluzi na zobrazenie stlpcov v <see cref="SelectCommandControl"/>
    /// Umoznuje posuvat stlpec dolava, posuvat stlpec doprava, odstranit stlpec a editovat stlpec
    /// TODO: funkcionalita pre vyber 
    /// </remarks>
    public partial class ColumnTileControl : UserControl
    {
        #region Variables, Constants And Constructors

        private Column _column;

        private static readonly Color _numericColumnColor = Color.DarkOrange,
            _textColumnColor = Color.DarkBlue,
            _mouseOverColor = Color.Red,
            _selectionColor = Color.Black;

        /// <summary>
        /// Inciializuje novu instanciu triedy <see cref="ColumnTileControl"/>
        /// </summary>
        public ColumnTileControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Vrati, nastavi hodnotu rozhodujucu ci je umoznene DaD
        /// </summary>
        public bool AllowDragAndDrop { get; set; } = false;

        /// <summary>
        /// Vrati, nastavi zorbazeny stlpec ako <see cref="cvti.data.core.Column"/>
        /// </summary>
        public Column Column
        {
            get
            {
                return _column;
            }
            set
            {
                _column = value;
                UpadteColumnVisual();
            }
        }

        /// <summary>
        /// Vrati nastavi background color pre dlazdicu ako <see cref="Color"/>
        /// </summary>
        public Color TileBackgroundColor
        {
            get => panelContent.BackColor;
            set => panelContent.BackColor = value;
        }

        #endregion

        public void UpadteColumnVisual()
        {
            if (_column is null)
                return;

            labelTableName.Text = _column.TableName + "." + _column.ColumnName;
            textBoxAlias.Text = _column?.ColumnAlias;
            panelTop.BackColor = _column.IsNumeric ? _numericColumnColor : _textColumnColor;

            labelFunctions.Text = string.Empty;
            foreach (var f in _column.Functions)
            {
                labelFunctions.Text += (f.IsAggregate ? "+ " : "- ") + f.ToString() + Environment.NewLine;
            }
        }

        private void panelContent_MouseEnter(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void panelContent_MouseLeave(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void panelContent_MouseDown(object sender, MouseEventArgs e)
        {
            Debug.WriteLine("Mouse down");
            DoDragDrop(this, DragDropEffects.Move);
        }

        private void panelContent_MouseUp(object sender, MouseEventArgs e)
        {
            Debug.WriteLine("Mouse up");
        }

        private void buttonMoveRight_Click(object sender, EventArgs e) => MoveRight?.Invoke(this, EventArgs.Empty);
        private void buttonMoveLeft_Click(object sender, EventArgs e) => MoveLeft?.Invoke(this, EventArgs.Empty);
        private void buttonRemove_Click(object sender, EventArgs e) => RemoveButtonClicked?.Invoke(this, EventArgs.Empty);
        private void buttonEdit_Click(object sender, EventArgs e) => EditButtonClicked?.Invoke(this, EventArgs.Empty);

        private void textBoxAlias_MouseDown(object sender, MouseEventArgs e) => textBoxAlias.ReadOnly = false;

        private void textBoxAlias_MouseLeave(object sender, EventArgs e)
        {
            if (!textBoxAlias.Focused)
            {
                textBoxAlias.BackColor = panelContent.BackColor;
                panelContent.Invalidate();
            }
        }

        private void textBoxAlias_Leave(object sender, EventArgs e)
        {
            if ((textBoxAlias.Text == Column.ColumnAlias) | (textBoxAlias.Text == ""))
                return;

            var aliasChanged = new AliasChangedEventArgs(textBoxAlias.Text, Column.ColumnAlias);
            ChangeAliasEvent?.Invoke(this, aliasChanged);
            if (aliasChanged.Revert)
            {
                System.Media.SystemSounds.Beep.Play();
                textBoxAlias.Text = Column.ColumnAlias;
            }
            else
            {
                Column.ColumnAlias = textBoxAlias.Text;

            }
            textBoxAlias.BackColor = panelContent.BackColor;
            panelContent.Invalidate();
            textBoxAlias.ReadOnly = false;
        }

        private void textBoxAlias_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void buttonVisible_Click(object sender, EventArgs e)
        {

        }

        #region Public Events

        /// <summary>
        /// Event signalizujuci zmenu aliasu ako <see cref="AliasChangedEventArgs"/>
        /// </summary>
        /// <remarks>
        /// V pripade ak neni mozne zmenit alias je treba nastavit property Revert na True
        /// </remarks>
        public event EventHandler<AliasChangedEventArgs> ChangeAliasEvent;

        /// <summary>
        /// Eventy signalizujuce posun dolava resp. doprava pre stlpec
        /// </summary>
        public event EventHandler MoveRight, MoveLeft;

        /// <summary>
        /// Eventy signalizujuce stlacenie tlacidla pre odstranenie resp. editaciu stlpca
        /// </summary>
        public event EventHandler RemoveButtonClicked, EditButtonClicked;

        #endregion
    }

    public class AliasChangedEventArgs : EventArgs
    {
        public AliasChangedEventArgs(string alias, string previousAlias)
            : base()
        {
            Alias = alias;
            PreviousAlias = previousAlias;
        }
        public string Alias { get; private set; }
        public string PreviousAlias { get; private set; }
        public bool Revert { get; set; } = false;
    }
}