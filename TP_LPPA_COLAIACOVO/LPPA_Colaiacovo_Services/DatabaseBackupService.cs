using LPPA_Colaiacovo_Services.Utilidades;
using System;
using System.Data.SqlClient;

namespace LPPA_Colaiacovo_Services
{
    public class DatabaseBackupService
    {
        public void CrearBackupBaseDeDatos()
        {
            var query = $"BACKUP DATABASE [GColaiacovoLPPA] TO DISK = 'C:\\backups\\backup.bak' WITH INIT";
            using (SqlConnection connection = new SqlConnection(SQLHelper.GetConnectionString()))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public bool CargarBackupBaseDeDatos()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    connection.Open();

                    // Desconecta todas las conexiones activas a la base de datos
                    string queryKillConnections = @"
                ALTER DATABASE [GColaiacovoLPPA] SET SINGLE_USER WITH ROLLBACK IMMEDIATE";

                    using (SqlCommand commandKillConnections = new SqlCommand(queryKillConnections, connection))
                    {
                        commandKillConnections.ExecuteNonQuery();
                    }

                    // Cambia el contexto a la base de datos 'master'
                    string queryUseMaster = "USE master";
                    using (SqlCommand commandUseMaster = new SqlCommand(queryUseMaster, connection))
                    {
                        commandUseMaster.ExecuteNonQuery();
                    }

                    // Construye la consulta para restaurar desde el backup
                    string queryRestore = @"
                RESTORE DATABASE [GColaiacovoLPPA] 
                FROM DISK = 'C:\\backups\\backup.bak' 
                WITH REPLACE";

                    using (SqlCommand commandRestore = new SqlCommand(queryRestore, connection))
                    {
                        commandRestore.ExecuteNonQuery();
                    }

                    // Vuelve a poner la base de datos en modo multiusuario
                    string querySetMultiUser = @"
                ALTER DATABASE [GColaiacovoLPPA] SET MULTI_USER";

                    using (SqlCommand commandSetMultiUser = new SqlCommand(querySetMultiUser, connection))
                    {
                        commandSetMultiUser.ExecuteNonQuery();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al restaurar desde el backup: {ex.Message}");
                return false;
            }
        }
    }
}
