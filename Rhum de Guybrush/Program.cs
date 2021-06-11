using System;

namespace Rhum_de_Guybrush
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Phatt");
            var carte = new Carte(@"C:\Users\Tom60\OneDrive\Documents\École\DUT INFO 1\Concep Objet\C#\Rhum de Guybrush\Cartes\Phatt.clair");
            carte.Affichage();
            carte.AffichageList();
            carte.Recherche(4);
            carte.TailleParcelle('b');
            carte.TailleMoyenne();

            Console.WriteLine("Phatt codé et décodé");
            Codage.Encodage(carte);
            carte = Codage.Decodage("Phatt.chiffre");
            carte.Affichage();
            carte.AffichageList();
            carte.Recherche(4);
            carte.TailleParcelle('b');
            carte.TailleMoyenne();

            Console.WriteLine();
            Console.WriteLine("Scabb");

            carte = new Carte(@"C:\Users\Tom60\OneDrive\Documents\École\DUT INFO 1\Concep Objet\C#\Rhum de Guybrush\Cartes\Scabb.clair");
            carte.Affichage();
            carte.AffichageList();
            carte.Recherche(4);
            carte.TailleParcelle('b');
            carte.TailleMoyenne();

            Console.WriteLine("Phatt codé et décodé");
            Codage.Encodage(carte);
            carte = Codage.Decodage("Scabb.chiffre");
            carte.Affichage();
            carte.AffichageList();
            carte.Recherche(4);
            carte.TailleParcelle('b');
            carte.TailleMoyenne();
        }
    }
}
