using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Rhum_de_Guybrush
{
    public class Carte
    {
        #region Attributs
            
        /// <summary>
        /// Nom de la carte
        /// </summary>
        private string nom;
        
        /// <summary>
        /// Tableau contenant les parcelles qui composent une carte
        /// </summary>
        private Parcelle[] parcelles;
        
        /// <summary>
        /// tableau contenant les parcelles cultivables qui composent une carte
        /// </summary>
        private readonly Parcelle[] parcellesCultivable;
        #endregion

        #region Accesseur
        /// <summary>
        /// Accesseur en lecture de l'attribut nom.
        /// </summary>
        /// <value>Le nom de la carte.</value>
        public string Nom => nom;
        /// <summary>
        /// Accesseur en lecture de la liste parcelles.
        /// </summary>
        public Parcelle[] Parcelles => parcelles;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur de la classe <see cref="Carte"/>.
        /// </summary>
        /// <param name="parcelles">liste des parcelles de la carte.</param>
        public Carte(Parcelle[] parcelles)
        {
            this.parcelles = parcelles;

            // On on récupérer seulement les parcelles cultivable et on met dans parcellesCultivable
            var pList = parcelles.ToList();
            pList = pList.FindAll(x => x.Type == Parcelle.TypeParcelle.Normal);
            parcellesCultivable = pList.ToArray();
        }
        /// <summary>
        /// Constructeur de la classe <see cref="Carte"/>.
        /// </summary>
        /// <param name="parcelles">Chemin vers l'accesseur de parcelle.</param>
        public Carte(string chemin)
        {
            StreamReader fichierClair = null;
            Parcelle.TypeParcelle typeDeParcelle;
            int l = 0;
            int c;
            int num;
            string ligne;

            parcelles = new Parcelle['z' - 'a' + 2];
            parcellesCultivable = new Parcelle['z' - 'a'];
            nom = Path.GetFileNameWithoutExtension(chemin); // Récupéartion du nom du fichier source

            try
            {
                fichierClair = new StreamReader(chemin); // ouverture du fichier pour lecture

                // Lecture du fichier
                while ((ligne = fichierClair.ReadLine()) != null)
                {
                    c = 0;
                    foreach (var lettre in ligne)
                    {
                        switch (lettre)
                        {
                            // Récupération du nom et type de la parcelle
                            case 'M':
                                typeDeParcelle = Parcelle.TypeParcelle.Mer;
                                num = parcellesCultivable.Length + 0;
                                break;
                            case 'F':
                                typeDeParcelle = Parcelle.TypeParcelle.Foret;
                                num = parcellesCultivable.Length + 1;
                                break;
                            default:
                                typeDeParcelle = Parcelle.TypeParcelle.Normal;
                                num = lettre - 'a';

                                if (parcellesCultivable[num] == null)
                                    parcellesCultivable[num] = new Parcelle(typeDeParcelle);

                                parcellesCultivable[num].Ajouter(new Unite(c, l));
                                break;
                        }

                        if (parcelles[num] == null)
                            parcelles[num] = new Parcelle(typeDeParcelle);

                        // Ajoue de la parcelle
                        parcelles[num].Ajouter(new Unite(c, l));
                        c++;
                    }
                    l++;
                }

                fichierClair.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Échec du chargement de la carte");
                Console.WriteLine("Erreur : {0}", e.Message);
            }
            finally
            {
                if (fichierClair != null)
                    fichierClair.Close(); // fermeture du fichier
            }
        }
        #endregion


        #region Méthodes
        public void Affichage()
        {
            char lettre = 'a';
            char[][] tab = new char[10][];

            // Initialisation des colognes du tableau
            for (int i = 0; i < tab.Length; i++)
                tab[i] = new char[10];

            // Lecture des parcelles pour récuperer toutes les unités
            // et les mettres dans un tableau en 2 dimension
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
                        tab[unite.Y][unite.X] = nom;
                }
            }

            // Affichage du tableau en 2 dimension
            foreach (var l in tab)
            {
                foreach(var c in l)
                {
                    // Ajoue de couleur pour les types
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

            // Lecture de toutes les pacelles cultivable
            for (int i = 0; i < parcellesCultivable.Length; i++)
            {
                var parcelle = parcellesCultivable[i];

                if (parcelle != null)
                {
                    // Récupération du nom
                    char nom = parcelle.Type switch
                    {
                        Parcelle.TypeParcelle.Foret => 'F',
                        Parcelle.TypeParcelle.Mer => 'M',
                        _ => lettre++,
                    };

                    // Affichage
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

            // Lecture de toutes les pacelles cultivable
            for (int i = 0; i < parcellesCultivable.Length; i++)
            {
                var parcelle = parcellesCultivable[i];

                if (parcelle == null)
                    continue;

                char nom = (char)('a' + i); // Récupération du nom

                // Rechercher et affichage
                if (parcelle.Taille >= taille)
                {
                    resultat.Add(parcelle);
                    Console.WriteLine($"Parcelle {nom} : {parcelle.Taille} unites");
                }
            }

            // Si on a rien trouvais
            if (resultat.Count == 0)
                Console.WriteLine("Aucune parcelle");

            return resultat;
        }

        public int TailleParcelle(char nom)
        {
            int taille = 0;
            Parcelle parcelle = parcellesCultivable[nom - 'a'];

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

            foreach (var parcelle in parcellesCultivable)
                if (parcelle != null)
                {
                    moyenne += parcelle.Taille;
                    nbParcelles++;
                }

            // Calcul de la moyenne
            moyenne /= nbParcelles;
            moyenne = Math.Round(moyenne, 2);

            Console.WriteLine("Aire moyenne : " + moyenne);

            return moyenne;
        }
        #endregion
    }
}
