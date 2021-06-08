using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhum_de_Guybrush
{
    public class Parcelle
    {
        #region Enumerations
        public enum TypeParcelle
        {
            Normal,
            Mer,
            Foret,
        }
        #endregion

        #region Attributs
        // id => c
        private TypeParcelle type;
        // type => Normal
        private List<Unite> unites;
        /*
         * (0,0) => Vrai, (0,1) => Vrai, (0,2) => Vrai
         * (1,0) => Vrai, (1,1) => Vrai, (1,2) => Faux
         * (2,0) => Vrai, (2,1) => Vrai, (2,2) => Vrai
         * 3 x 3
         * */
        #endregion

        #region Accesseur
        public TypeParcelle Type => type;
        public IReadOnlyList<Unite> Unites => unites;
        #endregion

        #region Constructeur
        public Parcelle(TypeParcelle type, List<Unite> unites)
        {
            this.type = type;
            this.unites = unites;
        }
        public Parcelle(TypeParcelle type)
        {
            this.type = type;
            this.unites = new List<Unite>();
        }
        #endregion

        #region Méthodes
        public void Ajouter(Unite unite) => unites.Add(unite);
        public void Supprimer(int index) => unites.RemoveAt(index);

        public long TailleTotal() => Unites.Count;

        public long TailleMoyenne()
        {
            return 0;
        }
        #endregion
    }
}