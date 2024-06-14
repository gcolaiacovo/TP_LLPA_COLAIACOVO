using Microsoft.VisualStudio.TestTools.UnitTesting;
using LPPA_Colaiacovo_DAL.Clases;
using LPPA_Colaiacovo_Services;

namespace LPPA_Colaiacovo_Test
{
    [TestClass]
    public class EncryptionServiceTest
    {
        public EncryptionServiceTest()
        {
        }

        [TestMethod]
        public void TestMethod1()
        {
            var contraseña = "carlosrodriguez123";
            var resultado = EncryptionService.Encriptar(contraseña);
        }
    }
}
