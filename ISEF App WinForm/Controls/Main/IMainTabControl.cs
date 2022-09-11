namespace cvti.isef.winformapp.Controls.Main
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMainTabControl
    {
        System.Threading.Tasks.Task Activate(data.ISEFDataManager manager);

        void Deactivate();

        data.ISEFDataManager DataManager { get; }

        /// <summary>
        /// Vrati, nastavi text zobrazeny ako title ked je tab vybrany
        /// </summary>
        /// <value>
        /// Text zobrazeny ako title, ked je tab vybrany
        /// </value>
        string TitleText { get; set; }
        
        /// <summary>
        /// Vrati, nastavi text zobrazeny ako info text ked je tab vybrany
        /// </summary>
        /// <value>
        /// Text zobrazeny ako info, ked je tab vybrany
        /// </value>
        string InfoText { get; set; }

        System.Drawing.Image TitleImage { get; set; }

        System.Drawing.Image BackImage { get; set; }
    }
}