using System;
using System.IO;

namespace Rhum_de_Guybrush
{
    class Program
    {
        static void Main(string[] args)
        {
            Carte carte;

            if (args.Length == 0)
            {
                Console.WriteLine("Utilisation : \"Rhum de Guybrush\" [Fichier]");
                Console.WriteLine("Ou vous pouvez déplacer une carte sur l'exécutable");
            }

            foreach (var arg in args)
            {
                if (File.Exists(arg))
                {
                    string extension = Path.GetExtension(arg);
                    switch (extension)
                    {
                        case ".chiffre":
                            carte = Codage.Decodage(arg);
                            Console.WriteLine("Decodage réussie");

                            carte.Affiche();
                            break;

                        case ".clair":
                            carte = new Carte(arg);
                            if (Codage.Encodage(carte))
                            {
                                Console.WriteLine("Encodage réussie et sauvegardé dans : ");
                                Console.WriteLine(Path.GetFullPath(carte.Nom + ".chiffre"));
                            }
                            carte.Affiche();
                            break;

                        default:
                            Console.WriteLine("Fichier inconnue");
                            break;
                    }
                }
                else
                    Console.WriteLine("Fichier non trouvais");
            }

            /*
            Console.WriteLine("Phatt");
            var carte = new Carte(@"C:\Users\Tom60\OneDrive\Documents\École\DUT INFO 1\Concep Objet\C#\Rhum de Guybrush\Cartes\Phatt.clair");
            carte.Affiche();
            carte.AfficheList();
            carte.Recherche(4);
            carte.TailleParcelle('b');
            carte.TailleMoyenne();

            Console.WriteLine("Phatt codé et décodé");
            Codage.Encodage(carte);
            carte = Codage.Decodage("Phatt.chiffre");
            carte.Affiche();
            carte.AfficheList();
            carte.Recherche(4);
            carte.TailleParcelle('b');
            carte.TailleMoyenne();

            Console.WriteLine();
            Console.WriteLine("Scabb");

            carte = new Carte(@"C:\Users\Tom60\OneDrive\Documents\École\DUT INFO 1\Concep Objet\C#\Rhum de Guybrush\Cartes\Scabb.clair");
            carte.Affiche();
            carte.AfficheList();
            carte.Recherche(4);
            carte.TailleParcelle('b');
            carte.TailleMoyenne();

            Console.WriteLine("Phatt codé et décodé");
            Codage.Encodage(carte);
            carte = Codage.Decodage("Scabb.chiffre");
            carte.Affiche();
            carte.AfficheList();
            carte.Recherche(4);
            carte.TailleParcelle('b');
            carte.TailleMoyenne();*/

            Console.WriteLine();
            Console.WriteLine("Appuyer sur n'importe quelle touche pour fermer le programme");
            Console.ReadKey();
        }
    }
}
