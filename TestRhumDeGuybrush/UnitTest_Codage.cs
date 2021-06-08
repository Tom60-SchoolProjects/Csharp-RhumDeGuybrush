using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhum_de_Guybrush;

namespace TestRhumDeGuybrush
{
    [TestClass]
    public class UnitTest_Codage
    {
        [TestMethod]
        public void Decodage()
        {
            //Codage.Decodage(@"C:\Users\Tom60\OneDrive\Documents\École\DUT INFO 1\Concep Objet\C#\Rhum de Guybrush\Cartes\Phatt.chiffre");
        }

        [TestMethod]
        public void Encodage()
        {
            Codage.Encodage(new Carte(@"C:\Users\Tom60\OneDrive\Documents\École\DUT INFO 1\Concep Objet\C#\Rhum de Guybrush\Cartes\Phatt.clair"));
        }

        [TestMethod]
        public void LectureCarte()
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
        }

        [TestMethod]
        public void AffichageList_Carte()
        {
            var carte = new Carte(@"C:\Users\Tom60\OneDrive\Documents\École\DUT INFO 1\Concep Objet\C#\Rhum de Guybrush\Cartes\Phatt.clair");
            carte.AffichageList();
        }

        [TestMethod]
        public void Affichage_Carte()
        {
            var carte = new Carte(@"C:\Users\Tom60\OneDrive\Documents\École\DUT INFO 1\Concep Objet\C#\Rhum de Guybrush\Cartes\Phatt.clair");
            carte.Affichage();
        }
    }
}
