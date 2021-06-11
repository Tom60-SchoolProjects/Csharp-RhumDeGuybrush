using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhum_de_Guybrush;
using System;

namespace TestRhumDeGuybrush
{
    [TestClass]
    public class UnitTest_Carte
    {
        [TestMethod]
        public void Lecture()
        {
            var carte = new Carte(@"C:\Users\Tom60\OneDrive\Documents\École\DUT INFO 1\Concep Objet\C#\Rhum de Guybrush\Cartes\Phatt.clair");
            Assert.IsNotNull(carte);
            int nombres = 0;
            foreach (var parcelle in carte.Parcelles)
            {
                if (parcelle != null)
                    nombres += parcelle.Unites.Count;
            }
            Assert.AreEqual(100, nombres);

            carte = new Carte(@"C:\Users\Tom60\OneDrive\Documents\École\DUT INFO 1\Concep Objet\C#\Rhum de Guybrush\Cartes\Scabb.clair");
            Assert.IsNotNull(carte);
            nombres = 0;
            foreach (var parcelle in carte.Parcelles)
            {
                if (parcelle != null)
                    nombres += parcelle.Unites.Count;
            }
            Assert.AreEqual(100, nombres);
        }

        [TestMethod]
        public void Affichage()
        {
            Console.WriteLine("Phatt");
            var carte = new Carte(@"C:\Users\Tom60\OneDrive\Documents\École\DUT INFO 1\Concep Objet\C#\Rhum de Guybrush\Cartes\Phatt.clair");
            carte.Affiche();

            Console.WriteLine();
            Console.WriteLine("Scabb");

            carte = new Carte(@"C:\Users\Tom60\OneDrive\Documents\École\DUT INFO 1\Concep Objet\C#\Rhum de Guybrush\Cartes\Scabb.clair");
            carte.Affiche();
        }

        [TestMethod]
        public void AffichageList()
        {
            Console.WriteLine("Phatt");
            var carte = new Carte(@"C:\Users\Tom60\OneDrive\Documents\École\DUT INFO 1\Concep Objet\C#\Rhum de Guybrush\Cartes\Phatt.clair");
            carte.AfficheList();

            Console.WriteLine();
            Console.WriteLine("Scabb");

            carte = new Carte(@"C:\Users\Tom60\OneDrive\Documents\École\DUT INFO 1\Concep Objet\C#\Rhum de Guybrush\Cartes\Scabb.clair");
            carte.AfficheList();
        }

        [TestMethod]
        public void Recherche()
        {
            var carte = new Carte(@"C:\Users\Tom60\OneDrive\Documents\École\DUT INFO 1\Concep Objet\C#\Rhum de Guybrush\Cartes\Scabb.clair");
            var recherche = carte.Recherche(4);
            Assert.AreEqual(recherche.Count, 9);

            Console.WriteLine();

            recherche = carte.Recherche(12);
            Assert.AreEqual(recherche.Count, 0);
        }

        [TestMethod]
        public void TailleParcelle()
        {
            var carte = new Carte(@"C:\Users\Tom60\OneDrive\Documents\École\DUT INFO 1\Concep Objet\C#\Rhum de Guybrush\Cartes\Scabb.clair");
            var taille = carte.TailleParcelle('s');
            Assert.AreEqual(taille, 0);

            taille = carte.TailleParcelle('b');
            Assert.AreEqual(taille, 6);
        }

        [TestMethod]
        public void TailleMoyenne()
        {
            var carte = new Carte(@"C:\Users\Tom60\OneDrive\Documents\École\DUT INFO 1\Concep Objet\C#\Rhum de Guybrush\Cartes\Scabb.clair");
            var moyenne = carte.TailleMoyenne();

            Assert.AreEqual(moyenne, 3.76);
        }
    }
}
