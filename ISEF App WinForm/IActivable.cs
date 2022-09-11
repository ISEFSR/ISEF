namespace cvti.isef.winformapp
{
    /// <summary>
    /// Indikuje control, ktory implementuje interface musi byt aktivovany aby bol schopny zobrazit data
    /// </summary>
    public interface IActivable
    {
        /// <summary>
        /// Aktivuje control na zaklade managera na manipulaciu s datami
        /// </summary>
        /// <param name="manager">Manager zodpovedny za manipulaciu s datami ako <see cref="data.ISEFDataManager"/></param>
        System.Threading.Tasks.Task Activate(data.ISEFDataManager manager);

        /// <summary>
        /// Deaktivuje control, vyprazdni zobrazene data 
        /// </summary>
        /// <remarks>
        /// Aby pri velkom objeme dat zbytocne data neboli v pamati
        /// </remarks>
        void Deactivate();
    }
}