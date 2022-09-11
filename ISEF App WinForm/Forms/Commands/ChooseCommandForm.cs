namespace cvti.isef.winformapp.Forms.Commands
{
    using cvti.data.Output;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Umozni vybrat command zo zoznamu commandov
    /// </summary>
    public partial class ChooseCommandForm : DialogBase
    {
        /// <summary>
        /// Inicializuje form pre vyber commandu
        /// </summary>
        /// <param name="commands">Dostupne commandy ako <see cref="IEnumerable{T}"/> kde T je <see cref="SelectCommand"/></param>
        public ChooseCommandForm(IEnumerable<SelectCommand> commands)
        {
            InitializeComponent();

            ShowWait();

            ShowImageIcon = false;

            foreach (var c in commands)
                listBoxCommands.Items.Add(c);
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            await Task.Delay(1350);

            panelCommands.Visible = true;

            HideWait();
        }

        public SelectCommand GetSelectedCndition()
            => listBoxCommands.SelectedItem as SelectCommand;
    }
}
