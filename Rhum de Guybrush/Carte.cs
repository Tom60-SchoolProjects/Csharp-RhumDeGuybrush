using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhum_de_Guybrush
{
    public class Carte
    {
        #region Attributs
        private string nom;
        private Parcelle[] parcelles;
        #endregion

        #region Accesseur
        public string Nom => nom;
        public Parcelle[] Parcelles => parcelles;
        #endregion

        #region Constructeur
        public Carte(Parcelle[] parcelles)
        {
            this.parcelles = parcelles;
        }

        public Carte(string chemin)
        {
            nom = Path.GetFileNameWithoutExtension(chemin);
            StreamReader fichierClair = new StreamReader(chemin);
            Parcelle.TypeParcelle typeDeParcelle;
            parcelles = new Parcelle[2 + 'z' - 'a'];
            int l = 0;
            int c;
            int num;
            string ligne;

            while ((ligne = fichierClair.ReadLine()) != null)
            {
                c = 0;
                foreach (var lettre in ligne)
                {
                    switch (lettre)
                    {
                        case 'M':
                            typeDeParcelle = Parcelle.TypeParcelle.Mer;
                            num = 0;
                            break;
                        case 'F':
                            typeDeParcelle = Parcelle.TypeParcelle.Foret;
                            num = 1;
                            break;
                        default:
                            typeDeParcelle = Parcelle.TypeParcelle.Normal;
                            num = 2 + lettre - 'a';
                            break;
                    }

                    if (parcelles[num] == null)
                        parcelles[num] = new Parcelle(typeDeParcelle);

                    parcelles[num].Ajouter(new Unite(l, c));
                    c++;
                }
                l++;
            }

            fichierClair.Close();
        }
        #endregion


        #region Méthodes
        public void Affichage()
        {
            char[][] tab = new char[10][];
            for (int i = 0; i < tab.Length; i++)
                tab[i] = new char[10];

            for (int i = 0; i < parcelles.Length; i++)
            {
                var parcelle = parcelles[i];
                char name = ObtenirNom(i);

                if (parcelle != null)
                    foreach (var unite in parcelle.Unites)
                    {
                        tab[unite.X][unite.Y] = name;
                    }
            }

            foreach(var l in tab)
            {
                foreach(var c in l)
                {
                    Console.Write(c);
                }
                Console.WriteLine();
            }
        }

        public void AffichageList()
        {
            for (int i = 0; i < parcelles.Length; i++)
            {
                var parcelle = parcelles[i];
                char nom = ObtenirNom(i);

                if (parcelle != null)
                    Console.WriteLine($"PARCELLE {nom} - {parcelle.TailleTotal()} unites");
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

        private char ObtenirNom(int i)
        {
            char nom = (char)(i - 2 + 'a');
            if (i == 0)
                nom = 'M';
            else if (i == 1)
                nom = 'F';
            return nom;
        }
        #endregion
    }
}
