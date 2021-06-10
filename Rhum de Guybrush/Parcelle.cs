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
        private TypeParcelle type;
        private List<Unite> unites;
        #endregion

        #region Accesseur
        public TypeParcelle Type => type;
        public IReadOnlyList<Unite> Unites => unites;
        public int Taille => Unites.Count;
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
        #endregion
    }
}
