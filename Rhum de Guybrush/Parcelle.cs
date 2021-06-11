using System;
using System.Collections.Generic;

namespace Rhum_de_Guybrush
{
    /// <summary>
    /// Classe Parcelle: modélise une parcelle.
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
        /// Type de parcelle.
        /// </summary>
        private readonly TypeParcelle type;
        /// <summary>
        /// Liste des unités.
        /// </summary>
        private readonly List<Unite> unites;
        #endregion

        #region Accesseur
        /// <summary>
        /// Accesseur en lecture de l'attribut type.
        /// </summary>
        /// <value>La type de la parcelle.</value>
        public TypeParcelle Type => type;
        /// <summary>
        /// Accesseur en lecture de l'attribut unites.
        /// </summary>
        /// <value>La liste d'unités de la parcelle.</value>
        public List<Unite> Unites => unites;
        /// <summary>
        /// Accesseur en lecture du nombre d'unités.
        /// </summary>
        /// <value>La taille de la parcelle.</value>
        public int Taille => Unites.Count;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur de la classe <see cref="Parcelle"/>.
        /// </summary>
        /// <param name="type">Type de la parcelle.</param>
        /// <param name="unites">Liste des unités.</param>
        public Parcelle(TypeParcelle type, List<Unite> unites)
        {
            this.type = type;
            this.unites = unites;
        }
        /// <summary>
        /// Constructeur de la classe <see cref="Parcelle"/>.
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
        /// Ajoute une unitée à la liste.
        /// </summary>
        /// <param name="unite">Unitée de la parcelle.</param>
        public void Ajouter(Unite unite) => unites.Add(unite);
        /// <summary>
        /// Supprimer une unite noUnite.
        /// </summary>
        /// <param name="noUnite">Numéro de l'unitée.</param>
        /// <returns><see langword="true"/> si la suppression à réussie, <see langword="false"/> sinon.</returns>
        public bool Supprimer(int noUnite)
        {
            if (noUnite < 1 || noUnite > unites.Count)
            {
                Console.Write("Numéro d'unitée invalide");
                return false;
            }

            unites.RemoveAt(noUnite);
            return true;
        }
        #endregion
    }
}
