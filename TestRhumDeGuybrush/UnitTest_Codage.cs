using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhum_de_Guybrush;
using System;
using System.IO;

namespace TestRhumDeGuybrush
{
    [TestClass]
    public class UnitTest_Codage
    {
        [TestMethod]
        public void Decodage()
        {
            Carte carte = Codage.Decodage(@"C:\Users\Tom60\OneDrive\Documents\École\DUT INFO 1\Concep Objet\C#\Rhum de Guybrush\Cartes\Phatt.chiffre");
            Carte carteOrigin = new Carte(@"C:\Users\Tom60\OneDrive\Documents\École\DUT INFO 1\Concep Objet\C#\Rhum de Guybrush\Cartes\Phatt.clair");

            /* Debug
             * // Affichage
            Console.WriteLine("Decodage");
            carte.Affichage();

            Console.WriteLine();

            Console.WriteLine("Origin");
            carteOrigin.Affichage();

            Console.WriteLine(Environment.NewLine);

            // Affichage list
            Console.WriteLine("Decodage");
            carte.AffichageList();

            Console.WriteLine();

            Console.WriteLine("Origin");
            carteOrigin.AffichageList();*/

            int tailleCarte = 0;
            foreach (var parcelle in carte.Parcelles)
                if (parcelle != null)
                    tailleCarte++;

            int tailleCarteOrigin = 0;
            foreach (var parcelle in carteOrigin.Parcelles)
                if (parcelle != null)
                    tailleCarteOrigin++;


            Assert.AreEqual(tailleCarte, tailleCarteOrigin);
            Assert.AreEqual(carte.TailleMoyenne(), carteOrigin.TailleMoyenne());

            var recherche = carte.Recherche(4);
            Assert.AreEqual(recherche.Count, 9);

            Console.WriteLine();

            recherche = carte.Recherche(12);
            Assert.AreEqual(recherche.Count, 0);
        }

        [TestMethod]
        public void Encodage()
        {
            Codage.Encodage(new Carte(@"C:\Users\Tom60\OneDrive\Documents\École\DUT INFO 1\Concep Objet\C#\Rhum de Guybrush\Cartes\Phatt.clair"));

            string origin = File.ReadAllText(@"C:\Users\Tom60\OneDrive\Documents\École\DUT INFO 1\Concep Objet\C#\Rhum de Guybrush\Cartes\Phatt.chiffre");
            string encodage = File.ReadAllText("Phatt.chiffre");

            Assert.AreEqual(origin, encodage);
        }
    }
}
