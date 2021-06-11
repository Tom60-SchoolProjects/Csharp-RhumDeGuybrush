﻿using System;
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
        private readonly string nom;
        private readonly Parcelle[] parcelles;
        private readonly Parcelle[] parcellesPlantable;
        #endregion

        #region Accesseur
        public string Nom => nom;
        public Parcelle[] Parcelles => parcelles;
        #endregion

        #region Constructeur
        public Carte(Parcelle[] parcelles)
        {
            this.parcelles = parcelles;

            // On on enléve les parcelle qui sont pas plantable et on met dans parcellesPlantable
            var pList = parcelles.ToList();
            pList = pList.FindAll(x => x.Type == Parcelle.TypeParcelle.Normal);
            parcellesPlantable = pList.ToArray();
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

                            parcellesPlantable[num].Ajouter(new Unite(c, l));
                            break;
                    }

                    if (parcelles[num] == null)
                        parcelles[num] = new Parcelle(typeDeParcelle);

                    parcelles[num].Ajouter(new Unite(c, l));
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
            char lettre = 'a';
            char[][] tab = new char[10][];
            for (int i = 0; i < tab.Length; i++)
                tab[i] = new char[10];

            for (int i = 0; i < parcelles.Length; i++)
            {
                var parcelle = parcelles[i];
                if (parcelle != null)
                {
                    char nom = parcelle.Type switch
                    {
                        Parcelle.TypeParcelle.Foret => 'F',
                        Parcelle.TypeParcelle.Mer => 'M',
                        _ => lettre++,
                    };
                    foreach (var unite in parcelle.Unites)
                    {
                        tab[unite.Y][unite.X] = nom;
                    }
                }
            }

            foreach (var l in tab)
            {
                foreach(var c in l)
                {
                    switch (c)
                    {
                        case 'M':
                            Console.ForegroundColor = ConsoleColor.Blue;
                            break;

                        case 'F':
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                    }

                    Console.Write("{0}", c);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
            }
        }

        public void AffichageList()
        {
            char lettre = 'a';
            for (int i = 0; i < parcellesPlantable.Length; i++)
            {
                var parcelle = parcellesPlantable[i];

                if (parcelle != null)
                {
                    char nom = parcelle.Type switch
                    {
                        Parcelle.TypeParcelle.Foret => 'F',
                        Parcelle.TypeParcelle.Mer => 'M',
                        _ => lettre++,
                    };

                    Console.WriteLine($"PARCELLE {nom} - {parcelle.Taille} unites");
                    foreach (var unite in parcelle.Unites)
                        Console.Write($"({unite.Y},{unite.X})\t");
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
                char nom = (char)('a' + i);

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
        #endregion
    }
}
