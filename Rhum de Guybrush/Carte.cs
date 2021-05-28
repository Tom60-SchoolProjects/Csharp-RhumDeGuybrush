using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhum_de_Guybrush
{
    public class Carte
    {
        #region Attributs
        Parcelle[] parcelles;
        #endregion

        #region Accesseur
        public Parcelle[] Parcelles => parcelles;
        #endregion

        #region Constructeur
        public Carte(Parcelle[] parcelles)
        {
            this.parcelles = parcelles;
        }
        #endregion


        #region Méthodes
        public void Affichage()
        {

        }

        public void AffichageList()
        {
            foreach (var parcelle in parcelles)
            {
                Console.WriteLine($"PARCELLE {parcelle.Nom} - {parcelle.TailleTotal()} unites");
            }
        }

        public void Recherche(long taille)
        {
            Console.WriteLine("Parcelles de taille supérieure à " + taille);
            foreach (var parcelle in parcelles)
            {
                if (parcelle.TailleTotal() >= taille)
                {

                }
            }
        }
        #endregion
    }
}
