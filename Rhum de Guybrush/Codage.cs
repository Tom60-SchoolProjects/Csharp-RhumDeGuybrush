using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Foret = 32,
            Mer = 64,
        }
        #endregion

        #region Attributs
        #endregion

        #region Accesseur
        #endregion

        #region Constructeur
        #endregion

        #region Méthodes
        /// <summary>
        /// Permettre de lire une carte chiffrée contenue dans un fichier «point chiffre» pour la charger en mémoire.
        /// </summary>
        /// <param name="chemin"></param>
        /// <returns></returns>
        public static Carte Decodage(string chemin)
        {
            try
            {
                StreamReader streamReader = new StreamReader(chemin);
                string texte = streamReader.ReadToEnd();
                string[] lignes = texte.Split('|', StringSplitOptions.RemoveEmptyEntries);
                string[] colonne;
                int nombre,cPrevious;
                bool bordureEst = false, bordureSud=false, bordureOuest=false, bordureNord=false;
                List<Parcelle> parcelles = new List<Parcelle>();
                List<Unite> unites = new List<Unite>();
                Parcelle.TypeParcelle typeParcelle;

                //unites.Clear();
                for (var l = 0; l < 10 && bordureSud;)
                {
                    var ligne = lignes[l];
                    colonne = ligne.Split(':');
                    for (var c = 0; c < 10 && bordureEst; c++)
                    {
                        unites.Add(new Unite(l, c));
                        bordureEst = bordureSud = bordureOuest = bordureNord = false;
                        var chiffre = colonne[c];
                        nombre = Convert.ToInt32(chiffre);

                        if (nombre >= (int)SensFrontiere.Mer)
                        {
                            typeParcelle = Parcelle.TypeParcelle.Mer;
                            nombre -= (int)SensFrontiere.Mer;
                        }
                        else if (nombre >= (int)SensFrontiere.Foret)
                        {
                            typeParcelle = Parcelle.TypeParcelle.Foret;
                            nombre -= (int)SensFrontiere.Foret;
                        }

                        if (nombre >= (int)SensFrontiere.Est)
                        {
                            nombre -= (int)SensFrontiere.Est;
                            bordureEst = true;
                        }

                        if (nombre >= (int)SensFrontiere.Sud)
                        {
                            nombre -= (int)SensFrontiere.Sud;
                            bordureSud = true;
                        }
                        if (nombre >= (int)SensFrontiere.Ouest)
                        {
                            nombre -= (int)SensFrontiere.Ouest;
                            bordureOuest = true;
                        }

                        if (nombre >= (int)SensFrontiere.Nord)
                        {
                            nombre -= (int)SensFrontiere.Nord;
                            bordureNord = true;
                        }
                        if (bordureEst)
                        {
                            cPrevious=c;
                        }
                        if (bordureEst && bordureSud)
                        {

                        }
                        // Trouver à quelle Parcelle ça appartient ? -> A chaque frontière EST on saute de ligne
                        // Trouver ces coordonnées ?
                        // Verifier chaque frontières Est et le mettre dans "parcelles[0]"-> parcelle n°1, "parcelles[1]"-> parcelle n°2
                        
                    }
                }
                // parcelles.Add(new Parcelle(typeParcelle, unites));

                return new Carte(parcelles.ToArray());
            }
            catch (Exception e)
            {
                Console.WriteLine("Échec du chargement de la carte");
                Console.WriteLine(e.Message);
                return null;
            }
        }

        /// <summary>
        /// Coder une carte claire afin d’obtenir une carte chiffrée.
        /// </summary>
        /// <param name="carte"></param>
        public static void Encodage(Carte carte)
        {
            int[][] tab = new int[10][];
            StreamWriter fichierChiffre = null;
            bool debut;

            // Initialisation des colognes du tableau
            for (int i = 0; i < tab.Length; i++)
                tab[i] = new int[10];

            // Calcul des fontier et stocke dans le tablea
            for (int i = 0; i < carte.Parcelles.Length; i++)
            {
                var parcelle = carte.Parcelles[i];

                if (parcelle != null)
                    foreach (var unite in parcelle.Unites)
                    {
                        int sensFrontier = 0;

                        foreach (var sousUnite in parcelle.Unites) // 1,1
                        {
                            // Y = Ligne, X = Cologne
                            if (unite.Y == sousUnite.Y && unite.X + 1 == sousUnite.X) // 1.2
                                sensFrontier += (int)SensFrontiere.Est; // 8
                            if (unite.Y == sousUnite.Y && unite.X - 1 == sousUnite.X) // 1,0
                                sensFrontier += (int)SensFrontiere.Ouest; // 2
                            if (unite.Y + 1 == sousUnite.Y && unite.X == sousUnite.X) // 2.1
                                sensFrontier += (int)SensFrontiere.Sud; // 4
                            if (unite.Y - 1 == sousUnite.Y && unite.X == sousUnite.X) //0,1
                                sensFrontier += (int)SensFrontiere.Nord; // 1
                        }

                        sensFrontier = ((int)SensFrontiere.Est + (int)SensFrontiere.Ouest + (int)SensFrontiere.Sud + (int)SensFrontiere.Nord) - sensFrontier;
                        sensFrontier += (int)parcelle.Type;
                        tab[unite.Y][unite.X] = sensFrontier;
                    }
            }

            // Convertion du tableau dans le ficher chiffre
            try
            {
                fichierChiffre = new StreamWriter(carte.Nom + ".chiffre");

                foreach (var l in tab)
                {
                    debut = true;
                    foreach (var c in l)
                    {
                        if (!debut)
                            fichierChiffre.Write(':');
                        fichierChiffre.Write(c);
                        debut = false;
                    }
                    fichierChiffre.Write('|');
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
                if (fichierChiffre != null)
                    fichierChiffre.Close(); // fermeture du fichier
            }
        }
        #endregion
    }
}
