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
                Carte carte = new Carte(null);
                StreamReader streamReader = new StreamReader(chemin);
                string texte = streamReader.ReadToEnd();
                string[] lignes = texte.Split('|');
                string[] colonne;
                int frontier;
                int unite;
                bool zero;
                Parcelle.TypeParcelle typeParcelle;

                foreach (var ligne in lignes)
                {
                    colonne = ligne.Split(':');
                    foreach (var chiffre in colonne)
                    {
                        frontier = 0;
                        unite = Convert.ToInt32(chiffre);
                        zero = false;

                        if (unite > (int)SensFrontiere.Mer)
                        {
                            typeParcelle = Parcelle.TypeParcelle.Mer;
                            unite -= (int)SensFrontiere.Mer;
                        }
                        else if (unite > (int)SensFrontiere.Foret)
                        {
                            typeParcelle = Parcelle.TypeParcelle.Foret;
                            unite -= (int)SensFrontiere.Foret;
                        }

                        if (unite == 10)
                            Console.WriteLine("");

                        if (Valid(unite - (int)SensFrontiere.Est, out zero) && frontier < unite)
                            frontier += (int)SensFrontiere.Est;
                        if (!zero && Valid(unite - (int)SensFrontiere.Sud, out zero) && frontier < unite)
                            frontier += (int)SensFrontiere.Sud;
                        if (!zero && Valid(unite - (int)SensFrontiere.Ouest, out zero) && frontier < unite)
                            frontier += (int)SensFrontiere.Ouest;
                        if (!zero && Valid(unite - (int)SensFrontiere.Nord, out zero) && frontier < unite)
                            frontier += (int)SensFrontiere.Nord;

                        /*switch (unite)
                        {
                            case (int)SensFrontiere.Est:
                                frontier = (int)SensFrontiere.Est;
                                break;

                            case (int)SensFrontiere.Sud:
                                frontier = (int)SensFrontiere.Sud;
                                break;

                            case (int)SensFrontiere.Ouest:
                                frontier = (int)SensFrontiere.Ouest;
                                break;

                            case (int)SensFrontiere.Nord:
                                frontier = (int)SensFrontiere.Nord;
                                break;

                            default:
                                if (Valid(unite - (int)SensFrontiere.Est))
                                    frontier += (int)SensFrontiere.Est;
                                if (Valid(unite - (int)SensFrontiere.Sud))
                                    frontier += (int)SensFrontiere.Sud;
                                if (Valid(unite - (int)SensFrontiere.Ouest))
                                    frontier += (int)SensFrontiere.Ouest;
                                if (Valid(unite - (int)SensFrontiere.Nord))
                                    frontier += (int)SensFrontiere.Nord;
                                break;
                        }*/

                        if (unite != frontier)
                            Console.WriteLine(frontier);
                    }
                }

                return carte;
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
        public static void Encodage(string chemin)
        {
            try
            {
                StreamReader fichierClair = new StreamReader(chemin);

                
                fichierClair.Close();

                StreamWriter fichierChiffre = new StreamWriter(Path.ChangeExtension(chemin, ".chiffre"));

                fichierChiffre.Flush();
                fichierChiffre.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Échec du chiffrement de la carte");
                Console.WriteLine(e.Message);
                return;
            }
        }

        private static bool Valid(int num, out bool zero)
        {
            zero = false;
            if (num == 0)
            {
                zero = true;
                return true;
            }
            else if (num == 1 || num % 2 == 0)
                return true;
            else
                return false;
        }
        #endregion
    }
}
