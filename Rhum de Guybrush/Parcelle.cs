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
        /// <summary>
        /// Permettre d'identifier le type d'une parcelle.
        /// </summary>
        public enum TypeParcelle
        {
            Normal,
            Mer,
            Foret,
        }
        #endregion

        #region Attributs
        /// <summary>
        /// Variables locales utiles à la création et modification d'une parcelle.
        /// </summary>
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
        /// <summary>
        /// Permettre l'accès a des variables locales.
        /// </summary>
        public TypeParcelle Type => type;
        public IReadOnlyList<Unite> Unites => unites;
        public int Taille => Unites.Count;
        #endregion

        #region Constructeur
        /// <summary>
        /// Permettre d'instancier une parcelle de 2 façons différentes.
        /// </summary>
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
        /// <summary>
        /// Permettre d'ajouter ou supprimer simplement un objet de type Unite.
        /// </summary>
        public void Ajouter(Unite unite) => unites.Add(unite);
        public void Supprimer(int index) => unites.RemoveAt(index);
        #endregion
    }
}
