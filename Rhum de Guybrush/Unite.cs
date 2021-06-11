namespace Rhum_de_Guybrush
{
    /// <summary>
    /// Classe Unite: modélise une unitée.
    /// </summary>
    public class Unite
    {
        #region Attributs
        /// <summary>
        /// Coordonnée X de l'unitée
        /// </summary>
        private readonly int x;
        /// <summary>
        /// Coordonnée Y de l'unitée
        /// </summary>
        private readonly int y;
        #endregion

        #region Accesseur
        /// <summary>
        /// Accesseur en lecture de l'attribut X.
        /// </summary>
        public int X => x;
        /// <summary>
        /// Accesseur en lecture de l'attribut Y.
        /// </summary>
        public int Y => y;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur de la classe <see cref="Unite"/>
        /// </summary>
        /// <param name="x">Coordonnée X de l'unitée.</param>
        /// <param name="y">Coordonnée Y de l'unitée.</param>
        public Unite(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        #endregion
    }
}
