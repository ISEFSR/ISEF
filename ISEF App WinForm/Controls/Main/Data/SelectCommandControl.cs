namespace cvti.isef.winformapp.Controls.Main.Data
{
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using cvti.data.Columns;
    using cvti.data.Conditions;
    using cvti.data.Output;

    /// <summary>
    /// Vizualna reprezentacia pre <see cref="SelectCommand"/>
    /// </summary>
    /// <remarks>
    /// zobrazuje a umoznuje editovat stlpce a podmienku pre <see cref="SelectCommand"/>
    /// Nemeni nastaveny command nastaveny command sa pretransformuje ( naklonuje )
    /// </remarks>
    public partial class SelectCommandControl : UserControl
    { 
        #region Variables And Constructors

        ColumnTileControl _dragControl;

        bool _changedOrder = false;

        private Condition _comandCondition;

        public SelectCommandControl()
        {
            InitializeComponent();

            ResizeTiles();
        }

        #endregion

        #region Public Events

        public event EventHandler<Column> SelectionChanged;

        public event EventHandler EditCondition;

        public event EventHandler CommandChanged;

        public event EventHandler CommandOrderChanged;

        public event EventHandler<AliasChangedEventArgs> AliasChanged;

        #endregion

        #region Public Properties

        public SelectCommand Command
        {
            get
            {
                var cmd = new SelectCommand(string.Empty);

                foreach (ColumnTileControl clmn in flowLayoutPanelColumns.Controls)
                {
                    if (clmn.Column != null)
                        cmd.AddColumn(clmn.Column);
                }

                cmd.CommandCondition = _comandCondition;

                return cmd;
            }
            set
            {
                if (value is null)
                {
                    while (flowLayoutPanelColumns.Controls.Count > 0)
                        flowLayoutPanelColumns.Controls[0].Dispose();
                    return;
                }
                _comandCondition = value.CommandCondition;

                while (flowLayoutPanelColumns.Controls.Count != value.Columns.Count())
                {
                    if (flowLayoutPanelColumns.Controls.Count > value.Columns.Count())
                    {
                        flowLayoutPanelColumns.Controls[0].Dispose();
                    }
                    else
                    {
                        AddColumn(null);
                    }
                }

                var i = 0;
                foreach (var c in value.Columns)
                    (flowLayoutPanelColumns.Controls[i++] as ColumnTileControl).Column = c;
            }
        }

        public bool SelectionMode { get; set; } = false;

        public Column SelectedColumn { get; set; } = null;

        #endregion

        #region Public Methods

        public void AddColumn(Column clmn)
        {
            var ctrl = new ColumnTileControl();
            ctrl.Margin = new Padding(0, 0, 0, 0);
            ctrl.Size = GetSize();

            ctrl.RemoveButtonClicked += Ctrl_RemoveButtonClicked;
            ctrl.EditButtonClicked += Ctrl_EditButtonClicked;
            ctrl.MoveLeft += Ctrl_MoveLeft;
            ctrl.MoveRight += Ctrl_MoveRight;
            ctrl.ChangeAliasEvent += columnTileControl_ChangeAliasEvent;

            flowLayoutPanelColumns.Controls.Add(ctrl);

            if (clmn != null)
                ctrl.Column = clmn;
        }

        #endregion

        #region Event Handlers(ColumnControls)

        public void SetCondition(Condition Cond) 
        {
            _comandCondition = Cond;
        }

        private void Ctrl_MoveRight(object sender, EventArgs e)
        {
            var ctrl = sender as ColumnTileControl;

            var index = flowLayoutPanelColumns.Controls.GetChildIndex(ctrl, false);

            if (index < flowLayoutPanelColumns.Controls.Count - 1)
            {
                flowLayoutPanelColumns.Controls.SetChildIndex(ctrl, index + 1);
            }

            CommandOrderChanged?.Invoke(this, EventArgs.Empty);
        }

        private void Ctrl_MoveLeft(object sender, EventArgs e)
        {
            var ctrl = sender as ColumnTileControl;

            var index = flowLayoutPanelColumns.Controls.GetChildIndex(ctrl, false);

            if (index > 0)
            {
                flowLayoutPanelColumns.Controls.SetChildIndex(ctrl, index - 1);
                CommandOrderChanged?.Invoke(this, EventArgs.Empty);
            }  
        }

        private void Ctrl_EditButtonClicked(object sender, EventArgs e)
        {
            MessageBox.Show("Option not available.","Not available",MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void Ctrl_RemoveButtonClicked(object sender, EventArgs e)
        {
            var ctrl = sender as ColumnTileControl;
            if (MessageBox.Show($"Skutočne si prajete odstrániť vybraný stĺpec {ctrl.Column}?", "Odstránenie stĺpca",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ctrl.Dispose();
                CommandChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        #endregion

        #region Event Handlers(OtherControls)

        private void linkLabelCondition_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                EditCondition?.Invoke(this, EventArgs.Empty);
        }

        private async void flowLayoutPanelColumns_DragDrop(object sender, DragEventArgs e)
        {
            if (_changedOrder)
            {
                _changedOrder = false;
            }
        }

        private void flowLayoutPanelColumns_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
            _dragControl = (ColumnTileControl)e.Data.GetData(typeof(ColumnTileControl));
        }

        private void flowLayoutPanelColumns_DragOver(object sender, DragEventArgs e)
        {
            if (_dragControl is null)
                return;

            var _destination = (FlowLayoutPanel)sender;
            var _source = (FlowLayoutPanel)_dragControl.Parent;

            if (_destination != _source)
            {

            }
            else
            {
                var p = _destination.PointToClient(new Point(e.X, e.Y));
                var item = _destination.GetChildAtPoint(p) as ColumnTileControl;
                var index = _destination.Controls.GetChildIndex(item, false);

                _destination.Controls.SetChildIndex(_dragControl, index);
                _destination.Invalidate();
                _changedOrder = true;
            }
        }

        #endregion

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            ResizeTiles();
        }

        private void ResizeTiles()
        {
            foreach (ColumnTileControl clmnTile in flowLayoutPanelColumns.Controls)
            {
                clmnTile.Size = GetSize();
            }
        }

        public void RefreshVisual()
        {
            foreach (ColumnTileControl clmnTile in flowLayoutPanelColumns.Controls)
            {
                clmnTile.UpadteColumnVisual();
            }
        }

        private Size GetSize() => new Size(180, flowLayoutPanelColumns.Height - 20);

        private void columnTileControl_ChangeAliasEvent(object sender, AliasChangedEventArgs e)
        {
            
            foreach (ColumnTileControl clmnTile in flowLayoutPanelColumns.Controls) {
                if (clmnTile.Column.ColumnAlias == e.Alias)
                {
                    e.Revert = true;
                    return;
                }
            }
            AliasChanged?.Invoke(this, e);
        }
    }
}