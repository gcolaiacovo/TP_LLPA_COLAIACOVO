using LPPA_Colaiacovo_DAL.Interfaces;
using LPPA_Colaiacovo_Entidades.Clases;
using LPPA_Colaiacovo_Mapper.Clases;
using LPPA_Colaiacovo_Services.Utilidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace LPPA_Colaiacovo_DAL.Clases
{
    public class DALBitacora : IDALGenerica<Bitacora>
    {
        private readonly MapperBitacora mapperBitacora;
        public DALBitacora()
        {
            this.mapperBitacora = new MapperBitacora();
        }

        public void Delete(int id)
        {
            var command = "DELETE FROM Bitacora WHERE Id = @Id";
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

        public Bitacora Get(int id)
        {
            var command = "SELECT * FROM Bitacora WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(SQLHelper.GetConnectionString()))
            {
                connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(command, connection))
                {
                    sqlCommand.Parameters.AddWithValue("@Id", id);
                    var reader = sqlCommand.ExecuteReader();
                    return mapperBitacora.Map(reader).FirstOrDefault();
                }
            }
        }

        public List<Bitacora> GetAll()
        {
            var command = "SELECT * FROM Bitacora";
            using (SqlConnection connection = new SqlConnection(SQLHelper.GetConnectionString()))
            {
                connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(command, connection))
                {
                    var reader = sqlCommand.ExecuteReader();
                    return mapperBitacora.Map(reader);
                }
            }
        }

        public void Save(Bitacora entity)
        {
            var command = entity.Id == 0 ?
                "INSERT INTO Bitacora (Descripcion, IdUsuario, Activo, FechaCreado, FechaModificado) VALUES (@Descripcion, @IdUsuario, @Activo, @FechaCreado, @FechaModificado)" :
                "UPDATE Bitacora SET Descripcion = @Descripcion, IdUsuario = @IdUsuario, Activo = @Activo, FechaCreado = @FechaCreado, FechaModificado = @FechaModificado WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(SQLHelper.GetConnectionString()))
            {
                connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(command, connection))
                {
                    sqlCommand.Parameters.AddWithValue("@Descripcion", entity.Descripcion);
                    sqlCommand.Parameters.AddWithValue("@IdUsuario", (object)entity.IdUsuario ?? DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@Activo", entity.Activo);
                    sqlCommand.Parameters.AddWithValue("@FechaCreado", entity.FechaCreado);
                    sqlCommand.Parameters.AddWithValue("@FechaModificado", (object)entity.FechaModificado ?? DBNull.Value);

                    if (entity.Id != 0)
                    {
                        sqlCommand.Parameters.AddWithValue("@Id", entity.Id);
                    }

                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
    }
}
