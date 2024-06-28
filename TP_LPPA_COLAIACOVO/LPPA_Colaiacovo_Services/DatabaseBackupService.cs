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

                    // Primero, asegúrate de cerrar las conexiones de la base de datos antes de restaurar
                    string queryCloseConnections = $"ALTER DATABASE [GColaiacovoLPPA] SET SINGLE_USER WITH ROLLBACK IMMEDIATE";
                    using (SqlCommand commandClose = new SqlCommand(queryCloseConnections, connection))
                    {
                        commandClose.ExecuteNonQuery();
                    }

                    // Construye la consulta para restaurar desde el backup
                    string queryRestore = $"RESTORE DATABASE [GColaiacovoLPPA] FROM DISK = 'backup.bak' WITH REPLACE";

                    using (SqlCommand commandRestore = new SqlCommand(queryRestore, connection))
                    {
                        commandRestore.ExecuteNonQuery();
                    }

                    // Restaura el estado multiusuario
                    string querySetMultiUser = $"ALTER DATABASE [GColaiacovoLPPA] SET MULTI_USER";
                    using (SqlCommand commandMultiUser = new SqlCommand(querySetMultiUser, connection))
                    {
                        commandMultiUser.ExecuteNonQuery();
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
