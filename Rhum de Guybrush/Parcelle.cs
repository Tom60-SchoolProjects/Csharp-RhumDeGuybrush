using System.Collections.Generic;

namespace Rhum_de_Guybrush
{
    /// <summary>
    /// Classe Parcelle: modélise une Parcelle.
    /// </summary>
    public class Parcelle
    {
        #region Enumerations
        /// <summary>
        /// Enumération des types de parcelle.
        /// </summary>
        public enum TypeParcelle
        {
            Normal = 0,
            Foret = 32,
            Mer = 64,
        }
        #endregion

        #region Attributs
        /// <summary>
        /// Type de la Parcelle.
        /// </summary>
        private readonly TypeParcelle type;
        /// <summary>
        /// Liste des unités dans la Parcelle.
        /// </summary>
        private readonly List<Unite> unites;
        #endregion

        #region Accesseur
        /// <summary>
        /// Accesseur en lecture de l'attribut Type.
        /// </summary>
        /// <value>Type de la Parcelle.</value>
        public TypeParcelle Type => type;
        /// <summary>
        /// Accesseur en lecture de la liste unites.
        /// </summary>
        /// <value>Liste des unités dans la Parcelle.</value>
        public IReadOnlyList<Unite> Unites => unites;
        /// <summary>
        /// Accesseur en lecture du nombre d'Unites.
        /// </summary>
        /// <value>Taille d'une Parcelle.</value>
        public int Taille => Unites.Count;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur de la classe <see cref="Parcelle"/>
        /// </summary>
        /// <param name="type">Type de la parcelle.</param>
        /// <param name="unites">Liste des unités dans la Parcelle.</param>
        public Parcelle(TypeParcelle type, List<Unite> unites)
        {
            this.type = type;
            this.unites = unites;
        }
        /// <summary>
        /// Constructeur de la classe <see cref="Parcelle"/>
        /// </summary>
        /// <param name="type">Type de la parcelle.</param>
        public Parcelle(TypeParcelle type)
        {
            this.type = type;
            this.unites = new List<Unite>();
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Ajouter un objet de type Unite.
        /// </summary>
        public void Ajouter(Unite unite) => unites.Add(unite);
        /// <summary>
        /// Supprimer un objet de type Unite.
        /// </summary>
        public void Supprimer(int index) => unites.RemoveAt(index);
        #endregion
    }
}
