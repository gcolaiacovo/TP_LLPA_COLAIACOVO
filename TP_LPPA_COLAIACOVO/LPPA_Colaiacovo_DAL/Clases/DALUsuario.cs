using LPPA_Colaiacovo_DAL.Interfaces;
using LPPA_Colaiacovo_DAL.Utilidades;
using LPPA_Colaiacovo_Entidades.Clases;
using LPPA_Colaiacovo_Mapper.Clases;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace LPPA_Colaiacovo_DAL.Clases
{
    public class DALUsuario : IDALGenerica<Usuario>
    {
        private readonly MapperUsuario mapperUsuario;
        public DALUsuario()
        {
            this.mapperUsuario = new MapperUsuario();
        }

        public void Delete(int id)
        {
            var command = "DELETE FROM Usuario WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(SQLHelper.GetConnectionString()))
            {
                connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(command, connection))
                {
                    sqlCommand.Parameters.AddWithValue("@Id", id);
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        public Usuario Get(int id)
        {
            var command = "SELECT * FROM Usuario WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(SQLHelper.GetConnectionString()))
            {
                connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(command, connection))
                {
                    sqlCommand.Parameters.AddWithValue("@Id", id);
                    var reader = sqlCommand.ExecuteReader();
                    return mapperUsuario.Map(reader).FirstOrDefault();
                }
            }
        }

        public List<Usuario> GetAll()
        {
            var command = "SELECT * FROM Usuario";
            using (SqlConnection connection = new SqlConnection(SQLHelper.GetConnectionString()))
            {
                connection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(command, connection))
                {
                    var reader = sqlCommand.ExecuteReader();
                    return mapperUsuario.Map(reader);
                }
            }

        }

        public void Save(Usuario entity)
        {
            var command = entity.Id == 0
                ? "INSERT INTO Usuario (Nombre, Apellido, Email, Contrasena, FechaNacimiento, Rol, Activo, FechaCreado, FechaModificado) OUTPUT INSERTED.Id VALUES (@Nombre, @Apellido, @Email, @Contrasena, @FechaNacimiento, @Rol, @Activo, @FechaCreado, @FechaModificado)"
                : "UPDATE Usuario SET Nombre = @Nombre, Apellido = @Apellido, Email = @Email, Contrasena = @Contrasena, FechaNacimiento = @FechaNacimiento, Rol = @Rol, Activo = @Activo, FechaModificado = @FechaModificado WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(SQLHelper.GetConnectionString()))
            {
                connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(command, connection))
                {
                    sqlCommand.Parameters.AddWithValue("@Nombre", entity.Nombre);
                    sqlCommand.Parameters.AddWithValue("@Apellido", entity.Apellido);
                    sqlCommand.Parameters.AddWithValue("@Email", entity.Email);
                    sqlCommand.Parameters.AddWithValue("@Contrasena", entity.Contrasena);
                    sqlCommand.Parameters.AddWithValue("@FechaNacimiento", (object)entity.FechaNacimiento ?? DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@Rol", entity.Rol);
                    sqlCommand.Parameters.AddWithValue("@Activo", entity.Activo);
                    sqlCommand.Parameters.AddWithValue("@FechaCreado", entity.FechaCreado);
                    sqlCommand.Parameters.AddWithValue("@FechaModificado", (object)entity.FechaModificado ?? DBNull.Value);

                    if (entity.Id != 0)
                    {
                        sqlCommand.Parameters.AddWithValue("@Id", entity.Id);
                        sqlCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        var newId = sqlCommand.ExecuteScalar();
                        if (newId != null)
                        {
                            entity.Id = Convert.ToInt32(newId);
                        }
                    }
                }
            }
        }
    }
}
