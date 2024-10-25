using LPPA_Colaiacovo_Services.Utilidades;
using System;
using System.Data.SqlClient;
using System.IO;

namespace LPPA_Colaiacovo_Services
{
    public class DatabaseBackupService
    {
        public void CrearBackupBaseDeDatos()
        {
            var fechaHora = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var backupPath = $"C:\\backups\\backup.bak";

            if (!Directory.Exists("C:\\backups\\"))
            {
                Directory.CreateDirectory("C:\\backups\\");
            }

            var query = $"BACKUP DATABASE [GColaiacovoLPPA] TO DISK = '{backupPath}' WITH INIT";

            try
            {
                using (SqlConnection connection = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error al crear el backup: {ex.Message}");
            }
        }

        public bool CargarBackupBaseDeDatos()
        {
            try
            {
                var databaseName = "GColaiacovoLPPA";
                string sqlRestore = $@"
                        USE master;
                        ALTER DATABASE [{databaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                        RESTORE DATABASE [{databaseName}]
                        FROM DISK = 'C:\\backups\\backup.bak'
                        WITH REPLACE;
                        ALTER DATABASE [{databaseName}] SET MULTI_USER;";

                using (SqlConnection conn = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlRestore, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al restaurar desde el backup: {ex.Message}");
                return false;
            }
        }
    }
}
