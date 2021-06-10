using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhum_de_Guybrush;
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

            Assert.AreEqual(carte.Parcelles.Length, carteOrigin.Parcelles.Length);
            Assert.AreEqual(carte.TailleMoyenne(), carteOrigin.TailleMoyenne());
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
