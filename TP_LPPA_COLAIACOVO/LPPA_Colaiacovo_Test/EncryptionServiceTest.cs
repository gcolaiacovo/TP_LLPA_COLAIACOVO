using LPPA_Colaiacovo_Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LPPA_Colaiacovo_Test
{
    [TestClass]
    public class EncryptionServiceTest
    {
        public EncryptionServiceTest()
        {
        }

        [TestMethod]
        public void EncryptionService_Encriptar_Test1()
        {
            var contraseña = "carlosrodriguez123";
            var resultado = EncryptionService.Encriptar(contraseña);
        }

        [TestMethod]
        public void EncryptionService_Encriptar_Test2()
        {
            var contraseña = "carlosrodriguez123";
            var resultado = EncryptionService.Encriptar(contraseña);
        }
    }
}
