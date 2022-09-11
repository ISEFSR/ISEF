namespace cvti.data.Output
{
    /// <summary>
    /// Typ hlavickoveho suboru 
    /// </summary>
    /// <remarks>
    /// Na zaklade typu hlavickoveho suboru je aplikovana globalna podmienka pre hlavicku
    /// </remarks>
    public enum HlavickaType
    {
        /// <summary>
        /// Data pre prijmy
        /// </summary>
        Prijmy,
        /// <summary>
        /// Data pre vydavky
        /// </summary>
        Vydavky,
        /// <summary>
        /// Data pre vydavky ( transfery )
        /// </summary>
        Transfery,
        /// <summary>
        /// Nezaradene bez podmienky
        /// </summary>
        Nezaradene
    }
}
