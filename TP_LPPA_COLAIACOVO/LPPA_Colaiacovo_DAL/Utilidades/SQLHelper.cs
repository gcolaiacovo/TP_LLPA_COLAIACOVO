﻿namespace LPPA_Colaiacovo_DAL.Utilidades
{
    public static class SQLHelper
    {
        public static string GetConnectionString()
        {
            return "Data Source=localhost;Initial Catalog=GColaiacovoLPPA;Integrated Security=True;";
            //return ConfigurationManager.ConnectionStrings["conexionDB"].ConnectionString;
        }
    }
}