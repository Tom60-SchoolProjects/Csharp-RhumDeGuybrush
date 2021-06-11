using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Rhum_de_Guybrush
{
    public static class Codage
    {
        #region Enumerations
        public enum SensFrontiere
        {
            Nord = 1,
            Ouest = 2,
            Sud = 4,
            Est = 8,
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Permettre de lire une carte chiffrée contenue dans un fichier «point chiffre» pour la charger en mémoire.
        /// </summary>
        /// <param name="chemin"></param>
        /// <returns></returns>
        public static Carte Decodage(string chemin)
        {
            int[][] tab = new int[10][];
            StreamReader fichierChiffre = null;
            List<Parcelle> parcelles = new List<Parcelle>();
            List<Unite> unites;
            Parcelle.TypeParcelle typeParcelle;

            // Initialisation des colognes du tableau
            for (int i = 0; i < tab.Length; i++)
                tab[i] = new int[10];

            try
            {
                fichierChiffre = new StreamReader(chemin); // ouverture du fichier pour lecture
                string texte = fichierChiffre.ReadToEnd();
                string[] lignes = texte.Split('|', StringSplitOptions.RemoveEmptyEntries);
                string[] colonnes;

                // Convertion du texte en tableau à deux dimenssion
                for (var l = 0; l < lignes.Length; l++)
                {
                    var ligne = lignes[l];
                    colonnes = ligne.Split(':');
                    for (var c = 0; c < colonnes.Length; c++)
                    {
                        string colonne = colonnes[c];
                        int chiffre = Convert.ToInt32(colonne);
                        tab[l][c] = chiffre;
                    }
                }

                // Recherche de toutes les parcelles
                boucle:
                for (var l = 0; l < tab.Length; l++)
                {
                    var colonnes2 = tab[l];
                    for (var c = 0; c < colonnes2.Length; c++)
                        if (colonnes2[c] != 0)
                        {
                            unites = TrouverUnite(ref tab, new Unite(c, l), out typeParcelle);
                            unites = unites.Distinct().ToList(); // enlève les doublons
                            parcelles.Add(new Parcelle(typeParcelle, unites));
                            goto boucle;
                        }
                }

                return new Carte(parcelles.ToArray());
            }
            catch (Exception e)
            {
                Console.WriteLine("Échec du déchiffrage de la carte");
                Console.WriteLine("Erreur : {0}", e.Message);
                return null;
            }
            finally
            {
                if (fichierChiffre != null)
                    fichierChiffre.Close(); // fermeture du fichier
            }
        }

        private static List<Unite> TrouverUnite(ref int[][] tab, Unite debut, out Parcelle.TypeParcelle typeParcelle)
        {
            int frontier = tab[debut.Y][debut.X];
            List<Unite> unites = new List<Unite>();
            typeParcelle = Parcelle.TypeParcelle.Normal;
            tab[debut.Y][debut.X] = 0;

            if (frontier != 0) // Traité l'unité s'il n'a pas déjà été traité
            {
                unites.Add(debut);

                // Récupérer la frontier
                if (frontier >= (int)Parcelle.TypeParcelle.Mer)
                    typeParcelle = Parcelle.TypeParcelle.Mer;
                else if (frontier >= (int)Parcelle.TypeParcelle.Foret)
                    typeParcelle = Parcelle.TypeParcelle.Foret;

                frontier -= (int)typeParcelle;

                // Ajouter les unités adjacentes
                if (frontier >= (int)SensFrontiere.Est)
                    frontier -= (int)SensFrontiere.Est;
                else
                    unites.AddRange(TrouverUnite(ref tab, new Unite(debut.X + 1, debut.Y), out _));

                if (frontier >= (int)SensFrontiere.Sud)
                    frontier -= (int)SensFrontiere.Sud;
                else
                    unites.AddRange(TrouverUnite(ref tab, new Unite(debut.X, debut.Y + 1), out _));

                if (frontier >= (int)SensFrontiere.Ouest)
                    frontier -= (int)SensFrontiere.Ouest;
                else
                    unites.AddRange(TrouverUnite(ref tab, new Unite(debut.X - 1, debut.Y), out _));

                if (frontier >= (int)SensFrontiere.Nord)
                    frontier -= (int)SensFrontiere.Nord;
                else
                    unites.AddRange(TrouverUnite(ref tab, new Unite(debut.X, debut.Y - 1), out _));
            }

            return unites;
        }

        /// <summary>
        /// Coder une carte claire afin d’obtenir une carte chiffrée.
        /// </summary>
        /// <param name="carte"></param>
        public static void Encodage(Carte carte)
        {
            int[][] tab = new int[10][];
            StreamWriter fichierClair = null;
            bool debut;

            // Initialisation des colognes du tableau
            for (int i = 0; i < tab.Length; i++)
                tab[i] = new int[10];

            // Calcul des frontières et stocke dans le tableau
            for (int i = 0; i < carte.Parcelles.Length; i++)
            {
                var parcelle = carte.Parcelles[i];

                if (parcelle != null)
                    foreach (var unite in parcelle.Unites)
                    {
                        int sensFrontier = 0;

                        foreach (var sousUnite in parcelle.Unites)
                        {
                            if (unite.Y == sousUnite.Y && unite.X + 1 == sousUnite.X)
                                sensFrontier += (int)SensFrontiere.Est;
                            if (unite.Y == sousUnite.Y && unite.X - 1 == sousUnite.X)
                                sensFrontier += (int)SensFrontiere.Ouest;
                            if (unite.Y + 1 == sousUnite.Y && unite.X == sousUnite.X)
                                sensFrontier += (int)SensFrontiere.Sud;
                            if (unite.Y - 1 == sousUnite.Y && unite.X == sousUnite.X)
                                sensFrontier += (int)SensFrontiere.Nord;
                        }

                        sensFrontier = ((int)SensFrontiere.Est + (int)SensFrontiere.Ouest + (int)SensFrontiere.Sud + (int)SensFrontiere.Nord) - sensFrontier;
                        sensFrontier += (int)parcelle.Type;
                        tab[unite.Y][unite.X] = sensFrontier;
                    }
            }

            // Convertion du tableau dans le ficher chiffre
            try
            {
                fichierClair = new StreamWriter(carte.Nom + ".chiffre"); // ouverture du fichier pour écriture

                foreach (var l in tab)
                {
                    debut = true;
                    foreach (var c in l)
                    {
                        if (!debut)
                            fichierClair.Write(':');
                        fichierClair.Write(c);
                        debut = false;
                    }
                    fichierClair.Write('|');
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Échec du chiffrement de la carte");
                Console.WriteLine("Erreur : {0}", e.Message);
                return;
            }
            finally
            {
                if (fichierClair != null)
                    fichierClair.Close(); // fermeture du fichier
            }
        }
        #endregion
    }
}
