namespace cvti.isef.winformapp
{
    /// <summary>
    /// Indikuje obsah controlu je zavisly na instancii <see cref="data.ISEFDataManager"/>
    /// </summary>
    public interface IDataManagerDependant
    {
        /// <summary>
        /// Vrati, nastavi manager pre manipulaciu s dataami apliakcie
        /// </summary>
        /// <remarks>
        /// Manager pre manipuluaciu s datami aplikacie ako <see cref="data.ISEFDataManager"/>
        /// </remarks>
        data.ISEFDataManager Manager { get; set; }
    }
}
