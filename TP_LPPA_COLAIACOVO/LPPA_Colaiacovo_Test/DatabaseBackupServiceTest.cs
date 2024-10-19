using Microsoft.VisualStudio.TestTools.UnitTesting;
using LPPA_Colaiacovo_DAL.Clases;
using LPPA_Colaiacovo_Services;

namespace LPPA_Colaiacovo_Test
{
    [TestClass]
    public class DatabaseBackupServiceTest
    {
        private readonly DatabaseBackupService databaseBackupService;
        public DatabaseBackupServiceTest()
        {
            databaseBackupService = new DatabaseBackupService();
        }

        [TestMethod]
        public void TestMethod1()
        {
            databaseBackupService.CargarBackupBaseDeDatos();
        }
    }
}
