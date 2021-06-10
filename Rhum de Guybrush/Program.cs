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

            Console.WriteLine();
            Console.WriteLine("Scabb");

            carte = new Carte(@"C:\Users\Tom60\OneDrive\Documents\École\DUT INFO 1\Concep Objet\C#\Rhum de Guybrush\Cartes\Scabb.clair");
            carte.Affichage();
        }
    }
}
