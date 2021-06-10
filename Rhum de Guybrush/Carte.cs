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
        private Parcelle[] parcellesPlantable;
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

            parcelles = new Parcelle['z' - 'a' + 2];
            parcellesPlantable = new Parcelle['z' - 'a'];
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
                            num = parcellesPlantable.Length + 0;
                            break;
                        case 'F':
                            typeDeParcelle = Parcelle.TypeParcelle.Foret;
                            num = parcellesPlantable.Length + 1;
                            break;
                        default:
                            typeDeParcelle = Parcelle.TypeParcelle.Normal;
                            num = lettre - 'a';

                            if (parcellesPlantable[num] == null)
                                parcellesPlantable[num] = new Parcelle(typeDeParcelle);

                            parcellesPlantable[num].Ajouter(new Unite(l, c));
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
                    if (typeDeParcelle = Parcelle.TypeParcelle.Mer)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("{0}",c);
                    }
                    if (typeDeParcelle = Parcelle.TypeParcelle.Foret)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("{0}",c);
                    }
                    if (typeDeParcelle = Parcelle.TypeParcelle.Normal)
                    {
                        Console.Write("{0}",c);
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
            }
        }

        public void AffichageList()
        {
            for (int i = 0; i < parcellesPlantable.Length; i++)
            {
                var parcelle = parcellesPlantable[i];
                char nom = ObtenirNom(i);

                if (parcelle != null)
                {
                    Console.WriteLine($"PARCELLE {nom} - {parcelle.Taille} unites");
                    foreach (var unite in parcelle.Unites)
                        Console.Write($"({unite.X},{unite.Y})\t");
                    Console.WriteLine(Environment.NewLine);
                }
            }
        }

        public IReadOnlyList<Parcelle> Recherche(long taille)
        {
            List<Parcelle> resultat = new List<Parcelle>();

            Console.WriteLine($"Parcelles de taille supérieure à {taille} : ");
            for (int i = 0; i < parcellesPlantable.Length; i++)
            {
                var parcelle = parcellesPlantable[i];
                if (parcelle == null)
                    continue;
                char nom = ObtenirNom(i);

                if (parcelle.Taille >= taille)
                {
                    resultat.Add(parcelle);
                    Console.WriteLine($"Parcelle {nom} : {parcelle.Taille} unites");
                }
            }

            if (resultat.Count == 0)
                Console.WriteLine("Aucune parcelle");

            return resultat;
        }

        public int TailleParcelle(char nom)
        {
            int taille = 0;
            int test = nom - 'a';
            Parcelle parcelle = parcellesPlantable[nom - 'a'];

            if (parcelle == null)
                Console.WriteLine($"Parcelle {nom} : inexistante");
            else
                taille = parcelle.Taille;

            Console.WriteLine($"Taille de la parcelle {nom} : {taille} unites" + Environment.NewLine);
            return taille;
        }

        public double TailleMoyenne()
        {
            double moyenne = 0;
            int nbParcelles = 0;

            foreach (var parcelle in parcellesPlantable)
                if (parcelle != null)
                {
                    moyenne += parcelle.Taille;
                    nbParcelles++;
                }

            moyenne /= nbParcelles;

            moyenne = Math.Round(moyenne, 2);

            Console.WriteLine("Aire moyenne : " + moyenne);

            return moyenne;
        }

        private static char ObtenirNom(int i)
        {
            char nom = (char)('a' + i);

            if (i == 'z' - 'a' + 0)
                nom = 'M';
            else if (i == 'z' - 'a' + 1)
                nom = 'F';
            return nom;
        }
        #endregion
    }
}
