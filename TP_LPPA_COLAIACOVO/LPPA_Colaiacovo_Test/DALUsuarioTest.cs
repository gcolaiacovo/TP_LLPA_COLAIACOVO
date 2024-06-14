using Microsoft.VisualStudio.TestTools.UnitTesting;
using LPPA_Colaiacovo_DAL.Clases;

namespace LPPA_Colaiacovo_Test
{
    [TestClass]
    public class DALUsuarioTest
    {
        private readonly DALUsuario DALUsuario;

        public DALUsuarioTest()
        {
            this.DALUsuario = new DALUsuario();
        }

        [TestMethod]
        public void TestMethod1()
        {
            var resultado = DALUsuario.GetAll();
        }
    }
}
