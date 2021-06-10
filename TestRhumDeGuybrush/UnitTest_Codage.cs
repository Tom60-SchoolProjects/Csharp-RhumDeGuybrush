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
    }
}
