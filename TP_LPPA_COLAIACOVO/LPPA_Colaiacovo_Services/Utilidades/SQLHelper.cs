using System.Configuration;

namespace LPPA_Colaiacovo_Services.Utilidades
{
    public static class SQLHelper
    {
        public static string GetConnectionString()
        {
            //return "Data Source=DESKTOP-QMDVD1K\\SQLEXPRESS;Initial Catalog=GColaiacovoLPPA;Integrated Security=True;";
            return ConfigurationManager.ConnectionStrings["conexionDB"].ConnectionString;
        }
    }
}
