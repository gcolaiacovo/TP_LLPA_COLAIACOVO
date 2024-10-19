using Microsoft.VisualStudio.TestTools.UnitTesting;
using LPPA_Colaiacovo_BLL.Clases;

namespace LPPA_Colaiacovo_Test
{
    [TestClass]
    public class BLLUsuarioTest
    {
        private readonly BLLUsuario BLLUsuario;

        public BLLUsuarioTest()
        {
            this.BLLUsuario = new BLLUsuario();
        }

        [TestMethod]
        public void CalcularDigitoVerificadorTest()
        {
            var usuario = BLLUsuario.GetUsuario(3);

            var resultado = BLLUsuario.CalcularChecksum(usuario);
         }
    }
}
