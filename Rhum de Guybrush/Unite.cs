namespace Rhum_de_Guybrush
{
    public class Unite
    {
        #region Attributs
        private int x;
        private int y;
        #endregion

        #region Accesseur
        public int X => x;
        public int Y => y;
        #endregion

        #region Constructeur
        public Unite(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        #endregion
    }
}
