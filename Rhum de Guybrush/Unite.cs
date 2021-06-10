namespace Rhum_de_Guybrush
{
    public class Unite
    {
        #region Attributs
        private int x;
        private int y;
        #endregion

        #region Accesseur
        /// <summary>
        /// Permettre d'accéder aux variables locales de la classe Unite.
        /// </summary>
        public int X => x;
        public int Y => y;
        #endregion

        #region Constructeur
        /// <summary>
        /// Permettre d'instancier un objet de type Unite.
        /// </summary>
        public Unite(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        #endregion
    }
}
