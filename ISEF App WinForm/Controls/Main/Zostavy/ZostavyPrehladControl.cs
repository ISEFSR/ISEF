namespace cvti.isef.winformapp.Controls.Main.Zostavy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using cvti.data.Core;
    using cvti.data.Files;
    using cvti.data.Output;

    /// <summary>
    /// Vizualna reprezentacia zostav 
    /// </summary>
    /// <remarks>
    /// Zostavy su delene do troch typov prijmove, vydavkove a trasferove
    /// </remarks>
    public partial class ZostavyPrehladControl : UserControl
    {
        /// <summary>
        /// Event signalizujuci zmenu vybranej zostavy
        /// </summary>
        /// <remarks>
        /// Treba ohandlovat vsetky 3 listboxy ( aby mohla byt polozka vybrana iba v 1 )
        /// </remarks>
        public event EventHandler<Zostava> VybranaZostavaZmenena;

        #region Constants And Constructors

        // Minimalna vyska pre listViews
        // vyska sa prisposobuje na zaklade poctu poloziek
        private const int MinimalHeight = 200;

        ZostavyManagerJson _manager;

        public ZostavyPrehladControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Properties And Methods

        /// <summary>
        /// Vrati vybranu zostavu
        /// </summary>
        /// <value>
        /// Vybrana zostava ako <see cref="Zostava"/>
        /// </value>
        public Zostava VybranaZostava { get; private set; } = null;

        /// <summary>
        /// Vrati vsetky zobrazene zostavy
        /// </summary>
        /// <returns>Zobrazene zostavy ako <see cref="IEnumerable{T}"/> kde T je <see cref="Zostava"/></returns>
        /// <remarks>
        /// Plan bol pouzit checkedlistbox preto vrat vybrane zostavy ( mozno to este zmenim )
        /// </remarks>
        public IEnumerable<Zostava> VratZobrazeneZostavy()
        {
            var z = new List<Zostava>();
            foreach (var i in listBoxPrijmove.Items)
                z.Add(i as Zostava);

            foreach (var i in listBoxVydavkove.Items)
                z.Add(i as Zostava);

            foreach (var i in listBoxTransferove.Items)
                z.Add(i as Zostava);

            return z;
        }

        /// <summary>
        /// Nastavi manager 
        /// </summary>
        /// <param name="manager">Manager ako <see cref="ZostavyManagerJson"/></param>
        public void NastavManager(ZostavyManagerJson manager)
        {
            if (_manager != null)
            {
                _manager.NewValueAdded -= _manager_NewZostavaAdded;
                _manager.ValueRemoved -= _manager_ZostavaRemoved;
            }

            _manager = manager;

            _manager.NewValueAdded += _manager_NewZostavaAdded;
            _manager.ValueRemoved += _manager_ZostavaRemoved;
        }

        /// <summary>
        /// Zobrazi zostavy na zaklade vybraneho okruhu v pripade ak je nastaveny manager
        /// </summary>
        /// <param name="okruh">Vybrany okruh ako <see cref="OkruhZostavyEnum"/></param>
        /// <remarks>
        /// uz si nepametam preco je nastav manager a zobraz zostavy splitnute do dvoch metod
        /// </remarks>
        public void ZobrazZostavy(OkruhZostavyEnum okruh)
        {
            if (_manager is null)
                return;

            listBoxPrijmove.Items.Clear();
            listBoxTransferove.Items.Clear();
            listBoxVydavkove.Items.Clear();

            foreach (var z in from zost in _manager.Values where zost.Okruh == okruh select zost)
            {
                _manager_NewZostavaAdded(this, z);
            }

            if (VybranaZostava is null)
            {
                if (listBoxPrijmove.Items.Count > 0)
                    listBoxPrijmove.SelectedIndex = 0;
                else if (listBoxVydavkove.Items.Count > 0)
                    listBoxVydavkove.SelectedIndex = 0;
                else if (listBoxTransferove.Items.Count > 0)
                    listBoxTransferove.SelectedIndex = 0;
            }
        }

        #endregion

        #region Event Handlers

        private void _manager_ZostavaRemoved(object sender, Zostava e)
        {
            // Just in case ( zostavy sa asi nebudu odstranovat )
            // ak by predsa tak ju vyhodim z preview
            switch (e.Hlavicka.Type)
            {
                case data.Output.HlavickaType.Prijmy:
                    listBoxPrijmove.Items.Remove(e);
                    break;
                case data.Output.HlavickaType.Transfery:
                    listBoxTransferove.Items.Remove(e);
                    break;
                case data.Output.HlavickaType.Vydavky:
                    listBoxVydavkove.Items.Remove(e);
                    break;
                case data.Output.HlavickaType.Nezaradene:
                    break;
            }
        }

        private void _manager_NewZostavaAdded(object sender, Zostava e)
        {
            // Just in case ( zostavy sa asi nebudu pridavat )
            // ak by predsa tak ju pridam do preview
            switch (e.Hlavicka.Type)
            {
                case data.Output.HlavickaType.Prijmy:
                    listBoxPrijmove.Items.Add(e);
                    break;
                case data.Output.HlavickaType.Transfery:
                    listBoxTransferove.Items.Add(e);
                    break;
                case data.Output.HlavickaType.Vydavky:
                    listBoxVydavkove.Items.Add(e);
                    break;
                case data.Output.HlavickaType.Nezaradene:
                    break;
            }
        }

        private void buttonRollPrijmy_Click(object sender, EventArgs e) => Roll(panelPrijmyData, listBoxPrijmove, sender as Button);
        private void buttonRollVydavky_Click(object sender, EventArgs e) => Roll(panelVydavkyData, listBoxVydavkove, sender as Button);
        private void buttonRollTransfery_Click(object sender, EventArgs e) => Roll(panelTransferyData, listBoxTransferove, sender as Button);

        private void listBoxPrijmove_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxPrijmove.SelectedIndex == -1)
                return;

            listBoxTransferove.SelectedIndex = -1;
            listBoxVydavkove.SelectedIndex = -1;

            VybranaZostava = listBoxPrijmove.SelectedItem as Zostava;

            VybranaZostavaZmenena?.Invoke(this, VybranaZostava);
        }

        private void listBoxVydavkove_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxVydavkove.SelectedIndex == -1)
                return;

            listBoxTransferove.SelectedIndex = -1;
            listBoxPrijmove.SelectedIndex = -1;

            VybranaZostava = listBoxVydavkove.SelectedItem as Zostava;

            VybranaZostavaZmenena?.Invoke(this, VybranaZostava);
        }

        private void listBoxTransferove_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxTransferove.SelectedIndex == -1)
                return;

            listBoxVydavkove.SelectedIndex = -1;
            listBoxPrijmove.SelectedIndex = -1;

            VybranaZostava = listBoxTransferove.SelectedItem as Zostava;

            VybranaZostavaZmenena?.Invoke(this, VybranaZostava);
        }

        #endregion

        #region Private Methods

        private void Roll(Panel clb, ListBox lBox, Button btn)
        {
            if (btn.Tag is null)
            {
                btn.Tag = 0;

                var buttonText = clb.Height == 0 ? "^" : "v";

                var toolTipText = clb.Height == 0 ? "Schovaj" : "Rozbaľ";

                var targetHeight = clb.Height == 0 ? CalculateHeight(lBox) : 0;
                var transition = new Transitions.Transition(new Transitions.TransitionType_Deceleration(350));
                transition.add(clb, "Height", targetHeight);
                transition.TransitionCompletedEvent += (snd, ea) => { btn.Tag = null; };
                transition.run();

                btn.Text = buttonText;

                toolTipInfo.SetToolTip(btn, toolTipText);
            }
        }

        private int CalculateHeight(ListBox lBox)
        {
            const int HeaderHeight = 30, ItemHeight = 25;

            var calculatedHeight = lBox.Items.Count * ItemHeight + HeaderHeight;

            return calculatedHeight < MinimalHeight ? MinimalHeight : calculatedHeight;
        }

        #endregion

        private void panelPrijmy_MouseEnter(object sender, EventArgs e)
        {
            //listBoxPrijmove.BorderStyle = BorderStyle.FixedSingle;
        }

        private void panelVydavky_MouseEnter(object sender, EventArgs e)
        {

        }

        private void panelTransfery_MouseEnter(object sender, EventArgs e)
        {

        }

        private void panelPrijmy_MouseLeave(object sender, EventArgs e)
        {
            //listBoxPrijmove.BorderStyle = BorderStyle.None;
        }

        private void panelVydavky_MouseLeave(object sender, EventArgs e)
        {

        }

        private void panelTransfery_MouseLeave(object sender, EventArgs e)
        {

        }
    }
}
